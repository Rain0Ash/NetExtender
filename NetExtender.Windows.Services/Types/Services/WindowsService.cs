// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ServiceProcess;
using NetExtender.Windows.Services.Types.Services.Interfaces;

namespace NetExtender.Windows.Services.Types.Services
{
    public class WindowsService : ServiceBase, IWindowsService
    {
        public ServiceBase Service
        {
            get
            {
                return this;
            }
        }

        protected String[]? Arguments { get; set; }

        protected IWindowsServicePauseStateHandler? PauseStateHandler { get; init; }

        protected sealed override void OnCustomCommand(Int32 command)
        {
            try
            {
                if (!BeforeCustomCommandCore(command))
                {
                    return;
                }

                if (!CustomCommandCore(command))
                {
                    return;
                }

                AfterCustomCommandCore(command);
            }
            catch (Exception exception)
            {
                if (!ExceptionCustomCommandCoreHandler(command, exception))
                {
                    throw;
                }
            }
            finally
            {
                FinallyCustomCommandCoreHandler(command);
            }
        }

        protected virtual Boolean BeforeCustomCommandCore(Int32 command)
        {
            return true;
        }

        protected virtual Boolean CustomCommandCore(Int32 command)
        {
            return true;
        }

        protected virtual Boolean AfterCustomCommandCore(Int32 command)
        {
            return true;
        }

        protected virtual Boolean ExceptionCustomCommandCoreHandler(Int32 command, Exception exception)
        {
            return false;
        }

        protected virtual Boolean FinallyCustomCommandCoreHandler(Int32 command)
        {
            return true;
        }

        protected sealed override Boolean OnPowerEvent(PowerBroadcastStatus power)
        {
            try
            {
                if (!BeforePowerEventCore(power))
                {
                    return false;
                }

                if (!PowerEventCore(power))
                {
                    return false;
                }

                return AfterPowerEventCore(power);
            }
            catch (Exception exception)
            {
                if (!ExceptionPowerEventCoreHandler(power, exception))
                {
                    throw;
                }
            }
            finally
            {
                FinallyPowerEventCoreHandler(power);
            }

            return base.OnPowerEvent(power);
        }

        protected virtual Boolean BeforePowerEventCore(PowerBroadcastStatus power)
        {
            return true;
        }

        protected virtual Boolean PowerEventCore(PowerBroadcastStatus power)
        {
            return true;
        }

        protected virtual Boolean AfterPowerEventCore(PowerBroadcastStatus power)
        {
            return true;
        }

        protected virtual Boolean ExceptionPowerEventCoreHandler(PowerBroadcastStatus power, Exception exception)
        {
            return false;
        }

        protected virtual Boolean FinallyPowerEventCoreHandler(PowerBroadcastStatus power)
        {
            return true;
        }

        protected sealed override void OnSessionChange(SessionChangeDescription description)
        {
            try
            {
                if (!BeforeSessionChangeCore(description))
                {
                    return;
                }

                if (!SessionChangeCore(description))
                {
                    return;
                }

                AfterSessionChangeCore(description);
            }
            catch (Exception exception)
            {
                if (!ExceptionSessionChangeCoreHandler(description, exception))
                {
                    throw;
                }
            }
            finally
            {
                FinallySessionChangeCoreHandler(description);
            }
        }

        protected virtual Boolean BeforeSessionChangeCore(SessionChangeDescription description)
        {
            return true;
        }

        protected virtual Boolean SessionChangeCore(SessionChangeDescription description)
        {
            return true;
        }

        protected virtual Boolean AfterSessionChangeCore(SessionChangeDescription description)
        {
            return true;
        }

        protected virtual Boolean ExceptionSessionChangeCoreHandler(SessionChangeDescription description, Exception exception)
        {
            return false;
        }

        protected virtual Boolean FinallySessionChangeCoreHandler(SessionChangeDescription description)
        {
            return true;
        }

        protected sealed override void OnStart(String[] args)
        {
            Arguments = args;

            try
            {
                if (!BeforeStartCore(args))
                {
                    return;
                }

                if (!StartCore(args))
                {
                    return;
                }

                AfterStartCore(args);
            }
            catch (Exception exception)
            {
                if (!ExceptionStartCoreHandler(args, exception))
                {
                    throw;
                }
            }
            finally
            {
                FinallyStartCoreHandler(args);
            }
        }

        protected virtual Boolean BeforeStartCore(String[] args)
        {
            return true;
        }

        protected virtual Boolean StartCore(String[] args)
        {
            return true;
        }

        protected virtual Boolean AfterStartCore(String[] args)
        {
            return true;
        }

        protected virtual Boolean ExceptionStartCoreHandler(String[] args, Exception exception)
        {
            return false;
        }

        protected virtual Boolean FinallyStartCoreHandler(String[] args)
        {
            return true;
        }

        protected sealed override void OnStop()
        {
            try
            {
                if (!BeforeStopCore())
                {
                    return;
                }

                if (!StopCore())
                {
                    return;
                }

                AfterStopCore();
            }
            catch (Exception exception)
            {
                if (!ExceptionStopCoreHandler(exception))
                {
                    throw;
                }
            }
            finally
            {
                FinallyStopCoreHandler();
            }
        }

        protected virtual Boolean BeforeStopCore()
        {
            return true;
        }

        protected virtual Boolean StopCore()
        {
            return true;
        }

        protected virtual Boolean AfterStopCore()
        {
            return true;
        }

        protected virtual Boolean ExceptionStopCoreHandler(Exception exception)
        {
            return false;
        }

        protected virtual Boolean FinallyStopCoreHandler()
        {
            return true;
        }

        protected sealed override void OnPause()
        {
            try
            {
                if (!BeforePauseCore())
                {
                    return;
                }

                if (!PauseCore())
                {
                    return;
                }

                AfterPauseCore();
            }
            catch (Exception exception)
            {
                if (!ExceptionPauseCoreHandler(exception))
                {
                    throw;
                }
            }
            finally
            {
                FinallyPauseCoreHandler();
            }
        }

        protected virtual Boolean BeforePauseCore()
        {
            return PauseStateHandler is not null && !PauseStateHandler.IsPaused;
        }

        protected virtual Boolean PauseCore()
        {
            if (PauseStateHandler is null || PauseStateHandler.IsPaused)
            {
                return false;
            }

            PauseStateHandler.Pause();
            return true;
        }

        protected virtual Boolean AfterPauseCore()
        {
            return true;
        }

        protected virtual Boolean ExceptionPauseCoreHandler(Exception exception)
        {
            return false;
        }

        protected virtual Boolean FinallyPauseCoreHandler()
        {
            return true;
        }

        protected sealed override void OnContinue()
        {
            try
            {
                if (!BeforeContinueCore())
                {
                    return;
                }

                if (!ContinueCore())
                {
                    return;
                }

                AfterContinueCore();
            }
            catch (Exception exception)
            {
                if (!ExceptionContinueCoreHandler(exception))
                {
                    throw;
                }
            }
            finally
            {
                FinallyContinueCoreHandler();
            }
        }

        protected virtual Boolean BeforeContinueCore()
        {
            return PauseStateHandler is not null && PauseStateHandler.IsPaused;
        }

        protected virtual Boolean ContinueCore()
        {
            if (PauseStateHandler is null || !PauseStateHandler.IsPaused)
            {
                return false;
            }

            PauseStateHandler.Resume();
            return true;
        }

        protected virtual Boolean AfterContinueCore()
        {
            return true;
        }

        protected virtual Boolean ExceptionContinueCoreHandler(Exception exception)
        {
            return false;
        }

        protected virtual Boolean FinallyContinueCoreHandler()
        {
            return true;
        }
    }
}