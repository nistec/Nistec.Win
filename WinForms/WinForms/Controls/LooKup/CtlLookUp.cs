using System;
using System.Data;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using System.Drawing.Design;
using System.Collections;

using Nistec.Win32;
using Nistec.Data;

using Nistec.Collections;
using Nistec.WinForms.Controls;
using Nistec.Win;



namespace Nistec.WinForms
{

	[Designer(typeof(Design.McEditDesigner))]
	[DefaultEvent("SelectedIndexChanged"),ToolboxItem(true)]
	[ToolboxBitmap (typeof(McComboBox), "Toolbox.Lookup_edit.bmp")]
	public class McLookUp : McComboBase,IDropDown,IMcLookUp //,IComboList
	{
		
		#region Base Members
		private System.ComponentModel.IContainer components = null;

		//Combo members
		private int							m_DropDownWidth;
		private  int						m_MaxDropDownItems;
		private int							m_SelectedIndex;
		private string						m_DisplayMember;
		private string						m_ValueMember;

		protected LookUpPopUp	m_ComboPopUp;

		internal McColumnCollection m_Cols=null;
		//internal int m_ColumnView=0;
		//internal int m_ColumnValue=0;
		internal bool m_ShowHeaders=false;

		private object m_SelectedValue;
		private int m_ColumnImage=-1;
		private ImageList m_ImageList=null;
	
		//disply
		private string columnDisplay="";
		private int[] m_ColWidths =null;
		private string[]m_ColCaption=null;

		//private LookupList m_LookupList = new LookupList();
		private LookupList m_LookupValue = new LookupList();
		private LookupList m_LookupView = new LookupList();
		private bool m_Sorted=false;
		private bool isSorted=false;
		

		private Keys m_LastKey = Keys.Space; //Last key pressed.
		public int ColumnSpacing = 4; //Minimum spacing between columns. Don't go crazy with this...
		private DataView m_dv = null;
		private bool isInitLookupList = false;
		private bool isInitLookupValue = false;
 
		private bool isTextChangedInternal = false;//Used when the text is being changed by another member of the class.
		public bool isLookupDropDown = true;
		public bool isLookupOn = true; //isLookupOning can be turned on or off. No need for the whole property write out.

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

		#endregion

		#region Constructors
		
		public McLookUp()
		{
			if(components == null)
				components = null;
			m_SelectedIndex= -1;
			m_DisplayMember="";
			m_ValueMember="";
			m_MaxDropDownItems  = 8;
	
			InitComboPopUp();
			this.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
			InitColumnCollection();
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated (e);
			if(Sorted && !isSorted && m_dv!=null)
			{
				DisplaySortSetting();
			}

//			if(isInitItems)
//				InitItems();
//			if(isInitAutoDisplay)
//				InitAutoDisplay();
		}

		#endregion

		#region Dispose

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			if(m_ComboPopUp!=null)
			{
				m_ComboPopUp.Closed -= new System.EventHandler(this.OnPopupClosed);
				m_ComboPopUp.internalList.DrawItem-=new DrawItemEventHandler(internalList_DrawItem);

				m_ComboPopUp.DisposePopUp(true);
			}
			base.Dispose( disposing );
		}

		#endregion

		#region Component Designer generated code

		protected virtual void InitComboPopUp()
		{
			m_ComboPopUp=new LookUpPopUp(this);
			m_ComboPopUp.Closed += new System.EventHandler(this.OnPopupClosed);
			m_ComboPopUp.internalList.DrawItem+=new DrawItemEventHandler(internalList_DrawItem);
		}

		private void InitializeComponent()
		{
			// 
			// McLookUp
			// 
		}
		#endregion

		#region Virtual Events

		protected virtual void OnSelectedValueChanged(EventArgs e)
		{
			if (this.SelectedValueChanged!=null)
				this.SelectedValueChanged(this, e);
		}

		protected virtual void OnSelectedIndexChanged(EventArgs e)
		{
			if (this.SelectedIndexChanged !=null)
				this.SelectedIndexChanged(this, e);
		}

		protected virtual void OnDataSourceChanged(EventArgs e)
		{
//			if ((this.Sorted && (DataSource != null)) && base.Created)
//			{
//				DataSource = null;
//				throw new Exception("ComboBoxDataSourceWithSort");
//			}
			if (DataSource == null)
			{
				//this.BeginUpdate();
				this.SelectedIndex = -1;
				this.ClearInternal();
				//this.EndUpdate();
			}
			InternalList.DataSource =DataSource;  
			//this.RefreshItems();


			if(this.DataSourceChanged!=null)
				this.DataSourceChanged(this,e);
		}


		protected virtual void OnDisplayMemberChanged(EventArgs e)
		{
			if(this.DisplayMemberChanged!=null)
				this.DisplayMemberChanged(this,e);
		}
 
		protected virtual void OnValueMemberChanged(EventArgs e)
		{
			if(this.ValueMemberChanged!=null)
				this.ValueMemberChanged(this,e);
		}

		protected virtual void OnSelectionChangeCommitted(EventArgs e)
		{
			if(SelectionChangeCommitted!=null)
				this.SelectionChangeCommitted(this,e);
		}

		private void internalList_DrawItem(object sender, DrawItemEventArgs e)
		{
			OnDrawItem(e);
		}

//		private DrawItemEventHandler GetDrawItemHandler()
//		{
//			FieldInfo info1 = typeof(System.Windows.Forms.ListBox).GetField("EVENT_DRAWITEM", BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Static);
//			return (DrawItemEventHandler) base.Events[info1.GetValue(null)];
//		}

        protected override bool OnInsertAction()
        {
            DoDropDown();
            return false;
        }
		#endregion

		#region Combo methods

		internal virtual void SetSelectedIndexInternal(int value,bool checkItems)
		{
			SetSelectedIndexInternal(value,checkItems,true);
		}

		internal virtual void SetSelectedIndexInternal(int value,bool checkItems,bool updateText)
		{
			if (SelectedIndex != value)
			{
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
				if(checkItems)
				{
					object obj =InternalList.Items[value];
					if(obj==null)
					{
						return;
					}
					InternalList.SelectedItem =obj;//InternalList.Items[value];
					//InternalList.SelectedIndex=value;
				}
				else
				{
					InternalList.SelectedIndex=value;
				}

				m_SelectedIndex = value;
				if(updateText)
				{
					this.UpdateText();
			
					if (base.IsHandleCreated)
					{
						this.OnTextChanged(EventArgs.Empty);
						this.OnSelectionChangeCommitted(EventArgs.Empty);
					}
					this.OnSelectedValueChanged(EventArgs.Empty);
					this.OnSelectedIndexChanged(EventArgs.Empty);
				}
			}
		}

		protected void UpdateText()
		{
			string text1 =base.DefaultValue;// "";
			int indx=SelectedIndex;
			if (indx != -1)
			{
				object obj1 = InternalList.Items[indx];
				if (obj1 != null)
				{
					text1 =GetItemText(obj1);
				}
			}
			base.Text = text1;
			this.m_TextBox.SelectAll(); 
		}
	

		protected virtual string GetItemText(object item)
		{
			if(m_Cols!=null)
			{
			item = ((DataRowView)item).Row[m_DisplayMember].ToString();
			}
			if (item is DataRowView) 
			{
				item = ((DataRowView)item).Row[DisplayMember.Substring(DisplayMember.IndexOf(".")+1)];
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


		private void UpdateSelectionInternal(object value,int index)
		{
			object item=null;
			if(value!=null)
			{
				item = ((DataRowView)value).Row[m_DisplayMember].ToString();
				object obj = ((DataRowView)value).Row[m_ValueMember];
				if (obj !=null) 
				{
					
					SelectedValue=obj;
					this.SelectedIndex = index;//IndexOfValue(obj.ToString());
					//SelectedValue = obj;
				}
			}
		}

		protected virtual void UpdateSelection(object value)
		{
			object item=null;
			if(value!=null)
			{
				item = ((DataRowView)value).Row[m_DisplayMember].ToString();
				object obj = ((DataRowView)value).Row[m_ValueMember];
				if (obj !=null) 
				{
					
					SelectedValue=obj;
					this.SelectedIndex =IndexOfValue(obj.ToString());
				}
			}
		}

		#endregion

		#region PopUp

		private void OnPopupClosed(object sender,System.EventArgs e)
		{
			base.EndHook();
			UpdateSelectionInternal(m_ComboPopUp.SelectedItem,m_ComboPopUp.SelectedIndex);
			m_ComboPopUp.DisposePopUp(false);
			//m_ComboPopUp=null;
			Invalidate(false);
		}

		protected virtual void OnDropDown(EventArgs e)
		{
			if(DropDownOcurred!=null)
				this.DropDownOcurred(this,e);
			if(filterMode!=LookupFilterMode.None)
			{
				SetFilterBound();
			}
		}

		protected virtual void ShowPopUp()
		{
			this.OnDropDown(new System.EventArgs());
				
			Point pt = new Point(this.Left,this.Bottom+1 );
			//m_ComboPopUp = new LookUpPopUp(this);
			m_ComboPopUp.Location = this.Parent.PointToScreen(pt);
			//m_ComboPopUp.Closed += new System.EventHandler(this.OnPopupClosed);
			m_ComboPopUp.ShowPopUp(m_ComboPopUp.Handle);//.ShowPopUp(m_ComboPopUp.Handle,4);
			m_ComboPopUp.Start = true;
			//DroppedDown = true;
			base.StartHook() ; 
		}

		public override void DoDropDown()
		{
			if(this.ReadOnly)
			{
				return; 
			}

			if(DroppedDown)
			{
				m_ComboPopUp.Close();
				return;
			}	
			else
			{
				ShowPopUp();
			}
			//base.DoDropDown ();
		}

		public override void CloseDropDown()
		{
			if(DroppedDown)
			{
				m_ComboPopUp.Close();
				return;
			}	
		}

		protected override bool GetMouseHook(IntPtr mh,IntPtr wparam)
		{
			if(!DroppedDown)
				return false;

			if(mh == m_ComboPopUp.Handle || mh == InternalList.Handle || mh ==InternalButton.Handle)
			{
				return false;
			}
//			if(this.DropDownStyle==ComboBoxStyle.DropDownList)
//			{
//				if(mh == this.Handle)
//					return false;
//			}
			if(mh == Handle || mh ==m_TextBox.Handle)
			{
				if(wparam==(IntPtr)513 && DroppedDown )
				{
					m_ComboPopUp.Close();
					return false;
				}
				return true;
			}
			if(wparam==(IntPtr)513 && DroppedDown )
			{
				m_ComboPopUp.Close();
				return false;
			}
			return true;
		}
	 
		#endregion

		#region Filter

		private LookupFilterMode filterMode;
		private string filter="";
		private string controlBoundName="";
		private Control controlFilterBound=null;

		[Category("Filter"),DefaultValue(LookupFilterMode.None)]
		public LookupFilterMode LookupFilterMode
		{
			get {return this.filterMode;}
			set{this.filterMode=value;}
		}

		[Category("Filter"),DefaultValue("")]
		public string RowFilter
		{
			get {return this.filter;}
			set{this.filter=value;}
		}

		[Category("Filter"),DefaultValue("")]
		public string ControlBoundName
		{
			get {return this.controlBoundName;}
			set{this.controlBoundName=value;}
		}

		[Category("Filter"),DefaultValue(null)]
		public Control ControlFilterBound
		{
			get {return this.controlFilterBound;}
			set{this.controlFilterBound=value;}
		}


		public void SetFilterBound()
		{
			string strFilter="";
			try
			{
				if(filter.Length>0)
				{
					strFilter=this.filter;
				}
				else
				{
					if(ControlFilterBound==null)
					{
						MsgBox.ShowWarning("Invalid ControlFilterBound");
						return;
					}
					if(controlBoundName=="")
					{
						MsgBox.ShowWarning("Invalid ControlBoundName");
						return;
					}
					string val=ControlFilterBound.Text;
                    if (WinHelp.IsNumber(val))
					{
						strFilter=controlBoundName + "=" + val ;
					}
					else
					{
						strFilter=controlBoundName + "='" + val + "'";
					}
				}
				DataView dv=this.DataSource;
				dv.RowFilter=strFilter;
				this.DataSource=dv;
			}
			catch(Exception ex)
			{
				MsgBox.ShowWarning(ex.Message);
			}
		}

		#endregion

		#region combo properties

		public string ColumnDisplay
		{
			get{return columnDisplay;}
			set
			{
				if(value==null || value=="")
				{
					columnDisplay="";
					return;
				}
				LookupColumnDisply lcd=new LookupColumnDisply();
				if(lcd.GetColumnsCaptionWidth(value,false)>0)//if(ParseColumnsWidth(value)!=null)
				{
					columnDisplay=value;
				}
			}
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Bindable(true), Description("ListControlSelectedValue"), Browsable(false), DefaultValue((string) null), Category("Data")]
		public virtual object SelectedValue
		{
			get
			{
				return m_SelectedValue;
			}
			set
			{
				if(value == System.DBNull.Value || value.Equals(null) || value.Equals(""))
				{
					SelectedIndex = -1;
					UpdateText();
					return;
				}

				if (InternalList != null)
				{
					//if (m_ColumnValue <0 || m_ColumnValue> m_Cols.Count)
					//{
					//	throw new Exception("Incorrect ColumnValue");
					//}
					//ValueMemberSetting();
					this.SelectedIndex =IndexOfValue(value.ToString());
					//int index =m_dv.Find(value);
					//this.SelectedIndex = index;
					m_SelectedValue =value; 
				}
			}
		}

		[Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), Category("Data"), TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), DefaultValue(""), Description("ListControlDisplayMember")]
		public string DisplayMember
		{
			get{ return m_DisplayMember; }
			set
			{

				try
				{
					if (value == null)
					{
						value = "";
					}
					if (!value.Equals(m_DisplayMember))
					{
						if (((m_dv != null) && !"".Equals(value)) && !m_dv.Table.Columns.Contains(value))
						{
							throw new ArgumentException("ListControlWrongDisplayMember",value);
						}

						//this.SetDataConnection(m_DataSource, value, false);
						m_DisplayMember = value;
						InternalList.DisplayMember = m_DisplayMember; 
						isSorted=false;
						if(m_Sorted)
						{
							DisplaySortSetting();
						}
						isInitLookupList=false;
						OnDisplayMemberChanged(EventArgs.Empty);
					}
				}
				catch (Exception)
				{
					m_DisplayMember = "";
					InternalList.DisplayMember = ""; 
				}
			}
		}

		[DefaultValue(""), Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), Category("Data"), Description("ListControlValueMember")]
		public string ValueMember
		{
			get{ return m_ValueMember; }
			set
			{
			
				if (value == null)
				{
					value = "";
				}
				if (!value.Equals(m_ValueMember))
				{
					//if (m_DisplayMember.Length == 0)
					//{
					//	this.SetDataConnection(m_DataSource, value, false);
					//}
					if (((m_dv != null) && !"".Equals(value)) && !m_dv.Table.Columns.Contains(value))
					{
						throw new ArgumentException("ListControlWrongValueMember",value);
					}
					m_ValueMember = value;
					InternalList.ValueMember = value; 
					isInitLookupValue=false;
					this.OnValueMemberChanged(EventArgs.Empty);
					this.OnSelectedValueChanged(EventArgs.Empty);
				}
			}
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
				SetSelectedIndexInternal(value,true);
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
				return  null;//itemMember; 
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

		#endregion

		#region Combo Hide Property

//		[Browsable(false),EditorBrowsable(EditorBrowsableState.Never)]
//		public new string DisplayMember
//		{
//			get{ return base.DisplayMember; }
//			set
//			{
//				base.DisplayMember=value;
//			}
//		}
//
//		[Browsable(false),EditorBrowsable(EditorBrowsableState.Never)]
//		public new string ValueMember
//		{
//			get{ return base.ValueMember; }
//			set
//			{
//				base.ValueMember=value;			
//			}
//		}

//		[Bindable(true)]
//		public new string Text
//		{
//			get{ return base.TextInternal; }
//			set
//			{
//				base.TextInternal=value;			
//			}
//		}

		[Browsable(false)]
		internal McListItems InternalList
		{
			get 
			{
				if(m_ComboPopUp==null)
					m_ComboPopUp=new LookUpPopUp(this);
				return  m_ComboPopUp.internalList;
			}
		}

		[Browsable(false),EditorBrowsable(EditorBrowsableState.Never)]
		public  McListItems.ObjectCollection Items
		{

			get {return InternalList.Items;}
			//set{ internalList.Items.AddRange(value); }
		}

 
		[Browsable(false),EditorBrowsable(EditorBrowsableState.Never)]
		public DrawMode DrawMode
		{
			get
			{
				return this.InternalList.DrawMode;// m_DrawMode;
			}
			set
			{
				this.InternalList.DrawMode=value;
			}
		}
 
		[Browsable(false),EditorBrowsable(EditorBrowsableState.Never)]
		public bool IntegralHeight
		{
			get
			{
				return InternalList.IntegralHeight;
			}
			set
			{
				InternalList.IntegralHeight=value;
			}
		}
 	
		[Localizable(true), Description("ComboBoxItemHeight"), Category("Behavior")]
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
				InternalList.ItemHeight=value;
			}
		}

		[Description("ComboBoxDropDownWidth"), Category("Behavior")]
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
					throw new ArgumentException("InvalidArgument ",value.ToString());//, objArray1));
				}
				if (DropDownWidth != value)
				{
					m_DropDownWidth=value;
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
					object[] objArray1 = new object[] { "value", value.ToString(), "1", "100" } ;
					throw new ArgumentOutOfRangeException("InvalidBoundArgument");//, objArray1));
				}
				m_MaxDropDownItems = (short) value;
			}
		}
		#endregion
		
		#region override 

		protected override void OnKeyDown(KeyEventArgs e)
		{
			if(InternalList !=null)
			{
	
				int CurrentInt = SelectedIndex ; 

				if (e.Shift  && e.KeyCode == Keys.Down)
				{
					DoDropDown(); //ShowPopUp();
				}
				else if (((e.Shift  && e.KeyCode == Keys.Up) || e.KeyCode == Keys.Escape) && (m_ComboPopUp!=null))
				{
					CloseDropDown();
				}
			
				else if (e.KeyCode ==System.Windows.Forms.Keys.Down )
				{
					if(this.ReadOnly)
					{
						return; 
					}
					if (Items.Count==0)
						return;
					if (CurrentInt >= Items.Count-1)    
						SelectedIndex =Items.Count-1;
					else
						SelectedIndex +=1;
					this.SelectionStart=0;
					//this.Text =Items[SelectedIndex].ToString (); 
					//this.SelectAll(); 
				}
				else if (e.KeyCode ==System.Windows.Forms.Keys.Up  )
				{
					if(this.ReadOnly )
					{
						return; 
					}
					if (CurrentInt >0)   
					{
						SelectedIndex -=1;
						//this.Text =Items[SelectedIndex].ToString ();
						//this.SelectAll(); 
					}
					this.SelectionStart=0;
				}
				else
				{
					try
					{
						if(!isInitLookupList)
							InitisLookupOnList();
						//base.OnKeyDown (e);
						m_LastKey = e.KeyCode;
					}
					catch(Exception ex)
					{
						throw new Exception(ex.Message + "\r\nIn McLookUp.OnKeyDown(KeyEventArgs).");
					}
				}
			}		
			base.OnKeyDown(e);

		}

		protected override void OnMouseWheel(System.Windows.Forms.MouseEventArgs e)
		{	
			base.OnMouseWheel (e);
			if(this.ReadOnly)
			{
				return; 
			}

			if(InternalList !=null)
			{
				if(Items.Count<=0)
					return;

				int CurrentInt = SelectedIndex ; 
				int Delta = e.Delta * SystemInformation.MouseWheelScrollLines / 120;
		    
				if (CurrentInt+Delta >= Items.Count )
					SelectedIndex =Items.Count-1;
				else if (CurrentInt+Delta < 0 )
					SelectedIndex =0;
				else
					SelectedIndex +=Delta;
			
				//this.Text =Items[SelectedIndex].ToString (); 
				//this.SelectAll(); 
			}
		}

        protected override void OnTextChanged(EventArgs e) //Doesn't call the base so no wiring up this event for you.
        {
            //Run a few checks to make sure there should be any "Lookup list" going on.
            if (isTextChangedInternal)//If the text is being changed by another member of this class do nothing
            {
                isTextChangedInternal = false; //It will only be getting changed once internally, next time do something.
                return;
            }
            if (!isLookupOn)
                return;
            if (SelectionStart < this.Text.Length)
                return;
            if ((m_LastKey == Keys.Back) || (m_LastKey == Keys.Delete))//Obviously we aren't going to find anything when they push Backspace or Delete
            {
                UpdateIndex();
                return;
            }
            if (m_LookupView == null || this.Text.Length < 1)
                return;

            LookupInternal();

        }
  
        public void Lookup(string text) //Doesn't call the base so no wiring up this event for you.
        {
            if (!isLookupOn)
                return;
            if (m_LookupView == null || text.Length < 1)
                return;
            this.Text = text;
            LookupInternal();
        }

        private void LookupInternal() //Doesn't call the base so no wiring up this event for you.
		{
			try
			{
                ////Run a few checks to make sure there should be any "Lookup list" going on.
                //if(isTextChangedInternal)//If the text is being changed by another member of this class do nothing
                //{
                //    isTextChangedInternal = false; //It will only be getting changed once internally, next time do something.
                //    return;
                //}
                //if(!isLookupOn)
                //    return;
                //if(SelectionStart < this.Text.Length)
                //    return;
                //if((m_LastKey == Keys.Back) || (m_LastKey == Keys.Delete))//Obviously we aren't going to find anything when they push Backspace or Delete
                //{
                //    UpdateIndex();
                //    return;
                //}
                //if(m_LookupView == null || this.Text.Length < 1)
                //    return;

                int iOffset = 0;

				//Put the current text into temp storage
				string sText;
				sText = this.Text;
				string sOriginal = sText;
				sText = sText.ToUpper();
				int iLength = sText.Length;
				string sFound = null;
				int index = 0;
				//see if what is currently in the text box matches anything in the string list
				for(index = 0; index < m_LookupView.Count; index++)
				{
					string sTemp = m_LookupView[index].ToUpper();
					if(sTemp.Length >= sText.Length)
					{
						if(sTemp.IndexOf(sText, 0, sText.Length) > -1)
						{
							sFound = m_LookupView[index];
							break;
						}
					}
				}
				if(sFound != null)
				{
					isTextChangedInternal = true;
					if(isLookupDropDown && !DroppedDown )
					{
						isTextChangedInternal = true;
						string sTempText = Text;
						this.DroppedDown = true;
						Text = sTempText;
						isTextChangedInternal = false;
					}
					if(this.Text != sFound)
					{	
						this.Text += sFound.Substring(iLength);
						this.SelectionStart = iLength + iOffset;
						this.SelectionLength = this.Text.Length - iLength + iOffset;
						//SelectedIndex = index;
						SetSelectedIndexInternal(index,false,false);
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
					Text = sOriginal;
					isTextChangedInternal = false;
					//base.OnSelectedIndexChanged(new EventArgs());
					this.SelectionStart = sOriginal.Length;
					this.SelectionLength = 0;
				}
			}
			catch(Exception)// ex)
			{
				//throw new Exception(ex.Message + "\r\nIn McLookUp.OnTextChanged(EventArgs).");
			}
		}


		#endregion

		#region Inits

		private void m_Columns_CollectionChanged(object sender, EventArgs e)
		{
			SetDropDownWidth();
		}

		private void SetDropDownWidth()
		{
			int dropDownWidth=0;
			int w=0;

			for(int index = 0; index < m_Cols.Count; index++)
			{
				if(m_Cols[index].Display)
				{

					w=m_Cols[index].Width;
					if(w>-1)
					{
						dropDownWidth += w;
					}
				}
			}

			DropDownWidth = dropDownWidth+16;//Another nice magic number to represent the vertical scroll bar width

		}

		private void InitColumnCollection()
		{
			if(m_Cols!=null)
			{
				m_Cols.Clear();
				m_Cols.CollectionChanged-=new EventHandler(m_Columns_CollectionChanged);
			}
			m_Cols=new McColumnCollection(this);
			m_Cols.CollectionChanged+=new EventHandler(m_Columns_CollectionChanged);

		}

		//Put all the data from the ColumnView into a LookupList for quicker lookup.
		private void InitisLookupOnList()
		{
            if (m_dv == null) return;

			m_LookupView.Clear();
			foreach(DataRowView drv in m_dv)
			{
				string sTemp = drv[m_DisplayMember].ToString();
				m_LookupView.Add(sTemp);
			}
			m_LookupView.Sort();
			isInitLookupList=true;
			if(m_Sorted)
			{
				DisplaySortSetting();
			}
			//DisplayMemberSetting(m_Sorted);
			//this.DisplayMember=m_Cols[m_ColumnView].ColumnName;
		}

		private void InitisLookupValue()
		{
            if (m_dv == null) return;
            m_LookupValue.Clear();
			foreach(DataRowView drv in m_dv)
			{
				string sTemp = drv[m_ValueMember].ToString();
				m_LookupValue.Add(sTemp);
			}
			m_LookupValue.Sort();
			isInitLookupValue=true;
			//DisplayValueSetting();
			this.ValueMember=m_Cols[m_ValueMember].ColumnName;

		}

		//command the ComboBox to update its SelectedIndex.
		//This function will do that based on the current text.
		public void UpdateIndex()
		{
			try
			{

				//if(isInitItems)
				//	InitItems();
                if (m_dv == null) return;

				if(!isInitLookupList)
					InitisLookupOnList();
				string sText = Text;
				int index = 0;
				for(index = 0; index < m_dv.Count; index++)
				{
					if(m_dv[index][DisplayMember].ToString() == sText)
					{
						if(SelectedIndex != index)
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
				if(index >= m_dv.Count)
				{
					isTextChangedInternal = true;
					//m_SelectedIndex = -1;
					SelectedIndex = -1;
					//base.OnSelectedIndexChanged(new EventArgs());
					isTextChangedInternal = false;
				}
			} 
			catch(Exception ex)
			{
				throw new Exception(ex.Message + "\r\nIn McLookUp.UpdateIndex().");
			}
		}

//		//Useful for setting the SelectedIndex to the index of a certain string.
//		public int SetToIndexOf(string sText)
//		{
//			try
//			{
//				int index = 0;
//				//see if what is currently in the text box matches anything in the string list
//				for(index = 0; index < m_LookupList.Count; index++)
//				{
//					string sTemp = m_LookupList[index].ToUpper();
//					if(sTemp == sText)
//						break;
//				}
//				if(index >= m_LookupList.Count)
//				{
//					index = -1;
//				}
//				//m_SelectedIndex = index;
//				SelectedIndex = index;
//				//base.OnSelectedIndexChanged(new EventArgs());
//				return index;
//			}
//			catch(Exception ex)
//			{
//				throw new Exception(ex.Message + "\r\nIn McLookUp.SetToIndexOf(string).");
//			}
//		}

		public int IndexOfValue(string sText)
		{
			if(!isInitLookupValue)
			{
				InitisLookupValue();
			}
			return m_LookupValue.IndexOf(sText);
		}

		public int IndexOfView(string sText)
		{
			if(!isInitLookupList)
			{
				InitisLookupOnList();
			}
			return m_LookupView.IndexOf(sText);
		}

		#endregion

		#region Auto Fill

		private int performColumnWidth=80;
		private bool autoFill=true;
		private bool allowSort=true;
		//private int activeColumnSort=-1;

		private int[] m_ColumnsWidth;
		private int[] m_SortOrders;


		[Category("Columns"),DefaultValue(true) ]
		public bool AutoFill
		{
			get{return this.autoFill;}
			set{this.autoFill=value;}
		}

		[Category("Columns"),DefaultValue(true) ]
		public bool AllowSort
		{
			get{return this.allowSort;}
			set{this.allowSort=value;}
		}


		[Category("Columns"),DefaultValue(80) ]
		public int PerformColumnWidth
		{
			get{return this.performColumnWidth;}
			set
			{
				if(value >0 && value <=this.Width)
				{
					this.performColumnWidth=value;
				}
			}
		}

//		private void  CreateDynamicColumns()
//		{
//			if(this.m_dv!=null)
//			{
//				InitColumnCollection ();
//				DataTable dt=this.m_dv.Table;
//				//this.Columns.Clear();	
//				int colCount=dt.Columns.Count;
//				//int scroallWidth=0;
//				//if((dt.Rows.Count*base.ItemHeight)>this.DropDownWidth)
//				//{
//				//	scroallWidth=20;
//				//}
//				int colWidth=performColumnWidth;
//				//int sumColWidth= (colCount*performColumnWidth);//-scroallWidth;
//				//if(this.Width<sumColWidth && colCount >0)
//				//{
//					//colWidth=sumColWidth/colCount;
//				//}
//				int orderDisplyType=0;
//				if(this.columnDisplay!="")
//				{
//					orderDisplyType=SetColumnsCaptionWidth(this.columnDisplay,true);
//					//m_ColWidths=ParseColumnsWidth(this.columnDispalyWidth);
//				}
//				if(orderDisplyType>0)
//				{
//					for(int i=0;i<m_ColWidths.Length;i++)
//					{
//						McColumn col=new McColumn();
//						col.ColumnName=dt.Columns[i].ColumnName;
//						if(orderDisplyType==1)
//						{
//							col.Caption=dt.Columns[i].Caption!=""?dt.Columns[i].Caption:col.ColumnName;
//						}
//						else
//						{
//							col.Caption=m_ColCaption[i]!=""?m_ColCaption[i]:col.ColumnName;
//						}
//						col.Width=m_ColWidths[i];
//						col.Display=m_ColWidths[i]>0;
//						m_Cols.Add(col);	
//					}
//				}
//				else if(this.m_ColWidths!=null)
//				{
//
//					for(int i=0;i<m_ColWidths.Length;i++)
//					{
//						McColumn col=new McColumn();
//						col.ColumnName=dt.Columns[i].ColumnName;
//						col.Caption=dt.Columns[i].Caption!=""?dt.Columns[i].Caption:col.ColumnName;
//						col.Width=m_ColWidths[i];
//						col.Display=m_ColWidths[i]>0;
//						m_Cols.Add(col);	
//					}
//				}
//				else
//				{
//					foreach(DataColumn c in dt.Columns)
//					{
//						McColumn col=new McColumn();
//						col.ColumnName=c.ColumnName;
//						col.Caption=c.Caption!=""?c.Caption:c.ColumnName;
//						col.Width=colWidth;
//						col.Display=true;
//						m_Cols.Add(col);	
//					}
//				}
//				SetColumnsWidth();
//			}
//		}
//

		private void  CreateDynamicColumns()
		{
			if(this.m_dv!=null)
			{
				InitColumnCollection ();
				DataTable dt=this.m_dv.Table;
				//this.Columns.Clear();	
				int colCount=dt.Columns.Count;
				int scroallWidth=0;
				if((dt.Rows.Count*ItemHeight)>this.MaxDropDownItems)
				{
					scroallWidth=20;
				}
				int colWidth=performColumnWidth;
				int sumColWidth= (colCount*performColumnWidth)-scroallWidth;
				
				int orderDisplyType=0;
				if(this.columnDisplay!="")
				{
					LookupColumnDisply lcd=new LookupColumnDisply();
					orderDisplyType=lcd.GetColumnsCaptionWidth(this.columnDisplay,true);
				}
				if(orderDisplyType>0)
				{
					for(int i=0;i<m_ColWidths.Length;i++)
					{
						McColumn col=new McColumn();
						col.ColumnName=dt.Columns[i].ColumnName;
						col.FieldType=GetDataType(dt.Columns[i].DataType);
						if(orderDisplyType==1)
						{
							col.Caption=dt.Columns[i].Caption!=""?dt.Columns[i].Caption:col.ColumnName;
						}
						else
						{
							col.Caption=m_ColCaption[i]!=""?m_ColCaption[i]:col.ColumnName;
						}
						col.Width=m_ColWidths[i];
						col.Display=m_ColWidths[i]>0;
						m_Cols.Add(col);	
					}
				}
				else if(this.m_ColWidths!=null)
				{

					for(int i=0;i<m_ColWidths.Length;i++)
					{
						McColumn col=new McColumn();
						col.ColumnName=dt.Columns[i].ColumnName;
                        col.FieldType = GetDataType(dt.Columns[i].DataType);
						col.Caption=dt.Columns[i].Caption!=""?dt.Columns[i].Caption:col.ColumnName;
						col.Width=m_ColWidths[i];
						col.Display=m_ColWidths[i]>0;
						m_Cols.Add(col);	
					}
				}
				else
				{
				
					if(this.Width<sumColWidth && colCount >0)
					{
						colWidth=sumColWidth/colCount;
					}
				
					foreach(DataColumn c in dt.Columns)
					{
						McColumn col=new McColumn();
						col.ColumnName=c.ColumnName;
                        col.FieldType = GetDataType(c.DataType);
						col.Caption=c.Caption!=""?c.Caption:c.ColumnName;
						col.Width=colWidth;
						col.Display=true;
						this.Columns.Add(col);	
					}
				}
				SetColumnsIndex();
				SetColumnsWidth();
			}
		}

		private FieldType GetDataType(System.Type type )
		{
			if(type.Equals(Type.GetType("System.Boolean")))
			{
				return FieldType.Bool;
			}
			return FieldType.Text;
		}

		private void SetColumnsIndex()
		{
            if (m_dv == null) return;

			int i=0;
			foreach(DataColumn c in m_dv.Table.Columns)
			{
				McColumn col=FindColumn(c.ColumnName);
				if(col!=null)
					col.Ordinal=i;
				i++;
			}
		}

		private McColumn FindColumn(string name)
		{
			foreach(McColumn c in m_Cols)
			{
				if(c.ColumnName==null)
				{
					throw new Exception("Invalid Column name in " + c.ToString());
				}
				if(c.ColumnName.Equals(name))
					return c;
			}
			return null;
		}

		private void SetColumnsWidth()
		{
			int cnt=this.Columns.Count;
			if(cnt>0)
			{
				int sumWidth=0;
				m_ColumnsWidth=null;
				m_ColumnsWidth =new int[cnt];
				for(int i=0 ;i< Columns.Count;i++)
				{
					m_ColumnsWidth.SetValue(Columns[i].Width,i);
					sumWidth+=Columns[i].Width;
				}
				m_SortOrders=new int[cnt];
				m_SortOrders.Initialize();
				//if(sumWidth > base.Width)
				//{
					this.DropDownWidth=sumWidth;
				//}
			}
		}

//		public int GetActiveColumnSort()
//		{
//			return this.activeColumnSort;
//		}

		#endregion

		#region DrawItem

		//This is where the magic happens that makes it appear dropped down with multiple columns
		//		protected override void OnDrawItem(DrawItemEventArgs e)
		//		{
		//			if(m_Cols==null)return;
		//			try
		//			{
		//				int iIndex = e.Index;
		//				if(iIndex > -1)
		//				{
		//					int iXPos = 0;
		//					int iYPos = 0;
		//
		//					DataRow dr = m_dv[iIndex].Row;
		//					bool rtl=(this.RightToLeft==RightToLeft.Yes);
		//					e.DrawBackground();
		//
		//					int colWidth=0;
		//					Rectangle rectStr=Rectangle.Empty;
		//			
		//					for(int index = 0; index < m_Cols.Count; index++) //Loop for drawing each column
		//					{
		//						if(m_Cols[index].Display == false)
		//							continue;
		//
		//						colWidth=m_Cols[index].Width;
		//						using(StringFormat sf=GetStringFormat(true,rtl))
		//						{
		//							if(rtl)
		//								rectStr=new Rectangle(e.Bounds.Width- (iXPos+colWidth-2), e.Bounds.Y, colWidth-ColumnSpacing, ItemHeight);
		//							else
		//								rectStr=new Rectangle (iXPos, e.Bounds.Y, colWidth-ColumnSpacing, ItemHeight);
		//				
		//							e.Graphics.DrawString(dr[index].ToString(), Font, new SolidBrush(e.ForeColor),(RectangleF)rectStr,sf);
		//						}
		//						//e.Graphics.DrawString(dr[index].ToString(), Font, new SolidBrush(e.ForeColor), new RectangleF(iXPos, e.Bounds.Y, m_Cols[index].Width-ColumnSpacing, ItemHeight));
		//						iXPos += colWidth;// - 4;
		//					}
		//					iXPos = 0;
		//					iYPos += ItemHeight;
		//					e.DrawFocusRectangle();
		//					//base.OnDrawItem(e);
		//				}
		//			}			
		//			catch(Exception ex)
		//			{
		//				throw new Exception(ex.Message + "\r\nIn McLookUp.OnDrawItem(DrawItemEventArgs).");
		//			}
		//		}
		//
		//		private StringFormat GetStringFormat(bool wordWrap,bool rtl)
		//		{
		//			StringFormat format1 = new StringFormat();
		//			format1.Trimming = StringTrimming.EllipsisCharacter;
		//			format1.FormatFlags = 0;
		//			if (!wordWrap)
		//			{
		//				format1.FormatFlags = StringFormatFlags.NoWrap;
		//			}
		//			//format1.HotkeyPrefix = HotkeyPrefix.Show;
		//			if (rtl)
		//			{
		//				format1.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
		//				format1.Alignment = StringAlignment.Near;
		//			}
		//			else
		//			{
		//				format1.Alignment = StringAlignment.Near;
		//			}
		//			return format1;
		//		}

		//Draw multiple columns
		protected virtual void OnDrawItem(DrawItemEventArgs e)
		{

			try
			{
				if(m_Cols==null) 
				{
					throw new ArgumentException("Invalid Columns");
				}
				int iIndex = e.Index;
				int colCount=m_Cols.Count;
				bool rtl=(this.RightToLeft == RightToLeft.Yes);
		
				if(iIndex > -1 && colCount>0)
				{
					int iXPos = 0;
					int iYPos = 0;
					int widthAdd=0;
					bool drawImage=false;
					Rectangle rectImage = Rectangle.Empty;

					DataRow dr =  ((DataView)this.DataSource)[iIndex].Row;//  m_dv[iIndex].Row;
					//DataRow dr =  m_DataView[iIndex].Row;
					int imageIndex=-1;
			
					if(ColumnImage>-1 && ColumnImage < colCount)
					{
						imageIndex=(int) Types.ToInt(dr[ColumnImage],-1);
					}

					e.DrawBackground();

					Rectangle rectAlter=new Rectangle(e.Bounds.X,e.Bounds.Y,e.Bounds.Width,ItemHeight);
			
					if ( (e.State & DrawItemState.Selected) == DrawItemState.Selected )
					{
						using(Brush slc=LayoutManager.GetBrushCaption())
							e.Graphics.FillRectangle(slc,(RectangleF)rectAlter);
						//e.DrawFocusRectangle();
					}
					//else if(iIndex%2==0 && m_Alternating)
					//{
					//	using(Brush bck=LayoutManager.GetBrushFlat())
					//	{
					//		e.Graphics.FillRectangle(bck,(RectangleF)rectAlter);
					//	}
					//}
				
					if(this.ImageList!=null && imageIndex > -1)
					{
		
						Rectangle bounds=e.Bounds;
						if ((imageIndex >= 0) && (imageIndex < ImageList.Images.Count))
						{
							//Rectangle rectImage;
							if (rtl)
								rectImage = new Rectangle(bounds.Width-ImageList.ImageSize.Width-1, bounds.Y + ((bounds.Height - ImageList.ImageSize.Height) / 2), ImageList.ImageSize.Width, ImageList.ImageSize.Height);
							else
								rectImage = new Rectangle(bounds.X , bounds.Y + ((bounds.Height - ImageList.ImageSize.Height) / 2), ImageList.ImageSize.Width, ImageList.ImageSize.Height);
		
							drawImage=true;
						}
					}

					int colWidth=0;
					Rectangle rectStr=Rectangle.Empty;
			
					for(int index = 0; index < colCount; index++) //Loop for drawing each column
					{
						colWidth=m_Cols[index].Width;
						int colIndex=m_Cols[index].Ordinal;
						if(m_Cols[index].Display == false || colWidth <=ColumnSpacing)
							continue;

						if(drawImage)
						{
							widthAdd=ImageList.ImageSize.Width + 1;
							if(colWidth > ColumnSpacing+widthAdd)
							{
								ImageList.Draw(e.Graphics, rectImage.X, rectImage.Y, rectImage.Width, rectImage.Height, imageIndex);
							}
							else
							{
								widthAdd=0;
							}
							drawImage=false;
						}
						else
						{
							widthAdd=0;
						}
                        if (m_Cols[index].FieldType == FieldType.Bool)
						{
							bool bRes=false;
							//TODO :check parse
							bRes=bool.Parse(dr[colIndex].ToString());

							Rectangle boolRect= new Rectangle(iXPos + ((colWidth-9)/2),e.Bounds.Y+((ItemHeight-9)/2),9,9);
							
							e.Graphics.DrawRectangle(new Pen(Brushes.Gray),boolRect);
							if(bRes)
							{
								using(Brush bCheck=LayoutManager.GetBrushHot(),bSlct=LayoutManager.GetBrushSelected())
								{
									//if ( (e.State & DrawItemState.Selected) == DrawItemState.Selected )
									//	e.Graphics.FillRectangle(bSlct,boolRect.X+2,boolRect.Y+2,6,6);
									//else
									e.Graphics.FillRectangle(bCheck,boolRect.X+2,boolRect.Y+2,6,6);
								}
							}

						}
						else
						{
							using(StringFormat sf=GetStringFormat(true,rtl))
							{
								if(rtl)
									rectStr=new Rectangle(e.Bounds.Width-(iXPos+widthAdd+colWidth-2), e.Bounds.Y, colWidth-widthAdd-ColumnSpacing, ItemHeight);
								else
									rectStr=new Rectangle (iXPos+widthAdd, e.Bounds.Y, colWidth-widthAdd-ColumnSpacing, ItemHeight);
				

								if(m_Cols[index].Alignment==HorizontalAlignment.Right)
									sf.Alignment=StringAlignment.Far;
								e.Graphics.DrawString(dr[colIndex].ToString(), Font, new SolidBrush(e.ForeColor),(RectangleF)rectStr,sf);
							}
						}
						//e.Graphics.DrawString(dr[index].ToString(), Font, new SolidBrush(e.ForeColor), new RectangleF(iXPos+widthAdd, e.Bounds.Y, colWidth-widthAdd-ColumnSpacing, ItemHeight));
						iXPos += colWidth;
					}
					iXPos = 0;
					iYPos += ItemHeight;
					//e.DrawFocusRectangle();

					//base.OnDrawItem(e);
					if(DrawItem!=null)
						this.DrawItem(this,e);

				}
			}			
			catch(Exception ex)
			{
				throw new Exception(ex.Message + "\r\nIn McColumnList.OnDrawItem(DrawItemEventArgs).");
			}
		}

		private StringFormat GetStringFormat(bool wordWrap,bool rtl)
		{
			StringFormat format1 = new StringFormat();
			format1.Trimming = StringTrimming.EllipsisCharacter;
			format1.FormatFlags = 0;
			if (!wordWrap)
			{
				format1.FormatFlags = StringFormatFlags.NoWrap;
			}
			//format1.HotkeyPrefix = HotkeyPrefix.Show;
			if (rtl)
			{
				format1.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
				format1.Alignment = StringAlignment.Near;
			}
			else
			{
				format1.Alignment = StringAlignment.Near;
			}
			return format1;
		}



		#endregion

		#region ColumnDisply


		//		private int SetColumnsCaptionWidth(string value,bool setArrays)
		//		{
		//			try
		//			{
		//				string[] strlist=value.Split(';');
		//				int cnt=strlist.Length;
		//				if(cnt==0)
		//				{
		//					return 0;
		//				}
		//				int[] results=new int[2];
		//				int orderType=-1;
		//				if(cnt>=2)
		//				{
		//					results[0]=Types.StringToInt(strlist[0],int.MinValue);
		//					results[1]=Types.StringToInt(strlist[1],int.MinValue);
		//
		//					if(results[0]==int.MinValue && results[1]==int.MinValue)
		//					{
		//						throw new InvalidCastException("Unrecognized Format");
		//					}
		//					else if(results[0]==int.MinValue )
		//					{
		//						orderType=2;//Caption;Width
		//					}
		//					else if(results[1]==int.MinValue )
		//					{
		//						orderType=3;//Width;Caption
		//					}
		//					else
		//					{
		//						orderType=1;//Width;//Width
		//					}
		//				}
		//				else
		//				{
		//					return -1;
		//				}
		//
		//	
		//				string[] strWidth=null;
		//				string[] strCaption=null;
		//				int[] intWidth=null;
		//		
		//				if(orderType==2)//Caption;Width
		//				{
		//					strCaption=GetSplitArray(value,0,1);
		//					strWidth=GetSplitArray(value,1,1);
		//					intWidth=ConvertArrayToInt(strWidth);
		//					//SetColumns(strCaption,strCaption,intWidth);
		//					if(setArrays)
		//					SetColumnsInternal(strCaption,intWidth);
		//				}
		//		
		//				else if(orderType==3)//Width;Caption
		//				{
		//					strWidth=GetSplitArray(value,0,1);
		//					strCaption=GetSplitArray(value,1,1);
		//					intWidth=ConvertArrayToInt(strWidth);
		//					//SetColumns(strCaption,strCaption,intWidth);
		//					if(setArrays)
		//						SetColumnsInternal(strCaption,intWidth);
		//				}
		//				else //orderType=0;//Width;//Width
		//				{
		//					strWidth=GetSplitArray(value,0,0);
		//					intWidth=ConvertArrayToInt(strWidth);
		//					//SetColumns(intWidth);
		//					if(setArrays)
		//						this.m_ColWidths=intWidth;
		//				}
		//				return orderType;
		//			}
		//			catch(Exception ex)
		//			{
		//				MsgBox.ShowError(ex.Message);
		//				return -1;
		//			}
		//		}
		//
		//		private string[] GetSplitArray(string value,int mode,int interval)
		//		{
		//			string[] strlist=value.Split(';');
		//			int cnt=strlist.Length;
		//			if(cnt==0)
		//			{
		//				return null;
		//			}
		//			string[] strRes=new string[cnt/(interval+1)];
		//			int j=0;
		//
		//			for(int i=mode;i<cnt;i++)
		//			{
		//				strRes[j]=strlist[i];
		//				j++;
		//				i+=interval;
		//			}
		//			return strRes;
		//		}
		//
		//		private int[] ConvertArrayToInt(string[] value)
		//		{
		//			int[] intWidths=new int[value.Length];
		//			for(int i=0;i< value.Length;i++)
		//			{
		//				int res=int.Parse(value[i]);
		//				if(res<0 || res > 1000)
		//				{
		//					throw new InvalidCastException("Value must be between 0 and 1000");
		//				}
		//				intWidths[i]=res;
		//			}
		//			return intWidths;
		//		}
		//
		//		private int[] ParseColumnsWidth(string value)
		//		{
		//			string[] strWidths=value.Split(';');
		//			int cnt=strWidths.Length;
		//			int[] intWidths=new int[cnt];
		//			try
		//			{
		//				for(int i=0;i< strWidths.Length;i++)
		//				{
		//					//intWidths[i]=Types.StringToInt(strWidths[i],-1);
		//					int res=int.Parse(strWidths[i]);
		//					if(res<0 || res > 1000)
		//					{
		//						throw new InvalidCastException("Value must be between 0 and 1000");
		//					}
		//					intWidths[i]=res;
		//				}
		//				return intWidths;
		//			}
		//			catch(Exception ex)
		//			{
		//				MsgBox.ShowError(ex.Message);
		//				return null;
		//			}
		//		}
		//
		//		private void SetColumnsInternal(string[]Caption,int[] ColumnWidth )
		//		{
		//			this.m_ColWidths=ColumnWidth;
		//			this.m_ColCaption=Caption;
		//		}

		public void SetColumns(int[] ColumnWidth )
		{
			m_ColWidths=ColumnWidth;//new m_ColWidths[ColumnWidth.Length];
		}

		public void SetColumns(string[]ColumnNames,int[] ColumnWidth )
		{
			SetColumns(ColumnNames,ColumnNames,ColumnWidth );
		}

		public void SetColumns(string[]ColumnNames,string[] Captions,int[] ColumnWidth )
		{
			if(!(ColumnNames.Length.Equals(Captions.Length)).Equals(ColumnWidth.Length))
			{
				MsgBox.ShowError("All the Arrays must be equals");
				return;
			}
			int cnt=ColumnNames.Length;
			InitColumnCollection();
			for(int i=0;i<cnt;i++)
			{
				m_Cols.Add(new McColumn(ColumnNames[i],Captions[i],ColumnWidth[i]));
			}
		}

		#endregion

		#region Data

		public void DataBind(DataView value, bool forceAutoFill)
		{
			DataBind(value,this.ValueMember,this.DisplayMember,forceAutoFill);
		}

		public void DataBind(DataView value,string valueMember,string displayMember, bool forceAutoFill)
		{
			try
			{

				this.m_dv=value;
				if(m_dv==null)
				{
					this.ClearInternal();
					return;
				}
			
				if(valueMember==null || valueMember=="" )
				{
					//throw new ArgumentException("Value member can not be null or empty ");
					valueMember=m_dv.Table.Columns[0].ColumnName;
				}
				else if(!m_dv.Table.Columns.Contains(valueMember))
				{
					throw new ArgumentException("Data source not Contains Column ", valueMember);
				}
				if(displayMember==null || displayMember=="" )
				{
					//throw new ArgumentException("Display member can not be null or empty ");
					displayMember=m_dv.Table.Columns.Count>1? m_dv.Table.Columns[1].ColumnName:m_dv.Table.Columns[0].ColumnName;
				}
				else if(!m_dv.Table.Columns.Contains(displayMember))
				{
					throw new ArgumentException("Data source not Contains Column ", displayMember);
				}

				this.ValueMember=valueMember;
				this.DisplayMember=displayMember;

				if(!DesignMode && this.m_dv!=null )
				{
					if(autoFill || forceAutoFill)
						CreateDynamicColumns();
					else if(this.Columns.Count==0 )
					{
						autoFill=true;
						CreateDynamicColumns();
					}
					else
					{
						SetColumnsIndex();
						SetColumnsWidth();
					}
					if(m_Sorted)
					{
						DisplaySortSetting();
					}
	
					//DisplayMemberSetting(m_Sorted);
					//ValueMemberSetting();
					//base.ValueMember=this.m_Cols[m_ColumnValue].ColumnName;
				}
				OnDataSourceChanged(EventArgs.Empty);
				this.Invalidate();
			}
			catch (Exception ex)
			{
				throw ex;
			}		
		}

		internal void ClearInternal()
		{
			this.Items.Clear();
			m_SelectedIndex = -1;
		}
		#endregion

		#region Properties

		[DefaultValue(false), Category("Columns")]
		public bool ShowColumnHeaders
		{
			get
			{
				return m_ShowHeaders;
			}
			set
			{
				m_ShowHeaders=value;
				Invalidate();
			}
		}


		[DefaultValue(null), Category("Data"), Description("ListControlDataSource"), TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), RefreshProperties(RefreshProperties.Repaint)]
		public DataView DataSource
		{
			get{ return this.m_dv; }
			set
			{ 

				bool forceAutoFill=m_dv!=null && this.Columns.Count>0 && !autoFill;

				if(value == null)
				{
					throw new Exception("DataSource cannot be set to null.\r\n McLookUp.DataSource (set)");
				}
//				if(!(value is DataView))
//				{
//					throw new Exception("DataSource not supported (should be a DataView).\r\n McLookUp.DataSource (set)");
//				}
				if (m_dv != value)
				{
					DataBind((DataView)value,false);
				}
			}
		}

		[Category("Columns"),DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Localizable(true)]
		public McColumnCollection Columns
		{
			get
			{
				if(m_Cols==null)
				{
					m_Cols = new McColumnCollection(this);
				}
				return m_Cols;
			}
		}

		//Convenient for resorting the ComboBox based on a column.
//		public void SortBy(string sCol, SortOrder so)
//		{
//			m_dv.Sort = sCol + " " + so.ToString();
//			//isInitItems = true;
//		}

		public bool Sorted
		{
			get{return m_Sorted;}
			set
			{
				m_Sorted=value;
				if(value && !DesignMode && !isSorted && m_dv!=null)
				{
					DisplaySortSetting();
				}
			}
		}
	
		private string DisplaySortSetting()
		{
			string colView=this.DisplayMember;//  m_Cols[m_ColumnView].ColumnName;
			if(this.m_dv!=null && this.m_dv.Sort!=colView)
			{
				isSorted=true;
				return this.m_dv.Sort=colView;//  m_Cols[m_ColumnView].ColumnName;
			}
			return colView;
		}

		//Indexer for retriving values based on the column string.
		//Will return the value of the given column at SelectedIndex row.
		//You may want to add an int indexer as well.
		[Browsable(false),EditorBrowsable(EditorBrowsableState.Advanced),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object this[string ColName]
		{
			get
			{
				try
				{
					if(SelectedIndex < 0)
						return null;
					object o = DataSource.Table.Rows[SelectedIndex][ColName];
					return o;
				}
				catch(Exception ex)
				{
					throw new Exception(ex.Message + "\r\nIn ColumnComboBox[string](get).");
				}
			}
			set
			{
				try
				{
					DataSource.Table.Rows[SelectedIndex][ColName] = value;
				}
				catch(Exception ex)
				{
					throw new Exception(ex.Message + "\r\nIn ColumnComboBox[string](set).");
				}
			}
		}

		[Category("Behavior"), DefaultValue((string) null)]
		public  ImageList ImageList
		{
			get
			{
				return m_ImageList;
			}
			set
			{
				m_ImageList = value;
				this.Invalidate();
			}
		}

		[DefaultValue(-1), Category("Behavior")]
		public int ColumnImage
		{
			get
			{
				return m_ColumnImage;
			}
			set
			{
				if(value < -1 || value > Columns.Count)
				{
					throw new ArgumentException("Index is out of range");
				}
				m_ColumnImage = value;
				this.Invalidate();
			}
		}

		#endregion		

		#region ComboPopUp

		public class LookUpPopUp : Nistec.WinForms.Controls.McPopUpBase
		{
			#region Ctor and members

			private Nistec.WinForms.McPanel pnlHeaders;
			private McLookUp Mc = null;
			private bool showHeaders=false;
			public object selectedItem=null;
			private int forceWidth=0;
			internal McListItems internalList;
			private bool dispose=false;


			/// <summary>
			/// Clean up any resources being used.
			/// </summary>
			protected override void Dispose( bool disposing )
			{
				base.Dispose(dispose);
			}

			public void DisposePopUp( bool disposing )
			{
				dispose=disposing;
				this.Dispose(disposing);
			}

			#endregion

			#region Windows Form Designer generated code
			/// <summary>
			/// Required method for Designer support - do not modify
			/// the contents of this method with the code editor.
			/// </summary>
			private void InitComponent()
			{
				this.internalList = new McListItems();
				this.pnlHeaders = new Nistec.WinForms.McPanel();
				this.SuspendLayout();
				// 
				// internalList
				// 
				this.internalList.ForeColor=Color.Black;
				this.internalList.BorderStyle=BorderStyle.FixedSingle;
				this.internalList.Dock = System.Windows.Forms.DockStyle.Fill;
				this.internalList.IntegralHeight=false;
				//this.internalList.Location = new System.Drawing.Point(72, 7);
				this.internalList.Name = "internalList";
				//this.internalList.Size = new System.Drawing.Size(10, 4);
				this.internalList.TabIndex = 0;
				this.internalList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.internalList_KeyUp);
				this.internalList.SelectionChanged+=new EventHandler(internalList_SelectionChanged);
				// 
				// pnlHeaders
				// 
				this.pnlHeaders.BorderStyle=BorderStyle.FixedSingle;
				this.pnlHeaders.BackColor=SystemColors.Control;
				this.pnlHeaders.Dock = System.Windows.Forms.DockStyle.Top;
				this.pnlHeaders.Location = new System.Drawing.Point(0, 0);
				this.pnlHeaders.Name = "pnlHeaders";
				this.pnlHeaders.Size = new System.Drawing.Size(272, 19);
				this.pnlHeaders.TabIndex = 1;
				this.pnlHeaders.ControlLayout=ControlLayout.Visual;
				//this.pnlHeaders.Paint+=new PaintEventHandler(pnlHeaders_Paint);
                this.pnlHeaders.CustomDrow += new PaintEventHandler(pnlHeaders_CustomDrow);
				// 
				// ComboPopUp
				// 
				this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
				this.ClientSize = new System.Drawing.Size(272, 237);
				this.Controls.Add(this.internalList);
				this.Controls.Add(this.pnlHeaders);
				this.Controls.SetChildIndex(this.pnlHeaders,1);
				this.Controls.SetChildIndex(this.internalList,0);
				this.Name = "ComboPopUp";
				this.ResumeLayout(false);

			}

 			#endregion

			#region Constructors
	
			public LookUpPopUp(McLookUp parent) : base(parent)
			{
				Mc=parent;
				parent.Controls[0].Focus ();
				InitComponent();

			}

			public override void ShowPopUp(IntPtr hwnd)
			{
				int heightAdd=2;
				if(Mc.Columns!=null)
				{
					showHeaders=Mc.m_ShowHeaders;
					//this.pnlHeaders.GradientStyle=GradientStyle.TopToBottom;//reversColor=true;
					this.pnlHeaders.Visible=showHeaders;
					if(showHeaders)
					{
						this.pnlHeaders.StylePainter=Mc.StylePainter;
						heightAdd+=this.pnlHeaders.Height;
					}
				}
	
				this.BackColor = Mc.BackColor;

				int cnt=internalList.Items.Count;
				int visibleItems=Mc.MaxDropDownItems;
				string selectedText=Mc.Text;
		  
				if(cnt==0)
					visibleItems=0;
				if(visibleItems > cnt)
				{
					visibleItems = cnt;
				}

				this.Height =(internalList.ItemHeight * visibleItems)+heightAdd;
			
				if(Mc.DropDownWidth==0)
					this.Width  =Mc.Width ; 
				else
					this.Width  =Mc.DropDownWidth ; 

				if(this.Width<112)this.forceWidth=this.Width;

				int index = -1;
				if(Mc.Text!="")
				{
					index = Mc.IndexOfView(selectedText);
				}
				if(index > -1)
				{
					internalList.SelectedIndex = index;
				}
			
				base.ShowPopUp (hwnd);
				if(forceWidth>0)
					this.Width=forceWidth;
				this.Invalidate();
			}


			#endregion

			#region Events handlers

			private void internalList_SelectionChanged(object sender, EventArgs e)
			{
				OnSelectionChanged();
				this.Close();
			}

			private void internalList_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
			{
				if(e.KeyData == Keys.Escape)
				{
					this.Close();
				}

				if(e.KeyData == Keys.Enter)
				{
					OnSelectionChanged();
					this.Close();
				}
			}

			#endregion

			#region Overrides

			protected override void OnClosed(System.EventArgs e)
			{
				this.Hide();
				base.OnClosed(e);
			}

			protected override void OnPaint(PaintEventArgs e)
			{
				base.OnPaint (e);
				DrawColumnHeaders(e.Graphics);

			}

            //private void pnlHeaders_Paint(object sender, PaintEventArgs e)
            //{
            //    DrawColumnHeaders(e.Graphics);
            //}

            void pnlHeaders_CustomDrow(object sender, PaintEventArgs e)
            {
                DrawColumnHeaders(e.Graphics);
            }

			internal void DrawColumnHeaders()
			{

				DrawColumnHeaders(this.CreateGraphics());
			}

			internal protected virtual void DrawColumnHeaders(Graphics g)
			{
				if(!showHeaders)
					return;

				int top=this.pnlHeaders.Top;
				int width=this.Width-2;  
				Rectangle bounds=this.pnlHeaders.ClientRectangle;

                Rectangle rect = new Rectangle(bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1);

                using (Brush sb =Mc.LayoutManager.GetBrushGradient(rect, 270f))
                {
                    g.FillRectangle(sb, rect);
                }

				int lft=0;  
				int remain=width;  

				using(System.Drawing.Brush bf =Mc.LayoutManager.GetBrushText(), bb = Mc.LayoutManager.GetBrushGradient(rect,270))
				{
					g.FillRectangle(bb,rect);
					using(System.Drawing.Pen p = Mc.LayoutManager.GetPenBorder())
					{
						Rectangle r=Rectangle.Empty;
						remain= width;
						foreach(McColumn c in Mc.m_Cols)
						{
							if((c.Width>0) && (c.Display) && (remain>0))
							{
								int colWidth= (c.Width > remain)? remain:c.Width;
								remain= width - c.Width;
								r=new Rectangle(rect.X+lft,rect.Top,colWidth,rect.Height);
								//Rectangle strRect=new Rectangle(rect.X+lft,rect.Top+2,colWidth,rect.Height-2);
								if(Mc.RightToLeft==RightToLeft.Yes)
								{
									r=new Rectangle(rect.X + rect.Width-lft-colWidth,rect.Top,colWidth,rect.Height);
								}
		
								g.DrawRectangle (p,r);
								Rectangle strRect=new Rectangle(r.X,r.Top+2,colWidth,r.Height-2);
								using(StringFormat sf=new StringFormat())
								{
									sf.Alignment=StringAlignment.Center;
									g.DrawString(c.Caption,this.Font,bf,(RectangleF)strRect,sf);
								}
								lft+=colWidth;
							}
						}
					}
				}
			}


			#endregion

			#region Methods
        
			public int SearchItem(string value)
			{
				int index = internalList.FindString(value);
				if (index > -1) internalList.SelectedIndex = index;

				return index;
			}

			private void OnSelectionChanged()
			{
				this.selectedItem=internalList.SelectedItem;
			}

			#endregion

			#region Properties

			public override object SelectedItem
			{
				get {return selectedItem;}
			}

			public int SelectedIndex
			{
				get {return internalList.SelectedIndex;}
			}
			//
			//		public new Control Parent
			//		{
			//			get {return Mc;}
			//		}

			#endregion

		}
		#endregion

	}



}