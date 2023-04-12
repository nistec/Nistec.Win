using System;
using System.Data;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Windows.Forms.Design;
using System.Drawing.Design;
using System.Globalization;  



using Nistec.Win32;
using Nistec.Win;

namespace Nistec.WinForms
{

	[DefaultEvent("SelectedIndexChanged"),Designer(typeof(Design.McEditDesigner)),
	System.ComponentModel.ToolboxItem(true)]
	[ToolboxBitmap (typeof(McMultiBox),"Toolbox.MultiBox.bmp")]
	public class McMultiBox : Nistec.WinForms.Controls.McComboBase,IMcTextBox,IDropDown,IComboList//,IPopUp
	{
		
		#region Members
		private System.ComponentModel.IContainer components = null;
		private MultiType m_MultiType;
		private ImageList imageList;
		private bool m_DisableDropDown;
		private object m_DataSource=null;
		private string m_ValueMember="";
		private string m_DisplayMember="";
		private ArrayList m_Items;
		private bool textAsValue;
		private DataView m_DataView;
		private object m_SelectedValue=null;
		private int m_SelectedIndex=-1;
		private string m_Filter="";
		private RangeType m_RangeValue;
        private bool m_AllowNull=true;
	

		#endregion

		#region Event Members
		// Events

		[Category("Property changed")]
        public event EventHandler ValueChanged = null;
		//public event DrawItemEventHandler DrawItem = null;
		
	 
		[Category("Action")]
		public event ButtonClickEventHandler ButtonClick  = null;

		//[Category("PropertyChanged"), Description("ListControlOnSelectedValueChanged")]
		//public event EventHandler SelectedValueChanged;
		//[Category("PropertyChanged"), Description("ListControlOnSelectedIndexChanged")]
		//public event EventHandler SelectedIndexChanged;

		#endregion

		#region Constructors
		
		public McMultiBox()
		{
			InitializeComponent();
			m_MultiType=MultiType.Custom; 
 			SetButtonImage(false);
		}


		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated (e);
			OnCreated ();
		}

		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed (e);
		}


		private void OnCreated()
		{
			switch(m_MultiType)
			{
				case MultiType.Date :
					m_RangeValue =new RangeType (RangeType.MinDate ,RangeType.MaxDate );
					FormatType=Formats.ShortDate;   
					m_TextBox.AppendFormatText(DateTime.Today.ToShortDateString());
                    ButtonVisible = true;
					//ButtonToolTip ="Calendar";
					SetButtonImage(true);
					break;
				case MultiType.Combo  :
					m_RangeValue =null;
					FormatType=Formats.None;
                    ButtonVisible = true;
                    //ButtonToolTip = "Combo list box";
					SetButtonImage(true);
					break;
				case MultiType.Boolean :
					m_RangeValue =null;
					FormatType=Formats.None;
                    ButtonVisible = true;
                    SetButtonImage(true);
					break;
				case MultiType.Explorer  :
					m_RangeValue =null;
					FormatType=Formats.None;
                    ButtonVisible = true;
                    //ButtonToolTip = "Explorer";
					SetButtonImage(false);
					break;
				case MultiType.Text :
					m_RangeValue =null;
					FormatType=Formats.None;
                    ButtonVisible = false;
                    break;
				case MultiType.Number :
					m_RangeValue =new RangeType (RangeType.MinNumber ,RangeType.MaxNumber );
					FormatType=Formats.StandadNumber;
                    ButtonVisible = false;
                    //ButtonToolTip = "";
					break;
                case MultiType.Memo:
                    m_RangeValue = null;
                    FormatType = Formats.None;
                    ButtonVisible = true;
                    SetButtonImage(true);
                    break;
                default:
					m_RangeValue =null;
					FormatType=Formats.None;
                    ButtonVisible = true;
                    //ButtonToolTip = m_MultiType.ToString();
					SetButtonImage(false);
					break;
			}
            if(this.DefaultValue.Length >0)
            {
			    this.m_TextBox.Text=this.DefaultValue;
            }
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
			base.Dispose( disposing );
		}

		#endregion

		#region Component Designer generated code
		private void InitializeComponent()
		{
			this.Name = "McMultiBox";
		}
		#endregion

		#region Validation

//		protected override void OnValidating(System.ComponentModel.CancelEventArgs e)
//		{
//			base.OnValidating(e);
//			IsValidating();
//		}
//
//		protected override void OnValidated(System.EventArgs e)
//		{
//
//			base.OnValidated(e);
//		}

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            if (MultiType == MultiType.Boolean || MultiType== MultiType.Combo)
            {
                e.Handled = true;
            }
        }

		protected override void OnKeyDown(KeyEventArgs e)
		{
			if(this.ReadOnly )
			{
				base.OnKeyDown(e);
				return; 
			}

			if(this.MultiType==MultiType.Combo || MultiType== MultiType.Boolean)
			{
				if (e.Shift  && e.KeyCode == Keys.Down)
				{
					DoDropDown();
				}
				else if (((e.Shift  && e.KeyCode == Keys.Up) || e.KeyCode == Keys.Escape))
				{
					CloseDropDown();
				}
			
				else if (e.KeyCode ==System.Windows.Forms.Keys.Down )
				{
					this.SelectionStart=0;
					SetSelectedIndex(m_SelectedIndex,1);
				}
				else if (e.KeyCode ==System.Windows.Forms.Keys.Up  )
				{
					this.SelectionStart=0;
					SetSelectedIndex(m_SelectedIndex, -1);
				}

                if (MultiType == MultiType.Boolean)
                {
                    e.Handled = true;
                    return;
                }
			}
			else if(this.MultiType==MultiType.Date)
			{
				if (e.Shift  && e.KeyCode == Keys.Down)// && m_DroppedDown )
				{
					DoDropDown();
				}
				else if (((e.Shift  && e.KeyCode == Keys.Up) || e.KeyCode == Keys.Escape))
				{
					CloseDropDown();
				}
			
				else if (e.KeyCode ==System.Windows.Forms.Keys.Down )
				{
					this.SelectionStart=0;
					AddDays(1);
				}
				else if (e.KeyCode ==System.Windows.Forms.Keys.Up  )
				{
					this.SelectionStart=0;
					AddDays(-1);
				}
			}
			else if(this.MultiType==MultiType.Number)
			{
				if (e.KeyCode ==System.Windows.Forms.Keys.Down )
				{
					this.SelectionStart=0;
					AddNumber(1);
				}
				else if (e.KeyCode ==System.Windows.Forms.Keys.Up  )
				{
					this.SelectionStart=0;
					AddNumber(-1);
				}
			}
			base.OnKeyDown(e);

		}

		protected override void OnMouseWheel(System.Windows.Forms.MouseEventArgs e)
		{	
			base.OnMouseWheel (e);
			if(this.ReadOnly )
			{
				return; 
			}

			if(this.MultiType==MultiType.Combo )
			{
				int Delta = e.Delta * SystemInformation.MouseWheelScrollLines / 120;
				SetSelectedIndex(m_SelectedIndex,Delta);
			}
			else if(this.MultiType==MultiType.Date)
			{
				int Delta = e.Delta * SystemInformation.MouseWheelScrollLines / 120;
				AddDays(Delta);
			}
			else if(this.MultiType==MultiType.Number)
			{
				int Delta = e.Delta * SystemInformation.MouseWheelScrollLines / 120;
				AddNumber(Delta);
			}
		}

		private void SetSelectedIndex(int value, int Delta)
		{
			int CurrentInt = value ; 

			if(this.m_DataView!=null && m_DisplayMember!="" && m_ValueMember!="")
			{
				if (CurrentInt+Delta >= m_DataView.Table.Rows.Count )
					CurrentInt =m_DataView.Table.Rows.Count-1;
				else if (CurrentInt+Delta < 0 )
					CurrentInt =0;
				else
					m_SelectedIndex +=Delta;

				m_SelectedIndex=CurrentInt;
				this.Text =m_DataView[m_SelectedIndex][m_DisplayMember].ToString (); 
				this.m_SelectedValue= m_DataView[m_SelectedIndex][m_ValueMember];
			}
			else if(Items.Count>0)
			{
				if (CurrentInt+Delta >= Items.Count )
					CurrentInt =Items.Count-1;
				else if (CurrentInt+Delta < 0 )
					CurrentInt =0;
				else
					CurrentInt +=Delta;

				m_SelectedIndex=CurrentInt;
				this.Text =Items[m_SelectedIndex].ToString (); 
			}
		}
	
		private void AddNumber(int value)
		{
			if (!this.Enabled)
				return;

			if(isValidRangeNumber(this.Text,value))
			{
				decimal val=Types.ToDecimal(this.Text,0); 
				val+=value;
				this.Text =val.ToString(); 
			}
		}

		private void AddDays(int value)
		{
			try
			{
				if (!this.Enabled)
					return;
				if(isValidRangeDate(this.Text,value))
				{
					DateTime curDate= DateTime.Parse(this.Text);
					curDate=curDate.AddDays (value);
					this.Text =curDate.ToString ("d"); 
				}
			}
			catch{}
		}

		private bool isValidRangeDate(string value,int addDays)
		{
			if(this.RangeValue.MinValue!="" && this.RangeValue.MaxValue!="" && value !="")
			{
				try
				{
					DateTime curDate= DateTime.Parse(value);
					curDate=curDate.AddDays(addDays);
					DateTime minDate= DateTime.Parse(this.RangeValue.MinValue);
					DateTime maxDate= DateTime.Parse(this.RangeValue.MaxValue);
					return (curDate <= maxDate && curDate >= minDate);
				}
				catch
				{
					return true;
				}
			}
			return true;

		}
		private bool isValidRangeNumber(string value,int addValue)
		{
			if(this.RangeValue.MinValue!="" && this.RangeValue.MaxValue!="" && value !="")
			{
				try
				{
					decimal curNum= decimal.Parse(value);
					curNum=curNum+addValue;
					decimal minNum= decimal.Parse(this.RangeValue.MinValue);
					decimal maxNum= decimal.Parse(this.RangeValue.MaxValue);
					return (curNum <= maxNum && curNum >= minNum);
				}
				catch
				{
					return true;
				}
			}
			return true;

		}

		private void FillItems()
		{
			if(m_DataView==null)return;
			this.Items.Clear();

			foreach(DataRow dr in m_DataView.Table.Rows)
			{
				m_Items.Add(dr[m_DisplayMember]);
			}
		}

		#endregion

		#region Value Changed events

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            this.m_TextBox.Focus();
            this.m_TextBox.SelectAll();
        }

		protected virtual void OnValueChanged(EventArgs e)
		{
			if(ValueChanged !=null) 
				ValueChanged (this, e);
		}

		public virtual object GetItemText(object value)
		{
			if(m_DataView==null ||  this.ValueMember=="" || this.DisplayMember =="")
			{
				return value;
			}
			m_DataView.Sort=this.ValueMember;
			int i=m_DataView.Find(value);
			if(i>-1)
			{
				return m_DataView[i][this.DisplayMember];
			}
			return null;
		}

		#endregion

		#region ButtonClick

		public override void DoDropDown()//ShowPopUp()
		{
			if(this.DisableDropDown || ReadOnly)
			{
				return; 
			}

			switch(m_MultiType)
			{
				case MultiType.Text    :
					if(ControlLayout !=ControlLayout.System)
                        this.ControlLayout = ControlLayout.System; 
					break;
				case MultiType.Boolean  :
					ListDropDown.DoDropDown(this,new object[]{"False","True"});
					break;
				case MultiType.Combo    :
					if(this.DataSource!=null)
					{
						ListDropDown.DoDropDown(this,this.DataSource,this.ValueMember,this.DisplayMember,TextAsValue);
					}
					else if(this.m_Items!=null)
					{
						ListDropDown.DoDropDown(this,this.m_Items);
					}
					break;
				case MultiType.Date    :
					if(isValidRangeDate(this.Text,0))
					{
						DateDropDown.DoDropDown(this,this.Text);
					}
					else
					{
						DateDropDown.DoDropDown(this);
					}
					break;
                case MultiType.Memo:
                    if (this.Text.Length==0 && this.ReadOnly)
                    {
                        //do nothing
                    }
                    else
                    {
                        McMemo.DoDropDown(this,this.ReadOnly);
                    }
                    break;
				case MultiType.Custom   :
					if(ButtonClick !=null)
                        ButtonClick(this, new ButtonClickEventArgs("Custom"));//  ButtonClickEvent ( this.m_Button   ,this.m_TextBox.Text ));
					break;
				case MultiType.Explorer  :
					Explorer m_Explorer=new Explorer ();
					m_Explorer.OpenExplorer (Text);
					break;
				case MultiType.Brows  :
					OpenFileDialog FileDlg = new OpenFileDialog();
                    if(!string.IsNullOrEmpty(m_Filter))
					    FileDlg.Filter  = m_Filter ;
                    else
                        FileDlg.Filter = "All files (*.txt)|*.txt|All files (*.*)|*.*";
					
                    FileDlg.FilterIndex = 1 ;
					FileDlg.RestoreDirectory = true ;
					FileDlg.Multiselect =false;	
	
					if(FileDlg.ShowDialog() == DialogResult.OK)
					{
						this.Text=FileDlg.FileName; 
					}
					break;
				case MultiType.BrowsFolder  :
					FolderBrowserDialog FolderDlg = new FolderBrowserDialog();
			
					// Set the help text description for the FolderBrowserDialog.
					FolderDlg.Description = 
						"Select the directory that you want to use as the default.";

					// Do not allow the user to create new files via the FolderBrowserDialog.
					FolderDlg.ShowNewFolderButton = true;

					// Default to the My Documents folder.
					//FolderDlg.RootFolder = Environment.SpecialFolder.Personal;
					if(FolderDlg.ShowDialog() == DialogResult.OK)
					{
						this.Text=FolderDlg.SelectedPath; 
						//OnValueChanged(new ValueChangedEventArgs ((object)FolderDlg.SelectedPath) );
					}
					break;
				case MultiType.Color  :
					ColorDialog cp = new ColorDialog();
					cp.AllowFullOpen = true ;
					cp.ShowHelp = true ;
					//cp.Color = System.Drawing.Color.FromName ( this.Text ); 
					if(cp.ShowDialog() == DialogResult.OK)
					{
						this.ForeColor = cp.Color;
						this.Text=cp.Color.Name ;
						//OnValueChanged(new ValueChangedEventArgs ((object)cp.Color.Name) );
					}
				
					//McColorPicker cp=new McColorPicker ();
					//cp.SelectedColor  = System.Drawing.Color.FromName ( this.Text );
					//m_MultiPopUp =new  MultiPopUp(this ,cp );
					//this.ShowPopUp (); 
					//this.Text=m_MultiPopUp.InnerControl.Text;
					break;
				case MultiType.Font   :
					FontDialog fp = new FontDialog();
					fp.AllowSimulations = true ;
					fp.AllowVectorFonts  = true ;
					fp.ShowHelp = true ;
					if(this.Text.Length >0)
					{
						string[] sfont  = this.Text.Split (',') ; 
						if(sfont.Length >2)
							fp.Font  =new Font ( sfont[0],(float)Convert.ToDouble (sfont[1]),GetFontStyle(sfont[2])  ) ; 
						else if(sfont.Length >1)
							fp.Font  =new Font ( sfont[0],(float)Convert.ToDouble (sfont[1]) ) ; 
						else
							fp.Font  =new Font ( sfont[0], Font.Size) ; 
				
					}
					
					if(fp.ShowDialog() == DialogResult.OK)
					{
						this.Font =new Font ( fp.Font.Name ,(float)8.25 ,fp.Font.Style ) ;
						this.Text=fp.Font.FontFamily.Name + " , " + fp.Font.Size.ToString () + " , " + fp.Font.Style.ToString ();
						//OnValueChanged(new ValueChangedEventArgs ((object)Text) );
					}
					break;
			}
	
		}

		private System.Drawing.FontStyle GetFontStyle(string s)
		{

			if(s.EndsWith ("Bold"))
				return System.Drawing.FontStyle.Bold;
			else if(s.EndsWith ("Italic"))
				return System.Drawing.FontStyle.Italic;
			else if(s.EndsWith ("Strikeout"))
				return System.Drawing.FontStyle.Strikeout ;
			else if(s.EndsWith ("Underline"))
				return System.Drawing.FontStyle.Underline;
			else
				return System.Drawing.FontStyle.Regular;
		}

		#endregion

		#region Properties
		private void SetButtonImage(bool isCombo)
		{
			if(isCombo)
				base.ButtonImage =ResourceUtil.LoadImage (Global.ImagesPath + "btnCombo.gif");
			else
				base.ButtonImage =ResourceUtil.LoadImage (Global.ImagesPath + "btnBrows.gif");
		}
        
        [Browsable(false),EditorBrowsable(EditorBrowsableState.Advanced)]
        public override bool ButtonVisible
        {
            get { return base.ButtonVisible; }
            set { base.ButtonVisible = value; }
        }

        [Category("Behavior")]
        [DefaultValue(true)]
        public bool AllowNull
        {
            get { return m_AllowNull; }
            set
            {
                m_AllowNull = value;
            }
        }


		[Category("Behavior")]
		[System.ComponentModel.RefreshProperties(RefreshProperties.All)]
		public MultiType MultiType
		{
			get {return m_MultiType;}
			set 
			{
				if(value == m_MultiType)
					return;
				m_MultiType = value;
				this.OnCreated();
			    this.Invalidate ();
			}
		}

		[Category("Behavior"), DefaultValue((string) null)]
		public ImageList ImageList
		{
			get
			{
				return this.imageList;
			}
			set
			{
				this.imageList = value;
				base.Invalidate();
			}
		}

		[Category("Data"), DefaultValue(null),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object SelectedItem
		{
			get
			{
				if(m_DataView!=null &&  this.ValueMember!="" && this.DisplayMember !="")
				{
					if(this.SelectedIndex>-1 && this.SelectedIndex < m_DataView.Table.Rows.Count)
					{
						return m_DataView[this.SelectedIndex];
					}
				}

				return this.Text;
			}
			set
			{
				//this.Text = value;
			}
		}

		[Category("Data"), DefaultValue(null),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object SelectedValue
		{
			get{return this.m_SelectedValue;}
			set
            {
                if (!m_AllowNull && value == null)
                    return;
                this.m_SelectedValue = value;
            }
		}

		[Category("Data"), DefaultValue(-1),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int SelectedIndex
		{
			get{return this.m_SelectedIndex;}
			set
			{
				this.SetSelectedIndex(value,0);
				//this.m_SelectedIndex = value;
			}
		}

		[Category("Data"), DefaultValue(null),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object DataSource
		{
			get{return this.m_DataSource;}
			set
			{
				this.m_DataSource = value;
				if(value is DataTable)
					this.m_DataView=((DataTable)value).DefaultView;
				else if(value is DataView)
					this.m_DataView=(DataView)value;
				else
					this.m_DataView=null;

			}
		}

		[Category("Data"), DefaultValue((string)null),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string ValueMember
		{
			get{return this.m_ValueMember;}
			set{this.m_ValueMember = value;}
		}

		[Category("Data"), DefaultValue((string)null),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string DisplayMember
		{
			get{return this.m_DisplayMember;}
			set{this.m_DisplayMember = value;}
		}

		[Category("Data"),
		Editor("System.Windows.Forms.Design.StringCollectionEditor,System.Design", 
			"System.Drawing.Design.UITypeEditor,System.Drawing"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ArrayList Items
		{
			get 
			{
				if(m_Items==null)
				{
					m_Items=new ArrayList();
				}
				return m_Items;
			}
            set
            {
                m_Items = value;
            }

		}

		[Category("Behavior"),DefaultValue(false),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool TextAsValue
		{
			get{return textAsValue;}
			set{textAsValue=value;}
		}

		[Category("Behavior"),DefaultValue(false)]
		public bool DisableDropDown
		{
			get{return m_DisableDropDown;}
			set{m_DisableDropDown=value;}
		}

        [Category("Behavior"), DefaultValue("")]
        public string Filter
        {
            get { return this.m_Filter; }
            set { this.m_Filter = value; }
        }

        //[DefaultValue(typeof(Size), "200,200")]
        //public Size PopupSize
        //{
        //    get
        //    {
        //        return popUpSize;
        //    }
        //    set
        //    {
        //        popUpSize = value;
        //    }
        //}

		#endregion

		#region IMcTextBox Members

        protected virtual void AppendInternal(string value)
        {
            if (!m_AllowNull && string.IsNullOrEmpty(value))
                return;
            this.m_TextBox.Text = value;
        }

        [Bindable(true)]
        public override string Text
        {
            get {return base.Text; }
            set
            {
                if (base.Text != value)
                {
                    if (!m_AllowNull && string.IsNullOrEmpty(value))
                        return;
                    base.Text = value;
                    this.OnValueChanged(EventArgs.Empty);
                }
            }
        }

		[Category("Behavior"),Description ("RangeType of min and max value"),DefaultValue(null)]
		public RangeType RangeValue
		{
			get	{return m_RangeValue;}
			set	
			{   m_RangeValue =value;
				//if(m_RangeValue !=null)
				//	m_RangeValue = value;
			}
		}

		[Category("Appearance")]
		[DefaultValue(Formats.None)]   
		public Formats FormatType
		{
			get{return m_TextBox.FormatType;}
			set
			{
				if(value==m_TextBox.FormatType)
					return;

				if(m_MultiType== MultiType.Date)
				{
                    if (WinHelp.GetBaseFormat(value) != FieldType.Date)
				   {
					//m_TextBox.FormatType=Formats.ShortDate ;
                    return; 
				   }
				}
				else if(m_MultiType== MultiType.Number)
				{
                    if (WinHelp.GetBaseFormat(value) != FieldType.Number)
				   {
					//m_TextBox.FormatType=Formats.StandadNumber ;
                    return; 
				   }
				}
				else if(m_MultiType== MultiType.Boolean)
				{
                    if (WinHelp.GetBaseFormat(value) != FieldType.Bool)
				   {
					   //m_TextBox.FormatType=Formats.TrueFalse ;
					   return; 
				   }
				}
				m_TextBox.FormatType=value;

			}
		}

//		[Category("Appearance")]
//		[DefaultValue("")]
//		public override string Format
//		{
//			get {return m_TextBox.Format;}
//			set {m_TextBox.Format = value;}
//		}
//
//		[Category("Behavior")]
//		[DefaultValue("")]
//		public override string ErrorMessage
//		{
//			get {return m_TextBox.ErrorMessage;}
//			set {m_TextBox.ErrorMessage = value;}
//		}

//		[Category("Behavior")]
//		[DefaultValue(false)]
//		public override  bool Required
//		{
//			get	{return m_TextBox.Required;}
//			set	{m_TextBox.Required = value;}
//		}
//
//		[Category("Behavior")]
//		[DefaultValue(false)]
//		public override bool ReadOnly
//		{
//			get {return m_TextBox.ReadOnly;}
//			set
//			{//if(m_TextBox.ReadOnly != value)
//				m_TextBox.ReadOnly = value;}
//		}

//		public void AppendText(string value)
//		{
//			m_TextBox.AppendText (value);  
//		}
//
//		public void ResetText()
//		{
//			m_TextBox.ResetText() ;  
//		}
//
//		public void AppendFormatText(string value)
//		{
//			m_TextBox.AppendFormatText(value);  
//		}

		[Browsable(false)]
		protected override FieldType BaseFormat 
		{
			get{return m_TextBox.BaseFormat;}
		}

		#endregion

		#region IControlKeyAction Members

		protected override bool OnInsertAction()
		{
			base.OnInsertAction ();
			DoDropDown();
            return false; 
		}

		protected override bool OnEscapeAction()//EscapeAction action)
		{
			base.OnEscapeAction ();//action);
			CloseDropDown();
			return false;
		}

		public override void ActionClick()
		{
			if(!this.DisableDropDown)
			{
                this.ButtonClick(this, new ButtonClickEventArgs("Action"));  
			}
		}

		public void ActionUndo()
		{
			if(m_TextBox.CanUndo )
				m_TextBox.Undo ();//	m_TextBox.ActionUndo(); 
		}
		
		public void ActionDefaultValue()
		{
			m_TextBox.Text=base.DefaultValue;// ActionDefaultValue(); 
		}

		public override bool IsValidating()
		{

			if(! this.m_TextBox.CausesValidation )
			{
				m_IsValid=true;
				return true;
			}

			bool ok=true;
            string msg="";

			switch(m_MultiType)
			{
				case MultiType.Combo  :
					//cancel=!Validator.ValidatingCombo(ref msg);
					break;
				case MultiType.Date  :
					ok = true;//Validator.ValidateDate (Text,Required,RangeValue,ref msg);
					break;
				case MultiType.Number:
					if(m_TextBox.FormatType == Formats.Money)
						ok = Validator.ValidateCurrency(Text,RangeValue,ref msg);
					else
						ok = Validator.ValidateNumber(Text,RangeValue,ref msg);
					break;
				case MultiType.Boolean :
						ok = Validator.ValidateBool (Text,ref msg);
					break;
				default:
					ok = true;//Validator.ValidateText (Text,Required,ref msg);
					break;
			}
	
			m_IsValid=ok;
			if(!ok)
			{
				//m_IsValid=false;
				//string msg= Nistec.Resources.ResexLibrary.GetWinMcString (Nistec.Resources.WinMcResexKeys.ContentNotExistInList);      
				OnErrorOcurred( new ErrorOcurredEventArgs(msg));
			}
			else
			{
				if(m_TextBox.Modified)
					m_TextBox.AppendFormatText (Text);
				m_IsValid=true;
			}
			return ok;
		}


		#endregion

	}

}