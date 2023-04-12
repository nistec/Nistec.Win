using System;
using System.Data;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Nistec.Win32;
using System.Runtime.InteropServices;


using Nistec.WinForms.Controls;

namespace Nistec.WinForms
{
    
	#region PopUpItem

	//[StructLayout( LayoutKind.Sequential)]
	[DesignTimeVisible(false),ToolboxItem(false)]
	public class PopUpItem : Component
	{
		#region Ctor
		public PopUpItem()
		{
		  imageIndex=-1; 
		  enabled=true;
		  //visible=true;
 		}

		public PopUpItem(McPopUp c)
		{
			imageIndex=-1; 
			enabled=true;
			//visible=true;
			this.ctlPopUp=c;
		}

		public enum MenuItemType
		{
			Button=0,
			Check=1,
			Separator=2
		}

		#endregion

		#region Methods
//		public ImageList GetImageList()
//		{
//          return this.ctlPopUp.ImageList;
//		}

		public void ReplaceChecked()
		{
			this.Checked=!this.Checked;
		}
		#endregion

		#region Members
		private bool isChecked=false;
		private ImageList imageList; 
		private int imageIndex; 
		private string text; 
		private object tag;
        private Image m_Image;
		//private bool visible;
		private bool enabled;
        private bool startGroup = false;

        internal Control owner;
		internal McPopUp ctlPopUp;
		#endregion

		#region Properties
		
		[Category("Appearance"),DefaultValue(false)]  
		public bool Checked 
		{
			get{return isChecked;}
			set{isChecked=value;}
		}

        [Category("Appearance"), DefaultValue(false)]
        public bool StartGroup
        {
            get { return startGroup; }
            set { startGroup = value; }
        }

        [Category("Appearance")]
        [DefaultValue(null)]
        public virtual Image Image
        {
            get { return m_Image; }
            set
            {
                if (m_Image != value)
                {
                    m_Image = value;
                }
            }
        }
		[Description("ImageList"), DefaultValue(null), Category("Appearance")]
		public ImageList ImageList
		{
			get
			{
				return this.imageList;
			}
			set
			{
				this.imageList=value;
			}
		}


 		//[TypeConverter(typeof(ImageIndexConverter)), Editor(typeof(McImageIndexEditor), typeof(System.Drawing.Design.UITypeEditor)), DefaultValue(-1)]
        [Description("ImageIndex"), Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design", typeof(System.Drawing.Design.UITypeEditor)), Category("Appearance"), DefaultValue(-1), Localizable(true), TypeConverter(typeof(ImageIndexConverter))]
        public int ImageIndex
		{
			get
			{
				if (((this.imageIndex != -1) && (this.imageList!= null)) && (this.imageIndex >= imageList.Images.Count))
				{
					return (this.imageList.Images.Count - 1);
				}
				return this.imageIndex;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentException("InvalidLowBoundArgumentEx");
				}
				if (this.imageIndex != value)
				{
					this.imageIndex = value;
				}
			}
		}

		public string Text
		{
			get{return text;}
			set
            {
                if (Text != value)
                {
                    text = value;
                    if(this.ctlPopUp!=null)
                    this.ctlPopUp.recalcWidth = true;
                }
            }
		}

		[Browsable(false),Category("Misc"),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object Tag
		{
			get{return tag;}
			set{tag=value;}
		}

//		[DefaultValue(true),Category("Appearance")]
//		public bool Visible
//		{
//			get{return visible;}
//			set{visible=value;}
//		}
		[DefaultValue(true),Category("Appearance")]
		public bool Enabled
		{
			get{return enabled;}
			set{enabled=value;}
		}

        public string Name
        {
            get { return base.Site==null ? "": base.Site.Name; }
        }	


		#endregion
	}

	#endregion

	#region PopUpItems Collection
		
	public class PopUpItemsCollection : Nistec.Collections.CollectionWithEvents
	{
		internal Control owner;
		internal McPopUp ctlPopUp;
		//internal ImageList imageList;

		public PopUpItemsCollection(){}
		public PopUpItemsCollection(Control ctl)
		{
			this.owner=ctl;
		}
		public PopUpItemsCollection(Control ctl,McPopUp cp)
		{
			this.owner=ctl;
			this.ctlPopUp=cp;
		}

		public PopUpItem Add(PopUpItem value)
		{
			value.owner=this.owner;
			value.ctlPopUp=this.ctlPopUp;
			if(ctlPopUp!=null && this.ctlPopUp.ImageList!=null)
			{
				value.ImageList=this.ctlPopUp.ImageList;
			}
			//value.Text=value.Site.Name;
			base.List.Add(value as object);
			if(ctlPopUp!=null)
			{
				this.ctlPopUp.CalcDropDownWidth(value);
			}
			return value;
		}

		public void AddRange(PopUpItemsCollection values)
		{
			foreach(PopUpItem itm in values)
			{
				Add(itm);
			}
		}

		public void AddRange(PopUpItemsCollection values,Control ctl,McPopUp cp)
		{
			this.ctlPopUp=cp;
			this.owner=ctl;
			foreach(PopUpItem itm in values)
			{
				itm.ctlPopUp=cp;
				itm.owner=ctl;
				Add(itm);
			}
		}

		public void AddRange(PopUpItem[] values)
		{
			foreach(PopUpItem itm in values)
			{
				Add(itm);
			}
		}

		public void AddRange(string[] values)
		{
			for(int i=0;i<values.Length;i++)
			{
				PopUpItem itm=new PopUpItem(ctlPopUp);
				itm.Text=values[i];
				//itm.Index=i;
				if(!this.Contains(itm))
					Add(itm);  
			}
		}

		public void Remove(PopUpItem value)
		{
			base.List.Remove(value as object);
		}

		public void Insert(int index, PopUpItem value)
		{
			value.owner=this.owner;
			value.ctlPopUp=this.ctlPopUp;
			if(ctlPopUp!=null && this.ctlPopUp.ImageList!=null)
			{
				value.ImageList=this.ctlPopUp.ImageList;
			}
			base.List.Insert(index, value as object);
		}

		public bool Contains(PopUpItem value)
		{
			return base.List.Contains(value as object);
		}

		public PopUpItem this[int index]
		{
			get { return (base.List[index] as PopUpItem); }
		}

		public PopUpItem this[string text]
		{
			get 
			{
				// Search for a Page with a matching title
				foreach(PopUpItem itm in base.List)
					if (itm.Text  == text)
						return itm;
				return null;
			}
		}

		public int IndexOf(PopUpItem value)
		{
			return base.List.IndexOf(value);
		}

		//			public PopUpItem Add(string text)
		//			{
		//				PopUpItem item = new PopUpItem();
		//				item.Text =text;
		//				return Add(item);
		//			}
		
		public void CopyTo(PopUpItem[] array, System.Int32 index)
		{
			PopUpItem[] itms=new PopUpItem[this.Count];
			int i=0;
			foreach (PopUpItem obj in base.List)
			{
				array.SetValue(obj,i);
				i++;
			}
		}

		public void CopyTo(object[] array, System.Int32 index)
		{
			object[] itms=new object[this.Count];
			int i=0;
			foreach (PopUpItem obj in base.List)
			{
				array.SetValue(obj.Text,i);
				i++;
			}
		}

		public void CopyTo(McListItems.ObjectCollection array)
		{
			foreach (PopUpItem obj in base.List)
			{
				if(obj.Text!=null)
				{
					array.Add(obj.Text);
				}
			}
		}

		#region AddProperty

		public PopUpItem AddItem(string Text)
		{
			return AddItem(Text,null, -1,true);//,true);
		}
		public PopUpItem AddItem(string Text,bool enabled)//,bool Visible)
		{
			return AddItem(Text,null, -1,enabled);//,Visible);
		}
		public PopUpItem AddItem(string Text,int imageIndex)
		{
			return AddItem(Text,null, imageIndex,true);//,true);
		}
		public PopUpItem AddItem(string Text,object tag, int imageIndex)
		{
			return AddItem(Text,tag, imageIndex,true);//,true);
		}

		public PopUpItem AddItem(string Text,int imageIndex,bool enabled)//,bool Visible)
		{
			return AddItem(Text,null, imageIndex,enabled);//,Visible);
		}

        //public PopUpItem AddItem(string Text, int imageIndex, bool enabled,bool startGroup)
        //{
        //    return AddItem(Text, null, imageIndex, enabled,startGroup);
        //}

		public PopUpItem AddItem(string Text,object tag, int imageIndex,bool enabled)//,bool Visible)
		{
            return AddItem(Text, null, imageIndex, enabled,false);
        }

        public PopUpItem AddItem(string Text, object tag, int imageIndex, bool enabled,bool startGroup)
        {
            PopUpItem item = new PopUpItem(ctlPopUp);
            int indx = this.Count;
            item.Text = Text;
            item.Tag = tag;
            item.StartGroup = startGroup;
            if (ctlPopUp != null && this.ctlPopUp.ImageList != null)
            {
                item.ImageList = this.ctlPopUp.ImageList;
                item.ImageIndex = imageIndex;
            }
            item.Enabled = enabled;
            //item.Visible  =Visible;
            //item.owner=this.owner;
            return Add(item);
        }
		#endregion
	}
	#endregion

}