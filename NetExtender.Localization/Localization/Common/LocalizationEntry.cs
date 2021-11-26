// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Culture;
using NetExtender.Utilities.Serialization;

namespace NetExtender.Localization.Common
{
    [Serializable]
    public readonly struct LocalizationEntry : IComparable<LocalizationEntry>, IEquatable<LocalizationEntry>
    {
        public static Boolean operator ==(LocalizationEntry first, LocalizationEntry second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(LocalizationEntry first, LocalizationEntry second)
        {
            return !(first == second);
        }
        
        public String? Key { get; }
        public LCID Lcid { get; }
        
        public LocalizationEntry(String? key, LCID lcid)
        {
            Key = key;
            Lcid = lcid;
        }

        public void Deconstruct(out String? key, out LCID lcid)
        {
            key = Key;
            lcid = Lcid;
        }
        
        public Int32 CompareTo(LocalizationEntry other)
        {
            Int32 compare = Lcid.Code.CompareTo(other.Lcid.Code);
            return compare != 0 ? compare : StringComparer.Ordinal.Compare(Key, other.Key);
        }

        public Boolean Equals(LocalizationEntry other)
        {
            return Lcid == other.Lcid && Key == other.Key;
        }
        
        public override Boolean Equals(Object? obj)
        {
            return obj is LocalizationEntry other && Equals(other);
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Key, Lcid);
        }

        public override String ToString()
        {
            return this.JsonSerializeObject();
        }
    }
}