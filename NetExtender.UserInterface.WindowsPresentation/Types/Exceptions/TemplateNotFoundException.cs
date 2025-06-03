// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;
using System.Windows;
using NetExtender.Utilities.Serialization;
using NetExtender.Utilities.Types;

namespace NetExtender.WindowsPresentation.Types.Exceptions
{
    public class TemplateNotFoundException<T> : TemplateNotFoundException where T : DependencyObject
    {
        public new Type? Type
        {
            get
            {
                return base.Type;
            }
        }
        
        public TemplateNotFoundException(String name)
            : base(name, typeof(T))
        {
        }
        
        public TemplateNotFoundException(String name, FrameworkElement? control)
            : base(name, typeof(T), control)
        {
        }
        
        public TemplateNotFoundException(String name, String? message)
            : base(name, typeof(T), message)
        {
        }
        
        public TemplateNotFoundException(String name, FrameworkElement? control, String? message)
            : base(name, typeof(T), control, message)
        {
        }
        
        public TemplateNotFoundException(String name, String? message, Exception? exception)
            : base(name, typeof(T), message, exception)
        {
        }
        
        public TemplateNotFoundException(String name, FrameworkElement? control, String? message, Exception? exception)
            : base(name, typeof(T), control, message, exception)
        {
        }
        
        protected TemplateNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
    
    public class TemplateNotFoundException : InvalidOperationException
    {
        private new const String Message = "Template '{0}' not found.";
        private const String TypeMessage = "Template '{0}' with type '{1}' not found.";
        private const String ControlMessage = "Template '{0}' for control '{1}' not found.";
        private const String TypeControlMessage = "Template '{0}' for control '{1}' with type '{2}' not found.";
        
        public String Name { get; }
        public Type? Type { get; init; }
        public FrameworkElement? Control { get; init; }
        
        public TemplateNotFoundException(String name)
            : this(name, null, (FrameworkElement?) null)
        {
        }
        
        public TemplateNotFoundException(String name, FrameworkElement? control)
            : this(name, null, control)
        {
        }
        
        public TemplateNotFoundException(String name, Type? type)
            : this(name, type, (FrameworkElement?) null)
        {
        }
        
        public TemplateNotFoundException(String name, Type? type, FrameworkElement? control)
            : this(name, type, control, null)
        {
        }
        
        public TemplateNotFoundException(String name, String? message)
            : this(name, null, null, message)
        {
        }
        
        public TemplateNotFoundException(String name, FrameworkElement? control, String? message)
            : this(name, null, control, message)
        {
        }
        
        public TemplateNotFoundException(String name, Type? type, String? message)
            : this(name, type, null, message)
        {
        }
        
        public TemplateNotFoundException(String name, Type? type, FrameworkElement? control, String? message)
            : base(message)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Type = type;
            Control = control;
        }
        
        public TemplateNotFoundException(String name, String? message, Exception? exception)
            : this(name, null, null, message, exception)
        {
        }
        
        public TemplateNotFoundException(String name, FrameworkElement? control, String? message, Exception? exception)
            : this(name, null, control, message, exception)
        {
        }
        
        public TemplateNotFoundException(String name, Type? type, String? message, Exception? exception)
            : this(name, type, null, message, exception)
        {
        }
        
        public TemplateNotFoundException(String name, Type? type, FrameworkElement? control, String? message, Exception? exception)
            : base(message, exception)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Type = type;
            Control = control;
        }
        
        protected TemplateNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Name = info.GetString(nameof(Name)) ?? String.Empty;
            Type = info.GetValueOrDefault<Type>(nameof(Type));
        }
        
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(Name), Name);
            info.AddValue(nameof(Type), Type);
        }
        
        private static String Format(String? name, Type? type, FrameworkElement? control, String? message)
        {
            if (message is not null)
            {
                return message;
            }
            
            const String unknown = nameof(unknown);
            name ??= unknown;
            
            if (control is null)
            {
                return type is not null ? TypeMessage.Format(name, type.Name) : Message.Format(name);
            }
            
            String format = $"{control.Name}({control.GetType()})";
            return type is not null ? TypeControlMessage.Format(name, type.Name, format) : ControlMessage.Format(name, format);
        }
    }
}