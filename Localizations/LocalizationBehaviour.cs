// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using NetExtender.Cultures.Comparers;
using NetExtender.Localizations.Interfaces;
using NetExtender.Localizations.Sub.Interfaces;
using NetExtender.Types.Culture;
using NetExtender.Utils.Types;

namespace NetExtender.Localizations
{
    public record LocalizationBehaviour : ILocalizationBehaviour
    {
        public CultureComparer Comparer { get; init; }

        public IEnumerable<KeyValuePair<CultureInfo, ISubLocalization>> Supported { get; init; }

        public LocalizationBehaviour()
        {
        }
        
        public LocalizationBehaviour(params CultureLCID[] order)
            : this(order?.DefaultPairs<CultureLCID, ISubLocalization>())
        {
        }
        
        public LocalizationBehaviour(params CultureInfo[] order)
            : this(order?.DefaultPairs<CultureInfo, ISubLocalization>())
        {
        }
        
        public LocalizationBehaviour(IEnumerable<CultureLCID> order)
            : this(order?.DefaultPairs<CultureLCID, ISubLocalization>())
        {
        }
        
        public LocalizationBehaviour(IEnumerable<CultureInfo> order)
            : this(order?.DefaultPairs<CultureInfo, ISubLocalization>())
        {
        }
        
        public LocalizationBehaviour(IEnumerable<KeyValuePair<CultureLCID, ISubLocalization>> order)
            : this(order?.Select(pair => new KeyValuePair<CultureInfo, ISubLocalization>(pair.Key.GetCultureInfo(), pair.Value)))
        {
        }
        
        public LocalizationBehaviour(IEnumerable<KeyValuePair<CultureInfo, ISubLocalization>> order)
        {
            Supported = order.DistinctByThrow(pair => pair.Key).ToImmutableArray();
            Comparer = new CultureComparer(Supported.Keys());
        }
    }
}