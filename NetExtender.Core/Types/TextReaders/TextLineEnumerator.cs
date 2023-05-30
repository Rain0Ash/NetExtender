// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace NetExtender.Types.TextReaders
{
    public sealed class TextLineEnumerator : IEnumerable<String>, IEnumerator<String>, IAsyncEnumerable<String>, IAsyncEnumerator<String>
    {
        private TextReader Reader { get; }
        private Boolean Disposing { get; }

        private volatile String? Line;

        public String Current
        {
            get
            {
                return Line ?? throw new InvalidOperationException();
            }
        }

        Object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public TextLineEnumerator(TextReader reader)
            : this(reader, false)
        {
        }

        public TextLineEnumerator(TextReader reader, Boolean disposing)
        {
            Reader = reader ?? throw new ArgumentNullException(nameof(reader));
            Disposing = disposing;
        }

        public Boolean MoveNext()
        {
            return (Line = Reader.ReadLine()) is not null;
        }

        public async ValueTask<Boolean> MoveNextAsync()
        {
            return (Line = await Reader.ReadLineAsync().ConfigureAwait(false)) is not null;
        }

        void IEnumerator.Reset()
        {
            throw new NotSupportedException();
        }

        public IEnumerator<String> GetEnumerator()
        {
            return this;
        }

        public IAsyncEnumerator<String> GetAsyncEnumerator(CancellationToken token = default)
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }

        public void Dispose()
        {
            if (Disposing)
            {
                Reader.Dispose();
            }
        }

        public ValueTask DisposeAsync()
        {
            Dispose();
            return ValueTask.CompletedTask;
        }
    }
}