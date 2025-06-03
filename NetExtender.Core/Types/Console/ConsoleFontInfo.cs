using System;
using NetExtender.Types.Console.Interfaces;

namespace NetExtender.Types.Console
{
    public class ConsoleFontInfo : IConsoleFontInfo
    {
        protected internal static Func<Font?>? Current { get; protected set; }
        
        private Font Internal;

        public String? FontName
        {
            get
            {
                return Internal.FontName;
            }
            protected set
            {
                Internal.FontName = value;
            }
        }

        public Int32 Index
        {
            get
            {
                return Internal.Index;
            }
            protected set
            {
                Internal.Index = value;
            }
        }

        public Int16 Width
        {
            get
            {
                return Internal.Width;
            }
            protected set
            {
                Internal.Width = value;
            }
        }

        public Int16 Size
        {
            get
            {
                return Internal.Size;
            }
            protected set
            {
                Internal.Size = value;
            }
        }

        public Int32 Family
        {
            get
            {
                return Internal.Family;
            }
            protected set
            {
                Internal.Family = value;
            }
        }

        public Int32 Weight
        {
            get
            {
                return Internal.Weight;
            }
            protected set
            {
                Internal.Weight = value;
            }
        }

        public Boolean IsEmpty
        {
            get
            {
                return Internal.IsEmpty;
            }
        }

        protected ConsoleFontInfo()
        {
        }

        public IConsoleFont Clone()
        {
            return Internal.Clone();
        }

        Object ICloneable.Clone()
        {
            return Clone();
        }

        protected internal struct Font : IStruct<Font>, IConsoleFont
        {
            public String? FontName { get; set; }
            public Int32 Index { get; set; }
            public Int16 Width { get; set; }
            public Int16 Size { get; set; }
            public Int32 Family { get; set; }
            public Int32 Weight { get; set; }

            public readonly Boolean IsEmpty
            {
                get
                {
                    return String.IsNullOrEmpty(FontName) && Index == 0 && Width == 0 && Size == 0 && Family == 0 && Weight == 0;
                }
            }

            public IConsoleFont Clone()
            {
                return new Font
                {
                    FontName = FontName,
                    Index = Index,
                    Width = Width,
                    Size = Size,
                    Family = Family,
                    Weight = Weight,
                };
            }

            Object ICloneable.Clone()
            {
                return Clone();
            }
        }
    }
}