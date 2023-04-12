
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using mControl.WinCtl.Controls;
using mControl.GridStyle.Controls;

namespace mControl.GridStyle.Columns
{
	
	public class GridButtonColumn : GridColumnStyle 
	{

		#region Members
		// Fields
		private GridButton  edit;
		private string m_Caption;
		private string m_CaptionPush;
//		private Color m_BorderColor;
//		private Color m_BackColor;
//		private Color m_ColorBrush1;
//		private Color m_ColorBrush2;

		//private ButtonState m_ButtonStyle; 
	
		//public event ButtonClickEventHandler ButtonClicked;

		#endregion

		#region Constructors

		public GridButtonColumn() : this((PropertyDescriptor) null, (string) null)
		{
			InitColumn();
		}
		public GridButtonColumn(PropertyDescriptor prop) : this(prop, null, false)
		{
			InitColumn();
		}
		public GridButtonColumn(PropertyDescriptor prop, bool isDefault) : this(prop, null, isDefault)
		{
			InitColumn();
		}
		public GridButtonColumn(PropertyDescriptor prop, string format) : this(prop, format, false)
		{
			InitColumn();
			this.Format = format;
		}
		public GridButtonColumn(PropertyDescriptor prop, string format, bool isDefault) : base(prop, isDefault)
		{
			InitColumn();
			this.Format = format;
		}
		private void InitColumn() 
		{
			m_Caption="...";
			m_CaptionPush="...";
			//m_BorderColor=SystemColors.ControlDark  ;

			this.edit = new GridButton();
			this.edit.Visible = false;
			m_ColumnType = ColumnTypes.ButtonColumn ;
			hostedCtl =this.edit;
		}

		#endregion

		#region internal override

		protected override void Abort(int rowNum)
		{
			this.RollBack();
			this.HideEditBox();
			this.EndEdit();
		}

		protected override bool Commit(CurrencyManager dataSource, int rowNum)
		{
			this.edit.Bounds = Rectangle.Empty;
			this.DebugOut("OnCommit completed without Exception.");
			this.EndEdit();
			return true;
		}

		protected override void ConcedeFocus()
		{
			this.edit.Bounds = Rectangle.Empty;
		}

 
		protected override void Edit(CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly, string instantText, bool cellIsVisible)
		{
			this.DebugOut("Begining Edit, rowNum :" + rowNum.ToString());
			Rectangle rectangle1 = bounds;
			this.edit.ReadOnly = (readOnly || this.ReadOnly) || this.DataGridTableStyle.ReadOnly;
			this.edit.Text =this.CaptionPush;//  this.GetText(this.GetColumnValueAtRow(source, rowNum));
			string colValue = this.GetText(this.GetColumnValueAtRow(source, rowNum));
			bool isMouseInRect=false;
	
			if (!this.edit.ReadOnly && (instantText != null))
			{ 
				((CtlGrid)this.DataGridTableStyle.DataGrid).ColumnStartedEditing(bounds);
				this.edit.IsInEditOrNavigateMode = false;
				//this.edit.Text = instantText;
			}
			if (cellIsVisible)
			{
				bounds.Offset(this.xMargin, 2 * this.yMargin);
				bounds.Width -= this.xMargin;
				bounds.Height -= 2 * this.yMargin;
				Rectangle controlBounds =bounds;
				controlBounds.Inflate (-2,-2);
				//controlBounds.Offset (1,-1);

				Point p  = this.DataGridTableStyle.DataGrid.PointToClient(Cursor.Position);
				Rectangle cursorBounds = new Rectangle(p.X, p.Y, 1, 1);

				isMouseInRect= cursorBounds.IntersectsWith(controlBounds); 


				this.DebugOut("edit bounds: " + bounds.ToString());
				this.edit.Bounds = controlBounds;
				//this.edit.BackColor = this.DataGridTableStyle.BackColor; 
				this.edit.Visible = true;
				//this.edit.TextAlign = NativeMethods.GetAlignment (this.Alignment);
				this.DataGridTableStyle.DataGrid.Invalidate(controlBounds);
			}
			else
			{
				this.edit.Bounds = rectangle1;
				this.edit.Visible = false;
			}
			this.edit.RightToLeft = this.DataGridTableStyle.DataGrid.RightToLeft;
			this.edit.Focus();
			this.editRow = rowNum;
			if (!this.edit.ReadOnly)
			{
				this.oldValue = this.edit.Text;
			}
			if (instantText == null)
			{
				//this.edit.SelectAll();
			}
			else
			{
				//int num1 = this.edit.Text.Length;
				//this.edit.SelectInternal(num1, 0);
			}
			if (this.edit.Visible && isMouseInRect)
			{

				//Point p  = this.DataGridTableStyle.DataGrid.PointToClient(Cursor.Position);
				//Rectangle cursorBounds = new Rectangle(p.X, p.Y, 1, 1);

				//if( cursorBounds.IntersectsWith(rectangle1)) 
				//{
				
				
				this.Invalidate();
				Application.DoEvents();
				//this.DataGridTableStyle.DataGrid.Refresh ();
				this.DataGridTableStyle.DataGrid.Invalidate(rectangle1);
				this.edit.PerformClick (); 
				//if(grid.CellClicked!=null)
				   //grid.OnCellClicked(this,new CellClickEventArgs (this.DataGridTableStyle, rowNum, this.CurrentCol())); 
				grid.OnButtonClicked(this,this.DataGridTableStyle, rowNum, this.CurrentCol()); 
				//if(ButtonClicked!=null)
				//	ButtonClicked(this,new ButtonClickEventArgs (rowNum,this.MappingName ,colValue));//EventArgs.Empty); 
				HideEditBox();
			}
		}

 
		protected void EndEdit()
		{
			this.edit.IsInEditOrNavigateMode = true;
			this.DebugOut("Ending Edit");
			this.Invalidate();
		}

 
		protected  override void EnterNullValue()
		{
			if ((!this.ReadOnly && this.edit.Visible) && this.edit.IsInEditOrNavigateMode)
			{
				this.edit.Text = this.NullText;
				this.edit.IsInEditOrNavigateMode = false;
				if ((this.DataGridTableStyle != null) && (this.DataGridTableStyle.DataGrid != null))
				{
					((CtlGrid)this.DataGridTableStyle.DataGrid).ColumnStartedEditing(this.edit.Bounds);
				}
			}
		}
 
		protected void HideEditBox()
		{
			bool flag1 = this.edit.Focused;
			this.edit.Visible = false;
			if ((flag1 && (this.DataGridTableStyle != null)) && ((this.DataGridTableStyle.DataGrid != null) && this.DataGridTableStyle.DataGrid.CanFocus))
			{
				this.DataGridTableStyle.DataGrid.Focus();
			}
		}

		protected override void PaintText(Graphics g, Rectangle bounds, string text, Brush backBrush, Brush foreBrush, bool alignToRight)
		{
			Rectangle rectangle1 = bounds;
			StringFormat format1 = new StringFormat();
			if (alignToRight)
			{
				format1.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
			}
			format1.Alignment =StringAlignment.Center;// (this.Alignment == HorizontalAlignment.Left) ? StringAlignment.Near : ((this.Alignment == HorizontalAlignment.Center) ? StringAlignment.Center : StringAlignment.Far);
			format1.LineAlignment =StringAlignment.Center ;
			format1.FormatFlags |= StringFormatFlags.NoWrap;
			g.FillRectangle(backBrush, rectangle1);
			rectangle1.Offset(0, 2 * this.yMargin);
			rectangle1.Height -= 2 * this.yMargin;
           
			Rectangle controlBounds =bounds;
            controlBounds.Inflate (-2,-2);
			//ButtonState bs = m_ButtonStyle;
			//ControlPaint.DrawButton(g, controlBounds, bs);
			//ControlPaint.DrawBorder(g, controlBounds, BorderColor, ButtonBorderStyle.Solid);
		    
//			using (System.Drawing.Drawing2D.LinearGradientBrush sb= new System.Drawing.Drawing2D.LinearGradientBrush 
//					   (controlBounds, m_ColorBrush1,m_ColorBrush2, 
//					   System.Drawing.Drawing2D.LinearGradientMode.Vertical))
//			{
//				g.FillRectangle(sb,controlBounds );
//			}

			using (Brush sb= grid.CtlStyleLayout.GetBrushGradient(controlBounds,90f,true))
			{
				g.FillRectangle(sb,controlBounds );
			}
			using(Pen p=grid.CtlStyleLayout.GetPenBorder())// new Pen (m_BorderColor))
			{
				g.DrawRectangle (p,controlBounds );
			}


//			using(SolidBrush sb1= new SolidBrush (m_BackColor), sb2= new SolidBrush (m_BorderColor))
//			{
//				g.FillRectangle(sb1,controlBounds );
//				using(Pen p= new Pen (sb2))
//				{
//					g.DrawRectangle (p,controlBounds );
//				}
//			}

			g.DrawString(m_Caption, this.DataGridTableStyle.DataGrid.Font, foreBrush, (RectangleF) controlBounds , format1);
			format1.Dispose();

			//if(edit.ButtonState == mControl.WinCtl.Controls.States.Normal  )
			//	edit.Visible =false;
		}

 
		protected  override void ReleaseHostedControl()
		{
			if (this.edit.Parent != null)
			{
				this.edit.Parent.Controls.Remove(this.edit);
			}
		}

		private void RollBack()
		{
			this.edit.Text = this.oldValue;
		}

 
		internal protected override void SetDataGridInColumnInternal(DataGrid value,bool forces)
		{
            if(this.edit.dataGrid==value && !forces)
				return;

			this.edit.SetDataGrid(value);
			if(this.grid==null)
				this.grid=((CtlGrid)value).interanlGrid as Grid;

//			Styles style =((CtlGrid)value).interanlGrid.CtlStyleLayout.StylePlan;   
//			if(style==Styles.Custom)   
//				this.edit.StyleCtl.SetStyleLayout(((CtlGrid)value).interanlGrid.CtlStyleLayout.Layout);   
//			else			
//				this.edit.StyleCtl.StylePlan =style;   
	
//			m_BorderColor=grid.GridLayout.BorderColor;// ((CtlGrid)value).GridLayout.BorderColor ;
//			m_BackColor =grid.GridLayout.FlatColor;//((CtlGrid)value).GridLayout.FlatColor ;
//			m_ColorBrush1 =grid.GridLayout.ColorBrush1;//((CtlGrid)value).GridLayout.ColorBrush1 ;
//			m_ColorBrush2 =grid.GridLayout.ColorBrush2;//((CtlGrid)value).GridLayout.ColorBrush2;
		}

		protected override void SetDataGridInColumn(DataGrid value)
		{
			base.SetDataGridInColumn(value);
			if (this.edit.Parent != null)
			{
				this.edit.Parent.Controls.Remove(this.edit);
			}
			if (value != null)
			{
				value.Controls.Add(this.edit);
			}
			SetDataGridInColumnInternal(value,false);
		}

		protected  override void UpdateUI(CurrencyManager source, int rowNum, string instantText)
		{
			this.edit.Text = this.Caption; 
//			this.edit.Text = this.GetText(this.GetColumnValueAtRow(source, rowNum));
//			if (!this.edit.ReadOnly && (instantText != null))
//			{
//				this.edit.Text = instantText;
//			}
		}

		#endregion
 
		#region Virtual

		internal  override bool KeyPress(int rowNum, Keys keyData)
		{
			if (this.edit.IsInEditOrNavigateMode)
			{
				return base.KeyPress(rowNum, keyData);
			}
			return false;
		}

		#endregion

		#region Properties

		[Browsable(false)]
		public virtual GridButton   Button
		{
			get
			{
				return this.edit;
			}
		}

  
//		[DefaultValue("")]
//		public override string NullText 
//		{
//			get{return base.NullText;}
//			set{base.NullText = GetFormatText(value);}
//		}
    
//		[DefaultValue(ButtonState.Flat )]
//		public ButtonState ButtonStyle 
//		{
//			get{return m_ButtonStyle;}
//			set{m_ButtonStyle = value;}
//		}
        
//		[DefaultValue(typeof(Color),"ControlDark")]
//		public Color BorderColor 
//		{
//			get{return m_BorderColor;}
//			set{m_BorderColor = value;}
//		}
//
//		[DefaultValue(typeof(Color),"Blue")]
//		public Color FocusColor 
//		{
//			get{return Color.Blue;}//  m_FocusColor;}
//			//set{m_FocusColor = value;}
//		}

		[DefaultValue("...")]
		public string Caption 
		{
			get{return m_Caption;}
			set{m_Caption = value;}
		}
        
		[DefaultValue("...")]
		public string CaptionPush 
		{
			get{return m_CaptionPush;}
			set{m_CaptionPush = value;}
		}

		#endregion
	
//		[Category("Style")]
//		public  mControl.WinCtl.Controls.Styles Style
//		{
//			get {return edit.StyleCtl.StylePlan;}
//			//set{edit.StyleCtl.StylePlan =value;} 
//		}
    

//		protected override void Paint(System.Drawing.Graphics g, System.Drawing.Rectangle bounds, CurrencyManager source, int rowNum, System.Drawing.Brush backBrush, System.Drawing.Brush foreBrush, bool alignToRight) 
//		{
//			try 
//			{
//				g.FillRectangle(backBrush, bounds);
//				Rectangle controlBounds =GetCellBounds(bounds);
//				Rectangle focusBounds = controlBounds;
//				focusBounds.Inflate(-2, -2);
//				System.Drawing.RectangleF BoundsF = new System.Drawing.RectangleF(((float)(bounds.X)), ((float)(bounds.Y)), ((float)(bounds.Width)), ((float)(bounds.Height)));
//				BoundsF.Inflate(-3, -3);
//				ButtonState bs = m_ButtonStyle;
//				ControlPaint.DrawButton(g, controlBounds, bs);
//				ControlPaint.DrawBorder(g, controlBounds, BorderColor, ButtonBorderStyle.Solid);
//				g.DrawString(m_Caption, this.DataGridTableStyle.DataGrid.Font, foreBrush, BoundsF, stringFormat);
//
//			}
//			catch  
//			{
//				throw new Exception("Error In Button column");
//			}
//		}

		#region Progress
//		internal void ProgressPaint(System.Drawing.Graphics g, System.Drawing.Rectangle bounds, System.Drawing.RectangleF boundsF) 
//		{
//			GridStyle.ClickEvent ButtonEvnt = null;//CtlGrid.ClickColumnEvent;
//			
//			if (ButtonEvnt != null) 
//			{
//				if (ButtonEvnt.ProgressValue > 0) 
//				{
//					int val = ButtonEvnt.ProgressValue;
//					Rectangle fillRect = new Rectangle((bounds.X + 2), (bounds.Y + 2), (bounds.Width - 3), (bounds.Height - 3));
//					int maxWidth = fillRect.Width;
//					double indexWidth = ((double)((fillRect.Width) / 100));
//					//  determines the width of each index.
//					fillRect.Width = ((int)(val * indexWidth));
//					if ((fillRect.Width > maxWidth)) 
//					{
//						fillRect.Width = maxWidth;
//					}
//					Pen p = new Pen(new SolidBrush(BorderColor));
//					try 
//					{
//						g.DrawRectangle(p, bounds);
//					}
//					finally 
//					{
//						p.Dispose();
//					}
//					// Dim sb As New System.Drawing.Drawing2D.LinearGradientBrush _
//					//                 (fillRect, m_ColorBrush1, m_ColorBrush2, _
//					//                  System.Drawing.Drawing2D.LinearGradientMode.Vertical)
//					SolidBrush sb = new SolidBrush(FocusColor);
//					//  m_ColorBrush1)
//					try 
//					{
//						g.FillRectangle(sb, fillRect);
//					}
//					finally 
//					{
//						sb.Dispose();
//					}
//					//g.DrawString((val.ToString() + "%"), this.DataGridTableStyle.DataGrid.Font, new SolidBrush(CtlGrid.m_ProgsTextColor), boundsF, stringFormat);
//					g.DrawString((val.ToString() + "%"), this.DataGridTableStyle.DataGrid.Font, new SolidBrush(Color.Yellow ), boundsF, stringFormat);
//				}
//			}
//			else 
//			{
//				g.DrawString(m_CaptionPush, this.DataGridTableStyle.DataGrid.Font, new SolidBrush(m_BorderColor), boundsF, stringFormat);
//			}
//		}
		#endregion
	}
}



