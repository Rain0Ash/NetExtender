// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

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
                get
                {
                    return TimeSpan.FromDays(DaysInYear);
                }
            }

            public static TimeSpan Two
            {
                get
                {
                    return TimeSpan.FromDays(DaysInYear * 2);
                }
            }

            public static TimeSpan Three
            {
                get
                {
                    return TimeSpan.FromDays(DaysInYear * 3);
                }
            }
            
            public static TimeSpan Four
            {
                get
                {
                    return TimeSpan.FromDays(DaysInYear * 4);
                }
            }

            public static TimeSpan Five
            {
                get
                {
                    return TimeSpan.FromDays(DaysInYear * 5);
                }
            }
            
            public static TimeSpan Six
            {
                get
                {
                    return TimeSpan.FromDays(DaysInYear * 6);
                }
            }
            
            public static TimeSpan Seven
            {
                get
                {
                    return TimeSpan.FromDays(DaysInYear * 7);
                }
            }
            
            public static TimeSpan Eight
            {
                get
                {
                    return TimeSpan.FromDays(DaysInYear * 8);
                }
            }
            
            public static TimeSpan Nine
            {
                get
                {
                    return TimeSpan.FromDays(DaysInYear * 9);
                }
            }

            public static TimeSpan Ten
            {
                get
                {
                    return TimeSpan.FromDays(DaysInYear * 10);
                }
            }
            
            public static TimeSpan Eleven
            {
                get
                {
                    return TimeSpan.FromDays(DaysInYear * 11);
                }
            }
            
            public static TimeSpan Twelve
            {
                get
                {
                    return TimeSpan.FromDays(DaysInYear * 12);
                }
            }

            public static TimeSpan Century
            {
                get
                {
                    return TimeSpan.FromDays(DaysInYear * YearsInCentury);
                }
            }

            public static TimeSpan Millennium
            {
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
                get
                {
                    return TimeSpan.FromDays(DaysInMonth);
                }
            }

            public static TimeSpan Two
            {
                get
                {
                    return TimeSpan.FromDays(DaysInMonth * 2);
                }
            }

            public static TimeSpan Three
            {
                get
                {
                    return TimeSpan.FromDays(DaysInMonth * 3);
                }
            }

            public static TimeSpan Four
            {
                get
                {
                    return TimeSpan.FromDays(DaysInMonth * 4);
                }
            }

            public static TimeSpan Five
            {
                get
                {
                    return TimeSpan.FromDays(DaysInMonth * 5);
                }
            }

            public static TimeSpan Six
            {
                get
                {
                    return TimeSpan.FromDays(DaysInMonth * 6);
                }
            }

            public static TimeSpan Seven
            {
                get
                {
                    return TimeSpan.FromDays(DaysInMonth * 7);
                }
            }

            public static TimeSpan Eight
            {
                get
                {
                    return TimeSpan.FromDays(DaysInMonth * 8);
                }
            }

            public static TimeSpan Nine
            {
                get
                {
                    return TimeSpan.FromDays(DaysInMonth * 9);
                }
            }

            public static TimeSpan Ten
            {
                get
                {
                    return TimeSpan.FromDays(DaysInMonth * 10);
                }
            }

            public static TimeSpan Eleven
            {
                get
                {
                    return TimeSpan.FromDays(DaysInMonth * 11);
                }
            }

            public static TimeSpan Twelve
            {
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
                get
                {
                    return TimeSpan.FromDays(DaysInWeek);
                }
            }

            public static TimeSpan Two
            {
                get
                {
                    return TimeSpan.FromDays(DaysInWeek * 2);
                }
            }

            public static TimeSpan Three
            {
                get
                {
                    return TimeSpan.FromDays(DaysInWeek * 3);
                }
            }

            public static TimeSpan Fourth
            {
                get
                {
                    return TimeSpan.FromDays(DaysInWeek * 4);
                }
            }

            public static TimeSpan Ten
            {
                get
                {
                    return TimeSpan.FromDays(DaysInWeek * 10);
                }
            }

            public static TimeSpan Get(Double count)
            {
                return TimeSpan.FromDays(DaysInWeek * count);
            }
        }

        public static class Day
        {
            public static TimeSpan Hundred
            {
                get
                {
                    return TimeSpan.FromDays(100);
                }
            }
            
            public static TimeSpan Fifty
            {
                get
                {
                    return TimeSpan.FromDays(50);
                }
            }
            
            public static TimeSpan Thirty
            {
                get
                {
                    return TimeSpan.FromDays(30);
                }
            }
            
            public static TimeSpan TwentyFive
            {
                get
                {
                    return TimeSpan.FromDays(25);
                }
            }
            
            public static TimeSpan Twenty
            {
                get
                {
                    return TimeSpan.FromDays(20);
                }
            }
            
            public static TimeSpan Fifteen
            {
                get
                {
                    return TimeSpan.FromDays(15);
                }
            }
            
            public static TimeSpan Twelve
            {
                get
                {
                    return TimeSpan.FromDays(12);
                }
            }
            
            public static TimeSpan Eleven
            {
                get
                {
                    return TimeSpan.FromDays(11);
                }
            }
            
            public static TimeSpan Ten
            {
                get
                {
                    return TimeSpan.FromDays(10);
                }
            }
            
            public static TimeSpan Nine
            {
                get
                {
                    return TimeSpan.FromDays(9);
                }
            }
            
            public static TimeSpan Eight
            {
                get
                {
                    return TimeSpan.FromDays(8);
                }
            }

            public static TimeSpan SevenHalf
            {
                get
                {
                    return TimeSpan.FromDays(7.5);
                }
            }

            public static TimeSpan Seven
            {
                get
                {
                    return TimeSpan.FromDays(7);
                }
            }

            public static TimeSpan Six
            {
                get
                {
                    return TimeSpan.FromDays(6);
                }
            }

            public static TimeSpan Five
            {
                get
                {
                    return TimeSpan.FromDays(5);
                }
            }
            
            public static TimeSpan Four
            {
                get
                {
                    return TimeSpan.FromDays(4);
                }
            }

            public static TimeSpan Three
            {
                get
                {
                    return TimeSpan.FromDays(3);
                }
            }
            
            public static TimeSpan TwoHalf
            {
                get
                {
                    return TimeSpan.FromDays(2.5);
                }
            }

            public static TimeSpan Two
            {
                get
                {
                    return TimeSpan.FromDays(2);
                }
            }

            public static TimeSpan OneHalf
            {
                get
                {
                    return TimeSpan.FromDays(1.5);
                }
            }

            public static TimeSpan One
            {
                get
                {
                    return TimeSpan.FromDays(1);
                }
            }

            public static TimeSpan ThreeQuarter
            {
                get
                {
                    return TimeSpan.FromDays(0.75);
                }
            }
            
            public static TimeSpan TwoThird
            {
                get
                {
                    return TimeSpan.FromDays(2.0 / 3.0);
                }
            }

            public static TimeSpan Half
            {
                get
                {
                    return TimeSpan.FromDays(0.5);
                }
            }
            
            public static TimeSpan OneThird
            {
                get
                {
                    return TimeSpan.FromDays(1.0 / 3.0);
                }
            }

            public static TimeSpan Quarter
            {
                get
                {
                    return TimeSpan.FromDays(0.25);
                }
            }
            
            public static TimeSpan OneTenth
            {
                get
                {
                    return TimeSpan.FromDays(0.1);
                }
            }

            public static TimeSpan Get(Double count)
            {
                return TimeSpan.FromDays(count);
            }
        }

        public static class Hour
        {
            public static TimeSpan Hundred
            {
                get
                {
                    return TimeSpan.FromHours(100);
                }
            }
            
            public static TimeSpan TwentyFour
            {
                get
                {
                    return TimeSpan.FromHours(24);
                }
            }
            
            public static TimeSpan TwentyThreeHalf
            {
                get
                {
                    return TimeSpan.FromHours(23.5);
                }
            }
            
            public static TimeSpan TwentyThree
            {
                get
                {
                    return TimeSpan.FromHours(23);
                }
            }
            
            public static TimeSpan TwentyTwoHalf
            {
                get
                {
                    return TimeSpan.FromHours(22.5);
                }
            }
            
            public static TimeSpan TwentyTwo
            {
                get
                {
                    return TimeSpan.FromHours(22);
                }
            }
            
            public static TimeSpan TwentyOneHalf
            {
                get
                {
                    return TimeSpan.FromHours(21.5);
                }
            }
            
            public static TimeSpan TwentyOne
            {
                get
                {
                    return TimeSpan.FromHours(21);
                }
            }
            
            public static TimeSpan TwentyHalf
            {
                get
                {
                    return TimeSpan.FromHours(20.5);
                }
            }
            
            public static TimeSpan Twenty
            {
                get
                {
                    return TimeSpan.FromHours(20);
                }
            }
            
            public static TimeSpan NineteenHalf
            {
                get
                {
                    return TimeSpan.FromHours(19.5);
                }
            }
            
            public static TimeSpan Nineteen
            {
                get
                {
                    return TimeSpan.FromHours(19);
                }
            }
            
            public static TimeSpan EighteenHalf
            {
                get
                {
                    return TimeSpan.FromHours(18.5);
                }
            }
            
            public static TimeSpan Eighteen
            {
                get
                {
                    return TimeSpan.FromHours(18);
                }
            }
            
            public static TimeSpan SeventeenHalf
            {
                get
                {
                    return TimeSpan.FromHours(17.5);
                }
            }
            
            public static TimeSpan Seventeen
            {
                get
                {
                    return TimeSpan.FromHours(17);
                }
            }
            
            public static TimeSpan SixteenHalf
            {
                get
                {
                    return TimeSpan.FromHours(16.5);
                }
            }
            
            public static TimeSpan Sixteen
            {
                get
                {
                    return TimeSpan.FromHours(16);
                }
            }
            
            public static TimeSpan FifteenHalf
            {
                get
                {
                    return TimeSpan.FromHours(15.5);
                }
            }
            
            public static TimeSpan Fifteen
            {
                get
                {
                    return TimeSpan.FromHours(15);
                }
            }
            
            public static TimeSpan FourteenHalf
            {
                get
                {
                    return TimeSpan.FromHours(14.5);
                }
            }
            
            public static TimeSpan Fourteen
            {
                get
                {
                    return TimeSpan.FromHours(14);
                }
            }
            
            public static TimeSpan ThirteenHalf
            {
                get
                {
                    return TimeSpan.FromHours(13.5);
                }
            }
            
            public static TimeSpan Thirteen
            {
                get
                {
                    return TimeSpan.FromHours(13);
                }
            }
            
            public static TimeSpan TwelveHalf
            {
                get
                {
                    return TimeSpan.FromHours(12.5);
                }
            }
            
            public static TimeSpan Twelve
            {
                get
                {
                    return TimeSpan.FromHours(12);
                }
            }
            
            public static TimeSpan ElevenHalf
            {
                get
                {
                    return TimeSpan.FromHours(11.5);
                }
            }
            
            public static TimeSpan Eleven
            {
                get
                {
                    return TimeSpan.FromHours(11);
                }
            }
            
            public static TimeSpan TenHalf
            {
                get
                {
                    return TimeSpan.FromHours(10.5);
                }
            }
            
            public static TimeSpan Ten
            {
                get
                {
                    return TimeSpan.FromHours(10);
                }
            }
            
            public static TimeSpan NineHalf
            {
                get
                {
                    return TimeSpan.FromHours(9.5);
                }
            }
            
            public static TimeSpan Nine
            {
                get
                {
                    return TimeSpan.FromHours(9);
                }
            }
            
            public static TimeSpan EightHalf
            {
                get
                {
                    return TimeSpan.FromHours(8.5);
                }
            }
            
            public static TimeSpan Eight
            {
                get
                {
                    return TimeSpan.FromHours(8);
                }
            }
            
            public static TimeSpan SevenHalf
            {
                get
                {
                    return TimeSpan.FromHours(7.5);
                }
            }
            
            public static TimeSpan Seven
            {
                get
                {
                    return TimeSpan.FromHours(7);
                }
            }
            
            public static TimeSpan SixHalf
            {
                get
                {
                    return TimeSpan.FromHours(6.5);
                }
            }
            
            public static TimeSpan Six
            {
                get
                {
                    return TimeSpan.FromHours(6);
                }
            }
            
            public static TimeSpan FiveHalf
            {
                get
                {
                    return TimeSpan.FromHours(5.5);
                }
            }

            public static TimeSpan Five
            {
                get
                {
                    return TimeSpan.FromHours(5);
                }
            }
            
            public static TimeSpan FourHalf
            {
                get
                {
                    return TimeSpan.FromHours(4.5);
                }
            }
            
            public static TimeSpan Four
            {
                get
                {
                    return TimeSpan.FromHours(4);
                }
            }
            
            public static TimeSpan ThreeHalf
            {
                get
                {
                    return TimeSpan.FromHours(3.5);
                }
            }
            
            public static TimeSpan Three
            {
                get
                {
                    return TimeSpan.FromHours(3);
                }
            }
            
            public static TimeSpan TwoHalf
            {
                get
                {
                    return TimeSpan.FromHours(2.5);
                }
            }

            public static TimeSpan Two
            {
                get
                {
                    return TimeSpan.FromHours(2);
                }
            }

            public static TimeSpan OneHalf
            {
                get
                {
                    return TimeSpan.FromHours(1.5);
                }
            }

            public static TimeSpan One
            {
                get
                {
                    return TimeSpan.FromHours(1);
                }
            }

            public static TimeSpan ThreeQuarter
            {
                get
                {
                    return TimeSpan.FromHours(0.75);
                }
            }

            public static TimeSpan TwoThird
            {
                get
                {
                    return TimeSpan.FromHours(2.0 / 3.0);
                }
            }

            public static TimeSpan Half
            {
                get
                {
                    return TimeSpan.FromHours(0.5);
                }
            }
            
            public static TimeSpan OneThird
            {
                get
                {
                    return TimeSpan.FromHours(1.0 / 3.0);
                }
            }

            public static TimeSpan Quarter
            {
                get
                {
                    return TimeSpan.FromHours(0.25);
                }
            }
            
            public static TimeSpan OneTenth
            {
                get
                {
                    return TimeSpan.FromHours(0.1);
                }
            }

            public static TimeSpan Get(Double count)
            {
                return TimeSpan.FromHours(count);
            }
        }

        public static class Minute
        {
            public static TimeSpan Hundred
            {
                get
                {
                    return TimeSpan.FromMinutes(100);
                }
            }
            
            public static TimeSpan Ten
            {
                get
                {
                    return TimeSpan.FromMinutes(10);
                }
            }
            
            public static TimeSpan NineHalf
            {
                get
                {
                    return TimeSpan.FromMinutes(9.5);
                }
            }
            
            public static TimeSpan Nine
            {
                get
                {
                    return TimeSpan.FromMinutes(9);
                }
            }
            
            public static TimeSpan Eight
            {
                get
                {
                    return TimeSpan.FromMinutes(8);
                }
            }
            
            public static TimeSpan SevenFive
            {
                get
                {
                    return TimeSpan.FromMinutes(7.5);
                }
            }
            
            public static TimeSpan SixHalf
            {
                get
                {
                    return TimeSpan.FromMinutes(6.5);
                }
            }
            
            public static TimeSpan Six
            {
                get
                {
                    return TimeSpan.FromMinutes(6);
                }
            }
            
            public static TimeSpan FiveHalf
            {
                get
                {
                    return TimeSpan.FromMinutes(5.5);
                }
            }

            public static TimeSpan Five
            {
                get
                {
                    return TimeSpan.FromMinutes(5);
                }
            }
            
            public static TimeSpan FourHalf
            {
                get
                {
                    return TimeSpan.FromMinutes(4.5);
                }
            }
            
            public static TimeSpan Four
            {
                get
                {
                    return TimeSpan.FromMinutes(4);
                }
            }
            
            public static TimeSpan ThreeHalf
            {
                get
                {
                    return TimeSpan.FromMinutes(3.5);
                }
            }

            public static TimeSpan Three
            {
                get
                {
                    return TimeSpan.FromMinutes(3);
                }
            }
            
            public static TimeSpan TwoHalf
            {
                get
                {
                    return TimeSpan.FromMinutes(2.5);
                }
            }

            public static TimeSpan Two
            {
                get
                {
                    return TimeSpan.FromMinutes(2);
                }
            }

            public static TimeSpan OneHalf
            {
                get
                {
                    return TimeSpan.FromMinutes(1.5);
                }
            }

            public static TimeSpan One
            {
                get
                {
                    return TimeSpan.FromMinutes(1);
                }
            }

            public static TimeSpan ThreeQuarter
            {
                get
                {
                    return TimeSpan.FromMinutes(0.75);
                }
            }
            
            public static TimeSpan TwoThird
            {
                get
                {
                    return TimeSpan.FromMinutes(2.0 / 3.0);
                }
            }

            public static TimeSpan Half
            {
                get
                {
                    return TimeSpan.FromMinutes(0.5);
                }
            }
            
            public static TimeSpan OneThird
            {
                get
                {
                    return TimeSpan.FromMinutes(1.0 / 3.0);
                }
            }

            public static TimeSpan Quarter
            {
                get
                {
                    return TimeSpan.FromMinutes(0.25);
                }
            }
            
            public static TimeSpan OneTenth
            {
                get
                {
                    return TimeSpan.FromMinutes(0.1);
                }
            }

            public static TimeSpan Get(Double count)
            {
                return TimeSpan.FromMinutes(count);
            }
        }

        public static class Second
        {
            public static TimeSpan Hundred
            {
                get
                {
                    return TimeSpan.FromSeconds(100);
                }
            }
            
            public static TimeSpan Ten
            {
                get
                {
                    return TimeSpan.FromSeconds(10);
                }
            }
            
            public static TimeSpan NineHalf
            {
                get
                {
                    return TimeSpan.FromSeconds(9.5);
                }
            }
            
            public static TimeSpan Nine
            {
                get
                {
                    return TimeSpan.FromSeconds(9);
                }
            }
            
            public static TimeSpan EightHalf
            {
                get
                {
                    return TimeSpan.FromSeconds(8.5);
                }
            }
            
            public static TimeSpan Eight
            {
                get
                {
                    return TimeSpan.FromSeconds(8);
                }
            }
            
            public static TimeSpan SevenHalf
            {
                get
                {
                    return TimeSpan.FromSeconds(7.5);
                }
            }
            
            public static TimeSpan Seven
            {
                get
                {
                    return TimeSpan.FromSeconds(7);
                }
            }
            
            public static TimeSpan SixHalf
            {
                get
                {
                    return TimeSpan.FromSeconds(6.5);
                }
            }
            
            public static TimeSpan Six
            {
                get
                {
                    return TimeSpan.FromSeconds(6);
                }
            }
            
            public static TimeSpan FiveHalf
            {
                get
                {
                    return TimeSpan.FromSeconds(5.5);
                }
            }

            public static TimeSpan Five
            {
                get
                {
                    return TimeSpan.FromSeconds(5);
                }
            }
            
            public static TimeSpan FourHalf
            {
                get
                {
                    return TimeSpan.FromSeconds(4.5);
                }
            }
            
            public static TimeSpan Four
            {
                get
                {
                    return TimeSpan.FromSeconds(4);
                }
            }
            
            public static TimeSpan ThreeHalf
            {
                get
                {
                    return TimeSpan.FromSeconds(3.5);
                }
            }

            public static TimeSpan Three
            {
                get
                {
                    return TimeSpan.FromSeconds(3);
                }
            }
            
            public static TimeSpan TwoHalf
            {
                get
                {
                    return TimeSpan.FromSeconds(2.5);
                }
            }

            public static TimeSpan Two
            {
                get
                {
                    return TimeSpan.FromSeconds(2);
                }
            }

            public static TimeSpan OneHalf
            {
                get
                {
                    return TimeSpan.FromSeconds(1.5);
                }
            }

            public static TimeSpan One
            {
                get
                {
                    return TimeSpan.FromSeconds(1);
                }
            }

            public static TimeSpan ThreeQuarter
            {
                get
                {
                    return TimeSpan.FromSeconds(0.75);
                }
            }
            
            public static TimeSpan TwoThird
            {
                get
                {
                    return TimeSpan.FromSeconds(2.0 / 3.0);
                }
            }

            public static TimeSpan Half
            {
                get
                {
                    return TimeSpan.FromSeconds(0.5);
                }
            }
            
            public static TimeSpan OneThird
            {
                get
                {
                    return TimeSpan.FromSeconds(1.0 / 3.0);
                }
            }

            public static TimeSpan Quarter
            {
                get
                {
                    return TimeSpan.FromSeconds(0.25);
                }
            }
            
            public static TimeSpan OneTenth
            {
                get
                {
                    return TimeSpan.FromSeconds(0.1);
                }
            }

            public static TimeSpan Get(Double count)
            {
                return TimeSpan.FromSeconds(count);
            }
        }

        public static class Millisecond
        {
            public static TimeSpan OneSecond
            {
                get
                {
                    return TimeSpan.FromMilliseconds(1000);
                }
            }

            public static TimeSpan ThreeQuarterSecond
            {
                get
                {
                    return TimeSpan.FromMilliseconds(750);
                }
            }

            public static TimeSpan HalfSecond
            {
                get
                {
                    return TimeSpan.FromMilliseconds(500);
                }
            }

            public static TimeSpan QuarterSecond
            {
                get
                {
                    return TimeSpan.FromMilliseconds(250);
                }
            }

            public static TimeSpan Hundred
            {
                get
                {
                    return TimeSpan.FromMilliseconds(100);
                }
            }

            public static TimeSpan Fifty
            {
                get
                {
                    return TimeSpan.FromMilliseconds(50);
                }
            }

            public static TimeSpan Thirty
            {
                get
                {
                    return TimeSpan.FromMilliseconds(30);
                }
            }

            public static TimeSpan TwentyFive
            {
                get
                {
                    return TimeSpan.FromMilliseconds(25);
                }
            }

            public static TimeSpan Twenty
            {
                get
                {
                    return TimeSpan.FromMilliseconds(20);
                }
            }

            public static TimeSpan Ten
            {
                get
                {
                    return TimeSpan.FromMilliseconds(10);
                }
            }
            
            public static TimeSpan Nine
            {
                get
                {
                    return TimeSpan.FromMilliseconds(9);
                }
            }
            
            public static TimeSpan Eight
            {
                get
                {
                    return TimeSpan.FromMilliseconds(8);
                }
            }
            
            public static TimeSpan SevenHalf
            {
                get
                {
                    return TimeSpan.FromMilliseconds(7.5);
                }
            }
            
            public static TimeSpan Seven
            {
                get
                {
                    return TimeSpan.FromMilliseconds(7);
                }
            }
            
            public static TimeSpan SixHalf
            {
                get
                {
                    return TimeSpan.FromMilliseconds(6.5);
                }
            }

            public static TimeSpan Five
            {
                get
                {
                    return TimeSpan.FromMilliseconds(5);
                }
            }
            
            public static TimeSpan Four
            {
                get
                {
                    return TimeSpan.FromMilliseconds(4);
                }
            }

            public static TimeSpan Three
            {
                get
                {
                    return TimeSpan.FromMilliseconds(3);
                }
            }
            
            public static TimeSpan TwoHalf
            {
                get
                {
                    return TimeSpan.FromMilliseconds(2.5);
                }
            }

            public static TimeSpan Two
            {
                get
                {
                    return TimeSpan.FromMilliseconds(2);
                }
            }

            public static TimeSpan One
            {
                get
                {
                    return TimeSpan.FromMilliseconds(1);
                }
            }
            
            public static TimeSpan ThreeQuarter
            {
                get
                {
                    return TimeSpan.FromMilliseconds(0.75);
                }
            }
            
            public static TimeSpan TwoThird
            {
                get
                {
                    return TimeSpan.FromMilliseconds(2.0 / 3.0);
                }
            }
            
            public static TimeSpan Half
            {
                get
                {
                    return TimeSpan.FromMilliseconds(0.5);
                }
            }
            
            public static TimeSpan OneThird
            {
                get
                {
                    return TimeSpan.FromMilliseconds(1.0 / 3.0);
                }
            }
            
            public static TimeSpan Quarter
            {
                get
                {
                    return TimeSpan.FromMilliseconds(0.25);
                }
            }
            
            public static TimeSpan OneTenth
            {
                get
                {
                    return TimeSpan.FromMilliseconds(0.1);
                }
            }

            public static TimeSpan Get(Double count)
            {
                return TimeSpan.FromMilliseconds(count);
            }
        }
    }
}