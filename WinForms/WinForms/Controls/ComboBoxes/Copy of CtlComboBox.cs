using System;
using System.Data;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using mControl.Util;
using mControl.Win32;
using System.Reflection;

using System.Collections;
using System.Windows.Forms.Design;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using mControl.WinCtl.Controls;
using mControl.Collections;


namespace mControl.WinCtl.Controls
{

    [Designer(typeof(Design.ComboBoxDesigner))]
    [DefaultEvent("SelectedIndexChanged"), ToolboxItem(true)]
    [ToolboxBitmap(typeof(CtlComboBox), "Toolbox.ComboBox.bmp")]
    public class CtlComboBox : CtlComboBase, IComboList, IDropDown
    {

        #region Base Members
        private System.ComponentModel.IContainer components = null;

        private ComboPopUp m_ComboPopUp;
        private bool m_AcceptItems;

        private int m_DropDownWidth;
        private ComboBoxStyle m_DropDownStyle;
        private int m_MaxDropDownItems;
        private int m_MaxLength;
        private bool m_Sorted;
        private int m_SelectedIndex;

        private CurrencyManager m_DataManager;
        private object m_DataSource;
        private string m_DisplayMember; //BindingMemberInfo
        private string m_ValueMember;	//BindingMemberInfo
        protected bool gridBounded;
        internal Rectangle TextRectInternal = Rectangle.Empty;
        internal bool shouldResetList;

        #endregion

        #region Event Members
        // Events
        [Category("PropertyChanged"), Description("ListControlOnDataSourceChanged")]
        public event EventHandler DataSourceChanged;
        [Category("PropertyChanged"), Description("ListControlOnDisplayMemberChanged")]
        public event EventHandler DisplayMemberChanged;
        [Category("PropertyChanged"), Description("ListControlOnValueMemberChanged")]
        public event EventHandler ValueMemberChanged;

        [Category("PropertyChanged"), Description("ListControlOnSelectedValueChanged")]
        public event EventHandler SelectedValueChanged;
        [Category("PropertyChanged"), Description("ListControlOnSelectedIndexChanged")]
        public event EventHandler SelectedIndexChanged;
        [Category("PropertyChanged"), Description("ListControlOnDrawItemEventHandler")]
        public event DrawItemEventHandler DrawItem;
        [Category("PropertyChanged"), Description("ListControlOnDropDownOcurred")]
        public event EventHandler DropDownOcurred;
        [Category("PropertyChanged"), Description("ListControlOnSelectionChangeCommitted")]
        public event EventHandler SelectionChangeCommitted;
        [Category("PropertyChanged"), Description("ListControlOnDropDownStyleChanged")]
        public event EventHandler DropDownStyleChanged;

        #endregion

        #region Constructors

        public CtlComboBox()
        {
            gridBounded = false;
            m_SelectedIndex = -1;
            m_DisplayMember = "";
            m_ValueMember = "";
            m_MaxDropDownItems = 8;
            m_AcceptItems = false;
            m_MaxLength = 0;
            m_Sorted = false;
            shouldResetList = true;

            InitializeComponent();
            InitComboPopUp();
        }

        internal CtlComboBox(bool net)
            : this()
        {
            this.m_netFram = net;
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (base.DefaultValue != "")
            {
                this.UpdateText(this.DefaultValue);
            }
            //try
            //{
            //    this.fromHandleCreate = true;
            //    this.SetAutoComplete(false, false);
            //}
            //finally
            //{
            //    this.fromHandleCreate = false;
            //}
        }


       


        #endregion

        #region Dispose

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            if (m_ComboPopUp != null)
            {
                //Control.CheckForIllegalCrossThreadCalls = true;
                m_ComboPopUp.Closed -= new System.EventHandler(this.OnPopUpClosed);
                m_ComboPopUp.internalList.DrawItem -= new DrawItemEventHandler(internalList_DrawItem);
                m_ComboPopUp.DisposePopUp(true);
            }
            base.Dispose(disposing);
        }

        #endregion

        #region Component Designer generated code

        protected virtual void InitComboPopUp()
        {
            m_ComboPopUp = new ComboPopUp(this);
            m_ComboPopUp.Closed += new System.EventHandler(this.OnPopUpClosed);
            m_ComboPopUp.internalList.DrawItem += new DrawItemEventHandler(internalList_DrawItem);
        }

        private void InitializeComponent()
        {
            this.Name = "CtlComboBox";
        }
        #endregion

        #region Win32 Handel

        internal IntPtr SendMessage(int msg, int wparam, IntPtr lparam)
        {
            return WinMethods.SendMessage(new HandleRef(this, this.Handle), msg, (IntPtr)wparam, lparam);
        }

        internal IntPtr SendMessage(int msg, int wparam, int lparam)
        {
            return WinMethods.SendMessage(new HandleRef(this, this.Handle), msg, wparam, lparam);
        }

        #endregion

        #region Validation

        protected override void OnValidating(System.ComponentModel.CancelEventArgs e)
        {
            if (this.DropDownStyle != ComboBoxStyle.DropDownList)
            {
                base.OnValidating(e);
                IsValidating();
            }
        }

        protected override void OnValidated(System.EventArgs e)
        {
            base.OnValidated(e);
        }

        private bool Validate_Item()
        {
            if (this.AcceptItems)
            {
                int index = ((CtlListCombo)this.InternalList).Items.IndexOf(this.Text);
                if (index > -1)
                {
                    ((CtlListCombo)this.InternalList).SelectedIndex = index;
                }
                return (index < 0);
            }
            return false;
        }

        #endregion

        #region Overrides

        //[EditorBrowsable(EditorBrowsableState.Advanced)]
        //protected override void OnTextChanged(EventArgs e)
        //{
        //    if (this.SystemAutoCompleteEnabled)
        //    {
        //        string text = this.Text;
        //        if (text != this.lastTextChangedValue)
        //        {
        //            base.OnTextChanged(e);
        //            this.lastTextChangedValue = text;
        //        }
        //    }
        //    else
        //    {
        //        base.OnTextChanged(e);
        //    }
        //}

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (this.ReadOnly)
            {
                base.OnKeyDown(e);
                return;
            }
            if (InternalList != null)
            {
                if (allowAutoComplete)
                {
                    KeyDownInternal(e);
                }
         
                //if (this.SystemAutoCompleteEnabled)
                //{
                //    if (e.KeyCode == Keys.Return)
                //    {
                //        this.NotifyAutoComplete(true);
                //    }
                //    else if ((e.KeyCode == Keys.Escape) && this.autoCompleteDroppedDown)
                //    {
                //        this.NotifyAutoComplete(false);
                //    }
                //    this.autoCompleteDroppedDown = false;
                //}

                int CurrentInt = SelectedIndex;

                if (e.KeyCode == Keys.Enter && this.DroppedDown)
                {
                    if (m_ComboPopUp != null)
                        m_ComboPopUp.OnSelectionChanged();
                }
                else if (e.Shift && e.KeyCode == Keys.Down)
                {
                    DoDropDown(); //ShowPopUp();
                }
                else if (((e.Shift && e.KeyCode == Keys.Up) || e.KeyCode == Keys.Escape) && (m_ComboPopUp != null))
                {
                    CloseDropDown();
                }

                else if (e.KeyCode == System.Windows.Forms.Keys.Down)
                {
                    //if (this.ReadOnly && this.DropDownStyle == ComboBoxStyle.Simple)
                    //{
                    //    return;
                    //}
                    if (Items.Count == 0)
                        return;
                    else if (CurrentInt >= Items.Count - 1)
                    {
                        if (this.DroppedDown)
                            this.InternalList.SelectedIndex = Items.Count - 1;
                        SelectedIndex = Items.Count - 1;
                    }
                    else
                    {
                        if (this.DroppedDown)
                            this.InternalList.SelectedIndex += 1;
                        SelectedIndex += 1;
                    }
                    this.SelectAll();
                }
                else if (e.KeyCode == System.Windows.Forms.Keys.Up)
                {
                    //if (this.ReadOnly && this.DropDownStyle == ComboBoxStyle.Simple)
                    //{
                    //    return;
                    //}
                    if (CurrentInt > 0)
                    {
                        if (this.DroppedDown)
                            this.InternalList.SelectedIndex -= 1;
                        SelectedIndex -= 1;
                    }
                    else
                    {
                        if (this.DroppedDown)
                            this.InternalList.SelectedIndex = 0;
                        SelectedIndex = 0;
                    }
                    this.SelectAll();
                }
            }
            base.OnKeyDown(e);

        }

        protected override void OnMouseWheel(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (this.ReadOnly)// && this.DropDownStyle == ComboBoxStyle.Simple)
            {
                return;
            }

            if (InternalList != null)
            {
                if (Items.Count <= 0)
                    return;

                int CurrentInt = SelectedIndex;
                int Delta = e.Delta * SystemInformation.MouseWheelScrollLines / 120;

                if (CurrentInt + Delta >= Items.Count)
                    SelectedIndex = Items.Count - 1;
                else if (CurrentInt + Delta < 0)
                    SelectedIndex = 0;
                else
                    SelectedIndex += Delta;

                //this.Text =Items[SelectedIndex].ToString (); 
                //this.SelectAll(); 
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (this.ReadOnly)
                return;
            if (this.DropDownStyle == ComboBoxStyle.DropDownList)
            {
                this.DoDropDown();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (this.DropDownStyle == ComboBoxStyle.DropDownList)
            {

                string selectedTxt = this.Text;
                if (SelectedIndex > -1 && selectedTxt.Length == 0)
                    selectedTxt = InternalList.Text; 

                if (selectedTxt.Length == 0)
                    return;

                //if (SelectedItem != null)
                //    selectedTxt = SelectedItem.ToString();


                int ItemHeight = Height - 7;
                int top = (Height - ItemHeight) / 2;
                int btnWidth = base.GetButtonRect.Width;
                Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);

                Rectangle bounds = new Rectangle(rect.X + 2, rect.Y + 2, rect.Width - btnWidth - 5, rect.Height - 4);
                Rectangle txtRect = this.m_TextBox.Bounds;// base.GetTextRect;// new Rectangle(RECTTEXT_LEFT,top,rect.Width-RECTTEXT_LEFT-btnWidth,ItemHeight);
                //Rectangle txtRect=new Rectangle(txtrect.X+2,txtrect.Y+3,txtrect.Width-2,txtrect.Height);//new Rectangle(RECTTEXT_LEFT,top,bounds.Width-RECTTEXT_LEFT,ItemHeight));
                //Rectangle txtRect=new Rectangle(txtrect.X,txtrect.Y,txtrect.Width,txtrect.Height);//new Rectangle(RECTTEXT_LEFT,top,bounds.Width-RECTTEXT_LEFT,ItemHeight));

                if (TextRectInternal != Rectangle.Empty)
                {
                    txtRect = TextRectInternal;
                }
                if (this.ButtonAlign == ButtonAlign.Left)
                {
                    bounds = new Rectangle(rect.X + btnWidth + 3, rect.Y + 2, rect.Width - btnWidth - 5, rect.Height - 4);
                    txtRect.X += btnWidth + 2;
                }

                Brush brushtxt;
                if (this.DrawMode == DrawMode.Normal)
                {
                    //g.FillRectangle(new SolidBrush(Color.Navy),bounds);
                    using (Brush sb = CtlStyleLayout.GetBrushCaption())
                        e.Graphics.FillRectangle(sb, bounds);

                    //g.DrawRectangle(Pens.DarkGray,bounds);
                    brushtxt = new SolidBrush(Color.White);
                }
                else
                {
                    bounds.Width -= 1;
                    bounds.Height -= 1;
                    e.Graphics.FillRectangle(CtlStyleLayout.GetBrushSelected(), bounds);
                    e.Graphics.DrawRectangle(Pens.DarkGray, bounds);
                    brushtxt = CtlStyleLayout.GetBrushText();
                }

                StringFormat sf = new StringFormat();
                sf.LineAlignment = StringAlignment.Center;
                if (this.TextAlign == HorizontalAlignment.Center)
                    sf.Alignment = StringAlignment.Center;
                else if (this.TextAlign == HorizontalAlignment.Left)
                    sf.Alignment = StringAlignment.Near;
                else if (this.TextAlign == HorizontalAlignment.Right)
                    sf.Alignment = StringAlignment.Far;

                e.Graphics.DrawString(this.Text, Font, brushtxt, txtRect, sf);
                sf.Dispose();
                brushtxt.Dispose();

            }
        }

        #endregion

        #region Virtual

        protected virtual void UpdateSelection(object value)
        {
            object item = null;
            if (value is DataRowView)
            {
                item = ((DataRowView)value).Row[this.DisplayMember.Substring(this.DisplayMember.IndexOf(".") + 1)];
                object obj = ((DataRowView)value).Row[this.ValueMember.Substring(this.ValueMember.IndexOf(".") + 1)];
                if (obj != null)
                    this.SelectedValue = obj;
            }
            else if (value is string)
            {
                int index = this.InternalList.FindStringExact((string)value.ToString());
                if (index > -1) this.SelectedIndex = index;
            }

        }

        internal virtual void SetSelectedIndexInternal(int value, bool checkItems)
        {
            SetSelectedIndexInternal(value, checkItems, true);
        }

        internal virtual void SetSelectedIndexInternal(int value, bool checkItems, bool updateText)
        {
            //if (SelectedIndex != value)
            //{
                if (value == -1)
                {
                    m_SelectedIndex = value;
                    //this.ResetText();
                    this.UpdateText();
                    return;
                }
                int num1 = 0;
                if (InternalList != null)
                {
                    num1 = InternalList.Items.Count;
                }

                if ((value < -1) || (value >= num1))
                {
                    throw new ArgumentOutOfRangeException("InvalidArgument");
                }
                if (checkItems)
                {
                    object obj = InternalList.Items[value];
                    if (obj == null)
                    {
                        return;
                    }
                    InternalList.SelectedItem = obj;//InternalList.Items[value];
                    //InternalList.SelectedIndex=value;
                }
                else
                {
                    InternalList.SelectedIndex = value;
                }

                m_SelectedIndex = value;
                if (updateText)
                {
                    this.UpdateText();

                    if (base.IsHandleCreated)
                    {
                        this.OnTextChanged(EventArgs.Empty);
                        this.OnSelectionChangeCommitted(EventArgs.Empty);
                    }
                    //this.OnSelectedValueChanged(EventArgs.Empty);
                    this.OnSelectedIndexChanged(EventArgs.Empty);
                }
            //}
        }

        #endregion

        #region Public  Methods

        public int FindString(string s)
        {
            return ((CtlListCombo)this.InternalList).FindString(s);
        }

        public int FindString(string s, int startIndex)
        {
            return ((CtlListCombo)this.InternalList).FindString(s, startIndex);
        }

        #endregion

        #region Internal Methods

        internal int FindStringInternal(string str, IList items, int startIndex, bool exact)
        {
            if ((str == null) || (items == null))
            {
                return -1;
            }
            if ((startIndex < -1) || (startIndex >= (items.Count - 1)))
            {
                return -1;
            }
            bool flag1 = false;
            int num1 = str.Length;
            int num2 = startIndex;
            while (true)
            {
                num2++;
                if (exact)
                {
                    flag1 = string.Compare(str, this.GetItemText(items[num2]), true, CultureInfo.CurrentCulture) == 0;
                }
                else
                {
                    flag1 = string.Compare(str, 0, this.GetItemText(items[num2]), 0, num1, true, CultureInfo.CurrentCulture) == 0;
                }
                if (flag1)
                {
                    return num2;
                }
                if (num2 == (items.Count - 1))
                {
                    num2 = -1;
                }
                if (num2 == startIndex)
                {
                    return -1;
                }
            }
        }

        protected void SetItemsCore(IList value)
        {
            //this.BeginUpdate();
            this.SuspendLayout();
            this.ClearInternal();
            this.AddRangeInternal(value);
            if (m_DataManager != null)
            {
                //SendMessage(0x14e, m_DataManager.Position, 0);
                //if (!m_SelectedValueChangedFired)
                //{
                this.OnSelectedValueChanged(EventArgs.Empty);
                //m_SelectedValueChangedFired = false;
                //}
            }
            //this.EndUpdate();
            this.ResumeLayout();
        }

        internal void ClearInternal()
        {
            this.Items.Clear();
            m_SelectedIndex = -1;
        }

        internal void AddRangeInternal(IList items)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }
            foreach (object obj1 in items)
            {
                if (obj1 == null)
                {
                    ClearInternal();
                    throw new ArgumentNullException("item");
                }
                InternalList.Items.Add(obj1);
            }

            InternalList.Sorted = m_Sorted;
        }


        private void CheckNoDataSource()
        {
            if (DataSource != null)
            {
                throw new ArgumentException("DataSourceLocksItems");
            }
        }

        protected void UpdateText(object value)
        {
            if (value == null || value.ToString() == "") return;
            try
            {
                if (this.DataSource != null)
                {
                    this.SelectedValue = value;
                    UpdateText();
                }
                else if (this.Items.Count > 0)
                {
                    this.SelectedItem = value;
                    UpdateText();
                }
            }
            catch { }
        }

        protected void UpdateText()
        {
            string text1 = base.DefaultValue;// "";
            int indx = SelectedIndex;
            if (indx != -1)
            {
                object obj1 = InternalList.Items[indx];
                if (obj1 != null)
                {
                    text1 = GetItemText(obj1);
                }
            }
            base.Text = text1;
            if (this.DropDownStyle == ComboBoxStyle.DropDownList)
            {
                this.Invalidate();
                //base.DrawControl(false);
            }
            else
            {
                this.m_TextBox.SelectAll();
            }
        }

        protected virtual string GetItemText(object item)
        {
            //			if(m_Columns!=null)
            //			{
            //				item = ((DataRowView)item).Row[m_ColumnView].ToString();
            //			}
            if (item is DataRowView)
            {
                item = ((DataRowView)item).Row[DisplayMember.Substring(DisplayMember.IndexOf(".") + 1)];
            }
            else if (item is string)
            {
                item = (string)item;
            }
            if (item == null)
            {
                return "";
            }

            return Convert.ToString(item, CultureInfo.CurrentCulture);
        }

        public void SetSort(string field)
        {
            if (m_DataSource != null)
                ((DataView)m_DataSource).Sort = field;
        }

        //		public string DataManagerFind( object item, string field)
        //		{
        //			PropertyDescriptor property1=GetPropertyDescriptor(item,field);
        //			int i= DataManagerFind(property1,item,true);
        //			return ((DataView)m_DataSource)[i][this.DisplayMember].ToString ();
        //		}

        protected virtual object GetDataRowItem(object item, string field)
        {
            DataView dv = (DataView)m_DataSource;
            if ((dv.Sort != field))
            {
                dv.Sort = field;//this.ValueMember;
            }
            int i = dv.Find(item);
            if ((i < 0))
            {
                return "";
            }
            return dv[i];
        }

        protected virtual string GetDataRowText(object item, string field)
        {
            //PropertyDescriptor property1 = GetPropertyDescriptor(item, field);
            //int i = DataManagerFind(property1, item, true);
            //return ((DataView)m_DataSource)[i][this.DisplayMember].ToString();

            DataView dv = (DataView)m_DataSource;
            if ((dv.Sort != field))
            {
                dv.Sort = field;// this.ValueMember;
            }
            int i = dv.Find(item);
            if ((i < 0))
            {
                return "";
            }
            return dv[i][this.DisplayMember].ToString();
        }

        protected void RefreshItem(int index)
        {
            SetItemInternal(index, this.Items[index]);
        }

        private void RefreshItems()
        {
            int num1 = this.SelectedIndex;
            CtlListCombo.ObjectCollection collection1 = Items;
            this.Items.Clear();
            object[] objArray1 = null;
            if ((m_DataManager != null) && (m_DataManager.Count != -1))
            {
                objArray1 = new object[m_DataManager.Count];
                for (int num2 = 0; num2 < objArray1.Length; num2++)
                {
                    objArray1[num2] = m_DataManager.List[num2];
                }
            }
            else if (collection1 != null)
            {
                objArray1 = new object[collection1.Count];
                collection1.CopyTo(objArray1, 0);
            }
            if (objArray1 != null)
            {
                this.Items.AddRange(objArray1);
            }
            if (m_DataManager != null)
            {
                this.SelectedIndex = m_DataManager.Position;
            }
            else
            {
                this.SelectedIndex = num1;
            }
        }

        protected void SetItemCore(int index, object value)
        {
            this.SetItemInternal(index, value);
        }

        internal void SetItemInternal(int index, object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            if ((index < 0) || (index >= InternalList.Items.Count))
            {
                throw new ArgumentOutOfRangeException("InvalidArgument");
            }
            InternalList.Items[index] = value;
            if (this.IsHandleCreated)
            {
                bool flag1 = index == m_SelectedIndex;
                InternalList.Items.RemoveAt(index);
                InternalList.Items.Insert(index, value);
                if (flag1)
                {
                    SelectedIndex = index;
                    this.UpdateText();
                }
            }
        }


        protected override bool IsInputKey(Keys keyData)
        {
            Keys keys1 = keyData & (Keys.Alt | Keys.KeyCode);
            if (((keys1 == Keys.Return) || (keys1 == Keys.Escape)) && this.DroppedDown)
            {
                return true;
            }
            return base.IsInputKey(keyData);
        }

        #endregion

        #region Data Manager

        protected CurrencyManager DataManager
        {
            get
            {
                return m_DataManager;
            }
        }

        private void SetDataConnection(object newDataSource, string newDisplayMember, bool force)
        {
            bool flag1 = m_DataSource != newDataSource;
            bool flag2 = !m_DisplayMember.Equals(newDisplayMember);
            if ((force || flag1) || flag2)
            {
                if (m_DataSource is IComponent)
                {
                    ((IComponent)DataSource).Disposed -= new EventHandler(this.DataSourceDisposed);
                }
                m_DataSource = newDataSource;
                m_DisplayMember = newDisplayMember;
                if (m_DataSource is IComponent)
                {
                    ((IComponent)m_DataSource).Disposed += new EventHandler(this.DataSourceDisposed);
                }
                CurrencyManager manager1 = null;
                if (((newDataSource != null) && (this.BindingContext != null)) && (newDataSource != Convert.DBNull))
                {
                    manager1 = (CurrencyManager)this.BindingContext[newDataSource];//, newDisplayMember];
                }
                //else if ((newDataSource is IList) && (newDataSource != null) && (newDataSource != Convert.DBNull))
                //{
                //    mControl.Util.BindControl bind = new BindControl();
                //    Binding b=bind.BindToString("Text", newDataSource, m_ValueMember);
                //    this.DataBindings.Add(b);
                    
                //    //this.DataBindings.Add("SelectedValue", newDataSource,"");// newDisplayMember);
                //    // Specify the CurrencyManager for the DataTable.
                //    manager1 = (CurrencyManager)this.BindingContext[newDataSource];
                //    // Set the initial Position of the control.
                //    //myCurrencyManager.Position = 0;

                //}

                if (m_DataManager != manager1)
                {
                    if (m_DataManager != null)
                    {
                        m_DataManager.ItemChanged -= new ItemChangedEventHandler(DataManager_ItemChanged);
                        m_DataManager.PositionChanged -= new EventHandler(DataManager_PositionChanged);
                    }
                    m_DataManager = manager1;
                    if (m_DataManager != null)
                    {
                        m_DataManager.ItemChanged += new ItemChangedEventHandler(DataManager_ItemChanged);
                        m_DataManager.PositionChanged += new EventHandler(DataManager_PositionChanged);
                    }
                }
                if (((m_DataManager != null) && (flag2 || flag1)) && (!"".Equals(m_DisplayMember) && !this.BindingMemberInfoInDataManager(m_DisplayMember)))
                {
                    throw new ArgumentException("ListControlWrongDisplayMember");//, "newDisplayMember");
                }
                //				if ((m_DataManager != null) && ((flag1 || flag2) || force))
                //				{
                //					//OnItemChanged(-1);
                //					//DataManager_ItemChanged(m_DataManager, new ItemChangedEventArgs(-1));
                //					InternalList.DataSource=m_DataSource;
                //					InternalList.DisplayMember =m_DisplayMember;
                //					//InternalList.ValueMember =m_ValueMember;
                //				}
            }
            if (flag1)
            {
                this.OnDataSourceChanged(EventArgs.Empty);
            }
            if (flag2)
            {
                this.OnDisplayMemberChanged(EventArgs.Empty);
            }
        }

        private void OnItemChanged(int index)
        {
            if (m_DataManager != null)
            {
                if (index == -1)
                {
                    this.SetItemsCore(m_DataManager.List);
                    this.SelectedIndex = m_DataManager.Position;
                }
                else
                {
                    this.SetItemCore(index, m_DataManager.List[index]);
                }
            }
        }

        private void DataManager_ItemChanged(object sender, ItemChangedEventArgs e)
        {
            OnItemChanged(e.Index);
        }

        private void DataManager_PositionChanged(object sender, EventArgs e)
        {
            if (m_DataManager != null)
            {
                this.SelectedIndex = m_DataManager.Position;
            }
        }

        private void DataSourceDisposed(object sender, EventArgs e)
        {
            this.SetDataConnection(null, "", true);
        }

        private bool BindingMemberInfoInDataManager(string memberInfo)//BindingMemberInfo bindingMemberInfo)
        {
            if (m_DataManager == null)
            {
                return false;
            }
            PropertyDescriptorCollection collection1 = m_DataManager.GetItemProperties();
            int num1 = collection1.Count;
            bool flag1 = false;
            for (int num2 = 0; num2 < num1; num2++)
            {
                if (!typeof(IList).IsAssignableFrom(collection1[num2].PropertyType) && collection1[num2].Name.Equals(memberInfo))//bindingMemberInfo.BindingField))
                {
                    flag1 = true;
                    break;
                }
            }
            return flag1;
        }

        protected object FilterItemOnProperty(object item)
        {
            return this.FilterItemOnProperty(item, m_DisplayMember);
        }

        protected object FilterItemOnProperty(object item, string field)
        {
            if ((item != null) && (field.Length > 0))
            {
                try
                {
                    PropertyDescriptor descriptor1;
                    if (m_DataManager != null)
                    {
                        descriptor1 = m_DataManager.GetItemProperties().Find(field, true);
                    }
                    else
                    {
                        descriptor1 = TypeDescriptor.GetProperties(item).Find(field, true);
                    }
                    if (descriptor1 != null)
                    {
                        item = descriptor1.GetValue(item);
                    }
                }
                catch (Exception)
                {
                }
            }
            return item;
        }

        internal int DataManagerFind(PropertyDescriptor property, object key, bool keepIndex)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            if (((property != null) && (m_DataManager.List is IBindingList)) && ((IBindingList)m_DataManager.List).SupportsSearching)
            {
                return ((IBindingList)m_DataManager.List).Find(property, key);
            }
            for (int num1 = 0; num1 < m_DataManager.List.Count; num1++)
            {
                object obj1 = property.GetValue(m_DataManager.List[num1]);
                if (key.Equals(obj1))
                {
                    return num1;
                }
            }
            return -1;
        }

        protected PropertyDescriptor GetPropertyDescriptor(object item, string field)
        {
            PropertyDescriptor descriptor1 = null;
            if ((item != null) && (field.Length > 0))
            {
                try
                {
                    if (m_DataManager != null)
                    {
                        descriptor1 = m_DataManager.GetItemProperties().Find(field, true);
                    }
                    else
                    {
                        descriptor1 = TypeDescriptor.GetProperties(item).Find(field, true);
                    }
                    //if (descriptor1 != null)
                    //{
                    //	item = descriptor1.GetValue(item);
                    //}
                }
                catch (Exception)
                {
                }
            }
            return descriptor1;
        }


        #endregion

        #region Virtual Events

        protected virtual void OnSelectedValueChanged(EventArgs e)
        {
            if (this.SelectedValueChanged != null)
                this.SelectedValueChanged(this, e);
        }

        protected virtual void OnSelectedIndexChanged(EventArgs e)
        {
            if (this.SelectedIndexChanged != null)
                this.SelectedIndexChanged(this, e);
        }

        protected override void OnBindingContextChanged(EventArgs e)
        {
            base.OnBindingContextChanged(e);
            if (m_DataSource != null)
            {
                this.SetDataConnection(m_DataSource, m_DisplayMember, gridBounded);
            }
        }

        protected virtual void OnDataSourceChanged(EventArgs e)
        {
            /*sort*/
            //if ((this.Sorted && (m_DataSource != null)) && base.Created)
            //{
            //    m_DataSource = null;
            //    throw new Exception("ComboBoxDataSourceWithSort");
            //}
            if (m_DataSource == null)
            {
                //this.BeginUpdate();
                this.SelectedIndex = -1;
                this.ClearInternal();
                //this.EndUpdate();
            }
            InternalList.DataSource = m_DataSource;
            //this.RefreshItems();


            if (this.DataSourceChanged != null)
                this.DataSourceChanged(this, e);
        }

        protected virtual void OnDisplayMemberChanged(EventArgs e)
        {
            if (this.DisplayMemberChanged != null)
                this.DisplayMemberChanged(this, e);
        }

        protected virtual void OnValueMemberChanged(EventArgs e)
        {
            if (this.ValueMemberChanged != null)
                this.ValueMemberChanged(this, e);
        }

        protected virtual void OnDropDown(EventArgs e)
        {
            if (DropDownOcurred != null)
                this.DropDownOcurred(this, e);
        }

        protected virtual void OnSelectionChangeCommitted(EventArgs e)
        {
            if (SelectionChangeCommitted != null)
                this.SelectionChangeCommitted(this, e);
        }

        protected virtual void OnDropDownStyleChanged(EventArgs e)
        {
            if (DropDownStyleChanged != null)
                this.DropDownStyleChanged(this, e);
        }

        //		protected virtual void OnDrawItem(DrawItemEventArgs e)
        //		{
        //			OnDrawItemInternal(e);
        //			if (this.DrawItem !=null)
        //				this.DrawItem(this, e);
        //		}

        private void internalList_DrawItem(object sender, DrawItemEventArgs e)
        {
            OnDrawItem(e);
        }

        private DrawItemEventHandler GetDrawItemHandler()
        {
            FieldInfo info1 = typeof(System.Windows.Forms.ListBox).GetField("EVENT_DRAWITEM", BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Static);
            return (DrawItemEventHandler)base.Events[info1.GetValue(null)];
        }

        protected virtual void OnDrawItem(DrawItemEventArgs e)
        {
            //DrawItemEventHandler handler1 = this.GetDrawItemHandler();
            if (this.DrawItem != null) //if (handler1 != null)
            {
                this.DrawItem(this, e);//handler1(this, e);
            }
            else if (this.DrawMode == DrawMode.OwnerDrawFixed || this.DrawMode == DrawMode.OwnerDrawVariable)
            {
                Graphics graphics1 = e.Graphics;
                Rectangle rectangle1 = e.Bounds;
                if ((e.State & DrawItemState.Selected) > DrawItemState.None)
                {
                    rectangle1.Width--;
                }
                DrawItemState state1 = e.State;
                if ((e.Index != -1) && (this.Items.Count > 0))
                {
                    //int num1 =-1;// this.UseFirstImage ? 0 : e.Index;
                    this.CtlStyleLayout.DrawItem(graphics1, rectangle1, this, e.State, this.GetItemText(this.Items[e.Index]));
                }
            }
        }

        protected virtual void OnDrawItemInternal(DrawItemEventArgs e)
        {
            //DrawItemEventHandler handler1 = this.GetDrawItemHandler();
            Graphics graphics1 = e.Graphics;
            Rectangle rectangle1 = e.Bounds;
            if ((e.State & DrawItemState.Selected) > DrawItemState.None)
            {
                rectangle1.Width--;
            }
            DrawItemState state1 = e.State;
            if ((e.Index != -1) && (this.Items.Count > 0))
            {
                //int num1 =0;// this.UseFirstImage ? 0 : e.Index;

                CtlStyleLayout.DrawItem(graphics1, rectangle1, this, e.State, this.GetItemText(this.Items[e.Index]));
            }
        }
        #endregion

        #region DropDown

        protected override bool GetMouseHook(IntPtr mh, IntPtr wparam)
        {
            if (!DroppedDown)
                return false;

            if (mh == m_ComboPopUp.Handle || mh == InternalList.Handle || mh == InternalButton.Handle)
            {
                return false;
            }
            if (this.DropDownStyle == ComboBoxStyle.DropDownList)
            {
                if (mh == this.Handle)
                    return false;
            }
            if (mh == Handle || mh == m_TextBox.Handle)
            {
                if (wparam == (IntPtr)513 && DroppedDown)
                {
                    m_ComboPopUp.Close();
                    return false;
                }
                return true;
            }
            //if (wparam == (IntPtr)522 && DroppedDown)//mouse wheel
            //{
            //    this.Select(0, 0);
            //    return true;
            //}
            if (wparam == (IntPtr)513 && DroppedDown)//mouse left
            {
                m_ComboPopUp.Close();
                return false;
            }
             return true;
        }

        private void OnPopUpClosed(object sender, System.EventArgs e)
        {
            base.EndHook();
            UpdateSelection(m_ComboPopUp.SelectedItem);
            m_ComboPopUp.DisposePopUp(false);
            //			if(!this.ReadOnly && this.m_AutoHide)
            //			{
            //				OnButtonHideChanged();//this.m_Button.Visible=this.Focused;//  false; 
            //			}
            Invalidate(false);
            OnDropUp(e);
        }

        protected virtual void OnDropUp(EventArgs e)
        {

        }

        private void ClosePopUp()
        {
            base.EndHook();
        }

        //[UseApiElements("ShowWindow")]
        protected virtual void ShowPopUp()
        {
            if (this.InternalList.Items.Count == 0)
                return;
            this.OnDropDown(new System.EventArgs());

            Point pt = new Point(this.Left, this.Bottom + 1);
            //m_ComboPopUp = new ComboPopUp(this);
            m_ComboPopUp.Location = this.Parent.PointToScreen(pt);
            //m_ComboPopUp.Closed += new System.EventHandler(this.OnPopUpClosed);
            //this.InternalList.frm=m_ComboPopUp; 
            m_ComboPopUp.ShowPopUp(m_ComboPopUp.Handle);
            m_ComboPopUp.Start = true;
            //DroppedDown = true;
            base.StartHook();
        }

        public override void DoDropDown()
        {
            if (this.ReadOnly)// && this.DropDownStyle == ComboBoxStyle.Simple)
            {
                return;
            }

            if (DroppedDown)
            {
                m_ComboPopUp.Close();
                return;
            }
            else
            {
                ShowPopUp();
            }
            base.DoDropDown();
        }

        public override void CloseDropDown()
        {
            if (DroppedDown)
            {
                m_ComboPopUp.Close();
                return;
            }
        }

        #endregion

        #region Data Properties


        //[DefaultValue((string)null), Category("Data"), Description("ListControlDataSource"), TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), RefreshProperties(RefreshProperties.Repaint)]
        [DefaultValue((string)null), AttributeProvider(typeof(IListSource)), Description("ListControlDataSource"), Category("Data"), RefreshProperties(RefreshProperties.Repaint)]
        public object DataSource
        {
            get { return ((CtlListCombo)this.InternalList).DataSource; }
            set
            {

                if (((value != null) && !(value is IList)) && !(value is IListSource))
                {
                    throw new Exception("BadDataSourceForComplexBinding");
                }
                if (m_DataSource != value)
                {
                    try
                    {
                        this.SetDataConnection(value, m_DisplayMember, false);
                        this.InternalList.DataSource = value;
                    }
                    catch (Exception ex)
                    {
                        this.DisplayMember = "";
                        throw ex;
                    }
                    if (value == null)
                    {
                        this.DisplayMember = "";
                    }
                }
            }
        }

        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), Category("Data"), TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), DefaultValue(""), Description("ListControlDisplayMember")]
        public string DisplayMember
        {
            get { return m_DisplayMember; }
            set
            {

                try
                {
                    this.SetDataConnection(m_DataSource, value, false);
                    InternalList.DisplayMember = m_DisplayMember;
                }
                catch (Exception)
                {
                    m_DisplayMember = value;
                    InternalList.DisplayMember = value;
                }
            }
        }

        [DefaultValue(""), Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), Category("Data"), Description("ListControlValueMember")]
        public string ValueMember
        {
            get { return m_ValueMember; }
            set
            {

                if (value == null)
                {
                    value = "";
                }
                //BindingMemberInfo info1 = new BindingMemberInfo(value);
                if (!value.Equals(m_ValueMember))
                {
                    if (m_DisplayMember.Length == 0)
                    {
                        this.SetDataConnection(m_DataSource, value, false);
                    }
                    if (((m_DataManager != null) && !"".Equals(value)) && !this.BindingMemberInfoInDataManager(value))
                    {
                        throw new ArgumentException("ListControlWrongValueMember");//, "value");
                    }
                    m_ValueMember = value;
                    InternalList.ValueMember = value;
                    this.OnValueMemberChanged(EventArgs.Empty);
                    this.OnSelectedValueChanged(EventArgs.Empty);
                }
            }
        }

        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), Description("ComboBoxItems"), Category("Data"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Localizable(true)]
        public CtlListCombo.ObjectCollection Items
        {
            get { return InternalList.Items; }
        }

        [Browsable(false)]
        internal CtlListCombo InternalList
        {
            get
            {
                if (m_ComboPopUp == null)
                    return null;
                return m_ComboPopUp.internalList;
            }
        }

        #endregion

        #region Combo Properties

        private void internalList_SelectedValueChanged(object sender, System.EventArgs e)
        {
            this.OnSelectedValueChanged(e);
        }

        protected void ClearSelection()
        {
            m_SelectedIndex = -1;
        }

        [Description("ComboBoxSelectedIndex"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int SelectedIndex
        {
            get
            {
                //				if(SelectedItem != null)
                //				{
                //					return InternalList.Items.IndexOf(SelectedItem); 
                //				}
                //if (base.IsHandleCreated)
                //{
                return m_SelectedIndex;
                //}
                //return -1;
            }
            set
            {
                SetSelectedIndexInternal(value, true);
            }
        }

        [Description("ComboBoxSelectedItem"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), Bindable(true)]
        public object SelectedItem
        {
            get
            {
                int num1 = SelectedIndex;
                //object itemMember=null;
                if (num1 != -1)
                {
                    return InternalList.Items[num1];
                }
                return null;//itemMember; 
            }
            set
            {
                int num1 = -1;
                if (InternalList != null)
                {
                    if (value != null)
                    {
                        num1 = InternalList.Items.IndexOf(value);
                    }
                    else
                    {
                        SelectedIndex = -1;
                    }
                }
                if (num1 != -1)
                {
                    SelectedIndex = num1;
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Bindable(true), Description("ListControlSelectedValue"), Browsable(false), DefaultValue((string)null), Category("Data")]
        public virtual object SelectedValue
        {
            get
            {
                if ((m_SelectedIndex != -1) && (m_DataManager != null))
                {
                    object obj1 = m_DataManager.List[m_SelectedIndex];
                    return this.FilterItemOnProperty(obj1, m_ValueMember);
                }
                return null;
            }
            set
            {
                if (value == System.DBNull.Value || value.Equals(null) || value.Equals(""))
                {
                    this.m_SelectedIndex = -1;
                    UpdateText();
                    return;
                }

                if (m_DataManager != null && this.InternalList != null)
                {
                    if (m_ValueMember.Equals(string.Empty))
                    {
                        throw new Exception("ListControlEmptyValueMemberInSettingSelectedValue");
                    }
                    //PropertyDescriptorCollection collection1 = m_DataManager.GetItemProperties();
                    //PropertyDescriptor descriptor1 = collection1.Find(m_ValueMember, true);
                    //int index = DataManagerFind(descriptor1, value, true);

                    int index = InternalList.SelectedIndex;

                    //m_ChangingLinkedSelection = true;
                    this.SelectedIndex = index;
                    InternalList.SelectedValue = value;
                    this.OnSelectedValueChanged(EventArgs.Empty);

                }

            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("ComboBoxSelectedText")]
        public string SelectedText
        {
            get
            {
                if (this.DropDownStyle == ComboBoxStyle.DropDownList)
                {
                    return "";
                }
                return this.Text.Substring(m_TextBox.SelectionStart, m_TextBox.SelectionLength);
            }
        }

        internal virtual string TextInternal
        {
            get { return m_TextBox.Text; }
            set { m_TextBox.Text = value; }
        }


        [Localizable(true), Bindable(true)]
        public override string Text
        {
            get
            {
                if ((this.SelectedItem != null) && !this.InternalList.BindingFieldEmpty)
                {
                    return this.InternalList.FilterItemOnPropertyInternal(this.SelectedItem).ToString();
                }
                return base.Text;
            }
            set
            {
                base.Text = value;
                if (!base.DesignMode)
                {
                    if (value == null)
                    {
                        this.SelectedIndex = -1;
                    }
                    else if ((value != null) && ((this.SelectedItem == null) || (string.Compare(value, this.InternalList.FilterItemOnPropertyInternal(this.SelectedItem).ToString(), false, CultureInfo.CurrentCulture) != 0)))
                    {
                        for (int num1 = 0; num1 < this.Items.Count; num1++)
                        {
                            if (string.Compare(value, this.InternalList.FilterItemOnPropertyInternal(this.Items[num1]).ToString(), false, CultureInfo.CurrentCulture) == 0)
                            {
                                this.SelectedIndex = num1;
                                return;
                            }
                        }
                        for (int num2 = 0; num2 < this.Items.Count; num2++)
                        {
                            if (string.Compare(value, this.InternalList.FilterItemOnPropertyInternal(this.Items[num2]).ToString(), true, CultureInfo.CurrentCulture) == 0)
                            {
                                this.SelectedIndex = num2;
                                return;
                            }
                        }
                    }
                }
            }
        }


        [Description("ComboBoxStyle"), DefaultValue(ComboBoxStyle.Simple), RefreshProperties(RefreshProperties.Repaint), Category("Appearance")]
        public ComboBoxStyle DropDownStyle
        {
            get
            {
                return m_DropDownStyle;
            }
            set
            {
                if (m_DropDownStyle != value)
                {
                    if (!Enum.IsDefined(typeof(ComboBoxStyle), value))
                    {
                        throw new InvalidEnumArgumentException("value", (int)value, typeof(ComboBoxStyle));
                    }
                    //CtlComboBox.PropStyle=(int) value;
                    if (base.IsHandleCreated)
                    {
                        base.RecreateHandle();
                    }

                    m_DropDownStyle = value;
                    this.OnDropDownStyleChanged(EventArgs.Empty);
                    if (m_DropDownStyle == ComboBoxStyle.DropDownList)
                    {
                        this.m_TextBox.Visible = false;
                    }
                    else
                    {
                        this.m_TextBox.Visible = true;
                    }
                    this.Invalidate();

                }
            }
        }

        [Description("ComboBoxDrawMode"), DefaultValue(DrawMode.Normal), RefreshProperties(RefreshProperties.Repaint), Category("Behavior")]
        public DrawMode DrawMode
        {
            get
            {
                return this.InternalList.DrawMode;// m_DrawMode;
            }
            set
            {
                //if (m_DrawMode != value)
                //{
                //if (!Enum.IsDefined(typeof(DrawMode), value))
                //{
                //	throw new InvalidEnumArgumentException("value", (int) value, typeof(DrawMode));
                //}
                InternalList.DrawMode = value;
                //m_DrawMode=value;
                base.RecreateHandle();
                //}
            }
        }

        [Description("ComboBoxDropDownWidth"), Category("Behavior"), DefaultValue(0)]
        public int DropDownWidth
        {
            get
            {
                return m_DropDownWidth;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("InvalidArgument ", value.ToString());//, objArray1));
                }
                if (DropDownWidth != value)
                {
                    m_DropDownWidth = value;
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), Description("ComboBoxDroppedDown")]
        public override bool DroppedDown
        {
            get
            {
                if (base.IsHandleCreated)
                {
                    return base.DroppedDown;
                }
                return false;
            }
            set
            {
                //		        if(value && !base.DisableDropDown)
                DoDropDown();//ShowPopUp();
                //this.SendMessage(0x14f, value ? -1 : 0, 0);

            }
        }


        [Category("Behavior"), Description("ComboBoxIntegralHeight"), DefaultValue(true), Localizable(true)]
        public bool IntegralHeight
        {
            get
            {
                return InternalList.IntegralHeight;//m_IntegralHeight;
            }
            set
            {
                InternalList.IntegralHeight = value;
                //if (m_IntegralHeight != value)
                //{
                //	m_IntegralHeight = value;
                base.RecreateHandle();
                //}
            }
        }

        [Localizable(true), DefaultValue(13), Description("ComboBoxItemHeight"), Category("Behavior")]
        public int ItemHeight
        {
            get
            {
                return InternalList.ItemHeight;
            }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentException("InvalidArgument");//, objArray1));
                }
                //if (m_ItemHeight != value)
                //{
                //m_ItemHeight= value;
                InternalList.ItemHeight = value;
                //if (this.DrawMode != DrawMode.Normal)
                //{
                //	this.UpdateItemHeight();
                //}
                //}
            }
        }


        [DefaultValue(8), Description("ComboBoxMaxDropDownItems"), Category("Behavior"), Localizable(true)]
        public int MaxDropDownItems
        {
            get
            {
                return m_MaxDropDownItems;
            }
            set
            {
                if ((value < 1) || (value > 100))
                {
                    object[] objArray1 = new object[] { "value", value.ToString(), "1", "100" };
                    throw new ArgumentOutOfRangeException("InvalidBoundArgument");//, objArray1));
                }
                m_MaxDropDownItems = (short)value;
            }
        }

        [Category("Behavior"), Description("ComboBoxMaxLength"), Localizable(true), DefaultValue(0)]
        public int MaxLength
        {
            get
            {
                return m_MaxLength;
            }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                if (m_MaxLength != value)
                {
                    m_MaxLength = value;
                }
            }
        }

        //		[Browsable(false), Description("ComboBoxPreferredHeight"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //		public override int PreferredHeight
        //		{
        //			get
        //			{
        //				if (m_PreferredHeight > -1)
        //				{
        //					return m_PreferredHeight;
        //				}
        //				int num1 = base.FontHeight + ((SystemInformation.BorderSize.Height * 4) + 3);
        //				m_PreferredHeight = (short) num1;
        //				return num1;
        //			}
        //		}

        [Category("Behavior"), DefaultValue(false), Description("ComboBoxSorted")]
        public bool Sorted
        {
            get
            {
                return m_Sorted;
            }
            set
            {
                if (m_Sorted != value)
                {   /*sort*/
                    //if ((DataSource != null) && value)
                    //{
                    //    throw new ArgumentException("ComboBoxSortWithDataSource");
                    //}
                    m_Sorted = value;
                    ///*sort*/this.RefreshItems();
                    InternalList.Sorted = value;
                    this.SelectedIndex = -1;
                    this.Invalidate();
                }
            }
        }

  
        [Category("Behavior"), DefaultValue(false)]
        public bool AcceptItems
        {
            get { return m_AcceptItems; }
            set { m_AcceptItems = value; }
        }

        #endregion

        #region IControlKeyAction Members

        protected override bool OnEnterAction()//EnterAction action)
        {

            //			if(DroppedDown && m_ComboPopUp != null)
            //			{
            //				CloseDropDown();// m_ComboPopUp.Close();
            //			}

            this.OnKeyDown(new KeyEventArgs(Keys.Enter));
            this.OnKeyPress(new KeyPressEventArgs((char)13));

            //if(action==EnterAction.MoveNext)
            //	ActionTabNext();
            return false;
        }

        protected override bool OnInsertAction()//InsertAction action)
        {
            DoDropDown();
            return true;
        }

        protected override bool OnEscapeAction()//EscapeAction action)
        {
            this.CloseDropDown();
            return true;
        }

        public override bool IsValidating()
        {

            bool ok = true;

            if (this.AcceptItems)
            {
                int index = ((CtlListCombo)this.InternalList).Items.IndexOf(this.Text);
                if (index > -1)
                {
                    ((CtlListCombo)this.InternalList).SelectedIndex = index;
                }
                ok = (index > -1);
            }

            if (!ok)
            {
                m_IsValid = false;
                string msg = RM.GetString(RM.ContentNotExistInList);
                OnErrorOcurred(new ErrorOcurredEventArgs(msg));
            }
            else
            {
                //if(m_TextBox.Modified)
                //	m_TextBox.AppendText (Text);
                m_IsValid = true;
            }

            return ok;
        }

        [Browsable(false)]
        protected override DataTypes BaseFormat
        {
            get { return m_TextBox.BaseFormat; }
        }

        #endregion

        #region IBind Members

        public override string BindPropertyName()
        {
            return "SelectedValue";
        }
        public override void BindDefaultValue()
        {
            //if (this.DefaultValue.Length > 0)
            //{
                this.UpdateText(this.DefaultValue);
                if (base.IsHandleCreated)
                {
                    OnSelectedValueChanged(EventArgs.Empty);
                }
            //}
        }
        #endregion

        #region ComboPopUp

        public class ComboPopUp : mControl.WinCtl.Controls.CtlPopUpBase
        {
            #region Ctor and members

            /// <summary>
            /// Required designer variable.
            /// </summary>
            //private System.ComponentModel.Container components = null;
            private CtlComboBox Ctl = null;
            private object selectedItem = null;
            private int forceWidth = 0;
            internal CtlListCombo internalList;
            private bool dispose = false;


            //public event SelectionChangedEventHandler SelectionChanged = null;
            delegate void DisposeCallBack(bool disposing);
           
            /// <summary>
            /// Clean up any resources being used.
            /// </summary>
            protected override void Dispose(bool disposing)
            {
                if (dispose)
                {
                    if (internalList.InvokeRequired)
                    {
                        internalList.Invoke(new DisposeCallBack(Dispose), disposing);
                    }
                    else
                    {
                        internalList.Dispose();
                    }

                    //if (components != null)
                    //{
                    //    components.Dispose();
                    //}
                }
                base.Dispose(dispose);// disposing );
            }

            public void DisposePopUp(bool disposing)
            {
                dispose = disposing;
                this.Dispose(disposing);
            }

            #endregion

            #region Windows Form Designer generated code
            /// <summary>
            /// Required method for Designer support - do not modify
            /// the contents of this method with the code editor.
            /// </summary>
            private void InitializeComponent()
            {
                this.Name = "ComboPopUp";

            }

            private void InitializeList()
            {
                this.internalList = new CtlListCombo();
                this.SuspendLayout();
                // 
                // internalList
                // 
                //this.internalList.ownerCtl=this;
                //this.internalList.ForeColor=Color.Black;
                this.internalList.BorderStyle = BorderStyle.FixedSingle;

                this.internalList.BackColor = Ctl.BackColor;
                this.internalList.ForeColor = Ctl.ForeColor;
                this.internalList.RightToLeft = Ctl.RightToLeft;

                this.BackColor = Ctl.BackColor;

                this.internalList.Dock = System.Windows.Forms.DockStyle.Fill;
                this.internalList.IntegralHeight = false;
                //this.internalList.Location = new System.Drawing.Point(72, 7);
                this.internalList.Name = "internalList";
                //this.internalList.Size = new System.Drawing.Size(10, 4);
                this.internalList.TabIndex = 0;
                //this.internalList.Visible = false;
                //this.internalList.DrawItem+=new DrawItemEventHandler(internalList_DrawItem); 
                this.internalList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.internalList_KeyUp);
                this.internalList.SelectionChanged += new EventHandler(internalList_SelectionChanged);
                this.internalList.MouseWheel += new MouseEventHandler(internalList_MouseWheel); 
                this.Controls.Add(this.internalList);

                this.ResumeLayout(false);
            }

        

            internal void ListSettingChanged()
            {
                if (this.internalList == null) return;

                internalList.BackColor = Ctl.BackColor;
                internalList.ForeColor = Ctl.ForeColor;
                internalList.RightToLeft = Ctl.RightToLeft;

                this.BackColor = Ctl.BackColor;
                Ctl.shouldResetList = false;
            }

            #endregion

            #region Constructors

            public ComboPopUp(CtlComboBox parent)
                : base(parent)
            {
                Ctl = parent;
                parent.Controls[0].Focus();
                InitializeComponent();
                InitializeList();
            }

            public override void ShowPopUp(IntPtr hwnd)
            {

                int heightAdd = 2;

                if (Ctl.shouldResetList)
                    ListSettingChanged();

                int cnt = internalList.Items.Count;
                int visibleItems = Ctl.MaxDropDownItems;
                string selectedText = Ctl.Text;

                if (cnt == 0)
                {
                    visibleItems = 0;
                }
                if (visibleItems > cnt)
                {
                    visibleItems = cnt;
                }

                this.Height = (Ctl.ItemHeight * visibleItems) + heightAdd;

                if (Ctl.DropDownWidth == 0)
                    this.Width = Ctl.Width;
                else
                    this.Width = Ctl.DropDownWidth < Ctl.Width ? Ctl.Width : Ctl.DropDownWidth;

                if (this.Width < 112) this.forceWidth = this.Width;

                int index = internalList.FindStringExact(selectedText);
                base.ShowPopUp(hwnd);
                if (index > -1)
                {
                    internalList.SelectedIndex = index;
                    this.selectedItem = internalList.SelectedItem;
                }
                if (forceWidth > 0)
                    this.Width = forceWidth;
              
                //if (!Ctl.ReadOnly)
                //{
                //    internalList.Focus();
                //}
            }

            #endregion

            #region Events handlers

            private void internalList_SelectionChanged(object sender, EventArgs e)
            {
                OnSelectionChanged();
            }

            private void internalList_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
            {
                if (e.KeyData == Keys.Escape)
                {
                    this.Close();
                }
                else if (e.KeyData == Keys.Enter)
                {
                    OnSelectionChanged();
                }
                else if (e.KeyData == Keys.Back || e.KeyData == Keys.Delete)
                {
                    Ctl.Focus();
                }
                else if (Ctl.allowAutoComplete)
                {
                    this.selectedItem = internalList.SelectedItem;
                    Ctl.UpdateSelection(selectedItem);
                    //Ctl.UpdateText();
                }
            }

            //			public new void Close()
            //			{
            //				IntPtr hDC = WinAPI.GetWindowDC(this.Handle);
            //				WinAPI.ReleaseDC(this.Handle, hDC);
            //			}

            internal void OnSelectionChanged()
            {
                this.selectedItem = internalList.SelectedItem;
                this.Close();
            }

            void internalList_MouseWheel(object sender, MouseEventArgs e)
            {
                if (Ctl.ReadOnly)// && this.DropDownStyle == ComboBoxStyle.Simple)
                {
                    return;
                }

                if (internalList != null)
                {
                    int count = internalList.Items.Count;
                    if (count <= 0)
                        return;

                    int CurrentInt = internalList.SelectedIndex;
                    int Delta = e.Delta * SystemInformation.MouseWheelScrollLines / 120;

                    if (CurrentInt + Delta >= count)
                        internalList.SelectedIndex = count - 1;
                    else if (CurrentInt + Delta < 0)
                        internalList.SelectedIndex = 0;
                    else
                        internalList.SelectedIndex += Delta;

                    //this.Text =Items[SelectedIndex].ToString (); 
                    //this.SelectAll(); 
                }
            }

            #endregion

            #region Overrides

            protected override void OnClosed(System.EventArgs e)
            {
 
                //Ctl.internalList.KeyUp -= new System.Windows.Forms.KeyEventHandler(this.internalList_KeyUp);
                //Ctl.internalList.SelectionChanged-=new EventHandler(internalList_SelectionChanged);
                //Ctl.internalList.Visible=false; 

                //this.Controls.Remove(Ctl.internalList);
                this.Hide();
                base.OnClosed(e);
            }

            public int SearchItem(string value)
            {
                int index = internalList.FindString(value);
                if (index > -1) internalList.SelectedIndex = index;

                return index;
            }

            #endregion

            #region Properties

            public override object SelectedItem
            {
                get { return selectedItem; }
            }

            //public int SelectedIndex
            //{
            //    get { return internalList.SelectedIndex; }
            //}
            //
            //		public new Control Parent
            //		{
            //			get {return Ctl;}
            //		}

            #endregion

        }
        #endregion

        #region Format

        //private static readonly object EVENT_FORMATSTRINGCHANGED;
        // Fields
        //private CurrencyManager dataManager;
        //private object dataSource;
        //private BindingMemberInfo displayMember;
        //private TypeConverter displayMemberConverter;
        //private static readonly object EVENT_DATASOURCECHANGED = new object();
        //private static readonly object EVENT_DISPLAYMEMBERCHANGED = new object();
        //private static readonly object EVENT_FORMAT = new object();
        //private static readonly object EVENT_FORMATINFOCHANGED = new object();
        //private static readonly object EVENT_FORMATSTRINGCHANGED = new object();
        //private static readonly object EVENT_FORMATTINGENABLEDCHANGED = new object();
        //private static readonly object EVENT_SELECTEDVALUECHANGED = new object();
        //private static readonly object EVENT_VALUEMEMBERCHANGED = new object();

        //public event ListControlConvertEventHandler FormatChanged;
        //public event EventHandler FormatInfoChanged;
        //public event EventHandler FormatStringChanged;
        //public event EventHandler FormatingEnabledChanged;
  
        //private IFormatProvider formatInfo;
        //private string formatString = string.Empty;
        //private bool formattingEnabled;
        ////private bool inSetDataConnection;
        ////private bool isDataSourceInitEventHooked;
        ////private bool isDataSourceInitialized;
        ////private static TypeConverter stringTypeConverter = null;
        ////private BindingMemberInfo valueMember;



 

        //[Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), DefaultValue((string)null)]
        //public IFormatProvider FormatInfo
        //{
        //    get
        //    {
        //        return this.formatInfo;
        //    }
        //    set
        //    {
        //        if (value != this.formatInfo)
        //        {
        //            this.formatInfo = value;
        //            this.RefreshItems();
        //            this.OnFormatInfoChanged(EventArgs.Empty);
        //        }
        //    }
        //}

        //[DefaultValue(""), Editor("System.Windows.Forms.Design.FormatStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), MergableProperty(false), Description("ListControlFormatString")]
        //public string FormatString
        //{
        //    get
        //    {
        //        return this.formatString;
        //    }
        //    set
        //    {
        //        if (value == null)
        //        {
        //            value = string.Empty;
        //        }
        //        if (!value.Equals(this.formatString))
        //        {
        //            this.formatString = value;
        //            this.RefreshItems();
        //            this.OnFormatStringChanged(EventArgs.Empty);
        //        }
        //    }
        //}

        //[Description("ListControlFormattingEnabled"), DefaultValue(false)]
        //public bool FormattingEnabled
        //{
        //    get
        //    {
        //        return this.formattingEnabled;
        //    }
        //    set
        //    {
        //        if (value != this.formattingEnabled)
        //        {
        //            this.formattingEnabled = value;
        //            this.RefreshItems();
        //            this.OnFormattingEnabledChanged(EventArgs.Empty);
        //        }
        //    }
        //}
 

        //protected virtual void OnFormat(ListControlConvertEventArgs e)
        //{
        //    if (FormatChanged != null)
        //        FormatChanged(this, e);
        //    //ListControlConvertEventHandler handler = base.Events[EVENT_FORMAT] as ListControlConvertEventHandler;
        //    //if (handler != null)
        //    //{
        //    //    handler(this, e);
        //    //}
        //}

        //protected virtual void OnFormatInfoChanged(EventArgs e)
        //{
        //    if (FormatInfoChanged != null)
        //        FormatInfoChanged(this, e);
        //    //EventHandler handler = base.Events[EVENT_FORMATINFOCHANGED] as EventHandler;
        //    //if (handler != null)
        //    //{
        //    //    handler(this, e);
        //    //}
        //}

        //protected virtual void OnFormatStringChanged(EventArgs e)
        //{
        //    if (FormatStringChanged != null)
        //        FormatStringChanged(this, e);
        //    //EventHandler handler = base.Events[EVENT_FORMATSTRINGCHANGED] as EventHandler;
        //    //if (handler != null)
        //    //{
        //    //    handler(this, e);
        //    //}
        //}

        //protected virtual void OnFormattingEnabledChanged(EventArgs e)
        //{
        //    if (FormatingEnabledChanged != null)
        //        FormatingEnabledChanged(this, e);
        //    //EventHandler handler = base.Events[EVENT_FORMATTINGENABLEDCHANGED] as EventHandler;
        //    //if (handler != null)
        //    //{
        //    //    handler(this, e);
        //    //}
        //}

        #endregion

        #region Lookup
        private bool allowAutoComplete=false;
        private LookupList m_LookupView;
        private bool isSorted = false;
        private Keys m_LastKey = Keys.Space;
        private bool isInitLookupList = false;

        private bool isTextChangedInternal = false;//Used when the text is being changed by another member of the class.
        public bool isLookupDropDown = true;
        public bool isLookupOn = true; //isLookupOning can be turned on or off. No need for the whole property write out.

        private void InitLookup()
        {
            if (m_LookupView == null)
                m_LookupView = new LookupList();
        }

        [DefaultValue(false)]
        public bool AllowAutoComplete
        {
            get { return allowAutoComplete; }
            set { allowAutoComplete = value; }
        }

        private LookupList LookupView
        {
            get
            {
                if (m_LookupView == null)
                    m_LookupView = new LookupList();
                return m_LookupView;
            }
        }

        private void SetSortedInternal(bool value)
        {
            if (value && !DesignMode && !isSorted && DataSource != null)
            {
                DisplaySortSetting();
            }
        }

        private string DisplaySortSetting()
        {
            string colView = this.DisplayMember;
            DataView dv = (DataView)DataManager.List;

            if (this.DataSource != null && dv.Sort != colView)
            {
                isSorted = true;
                dv.Sort = colView;
            }
            return colView;
        }

        private void KeyDownInternal(KeyEventArgs e)
        {
            try
            {
                if (!isInitLookupList)
                    InitisLookupOnList();
                m_LastKey = e.KeyCode;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\r\nIn CtlLookUp.OnKeyDown(KeyEventArgs).");
            }
        }

        protected override void OnTextChanged(EventArgs e) //Doesn't call the base so no wiring up this event for you.
        {
            if (!allowAutoComplete)
                return;
         
            try
            {
                //Run a few checks to make sure there should be any "Lookup list" going on.
                if (isTextChangedInternal)//If the text is being changed by another member of this class do nothing
                {
                    isTextChangedInternal = false; //It will only be getting changed once internally, next time do something.
                    return;
                }
                if (!isLookupOn)
                    return;
                if (SelectionStart < this.TextInternal.Length)
                    return;
                int iOffset = 0;
                if ((m_LastKey == Keys.Back) || (m_LastKey == Keys.Delete))//Obviously we aren't going to find anything when they push Backspace or Delete
                {
                    //UpdateIndex();
                    return;
                }
                if (m_LookupView == null || this.Text.Length < 1)
                    return;

                //Put the current text into temp storage
                string sText;
                sText = this.TextInternal;
                string sOriginal = sText;
                sText = sText.ToUpper();
                int iLength = sText.Length;
                string sFound = null;
                int index = 0;
                //see if what is currently in the text box matches anything in the string list
                for (index = 0; index < m_LookupView.Count; index++)
                {
                    string sTemp = m_LookupView[index].ToUpper();
                    if (sTemp.Length >= sText.Length)
                    {
                        if (sTemp.IndexOf(sText, 0, sText.Length) > -1)
                        {
                            sFound = m_LookupView[index];
                            break;
                        }
                    }
                }
                if (sFound != null)
                {
                    isTextChangedInternal = true;
                    if (isLookupDropDown && !DroppedDown)
                    {
                        isTextChangedInternal = true;
                        string sTempText = TextInternal;
                        this.DroppedDown = true;
                        TextInternal = sTempText;
                        isTextChangedInternal = false;
                    }
                    if (this.TextInternal != sFound)
                    {
                        this.TextInternal += sFound.Substring(iLength);
                        this.SelectionStart = iLength + iOffset;
                        this.SelectionLength = this.TextInternal.Length - iLength + iOffset;
                        //SelectedIndex = index;
                        SetSelectedIndexInternal(index, false, false);
                        //base.OnSelectedIndexChanged(new EventArgs());
                    }
                    else
                    {
                        UpdateIndex();
                        this.SelectionStart = iLength;
                        this.SelectionLength = 0;
                    }
                }
                else
                {
                    isTextChangedInternal = true;
                    SelectedIndex = -1;
                    TextInternal = sOriginal;
                    isTextChangedInternal = false;
                    //base.OnSelectedIndexChanged(new EventArgs());
                    this.SelectionStart = sOriginal.Length;
                    this.SelectionLength = 0;
                }
            }
            catch (Exception)// ex)
            {
                //throw new Exception(ex.Message + "\r\nIn CtlLookUp.OnTextChanged(EventArgs).");
            }
        }

          //Put all the data from the ColumnView into a LookupList for quicker lookup.
        private void InitisLookupOnList()
        {
            InitLookup();

            m_LookupView.Clear();

            if (this.DataSource != null)
            {
                DataView dv = (DataView)DataManager.List;
                //dv.Sort = m_DisplayMember;
                foreach (DataRowView drv in dv)
                {
                    string sTemp = drv[m_DisplayMember].ToString();
                    m_LookupView.Add(sTemp);
                }
            }
            else if (this.Items != null && Items.Count > 0)
            {
                foreach (object  o in Items)
                {
                    string sTemp = o.ToString();
                    m_LookupView.Add(sTemp);
                }
            }
            else
            {
                return;
            }
            m_LookupView.Sort();
            isInitLookupList = true;
            //if (!m_Sorted)
            //{
                //DisplaySortSetting();
            //}
            //DisplayMemberSetting(m_Sorted);
            //this.DisplayMember=m_Cols[m_ColumnView].ColumnName;
        }

 
        //command the ComboBox to update its SelectedIndex.
        //This function will do that based on the current text.
        private void UpdateIndex()
        {
            try
            {

                //if(isInitItems)
                //	InitItems();
                //if (m_dv == null) return;

                if (!isInitLookupList)
                    InitisLookupOnList();
                string sText = TextInternal;
                int index = 0;

                if (this.DataSource != null)
                {
                    DataView dv = (DataView)DataManager.List;

                    for (index = 0; index < dv.Count; index++)
                    {
                        if (dv[index][DisplayMember].ToString() == sText)
                        {
                            if (SelectedIndex != index)
                            {
                                isTextChangedInternal = true;
                                //m_SelectedIndex = index;
                                SelectedIndex = index;
                                //base.OnSelectedIndexChanged(new EventArgs());
                                isTextChangedInternal = false;
                            }
                            break;
                        }
                    }
                }
                else if (this.Items != null && Items.Count > 0)
                {
                    for (index = 0; index < Items.Count; index++)
                    {
                        if (Items[index].ToString() == sText)
                        {
                            if (SelectedIndex != index)
                            {
                                isTextChangedInternal = true;
                                //m_SelectedIndex = index;
                                SelectedIndex = index;
                                //base.OnSelectedIndexChanged(new EventArgs());
                                isTextChangedInternal = false;
                            }
                            break;
                        }
                    }
                }
                else
                {
                    return;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\r\nIn CtlLookUp.UpdateIndex().");
            }
        }

        //Useful for setting the SelectedIndex to the index of a certain string.
        private int SetToIndexOf(string sText)
        {
            try
            {
                int index = 0;
                //see if what is currently in the text box matches anything in the string list
                for (index = 0; index < m_LookupView.Count; index++)
                {
                    string sTemp = m_LookupView[index].ToUpper();
                    if (sTemp == sText)
                        break;
                }
                if (index >= m_LookupView.Count)
                {
                    index = -1;
                }
                //m_SelectedIndex = index;
                SelectedIndex = index;
                //base.OnSelectedIndexChanged(new EventArgs());
                return index;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\r\nIn CtlLookUp.SetToIndexOf(string).");
            }
        }

        private int IndexOfView(string sText)
        {
            if (!isInitLookupList)
            {
                InitisLookupOnList();
            }
            return m_LookupView.IndexOf(sText);
        }

        #endregion


        #region AutoComplit

        //private void OnSelectionChangeCommittedInternal(EventArgs e)
        //{
        //    if (this.allowCommit)
        //    {
        //        try
        //        {
        //            this.allowCommit = false;
        //            this.OnSelectionChangeCommitted(e);
        //        }
        //        finally
        //        {
        //            this.allowCommit = true;
        //        }
        //    }
        //}
        //private bool InterceptAutoCompleteKeystroke(Message m)
        //{
        //    if (m.Msg == 0x100)
        //    {
        //        if (((int)m.WParam) == 0x2e)
        //        {
        //            this.MatchingText = "";
        //            this.autoCompleteTimeStamp = DateTime.Now.Ticks;
        //            if (this.Items.Count > 0)
        //            {
        //                this.SelectedIndex = 0;
        //            }
        //            return false;
        //        }
        //    }
        //    else if (m.Msg == 0x102)
        //    {
        //        string s;
        //        char c = (char)((ushort)((long)m.WParam));
        //        switch (c)
        //        {
        //            case '\b':
        //                if (((DateTime.Now.Ticks - this.autoCompleteTimeStamp) > 0x989680) || (this.MatchingText.Length <= 1))
        //                {
        //                    this.MatchingText = "";
        //                    if (this.Items.Count > 0)
        //                    {
        //                        this.SelectedIndex = 0;
        //                    }
        //                }
        //                else
        //                {
        //                    this.MatchingText = this.MatchingText.Remove(this.MatchingText.Length - 1);
        //                    this.SelectedIndex = this.FindString(this.MatchingText);
        //                }
        //                this.autoCompleteTimeStamp = DateTime.Now.Ticks;
        //                return false;

        //            case '\x001b':
        //                this.MatchingText = "";
        //                break;
        //        }
        //        if (((c != '\x001b') && (c != '\r')) && (!this.DroppedDown && (this.AutoCompleteMode != AutoCompleteMode.Append)))
        //        {
        //            this.DroppedDown = true;
        //        }
        //        if ((DateTime.Now.Ticks - this.autoCompleteTimeStamp) > 0x989680)
        //        {
        //            s = new string(c, 1);
        //            if (this.FindString(s) != -1)
        //            {
        //                this.MatchingText = s;
        //            }
        //            this.autoCompleteTimeStamp = DateTime.Now.Ticks;
        //            return false;
        //        }
        //        s = this.MatchingText + c;
        //        int num = this.FindString(s);
        //        if (num != -1)
        //        {
        //            this.MatchingText = s;
        //            if (num != this.SelectedIndex)
        //            {
        //                this.SelectedIndex = num;
        //            }
        //        }
        //        this.autoCompleteTimeStamp = DateTime.Now.Ticks;
        //        return true;
        //    }
        //    return false;
        //}
        //private string MatchingText
        //{
        //    get
        //    {
        //        string text = matchingText;// (string)base.Properties.GetObject(PropMatchingText);
        //        if (text != null)
        //        {
        //            return text;
        //        }
        //        return string.Empty;
        //    }
        //    set
        //    {
        //        if (value != null)// || base.Properties.ContainsObject(PropMatchingText))
        //        {
        //            //base.Properties.SetObject(PropMatchingText, value);
        //            matchingText = value;
        //        }
        //    }
        //}
        //private const int AutoCompleteTimeout = 0x989680;
        //private string matchingText;
  
        //private bool allowCommit;
        //private AutoCompleteStringCollection autoCompleteCustomSource;
        //private bool autoCompleteDroppedDown;
        //private AutoCompleteSource autoCompleteSource = AutoCompleteSource.None;
        //private AutoCompleteMode autoCompleteMode;
        //private long autoCompleteTimeStamp;
        ////private bool canFireLostFocus;
        ////private string currentText;
        //private StringSource stringSource;
        ////private ComboPopUp childEdit;
        //private bool fromHandleCreate;
        ////private CtlListCombo.ObjectCollection itemsCollection;
        ////private CtlListCombo.ObjectCollection ObjectCollection;

        //private string lastTextChangedValue;
 

 

        //[Description("ComboBoxAutoCompleteCustomSource"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Localizable(true)]
        //public AutoCompleteStringCollection AutoCompleteCustomSource
        //{
        //    get
        //    {
        //        if (this.autoCompleteCustomSource == null)
        //        {
        //            this.autoCompleteCustomSource = new AutoCompleteStringCollection();
        //            this.autoCompleteCustomSource.CollectionChanged += new CollectionChangeEventHandler(this.OnAutoCompleteCustomSourceChanged);
        //        }
        //        return this.autoCompleteCustomSource;
        //    }
        //    set
        //    {
        //        if (this.autoCompleteCustomSource != value)
        //        {
        //            if (this.autoCompleteCustomSource != null)
        //            {
        //                this.autoCompleteCustomSource.CollectionChanged -= new CollectionChangeEventHandler(this.OnAutoCompleteCustomSourceChanged);
        //            }
        //            this.autoCompleteCustomSource = value;
        //            if (this.autoCompleteCustomSource != null)
        //            {
        //                this.autoCompleteCustomSource.CollectionChanged += new CollectionChangeEventHandler(this.OnAutoCompleteCustomSourceChanged);
        //            }
        //            this.SetAutoComplete(false, true);
        //        }
        //    }
        //}

        //[DefaultValue(0), Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Description("ComboBoxAutoCompleteMode")]
        //public AutoCompleteMode AutoCompleteMode
        //{
        //    get
        //    {
        //        return this.autoCompleteMode;
        //    }
        //    set
        //    {
        //        if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
        //        {
        //            throw new InvalidEnumArgumentException("value", (int)value, typeof(AutoCompleteMode));
        //        }
        //        if (((this.DropDownStyle == ComboBoxStyle.DropDownList) && (this.AutoCompleteSource != AutoCompleteSource.ListItems)) && (value != AutoCompleteMode.None))
        //        {
        //            throw new NotSupportedException("ComboBoxAutoCompleteModeOnlyNoneAllowed");
        //        }
        //        if (Application.OleRequired() != System.Threading.ApartmentState.STA)
        //        {
        //            throw new System.Threading.ThreadStateException("ThreadMustBeSTA");
        //        }
        //        bool reset = false;
        //        if ((this.autoCompleteMode != AutoCompleteMode.None) && (value == AutoCompleteMode.None))
        //        {
        //            reset = true;
        //        }
        //        this.autoCompleteMode = value;
        //        this.SetAutoComplete(reset, true);
        //    }
        //}


        //[DefaultValue(0x80), Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Description("ComboBoxAutoCompleteSource")]
        //public AutoCompleteSource AutoCompleteSource
        //{
        //    get
        //    {
        //        return this.autoCompleteSource;
        //    }
        //    set
        //    {
        //        if (!ClientUtils.IsEnumValid_NotSequential(value, (int)value, new int[] { 0x80, 7, 6, 0x40, 1, 0x20, 2, 0x100, 4 }))
        //        {
        //            throw new InvalidEnumArgumentException("value", (int)value, typeof(AutoCompleteSource));
        //        }
        //        if (((this.DropDownStyle == ComboBoxStyle.DropDownList) && (this.AutoCompleteMode != AutoCompleteMode.None)) && (value != AutoCompleteSource.ListItems))
        //        {
        //            throw new NotSupportedException("ComboBoxAutoCompleteSourceOnlyListItemsAllowed");
        //        }
        //        if (Application.OleRequired() != System.Threading.ApartmentState.STA)
        //        {
        //            throw new System.Threading.ThreadStateException("ThreadMustBeSTA");
        //        }
        //        if (((value != AutoCompleteSource.None) && (value != AutoCompleteSource.CustomSource)) && (value != AutoCompleteSource.ListItems))
        //        {
        //            FileIOPermission permission = new FileIOPermission(PermissionState.Unrestricted);
        //            permission.AllFiles = FileIOPermissionAccess.PathDiscovery;
        //            permission.Demand();
        //        }
        //        this.autoCompleteSource = value;
        //        this.SetAutoComplete(false, true);
        //    }
        //}

        //private void OnAutoCompleteCustomSourceChanged(object sender, CollectionChangeEventArgs e)
        //{
        //    if (this.AutoCompleteSource == AutoCompleteSource.CustomSource)
        //    {
        //        if (this.AutoCompleteCustomSource.Count == 0)
        //        {
        //            this.SetAutoComplete(true, true);
        //        }
        //        else
        //        {
        //            this.SetAutoComplete(true, false);
        //        }
        //    }
        //}
        //private void SetAutoComplete(bool reset, bool recreate)
        //{
        //    if (!base.IsHandleCreated)
        //    {
        //        return;//- goto Label_0010;

        //    }
        //    //if (this.childEdit == null)
        //    //{
        //    //}
        //    if (this.AutoCompleteMode != AutoCompleteMode.None)
        //    {
        //        if ((!this.fromHandleCreate && recreate) && base.IsHandleCreated)
        //        {
        //            AutoCompleteMode autoCompleteMode = this.AutoCompleteMode;
        //            this.autoCompleteMode = AutoCompleteMode.None;
        //            base.RecreateHandle();
        //            this.autoCompleteMode = autoCompleteMode;
        //        }
        //        if (this.AutoCompleteSource == AutoCompleteSource.CustomSource)
        //        {
        //            if (this.AutoCompleteCustomSource != null)
        //            {
        //                if (this.AutoCompleteCustomSource.Count == 0)
        //                {
        //                    int flags = -1610612736;
        //                    SHAutoComplete(new HandleRef(this, this.InternalList.Handle), flags);
        //                }
        //                else if (this.stringSource != null)
        //                {
        //                    this.stringSource.RefreshList(this.GetStringsForAutoComplete(this.AutoCompleteCustomSource));
        //                }
        //                else
        //                {
        //                    this.stringSource = new StringSource(this.GetStringsForAutoComplete(this.AutoCompleteCustomSource));
        //                    if (!this.stringSource.Bind(new HandleRef(this, this.InternalList.Handle), (int)this.AutoCompleteMode))
        //                    {
        //                        throw new ArgumentException("AutoCompleteFailure");
        //                    }
        //                }
        //            }
        //        }
        //        else if (this.AutoCompleteSource == AutoCompleteSource.ListItems)
        //        {
        //            if (this.DropDownStyle == ComboBoxStyle.DropDownList)
        //            {
        //                int flags = -1610612736;
        //                SHAutoComplete(new HandleRef(this, this.InternalList.Handle), flags);
        //            }
        //            else if (this.Items != null)
        //            {
        //                if (this.Items.Count == 0)
        //                {
        //                    int flags = -1610612736;
        //                    SHAutoComplete(new HandleRef(this, this.InternalList.Handle), flags);
        //                }
        //                else if (this.stringSource != null)
        //                {
        //                    this.stringSource.RefreshList(this.GetStringsForAutoComplete(this.Items));
        //                }
        //                else
        //                {
        //                    this.stringSource = new StringSource(this.GetStringsForAutoComplete(this.Items));
        //                    if (!this.stringSource.Bind(new HandleRef(this, this.InternalList.Handle), (int)this.AutoCompleteMode))
        //                    {
        //                        throw new ArgumentException("AutoCompleteFailureListItems");
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            try
        //            {
        //                int num4 = 0;
        //                if (this.AutoCompleteMode == AutoCompleteMode.Suggest)
        //                {
        //                    num4 |= -1879048192;
        //                }
        //                if (this.AutoCompleteMode == AutoCompleteMode.Append)
        //                {
        //                    num4 |= 0x60000000;
        //                }
        //                if (this.AutoCompleteMode == AutoCompleteMode.SuggestAppend)
        //                {
        //                    num4 |= 0x10000000;
        //                    num4 |= 0x40000000;
        //                }
        //                SHAutoComplete(new HandleRef(this, this.InternalList.Handle), ((int)this.AutoCompleteSource) | num4);
        //            }
        //            catch (System.Security.SecurityException)
        //            {
        //            }
        //        }
        //    }
        //    else if (reset)
        //    {
        //        int flags = -1610612736;
        //        SHAutoComplete(new HandleRef(this, this.InternalList.Handle), flags);
        //    }
        //}

        //private string[] GetStringsForAutoComplete(IList collection)
        //{
        //    if (collection is AutoCompleteStringCollection)
        //    {
        //        string[] textArray = new string[this.AutoCompleteCustomSource.Count];
        //        for (int i = 0; i < this.AutoCompleteCustomSource.Count; i++)
        //        {
        //            textArray[i] = this.AutoCompleteCustomSource[i];
        //        }
        //        return textArray;
        //    }
        //    if (!(collection is CtlListCombo.ObjectCollection))
        //    {
        //        return new string[0];
        //    }
        //    string[] textArray2 = new string[this.Items.Count];
        //    for (int i = 0; i < this.Items.Count; i++)
        //    {
        //        textArray2[i] = GetItemText(this.Items[i]);
        //    }
        //    return textArray2;
        //}

        //[DllImport("shlwapi.dll")]
        //public static extern int SHAutoComplete(HandleRef hwndEdit, int flags);

        //private void NotifyAutoComplete()
        //{
        //    this.NotifyAutoComplete(true);
        //}

        //private void NotifyAutoComplete(bool setSelectedIndex)
        //{
        //    string text = this.Text;
        //    bool flag = text != this.lastTextChangedValue;
        //    bool flag2 = false;
        //    if (setSelectedIndex)
        //    {
        //        int num = this.FindStringIgnoreCase(text);
        //        if ((num != -1) && (num != this.SelectedIndex))
        //        {
        //            this.SelectedIndex = num;
        //            this.SelectionStart = 0;
        //            this.SelectionLength = text.Length;
        //            flag2 = true;
        //        }
        //    }
        //    if (flag && !flag2)
        //    {
        //        this.OnTextChanged(EventArgs.Empty);
        //    }
        //    this.lastTextChangedValue = text;
        //}

        //private int FindStringIgnoreCase(string value)
        //{
        //    int num = this.FindStringExact(value, -1, false);
        //    if (num == -1)
        //    {
        //        num = this.FindStringExact(value, -1, true);
        //    }
        //    return num;
        //}

        //internal int FindStringExact(string s, int startIndex, bool ignorecase)
        //{
        //    if (s == null)
        //    {
        //        return -1;
        //    }
        //    if ((this.Items == null) || (this.Items.Count == 0))
        //    {
        //        return -1;
        //    }
        //    if ((startIndex < -1) || (startIndex >= this.Items.Count))
        //    {
        //        throw new ArgumentOutOfRangeException("startIndex");
        //    }
        //    return FindStringInternal(s, this.Items, startIndex, true, ignorecase);
        //}

        //internal int FindStringInternal(string str, IList items, int startIndex, bool exact, bool ignorecase)
        //{
        //    if ((str != null) && (items != null))
        //    {
        //        if ((startIndex < -1) || (startIndex >= items.Count))
        //        {
        //            return -1;
        //        }
        //        bool flag = false;
        //        int length = str.Length;
        //        int num2 = 0;
        //        for (int i = (startIndex + 1) % items.Count; num2 < items.Count; i = (i + 1) % items.Count)
        //        {
        //            num2++;
        //            if (exact)
        //            {
        //                flag = string.Compare(str, this.GetItemText(items[i]), ignorecase, CultureInfo.CurrentCulture) == 0;
        //            }
        //            else
        //            {
        //                flag = string.Compare(str, 0, this.GetItemText(items[i]), 0, length, ignorecase, CultureInfo.CurrentCulture) == 0;
        //            }
        //            if (flag)
        //            {
        //                return i;
        //            }
        //        }
        //    }
        //    return -1;
        //}


        //private bool SystemAutoCompleteEnabled
        //{
        //    get
        //    {
        //        if (this.autoCompleteMode != AutoCompleteMode.None)
        //        {
        //            return (this.DropDownStyle != ComboBoxStyle.DropDownList);
        //        }
        //        return false;
        //    }
        //}
 

 

 


 


 


 

        #endregion
    }
}