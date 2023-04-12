using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Nistec.WinForms;

namespace Nistec.Wizards
{

  
	/// <summary>
	/// Summary description for InsertList.
	/// </summary>
	[Designer(typeof(Design.McEditListDesigner))]
	[System.ComponentModel.ToolboxItem(true)]
	[ToolboxBitmap (typeof(McEditList), "Toolbox.EditList.bmp")]
    public class McEditList : Nistec.WinForms.Controls.McContainer//Nistec.WinForms.McEditBase
	{

		#region delegate
		public event  ItemInsertEventHandler ItemInsert ;
		public event  ItemUpdateEventHandler ItemUpdate ;
		public event  ItemRemoveEventHandler ItemRemove ;
		#endregion

		#region Members
		private Nistec.WinForms.McListBox	m_ListBox;
		private Nistec.WinForms.McTextBox	m_TextBox;
		private System.Windows.Forms.ToolTip		toolTip1;
		private System.Windows.Forms.ContextMenu	contextMenu1;
		private bool								m_AllowAdd;
		private bool								m_AllowEdit;
		private bool								m_AllowRemove;
        private EditListType m_ListBoxType; 
		private System.Drawing.Color				m_RectColor;
		private bool allowLookup;
        private bool caseSensitive;
        private System.Windows.Forms.BorderStyle m_BorderStyle;
 

		#endregion

		#region MemuItems
		//private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem mnuAdd;
		private System.Windows.Forms.MenuItem mnuEdit;
		private System.Windows.Forms.MenuItem mnuRemove;
		private System.Windows.Forms.MenuItem mnuserch;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.MenuItem mnuSort;
		#endregion

		#region Constructor

		public McEditList():base()//BorderStyle.FixedSingle)
		{
            m_AllowAdd = true;
            m_AllowEdit = true;
            m_AllowRemove = true;
            this.allowLookup = true;
            caseSensitive = false;
            m_ListBoxType = EditListType.Insert;
            m_RectColor = Color.Yellow;

            m_BorderStyle = BorderStyle.FixedSingle;


			InitializeComponent();
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
			this.m_ListBox = new Nistec.WinForms.McListBox();
			this.m_TextBox = new Nistec.WinForms.McTextBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.mnuAdd = new System.Windows.Forms.MenuItem();
			this.mnuEdit = new System.Windows.Forms.MenuItem();
			this.mnuRemove = new System.Windows.Forms.MenuItem();
			this.mnuserch = new System.Windows.Forms.MenuItem();
			this.mnuSort = new System.Windows.Forms.MenuItem();
			this.SuspendLayout();
			// 
			// m_ListBox
			// 
			this.m_ListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.m_ListBox.Location = new System.Drawing.Point(0, 0);
			this.m_ListBox.Name = "m_ListBox";
			this.m_ListBox.Size = new System.Drawing.Size(110, 54);
			this.m_ListBox.TabIndex = 2;
			this.toolTip1.SetToolTip(this.m_ListBox, "To Edit item press insert ,To remove item press Delete");
			this.m_ListBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mListBox_KeyDown);
			this.m_ListBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mListBox_MouseDown);
			this.m_ListBox.SelectedIndexChanged += new System.EventHandler(this.mListBox_SelectedIndexChanged);
			// 
			// m_TextBox
			// 
			this.m_TextBox.AcceptsReturn = true;
			this.m_TextBox.BackColor = System.Drawing.Color.White;
			this.m_TextBox.BorderStyle = BorderStyle.None ;
			this.m_TextBox.ForeColor = System.Drawing.Color.Black;
			this.m_TextBox.Location = new System.Drawing.Point(0, 0);
			this.m_TextBox.MaxLength = 32567;
			this.m_TextBox.Name = "m_TextBox";
			this.m_TextBox.Size = new System.Drawing.Size(112, 13);
			this.m_TextBox.TabIndex = 1;
			this.m_TextBox.Text = "";
			this.toolTip1.SetToolTip(this.m_TextBox, "To Update item press Enter");
			this.m_TextBox.Visible = false;
			this.m_TextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_TextBox_KeyPress);
			this.m_TextBox.TextChanged += new System.EventHandler(this.m_TextBox_TextChanged);
			this.m_TextBox.Leave += new System.EventHandler(this.m_TextBox_Leave);
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.mnuAdd,
																						 this.mnuEdit,
																						 this.mnuRemove,
																						 this.mnuserch,
																						 this.mnuSort});
			this.contextMenu1.Popup += new System.EventHandler(this.contextMenu1_Popup);
			// 
			// mnuAdd
			// 
			this.mnuAdd.Index = 0;
			this.mnuAdd.Text = "Add new";
			this.mnuAdd.Click += new System.EventHandler(this.mnuAdd_Click);
			// 
			// mnuEdit
			// 
			this.mnuEdit.Index = 1;
			this.mnuEdit.Text = "Edit";
			this.mnuEdit.Click += new System.EventHandler(this.mnuEdit_Click);
			// 
			// mnuRemove
			// 
			this.mnuRemove.Index = 2;
			this.mnuRemove.Text = "Remove";
			this.mnuRemove.Click += new System.EventHandler(this.mnuRemove_Click);
			// 
			// mnuserch
			// 
			this.mnuserch.Index = 3;
			this.mnuserch.Text = "Find";
			this.mnuserch.Click += new System.EventHandler(this.mnuserch_Click);
			// 
			// mnuSort
			// 
			this.mnuSort.Index = 4;
			this.mnuSort.Text = "Sort";
			this.mnuSort.Click += new System.EventHandler(this.mnuSort_Click);
			// 
			// McEditList
			// 
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this.m_TextBox);
			this.Controls.Add(this.m_ListBox);
			this.Name = "McEditList";
			this.Size = new System.Drawing.Size(112, 56);
			this.toolTip1.SetToolTip(this, "To Update item press Enter To remove item press Delete");
			this.ResumeLayout(false);

		}
		#endregion

		#region property

		[DefaultValue(true)]
		public virtual bool AllowLookup
		{
			get{return allowLookup;}
			set
			{
				allowLookup=value;
			}
		}
        
        [DefaultValue(false)]
        public virtual bool AllowSensitive
		{
            get { return caseSensitive; }
			set
			{
                caseSensitive = value;
			}
		}

		[Category("Style")]
        [RefreshProperties(RefreshProperties.All), DefaultValue(EditListType.Insert)]
        public EditListType EditListType
		{
			get{return m_ListBoxType;}
			set
			{  
				if(	m_ListBoxType!=value)
				{
					m_ListBoxType=value;
					SetSize();
					this.Invalidate() ;
				}
			}
		}

        [Category("Appearance")]
        [DefaultValue(System.Windows.Forms.BorderStyle.FixedSingle)]
        public  System.Windows.Forms.BorderStyle BorderStyle
        {
            get { return m_BorderStyle; }
            set
            {
                if (BorderStyle != value)
                {
                    m_BorderStyle =value;
                    //this.m_ListBox.BorderStyle =value;
                    //this.m_TextBox.BorderStyle =value; 
                    this.Invalidate();
                }
            }
        }


		[System.ComponentModel.RefreshProperties(RefreshProperties.Repaint)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public Nistec.WinForms.McListBox List
		{
			get{return m_ListBox;}
		}

		[System.ComponentModel.Browsable (false)]  
		public Nistec.WinForms.McTextBox  McTextBox
		{
			get{return m_TextBox;}
		}

		[Category("Behavior")]
		[DefaultValue(true)]
		public bool  AllowAdd
		{
			get{return m_AllowAdd ;}
			set 
			{
				m_AllowAdd=value;
				this.mnuAdd.Enabled =value;
			}
		}
		
		[Category("Behavior")]
		[DefaultValue(true)]
		public  bool  AllowEdit
		{
			get{return m_AllowEdit ;}
			set 
			{
				m_AllowEdit=value;
				this.mnuEdit.Enabled =value;   
			}
		}
	
		[Category("Behavior")]
		[DefaultValue(true)]
		public bool  AllowRemove
		{
			get{return m_AllowRemove ;}
			set 
			{
				m_AllowRemove=value;
				this.mnuRemove.Enabled =value;   
			}
		}

		#endregion

		#region ListProperty

		[System.ComponentModel.Browsable (false)] 
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(-1)]
		public int SlectedIndex
		{
			get{return m_ListBox.SelectedIndex;}  
			set{m_ListBox.SelectedIndex=value;}  
		}

		[System.ComponentModel.Browsable (false)] 
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object SlectedValue
		{
			get{return m_ListBox.SelectedValue;}  
			set{m_ListBox.SelectedValue=value;}  
		}

		[System.ComponentModel.Browsable (false)] 
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object SlectedItem
		{
			get{return m_ListBox.SelectedItem ;}  
			set{m_ListBox.SelectedItem=value;}  
		}

		#endregion

		#region McTextBox

		[DefaultValue(typeof(Color),"Yellow")]
		public System.Drawing.Color SelectionEditColor
		{
			get{return m_RectColor;}
			set{m_RectColor=value;}
		}

		private void m_TextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
    		switch(e.KeyData)
			{
				case Keys.Enter :
					EnterItem();
					break;
				case Keys.Insert:
					EnterItem();
					break;
			  
			}
			base.OnKeyDown (e);
		}

		private void m_TextBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar==13)
			{
				EnterItem();
			}
			else if (e.KeyChar==27)
			{
				if(this.m_ListBoxType ==EditListType.Edit )
					this.m_TextBox.Visible=false;
				else if(this.m_ListBoxType ==EditListType.Insert  )
				{
					this.m_TextBox.Text ="";//m_ListBox.SelectedItem.ToString () ;
					//this.m_ListBox.SelectedIndex =-1; 
				}
			}
			base.OnKeyPress (e);
		}



		private void m_TextBox_Leave(object sender, System.EventArgs e)
		{
			if(m_ListBoxType==EditListType.Edit )
				this.m_TextBox.Visible=false;
		}

		#endregion

		#region Find
		private void m_TextBox_TextChanged(object sender, System.EventArgs e)
		{
		  base.OnTextChanged (e);
           if(this.m_ListBoxType ==EditListType.Insert )
			  FindString(this.m_TextBox.Text);
		}

		private void FindString(string searchString)
		{
			// Set the SelectionMode property of the McListBox to select multiple items.
			m_ListBox.SelectionMode = SelectionMode.One;// .MultiExtended;
   
			// Set our intial index variable to -1.
			int x =-1;
			// If the search string is empty exit.
			if (searchString.Length != 0)
			{
				// Loop through and find each item that matches the search string.
				//do
				//{
				// Retrieve the item based on the previous index found. Starts with -1 which searches start.
				x = m_ListBox.FindString(searchString, x);
				// If no item is found that matches exit.
				if (x != -1)
				{
					// Since the FindString loops infinitely, determine if we found first item again and exit.
					if (m_ListBox.SelectedIndices.Count > 0)
					{
						if(x == (int)m_ListBox.SelectedIndices[0])
							return;
					}
					// Select the item in the McListBox once it is found.
					m_ListBox.SetSelected(x,true);
				}
   
				//}while(x != -1);
			}
		}
		#endregion

		#region override

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
          base.OnPaint(e);
			if(m_ListBoxType==EditListType.Insert  )
			{
				//444m_TextBox.StyleCtl.SetStyleLayout(m_ListBox.StyleCtl.Layout);
			}
		}
		
		protected override void OnSizeChanged(System.EventArgs e)
		{
			base.OnSizeChanged (e);
			SetSize();
		}

		protected override void OnFontChanged(System.EventArgs e)
		{
			base.OnFontChanged (e);
			m_ListBox.Font=this.Font;
			m_TextBox.Font=this.Font;
		}

		protected  void SetSize()
		{
			if(m_ListBoxType==EditListType.Edit )
			{
				m_TextBox.Visible =false;
				m_ListBox.Location =new Point (1,1);
				m_ListBox.Size  =new Size (this.Width-2 ,this.Height-4);
				m_TextBox.Size   =new Size (m_ListBox.Width ,m_ListBox.ItemHeight);
				m_TextBox.BackColor  =this.m_RectColor ;
				m_TextBox.BorderStyle   =BorderStyle.None ;
			}
			else if(m_ListBoxType==EditListType.Insert  )
			{
				m_TextBox.BorderStyle   = m_ListBox.BorderStyle ;
				m_TextBox.Location =new Point (1,1);
				m_TextBox.Size   =new Size (this.Width-2 ,20);
				m_ListBox.Location =new Point (1,this.m_TextBox.Top + m_TextBox.Height +2);
				m_ListBox.Size  =new Size (this.Width-2 ,this.Height -m_TextBox.Height-6);
				m_TextBox.BackColor  =this.m_ListBox.BackColor ;
				m_TextBox.Visible =true;
			}
			else //if(m_ListBoxType==EditListType.Default )
			{
				m_ListBox.Location =new Point (1,1);
				m_ListBox.Size  =new Size (this.Width-2 ,this.Height-4 );
				m_TextBox.Visible =false;
			}
		}

		private void mListBox_Click(object sender, System.EventArgs e)
		{
			base.OnClick (e);
			this.m_TextBox.Text  = m_ListBox.SelectedItem.ToString();
			int i=this.m_ListBox.SelectedIndex;  
			if (i==indx && m_AllowEdit)
				BoxFocus();
			indx=i;
		}

		private void mListBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			// Get the currently selected item in the McListBox.
			this.m_TextBox.Text  = "";
			if(m_ListBox.SelectedItem!=null) 
			{
				this.m_TextBox.Text  = m_ListBox.SelectedItem.ToString();
				if(m_ListBoxType==EditListType.Edit )
				{
					int i=this.m_ListBox.SelectedIndex;  
					if (i==indx && m_AllowEdit)
						BoxFocus();
					indx=i;
				}
			}
		}

		private void mListBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
		
			if(e.KeyData==Keys.Delete )
			{
				RemoveItem();
			} 
			else if(e.KeyData==Keys.Insert )
			{
				if(this.m_ListBoxType ==EditListType.Edit )
				{
					if(m_AllowAdd)
						BoxFocus();
				}
			}
			base.OnKeyDown (e);
		}
		#endregion

		#region Methods

		private int indx=-1;

		private void BoxFocus()
		{
			int itemSelected=this.m_ListBox.SelectedIndex;
			if (itemSelected<0 || ! m_AllowEdit)
				return;

			string itemText=this.m_ListBox.Items[itemSelected].ToString();
															 
			Rectangle r=this.m_ListBox.GetItemRectangle(itemSelected);
			Rectangle l=this.m_ListBox.Bounds ;

			System.Drawing.Point p; 
			if (this.m_ListBox.RightToLeft ==RightToLeft.Yes )
				p=new System.Drawing.Point(r.X+18,l.Y+ r.Y+1);
			else
				p=new System.Drawing.Point(r.X+2,l.Y+ r.Y+1);
	        
			//BoxType="Edit";
			this.m_TextBox.BackColor=m_RectColor; 
			this.m_TextBox.Location=p;
			this.m_TextBox.Size=new System.Drawing.Size(r.Width , r.Height-10);
			this.m_TextBox.Visible=true;
			this.m_TextBox.Text=itemText;
			this.m_TextBox.Focus();
			this.m_TextBox.SelectAll();
		}

		private void RemoveItem()
		{
			if(this.SlectedIndex>=0)
			{
				object val=this.m_ListBox.SelectedItem;
				int indx=this.m_ListBox.SelectedIndex;
				if(m_AllowRemove)
					this.m_ListBox.Items.RemoveAt (this.SlectedIndex);   
				if(ItemRemove!=null )
					ItemRemove( this.m_ListBox, new  ItemRemoveEvent ( this.m_ListBox ,indx,val ) );
                    
				if(this.m_ListBox.Items.Count >0)   
					this.m_ListBox.SetSelected (0,true);//Refresh ();
				else
					this.m_ListBox.Items.Clear ();
			}
	
		}


		private int SerchItem()
		{
			int i=this.m_ListBox.FindString (this.m_TextBox.Text);     
			if(i>=0)
				this.m_ListBox.SetSelected (i,true);
			return i;
		}

		private void EnterItem()
		{
            string text = this.m_TextBox.Text;

			if(this.m_ListBoxType ==EditListType.Insert )
			{
                if (text == String.Empty)
					return;
				int i=-1;
				if(this.allowLookup)
				{
					//int i=SerchItem();
                    i = this.m_ListBox.FindStringExact(text);     
				}
                if (i >= 0)
                {
                    if (this.caseSensitive)
                    {
                        if (!text.Equals(this.m_ListBox.Items[i].ToString()))
                            i = -1;
                    }
                    else
                    {
                        this.m_ListBox.SetSelected(i, true);
                    }
                }
                if (i <0 && m_AllowAdd)
                {
                    this.m_ListBox.Items.Add(text);
                    if (ItemInsert != null)
                        ItemInsert(this.m_ListBox, new ItemInsertEvent(this.m_ListBox, text));
                }
				this.McTextBox.Text ="";  
			}
			else if(this.m_ListBoxType ==EditListType.Edit  )
			{
				if(m_AllowEdit)
				{
					this.m_ListBox.Items[m_ListBox.SelectedIndex]= this.m_TextBox.Text;     
					if(ItemUpdate!=null)
						ItemUpdate( this.m_ListBox,new ItemUpdateEvent ( this.m_ListBox ,this.m_TextBox.Text ));
					//m_ListBox.SelectedIndex=-1;
				}
				this.m_TextBox.Visible=false;
			}
	
		}

		#endregion

		#region ContextMenu
		private void mListBox_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if( e.Button == MouseButtons.Right)
                this.contextMenu1.Show(this, new Point(e.X, e.Y));
           base.OnMouseDown (e);
		}

		private void mnuAdd_Click(object sender, System.EventArgs e)
		{
			if(m_ListBoxType==EditListType.Edit )
			{
                object o = MsgDlg.ShowDialog(this, "Add new item", "Nistec.Add Item");
				if(!o.Equals (""))
				{
	  			  if( m_AllowAdd)
						 this.m_ListBox.Items.Add (this.m_TextBox.Text );     
	  			  if(ItemInsert!=null)
						ItemInsert( this.m_ListBox,new ItemInsertEvent ( this.m_ListBox ,o.ToString () ));
				}
			}
		}

		private void mnuEdit_Click(object sender, System.EventArgs e)
		{
			if(m_ListBoxType==EditListType.Edit )
			{
				if(this.m_ListBox.SelectedIndex>=0 )//&& ItemRemove!=null)
					BoxFocus();
			}
		}

		private void mnuRemove_Click(object sender, System.EventArgs e)
		{
			RemoveItem();
		}

		private void mnuserch_Click(object sender, System.EventArgs e)
		{
			//BoxFocus("Serch",Color.LightSteelBlue);
			if(m_ListBoxType==EditListType.Edit )
			{
                object o = MsgDlg.ShowDialog(this,"Find item", "Nistec.Find Item");
				if(!o.Equals (""))
				{
					int i=this.m_ListBox.FindString (o.ToString() );     
					if(i>=0)
						this.m_ListBox.SetSelected (i,true);
					else
                        MsgDlg.ShowMsg("Item not found");
				}
			}
		}

		private void mnuSort_Click(object sender, System.EventArgs e)
		{
			this.m_ListBox.Sorted=true;    
		}

		private void contextMenu1_Popup(object sender, System.EventArgs e)
		{
			if(m_ListBoxType==EditListType.Edit )
			{
				this.mnuAdd.Enabled =m_AllowAdd;   
				this.mnuEdit.Enabled =m_AllowEdit;   
				this.mnuRemove.Enabled =m_AllowRemove;   
			}
			else if(m_ListBoxType==EditListType.Insert  )
			{
				this.mnuserch.Visible =false;
				this.mnuAdd.Checked =m_AllowAdd;
				this.mnuEdit.Checked =m_AllowEdit;
				this.mnuRemove.Checked =m_AllowRemove;
				this.mnuAdd.Enabled =false;   
				this.mnuEdit.Visible =false;   
				this.mnuRemove.Enabled =false;   
			}
			else //if(m_ListBoxType==EditListType.Default )
			{
				this.mnuserch.Visible =false;
				this.mnuAdd.Visible =false;   
				this.mnuEdit.Visible  =false;   
				this.mnuRemove.Visible =false;   
			}

		}

		#endregion

		#region IStyleCtl

		protected override void OnStylePainterChanged(EventArgs e)
		{
			base.OnStylePainterChanged (e);
			m_ListBox.StylePainter=this.StylePainter;
			m_TextBox.StylePainter=this.StylePainter;
		}
  
		public override void SetStyleLayout(StyleLayout value)
		{
			//m_Style.SetStyleLayout(value);
			//m_ListBox.StyleCtl.SetStyleLayout(value);
			//m_TextBox.StyleCtl.SetStyleLayout(value);
			Invalidate();
			if(this.DesignMode)
				Refresh();
		}

		protected override void OnStylePropertyChanged(PropertyChangedEventArgs e)
		{
			base.OnStylePropertyChanged(e);
//			m_ListBox.StyleCtl.SetStyleLayout(m_Style.Layout);
//			m_TextBox.StyleCtl.SetStyleLayout(m_Style.Layout);
//			if(this.DesignMode)
//				Refresh();
		}

		#endregion

	}
}



