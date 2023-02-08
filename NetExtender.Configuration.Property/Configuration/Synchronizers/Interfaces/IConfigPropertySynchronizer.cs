// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using NetExtender.Configuration.Properties.Interfaces;
using NetExtender.Types.Behavior.Interfaces;

namespace NetExtender.Configuration.Synchronizers.Interfaces
{
    public interface IConfigPropertySynchronizer : IChangeableBehavior, ICollection<IConfigPropertyInfo>
    {
    }
}