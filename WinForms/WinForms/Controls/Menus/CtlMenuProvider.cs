using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Text;
using System.ComponentModel;
using System.Collections;

using Nistec.Drawing;



namespace Nistec.WinForms
{

	[ToolboxItem(false), ToolboxBitmap(typeof(McMenuProvider), "Toolbox.MenuDesigner.bmp"), ProvideProperty("ImageList", typeof(MenuItem)), ProvideProperty("ImageIndex", typeof(MenuItem)), ProvideProperty("Draw", typeof(MenuItem))]
	public class McMenuProvider : Component, IExtenderProvider
	{

		//private System.ComponentModel.Container components = null;

		// Fields
		private DrawItemEventHandler diEventHandler;
		private Font font;
		//private int grWidth;
		private Hashtable imageIndexes;
		private Hashtable imageLists;
		private const int ImageSize = 0x16;
		private MeasureItemEventHandler miEventHandler;
		private RightToLeft m_rightToLeft;
		private Color barColor;
		//private ImageList m_imageList; 


	
		#region Constructor
		public McMenuProvider(System.ComponentModel.IContainer container)
		{
			container.Add(this);
			this.diEventHandler = null;
			this.miEventHandler = null;
			//this.grWidth = 0x18;
			this.diEventHandler = new DrawItemEventHandler(this.DrawItem);
			this.miEventHandler = new MeasureItemEventHandler(this.MeasureItem);
			this.font = new Font("Arial", 8f);
			this.imageIndexes = new Hashtable();
			this.imageLists = new Hashtable();
		}

		public McMenuProvider()
		{
			this.diEventHandler = null;
			this.miEventHandler = null;
			//this.grWidth = 0x18;
			this.diEventHandler = new DrawItemEventHandler(this.DrawItem);
			this.miEventHandler = new MeasureItemEventHandler(this.MeasureItem);
			this.font = new Font("Arial", 8f);
			this.imageIndexes = new Hashtable();
			this.imageLists = new Hashtable();
		}

		#endregion
 
		#region Provider methods
		public static void AddMenuProviderToMenu(Menu menu, McMenuProvider provider)
		{
			foreach (MenuItem item1 in menu.MenuItems)
			{
				if (!(item1.Parent is MainMenu))
				{
					item1.OwnerDraw = true;
					item1.DrawItem += new DrawItemEventHandler(provider.DrawItem);
					item1.MeasureItem += new MeasureItemEventHandler(provider.MeasureItem);
				}
			}
		}

 
		public static void AddMenuProviderToMenuItem(MenuItem menuItem, McMenuProvider provider)
		{
			McMenuProvider.AddMenuProviderToMenuItem(menuItem, provider, null, -1);
		}

 
		public static void AddMenuProviderToMenuItem(MenuItem menuItem, McMenuProvider provider, ImageList imageList, int imageIndex)
		{
			if (!(menuItem.Parent is MainMenu))
			{
				menuItem.OwnerDraw = true;
				menuItem.DrawItem += provider.diEventHandler;
				menuItem.MeasureItem += provider.miEventHandler;
				provider.SetImageList(menuItem, imageList);
				provider.SetImageIndex(menuItem, imageIndex);
			}
		}

		#endregion

		#region IExtenderProvider

		public bool CanExtend(object e)
		{
			return (e is MenuItem);
		}

 
		public void Clear()
		{
			this.imageLists.Clear();
			this.imageIndexes.Clear();
		}

		#endregion
 
		private string ConvertShortcut(Shortcut shortcut)
		{
			if (shortcut == Shortcut.None)
			{
				return "";
			}
			Keys keys1 = (Keys) shortcut;
			return TypeDescriptor.GetConverter(typeof(Keys)).ConvertToString(keys1);
		}

 
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.font != null)
				{
					this.font.Dispose();
				}
				if (this.imageIndexes != null)
				{
					this.imageIndexes.Clear();
					this.imageIndexes = null;
				}
				if (this.imageLists != null)
				{
					foreach (ImageList list1 in this.imageLists.Values)
					{
						if (list1 != null)
						{
							list1.Dispose();
						}
					}
					this.imageLists.Clear();
					this.imageLists = null;
				}
			}
			base.Dispose(disposing);
		}


		#region Draw MenuItem

//		public virtual void DrawItem(object sender, DrawItemEventArgs e)
//		{
//	
//			const int grWidth = 0x18;
//			int  ShortcutWidth=0;
//			int  ArrowWidth=15;
//			Font font = SystemInformation.MenuFont;
//			MenuItem item1 = (MenuItem) sender;
//			bool rightToLeft=this.m_rightToLeft==RightToLeft.Yes;
//
//
//			Image image = null;
//			int indx = -1;
//
//			if (this.imageIndexes[item1] != null)
//			{
//				indx = (int) this.imageIndexes[item1];
//			}
//			if (((m_imageList != null) && (indx != -1)) && (indx < m_imageList.Images.Count))
//			{
//				image = this.m_imageList.Images[indx];
//			}
//
//
//			using (Bitmap bitmap1 = new Bitmap(e.Bounds.Width, e.Bounds.Height))
//			{
//				using (Graphics graphics1 = Graphics.FromImage(bitmap1))
//				{
//					StringFormat format2;
//					Rectangle rectIcon;
//					Rectangle rectTextItem;
//					Rectangle rectItem=new Rectangle(0, 0, e.Bounds.Width - 1, e.Bounds.Height - 1);;
//					Rectangle rectIconBar;
//					Rectangle rectText;
//
//					string textShort="";
//
//					if (item1.ShowShortcut)
//					{
//						textShort = this.ConvertShortcut(item1.Shortcut);
//						SizeF ef1 = graphics1.MeasureString(textShort, font);
//						float single1 = ef1.Width;
//						ShortcutWidth=2+(int)single1;
//					}
//                    
//					if(rightToLeft)
//					{
//						rectIcon = new Rectangle(e.Bounds.Width-grWidth, 0, grWidth, e.Bounds.Height - 1);
//						rectTextItem = new Rectangle(0, 0, e.Bounds.Width-grWidth, e.Bounds.Height);
//						rectIconBar = new Rectangle(e.Bounds.Width-grWidth, 0, grWidth, e.Bounds.Height);
//						SizeF txtSize = graphics1.MeasureString(item1.Text, font);
//						rectText = rectTextItem;
//						//rectText = new Rectangle(ShortcutWidth+(rectTextItem.Width-ShortcutWidth)-((int)txtSize.Width),rectTextItem.Y,(int)txtSize.Width,rectTextItem.Height);
//						rectText.X -= 5;
//
//					}
//					else
//					{
//						rectIcon = new Rectangle(0, 0, grWidth, e.Bounds.Height - 1);
//						rectTextItem = new Rectangle(grWidth, 0, e.Bounds.Width, e.Bounds.Height);
//						rectIconBar = new Rectangle(0, 0, grWidth, e.Bounds.Height);
//						rectText = rectTextItem;
//						rectText.X += 5;
//					}
//
//					Brush sbSelected = LayoutManager.GetBrushSelected();
//					Brush sbContent = LayoutManager.GetBrushContent();
//					Brush sbText = LayoutManager.GetBrushText();
//					Pen penSelected = LayoutManager.GetPenHot();
//
//
//					if (item1.Parent is MainMenu)
//					{
//						rectText = rectItem;
//					}
//					else if (((e.State & DrawItemState.Selected) <= DrawItemState.None) | !item1.Enabled)
//					{
//						using (Brush brush1 = LayoutManager.GetBrushMenuBar(rectIconBar,0f,rightToLeft))// McBrushes.GetControlBrush(rectIconBar, 0f))
//						{
//							graphics1.FillRectangle(brush1, rectIconBar);
//						}
//					}
//					if (((item1.Text.Length >= 4) && (item1.Text.Substring(0, 2) == "--")) && (item1.Text.Substring(item1.Text.Length - 2) == "--"))
//					{
//
//						using (Brush sb = LayoutManager.GetBrushMenuBar(rectIconBar,90f,true))
//						{
//							graphics1.FillRectangle(sb, rectItem);//McBrushes.GetActiveCaptionBrush(rectItem, 90f), rectItem);
//						}
//						using (StringFormat format1 = new StringFormat())
//						{
//							format1.Alignment = StringAlignment.Center;
//							format1.LineAlignment = StringAlignment.Center;
//							format1.FormatFlags = StringFormatFlags.NoWrap;
//							format1.Trimming = StringTrimming.EllipsisCharacter;
//							format1.HotkeyPrefix = HotkeyPrefix.Hide;
//
//							
//							using (Brush brush2 =  new SolidBrush(SystemColors.ActiveCaptionText))
//							{
//								graphics1.DrawString(item1.Text.Substring(2, item1.Text.Length - 4),font, brush2, (RectangleF) rectItem, format1);
//							}
//						}
//						graphics1.DrawRectangle(SystemPens.ControlDark, rectItem);
//						goto Label_06DD;
//					}
//					//Seperator
//					if (item1.Text == "-")
//					{
//						int lineHeight = rectTextItem.Y + (rectTextItem.Height / 2);
//			
//						graphics1.FillRectangle(sbContent, rectTextItem);
//						if(rightToLeft)
//							graphics1.DrawLine(SystemPens.ControlDark, rectTextItem.X+2 , lineHeight, rectTextItem.Width-2, lineHeight);
//						else
//							graphics1.DrawLine(SystemPens.ControlDark, rectTextItem.X + 8, lineHeight, rectTextItem.Width, lineHeight);
//			
//						goto Label_06DD;
//					}
//	
//					//Image
//					Rectangle rectImage = rectTextItem;
//					if (image != null)
//					{
//						if(rightToLeft)
//							rectImage = new Rectangle(rectTextItem.Width+((grWidth - image.Width) / 2), rectTextItem.Y + ((0x16 - image.Height) / 2), image.Width, image.Height);
//						else
//							rectImage = new Rectangle((grWidth - image.Width) / 2, rectTextItem.Y + ((0x16 - image.Height) / 2), image.Width, image.Height);
//					}
//					if (((e.State & DrawItemState.Selected) > DrawItemState.None) && item1.Enabled)
//					{
//						graphics1.FillRectangle(sbSelected, rectItem);//McBrushes.Focus, rectItem);
//						graphics1.DrawRectangle(penSelected, rectItem);//McPens.SelectedText, rectItem);
//					}
//					else
//					{
//						if (item1.Parent is MainMenu)
//						{
//	
//							using (Brush brush4 = LayoutManager.GetBrushFlatLayout())//GetBrushBar())// new SolidBrush(BarColor))//SystemColors.Control))
//							{
//								graphics1.FillRectangle(brush4, 0, 0, e.Bounds.Width, e.Bounds.Height);
//								goto Label_040D;
//							}
//						}
//						graphics1.FillRectangle(sbContent, rectTextItem);
//					}
//				Label_040D://String
//					format2 = new StringFormat();
//					try
//					{
//						if (item1.Parent is MainMenu)
//						{
//							format2.Alignment = StringAlignment.Center;
//						}
//						else
//						{
//							if(rightToLeft)
//								format2.Alignment = StringAlignment.Far;
//							else
//								format2.Alignment = StringAlignment.Near;
//						}
//						format2.LineAlignment = StringAlignment.Center;
//						format2.FormatFlags |= StringFormatFlags.NoWrap;
//						format2.HotkeyPrefix = HotkeyPrefix.Show;
//						if (item1.Enabled)
//						{
//							if (item1.DefaultItem)
//							{
//								using (Font font1 = new Font(font, FontStyle.Bold))
//								{
//									graphics1.DrawString(item1.Text, font1, sbText, (RectangleF) rectText, format2);
//									goto Label_051C;
//								}
//							}
//							graphics1.DrawString(item1.Text, font, sbText, (RectangleF) rectText, format2);
//							goto Label_051C;
//							
//						}
//						using (Brush sbDisable = LayoutManager.GetBrushTextDisable())// McControlPaint.Light(colorText, 150)))
//						{
//							graphics1.DrawString(item1.Text, font, sbDisable, (RectangleF) rectText, format2);
//						}
//					}
//					finally
//					{
//						if (format2 != null)
//						{
//							format2.Dispose();
//						}
//					}
//				Label_051C://Shortcut
//					if (item1.ShowShortcut)
//					{
//						using (StringFormat format3 = new StringFormat())
//						{
//							format3.LineAlignment = StringAlignment.Center;
//							Rectangle shortRect;
//							if(rightToLeft)
//							{
//								format3.Alignment = StringAlignment.Near;
//								shortRect=	new Rectangle(rectItem.X+ArrowWidth +10, rectItem.Y,rectItem.Width - ArrowWidth , rectItem.Height);
//							}
//							else
//							{
//								format3.Alignment = StringAlignment.Far;
//								shortRect=	new Rectangle(rectItem.X, rectItem.Y, rectItem.Width - ArrowWidth, rectItem.Height);
//							}
//							if (item1.Enabled)
//							{
//								graphics1.DrawString(textShort, font, sbText, (RectangleF) shortRect, format3);//new Rectangle(rectItem.X, rectItem.Y, rectItem.Width - 15, rectItem.Height), format3);
//								goto Label_060C;
//								
//							}
//							ControlPaint.DrawStringDisabled(graphics1, textShort, font, SystemColors.Control, (RectangleF) shortRect, format3);
//						}
//					}
//				Label_060C://Checked
//					if (item1.Checked && item1.Enabled)
//					{
//						int  iconPointX;
//						int  iconPointY=rectTextItem.Y + 11;
//						if(rightToLeft)
//						{
//							iconPointX=rectTextItem.Width+11+1;
//							rectIcon.X+=2;
//						}
//						else
//						{
//							iconPointX=11;
//							rectIcon.X++;
//						}
//						
//						rectIcon.Y++;
//						rectIcon.Width -= 4;
//						rectIcon.Height -= 2;
//	
//						graphics1.FillRectangle(sbSelected, rectIcon);//McBrushes.Focus, rectItem);
//						graphics1.DrawRectangle(penSelected, rectIcon);//McPens.SelectedText, rectItem);
//			
//						if (image == null)
//						{
//							McControlPaint.DrawCheck(graphics1, iconPointX, iconPointY, true);
//						}
//					}
//					if (image != null)
//					{
//		
//						if (item1.Enabled)
//						{
//							if(rightToLeft)
//								graphics1.DrawImage(image,rectImage.X, rectImage.Y, rectImage.Width, rectImage.Height);
//							else
//								graphics1.DrawImage(image,rectImage.X, rectImage.Y, rectImage.Width, rectImage.Height);
//						}
//						else
//						{
//							McControlPaint.DrawImageDisabled(graphics1, image as Bitmap, rectImage.X, rectImage.Y);
//						}
//					}
//				Label_06DD:
//					e.Graphics.DrawImage(bitmap1, e.Bounds.Left, e.Bounds.Top, e.Bounds.Width, e.Bounds.Height);
//
//					sbSelected.Dispose();
//					penSelected.Dispose();
//					sbContent.Dispose();
//					sbText.Dispose();
//					graphics1.Dispose();
//				}
//			}
//		}

		#endregion

		#region DrawItem

		public virtual void DrawItem(object sender, DrawItemEventArgs e)
		{
			MenuItem item1 = (MenuItem) sender;
			Image image1 = null;
			int indx = -1;

			ImageList list1 = this.imageLists[item1] as ImageList;

			if (this.imageIndexes[item1] != null)
			{
				indx = (int) this.imageIndexes[item1];
			}
			if (((list1 != null) && (indx != -1)) && (indx < list1.Images.Count))
			{
				image1 = list1.Images[indx];
			}
			//if (((m_imageList != null) && (indx != -1)) && (indx < m_imageList.Images.Count))
			//{
			//	image1 = this.m_imageList.Images[indx];
			//}
			LayoutManager.DrawMenuItem(item1,e,image1,this.RightToLeft==RightToLeft.Yes,true);
//			if(m_StyleGuide!=null)
//				m_StyleGuide.Layout.DrawMenuItem(item1,e,image1,this.RightToLeft==RightToLeft.Yes);//,barColor);
//			else
//				m_Style.DrawMenuItem(item1,e,image1,this.RightToLeft==RightToLeft.Yes);//,barColor);
		}

//		public virtual void DrawItem(object sender, DrawItemEventArgs e)
//		{
//			Color color1 = SystemColors.ControlText;
//			Color color2 = McColors.Content;
//			MenuItem item1 = (MenuItem) sender;
//			using (Bitmap bitmap1 = new Bitmap(e.Bounds.Width, e.Bounds.Height))
//			{
//				using (Graphics graphics1 = Graphics.FromImage(bitmap1))
//				{
//					StringFormat format2;
//					Rectangle rectIcon;
//					Rectangle rectangle2;
//					Rectangle rectItem=new Rectangle(0, 0, e.Bounds.Width - 1, e.Bounds.Height - 1);;
//					Rectangle rectIconBar;
//					Rectangle rectText;
//					if(rightToLeft==RightToLeft.Yes)
//					{
//						rectIcon = new Rectangle(e.Bounds.Width-this.grWidth, 0, this.grWidth, e.Bounds.Height - 1);
//						rectangle2 = new Rectangle(0, 0, e.Bounds.Width-this.grWidth, e.Bounds.Height);
//						//rectItem = new Rectangle(0, 0, e.Bounds.Width-1, e.Bounds.Height - 1);
//						rectIconBar = new Rectangle(e.Bounds.Width-this.grWidth, 0, this.grWidth, e.Bounds.Height);
//						SizeF txtSize = graphics1.MeasureString(item1.Text, this.font);
//						rectText = new Rectangle(45+(rectangle2.Width-45)-((int)txtSize.Width),rectangle2.Y,(int)txtSize.Width,rectangle2.Height);
//						//rectText.X += 45;
//
//					}
//					else
//					{
//						rectIcon = new Rectangle(0, 0, this.grWidth, e.Bounds.Height - 1);
//						rectangle2 = new Rectangle(this.grWidth, 0, e.Bounds.Width, e.Bounds.Height);
//						//rectItem = new Rectangle(0, 0, e.Bounds.Width - 1, e.Bounds.Height - 1);
//						rectIconBar = new Rectangle(0, 0, this.grWidth, e.Bounds.Height);
//						rectText = rectangle2;
//						rectText.X += 5;
//					}
//					if (item1.Parent is MainMenu)
//					{
//						rectText = rectItem;
//					}
//					else if (((e.State & DrawItemState.Selected) <= DrawItemState.None) | !item1.Enabled)
//					{
//						using (Brush brush1 = McBrushes.GetControlBrush(rectIconBar, 0f))
//						{
//							graphics1.FillRectangle(brush1, rectIconBar);
//						}
//					}
//					if (((item1.Text.Length >= 4) && (item1.Text.Substring(0, 2) == "--")) && (item1.Text.Substring(item1.Text.Length - 2) == "--"))
//					{
//						graphics1.FillRectangle(McBrushes.GetActiveCaptionBrush(rectItem, 90f), rectItem);
//						using (StringFormat format1 = new StringFormat())
//						{
//							format1.Alignment = StringAlignment.Center;
//							format1.LineAlignment = StringAlignment.Center;
//							format1.FormatFlags = StringFormatFlags.NoWrap;
//							format1.Trimming = StringTrimming.EllipsisCharacter;
//							format1.HotkeyPrefix = HotkeyPrefix.Hide;
//							using (Brush brush2 = new SolidBrush(SystemColors.ActiveCaptionText))
//							{
//								graphics1.DrawString(item1.Text.Substring(2, item1.Text.Length - 4), this.Font, brush2, (RectangleF) rectItem, format1);
//							}
//						}
//						graphics1.DrawRectangle(SystemPens.ControlDark, rectItem);
//						goto Label_06DD;
//					}
//					if (item1.Text == "-")
//					{
//						int num1 = rectangle2.Y + (rectangle2.Height / 2);
//						using (Brush brush3 = new SolidBrush(color2))
//						{
//							graphics1.FillRectangle(brush3, rectangle2);
//						}
//						graphics1.DrawLine(SystemPens.ControlDark, rectangle2.X + 8, num1, rectangle2.Width, num1);
//						goto Label_06DD;
//					}
//					Image image1 = null;
//					ImageList list1 = this.imageLists[item1] as ImageList;
//					int num2 = -1;
//					if (this.imageIndexes[item1] != null)
//					{
//						num2 = (int) this.imageIndexes[item1];
//					}
//					if (((list1 != null) && (num2 != -1)) && (num2 < list1.Images.Count))
//					{
//						image1 = list1.Images[num2];
//					}
//					Rectangle rectImage = rectangle2;
//					if (image1 != null)
//					{
//						if(rightToLeft==RightToLeft.Yes)
//							rectImage = new Rectangle(rectangle2.Width+((this.grWidth - list1.Images[num2].Width) / 2), rectangle2.Y + ((0x16 - list1.Images[num2].Height) / 2), image1.Width, image1.Height);
//						else
//							rectImage = new Rectangle((this.grWidth - list1.Images[num2].Width) / 2, rectangle2.Y + ((0x16 - list1.Images[num2].Height) / 2), image1.Width, image1.Height);
//					}
//					if (((e.State & DrawItemState.Selected) > DrawItemState.None) && item1.Enabled)
//					{
//						graphics1.FillRectangle(McBrushes.Focus, rectItem);
//						graphics1.DrawRectangle(McPens.SelectedText, rectItem);
//					}
//					else
//					{
//						if (item1.Parent is MainMenu)
//						{
//							using (Brush brush4 = new SolidBrush(SystemColors.Control))
//							{
//								graphics1.FillRectangle(brush4, 0, 0, e.Bounds.Width, e.Bounds.Height);
//								goto Label_040D;
//							}
//						}
//						using (Brush brush5 = new SolidBrush(color2))
//						{
//							graphics1.FillRectangle(brush5, rectangle2);
//						}
//					}
//				Label_040D:
//					format2 = new StringFormat();
//					try
//					{
//						if (item1.Parent is MainMenu)
//						{
//							format2.Alignment = StringAlignment.Center;
//						}
//						else
//						{
//							format2.Alignment = StringAlignment.Near;
//						}
//						format2.LineAlignment = StringAlignment.Center;
//						format2.FormatFlags |= StringFormatFlags.NoWrap;
//						format2.HotkeyPrefix = HotkeyPrefix.Show;
//						if (item1.Enabled)
//						{
//							using (Brush brush6 = new SolidBrush(color1))
//							{
//								if (item1.DefaultItem)
//								{
//									using (Font font1 = new Font(this.font, FontStyle.Bold))
//									{
//										graphics1.DrawString(item1.Text, font1, brush6, (RectangleF) rectText, format2);
//										goto Label_051C;
//									}
//								}
//								graphics1.DrawString(item1.Text, this.font, brush6, (RectangleF) rectText, format2);
//								goto Label_051C;
//							}
//						}
//						using (Brush brush7 = new SolidBrush(McControlPaint.Light(color1, 150)))
//						{
//							graphics1.DrawString(item1.Text, this.font, brush7, (RectangleF) rectText, format2);
//						}
//					}
//					finally
//					{
//						if (format2 != null)
//						{
//							format2.Dispose();
//						}
//					}
//				Label_051C:
//					if (item1.ShowShortcut)
//					{
//						using (StringFormat format3 = new StringFormat())
//						{
//							format3.Alignment = StringAlignment.Far;
//							format3.LineAlignment = StringAlignment.Center;
//							string text1 = this.ConvertShortcut(item1.Shortcut);
//							SizeF ef1 = graphics1.MeasureString(text1, this.font);
//							float single1 = ef1.Width;
//							Rectangle shortRect;
//							if(rightToLeft==RightToLeft.Yes)
//								shortRect=	new Rectangle(e.Bounds.X, rectItem.Y, 40, rectItem.Height);
//							else
//								shortRect=	new Rectangle(rectItem.X, rectItem.Y, rectItem.Width - 15, rectItem.Height);
//							if (item1.Enabled)
//							{
//								using (Brush brush8 = new SolidBrush(color1))
//								{
//									graphics1.DrawString(text1, this.font, brush8, (RectangleF) shortRect, format3);//new Rectangle(rectItem.X, rectItem.Y, rectItem.Width - 15, rectItem.Height), format3);
//									goto Label_060C;
//								}
//							}
//							ControlPaint.DrawStringDisabled(graphics1, text1, this.font, SystemColors.Control, (RectangleF) shortRect, format3);
//							//ControlPaint.DrawStringDisabled(graphics1, text1, this.font, SystemColors.Control, (RectangleF) new Rectangle(rectItem.X, rectItem.Y, rectItem.Width - 15, rectItem.Height), format3);
//						}
//					}
//				Label_060C:
//					if (item1.Checked && item1.Enabled)
//					{
//						rectIcon.X++;
//						rectIcon.Y++;
//						rectIcon.Width -= 4;
//						rectIcon.Height -= 2;
//						graphics1.FillRectangle(McBrushes.Selected, rectIcon);
//						graphics1.DrawRectangle(McPens.SelectedText, rectIcon);
//						if (image1 == null)
//						{
//							if(rightToLeft==RightToLeft.Yes)
//								McControlPaint.DrawCheck(graphics1, rectangle2.Width+11, rectangle2.Y + 11, true);
//							else
//								McControlPaint.DrawCheck(graphics1, 11, rectangle2.Y + 11, true);
//						}
//					}
//					if (image1 != null)
//					{
//		
//						if (item1.Enabled)
//						{
//							if(rightToLeft==RightToLeft.Yes)
//								list1.Draw(graphics1,rectImage.X, rectImage.Y, rectImage.Width, rectImage.Height, num2);
//							else
//								list1.Draw(graphics1,rectImage.X, rectImage.Y, rectImage.Width, rectImage.Height, num2);
//						}
//						else
//						{
//							McControlPaint.DrawImageDisabled(graphics1, image1 as Bitmap, rectImage.X, rectImage.Y);
//						}
//					}
//				Label_06DD:
//					e.Graphics.DrawImage(bitmap1, e.Bounds.Left, e.Bounds.Top, e.Bounds.Width, e.Bounds.Height);
//					graphics1.Dispose();
//				}
//			}
//		}
//
 
		#endregion

		[Browsable(false)]
		public bool GetDraw(MenuItem control)
		{
			return true;
		}

 
		//[TypeConverter(typeof(ImageIndexConverter)), Editor(typeof(Design.McImageIndexEditor), typeof(UITypeEditor))]
        [Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design", typeof(System.Drawing.Design.UITypeEditor)), Category("Appearance"), DefaultValue(-1), Localizable(true), TypeConverter(typeof(ImageIndexConverter))]
        public int GetImageIndex(MenuItem control)
		{
			if (this.imageIndexes[control] == null)
			{
				return -1;
			}
			return (int) this.imageIndexes[control];
		}

		public ImageList GetImageList(MenuItem control)
		{
			if (this.imageLists[control] == null)
			{
				return null;
			}
			return (ImageList) this.imageLists[control];
		}

		public virtual void MeasureItem(object sender, MeasureItemEventArgs e)
		{
			SizeF ef1;
			MenuItem item1 = (MenuItem) sender;
			Graphics graphics1 = e.Graphics;
			if (((item1.Text.Length >= 4) && (item1.Text.Substring(0, 2) == "--")) && (item1.Text.Substring(item1.Text.Length - 2) == "--"))
			{
				ef1 = graphics1.MeasureString(item1.Text.Substring(2, item1.Text.Length - 4), this.font);
				e.ItemWidth = (int) ef1.Width;
				e.ItemHeight = 20;
			}
			else if (item1.Text != "-")
			{
				int num1 = 0;
				if (item1.ShowShortcut)
				{
					ef1 = graphics1.MeasureString(this.ConvertShortcut(item1.Shortcut), this.font);
					num1 = (int) ef1.Width;
				}
				if (num1 != 0)
				{
					num1 += 15;
				}
				e.ItemHeight = ((int) this.font.GetHeight()) + 6;
				if (e.ItemHeight < 0x16)
				{
					e.ItemHeight = 0x16;
				}
				ef1 = graphics1.MeasureString(item1.Text, this.font);
				e.ItemWidth = ((int) ef1.Width) + num1;
				if (!(item1.Parent is MainMenu))
				{
					e.ItemWidth += 0x2c;
				}
			}
			else
			{
				e.ItemWidth = 20;
				e.ItemHeight = 3;
			}
		}

 
		public void SetDraw(MenuItem control, bool value)
		{
			if (!base.DesignMode)
			{
				if (value)
				{
					if (control.Parent is MainMenu)
					{
						return;
					}
					control.MeasureItem += new MeasureItemEventHandler(this.MeasureItem);
					control.DrawItem += new DrawItemEventHandler(this.DrawItem);
					control.OwnerDraw = true;
				}
				else
				{
					control.MeasureItem -= new MeasureItemEventHandler(this.MeasureItem);
					control.DrawItem -= new DrawItemEventHandler(this.DrawItem);
					control.OwnerDraw = false;
					if (this.imageIndexes.Contains(control))
					{
						this.imageIndexes.Remove(control);
					}
				}
			}
		}

 
		//[Editor(typeof(Design.McImageIndexEditor), typeof(UITypeEditor)), TypeConverter(typeof(ImageIndexConverter))]
        [Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design", typeof(System.Drawing.Design.UITypeEditor)), Category("Appearance"), DefaultValue(-1), Localizable(true), TypeConverter(typeof(ImageIndexConverter))]
        public void SetImageIndex(MenuItem control, int value)
		{
			if (!this.imageIndexes.Contains(control))
			{
				this.imageIndexes.Add(control, value);
			}
			else
			{
				this.imageIndexes[control] = value;
			}
		}

 
		public void SetImageList(MenuItem control, ImageList value)
		{
			if (!this.imageLists.Contains(control))
			{
				this.imageLists.Add(control, value);
			}
			else
			{
				this.imageLists[control] = value;
			}
		}

		[Category("Behavior")]
		public Font Font
		{
			get
			{
				return this.font;
			}
			set
			{
				this.font = value;
			}
		}
 
		[Category("Behavior")]
		public RightToLeft RightToLeft
		{
			get
			{
				return this.m_rightToLeft;
			}
			set
			{
				this.m_rightToLeft = value;
			}
		}

		#region ILayout

		//protected IStyleGuide			m_StyleGuide;
		protected IStyle			m_StylePainter;

		[Category("Style"),DefaultValue(null),RefreshProperties(RefreshProperties.All)]
		public IStyle StylePainter
		{
			get {return m_StylePainter;}
			set 
			{
				if(m_StylePainter!=value)
				{
					if (this.m_StylePainter != null)
						this.m_StylePainter.PropertyChanged -=new PropertyChangedEventHandler(m_Style_PropertyChanged); 
					m_StylePainter = value;
					if (this.m_StylePainter != null)
						this.m_StylePainter.PropertyChanged +=new PropertyChangedEventHandler(m_Style_PropertyChanged); 
					OnStylePainterChanged(EventArgs.Empty);
				}
			}
		}

//		[Category("Style"),DefaultValue(null),RefreshProperties(RefreshProperties.All)]
//		public IStyleGuide StyleGuide
//		{
//			get {return m_StyleGuide;}
//			set 
//			{
//				if(m_StyleGuide!=value)
//				{
//					if (this.m_StyleGuide != null)
//						this.m_StyleGuide.PropertyChanged -=new PropertyChangedEventHandler(m_Style_PropertyChanged); 
//					m_StyleGuide = value;
//					if (this.m_StyleGuide != null)
//						this.m_StyleGuide.PropertyChanged +=new PropertyChangedEventHandler(m_Style_PropertyChanged); 
//					OnStyleGuideChanged(EventArgs.Empty);
//				}
//			}
//		}

		[Browsable(false)]
		public virtual IStyleLayout LayoutManager
		{
			get
			{
				//if(this.m_StyleGuide!=null)
				//	return this.m_StyleGuide.Layout as IStyleLayout;
				if(this.m_StylePainter!=null)
					return this.m_StylePainter.Layout as IStyleLayout;
				else
					return StyleLayout.DefaultLayout as IStyleLayout;// this.m_Style as IStyleLayout;
			}
		}

		public virtual void SetStyleLayout(StyleLayout value)
		{
			if(this.m_StylePainter!=null)
				this.m_StylePainter.Layout.SetStyleLayout(value); 
		}

		public virtual void SetStyleLayout(Styles value)
		{
			if(this.m_StylePainter!=null)
				m_StylePainter.Layout.SetStyleLayout(value);
		}

//		protected virtual void OnStyleGuideChanged(EventArgs e)
//		{
//			this.barColor=LayoutManager.Layout.BackgroundColorInternal;
//		}

		protected virtual void OnStylePainterChanged(EventArgs e)
		{
			this.barColor=LayoutManager.Layout.BackgroundColorInternal;
		}

		protected virtual void OnStylePropertyChanged(PropertyChangedEventArgs e)
		{
			this.barColor=LayoutManager.Layout.BackgroundColorInternal;
		}

		private void m_Style_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{

			OnStylePropertyChanged(e);
		}

		#endregion

	}

}
