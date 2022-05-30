// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Diagnostics.CodeAnalysis;
using NetExtender.Configuration.Behavior.Transactions;
using NetExtender.Localization.Behavior.Interfaces;
using NetExtender.Localization.Behavior.Transactions.Interfaces;

namespace NetExtender.Localization.Behavior.Transactions
{
    [SuppressMessage("ReSharper", "SuggestBaseTypeForParameterInConstructor")]
    public class LocalizationBehaviorTransaction : ConfigBehaviorTransaction, ILocalizationBehaviorTransaction
    {
        public new ILocalizationBehavior Original
        {
            get
            {
                return (ILocalizationBehavior) base.Original;
            }
        }

        public new ILocalizationBehavior Transaction
        {
            get
            {
                return (ILocalizationBehavior) base.Transaction;
            }
        }
        
        public LocalizationBehaviorTransaction(ILocalizationBehavior original, ILocalizationBehavior transaction)
            : base(original, transaction)
        {
        }
    }
}