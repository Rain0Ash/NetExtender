// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NetExtender.Apps.Domains;
using NetExtender.Apps.Reader.WebSocket;
using NetExtender.Events.Args;
using NetExtender.Messages;
using NetExtender.Messages.Interfaces;
using NetExtender.Messages.Rules.Interfaces;
using NetExtender.Utils.Types;

namespace NetExtender.Apps.Reader
{
    public class ExternalReader : MessageReader<String>
    {
        private readonly ExternalWebSocketNetworkServer _server;

        private const Byte CreateServerTries = 3;
        
        internal ExternalReader(UInt16 port = 0)
        {
            _server = InitializeServer(port);
        }

        protected override Task<Boolean> DefaultHandlerAsync(IConsoleRule<String> rule, IReaderMessage<String> message)
        {
            return TaskUtils.True;
        }

        private ExternalWebSocketNetworkServer InitializeServer(UInt16 port)
        {
            if (port <= 0)
            {
                return new ExternalWebSocketNetworkServer(this, port);
            }
            
            for (Byte tries = 0; tries < CreateServerTries;)
            {
                try
                {
                    return new ExternalWebSocketNetworkServer(this, ExternalWebSocketNetworkServer.DefaultPort + tries);
                }
                catch (SocketException)
                {
                    if (++tries >= CreateServerTries)
                    {
                        throw;
                    }
                }
            }

            return null;
        }

        protected override Boolean OnStart()
        {
            _server.Start();
            return base.OnStart();
        }

        protected override Boolean OnPause()
        {
            _server.Stop();
            return base.OnStop();
        }
        
        protected override Boolean OnResume()
        {
            _server.Start();
            return base.OnStop();
        }
        
        protected override Boolean OnStop()
        {
            _server.Stop();
            return base.OnStop();
        }

        protected override Boolean CheckMessage(IReaderMessage<String> message)
        {
            return message.IsExternal;
        }

        public void ProcessExternalInputAsync(String arg)
        {
            ProcessExternalInputAsync(null, arg);
        }
        
        public void ProcessExternalInputAsync(Object sender, String arg)
        {
            ProcessExternalInputAsync(sender, new[]{arg});
        }
        
        public void ProcessExternalInputAsync(String[] args)
        {
            ProcessExternalInputAsync(null, args);
        }
        
        public void ProcessExternalInputAsync(Object sender, String[] args)
        {
            ProcessExternalInputAsync(sender, args.Serialize());
        }
        
        public void ProcessExternalInputAsync(Byte[] bytes)
        {
            ProcessExternalInputAsync(null, bytes);
        }
        
        public Task ProcessExternalInputAsync(Object sender, Byte[] bytes)
        {
            return ProcessExternalInputAsync(sender, new TypeHandledEventArgs<Byte[]>(bytes));
        }

        public async Task ProcessExternalInputAsync(Object sender, TypeHandledEventArgs<Byte[]> e)
        {
            if (!e.Handled)
            {
                e.Handled = await ProcessExternalInputHandlerAsync(e.Value).ConfigureAwait(false);
            }
        }
        
        private async Task<Boolean> ProcessExternalInputHandlerAsync(Byte[] message)
        {
            String[] args = message.Deserialize<String[]>();
            
            if (args is null || args.Length <= 0)
            {
                return true;
            }

            ReaderMessageOptions options = ReaderMessageOptions.External;
            
            // ReSharper disable once InvertIf
            if (args.Length == 1 && args[0].StartsWith(Domain.Current.ProtocolName, StringComparison.InvariantCultureIgnoreCase))
            {
                args = Regex.Replace(args[0], $"{Domain.Current.ProtocolName}:(\\/|\\\\)*", String.Empty, RegexOptions.IgnoreCase | RegexOptions.Compiled)
                    .Replace('/', ' ')
                    .Replace('\\', ' ')
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(Uri.UnescapeDataString).ToArray();

                if (args.Length <= 0)
                {
                    return true;
                }

                options |= ReaderMessageOptions.Protocol;
            }

            return await ProcessInputAsync(new ReaderStringMessage(args, options)).ConfigureAwait(false);
        }

        protected override void Dispose(Boolean disposing)
        {
            _server.Dispose();
            base.Dispose(disposing);
        }
    }
}