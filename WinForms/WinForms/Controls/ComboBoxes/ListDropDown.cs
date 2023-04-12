using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;   
using System.Collections;
using System.Data; 
using System.Runtime.InteropServices;

using Nistec.Win32;



using System.Windows.Forms.Design;
using System.Drawing.Design;
using System.Globalization;
using Nistec.WinForms.Controls;
using Nistec.Win;


namespace Nistec.WinForms
{

	public interface IListDropDown
	{
		Nistec.WinForms.Controls.McListItems.ObjectCollection Items{get;}
		object DataSource  {get;set;}
		string DisplayMember  {get;set;}
		string ValueMember  {get;set;}
		ComboBoxStyle DropDownStyle  {get;set;}
		DrawMode DrawMode  {get;set;}
		int DropDownWidth  {get;set;}
		bool DroppedDown  {get;set;}
		int ItemHeight  {get;set;}
		int MaxDropDownItems  {get;set;}
		//ImageList ImageList  {get;set;}
		void DoDropDown();
	}
	
	
	/// <summary>
	/// Summary description for ListDropDown.
	/// </summary>

	[System.ComponentModel.ToolboxItem(false)]
	public class ListDropDown : Nistec.WinForms.Controls.McPopUpBase,IListDropDown
	{
 
		#region Members
	
		private bool						m_AcceptItems;
		private McListItems				internalList;
		private Control						Mc;
	
        //public event EventHandler PopUpShow;
        //public event EventHandler PopUpClosed;

		private  ComboBoxStyle		m_DropDownStyle;
		private  int				m_DropDownWidth;
		private  bool				m_DroppedDown;
		private  int				m_MaxDropDownItems;
		private  int				m_MaxLength;
		private  int				m_PreferredHeight;
		private bool				m_TextAsValue=true;
		//private int					m_SelectedIndex;
		private bool				m_SelectedValueChangedFired;

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

		public event EventHandler SelectionChanged;

		#endregion

		#region Constructor

		public static IListDropDown GetDropDown(Control ctl)
		{
			ListDropDown dd=new ListDropDown(ctl);
			return (IListDropDown)dd ;
		}

		public static IListDropDown GetDropDown(Control ctl,object dataSource,string valueMember,string displayMember,bool textAsValue)
		{
			ListDropDown dd=new ListDropDown(ctl,dataSource,valueMember,displayMember,textAsValue);
			return (IListDropDown)dd ;
		}

		public static IListDropDown GetDropDown(Control ctl,object[] items)
		{
			ListDropDown dd=new ListDropDown(ctl,items);
			return (IListDropDown)dd ;
		}

		public static IListDropDown GetDropDown(Control ctl,ArrayList items)
		{
			ListDropDown dd=new ListDropDown(ctl,items);
			return (IListDropDown)dd ;
		}

		public static void DoDropDown(Control ctl,object dataSource,string valueMember,string displayMember,bool textAsValue)
		{
			ListDropDown dd=new ListDropDown(ctl,dataSource,valueMember,displayMember,textAsValue);
			dd.DoDropDown();
		}

        public static void DoDropDown(Control ctl, object dataSource, string valueMember, string displayMember, int dropDownWidth)
        {
            ListDropDown dd = new ListDropDown(ctl, dataSource, valueMember, displayMember, false);
            dd.DropDownWidth = dropDownWidth;
            dd.DoDropDown();
        }

		public static void DoDropDown(Control ctl,object[] items)
		{
			ListDropDown dd=new ListDropDown(ctl,items);
			dd.DoDropDown();
		}

		public static void DoDropDown(Control ctl,ArrayList items)
		{
			ListDropDown dd=new ListDropDown(ctl,items);
			dd.DoDropDown();
		}

		public ListDropDown(Control parent)
		{
			Mc=parent;
			InitComponents();
		}

		public ListDropDown(Control parent,object dataSource,string valueMember,string displayMember,bool textAsValue)
		{
			Mc=parent;
			this.m_TextAsValue=textAsValue;
			InitComponents();
			this.internalList.ValueMember=valueMember;
			this.internalList.DisplayMember=displayMember;
            this.internalList.DataSource = dataSource;
        }

		public ListDropDown(Control parent,object[] items)
		{
			Mc=parent;
			InitComponents();
			this.internalList.Items.AddRange(items);
		}
		public ListDropDown(Control parent,ArrayList items )
		{
			Mc=parent;
			InitComponents();
			object[] list=new object[items.Count];
			items.CopyTo(list,0);
			this.internalList.Items.AddRange(list);
		}
		private void InitComponents()
		{
			m_MaxDropDownItems  = 8;
			m_AcceptItems = false;
			//m_SelectedIndex=-1;

			InitializeComponent();

			this.internalList =new  McListItems();
			//IntegralHeight=true;
			this.internalList.DrawItem+=new DrawItemEventHandler(internalList_DrawItem);
			this.internalList.IntegralHeight=false;

			this.internalList.ForeColor =Mc.ForeColor;
			this.internalList.BackColor =Mc.BackColor;
			this.internalList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.internalList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.internalList.RightToLeft  =Mc.RightToLeft ;    
			
			this.internalList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.mListBox_KeyUp);
            this.Controls.Add(this.internalList);

		}
        private bool IsMouseOnScroll(Nistec.Win32.POINT wpt)
        {
            int x = wpt.x;

            if (this.Items.Count <= MaxDropDownItems)
            {
                return false;
            }

            Point pt = new Point(Mc.Left, Mc.Bottom + 1);
            Point p = Mc.Parent.PointToScreen(pt);

            //Point pt = new Point(Mc.Left, Mc.Bottom + 1);
            //Point p = this.internalList.PointToScreen(pt);
            int right = p.X + this.Width;

            if (x >= (right - 30) && x <= right)
            {
                return true;
            }
            return false;
        }


		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(this.internalList!=null)
				{
					this.internalList.Dispose();
				}

			}
			base.Dispose( disposing );
		}


		#endregion

		#region Designer generated code
		private void InitializeComponent()
		{

			this.MinimumSize = new System.Drawing.Size(1, 1);
			this.Name = "ListDropDown";

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

		protected virtual void OnDropDown(EventArgs e)
		{
			if(DropDownOcurred!=null)
				this.DropDownOcurred(this,e);
		}
		
		protected virtual void OnSelectionChangeCommitted(EventArgs e)
		{
			if(SelectionChangeCommitted!=null)
				this.SelectionChangeCommitted(this,e);
		}

		protected virtual void OnDropDownStyleChanged(EventArgs e)
		{
			if(DropDownStyleChanged!=null)
				this.DropDownStyleChanged(this,e);
		}

		protected virtual void OnDrawItem(DrawItemEventArgs e)
		{
			if (this.DrawItem !=null)
				this.DrawItem(this, e);
		}

		private void internalList_DrawItem(object sender, DrawItemEventArgs e)
		{
			OnDrawItem(e);
		}


		#endregion

		#region Internal Methods

		private void CheckNoDataSource()
		{
			if (DataSource != null)
			{
				throw new ArgumentException("DataSourceLocksItems");
			}
		}

		private void UpdateText()
		{
			string text1 = "";
			if (SelectedIndex != -1)
			{
				object obj1 = internalList.Items[SelectedIndex];
				if (obj1 != null)
				{
					text1 = GetItemText(obj1);
				}
			}
			Mc.Text = text1;
		}
	
		public virtual string GetItemText(object item)
		{
			if (SelectedItem is DataRowView) 
			{
				item = ((DataRowView)SelectedItem).Row[DisplayMember.Substring(DisplayMember.IndexOf(".")+1)];
			}
			else if (SelectedItem is string) 
			{
				item = (string)SelectedItem;
			}

			if (item == null)
			{
				return "";
			}
			return Convert.ToString(item, CultureInfo.CurrentCulture);
		}

		public virtual string GetDataRowText(object item,string field)
		{
			DataView dv=(DataView)DataSource;  
			if ((dv.Sort != field)) 
			{
				dv.Sort = field;// this.ValueMember;
			}
			int i = dv.Find(item);
			if ((i < 0)) 
			{
				return "";
			}
			return dv[i][this.DisplayMember].ToString ();
		}

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
			if ((index < 0) || (index >= internalList.Items.Count))
			{
				throw new ArgumentOutOfRangeException("InvalidArgument");
			}
			internalList.Items[index] = value;
			if (Mc.IsHandleCreated)
			{
				bool flag1 = index == SelectedIndex;
				internalList.Items.RemoveAt(index);
				internalList.Items.Insert(index, value);
				if (flag1)
				{
					SelectedIndex = index;
					this.UpdateText();
				}
			}
		}
 
		protected void SetItemsCore(IList value)
		{
			this.ClearInternal();
			this.AddRangeInternal(value);
			if (!m_SelectedValueChangedFired)
			{
				this.OnSelectedValueChanged(EventArgs.Empty);
				m_SelectedValueChangedFired = false;
			}
		}

		internal void ClearInternal()
		{
			this.Items.Clear();
			SelectedIndex = -1;
		}

		internal void AddRangeInternal(IList items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			if (Sorted)
			{
				foreach (object obj1 in items)
				{
					if (obj1 == null)
					{
						throw new ArgumentNullException("item");
					}
				}
				internalList.Items.AddRange((McListItems.ObjectCollection)items);
				//internalList.Sort(this.Comparer);
				if (Mc.IsHandleCreated)
				{
					Exception exception1 = null;
					object[] objArray1 = new object[items.Count];
					items.CopyTo(objArray1, 0);
					//Array.Sort(objArray1, this.Comparer);
					object[] objArray2 = objArray1;
					for (int num2 = 0; num2 < objArray2.Length; num2++)
					{
						object obj2 = objArray2[num2];
						if (exception1 == null)
						{
							try
							{
								int num1 = internalList.Items.IndexOf(obj2);
								internalList.Items.Insert(num1, obj2);
							}
							catch (Exception exception2)
							{
								exception1 = exception2;
								internalList.Items.Remove(obj2);
							}
						}
						else
						{
							internalList.Items.Remove(obj2);
						}
					}
					if (exception1 != null)
					{
						throw exception1;
					}
				}
			}
			else
			{
				foreach (object obj3 in items)
				{
					if (obj3 == null)
					{
						throw new ArgumentNullException("item");
					}
				}
				internalList.Items.AddRange((McListItems.ObjectCollection)items);
				if (Mc.IsHandleCreated)
				{
					Exception exception3 = null;
					foreach (object obj4 in items)
					{
						if (exception3 == null)
						{
							try
							{
								internalList.Items.Add(obj4);
								continue;
							}
							catch (Exception exception4)
							{
								exception3 = exception4;
								internalList.Items.Remove(obj4);
								continue;
							}
						}
						internalList.Items.Remove(obj4);
					}
					if (exception3 != null)
					{
						throw exception3;
					}
				}
			}
		}

		#endregion

		#region Data Properties

		[DefaultValue((string) null), Category("CatData"), Description("ListControlDataSource"), TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), RefreshProperties(RefreshProperties.Repaint)]
		public object DataSource
		{
			get
			{
				return internalList.DataSource;
			}
			set
			{
				internalList.DataSource=value;
			}
		}
 
		[Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), Category("Data"), TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), DefaultValue(""), Description("ListControlDisplayMember")]
		public string DisplayMember
		{
			get
			{
				return internalList.DisplayMember;
			}
			set
			{
				internalList.DisplayMember=value;
			}
		}
 
		[DefaultValue(""), Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), Category("Data"), Description("ListControlValueMember")]
		public string ValueMember
		{
			get
			{
				return internalList.ValueMember;
			}
			set
			{
				internalList.ValueMember=value;
			}
		}

		[Category("Data"),
		Editor("System.Windows.Forms.Design.StringCollectionEditor,System.Design", 
			"System.Drawing.Design.UITypeEditor,System.Drawing"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public Nistec.WinForms.Controls.McListItems.ObjectCollection Items
		{
			get {return internalList.Items;}
		}

		[Browsable(false)]
		internal McListItems InternalList
		{
			get {return internalList;}
		}

        [Browsable(false)]
        public new bool InvokeRequired
        {
            get { return internalList.InvokeRequired; }
        }

		#endregion

		#region Combo Properties

		public bool TextAsValue
		{
			get{return this.m_TextAsValue;}
			set{this.m_TextAsValue=value;}
		}


		[Description("ComboBoxSelectedIndex"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int SelectedIndex
		{
			get
			{

				return internalList.SelectedIndex;
//				if(SelectedItem != null)
//				{
//					return internalList.Items.IndexOf(SelectedItem); 
//				}
//				if (Mc.IsHandleCreated)
//				{
//					return m_SelectedIndex;
//				}
//				return -1;
			}
			set
			{

				internalList.SelectedIndex=value;
//				if (SelectedIndex != value)
//				{
//					int num1 = 0;
//					if (internalList != null)
//					{
//						num1 = internalList.Items.Count;
//					}
//					
//					if(value > -1 && value < num1)
//					{
//						internalList.SelectedItem =internalList.Items[value];
//						m_SelectedIndex = value;
//					
//						this.UpdateText();
//						if (Mc.IsHandleCreated)
//						{
//							//Parent.OnTextChanged(EventArgs.Empty);
//						}
//						this.OnSelectedValueChanged(EventArgs.Empty);
//						this.OnSelectedIndexChanged(EventArgs.Empty);
//					}
//				}
			}
		}
 
		[Description("ComboBoxSelectedItem"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), Bindable(true)]
		public override  object SelectedItem
		{
			get
			{
				return internalList.SelectedItem;
			}
			//			set
			//			{
			//				internalList.SelectedItem=value;
			//			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Bindable(true), Description("ListControlSelectedValue"), Browsable(false), DefaultValue((string) null), Category("Data")]
		public object SelectedValue
		{
			get
			{
				return internalList.SelectedValue;
			}
			set
			{
				string text1 = ValueMember;
				if (text1.Equals(string.Empty))
				{
					throw new Exception("Empty ValueMember");
				}
	
				internalList.SelectedValue=value;
			}
		}
 

		[Description("ComboBoxStyle"), DefaultValue(ComboBoxStyle.Simple), RefreshProperties(RefreshProperties.Repaint), Category("CatAppearance")]
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
						throw new InvalidEnumArgumentException("value", (int) value, typeof(ComboBoxStyle));
					}

					m_DropDownStyle=value;
					this.OnDropDownStyleChanged(EventArgs.Empty);
				}
			}
		}
 
		[Description("ComboBoxDrawMode"), DefaultValue(DrawMode.Normal), RefreshProperties(RefreshProperties.Repaint), Category("Behavior")]
		public DrawMode DrawMode
		{
			get
			{
				return internalList.DrawMode;
			}
			set
			{
					internalList.DrawMode=value;
			}
		}
 
		[Description("ComboBoxDropDownWidth"),DefaultValue(0), Category("Behavior")]
		public int DropDownWidth
		{
			get
			{
				return m_DropDownWidth;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentException("InvalidArgument");//, objArray1));
				}
				if (m_DropDownWidth != value)
				{
					m_DropDownWidth=value;
				}
			}
		}
 
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), Description("ComboBoxDroppedDown")]
		public bool DroppedDown
		{
			get
			{
				if (Mc.IsHandleCreated)
				{
					return m_DroppedDown;
				}
				return false;
			}
			set
			{
				if(m_DroppedDown)
				{
					this.Close();
					return;
				}	
				if(value)
					ShowPopUp();		
			}
		}
 
		[Category("Behavior"), Description("ComboBoxIntegralHeight"), DefaultValue(true), Localizable(true)]
		public bool IntegralHeight
		{
			get
			{
				return internalList.IntegralHeight;
			}
			set
			{
				internalList.IntegralHeight=value;
			}
		}
 
		[Localizable(true),DefaultValue(13), Description("ComboBoxItemHeight"), Category("Behavior")]
		public int ItemHeight
		{
			get
			{
				return internalList.ItemHeight;  
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentException("InvalidArgument");//, objArray1));
				}
					internalList.ItemHeight=value;
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
					throw new ArgumentOutOfRangeException("InvalidBoundArgument");//, objArray1));
				}
				m_MaxDropDownItems = (short) value;
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
					m_MaxLength= value;
				}
			}
		}
 
		[Browsable(false), Description("ComboBoxPreferredHeight"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int PreferredHeight
		{
			get
			{
				if (m_PreferredHeight > -1)
				{
					return m_PreferredHeight;
				}
				int num1 = Mc.Font.Height + ((SystemInformation.BorderSize.Height * 4) + 3);
				m_PreferredHeight = (short) num1;
				return num1;
			}
		}

		[Category("Behavior"), DefaultValue(false), Description("ComboBoxSorted")]
		public bool Sorted
		{
			get
			{
				return internalList.Sorted;
			}
			set
			{
				internalList.Sorted=value;
			}
		}

		[Category("Behavior"),DefaultValue(false)]
		public bool AcceptItems
		{
			get {return m_AcceptItems;}
			set {m_AcceptItems = value;}
		}

//		[Category("Behavior"), DefaultValue((string) null)]
//		public ImageList ImageList
//		{
//			get
//			{
//				return this.internalList.ImageList;
//			}
//			set
//			{
//				this.internalList.ImageList = value;
//				
//			}
//		}
		#endregion

		#region PopUp

		public void DoDropDown()
		{
			if(m_DroppedDown)
			{
				Close();
				return;
			}	
		
			ShowPopUp();			
		}

		protected override void OnPopupClosed(System.EventArgs e)
		{
			m_DroppedDown = false;
			this.Dispose();
			internalList.Visible =false;  
			Mc.Invalidate(false);
			if(SelectionChanged !=null) 
				this.SelectionChanged (this,e);
            base.OnPopupClosed(e);
            //if(PopUpClosed!=null)
            //    PopUpClosed(Mc,e);
		}

	
		[UseApiElements("ShowWindow")]
		protected void ShowPopUp()
		{

			m_DroppedDown = true;

			int cnt=this.internalList.Items.Count;
			int visibleItems=this.MaxDropDownItems;
			string selectedText=Mc.Text;
		  
			if(cnt==0)
				visibleItems=0;
			if(visibleItems > cnt)
			{
				visibleItems = cnt;
			}

			this.Height =(this.internalList.ItemHeight * visibleItems)+4;

			if(this.DropDownWidth==0)
				this.Width  =Mc.Width ; 
			else
				this.Width  =this.DropDownWidth ; 
			    		       
			//this.ResumeLayout(false);
			if(this.DataSource!=null && m_TextAsValue && Mc.Text.Length>0)
			{
				this.internalList.SelectedValue=(object)Mc.Text;
			}
			else if(selectedText.Length>0)
			{
				int index = this.internalList.FindStringExact(selectedText);
				if(index > -1)
				{
					this.internalList.SelectedIndex = index;
				}
			}
			//if(Items.Count <=0)
			//	return;
	
			Point pt = new Point(Mc.Left,Mc.Bottom+1 );

			this.Location = Mc.Parent.PointToScreen (pt);
			//this.Closed += new System.EventHandler(this.OnPopupClosed);
			//this.Show();
            WinAPI.ShowWindow(this.Handle, WindowShowStyle.ShowNormalNoActivate);
			this.internalList.Focus();

			StartHook();
			this.Start = true;
            //if(PopUpShow!=null)
            //    PopUpShow(Mc,new EventArgs());
		}

		#endregion
		
		#region Internal helpers

		internal bool IsMatchHandle(IntPtr mh)
		{
			if(!m_DroppedDown)
				return false;
			
			if(this.IsHandleCreated)
			{
				if(mh==this.Handle)
					return true;
			}

			if(internalList.IsHandleCreated)
			{
				if (mh==internalList.Handle)
					return true;

			}
			return false;
		}

		internal IntPtr GetComboPopUpHandle()
		{
			if(this.IsHandleCreated)
				return (IntPtr)this.Handle;
			return IntPtr.Zero;
		}

		internal IntPtr GetListHandle()
		{
			if(internalList.IsHandleCreated)
				return (IntPtr)internalList.Handle;
			return IntPtr.Zero;
		}

		public bool Validate_Item(string Text)
		{
			if (m_AcceptItems)
			{
				MsgBox.ShowInfo (Mc.Text);
				int index = internalList.Items.IndexOf(Mc.Text);
				if (index > -1) 
				{
					internalList.SelectedIndex = index;
				}
				return (index<0);
			}
			return false;
		}

		public int FindString(string s)
		{
			return ((Nistec.WinForms.Controls.McListItems)internalList).FindString (s);
		}

		public int FindString(string s,int startIndex)
		{
			return ((Nistec.WinForms.Controls.McListItems)internalList).FindString (s,startIndex);
		}

		#endregion

		#region Events handlers

		private void mListBox_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyData == Keys.Escape)
			{
				this.Close();
			}

			if(e.KeyData == Keys.Enter)
			{
				OnSelected();
				this.Close();
			}
		}

		protected virtual void OnSelected()
		{
			if(this.internalList.SelectedItem != null)
			{
				object pitem = null;
				object ditem = null;
				if (this.SelectedItem is DataRowView) 
				{
					ditem = ((DataRowView)this.SelectedItem).Row[this.DisplayMember.Substring(this.DisplayMember.IndexOf(".")+1)];
					pitem =this.SelectedValue;
					Mc.Text=pitem.ToString();
				}
				else if (this.SelectedItem is string) 
				{
					ditem = (string)this.SelectedItem;
					pitem=ditem;
					Mc.Text=ditem.ToString();
				}

				if(Mc is IComboList)
				{
					//int index = this.internalList.FindStringExact((string)ditem);
					//if (index > -1) this.SelectedIndex = index;
					((IComboList)Mc).SelectedIndex=this.internalList.SelectedIndex;
					((IComboList)Mc).SelectedValue=this.internalList.SelectedValue;
				}
				if(this.SelectionChanged != null)
				{
					this.SelectionChanged(this,new SelectionChangedEventArgs(pitem));
				}
			}


			this.Close();
		}

		protected override void OnClosed(System.EventArgs e)
		{
			EndHook();
			this.internalList.KeyUp -= new System.Windows.Forms.KeyEventHandler(this.mListBox_KeyUp);
			this.internalList.Visible=false; 
		
			//this.Controls.Remove(this.internalList);
			base.OnClosed(e);
		}

		#endregion

		#region Mouse Hook Methods

		private IntPtr					m_mouseHookHandle;
		private GCHandle				m_mouseProcHandle;
		private bool					IsHooked;

		protected virtual void StartHook()
		{ 
			if(IsHooked){EndHook();}
			// Mouse hook
			WinAPI.HookProc mouseHookProc = new WinAPI.HookProc(MouseHook);
			m_mouseProcHandle = GCHandle.Alloc(mouseHookProc);
			m_mouseHookHandle = WinAPI.SetWindowsHookEx((int)WindowsHookCodes.WH_MOUSE, 
				mouseHookProc, IntPtr.Zero, WinAPI.GetCurrentThreadId());
			if(m_mouseHookHandle ==IntPtr.Zero)
			{
				throw new ApplicationException(" Error Create handle");
			}
			IsHooked=true;
			m_DroppedDown=true;
		}

		protected virtual void EndHook()
		{
			// Unhook   
			if(!IsHooked){return;}
			WinAPI.UnhookWindowsHookEx( m_mouseHookHandle );
			m_mouseProcHandle.Free();
			m_mouseHookHandle = IntPtr.Zero;
			IsHooked=false;
			m_DroppedDown=false;
		}

		private IntPtr MouseHook(int code, IntPtr wparam, IntPtr lparam) 
		{
			MOUSEHOOKSTRUCT mh = (MOUSEHOOKSTRUCT )Marshal.PtrToStructure(lparam, typeof(MOUSEHOOKSTRUCT));
			bool res=GetMouseHook(mh,wparam);
			if (res)
			{
				//WinAPI.SetFocus(IntPtr.Zero);
				return mh.hwnd;
			}
             
			return WinAPI.CallNextHookEx( m_mouseHookHandle, code, wparam, lparam );
		}

        protected virtual bool GetMouseHook(MOUSEHOOKSTRUCT mh, IntPtr wparam)
		{

			Msgs msg = (Msgs) wparam.ToInt32();
//			if ((msg == Msgs.WM_LBUTTONDOWN) || (msg == Msgs.WM_RBUTTONDOWN)|| (msg == Msgs.WM_NCLBUTTONDOWN))
			if ((msg == Msgs.WM_LBUTTONUP) || (msg == Msgs.WM_RBUTTONUP)|| (msg == Msgs.WM_NCLBUTTONUP))
			{

				if ((mh.hwnd == this.Handle) || (mh.hwnd == this.internalList.Handle))
				{
                    if (IsMouseOnScroll(mh.pt))
                        return false;
					//EndHook();
					this.OnSelected();
					return true;
				}
				else
				{
					this.Close();
					return true;
				}
			}
			return false;
		}

		#endregion

	}

}
