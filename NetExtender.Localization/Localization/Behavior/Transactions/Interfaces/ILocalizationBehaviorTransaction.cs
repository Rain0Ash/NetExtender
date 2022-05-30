// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.Configuration.Behavior.Transactions.Interfaces;
using NetExtender.Localization.Behavior.Interfaces;

namespace NetExtender.Localization.Behavior.Transactions.Interfaces
{
    public interface ILocalizationBehaviorTransaction : IConfigBehaviorTransaction
    {
        public new ILocalizationBehavior Transaction { get; }
    }
}