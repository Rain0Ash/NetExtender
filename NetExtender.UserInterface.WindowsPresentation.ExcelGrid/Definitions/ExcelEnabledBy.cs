using System;
using System.ComponentModel;
using NetExtender.Interfaces.Notify;
using NetExtender.Utilities.Types;

namespace NetExtender.UserInterface.WindowsPresentation.ExcelGrid
{
    public class ExcelEnabledBy : INotifyProperty
    {
        public event PropertyChangingEventHandler? PropertyChanging;
        public event PropertyChangedEventHandler? PropertyChanged;
        
        private String? _property;
        public String? Property
        {
            get
            {
                return _property;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _property, value);
            }
        }
        
        private Object? _value;
        public Object? Value
        {
            get
            {
                return _value;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _value, value);
            }
        }
        
        private Object? _source;
        public Object? Source
        {
            get
            {
                return _source;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _source, value);
            }
        }
    }
}