using System;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using mControl.Win32;
using mControl.WinCtl.Controls;
using mControl.GridStyle; 
using mControl.GridStyle.Columns;  
using mControl.Util;

namespace mControl.GridStyle.Controls
{
	public enum GridFilterType
	{
		None=0,
		Equal=1,
		Like=2,
	}


	[ToolboxBitmap (typeof(DataGrid))]
	[System.ComponentModel.ToolboxItem(false)]
	public class GridControl :mControl.WinCtl.Controls.CtlButtonBase 
	{
		#region Members
		internal CtlGrid dataGrid;

		private System.ComponentModel.Container components   = null;
		private GridPopUp m_GridPopUp;
      	private Grid mGrid;
		private bool mDroppedDown;
		private DataView m_DataView;
		private object m_DataSource;
		private string m_DataMember;
		private string m_RowFilter;
		private string m_FieldFilter;
		private string m_ValueFilter;
		private GridFilterType m_FilterType; 
		private string m_Operator;
		private int m_VisibleRows;
		private int m_VisibleWidth;
		private int m_MaxWidth;
		private bool isCreated;

		internal protected static Image collapsed;
		internal protected static Image expaned;


		//[Category("Behavior")]
		//public  event System.EventHandler ButtonClick = null;

		#endregion

		#region Constructors
		static GridControl()
		{
			GridControl.collapsed =NativeMethods.LoadImage("mControl.GridStyle.Images.collapsed.gif");
			GridControl.expaned  =NativeMethods.LoadImage ("mControl.GridStyle.Images.expaned.gif");

		}

		public GridControl()
		{
			base.NetReflectedFram("ba7fa38f0b671cbc");
			mDroppedDown=false;
			m_RowFilter=String.Empty;
			m_FieldFilter=String.Empty;
			m_ValueFilter=String.Empty ;
			m_DataMember=String.Empty;
			m_FilterType=GridFilterType.Equal; 
			m_Operator="=";
			m_VisibleRows=10;
			m_VisibleWidth=0;
			m_MaxWidth=Grid.MaxGridWidth;
			isCreated=false;

			InitializeComponent();
			this.BackColor =Color.AliceBlue; 

//			this.collapsed =NativeMethods.LoadImage("mControl.GridStyle.Images.collapsed.gif");
//			this.expaned  =NativeMethods.LoadImage ("mControl.GridStyle.Images.expaned.gif");

		}

		#endregion

		#region Dispose
		protected override void Dispose( bool disposing )
		{
			if(	mDroppedDown)
				this.m_GridPopUp.Close (); 
			
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
			this.mGrid = new GridStyle.Grid();
			//((System.ComponentModel.ISupportInitialize)(this.mGrid)).BeginInit();
			// 
			// mGrid
			// 
			this.mGrid.DataMember = "";
			this.mGrid.GridTableStyle.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.mGrid.BackgroundColor  = System.Drawing.Color.White ;
			this.mGrid.Location = new System.Drawing.Point(0, 0);
			this.mGrid.CaptionVisible =false;
			this.mGrid.GridTableStyle.RowHeadersVisible  =false;
			this.mGrid.GridTableStyle.ReadOnly  =true;
			this.mGrid.StatusBarMode=mControl.GridStyle.Controls.StatusBarMode.Hide;
			this.mGrid.Name = "mGrid";
			this.mGrid.TabIndex = 0;
			// 
			// GridControl
			// 
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Name = "GridControl";
			this.Size = new System.Drawing.Size(24, 19);
			//this.Text = "+";
			//this.Click +=new EventHandler(GridControl_ButtonClick);
			//((System.ComponentModel.ISupportInitialize)(this.mGrid)).EndInit();

		}
		#endregion
		
		#region Events handlers

		/*private void OnPopUpSelectionChanged(object sender,DateChangedEventArgs e)
		{
			this.Value = e.Date;
			IsChange=false;
		}*/
		
		
		private void mTextBox_ProccessMessage(object sender, MessageEventArgs e)
		{
			Message m = e.WindowsMessage;

			if(mDroppedDown && m_GridPopUp != null && IsNeeded(ref m))
			{
				//m_GridPopUp.PostMessage(ref m);
				e.Result = true;
			}

			e.Result = false;
		}

		#endregion

		#region Overrides

		public override IStyleLayout CtlStyleLayout
		{
			get{return dataGrid.GridLayout as IStyleLayout;} 
		}

		private void OnPopUpClosed(object sender,System.EventArgs e)
		{
			mDroppedDown = false;
			m_GridPopUp.Dispose();
			//this.Text ="+";
			Invalidate(false);
		}

		public void DoDropDown()
		{
			base.PerformClick ();
		}

		protected override void OnClick(EventArgs e)
		{
			base.OnClick (e);
			if(mDroppedDown )
			{
				m_GridPopUp.Close();
				return;
			}	
		
			ShowPopUp();			

		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);

			if (e.Shift  && e.KeyCode == Keys.Down)
				this.ShowPopUp();
			else if ((e.Shift && e.KeyCode == Keys.Up) || (e.KeyCode == Keys.Escape)) 
			{
				if(mDroppedDown && m_GridPopUp != null)
				{
					m_GridPopUp.Close();
					mDroppedDown = false;
				}
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint (e);
//			Rectangle rect=this.ClientRectangle;
//			using(Brush sb=new SolidBrush (this.StyleCtl.ForeColor))
//			{
//				e.Graphics.DrawString(this.Text,this.Font,sb,rect.X ,((rect.Height-this.FontHeight) /2)-1);
//			}
            DrawImage(e.Graphics);

		}

		internal void DrawImage(Graphics g)
		{

			Rectangle bounds=new Rectangle(0,0, this.Width-1, this.Height-1);

			Image image=null;
			if(mDroppedDown)
				image=expaned; 
			else
				image=collapsed; 
	
			if(image !=null) 
			{
				PointF iPoint=new PointF (0,0);
				iPoint.Y=((bounds.Height -image.Height)/2);
				iPoint.X =((bounds.Width -image.Width)/2);

				g.DrawImage (image, iPoint.X, iPoint.Y+1);
			}
		}

		protected override void OnStylePropertyChanged(PropertyChangedEventArgs e)
		{
			base.OnStylePropertyChanged (e);
			mGrid.SetStyleLayout(this.dataGrid.interanlGrid.CtlStyleLayout.StylePlan);
		}


		#endregion

		#region Setting

		public void Resetting()
		{
			mGrid.SetStyleLayout(this.dataGrid.interanlGrid.CtlStyleLayout.Layout);
			mGrid.BeginInit();
			mGrid.DataSource=this.DataSource;
			mGrid.DataMember=this.DataMember;
			mGrid.EndInit();
			m_DataView=DataList;
			mGrid.SetTableStyles(false);
			//mGrid.SetStyleLayoutInternal(true);
			this.m_VisibleWidth =mGrid.GetWidthInternal();
  			isCreated=true;
		}

		private bool CanShow()
		{
			if(mGrid==null)
				return false;
			if(this.DataSource ==null)
			{
				MsgBox.ShowWarning ("Invalid DataSource");
				return false;
			}
//			if(this.DataMember.Length==0)
//			{
//				MsgBox.ShowWarning ("Invalid DataMember");
//				return false;
//			}
			return true;
		}

		public void SetDataGrid(DataGrid parentGrid)
		{
			this.dataGrid = (CtlGrid)parentGrid;
		}
 
		#endregion

		#region Filter

		private string colType() 
		{
			if(m_FieldFilter!="")
			   return m_DataView.Table.Columns[m_FieldFilter].DataType.ToString();
			return "";
		}

		private bool cmdFilter( ) 
		{
			string strFilter="";

			if(this.m_ValueFilter.Length==0 || this.m_FieldFilter.Length==0)
			{
				strFilter = ""; 
			}
			else if (FilterType == GridFilterType.None) 
			{
				strFilter = "";
			}
			else if (colType().EndsWith("String")) 
			{
				if (FilterType == GridFilterType.Like ) 
				{
					strFilter = (this.m_FieldFilter  + (" like \'%" 
						+ (this.m_ValueFilter  + "%\'")));
				}
				else 
				{
					strFilter = (this.m_FieldFilter  + (" " 
						+ (this.m_Operator  + (" \'" 
						+ (this.m_ValueFilter + "\'")))));
				}
			}
			else if (colType().EndsWith("DateTime")) 
			{
				strFilter = (this.m_FieldFilter  + (" " 
					+ (this.m_Operator  + (" \'" 
					+ (this.m_ValueFilter + "\'")))));
			}
			else if (Regx.IsNumeric (this.m_ValueFilter )) 
			{
				strFilter = (this.m_FieldFilter  + (" " 
					+ (this.m_Operator  + (" " + this.m_ValueFilter ))));
			}
			else 
			{
				MessageBox.Show("UN KNOWN DATA TYPE","mControl",MessageBoxButtons.OK , MessageBoxIcon.Exclamation);
                return false; 
			}
			
			/*if ((txtFilter.Text.Trim().Length == 0)) 
			{
				MessageBox.Show ("NO RECORDS FOUND ","mControl",MessageBoxButtons.OK , MessageBoxIcon.Exclamation);
				return;
			}*/

			//this.m_DataSource.RowFilter = strFilter;
			mGrid.RemoveFilter();
			if(strFilter.Length>0)
			{
				mGrid.SetFilter(strFilter);
				//mGrid.DataSource = m_DataSource;
			}
			// //Display the number of rows in the view
			if ((m_DataView.Count == 0)) 
			{
				MessageBox.Show ("NO RECORDS FOUND","mControl",MessageBoxButtons.OK , MessageBoxIcon.Information );
                return false;  
			}
	
			//setGrid();
			return true;
		}
        
		private void cmdRemoveFilter() 
		{
			m_DataView.RowFilter = "";
			mGrid.DataSource = m_DataSource;
		}

		[Browsable(false)]
		public System.Data.DataView DataList 
		{
			get
			{
				if(DataSource!=null)
				{
					try
					{
						//return (System.Data.DataView)((System.Data.DataSet)CM.List).Tables[this.DataMember].DefaultView;
						if(DataSource is System.Data.DataSet)
						{
							if(this.DataMember.Length >0) 
								return (System.Data.DataView)((System.Data.DataSet)DataSource).Tables[this.DataMember].DefaultView;
							else
								return (System.Data.DataView)((System.Data.DataSet)DataSource).Tables[0].DefaultView;
						}
						if(DataSource is System.Data.DataView)
						{
							return (System.Data.DataView) this.DataSource;
							//return ((System.Data.DataView)(CM.List));
						}
						if(DataSource is System.Data.DataTable)
						{
							return (System.Data.DataView) ((System.Data.DataTable)this.DataSource).DefaultView;
							//return (System.Data.DataView)((System.Data.DataTable)CM.List).DefaultView;
						}
						throw new Exception ("Data Source not valid");	
					}
					catch(Exception ex)
					{
						throw  ex;
					}
				}
				else
					return null;
			}
		}

		#endregion

		#region Show

		[UseApiElements("ShowWindow")]
		private void ShowPopUp()
		{
			if(!CanShow())
	          return;
            if(!isCreated)
				Resetting();
			if(this.GridTableStyle ==null )
				throw new Exception("Error in GridTableStyle ");
			if (!cmdFilter())
				return;

			int width=4+this.m_VisibleWidth;

			if(this.GridTableStyle.RowHeadersVisible )
				width+=this.GridTableStyle.RowHeaderWidth;
	
 			int scrollBottom=0;
			if(MaxWidth<=this.m_VisibleWidth)
               scrollBottom+=Grid.DefaultScrollWidth;

			int statusHeight=0; 
			if(mGrid.IsStatusBarVisible)
			  statusHeight=mGrid.StatusBar.Height;
	
			int height=mGrid.GetHeightInternal(this.m_VisibleRows);
			height+=(statusHeight+scrollBottom);
	
			if(m_DataView.Count <= this.m_VisibleRows)
			{
				height+=4;//= 28 +(m_DataSource.Count * mGrid.GridTableStyle.PreferredRowHeight) ;
				width+=2;
			}
			else
			{
				height+= 2;//4 ;
				width+=Grid.DefaultScrollWidth;
			}
			mGrid.Size =new Size (width,height ); 

			Point pt = this.Parent.PointToScreen(new Point(this.Left,this.Bottom ));
			if (this.RightToLeft ==RightToLeft.Yes )
				pt = this.Parent.PointToScreen(new Point(this.Left-(mGrid.Size.Width-this.Width ),this.Bottom ));

			mGrid.RightToLeft =this.RightToLeft;

			m_GridPopUp = new GridPopUp(this,mGrid.Size);
			//m_GridPopUp.SelectionChanged += new DateSelectionChangedEventHandler(this.OnPopUpSelectionChanged);
			m_GridPopUp.Closed += new System.EventHandler(this.OnPopUpClosed);

			Rectangle screenRect = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
			if(screenRect.Bottom < pt.Y + m_GridPopUp.Height)
			{
				pt.Y = pt.Y - m_GridPopUp.Height - this.Height - 1;
			}

			if(screenRect.Right < pt.X + m_GridPopUp.Width)
			{
				pt.X = screenRect.Right - m_GridPopUp.Width - 2;
			}

			m_GridPopUp.Location = pt;
			
			Win32.WinAPI.ShowWindow(m_GridPopUp.Handle,4);
			//mGrid.SetStyleLayout(this.dataGrid.interanlGrid.CtlStyleLayout.StylePlan);
			m_GridPopUp.Start = true;
			mDroppedDown = true;
			//this.Text ="--";
			this.Invalidate();

		}

		[UseApiElements("WM_MOUSEWHEEL, WM_KEYUP, WM_KEYDOWN, WM_CHAR")]
		private bool IsNeeded(ref  System.Windows.Forms.Message m)
		{
			if(m.Msg == (int)Msgs.WM_MOUSEWHEEL)
			{
				return true;
			}

			if(m.Msg == (int)Msgs.WM_KEYUP || m.Msg == (int)Msgs.WM_KEYDOWN)
			{
				return true;
			}

			if(m.Msg == (int)Msgs.WM_CHAR)
			{
				return true;
			}

			return true;//false;
		}

		#endregion

		#region Properties
		
		public int VisibleRows
		{
			get{return m_VisibleRows;}
			set{m_VisibleRows=value;}
		}

		public GridFilterType FilterType
		{
			get{return m_FilterType;}
			set{m_FilterType=value;}
		}

		public Grid InternalGrid
		{
			get{return mGrid;}
		}

		public DataGridTableStyle GridTableStyle
		{
			get{return mGrid.GridTableStyle;}
		}

		//[TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), Description("DataGridDataSourceDescr"), DefaultValue((string) null), Category("Data"), RefreshProperties(RefreshProperties.Repaint)]
		public object DataSource
		{
			get{return m_DataSource;}
			set{m_DataSource=value;	}
		}

		public string DataMember
		{
			get{return m_DataMember;}
			set{m_DataMember=value;	}
		}

		public string RowFilter
		{
			get{return m_RowFilter;}
			set{m_RowFilter=value;}
		}

		public string FieldFilter
		{
			get{return m_FieldFilter;}
			set{m_FieldFilter=value;}
		}

		public string ValueFilter
		{
			get{return m_ValueFilter;}
			set{m_ValueFilter=value;}
		}

		public int MaxWidth
		{
			get{return m_MaxWidth;}
			set
			{
				if(m_MaxWidth!=value)
				{
					bool ok=mGrid.SetMaxWidth (value);
					if(ok)
						m_MaxWidth=value;
				}
			}
		}

		#endregion

	}


}