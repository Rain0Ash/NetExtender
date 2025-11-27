using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;
using NetExtender.Types.Monads;
using NetExtender.Types.Reflection;
using NetExtender.Utilities.Core;

namespace NetExtender.Interfaces
{
    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderParsable<TSelf> : INetExtenderParsable, INetExtenderMethodOperator<TSelf>,
        INetExtenderOperator<TSelf, INetExtenderParsable<TSelf>, INetExtenderParsable<TSelf>.OperatorHandler, INetExtenderParsable<TSelf>.OperatorHandler.Set>
#if NET7_0_OR_GREATER
    , IParsable<TSelf>
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderParsable.Group | INetExtenderMethodOperator<TSelf>.Group;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderParsable<TSelf>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderParsable<TSelf>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderParsable<TSelf>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderParsable<TSelf>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderParsable<TSelf>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Parse(String value)
        {
            return Storage.Parse.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Parse(String value, IFormatProvider? provider)
        {
            return Storage.FormatParse.Invoke(value, provider);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(String value, [MaybeNullWhen(false)] out TSelf result)
        {
            return Storage.TryParse.Invoke(value, out result);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(String value, IFormatProvider? provider, [MaybeNullWhen(false)] out TSelf result)
        {
            return Storage.FormatTryParse.Invoke(value, provider, out result);
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderParsable<TSelf>, INetExtenderParsable<TSelf>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
                internal readonly ParseHandler<String, TSelf> Parse = null!;
                internal readonly FormatParseHandler<String, TSelf> FormatParse = null!;
                internal readonly TryParseHandler<String, TSelf> TryParse = null!;
                internal readonly FormatTryParseHandler<String, TSelf> FormatTryParse = null!;
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                ParseHandler<String, TSelf>? parse = Method<ParseHandler<String, TSelf>>(this, Parse);
                FormatParseHandler<String, TSelf>? fparse = Method<FormatParseHandler<String, TSelf>>(this, Parse);
                TryParseHandler<String, TSelf>? tparse = Method<TryParseHandler<String, TSelf>>(this, TryParse);
                FormatTryParseHandler<String, TSelf>? ftparse = Method<FormatTryParseHandler<String, TSelf>>(this, TryParse);

                if (parse is null && fparse is null && tparse is null && ftparse is null)
                {
                    yield return Exception<FormatParseHandler<String, TSelf>>(this, Parse);
                    yield break;
                }

                static Boolean TryParseFallback(String value, [MaybeNullWhen(false)] out TSelf result)
                {
                    return TryParse(value, null, out result);
                }

                static Boolean TryParseFormatFallback(String value, IFormatProvider? provider, [MaybeNullWhen(false)] out TSelf result)
                {
                    try
                    {
                        result = Parse(value, provider);
                        return true;
                    }
                    catch (Exception)
                    {
                        result = default;
                        return false;
                    }
                }

                Set(in set.Parse) = parse ?? (static value => Parse(value, null));
                Set(in set.FormatParse) = fparse ?? (static (value, provider) => TryParse(value, provider, out TSelf? result) ? result : throw Exception());
                Set(in set.TryParse) = tparse ?? TryParseFallback;
                Set(in set.FormatTryParse) = ftparse ?? TryParseFormatFallback;
            }
        }
    }

    public interface INetExtenderParsable : INetExtenderOperator
    {
        public new const NumericsInterfaceGroup Group = NumericsInterfaceGroup.Parse;

        [SuppressMessage("ReSharper", "IdentifierTypo")]
        [ReflectionSystemResource(typeof(Int32))]
        private static class SR
        {
            private static Type SRType { get; } = SRUtilities.SRType(typeof(Int32).Assembly);

            [ReflectionSystemResource(typeof(Int32))]
            public static SRInfo Format_InvalidString { get; } = new SRInfo(SRType);
        }

        protected static FormatException Exception()
        {
            return new FormatException(SR.Format_InvalidString);
        }
    }
}