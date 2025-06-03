using System;
using NetExtender.Interfaces;

namespace NetExtender.Types.Console.Interfaces
{
    public interface IConsoleFont : IConsoleFontInfo
    {
        public new static IConsoleFont New
        {
            get
            {
                return IConsoleFontInfo.New;
            }
        }
        
        public new static IConsoleFont? Current
        {
            get
            {
                return ConsoleFontInfo.Current?.Invoke();
            }
        }

        public new String? FontName { get; set; }
        public new Int32 Index { get; set; }
        public new Int16 Width { get; set; }
        public new Int16 Size { get; set; }
        public new Int32 Family { get; set; }
        public new Int32 Weight { get; set; }
    }

    public interface IConsoleFontInfo : ICloneable<IConsoleFont>, ICloneable
    {
        public static IConsoleFont New
        {
            get
            {
                return Current?.Clone() ?? new ConsoleFontInfo.Font();
            }
        }

        public static IConsoleFontInfo? Current
        {
            get
            {
                return ConsoleFontInfo.Current?.Invoke();
            }
        }

        public String? FontName { get; }
        public Int32 Index { get; }
        public Int16 Width { get; }
        public Int16 Size { get; }
        public Int32 Family { get; }
        public Int32 Weight { get; }
        public Boolean IsEmpty { get; }

        public new IConsoleFont Clone();
    }
}