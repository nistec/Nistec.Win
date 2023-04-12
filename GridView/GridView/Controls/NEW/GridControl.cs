using System;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using mControl.Win32;
using mControl.WinCtl.Controls;
using mControl.GridStyle; 
using mControl.Util;

namespace mControl.GridStyle
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
		internal Grid dataGrid;
		private const int buttonWidth=11;
		private System.ComponentModel.Container components   = null;
		private Grid m_Grid;
		private GridPopUp m_GridPopUp;
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

		#region Members
		// Events
		[Category("Behavior")]
		public event EventHandler DropDown;
		[Category("Behavior")]
		public event EventHandler DropUp;


		internal Size popUpSize;
	
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
			//base.FixSize=true;

//			this.collapsed =NativeMethods.LoadImage("mControl.GridStyle.Images.collapsed.gif");
//			this.expaned  =NativeMethods.LoadImage ("mControl.GridStyle.Images.expaned.gif");

			popUpSize=new Size(200, 200);
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
			this.m_Grid = new GridStyle.Grid();
			//((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
			// 
			// m_Grid
			// 
			this.m_Grid.DataMember = "";
			this.m_Grid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.m_Grid.BackgroundColor  = System.Drawing.Color.White ;
			this.m_Grid.Location = new System.Drawing.Point(0, 0);
			this.m_Grid.CaptionVisible =false;
			this.m_Grid.RowHeadersVisible  =false;
			this.m_Grid.ReadOnly  =true;
			//this.dataGrid.StatusBarMode=mControl.GridStyle.Controls.StatusBarMode.Hide;
			this.m_Grid.Name = "Grid";
			this.m_Grid.TabIndex = 0;
			// 
			// GridControl
			// 
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Name = "GridControl";
			this.Size = new System.Drawing.Size(24, 19);
			//this.Text = "+";
			//this.Click +=new EventHandler(GridControl_ButtonClick);
			//((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();

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
			get{return dataGrid.CtlStyleLayout as IStyleLayout;} 
		}

		private void OnPopUpClosed(object sender,System.EventArgs e)
		{
			mDroppedDown = false;
			m_GridPopUp.Dispose();
			//this.Text ="+";
			Invalidate(false);
		}

		public void DoDropDownX()
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

			Rectangle bounds=new Rectangle(0,-1,buttonWidth,buttonWidth);//, this.Width-1, this.Height-1);

			Image image=null;
			if(mDroppedDown)
				image=expaned; 
			else
				image=collapsed; 
	
			if(image !=null) 
			{
				//bounds.Offset(-1,-1);
				//PointF iPoint=new PointF (0,0);
				//iPoint.Y=2;//((bounds.Height -image.Height)/2);
				//iPoint.X =2;//((bounds.Width -image.Width)/2);

				g.DrawImage (image,bounds);// bounds.X, bounds.Y);
				//g.DrawImage(image,(RectangleF) bounds);  

			}
		}

		protected override void OnStylePropertyChanged(PropertyChangedEventArgs e)
		{
			base.OnStylePropertyChanged (e);
			m_Grid.SetStyleLayout(this.dataGrid.CtlStyleLayout.StylePlan);
		}


		#endregion

		#region Setting

		public void Resetting()
		{
			m_Grid.SetStyleLayout(this.dataGrid.CtlStyleLayout.Layout);
			m_Grid.BeginInit();
			m_Grid.DataSource=this.DataSource;
			m_Grid.DataMember=this.DataMember;
			//m_Grid.MappingName="tbl2";
			m_Grid.EndInit();
			m_DataView=DataList;
			//dataGrid.SetTableStyles(false);
			//dataGrid.SetStyleLayoutInternal(true);
			this.m_VisibleWidth =CalcGridWidth();
  			isCreated=true;
		}

		public int CalcGridWidth()
		{
			int width=0;
			int rowHeader=m_Grid.RowHeadersVisible ? m_Grid.RowHeaderWidth :0;

			if(DataSource!=null)
			{
				int i=m_DataView.Table.Columns.Count;
				width=i*m_Grid.PreferredColumnWidth;
			}
			else
			{
				width=this.Width;	
			}
            
			width += rowHeader; 
			if(width> Grid.MaxGridWidth)
				width= Grid.MaxGridWidth; 
			return width;
		}
		
		private int CalcGridHeight()
		{

			
			int colHeader=m_Grid.ColumnHeadersVisible ? Grid.DefaultColumnHeaderHeight :0;
			int cnt =m_DataView.Count <= this.m_VisibleRows?m_DataView.Count:this.m_VisibleRows;	
			int height=colHeader + (cnt * m_Grid.PreferredRowHeight);

			int scrollBottom=0;
			if(MaxWidth<=this.m_VisibleWidth)
				scrollBottom+=Grid.DefaultScrollWidth;

			int statusHeight=0; 
			//			if(dataGrid.IsStatusBarVisible)
			//			  statusHeight=dataGrid.StatusBar.Height;
	
			//int height=m_Grid.Height;
			height+=colHeader+(statusHeight+scrollBottom);
	
//			if(m_DataView.Count <= this.m_VisibleRows)
//			{
//				height+=4;//= 28 +(m_DataSource.Count * dataGrid.GridTableStyle.PreferredRowHeight) ;
//				//width+=2;
//			}
//			else
//			{
//				height+= 2;//4 ;
//				//width+=Grid.DefaultScrollWidth;
//			}
			return height;
		}

		private bool CanShow()
		{
			if(m_Grid==null)
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

		public void SetDataGrid(Grid parentGrid)
		{
			this.dataGrid = (Grid)parentGrid;
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
			m_Grid.RemoveFilter();
			if(strFilter.Length>0)
			{
				m_Grid.SetFilter(strFilter);
				//dataGrid.DataSource = m_DataSource;
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
			m_Grid.DataSource = m_DataSource;
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
		private void ShowPopUpX()
		{
			if(!CanShow())
	          return;
            if(!isCreated)
				Resetting();
			if(this.m_Grid ==null )
				throw new Exception("Error in GridTableStyle ");
			if (!cmdFilter())
				return;

			int width=4+this.m_VisibleWidth;
			int height=this.CalcGridHeight();

//			if(this.m_Grid.RowHeadersVisible )
//				width+=this.m_Grid.RowHeaderWidth;
//	
// 			int scrollBottom=0;
//			if(MaxWidth<=this.m_VisibleWidth)
//               scrollBottom+=Grid.DefaultScrollWidth;
//
//			int statusHeight=0; 
////			if(dataGrid.IsStatusBarVisible)
////			  statusHeight=dataGrid.StatusBar.Height;
//	
//			int height=m_Grid.Height;
//			height+=(statusHeight+scrollBottom);
//	
//			if(m_DataView.Count <= this.m_VisibleRows)
//			{
//				height+=4;//= 28 +(m_DataSource.Count * dataGrid.GridTableStyle.PreferredRowHeight) ;
//				width+=2;
//			}
//			else
//			{
//				height+= 2;//4 ;
//				width+=Grid.DefaultScrollWidth;
//			}


			m_Grid.Size =new Size (width,height ); 

			Point pt = this.Parent.PointToScreen(new Point(this.Left,this.Bottom ));
			if (this.RightToLeft ==RightToLeft.Yes )
				pt = this.Parent.PointToScreen(new Point(this.Left-(m_Grid.Size.Width-this.Width ),this.Bottom ));

			m_Grid.RightToLeft =this.RightToLeft;

			m_GridPopUp = new GridPopUp(this,m_Grid.Size);
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
			//dataGrid.SetStyleLayout(this.dataGrid.interanlGrid.CtlStyleLayout.StylePlan);
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
			get{return m_Grid;}
		}

//		public GridTableStyle GridTableStyle
//		{
//			get{return dataGrid.GridTableStyle;}
//		}

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
					bool ok=m_Grid.SetMaxWidth (value);
					if(ok)
						m_MaxWidth=value;
				}
			}
		}


		#endregion

		#region comb PopUp

		private bool m_DroppedDown;

		public void DoDropDown()
		{
			if(m_DroppedDown)
			{
				m_GridPopUp.Close();
				m_DroppedDown=false;
				return;
			}	
			ShowPopUp();
		}

		public void CloseDropDown()
		{
			if(m_DroppedDown)
			{
				m_GridPopUp.Close();
			}
		}

		public void ShowPopUp()
		{
			if(!CanShow())
				return;
			if(!isCreated)
				Resetting();
			if(this.m_Grid ==null )
				throw new Exception("Error in GridTableStyle ");
			if (!cmdFilter())
				return;

			int width=4+this.m_VisibleWidth;
			int height=this.CalcGridHeight();

			m_Grid.Size =new Size (width,height ); 

			Point pt = this.Parent.PointToScreen(new Point(this.Left,this.Bottom ));
			if (this.RightToLeft ==RightToLeft.Yes )
				pt = this.Parent.PointToScreen(new Point(this.Left-(m_Grid.Size.Width-this.Width ),this.Bottom ));

			m_Grid.RightToLeft =this.RightToLeft;

//			m_GridPopUp = new GridPopUp(this,m_Grid.Size);
//			m_GridPopUp.Closed += new System.EventHandler(this.OnPopUpClosed);


			if (this.m_GridPopUp == null)
			{
				this.InvokeDropDown(EventArgs.Empty);
				this.m_GridPopUp = new GridPopUp(this,m_Grid.Size);
				Form form1 = base.FindForm();
				if (form1 != null)
				{
					form1.AddOwnedForm(this.m_GridPopUp);
				}
				Rectangle rectangle1 = base.RectangleToScreen(base.ClientRectangle);
				this.m_GridPopUp.Handle.ToString();
//				this.m_GridPopUp.Location = new Point(rectangle1.Left, (rectangle1.Top + base.Height) + 1);
//				//int num1 = 210;
//				//int num2 =232;// 270;
//				this.m_GridPopUp.Size =this.popUpSize;// new Size(num1, num2);
//				if (this.m_GridPopUp.Bottom > Screen.PrimaryScreen.WorkingArea.Bottom)
//				{
//					this.m_GridPopUp.Top = (rectangle1.Top - 1) - this.m_GridPopUp.Height;
//				}
//				if (Screen.PrimaryScreen.Bounds.Right < this.m_GridPopUp.Right)
//				{
//					this.m_GridPopUp.Left = rectangle1.Right - this.m_GridPopUp.Width;
//				}
				

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

				this.m_GridPopUp.LockClose = true;
				this.m_GridPopUp.Closed += new EventHandler(this.OnClosePopup);
				this.m_GridPopUp.ShowPopupForm();
				m_DroppedDown=true;
			}
			else
			{
				this.m_GridPopUp.ClosePopupForm();
				this.m_GridPopUp = null;
				m_DroppedDown=false;
			}
		}

		#endregion

		#region DropDown methods

		public void InvokeDropDown(EventArgs e)
		{
			this.OnDropDown(e);
		}

		public void InvokeDropUp(EventArgs e)
		{
			this.OnDropUp(e);
		}

		//		public void InvokeSelectedIndexChanged(EventArgs e)
		//		{
		//			this.OnSelectedIndexChanged(e);
		//		}

		private void OnClosePopup(object sender, EventArgs e)
		{
			if (this.m_GridPopUp != null)
			{
				this.InvokeDropUp(e);
				//string memoText=this.m_GridPopUp.GetMemoText();
				//bool printMemo=this.m_GridPopUp.PrintMemo();
				if( this.m_GridPopUp.GetResult()==DialogResult.OK)
				{
					//this.Text=memoText;
					this.popUpSize=this.m_GridPopUp.Size;
	
				}
				this.m_GridPopUp.Closed -= new EventHandler(this.OnClosePopup);

				this.m_GridPopUp.Dispose();
				this.m_GridPopUp = null;
				m_DroppedDown=false;
//				if(printMemo)
//				{
//					this.Print(memoText);
//				}
			}
		}

		protected virtual void OnDropDown(EventArgs e)
		{
			if (this.DropDown != null)
			{
				this.DropDown(this, e);
			}
		}

		protected virtual void OnDropUp(EventArgs e)
		{
			if (this.DropUp != null)
			{
				this.DropUp(this, e);
			}
		}

		#endregion

		#region override

		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged(e);
			//base.TextBox.Visible = false;
			base.Invalidate();
		}

 
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if ((e.Button & MouseButtons.Left) > MouseButtons.None)
			{
				ShowPopUp();
			}
			else
			{
				if (this.m_GridPopUp != null)
				{
					this.m_GridPopUp.ClosePopupForm();
				}
				this.m_GridPopUp = null;
			}
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if (this.m_GridPopUp != null)
			{
				this.m_GridPopUp.LockClose = false;
			}
		}


		#endregion


	}


}