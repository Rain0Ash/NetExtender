using System;
using System.Numerics;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Types.Numerics
{
    public readonly struct BigComplex : IEquatable<Complex>, IEquatable<BigComplex>, IFormattable
    {
        public static implicit operator BigComplex(SByte value)
        {
            return new BigComplex(value, Decimal.Zero);
        }

        public static implicit operator BigComplex(Byte value)
        {
            return new BigComplex(value, Decimal.Zero);
        }
        
        public static implicit operator BigComplex(Int16 value)
        {
            return new BigComplex(value, Decimal.Zero);
        }

        public static implicit operator BigComplex(UInt16 value)
        {
            return new BigComplex(value, Decimal.Zero);
        }

        public static implicit operator BigComplex(Int32 value)
        {
            return new BigComplex(value, Decimal.Zero);
        }

        public static implicit operator BigComplex(UInt32 value)
        {
            return new BigComplex(value, Decimal.Zero);
        }

        public static implicit operator BigComplex(Int64 value)
        {
            return new BigComplex(value, Decimal.Zero);
        }

        public static implicit operator BigComplex(UInt64 value)
        {
            return new BigComplex(value, Decimal.Zero);
        }

        public static explicit operator BigComplex(Single value)
        {
            return new BigComplex((Decimal) value, Decimal.Zero);
        }

        public static explicit operator BigComplex(Double value)
        {
            return new BigComplex((Decimal) value, Decimal.Zero);
        }

        public static implicit operator BigComplex(Decimal value)
        {
            return new BigComplex(value, Decimal.Zero);
        }

        public static explicit operator BigComplex(BigInteger value)
        {
            return new BigComplex((Decimal) value, Decimal.Zero);
        }

        public static explicit operator Complex(BigComplex value)
        {
            return new Complex((Double) value.Real, (Double) value.Imaginary);
        }

        public static Boolean operator ==(BigComplex first, BigComplex second)
        {
            return first._real == second._real && first._imaginary == second._imaginary;
        }

        public static Boolean operator !=(BigComplex first, BigComplex second)
        {
            return first._real != second._real || first._imaginary != second._imaginary;
        }

        public static BigComplex operator -(BigComplex value)
        {
            return new BigComplex(-value._real, -value._imaginary);
        }

        public static BigComplex operator +(BigComplex first, BigComplex second)
        {
            return new BigComplex(first._real + second._real, first._imaginary + second._imaginary);
        }

        public static BigComplex operator +(BigComplex first, Decimal second)
        {
            return new BigComplex(first._real + second, first._imaginary);
        }

        public static BigComplex operator +(Decimal first, BigComplex second)
        {
            return new BigComplex(first + second._real, second._imaginary);
        }

        public static BigComplex operator -(BigComplex first, BigComplex second)
        {
            return new BigComplex(first._real - second._real, first._imaginary - second._imaginary);
        }

        public static BigComplex operator -(BigComplex first, Decimal second)
        {
            return new BigComplex(first._real - second, first._imaginary);
        }

        public static BigComplex operator -(Decimal first, BigComplex second)
        {
            return new BigComplex(first - second._real, -second._imaginary);
        }

        public static BigComplex operator *(BigComplex first, BigComplex second)
        {
            return new BigComplex(first._real * second._real - first._imaginary * second._imaginary, first._imaginary * second._real + first._real * second._imaginary);
        }

        public static BigComplex operator *(BigComplex first, Decimal second)
        {
            return new BigComplex(first._real * second, first._imaginary * second);
        }

        public static BigComplex operator *(Decimal first, BigComplex second)
        {
            return new BigComplex(first * second._real, first * second._imaginary);
        }

        public static BigComplex operator /(BigComplex first, BigComplex second)
        {
            Decimal division;
            if (Math.Abs(second._imaginary) < Math.Abs(second._real))
            {
                division = second._imaginary / second._real;
                return new BigComplex((first._real + first._imaginary * division) / (second._real + second._imaginary * division), (first._imaginary - first._real * division) / (second._real + second._imaginary * division));
            }

            division = second._real / second._imaginary;
            return new BigComplex((first._imaginary + first._real * division) / (second._imaginary + second._real * division), (-first._real + first._imaginary * division) / (second._imaginary + second._real * division));
        }

        public static BigComplex operator /(BigComplex first, Decimal second)
        {
            if (second == Decimal.Zero)
            {
                throw new DivideByZeroException();
            }

            return new BigComplex(first._real / second, first._imaginary / second);
        }

        public static BigComplex operator /(Decimal first, BigComplex second)
        {
            Decimal division;
            if (Math.Abs(second._imaginary) < Math.Abs(second._real))
            {
                division = second._imaginary / second._real;
                return new BigComplex(first / (second._real + second._imaginary * division), -first * division / (second._real + second._imaginary * division));
            }

            division = second._real / second._imaginary;
            return new BigComplex(first * division / (second._imaginary + second._real * division), -first / (second._imaginary + second._real * division));
        }
        
        public static readonly BigComplex Zero = new BigComplex(Decimal.Zero, Decimal.Zero);
        public static readonly BigComplex One = new BigComplex(Decimal.One, Decimal.Zero);
        public static readonly BigComplex ImaginaryOne = new BigComplex(Decimal.Zero, Decimal.One);

        private readonly Decimal _real;
        public Decimal Real
        {
            get
            {
                return _real;
            }
        }

        private readonly Decimal _imaginary;
        public Decimal Imaginary
        {
            get
            {
                return _imaginary;
            }
        }

        public Decimal Magnitude
        {
            get
            {
                return Abs(this);
            }
        }

        public Decimal Phase
        {
            get
            {
                return _imaginary.Atan2(_real);
            }
        }

        public BigComplex(Decimal real)
            : this(real, Decimal.Zero)
        {
        }

        public BigComplex(Decimal real, Decimal imaginary)
        {
            _real = real;
            _imaginary = imaginary;
        }

        public static BigComplex FromPolarCoordinates(Decimal magnitude, Decimal phase)
        {
            return new BigComplex(magnitude * phase.Cos(), magnitude * phase.Sin());
        }

        public static Decimal Abs(BigComplex value)
        {
            return Hypot(value._real, value._imaginary);
        }

        public static BigComplex Negate(BigComplex value)
        {
            return -value;
        }

        public static BigComplex Add(BigComplex first, BigComplex second)
        {
            return first + second;
        }

        public static BigComplex Add(BigComplex first, Decimal second)
        {
            return first + second;
        }

        public static BigComplex Add(Decimal first, BigComplex second)
        {
            return first + second;
        }

        public static BigComplex Subtract(BigComplex first, BigComplex second)
        {
            return first - second;
        }

        public static BigComplex Subtract(BigComplex first, Decimal second)
        {
            return first - second;
        }

        public static BigComplex Subtract(Decimal first, BigComplex second)
        {
            return first - second;
        }

        public static BigComplex Multiply(BigComplex first, BigComplex second)
        {
            return first * second;
        }

        public static BigComplex Multiply(BigComplex first, Decimal second)
        {
            return first * second;
        }

        public static BigComplex Multiply(Decimal first, BigComplex second)
        {
            return first * second;
        }

        public static BigComplex Divide(BigComplex dividend, BigComplex divisor)
        {
            return dividend / divisor;
        }

        public static BigComplex Divide(BigComplex dividend, Decimal divisor)
        {
            return dividend / divisor;
        }

        public static BigComplex Divide(Decimal dividend, BigComplex divisor)
        {
            return dividend / divisor;
        }

        public static BigComplex Conjugate(BigComplex value)
        {
            return new BigComplex(value._real, -value._imaginary);
        }

        public static BigComplex Reciprocal(BigComplex value)
        {
            return value is not { _real: Decimal.Zero, _imaginary: Decimal.Zero } ? One / value : Zero;
        }

        private static BigComplex Scale(BigComplex value, Decimal factor)
        {
            return new BigComplex(factor * value._real, factor * value._imaginary);
        }

        public static BigComplex Pow(BigComplex value, Decimal power)
        {
            return Pow(value, new BigComplex(power, Decimal.Zero));
        }

        public static BigComplex Exp(BigComplex value)
        {
            Decimal exp = value._real.Exp();
            return new BigComplex(exp * value._imaginary.Cos(), exp * value._imaginary.Sin());
        }

        public static BigComplex Pow(BigComplex value, BigComplex power)
        {
            if (power == Zero)
            {
                return One;
            }

            if (value == Zero)
            {
                return Zero;
            }

            Decimal abs = Abs(value);
            Decimal atan2 = value._imaginary.Atan2(value._real);
            Decimal eval = power._real * atan2 + power._imaginary * abs.Log();
            Decimal pow = abs.Pow(power._real) * MathUtilities.Constants.Decimal.E.Pow(-power._imaginary * atan2);
            return new BigComplex(pow * eval.Cos(), pow * eval.Sin());
        }

        public static BigComplex Sqrt(BigComplex value)
        {
            if (value._imaginary == Decimal.Zero)
            {
                return value._real < Decimal.Zero ? new BigComplex(Decimal.Zero, (-value._real).Sqrt()) : new BigComplex(value._real.Sqrt(), Decimal.Zero);
            }

            Boolean flag = false;
            Decimal real1 = value._real;
            Decimal imaginary1 = value._imaginary;
            if (Math.Abs(real1) >= MathUtilities.Constants.Decimal.SqrtRescaleThreshold || Math.Abs(imaginary1) >= MathUtilities.Constants.Decimal.SqrtRescaleThreshold)
            {
                real1 *= 0.25M;
                imaginary1 *= 0.25M;
                flag = true;
            }

            Decimal real2;
            Decimal imaginary2;
            if (real1 >= Decimal.Zero)
            {
                real2 = ((Hypot(real1, imaginary1) + real1) * 0.5M).Sqrt();
                imaginary2 = imaginary1 / (2M * real2);
            }
            else
            {
                imaginary2 = ((Hypot(real1, imaginary1) - real1) * 0.5M).Sqrt();
                if (imaginary1 < Decimal.Zero)
                {
                    imaginary2 = -imaginary2;
                }

                real2 = imaginary1 / (2M * imaginary2);
            }

            if (!flag)
            {
                return new BigComplex(real2, imaginary2);
            }

            real2 *= 2M;
            imaginary2 *= 2M;

            return new BigComplex(real2, imaginary2);
        }

        public static BigComplex Log(BigComplex value)
        {
            return new BigComplex(Abs(value).Log(), value._imaginary.Atan2(value._real));
        }

        public static BigComplex Log(BigComplex value, Decimal @base)
        {
            return Log(value) / Log(@base);
        }

        public static BigComplex Log10(BigComplex value)
        {
            return Scale(Log(value), MathUtilities.Constants.Decimal.ILog10);
        }

        private static Decimal Log1P(Decimal value)
        {
            Decimal d = Decimal.One + value;
            if (d == Decimal.One)
            {
                return value;
            }

            return value < 0.75M ? value * d.Log() / (d - Decimal.One) : d.Log();
        }

        public static BigComplex Sin(BigComplex value)
        {
            Decimal exp = value._imaginary.Exp();
            Decimal invert = Decimal.One / exp;
            return new BigComplex(value._real.Sin() * (exp + invert) * 0.5M, value._real.Cos() * (exp - invert) * 0.5M);
        }

        public static BigComplex Sinh(BigComplex value)
        {
            BigComplex complex = Sin(new BigComplex(-value._imaginary, value._real));
            return new BigComplex(complex._imaginary, -complex._real);
        }

        public static BigComplex Asin(BigComplex value)
        {
            AsinCore(Math.Abs(value.Real), Math.Abs(value.Imaginary), out Decimal b, out Decimal bprime, out Decimal v);
            
            Decimal real = bprime >= Decimal.Zero ? bprime.Atan() : b.Asin();
            if (value.Real < Decimal.Zero)
            {
                real = -real;
            }

            if (value.Imaginary < Decimal.Zero)
            {
                v = -v;
            }

            return new BigComplex(real, v);
        }

        public static BigComplex Cos(BigComplex value)
        {
            Decimal exp = value._imaginary.Exp();
            Decimal invert = Decimal.One / exp;
            return new BigComplex(value._real.Cos() * (exp + invert) * 0.5M, -value._real.Sin() * (exp - invert) * 0.5M);
        }

        public static BigComplex Cosh(BigComplex value)
        {
            return Cos(new BigComplex(-value._imaginary, value._real));
        }

        public static BigComplex Acos(BigComplex value)
        {
            AsinCore(Math.Abs(value.Real), Math.Abs(value.Imaginary), out Decimal b, out Decimal bprime, out Decimal v);
            
            Decimal real = bprime >= Decimal.Zero ? (Decimal.One / bprime).Atan() : b.Acos();
            if (value.Real < Decimal.Zero)
            {
                real = MathUtilities.Constants.Decimal.PI - real;
            }

            if (value.Imaginary > Decimal.Zero)
            {
                v = -v;
            }

            return new BigComplex(real, v);
        }

        public static BigComplex Tan(BigComplex value)
        {
            Decimal realx2 = value._real * 2M;
            Decimal imaginaryx2 = value._imaginary * 2M;
            Decimal exp = imaginaryx2.Exp();
            Decimal iexp = Decimal.One / exp;
            Decimal hsexp = (exp + iexp) * 0.5M;

            if (value._imaginary.Abs() <= 4M)
            {
                Decimal sum = realx2.Cos() + hsexp;
                return new BigComplex(realx2.Sin() / sum, (exp - iexp) * 0.5M / sum);
            }

            Decimal eval = Decimal.One + realx2.Cos() / hsexp;
            return new BigComplex(realx2.Sin() / hsexp / eval, imaginaryx2.Tanh() / eval);
        }

        public static BigComplex Tanh(BigComplex value)
        {
            BigComplex complex = Tan(new BigComplex(-value._imaginary, value._real));
            return new BigComplex(complex._imaginary, -complex._real);
        }

        public static BigComplex Atan(BigComplex value)
        {
            BigComplex complex = new BigComplex(2M, Decimal.Zero);
            return ImaginaryOne / complex * (Log(One - ImaginaryOne * value) - Log(One + ImaginaryOne * value));
        }

        // ReSharper disable once CognitiveComplexity
        private static void AsinCore(Decimal x, Decimal y, out Decimal b, out Decimal bprime, out Decimal v)
        {
            if (x > MathUtilities.Constants.Decimal.AsinOverflowThreshold || y > MathUtilities.Constants.Decimal.AsinOverflowThreshold)
            {
                b = -1M;
                bprime = x / y;
                Decimal num1;
                Decimal d;
                if (x < y)
                {
                    num1 = x;
                    d = y;
                }
                else
                {
                    num1 = y;
                    d = x;
                }

                Decimal num2 = num1 / d;
                v = MathUtilities.Constants.Decimal.Log2 + d.Log() + 0.5M * Log1P(num2 * num2);
            }
            else
            {
                Decimal num3 = Hypot(x + Decimal.One, y);
                Decimal num4 = Hypot(x - Decimal.One, y);
                Decimal num5 = (num3 + num4) * 0.5M;
                b = x / num5;
                if (b > 0.75M)
                {
                    if (x <= Decimal.One)
                    {
                        Decimal num6 = (y * y / (num3 + (x + Decimal.One)) + (num4 + (Decimal.One - x))) * 0.5M;
                        bprime = x / ((num5 + x) * num6).Sqrt();
                    }
                    else
                    {
                        Decimal num7 = (Decimal.One / (num3 + (x + Decimal.One)) + Decimal.One / (num4 + (x - Decimal.One))) * 0.5M;
                        bprime = x / y / ((num5 + x) * num7).Sqrt();
                    }
                }
                else
                {
                    bprime = -Decimal.One;
                }

                if (num5 < 1.5M)
                {
                    if (x < Decimal.One)
                    {
                        Decimal num8 = (Decimal.One / (num3 + (x + Decimal.One)) + Decimal.One / (num4 + (Decimal.One - x))) * 0.5M;
                        Decimal num9 = y * y * num8;
                        v = Log1P(num9 + y * (num8 * (num5 + Decimal.One)).Sqrt());
                    }
                    else
                    {
                        Decimal num10 = (y * y / (num3 + (x + Decimal.One)) + (num4 + (x - Decimal.One))) * 0.5M;
                        v = Log1P(num10 + (num10 * (num5 + Decimal.One)).Sqrt());
                    }
                }
                else
                {
                    v = (num5 + ((num5 - Decimal.One) * (num5 + Decimal.One)).Sqrt()).Log();
                }
            }
        }

        private static Decimal Hypot(Decimal a, Decimal b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);
            Decimal d1;
            Decimal d2;
            if (a < b)
            {
                d1 = a;
                d2 = b;
            }
            else
            {
                d1 = b;
                d2 = a;
            }

            if (d1 == Decimal.Zero)
            {
                return d2;
            }

            Decimal num = d1 / d2;
            return d2 * (Decimal.One + num * num).Sqrt();
        }

        public override Int32 GetHashCode()
        {
            const Int32 num = 99999997;
            return _real.GetHashCode() % num ^ _imaginary.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return other is BigComplex complex && Equals(complex);
        }

        public Boolean Equals(Complex other)
        {
            throw new NotImplementedException();
        }

        public Boolean Equals(BigComplex value)
        {
            return _real.Equals(value._real) && _imaginary.Equals(value._imaginary);
        }

        public override String ToString()
        {
            return _imaginary switch
            {
                < 0 => $"{_real} - {Math.Abs(_imaginary)}i",
                0 => _real.ToString(),
                _ => $"{_real} + {_imaginary}i",
            };
        }

        public String ToString(String? format)
        {
            return ToString(format, null);
        }

        public String ToString(IFormatProvider? provider)
        {
            return ToString(null, provider);
        }

        public String ToString(String? format, IFormatProvider? provider)
        {
            return _imaginary switch
            {
                < 0 => $"{_real.ToString(format, provider)} - {Math.Abs(_imaginary).ToString(format, provider)}i",
                0 => _real.ToString(format, provider),
                _ => $"{_real.ToString(format, provider)} + {_imaginary.ToString(format, provider)}i",
            };
        }
    }
}