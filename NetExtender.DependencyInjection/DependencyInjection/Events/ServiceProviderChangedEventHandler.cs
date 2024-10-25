using System;

namespace NetExtender.DependencyInjection.Events
{
    public delegate void ServiceProviderChangedEventHandler(Object? sender, ServiceProviderEventArgs args);
}