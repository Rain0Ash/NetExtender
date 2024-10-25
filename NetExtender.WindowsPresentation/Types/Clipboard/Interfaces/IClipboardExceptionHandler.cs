using System;

namespace NetExtender.WindowsPresentation.Types.Clipboard.Interfaces
{
    public interface IClipboardExceptionHandler
    {
        public Boolean Handle(Exception? exception);
    }
}