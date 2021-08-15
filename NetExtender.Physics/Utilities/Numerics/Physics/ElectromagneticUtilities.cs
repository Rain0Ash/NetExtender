// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Utilities.Numerics.Physics
{
    public enum ElectromagneticRadiation
    {
        Unknown,
        GammaRays,
        HardXRays,
        SoftXRays,
        ExtremeUltraviolet,
        NearUltraviolet,
        Visible,
        NearInfrared,
        MidInfrared,
        FarInfrared,
        ExtremelyHighFrequency,
        SuperHighFrequency,
        UltraHighFrequency,
        VeryHighFrequency,
        HighFrequency,
        MediumFrequency,
        LowFrequency,
        VeryLowFrequency,
        UltraLowFrequency,
        SuperLowFrequency,
        ExtremelyLowFrequency,
    }

    public enum ElectromagneticType
    {
        Unknown,
        Gamma,
        XRay,
        Ultraviolet,
        Visible,
        Infrared,
        Microwave,
        Radiowave
    }

    public static class ElectromagneticUtilities
    {
        public const Int32 MaxVisibleWaveLength = 780;
        public const Int32 MinVisibleWaveLength = 380;

        /// <summary>
        /// Return <see cref="ElectromagneticType"/> from it's wavelength
        /// </summary>
        /// <param name="wavelength">Wavelength in nm</param>
        /// <returns><see cref="ElectromagneticType"/></returns>
        public static ElectromagneticType GetElectromagneticTypeFromWaveLength(Double wavelength)
        {
            return wavelength switch
            {
                < Double.Epsilon => ElectromagneticType.Unknown,
                < 0.005 => ElectromagneticType.Gamma,
                < 10 => ElectromagneticType.XRay,
                < MinVisibleWaveLength => ElectromagneticType.Ultraviolet,
                <= MaxVisibleWaveLength => ElectromagneticType.Visible,
                < 1000000 => ElectromagneticType.Infrared,
                < 10000000000 => ElectromagneticType.Microwave,
                _ => ElectromagneticType.Radiowave
            };
        }
        
        /// <summary>
        /// Return <see cref="ElectromagneticType"/> from it's wavelength
        /// </summary>
        /// <param name="wavelength">Wavelength in nm</param>
        /// <returns><see cref="ElectromagneticType"/></returns>
        public static ElectromagneticType GetElectromagneticTypeFromWaveLength(Decimal wavelength)
        {
            return wavelength switch
            {
                <= 0 => ElectromagneticType.Unknown,
                < 0.005M => ElectromagneticType.Gamma,
                < 10 => ElectromagneticType.XRay,
                < MinVisibleWaveLength => ElectromagneticType.Ultraviolet,
                <= MaxVisibleWaveLength => ElectromagneticType.Visible,
                < 1000000 => ElectromagneticType.Infrared,
                < 10000000000 => ElectromagneticType.Microwave,
                _ => ElectromagneticType.Radiowave
            };
        }
        
        /// <summary>
        /// Return <see cref="ElectromagneticType"/> from it's frequency
        /// </summary>
        /// <param name="wavelength">Wavelength in thz</param>
        /// <returns><see cref="ElectromagneticType"/></returns>
        public static ElectromagneticType GetElectromagneticTypeFromFrequency(Double wavelength)
        {
            return wavelength switch
            {
                < Double.Epsilon => ElectromagneticType.Unknown,
                >= 60000000 => ElectromagneticType.Gamma,
                >= 30000 => ElectromagneticType.XRay,
                > 750 => ElectromagneticType.Ultraviolet,
                >= 429 => ElectromagneticType.Visible,
                >= 0.3 => ElectromagneticType.Infrared,
                >= 0.0003 => ElectromagneticType.Microwave,
                _ => ElectromagneticType.Radiowave
            };
        }
        
        /// <summary>
        /// Return <see cref="ElectromagneticType"/> from it's frequency
        /// </summary>
        /// <param name="wavelength">Wavelength in thz</param>
        /// <returns><see cref="ElectromagneticType"/></returns>
        public static ElectromagneticType GetElectromagneticTypeFromFrequency(Decimal wavelength)
        {
            return wavelength switch
            {
                <= 0 => ElectromagneticType.Unknown,
                >= 60000000 => ElectromagneticType.Gamma,
                >= 30000 => ElectromagneticType.XRay,
                > 750 => ElectromagneticType.Ultraviolet,
                >= 429 => ElectromagneticType.Visible,
                >= 0.3M => ElectromagneticType.Infrared,
                >= 0.0003M => ElectromagneticType.Microwave,
                _ => ElectromagneticType.Radiowave
            };
        }

        public static ElectromagneticType ToElectromagneticType(this ElectromagneticRadiation type)
        {
            return type switch
            {
                ElectromagneticRadiation.Unknown => ElectromagneticType.Unknown,
                ElectromagneticRadiation.GammaRays => ElectromagneticType.Gamma,
                ElectromagneticRadiation.HardXRays => ElectromagneticType.XRay,
                ElectromagneticRadiation.SoftXRays => ElectromagneticType.XRay,
                ElectromagneticRadiation.ExtremeUltraviolet => ElectromagneticType.Ultraviolet,
                ElectromagneticRadiation.NearUltraviolet => ElectromagneticType.Ultraviolet,
                ElectromagneticRadiation.Visible => ElectromagneticType.Visible,
                ElectromagneticRadiation.NearInfrared => ElectromagneticType.Infrared,
                ElectromagneticRadiation.MidInfrared => ElectromagneticType.Infrared,
                ElectromagneticRadiation.FarInfrared => ElectromagneticType.Infrared,
                ElectromagneticRadiation.ExtremelyHighFrequency => ElectromagneticType.Microwave,
                ElectromagneticRadiation.SuperHighFrequency => ElectromagneticType.Microwave,
                ElectromagneticRadiation.UltraHighFrequency => ElectromagneticType.Microwave,
                ElectromagneticRadiation.VeryHighFrequency => ElectromagneticType.Radiowave,
                ElectromagneticRadiation.HighFrequency => ElectromagneticType.Radiowave,
                ElectromagneticRadiation.MediumFrequency => ElectromagneticType.Radiowave,
                ElectromagneticRadiation.LowFrequency => ElectromagneticType.Radiowave,
                ElectromagneticRadiation.VeryLowFrequency => ElectromagneticType.Radiowave,
                ElectromagneticRadiation.UltraLowFrequency => ElectromagneticType.Radiowave,
                ElectromagneticRadiation.SuperLowFrequency => ElectromagneticType.Radiowave,
                ElectromagneticRadiation.ExtremelyLowFrequency => ElectromagneticType.Radiowave,
                _ => throw new NotSupportedException()
            };
        }
        
        public static Double Frequency(Double wavelength)
        {
            if (wavelength < Double.Epsilon)
            {
                throw new ArgumentOutOfRangeException(nameof(wavelength));
            }

            return PhysicsUtilities.C / wavelength;
        }
        
        public static Decimal Frequency(Decimal wavelength)
        {
            if (wavelength <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(wavelength));
            }

            return PhysicsUtilities.C / wavelength;
        }
        
        public static Double Wavelength(Double frequency)
        {
            if (frequency < Double.Epsilon)
            {
                throw new ArgumentOutOfRangeException(nameof(frequency));
            }

            return PhysicsUtilities.C / frequency;
        }
        
        public static Decimal Wavelength(Decimal frequency)
        {
            if (frequency <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(frequency));
            }

            return PhysicsUtilities.C / frequency;
        }

        public static Double DoplerWavelength(Double wavelength, Double velocity)
        {
            return DoplerWavelength(wavelength, velocity, 0);
        }
        
        public static Decimal DoplerWavelength(Decimal wavelength, Decimal velocity)
        {
            return DoplerWavelength(wavelength, velocity, 0);
        }

        public static Double DoplerWavelength(Double wavelength, Double velocity, Double angle)
        {
            if (wavelength < Double.Epsilon)
            {
                throw new ArgumentOutOfRangeException(nameof(wavelength));
            }

            return velocity switch
            {
                < Double.Epsilon => wavelength,
                >= PhysicsUtilities.C => throw new ArgumentOutOfRangeException(nameof(velocity)),
                _ => Wavelength(DoplerFrequency(Frequency(wavelength), velocity, angle))
            };
        }
        
        public static Decimal DoplerWavelength(Decimal wavelength, Decimal velocity, Decimal angle)
        {
            if (wavelength <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(wavelength));
            }

            return velocity switch
            {
                <= 0 => wavelength,
                >= PhysicsUtilities.C => throw new ArgumentOutOfRangeException(nameof(velocity)),
                _ => Wavelength(DoplerFrequency(Frequency(wavelength), velocity, angle))
            };
        }

        public static Double DoplerFrequency(Double frequency, Double velocity)
        {
            return DoplerFrequency(frequency, velocity, 0);
        }
        
        public static Decimal DoplerFrequency(Decimal frequency, Decimal velocity)
        {
            return DoplerFrequency(frequency, velocity, 0);
        }

        public static Double DoplerFrequency(Double frequency, Double velocity, Double angle)
        {
            if (frequency < Double.Epsilon)
            {
                throw new ArgumentOutOfRangeException(nameof(frequency));
            }

            return velocity switch
            {
                < Double.Epsilon => frequency,
                >= PhysicsUtilities.C => throw new ArgumentOutOfRangeException(nameof(velocity)),
                _ => frequency * Math.Sqrt(1 - velocity * velocity / PhysicsUtilities.SquareC) / (1 - velocity / PhysicsUtilities.C * Math.Cos(angle))
            };
        }
        
        public static Decimal DoplerFrequency(Decimal frequency, Decimal velocity, Decimal angle)
        {
            if (frequency <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(frequency));
            }

            return velocity switch
            {
                <= 0 => frequency,
                >= PhysicsUtilities.C => throw new ArgumentOutOfRangeException(nameof(velocity)),
                _ => frequency * (1 - velocity * velocity / PhysicsUtilities.SquareC).Sqrt() / (1 - velocity / PhysicsUtilities.C * MathUtilities.Cos(angle))
            };
        }
    }
}