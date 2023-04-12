namespace Nistec.Printing.Data
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Xml;
    //=using Nistec.Util;
    using System.Data;

    public abstract class AdoMap
    {

        #region members

        private System.Guid _guid;
        //private string _name;
        //private object _value;

        private AdoProperties _properties;
        private bool _saveData;
        private bool _stopExecution = false;
        private bool _batchNotResponding;
        private AdoOutput _output;

        public event AdoExecutionEventHandler ExecutionCommited;
        public event AdoExecutionEventHandler ExecutionProgress;
        public event AdoExecutionEventHandler ExecutionStarted;

        public event EventHandler PropertiesChanged;
        public event EventHandler ValueChanged;

        #endregion

        public AdoMap(string name,string description)
        {
            this._guid = System.Guid.NewGuid();
            this._properties = null;
            //this._name = name;
            _output = new AdoOutput(name,description);

        }

        public AdoMap()
        {
            this._guid = System.Guid.NewGuid();
            //this._name = "";
            this._properties = null;
            _output = new AdoOutput();
        }

        #region protected override

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

        protected virtual void OnPropertiesChanged()
        {
            if (this.PropertiesChanged != null)
            {
                this.PropertiesChanged(this, new EventArgs());
            }
            //this.OnChanged();
        }

        protected virtual void OnExecutionCommited(uint objectsProcessed, uint objectsTotal)
        {
            if (this.ExecutionCommited != null)
            {
                this.ExecutionCommited(this, new AdoExecutionEventArgs(objectsProcessed, objectsTotal));
            }
        }

        protected virtual void OnExecutionProgress(uint objectsProcessed, uint objectsTotal)
        {
            if (this.ExecutionProgress != null)
            {
                AdoExecutionEventArgs e = new AdoExecutionEventArgs(objectsProcessed, objectsTotal);
                this.ExecutionProgress(this, e);
                this._stopExecution = e.StopExecution;
            }
        }

        protected virtual void OnExecutionStarted(uint objectsTotal)
        {
            if (this.ExecutionStarted != null)
            {
                AdoExecutionEventArgs e = new AdoExecutionEventArgs(0, objectsTotal);
                this.ExecutionStarted(this, e);
                this._stopExecution = e.StopExecution;
            }
        }
        #endregion

        #region properties

        public virtual bool HasProperties
        {
            get
            {
                return (this._properties != null);
            }
        }

        //public virtual string Name
        //{
        //    get
        //    {
        //        return this._name;
        //    }
        //    set
        //    {
        //        if (this._name != value)
        //        {
        //            this._name = value;
        //            this.OnNameChanged();
        //        }
        //    }
        //}

        //public object Value
        //{
        //    get
        //    {
        //        return this._value;
        //    }
        //    set
        //    {
        //        if (this._value != value)
        //        {
        //            this._value = value;
        //            if ((value != null) && (value is DataTable))
        //            {
        //                (value as DataTable).RowChanged += new DataRowChangeEventHandler(this.Parameter_RowChanged);
        //            }
        //            this.OnValueChanged();
        //        }
        //    }
        //}

        public virtual AdoProperties Properties
        {
            get
            {
                return this._properties;
            }
            set
            {
                this._properties = value;
                this.OnPropertiesChanged();
            }
        }

        public virtual AdoOutput Output
        {
            get
            {
                return this._output;
            }
            set { this._output = value; }
         }


        public void ResetObjectsProcessed()
        {
            this._output.ResetObjectsProcessed();
        }

        public bool SaveData
        {
            get
            {
                return this._saveData;
            }
            set
            {
                this._saveData = value;
                //this.OnChanged();
            }
        }

        public bool StopProcessing
        {
            get
            {
                if (this._stopExecution)
                {
                    return true;
                }
                return false;
            }
            //internal
            //set { this._stopExecution = value; }
        }

        #endregion

        #region public override 

        public virtual AdoTable GetSchema()
        {
            return null;
        }

        public virtual uint ExecuteCommit()
        {
            uint res= this._output.DoCommit(this._output.Description);
            this.OnExecutionCommited(this._output.ObjectsProcessed,this._output.TotalObjects);
            return res;
        }

        public virtual void CancelExecute(string message)
        {
            _stopExecution = true;
            this._output.DoCancel(message);
        }

        public abstract bool ExecuteBatch(uint batchSize);
        //{
        //    //this._output.DoCancel("Batch operation has not been implemented.");
        //    return false;
        //}

        public virtual void ExecuteBegin(uint totalObjects)
        {
            this._batchNotResponding = false;
            _stopExecution = false;
            this._output.DoBegin(totalObjects);
            this.OnExecutionStarted(totalObjects);
        }

        public virtual bool Execute()
        {
            bool flag = false;
            //this._errorDescription = "";
            ExecuteBegin(0);
            flag = ExecuteBatch(0);
            if (flag)
            {
                ExecuteCommit();
            }
            return flag;
        }

        #endregion

        #region public methods

         public bool BatchNotResponding 
        {
            get
            {
                return this._batchNotResponding;
            }
            set
            {
                this._batchNotResponding = value;
            }
        }

        //public void BeginProcessing(uint totalObjects)
        //{
        //    //if ((base.Parent is Mapping.AdoMap) && ((base.Parent as Mapping.AdoMap).BatchCycle == 1))
        //    //{
        //    //    this._batchObjectsProcessed = 0L;
        //    //}
        //    //this._objectsProcessed = 0;
        //    //this._lastPercentage = -1;
        //    //this._totalObjects = totalObjects;
        //    this._output.DoBegin(totalObjects);
        //    this.OnExecutionStarted(totalObjects);
        //}

        //public void EndProcessing()
        //{
        //    this.OnExecutionFinished(this._objectsProcessed, this._totalObjects);
        //}


        //public void UpdateProcessing()
        //{
        //    this._objectsProcessed++;
        //    this._batchObjectsProcessed += 1L;
            
        //    this._output.DoProgress();
        //    int length = 0;
        //    int num2 = 0;
        //    try
        //    {
        //        if (this._totalObjects == 0)
        //        {
        //            length = this._objectsProcessed.ToString().Length;
        //            num2 = Convert.ToInt32((double)((Convert.ToDouble(this._objectsProcessed) / Math.Pow(10.0, (double)length)) * 100.0));
        //        }
        //        else
        //        {
        //            num2 = Convert.ToInt32((double)((Convert.ToDouble(this._objectsProcessed) / Convert.ToDouble(this._totalObjects)) * 100.0));
        //        }
        //    }
        //    catch
        //    {
        //    }
        //    if (num2 != this._lastPercentage)
        //    {
        //        this._lastPercentage = num2;
        //        if (this._totalObjects == 0)
        //        {
        //            this.OnExecutionProgress(this._objectsProcessed, Convert.ToInt32(Math.Pow(10.0, (double)length)));
        //        }
        //        else
        //        {
        //            this.OnExecutionProgress(this._objectsProcessed, this._totalObjects);
        //        }
        //    }
        //}

        public void UpdateProcessing()
        {
            if (this._output.DoProgress())
            {
                int length = this._output.GetLength();
                if (this._output.TotalObjects == 0)
                {
                    this.OnExecutionProgress(this._output.ObjectsProcessed, Convert.ToUInt32(Math.Pow(10.0, (double)length)));
                }
                else
                {
                    this.OnExecutionProgress(this._output.ObjectsProcessed, this._output.TotalObjects);
                }
            }
        }

        protected virtual void UpdateSchema()
        {
            AdoProperties properties = this.Properties as AdoProperties;
            if ((this.Output == null) || (this.Output.Value == null))
            {
                properties._DataSource.Columns.Clear();
            }
            else
            {
                DataTable table = this.Output.Value as DataTable;
                if (properties._DataSource.TableName == "")
                {
                    properties._DataSource.TableName = table.TableName;
                }
                if (properties._DataSource.Columns.Count == table.Columns.Count)
                {
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        properties._DataSource.Columns[i].ColumnName = table.Columns[i].ColumnName;
                    }
                }
                else
                {
                    properties._DataSource.Columns.Clear();
                    foreach (DataColumn column in table.Columns)
                    {
                        int length = 0;
                        if (column.DataType == typeof(DateTime))
                        {
                            length = Convert.ToDateTime("30 September 2000 23:59:59").ToString(properties._dateFormat).Length;
                        }
                        else
                        {
                            for (int j = 0; j < table.Rows.Count; j++)
                            {
                                string str = table.Rows[j][column].ToString();
                                length =Math.Max(length, str.Length);
                            }
                        }
                        properties._DataSource.Columns.Add(new AdoColumn(column.ColumnName, column.DataType, length));
                    }
                }
            }
        }

  

        #endregion

    }
}

