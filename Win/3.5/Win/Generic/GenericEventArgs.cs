using System;
using System.Collections.Generic;
using System.Text;

namespace MControl.Generic
{
    #region GenericEventArgs

    public delegate void GenericEventHandler<T>(object sender, GenericEventArgs<T> e);
    public delegate void GenericEventHandler<T1,T2>(object sender, GenericEventArgs<T1, T2> e);
    public delegate void GenericEventHandler<T1, T2,T3>(object sender, GenericEventArgs<T1, T2,T3> e);

    public class GenericEventArgs<T> : EventArgs
    {
        public readonly T Args;
        public readonly int State;

        public GenericEventArgs(T arg)
        {
            Args = arg;
        }
        public GenericEventArgs(T arg, int state)
        {
            Args = arg;
            State = state;
        }
    }

    public class GenericEventArgs<T1,T2>:EventArgs
    {
        public readonly T1 Args1;
        public readonly T2 Args2;
        
        public GenericEventArgs(T1 arg)
        {
            Args1 = arg;
        }
        public GenericEventArgs(T1 arg1, T2 arg2)
        {
            Args1 = arg1;
            Args2 = arg2;
        }

    }
    
    public class GenericEventArgs<T1, T2,T3> : EventArgs
    {
        public readonly T1 Args1;
        public readonly T2 Args2;
        public readonly T3 Args3;

        public GenericEventArgs(T1 arg)
        {
            Args1 = arg;
        }
        public GenericEventArgs(T1 arg1, T2 arg2)
        {
            Args1 = arg1;
            Args2 = arg2;
        }
        public GenericEventArgs(T1 arg1, T2 arg2, T3 arg3)
        {
            Args1 = arg1;
            Args2 = arg2;
            Args3 = arg3;
        }
    }
    #endregion

 }
