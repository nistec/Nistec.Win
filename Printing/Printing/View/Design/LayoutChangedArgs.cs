namespace MControl.Printing.View.Design
{
    using System;

    public class LayoutChangedArgs : EventArgs
    {
        private LayoutChangedType _EventType;

        public LayoutChangedArgs(LayoutChangedType type)
        {
            this._EventType = type;
        }

        public LayoutChangedType Type
        {
            get
            {
                return this._EventType;
            }
        }
    }
}

