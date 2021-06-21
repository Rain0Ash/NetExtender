// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Net;

namespace NetExtender
{
    public delegate void EmptyHandler();

    public delegate void StringHandler(String str);

    public delegate void EnumerableHandler(IEnumerable<Object> list);

    public delegate void BooleanHandler(Boolean boolean);

    public delegate void HttpStatusCodeHandler(HttpStatusCode status);

    public delegate void FuncHandler<out T, in TOut>(Func<T, TOut> function);

    public delegate TOut ParseHandler<in T, out TOut>(T value);
    
    public delegate Boolean TryParseHandler<in T, TOut>(T value, out TOut result);

    public delegate Boolean TryConverter<in TIn, TOut>(TIn value, out TOut converted);
    
    public delegate T StructLazyFactoryHandler<TStruct, out T>(in TStruct @struct) where TStruct : struct;

    public delegate void ObjectHandler(Object obj);

    public delegate void IndexObjectHandler(Int32 index, Object obj);

    public delegate void KeyValueHandler(Object key, Object value);

    public delegate void KeyValueHandler<in T1, in T2>(T1 key, T2 value);
        
    public delegate void ObjectArrayHandler(Object[] obj);

    public delegate void TypeHandler<in T>(T type);
    
    public delegate void SenderTypeHandler<in T>(Object sender, T type);

    public delegate void IndexTypeHandler<in T>(Int32 index, T type);

    public delegate void SenderIndexTypeHandler<in T>(Object sender, Int32 index, T type);
    
    public delegate void RTypeHandler<T>(ref T type);
    
    public delegate void SenderRTypeHandler<T>(Object sender, ref T type);

    public delegate void IndexRTypeHandler<T>(Int32 index, ref T type);
    
    public delegate void SenderIndexRTypeHandler<T>(Object sender, Int32 index, ref T type);

    public delegate void TypeArrayHandler<in T>(T[] array);

    public delegate void TypeKeyValueHandler<in TKey, in TValue>(TKey key, TValue value);

    public delegate void IndexTypeKeyValueHandler<in TKey, in TValue>(Int32 index, TKey key, TValue value);

    public delegate void RTypeKeyValueHandler<TKey, TValue>(ref TKey key, ref TValue value);

    public delegate void IndexRTypeKeyValueHandler<TKey, TValue>(Int32 index, ref TKey key, ref TValue value);
}