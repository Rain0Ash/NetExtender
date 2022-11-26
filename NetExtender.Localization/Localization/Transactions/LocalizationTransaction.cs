// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Configuration.Transactions;
using NetExtender.Localization.Behavior.Transactions.Interfaces;
using NetExtender.Localization.Interfaces;
using NetExtender.Localization.Transactions.Interfaces;
using NetExtender.Localization.Utilities;

namespace NetExtender.Localization.Transactions
{
    [SuppressMessage("ReSharper", "SuggestBaseTypeForParameterInConstructor")]
    public class LocalizationTransaction : ConfigTransaction, ILocalizationTransaction
    {
        public new ILocalizationConfig Original
        {
            get
            {
                return (ILocalizationConfig) base.Original;
            }
        }

        public new ILocalizationConfig Transaction
        {
            get
            {
                return (ILocalizationConfig) base.Transaction;
            }
        }

        public LocalizationTransaction(ILocalizationConfig original, ILocalizationBehaviorTransaction transaction)
            : this(original, transaction is not null ? transaction.Transaction.Create() : throw new ArgumentNullException(nameof(transaction)))
        {
        }

        public LocalizationTransaction(ILocalizationConfig original, ILocalizationConfig transaction)
            : base(original, transaction)
        {
        }
    }
}