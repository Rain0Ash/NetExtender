// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Utils.Numerics.Physics
{
    public static class PhysicsUtils
    {
        public const Double PI = Math.PI;
        public const Double E = Math.E;
        public const Int32 C = 299792458;
        public const Int64 LongC = C;
        public const Int64 SquareC = LongC * LongC;
        public const Double GravitationalConstant = 6.6740831e-11;
        public const Double UniversalGasConstant = 8.3144598;
        public const Double BoltzmannConstant = 1.3806485279e-23;
        public const Double AbsoluteZeroCelsiusTemperature = -273.15;
        public const Double CelsiusZeroToKelvinTemperature = 273.15;
        public const Double PlanckConstant = 6.62607004081e-34;
        public const Double ReducedPlanckConstant = PlanckConstant / (2 * PI);
        public const Double DiracConstant = PlanckConstant / (2 * PI);
        public const Double StefanBoltzmannConstant = PI * PI *
                                                      (BoltzmannConstant * BoltzmannConstant * BoltzmannConstant * BoltzmannConstant) /
                                                      (60 * ReducedPlanckConstant * ReducedPlanckConstant * ReducedPlanckConstant * C * C);
    }
}