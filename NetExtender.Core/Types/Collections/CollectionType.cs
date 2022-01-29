// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Collections
{
    [Flags]
    public enum CollectionType
    {
        None = 0,
        Generic = 1,
        Enumerable = 2,
        GenericEnumerable = Enumerable | Generic,
        Collection = 4 | Enumerable,
        GenericCollection = Collection | Generic,
        Array = 8 | Collection,
        GenericArray = Array | Generic,
        List = 16 | Collection,
        GenericList = List | Generic,
        Set = 32 | Collection,
        GenericSet = Set | Generic,
        Dictionary = 64 | Collection,
        GenericDictionary = Dictionary | Generic,
        Map = 128 | Collection,
        GenericMap = Map | Generic,
        Stack = 256 | Collection,
        GenericStack = Stack | Generic,
        Queue = 512 | Collection,
        GenericQueue = Queue | Generic,
    }
}