// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com


using NetExtender.Configuration.Behavior.Transactions.Interfaces;
using NetExtender.Configuration.Cryptography.Behavior.Interfaces;

namespace NetExtender.Configuration.Cryptography.Behavior.Transactions.Interfaces
{
    public interface ICryptographyConfigBehaviorTransaction : IConfigBehaviorTransaction
    {
        public new ICryptographyConfigBehavior Transaction { get; }
    }
}