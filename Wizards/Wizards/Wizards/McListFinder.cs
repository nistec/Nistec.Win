using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data; 
using Nistec.WinForms;
using Nistec.Collections;
 
namespace Nistec.Wizards
{

	/// <summary>
	/// Summary description for FinderList.
	/// </summary>
	[System.ComponentModel.ToolboxItem(true)]
	[ToolboxBitmap(typeof(McListFinder),"Toolbox.McListFinder.bmp")]
    public class McListFinder : Nistec.WinForms.Controls.McContainer//UserControl//,IStyleCtl//
	{

		#region Constructor and members
		protected Nistec.WinForms.McLabel panelTop;
 
		private Nistec.WinForms.McButton cmdOK;
		private Nistec.WinForms.McPanel panelBottom;
		//private Nistec.WinForms.McPanel panelList;
		private Nistec.WinForms.McButton cmdExit;
		private Nistec.WinForms.McTextBox SerchTxt;
		private System.Windows.Forms.ImageList imageList1;
		private Nistec.WinForms.McLabel LblMsg;
		private Nistec.WinForms.McListBox list;
		private bool showButtons= true;
 
		//private Nistec.WinForms.Styles _StylePlan;
		//private Nistec.WinForms.IStyleGuide m_StyleGuide;

		private System.ComponentModel.IContainer components;
		//private Form m_Form;


		// Instance events
		public event EventHandler SerchTextChanged;
		public event EventHandler OkClick;
		public event EventHandler CancelClick;
		//public event EventHandler NoClick;
		public event EventHandler HelpClick;
		private bool closeOnOK;


		public McListFinder(DataView dv,string Caption)
		{
			InitFinder(null, dv,"","",Caption,SelectionMode.One  );
		}

		public McListFinder(DataView dv,string DisplayMember,string ValueMember,string Caption)
		{
			InitFinder(null,dv,DisplayMember,ValueMember,Caption,SelectionMode.One );
		}

		public McListFinder(Form owner,DataView dv,string DisplayMember,string ValueMember,string Caption)
		{
			InitFinder(owner,dv,DisplayMember,ValueMember,Caption,SelectionMode.One);
		}

		public McListFinder(Form owner,DataView dv,string DisplayMember,string ValueMember,string Caption,System.Windows.Forms.SelectionMode selectionMode)
		{
			InitFinder(owner,dv,DisplayMember,ValueMember,Caption,selectionMode);
		}

		public McListFinder(Form owner,string Caption)
		{
			InitializeComponent();
			this.RightToLeft=owner.RightToLeft;  
			//this.Owner =owner;
			this.Text =Caption;
		}

		private  void InitFinder(Form owner,DataView dv,string DisplayMember,string ValueMember,string Caption,System.Windows.Forms.SelectionMode selectionMode)
		{
		//	this.showButtons=true;
			InitializeComponent();
			dv.Sort =DisplayMember;
			this.RightToLeft=owner.RightToLeft;  
			list.DisplayMember =DisplayMember;
			list.ValueMember =ValueMember;
            list.DataSource = dv;
            list.SelectionMode = selectionMode; 
			this.panelTop.Text=Caption;
			//this.Owner =owner;
			this.Text =Caption;

           
			//if(Owner is IStyleCtl)
			//	base.SetStyleLayout(((IStyleCtl)Owner).StyleGuide.GetStyleLayout());
		}

		public McListFinder()
		{

			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			//
			// TODO: Add any constructor code after InitializeComponent call
			//

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
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

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated (e);
			if(this.SerchTxt.Visible)
				this.SerchTxt.Focus();
		}

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(McListFinder));
			this.panelTop = new Nistec.WinForms.McLabel();
			this.cmdOK = new Nistec.WinForms.McButton();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.panelBottom = new Nistec.WinForms.McPanel();
			this.LblMsg = new Nistec.WinForms.McLabel();
			this.cmdExit = new Nistec.WinForms.McButton();
			this.SerchTxt = new Nistec.WinForms.McTextBox();
			this.list = new Nistec.WinForms.McListBox();
			this.panelBottom.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelTop
			// 
			this.panelTop.ControlLayout = Nistec.WinForms.ControlLayout.Visual;
			this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelTop.FixSize = false;
			this.panelTop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.panelTop.Location = new System.Drawing.Point(0, 0);
			this.panelTop.Name = "panelTop";
			this.panelTop.Size = new System.Drawing.Size(136, 20);
			this.panelTop.TabIndex = 0;
			this.panelTop.Text = "List Finder Mc";
			// 
			// cmdOK
			// 
			this.cmdOK.ControlLayout = Nistec.WinForms.ControlLayout.XpLayout;
			this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.cmdOK.Dock = System.Windows.Forms.DockStyle.Left;
			this.cmdOK.FixSize = false;
			this.cmdOK.ImageIndex = 1;
			this.cmdOK.ImageList = this.imageList1;
			this.cmdOK.Location = new System.Drawing.Point(1, 1);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(24, 22);
			this.cmdOK.TabIndex = 2;
			this.cmdOK.ToolTipText = "OK";
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// panelBottom
			// 
			this.panelBottom.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.panelBottom.Controls.Add(this.LblMsg);
			this.panelBottom.Controls.Add(this.cmdExit);
			this.panelBottom.Controls.Add(this.cmdOK);
			this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelBottom.DockPadding.All = 1;
			this.panelBottom.Location = new System.Drawing.Point(0, 128);
			this.panelBottom.Name = "panelBottom";
			this.panelBottom.Size = new System.Drawing.Size(136, 24);
			this.panelBottom.TabIndex = 4;
			// 
			// LblMsg
			// 
			this.LblMsg.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.LblMsg.ControlLayout = Nistec.WinForms.ControlLayout.Flat;
			this.LblMsg.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblMsg.FixSize = false;
			this.LblMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.LblMsg.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.LblMsg.Location = new System.Drawing.Point(49, 1);
			this.LblMsg.Name = "LblMsg";
			this.LblMsg.Size = new System.Drawing.Size(86, 22);
			this.LblMsg.TabIndex = 4;
			this.LblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cmdExit
			// 
			this.cmdExit.ControlLayout = Nistec.WinForms.ControlLayout.XpLayout;
			this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.No;
			this.cmdExit.Dock = System.Windows.Forms.DockStyle.Left;
			this.cmdExit.FixSize = false;
			this.cmdExit.ImageIndex = 0;
			this.cmdExit.ImageList = this.imageList1;
			this.cmdExit.Location = new System.Drawing.Point(25, 1);
			this.cmdExit.Name = "cmdExit";
			this.cmdExit.Size = new System.Drawing.Size(24, 22);
			this.cmdExit.TabIndex = 3;
			this.cmdExit.ToolTipText = "Cancel";
			this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
			// 
			// SerchTxt
			// 
			this.SerchTxt.BackColor = System.Drawing.Color.White;
			this.SerchTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.SerchTxt.Dock = System.Windows.Forms.DockStyle.Top;
			this.SerchTxt.ForeColor = System.Drawing.Color.Black;
			//this.SerchTxt.KeyAction.OnEscapeAction = Nistec.WinForms.EscapeAction.Undo;
			this.SerchTxt.Location = new System.Drawing.Point(0, 20);
			this.SerchTxt.Name = "SerchTxt";
			this.SerchTxt.Size = new System.Drawing.Size(136, 20);
			this.SerchTxt.TabIndex = 1;
			this.SerchTxt.TextChanged+=new EventHandler(SerchTxt_TextChanged);
			// 
			// list
			// 
			this.list.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.list.Dock = System.Windows.Forms.DockStyle.Fill;
			this.list.IntegralHeight = false;
			this.list.Location = new System.Drawing.Point(0, 40);
			this.list.Name = "list";
			this.list.Size = new System.Drawing.Size(136, 88);
			this.list.TabIndex = 7;
			this.list.DoubleClick += new System.EventHandler(this.list_DoubleClick);
			this.list.SelectedIndexChanged+=new EventHandler(list_SelectedIndexChanged);
			this.list.DoubleClick+=new EventHandler(list_DoubleClick);
			this.list.MouseDown+=new MouseEventHandler(list_MouseDown);
			this.list.MouseUp+=new MouseEventHandler(list_MouseUp);
			this.list.KeyDown+=new KeyEventHandler(list_KeyDown);
			this.list.KeyUp+=new KeyEventHandler(list_KeyUp);
			//this.list.SelectedValueChanged += new System.EventHandler(this.list_SelectedValueChanged);
			// 
			// McListFinder
			// 
			this.Controls.Add(this.list);
			this.Controls.Add(this.SerchTxt);
			this.Controls.Add(this.panelTop);
			this.Controls.Add(this.panelBottom);
            this.Controls.SetChildIndex(this.panelTop,0);
            this.Controls.SetChildIndex(this.SerchTxt, 0);
            this.Controls.SetChildIndex(this.panelBottom, 0);
            this.Controls.SetChildIndex(this.list, 0);
            this.Name = "McListFinder";
			this.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Size = new System.Drawing.Size(136, 152);
			this.Resize += new System.EventHandler(this.ListFinder_Resize);
			this.panelBottom.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region Static Methods

		public static int DefaultRowVisible
		{
			get{return 20;}
		}
		public static int DefaultWidth
		{
			get{return 200;}
	    }
		public static int DefaultHeight
		{
			get{return 200;}
		}
		public static int MaxHeight
		{
			get{return 600;}
		}
		public static int MaxWidth
		{
			get{return 600;}
		}

        private ListItems m_ListItems = null;

//		public static ListItem[] OpenFinder(Form owner, DataView dv,string DisplayMember,string ValueMember,string Caption)
//		{
// 		  ListFinder f=new ListFinder(owner,dv,DisplayMember,ValueMember,Caption);
//		  f.ShowDialog ();
//	  	  return f.m_ListItems; 			  
//		}
//
//		public static ListItem[] OpenFinder(Form owner, DataView dv,string DisplayMember,string ValueMember,string Caption,SelectionMode selectionMode)
//		{
//			ListFinder f=new ListFinder(owner,dv,DisplayMember,ValueMember,Caption,selectionMode);
//			f.ShowDialog ();
//			return f.m_ListItems; 			  
//		}
//
//		public static ListItem[] OpenListDlg(Form owner, DataView dv,string DisplayMember,string ValueMember,string Caption,SelectionMode selectionMode)
//		{
//			ListFinder f=new ListFinder(owner,dv,DisplayMember,ValueMember,Caption,selectionMode);
//			f.SerchTxt.Visible =false; 
//			f.ShowDialog ();
//			return f.m_ListItems; 			  
//		}
		#endregion

		#region Property

		[Browsable(false),EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool AutoChildrenStyle
		{
			get{return base.AutoChildrenStyle;}
			set
			{
				base.AutoChildrenStyle=value;
			}
		}

        public ListItem[] SelectedListItems
		{
			get
            {
                if (m_ListItems == null)
                    return null;
                return m_ListItems.ToArray();
            }
		}

		[Browsable(false)]
		public Nistec.WinForms.McTextBox McTextBox
		{
			get{return  this.SerchTxt;}
		}

		[Browsable(false)]
		public Nistec.WinForms.McLabel McTitle
		{
			get{return  this.panelTop;}
		}

		public bool OpenAsListOnly 
		{
			get{return !SerchTxt.Visible ;}
			set{SerchTxt.Visible=!value;}
		}

		public bool ShowTitle
		{
			get{return this.panelTop.Visible ;}
			set{panelTop.Visible=value;}
		}

		public bool ShowButtons
		{
			get{return this.showButtons ;}
			set
			{
				if(this.showButtons!=value)
				{
					this.showButtons=value;
                    this.panelBottom.Visible = value;
                    //this.cmdOK.Visible=this.showButtons;
                    //this.cmdExit.Visible=this.showButtons;
  				}
			}
		}

//		public RightToLeft Rtl
//		{
//			get{return this.RightToLeft;}   
//			set{this.RightToLeft=value; 
//				this.Invalidate ();
//			   }
//		}

		[Category("TextBox")]
		public  string FinderText
		{
			get{return  this.SerchTxt.Text;}
			set{this.SerchTxt.Text=value;}
		}

		public int CalcHeight(int maxRowVisible)
		{
			int height=maxRowVisible * list.ItemHeight;
			int ex=this.panelTop.Height+this.panelBottom.Height+this.McTextBox.Height+10; 
			if(height < McListFinder.DefaultHeight)
				return McListFinder.DefaultHeight + ex;
			if(height > McListFinder.MaxHeight)
				return McListFinder.DefaultHeight + ex;

			return height + ex;
		}

		public int CalcWidth(int width)
		{
			if(width < McListFinder.DefaultWidth)
				return McListFinder.DefaultWidth;
			return width;
		}

		#endregion

		#region List

		[Description("ListBoxItems"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Localizable(true), Category("Data"), Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
		public McListBox.ObjectCollection Items
		{
			get
			{
				return this.list.Items;
			}
		}

		[Browsable(false)]
		public Nistec.WinForms.McListBox List
		{
			get{return  list;}
		}

		[DefaultValue((string) null), Category("Data"), Description("ListControlDataSource"), TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), RefreshProperties(RefreshProperties.Repaint)]
		public object DataSource
		{
			get{ return list.DataSource; }
			set{list.DataSource=value;}
		}

		[Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor)), Category("Data"), TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), DefaultValue(""), Description("ListControlDisplayMember")]
		public string DisplayMember
		{
			get{ return list.DisplayMember; }
			set{list.DisplayMember=value;}
		}

		[DefaultValue(""), Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor)), Category("Data"), Description("ListControlValueMember")]
		public string ValueMember
		{
			get{ return list.ValueMember; }
			set{list.ValueMember=value;}
		}
        
		public SelectionMode SelectionMode
		{
			get{return list.SelectionMode;}
			set{list.SelectionMode=value;}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("ListBoxSelectedIndex"), Bindable(true), Browsable(false)]
		public int SelectedIndex
		{
			get{return list.SelectedIndex;}
			set{list.SelectedIndex=value;}
		}
 
		[Bindable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("ListBoxSelectedItem"), Browsable(false)]
		public object SelectedItem
		{
			get{return list.SelectedItem;}
			set{list.SelectedItem=value;}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Bindable(true), Description("ListControlSelectedValue"), Browsable(false), DefaultValue((string) null), Category("Data")]
		public object SelectedValue
		{
			get{return list.SelectedValue;}
			set{list.SelectedValue=value;}
		}


		#endregion

		#region Title

		[Category("Title")]
		public string Title
		{
			get{return this.panelTop.Text;}   
			set
			{
				this.panelTop.Text=value; 
				this.Invalidate ();
			}
		}

		[Category("Title")]
		public int TitleHeight
		{
			get{return this.panelTop.Height;}   
			set
			{
				this.panelTop.Height=value; 
				this.Invalidate ();
			}
		}

		[Category("Title")]
		[DefaultValue(System.Drawing.ContentAlignment.MiddleCenter)]
		public virtual System.Drawing.ContentAlignment TitleTextAlign
		{
			get{ return this.panelTop.TextAlign; }

			set
			{
					this.panelTop.TextAlign = value;
					this.Invalidate();
			}
		}

		[Category("Title")]
		[DefaultValue(System.Drawing.ContentAlignment.MiddleCenter)]
		public virtual System.Drawing.ContentAlignment TitleImageAlign
		{
			get{ return this.panelTop.ImageAlign; }

			set
			{
				this.panelTop.ImageAlign = value;
				this.Invalidate();
			}
		}

		[Category("Title")]
		[Description("ButtonImageIndex"), Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design", typeof(System.Drawing.Design.UITypeEditor)), DefaultValue(-1), Localizable(true), TypeConverter(typeof(ImageIndexConverter))]
		public int TitleImageIndex
		{
			get
			{
				return this.panelTop.ImageIndex;
			}
			set
			{
					this.panelTop.ImageIndex = value;
					base.Invalidate();
			}
		}

		[Category("Title")]
		[Description("ButtonImageList"), DefaultValue((string) null)]
		public ImageList TitleImageList
		{
			get
			{
				return this.panelTop.ImageList;
			}
			set
			{
					this.panelTop.ImageList = value;
					base.Invalidate();
			}
		}

		#endregion

		#region Private methods

		private void SerchTxt_TextChanged(object sender, System.EventArgs e)
		{
			FindString(this.SerchTxt.Text);
		}

		private void FindString(string searchString)
		{
			// Set the SelectionMode property of the McListBox to select multiple items.
			list.SelectionMode = SelectionMode.One;// .MultiExtended;
   
			// Set our intial index variable to -1.
			int x =-1;
			// If the search string is empty exit.
			if (searchString.Length != 0)
			{
				// Loop through and find each item that matches the search string.
				//do
				//{
					// Retrieve the item based on the previous index found. Starts with -1 which searches start.
					x = list.FindString(searchString, x);
					// If no item is found that matches exit.
					if (x != -1)
					{
						// Since the FindString loops infinitely, determine if we found first item again and exit.
						if (list.SelectedIndices.Count > 0)
						{
							if(x == (int)list.SelectedIndices[0])
								return;
						}
						// Select the item in the McListBox once it is found.
						list.SetSelected(x,true);
					}
   
				//}while(x != -1);
			}
		}

		private void SetReturnValue()
		{
			int itms=list.SelectedItems.Count;
            if (itms <= 0)
            {
                m_ListItems = null;
                return;
            }

            m_ListItems = new ListItems();
            object pitem = null;

            for (int i = 0; i < itms; i++)
            {
                if (list.SelectedItems[i] is DataRowView)
                {
                    pitem = list.SelectedItems[i];
                    string textItem = (string)((DataRowView)pitem).Row[list.DisplayMember.Substring(list.DisplayMember.IndexOf(".") + 1)];
                    object valueItem = ((DataRowView)pitem).Row[list.ValueMember];
                    m_ListItems.Add(new ListItem(valueItem,textItem,0));
                    //m_ListItems[i].DisplayMember = (string)((DataRowView)pitem).Row[list.DisplayMember.Substring(list.DisplayMember.IndexOf(".") + 1)];
                    //m_ListItems[i].ValueMember = ((DataRowView)pitem).Row[list.ValueMember];

                }
                else
                {
                    m_ListItems.Add(new ListItem(list.SelectedItems[i].ToString()));
                    //m_ListItems[i].DisplayMember = list.SelectedItems[i].ToString();
                    //m_ListItems[i].ValueMember = list.SelectedItems[i].ToString();
                }
            }

            //if(itms==1)
            //{
            //    m_ListItems = new ListItem[itms];

            //    if (list.SelectedItem is DataRowView && ! string.IsNullOrEmpty(list.DisplayMember)) 
            //    {
            //        m_ListItems[0].DisplayMember =(string) ((DataRowView)list.SelectedItem).Row[list.DisplayMember.Substring(list.DisplayMember.IndexOf(".")+1)];
            //        m_ListItems[0].ValueMember  = ((DataRowView)list.SelectedItem).Row[list.ValueMember];
            //    }
            //    else
            //    {
            //        m_ListItems[0].DisplayMember = list.SelectedItem.ToString(); 
            //        m_ListItems[0].ValueMember   = list.SelectedValue;
            //    }
            //}
            //else if(itms >1)
            //{
            //    m_ListItems = new Nistec.Data.Record[itms];
            //    object pitem = null;
				
            //        for(int i=0;i<itms;i++)
            //        {
            //            if (list.SelectedItems[i] is DataRowView) 
            //            {
            //                pitem=list.SelectedItems[i];
            //                m_ListItems[i].DisplayMember  =(string) ((DataRowView)pitem).Row[list.DisplayMember.Substring(list.DisplayMember.IndexOf(".")+1)];
            //                m_ListItems[i].ValueMember   = ((DataRowView)pitem).Row[list.ValueMember];

            //            }
            //            else
            //            {
            //                m_ListItems[i].DisplayMember  = list.SelectedItems[i].ToString ();
            //                m_ListItems[i].ValueMember   = list.SelectedItems[i].ToString ();
            //            }
            //        }
            //    }
            //    else
            //    {
            //      m_ListItems=null;
            //    }
			//this.Close ();
		}
		
		private void cmdOK_Click(object sender, System.EventArgs e)
		{
			closeOnOK=true;
			OnOkClick(e);
		}

		private void list_DoubleClick(object sender, System.EventArgs e)
		{
		    closeOnOK=false;
				OnOkClick(e);
		}

		private void list_SelectedIndexChanged(object sender, EventArgs e)
		{
			if(list.SelectionMode ==SelectionMode.One)
			{
                if (list.SelectedItem is DataRowView && !string.IsNullOrEmpty(list.DisplayMember)) 
				{
					object obj =((DataRowView)list.SelectedItem).Row[list.DisplayMember.Substring(list.DisplayMember.IndexOf(".")+1)];
					this.LblMsg.Text =obj.ToString();
					//m_ListItems[0].DisplyMember = ((DataRowView)list.SelectedItem).Row[list.DisplayMember.Substring(list.DisplayMember.IndexOf(".")+1)];
					//m_ListItems[0].ValueMember  = ((DataRowView)list.SelectedItem).Row[list.ValueMember];
				}
				else
				{
					this.LblMsg.Text =list.SelectedItem.ToString (); 
					//m_ListItems[0].DisplyMember  = list.SelectedItem.ToString (); 
					//m_ListItems[0].ValueMember   = list.SelectedValue;
				}
			}
			else if(list.SelectedItems.Count >0) 
			{
				this.LblMsg.Text = list.SelectedItems.Count.ToString () + " Items Selected.";  
			}
		}

		private void cmdExit_Click(object sender, System.EventArgs e)
		{
			OnCancelClick(e);
        }


		private void ListFinder_Resize(object sender, System.EventArgs e)
		{
			LblMsg.Width =panelBottom.Width-cmdExit.Width -cmdOK.Width-1;   
		}

		protected override bool ProcessDialogKey(Keys keyData)
		{

			if (keyData == Keys.Escape )
			{
				PreformeCancelClick();
				return true;
			} 
			else if (keyData == Keys.Enter)
			{
				PreformeOkClick();
				return true;
			} 
			return base.ProcessDialogKey(keyData);
		}

		private void list_MouseDown(object sender, MouseEventArgs e)
		{
			this.OnMouseDown(e);
		}

		private void list_MouseUp(object sender, MouseEventArgs e)
		{
			this.OnMouseUp(e);
		}

		private void list_KeyDown(object sender, KeyEventArgs e)
		{
			this.OnKeyDown(e);
		}

		private void list_KeyUp(object sender, KeyEventArgs e)
		{
			this.OnKeyUp(e);
		}


		#endregion

		#region Override

		protected override void OnStylePainterChanged(EventArgs e)
		{
			base.OnStylePainterChanged (e);
			base.SetChildrenStyle(false);
			//this.panelBottom.SetChildrenStyle();
		}
          
		public virtual void OnSerchTextChanged(EventArgs e)
		{
			if (SerchTextChanged != null)
				SerchTextChanged(this, e);
		}

		public virtual void OnCancelClick(EventArgs e)
		{
			Form form=this.FindForm();
			form.DialogResult=DialogResult.No; 
			form.Close ();

			if (CancelClick != null)
				CancelClick(this, e);
		}
        
		public virtual void OnOkClick(EventArgs e)
		{
			SetReturnValue();
			Form form=this.FindForm();
			form.DialogResult=DialogResult.OK; 
			if(closeOnOK)
			   form.Close ();

			if (OkClick != null)
				OkClick(this, e);
		}
        
		public virtual void OnHelpClick(EventArgs e)
		{
			if (HelpClick != null)
				HelpClick(this, e);
		}

		public virtual void PreformeOkClick()
		{
			closeOnOK=true;
			OnOkClick(EventArgs.Empty); 
		}
		public virtual void PreformeCancelClick()
		{
			OnCancelClick(EventArgs.Empty); 
		}

		#endregion

		#region IStyleCtl

//		public override void SetStyleLayout(StyleLayout value)
//		{
//			if(this.m_StylePainter!=null)
//			{
//				this.m_StylePainter.Layout.SetStyleLayout(value); 
//				foreach(Control ctl in this.Controls )
//				{
//					if(ctl is IStyleCtl )
//					{
//						((IStyleCtl)ctl).SetStyleLayout(value); 
//					}
//				}
//				Invalidate(true);
//			}
//		}
//
//		public override void SetStyleLayout(Styles value)
//		{
//			if(this.m_StylePainter!=null)
//			{
//				m_StylePainter.Layout.SetStyleLayout(value);
//				foreach(Control ctl in this.Controls )
//				{
//					if(ctl is IStyleCtl )
//					{
//						((IStyleCtl)ctl).SetStyleLayout(value); 
//					}
//				}
//				Invalidate(true);
//			}
//		}
//
//		protected override void OnStylePainterChanged(EventArgs e)
//		{
//			foreach(Control ctl in this.Controls )
//			{
//				if(ctl is IStyleCtl )
//				{
//					((IStyleCtl)ctl).StylePainter=m_StylePainter; 
//				}
//			}
//			Invalidate(true);
//		}

		#endregion

	}
}
