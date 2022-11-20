// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using NetExtender.Types.Memento.Builder.Interfaces;

namespace NetExtender.Types.Memento.Builder
{
    public class MementoBuilder<TSource> : IMementoBuilder<TSource> where TSource : class
    {
        private List<IMementoProperty<TSource>> Internal { get; }

        public MementoBuilder()
        {
            Internal = new List<IMementoProperty<TSource>>();
        }

        public virtual MementoBuilder<TSource> Remember(IMementoProperty<TSource> property)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            Internal.Add(property);
            return this;
        }

        IMementoBuilder<TSource> IMementoBuilder<TSource>.Remember(IMementoProperty<TSource> property)
        {
            return Remember(property);
        }

        public virtual IMementoProperty<TSource>[] Build()
        {
            return Internal.Select(item => item.Item()).ToArray();
        }

        public virtual IMementoItem<TSource> Build(TSource source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            IEnumerable<IMementoProperty<TSource>> selector = Internal.Select(item => item.New().Update(source));
            return new MementoGroup<TSource>(source, selector);
        }
    }
}