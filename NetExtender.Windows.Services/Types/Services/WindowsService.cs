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
                if (!BeforeCustomCommandInternal(command))
                {
                    return;
                }

                if (!CustomCommandInternal(command))
                {
                    return;
                }

                AfterCustomCommandInternal(command);
            }
            catch (Exception exception)
            {
                if (!ExceptionCustomCommandInternalHandler(command, exception))
                {
                    throw;
                }
            }
            finally
            {
                FinallyCustomCommandInternalHandler(command);
            }
        }

        protected virtual Boolean BeforeCustomCommandInternal(Int32 command)
        {
            return true;
        }

        protected virtual Boolean CustomCommandInternal(Int32 command)
        {
            return true;
        }

        protected virtual Boolean AfterCustomCommandInternal(Int32 command)
        {
            return true;
        }

        protected virtual Boolean ExceptionCustomCommandInternalHandler(Int32 command, Exception exception)
        {
            return false;
        }

        protected virtual Boolean FinallyCustomCommandInternalHandler(Int32 command)
        {
            return true;
        }

        protected sealed override Boolean OnPowerEvent(PowerBroadcastStatus power)
        {
            try
            {
                if (!BeforePowerEventInternal(power))
                {
                    return false;
                }

                if (!PowerEventInternal(power))
                {
                    return false;
                }

                return AfterPowerEventInternal(power);
            }
            catch (Exception exception)
            {
                if (!ExceptionPowerEventInternalHandler(power, exception))
                {
                    throw;
                }
            }
            finally
            {
                FinallyPowerEventInternalHandler(power);
            }

            return base.OnPowerEvent(power);
        }

        protected virtual Boolean BeforePowerEventInternal(PowerBroadcastStatus power)
        {
            return true;
        }

        protected virtual Boolean PowerEventInternal(PowerBroadcastStatus power)
        {
            return true;
        }

        protected virtual Boolean AfterPowerEventInternal(PowerBroadcastStatus power)
        {
            return true;
        }

        protected virtual Boolean ExceptionPowerEventInternalHandler(PowerBroadcastStatus power, Exception exception)
        {
            return false;
        }

        protected virtual Boolean FinallyPowerEventInternalHandler(PowerBroadcastStatus power)
        {
            return true;
        }

        protected sealed override void OnSessionChange(SessionChangeDescription description)
        {
            try
            {
                if (!BeforeSessionChangeInternal(description))
                {
                    return;
                }

                if (!SessionChangeInternal(description))
                {
                    return;
                }

                AfterSessionChangeInternal(description);
            }
            catch (Exception exception)
            {
                if (!ExceptionSessionChangeInternalHandler(description, exception))
                {
                    throw;
                }
            }
            finally
            {
                FinallySessionChangeInternalHandler(description);
            }
        }

        protected virtual Boolean BeforeSessionChangeInternal(SessionChangeDescription description)
        {
            return true;
        }

        protected virtual Boolean SessionChangeInternal(SessionChangeDescription description)
        {
            return true;
        }

        protected virtual Boolean AfterSessionChangeInternal(SessionChangeDescription description)
        {
            return true;
        }

        protected virtual Boolean ExceptionSessionChangeInternalHandler(SessionChangeDescription description, Exception exception)
        {
            return false;
        }

        protected virtual Boolean FinallySessionChangeInternalHandler(SessionChangeDescription description)
        {
            return true;
        }

        protected sealed override void OnStart(String[] args)
        {
            Arguments = args;

            try
            {
                if (!BeforeStartInternal(args))
                {
                    return;
                }

                if (!StartInternal(args))
                {
                    return;
                }

                AfterStartInternal(args);
            }
            catch (Exception exception)
            {
                if (!ExceptionStartInternalHandler(args, exception))
                {
                    throw;
                }
            }
            finally
            {
                FinallyStartInternalHandler(args);
            }
        }

        protected virtual Boolean BeforeStartInternal(String[] args)
        {
            return true;
        }

        protected virtual Boolean StartInternal(String[] args)
        {
            return true;
        }

        protected virtual Boolean AfterStartInternal(String[] args)
        {
            return true;
        }

        protected virtual Boolean ExceptionStartInternalHandler(String[] args, Exception exception)
        {
            return false;
        }

        protected virtual Boolean FinallyStartInternalHandler(String[] args)
        {
            return true;
        }

        protected sealed override void OnStop()
        {
            try
            {
                if (!BeforeStopInternal())
                {
                    return;
                }

                if (!StopInternal())
                {
                    return;
                }

                AfterStopInternal();
            }
            catch (Exception exception)
            {
                if (!ExceptionStopInternalHandler(exception))
                {
                    throw;
                }
            }
            finally
            {
                FinallyStopInternalHandler();
            }
        }

        protected virtual Boolean BeforeStopInternal()
        {
            return true;
        }

        protected virtual Boolean StopInternal()
        {
            return true;
        }

        protected virtual Boolean AfterStopInternal()
        {
            return true;
        }

        protected virtual Boolean ExceptionStopInternalHandler(Exception exception)
        {
            return false;
        }

        protected virtual Boolean FinallyStopInternalHandler()
        {
            return true;
        }

        protected sealed override void OnPause()
        {
            try
            {
                if (!BeforePauseInternal())
                {
                    return;
                }

                if (!PauseInternal())
                {
                    return;
                }

                AfterPauseInternal();
            }
            catch (Exception exception)
            {
                if (!ExceptionPauseInternalHandler(exception))
                {
                    throw;
                }
            }
            finally
            {
                FinallyPauseInternalHandler();
            }
        }

        protected virtual Boolean BeforePauseInternal()
        {
            return PauseStateHandler is not null && !PauseStateHandler.IsPaused;
        }

        protected virtual Boolean PauseInternal()
        {
            if (PauseStateHandler is null || PauseStateHandler.IsPaused)
            {
                return false;
            }

            PauseStateHandler.Pause();
            return true;
        }

        protected virtual Boolean AfterPauseInternal()
        {
            return true;
        }

        protected virtual Boolean ExceptionPauseInternalHandler(Exception exception)
        {
            return false;
        }

        protected virtual Boolean FinallyPauseInternalHandler()
        {
            return true;
        }

        protected sealed override void OnContinue()
        {
            try
            {
                if (!BeforeContinueInternal())
                {
                    return;
                }

                if (!ContinueInternal())
                {
                    return;
                }

                AfterContinueInternal();
            }
            catch (Exception exception)
            {
                if (!ExceptionContinueInternalHandler(exception))
                {
                    throw;
                }
            }
            finally
            {
                FinallyContinueInternalHandler();
            }
        }

        protected virtual Boolean BeforeContinueInternal()
        {
            return PauseStateHandler is not null && PauseStateHandler.IsPaused;
        }

        protected virtual Boolean ContinueInternal()
        {
            if (PauseStateHandler is null || !PauseStateHandler.IsPaused)
            {
                return false;
            }

            PauseStateHandler.Resume();
            return true;
        }

        protected virtual Boolean AfterContinueInternal()
        {
            return true;
        }

        protected virtual Boolean ExceptionContinueInternalHandler(Exception exception)
        {
            return false;
        }

        protected virtual Boolean FinallyContinueInternalHandler()
        {
            return true;
        }
    }
}