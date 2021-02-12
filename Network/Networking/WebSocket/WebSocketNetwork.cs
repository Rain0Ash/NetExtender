using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;
using NetExtender.Network.Networking.Http;
using NetExtender.Network.Networking.WebSocket.Interfaces;
using NetExtender.Random.Interfaces;
using NetExtender.Utils.Numerics;

namespace NetExtender.Network.Networking.WebSocket
{
    /// <summary>
    ///     WebSocket utility class
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    public class WebSocketNetwork : IWebSocketNetwork
    {
        /// <summary>
        ///     Final frame
        /// </summary>
        public const Byte WsFin = 0x80;

        /// <summary>
        ///     Text frame
        /// </summary>
        public const Byte WsText = 0x01;

        /// <summary>
        ///     Binary frame
        /// </summary>
        public const Byte WsBinary = 0x02;

        /// <summary>
        ///     Close frame
        /// </summary>
        public const Byte WsClose = 0x08;

        /// <summary>
        ///     Ping frame
        /// </summary>
        public const Byte WsPing = 0x09;

        /// <summary>
        ///     Pong frame
        /// </summary>
        public const Byte WsPong = 0x0A;

        private readonly IWebSocketNetwork _wsHandler;

        /// <summary>
        ///     Receive buffer
        /// </summary>
        internal readonly List<Byte> WsReceiveBuffer = new List<Byte>();

        /// <summary>
        ///     Receive buffer lock
        /// </summary>
        internal readonly Object WsReceiveLock = new Object();

        /// <summary>
        ///     Receive mask
        /// </summary>
        internal readonly Byte[] WsReceiveMask = new Byte[4];

        /// <summary>
        ///     Send buffer
        /// </summary>
        internal readonly List<Byte> WsSendBuffer = new List<Byte>();

        /// <summary>
        ///     Send buffer lock
        /// </summary>
        internal readonly Object WsSendLock = new Object();

        /// <summary>
        ///     Send mask
        /// </summary>
        internal readonly Byte[] WsSendMask = new Byte[4];

        /// <summary>
        ///     Handshaked flag
        /// </summary>
        internal Boolean WsHandshaked;

        /// <summary>
        ///     Received frame header size
        /// </summary>
        internal Int32 WsHeaderSize;

        /// <summary>
        ///     Received frame payload size
        /// </summary>
        internal Int32 WsPayloadSize;

        /// <summary>
        ///     Received frame flag
        /// </summary>
        internal Boolean WsReceived;

        public WebSocketNetwork(IWebSocketNetwork wsHandler)
        {
            _wsHandler = wsHandler;
            ClearWsBuffers();
        }

        /// <summary>
        ///     Perform WebSocket client upgrade
        /// </summary>
        /// <param name="response">WebSocket upgrade HTTP response</param>
        /// <param name="id">WebSocket client Id</param>
        /// <returns>'true' if the WebSocket was successfully upgrade, 'false' if the WebSocket was not upgrade</returns>
        public Boolean PerformClientUpgrade(HttpNetworkResponse response, Guid id)
        {
            if (response.Status != 101)
            {
                return false;
            }

            Boolean error = false;
            Boolean accept = false;
            Boolean connection = false;
            Boolean upgrade = false;

            // Validate WebSocket handshake headers
            for (Int32 i = 0; i < response.Headers; ++i)
            {
                Tuple<String, String> header = response.Header(i);
                String key = header.Item1;
                String value = header.Item2;

                if (key == "Connection")
                {
                    if (value != "Upgrade")
                    {
                        error = true;
                        _wsHandler.OnWsError("Invalid WebSocket handshaked response: 'Connection' header value must be 'Upgrade'");
                        break;
                    }

                    connection = true;
                }
                else if (key == "Upgrade")
                {
                    if (value != "websocket")
                    {
                        error = true;
                        _wsHandler.OnWsError("Invalid WebSocket handshaked response: 'Upgrade' header value must be 'websocket'");
                        break;
                    }

                    upgrade = true;
                }
                else if (key == "Sec-WebSocket-Accept")
                {
                    // Calculate the original WebSocket hash
                    String wskey = Convert.ToBase64String(Encoding.UTF8.GetBytes(id.ToString())) + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";
                    String wshash;
                    using (SHA1Managed sha1 = new SHA1Managed())
                    {
                        wshash = Encoding.UTF8.GetString(sha1.ComputeHash(Encoding.UTF8.GetBytes(wskey)));
                    }

                    // Get the received WebSocket hash
                    wskey = Encoding.UTF8.GetString(Convert.FromBase64String(value));

                    // Compare original and received hashes
                    if (String.Compare(wskey, wshash, StringComparison.InvariantCulture) != 0)
                    {
                        error = true;
                        _wsHandler.OnWsError("Invalid WebSocket handshaked response: 'Sec-WebSocket-Accept' value validation failed");
                        break;
                    }

                    accept = true;
                }
            }

            // Failed to perform WebSocket handshake
            if (!accept || !connection || !upgrade)
            {
                if (!error)
                {
                    _wsHandler.OnWsError("Invalid WebSocket response");
                }

                return false;
            }

            // WebSocket successfully handshaked!
            WsHandshaked = true;
            IRandom random = RandomUtils.Create();
            random.NextBytes(WsSendMask);
            _wsHandler.OnWsConnected(response);

            return true;
        }

        /// <summary>
        ///     Perform WebSocket server upgrade
        /// </summary>
        /// <param name="request">WebSocket upgrade HTTP request</param>
        /// <param name="response">WebSocket upgrade HTTP response</param>
        /// <returns>'true' if the WebSocket was successfully upgrade, 'false' if the WebSocket was not upgrade</returns>
        public Boolean PerformServerUpgrade(HttpNetworkRequest request, HttpNetworkResponse response)
        {
            if (request.Method != "GET")
            {
                return false;
            }

            Boolean error = false;
            Boolean connection = false;
            Boolean upgrade = false;
            Boolean wsKey = false;
            Boolean wsVersion = false;

            String accept = "";

            // Validate WebSocket handshake headers
            for (Int32 i = 0; i < request.Headers; ++i)
            {
                Tuple<String, String> header = request.Header(i);
                String key = header.Item1;
                String value = header.Item2;

                if (key == "Connection")
                {
                    if (value != "Upgrade" && value != "keep-alive, Upgrade")
                    {
                        error = true;
                        response.MakeErrorResponse("Invalid WebSocket handshaked request: 'Connection' header value must be 'Upgrade' or 'keep-alive, Upgrade'", 400);
                        break;
                    }

                    connection = true;
                }
                else if (key == "Upgrade")
                {
                    if (value != "websocket")
                    {
                        error = true;
                        response.MakeErrorResponse("Invalid WebSocket handshaked request: 'Upgrade' header value must be 'websocket'", 400);
                        break;
                    }

                    upgrade = true;
                }
                else if (key == "Sec-WebSocket-Key")
                {
                    if (String.IsNullOrEmpty(value))
                    {
                        error = true;
                        response.MakeErrorResponse("Invalid WebSocket handshaked request: 'Sec-WebSocket-Key' header value must be non empty", 400);
                        break;
                    }

                    // Calculate the original WebSocket hash
                    String wskey = value + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";
                    Byte[] wshash;
                    using (SHA1Managed sha1 = new SHA1Managed())
                    {
                        wshash = sha1.ComputeHash(Encoding.UTF8.GetBytes(wskey));
                    }

                    accept = Convert.ToBase64String(wshash);

                    wsKey = true;
                }
                else if (key == "Sec-WebSocket-Version")
                {
                    if (value != "13")
                    {
                        error = true;
                        response.MakeErrorResponse("Invalid WebSocket handshaked request: 'Sec-WebSocket-Version' header value must be '13'", 400);
                        break;
                    }

                    wsVersion = true;
                }
            }

            // Filter out non WebSocket handshake requests
            if (!connection && !upgrade && !wsKey && !wsVersion)
            {
                return false;
            }

            // Failed to perform WebSocket handshake
            if (!connection || !upgrade || !wsKey || !wsVersion)
            {
                if (!error)
                {
                    response.MakeErrorResponse("Invalid WebSocket response", 400);
                }

                _wsHandler.SendResponse(response);
                return false;
            }

            // Prepare WebSocket upgrade success response
            response.Clear();
            response.SetBegin(101);
            response.SetHeader("Connection", "Upgrade");
            response.SetHeader("Upgrade", "websocket");
            response.SetHeader("Sec-WebSocket-Accept", accept);
            response.SetBody();

            // Validate WebSocket upgrade request and response
            if (!_wsHandler.OnWsConnecting(request, response))
            {
                return false;
            }

            // Send WebSocket upgrade response
            _wsHandler.SendResponse(response);

            // WebSocket successfully handshaked!
            WsHandshaked = true;
            Array.Fill(WsSendMask, (Byte) 0);
            _wsHandler.OnWsConnected(request);

            return true;
        }

        /// <summary>
        ///     Prepare WebSocket send frame
        /// </summary>
        /// <param name="opcode">WebSocket opcode</param>
        /// <param name="mask">WebSocket mask</param>
        /// <param name="buffer">Buffer to send</param>
        /// <param name="offset">Buffer offset</param>
        /// <param name="size">Buffer size</param>
        /// <param name="status">WebSocket status (default is 0)</param>
        public void PrepareSendFrame(Byte opcode, Boolean mask, Byte[] buffer, Int64 offset, Int64 size, Int32 status = 0)
        {
            // Clear the previous WebSocket send buffer
            WsSendBuffer.Clear();

            // Append WebSocket frame opcode
            WsSendBuffer.Add(opcode);

            // Append WebSocket frame size
            if (size <= 125)
            {
                WsSendBuffer.Add((Byte) (((Int32) size & 0xFF) | (mask ? 0x80 : 0)));
            }
            else if (size <= 65535)
            {
                WsSendBuffer.Add((Byte) (126 | (mask ? 0x80 : 0)));
                WsSendBuffer.Add((Byte) ((size >> 8) & 0xFF));
                WsSendBuffer.Add((Byte) (size & 0xFF));
            }
            else
            {
                WsSendBuffer.Add((Byte) (127 | (mask ? 0x80 : 0)));
                for (Int32 i = 7; i >= 0; --i)
                {
                    WsSendBuffer.Add((Byte) ((size >> (8 * i)) & 0xFF));
                }
            }

            if (mask)
            {
                // Append WebSocket frame mask
                WsSendBuffer.Add(WsSendMask[0]);
                WsSendBuffer.Add(WsSendMask[1]);
                WsSendBuffer.Add(WsSendMask[2]);
                WsSendBuffer.Add(WsSendMask[3]);
            }

            // Resize WebSocket frame buffer
            Int32 bufferOffset = WsSendBuffer.Count;
            WsSendBuffer.AddRange(new Byte[size]);

            // Mask WebSocket frame content
            for (Int32 i = 0; i < size; ++i)
            {
                WsSendBuffer[bufferOffset + i] = (Byte) (buffer[offset + i] ^ WsSendMask[i % 4]);
            }
        }

        /// <summary>
        ///     Prepare WebSocket send frame
        /// </summary>
        /// <param name="buffer">Buffer to send</param>
        /// <param name="offset">Buffer offset</param>
        /// <param name="size">Buffer size</param>
        [SuppressMessage("ReSharper", "ShiftExpressionRightOperandNotEqualRealCount")]
        public void PrepareReceiveFrame(Byte[] buffer, Int64 offset, Int64 size)
        {
            lock (WsReceiveLock)
            {
                Int32 index = 0;

                // Clear received data after WebSocket frame was processed
                if (WsReceived)
                {
                    WsReceived = false;
                    WsHeaderSize = 0;
                    WsPayloadSize = 0;
                    WsReceiveBuffer.Clear();
                    Array.Clear(WsReceiveMask, 0, WsReceiveMask.Length);
                }

                while (size > 0)
                {
                    // Clear received data after WebSocket frame was processed
                    if (WsReceived)
                    {
                        WsReceived = false;
                        WsHeaderSize = 0;
                        WsPayloadSize = 0;
                        WsReceiveBuffer.Clear();
                        Array.Clear(WsReceiveMask, 0, WsReceiveMask.Length);
                    }

                    // Prepare WebSocket frame opcode and mask flag
                    if (WsReceiveBuffer.Count < 2)
                    {
                        for (Int32 i = 0; i < 2; ++i, ++index, --size)
                        {
                            if (size == 0)
                            {
                                return;
                            }

                            WsReceiveBuffer.Add(buffer[offset + index]);
                        }
                    }

                    Byte opcode = (Byte) (WsReceiveBuffer[0] & 0x0F);
                    //Boolean fin = ((WsReceiveBuffer[0] >> 7) & 0x01) != 0;
                    Boolean mask = ((WsReceiveBuffer[1] >> 7) & 0x01) != 0;
                    Int32 payload = WsReceiveBuffer[1] & ~0x80;

                    // Prepare WebSocket frame size
                    if (payload <= 125)
                    {
                        WsHeaderSize = 2 + (mask ? 4 : 0);
                        WsPayloadSize = payload;
                        WsReceiveBuffer.Capacity = WsHeaderSize + WsPayloadSize;
                    }
                    else
                    {
                        switch (payload)
                        {
                            case 126:
                            {
                                if (WsReceiveBuffer.Count < 4)
                                {
                                    for (Int32 i = 0; i < 2; ++i, ++index, --size)
                                    {
                                        if (size == 0)
                                        {
                                            return;
                                        }

                                        WsReceiveBuffer.Add(buffer[offset + index]);
                                    }
                                }

                                payload = (WsReceiveBuffer[2] << 8) | (WsReceiveBuffer[3] << 0);
                                WsHeaderSize = 4 + (mask ? 4 : 0);
                                WsPayloadSize = payload;
                                WsReceiveBuffer.Capacity = WsHeaderSize + WsPayloadSize;
                                break;
                            }
                            case 127:
                            {
                                if (WsReceiveBuffer.Count < 10)
                                {
                                    for (Int32 i = 0; i < 8; ++i, ++index, --size)
                                    {
                                        if (size == 0)
                                        {
                                            return;
                                        }

                                        WsReceiveBuffer.Add(buffer[offset + index]);
                                    }
                                }

                                // ReSharper disable once ShiftExpressionRealShiftCountIsZero
                                payload = (WsReceiveBuffer[2] << 56) | (WsReceiveBuffer[3] << 48) | (WsReceiveBuffer[4] << 40) | (WsReceiveBuffer[5] << 32) |
                                          (WsReceiveBuffer[6] << 24) |
                                          (WsReceiveBuffer[7] << 16) | (WsReceiveBuffer[8] << 8) | (WsReceiveBuffer[9] << 0);
                                WsHeaderSize = 10 + (mask ? 4 : 0);
                                WsPayloadSize = payload;
                                WsReceiveBuffer.Capacity = WsHeaderSize + WsPayloadSize;
                                break;
                            }
                        }
                    }

                    // Prepare WebSocket frame mask
                    if (mask)
                    {
                        if (WsReceiveBuffer.Count < WsHeaderSize)
                        {
                            for (Int32 i = 0; i < 4; ++i, ++index, --size)
                            {
                                if (size == 0)
                                {
                                    return;
                                }

                                WsReceiveBuffer.Add(buffer[offset + index]);
                                WsReceiveMask[i] = buffer[offset + index];
                            }
                        }
                    }

                    Int32 total = WsHeaderSize + WsPayloadSize;
                    Int32 length = Math.Min(total - WsReceiveBuffer.Count, (Int32) size);

                    // Prepare WebSocket frame payload
                    WsReceiveBuffer.AddRange(buffer[((Int32) offset + index)..((Int32) offset + index + length)]);
                    index += length;
                    size -= length;

                    // Process WebSocket frame
                    if (WsReceiveBuffer.Count != total)
                    {
                        continue;
                    }

                    Int32 bufferOffset = WsHeaderSize;

                    // Unmask WebSocket frame content
                    if (mask)
                    {
                        for (Int32 i = 0; i < WsPayloadSize; ++i)
                        {
                            WsReceiveBuffer[bufferOffset + i] ^= WsReceiveMask[i % 4];
                        }
                    }

                    WsReceived = true;

                    if ((opcode & WsPing) == WsPing)
                    {
                        // Call the WebSocket ping handler
                        _wsHandler.OnWsPing(WsReceiveBuffer.ToArray(), bufferOffset, WsPayloadSize);
                    }
                    else if ((opcode & WsPong) == WsPong)
                    {
                        // Call the WebSocket pong handler
                        _wsHandler.OnWsPong(WsReceiveBuffer.ToArray(), bufferOffset, WsPayloadSize);
                    }
                    else if ((opcode & WsClose) == WsClose)
                    {
                        // Call the WebSocket close handler
                        _wsHandler.OnWsClose(WsReceiveBuffer.ToArray(), bufferOffset, WsPayloadSize);
                    }
                    else if ((opcode & WsText) == WsText || (opcode & WsBinary) == WsBinary)
                    {
                        // Call the WebSocket received handler
                        _wsHandler.OnWsReceived(WsReceiveBuffer.ToArray(), bufferOffset, WsPayloadSize);
                    }
                }
            }
        }

        /// <summary>
        ///     Required WebSocket receive frame size
        /// </summary>
        public Int32 RequiredReceiveFrameSize()
        {
            lock (WsReceiveLock)
            {
                if (WsReceived)
                {
                    return 0;
                }

                // Required WebSocket frame opcode and mask flag
                if (WsReceiveBuffer.Count < 2)
                {
                    return 2 - WsReceiveBuffer.Count;
                }

                Boolean mask = ((WsReceiveBuffer[1] >> 7) & 0x01) != 0;
                Int32 payload = WsReceiveBuffer[1] & ~0x80;

                switch (payload)
                {
                    // Required WebSocket frame size
                    case 126 when WsReceiveBuffer.Count < 4:
                        return 4 - WsReceiveBuffer.Count;
                    case 127 when WsReceiveBuffer.Count < 10:
                        return 10 - WsReceiveBuffer.Count;
                }

                // Required WebSocket frame mask
                if (mask && WsReceiveBuffer.Count < WsHeaderSize)
                {
                    return WsHeaderSize - WsReceiveBuffer.Count;
                }

                // Required WebSocket frame payload
                return WsHeaderSize + WsPayloadSize - WsReceiveBuffer.Count;
            }
        }

        /// <summary>
        ///     Clear WebSocket send/receive buffers
        /// </summary>
        public void ClearWsBuffers()
        {
            lock (WsReceiveLock)
            {
                WsReceived = false;
                WsHeaderSize = 0;
                WsPayloadSize = 0;
                WsReceiveBuffer.Clear();
                Array.Clear(WsReceiveMask, 0, WsReceiveMask.Length);
            }

            lock (WsSendLock)
            {
                WsSendBuffer.Clear();
                Array.Clear(WsSendMask, 0, WsSendMask.Length);
            }
        }
    }
}