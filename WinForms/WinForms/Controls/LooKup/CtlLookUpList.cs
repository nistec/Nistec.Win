using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Data;
using System.Windows.Forms;


using Nistec.Collections;
using Nistec.Data;
using Nistec.WinForms.Controls;
using Nistec.Win;

namespace Nistec.WinForms
{
	/// <summary>
	/// Summary description for McLookUpList.
	/// </summary>
	[ToolboxItem(true),Designer(typeof(Design.LookUpListDesigner))]
	[ToolboxBitmap(typeof(McLookUpList), "Toolbox.ColumnList.bmp")]
	public class McLookUpList : McContainer,IBind,IMcLookUp	
	{

		#region Static Ctor

		private static  Icon icoAsc;
		private System.Windows.Forms.ImageList imageList1;
		private static  Icon icoDesc;

		static McLookUpList()
		{
			McLookUpList.icoAsc = Nistec.Drawing.DrawUtils.LoadIcon(Type.GetType("Nistec.WinForms.McLookUpList"),
				"Nistec.WinForms.Images.SortDown.ico");
  
			McLookUpList.icoDesc = Nistec.Drawing.DrawUtils.LoadIcon(Type.GetType("Nistec.WinForms.McLookUpList"),
				"Nistec.WinForms.Images.SortUp.ico");
  		
		}

		#endregion

		#region Ctor and members
		internal Nistec.WinForms.McPanel pnlLookup;
		internal Nistec.WinForms.McPanel pnlHeader;
		private Nistec.WinForms.McColumnList ctlList;
		private Nistec.WinForms.McTextBox ctlText;
		private Nistec.WinForms.McButtonMenu btnMenu;

		private bool palLookupInit=false;
		private const int MinimumWidth=80;
		private bool ishowHeadings = true;
		private int activeHeaderDisply=-1;
		private System.ComponentModel.IContainer components;

		public McLookUpList()
		{
			InitializeComponent();

			//pnlLookup.m_netFram=true;
			//pnlHeader.m_netFram=true;
			//ctlList.m_netFram=true;

			//this.pnlHeader.GradientStyle=GradientStyle.TopToBottom;//reversColor=true;
			ctlList.ColumnViewSetting();
			base.AutoChildrenStyle=true;
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

		#endregion

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(McLookUpList));
			this.pnlHeader = new Nistec.WinForms.McPanel();
			this.pnlLookup = new Nistec.WinForms.McPanel();
			this.ctlList = new Nistec.WinForms.McColumnList();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// pnlHeader
			// 
			this.pnlHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlHeader.ControlLayout = Nistec.WinForms.ControlLayout.Visual;
			this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlHeader.Location = new System.Drawing.Point(0, 28);
			this.pnlHeader.Name = "pnlHeader";
			this.pnlHeader.Size = new System.Drawing.Size(144, 20);
			this.pnlHeader.TabIndex = 0;
			this.pnlHeader.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlHeader_MouseUp);
			//this.pnlHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlHeader_Paint);
            this.pnlHeader.CustomDrow += new PaintEventHandler(pnlHeader_CustomDrow);
            // 
			// pnlLookup
			// 
			this.pnlLookup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlLookup.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlLookup.Location = new System.Drawing.Point(0, 0);
			this.pnlLookup.Name = "pnlLookup";
			this.pnlLookup.Size = new System.Drawing.Size(144, 28);
			this.pnlLookup.TabIndex = 0;
			this.pnlLookup.Visible = false;
			// 
			// ctlList
			// 
			this.ctlList.ColumnDisplay = "";
			this.ctlList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ctlList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.ctlList.IntegralHeight = false;
			this.ctlList.Location = new System.Drawing.Point(0, 48);
			this.ctlList.Name = "ctlList";
			this.ctlList.ReadOnly = false;
			this.ctlList.Size = new System.Drawing.Size(144, 120);
			this.ctlList.TabIndex = 1;
			this.ctlList.DataSourceChanged += new System.EventHandler(this.ctlList_DataSourceChanged);
			this.ctlList.SelectedIndexChanged += new System.EventHandler(this.ctlList_SelectedIndexChanged);
			this.ctlList.DoubleClick+=new EventHandler(ctlList_DoubleClick);
			this.ctlList.Click+=new EventHandler(ctlList_Click);
			this.ctlList.MouseDown+=new MouseEventHandler(ctlList_MouseDown);
			this.ctlList.MouseUp+=new MouseEventHandler(ctlList_MouseUp);
			this.ctlList.KeyDown+=new KeyEventHandler(ctlList_KeyDown);
			this.ctlList.KeyUp+=new KeyEventHandler(ctlList_KeyUp);
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// McLookUpList
			// 
			this.Controls.Add(this.ctlList);
			this.Controls.Add(this.pnlHeader);
			this.Controls.Add(this.pnlLookup);
			this.Size = new System.Drawing.Size(144, 168);
			this.ResumeLayout(false);

		}

   
		private void InitializePnlLookup()
		{
			this.pnlLookup.autoChildrenStyle=true;
			this.ctlText=new Nistec.WinForms.McTextBox();
			this.btnMenu=new  Nistec.WinForms.McButtonMenu() ;
			// 
			// ctlText
			// 
			this.ctlText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ctlText.Name = "ctlText";
			this.ctlText.ReadOnly = false;
			this.ctlText.TabIndex = 0;
			this.ctlText.Text = "";
			this.ctlText.KeyDown+=new KeyEventHandler(ctlText_KeyDown);
			this.ctlText.TextChanged+=new EventHandler(ctlText_TextChanged);
			ctlText.m_netFram=true;
			// 
			// btnMenu
			// 
			this.btnMenu.ButtonMenuStyle=ButtonMenuStyles.Combo;
			this.btnMenu.ControlLayout= Nistec.WinForms.ControlLayout.Visual;
			this.btnMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.btnMenu.Name = "btnMenu";
			this.btnMenu.TabIndex = 1;
			this.btnMenu.Text = "";
			this.btnMenu.PopUp.ItemHeight=16;
            this.btnMenu.SelectedItemClick += new SelectedPopUpItemEventHandler(btnMenu_SelectedItemClick);
	
			//btnMenu.m_netFram=true;

			this.btnMenu.PopUp.ImageList=this.imageList1;;
			this.btnMenu.PopUp.StylePainter=this.StylePainter;
	
			btnMenu.AddPopUpItem("Filter",0);
			btnMenu.AddPopUpItem("Print",1);
			btnMenu.AddPopUpItem("Export",2);
			//btnMenu.AddPopUpItem("ViewDB",3);
			btnMenu.AddPopUpItem("Hide Lookup",4);
			
			pnlLookupSetting();

			this.pnlLookup.Controls.Add(this.ctlText);
			this.pnlLookup.Controls.Add(this.btnMenu);
			
		}


		private void pnlLookupSetting()
		{
			if(this.RightToLeft==RightToLeft.Yes)
			{
				this.btnMenu.Size = new System.Drawing.Size(20, 18);
				this.btnMenu.Location = new System.Drawing.Point(4, 4);
				this.btnMenu.Anchor=AnchorStyles.Left|AnchorStyles.Top;
			
				this.ctlText.Size = new System.Drawing.Size(pnlLookup.Width-this.btnMenu.Width-8, 20);
				this.ctlText.Location = new System.Drawing.Point(pnlLookup.Width-this.ctlText.Width-4, 4);
				this.ctlText.Anchor=AnchorStyles.Right|AnchorStyles.Top;
			}
			else
			{
				this.btnMenu.Size = new System.Drawing.Size(20, 18);
				this.btnMenu.Location = new System.Drawing.Point(pnlLookup.Width-this.btnMenu.Width-4, 4);
				this.btnMenu.Anchor=AnchorStyles.Top|AnchorStyles.Right;
			
				this.ctlText.Location = new System.Drawing.Point(4, 4);
				this.ctlText.Size = new System.Drawing.Size(pnlLookup.Width-this.btnMenu.Width-8, 20);
				this.ctlText.Anchor=AnchorStyles.Left|AnchorStyles.Top |AnchorStyles.Right;

			}

		}

        private void btnMenu_SelectedItemClick(object sender, SelectedPopUpItemEvent e)
		{
			OnPopUpItemClick(e);
		}

        protected virtual void OnPopUpItemClick(SelectedPopUpItemEvent e)
		{
			switch(e.Item.Text)
			{
				case "Filter":
					this.ctlList.PerformFilter();
					break;
				case "Print":
					this.ctlList.PerformPrint();
					break;
				case "Export":
					this.ctlList.PerformExport();
					break;
                //case "ViewDB":
                //    this.ctlList.PerformGetDataDB();
                //    break;
				case "Hide Lookup":
					this.ShowLookupPanel=false;
					break;
			}
		}

		#endregion

		#region Lookup

		private LookupList m_LookupList = new LookupList();
		private Keys m_LastKey = Keys.Space; //Last key pressed.
	
		private bool isInitLookupList = true;
		private bool isTextChangedInternal = false;//Used when the text is being changed by another member of the class.
		//public bool isLookupDropDown = true;
		public bool isLookupOn = true; //isLookupOning can be turned on or off. No need for the whole property write out.

        [Category("Lookup"),Browsable(false),EditorBrowsable( EditorBrowsableState.Advanced),DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden)]
        public LookupList LookupList
        {
            get { return this.m_LookupList; }
        }

		[Category("Lookup")]
		public bool ShowLookupPanel
		{
			get{return this.pnlLookup.Visible;}
			set
			{
				this.pnlLookup.Visible=value;
				if(!palLookupInit && value)
				{
					InitializePnlLookup();
				}
			}
		}

		private void ctlText_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				if(isInitLookupList)
					InitisLookupOnList();
				//base.OnKeyDown (e);
				m_LastKey = e.KeyCode;
			}
			catch//(Exception ex)
			{
				//throw new Exception(ex.Message + "\r\nIn McLookUp.OnKeyDown(KeyEventArgs).");
			}
		}

		private void ctlText_TextChanged(object sender, EventArgs e)
		{
			OnLookupTextChanged(e);
		}


        protected virtual void OnLookupTextChanged(EventArgs e) //Doesn't call the base so no wiring up this event for you.
        {
            //Run a few checks to make sure there should be any "Lookup list" going on.
            if (isTextChangedInternal)//If the text is being changed by another member of this class do nothing
            {
                isTextChangedInternal = false; //It will only be getting changed once internally, next time do something.
                return;
            }
            if (!isLookupOn)
                return;
            if (ctlText.SelectionStart < ctlText.Text.Length)
                return;
            if ((m_LastKey == Keys.Back) || (m_LastKey == Keys.Delete))//Obviously we aren't going to find anything when they push Backspace or Delete
            {
                UpdateIndex();
                return;
            }
            if (m_LookupList == null || ctlText.Text.Length < 1)
                return;

            LookupInternal();

        }

        public void Lookup(string text) //Doesn't call the base so no wiring up this event for you.
        {
            if (!isLookupOn)
                return;
            if (m_LookupList == null || text.Length < 1)
                return;
            this.ctlText.Text = text;

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
                //if(ctlText.SelectionStart < ctlText.Text.Length)
                //    return;
                //if((m_LastKey == Keys.Back) || (m_LastKey == Keys.Delete))//Obviously we aren't going to find anything when they push Backspace or Delete
                //{
                //    UpdateIndex();
                //    return;
                //}
                //if(m_LookupList == null || ctlText.Text.Length < 1)
                //    return;

                int iOffset = 0;

				//Put the current text into temp storage
				string sText;
				sText = ctlText.Text;
				string sOriginal = sText;
				sText = sText.ToUpper();
				int iLength = sText.Length;
				string sFound = null;
				int index = 0;
				//see if what is currently in the text box matches anything in the string list
				for(index = 0; index < m_LookupList.Count; index++)
				{
					string sTemp = m_LookupList[index].ToUpper();
					if(sTemp.Length >= sText.Length)
					{
						if(sTemp.IndexOf(sText, 0, sText.Length) > -1)
						{
							sFound = m_LookupList[index];
							break;
						}
					}
				}
				if(sFound != null)
				{
					isTextChangedInternal = true;
					//if(isLookupDropDown)// && !DroppedDown )
					//{
						isTextChangedInternal = true;
						string sTempText = ctlText.Text;
						//this.DroppedDown = true;
						ctlText.Text = sTempText;
						isTextChangedInternal = false;
					//}
					if(ctlText.Text != sFound)
					{	
						ctlText.Text += sFound.Substring(iLength);
						ctlText.SelectionStart = iLength + iOffset;
						ctlText.SelectionLength = ctlText.Text.Length - iLength + iOffset;
						SelectedIndex = index;
						//base.OnSelectedIndexChanged(new EventArgs());
					}
					else
					{
						UpdateIndex();
						ctlText.SelectionStart = iLength;
						ctlText.SelectionLength = 0;
					}
				}
				else
				{
					isTextChangedInternal = true;
					SelectedIndex = -1;
					ctlText.Text = sOriginal;
					isTextChangedInternal = false;
					//base.OnSelectedIndexChanged(new EventArgs());
					ctlText.SelectionStart = sOriginal.Length;
					ctlText.SelectionLength = 0;
				}
			}
			catch(Exception)// ex)
			{
				//throw new Exception(ex.Message + "\r\nIn McLookUp.OnTextChanged(EventArgs).");
			}
		}

		//Put all the data from the ColumnView into a LookupList for quicker lookup.
		private void InitisLookupOnList()
		{
            if (this.DataSource == null) return;
			m_LookupList.Clear();
			foreach(DataRowView drv in DataSource)
			{
				string sTemp = drv[DisplayMember].ToString();
				m_LookupList.Add(sTemp);
			}
		}

        public void InitLookupList(bool async)
        {
            if (this.DataSource == null) return;
            m_LookupList.Clear();
            m_LookupList.AddRange(DataSource, DisplayMember, async);
        }

		//command the ComboBox to update its SelectedIndex.
		//This function will do that based on the current text.
		public void UpdateIndex()
		{
			try
			{
				//if(isInitItems)
				//	InitItems();
                if (this.DataSource == null) return;

				if(isInitLookupList)
					InitisLookupOnList();
				string sText = ctlText.Text;
				int index = 0;
				for(index = 0; index < DataSource.Count; index++)
				{
					if(DataSource[index][DisplayMember].ToString() == sText)
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
				if(index >= DataSource.Count)
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

		//Useful for setting the SelectedIndex to the index of a certain string.
		public int SetIndexOf(string sText)
		{
			try
			{
                int index = m_LookupList.IndexOf(sText); 

                ////see if what is currently in the text box matches anything in the string list
                //for(index = 0; index < m_LookupList.Count; index++)
                //{
                //    string sTemp = m_LookupList[index].ToUpper();
                //    if(sTemp == sText)
                //        break;
                //}
                //if(index >= m_LookupList.Count)
                //{
                //    index = -1;
                //}
				//m_SelectedIndex = index;
                SelectedIndex = index;
				//base.OnSelectedIndexChanged(new EventArgs());
				return index;
			}
			catch(Exception ex)
			{
                throw new Exception (ex.Message + "\r\nIn McLookUp.SetToIndexOf(string).");
			}
		}

        public int Find(string sText)
        {
            try
            {
                int index = m_LookupList.Find(sText);
                SelectedIndex = index;
                if (index < 0)
                {
                    MsgDlg.ShowMsg(RM.GetString("ValueNotFound"), "Nistec");
                }
                return index;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\r\nIn McLookUp.Find(string).");
            }
        }

        public int FindNext(string sText)
        {
            try
            {
                int index = m_LookupList.FindNext(sText);
                SelectedIndex = index;
                if (index < 0)
                {
                    MsgDlg.ShowMsg(RM.GetString("ValueNotFound"), "Nistec");
                }
                return index;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\r\nIn McLookUp.FindNext(string).");
            }
        }

        public void SetSelected(int index,bool value)
        {
            this.ctlList.SetSelected(index, value);
        }

        public void ClearSelected()
        {
            this.ctlList.ClearSelected();
        }

		#endregion

		#region Event members

		[Category("PropertyChanged"), Description("ListControlOnDataSourceChanged")]
		public event EventHandler DataSourceChanged;
		[Category("PropertyChanged"), Description("ListControlOnSelectedIndexChanged")]
		public event EventHandler SelectedIndexChanged;

		#endregion

		#region override

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated (e);
			//ctlList.ColumnCount=this.Columns.Count;
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize (e);
			if(Size.Width<MinimumWidth)
			{
				this.Width=MinimumWidth;
			}
		}

		protected override void OnRightToLeftChanged(EventArgs e)
		{
			base.OnRightToLeftChanged (e);
            if (this.ShowLookupPanel)
            {
                pnlLookupSetting();
            }
		}


		#endregion

		#region List Properties

        //[Browsable(false),EditorBrowsable(EditorBrowsableState.Advanced)]
        //public override Color BackColor
        //{
        //    get
        //    {
        //        return ctlList.BackColor;
        //    }
        //    set
        //    {
        //        ctlList.BackColor = value;
        //    }
        //}

        //[Browsable(false),EditorBrowsable(EditorBrowsableState.Advanced)]
        //public override Color ForeColor
        //{
        //    get
        //    {
        //        return ctlList.ForeColor;
        //    }
        //    set
        //    {
        //        ctlList.ForeColor = value;
        //    }
        //}


		[Localizable(true), DefaultValue(true), Category("Behavior"), Description("ListBoxIntegralHeight"), RefreshProperties(RefreshProperties.Repaint)]
		public bool IntegralHeight
		{
			get
			{
				return ctlList.IntegralHeight;
			}
			set
			{
				ctlList.IntegralHeight = value;
			}
		}
 
		[Category("Behavior"), RefreshProperties(RefreshProperties.Repaint), DefaultValue(13), Localizable(true), Description("ListBoxItemHeight")]
		public virtual int ItemHeight
		{
			get
			{
				return ctlList.ItemHeight;
			}
			set
			{
				ctlList.ItemHeight = value;
			}
		}
 
		//[Description("ListBoxItems"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Localizable(true), Category("Data"), Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public ListBox.ObjectCollection Items
		{
			get
			{
				return ctlList.Items;
			}
		}
 
		[Description("ListBoxPreferredHeight"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Advanced)]
		public int PreferredHeight
		{
			get
			{
				return ctlList.PreferredHeight;
			}
		}

		public override RightToLeft RightToLeft
		{
			get
			{
				return base.RightToLeft;
			}
			set
			{
				base.RightToLeft = value;
				ctlList.RightToLeft=value;
			}
		}

		[Localizable(true), DefaultValue(false), Category("Behavior"), Description("ListBoxScrollIsVisible")]
		public bool ScrollAlwaysVisible
		{
			get
			{
				return ctlList.ScrollAlwaysVisible;
			}
			set
			{
				ctlList.ScrollAlwaysVisible = value;
			}
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("ListBoxSelectedIndex"), Bindable(true), Browsable(false)]
		public virtual int SelectedIndex
		{
			get
			{
				return ctlList.SelectedIndex;
			}
			set
			{
				ctlList.SelectedIndex=value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("ListBoxSelectedIndex"), Bindable(true), Browsable(false)]
		public virtual object SelectedValue
		{
			get
			{
				return ctlList.SelectedValue;
			}
			set
			{
				ctlList.SelectedValue=value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), Description("ListBoxSelectedIndices")]
		public ListBox.SelectedIndexCollection SelectedIndices
		{
			get
			{
				return ctlList.SelectedIndices;
			}
		}

		[Bindable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("ListBoxSelectedItem"), Browsable(false)]
		public object SelectedItem
		{
			get
			{
				return ctlList.SelectedItems;
			}
			set
			{
				ctlList.SelectedItem=value;
			}
		}
 
		[Description("ListBoxSelectedItems"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ListBox.SelectedObjectCollection SelectedItems
		{
			get
			{
				return ctlList.SelectedItems;
			}
		}

		[Description("ListBoxSelectionMode"), DefaultValue(1), Category("Behavior")]
		public virtual SelectionMode SelectionMode
		{
			get
			{
				return ctlList.SelectionMode;
			}
			set
			{
				ctlList.SelectionMode = value;
			}
		}
		//		[DefaultValue(false), Category("Behavior"), Description("ListBoxSorted")]
		//		public bool Sorted
		//		{
		//			get
		//			{
		//				return ctlList.Sorted;
		//			}
		//			set
		//			{
		//					ctlList.Sorted = value;
		//			}
		//		}
 
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Bindable(false)]
		public override string Text
		{
			get
			{
				return ctlList.Text;
			}
			set
			{
				ctlList.Text = value;
			}
		}
 
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("ListBoxTopIndex"), Browsable(false)]
		public int TopIndex
		{
			get
			{
				return ctlList.TopIndex;
			}
			set
			{
				ctlList.TopIndex = value;
			}
		}

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Bindable(false)]
        public string LookupText
        {
            get
            {
                return ctlText.Text;
            }
            //set
            //{
            //    ctlText.Text = value;
            //}
        }
		#endregion

		#region Data

		public void DataBind(DataView value, bool forceAutoFill)
		{
			ctlList.DataBind(value,forceAutoFill);
		}

		public void DataBind(DataView value,string valueMember,string displayMember, bool forceAutoFill)
		{
			ctlList.DataBind(value,valueMember,displayMember,forceAutoFill);
		}

//		public void DataBind(DataView value, bool forceAutoFill)
//		{
//			ctlList.DataBind(value,forceAutoFill);
//		}
//
//		public void DataBind(DataView value,int columnValue,int columnView, bool forceAutoFill)
//		{
//			ctlList.DataBind(value,columnValue,columnView,forceAutoFill);
//		}

		[DefaultValue(null), Category("Data"), Description("ListControlDataSource"), TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), RefreshProperties(RefreshProperties.Repaint)]
		public DataView DataSource
		{
			get{ return  ctlList.DataSource; }
			set
			{ 
				ctlList.DataSource=value;
				this.Invalidate(true);
			}
		}

		#endregion

		#region MultiColumns

		[Category("Columns"),DefaultValue(true) ]
		public bool AutoFill
		{
			get{return ctlList.AutoFill;}
			set{ctlList.AutoFill=value;}
		}

		[Category("Columns"),DefaultValue(true) ]
		public bool AllowSort
		{
			get{return ctlList.AllowSort;}
			set{ctlList.AllowSort=value;}
		}


		[Category("Columns"),DefaultValue(80) ]
		public int PerformColumnWidth
		{
			get{return ctlList.PerformColumnWidth;}
			set
			{
				if(value >0 && value <=this.Width)
				{
					ctlList.PerformColumnWidth=value;
				}
			}
		}


		//Convenient for resorting the ComboBox based on a column.
        public void SortBy(string Col, SortDiraction so)
		{
			ctlList.SortBy(Col, so);
			//base.Invalidate(true);
			//isInitItems = true;
		}


		[Category("Columns")]
		public int ColumnSpacing
		{
			get
			{
				if(ctlList==null)
					return 3;

				return ctlList.ColumnSpacing;
			}
			set
			{
				if(value < 0 || value > 10)
					throw new Exception("ColumnSpacing must be between 0 and 10");
				if(ctlList!=null)
					ctlList.ColumnSpacing = value;
			}
		}

		[Category("Columns"),DefaultValue("")]
		public string ValueMember
		{
			get
			{
				return ctlList.ValueMember;
			}
			set
			{
				ctlList.ValueMember = value;
			}
		}

		[Category("Columns"),DefaultValue("")]
		public string DisplayMember
		{
			get
			{
				return ctlList.DisplayMember;
			}
			set
			{
				ctlList.DisplayMember = value;
			}
		}

//		[Category("Columns"),DefaultValue(0)]
//		public int ColumnView
//		{
//			get
//			{
//				return ctlList.ColumnView;
//			}
//			set
//			{
//				ctlList.ColumnView = value;
//			}
//		}
//
//		[Category("Columns"),DefaultValue(0)]
//		public int ColumnValue
//		{
//			get
//			{
//				return ctlList.ColumnValue;
//			}
//			set
//			{
//				ctlList.ColumnValue = value;
//			}
//		}


		//Does nothing... yet
		//		public new bool Sorted
		//		{
		//			get
		//			{
		//				return false;
		//			}
		//		}

		//Indexer for retriving values based on the column string.
		//Will return the value of the given column at SelectedIndex row.
		[Browsable(false),EditorBrowsable(EditorBrowsableState.Advanced),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object this[string ColName]
		{
			get
			{
				return ctlList[ColName];
			}
			set
			{
				try
				{
					ctlList[ColName]= value;
				}
				catch(Exception ex)
				{
                    MsgBox.ShowError(ex.Message + "\r\nIn McColumnList[string](set).");
				}
			}
		}

		//Indexer for retriving values based on the column string.
		//Will return the value of the given column at SelectedIndex row.
		[Browsable(false),EditorBrowsable(EditorBrowsableState.Advanced),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object this[int ColIndex]
		{
			get
			{
				try
				{
					return ctlList[ColIndex];
				}
				catch(Exception ex)
				{
					throw new Exception(ex.Message + "\r\nIn McColumnList[string](get).");
				}
			}
			set
			{
				try
				{
					ctlList[ColIndex] = value;
				}
				catch(Exception ex)
				{
                    MsgBox.ShowError(ex.Message + "\r\nIn McColumnList[string](set).");
				}
			}
		}

		[Browsable(false),EditorBrowsable(EditorBrowsableState.Advanced),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object this[int Row ,int ColIndex]
		{
			get
			{
				try
				{
					if(Row < 0 || DataSource==null)
						return null;
					return ctlList.DataSource[Row][ColIndex];
				}
				catch(Exception ex)
				{
					throw new Exception(ex.Message + "\r\nIn McColumnList[string](get).");
				}

			}
			set
			{
				try
				{
                    if (this.DataSource == null) return;

					ctlList.DataSource[Row][ColIndex] = value;
				}
				catch(Exception ex)
				{
                    MsgBox.ShowError(ex.Message + "\r\nIn McColumnList[string](set).");
				}
			}
		}

		[Category("Columns"),DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Localizable(true)]
		public McColumnCollection Columns
		{
			get
			{
				return ctlList.Columns;
			}
		}



		#endregion

		#region Filter

		[Category("Filter"),DefaultValue(LookupFilterMode.None)]
		public LookupFilterMode LookupFilterMode
		{
			get {return ctlList.LookupFilterMode;}
			set{ctlList.LookupFilterMode=value;}
		}

		[Category("Filter"),DefaultValue("")]
		public string RowFilter
		{
			get {return ctlList.RowFilter;}
			set{ctlList.RowFilter=value;}
		}

		[Category("Filter"),DefaultValue("")]
		public string ControlBoundName
		{
			get {return ctlList.ControlBoundName;}
			set{ctlList.ControlBoundName=value;}
		}

		[Category("Filter"),DefaultValue(null)]
		public Control ControlFilterBound
		{
			get {return ctlList.ControlFilterBound;}
			set{ctlList.ControlFilterBound=value;}
		}

		public void SetFilterBound()
		{
			ctlList.SetFilterBound();
		}
		public void SetFilterBound(string filter)
		{
			this.ctlList.RowFilter=filter;
			ctlList.SetFilterBound();
		}
		#endregion

		#region ColumnDisply

		public string ColumnDisplay
		{
			get{return ctlList.ColumnDisplay;}
			set
			{
				ctlList.ColumnDisplay=value;
			}
		}

		public void SetColumns(int[] ColumnWidth )
		{
			ctlList.SetColumns(ColumnWidth);
		}

		public void SetColumns(string[]ColumnNames,int[] ColumnWidth )
		{
			ctlList.SetColumns(ColumnNames,ColumnNames,ColumnWidth );
		}

		public void SetColumns(string[]ColumnNames,string[] Captions,int[] ColumnWidth )
		{
			ctlList.SetColumns(ColumnNames,Captions,ColumnWidth );
		}

		#endregion

		#region events

		private void pnlHeader_Paint(object sender, PaintEventArgs e)
		{
			//DrawColumnHeaders(e);
		}
        void pnlHeader_CustomDrow(object sender, PaintEventArgs e)
        {

            DrawColumnHeaders(e);
        }

		private void ctlList_DataSourceChanged(object sender, System.EventArgs e)
		{
			OnDataSourceChanged(e);
		}

		private void ctlList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			OnSelectedIndexChanged(e);
		}


		protected virtual void OnDataSourceChanged(System.EventArgs e)
		{
			if(DataSourceChanged!=null)
				this.DataSourceChanged(this,e);
		}

		protected virtual void OnSelectedIndexChanged(System.EventArgs e)
		{
			if(SelectedIndexChanged!=null)
				this.SelectedIndexChanged(this,e);
		}

		private void ctlList_DoubleClick(object sender, EventArgs e)
		{
			this.OnDoubleClick(e);
		}

		private void ctlList_Click(object sender, EventArgs e)
		{
			this.OnClick(e);
		}

		private void ctlList_MouseDown(object sender, MouseEventArgs e)
		{
			this.OnMouseDown(e);
		}

		private void ctlList_MouseUp(object sender, MouseEventArgs e)
		{
			this.OnMouseUp(e);
		}

		private void ctlList_KeyDown(object sender, KeyEventArgs e)
		{
			this.OnKeyDown(e);
		}

		private void ctlList_KeyUp(object sender, KeyEventArgs e)
		{
			this.OnKeyUp(e);
		}

		#endregion

		#region Properties

		[Browsable(false),EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool AutoChildrenStyle
		{
			get{return base.AutoChildrenStyle;}
			set
			{
				base.AutoChildrenStyle=value;
			}
		}

		[Category("Behavior"), DefaultValue((string) null)]
		public new ImageList ImageList
		{
			get
			{
				if(ctlList==null)
					return null;
				return ctlList.ImageList;
			}
			set
			{
				if(ctlList!=null)
				{
					ctlList.ImageList = value;
					this.Invalidate();
				}
			}
		}

		[DefaultValue(-1), Category("Columns")]
		public int ColumnImage
		{
			get
			{
				if(ctlList==null)
					return -1;
				return ctlList.ColumnImage;
			}
			set
			{
				if(ctlList!=null)
				{
					if(value < -1 || value > Columns.Count)
					{
						throw new ArgumentException("Index is out of range");
					}
					ctlList.ColumnImage = value;
					this.Invalidate();
				}
			}
		}

		[DefaultValue(true), Category("Appearance")]
		public bool AlternatingDraw
		{
			get
			{
				return ctlList.AlternatingDraw;
			}
			set
			{
				ctlList.AlternatingDraw = value;
				this.Invalidate();
			}
		}

		[DefaultValue(true), Category("Columns")]
		public bool ShowColumnHeaders
		{
			get
			{
				return ishowHeadings;
			}
			set
			{
				if(ishowHeadings!=value)
				{
					ishowHeadings=value;
					this.pnlHeader.Visible=value;
					//CreateHeaders();
					base.Invalidate();
				}
			}
		}

		#endregion

		#region ColumnHeaders

		//		protected override void OnPaint(PaintEventArgs e)
		//		{
		//			//base.OnPaint (e);
		//			Rectangle rect = new Rectangle(1, 1, this.Width-2, this.Height-2);
		//			//base.LayoutManager.DrawBorder(e.Graphics,rect,false,this.Enabled,this.Focused,false);
		//			ControlPaint.DrawBorder(e.Graphics,this.ctlList.ClientRectangle,Color.Red,ButtonBorderStyle.Solid);
		//	
		//		}

		//		private int[] m_SortOrders;

		internal protected virtual void DrawColumnHeaders(PaintEventArgs pe)
		{
			if(!ShowColumnHeaders)
				return;

			int top=this.pnlHeader.Top;
			int width=this.Width-2;  
			Rectangle rect=this.pnlHeader.ClientRectangle;
            Rectangle rectH = new Rectangle(rect.X, rect.Y, rect.Width - 1, rect.Height - 1);

            using (Brush sb = LayoutManager.GetBrushGradient(rectH, 270f))
            {
                pe.Graphics.FillRectangle(sb, rectH);
            }

			int lft=0;  
			int remain=width;  

			using(System.Drawing.Brush bf =LayoutManager.GetBrushText())//, bb = LayoutManager.GetBrushGradient(rect,270))//.GetBrushCaptionGradient(rect,90,true))
			{
				//pe.Graphics.FillRectangle(bb,rect);
				using(System.Drawing.Pen p = this.LayoutManager.GetPenBorder())
				{
					Rectangle r=Rectangle.Empty;
					remain= width;;
					int i=0;
					bool rtl=(this.RightToLeft==RightToLeft.Yes);
					foreach(McColumn c in this.Columns)
					{
						if((c.Width>0) && (c.Display) && (remain>0))
						{
							int colWidth= (c.Width > remain)? remain:c.Width;
							remain= width - c.Width;
							//Rectangle strRect=new Rectangle(rect.X+lft,rect.Top+2,colWidth,rect.Height-2);
							if(rtl)
							{
								r=new Rectangle(rect.Width-(lft+colWidth),rect.Top,colWidth,rect.Height);
							}
							else
							{
								r=new Rectangle(rect.X+lft,rect.Top,colWidth,rect.Height);
							}
							//pe.Graphics.FillRectangle(bb,r);
							pe.Graphics.DrawRectangle (p,r);
							if(activeHeaderDisply==i)
							{
								int activeCol=ctlList.activeColumnSort;
								Icon icon=null;
                                if (ctlList.Columns[activeCol].SortOrder == SortDiraction.ASC)
									icon=McLookUpList.icoAsc;
								else
									icon=McLookUpList.icoDesc;
								//if(ctlList.m_SortOrders[i]==0)
								//	icon=McLookUpList.icoAsc;
								//else
								//	icon=McLookUpList.icoDesc;
						
								pe.Graphics.DrawIcon(icon,r.X +4,(r.Height-icon.Height)/2);
			
							}

							Rectangle strRect=new Rectangle(r.X,r.Top+2,colWidth,r.Height-2);
							using(StringFormat sf=new StringFormat())
							{
								sf.Alignment=StringAlignment.Center;
								sf.Trimming = StringTrimming.EllipsisCharacter;
								sf.FormatFlags = 0;
								pe.Graphics.DrawString(c.Caption,this.Font,bf,(RectangleF)strRect,sf);
							}
							lft+=colWidth;
							i++;
						}
					}
				}
			}
		}

		private bool SetHeaderColumn(int x)
		{
			int point=0;
			int nonDisplay=0;

			if(this.RightToLeft==RightToLeft.Yes)
			{
                //point=this.pnlHeader.Width;
                //for(int i =Columns.Count-1;i>=0;i--)
                //{
                //    if(Columns[i].Display)
                //    {
                //        point-=Columns[i].Width;
                //    }
                //    else
                //        nonDisplay++;
                //    if(x>=point)
                //    {
                //        activeHeaderDisply=(Columns.Count-1)-i+nonDisplay;
                //        ctlList.activeColumnSort=nonDisplay+activeHeaderDisply;
                //        return true;
                //    }
                //}
                for (int i = Columns.Count - 1; i >= 0; i--)
                {
                    if (Columns[i].Display)
                    {
                        point += Columns[i].Width;
                    }
                    else
                        nonDisplay++;
                    if (x <= point)
                    {
                        activeHeaderDisply = i - nonDisplay;
                        ctlList.activeColumnSort = nonDisplay + activeHeaderDisply;
                        return true;
                    }
                }
			}
			else
			{
				for(int i =0;i< Columns.Count;i++)
				{
					if(Columns[i].Display)
					{
						point+=Columns[i].Width;
					}
					else
						nonDisplay++;
					if(x<=point)
					{
						activeHeaderDisply=i-nonDisplay;
						ctlList.activeColumnSort=nonDisplay+activeHeaderDisply;
						return true;
					}
				}
			}
			activeHeaderDisply=-1;
			ctlList.activeColumnSort=-1;
			return false;
		}

		private void pnlHeader_MouseUp(object sender, MouseEventArgs e)
		{
            if (this.DataSource == null || !ctlList.AllowSort || e.Button != MouseButtons.Left) return;
			try
			{
				bool ok=SetHeaderColumn(e.X);
				//int colSort=GetHeaderColumn(e.X);	
				//ctlList.activeColumnSort=colSort;
				if(ok)
				{
					int colSort=ctlList.activeColumnSort;
					this.SortBy(Columns[colSort].ColumnName,ctlList.Columns[colSort].SortOrder);
					//ctlList.m_SortOrders[colSort]=Math.Abs(ctlList.m_SortOrders[colSort]-1);
					ctlList.Columns[colSort].ToggelSortOrder();
					this.pnlHeader.Invalidate();
				}
			}
			catch(Exception ex)
			{
				MsgBox.ShowError(ex.Message);
			}
		}

		#endregion

		#region public Methods

		public virtual  void PerformImport()
		{
			this.ctlList.PerformImport();			
		}

		public virtual  void PerformExport()
		{
			this.ctlList.PerformExport();			
		}
        public virtual void PerformExport(string filter, string sort)
        {
            this.ctlList.PerformExport(filter, sort);			
        }
		public virtual void PerformPrint()
		{
			this.ctlList.PerformPrint();			
		}

		public virtual void PerformFilter()
		{
			this.ctlList.PerformFilter();			
		}

        //public virtual void PerformGetDataDB()
        //{
        //    this.ctlList.PerformGetDataDB();
        //    this.Invalidate(true);
        //}
		#endregion

        #region StyleProperty

        [Category("Style"), DefaultValue(typeof(Color), "WindowText")]
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set
            {
                SerializeForeColor(value, true);
            }
        }

        [Category("Style"), DefaultValue(typeof(Color), "Window")]
        public override Color BackColor
        {
            get { return base.BackColor; }
            set
            {
                SerializeBackColor(value, true);
            }
        }

        protected override void OnStylePropertyChanged(PropertyChangedEventArgs e)
        {
            if (this.btnMenu != null)
            {
                this.btnMenu.StylePainter = this.StylePainter;
            }

            if (e.PropertyName.Equals("ControlLayout") || e.PropertyName.Equals("BackColor"))
                SerializeBackColor(Color.Empty, false);
            if (e.PropertyName.Equals("ControlLayout") || e.PropertyName.Equals("ForeColor"))
                SerializeForeColor(Color.Empty, false);
            if (e.PropertyName.Equals("ControlLayout") || e.PropertyName.Equals("TextFont"))
                SerializeFont(Form.DefaultFont, false);
            if ((DesignMode || IsHandleCreated))
                this.Invalidate(true);
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected void SerializeFont(Font value, bool force)
        {
            if (ShouldSerializeForeColor())
                this.Font = LayoutManager.Layout.TextFontInternal;
            else if (force)
                this.Font = value;
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected void SerializeForeColor(Color value, bool force)
        {
            if (ShouldSerializeForeColor())
            {
                ctlList.ForeColor = LayoutManager.Layout.ForeColorInternal;
            }
            else if (force)
            {
                ctlList.ForeColor = value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected void SerializeBackColor(Color value, bool force)
        {
            if (ShouldSerializeBackColor())
            {
                ctlList.BackColor = LayoutManager.Layout.BackColorInternal;
            }
            else if (force)
            {
                ctlList.BackColor = value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected bool ShouldSerializeBackColor()
        {
            return IsHandleCreated && StylePainter != null;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected bool ShouldSerializeForeColor()
        {
            return IsHandleCreated && StylePainter != null;
        }

        [Category("Apperarace"), DefaultValue(BorderStyle.FixedSingle)]
        public BorderStyle BorderStyle
        {
            get { return ctlList.BorderStyle; }
            set { ctlList.BorderStyle = value; }
        }

        protected override void OnStylePainterChanged(EventArgs e)
        {
            base.OnStylePainterChanged(e);
            OnStylePropertyChanged(new PropertyChangedEventArgs("ControlLayout"));
        }

        #endregion

        #region IBind Members

		//private bool readOnly=false;

		public bool ReadOnly
		{
			get
			{
				return false;
			}
			set
			{
				// TODO:  Add McLookUpList.ReadOnly setter implementation
			}
		}

		[Browsable(false)]
		public BindingFormat BindFormat
		{
			get{return BindingFormat.String;}
		}

		public string BindPropertyName()
		{
			
			return  "SelectedValue";
		}
 
        public virtual void BindDefaultValue()
        {
            //if (ctlList.DefaultValue.Length > 0)
            //{
                ctlList.SelectedValue = ctlList.DefaultValue;
                //if (base.IsHandleCreated)
                //{
                //    this.OnSelectedIndexChanged(EventArgs.Empty);
                //}
                //}
        }
		#endregion

	}
}
