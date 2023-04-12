using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Runtime.InteropServices;
using MControl.Win;

namespace MControl//.Data
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Range
    {
        private int min;
        private int max;
        private bool isNotNull;
        public Range(int min, int max)
        {
            if (min > max)
            {
                throw ExceptionHelper.RangeArgument(min, max);
            }
            this.min = min;
            this.max = max;
            this.isNotNull = true;
        }

        public int Count
        {
            get
            {
                if (this.IsNull)
                {
                    return 0;
                }
                return ((this.max - this.min) + 1);
            }
        }
        public bool IsNull
        {
            get
            {
                return !this.isNotNull;
            }
        }
        public int Max
        {
            get
            {
                this.CheckNull();
                return this.max;
            }
        }
        public int Min
        {
            get
            {
                this.CheckNull();
                return this.min;
            }
        }
        internal void CheckNull()
        {
            if (this.IsNull)
            {
                throw ExceptionHelper.NullRange();
            }
        }
    }

}
