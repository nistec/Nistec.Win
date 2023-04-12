using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Runtime.InteropServices;

using System.Threading;
using MControl.Win;

namespace MControl.Collections
{

    /// <summary>
    /// Represents a collection of key/value pairs that are sorted by the keys and
    /// are accessible by key and by index.
    /// </summary>
    /// <typeparam name="Key"></typeparam>
    /// <typeparam name="Value"></typeparam>
    [Serializable]//, DebuggerDisplay("Count = {Count}"), ComVisible(false), DebuggerTypeProxy(typeof(System_DictionaryDebugView<,>))]
    public class GenericList<Key, Value> : IDictionary<Key, Value>, ICollection<KeyValuePair<Key, Value>>, IEnumerable<KeyValuePair<Key, Value>>, IDictionary, ICollection, IEnumerable
    {

        // Fields
        private const int _defaultCapacity = 4;
        private int _size;
        [NonSerialized]
        private object _syncRoot;
        private IComparer<Key> comparer;
        private static Key[] emptyKeys;
        private static Value[] emptyValues;
        private KeyList/*<Key, Value>*/ keyList;
        private Key[] keys;
        private ValueList/*<Key, Value>*/ valueList;
        private Value[] values;
        private int version;

        // Methods
        static GenericList()
        {
            GenericList<Key, Value>.emptyKeys = new Key[0];
            GenericList<Key, Value>.emptyValues = new Value[0];
        }
        /// <summary>
        /// Initializes a new instance of the GenericList class that
        ///     is empty, has the default initial capacity
        /// </summary>
        public GenericList()
        {
            this.keys = GenericList<Key, Value>.emptyKeys;
            this.values = GenericList<Key, Value>.emptyValues;
            this._size = 0;
            this.comparer = Comparer<Key>.Default;
        }
        /// <summary>
        /// Initializes a new instance of the GenericList class that
        ///     is empty, has the default initial capacity, and is sorted according to the
        ///     specified System.Collections.IComparer interface.
        /// </summary>
        /// <param name="comparer"></param>
        public GenericList(IComparer<Key> comparer)
            : this()
        {
            if (comparer != null)
            {
                this.comparer = comparer;
            }
        }
        /// <summary>
        /// Initializes a new instance of the GenericList class that
        ///     contains elements copied from the specified dictionary, has the same initial
        ///     capacity as the number of elements copied, and is sorted according to the
        ///     System.IComparable interface implemented by each key.
        /// </summary>
        /// <param name="dictionary"></param>
        public GenericList(IDictionary<Key, Value> dictionary)
            : this(dictionary, null)
        {
        }
        /// <summary>
        /// Initializes a new instance of the GenericList class that
        ///     is empty, has the specified initial capacity, and is sorted according to
        ///     the System.IComparable interface implemented by each key added to the GenericList object.
        /// </summary>
        /// <param name="capacity"></param>
        public GenericList(int capacity)
        {
            if (capacity < 0)
            {
                ExceptionHelper.ArgumentOutOfRangeException(ExceptionArgument.capacity, ExceptionResource.ArgumentOutOfRange_NeedNonNegNumRequired);
            }
            this.keys = new Key[capacity];
            this.values = new Value[capacity];
            this.comparer = Comparer<Key>.Default;
        }
        /// <summary>
        /// Initializes a new instance of the GenericList class that
        ///     is empty, has the specified initial capacity, and is sorted according to
        ///     the specified System.Collections.IComparer interface.
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="comparer"></param>
        public GenericList(IDictionary<Key, Value> dictionary, IComparer<Key> comparer)
            : this((dictionary != null) ? dictionary.Count : 0, comparer)
        {
            if (dictionary == null)
            {
                ExceptionHelper.ArgumentNullException(ExceptionArgument.dictionary);
            }
            dictionary.Keys.CopyTo(this.keys, 0);
            dictionary.Values.CopyTo(this.values, 0);
            Array.Sort<Key, Value>(this.keys, this.values, comparer);
            this._size = dictionary.Count;
        }
        /// <summary>
        /// Initializes a new instance of the GenericList class that
        ///     contains elements copied from the specified dictionary, has the same initial
        ///     capacity as the number of elements copied, and is sorted according to the
        ///     specified System.Collections.IComparer interface.
        /// </summary>
        /// <param name="capacity"></param>
        /// <param name="comparer"></param>
        public GenericList(int capacity, IComparer<Key> comparer)
            : this(comparer)
        {
            this.Capacity = capacity;
        }

        public void Add(Key key, Value value)
        {
            if (key == null)
            {
                ExceptionHelper.ArgumentNullException(ExceptionArgument.key);
            }
            int num = Array.BinarySearch<Key>(this.keys, 0, this._size, key, this.comparer);
            if (num >= 0)
            {
                ExceptionHelper.ArgumentException(ExceptionResource.Argument_AddingDuplicate);
            }
            this.Insert(~num, key, value);
        }

        public void Clear()
        {
            this.version++;
            Array.Clear(this.keys, 0, this._size);
            Array.Clear(this.values, 0, this._size);
            this._size = 0;
        }

        public bool ContainsKey(Key key)
        {
            return (this.IndexOfKey(key) >= 0);
        }

        public bool ContainsValue(Value value)
        {
            return (this.IndexOfValue(value) >= 0);
        }

        private void EnsureCapacity(int min)
        {
            int num = (this.keys.Length == 0) ? 4 : (this.keys.Length * 2);
            if (num < min)
            {
                num = min;
            }
            this.Capacity = num;
        }

        private Value GetByIndex(int index)
        {
            if ((index < 0) || (index >= this._size))
            {
                ExceptionHelper.ArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_Index);
            }
            return this.values[index];
        }

        public IEnumerator<KeyValuePair<Key, Value>> GetEnumerator()
        {
            return new Enumerator/*<Key, Value>*/((GenericList<Key, Value>)this, 1);
        }

        private Key GetKey(int index)
        {
            if ((index < 0) || (index >= this._size))
            {
                ExceptionHelper.ArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_Index);
            }
            return this.keys[index];
        }

        private KeyList/*<Key, Value>*/ GetKeyListHelper()
        {
            if (this.keyList == null)
            {
                this.keyList = new KeyList/*<Key, Value>*/((GenericList<Key, Value>)this);
            }
            return this.keyList;
        }

        private ValueList/*<Key, Value>*/ GetValueListHelper()
        {
            if (this.valueList == null)
            {
                this.valueList = new ValueList/*<Key, Value>*/((GenericList<Key, Value>)this);
            }
            return this.valueList;
        }

        public int IndexOfKey(Key key)
        {
            if (key == null)
            {
                ExceptionHelper.ArgumentNullException(ExceptionArgument.key);
            }
            int num = Array.BinarySearch<Key>(this.keys, 0, this._size, key, this.comparer);
            if (num < 0)
            {
                return -1;
            }
            return num;
        }

        public int IndexOfValue(Value value)
        {
            return Array.IndexOf<Value>(this.values, value, 0, this._size);
        }

        private void Insert(int index, Key key, Value value)
        {
            if (this._size == this.keys.Length)
            {
                this.EnsureCapacity(this._size + 1);
            }
            if (index < this._size)
            {
                Array.Copy(this.keys, index, this.keys, index + 1, this._size - index);
                Array.Copy(this.values, index, this.values, index + 1, this._size - index);
            }
            this.keys[index] = key;
            this.values[index] = value;
            this._size++;
            this.version++;
        }

        private static bool IsCompatibleKey(object key)
        {
            if (key == null)
            {
                ExceptionHelper.ArgumentNullException(ExceptionArgument.key);
            }
            return (key is Key);
        }

        public bool Remove(Key key)
        {
            int index = this.IndexOfKey(key);
            if (index >= 0)
            {
                this.RemoveAt(index);
            }
            return (index >= 0);
        }

        public void RemoveAt(int index)
        {
            if ((index < 0) || (index >= this._size))
            {
                ExceptionHelper.ArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_Index);
            }
            this._size--;
            if (index < this._size)
            {
                Array.Copy(this.keys, index + 1, this.keys, index, this._size - index);
                Array.Copy(this.values, index + 1, this.values, index, this._size - index);
            }
            this.keys[this._size] = default(Key);
            this.values[this._size] = default(Value);
            this.version++;
        }

        void ICollection<KeyValuePair<Key, Value>>.Add(KeyValuePair<Key, Value> keyValuePair)
        {
            this.Add(keyValuePair.Key, keyValuePair.Value);
        }

        bool ICollection<KeyValuePair<Key, Value>>.Contains(KeyValuePair<Key, Value> keyValuePair)
        {
            int index = this.IndexOfKey(keyValuePair.Key);
            return ((index >= 0) && EqualityComparer<Value>.Default.Equals(this.values[index], keyValuePair.Value));
        }

        void ICollection<KeyValuePair<Key, Value>>.CopyTo(KeyValuePair<Key, Value>[] array, int arrayIndex)
        {
            if (array == null)
            {
                ExceptionHelper.ArgumentNullException(ExceptionArgument.array);
            }
            if ((arrayIndex < 0) || (arrayIndex > array.Length))
            {
                ExceptionHelper.ArgumentOutOfRangeException(ExceptionArgument.arrayIndex, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
            }
            if ((array.Length - arrayIndex) < this.Count)
            {
                ExceptionHelper.ArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
            }
            for (int i = 0; i < this.Count; i++)
            {
                KeyValuePair<Key, Value> pair = new KeyValuePair<Key, Value>(this.keys[i], this.values[i]);
                array[arrayIndex + i] = pair;
            }
        }

        bool ICollection<KeyValuePair<Key, Value>>.Remove(KeyValuePair<Key, Value> keyValuePair)
        {
            int index = this.IndexOfKey(keyValuePair.Key);
            if ((index >= 0) && EqualityComparer<Value>.Default.Equals(this.values[index], keyValuePair.Value))
            {
                this.RemoveAt(index);
                return true;
            }
            return false;
        }

        IEnumerator<KeyValuePair<Key, Value>> IEnumerable<KeyValuePair<Key, Value>>.GetEnumerator()
        {
            return new Enumerator/*<Key, Value>*/((GenericList<Key, Value>)this, 1);
        }

        void ICollection.CopyTo(Array array, int arrayIndex)
        {
            if (array == null)
            {
                ExceptionHelper.ArgumentNullException(ExceptionArgument.array);
            }
            if (array.Rank != 1)
            {
                ExceptionHelper.ArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
            }
            if (array.GetLowerBound(0) != 0)
            {
                ExceptionHelper.ArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
            }
            if ((arrayIndex < 0) || (arrayIndex > array.Length))
            {
                ExceptionHelper.ArgumentOutOfRangeException(ExceptionArgument.arrayIndex, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
            }
            if ((array.Length - arrayIndex) < this.Count)
            {
                ExceptionHelper.ArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
            }
            KeyValuePair<Key, Value>[] pairArray = array as KeyValuePair<Key, Value>[];
            if (pairArray != null)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    pairArray[i + arrayIndex] = new KeyValuePair<Key, Value>(this.keys[i], this.values[i]);
                }
            }
            else
            {
                object[] objArray = array as object[];
                if (objArray == null)
                {
                    ExceptionHelper.ArgumentException(ExceptionResource.Argument_InvalidArrayType);
                }
                try
                {
                    for (int j = 0; j < this.Count; j++)
                    {
                        objArray[j + arrayIndex] = new KeyValuePair<Key, Value>(this.keys[j], this.values[j]);
                    }
                }
                catch (ArrayTypeMismatchException)
                {
                    ExceptionHelper.ArgumentException(ExceptionResource.Argument_InvalidArrayType);
                }
            }
        }

        void IDictionary.Add(object key, object value)
        {
            GenericList<Key, Value>.VerifyKey(key);
            GenericList<Key, Value>.VerifyValueType(value);
            this.Add((Key)key, (Value)value);
        }

        bool IDictionary.Contains(object key)
        {
            if (GenericList<Key, Value>.IsCompatibleKey(key))
            {
                return this.ContainsKey((Key)key);
            }
            return false;
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return new Enumerator/*<Key, Value>*/((GenericList<Key, Value>)this, 2);
        }

        void IDictionary.Remove(object key)
        {
            if (GenericList<Key, Value>.IsCompatibleKey(key))
            {
                this.Remove((Key)key);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator/*<Key, Value>*/((GenericList<Key, Value>)this, 1);
        }

        public void TrimExcess()
        {
            int num = (int)(this.keys.Length * 0.9);
            if (this._size < num)
            {
                this.Capacity = this._size;
            }
        }

        public bool TryGetValue(Key key, out Value value)
        {
            int index = this.IndexOfKey(key);
            if (index >= 0)
            {
                value = this.values[index];
                return true;
            }
            value = default(Value);
            return false;
        }

        private static void VerifyKey(object key)
        {
            if (key == null)
            {
                ExceptionHelper.ArgumentNullException(ExceptionArgument.key);
            }
            if (!(key is Key))
            {
                ExceptionHelper.WrongKeyTypeArgumentException(key, typeof(Key));
            }
        }

        private static void VerifyValueType(object value)
        {
            if (!(value is Value) && ((value != null) || typeof(Value).IsValueType))
            {
                ExceptionHelper.WrongValueTypeArgumentException(value, typeof(Value));
            }
        }

        // Properties
        public int Capacity
        {
            get
            {
                return this.keys.Length;
            }
            set
            {
                if (value != this.keys.Length)
                {
                    if (value < this._size)
                    {
                        ExceptionHelper.ArgumentOutOfRangeException(ExceptionArgument.value, ExceptionResource.ArgumentOutOfRange_SmallCapacity);
                    }
                    if (value > 0)
                    {
                        Key[] destinationArray = new Key[value];
                        Value[] localArray2 = new Value[value];
                        if (this._size > 0)
                        {
                            Array.Copy(this.keys, 0, destinationArray, 0, this._size);
                            Array.Copy(this.values, 0, localArray2, 0, this._size);
                        }
                        this.keys = destinationArray;
                        this.values = localArray2;
                    }
                    else
                    {
                        this.keys = GenericList<Key, Value>.emptyKeys;
                        this.values = GenericList<Key, Value>.emptyValues;
                    }
                }
            }
        }

        public IComparer<Key> Comparer
        {
            get
            {
                return this.comparer;
            }
        }

        public int Count
        {
            get
            {
                return this._size;
            }
        }

        public Value this[Key key]
        {
            get
            {
                int index = this.IndexOfKey(key);
                if (index >= 0)
                {
                    return this.values[index];
                }
                ExceptionHelper.KeyNotFoundException();
                return default(Value);
            }
            set
            {
                if (key == null)
                {
                    ExceptionHelper.ArgumentNullException(ExceptionArgument.key);
                }
                int index = Array.BinarySearch<Key>(this.keys, 0, this._size, key, this.comparer);
                if (index >= 0)
                {
                    this.values[index] = value;
                    this.version++;
                }
                else
                {
                    this.Insert(~index, key, value);
                }
            }
        }

        public IList<Key> Keys
        {
            get
            {
                return this.GetKeyListHelper();
            }
        }

        bool ICollection<KeyValuePair<Key, Value>>.IsReadOnly
        {
            get
            {
                return false;
            }
        }

        ICollection<Key> IDictionary<Key, Value>.Keys
        {
            get
            {
                return this.GetKeyListHelper();
            }
        }

        ICollection<Value> IDictionary<Key, Value>.Values
        {
            get
            {
                return this.GetValueListHelper();
            }
        }

        bool ICollection.IsSynchronized
        {
            get
            {
                return false;
            }
        }

        object ICollection.SyncRoot
        {
            get
            {
                if (this._syncRoot == null)
                {
                    Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
                }
                return this._syncRoot;
            }
        }

        bool IDictionary.IsFixedSize
        {
            get
            {
                return false;
            }
        }

        bool IDictionary.IsReadOnly
        {
            get
            {
                return false;
            }
        }

        object IDictionary.this[object key]
        {
            get
            {
                if (GenericList<Key, Value>.IsCompatibleKey(key))
                {
                    int index = this.IndexOfKey((Key)key);
                    if (index >= 0)
                    {
                        return this.values[index];
                    }
                }
                return null;
            }
            set
            {
                GenericList<Key, Value>.VerifyKey(key);
                GenericList<Key, Value>.VerifyValueType(value);
                this[(Key)key] = (Value)value;
            }
        }

        ICollection IDictionary.Keys
        {
            get
            {
                return this.GetKeyListHelper();
            }
        }

        ICollection IDictionary.Values
        {
            get
            {
                return this.GetValueListHelper();
            }
        }

        public IList<Value> Values
        {
            get
            {
                return this.GetValueListHelper();
            }
        }

        public object SyncRoot
        {
            get
            {
                if (this._syncRoot == null)
                {
                    Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
                }
                return this._syncRoot;
            }
        }



        // Nested Types
        [Serializable, StructLayout(LayoutKind.Sequential)]
        private struct Enumerator : IEnumerator<KeyValuePair<Key, Value>>, IDisposable, IDictionaryEnumerator, IEnumerator
        {
            internal const int KeyValuePair = 1;
            internal const int DictEntry = 2;
            private GenericList<Key, Value> _genericList;
            private Key key;
            private Value value;
            private int index;
            private int version;
            private int getEnumeratorRetType;
            internal Enumerator(GenericList<Key, Value> genericList, int getEnumeratorRetType)
            {
                this._genericList = genericList;
                this.index = 0;
                this.version = this._genericList.version;
                this.getEnumeratorRetType = getEnumeratorRetType;
                this.key = default(Key);
                this.value = default(Value);
            }

            public void Dispose()
            {
                this.index = 0;
                this.key = default(Key);
                this.value = default(Value);
            }

            object IDictionaryEnumerator.Key
            {
                get
                {
                    if ((this.index == 0) || (this.index == (this._genericList.Count + 1)))
                    {
                        ExceptionHelper.InvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
                    }
                    return this.key;
                }
            }
            public bool MoveNext()
            {
                if (this.version != this._genericList.version)
                {
                    ExceptionHelper.InvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
                }
                if (this.index < this._genericList.Count)
                {
                    this.key = this._genericList.keys[this.index];
                    this.value = this._genericList.values[this.index];
                    this.index++;
                    return true;
                }
                this.index = this._genericList.Count + 1;
                this.key = default(Key);
                this.value = default(Value);
                return false;
            }

            DictionaryEntry IDictionaryEnumerator.Entry
            {
                get
                {
                    if ((this.index == 0) || (this.index == (this._genericList.Count + 1)))
                    {
                        ExceptionHelper.InvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
                    }
                    return new DictionaryEntry(this.key, this.value);
                }
            }
            public KeyValuePair<Key, Value> Current
            {
                get
                {
                    return new KeyValuePair<Key, Value>(this.key, this.value);
                }
            }
            object IEnumerator.Current
            {
                get
                {
                    if ((this.index == 0) || (this.index == (this._genericList.Count + 1)))
                    {
                        ExceptionHelper.InvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
                    }
                    if (this.getEnumeratorRetType == 2)
                    {
                        return new DictionaryEntry(this.key, this.value);
                    }
                    return new KeyValuePair<Key, Value>(this.key, this.value);
                }
            }
            object IDictionaryEnumerator.Value
            {
                get
                {
                    if ((this.index == 0) || (this.index == (this._genericList.Count + 1)))
                    {
                        ExceptionHelper.InvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
                    }
                    return this.value;
                }
            }
            void IEnumerator.Reset()
            {
                if (this.version != this._genericList.version)
                {
                    ExceptionHelper.InvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
                }
                this.index = 0;
                this.key = default(Key);
                this.value = default(Value);
            }
        }

        [Serializable]//, DebuggerDisplay("Count = {Count}"), DebuggerTypeProxy(typeof(System_DictionaryKeyCollectionDebugView<,>))]
        private sealed class KeyList : IList<Key>, ICollection<Key>, IEnumerable<Key>, ICollection, IEnumerable
        {
            // Fields
            private GenericList<Key, Value> _dict;

            // Methods
            internal KeyList(GenericList<Key, Value> dictionary)
            {
                this._dict = dictionary;
            }

            public void Add(Key key)
            {
                ExceptionHelper.NotSupportedException(ExceptionResource.NotSupported_GenericListNestedWrite);
            }

            public void Clear()
            {
                ExceptionHelper.NotSupportedException(ExceptionResource.NotSupported_GenericListNestedWrite);
            }

            public bool Contains(Key key)
            {
                return this._dict.ContainsKey(key);
            }

            public void CopyTo(Key[] array, int arrayIndex)
            {
                Array.Copy(this._dict.keys, 0, array, arrayIndex, this._dict.Count);
            }

            public IEnumerator<Key> GetEnumerator()
            {
                return new GenericList<Key, Value>.GenericListKeyEnumerator(this._dict);
            }

            public int IndexOf(Key key)
            {
                if (key == null)
                {
                    ExceptionHelper.ArgumentNullException(ExceptionArgument.key);
                }
                int num = Array.BinarySearch<Key>(this._dict.keys, 0, this._dict.Count, key, this._dict.comparer);
                if (num >= 0)
                {
                    return num;
                }
                return -1;
            }

            public void Insert(int index, Key value)
            {
                ExceptionHelper.NotSupportedException(ExceptionResource.NotSupported_GenericListNestedWrite);
            }

            public bool Remove(Key key)
            {
                ExceptionHelper.NotSupportedException(ExceptionResource.NotSupported_GenericListNestedWrite);
                return false;
            }

            public void RemoveAt(int index)
            {
                ExceptionHelper.NotSupportedException(ExceptionResource.NotSupported_GenericListNestedWrite);
            }

            void ICollection.CopyTo(Array array, int arrayIndex)
            {
                if ((array != null) && (array.Rank != 1))
                {
                    ExceptionHelper.ArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
                }
                try
                {
                    Array.Copy(this._dict.keys, 0, array, arrayIndex, this._dict.Count);
                }
                catch (ArrayTypeMismatchException)
                {
                    ExceptionHelper.ArgumentException(ExceptionResource.Argument_InvalidArrayType);
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return new GenericList<Key, Value>.GenericListKeyEnumerator(this._dict);
            }

            // Properties
            public int Count
            {
                get
                {
                    return this._dict._size;
                }
            }

            public bool IsReadOnly
            {
                get
                {
                    return true;
                }
            }

            public Key this[int index]
            {
                get
                {
                    return this._dict.GetKey(index);
                }
                set
                {
                    ExceptionHelper.NotSupportedException(ExceptionResource.NotSupported_KeyCollectionSet);
                }
            }

            bool ICollection.IsSynchronized
            {
                get
                {
                    return false;
                }
            }

            object ICollection.SyncRoot
            {
                get
                {
                    return ((ICollection)this._dict).SyncRoot;
                }
            }
        }

        [Serializable]
        private sealed class GenericListKeyEnumerator : IEnumerator<Key>, IDisposable, IEnumerator
        {
            // Fields
            private GenericList<Key, Value> _genericList;
            private Key currentKey;
            private int index;
            private int version;

            // Methods
            internal GenericListKeyEnumerator(GenericList<Key, Value> genericList)
            {
                this._genericList = genericList;
                this.version = genericList.version;
            }

            public void Dispose()
            {
                this.index = 0;
                this.currentKey = default(Key);
            }

            public bool MoveNext()
            {
                if (this.version != this._genericList.version)
                {
                    ExceptionHelper.InvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
                }
                if (this.index < this._genericList.Count)
                {
                    this.currentKey = this._genericList.keys[this.index];
                    this.index++;
                    return true;
                }
                this.index = this._genericList.Count + 1;
                this.currentKey = default(Key);
                return false;
            }

            void IEnumerator.Reset()
            {
                if (this.version != this._genericList.version)
                {
                    ExceptionHelper.InvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
                }
                this.index = 0;
                this.currentKey = default(Key);
            }

            // Properties
            public Key Current
            {
                get
                {
                    return this.currentKey;
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    if ((this.index == 0) || (this.index == (this._genericList.Count + 1)))
                    {
                        ExceptionHelper.InvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
                    }
                    return this.currentKey;
                }
            }
        }

        [Serializable]
        private sealed class GenericListValueEnumerator : IEnumerator<Value>, IDisposable, IEnumerator
        {
            // Fields
            private GenericList<Key, Value> _genericList;
            private Value currentValue;
            private int index;
            private int version;

            // Methods
            internal GenericListValueEnumerator(GenericList<Key, Value> genericList)
            {
                this._genericList = genericList;
                this.version = genericList.version;
            }

            public void Dispose()
            {
                this.index = 0;
                this.currentValue = default(Value);
            }

            public bool MoveNext()
            {
                if (this.version != this._genericList.version)
                {
                    ExceptionHelper.InvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
                }
                if (this.index < this._genericList.Count)
                {
                    this.currentValue = this._genericList.values[this.index];
                    this.index++;
                    return true;
                }
                this.index = this._genericList.Count + 1;
                this.currentValue = default(Value);
                return false;
            }

            void IEnumerator.Reset()
            {
                if (this.version != this._genericList.version)
                {
                    ExceptionHelper.InvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
                }
                this.index = 0;
                this.currentValue = default(Value);
            }

            // Properties
            public Value Current
            {
                get
                {
                    return this.currentValue;
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    if ((this.index == 0) || (this.index == (this._genericList.Count + 1)))
                    {
                        ExceptionHelper.InvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
                    }
                    return this.currentValue;
                }
            }
        }

        [Serializable]//, DebuggerDisplay("Count = {Count}"), DebuggerTypeProxy(typeof(System_DictionaryValueCollectionDebugView<,>))]
        private sealed class ValueList : IList<Value>, ICollection<Value>, IEnumerable<Value>, ICollection, IEnumerable
        {
            // Fields
            private GenericList<Key, Value> _dict;

            // Methods
            internal ValueList(GenericList<Key, Value> dictionary)
            {
                this._dict = dictionary;
            }

            public void Add(Value key)
            {
                ExceptionHelper.NotSupportedException(ExceptionResource.NotSupported_GenericListNestedWrite);
            }

            public void Clear()
            {
                ExceptionHelper.NotSupportedException(ExceptionResource.NotSupported_GenericListNestedWrite);
            }

            public bool Contains(Value value)
            {
                return this._dict.ContainsValue(value);
            }

            public void CopyTo(Value[] array, int arrayIndex)
            {
                Array.Copy(this._dict.values, 0, array, arrayIndex, this._dict.Count);
            }

            public IEnumerator<Value> GetEnumerator()
            {
                return new GenericList<Key, Value>.GenericListValueEnumerator(this._dict);
            }

            public int IndexOf(Value value)
            {
                return Array.IndexOf<Value>(this._dict.values, value, 0, this._dict.Count);
            }

            public void Insert(int index, Value value)
            {
                ExceptionHelper.NotSupportedException(ExceptionResource.NotSupported_GenericListNestedWrite);
            }

            public bool Remove(Value value)
            {
                ExceptionHelper.NotSupportedException(ExceptionResource.NotSupported_GenericListNestedWrite);
                return false;
            }

            public void RemoveAt(int index)
            {
                ExceptionHelper.NotSupportedException(ExceptionResource.NotSupported_GenericListNestedWrite);
            }

            void ICollection.CopyTo(Array array, int arrayIndex)
            {
                if ((array != null) && (array.Rank != 1))
                {
                    ExceptionHelper.ArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
                }
                try
                {
                    Array.Copy(this._dict.values, 0, array, arrayIndex, this._dict.Count);
                }
                catch (ArrayTypeMismatchException)
                {
                    ExceptionHelper.ArgumentException(ExceptionResource.Argument_InvalidArrayType);
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return new GenericList<Key, Value>.GenericListValueEnumerator(this._dict);
            }

            // Properties
            public int Count
            {
                get
                {
                    return this._dict._size;
                }
            }

            public bool IsReadOnly
            {
                get
                {
                    return true;
                }
            }

            public Value this[int index]
            {
                get
                {
                    return this._dict.GetByIndex(index);
                }
                set
                {
                    ExceptionHelper.NotSupportedException(ExceptionResource.NotSupported_GenericListNestedWrite);
                }
            }

            bool ICollection.IsSynchronized
            {
                get
                {
                    return false;
                }
            }

            object ICollection.SyncRoot
            {
                get
                {
                    return ((ICollection)this._dict).SyncRoot;
                }
            }
        }
    }

}