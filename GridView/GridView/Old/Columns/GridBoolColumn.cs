using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

//using mControl.GridStyle.Columns;

namespace mControl.GridStyle.Columns
{
	/// <summary>
	/// Summary description for GridBoolColumn.
	/// </summary>
	public class GridBoolColumn: GridColumnStyle
	{


		#region Members
		// Fields
		private bool allowNull;
		private object currentValue;
		private int editingRow;
		//private static readonly object EventAllowNull;
		//private static readonly object EventFalseValue;
		//private static readonly object EventTrueValue;
		private object falseValue;
		private static readonly int idealCheckSize;
		private bool isEditing;
		private bool isSelected;
		private object nullValue;
		private object trueValue;

		private Color m_BorderColor;
		private Color m_CheckColor;

		// Events
		public event EventHandler AllowNullChanged;
		public event EventHandler FalseValueChanged;
		public event EventHandler TrueValueChanged;
		public event EventHandler CheckedChanged;

		#endregion

		#region Constructor

		static GridBoolColumn()
		{
			GridBoolColumn.idealCheckSize = 12;
			//GridBoolColumn.EventTrueValue = new object();
			//GridBoolColumn.EventFalseValue = new object();
			//GridBoolColumn.EventAllowNull = new object();
		}

		public GridBoolColumn()
		{
			InitColumn();
		}

		public GridBoolColumn(PropertyDescriptor prop) : base(prop)
		{
			InitColumn();
		}

		public GridBoolColumn(PropertyDescriptor prop, bool isDefault) : base(prop, isDefault)
		{
			InitColumn();
		}

		private void InitColumn()
		{
			m_BorderColor=Color.Blue ;
			m_CheckColor=Color.Blue;

			this.isEditing = false;
			this.isSelected = false;
			this.allowNull = false;
			this.editingRow = -1;
			this.currentValue = Convert.DBNull;
			this.trueValue = true;
			this.falseValue = false;
			this.nullValue = Convert.DBNull;
			m_ColumnType = ColumnTypes.BoolColumn ;
		}

		#endregion

		#region internal override

		protected override bool Commit(CurrencyManager dataSource, int rowNum)
		{
			this.isSelected = false;
			if (this.isEditing)
			{
				this.SetColumnValueAtRow(dataSource, rowNum, this.currentValue);
				this.isEditing = false;
				this.Invalidate();
			}
			return true;
		}

		protected  override void Abort(int rowNum)
		{
			this.isSelected = false;
			this.isEditing = false;
			this.Invalidate();
		}

		protected  override void ConcedeFocus()
		{
			base.ConcedeFocus();
			this.isSelected = false;
			this.isEditing = false;
		}

 
		protected override void Edit(CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly, string instantText, bool cellIsVisible)
		{
			this.isSelected = true;
			DataGrid grid1 = this.DataGridTableStyle.DataGrid;
			if (!grid1.Focused)
			{
				grid1.Focus();
			}
			if (!readOnly && !this.IsReadOnly())
			{
				this.editingRow = rowNum;
				this.currentValue = this.GetColumnValueAtRow(source, rowNum);
			}
			base.Invalidate();
		}

		protected override void EnterNullValue()
		{
			if ((this.AllowNullValue && !this.IsReadOnly()) && (this.currentValue != Convert.DBNull))
			{
				this.currentValue = Convert.DBNull;
				this.Invalidate();
			}
		}

 
		private Rectangle GetCheckBoxBounds(Rectangle bounds, bool alignToRight)
		{
			if (alignToRight)
			{
				return new Rectangle(bounds.X + ((bounds.Width - GridBoolColumn.idealCheckSize) / 2), bounds.Y + ((bounds.Height - GridBoolColumn.idealCheckSize) / 2), (bounds.Width < GridBoolColumn.idealCheckSize) ? bounds.Width : GridBoolColumn.idealCheckSize, GridBoolColumn.idealCheckSize);
			}
			return new Rectangle(Math.Max(0, bounds.X + ((bounds.Width - GridBoolColumn.idealCheckSize) / 2)), Math.Max(0, bounds.Y + ((bounds.Height - GridBoolColumn.idealCheckSize) / 2)), (bounds.Width < GridBoolColumn.idealCheckSize) ? bounds.Width : GridBoolColumn.idealCheckSize, GridBoolColumn.idealCheckSize);
		}

		protected  override object GetColumnValueAtRow(CurrencyManager lm, int row)
		{
			object obj1 = base.GetColumnValueAtRow(lm, row);
			object obj2 = Convert.DBNull;
			if (obj1.Equals(this.trueValue))
			{
				return true;
			}
			if (obj1.Equals(this.falseValue))
			{
				obj2 = false;
			}
			return obj2;
		}

		protected  override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum)
		{
			this.Paint(g, bounds, source, rowNum, false);
		}

 
		protected  override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, bool alignToRight)
		{
			this.Paint(g, bounds, source, rowNum, (SolidBrush)new SolidBrush (this.DataGridTableStyle.BackColor), (SolidBrush)new SolidBrush (this.DataGridTableStyle.ForeColor), alignToRight);
		}

		protected  override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, Brush backBrush, Brush foreBrush, bool alignToRight)
		{
			object obj1 = (this.isEditing && (this.editingRow == rowNum)) ? this.currentValue : this.GetColumnValueAtRow(source, rowNum);
			ButtonState state1 = ButtonState.Flat;// .Inactive;
			if (!Convert.IsDBNull(obj1))
			{
				state1 = ((bool) obj1) ? ButtonState.Checked : ButtonState.Normal;
			}
			Rectangle rectangle1 = this.GetCheckBoxBounds(bounds, alignToRight);
			Region region1 = g.Clip;
			g.ExcludeClip(rectangle1);
			//Brush brush1 = this.DataGridTableStyle.IsDefault ? this.DataGridTableStyle.DataGrid.SelectionBackBrush : this.DataGridTableStyle.SelectionBackBrush;
		
			Brush brush1=(SolidBrush) new SolidBrush (this.DataGridTableStyle.DataGrid.SelectionBackColor);
			
			if ((this.isSelected && (this.editingRow == rowNum)) && !this.IsReadOnly())
			{
				g.FillRectangle(brush1, bounds);
			}
			else
			{
				g.FillRectangle(backBrush, bounds);
			}
			g.Clip = region1;

			Rectangle drawRect = new Rectangle(rectangle1.Left + 3,rectangle1.Top + 3, 6, 6);

			 //new SolidBrush (m_CheckColor)
			using (Brush sb1=grid.CtlStyleLayout.GetBrushHot(),sb2=new SolidBrush (Color.White),sb3=new SolidBrush (Color.LightGray ))
			{
				g.FillRectangle (sb2,rectangle1);
				if (state1 == ButtonState.Flat)//Inactive)
				{

					g.FillRectangle (sb3,drawRect);
					//ControlPaint.DrawMixedCheckBox(g, rectangle1, ButtonState.Checked);
				}
				else if (state1 == ButtonState.Checked )//Inactive)
				{
					g.FillRectangle (sb1,drawRect);
					//ControlPaint.DrawMixedCheckBox(g, rectangle1, ButtonState.Checked);
				}
				else 
				{
					g.FillRectangle (sb2,drawRect);
					//ControlPaint.DrawCheckBox(g, rectangle1, state1);
				}
			}

			using(Pen pen=grid.CtlStyleLayout.GetPenBorder())
			{
                rectangle1.Width-=1;
				rectangle1.Height-=1;
				g.DrawRectangle(pen,rectangle1);
			}
			//ControlPaint.DrawBorder(g, rectangle1,grid.BorderColor, ButtonBorderStyle.Solid );
		
			//if(bounds==CtlGrid.m_FocusBounds )  
			//	ControlPaint.DrawBorder(g, bounds, m_FocusColor , ButtonBorderStyle.Solid );
			//else
			//	ControlPaint.DrawBorder(g, bounds, Color.Transparent  , ButtonBorderStyle.None  );
	
			
			if ((this.IsReadOnly() && this.isSelected) && (source.Position == rowNum))
			{
				bounds.Inflate(-1, -1);
				Pen pen1 = new Pen(brush1);
				pen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;//Dash;
				g.DrawRectangle(pen1, bounds);
				pen1.Dispose();
				bounds.Inflate(1, 1);
			}
		}

 
		protected  override void SetColumnValueAtRow(CurrencyManager lm, int row, object value)
		{
			object obj1 = null;
			bool flag1 = true;
			if (flag1.Equals(value))
			{
				obj1 = this.TrueValue;
			}
			else
			{
				flag1 = false;
				if (flag1.Equals(value))
				{
					obj1 = this.FalseValue;
				}
				else if (Convert.IsDBNull(value))
				{
					obj1 = this.NullValue;
				}
			}
			this.currentValue = obj1;
			base.SetColumnValueAtRow(lm, row, obj1);
		}

		internal protected override void SetDataGridInColumnInternal(DataGrid value,bool forces)
		{
			//m_BorderColor =((CtlGrid)value).GridLayout.BorderColor ;
			//m_CheckColor=((CtlGrid)value).GridLayout.BorderHotColor ;
			if(this.grid==null)
				this.grid=((CtlGrid)value).interanlGrid as Grid;
		}


		protected override void SetDataGridInColumn(DataGrid value)
		{
			base.SetDataGridInColumn(value);
			SetDataGridInColumnInternal(value,false);
		}


		protected override int GetMinimumHeight()
		{
			return (GridBoolColumn.idealCheckSize + 2);
		}
 
		protected override int GetPreferredHeight(Graphics g, object value)
		{
			return (GridBoolColumn.idealCheckSize + 2);
		}

		protected  override Size GetPreferredSize(Graphics g, object value)
		{
			return new Size(GridBoolColumn.idealCheckSize + 2, GridBoolColumn.idealCheckSize + 2);
		}
		#endregion

		#region internal events

		internal override bool KeyPress(int rowNum, Keys keyData)
		{
			if ((this.isSelected && (this.editingRow == rowNum)) && (!this.IsReadOnly() && ((keyData & Keys.KeyCode) == Keys.Space)))
			{
				this.ToggleValue();
				this.Invalidate();
				return true;
			}
			return base.KeyPress(rowNum, keyData);
		}

		internal override bool MouseDown(int rowNum, int x, int y)
		{
			base.MouseDown(rowNum, x, y);
			if ((this.isSelected && (this.editingRow == rowNum)) && !this.IsReadOnly())
			{
				this.ToggleValue();
				this.Invalidate();
				return true;
			}
			return false;
		}

		private void OnAllowNullChanged(EventArgs e)
		{
			if(AllowNullChanged !=null)
				AllowNullChanged(this,e);
			
//			EventHandler handler1 = base.Events[GridBoolColumn.EventAllowNull] as EventHandler;
//			if (handler1 != null)
//			{
//				handler1(this, e);
//			}
		}

		private void OnFalseValueChanged(EventArgs e)
		{
			if(FalseValueChanged  !=null)
				FalseValueChanged(this,e);

//			EventHandler handler1 = base.Events[GridBoolColumn.EventFalseValue] as EventHandler;
//			if (handler1 != null)
//			{
//				handler1(this, e);
//			}
		}

 
		private void OnTrueValueChanged(EventArgs e)
		{
			if(TrueValueChanged  !=null)
				TrueValueChanged(this,e);

//			EventHandler handler1 = base.Events[GridBoolColumn.EventTrueValue] as EventHandler;
//			if (handler1 != null)
//			{
//				handler1(this, e);
//			}
		}

		#endregion

		#region Properties
 
		private bool IsReadOnly()
		{
			bool flag1 = this.ReadOnly;
			if (this.DataGridTableStyle != null)
			{
				flag1 = flag1 || this.DataGridTableStyle.ReadOnly;
				if (this.DataGridTableStyle.DataGrid != null)
				{
					flag1 = flag1 || this.DataGridTableStyle.DataGrid.ReadOnly;
				}
			}
			return flag1;
		}

		private void ToggleValue()
		{
			if ((this.currentValue is bool) && !((bool) this.currentValue))
			{
				this.currentValue = true;
			}
			else if (this.AllowNullValue)
			{
				if (Convert.IsDBNull(this.currentValue))
				{
					this.currentValue = false;
				}
				else
				{
					this.currentValue = Convert.DBNull;
				}
			}
			else
			{
				this.currentValue = false;
			}
			this.isEditing = true;
			((CtlGrid)this.DataGridTableStyle.DataGrid).ColumnStartedEditing(Rectangle.Empty);
			if(this.CheckedChanged!=null)
			{
               this.CheckedChanged(this,EventArgs.Empty);
			}
		}

 
		[Browsable(true),Description("DataGridBooleanColumnAllowNullValue"), DefaultValue(false), Category("Behavior")]
		public bool AllowNullValue
		{
			get
			{
				return this.allowNull;
			}
			set
			{
				if (this.allowNull != value)
				{
					this.allowNull = value;
					if (!value && Convert.IsDBNull(this.currentValue))
					{
						this.currentValue = false;
						this.Invalidate();
					}
					this.OnAllowNullChanged(EventArgs.Empty);
				}
			}
		}
 
		[DefaultValue(false),TypeConverter(typeof(StringConverter))]
		public object FalseValue
		{
			get
			{
				return this.falseValue;
			}
			set
			{
				if (!this.falseValue.Equals(value))
				{
					this.falseValue = value;
					this.OnFalseValueChanged(EventArgs.Empty);
					this.Invalidate();
				}
			}
		}
		[TypeConverter(typeof(StringConverter))]
		public object NullValue
		{
			get
			{
				return this.nullValue;
			}
			set
			{
				if (!this.nullValue.Equals(value))
				{
					this.nullValue = value;
					this.OnFalseValueChanged(EventArgs.Empty);
					this.Invalidate();
				}
			}
		}
 
		[DefaultValue(true),TypeConverter(typeof(StringConverter))]
		public object TrueValue
		{
			get
			{
				return this.trueValue;
			}
			set
			{
				if (!this.trueValue.Equals(value))
				{
					this.trueValue = value;
					this.OnTrueValueChanged(EventArgs.Empty);
					this.Invalidate();
				}
			}
		}

//		[DefaultValue(ButtonState.Flat )]
//		public ButtonState ButtonStyle
//		{
//			get{return m_ButtonStyle;}
//			set{m_ButtonStyle = value;}
//		}

//		[DefaultValue(typeof(Color),"Blue")]
//		public Color BorderColor
//		{
//			get{return m_BorderColor;}
//			set{m_BorderColor = value;}
//		}
		

		#endregion

		#region Events

//		public event EventHandler AllowNullChanged
//		{
//			add
//			{
//				base.AddHandler(GridBoolColumn.EventAllowNull, value);
//			}
//			remove
//			{
//				base.RemoveHandler(GridBoolColumn.EventAllowNull, value);
//			}
//		}
// 
//		public event EventHandler FalseValueChanged
//		{
//			add
//			{
//				base.AddHandler(GridBoolColumn.EventFalseValue, value);
//			}
//			remove
//			{
//				base.RemoveHandler(GridBoolColumn.EventFalseValue, value);
//			}
//		}
		#endregion 

	}
}
