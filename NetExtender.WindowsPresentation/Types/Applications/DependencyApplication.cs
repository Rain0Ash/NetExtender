using System.Windows;
using NetExtender.Types.Dispatchers;
using NetExtender.Types.Dispatchers.Interfaces;
using NetExtender.WindowsPresentation.Types.Applications.Interfaces;

namespace NetExtender.WindowsPresentation.Types.Applications
{
    public class DependencyApplication : Application, IDependencyApplication
    {
        public virtual WindowsPresentationServiceProvider Provider
        {
            get
            {
                return WindowsPresentationServiceProvider.Internal;
            }
        }
        
        private readonly IDispatcher _dispatcher;
        
        IDispatcher IDependencyApplication.Dispatcher
        {
            get
            {
                return _dispatcher;
            }
        }
        
        public DependencyApplication()
        {
            _dispatcher = new DispatcherWrapper(Dispatcher);
        }
    }
}