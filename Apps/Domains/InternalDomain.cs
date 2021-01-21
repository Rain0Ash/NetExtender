// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using NetExtender.Utils.Types;
using NetExtender.Apps.Data.Common;
using NetExtender.Apps.Data.Interfaces;
using NetExtender.Apps.Domains.Applications;
using NetExtender.Apps.Domains.Applications.Interfaces;
using NetExtender.Apps.Domains.Interfaces;
using NetExtender.Apps.Reader;
using NetExtender.Events.Args;
using NetExtender.Exceptions;
using NetExtender.GUI;
using WPFApp = System.Windows.Application;

namespace NetExtender.Apps.Domains
{
    public static partial class Domain
    {
        [Serializable]
        public readonly struct StartedAppDataMessage
        {
            public AppDataMessage This { get; }
            public AppDataMessage Another { get; }
            public Boolean Handled { get; }

            public StartedAppDataMessage(AppDataMessage @this, AppDataMessage another, Boolean handled = false)
            {
                This = @this;
                Another = another;
                Handled = handled;
            }
        }
        
        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        private class InternalDomain : IDomain
        {
            public event TypeHandler<TypeHandledEventArgs<AppDataMessage>> AnotherDomainStarted;
            public event TypeHandler<TypeHandledEventArgs<StartedAppDataMessage>> AnotherDomainHandled;

            private event SenderTypeHandler<TypeHandledEventArgs<Byte[]>> MessageReceived;

            private IApplication _application;

            public IApplication Application
            {
                get
                {
                    return _application ?? throw new NotInitializedException();
                }
                private set
                {
                    if (_application is not null)
                    {
                        throw new AlreadyInitializedException();
                    }

                    _application = value;
                }
            }

            public GUIType GUIType
            {
                get
                {
                    return Application.GUIType;
                }
            }

            public DateTime StartedAt
            {
                get
                {
                    return Data.StartedAt;
                }
            }

            private IIPCAppData _data;

            public IIPCAppData Data
            {
                get
                {
                    return _data;
                }
                private set
                {
                    if (value is null)
                    {
                        throw new ArgumentNullException(nameof(value));
                    }

                    if (_data is not null)
                    {
                        _data.MessageReceived -= OnMessageReceive;
                        _data.Dispose();
                    }

                    value.MessageReceived += OnMessageReceive;

                    _data = value;
                }
            }

            public Dispatcher Dispatcher
            {
                get
                {
                    return Application.Dispatcher;
                }
            }

            public ShutdownMode ShutdownMode
            {
                get
                {
                    return Application.ShutdownMode;
                }
                set
                {
                    Application.ShutdownMode = value;
                }
            }

            private ExternalReader _reader;

            public ExternalReader MessagesReader
            {
                get
                {
                    if (_reader is not null)
                    {
                        return _reader;
                    }

                    _reader = new ExternalReader();
                    MessageReceived += OnMessageReceivedToReader;

                    return _reader;
                }
            }

            public Guid Guid
            {
                get
                {
                    return Data.Guid;
                }
            }

            public AppVersion Version
            {
                get
                {
                    return Data.Version;
                }
            }

            public AppInformation Information
            {
                get
                {
                    return Data.Information;
                }
            }

            public AppStatus Status
            {
                get
                {
                    return Data.Status;
                }
            }

            public String StatusData
            {
                get
                {
                    return Data.StatusData;
                }
            }

            public AppBranch Branch
            {
                get
                {
                    return Data.Branch;
                }
            }

            public String BranchData
            {
                get
                {
                    return Data.BranchData;
                }
            }

            public String AppName
            {
                get
                {
                    return Data.AppName;
                }
            }

            public CultureInfo Culture
            {
                get
                {
                    return Thread.CurrentThread.CurrentCulture;
                }
                set
                {
                    Thread.CurrentThread.CurrentCulture = value;
                }
            }

            public Boolean AlreadyStarted
            {
                get
                {
                    return Data.AlreadyStarted;
                }
            }

            public Boolean UseProtocol
            {
                get
                {
                    return Data.UseProtocol;
                }
                set
                {
                    Data.UseProtocol = value;
                }
            }

            public String ProtocolName
            {
                get
                {
                    return Data.ProtocolName;
                }
            }

            public InternalDomain(IIPCAppData data)
            {
                Culture = CultureInfo.InvariantCulture;
                Data = data;
            }

            private void OnMessageReceivedToReader(Object sender, TypeHandledEventArgs<Byte[]> args)
            {
                if (args.Handled)
                {
                    return;
                }

                _reader.ProcessExternalInputAsync(sender, args).RunSynchronously();
            }

            private async void TrySendMessage()
            {
                //TODO: fix
                AppDataMessage message = Data.Message;

                void HandleReceivedMsg(Object sender, TypeHandledEventArgs<Byte[]> args)
                {
                    // ReSharper disable once AsyncConverter.AsyncWait
                    HandleReceivedMessageAsync(message, args).Wait();
                }

                MessageReceived += HandleReceivedMsg;

                if (!Data.AlreadyStarted)
                {
                    return;
                }

                await Data.SendMessageAsync(message.Serialize()).ConfigureAwait(false);
            }

            private async Task HandleReceivedMessageAsync(AppDataMessage message, TypeHandledEventArgs<Byte[]> args)
            {
                if (args.Handled)
                {
                    return;
                }

                if (args.Value.TryDeserialize(out StartedAppDataMessage msg))
                {
                    args.Handled = true;

                    if (!message.Equals(msg.This))
                    {
                        return;
                    }

                    AnotherDomainHandled?.Invoke(new TypeHandledEventArgs<StartedAppDataMessage>(msg));
                }
                else if (args.Value.TryDeserialize(out AppDataMessage another))
                {
                    TypeHandledEventArgs<AppDataMessage> handler = new TypeHandledEventArgs<AppDataMessage>(another);
                    AnotherDomainStarted?.Invoke(handler);

                    await Data.SendMessageAsync(new StartedAppDataMessage(message, another, handler.Handled).Serialize()).ConfigureAwait(false);
                    args.Handled = true;
                }
            }

            public IDomain Initialize(GUIType type)
            {
                return Initialize<WPFApp>(type);
            }

            public IDomain Initialize<TApp>(GUIType type) where TApp : WPFApp, new()
            {
                return Initialize(new TApp(), type);
            }

            public IDomain Initialize<TApp>(TApp app, GUIType type) where TApp : WPFApp, new()
            {
                Application = type switch
                {
                    GUIType.Console => new ConsoleApplication<TApp>(app),
                    GUIType.ConsoleGUI => new ConsoleApplication<TApp>(app),
                    GUIType.WinForms => new WinFormsApplication(),
                    GUIType.WPF => new WPFApplication<TApp>(app),
                    GUIType.None => throw new NotSupportedException(),
                    _ => throw new NotSupportedException()
                };

                return this;
            }

            public void Run()
            {
                Application.Run();
            }

            public void Shutdown(Int32 code = 0)
            {
                Application.Shutdown(code);
            }

            public void Shutdown(Boolean force)
            {
                Application.Shutdown(force);
            }

            public void Shutdown(Int32 code, Boolean force)
            {
                Application.Shutdown(code, force);
            }

            public Task<Boolean> ShutdownAsync(Int32 code, Int32 milli)
            {
                return Application.ShutdownAsync(code, milli);
            }

            public Task<Boolean> ShutdownAsync(Int32 code, Int32 milli, CancellationToken token)
            {
                return Application.ShutdownAsync(code, milli, token);
            }

            public Task<Boolean> ShutdownAsync(Int32 code, Int32 milli, Boolean force)
            {
                return Application.ShutdownAsync(code, milli, force);
            }

            public Task<Boolean> ShutdownAsync(Int32 code, Int32 milli, Boolean force, CancellationToken token)
            {
                return Application.ShutdownAsync(code, milli, force, token);
            }

            public void Restart()
            {
                Application.Restart();
            }

            public void Restart(Int32 milli)
            {
                Application.Restart(milli);
            }

            public void Restart(CancellationToken token)
            {
                Application.Restart(token);
            }

            public void Restart(Int32 milli, CancellationToken token)
            {
                Application.Restart(milli, token);
            }

            private void OnMessageReceive(Object sender, TypeHandledEventArgs<Byte[]> e)
            {
                MessageReceived?.Invoke(sender, e);
            }

            public Task SendMessageAsync(Byte[] message)
            {
                return Data.SendMessageAsync(message);
            }

            public Task SendMessageAsync(IEnumerable<Byte[]> message)
            {
                return Data.SendMessageAsync(message);
            }

            public void Dispose()
            {
                _data?.Dispose();
                _reader?.Dispose();
            }
        }
    }
}