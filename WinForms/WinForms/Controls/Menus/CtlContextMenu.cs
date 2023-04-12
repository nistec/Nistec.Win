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
	[ToolboxItem(false), ToolboxBitmap(typeof(McContextMenu),"Toolbox.PopUpMenu.bmp"), ProvideProperty("ImageList", typeof(MenuItem)), ProvideProperty("ImageIndex", typeof(MenuItem)), ProvideProperty("Draw", typeof(MenuItem))]
	public class McContextMenu : ContextMenu, IExtenderProvider,ILayout,IMenu
	{

		#region Members
		private DrawItemEventHandler drawEventHandler;
		private Font font;
		//private int grWidth;
		private Hashtable imageIndexes;
		//private Hashtable imageLists;
		private const int ImageSize = 0x16;
		const int grWidth = 0x18;
		private MeasureItemEventHandler measureEventHandler;
		//private RightToLeft rightToLeft;
		private ImageList m_imageList; 
		private Hashtable hashTags;

		internal int itemWidth;

		//private bool created;
		#endregion

		#region Constructor

		public McContextMenu()
		{
			this.drawEventHandler = null;
			this.measureEventHandler = null;
			//this.grWidth = 0x18;
			this.drawEventHandler = new DrawItemEventHandler(this.DrawItem);
			this.measureEventHandler = new MeasureItemEventHandler(this.MeasureItem);
			this.font = SystemInformation.MenuFont;
			//this.font = new Font("Arial", 8f);
			this.imageIndexes = new Hashtable();
			this.hashTags = new Hashtable();
			//this.imageLists = new Hashtable();
			//created=false;
			//Nistec.Util.Net.netWinMc.NetFram(this.Name);
		}

		#endregion

		#region Draw Item

		public virtual void DrawItem(object sender, DrawItemEventArgs e)
		{
			Image image1 = null;
			int indx = -1;
			MenuItem item1 = (MenuItem) sender;
			if (this.imageIndexes[item1] != null)
			{
				indx = (int) this.imageIndexes[item1];
			}
			if (((m_imageList != null) && (indx != -1)) && (indx < m_imageList.Images.Count))
			{
				image1 = this.m_imageList.Images[indx];
			}
			LayoutManager.DrawMenuItem(item1,e,image1,this.RightToLeft==RightToLeft.Yes,false);
//			if(m_StyleGuide!=null)
//				m_StyleGuide.Layout.DrawMenuItem(item1,e,image1,this.RightToLeft==RightToLeft.Yes);//,barColor);
//			else
//				m_Style.DrawMenuItem(item1,e,image1,this.RightToLeft==RightToLeft.Yes);//,barColor);

			//m_Style.DrawItem(item1,e,image1,this.RightToLeft==RightToLeft.Yes);
            //m_Style.PaintItem(item1,e,image1, GetImageSize(),this.RightToLeft==RightToLeft.Yes);

		}

		#endregion

		#region HashTags Members

		public object GetTag(MenuItem item) 
		{
			if (item != null && hashTags != null) 
			{
				if (hashTags.ContainsKey(item)) 
				{
					return hashTags[item];
				}
			}
			return null;			
		}

		public void AddTag(MenuItem item, object value)
		{			
			if (item != null && hashTags != null) 
			{
				SetTag(item,value);//hashTags[item] = value;
			}
		}     

		public void AddTag(int index, object value)
		{			
			MenuItem item=this.MenuItems[index];

			if (item != null && hashTags != null) 
			{
				SetTag(item,value);// hashTags[item] = value;
			}
		}     

		public void RemoveTag(Component item)
		{			
			if (item != null && hashTags != null) 
			{
				if (hashTags.ContainsKey(item)) 
				{
					hashTags.Remove(item)  ;
				}
			}
		}     

		#endregion

		#region IExtenderProvider Members

		public bool CanExtend(object e)
		{
			if (e is MenuItem)
		{
                ContextMenu mnu=((MenuItem)e).GetContextMenu();
				if(mnu==null )
				{
					if(((MenuItem)e).GetMainMenu()!=null)
						return false;
			        
//                   // this.SetDraw(((MenuItem)e),true); 
//				   //this.SetImageIndex(((MenuItem)e),-1);
//				
//					mnu=((MenuItem)e).GetContextMenu();
//					if(mnu!=null )
//					{
//						if(mnu==this)
//							return true;
//						else
//							return false;
//					}
			
					if(((MenuItem)e).Parent is ContextMenu)
					{
                       if(((MenuItem)e).Parent==this)
							return true;
						else
							return false;
					}
					if(((MenuItem)e).Parent is MenuItem)
					{
                      
						mnu=((MenuItem)e).Parent.GetContextMenu();
						if(mnu!=null )
						{
							if(mnu==this)
								return true;
							else
								return false;
						}
					}
					return true;
				}
				if(mnu==this)
					return true;
				else
					return false;
//				if(((MenuItem)e).Parent.GetContextMenu()!=this )
//					return false;
				//return  ((MenuItem)e).GetContextMenu().SourceControl.ContextMenu==this;
			}
			return false;//(e is MenuItem);
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

		#region Items Members

		public MenuItem AddItem(string caption,int imageIndex,EventHandler onclick)
		{
			MenuItem mn= this.MenuItems.Add (caption ,onclick);
			this.SetDraw(mn,true);
			this.SetImageIndex (mn,imageIndex);
			this.SetTag(mn,null);
			return mn;
		}

		public MenuItem AddItem(string caption,ImageList imgList,int imageIndex,EventHandler onclick)
		{
			if(this.m_imageList==null)
			{
				m_imageList=new ImageList();
				//this.Container.Add (this.m_imageList); 
			}
			int indx=this.m_imageList.Images.Count; 
			this.m_imageList.Images.Add (imgList.Images[imageIndex]);  
			MenuItem mn= this.MenuItems.Add (caption ,onclick);
			this.SetDraw(mn,true);
	
			if(this.m_imageList.Images.Count > indx)
			{
				this.SetImageIndex (mn,indx);
			}
			this.SetTag(mn,null);
			return mn;
		}

		public MenuItem AddItem(string caption,object tag,int imageIndex,EventHandler onclick)
		{
			MenuItem mn= this.MenuItems.Add (caption ,onclick);
			this.SetDraw(mn,true);
			SetTag(mn,tag);
			this.SetImageIndex (mn,imageIndex);
			return mn;
		}
		
		public MenuItem AddItem(string caption,object tag,ImageList imgList,int imageIndex,EventHandler onclick)
		{
           MenuItem mn= AddItem(caption,imgList,imageIndex,onclick);
			this.SetDraw(mn,true);
			SetTag(mn,tag);
		   return mn;
		}

		public MenuItem AddItem(MenuItem parent,string caption,int imageIndex,EventHandler onclick)
		{
			MenuItem mn= this.AddItem(caption ,imageIndex,onclick);
			parent.MenuItems.Add(mn);
			return mn;
		}
		public MenuItem AddItem(MenuItem parent,string caption,ImageList imgList,int imageIndex,EventHandler onclick)
		{
			MenuItem mn= this.AddItem(caption ,imgList,imageIndex,onclick);
			parent.MenuItems.Add(mn);
			return mn;
		}
		public MenuItem AddItem(MenuItem parent,string caption,object tag,int imageIndex,EventHandler onclick)
		{
			MenuItem mn= this.AddItem(caption ,tag,imageIndex,onclick);
			parent.MenuItems.Add(mn);
			return mn;
		}
		
		public MenuItem AddItem(MenuItem parent,string caption,object tag,ImageList imgList,int imageIndex,EventHandler onclick)
		{
			MenuItem mn= this.AddItem(caption ,tag,imgList,imageIndex,onclick);
			parent.MenuItems.Add(mn);
			return mn;
		}
		
		public void AddItemRange(MenuItem[] items)
		{
			for(int i=0;i<items.Length;i++)
			{
				int index= base.MenuItems.Add(i,items[i]);
				MenuItem mn=base.MenuItems[index];
				this.SetDraw(mn,true);
				this.SetImageIndex (mn,-1);
				SetTag(mn,null);
			}
		}

		public void RemoveItem(int index)
		{			
     	   RemoveItem(MenuItems[index]);
		}

		public void RemoveItem(MenuItem item)
		{			
			if (item != null) 
			{
				if (this.imageIndexes.Contains(item))
				{
					this.imageIndexes.Remove(item);
				}
				if (hashTags.ContainsKey(item)) 
				{
					hashTags.Remove(item)  ;
				}
				this.SetDraw(item,false);
				this.MenuItems.Remove(item);
			}
		}     

		public void ClearItems()
		{
			foreach(MenuItem mn in this.MenuItems)
				this.SetDraw(mn,false);

			this.imageIndexes.Clear();
			this.hashTags.Clear();
			this.MenuItems.Clear();
		}

		public int IndexOf(MenuItem item)
		{
			return this.MenuItems.IndexOf(item);
		}
		public bool Contains(MenuItem item)
		{
			return this.MenuItems.Contains(item);
		}
		public bool ItemEquals(object item)
		{
			return this.MenuItems.Equals(item);
		}
		public int ItemsCount
		{
			get{return this.MenuItems.Count;}
		}
		public bool IsReadOnly
		{
			get{return this.MenuItems.IsReadOnly;}
		}
		public string ItemsToString()
		{
			return this.MenuItems.ToString();
		}

		public void CopyTo(MenuItem[] dest,int index)
		{
			this.MenuItems.CopyTo(dest,index);
		}

		public System.Collections.IEnumerator GetEnumerator()
		{
			return this.MenuItems.GetEnumerator();
		}


//		internal new System.Windows.Forms.Menu.MenuItemCollection MenuItems
//		{
//			get{return base.MenuItems;}
//		}

		#endregion

		#region MenuProvider Not Used

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
				//SetImageList(menuItem, m_imageList);//imageList);
				SetImageIndex(menuItem, imageIndex);
			}
		}

		//		private ImageList GetImageList(MenuItem control)
		//		{
		//			if (this.imageLists[control] == null)
		//			{
		//				return null;
		//			}
		//			return (ImageList) this.imageLists[control];
		//		}

		//		public void SetImageList(MenuItem control, ImageList value)
		//		{
		//			if (!this.imageLists.Contains(control))
		//			{
		//				this.imageLists.Add(control, value);
		//			}
		//			else
		//			{
		//				this.imageLists[control] = value;
		//			}
		//		}

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
			SizeF ef1;
			MenuItem item1 = (MenuItem) sender;
			Graphics graphics1 = e.Graphics;
			if (((item1.Text.Length >= 4) && (item1.Text.Substring(0, 2) == "--")) && (item1.Text.Substring(item1.Text.Length - 2) == "--"))
			{
				ef1 = graphics1.MeasureString(item1.Text.Substring(2, item1.Text.Length - 4), this.font);
			    e.ItemWidth = (int) ef1.Width;
				if ((itemWidth-18) > e.ItemWidth)
					e.ItemWidth = (int) (itemWidth-18);
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
					if ((itemWidth-18) > e.ItemWidth)
					   e.ItemWidth = (int) (itemWidth-18);
		
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
				if (this.hashTags != null)
				{
					this.hashTags.Clear();
					this.hashTags = null;
				}

			}
			base.Dispose(disposing);
		}

		internal void SetDraw(MenuItem control)
		{
			SetDraw(control,true);
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

 
		//[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design", typeof(System.Drawing.Design.UITypeEditor)), Category("Appearance"), DefaultValue(-1), Localizable(true), TypeConverter(typeof(ImageIndexConverter))]
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

		[Category("Behavior"), DefaultValue(null), Localizable(true)]
		public void SetTag(MenuItem control, object value)
		{
			if (!this.hashTags.Contains(control))
			{
				this.hashTags.Add(control, value);
			}
			else
			{
				this.hashTags[control] = value;
			}
		}

//		public void Redraw()
//		{
//			foreach(MenuItem itm in this.MenuItems)
//			{
//				// SetDraw(itm,false);
//				itm.MeasureItem -= new MeasureItemEventHandler(this.MeasureItem);
//				itm.DrawItem -= new DrawItemEventHandler(this.DrawItem);
//				itm.OwnerDraw = false;
//				//created=false;
//			}
//			foreach(MenuItem itm in this.MenuItems)
//			{
//				SetDraw(itm,true);
//				if(!this.imageIndexes.Contains(itm))
//				    SetImageIndex(itm,-1);
//			}
//		}
//
//		[Category("Behavior"),System.ComponentModel.RefreshProperties(RefreshProperties.Repaint)]
//		public bool RedrawItems
//		{
//			get{return false;} 
//			set{Redraw();}
//		}
	
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
					OnImageListChange(EventArgs.Empty);
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
			LayoutManager.SetStyleLayout(value);
			//if(this.m_StylePainter!=null)
			//	this.m_StylePainter.Layout.SetStyleLayout(value); 
		}

		public virtual void SetStyleLayout(Styles value)
		{
			LayoutManager.SetStyleLayout(value);
			//if(this.m_StylePainter!=null)
			//	m_StylePainter.Layout.SetStyleLayout(value);
		}

		protected virtual void OnStylePainterChanged(EventArgs e)
		{
		}

		protected virtual void OnStylePropertyChanged(PropertyChangedEventArgs e)
		{

		}

		private void m_Style_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			OnStylePropertyChanged(e);
		}

		#endregion

	}

}
