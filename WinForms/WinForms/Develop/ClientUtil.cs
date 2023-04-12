using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace mControl.Util
{
    public static class ClientUtils
    {
        // Methods
        public static int GetBitCount(uint x)
        {
            int num = 0;
            while (x > 0)
            {
                x &= x - 1;
                num++;
            }
            return num;
        }

        public static bool IsCriticalException(Exception ex)
        {
            return (((((ex is NullReferenceException) || (ex is StackOverflowException)) || ((ex is OutOfMemoryException) || (ex is System.Threading.ThreadAbortException))) || ((ex is ExecutionEngineException) || (ex is IndexOutOfRangeException))) || (ex is AccessViolationException));
        }

        public static bool IsEnumValid(Enum enumValue, int value, int minValue, int maxValue)
        {
            return ((value >= minValue) && (value <= maxValue));
        }

        public static bool IsEnumValid(Enum enumValue, int value, int minValue, int maxValue, int maxNumberOfBitsOn)
        {
            return (((value >= minValue) && (value <= maxValue)) && (GetBitCount((uint)value) <= maxNumberOfBitsOn));
        }

        public static bool IsEnumValid_Masked(Enum enumValue, int value, uint mask)
        {
            return ((value & mask) == value);
        }

        public static bool IsEnumValid_NotSequential(Enum enumValue, int value, params int[] enumValues)
        {
            for (int i = 0; i < enumValues.Length; i++)
            {
                if (enumValues[i] == value)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsSecurityOrCriticalException(Exception ex)
        {
            return ((ex is System.Security.SecurityException) || IsCriticalException(ex));
        }

        // Nested Types
        internal class WeakRefCollection : IList, ICollection, IEnumerable
        {
            // Fields
            private ArrayList _innerList;

            // Methods
            internal WeakRefCollection()
            {
                this._innerList = new ArrayList(4);
            }

            internal WeakRefCollection(int size)
            {
                this._innerList = new ArrayList(size);
            }

            public int Add(object value)
            {
                return this.InnerList.Add(this.CreateWeakRefObject(value));
            }

            public void Clear()
            {
                this.InnerList.Clear();
            }

            public bool Contains(object value)
            {
                return this.InnerList.Contains(this.CreateWeakRefObject(value));
            }

            private static void Copy(ClientUtils.WeakRefCollection sourceList, int sourceIndex, ClientUtils.WeakRefCollection destinationList, int destinationIndex, int length)
            {
                if (sourceIndex < destinationIndex)
                {
                    sourceIndex += length;
                    destinationIndex += length;
                    while (length > 0)
                    {
                        destinationList.InnerList[--destinationIndex] = sourceList.InnerList[--sourceIndex];
                        length--;
                    }
                }
                else
                {
                    while (length > 0)
                    {
                        destinationList.InnerList[destinationIndex++] = sourceList.InnerList[sourceIndex++];
                        length--;
                    }
                }
            }

            public void CopyTo(Array array, int index)
            {
                this.InnerList.CopyTo(array, index);
            }

            private WeakRefObject CreateWeakRefObject(object value)
            {
                if (value == null)
                {
                    return null;
                }
                return new WeakRefObject(value);
            }

            public override bool Equals(object obj)
            {
                ClientUtils.WeakRefCollection refs = obj as ClientUtils.WeakRefCollection;
                if ((refs == null) || (this.Count != refs.Count))
                {
                    return false;
                }
                for (int i = 0; i < this.Count; i++)
                {
                    if (this.InnerList[i] != refs.InnerList[i])
                    {
                        return false;
                    }
                }
                return true;
            }

            public IEnumerator GetEnumerator()
            {
                return this.InnerList.GetEnumerator();
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            public int IndexOf(object value)
            {
                return this.InnerList.IndexOf(this.CreateWeakRefObject(value));
            }

            public void Insert(int index, object value)
            {
                this.InnerList.Insert(index, this.CreateWeakRefObject(value));
            }

            public void Remove(object value)
            {
                this.InnerList.Remove(this.CreateWeakRefObject(value));
            }

            public void RemoveAt(int index)
            {
                this.InnerList.RemoveAt(index);
            }

            public void ScavengeReferences()
            {
                int index = 0;
                int count = this.Count;
                for (int i = 0; i < count; i++)
                {
                    if (this[index] == null)
                    {
                        this.InnerList.RemoveAt(index);
                    }
                    else
                    {
                        index++;
                    }
                }
            }

            // Properties
            public int Count
            {
                get
                {
                    return this.InnerList.Count;
                }
            }

            internal ArrayList InnerList
            {
                get
                {
                    return this._innerList;
                }
            }

            public bool IsFixedSize
            {
                get
                {
                    return this.InnerList.IsFixedSize;
                }
            }

            public bool IsReadOnly
            {
                get
                {
                    return this.InnerList.IsReadOnly;
                }
            }

            public object this[int index]
            {
                get
                {
                    WeakRefObject obj2 = this.InnerList[index] as WeakRefObject;
                    if ((obj2 != null) && obj2.IsAlive)
                    {
                        return obj2.Target;
                    }
                    return null;
                }
                set
                {
                    this.InnerList[index] = this.CreateWeakRefObject(value);
                }
            }

            bool ICollection.IsSynchronized
            {
                get
                {
                    return this.InnerList.IsSynchronized;
                }
            }

            object ICollection.SyncRoot
            {
                get
                {
                    return this.InnerList.SyncRoot;
                }
            }

            // Nested Types
            internal class WeakRefObject
            {
                // Fields
                private int hash;
                private object strongHolder;
                private WeakReference weakHolder;

                // Methods
                internal WeakRefObject(object obj)
                    : this(obj, true)
                {
                }

                internal WeakRefObject(object obj, bool weakRef)
                {
                    if (obj != null)
                    {
                        this.hash = obj.GetHashCode();
                        if (weakRef)
                        {
                            this.weakHolder = new WeakReference(obj);
                            this.strongHolder = null;
                        }
                        else
                        {
                            this.strongHolder = obj;
                            this.weakHolder = null;
                        }
                    }
                    else
                    {
                        this.weakHolder = null;
                        this.strongHolder = null;
                        this.hash = 0;
                    }
                }

                public override bool Equals(object obj)
                {
                    if (obj == this)
                    {
                        return true;
                    }
                    ClientUtils.WeakRefCollection.WeakRefObject obj2 = obj as ClientUtils.WeakRefCollection.WeakRefObject;
                    return (((obj2 != null) && (this.Target == obj2.Target)) && (this.Target != null));
                }

                public override int GetHashCode()
                {
                    return this.hash;
                }

                // Properties
                internal bool IsAlive
                {
                    get
                    {
                        if (this.IsWeakReference)
                        {
                            return this.weakHolder.IsAlive;
                        }
                        return true;
                    }
                }

                internal bool IsWeakReference
                {
                    get
                    {
                        return (this.strongHolder == null);
                    }
                }

                internal object Target
                {
                    get
                    {
                        if (!this.IsWeakReference)
                        {
                            return this.strongHolder;
                        }
                        if (this.weakHolder != null)
                        {
                            return this.weakHolder.Target;
                        }
                        return null;
                    }
                }
            }
        }
    }

}
