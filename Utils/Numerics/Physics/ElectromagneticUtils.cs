// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Utils.Numerics.Physics
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
        NearInfared,
        MidInfared,
        FarInfared,
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
        Infared,
        Microwave,
        Radiowave
    }

    public static class ElectromagneticUtils
    {
        /// <summary>
        /// Return <see cref="ElectromagneticType"/> from it's wavelength
        /// </summary>
        /// <param name="wavelength">Wavelength in nm</param>
        /// <returns><see cref="ElectromagneticType"/></returns>
        public static ElectromagneticType GetElectromagneticTypeFromWaveLength(Double wavelength)
        {
            return wavelength switch
            {
                <= 0 => ElectromagneticType.Unknown,
                < 0.005 => ElectromagneticType.Gamma,
                < 10 => ElectromagneticType.XRay,
                < 380 => ElectromagneticType.Ultraviolet,
                <= 780 => ElectromagneticType.Visible,
                < 1000000 => ElectromagneticType.Infared,
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
                <= 0 => ElectromagneticType.Unknown,
                >= 60000000 => ElectromagneticType.Gamma,
                >= 30000 => ElectromagneticType.XRay,
                > 750 => ElectromagneticType.Ultraviolet,
                >= 429 => ElectromagneticType.Visible,
                >= 0.3 => ElectromagneticType.Infared,
                >= 0.0003 => ElectromagneticType.Microwave,
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
                ElectromagneticRadiation.NearInfared => ElectromagneticType.Infared,
                ElectromagneticRadiation.MidInfared => ElectromagneticType.Infared,
                ElectromagneticRadiation.FarInfared => ElectromagneticType.Infared,
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
            if (wavelength < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(wavelength));
            }

            return PhysicsUtils.C / wavelength;
        }
        
        public static Double Wavelength(Double frequency)
        {
            if (frequency < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(frequency));
            }

            return PhysicsUtils.C / frequency;
        }
    }
}