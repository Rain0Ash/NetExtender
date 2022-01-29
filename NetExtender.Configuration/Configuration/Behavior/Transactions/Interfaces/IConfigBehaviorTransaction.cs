// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.Configuration.Behavior.Interfaces;
using NetExtender.Initializer.Types.Transactions.Interfaces;

namespace NetExtender.Configuration.Behavior.Transactions.Interfaces
{
    public interface IConfigBehaviorTransaction : ITransaction
    {
        public IConfigBehavior Transaction { get; }
    }
}