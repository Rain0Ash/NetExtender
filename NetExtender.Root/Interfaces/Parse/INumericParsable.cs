using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Core;

namespace NetExtender.Interfaces
{
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface IInheritNumericParsable<TSelf> : INetExtenderNumericParsable<TSelf>, IInheritParsable<TSelf>
#if NET7_0_OR_GREATER
        where TSelf : IInheritNumericParsable<TSelf>?
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderNumericParsable<TSelf>.Group | IInheritParsable<TSelf>.Group;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumericParsable<TSelf>, INetExtenderNumericParsable<TSelf>.OperatorHandler, INetExtenderNumericParsable<TSelf>.OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static INetExtenderNumericParsable<TSelf>.OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumericParsable<TSelf>, INetExtenderNumericParsable<TSelf>.OperatorHandler, INetExtenderNumericParsable<TSelf>.OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static INetExtenderNumericParsable<TSelf>.OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumericParsable<TSelf>, INetExtenderNumericParsable<TSelf>.OperatorHandler, INetExtenderNumericParsable<TSelf>.OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static INetExtenderNumericParsable<TSelf>.OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumericParsable<TSelf>, INetExtenderNumericParsable<TSelf>.OperatorHandler, INetExtenderNumericParsable<TSelf>.OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderNumericParsable<TSelf>, INetExtenderNumericParsable<TSelf>.OperatorHandler, INetExtenderNumericParsable<TSelf>.OperatorHandler.Set>.Ensure();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Parse(String value, NumberStyles style)
        {
            return INetExtenderNumericParsable<TSelf>.Parse(value, style);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Parse(String value, NumberStyles style, IFormatProvider? provider)
        {
            return INetExtenderNumericParsable<TSelf>.Parse(value, style, provider);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean TryParse(String value, NumberStyles style, [MaybeNullWhen(false)] out TSelf result)
        {
            return INetExtenderNumericParsable<TSelf>.TryParse(value, style, out result);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean TryParse(String value, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out TSelf result)
        {
            return INetExtenderNumericParsable<TSelf>.TryParse(value, style, provider, out result);
        }

        protected new class OperatorHandler : NoHandler
        {
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderNumericParsable<TSelf> : INetExtenderParsable<TSelf>,
        INetExtenderOperator<TSelf, INetExtenderNumericParsable<TSelf>, INetExtenderNumericParsable<TSelf>.OperatorHandler, INetExtenderNumericParsable<TSelf>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = NumericsInterfaceGroup.NumericParse | INetExtenderParsable<TSelf>.Group;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumericParsable<TSelf>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumericParsable<TSelf>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumericParsable<TSelf>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumericParsable<TSelf>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderNumericParsable<TSelf>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Parse(String value, NumberStyles style)
        {
            return Storage.Parse.Invoke(value, style);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Parse(String value, NumberStyles style, IFormatProvider? provider)
        {
            return Storage.NumericParse.Invoke(value, style, provider);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(String value, NumberStyles style, [MaybeNullWhen(false)] out TSelf result)
        {
            return Storage.TryParse.Invoke(value, style, out result);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(String value, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out TSelf result)
        {
            return Storage.NumericTryParse.Invoke(value, style, provider, out result);
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderNumericParsable<TSelf>, INetExtenderNumericParsable<TSelf>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
                internal readonly StyleParseHandler<String, TSelf> Parse = null!;
                internal readonly NumericParseHandler<String, TSelf> NumericParse = null!;
                internal readonly StyleTryParseHandler<String, TSelf> TryParse = null!;
                internal readonly NumericTryParseHandler<String, TSelf> NumericTryParse = null!;
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderParsable<TSelf>.SafeHandler;

                StyleParseHandler<String, TSelf>? parse = Initialize<StyleParseHandler<String, TSelf>>(this, Parse);
                NumericParseHandler<String, TSelf>? fparse = Initialize<NumericParseHandler<String, TSelf>>(this, Parse);
                StyleTryParseHandler<String, TSelf>? tparse = Initialize<StyleTryParseHandler<String, TSelf>>(this, TryParse);
                NumericTryParseHandler<String, TSelf>? ftparse = Initialize<NumericTryParseHandler<String, TSelf>>(this, TryParse);

                if (parse is null && fparse is null && tparse is null && ftparse is null)
                {
                    yield return Exception<NumericParseHandler<String, TSelf>>(this, Parse);
                    yield break;
                }

                static Boolean TryParseFallback(String value, NumberStyles style, [MaybeNullWhen(false)] out TSelf result)
                {
                    return TryParse(value, style, null, out result);
                }

                static Boolean TryParseNumericFallback(String value, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out TSelf result)
                {
                    try
                    {
                        result = Parse(value, style, provider);
                        return true;
                    }
                    catch (Exception)
                    {
                        result = default;
                        return false;
                    }
                }

                Set(in set.Parse) = parse ?? (static (value, style) => Parse(value, style, null));
                Set(in set.NumericParse) = fparse ?? (static (value, style, provider) => TryParse(value, style, provider, out TSelf? result) ? result : throw Exception());
                Set(in set.TryParse) = tparse ?? TryParseFallback;
                Set(in set.NumericTryParse) = ftparse ?? TryParseNumericFallback;
            }
        }
    }
}