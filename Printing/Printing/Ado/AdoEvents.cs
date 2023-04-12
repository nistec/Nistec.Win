namespace Nistec.Printing.Data
{
    using System;
    using System.Runtime.CompilerServices;

    public delegate void AdoExecutionEventHandler(object sender, AdoExecutionEventArgs e);

    public class AdoExecutionEventArgs : EventArgs
    {
        private uint _objectsProcessed;
        private uint _objectsTotal;
        private bool _stopExecution;
        
        public AdoExecutionEventArgs(uint objectsProcessed, uint objectsTotal)
        {
            this._objectsProcessed = objectsProcessed;
            this._objectsTotal = objectsTotal;
            this._stopExecution = false;
        }

        public uint ObjectsProcessed
        {
            get
            {
                return this._objectsProcessed;
            }
        }

        public uint ObjectsTotal
        {
            get
            {
                return this._objectsTotal;
            }
        }

        public bool StopExecution
        {
            get
            {
                return this._stopExecution;
            }
            set
            {
                this._stopExecution = value;
            }
        }
    }
}

