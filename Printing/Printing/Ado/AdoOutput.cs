using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Nistec.Printing.Data
{
 
    public class AdoOutput
    {
        #region members

        public event EventHandler ValueChanged;

        private AdoState _AdoState= AdoState.Default;

        public AdoState AdoState
        {
            get { return _AdoState; }
        }

        private object _value;

        public object Value
        {
            get
            {
                return this._value;
            }
            set
            {
                if (this._value != value)
                {
                    this._value = value;
                    if ((value != null) && (value is DataTable))
                    {
                        (value as DataTable).RowChanged += new DataRowChangeEventHandler(this.Parameter_RowChanged);
                    }
                    this.OnValueChanged();
                }
            }
        }

        private string _name;
        
        public virtual string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                if (this._name != value)
                {
                    this._name = value;
                    //this.OnNameChanged();
                }
            }
        }
        public bool HasErrors
        {
            get { return _ErrorDescription!=""; }
        }

        bool _ExecutionStoped=false;

        public bool ExecutionStoped
        {
            get { return _ExecutionStoped; }
        }
        string _ErrorDescription="";

        public string ErrorDescription
        {
            get { return _ErrorDescription; }
        }

        string _Description = "";

        public string Description
        {
            get { return _Description; }
        }
        //string _ResultMessage="";

        public string ResultMessage
        {
            get 
            {
                if (HasErrors)
                    return _ErrorDescription;
                return _Description; 
            }
        }

        uint _ObjectsProcessed;

        public uint ObjectsProcessed
        {
            get { return _ObjectsProcessed; }
        }
        
        string _ProgressMessage = "";

        public string ProgressMessage
        {
            get { return _ProgressMessage; }
        }

        private byte _Percentage = 0;

        public byte Percentage
        {
            get { return _Percentage; }
        }
        private uint _TotalObjects = 0;

        public uint TotalObjects
        {
            get { return _TotalObjects; }
            //set { _TotalObjects = value; }
        }
        #endregion

        #region ctor

        public AdoOutput()
        {

        }

        public AdoOutput(string name, string description)
        {
            _name = name;
            _Description = description;
        }
        #endregion

        #region methods

        internal void DoBegin(uint totalObjects)
        {
            this._ObjectsProcessed = 0;
            this._Percentage = 0;
            this._TotalObjects = totalObjects;
            this._AdoState = AdoState.Process;
            //this.OnExecutionStarted(totalObjects);
        }

        internal uint DoCommit(string result)
        {
            _Description = result;
            _ErrorDescription="";
            _Percentage = 100;
            this._AdoState = AdoState.Commited;
            return _ObjectsProcessed;
        }

        internal void DoCancel(string result)
        {
            _Description = result;
            _ErrorDescription = result;
            _ExecutionStoped = true;
            this._AdoState = AdoState.Canceled;
        }

        internal bool DoProgress(uint processed, string mesage)
        {
            _ObjectsProcessed = processed;
            _ProgressMessage = mesage;
            //if (_TotalObjects > 0)
            //{
            //    _Percentage =(byte) ((float)(_ObjectsProcessed / _TotalObjects)) * 100);
            //}
            return UpdateProcessing();
        }

        internal bool DoProgress()
        {
            _ObjectsProcessed++;
            //_ProgressMessage = mesage;
            //if (_TotalObjects > 0)
            //{
            //    _Percentage =(byte) ((float)(_ObjectsProcessed / _TotalObjects)) * 100);
            //}
            return UpdateProcessing();

        }

        internal int GetLength()
        {
            return  this._ObjectsProcessed.ToString().Length;
        }

        internal void ResetObjectsProcessed()
        {
            this._ObjectsProcessed = 0;
        }

        private bool UpdateProcessing()
        {
            int length = 0;
            byte percentage = 0;
            try
            {
                if (this._TotalObjects == 0)
                {
                    length = this._ObjectsProcessed.ToString().Length;
                    percentage = Convert.ToByte((double)((Convert.ToDouble(this._ObjectsProcessed) / Math.Pow(10.0, (double)length)) * 100.0));
                }
                else
                {
                    percentage = Convert.ToByte((double)((Convert.ToDouble(this._ObjectsProcessed) / Convert.ToDouble(this._TotalObjects)) * 100.0));
                }
            }
            catch
            {
            }
            if (percentage != this._Percentage)
            {
                this._Percentage = percentage;
                return true;
                //if (this._TotalObjects == 0)
                //{
                //    this.OnExecutionProgress(this._ObjectsProcessed, Convert.ToInt32(Math.Pow(10.0, (double)length)));
                //}
                //else
                //{
                //    this.OnExecutionProgress(this._ObjectsProcessed, this._TotalObjects);
                //}
            }
            return false;

        }

        protected virtual void OnValueChanged()
        {
            if (this.ValueChanged != null)
            {
                this.ValueChanged(this, new EventArgs());
            }
        }

        private void Parameter_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            //this.OnChanged();
        }

        #endregion

    }
}
