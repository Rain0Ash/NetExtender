// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.Configuration.Properties.Interfaces;

namespace NetExtender.Configuration.Cryptography.Properties.Interfaces
{
    public interface ICryptographyConfigProperty<T> : IConfigProperty<T>, ICryptographyConfigPropertyInfo
    {
    }

    public interface ICryptographyConfigProperty : IConfigProperty, ICryptographyConfigPropertyInfo
    {
    }
}