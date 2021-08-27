// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetExtender.Types.TextWriters
{
    public class LockedTextWriterWrapper : TextWriter
    {
        public TextWriter Internal { get; }
        
        public override Encoding Encoding
        {
            get
            {
                return Internal.Encoding;
            }
        }

        public override IFormatProvider FormatProvider
        {
            get
            {
                return Internal.FormatProvider;
            }
        }

        [AllowNull]
        public override String NewLine
        {
            get
            {
                return Internal.NewLine;
            }
            set
            {
                Internal.NewLine = value;
            }
        }

        public LockedTextWriterWrapper(TextWriter writer)
        {
            Internal = writer ?? throw new ArgumentNullException(nameof(writer));
            Monitor.Enter(Internal);
        }

        public override void Write(Boolean value)
        {
            Internal.Write(value);
        }

        public override void Write(Char value)
        {
            Internal.Write(value);
        }

        public override void Write(Char[]? buffer)
        {
            Internal.Write(buffer);
        }

        public override void Write(Char[] buffer, Int32 index, Int32 count)
        {
            Internal.Write(buffer, index, count);
        }

        public override void Write(Decimal value)
        {
            Internal.Write(value);
        }

        public override void Write(Double value)
        {
            Internal.Write(value);
        }

        public override void Write(Int32 value)
        {
            Internal.Write(value);
        }

        public override void Write(Int64 value)
        {
            Internal.Write(value);
        }

        public override void Write(Object? value)
        {
            Internal.Write(value);
        }

        public override void Write(ReadOnlySpan<Char> buffer)
        {
            Internal.Write(buffer);
        }

        public override void Write(Single value)
        {
            Internal.Write(value);
        }

        public override void Write(String? value)
        {
            Internal.Write(value);
        }

        public override void Write(String format, Object? arg0)
        {
            Internal.Write(format, arg0);
        }

        public override void Write(String format, Object? arg0, Object? arg1)
        {
            Internal.Write(format, arg0, arg1);
        }

        public override void Write(String format, Object? arg0, Object? arg1, Object? arg2)
        {
            Internal.Write(format, arg0, arg1, arg2);
        }

        public override void Write(String format, params Object?[] arg)
        {
            Internal.Write(format, arg);
        }

        public override void Write(StringBuilder? value)
        {
            Internal.Write(value);
        }

        public override void Write(UInt32 value)
        {
            Internal.Write(value);
        }

        public override void Write(UInt64 value)
        {
            Internal.Write(value);
        }

        public override Task WriteAsync(Char value)
        {
            return Internal.WriteAsync(value);
        }

        public new Task WriteAsync(Char[]? buffer)
        {
            return Internal.WriteAsync(buffer);
        }

        public override Task WriteAsync(Char[] buffer, Int32 index, Int32 count)
        {
            return Internal.WriteAsync(buffer, index, count);
        }

        public override Task WriteAsync(ReadOnlyMemory<Char> buffer, CancellationToken cancellationToken = new CancellationToken())
        {
            return Internal.WriteAsync(buffer, cancellationToken);
        }

        public override Task WriteAsync(String? value)
        {
            return Internal.WriteAsync(value);
        }

        public override Task WriteAsync(StringBuilder? value, CancellationToken cancellationToken = new CancellationToken())
        {
            return Internal.WriteAsync(value, cancellationToken);
        }

        public override void WriteLine()
        {
            Internal.WriteLine();
        }

        public override void WriteLine(Boolean value)
        {
            Internal.WriteLine(value);
        }

        public override void WriteLine(Char value)
        {
            Internal.WriteLine(value);
        }

        public override void WriteLine(Char[]? buffer)
        {
            Internal.WriteLine(buffer);
        }

        public override void WriteLine(Char[] buffer, Int32 index, Int32 count)
        {
            Internal.WriteLine(buffer, index, count);
        }

        public override void WriteLine(Decimal value)
        {
            Internal.WriteLine(value);
        }

        public override void WriteLine(Double value)
        {
            Internal.WriteLine(value);
        }

        public override void WriteLine(Int32 value)
        {
            Internal.WriteLine(value);
        }

        public override void WriteLine(Int64 value)
        {
            Internal.WriteLine(value);
        }

        public override void WriteLine(Object? value)
        {
            Internal.WriteLine(value);
        }

        public override void WriteLine(ReadOnlySpan<Char> buffer)
        {
            Internal.WriteLine(buffer);
        }

        public override void WriteLine(Single value)
        {
            Internal.WriteLine(value);
        }

        public override void WriteLine(String? value)
        {
            Internal.WriteLine(value);
        }

        public override void WriteLine(String format, Object? arg0)
        {
            Internal.WriteLine(format, arg0);
        }

        public override void WriteLine(String format, Object? arg0, Object? arg1)
        {
            Internal.WriteLine(format, arg0, arg1);
        }

        public override void WriteLine(String format, Object? arg0, Object? arg1, Object? arg2)
        {
            Internal.WriteLine(format, arg0, arg1, arg2);
        }

        public override void WriteLine(String format, params Object?[] arg)
        {
            Internal.WriteLine(format, arg);
        }

        public override void WriteLine(StringBuilder? value)
        {
            Internal.WriteLine(value);
        }

        public override void WriteLine(UInt32 value)
        {
            Internal.WriteLine(value);
        }

        public override void WriteLine(UInt64 value)
        {
            Internal.WriteLine(value);
        }

        public override Task WriteLineAsync()
        {
            return Internal.WriteLineAsync();
        }

        public override Task WriteLineAsync(Char value)
        {
            return Internal.WriteLineAsync(value);
        }

        public new Task WriteLineAsync(Char[]? buffer)
        {
            return Internal.WriteLineAsync(buffer);
        }

        public override Task WriteLineAsync(Char[] buffer, Int32 index, Int32 count)
        {
            return Internal.WriteLineAsync(buffer, index, count);
        }

        public override Task WriteLineAsync(ReadOnlyMemory<Char> buffer, CancellationToken cancellationToken = new CancellationToken())
        {
            return Internal.WriteLineAsync(buffer, cancellationToken);
        }

        public override Task WriteLineAsync(String? value)
        {
            return Internal.WriteLineAsync(value);
        }

        public override Task WriteLineAsync(StringBuilder? value, CancellationToken cancellationToken = new CancellationToken())
        {
            return Internal.WriteLineAsync(value, cancellationToken);
        }
        
        public override void Flush()
        {
            Internal.Flush();
        }

        public override Task FlushAsync()
        {
            return Internal.FlushAsync();
        }

        public override void Close()
        {
            Internal.Close();
        }

        protected override void Dispose(Boolean disposing)
        {
            Monitor.Exit(Internal);
        }

        public override ValueTask DisposeAsync()
        {
            Dispose();
            return ValueTask.CompletedTask;
        }

        [Obsolete("This Remoting API is not supported and throws PlatformNotSupportedException.", DiagnosticId = "SYSLIB0010", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
        public new Object GetLifetimeService()
        {
            return Internal.GetLifetimeService();
        }

        [Obsolete("This Remoting API is not supported and throws PlatformNotSupportedException.", DiagnosticId = "SYSLIB0010", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
        public override Object InitializeLifetimeService()
        {
            return Internal.InitializeLifetimeService();
        }
    }
}