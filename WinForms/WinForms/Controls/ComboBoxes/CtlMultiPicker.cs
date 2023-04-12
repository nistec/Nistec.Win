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
using Nistec.Drawing;
using Nistec.Win;


namespace Nistec.WinForms
{

	public enum PickerType
	{
		Normal=0,
		Images=1,
		Colors=2,
		Fonts=3,
		Bool=4
	}


    [Designer(typeof(Design.McEditDesigner)), ToolboxItem(true), ToolboxBitmap(typeof(McMultiPicker), "Toolbox.DropDown.bmp")]
	[DefaultEvent("SelectedIndexChanged")]
	public class McMultiPicker : McComboBox,IMcList//,IMcTextBox,ICombo,IDropDown
	{

		#region ComboMembers

        private PickerType pickerType;
		private ImageList					imageList;
		internal bool listCreated;

		private float fontSize;  
		private int defaultImageIndex=-1;
		private DrawItemStyle drawItemStyle=DrawItemStyle.Default;

		#endregion

		#region Constructors
		
		public McMultiPicker()
		{
			pickerType=PickerType.Normal;
			InitDropDown();
		}

        public McMultiPicker(PickerType listType)
		{
            pickerType = listType;
			InitDropDown();
		}

		private void InitDropDown()
		{
			this.fontSize=8.25f;
	
			InitializeComponent();
			//base.ButtonImage =ResourceUtil.LoadImage (Global.ImagesPath + "btnCombo.gif");
			m_TextBox.ReadOnly =true;
			ControlLayout =ControlLayout.Visual; 
			base.DropDownStyle=ComboBoxStyle.DropDown;
			ButtonToolTip ="DropDown list box";
			base.ReadOnly=true;
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated (e);
			if(pickerType==PickerType.Images)
			{
				if(imageList!=null && this.ItemHeight<imageList.ImageSize.Height)
				{
					this.ItemHeight=imageList.ImageSize.Height+1;
				}
			}
			SetDefaultValue(DefaultValue);
		}

		private void Resetting()
		{

			switch(this.pickerType)
			{
				case PickerType.Colors:
					//base.DefaultValue="White";
					ButtonToolTip ="Color list box";
					//this.DropDownStyle=ComboBoxStyle.DropDownList;
					break;
				case PickerType.Fonts:
					//base.DefaultValue="Arial";
					ButtonToolTip ="Font list box";
					break;
				case PickerType.Bool:
					//base.DefaultValue=Nistec.Resources.ResexLibrary.GetCommonString(Nistec.Resources.RM.Yes);
					ButtonToolTip ="Boolean list box";
					break;
				case PickerType.Images:
					//base.DefaultValue="";
					ButtonToolTip ="Image list box";
					break;
				default:
					ButtonToolTip ="DropDown list box";
					//base.DefaultValue="";
					break;
			}

			SetListType(this.pickerType);

			m_TextBox.Visible=(this.pickerType!=PickerType.Colors && DropDownStyle!=ComboBoxStyle.DropDownList);
			this.Invalidate();

		}

        internal void SetListType(PickerType type)
		{
			this.listCreated=false;
            this.pickerType = type;
			//this.internalList.pickerType=type;
         
		}

		public void SetDefaultValue(string value)
		{
			if(DefaultValue!=value)
			   DefaultValue=value;

			if(value!=null && value !="")
			{
				if(pickerType==PickerType.Fonts)
				{
					this.SetSelectedFont(new Font(value,this.fontSize));
				}
				else if(pickerType==PickerType.Colors)
				{
					Color color=Color.FromName(value);
					this.SetSelectedColor(color);
				}
				else if(pickerType==PickerType.Bool)
				{
					this.SelectedItem=value;
				}
                this.Text = DefaultValue;
			}

		}
		#endregion

		#region Dispose

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
//				if(m_DropDown!=null)
//				{
//					m_DropDown.Dispose();
//				}
			}
			base.Dispose( disposing );
		}

		#endregion

		#region Component Designer generated code
		private void InitializeComponent()
		{
			this.Name = "McDropDown";

		}
		#endregion

		#region DrawItems

		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			if(this.DrawMode!=DrawMode.Normal)
			{
				switch(pickerType)
				{
					case PickerType.Colors:
						DrawItemColor(e);
						break;
					case PickerType.Fonts:
						DrawItemFont(e);
						break;
					case PickerType.Images:
						DrawItemImages (e);
						break;
					default:
						base.OnDrawItem (e);
						break;

				}
			}
		}
		
		protected virtual void DrawItemImages(DrawItemEventArgs e)
		{
			//if (this.DrawMode == DrawMode.OwnerDrawFixed || this.DrawMode == DrawMode.OwnerDrawVariable)
			//{
				Graphics graphics1 = e.Graphics;
				Rectangle rectangle1 = e.Bounds;
				if ((e.State & DrawItemState.Selected) > DrawItemState.None)
				{
					rectangle1.Width--;
				}
				DrawItemState state1 = e.State;
				if ((e.Index != -1) && (this.Items.Count > 0))
				{
					if (e.Index >= this.imageList.Images.Count)
					{

					}
					int num1=this.defaultImageIndex;
					if(this.defaultImageIndex == -1 && e.Index < this.imageList.Images.Count)
					{
						num1=e.Index;
					}
					
					base.LayoutManager.DrawItem(graphics1, rectangle1,this, state1, this.GetItemText(this.Items[e.Index]),num1);
				}
			//}
		}

		private void DrawItemFont(DrawItemEventArgs e)
		{
			if (e.Index != -1) //|| this.drawCombo) && base.Enabled)
			{
				
				string text1 =base.Text;// ((McFont)owner).SelectedFont.Name;
				if (e.Index != -1)
				{
					text1 = this.Items[e.Index].ToString();
				}
				Rectangle rectangle1 = e.Bounds;
				if ((e.State & DrawItemState.Selected) > DrawItemState.None)
				{
					rectangle1.Width--;
				}
				McPaint.DrawItem(e.Graphics, rectangle1, e.State, text1, null, e.Index, this.Font, this.BackColor, this.ForeColor, 30, base.RightToLeft);
				Rectangle rectangle2 = new Rectangle(e.Bounds.X, e.Bounds.Y, 0x1c, e.Bounds.Height - 1);
				Graphics graphics1 = e.Graphics;
				if (((e.State & DrawItemState.Focus) > DrawItemState.None) || ((e.State & DrawItemState.Selected) > DrawItemState.None))
				{
					graphics1.DrawRectangle(McPens.SelectedText, rectangle2);
				}
				if (base.Enabled)
				{
					using (StringFormat format1 = new StringFormat())
					{
						format1.LineAlignment = StringAlignment.Center;
						format1.Alignment = StringAlignment.Center;
						format1.FormatFlags = StringFormatFlags.NoWrap;
						if (this.RightToLeft == RightToLeft.Yes)
						{
							format1.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
						}
						new Rectangle(e.Bounds.X + 1, e.Bounds.Y + 1, 0x1c, e.Bounds.Height - 3);
						using (Font font1 = new Font(text1, 10f))
						{
							graphics1.DrawString("ab", font1, McBrushes.SelectedText, (RectangleF) rectangle2, format1);
						}
					}
				}
			}
		}

		private void DrawItemColor(System.Windows.Forms.DrawItemEventArgs e)
		{

			const int RECTCOLOR_LEFT = 4;
			const int RECTCOLOR_TOP = 2;
			const int RECTCOLOR_WIDTH = 40;
			const int RECTTEXT_MARGIN = 4;
			const int RECTTEXT_LEFT = RECTCOLOR_LEFT + RECTCOLOR_WIDTH + RECTTEXT_MARGIN;

			Graphics g = e.Graphics;
			Color BlockColor = Color.Empty;
			int left = RECTCOLOR_LEFT;
			Rectangle bounds =e.Bounds;

			if(e.State == DrawItemState.Selected || e.State == DrawItemState.None)
				e.DrawBackground();

			bool selected = (e.State & DrawItemState.Selected) > 0;
			if ( e.Index != -1 && this.Items.Count > 0)
			{
				if (selected)// && Enabled )
				{
					using ( Brush b = base.LayoutManager.GetBrushSelected())// new SolidBrush(Color.Yellow))//McBrushes.Selected m_SelectionColor) )
					{
						g.FillRectangle(b, bounds.Left, bounds.Top, bounds.Width, bounds.Height);
					}
					using ( Pen p = base.LayoutManager.GetPenBorder())//new Pen(m_SelectionBorderColor) )
					{
						g.DrawRectangle(p, bounds.Left, bounds.Top, bounds.Width-1, bounds.Height-1);
					}
				}
				else
				{
					using (Brush b = new SolidBrush(this.BackColor))
					{
						g.FillRectangle(b, bounds.Left, bounds.Top, bounds.Width, bounds.Height);
					}
				}
			}

			if(e.Index == -1) 
				BlockColor = this.SelectedIndex < 0 ? BackColor : Color.FromName(this.SelectedItem.ToString ());
			else 
				BlockColor = Color.FromName((string)this.Items[e.Index]);
		
			g.FillRectangle(new SolidBrush(BlockColor),left,e.Bounds.Top+RECTCOLOR_TOP,RECTCOLOR_WIDTH,this.ItemHeight - 2 * RECTCOLOR_TOP);
	
			g.DrawRectangle(Pens.Black,left,e.Bounds.Top+RECTCOLOR_TOP,RECTCOLOR_WIDTH,this.ItemHeight - 2 * RECTCOLOR_TOP);
		
			g.DrawString(BlockColor.Name,e.Font,new SolidBrush(ForeColor),new Rectangle(RECTTEXT_LEFT,e.Bounds.Top,e.Bounds.Width-RECTTEXT_LEFT,this.ItemHeight));
		
			//base.OnDrawItem(e);
	
		}


		#endregion

		#region Fill

		protected override void ShowPopUp()
		{
			if(pickerType==PickerType.Fonts)
			{
				FillFont();
			}
			else if(pickerType==PickerType.Colors)
			{
				FillColors();
			}
			else if(pickerType==PickerType.Bool)
			{
				FillBool();
			}

			base.ShowPopUp ();
		}

		internal void FillFont()
		{
			if(listCreated)
				return;
			try
			{
				ArrayList list1 = new ArrayList();
				int num1 = 0;
				int num2 = -1;
				FontFamily[] familyArray1 = FontFamily.Families;
				string text=base.Text;
				for (int num3 = 0; num3 < familyArray1.Length; num3++)
				{
					FontFamily family1 = familyArray1[num3];
					if (family1.IsStyleAvailable(FontStyle.Regular))
					{
						list1.Add(family1.Name);
						if (text == family1.Name)
						{
							num2 = num1;
						}
						num1++;
					}
				}
				this.Items.Clear();
				this.Items.AddRange((object[]) list1.ToArray(typeof(object)));
				this.DrawMode=DrawMode.OwnerDrawFixed;
				this.ItemHeight=14;
				this.SelectedIndex = num2;
				listCreated=true;
			}
			catch
			{
			}
		}

		internal void FillColors()
		{
			if(listCreated)
				return;
			try
			{
				ArrayList list1 = new ArrayList();
				int num1 = 0;
				int num2 = -1;
				Nistec.Collections.ColorCollection colors=new Nistec.Collections.KnownColorCollection(Nistec.Collections.KnownColorFilter.Web);
				foreach(Color color in colors)
				{
					list1.Add(color.Name);
					if (this.Parent.Text == color.Name)
					{
						num2 = num1;
					}
					num1++;
				}
				this.Items.Clear();
				this.Items.AddRange((object[]) list1.ToArray(typeof(object)));
				this.DrawMode=DrawMode.OwnerDrawFixed;
				this.ItemHeight=14;
				this.SelectedIndex = num2;
				listCreated=true;
			}
			catch
			{
			}
		}

		/// <summary>
		/// Fill bollean values Yes/No
		/// </summary>
		internal void FillBool()
		{
			if(listCreated)
				return;
			this.Items.Clear();
            this.Items.AddRange(WinHelp.GetBoolRange(BoolFormats.YesNo));
			listCreated=true;
		}
		internal void FillBool(BoolFormats formatType)
		{
			if(listCreated)
				return;
			this.Items.Clear();
            this.Items.AddRange(WinHelp.GetBoolRange(formatType)); 
			listCreated=true;
		}
		internal void FillBool(string FalseValue,string TrueValue)
		{
			if(listCreated)
				return;
			this.Items.Clear();  
			this.Items.AddRange( new object[]{FalseValue,TrueValue}); 
			listCreated=true;
		}


		#endregion

		#region Virtual Events

		Rectangle ItemRect=Rectangle.Empty;
		Rectangle TextRect=Rectangle.Empty;

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint (e);
			if(this.pickerType==PickerType.Colors)
			{
				DrawControlColors(e.Graphics);
			}
			else if(this.pickerType==PickerType.Images)
			{
				DrawControlImages(e.Graphics);
			}
//			else
//				base.OnPaint (e);//base.DrawControl (g, hot, focused);
		}


	
//		protected override void DrawControl(Graphics g, bool hot, bool focused)
//		{
//			if(this.pickerType==PickerType.Colors)
//			{
//				DrawControlColors(g,hot,focused);
//			}
//			else if(this.pickerType==PickerType.Images)
//			{
//				DrawControlImages(g,hot,focused);
//			}
//			else
//				base.DrawControl (g, hot, focused);
//		}

	
		protected virtual void DrawControlImages(Graphics g)//, bool hot, bool focused)
		{
			int index=this.SelectedIndex;

			if(index==-1 || imageList==null)
			{
				//base.DrawControl (g, hot, focused);
				return;
			}

			SetItemsRect(2,this.imageList.ImageSize.Width,this.imageList.ImageSize.Height);
			base.TextRectInternal=this.TextRect;
			//base.DrawControl (g, hot, focused);

			int imgIndex=this.defaultImageIndex;
	
			if(this.defaultImageIndex == -1 && index < this.imageList.Images.Count)
			{
				imgIndex=index;
			}

			if(imageList.ImageSize.Height<this.Height)
			{
				g.DrawImage(imageList.Images[imgIndex],this.ItemRect);
			}
		}


		protected virtual void DrawControlColors(Graphics g)//, bool hot, bool focused)
		{

			const int RECTCOLOR_WIDTH = 40;
		
			SetItemsRect(4,RECTCOLOR_WIDTH,10);
			base.TextRectInternal=this.TextRect;
			//base.DrawControl (g, hot, focused);

			Color selectedColor;
			if(SelectedItem!=null)
				selectedColor  =System.Drawing.Color.FromName  ( SelectedItem.ToString ());
			else if(DefaultValue!=null && DefaultValue!="")
				selectedColor  =System.Drawing.Color.FromName  ( DefaultValue);
			else
				selectedColor  =Color.White;

			g.FillRectangle(new SolidBrush(selectedColor),this.ItemRect);
	
			g.DrawRectangle(Pens.Black,this.ItemRect);
		
			//g.DrawString(selectedColor.Name,Font,new SolidBrush(ForeColor),txtRect);//new Rectangle(RECTTEXT_LEFT,top,bounds.Width-RECTTEXT_LEFT,ItemHeight));
			//}
		}

		private void SetItemsRect(int textMargin,int ItemWidth,int ItemHeight)
		{

			const int RECT_LEFT = 4;
			int RECTTEXT_MARGIN = textMargin;
	
			Rectangle rect= this.ClientRectangle;
			int RECT_WIDTH = ItemWidth;
			int RECTTEXT_LEFT = RECT_LEFT + RECT_WIDTH + RECTTEXT_MARGIN;

			int top =(Height -ItemHeight)/2;
			int btnWidth=base.GetButtonRect.Width;
			int left =RECT_LEFT;
      
			this.ItemRect=new Rectangle(left,top,RECT_WIDTH,ItemHeight);
			this.TextRect=new Rectangle(RECTTEXT_LEFT,top,rect.Width-RECTTEXT_LEFT-btnWidth,ItemHeight);
	
			if(this.RightToLeft==RightToLeft.Yes )
			{
				left =Width-RECT_WIDTH-RECT_LEFT;
				this.ItemRect=new Rectangle(left,top,RECT_WIDTH,ItemHeight);
				this.TextRect=new Rectangle(btnWidth+2,top,rect.Width-RECTTEXT_LEFT-btnWidth,ItemHeight);
			}
		}

		#endregion

		#region Combo Properties

        [Category("Advanced"), DefaultValue(PickerType.Normal)]
        public PickerType PickerType
		{
            get { return this.pickerType; }
			set
			{
                if (PickerType != value)
				{
                    this.pickerType = value;
					Resetting();
				}
			}
		}

 		[Category("Behavior"), DefaultValue(null)]
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

		public Color GetSelectedColor()
		{
			if(this.PickerType==PickerType.Colors && IsHandleCreated)
			{
				return Color.FromName((string)base.Items[SelectedIndex]);

			}
			return Color.Black;
		}

		public void SetSelectedColor(Color value)
		{
			if(this.PickerType==PickerType.Colors)
			{
				base.SelectedItem=value.Name;
				this.Invalidate();
			}			
		}
		

		public Font GetSelectedFont()
		{
			if(this.PickerType==PickerType.Fonts && IsHandleCreated)
			{
				return new Font((string)base.Items[SelectedIndex],fontSize);
			}
			return Control.DefaultFont;
		}
		public void	SetSelectedFont(Font value)
		{
			if(this.PickerType==PickerType.Fonts)
			{
				int indx= base.Items.IndexOf(value.Name);
				if(indx> -1)
				{
					base.SelectedIndex=indx;
					this.Invalidate();
				}
			}			
			
		}


//		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("ListControlSelectedColor"), Browsable(false), DefaultValue(typeof(Color),"Black"), Category("Data")]
//		public Color SelectedColor
//		{
//			get
//			{
//				if(this.PickerType==PickerType.Colors && IsHandleCreated)
//				{
//					return Color.FromName((string)base.Items[SelectedIndex]);
//
//				}
//				return Color.Black;
//			}
//			set
//			{
//				if(this.PickerType==PickerType.Colors)
//				{
//		
//					base.SelectedItem=value.Name;
//					this.Invalidate();
////					int indx= m_DropDown.Items.IndexOf(value.Name);
////					if(indx> -1)
////					{
////						m_DropDown.SelectedIndex=indx;
////						this.Invalidate();
////					}
//				}			
//			}
//		}
//
//		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("ListControlSelectedColor"), Browsable(false), Category("Data")]
//		public Font SelectedFont
//		{
//			get
//			{
//				if(this.PickerType==PickerType.Fonts && IsHandleCreated)
//				{
//					return new Font((string)base.Items[SelectedIndex],fontSize);
//				}
//				return Control.DefaultFont;
//			}
//			set
//			{
//				if(this.PickerType==PickerType.Fonts)
//				{
//		
//					int indx= base.Items.IndexOf(value.Name);
//					if(indx> -1)
//					{
//						base.SelectedIndex=indx;
//						this.Invalidate();
//					}
//				}			
//			}
//		}
//
//		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("Font size for selected font property"), Browsable(false),DefaultValue(8.25f), Category("Data")]
		public float FontSize
		{
			get
			{
				return this.fontSize;
			}
			set
			{
				if(value >0)
				{
					this.fontSize=value;
				}			
			}
		}

		[DefaultValue(-1), Category("Behavior")]
		public int DefaultImageIndex
		{
			get
			{
				return this.defaultImageIndex;
			}
			set
			{
				this.defaultImageIndex = value;
				base.Invalidate();
			}
		}

		[DefaultValue(DrawItemStyle.Default), Category("Behavior")]
		public DrawItemStyle DrawItemStyle
		{
			get
			{
				return this.drawItemStyle;
			}
			set
			{
				this.drawItemStyle = value;
				base.Invalidate();
			}
		}

		#endregion

		#region DropDown Properties

	
		[Description("ComboBoxStyle"), DefaultValue(1), RefreshProperties(RefreshProperties.Repaint), Category("Appearance")]
		public new ComboBoxStyle DropDownStyle
		{
			get
			{
				return base.DropDownStyle;
			}
			set
			{
				if (base.DropDownStyle != value)
				{
					if (!Enum.IsDefined(typeof(ComboBoxStyle), value))
					{
						throw new InvalidEnumArgumentException("value", (int) value, typeof(ComboBoxStyle));
					}
					if (base.IsHandleCreated)
					{
						base.RecreateHandle();
					}
					if(this.pickerType==PickerType.Colors)
					{
						base.DropDownStyle=ComboBoxStyle.DropDownList;
						return;
					}
  					base.DropDownStyle=value;
					//this.OnDropDownStyleChanged(EventArgs.Empty);
					if(base.DropDownStyle==ComboBoxStyle.DropDownList)
					{
						this.m_TextBox.Visible=false;
					}
					else
					{
						this.m_TextBox.Visible=true;
					}
					this.Invalidate();  

				}
			}
		}


		#endregion

		#region Hide Combo Properties

		[Browsable(false),EditorBrowsable(EditorBrowsableState.Never)]
		public new DrawMode DrawMode
		{
			get{return DrawMode.OwnerDrawFixed;}
			set{base.DrawMode=DrawMode.OwnerDrawFixed;}
		}


		[Browsable(false),EditorBrowsable(EditorBrowsableState.Never)]
		public new object DataSource
		{
			get{ return base.DataSource; }
			set
			{
				base.DataSource=value;
			}
		}

		[Browsable(false),EditorBrowsable(EditorBrowsableState.Never)]
		public new string DisplayMember
		{
			get{ return base.DisplayMember; }
			set
			{
				base.DisplayMember = value; 
			}
		}

		[Browsable(false),EditorBrowsable(EditorBrowsableState.Never)]
		public new string ValueMember
		{
			get{ return base.ValueMember; }
			set
			{
				base.ValueMember = value;
			}
		}

		[Browsable(false),EditorBrowsable(EditorBrowsableState.Never)]
		public override object SelectedValue
		{
			get
			{
				return base.SelectedValue;
			}
			set
			{
				base.SelectedValue =value; 
			}
		}
 
		#endregion

		#region IMcTextBox Members

		[Browsable(false),Category("Behavior"),DefaultValue(false)]
		public override bool ReadOnly
		{
            get { return false;/* m_TextBox.ReadOnly;*/ }
			set	{/*m_TextBox.ReadOnly = value;*/}
		}

		#endregion

		#region IBind Members

		public override string BindPropertyName()
		{
			return "Text";
		}

		#endregion

	}

}