// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetExtender.Types.TextWriters
{
    public class FilterTextWriterWrapper : TextWriter, ISet<String>, IReadOnlySet<String>
    {
        protected TextWriter Internal { get; }

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

        protected ISet<String> Filter { get; }

        public Int32 Count
        {
            get
            {
                return Filter.Count;
            }
        }

        public Boolean IsReadOnly
        {
            get
            {
                return Filter.IsReadOnly;
            }
        }

        public FilterTextWriterWrapper(TextWriter writer)
        {
            Internal = writer ?? throw new ArgumentNullException(nameof(writer));
            Filter = new HashSet<String>();
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

        public override Task WriteAsync(ReadOnlyMemory<Char> buffer, CancellationToken token = default)
        {
            return Internal.WriteAsync(buffer, token);
        }

        public override Task WriteAsync(String? value)
        {
            return Internal.WriteAsync(value);
        }

        public override Task WriteAsync(StringBuilder? value, CancellationToken token = default)
        {
            return Internal.WriteAsync(value, token);
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
            if (Filter.Contains(new String(buffer, index, count)))
            {
                return;
            }

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
            if (Filter.Contains(value!))
            {
                return;
            }

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

        public override Task WriteLineAsync(ReadOnlyMemory<Char> buffer, CancellationToken token = default)
        {
            return Internal.WriteLineAsync(buffer, token);
        }

        public override Task WriteLineAsync(String? value)
        {
            return Internal.WriteLineAsync(value);
        }

        public override Task WriteLineAsync(StringBuilder? value, CancellationToken token = default)
        {
            return Internal.WriteLineAsync(value, token);
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
            Internal.Dispose();
            base.Dispose(disposing);
        }

        public override ValueTask DisposeAsync()
        {
            return Internal.DisposeAsync();
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

        public Boolean Contains(String item)
        {
            return Filter.Contains(item);
        }

        void ICollection<String>.Add(String item)
        {
            Add(item);
        }

        public Boolean Add(String item)
        {
            return Filter.Add(item);
        }

        public Boolean Remove(String item)
        {
            return Filter.Remove(item);
        }

        public void ExceptWith(IEnumerable<String> other)
        {
            Filter.ExceptWith(other);
        }

        public void IntersectWith(IEnumerable<String> other)
        {
            Filter.IntersectWith(other);
        }

        public Boolean IsProperSubsetOf(IEnumerable<String> other)
        {
            return Filter.IsProperSubsetOf(other);
        }

        public Boolean IsProperSupersetOf(IEnumerable<String> other)
        {
            return Filter.IsProperSupersetOf(other);
        }

        public Boolean IsSubsetOf(IEnumerable<String> other)
        {
            return Filter.IsSubsetOf(other);
        }

        public Boolean IsSupersetOf(IEnumerable<String> other)
        {
            return Filter.IsSupersetOf(other);
        }

        public Boolean Overlaps(IEnumerable<String> other)
        {
            return Filter.Overlaps(other);
        }

        public Boolean SetEquals(IEnumerable<String> other)
        {
            return Filter.SetEquals(other);
        }

        public void SymmetricExceptWith(IEnumerable<String> other)
        {
            Filter.SymmetricExceptWith(other);
        }

        public void UnionWith(IEnumerable<String> other)
        {
            Filter.UnionWith(other);
        }

        public void Clear()
        {
            Filter.Clear();
        }

        public void CopyTo(String[] array, Int32 index)
        {
            Filter.CopyTo(array, index);
        }

        public IEnumerator<String> GetEnumerator()
        {
            return Filter.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) Filter).GetEnumerator();
        }
    }
}