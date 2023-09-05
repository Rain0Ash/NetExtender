// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MassTransit
{
    internal struct NewId
    {
    }
}

namespace NetExtender.StrongId
{
    [Flags]
    public enum StrongIdParseType : UInt32
    {
        None = 0,
        ParseSpan = 1,
        ParseSpanDirect = 2 | ParseSpan,
        ParseSpanProvider = 4,
        ParseSpanProviderDirect = 8 | ParseSpanProvider,
        ParseSpanNumber = 16,
        ParseSpanNumberDirect = 32 | ParseSpanNumber,
        ParseSpanNumberProvider = 64,
        ParseSpanNumberProviderDirect = 128 | ParseSpanNumberProvider,
        ParseString = 256,
        ParseStringDirect = 512 | ParseString,
        ParseStringProvider = 1024,
        ParseStringProviderDirect = 2048 | ParseStringProvider,
        ParseStringNumber = 4096,
        ParseStringNumberDirect = 8192 | ParseStringNumber,
        ParseStringNumberProvider = 16384,
        ParseStringNumberProviderDirect = 32768 | ParseStringNumberProvider,
        TryParseSpan = 65536,
        TryParseSpanDirect = 131072 | TryParseSpan,
        TryParseSpanProvider = 262144,
        TryParseSpanProviderDirect = 524288 | TryParseSpanProvider,
        TryParseSpanNumber = 1048576,
        TryParseSpanNumberDirect = 2097152 | TryParseSpanNumber,
        TryParseSpanNumberProvider = 4194304,
        TryParseSpanNumberProviderDirect = 8388608 | TryParseSpanNumberProvider,
        TryParseString = 16777216,
        TryParseStringDirect = 33554432 | TryParseString,
        TryParseStringProvider = 67108864,
        TryParseStringProviderDirect = 134217728 | TryParseStringProvider,
        TryParseStringNumber = 268435456,
        TryParseStringNumberDirect = 536870912 | TryParseStringNumber,
        TryParseStringNumberProvider = 1073741824,
        TryParseStringNumberProviderDirect = 2147483648 | TryParseStringNumberProvider,
        
        NumberSpan = ParseSpanDirect | ParseSpanProvider | ParseSpanNumber | ParseSpanNumberProviderDirect,
        NumberString = ParseStringDirect | ParseStringProvider | ParseStringNumber | ParseStringNumberProviderDirect,
        TryNumberSpan = TryParseSpanDirect | TryParseSpanProvider | TryParseSpanNumber | TryParseSpanNumberProviderDirect,
        TryNumberString = TryParseStringDirect | TryParseStringProvider | TryParseStringNumber | TryParseStringNumberProviderDirect,
        Number = NumberSpan | NumberString | TryNumberSpan | TryNumberString,
        String = None,
        Guid = ParseSpanDirect | ParseStringDirect | TryParseSpanDirect | TryParseStringDirect
    }

    [Flags]
    public enum StrongIdFormatType : Byte
    {
        None = 0,
        Provider = 1,
        ProviderDirect = 2 | Provider,
        Format = 4,
        FormatDirect = 8 | Format,
        FormatProvider = 16,
        FormatProviderDirect = 32 | FormatProvider,
        
        Number = ProviderDirect | FormatDirect | FormatProviderDirect,
        String = None,
        Guid = Provider | FormatDirect | FormatProviderDirect
    }

    public static class StrongIdUtilities
    {
        public static class Categories
        {
            public const String Usage = nameof(Usage);
        }

        internal struct TYPE
        {
        }

        internal struct TYPENAME
        {
        }

        internal struct STRONGID
        {
        }

        internal struct UNDERLYING
        {
        }

        internal struct SWAGGERTYPE
        {
        }

        internal struct SWAGGERFORMAT
        {
        }

        internal struct NULLABLE
        {
        }

        internal struct NUMBERSTYLE
        {
        }

        internal struct ACCESSIBILITY
        {
        }

        internal struct INTERFACES
        {
        }

        private static String[] Separator { get; } = { ", " };
        public static String[] Split<T>(this T value) where T : unmanaged, Enum
        {
            return value.ToString().Split(Separator, StringSplitOptions.RemoveEmptyEntries);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? As<T>(this TypedConstant value) where T : unmanaged, Enum
        {
            Object? item = value.Value;
            return item is not null ? (T) item : null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IncrementalValuesProvider<T> WhereNotNull<T>(this IncrementalValuesProvider<T?> source)
        {
            return source.Where(item => item is not null)!;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsDeclaration(this SyntaxKind value)
        {
            return value switch
            {
                SyntaxKind.ClassDeclaration => true,
                SyntaxKind.StructDeclaration => true,
                SyntaxKind.RecordDeclaration => true,
                _ => false
            };
        }

        public static Accessibility GetAccessibility(this TypeDeclarationSyntax syntax)
        {
            SyntaxTokenList modifiers = syntax.Modifiers;

            if (modifiers.Any(SyntaxKind.PublicKeyword))
            {
                return Accessibility.Public;
            }

            if (modifiers.Any(SyntaxKind.PrivateKeyword))
            {
                return modifiers.Any(SyntaxKind.ProtectedKeyword) ? Accessibility.ProtectedAndInternal : Accessibility.Private;
            }

            if (modifiers.Any(SyntaxKind.InternalKeyword))
            {
                return modifiers.Any(SyntaxKind.ProtectedKeyword) ? Accessibility.ProtectedOrInternal : Accessibility.Internal;
            }

            return modifiers.Any(SyntaxKind.ProtectedKeyword) ? Accessibility.Protected : Accessibility.NotApplicable;
        }

        public static String ToAccessibilityString(this Accessibility value)
        {
            return value switch
            {
                Accessibility.Public => "public",
                Accessibility.Internal => "internal",
                Accessibility.ProtectedOrInternal => "protected internal",
                Accessibility.Protected => "protected",
                Accessibility.ProtectedAndInternal => "private protected",
                Accessibility.Private => "private",
                _ => String.Empty
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNullable(this StrongIdUnderlyingType type)
        {
            return type switch
            {
                StrongIdUnderlyingType.SByte => false,
                StrongIdUnderlyingType.SByteNullable => true,
                StrongIdUnderlyingType.Byte => false,
                StrongIdUnderlyingType.ByteNullable => true,
                StrongIdUnderlyingType.Int16 => false,
                StrongIdUnderlyingType.Int16Nullable => true,
                StrongIdUnderlyingType.UInt16 => false,
                StrongIdUnderlyingType.UInt16Nullable => true,
                StrongIdUnderlyingType.Int32 => false,
                StrongIdUnderlyingType.Int32Nullable => true,
                StrongIdUnderlyingType.UInt32 => false,
                StrongIdUnderlyingType.UInt32Nullable => true,
                StrongIdUnderlyingType.Int64 => false,
                StrongIdUnderlyingType.Int64Nullable => true,
                StrongIdUnderlyingType.UInt64 => false,
                StrongIdUnderlyingType.UInt64Nullable => true,
                StrongIdUnderlyingType.Single => false,
                StrongIdUnderlyingType.SingleNullable => true,
                StrongIdUnderlyingType.Double => false,
                StrongIdUnderlyingType.DoubleNullable => true,
                StrongIdUnderlyingType.Decimal => false,
                StrongIdUnderlyingType.DecimalNullable => true,
                StrongIdUnderlyingType.BigInteger => false,
                StrongIdUnderlyingType.BigIntegerNullable => true,
                StrongIdUnderlyingType.String => false,
                StrongIdUnderlyingType.StringCurrentCulture => false,
                StrongIdUnderlyingType.StringCurrentCultureIgnoreCase => false,
                StrongIdUnderlyingType.StringInvariantCulture => false,
                StrongIdUnderlyingType.StringInvariantCultureIgnoreCase => false,
                StrongIdUnderlyingType.StringOrdinal => false,
                StrongIdUnderlyingType.StringOrdinalIgnoreCase => false,
                StrongIdUnderlyingType.StringNullable => true,
                StrongIdUnderlyingType.StringNullableCurrentCulture => true,
                StrongIdUnderlyingType.StringNullableCurrentCultureIgnoreCase => true,
                StrongIdUnderlyingType.StringNullableInvariantCulture => true,
                StrongIdUnderlyingType.StringNullableInvariantCultureIgnoreCase => true,
                StrongIdUnderlyingType.StringNullableOrdinal => true,
                StrongIdUnderlyingType.StringNullableOrdinalIgnoreCase => true,
                StrongIdUnderlyingType.Guid => false,
                StrongIdUnderlyingType.GuidNullable => true,
                StrongIdUnderlyingType.NewId => false,
                _ => throw new InvalidOperationException($"Invalid enum type: {type}")
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsString(this StrongIdUnderlyingType type)
        {
            return type switch
            {
                StrongIdUnderlyingType.SByte => false,
                StrongIdUnderlyingType.SByteNullable => false,
                StrongIdUnderlyingType.Byte => false,
                StrongIdUnderlyingType.ByteNullable => false,
                StrongIdUnderlyingType.Int16 => false,
                StrongIdUnderlyingType.Int16Nullable => false,
                StrongIdUnderlyingType.UInt16 => false,
                StrongIdUnderlyingType.UInt16Nullable => false,
                StrongIdUnderlyingType.Int32 => false,
                StrongIdUnderlyingType.Int32Nullable => false,
                StrongIdUnderlyingType.UInt32 => false,
                StrongIdUnderlyingType.UInt32Nullable => false,
                StrongIdUnderlyingType.Int64 => false,
                StrongIdUnderlyingType.Int64Nullable => false,
                StrongIdUnderlyingType.UInt64 => false,
                StrongIdUnderlyingType.UInt64Nullable => false,
                StrongIdUnderlyingType.Single => false,
                StrongIdUnderlyingType.SingleNullable => false,
                StrongIdUnderlyingType.Double => false,
                StrongIdUnderlyingType.DoubleNullable => false,
                StrongIdUnderlyingType.Decimal => false,
                StrongIdUnderlyingType.DecimalNullable => false,
                StrongIdUnderlyingType.BigInteger => false,
                StrongIdUnderlyingType.BigIntegerNullable => false,
                StrongIdUnderlyingType.String => true,
                StrongIdUnderlyingType.StringCurrentCulture => true,
                StrongIdUnderlyingType.StringCurrentCultureIgnoreCase => true,
                StrongIdUnderlyingType.StringInvariantCulture => true,
                StrongIdUnderlyingType.StringInvariantCultureIgnoreCase => true,
                StrongIdUnderlyingType.StringOrdinal => true,
                StrongIdUnderlyingType.StringOrdinalIgnoreCase => true,
                StrongIdUnderlyingType.StringNullable => true,
                StrongIdUnderlyingType.StringNullableCurrentCulture => true,
                StrongIdUnderlyingType.StringNullableCurrentCultureIgnoreCase => true,
                StrongIdUnderlyingType.StringNullableInvariantCulture => true,
                StrongIdUnderlyingType.StringNullableInvariantCultureIgnoreCase => true,
                StrongIdUnderlyingType.StringNullableOrdinal => true,
                StrongIdUnderlyingType.StringNullableOrdinalIgnoreCase => true,
                StrongIdUnderlyingType.Guid => false,
                StrongIdUnderlyingType.GuidNullable => false,
                StrongIdUnderlyingType.NewId => false,
                _ => throw new InvalidOperationException($"Invalid enum type: {type}")
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsCultureString(this StrongIdUnderlyingType type)
        {
            return type switch
            {
                StrongIdUnderlyingType.SByte => false,
                StrongIdUnderlyingType.SByteNullable => false,
                StrongIdUnderlyingType.Byte => false,
                StrongIdUnderlyingType.ByteNullable => false,
                StrongIdUnderlyingType.Int16 => false,
                StrongIdUnderlyingType.Int16Nullable => false,
                StrongIdUnderlyingType.UInt16 => false,
                StrongIdUnderlyingType.UInt16Nullable => false,
                StrongIdUnderlyingType.Int32 => false,
                StrongIdUnderlyingType.Int32Nullable => false,
                StrongIdUnderlyingType.UInt32 => false,
                StrongIdUnderlyingType.UInt32Nullable => false,
                StrongIdUnderlyingType.Int64 => false,
                StrongIdUnderlyingType.Int64Nullable => false,
                StrongIdUnderlyingType.UInt64 => false,
                StrongIdUnderlyingType.UInt64Nullable => false,
                StrongIdUnderlyingType.Single => false,
                StrongIdUnderlyingType.SingleNullable => false,
                StrongIdUnderlyingType.Double => false,
                StrongIdUnderlyingType.DoubleNullable => false,
                StrongIdUnderlyingType.Decimal => false,
                StrongIdUnderlyingType.DecimalNullable => false,
                StrongIdUnderlyingType.BigInteger => false,
                StrongIdUnderlyingType.BigIntegerNullable => false,
                StrongIdUnderlyingType.String => false,
                StrongIdUnderlyingType.StringCurrentCulture => true,
                StrongIdUnderlyingType.StringCurrentCultureIgnoreCase => true,
                StrongIdUnderlyingType.StringInvariantCulture => true,
                StrongIdUnderlyingType.StringInvariantCultureIgnoreCase => true,
                StrongIdUnderlyingType.StringOrdinal => true,
                StrongIdUnderlyingType.StringOrdinalIgnoreCase => true,
                StrongIdUnderlyingType.StringNullable => false,
                StrongIdUnderlyingType.StringNullableCurrentCulture => true,
                StrongIdUnderlyingType.StringNullableCurrentCultureIgnoreCase => true,
                StrongIdUnderlyingType.StringNullableInvariantCulture => true,
                StrongIdUnderlyingType.StringNullableInvariantCultureIgnoreCase => true,
                StrongIdUnderlyingType.StringNullableOrdinal => true,
                StrongIdUnderlyingType.StringNullableOrdinalIgnoreCase => true,
                StrongIdUnderlyingType.Guid => false,
                StrongIdUnderlyingType.GuidNullable => false,
                StrongIdUnderlyingType.NewId => false,
                _ => throw new InvalidOperationException($"Invalid enum type: {type}")
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsIgnoreCase(this StrongIdUnderlyingType type)
        {
            return type switch
            {
                StrongIdUnderlyingType.SByte => false,
                StrongIdUnderlyingType.SByteNullable => false,
                StrongIdUnderlyingType.Byte => false,
                StrongIdUnderlyingType.ByteNullable => false,
                StrongIdUnderlyingType.Int16 => false,
                StrongIdUnderlyingType.Int16Nullable => false,
                StrongIdUnderlyingType.UInt16 => false,
                StrongIdUnderlyingType.UInt16Nullable => false,
                StrongIdUnderlyingType.Int32 => false,
                StrongIdUnderlyingType.Int32Nullable => false,
                StrongIdUnderlyingType.UInt32 => false,
                StrongIdUnderlyingType.UInt32Nullable => false,
                StrongIdUnderlyingType.Int64 => false,
                StrongIdUnderlyingType.Int64Nullable => false,
                StrongIdUnderlyingType.UInt64 => false,
                StrongIdUnderlyingType.UInt64Nullable => false,
                StrongIdUnderlyingType.Single => false,
                StrongIdUnderlyingType.SingleNullable => false,
                StrongIdUnderlyingType.Double => false,
                StrongIdUnderlyingType.DoubleNullable => false,
                StrongIdUnderlyingType.Decimal => false,
                StrongIdUnderlyingType.DecimalNullable => false,
                StrongIdUnderlyingType.BigInteger => false,
                StrongIdUnderlyingType.BigIntegerNullable => false,
                StrongIdUnderlyingType.String => false,
                StrongIdUnderlyingType.StringCurrentCulture => false,
                StrongIdUnderlyingType.StringCurrentCultureIgnoreCase => true,
                StrongIdUnderlyingType.StringInvariantCulture => false,
                StrongIdUnderlyingType.StringInvariantCultureIgnoreCase => true,
                StrongIdUnderlyingType.StringOrdinal => false,
                StrongIdUnderlyingType.StringOrdinalIgnoreCase => true,
                StrongIdUnderlyingType.StringNullable => false,
                StrongIdUnderlyingType.StringNullableCurrentCulture => false,
                StrongIdUnderlyingType.StringNullableCurrentCultureIgnoreCase => true,
                StrongIdUnderlyingType.StringNullableInvariantCulture => false,
                StrongIdUnderlyingType.StringNullableInvariantCultureIgnoreCase => true,
                StrongIdUnderlyingType.StringNullableOrdinal => false,
                StrongIdUnderlyingType.StringNullableOrdinalIgnoreCase => true,
                StrongIdUnderlyingType.Guid => false,
                StrongIdUnderlyingType.GuidNullable => false,
                StrongIdUnderlyingType.NewId => false,
                _ => throw new InvalidOperationException($"Invalid enum type: {type}")
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (String FullName, String Name) UnderlyingType(this StrongIdUnderlyingType type)
        {
            return type switch
            {
                StrongIdUnderlyingType.SByte => (typeof(SByte).FullName, nameof(SByte)),
                StrongIdUnderlyingType.SByteNullable => (typeof(SByte).FullName + "?", nameof(SByte) + "?"),
                StrongIdUnderlyingType.Byte => (typeof(Byte).FullName, nameof(Byte)),
                StrongIdUnderlyingType.ByteNullable => (typeof(Byte).FullName + "?", nameof(Byte) + "?"),
                StrongIdUnderlyingType.Int16 => (typeof(Int16).FullName, nameof(Int16)),
                StrongIdUnderlyingType.Int16Nullable => (typeof(Int16).FullName + "?", nameof(Int16) + "?"),
                StrongIdUnderlyingType.UInt16 => (typeof(UInt16).FullName, nameof(UInt16)),
                StrongIdUnderlyingType.UInt16Nullable => (typeof(UInt16).FullName + "?", nameof(UInt16) + "?"),
                StrongIdUnderlyingType.Int32 => (typeof(Int32).FullName, nameof(Int32)),
                StrongIdUnderlyingType.Int32Nullable => (typeof(Int32).FullName + "?", nameof(Int32) + "?"),
                StrongIdUnderlyingType.UInt32 => (typeof(UInt32).FullName, nameof(UInt32)),
                StrongIdUnderlyingType.UInt32Nullable => (typeof(UInt32).FullName + "?", nameof(UInt32) + "?"),
                StrongIdUnderlyingType.Int64 => (typeof(Int64).FullName, nameof(Int64)),
                StrongIdUnderlyingType.Int64Nullable => (typeof(Int64).FullName + "?", nameof(Int64) + "?"),
                StrongIdUnderlyingType.UInt64 => (typeof(UInt64).FullName, nameof(UInt64)),
                StrongIdUnderlyingType.UInt64Nullable => (typeof(UInt64).FullName + "?", nameof(UInt64) + "?"),
                StrongIdUnderlyingType.Single => (typeof(Single).FullName, nameof(Single)),
                StrongIdUnderlyingType.SingleNullable => (typeof(Single).FullName + "?", nameof(Single) + "?"),
                StrongIdUnderlyingType.Double => (typeof(Double).FullName, nameof(Double)),
                StrongIdUnderlyingType.DoubleNullable => (typeof(Double).FullName + "?", nameof(Double) + "?"),
                StrongIdUnderlyingType.Decimal => (typeof(Decimal).FullName, nameof(Decimal)),
                StrongIdUnderlyingType.DecimalNullable => (typeof(Decimal).FullName + "?", nameof(Decimal) + "?"),
                StrongIdUnderlyingType.BigInteger => (typeof(BigInteger).FullName, nameof(BigInteger)),
                StrongIdUnderlyingType.BigIntegerNullable => (typeof(BigInteger).FullName + "?", nameof(BigInteger) + "?"),
                StrongIdUnderlyingType.String => (typeof(String).FullName, nameof(String)),
                StrongIdUnderlyingType.StringCurrentCulture => (typeof(String).FullName, nameof(String)),
                StrongIdUnderlyingType.StringCurrentCultureIgnoreCase => (typeof(String).FullName, nameof(String)),
                StrongIdUnderlyingType.StringInvariantCulture => (typeof(String).FullName, nameof(String)),
                StrongIdUnderlyingType.StringInvariantCultureIgnoreCase => (typeof(String).FullName, nameof(String)),
                StrongIdUnderlyingType.StringOrdinal => (typeof(String).FullName, nameof(String)),
                StrongIdUnderlyingType.StringOrdinalIgnoreCase => (typeof(String).FullName, nameof(String)),
                StrongIdUnderlyingType.StringNullable => (typeof(String).FullName + "?", nameof(String) + "?"),
                StrongIdUnderlyingType.StringNullableCurrentCulture => (typeof(String).FullName + "?", nameof(String) + "?"),
                StrongIdUnderlyingType.StringNullableCurrentCultureIgnoreCase => (typeof(String).FullName + "?", nameof(String) + "?"),
                StrongIdUnderlyingType.StringNullableInvariantCulture => (typeof(String).FullName + "?", nameof(String) + "?"),
                StrongIdUnderlyingType.StringNullableInvariantCultureIgnoreCase => (typeof(String).FullName + "?", nameof(String) + "?"),
                StrongIdUnderlyingType.StringNullableOrdinal => (typeof(String).FullName + "?", nameof(String) + "?"),
                StrongIdUnderlyingType.StringNullableOrdinalIgnoreCase => (typeof(String).FullName + "?", nameof(String) + "?"),
                StrongIdUnderlyingType.Guid => (typeof(Guid).FullName, nameof(Guid)),
                StrongIdUnderlyingType.GuidNullable => (typeof(Guid).FullName + "?", nameof(Guid) + "?"),
                StrongIdUnderlyingType.NewId => (typeof(MassTransit.NewId).FullName, nameof(MassTransit.NewId)),
                _ => throw new InvalidOperationException($"Invalid enum type: {type}")
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (StrongIdParseType Type, NumberStyles? Style) Parseable(this StrongIdUnderlyingType type)
        {
            return type switch
            {
                StrongIdUnderlyingType.SByte => (StrongIdParseType.Number, NumberStyles.Integer),
                StrongIdUnderlyingType.SByteNullable => (StrongIdParseType.Number, NumberStyles.Integer),
                StrongIdUnderlyingType.Byte => (StrongIdParseType.Number, NumberStyles.Integer),
                StrongIdUnderlyingType.ByteNullable => (StrongIdParseType.Number, NumberStyles.Integer),
                StrongIdUnderlyingType.Int16 => (StrongIdParseType.Number, NumberStyles.Integer),
                StrongIdUnderlyingType.Int16Nullable => (StrongIdParseType.Number, NumberStyles.Integer),
                StrongIdUnderlyingType.UInt16 => (StrongIdParseType.Number, NumberStyles.Integer),
                StrongIdUnderlyingType.UInt16Nullable => (StrongIdParseType.Number, NumberStyles.Integer),
                StrongIdUnderlyingType.Int32 => (StrongIdParseType.Number, NumberStyles.Integer),
                StrongIdUnderlyingType.Int32Nullable => (StrongIdParseType.Number, NumberStyles.Integer),
                StrongIdUnderlyingType.UInt32 => (StrongIdParseType.Number, NumberStyles.Integer),
                StrongIdUnderlyingType.UInt32Nullable => (StrongIdParseType.Number, NumberStyles.Integer),
                StrongIdUnderlyingType.Int64 => (StrongIdParseType.Number, NumberStyles.Integer),
                StrongIdUnderlyingType.Int64Nullable => (StrongIdParseType.Number, NumberStyles.Integer),
                StrongIdUnderlyingType.UInt64 => (StrongIdParseType.Number, NumberStyles.Integer),
                StrongIdUnderlyingType.UInt64Nullable => (StrongIdParseType.Number, NumberStyles.Integer),
                StrongIdUnderlyingType.Single => (StrongIdParseType.Number, NumberStyles.Float | NumberStyles.AllowThousands),
                StrongIdUnderlyingType.SingleNullable => (StrongIdParseType.Number, NumberStyles.Float | NumberStyles.AllowThousands),
                StrongIdUnderlyingType.Double => (StrongIdParseType.Number, NumberStyles.Float | NumberStyles.AllowThousands),
                StrongIdUnderlyingType.DoubleNullable => (StrongIdParseType.Number, NumberStyles.Float | NumberStyles.AllowThousands),
                StrongIdUnderlyingType.Decimal => (StrongIdParseType.Number, NumberStyles.Number),
                StrongIdUnderlyingType.DecimalNullable => (StrongIdParseType.Number, NumberStyles.Number),
                StrongIdUnderlyingType.BigInteger => (StrongIdParseType.Number, NumberStyles.Integer),
                StrongIdUnderlyingType.BigIntegerNullable => (StrongIdParseType.Number, NumberStyles.Integer),
                StrongIdUnderlyingType.String => (StrongIdParseType.String, null),
                StrongIdUnderlyingType.StringCurrentCulture => (StrongIdParseType.String, null),
                StrongIdUnderlyingType.StringCurrentCultureIgnoreCase => (StrongIdParseType.String, null),
                StrongIdUnderlyingType.StringInvariantCulture => (StrongIdParseType.String, null),
                StrongIdUnderlyingType.StringInvariantCultureIgnoreCase => (StrongIdParseType.String, null),
                StrongIdUnderlyingType.StringOrdinal => (StrongIdParseType.String, null),
                StrongIdUnderlyingType.StringOrdinalIgnoreCase => (StrongIdParseType.String, null),
                StrongIdUnderlyingType.StringNullable => (StrongIdParseType.String, null),
                StrongIdUnderlyingType.StringNullableCurrentCulture => (StrongIdParseType.String, null),
                StrongIdUnderlyingType.StringNullableCurrentCultureIgnoreCase => (StrongIdParseType.String, null),
                StrongIdUnderlyingType.StringNullableInvariantCulture => (StrongIdParseType.String, null),
                StrongIdUnderlyingType.StringNullableInvariantCultureIgnoreCase => (StrongIdParseType.String, null),
                StrongIdUnderlyingType.StringNullableOrdinal => (StrongIdParseType.String, null),
                StrongIdUnderlyingType.StringNullableOrdinalIgnoreCase => (StrongIdParseType.String, null),
                StrongIdUnderlyingType.Guid => (StrongIdParseType.Guid, null),
                StrongIdUnderlyingType.GuidNullable => (StrongIdParseType.Guid, null),
                StrongIdUnderlyingType.NewId => (StrongIdParseType.None, null),
                _ => throw new InvalidOperationException($"Invalid enum type: {type}")
            };
        }
        
        public static (StrongIdParseType Parse, StrongIdParseType Direct) Split(this StrongIdParseType value)
        {
            // ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault
            return value switch
            {
                StrongIdParseType.None => (StrongIdParseType.None, StrongIdParseType.None),
                StrongIdParseType.ParseSpan => (StrongIdParseType.ParseSpan, StrongIdParseType.ParseSpanDirect),
                StrongIdParseType.ParseSpanDirect => (StrongIdParseType.ParseSpan, StrongIdParseType.ParseSpanDirect),
                StrongIdParseType.ParseSpanProvider => (StrongIdParseType.ParseSpanProvider, StrongIdParseType.ParseSpanProviderDirect),
                StrongIdParseType.ParseSpanProviderDirect => (StrongIdParseType.ParseSpanProvider, StrongIdParseType.ParseSpanProviderDirect),
                StrongIdParseType.ParseSpanNumber => (StrongIdParseType.ParseSpanNumber, StrongIdParseType.ParseSpanNumberDirect),
                StrongIdParseType.ParseSpanNumberDirect => (StrongIdParseType.ParseSpanNumber, StrongIdParseType.ParseSpanNumberDirect),
                StrongIdParseType.ParseSpanNumberProvider => (StrongIdParseType.ParseSpanNumberProvider, StrongIdParseType.ParseSpanNumberProviderDirect),
                StrongIdParseType.ParseSpanNumberProviderDirect => (StrongIdParseType.ParseSpanNumberProvider, StrongIdParseType.ParseSpanNumberProviderDirect),
                StrongIdParseType.ParseString => (StrongIdParseType.ParseString, StrongIdParseType.ParseStringDirect),
                StrongIdParseType.ParseStringDirect => (StrongIdParseType.ParseString, StrongIdParseType.ParseStringDirect),
                StrongIdParseType.ParseStringProvider => (StrongIdParseType.ParseStringProvider, StrongIdParseType.ParseStringProviderDirect),
                StrongIdParseType.ParseStringProviderDirect => (StrongIdParseType.ParseStringProvider, StrongIdParseType.ParseStringProviderDirect),
                StrongIdParseType.ParseStringNumberDirect => (StrongIdParseType.ParseStringNumber, StrongIdParseType.ParseStringNumberDirect),
                StrongIdParseType.ParseStringNumber => (StrongIdParseType.ParseStringNumber, StrongIdParseType.ParseStringNumberDirect),
                StrongIdParseType.ParseStringNumberProvider => (StrongIdParseType.ParseStringNumberProvider, StrongIdParseType.ParseStringNumberProviderDirect),
                StrongIdParseType.ParseStringNumberProviderDirect => (StrongIdParseType.ParseStringNumberProvider, StrongIdParseType.ParseStringNumberProviderDirect),
                StrongIdParseType.TryParseSpan => (StrongIdParseType.TryParseSpan, StrongIdParseType.TryParseSpanDirect),
                StrongIdParseType.TryParseSpanDirect => (StrongIdParseType.TryParseSpan, StrongIdParseType.TryParseSpanDirect),
                StrongIdParseType.TryParseSpanProvider => (StrongIdParseType.TryParseSpanProvider, StrongIdParseType.TryParseSpanProviderDirect),
                StrongIdParseType.TryParseSpanProviderDirect => (StrongIdParseType.TryParseSpanProvider, StrongIdParseType.TryParseSpanProviderDirect),
                StrongIdParseType.TryParseSpanNumber => (StrongIdParseType.TryParseSpanNumber, StrongIdParseType.TryParseSpanNumberDirect),
                StrongIdParseType.TryParseSpanNumberDirect => (StrongIdParseType.TryParseSpanNumber, StrongIdParseType.TryParseSpanNumberDirect),
                StrongIdParseType.TryParseSpanNumberProvider => (StrongIdParseType.TryParseSpanNumberProvider, StrongIdParseType.TryParseSpanNumberProviderDirect),
                StrongIdParseType.TryParseSpanNumberProviderDirect => (StrongIdParseType.TryParseSpanNumberProvider, StrongIdParseType.TryParseSpanNumberProviderDirect),
                StrongIdParseType.TryParseString => (StrongIdParseType.TryParseString, StrongIdParseType.TryParseStringDirect),
                StrongIdParseType.TryParseStringDirect => (StrongIdParseType.TryParseString, StrongIdParseType.TryParseStringDirect),
                StrongIdParseType.TryParseStringProvider => (StrongIdParseType.TryParseStringProvider, StrongIdParseType.TryParseStringProviderDirect),
                StrongIdParseType.TryParseStringProviderDirect => (StrongIdParseType.TryParseStringProvider, StrongIdParseType.TryParseStringProviderDirect),
                StrongIdParseType.TryParseStringNumber => (StrongIdParseType.TryParseStringNumber, StrongIdParseType.TryParseStringNumberDirect),
                StrongIdParseType.TryParseStringNumberDirect => (StrongIdParseType.TryParseStringNumber, StrongIdParseType.TryParseStringNumberDirect),
                StrongIdParseType.TryParseStringNumberProvider => (StrongIdParseType.TryParseStringNumberProvider, StrongIdParseType.TryParseStringNumberProviderDirect),
                StrongIdParseType.TryParseStringNumberProviderDirect => (StrongIdParseType.TryParseStringNumberProvider, StrongIdParseType.TryParseStringNumberProviderDirect),
                _ => throw new InvalidOperationException()
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StrongIdFormatType Formattable(this StrongIdUnderlyingType type)
        {
            return type switch
            {
                StrongIdUnderlyingType.SByte => StrongIdFormatType.Number,
                StrongIdUnderlyingType.SByteNullable => StrongIdFormatType.Number,
                StrongIdUnderlyingType.Byte => StrongIdFormatType.Number,
                StrongIdUnderlyingType.ByteNullable => StrongIdFormatType.Number,
                StrongIdUnderlyingType.Int16 => StrongIdFormatType.Number,
                StrongIdUnderlyingType.Int16Nullable => StrongIdFormatType.Number,
                StrongIdUnderlyingType.UInt16 => StrongIdFormatType.Number,
                StrongIdUnderlyingType.UInt16Nullable => StrongIdFormatType.Number,
                StrongIdUnderlyingType.Int32 => StrongIdFormatType.Number,
                StrongIdUnderlyingType.Int32Nullable => StrongIdFormatType.Number,
                StrongIdUnderlyingType.UInt32 => StrongIdFormatType.Number,
                StrongIdUnderlyingType.UInt32Nullable => StrongIdFormatType.Number,
                StrongIdUnderlyingType.Int64 => StrongIdFormatType.Number,
                StrongIdUnderlyingType.Int64Nullable => StrongIdFormatType.Number,
                StrongIdUnderlyingType.UInt64 => StrongIdFormatType.Number,
                StrongIdUnderlyingType.UInt64Nullable => StrongIdFormatType.Number,
                StrongIdUnderlyingType.Single => StrongIdFormatType.Number,
                StrongIdUnderlyingType.SingleNullable => StrongIdFormatType.Number,
                StrongIdUnderlyingType.Double => StrongIdFormatType.Number,
                StrongIdUnderlyingType.DoubleNullable => StrongIdFormatType.Number,
                StrongIdUnderlyingType.Decimal => StrongIdFormatType.Number,
                StrongIdUnderlyingType.DecimalNullable => StrongIdFormatType.Number,
                StrongIdUnderlyingType.BigInteger => StrongIdFormatType.Number,
                StrongIdUnderlyingType.BigIntegerNullable => StrongIdFormatType.Number,
                StrongIdUnderlyingType.String => StrongIdFormatType.String,
                StrongIdUnderlyingType.StringCurrentCulture => StrongIdFormatType.String,
                StrongIdUnderlyingType.StringCurrentCultureIgnoreCase => StrongIdFormatType.String,
                StrongIdUnderlyingType.StringInvariantCulture => StrongIdFormatType.String,
                StrongIdUnderlyingType.StringInvariantCultureIgnoreCase => StrongIdFormatType.String,
                StrongIdUnderlyingType.StringOrdinal => StrongIdFormatType.String,
                StrongIdUnderlyingType.StringOrdinalIgnoreCase => StrongIdFormatType.String,
                StrongIdUnderlyingType.StringNullable => StrongIdFormatType.String,
                StrongIdUnderlyingType.StringNullableCurrentCulture => StrongIdFormatType.String,
                StrongIdUnderlyingType.StringNullableCurrentCultureIgnoreCase => StrongIdFormatType.String,
                StrongIdUnderlyingType.StringNullableInvariantCulture => StrongIdFormatType.String,
                StrongIdUnderlyingType.StringNullableInvariantCultureIgnoreCase => StrongIdFormatType.String,
                StrongIdUnderlyingType.StringNullableOrdinal => StrongIdFormatType.String,
                StrongIdUnderlyingType.StringNullableOrdinalIgnoreCase => StrongIdFormatType.String,
                StrongIdUnderlyingType.Guid => StrongIdFormatType.Guid,
                StrongIdUnderlyingType.GuidNullable => StrongIdFormatType.Guid,
                StrongIdUnderlyingType.NewId => StrongIdFormatType.Guid,
                _ => throw new InvalidOperationException($"Invalid enum type: {type}")
            };
        }

        public static (String Type, String? Format) Swagger(this StrongIdUnderlyingType type)
        {
            return type switch
            {
                StrongIdUnderlyingType.SByte => ("integer", "int32"),
                StrongIdUnderlyingType.SByteNullable => ("integer", "int32"),
                StrongIdUnderlyingType.Byte => ("integer", "int32"),
                StrongIdUnderlyingType.ByteNullable => ("integer", "int32"),
                StrongIdUnderlyingType.Int16 => ("integer", "int32"),
                StrongIdUnderlyingType.Int16Nullable => ("integer", "int32"),
                StrongIdUnderlyingType.UInt16 => ("integer", "int32"),
                StrongIdUnderlyingType.UInt16Nullable => ("integer", "int32"),
                StrongIdUnderlyingType.Int32 => ("integer", "int32"),
                StrongIdUnderlyingType.Int32Nullable => ("integer", "int32"),
                StrongIdUnderlyingType.UInt32 => ("integer", "int32"),
                StrongIdUnderlyingType.UInt32Nullable => ("integer", "int32"),
                StrongIdUnderlyingType.Int64 => ("integer", "int64"),
                StrongIdUnderlyingType.Int64Nullable => ("integer", "int64"),
                StrongIdUnderlyingType.UInt64 => ("integer", "int64"),
                StrongIdUnderlyingType.UInt64Nullable => ("integer", "int64"),
                StrongIdUnderlyingType.Single => ("number", "float"),
                StrongIdUnderlyingType.SingleNullable => ("number", "float"),
                StrongIdUnderlyingType.Double => ("number", "double"),
                StrongIdUnderlyingType.DoubleNullable => ("number", "double"),
                StrongIdUnderlyingType.Decimal => ("number", "double"),
                StrongIdUnderlyingType.DecimalNullable => ("number", "double"),
                StrongIdUnderlyingType.BigInteger => ("integer", "int64"),
                StrongIdUnderlyingType.BigIntegerNullable => ("integer", "int64"),
                StrongIdUnderlyingType.String => ("string", ""),
                StrongIdUnderlyingType.StringCurrentCulture => ("string", ""),
                StrongIdUnderlyingType.StringCurrentCultureIgnoreCase => ("string", ""),
                StrongIdUnderlyingType.StringInvariantCulture => ("string", ""),
                StrongIdUnderlyingType.StringInvariantCultureIgnoreCase => ("string", ""),
                StrongIdUnderlyingType.StringOrdinal => ("string", ""),
                StrongIdUnderlyingType.StringOrdinalIgnoreCase => ("string", ""),
                StrongIdUnderlyingType.StringNullable => ("string", ""),
                StrongIdUnderlyingType.StringNullableCurrentCulture => ("string", ""),
                StrongIdUnderlyingType.StringNullableCurrentCultureIgnoreCase => ("string", ""),
                StrongIdUnderlyingType.StringNullableInvariantCulture => ("string", ""),
                StrongIdUnderlyingType.StringNullableInvariantCultureIgnoreCase => ("string", ""),
                StrongIdUnderlyingType.StringNullableOrdinal => ("string", ""),
                StrongIdUnderlyingType.StringNullableOrdinalIgnoreCase => ("string", ""),
                StrongIdUnderlyingType.Guid => ("string", "uuid"),
                StrongIdUnderlyingType.GuidNullable => ("string", "uuid"),
                StrongIdUnderlyingType.NewId => ("string", "uuid"),
                _ => throw new InvalidOperationException($"Invalid enum type: {type}")
            };
        }

        public static (StrongIdFormatType Format, StrongIdFormatType Direct) Split(this StrongIdFormatType value)
        {
            // ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault
            return value switch
            {
                StrongIdFormatType.None => (StrongIdFormatType.None, StrongIdFormatType.None),
                StrongIdFormatType.Provider => (StrongIdFormatType.Provider, StrongIdFormatType.ProviderDirect),
                StrongIdFormatType.ProviderDirect => (StrongIdFormatType.Provider, StrongIdFormatType.ProviderDirect),
                StrongIdFormatType.Format => (StrongIdFormatType.Format, StrongIdFormatType.FormatDirect),
                StrongIdFormatType.FormatDirect => (StrongIdFormatType.Format, StrongIdFormatType.FormatDirect),
                StrongIdFormatType.FormatProvider => (StrongIdFormatType.FormatProvider, StrongIdFormatType.FormatProviderDirect),
                StrongIdFormatType.FormatProviderDirect => (StrongIdFormatType.FormatProvider, StrongIdFormatType.FormatProviderDirect),
                _ => throw new InvalidOperationException()
            };
        }

        internal static String Indentation(this String value, Byte count)
        {
            if (String.IsNullOrEmpty(value) || count <= 0)
            {
                return value;
            }

            String[] split = value.Split('\n');
            StringBuilder builder = new StringBuilder();
            for (Int32 i = 0; i < split.Length; i++)
            {
                String line = split[i];
                if (!String.IsNullOrWhiteSpace(line))
                {
                    line = new String(' ', count * 4) + line;
                }

                builder.Append(line);

                if (i < split.Length - 1)
                {
                    builder.Append('\n');
                }
            }

            return builder.ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static String Remove(this String value, String remove)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (remove is null)
            {
                throw new ArgumentNullException(nameof(remove));
            }

            return value.Replace(remove, String.Empty);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static StringBuilder Remove(this StringBuilder builder, String remove)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (remove is null)
            {
                throw new ArgumentNullException(nameof(remove));
            }

            return builder.Replace(remove, String.Empty);
        }

        private static String TypeName(Type type)
        {
            if (!type.IsGenericType)
            {
                return type.Namespace is not null ? $"{type.Namespace}.{type.Name}" : type.Name;
            }

            String typename = type.GetGenericTypeDefinition().Name;
            typename = typename.Substring(0, typename.IndexOf('`'));
            Type[] generic = type.GetGenericArguments();
            String[] arguments = new String[generic.Length];
            for (Int32 i = 0; i < generic.Length; i++)
            {
                arguments[i] = TypeName(generic[i]);
            }
            
            return type.Namespace is not null ? $"{type.Namespace}.{typename}<{String.Join(", ", arguments)}>" : $"{typename}<{String.Join(", ", arguments)}>";
        }

        internal static StrongIdUnderlyingType Convert(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return TypeName(type) switch
            {
                "System.SByte" => StrongIdUnderlyingType.SByte,
                "System.Nullable<System.SByte>" => StrongIdUnderlyingType.SByteNullable,
                "System.Byte" => StrongIdUnderlyingType.Byte,
                "System.Nullable<System.Byte>" => StrongIdUnderlyingType.ByteNullable,
                "System.Int16" => StrongIdUnderlyingType.Int16,
                "System.Nullable<System.Int16>" => StrongIdUnderlyingType.Int16Nullable,
                "System.UInt16" => StrongIdUnderlyingType.UInt16,
                "System.Nullable<System.UInt16>" => StrongIdUnderlyingType.UInt16Nullable,
                "System.Int32" => StrongIdUnderlyingType.Int32,
                "System.Nullable<System.Int32>" => StrongIdUnderlyingType.Int32Nullable,
                "System.UInt32" => StrongIdUnderlyingType.UInt32,
                "System.Nullable<System.UInt32>" => StrongIdUnderlyingType.UInt32Nullable,
                "System.Int64" => StrongIdUnderlyingType.Int64,
                "System.Nullable<System.Int64>" => StrongIdUnderlyingType.Int64Nullable,
                "System.UInt64" => StrongIdUnderlyingType.UInt64,
                "System.Nullable<System.UInt64>" => StrongIdUnderlyingType.UInt64Nullable,
                "System.Single" => StrongIdUnderlyingType.Single,
                "System.Nullable<System.Single>" => StrongIdUnderlyingType.SingleNullable,
                "System.Double" => StrongIdUnderlyingType.Double,
                "System.Nullable<System.Double>" => StrongIdUnderlyingType.DoubleNullable,
                "System.Decimal" => StrongIdUnderlyingType.Decimal,
                "System.Nullable<System.Decimal>" => StrongIdUnderlyingType.DecimalNullable,
                "System.Numerics.BigInteger" => StrongIdUnderlyingType.BigInteger,
                "System.Nullable<System.Numerics.BigInteger>" => StrongIdUnderlyingType.BigIntegerNullable,
                "System.String" => StrongIdUnderlyingType.String,
                "System.Guid" => StrongIdUnderlyingType.Guid,
                "System.Nullable<System.Guid>" => StrongIdUnderlyingType.GuidNullable,
                "MassTransit.NewId" => StrongIdUnderlyingType.NewId,
                _ => throw new NotSupportedException($"Type '{type.FullName}' not supported.")
            };
        }
        
        internal static StrongIdUnderlyingType Convert(this ITypeSymbol type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            SymbolDisplayFormat format = new SymbolDisplayFormat(SymbolDisplayGlobalNamespaceStyle.Omitted, SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces, SymbolDisplayGenericsOptions.IncludeTypeParameters);

            return type.ToDisplayString(format) switch
            {
                "System.SByte" => StrongIdUnderlyingType.SByte,
                "System.SByte?" => StrongIdUnderlyingType.SByteNullable,
                "System.Byte" => StrongIdUnderlyingType.Byte,
                "System.Byte?" => StrongIdUnderlyingType.ByteNullable,
                "System.Int16" => StrongIdUnderlyingType.Int16,
                "System.Int16?" => StrongIdUnderlyingType.Int16Nullable,
                "System.UInt16" => StrongIdUnderlyingType.UInt16,
                "System.UInt16?" => StrongIdUnderlyingType.UInt16Nullable,
                "System.Int32" => StrongIdUnderlyingType.Int32,
                "System.Int32?" => StrongIdUnderlyingType.Int32Nullable,
                "System.UInt32" => StrongIdUnderlyingType.UInt32,
                "System.UInt32?" => StrongIdUnderlyingType.UInt32Nullable,
                "System.Int64" => StrongIdUnderlyingType.Int64,
                "System.Int64?" => StrongIdUnderlyingType.Int64Nullable,
                "System.UInt64" => StrongIdUnderlyingType.UInt64,
                "System.UInt64?" => StrongIdUnderlyingType.UInt64Nullable,
                "System.Single" => StrongIdUnderlyingType.Single,
                "System.Single?" => StrongIdUnderlyingType.SingleNullable,
                "System.Double" => StrongIdUnderlyingType.Double,
                "System.Double?" => StrongIdUnderlyingType.DoubleNullable,
                "System.Decimal" => StrongIdUnderlyingType.Decimal,
                "System.Decimal?" => StrongIdUnderlyingType.DecimalNullable,
                "System.Numerics.BigInteger" => StrongIdUnderlyingType.BigInteger,
                "System.Numerics.BigInteger?" => StrongIdUnderlyingType.BigIntegerNullable,
                "System.String" => StrongIdUnderlyingType.String,
                "System.Guid" => StrongIdUnderlyingType.Guid,
                "System.Guid?" => StrongIdUnderlyingType.GuidNullable,
                "MassTransit.NewId" => StrongIdUnderlyingType.NewId,
                _ => throw new NotSupportedException($"Type '{type.ToDisplayString(format)}' not supported.")
            };
        }
    }
}