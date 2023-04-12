using System;
using System.Collections;
using System.Threading;
using System.Collections.Generic;
using System.Data;

using System.Runtime.Remoting;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;

namespace MControl.Collections
{

    /// <summary>
    /// ComparePredicate
    /// </summary>
    public enum ComparePredicate
    {
        /// <summary>
        /// Exact
        /// </summary>
        Exact=0,
        /// <summary>
        /// Start
        /// </summary>
        Start=1,
        /// <summary>
        /// Last
        /// </summary>
        Last=2,
        /// <summary>
        /// In
        /// </summary>
        In=3
    }

        #region LookupList

        public class LookupList
        {
            List<string> list;
            bool alowDuplicate=true;
            private int selectedIndex = -1;
            private bool sorted=false;
            private IComparer<string> comparer;

            /// <summary>
            /// LookupList ctor
            /// </summary>
            public LookupList()
            {
                list = new List<string>();
                this.comparer = Comparer<string>.Default;

            }
            ///destructor
            ~LookupList()
            {
                list.Clear();
            }

            /// <summary>
            /// Get or Set SelectedIndex
            /// </summary>
            public int SelectedIndex
            {
                get { return selectedIndex; }
                set { selectedIndex = value; }
            }

            /// <summary>
            /// Get value indicating the list is sorted.
            /// /// </summary>
            public bool Sorted
            {
                get { return sorted; }
                set { sorted = value; }
            }

            /// <summary>
            /// Get or Set value indication list allow duplicate items
            /// </summary>
            public bool AlowDuplicate
            {
                get { return alowDuplicate; }
                set { alowDuplicate = value; }
            }

            /// <summary>
            /// Get value indicating the list is AddCompleted.
            /// /// </summary>
            public bool AddCompleted
            {
                get { return addCompleted; }
            }

            //private int Insert(int index,string text)
            //{
            //    int index = Array.BinarySearch<string>(this.array, index,list.Count, text, this.comparer);
            //    if (index >= 0)
            //    {
            //        if (!alowDuplicate)
            //        {
            //            ExceptionHelper.ArgumentException(ExceptionResource.Argument_AddingDuplicate);
            //            return -1;
            //        }
            //    }
            //    //list.Insert(~index, text);

            //    if (this._size == this.array.Length)
            //    {
            //        this.EnsureCapacity(this._size + 1);
            //    }
            //    if (index < this._size)
            //    {
            //        Array.Copy(this.keys, index, this.keys, index + 1, this._size - index);
            //    }
            //    this.array[index] = key;
            //    this._size++;
            //}


            /// <summary>
            ///add an item to the end of the list
            ///returns the number of items in the list
            /// </summary>
            /// <param name="s"></param>
            /// <returns></returns>
            public int Add(string s)
            {
                if (!alowDuplicate)
                {
                    if (list.Contains(s))
                        return -1;
                }
                list.Add(s);
                if (sorted)
                    this.AsyncSort();
                return list.Count;
            }
            /// <summary>
            /// AddRange
            /// </summary>
            /// <param name="sl"></param>
            public void AddRange(LookupList sl)
            {
                list.AddRange(sl.list);
                if (sorted)
                    this.AsyncSort();
            }

            /// <summary>
            /// AddRange from DataView by DisplayMember column
            /// </summary>
            /// <param name="dataSource"></param>
            /// <param name="displayMember"></param>
            /// <param name="async">Use asynchronize</param>
            public void AddRange(DataView dataSource, string displayMember, bool async)
            {
                if (dataSource == null || string.IsNullOrEmpty(displayMember))
                    return;
                if (async)
                {
                    DisplayMember = displayMember;
                    Thread th = new Thread(new ParameterizedThreadStart(AsyncAddWorker));// (new ThreadStart(AsyncAddWorker));
                    th.Start(dataSource);
                    Thread.Sleep(0);
                }
                else
                {
                    addCompleted = false;
                    bool flag = sorted;
                    sorted = false;
                    foreach (DataRowView drv in dataSource)
                    {
                        string sTemp = drv[displayMember].ToString();
                        Add(sTemp);
                    }
                    if (flag)
                    {
                        sorted = true;
                        this.AsyncSort();
                    }
                    addCompleted = true;
                }
            }

 
            bool addCompleted=true;
            string DisplayMember;

            private void AsyncAddWorker(object dataSource)
            {
                DataView DataSource = (DataView)dataSource;
                addCompleted = false;
                bool flag = sorted;
                sorted = false;
                foreach (DataRowView drv in DataSource)
                {
                    string sTemp = drv[DisplayMember].ToString();
                    Add(sTemp);
                }
                if (flag)
                {
                    sorted = true;
                    this.AsyncSort();
                }
                addCompleted = true;
            }

            /// <summary>
            ///insert a string into the list only if the same string hasn't been added yet
            /// </summary>
            /// <param name="s"></param>
            /// <returns></returns>
            public int AddNoDuplicate(string s)
            {
                if (list.IndexOf(s) == -1)
                {
                    list.Add(s);
                    if (sorted)
                        this.AsyncSort();
                }
                return list.Count;
            }

            /// <summary>
            ///insert an item at the desired position
            ///will throw an error if index is out of range
            /// </summary>
            /// <param name="index"></param>
            /// <param name="s"></param>
            public void Insert(int index, string s)
            {
                if (!alowDuplicate)
                {
                    if (list.Contains(s))
                        return ;
                }
                list.Insert(index, s);
                if (sorted)
                    this.AsyncSort();
            }
            
            /// <summary>
            ///remove an item from the list
            /// </summary>
            /// <param name="s"></param>
            public void Remove(String s)
            {
                list.Remove(s);
            }

            /// <summary>
            ///remove an item from the list by index
            /// </summary>
            /// <param name="index"></param>
            public void RemoveAt(int index)
            {
                list.RemoveAt(index);
            }

            /// <summary>
            /// Replace
            /// </summary>
            /// <param name="sFind">text to find</param>
            /// <param name="sReplace">text to replace</param>
            /// <returns></returns>
            public int Replace(string sFind, string sReplace)
            {
                int index = list.IndexOf(sFind);
                if (index > -1)
                {
                    if (!alowDuplicate)
                    {
                        if (list.Contains(sReplace))
                            return list.Count;
                    }

                    list.RemoveAt(index);
                    list.Insert(index, sReplace);
                    if (sorted)
                        this.AsyncSort();
                }
                return list.Count;
            }

            /// <summary>
            ///remove all items from the list
            /// </summary>
            public void Clear()
            {
                list.Clear();
            }
 
            /// <summary>
            /// String
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            public String this[int index]
            {
                get
                {
                    return (string)list[index];
                }
                set
                {
                    if (index >= list.Count)
                        Add(value);
                    else
                        list[index] = value;
                }
            }

 
            /// <summary>
            ///ToString() override
            ///Return the contents of the list with the items seperated by a new line
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                string sRHS = "";
                int index = 0;
                while (index < list.Count)
                {
                    sRHS += (string)list[index++] + "\n";
                }
                return sRHS;
            }

            /// <summary>
            ///return the contents of the list seperated by the given seperator string
            /// </summary>
            /// <param name="sSeperator"></param>
            /// <returns></returns>
            public String ToString(string sSeperator)
            {
                string sRHS = "";
                int index = 0;
                while (index < list.Count)
                {
                    sRHS += (string)list[index++];
                    if (index < list.Count)
                        sRHS += sSeperator;
                }
                return sRHS;
            }

             /// <summary>
            ///Sort the string in the array
            /// </summary>
            public void Sort()
            {
                list.Sort();
            }

            /// <summary>
            ///Sort the string in the array asynchronize
            /// </summary>
            public void AsyncSort()
            {
                Thread th = new Thread(new ThreadStart(SortWorker));
                th.Start();
                Thread.Sleep(0);
            }

            private void SortWorker()
            {
                list.Sort();
            }

            /// <summary>
            ///Count
            //returns the number of items in the list
            /// </summary>
            public int Count
            {
                get
                {
                    return list.Count;
                }
            }

            /// <summary>
            /// Finds the index of a given string.
            /// </summary>
            /// <param name="text">String to find the index of.</param>
            /// <returns>The index of text or -1 if it doesn't exist.</returns>
            public int IndexOf(string text)
            {
                return list.IndexOf(text);
            }

            #region Find

            string strFind;

            // Search predicate returns true if a string  in item.
            private bool CompareStart(string item)
            {
                if (strFind.Length > item.Length)
                    return false;
                if (item.Substring(0, strFind.Length).ToLower() == strFind)
                    return true;
                else
                    return false;
            }
            // Search predicate returns true if a string  in item.
            private bool CompareLast(string item)
            {
                if (strFind.Length > item.Length)
                    return false;
                if (item.Substring(item.Length - strFind.Length).ToLower() == strFind)
                    return true;
                else
                    return false;
            }
            // Search predicate returns true if a string  in item.
            private bool CompareIn(string item)
            {
                if (strFind.Length > item.Length)
                    return false;
                if (item.ToLower().Contains(strFind))
                    return true;
                else
                    return false;
            }
            // Search predicate returns true if a string equals item.
            private bool CompareExact(string item)
            {
                if (strFind.Length > item.Length)
                    return false;
                if (item.ToLower().Equals(strFind))
                    return true;
                else
                    return false;
            }

            /// <summary>
            /// Search for am element that matches the conditions defined by the specifed predicate and return the first occurence within the Lookup list.  
            /// </summary>
            /// <param name="text"></param>
            /// <param name="compare"></param>
            /// <returns></returns>
            public string FindFirst(string text,ComparePredicate predicate)
            {
                strFind = text.ToLower();

                switch (predicate)
                {
                    case   ComparePredicate.Start:
                      return   list.Find(CompareStart);
                    case ComparePredicate.Last:
                        return list.Find(CompareLast);
                    case ComparePredicate.In:
                        return list.Find(CompareIn);
                    default:
                        return list.Find(CompareExact);
                }
            }

            /// <summary>
            /// Search for am element that matches the conditions defined by the specifed predicate and return the last occurence within the Lookup list.  
            /// </summary>
            /// <param name="text"></param>
            /// <param name="predicate"></param>
            /// <returns></returns>
            public string FindLast(string text, ComparePredicate predicate)
            {
                strFind = text.ToLower();

                switch (predicate)
                {
                    case ComparePredicate.Start:
                        return list.FindLast(CompareStart);
                    case ComparePredicate.Last:
                        return list.FindLast(CompareLast);
                    case ComparePredicate.In:
                        return list.FindLast(CompareIn);
                    default:
                        return list.FindLast(CompareExact);
                }
            }

            /// <summary>
            /// Search for am element that matches the conditions defined by the specifed predicate and return all the elements occurence within the Lookup list.  
            /// </summary>
            /// <param name="text"></param>
            /// <param name="predicate"></param>
            /// <returns></returns>
            public List<string> FindAll(string text, ComparePredicate predicate)
            {
                strFind = text.ToLower();

                switch (predicate)
                {
                    case ComparePredicate.Start:
                        return list.FindAll(CompareStart);
                    case ComparePredicate.Last:
                        return list.FindAll(CompareLast);
                    case ComparePredicate.In:
                        return list.FindAll(CompareIn);
                    default:
                        return list.FindAll(CompareExact);
                }
            }

            /// <summary>
            /// Search for am element that matches the conditions defined by the specifed predicate and return the first index of  occurence within the Lookup list.  
            /// </summary>
            /// <param name="text"></param>
            /// <param name="predicate"></param>
            /// <returns></returns>
            public int FindIndex(int startIndex, string text, ComparePredicate predicate)
            {
                strFind = text.ToLower();

                switch (predicate)
                {
                    case ComparePredicate.Start:
                        return list.FindIndex(startIndex,CompareStart);
                    case ComparePredicate.Last:
                        return list.FindIndex(startIndex, CompareLast);
                    case ComparePredicate.In:
                        return list.FindIndex(startIndex, CompareIn);
                    default:
                        return list.FindIndex(startIndex, CompareExact);
                }
            }


            /// <summary>
            /// Search for am element that matches the conditions defined by the specifed predicate and return the last index of  occurence within the Lookup list.  
            /// </summary>
            /// <param name="text"></param>
            /// <param name="predicate"></param>
            /// <returns></returns>
            public int FindLastIndex(int startIndex, string text, ComparePredicate predicate)
            {
                strFind = text.ToLower();

                switch (predicate)
                {
                    case ComparePredicate.Start:
                        return list.FindLastIndex(startIndex, CompareStart);
                    case ComparePredicate.Last:
                        return list.FindLastIndex(startIndex, CompareLast);
                    case ComparePredicate.In:
                        return list.FindLastIndex(startIndex, CompareIn);
                    default:
                        return list.FindLastIndex(startIndex, CompareExact);
                }
            }

            /// <summary>
            /// Finds the first item of a given string start within.
            /// </summary>
            /// <param name="text">String to find the index of.</param>
            /// <returns>The items index that text start within.</returns>
            public int Find(string text)
            {
                return FindIndex(0, text, ComparePredicate.Start);
                //return Find(text, 0);
            }

            /// <summary>
            /// Finds the first item of a given string start within.
            /// </summary>
            /// <param name="text">String to find the index of.</param>
            /// <param name="start">the index to start from</param>
            /// <returns>The items index that text start within.</returns>
            public int Find(string text, int start)
            {
                if (start >= Count || start <0)
                    start = 0;

                return FindIndex(start, text, ComparePredicate.Start);

                //string temp = text.ToLower();
                //int length=temp.Length;

                

                //string cur="";

                //if (sorted)
                //{
                //    int num = Array.BinarySearch<string>(list.ToArray(), start, this.Count, text, this.comparer);
                //}

                //for (int i = start; i < list.Count; i++)
                //{
                //    cur=list[i].ToLower();

                //    if (cur.Length < length)
                //        continue;
                //    if (cur.Substring(0,length).Equals(temp))
                //    {
                //        return i;
                //    }
                //}
                //return -1;
            }

            /// <summary>
            /// Finds the next item of a given string start within form SelectedIndex.
            /// </summary>
            /// <param name="text"></param>
            /// <returns></returns>
            public int FindNext(string text)
            {
                this.selectedIndex = Find(text, selectedIndex + 1);
                return selectedIndex;
            }


            ///// <summary>
            ///// Finds the items of a given string occures within.
            ///// </summary>
            ///// <param name="text">String to find the index of.</param>
            ///// <returns>The array of items index that text occures within</returns>
            //public int[] FindLike(string text)
            //{
            //    string temp = text.ToLower();
            //    List<int> result = new List<int>();

            //    for (int i = 0; i < list.Count; i++)
            //    {
            //        if (list[i].ToLower().Contains(temp))
            //        {
            //            result.Add(i);
            //        }
            //    }
            //    return result.ToArray();
            //}

            #endregion

            /// <summary>
            ///conversion operators
            ///convert a LookupList to a string[]
            /// </summary>
            /// <param name="sl"></param>
            /// <returns></returns>
            public static implicit operator string[](LookupList sl)
            {
                string[] sLHS = new string[sl.list.Count];
                int index = 0;
                while (index < sl.list.Count)
                {
                    sLHS[index] = (string)sl.list[index];
                    index++;
                }
                return sLHS;
            }

            /// <summary>
            ///convert a string[] to a LookupList
            /// </summary>
            /// <param name="sa"></param>
            /// <returns></returns>
            public static implicit operator LookupList(string[] sa)
            {
                LookupList sl = new LookupList();
                for (int index = 0; index < sa.Length; index++)
                {
                    sl.Add(sa[index]);
                }
                return sl;
            }

            /// <summary>
            ///convert a object[] to a LookupList
            /// </summary>
            /// <param name="sa"></param>
            /// <returns></returns>
            public static implicit operator LookupList(object[] sa)
            {
                LookupList sl = new LookupList();
                for (int index = 0; index < sa.Length; index++)
                {
                    sl.Add(sa[index].ToString());
                }
                return sl;
            }
        }
        #endregion

        #region LookupThreadSort

    /// <summary>
        /// LookupThreadSort
    /// </summary>
        public  class LookupThreadSort
        {
            /// <summary>
            /// Start sort
            /// </summary>
            /// <param name="dv"></param>
            /// <param name="col"></param>
            public static void Start(System.Data.DataView dv, string col)
            {
                m_dv = dv;
                m_col = col;

                ThreadStart myThreadDelegate = new ThreadStart(ThreadWork.DoWork);
                Thread myThread = new Thread(myThreadDelegate);
                myThread.Start();
                Thread.Sleep(0);
                Console.WriteLine("In main. Attempting to restart myThread.");
                try
                {
                    myThread.Start();
                }
                catch (ThreadStateException e)
                {
                    Console.WriteLine("Caught: {0}", e.Message);
                }
            }

            internal static System.Data.DataView m_dv;
            internal static string m_col;

            internal class ThreadWork
            {

                internal static void DoWork()
                {
                    //Thread.Sleep(0);
                    Console.WriteLine("Working thread...");

                    lock (LookupThreadSort.m_dv)
                    {
                        LookupThreadSort.m_dv.Sort = LookupThreadSort.m_col;
                    }
                }
            }

        }


        internal class LookupThreadSort1
        {
            private delegate void RunProcDelegate(string text);
            private RunProcDelegate m_runProc;
            private Thread m_Thread;

            private bool Finished = false;
            private bool Started = false;
            internal System.Data.DataView m_dv;
            internal string m_columnSort = "";
            private System.Windows.Forms.Control ctl;

            internal LookupThreadSort1(System.Windows.Forms.Control c, System.Data.DataView dv, string col)
            {
                ctl = c;
                m_dv = dv;
                m_columnSort = col;
            }

            internal void Start()
            {
                Thread.CurrentThread.Name = "Thread Principal";
                m_runProc = new RunProcDelegate(this.onRnuProc);

                m_Thread = new Thread(new ThreadStart(this.ThreadProc));
                m_Thread.Name = "Thread Proc";
                m_Thread.Start();
            }

            private void ThreadProc()
            {
                object[] args = new object[1];
                while (!Finished)
                {
                    Thread.Sleep(10);
                    ctl.Invoke(m_runProc, args);
                    Thread.Sleep(100);
                }
                //ctl.Invoke(m_runProc,args);
            }

            private void onRnuProc(string text)
            {
                Finished = SetDataView();
            }

            private bool SetDataView()
            {
                if (!Started)
                {
                    Started = true;
                    m_dv.Sort = m_columnSort;
                    return true;
                }
                return false;
            }
        }
        #endregion

        #region AsyncSort


        /// <summary>
        /// SortSyncronized
    /// </summary>
        [Synchronization()]
        public class SortSyncronized : ContextBoundObject
        {
            // A method that does some work - returns the square of the given number
            public string Sort(System.Data.DataView dv, string colView)
            {
                Console.Write("Syncronized.Sort called.  ");
                Console.WriteLine("The hash of the current thread is: {0}", Thread.CurrentThread.GetHashCode());
                return dv.Sort = colView;
            }
        }

        //
        // Async delegate used to call a method with this signature asynchronously
        //
        public delegate string SortSyncDelegate(System.Data.DataView dv, string colView);

    /// <summary>
        /// AsyncSort
    /// </summary>
        public class AsyncSort
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="dv"></param>
            /// <param name="colView"></param>
            public static void Start(System.Data.DataView dv, string colView)
            {
                string callResult = "";
                //Create an instance of a context-bound type SampleSynchronized
                //Because SampleSynchronized is context-bound, the object sampSyncObj 
                //is a transparent proxy
                SortSyncronized sortSyncObj = new SortSyncronized();


                ////call the method synchronously
                //Console.Write("Making a synchronous call on the object.  ");
                //Console.WriteLine("The hash of the current thread is: {0}", Thread.CurrentThread.GetHashCode());
                //callResult = sortSyncObj.Sort(dv, colView);
                ////Console.WriteLine("Result of calling sampSyncObj.Square with {0} is {1}.\n\n", callParameter, callResult);


                //call the method asynchronously
                Console.Write("Making an asynchronous call on the object.  ");
                Console.WriteLine("The hash of the current thread is: {0}", Thread.CurrentThread.GetHashCode());
                SortSyncDelegate sortDelegate = new SortSyncDelegate(sortSyncObj.Sort);
                //callParameter = 17;

                IAsyncResult aResult = sortDelegate.BeginInvoke(dv, colView, null, null);

                //Wait for the call to complete
                aResult.AsyncWaitHandle.WaitOne();

                callResult = sortDelegate.EndInvoke(aResult);
                //Console.WriteLine("Result of calling sampSyncObj.Square with {0} is {1}.", callParameter, callResult);
            }

        }

        #endregion

    }

