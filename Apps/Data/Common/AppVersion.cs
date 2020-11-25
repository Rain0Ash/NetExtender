// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using NetExtender.Apps.Domains;
using NetExtender.Utils.Types;

namespace NetExtender.Apps.Data.Common
{
    [Serializable]
    public readonly struct AppVersion : IComparable<AppVersion>
    {
        public static AppVersion Default { get; } = new AppVersion(1);

        private const String ParsePattern = @"^(?<major>\d+)(?:\.(?<minor>\d+))?(?:\.(?<patch>\d+))?(?: (?<datetime>(?<date>\d{2}(?:\/|\.)\d{2}(?:\/|\.)\d{1,4})(?: (?<time>\d{1,2}:\d{1,2}:\d{1,2}))?))?$";
        private static readonly Regex ParseRegex = new Regex(ParsePattern, RegexOptions.Compiled);
        
        public static AppVersion Parse(String str)
        {
            if (String.IsNullOrWhiteSpace(str))
            {
                throw new ArgumentException(@"Value cannot be null or whitespace.", nameof(str));
            }
            
            CultureInfo info = CultureInfo.InvariantCulture;
            IDictionary<String, IList<String>> captures = ParseRegex.MatchNamedCaptures(str);
            UInt32 major = captures.TryGetValue(nameof(major), out IList<String> values) ? UInt32.Parse(values[0], info) : 1;
            UInt32 minor = captures.TryGetValue(nameof(minor), out values) ? UInt32.Parse(values[0], info) : 0;
            UInt32 patch = captures.TryGetValue(nameof(patch), out values) ? UInt32.Parse(values[0], info) : 0;
            DateTime? datetime = captures.TryGetValue(nameof(datetime), out values) ? DateTime.Parse(values[0], info) : (DateTime?) null;

            return new AppVersion(major, minor, patch, datetime);
        }

        public static Boolean TryParse(String str, out AppVersion version)
        {
            try
            {
                version = Parse(str);
                return true;
            }
            catch (Exception)
            {
                version = default;
                return false;
            }
        }
        
        public static implicit operator String(AppVersion data)
        {
            return data.ToString();
        }
        
        public static Boolean operator ==(AppVersion first, AppVersion second)
        {
            return first.CompareTo(second) == 0;
        }

        public static Boolean operator !=(AppVersion first, AppVersion second)
        {
            return !(first == second);
        }
        
        public static Boolean operator >(AppVersion first, AppVersion second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator <(AppVersion first, AppVersion second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator >=(AppVersion first, AppVersion second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static Boolean operator <=(AppVersion first, AppVersion second)
        {
            return first.CompareTo(second) <= 0;
        }

        public UInt32 Major { get; }
        public UInt32 Minor { get; }
        public UInt32 Patch { get; }
        
        public DateTime? BuildTime { get; }

        public String Version
        {
            get
            {
                String major = Major.ToString();
                String minor = Minor == 0 && Patch == 0 ? String.Empty : Minor.ToString();
                String patch = Patch == 0 ? String.Empty : Patch.ToString();

                return ".".Join(JoinType.NotWhiteSpace, major, minor, patch);
            }
        }

        public AppVersion(UInt32 major, UInt32 minor = 0, UInt32 patch = 0)
            : this(major, minor, patch, Domain.BuildDateTime)
        {
        }
        
        public AppVersion(UInt32 major, DateTime? time)
            : this(major, 0, 0, time)
        {
        }
        
        public AppVersion(UInt32 major, UInt32 minor, DateTime? time)
            : this(major, minor, 0, time)
        {
        }

        public AppVersion(UInt32 major, UInt32 minor, UInt32 patch, DateTime? time)
        {
            Major = major;
            Minor = minor;
            Patch = patch;
            BuildTime = time;
        }

        public Boolean Equals(AppVersion other)
        {
            return Major == other.Major && Minor == other.Minor && Patch == other.Patch && BuildTime.Equals(other.BuildTime);
        }

        public override Boolean Equals(Object obj)
        {
            return obj is AppVersion other && Equals(other);
        }

        public Int32 CompareTo(AppVersion other)
        {
            Int32 major = Major.CompareTo(other.Major);
            if (major != 0)
            {
                return major;
            }

            Int32 minor = Minor.CompareTo(other.Minor);
            if (minor != 0)
            {
                return minor;
            }

            Int32 patch = Patch.CompareTo(other.Patch);
            if (patch != 0)
            {
                return patch;
            }

            return BuildTime?.CompareTo(other.BuildTime) ?? -1;
        }
        
        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Major, Minor, Patch, BuildTime);
        }
        
        public override String ToString()
        {
            return $"{Version}{(BuildTime is not null ? $" {Convert.ToString(BuildTime, CultureInfo.InvariantCulture)}" : String.Empty)}";
        }
    }
}