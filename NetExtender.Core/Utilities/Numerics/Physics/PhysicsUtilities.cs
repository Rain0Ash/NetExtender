// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Utilities.Numerics.Physics
{
    public static class PhysicsUtilities
    {
        public static class Constants
        {
            public static class Int32
            {
                public const System.Int32 C = 299792458;
            }
            
            public static class Int64
            {
                public const System.Int64 C = Int32.C;
                public const System.Int64 SquareC = C * C;
            }
            
            public static class Single
            {
                public const System.Single PI = MathUtilities.Constants.Single.PI;
                public const System.Single E = MathUtilities.Constants.Single.E;
                public const System.Single C = Int32.C;
                public const System.Single SquareC = C * C;
                
                public const System.Single GravitationalConstant = 6.6740831e-11F;
                public const System.Single UniversalGasConstant = 8.3144598F;
                public const System.Single BoltzmannConstant = 1.3806485279e-23F;
                public const System.Single AbsoluteZeroCelsiusTemperature = -273.15F;
                public const System.Single CelsiusZeroToKelvinTemperature = 273.15F;
                public const System.Single PlanckConstant = 6.62607004081e-34F;
                public const System.Single ReducedPlanckConstant = PlanckConstant / (2 * PI);
                public const System.Single DiracConstant = PlanckConstant / (2 * PI);
            }
            
            public static class Double
            {
                public const System.Double PI = MathUtilities.Constants.Double.PI;
                public const System.Double E = MathUtilities.Constants.Double.E;
                public const System.Double C = Int64.C;
                public const System.Double SquareC = C * C;
                
                public const System.Double GravitationalConstant = 6.6740831e-11;
                public const System.Double UniversalGasConstant = 8.3144598;
                public const System.Double BoltzmannConstant = 1.3806485279e-23;
                public const System.Double AbsoluteZeroCelsiusTemperature = -273.15;
                public const System.Double CelsiusZeroToKelvinTemperature = 273.15;
                public const System.Double PlanckConstant = 6.62607004081e-34;
                public const System.Double ReducedPlanckConstant = PlanckConstant / (2 * PI);
                private const System.Double SquareReducedPlanckConstant = ReducedPlanckConstant * ReducedPlanckConstant;
                private const System.Double CubeReducedPlanckConstant = SquareReducedPlanckConstant * ReducedPlanckConstant;
                public const System.Double DiracConstant = PlanckConstant / (2 * PI);
                
                private const System.Double SquareBoltzmannConstant = BoltzmannConstant * BoltzmannConstant;
                private const System.Double DoubleSquareBoltzmannConstant = SquareBoltzmannConstant * SquareBoltzmannConstant;
                
                public const System.Double StefanBoltzmannConstant = PI * PI * (SquareBoltzmannConstant * SquareBoltzmannConstant) / (60 * CubeReducedPlanckConstant * C * C);
            }
            
            public static class Decimal
            {
                public const System.Decimal PI = MathUtilities.Constants.Decimal.PI;
                public const System.Decimal E = MathUtilities.Constants.Decimal.E;
                public const System.Decimal C = Int64.C;
                public const System.Decimal SquareC = C * C;
                
                public const System.Decimal GravitationalConstant = 6.6740831e-11M;
                public const System.Decimal UniversalGasConstant = 8.3144598M;
                public const System.Decimal BoltzmannConstant = 1.3806485279e-23M;
                public const System.Decimal AbsoluteZeroCelsiusTemperature = -273.15M;
                public const System.Decimal CelsiusZeroToKelvinTemperature = 273.15M;
            }
        }
    }
}