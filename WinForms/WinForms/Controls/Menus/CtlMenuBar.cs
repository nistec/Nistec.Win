using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Text;
using System.ComponentModel;
using System.Collections;


using System.Diagnostics;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;

using Nistec.Drawing;



namespace Nistec.WinForms
{
	[ToolboxItem(true), ToolboxBitmap(typeof(McMenuBar),"Toolbox.MenuBar.bmp"), ProvideProperty("ImageList", typeof(MenuItem)), ProvideProperty("ImageIndex", typeof(MenuItem)), ProvideProperty("Draw", typeof(MenuItem))]
	public class McMenuBar : MainMenu, IExtenderProvider,ILayout,IMenu
	{

		#region Private Constants
		private const int MIM_BACKGROUND = 0x2;
		private const int WM_NCPAINT = 0x0085;
		#endregion

		#region Members
		private DrawItemEventHandler drawEventHandler;
		private Font font;
		//private int grWidth;
		private Hashtable imageIndexes;
		//private Hashtable imageLists;
		private const int ImageSize = 0x16;
		private MeasureItemEventHandler measureEventHandler;
		//private RightToLeft rightToLeft;
		private ImageList m_imageList; 
		//private Hashtable Tags;

		//private StyleLayout m_StyleLayout;
		private bool created;
		private IntPtr BarBrush;
		private Bitmap barBitmap = null;
		internal bool m_DrawMenuBar=true;
 	
		private Color barColor;
		private Rectangle BarRect;

		private Form m_Form;
		#endregion

		#region Constructor

		public McMenuBar()
		{
			this.drawEventHandler = null;
			this.measureEventHandler = null;
			//this.grWidth = 0x18;
			this.drawEventHandler = new DrawItemEventHandler(this.DrawItem);
			this.measureEventHandler = new MeasureItemEventHandler(this.MeasureItem);
			this.font = SystemInformation.MenuFont;
			//this.font = new Font("Arial", 8f);
			this.imageIndexes = new Hashtable();
			//this.imageLists = new Hashtable();
			this.created=false;
			//this.m_StyleLayout=m_Style.Layout;
        	this.barColor=SystemColors.Control;
			this.BarRect=Rectangle.Empty;

			//Nistec.Util.Net.netWinMc.NetFram(this.Name);
		}

	

		#endregion

		#region Draw mainMenu

		/// <summary>
		/// Re-creates the bitmap used for painting the menu bar.
		/// </summary>
		private void RecreateBitmap() 
		{
			//Form form=GetForm();

			if ((m_Form != null) )//&& (mMenuDesigner != null)) 
			{
				//form.Resize +=new EventHandler(form_Resize);

				//this.barColor=m_Form.BackColor;

				//m_Style.BarColor=barColor;

				if (barBitmap != null) 
				{
					barBitmap.Dispose();
				}

				Rectangle BarRect= new Rectangle(0, 0, m_Form.Width, SystemInformation.MenuHeight+4);

				// Dispose of the brush used for painting the bar.
				if (BarBrush != IntPtr.Zero) 
				{
					Win32Methods.DeleteObject(BarBrush);
					BarBrush = IntPtr.Zero;
				}

				// Recreate the bitmap, then call the MenuDesigner to draw the bitmap.
				barBitmap = new Bitmap(BarRect.Width, BarRect.Height);

				using (Graphics g = Graphics.FromImage(barBitmap)) 
				{										
					PaintMenuBar(g, BarRect);
				}

				// Re-create the brush and notify Windows
				//SetBrush();
				BarBrush = Win32Methods.CreatePatternBrush(barBitmap.GetHbitmap());
				//Form form=GetForm(); 
				MENUINFO mi = new MENUINFO(m_Form);
				mi.fMask = MIM_BACKGROUND;
				mi.hbrBack = BarBrush;

				Win32Methods.SetMenuInfo(this.Handle, ref mi);	
				Win32Methods.SendMessage(m_Form.Handle, WM_NCPAINT, 0, 0);
				Win32Methods.DrawMenuBar(m_Form.Handle);

			}																			
		}

		/// <summary>
		/// Forces re-creation of the Menu Bar bitmap when the form is resized.
		/// </summary>
		/// <param name="sender">The <see cref="object"/> which triggered this event.</param>
		/// <param name="e">An <see cref="EventArgs"/> object providing data for this event.</param>
		private void form_Resize(object sender, EventArgs e) 
		{
			if(m_DrawMenuBar)
			{
				RecreateBitmap() ;
			}
		}

		/// <summary>
		/// Overridden. See <see cref="BaseMenu.PaintBar"/>
		/// </summary>		
		private void PaintMenuBar(Graphics g, Rectangle r) 
		{

			using (Brush b = LayoutManager.GetBrushMenuBar(r,270f))//GetBrushFlatLayout())//  new SolidBrush(barColor)) 
			{
				g.FillRectangle(b, r);			
			}
		}

		public void Refresh()
		{
			if(m_DrawMenuBar)
			{
				RecreateBitmap() ;
			}
		}

		private void Initilaize()
		{
			if(m_Form ==null)
			{
				m_Form=GetForm();
				m_Form.Resize+=new EventHandler(form_Resize); 
				created=true;
			}
		}

		#endregion

		#region Draw Item

		public virtual void DrawItem(object sender, DrawItemEventArgs e)
		{
			MenuItem item1 = (MenuItem) sender;
			Image image1 = null;
			int indx = -1;

			if (this.imageIndexes[item1] != null)
			{
				indx = (int) this.imageIndexes[item1];
			}
			if (((m_imageList != null) && (indx != -1)) && (indx < m_imageList.Images.Count))
			{
				image1 = this.m_imageList.Images[indx];
			}
			LayoutManager.DrawMenuItem(item1,e,image1,this.RightToLeft==RightToLeft.Yes,m_DrawMenuBar);//,barColor);
//			if(m_StyleGuide!=null)
//				m_StyleGuide.Layout.DrawMenuItem(item1,e,image1,this.RightToLeft==RightToLeft.Yes);//,barColor);
//			else
//				m_Style.DrawMenuItem(item1,e,image1,this.RightToLeft==RightToLeft.Yes);//,barColor);

		}

		#endregion

		#region IExtenderProvider Members

		public bool CanExtend(object e)
		{
			if (e is MenuItem)
			{
				return ((MenuItem)e).GetMainMenu()==this;
			}
			return (e is MenuItem);
		}
 
		public void Clear()
		{
			//this.imageLists.Clear();
			this.imageIndexes.Clear();
		}

		[Browsable(false)]
		public bool GetDraw(MenuItem control)
		{
			return true;
		}

		#endregion

		#region MenuProvider Not Used

		public MenuItem AddItem(string caption,int imageIndex,EventHandler onclick)
		{
			MenuItem mn= this.MenuItems.Add (caption ,onclick);
			this.SetImageIndex (mn,imageIndex);
			return mn;
		}

		public MenuItem AddItem(string caption,ImageList imgList,int imageIndex,EventHandler onclick)
		{
			if(this.m_imageList==null)
			{
				this.Container.Add (this.m_imageList); 
			}
			int indx=this.m_imageList.Images.Count; 
			this.m_imageList.Images.Add (imgList.Images[imageIndex]);  
			MenuItem mn= this.MenuItems.Add (caption ,onclick);
	
			if(this.m_imageList.Images.Count > indx)
			{
				this.SetImageIndex (mn,indx);
			}
			return mn;
		}

		public  void AddMenuProviderToMenu(Menu menu)
		{
			foreach (MenuItem item1 in menu.MenuItems)
			{
				if (!(item1.Parent is MainMenu))
				{
					item1.OwnerDraw = true;
					item1.DrawItem += new DrawItemEventHandler(DrawItem);
					item1.MeasureItem += new MeasureItemEventHandler(MeasureItem);
				}
			}
		}

 
		public void AddMenuProviderToMenuItem(MenuItem menuItem)
		{
			AddMenuProviderToMenuItem(menuItem, null, -1);
		}

 
		public  void AddMenuProviderToMenuItem(MenuItem menuItem, ImageList imageList, int imageIndex)
		{
			if (!(menuItem.Parent is MainMenu))
			{
				menuItem.OwnerDraw = true;
				menuItem.DrawItem += this.drawEventHandler;
				menuItem.MeasureItem += this.measureEventHandler;
				m_imageList=imageList; //SetImageList(menuItem, m_imageList);//imageList);
				SetImageIndex(menuItem, imageIndex);
			}
		}

//				private ImageList GetImageList(MenuItem control)
//				{
//					if (this.imageLists[control] == null)
//					{
//						return null;
//					}
//					return (ImageList) this.imageLists[control];
//				}
//
//				public void SetImageList(MenuItem control, ImageList value)
//				{
//					if (!this.imageLists.Contains(control))
//					{
//						this.imageLists.Add(control, value);
//					}
//					else
//					{
//						this.imageLists[control] = value;
//					}
//				}


		#endregion

		#region Settings

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

		public ImageList GetImageList()
		{
			return (ImageList) this.m_imageList;
		}

		public virtual void MeasureItem(object sender, MeasureItemEventArgs e)
		{

			if(!created)
			{
				this.Initilaize();
			}

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
				if(this.m_imageList !=null)
				{
                  this.m_imageList.Dispose(); 
				}
//				if (this.imageLists != null)
//				{
//					foreach (ImageList list1 in this.imageLists.Values)
//					{
//						if (list1 != null)
//						{
//							list1.Dispose();
//						}
//					}
//					this.imageLists.Clear();
//					this.imageLists = null;
//				}
			}
			base.Dispose(disposing);
		}

		public void SetDraw(MenuItem control, bool value)
		{

			if (!base.DesignMode)
			{
				
				if (value)
				{
					//if (control.Parent is MainMenu)
					//{
						//return;
					//}
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

 
		//[Editor(typeof(McImageIndexEditor), typeof(UITypeEditor)), TypeConverter(typeof(ImageIndexConverter))]
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

		private void ReSetting()
		{
			int indx=-1;
			foreach(MenuItem itm in this.MenuItems)
			{
				indx=-1;

				if(imageIndexes.Contains(itm))
				{
					indx=this.GetImageIndex(itm);  
				}
				ReSetting(itm,indx);
			}
			created=false;
		}

		private void ReSetting(MenuItem itm,int imageIndex)
		{
			if(this.MenuItems.Contains(itm))
			{
				// SetDraw(itm,false);
				itm.MeasureItem -= new MeasureItemEventHandler(this.MeasureItem);
				itm.DrawItem -= new DrawItemEventHandler(this.DrawItem);
				itm.OwnerDraw = false;
			}
			else
			{
                this.MenuItems.Add(itm);
				SetImageIndex(itm,imageIndex);
			}
				//created=false;
				SetDraw(itm,true);
		}

    	#endregion

		#region Properties

		[DefaultValue(null),Category("Appearance"),Description("The ImageList used to draw menu images.")]
		public ImageList ImageList 
		{
			get { return m_imageList ;  }
			set 
			{ 
				if(m_imageList!=value)
				{
					m_imageList = value; 
					//OnImageListChange(EventArgs.Empty);
				}
			}		
		}

		protected virtual void OnImageListChange(EventArgs e)
		{
			if(this.ImageList==null)
			{
				foreach(MenuItem itm in this.MenuItems) 
				{
					SetImageIndex(itm,-1);  
				}
			}
		}

		[DefaultValue(true),Category("Appearance"),Description("if true Draw the top menu bar.")]
		public bool DrawMenuBar
		{
			get { return m_DrawMenuBar ;  }
			set 
			{ 
				m_DrawMenuBar = value; 
			}		
		}

//		[Category("Appearance"),DefaultValue(typeof(Color),"Control")]
//		public Color BarColor
//		{
//			get
//			{
//				return this.barColor;
//			}
//			set
//			{
//				this.barColor = value;
//			}
//		}

//		[Category("Behavior")]
//		public Font Font
//		{
//			get
//			{
//				return this.font;
//			}
//			set
//			{
//				this.font = value;
//			}
//		}

		#endregion

		#region ILayout

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Category("Style"), DefaultValue(ControlLayout.Visual)]
        public virtual ControlLayout ControlLayout
        {
            get { return ControlLayout.Visual; }
            set
            {
            }
        }

		protected IStyle			m_StylePainter;
  
		[Browsable(false)]
		public PainterTypes PainterType
		{
			get{return PainterTypes.Flat;}
		}
		
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

		[Browsable(false)]
		public virtual IStyleLayout LayoutManager
		{
			get
			{
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

		protected virtual void OnStylePainterChanged(EventArgs e)
		{
			//this.barColor=LayoutManager.Layout.BackgroundColorInternal;
			Refresh();
		}

		protected virtual void OnStylePropertyChanged(PropertyChangedEventArgs e)
		{
			//this.barColor=LayoutManager.Layout.BackgroundColorInternal;
			if(e.PropertyName=="StylePlan")
				Refresh();
		}

		private void m_Style_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{

			OnStylePropertyChanged(e);
		}

		#endregion

	}

}
