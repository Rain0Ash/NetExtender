using System;
using System.ComponentModel;
using NetExtender.Interfaces.Notify;
using NetExtender.Utilities.Types;

namespace NetExtender.UserInterface.WindowsPresentation.ExcelGrid
{
    public class ExcelCellDescriptor : INotifyProperty
    {
        public event PropertyChangingEventHandler? PropertyChanging;
        public event PropertyChangedEventHandler? PropertyChanged;

        private ExcelPropertyDefinition? _property;
        public ExcelPropertyDefinition? Property
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

        private Object? _item;
        public Object? Item
        {
            get
            {
                return _item;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _item, value);
            }
        }
        
        private Type? _type;
        public Type? PropertyType
        {
            get
            {
                return _type;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _type, value);
            }
        }
        
        private String? _path;
        public String? BindingPath
        {
            get
            {
                return _path;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _path, value);
            }
        }
        
        private Object? _source;
        public Object? BindingSource
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