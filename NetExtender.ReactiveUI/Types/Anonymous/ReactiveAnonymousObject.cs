// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using NetExtender.ReactiveUI.Types.Anonymous.Interfaces;
using NetExtender.Types.Anonymous;
using NetExtender.Utilities.Types;
using ReactiveUI;

namespace NetExtender.ReactiveUI.Anonymous.Core
{
    public abstract class ReactiveAnonymousObject : ReactiveObject, IReactiveAnonymousObject
    {
        public IEnumerator<KeyValuePair<String, Object?>> GetEnumerator()
        {
            return AnonymousTypeUtilities.Values(this).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        public AnonymousObjectProperty this[Int32 index]
        {
            get
            {
                return new AnonymousObjectProperty(this, index);
            }
        }

        public AnonymousObjectProperty this[String property]
        {
            get
            {
                return new AnonymousObjectProperty(this, property);
            }
        }
    }
}