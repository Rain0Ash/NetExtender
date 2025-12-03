// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;

namespace NetExtender.Utilities.Types
{
    public static class Time
    {
        public const Int32 MillisecondsInSecond = 1000;

        public const Int32 SecondsInMinute = 60;

        public const Int32 MinutesInHour = 60;

        public const Int32 HoursInDay = 24;

        public const Int32 DaysInWeek = 7;

        public const Int32 WeeksInMonth = 4;
        public const Int32 DaysInMonth = 30;

        public const Int32 MonthsInYear = 12;
        public const Int32 WeeksInYear = 52;
        public const Int32 DaysInYear = 365;

        public const Int32 YearsInCentury = 100;
        public const Int32 YearsInMillennium = 1000;

        public static readonly TimeSpan Zero = TimeSpan.Zero;

        public static class Year
        {
            public static TimeSpan One
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(DaysInYear);
                }
            }

            public static TimeSpan Two
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(DaysInYear * 2);
                }
            }

            public static TimeSpan Three
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(DaysInYear * 3);
                }
            }

            public static TimeSpan Four
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(DaysInYear * 4);
                }
            }

            public static TimeSpan Five
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(DaysInYear * 5);
                }
            }

            public static TimeSpan Six
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(DaysInYear * 6);
                }
            }

            public static TimeSpan Seven
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(DaysInYear * 7);
                }
            }

            public static TimeSpan Eight
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(DaysInYear * 8);
                }
            }

            public static TimeSpan Nine
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(DaysInYear * 9);
                }
            }

            public static TimeSpan Ten
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(DaysInYear * 10);
                }
            }

            public static TimeSpan Eleven
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(DaysInYear * 11);
                }
            }

            public static TimeSpan Twelve
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(DaysInYear * 12);
                }
            }

            public static TimeSpan Century
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(DaysInYear * YearsInCentury);
                }
            }

            public static TimeSpan Millennium
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(DaysInYear * YearsInMillennium);
                }
            }
        }

        public static class Month
        {
            public static TimeSpan One
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(DaysInMonth);
                }
            }

            public static TimeSpan Two
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(DaysInMonth * 2);
                }
            }

            public static TimeSpan Three
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(DaysInMonth * 3);
                }
            }

            public static TimeSpan Four
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(DaysInMonth * 4);
                }
            }

            public static TimeSpan Five
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(DaysInMonth * 5);
                }
            }

            public static TimeSpan Six
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(DaysInMonth * 6);
                }
            }

            public static TimeSpan Seven
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(DaysInMonth * 7);
                }
            }

            public static TimeSpan Eight
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(DaysInMonth * 8);
                }
            }

            public static TimeSpan Nine
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(DaysInMonth * 9);
                }
            }

            public static TimeSpan Ten
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(DaysInMonth * 10);
                }
            }

            public static TimeSpan Eleven
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(DaysInMonth * 11);
                }
            }

            public static TimeSpan Twelve
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(DaysInMonth * 12);
                }
            }
        }

        public static class Week
        {
            public static TimeSpan One
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(DaysInWeek);
                }
            }

            public static TimeSpan Two
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(DaysInWeek * 2);
                }
            }

            public static TimeSpan Three
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(DaysInWeek * 3);
                }
            }

            public static TimeSpan Fourth
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(DaysInWeek * 4);
                }
            }

            public static TimeSpan Ten
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(DaysInWeek * 10);
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TimeSpan Get(Double count)
            {
                return TimeSpan.FromDays(DaysInWeek * count);
            }
        }

        public static class Day
        {
            public static TimeSpan Hundred
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(100);
                }
            }

            public static TimeSpan Fifty
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(50);
                }
            }

            public static TimeSpan Thirty
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(30);
                }
            }

            public static TimeSpan TwentyFive
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(25);
                }
            }

            public static TimeSpan Twenty
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(20);
                }
            }

            public static TimeSpan Fifteen
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(15);
                }
            }

            public static TimeSpan Twelve
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(12);
                }
            }

            public static TimeSpan Eleven
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(11);
                }
            }

            public static TimeSpan Ten
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(10);
                }
            }

            public static TimeSpan Nine
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(9);
                }
            }

            public static TimeSpan Eight
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(8);
                }
            }

            public static TimeSpan SevenHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(7.5);
                }
            }

            public static TimeSpan Seven
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(7);
                }
            }

            public static TimeSpan Six
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(6);
                }
            }

            public static TimeSpan Five
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(5);
                }
            }

            public static TimeSpan Four
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(4);
                }
            }

            public static TimeSpan Three
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(3);
                }
            }

            public static TimeSpan TwoHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(2.5);
                }
            }

            public static TimeSpan Two
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(2);
                }
            }

            public static TimeSpan OneHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(1.5);
                }
            }

            public static TimeSpan One
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(1);
                }
            }

            public static TimeSpan ThreeQuarter
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(0.75);
                }
            }

            public static TimeSpan TwoThird
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(2.0 / 3.0);
                }
            }

            public static TimeSpan Half
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(0.5);
                }
            }

            public static TimeSpan OneThird
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(1.0 / 3.0);
                }
            }

            public static TimeSpan Quarter
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(0.25);
                }
            }

            public static TimeSpan OneTenth
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromDays(0.1);
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TimeSpan Get(Double count)
            {
                return TimeSpan.FromDays(count);
            }
        }

        public static class Hour
        {
            public static TimeSpan Hundred
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(100);
                }
            }

            public static TimeSpan TwentyFour
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(24);
                }
            }

            public static TimeSpan TwentyThreeHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(23.5);
                }
            }

            public static TimeSpan TwentyThree
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(23);
                }
            }

            public static TimeSpan TwentyTwoHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(22.5);
                }
            }

            public static TimeSpan TwentyTwo
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(22);
                }
            }

            public static TimeSpan TwentyOneHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(21.5);
                }
            }

            public static TimeSpan TwentyOne
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(21);
                }
            }

            public static TimeSpan TwentyHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(20.5);
                }
            }

            public static TimeSpan Twenty
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(20);
                }
            }

            public static TimeSpan NineteenHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(19.5);
                }
            }

            public static TimeSpan Nineteen
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(19);
                }
            }

            public static TimeSpan EighteenHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(18.5);
                }
            }

            public static TimeSpan Eighteen
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(18);
                }
            }

            public static TimeSpan SeventeenHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(17.5);
                }
            }

            public static TimeSpan Seventeen
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(17);
                }
            }

            public static TimeSpan SixteenHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(16.5);
                }
            }

            public static TimeSpan Sixteen
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(16);
                }
            }

            public static TimeSpan FifteenHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(15.5);
                }
            }

            public static TimeSpan Fifteen
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(15);
                }
            }

            public static TimeSpan FourteenHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(14.5);
                }
            }

            public static TimeSpan Fourteen
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(14);
                }
            }

            public static TimeSpan ThirteenHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(13.5);
                }
            }

            public static TimeSpan Thirteen
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(13);
                }
            }

            public static TimeSpan TwelveHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(12.5);
                }
            }

            public static TimeSpan Twelve
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(12);
                }
            }

            public static TimeSpan ElevenHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(11.5);
                }
            }

            public static TimeSpan Eleven
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(11);
                }
            }

            public static TimeSpan TenHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(10.5);
                }
            }

            public static TimeSpan Ten
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(10);
                }
            }

            public static TimeSpan NineHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(9.5);
                }
            }

            public static TimeSpan Nine
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(9);
                }
            }

            public static TimeSpan EightHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(8.5);
                }
            }

            public static TimeSpan Eight
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(8);
                }
            }

            public static TimeSpan SevenHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(7.5);
                }
            }

            public static TimeSpan Seven
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(7);
                }
            }

            public static TimeSpan SixHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(6.5);
                }
            }

            public static TimeSpan Six
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(6);
                }
            }

            public static TimeSpan FiveHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(5.5);
                }
            }

            public static TimeSpan Five
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(5);
                }
            }

            public static TimeSpan FourHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(4.5);
                }
            }

            public static TimeSpan Four
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(4);
                }
            }

            public static TimeSpan ThreeHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(3.5);
                }
            }

            public static TimeSpan Three
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(3);
                }
            }

            public static TimeSpan TwoHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(2.5);
                }
            }

            public static TimeSpan Two
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(2);
                }
            }

            public static TimeSpan OneHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(1.5);
                }
            }

            public static TimeSpan One
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(1);
                }
            }

            public static TimeSpan ThreeQuarter
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(0.75);
                }
            }

            public static TimeSpan TwoThird
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(2.0 / 3.0);
                }
            }

            public static TimeSpan Half
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(0.5);
                }
            }

            public static TimeSpan OneThird
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(1.0 / 3.0);
                }
            }

            public static TimeSpan Quarter
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(0.25);
                }
            }

            public static TimeSpan OneTenth
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromHours(0.1);
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TimeSpan Get(Double count)
            {
                return TimeSpan.FromHours(count);
            }
        }

        public static class Minute
        {
            public static TimeSpan Hundred
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMinutes(100);
                }
            }

            public static TimeSpan Ten
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMinutes(10);
                }
            }

            public static TimeSpan NineHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMinutes(9.5);
                }
            }

            public static TimeSpan Nine
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMinutes(9);
                }
            }

            public static TimeSpan Eight
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMinutes(8);
                }
            }

            public static TimeSpan SevenFive
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMinutes(7.5);
                }
            }

            public static TimeSpan SixHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMinutes(6.5);
                }
            }

            public static TimeSpan Six
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMinutes(6);
                }
            }

            public static TimeSpan FiveHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMinutes(5.5);
                }
            }

            public static TimeSpan Five
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMinutes(5);
                }
            }

            public static TimeSpan FourHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMinutes(4.5);
                }
            }

            public static TimeSpan Four
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMinutes(4);
                }
            }

            public static TimeSpan ThreeHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMinutes(3.5);
                }
            }

            public static TimeSpan Three
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMinutes(3);
                }
            }

            public static TimeSpan TwoHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMinutes(2.5);
                }
            }

            public static TimeSpan Two
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMinutes(2);
                }
            }

            public static TimeSpan OneHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMinutes(1.5);
                }
            }

            public static TimeSpan One
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMinutes(1);
                }
            }

            public static TimeSpan ThreeQuarter
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMinutes(0.75);
                }
            }

            public static TimeSpan TwoThird
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMinutes(2.0 / 3.0);
                }
            }

            public static TimeSpan Half
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMinutes(0.5);
                }
            }

            public static TimeSpan OneThird
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMinutes(1.0 / 3.0);
                }
            }

            public static TimeSpan Quarter
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMinutes(0.25);
                }
            }

            public static TimeSpan OneTenth
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMinutes(0.1);
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TimeSpan Get(Double count)
            {
                return TimeSpan.FromMinutes(count);
            }
        }

        public static class Second
        {
            public static TimeSpan Hundred
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromSeconds(100);
                }
            }

            public static TimeSpan Ten
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromSeconds(10);
                }
            }

            public static TimeSpan NineHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromSeconds(9.5);
                }
            }

            public static TimeSpan Nine
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromSeconds(9);
                }
            }

            public static TimeSpan EightHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromSeconds(8.5);
                }
            }

            public static TimeSpan Eight
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromSeconds(8);
                }
            }

            public static TimeSpan SevenHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromSeconds(7.5);
                }
            }

            public static TimeSpan Seven
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromSeconds(7);
                }
            }

            public static TimeSpan SixHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromSeconds(6.5);
                }
            }

            public static TimeSpan Six
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromSeconds(6);
                }
            }

            public static TimeSpan FiveHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromSeconds(5.5);
                }
            }

            public static TimeSpan Five
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromSeconds(5);
                }
            }

            public static TimeSpan FourHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromSeconds(4.5);
                }
            }

            public static TimeSpan Four
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromSeconds(4);
                }
            }

            public static TimeSpan ThreeHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromSeconds(3.5);
                }
            }

            public static TimeSpan Three
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromSeconds(3);
                }
            }

            public static TimeSpan TwoHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromSeconds(2.5);
                }
            }

            public static TimeSpan Two
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromSeconds(2);
                }
            }

            public static TimeSpan OneHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromSeconds(1.5);
                }
            }

            public static TimeSpan One
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromSeconds(1);
                }
            }

            public static TimeSpan ThreeQuarter
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromSeconds(0.75);
                }
            }

            public static TimeSpan TwoThird
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromSeconds(2.0 / 3.0);
                }
            }

            public static TimeSpan Half
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromSeconds(0.5);
                }
            }

            public static TimeSpan OneThird
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromSeconds(1.0 / 3.0);
                }
            }

            public static TimeSpan Quarter
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromSeconds(0.25);
                }
            }

            public static TimeSpan OneTenth
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromSeconds(0.1);
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TimeSpan Get(Double count)
            {
                return TimeSpan.FromSeconds(count);
            }
        }

        public static class Millisecond
        {
            public static TimeSpan OneSecond
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMilliseconds(1000);
                }
            }

            public static TimeSpan ThreeQuarterSecond
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMilliseconds(750);
                }
            }

            public static TimeSpan HalfSecond
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMilliseconds(500);
                }
            }

            public static TimeSpan QuarterSecond
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMilliseconds(250);
                }
            }

            public static TimeSpan Hundred
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMilliseconds(100);
                }
            }

            public static TimeSpan Fifty
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMilliseconds(50);
                }
            }

            public static TimeSpan Thirty
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMilliseconds(30);
                }
            }

            public static TimeSpan TwentyFive
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMilliseconds(25);
                }
            }

            public static TimeSpan Twenty
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMilliseconds(20);
                }
            }

            public static TimeSpan Ten
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMilliseconds(10);
                }
            }

            public static TimeSpan Nine
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMilliseconds(9);
                }
            }

            public static TimeSpan Eight
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMilliseconds(8);
                }
            }

            public static TimeSpan SevenHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMilliseconds(7.5);
                }
            }

            public static TimeSpan Seven
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMilliseconds(7);
                }
            }

            public static TimeSpan SixHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMilliseconds(6.5);
                }
            }

            public static TimeSpan Five
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMilliseconds(5);
                }
            }

            public static TimeSpan Four
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMilliseconds(4);
                }
            }

            public static TimeSpan Three
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMilliseconds(3);
                }
            }

            public static TimeSpan TwoHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMilliseconds(2.5);
                }
            }

            public static TimeSpan Two
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMilliseconds(2);
                }
            }

            public static TimeSpan One
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMilliseconds(1);
                }
            }

            public static TimeSpan ThreeQuarter
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMilliseconds(0.75);
                }
            }

            public static TimeSpan TwoThird
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMilliseconds(2.0 / 3.0);
                }
            }

            public static TimeSpan Half
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMilliseconds(0.5);
                }
            }

            public static TimeSpan OneThird
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMilliseconds(1.0 / 3.0);
                }
            }

            public static TimeSpan Quarter
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMilliseconds(0.25);
                }
            }

            public static TimeSpan OneTenth
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMilliseconds(0.1);
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TimeSpan Get(Double count)
            {
                return TimeSpan.FromMilliseconds(count);
            }
        }

#if NET7_0_OR_GREATER
        public static class Microsecond
        {
            public static TimeSpan OneMillisecond
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(1000);
                }
            }

            public static TimeSpan ThreeQuarterMillisecond
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(750);
                }
            }

            public static TimeSpan HalfMillisecond
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(500);
                }
            }

            public static TimeSpan QuarterMillisecond
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(250);
                }
            }

            public static TimeSpan Hundred
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(100);
                }
            }

            public static TimeSpan Fifty
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(50);
                }
            }

            public static TimeSpan Thirty
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(30);
                }
            }

            public static TimeSpan TwentyFive
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(25);
                }
            }

            public static TimeSpan Twenty
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(20);
                }
            }

            public static TimeSpan Ten
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(10);
                }
            }

            public static TimeSpan Nine
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(9);
                }
            }

            public static TimeSpan Eight
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(8);
                }
            }

            public static TimeSpan SevenHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(7.5);
                }
            }

            public static TimeSpan Seven
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(7);
                }
            }

            public static TimeSpan SixHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(6.5);
                }
            }

            public static TimeSpan Five
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(5);
                }
            }

            public static TimeSpan Four
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(4);
                }
            }

            public static TimeSpan Three
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(3);
                }
            }

            public static TimeSpan TwoHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(2.5);
                }
            }

            public static TimeSpan Two
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(2);
                }
            }

            public static TimeSpan One
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(1);
                }
            }

            public static TimeSpan ThreeQuarter
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(0.75);
                }
            }

            public static TimeSpan TwoThird
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(2.0 / 3.0);
                }
            }

            public static TimeSpan Half
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(0.5);
                }
            }

            public static TimeSpan OneThird
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(1.0 / 3.0);
                }
            }

            public static TimeSpan Quarter
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(0.25);
                }
            }

            public static TimeSpan OneTenth
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(0.1);
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TimeSpan Get(Double count)
            {
                return TimeSpan.FromMicroseconds(count);
            }
        }

        public static class Nanosecond
        {
            public static TimeSpan OneMicrosecond
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(1);
                }
            }

            public static TimeSpan ThreeQuarterMicrosecond
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(0.75);
                }
            }

            public static TimeSpan HalfMicrosecond
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(0.50);
                }
            }

            public static TimeSpan QuarterMicrosecond
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(0.25);
                }
            }

            public static TimeSpan Hundred
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(0.1);
                }
            }

            public static TimeSpan Fifty
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(0.05);
                }
            }

            public static TimeSpan Thirty
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(0.03);
                }
            }

            public static TimeSpan TwentyFive
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(0.025);
                }
            }

            public static TimeSpan Twenty
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(0.02);
                }
            }

            public static TimeSpan Ten
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(0.01);
                }
            }

            public static TimeSpan Nine
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(0.009);
                }
            }

            public static TimeSpan Eight
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(0.008);
                }
            }

            public static TimeSpan SevenHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(0.0075);
                }
            }

            public static TimeSpan Seven
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(0.007);
                }
            }

            public static TimeSpan SixHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(0.0065);
                }
            }

            public static TimeSpan Five
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(0.005);
                }
            }

            public static TimeSpan Four
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(0.004);
                }
            }

            public static TimeSpan Three
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(0.003);
                }
            }

            public static TimeSpan TwoHalf
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(0.0025);
                }
            }

            public static TimeSpan Two
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(0.002);
                }
            }

            public static TimeSpan One
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(0.001);
                }
            }

            public static TimeSpan ThreeQuarter
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(0.00075);
                }
            }

            public static TimeSpan TwoThird
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(0.002 / 3.0);
                }
            }

            public static TimeSpan Half
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(0.0005);
                }
            }

            public static TimeSpan OneThird
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(0.001 / 3.0);
                }
            }

            public static TimeSpan Quarter
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(0.00025);
                }
            }

            public static TimeSpan OneTenth
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TimeSpan.FromMicroseconds(0.0001);
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TimeSpan Get(Double count)
            {
                return TimeSpan.FromMicroseconds(count / 1000);
            }
        }
#endif
    }
}