
namespace Nistec.WinForms.Controls
{

	using System;
	using System.Windows.Forms;
	using System.ComponentModel;
	using System.Drawing;
	using System.Collections;
	using System.Runtime.InteropServices;
	using System.Security;
	using System.Security.Permissions;
	using System.Globalization;
	using System.Data;
	using System.Reflection;

	using Nistec.Win32;
	
	using Nistec.Drawing;


	
	[ToolboxItem(false),DefaultProperty("Items"), DefaultEvent("SelectedIndexChanged")]
	public class McListItems : ListControl//,IMcList
	{

		#region Members
		// Fields
		private BorderStyle borderStyle;
		//private static bool checkedOS;
		private int columnWidth;
		public const int DefaultItemHeight = 13;
		private bool doubleClickFired;
		private DrawMode drawMode;
		private static readonly object EVENT_DRAWITEM;
		private static readonly object EVENT_MEASUREITEM;
		private static readonly object EVENT_SELECTEDINDEXCHANGED;
		private bool fontIsChanged;
		private int horizontalExtent;
		private bool horizontalScrollbar;
		private bool integralHeight;
		private bool integralHeightAdjust;
		private int itemHeight;
		private ObjectCollection itemsCollection;
		private int maxWidth;
		private bool multiColumn;
		public const int NoMatches = -1;
		private int requestedHeight;
		//private static bool runningOnWin2K;
		//private bool scrollAlwaysVisible;
		private bool selectedValueChangedFired;
		private bool sorted;
		private int topIndex;
		private int updateCount;
		private bool useTabStops;

		private bool hotTrack;
		//private ImageList imageList;
		//private bool useFirstImage;
		//private DrawItemStyle drawItemStyle;

		//internal ILayout ownerMc ;
		internal int padding;

		public event EventHandler SelectionChanged;

		#endregion

		#region Constructor
		static McListItems()
		{
			McListItems.EVENT_SELECTEDINDEXCHANGED = new object();
			McListItems.EVENT_DRAWITEM = new object();
			McListItems.EVENT_MEASUREITEM = new object();
			//McListItems.checkedOS = false;
			//McListItems.runningOnWin2K = true;
		}

        public McListItems()
		{
			this.hotTrack=true;
			this.itemHeight = 13;
			this.horizontalExtent = 0;
			this.maxWidth = -1;
			this.updateCount = 0;
			this.sorted = false;
			//this.scrollAlwaysVisible = false;
			this.integralHeight = true;
			this.integralHeightAdjust = false;
			this.multiColumn = false;
			this.horizontalScrollbar = false;
			this.useTabStops = true;
			this.fontIsChanged = false;
			this.doubleClickFired = false;
			this.selectedValueChangedFired = false;
			this.drawMode = DrawMode.Normal;
			this.borderStyle = BorderStyle.None;//Fixed3D;
			base.SetStyle(ControlStyles.UserPaint, false);
			base.SetStyle(ControlStyles.StandardClick, false);
			base.SetBounds(0, 0, 120, 0x60);
			this.requestedHeight = base.Height;
		}

		#endregion

		#region InternalMethods

		#region Internal OPtional

		//		internal Form form;
		//
		//		[Browsable(false)]
		//		internal virtual McListItems InternalList
		//		{
		//			get{return this;}
		//		}
		//
		//		internal event	EventHandler SelectedInternalChanged;
		//		//internal int selectedIndexInternal =-1;
		//		
		//		internal void OnSelectedInternalChanged(EventArgs e)
		//		{
		//			if (SelectedInternalChanged != null)
		//			{
		//				SelectedInternalChanged(this, e);
		//			}
		//		}
		#endregion

		internal bool BindingFieldEmpty
		{
			get
			{
				if (this.DisplayMember.Length <= 0)
				{
					return true;
				}
				return false;
			}
		}
 
		internal object FilterItemOnPropertyInternal(object value)
		{
			return base.FilterItemOnProperty(value);
		}

		internal Graphics CreateGraphicsInternal()
		{
			return Graphics.FromHwndInternal(this.Handle);
		}


		internal IntPtr SendMessage(int msg, int wparam, int lparam)
		{
			return Win32.WinAPI.SendMessage(new HandleRef(this, this.Handle), msg, wparam, lparam);
		}

		internal IntPtr SendMessage(int msg, int wparam, ref Win32.RECT lparam)
		{
			return Win32.WinAPI.SendMessage(new HandleRef(this, this.Handle), msg, wparam, ref lparam);
		}

		internal IntPtr SendMessage(int msg, IntPtr wparam, IntPtr lparam)
		{
			return Win32.WinAPI.SendMessage(new HandleRef(this, this.Handle), msg, wparam, lparam);
		}

		internal IntPtr SendMessage(int msg, int wparam, string lparam)
		{
			return Win32.WinAPI.SendMessage(new HandleRef(this, this.Handle), msg, wparam, lparam);
		}

		internal void BeginUpdateInternal()
		{
			if (this.IsHandleCreated)
			{
				if (this.updateCount == 0)
				{
					this.SendMessage(11, 0, 0);
				}
				this.updateCount = (short) (this.updateCount + 1);
			}
		}

		internal bool EndUpdateInternal(bool invalidate)
		{
			if (this.updateCount <= 0)
			{
				return false;
			}
			this.updateCount = (short) (this.updateCount - 1);
			if (this.updateCount == 0)
			{
				this.SendMessage(11, -1, 0);
				if (invalidate)
				{
					this.Invalidate();
				}
			}
			return true;
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal virtual bool ShouldSerializeBackColor()
		{
			return !base.BackColor.IsEmpty;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		internal virtual bool ShouldSerializeForeColor()
		{
			return !base.ForeColor.IsEmpty;
		}

		#endregion

		#region Methods

		protected virtual void AddItemsCore(object[] value)
		{
			if (((value == null) ? 0 : value.Length) != 0)
			{
				this.Items.AddRangeInternal(value);
			}
		}

		public void BeginUpdate()
		{
			BeginUpdateInternal();
			this.updateCount++;
		}

		private void CheckIndex(int index)
		{
			if ((index < 0) || (index >= this.Items.Count))
			{
				throw new ArgumentOutOfRangeException("ArgumentOutOfRangeException");//SR.GetString("IndexOutOfRange", new object[] { index.ToString() }));
			}
		}

 
		private void CheckNoDataSource()
		{
            /*sort*/
            //if (base.DataSource != null)
            //{
            //    throw new ArgumentException("DataSourceLocksItems");
            //}
		}

		internal virtual int ComputeMaxItemWidth(int oldMax)
		{
			int num1 = oldMax;
			IntPtr ptr1 = Win32.WinAPI.GetDC(new HandleRef(this, base.Handle));
			IntPtr ptr2 = Win32.WinAPI.SelectObject(new HandleRef(this, ptr1), new HandleRef(this,Nistec.Win32.WinMethods.GetFontHandle( base.Font)));
			try
			{
				Win32.RECT rect1 = new Win32.RECT();
				foreach (object obj1 in this.Items)
				{
					Win32.WinAPI.DrawText((IntPtr)new HandleRef(this, ptr1), obj1.ToString(), obj1.ToString().Length, ref rect1, 0x400);
					int num2 = rect1.right - rect1.left;
					if (num2 > num1)
					{
						num1 = num2;
					}
				}
				return num1;
			}
			finally
			{
				Win32.WinAPI.SelectObject(new HandleRef(this, ptr1), new HandleRef(this, ptr2));
				Win32.WinAPI.ReleaseDC(new HandleRef(this, base.Handle), new HandleRef(this, ptr1));
			}
//			if(this.imageList!=null)
//			{
//              return num1  +  this.imageList.ImageSize.Width + padding;
//			}
//			return num1 + padding;
		}

		protected virtual McListItems.ObjectCollection CreateItemCollection()
		{
			return new McListItems.ObjectCollection(this);
		}

 
		public void EndUpdate()
		{
			this.EndUpdateInternal(true);
			this.updateCount--;
			int num1 = (this.itemsCollection == null) ? 0 : this.itemsCollection.Count;
			if (((this.updateCount == 0) && this.sorted) && (num1 > 1))
			{
				this.Sort();
			}
		}

		public int FindString(string s)
		{
			return this.FindString(s, -1);
		}

 
		public int FindString(string s, int startIndex)
		{
			if (s == null)
			{
				return -1;
			}
			int num1 = (this.itemsCollection == null) ? 0 : this.itemsCollection.Count;
			if (num1 == 0)
			{
				return -1;
			}
			if ((startIndex < -1) || (startIndex >= (num1 - 1)))
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			return this.FindStringInternal(s, this.Items, startIndex, false);
		}

		public int FindStringExact(string s)
		{
			return this.FindStringExact(s, -1);
		}

		public int FindStringExact(string s, int startIndex)
		{
			if (s == null)
			{
				return -1;
			}
			int num1 = (this.itemsCollection == null) ? 0 : this.itemsCollection.Count;
			if (num1 == 0)
			{
				return -1;
			}
			if ((startIndex < -1) || (startIndex >= (num1 - 1)))
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			return FindStringInternal(s, this.Items, startIndex, true);
		}

		internal int FindStringInternal(string str, IList items, int startIndex, bool exact)
		{
			if ((str == null) || (items == null))
			{
				return -1;
			}
			if ((startIndex < -1) || (startIndex >= (items.Count - 1)))
			{
				return -1;
			}
			bool flag1 = false;
			int num1 = str.Length;
			int num2 = startIndex;
			while (true)
			{
				num2++;
				if (exact)
				{
					flag1 = string.Compare(str, this.GetItemText(items[num2]), true, CultureInfo.CurrentCulture) == 0;
				}
				else
				{
					flag1 = string.Compare(str, 0, this.GetItemText(items[num2]), 0, num1, true, CultureInfo.CurrentCulture) == 0;
				}
				if (flag1)
				{
					return num2;
				}
				if (num2 == (items.Count - 1))
				{
					num2 = -1;
				}
				if (num2 == startIndex)
				{
					return -1;
				}
			}
		}
 
		public int GetItemHeight(int index)
		{
			int num1 = (this.itemsCollection == null) ? 0 : this.itemsCollection.Count;
			if ((index < 0) || ((index > 0) && (index >= num1)))
			{
				throw new ArgumentOutOfRangeException("ArgumentOutOfRangeException");//SR.GetString("InvalidArgument", new object[] { "index", index.ToString() }));
			}
			if (this.drawMode != DrawMode.OwnerDrawVariable)
			{
				index = 0;
			}
			if (!base.IsHandleCreated)
			{
				return this.itemHeight;
			}
			int num2 = (int) SendMessage(0x1a1, index, 0);
			if (num2 == -1)
			{
				throw new Win32Exception();
			}
			return num2;
		}

		public Rectangle GetItemRectangle(int index)
		{
			this.CheckIndex(index);
			Win32.RECT rect1 = new Win32.RECT();
			SendMessage(0x198, index, ref rect1);
			return Rectangle.FromLTRB(rect1.left, rect1.top, rect1.right, rect1.bottom);
		}

		public bool GetSelected(int index)
		{
			this.CheckIndex(index);
			return this.GetSelectedInternal(index);
		}

		private bool GetSelectedInternal(int index)
		{
			if (base.IsHandleCreated)
			{
				int num1 = (int) SendMessage(0x187, index, 0);
				if (num1 == -1)
				{
					throw new Win32Exception();
				}
				return (num1 > 0);
			}
			if (this.itemsCollection != null) //&& this.SelectedItems.GetSelected(index))
			{
				return true;
			}
			return false;
		}

		public int IndexFromPoint(Point p)
		{
			return this.IndexFromPoint(p.X, p.Y);
		}

		public int IndexFromPoint(int x, int y)
		{
			Win32.RECT rect1 = new Win32.RECT();
			Win32.WinAPI.GetClientRect((IntPtr)new HandleRef(this, base.Handle), ref rect1);
			if (((rect1.left <= x) && (x < rect1.right)) && ((rect1.top <= y) && (y < rect1.bottom)))
			{
				int num1 = (int) SendMessage(0x1a9, 0, (int) Win32.WinAPI.MAKELPARAM(x, y));
				if (Win32.WinAPI.HIWORD(num1) == 0)
				{
					return Win32.WinAPI.LOWORD(num1);
				}
			}
			return -1;
		}

		#endregion

		#region Native Methods
 
		private int NativeAdd(object item)
		{
			int num1 = (int) this.SendMessage( 0x180, 0, base.GetItemText(item));
			if (num1 == -2)
			{
				throw new OutOfMemoryException();
			}
			if (num1 == -1)
			{
				throw new OutOfMemoryException("ListBoxItemOverflow");
			}
			return num1;
		}

 
		private void NativeClear()
		{
			this.SendMessage(0x184, 0, 0);
		}

		private int NativeInsert(int index, object item)
		{
			int num1 = (int) Win32.WinAPI.SendMessage(new HandleRef(this, this.Handle), 0x181, index, base.GetItemText(item));
			if (num1 == -2)
			{
				throw new OutOfMemoryException();
			}
			if (num1 == -1)
			{
				throw new OutOfMemoryException("ListBoxItemOverflow");
			}
			return num1;
		}

 
		private void NativeRemoveAt(int index)
		{
			bool flag1 = ((int) Win32.WinAPI.SendMessage(new HandleRef(this, this.Handle), 0x187, (IntPtr) index, IntPtr.Zero)) > 0;
			SendMessage(0x182, index, 0);
			if (flag1)
			{
				this.OnSelectedIndexChanged(EventArgs.Empty);
			}
		}

		private void NativeSetSelected(int index, bool value)
		{
			SendMessage(390, value ? index : -1, 0);
		}

		private void NativeUpdateSelection()
		{
			int num1 = this.Items.Count;
			int[] numArray1 = null;
			int num3 = (int) SendMessage(0x188, 0, 0);
			if (num3 >= 0)
			{
				numArray1 = new int[] { num3 };
			}
		}

		#endregion

		#region override

		protected override void OnChangeUICues(UICuesEventArgs e)
		{
			base.Invalidate();
			base.OnChangeUICues(e);
		}
 
		protected override void OnDataSourceChanged(EventArgs e)
		{
			if (base.DataSource == null)
			{
				this.BeginUpdate();
				this.SelectedIndex = -1;
				this.Items.ClearInternal();
				this.EndUpdate();
			}
			base.OnDataSourceChanged(e);
			this.RefreshItems();
		}
 
		protected override void OnDisplayMemberChanged(EventArgs e)
		{
			base.OnDisplayMemberChanged(e);
			if ((base.DataManager != null) && base.IsHandleCreated)
			{
				this.RefreshItems();
				this.SelectedIndex = base.DataManager.Position;
			}
		}
 
		protected virtual void OnDrawItem(DrawItemEventArgs e)
		{
			DrawItemEventHandler handler1 = (DrawItemEventHandler) base.Events[McListItems.EVENT_DRAWITEM];
			if (handler1 != null)
			{
				handler1(this, e);
			}
			//if (this.DrawMode == DrawMode.OwnerDrawFixed || this.DrawMode == DrawMode.OwnerDrawVariable)
			//{
				//OnDrawItemInternal(e); 
			//}

			//OnDrawItemInternal(e);
		}
 
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.fontIsChanged = true;
			this.integralHeightAdjust = true;
			try
			{
				base.Height = this.requestedHeight;
			}
			finally
			{
				this.integralHeightAdjust = false;
			}
			this.maxWidth = -1;
			this.UpdateHorizontalExtent();
		}
 
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			this.SendMessage(0x1a5, CultureInfo.CurrentCulture.LCID, 0);
			if (this.columnWidth != 0)
			{
				this.SendMessage(0x195, this.columnWidth, 0);
			}
			if (this.drawMode == DrawMode.OwnerDrawFixed)
			{
				this.SendMessage(0x1a0, 0, this.ItemHeight);
			}
			if (this.topIndex != 0)
			{
				this.SendMessage(0x197, this.topIndex, 0);
			}
			if (this.itemsCollection != null)
			{
				int num1 = this.itemsCollection.Count;
				for (int num2 = 0; num2 < num1; num2++)
				{
					this.NativeAdd(this.itemsCollection[num2]);
				}
			}
			this.UpdateHorizontalExtent();
		}
 
		protected override void OnHandleDestroyed(EventArgs e)
		{
			if (base.Disposing)
			{
				this.itemsCollection = null;
			}
			base.OnHandleDestroyed(e);
		}

		protected virtual void OnMeasureItem(MeasureItemEventArgs e)
		{
			MeasureItemEventHandler handler1 = (MeasureItemEventHandler) base.Events[McListItems.EVENT_MEASUREITEM];
			if (handler1 != null)
			{
				handler1(this, e);
			}
		}
 
		protected override void OnParentChanged(EventArgs e)
		{
			base.OnParentChanged(e);
			if (this.Parent != null)
			{
				base.RecreateHandle();
			}
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			if (this.RightToLeft == RightToLeft.Yes)
			{
				base.Invalidate();
			}
		}

		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			base.OnSelectedIndexChanged(e);
			EventHandler handler1 = (EventHandler) base.Events[McListItems.EVENT_SELECTEDINDEXCHANGED];
			if (handler1 != null)
			{
				handler1(this, e);
			}
			if ((base.DataManager != null) && (base.DataManager.Position != this.SelectedIndex))
			{
				base.DataManager.Position = this.SelectedIndex;
			}
		}

		protected override void OnSelectedValueChanged(EventArgs e)
		{
			base.OnSelectedValueChanged(e);
			this.selectedValueChangedFired = true;
		}

		public override void Refresh()
		{
			if (this.drawMode == DrawMode.OwnerDrawVariable)
			{
				int num1 = this.Items.Count;
				Graphics graphics1 = CreateGraphicsInternal();
				try
				{
					for (int num2 = 0; num2 < num1; num2++)
					{
						MeasureItemEventArgs args1 = new MeasureItemEventArgs(graphics1, num2, this.ItemHeight);
						this.OnMeasureItem(args1);
					}
				}
				finally
				{
					graphics1.Dispose();
				}
			}
			base.Refresh();
		}

 
		protected override void RefreshItem(int index)
		{
			this.Items.SetItemInternal(index, this.Items[index]);
		}

		private new void RefreshItems()
		{
			McListItems.ObjectCollection collection1 = this.itemsCollection;
			this.itemsCollection = null;
			if (base.IsHandleCreated)
			{
				this.NativeClear();
			}
			object[] objArray1 = null;
			if ((base.DataManager != null) && (base.DataManager.Count != -1))
			{
				objArray1 = new object[base.DataManager.Count];
				for (int num1 = 0; num1 < objArray1.Length; num1++)
				{
					objArray1[num1] = base.DataManager.List[num1];
				}
			}
			else if (collection1 != null)
			{
				objArray1 = new object[collection1.Count];
				collection1.CopyTo(objArray1, 0);
			}
			if (objArray1 != null)
			{
				this.Items.AddRangeInternal(objArray1);
			}
			if (base.DataManager != null) 
			{
				this.SelectedIndex = base.DataManager.Position;
			}
			else if (collection1 != null)
			{
				int num2 = collection1.Count;
				for (int num3 = 0; num3 < num2; num3++)
				{
					//if (collection1.InnerArray.GetState(num3, McListItems.SelectedObjectCollection.SelectedObjectMask))
					//{
					this.SelectedItem = collection1[num3];
					//}
				}
			}
		}
 
		private void ResetItemHeight()
		{
			this.itemHeight = 13;
		}

		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			if (!this.integralHeightAdjust && (height != base.Height))
			{
				this.requestedHeight = height;
			}
			base.SetBoundsCore(x, y, width, height, specified);
		}

		protected override void SetItemCore(int index, object value)
		{
			this.Items.SetItemInternal(index, value);
		}

		protected override void SetItemsCore(IList value)
		{
			this.BeginUpdate();
			this.Items.ClearInternal();
			this.Items.AddRangeInternal(value);
			if (base.DataManager != null)
			{
				this.SendMessage(390, base.DataManager.Position, 0);
				if (!this.selectedValueChangedFired)
				{
					this.OnSelectedValueChanged(EventArgs.Empty);
					this.selectedValueChangedFired = false;
				}
			}
			this.EndUpdate();
		}

 
		public void SetSelected(int index, bool value)
		{
			int num1 = (this.itemsCollection == null) ? 0 : this.itemsCollection.Count;
			if ((index < 0) || (index >= num1))
			{
				throw new ArgumentOutOfRangeException("ArgumentOutOfRangeException");//SR.GetString("InvalidArgument", new object[] { "index", index.ToString() }));
			}
			if (base.IsHandleCreated)
			{
				this.NativeSetSelected(index, value);
			}
			this.OnSelectedIndexChanged(EventArgs.Empty);
		}

		protected virtual void Sort()
		{
			this.CheckNoDataSource();
			if (this.sorted && (this.itemsCollection != null))
			{
				this.itemsCollection.InnerArray.Sort();
				if (base.IsHandleCreated)
				{
					this.NativeClear();
					int num1 = this.itemsCollection.Count;
					for (int num2 = 0; num2 < num1; num2++)
					{
						this.NativeAdd(this.itemsCollection[num2]);
						//if (collection1.GetSelected(num2))
						//{
						//	this.NativeSetSelected(num2, true);
						//}
					}
				}
			}
		}

		public override string ToString()
		{
			string text1 = base.ToString();
			if (this.itemsCollection != null)
			{
				text1 = text1 + ", Items.Count: " + this.Items.Count.ToString();
				if (this.Items.Count > 0)
				{
					string text2 = base.GetItemText(this.Items[0]);
					string text3 = (text2.Length > 40) ? text2.Substring(0, 40) : text2;
					text1 = text1 + ", Items[0]: " + text3;
				}
			}
			return text1;
		}

		private void UpdateHorizontalExtent()
		{
			if ((!this.multiColumn && this.horizontalScrollbar) && base.IsHandleCreated)
			{
				int num1 = this.horizontalExtent;
				if (num1 == 0)
				{
					num1 = this.MaxItemWidth;
				}
				this.SendMessage(0x194, num1, 0);
			}
		}

		private void UpdateMaxItemWidth(object item, bool removing)
		{
			if (!this.horizontalScrollbar || (this.horizontalExtent > 0))
			{
				this.maxWidth = -1;
			}
			else if (this.maxWidth > -1)
			{
				int num1;
				using (Graphics graphics1 = CreateGraphicsInternal())
				{
					SizeF ef1 = graphics1.MeasureString(item.ToString(), this.Font);
					num1 = (int) Math.Ceiling((double) ef1.Width);
				}
				if (removing)
				{
					if (num1 < this.maxWidth)
					{
						return;
					}
					this.maxWidth = -1;
				}
				else if (num1 > this.maxWidth)
				{
					this.maxWidth = num1;
				}
			}
		}

		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode=true), SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode=true)]
		protected virtual void WmReflectCommand(ref Message m)
		{
			switch ((((int) m.WParam) >> 0x10))
			{
				case 1:
				{
					this.OnSelectedIndexChanged(EventArgs.Empty);
					return;
				}
				case 2:
				{
					return;
				}
			}
		}

		private void WmReflectDrawItem(ref Message m)
		{
			Win32.WinAPI.DRAWITEMSTRUCT drawitemstruct1 = (Win32.WinAPI.DRAWITEMSTRUCT) m.GetLParam(typeof(Win32.WinAPI.DRAWITEMSTRUCT));
			IntPtr ptr1 = drawitemstruct1.hDC;
			IntPtr ptr2 = Win32.WinAPI.SetUpPalette(ptr1, false, false);
			try
			{
				Graphics graphics1 = Graphics.FromHdcInternal(ptr1);
				try
				{
					Rectangle rectangle1 = Rectangle.FromLTRB(drawitemstruct1.rcItem.left, drawitemstruct1.rcItem.top, drawitemstruct1.rcItem.right, drawitemstruct1.rcItem.bottom);
					if (this.HorizontalScrollbar)
					{
						if (this.MultiColumn)
						{
							rectangle1.Width = Math.Max(this.ColumnWidth, rectangle1.Width);
						}
						else
						{
							rectangle1.Width = Math.Max(this.MaxItemWidth, rectangle1.Width);
						}
					}
					this.OnDrawItem(new DrawItemEventArgs(graphics1, this.Font, rectangle1, drawitemstruct1.itemID, (DrawItemState) drawitemstruct1.itemState, this.ForeColor, this.BackColor));
				}
				finally
				{
					graphics1.Dispose();
				}
			}
			finally
			{
				if (ptr2 != IntPtr.Zero)
				{
					Win32.WinAPI.SelectPalette(new HandleRef(null, ptr1), new HandleRef(null, ptr2), 0);
				}
			}
			m.Result = (IntPtr) 1;
		}

		private void WmReflectMeasureItem(ref Message m)
		{
			Win32.WinAPI.MEASUREITEMSTRUCT measureitemstruct1 = (Win32.WinAPI.MEASUREITEMSTRUCT) m.GetLParam(typeof(Win32.WinAPI.MEASUREITEMSTRUCT));
			if ((this.drawMode == DrawMode.OwnerDrawVariable) && (measureitemstruct1.itemID >= 0))
			{
				Graphics graphics1 = Graphics.FromHwndInternal(this.Handle);// base.CreateGraphicsInternal();
				MeasureItemEventArgs args1 = new MeasureItemEventArgs(graphics1, measureitemstruct1.itemID, this.ItemHeight);
				try
				{
					this.OnMeasureItem(args1);
					measureitemstruct1.itemHeight = args1.ItemHeight;
					goto Label_006A;
				}
				finally
				{
					graphics1.Dispose();
				}
			}
			measureitemstruct1.itemHeight = this.ItemHeight;
			Label_006A:
				Marshal.StructureToPtr(measureitemstruct1, m.LParam, false);
			m.Result = (IntPtr) 1;
		}

//		public void PostMessage(ref Message m)
//		{
//			base.WndProc(ref m);
//		}

		internal protected virtual void OnSelectionChanged(EventArgs e)
		{
			if(SelectionChanged!=null)
				SelectionChanged(this,e);
		}

		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode=true)]
		protected override void WndProc(ref Message m)
		{

			if(m.Msg == (int)Msgs.WM_LBUTTONUP)
			{
				OnSelectionChanged(EventArgs.Empty);
				return;
			}

			if(m.Msg == (int)Msgs.WM_LBUTTONDOWN)
			{
				return;
			}

			if(m.Msg == (int)Msgs.WM_MBUTTONDOWN)
			{
				return;
			}

			//base.WndProc(ref m);
		

//			if(m.Msg == (int)Msgs.WM_LBUTTONUP)
//			{
//				//ComboPopUp frm = (ComboPopUp)this.FindForm();
//				
//				object pitem = null;
//				if (SelectedItem is DataRowView) 
//				{
//					pitem = ((DataRowView)SelectedItem).Row[this.DisplayMember.Substring(this.DisplayMember.IndexOf(".")+1)];
//					object obj = ((DataRowView)SelectedItem).Row[this.ValueMember.Substring(this.ValueMember.IndexOf(".")+1)];
//					if (obj !=null) 
//					{
//						this.SelectedValue = obj;
//						int index = this.FindStringExact((string)pitem);
//						if (index > -1)
//						{
//							this.SelectedIndex=index;// ((ICombo)frm.Parent).SelectedIndex = index;
//							OnSelectedInternalChanged(EventArgs.Empty);
//						}
//						this.Text=(string)pitem.ToString();
//						//OnSelectedInternalChanged(EventArgs.Empty);
//					}
//				}
//				else if (SelectedItem is string) 
//				{
//					pitem = (string)SelectedItem.ToString();
//					int index = this.FindStringExact((string)pitem);
//					if (index > -1)
//					{
//						this.SelectedIndex=index;// ((ICombo)frm.Parent).SelectedIndex = index;
//						OnSelectedInternalChanged(EventArgs.Empty);
//					}
//				}
//	            if(form!=null)
//				   form.Close();
//				return;
//			}
//
//			if(m.Msg == (int)Msgs.WM_LBUTTONDOWN)
//			{
//				return;
//			}
//
//			if(m.Msg == (int)Msgs.WM_MBUTTONDOWN)
//			{
//				return;
//			}

			int num3 = m.Msg;

			if (num3 <= 0x203)
			{
				if (num3 == 0x47)
				{
					base.WndProc(ref m);
					if (this.integralHeight && this.fontIsChanged)
					{
						base.Height = Math.Max(base.Height, this.ItemHeight);
						this.fontIsChanged = false;
					}
					return;
				}
				switch (num3)
				{
					case 0x201:
					{
						base.WndProc(ref m);
						return;
					}
					case 0x202:
					{
						int num1 = (short) ((int) m.LParam);
						int num2 = ((int) m.LParam) >> 0x10;
						Point point1 = new Point(num1, num2);
						point1 = base.PointToScreen(point1);
						if (base.Capture && (Win32.WinAPI.WindowFromPoint(point1.X, point1.Y) == base.Handle))
						{
							if (!this.doubleClickFired)// && !base.ValidationCancelled)
							{
								this.OnClick(EventArgs.Empty);
							}
							else
							{
								this.doubleClickFired = false;
								//if (!base.ValidationCancelled)
								//{
								this.OnDoubleClick(EventArgs.Empty);
								//}
							}
						}
						base.WndProc(ref m);
						this.doubleClickFired = false;
						return;
					}
					case 0x203:
					{
						this.doubleClickFired = true;
						base.WndProc(ref m);
						return;
					}
				}
			}
			else
			{
				switch (num3)
				{
					case 0x202b:
					{
						this.WmReflectDrawItem(ref m);
						return;
					}
					case 0x202c:
					{
						this.WmReflectMeasureItem(ref m);
						return;
					}
					default:
					{
						if (num3 != 0x2111)
						{
							goto Label_016E;
						}
						this.WmReflectCommand(ref m);
						return;
					}
				}
			}
			Label_016E:
				base.WndProc(ref m);
		}

		#endregion

		#region DrawItems

		private DrawItemEventHandler GetDrawItemHandler()
		{
			FieldInfo info1 = typeof(System.Windows.Forms.ListBox).GetField("EVENT_DRAWITEM", BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Static);
			return (DrawItemEventHandler) base.Events[info1.GetValue(null)];
		}

//		protected virtual void OnDrawItemInternal(DrawItemEventArgs e)
//		{
//			if(ownerMc !=null)
//			{
//				DrawItemEventHandler handler1 = this.GetDrawItemHandler();
//				if (handler1 == null)
//				{
//					Graphics graphics1 = e.Graphics;
//					Rectangle rectangle1 = e.Bounds;
//					if ((e.State & DrawItemState.Selected) > DrawItemState.None)
//					{
//						rectangle1.Width--;
//					}
//					DrawItemState state1 = e.State;
//					if ((e.Index != -1) && (this.Items.Count > 0))
//					{
//						int num1 = this.UseFirstImage ? 0 : e.Index;
//					
//						ownerMc.LayoutManager.DrawItem(graphics1, rectangle1,this, state1, this.GetItemText(this.Items[e.Index]),num1);
//					}
//				}
//			
//				else
//				{
//					handler1(this, e);
//				}
//			}
//		}

		protected virtual void OnHotTrack(MouseEventArgs e)
		{
			Point point1 = base.PointToClient(Cursor.Position);
			for (int num1 = 0; num1 < this.Items.Count; num1++)
			{
				Rectangle rectangle1 = this.GetItemRectangle(num1);
				if (rectangle1.Contains(point1))
				{
					this.SelectedIndex = num1;
					return;
				}
			}
		}

//		protected override void OnDrawItem(DrawItemEventArgs e)
//		{
//			if (this.DrawMode == DrawMode.OwnerDrawFixed || this.DrawMode == DrawMode.OwnerDrawVariable)
//			{
//				OnDrawItemInternal(e); 
//			}
//		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (this.HotTrack)
			{
				OnHotTrack(e);
			}
		}
 
		protected override void OnSystemColorsChanged(EventArgs e)
		{
			base.OnSystemColorsChanged(e);
			//McColors.InitColors();
		}


        protected override void OnMouseWheel(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            //if (this.ReadOnly)// && this.DropDownStyle == ComboBoxStyle.Simple)
            //{
            //    return;
            //}

            if (Items.Count <= 0)
                return;

            int CurrentInt = SelectedIndex;
            int Delta = e.Delta * SystemInformation.MouseWheelScrollLines / 120;

            if (CurrentInt + Delta >= Items.Count)
                SelectedIndex = Items.Count - 1;
            else if (CurrentInt + Delta < 0)
                SelectedIndex = 0;
            else
                SelectedIndex += Delta;

            //this.Text =Items[SelectedIndex].ToString (); 
            //this.SelectAll(); 
        }

		#endregion

		#region DrawItems

		//TODO:Check this

		//		[UseApiElements("ShowScrollBar")]
		//		protected virtual void DrawItemInternal(DrawItemEventArgs e)
		//		{
		//			if (this.DrawMode == DrawMode.OwnerDrawFixed || this.DrawMode == DrawMode.OwnerDrawVariable)
		//			{
		//				//WinAPI.ShowScrollBar(this.Handle, (int)ScrollBarTypes.SB_BOTH, 0);
		//
		//				// Draw the background of the McListBox control for each item.
		//				e.DrawBackground();
		//				// Create a new Brush and initialize to a Black colored brush by default.
		//				Brush brush = Brushes.Black;
		//				// Draw the current item text based on the current Font and the custom brush settings.
		//				e.Graphics.DrawString(Items[e.Index].ToString(), e.Font, brush,e.Bounds,StringFormat.GenericDefault);
		//				// If the McListBox has focus, draw a focus rectangle around the selected item.
		//				e.DrawFocusRectangle();
		//
		//				//bool selected = (e.State & DrawItemState.Selected) > 0;
		//				//if ( e.Index != -1 && this.Items.Count > 0)
		//				//		DrawListBoxItem(e.Graphics, e.Bounds, e.Index, selected);
		//				
		//			}
		//
		//		}
		//
		//		private  void DrawListBoxItem(Graphics g, Rectangle bounds, int index, bool selected)
		//		{
		//			if (index != -1)
		//			{
		//				if ( selected && Enabled )
		//				{
		//					using ( Brush b = new SolidBrush(m_SelectionColor) )
		//					{
		//						g.FillRectangle(b, bounds.Left, bounds.Top, bounds.Width, bounds.Height);
		//					}
		//					using ( Pen p = new Pen(m_SelectionBorderColor) )
		//					{
		//						g.DrawRectangle(p, bounds.Left, bounds.Top, bounds.Width-1, bounds.Height-1);
		//					}
		//
		//				}
		//				else
		//				{
		//					using (Brush b = new SolidBrush(this.BackColor))
		//					{
		//						g.FillRectangle(b, bounds.Left, bounds.Top, bounds.Width, bounds.Height);
		//					}
		//				}
		//
		//				object pitem = null;
		//				if (this.Items[index] is DataRowView) 
		//				{
		//					pitem = ((DataRowView)this.Items[index]).Row[this.DisplayMember.Substring(this.DisplayMember.IndexOf(".")+1)];
		//				}
		//				else if (this.Items[index] is string) 
		//				{
		//					pitem = (string)this.Items[index];
		//				}
		//				else //Custom objects
		//				{
		//					if (this.Items[index] != null && this.DisplayMember == String.Empty && this.ValueMember == String.Empty)
		//						pitem = this.Items[index].ToString();
		//				}
		//								
		//				if ( pitem != null )
		//				{
		//
		//					if ( Enabled )
		//					{
		//						using (Brush brush1=new SolidBrush(this.ForeColor))
		//						{
		//							g.DrawString((string)pitem, SystemInformation.MenuFont,
		//								brush1, new RectangleF(bounds.Left+2, bounds.Top, 
		//								bounds.Width-20, bounds.Height));
		//
		//							//g.DrawString((string)pitem, SystemInformation.MenuFont, 
		//							//	brush1, new Point(bounds.Left+2, bounds.Top));
		//						}
		//						if ( selected && Enabled )
		//						{
		//							using (Brush brush1=new SolidBrush(this.m_SelectionForeColor ))
		//							{
		//								g.DrawString((string)pitem, SystemInformation.MenuFont,
		//									brush1, new RectangleF(bounds.Left+2, bounds.Top, 
		//									bounds.Width-20, bounds.Height));
		//							}
		//
		//						}
		//					}
		//					else
		//					{
		//						g.DrawString((string)pitem, SystemInformation.MenuFont,
		//							SystemBrushes.ControlDark, new RectangleF(bounds.Left+2, bounds.Top, 
		//							bounds.Width-20, bounds.Height));
		//
		//						//g.DrawString((string)pitem, SystemInformation.MenuFont, 
		//						//	SystemBrushes.ControlDark, new Point(bounds.Left+2, bounds.Top));
		//					}
		//				}
		//			}
		//		}

		#endregion

		#region Properties

		[Category("Behavior"), DefaultValue(false)]
		public bool HotTrack
		{
			get
			{
				return this.hotTrack;
			}
			set
			{
				this.hotTrack = value;
				base.Invalidate();
			}
		}
//		[Category("Behavior"), DefaultValue((string) null)]
//		public ImageList ImageList
//		{
//			get
//			{
//				return this.imageList;
//			}
//			set
//			{
//				this.imageList = value;
//				base.Invalidate();
//			}
//		}

//		[DefaultValue(false), Category("Behavior")]
//		public bool UseFirstImage
//		{
//			get
//			{
//				return this.useFirstImage;
//			}
//			set
//			{
//				this.useFirstImage = value;
//				base.Invalidate();
//			}
//		}
//
//		[DefaultValue(DrawItemStyle.Default), Category("Behavior")]
//		public DrawItemStyle DrawItemStyle
//		{
//			get
//			{
//				return this.drawItemStyle;
//			}
//			set
//			{
//				this.drawItemStyle = value;
//				base.Invalidate();
//			}
//		}


		public override Color BackColor
		{
			get
			{
				if (this.ShouldSerializeBackColor())
				{
					return base.BackColor;
				}
				return SystemColors.Window;
			}
			set
			{
				base.BackColor = value;
			}
		}
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override Image BackgroundImage
		{
			get
			{
				return base.BackgroundImage;
			}
			set
			{
				base.BackgroundImage = value;
			}
		}
		[Description("ListBoxBorder"), DefaultValue(2), Category("Appearance"), DispId(-504)]
		public BorderStyle BorderStyle
		{
			get
			{
				return this.borderStyle;
			}
			set
			{
				if (!Enum.IsDefined(typeof(BorderStyle), value))
				{
					throw new InvalidEnumArgumentException("value", (int) value, typeof(BorderStyle));
				}
				if (value != this.borderStyle)
				{
					this.borderStyle = value;
					base.RecreateHandle();
					this.integralHeightAdjust = true;
					try
					{
						base.Height = this.requestedHeight;
					}
					finally
					{
						this.integralHeightAdjust = false;
					}
				}
			}
		}
		[Localizable(true), Description("ListBoxColumnWidth"), Category("Behavior"), DefaultValue(0)]
		public int ColumnWidth
		{
			get
			{
				return this.columnWidth;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("InvalidLowBoundArgumentEx");//SR.GetString("InvalidLowBoundArgumentEx", new object[] { "value", value.ToString(), "0" }));
				}
				if (this.columnWidth != value)
				{
					this.columnWidth = value;
					if (this.columnWidth == 0)
					{
						base.RecreateHandle();
					}
					else if (base.IsHandleCreated)
					{
						this.SendMessage(0x195, this.columnWidth, 0);
					}
				}
			}
		}
 
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode=true)]
			get
			{
				CreateParams params1 = base.CreateParams;
				params1.ClassName ="LISTBOX";
				params1.Style |= 0x200041;
				//if (this.scrollAlwaysVisible)
				//{
				//	params1.Style |= 0x1000;
				//}
				if (!this.integralHeight)
				{
					params1.Style |= 0x100;
				}
				if (this.useTabStops)
				{
					params1.Style |= 0x80;
				}
				switch (this.borderStyle)
				{
					case BorderStyle.FixedSingle:
					{
						params1.Style |= 0x800000;
						break;
					}
					case BorderStyle.Fixed3D:
					{
						params1.ExStyle |= 0x200;
						break;
					}
				}
				if (this.multiColumn)
				{
					params1.Style |= 0x100200;
				}
				else if (this.horizontalScrollbar)
				{
					params1.Style |= 0x100000;
				}
				switch (this.drawMode)
				{
					case DrawMode.Normal:
					{
						return params1;
					}
					case DrawMode.OwnerDrawFixed:
					{
						params1.Style |= 0x10;
						return params1;
					}
					case DrawMode.OwnerDrawVariable:
					{
						params1.Style |= 0x20;
						return params1;
					}
				}
				return params1;
			}
		}


		protected override Size DefaultSize
		{
			get
			{
				return new Size(120, 0x60);
			}
		}
		[RefreshProperties(RefreshProperties.Repaint), Description("ListBoxDrawMode"), DefaultValue(0), Category("Behavior")]
		public virtual DrawMode DrawMode
		{
			get
			{
				return this.drawMode;
			}
			set
			{
				if (!Enum.IsDefined(typeof(DrawMode), value))
				{
					throw new InvalidEnumArgumentException("value", (int) value, typeof(DrawMode));
				}
				if (this.drawMode != value)
				{
					if (this.MultiColumn && (value == DrawMode.OwnerDrawVariable))
					{
						throw new ArgumentException("ListBoxVarHeightMultiCol", "value");
					}
					this.drawMode = value;
					base.RecreateHandle();
				}
			}
		}
		internal int FocusedIndex
		{
			get
			{
				if (base.IsHandleCreated)
				{
					return (int) this.SendMessage(0x19f, 0, 0);
				}
				return -1;
			}
		}
		public override Color ForeColor
		{
			get
			{
				if (this.ShouldSerializeForeColor())
				{
					return base.ForeColor;
				}
				return SystemColors.WindowText;
			}
			set
			{
				base.ForeColor = value;
			}
		}
		[Category("Behavior"), Localizable(true), Description("ListBoxHorizontalExtent"), DefaultValue(0)]
		public int HorizontalExtent
		{
			get
			{
				return this.horizontalExtent;
			}
			set
			{
				if (value != this.horizontalExtent)
				{
					this.horizontalExtent = value;
					this.UpdateHorizontalExtent();
				}
			}
		}
		[Category("Behavior"), Description("ListBoxHorizontalScrollbar"), Localizable(true), DefaultValue(false)]
		public bool HorizontalScrollbar
		{
			get
			{
				return this.horizontalScrollbar;
			}
			set
			{
				if (value != this.horizontalScrollbar)
				{
					this.horizontalScrollbar = value;
					if (!this.MultiColumn)
					{
						base.RecreateHandle();
					}
				}
			}
		}
		[Localizable(true), DefaultValue(true), Category("Behavior"), Description("ListBoxIntegralHeight"), RefreshProperties(RefreshProperties.Repaint)]
		public bool IntegralHeight
		{
			get
			{
				return this.integralHeight;
			}
			set
			{
				if (this.integralHeight != value)
				{
					this.integralHeight = value;
					base.RecreateHandle();
					this.integralHeightAdjust = true;
					try
					{
						base.Height = this.requestedHeight;
					}
					finally
					{
						this.integralHeightAdjust = false;
					}
				}
			}
		}
 
		[Category("Behavior"), RefreshProperties(RefreshProperties.Repaint), DefaultValue(13), Localizable(true), Description("ListBoxItemHeight")]
		public virtual int ItemHeight
		{
			get
			{
				//if ((this.drawMode != DrawMode.OwnerDrawFixed) && (this.drawMode != DrawMode.OwnerDrawVariable))
				//{
				//	return this.GetItemHeight(0);
				//}
				return this.itemHeight;
			}
			set
			{
				if ((value < 1) || (value > 0xff))
				{
					throw new ArgumentOutOfRangeException("ArgumentOutOfRangeException");//SR.GetString("InvalidExBoundArgument", new object[] { "value", value.ToString(), "0", "256" }));
				}
				if (this.itemHeight != value)
				{
					this.itemHeight = value;
					if ((this.drawMode == DrawMode.OwnerDrawFixed) && base.IsHandleCreated)
					{
						this.BeginUpdate();
						this.SendMessage(0x1a0, 0, value);
						if (this.IntegralHeight)
						{
							Size size1 = base.Size;
							base.Size = new Size(size1.Width + 1, size1.Height);
							base.Size = size1;
						}
						this.EndUpdate();
					}
				}
			}
		}
 
		[Description("ListBoxItems"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Localizable(true), Category("Data"), Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
		public McListItems.ObjectCollection Items
		{
			get
			{
				if (this.itemsCollection == null)
				{
					this.itemsCollection = this.CreateItemCollection();
				}
				return this.itemsCollection;
			}
		}
 
		internal virtual int MaxItemWidth
		{
			get
			{
				if (this.horizontalExtent > 0)
				{
					return this.horizontalExtent;
				}
				if (this.DrawMode != DrawMode.Normal)
				{
					return -1;
				}
				if (this.maxWidth <= -1)
				{
					this.maxWidth = this.ComputeMaxItemWidth(this.maxWidth);
				}
				return this.maxWidth;
			}
		}
 
		[DefaultValue(false), Description("ListBoxMultiColumn"), Category("Behavior")]
		public bool MultiColumn
		{
			get
			{
				return this.multiColumn;
			}
			set
			{
				if (this.multiColumn != value)
				{
					if (value && (this.drawMode == DrawMode.OwnerDrawVariable))
					{
						throw new ArgumentException("ListBoxVarHeightMultiCol", "value");
					}
					this.multiColumn = value;
					base.RecreateHandle();
				}
			}
		}
		[Description("ListBoxPreferredHeight"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Advanced)]
		public int PreferredHeight
		{
			get
			{
				int num1 = 0;
				if (this.drawMode == DrawMode.OwnerDrawVariable)
				{
					if (this.itemsCollection != null)
					{
						int num2 = this.itemsCollection.Count;
						for (int num3 = 0; num3 < num2; num3++)
						{
							num1 += this.GetItemHeight(num3);
						}
					}
				}
				else
				{
					int num4 = (this.itemsCollection == null) ? 0 : this.itemsCollection.Count;
					num1 = this.GetItemHeight(0) * num4;
				}
				if (this.borderStyle != BorderStyle.None)
				{
					num1 += (SystemInformation.BorderSize.Height * 4) + 3;
				}
				return num1;
			}
		}
		public override RightToLeft RightToLeft
		{
			get
			{
                //if (!McListItems.RunningOnWin2K)
                //{
                //    return RightToLeft.No;
                //}
				return base.RightToLeft;
			}
			set
			{
				base.RightToLeft = value;
			}
		}
        //private static bool RunningOnWin2K
        //{
        //    get
        //    {
        //        if (!McListItems.checkedOS)
        //        {
        //            new EnvironmentPermission(PermissionState.Unrestricted).Assert();
        //            try
        //            {
        //                if ((Environment.OSVersion.Platform != PlatformID.Win32NT) || (Environment.OSVersion.Version.Major < 5))
        //                {
        //                    McListItems.runningOnWin2K = false;
        //                }
        //            }
        //            finally
        //            {
        //                CodeAccessPermission.RevertAssert();
        //            }
        //        }
        //        return McListItems.runningOnWin2K;
        //    }
        //}
 
		//		[Localizable(true), DefaultValue(false), Category("Behavior"), Description("ListBoxScrollIsVisible")]
		//		public bool ScrollAlwaysVisible
		//		{
		//			get
		//			{
		//				return this.scrollAlwaysVisible;
		//			}
		//			set
		//			{
		//				if (this.scrollAlwaysVisible != value)
		//				{
		//					this.scrollAlwaysVisible = value;
		//					base.RecreateHandle();
		//				}
		//			}
		//		}


		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("ListBoxSelectedIndex"), Bindable(true), Browsable(false)]
		public override int SelectedIndex
		{
			get
			{
				if (base.IsHandleCreated)
				{
					return (int) this.SendMessage(0x188, 0, 0);
				}
				return -1;
			}
			set
			{
				int num1 = (this.itemsCollection == null) ? 0 : this.itemsCollection.Count;
				if ((value < -1) || (value >= num1))
				{
					throw new ArgumentOutOfRangeException("Argument Out Of RangeType");//,SR.GetString("InvalidArgument", new object[] { "value", value.ToString() }));
				}

				if (value != -1)
				{
					int num2 = this.SelectedIndex;
					if (num2 != value)
					{
						if (base.IsHandleCreated)
						{
							this.NativeSetSelected(value, true);
						}
						this.OnSelectedIndexChanged(EventArgs.Empty);
					}
				}
			}
		}
 
		[Bindable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("ListBoxSelectedItem"), Browsable(false)]
		public object SelectedItem
		{
			get
			{
				if (this.itemsCollection != null)
				{
					int num1 = SelectedIndex;
					if (num1 != -1)
					{
						return this.itemsCollection[num1];
					}
				}
				return null;
			}
			set
			{
				if (this.itemsCollection != null)
				{
					if (value != null)
					{
						int num1 = this.itemsCollection.IndexOf(value);
						if (num1 != -1)
						{
							this.SelectedIndex = num1;
						}
					}
					else
					{
						this.SelectedIndex = -1;
					}
				}
			}
		}
 
		[DefaultValue(false), Category("Behavior"), Description("ListBoxSorted")]
		public bool Sorted
		{
			get
			{
				return this.sorted;
			}
			set
			{
				if (this.sorted != value)
				{
					this.sorted = value;
					if ((this.sorted && (this.itemsCollection != null)) && (this.itemsCollection.Count > 1))
					{
						this.Sort();
					}
				}
			}
		}
 
		[Localizable(true), Bindable(true)]
		public override string Text
		{
			get
			{
				if ((this.SelectedItem != null) && !BindingFieldEmpty)
				{
					return base.FilterItemOnProperty(this.SelectedItem).ToString();
				}
				return base.Text;
			}
			set
			{
				base.Text = value;
				if (!base.DesignMode)
				{
					if (value == null)
					{
						this.SelectedIndex = -1;
					}
					else if ((value != null) && ((this.SelectedItem == null) || (string.Compare(value, base.FilterItemOnProperty(this.SelectedItem).ToString(), false, CultureInfo.CurrentCulture) != 0)))
					{
						for (int num1 = 0; num1 < this.Items.Count; num1++)
						{
							if (string.Compare(value, base.FilterItemOnProperty(this.Items[num1]).ToString(), false, CultureInfo.CurrentCulture) == 0)
							{
								this.SelectedIndex = num1;
								return;
							}
						}
						for (int num2 = 0; num2 < this.Items.Count; num2++)
						{
							if (string.Compare(value, base.FilterItemOnProperty(this.Items[num2]).ToString(), true, CultureInfo.CurrentCulture) == 0)
							{
								this.SelectedIndex = num2;
								return;
							}
						}
					}
				}
			}
		}
 

//		[Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Bindable(false)]
//		public override string Text
//		{
//			get
//			{
//				if (this.SelectedItem != null)
//				{
//					return base.FilterItemOnProperty(this.SelectedItem).ToString();
//				}
//				return base.Text;
//			}
//			set
//			{
//				base.Text = value;
//				if (((value != null)) && ((this.SelectedItem == null) || !value.Equals(base.GetItemText(this.SelectedItem))))
//				{
//					int num1 = this.Items.Count;
//					for (int num2 = 0; num2 < num1; num2++)
//					{
//						if (string.Compare(value, base.GetItemText(this.Items[num2]), true, CultureInfo.CurrentCulture) == 0)
//						{
//							this.SelectedIndex = num2;
//							return;
//						}
//					}
//				}
//			}
//		}
 
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("ListBoxTopIndex"), Browsable(false)]
		public int TopIndex
		{
			get
			{
				if (base.IsHandleCreated)
				{
					return (int) this.SendMessage(0x18e, 0, 0);
				}
				return this.topIndex;
			}
			set
			{
				if (base.IsHandleCreated)
				{
					this.SendMessage(0x197, value, 0);
				}
				else
				{
					this.topIndex = value;
				}
			}
		}
		[Category("Behavior"), Description("ListBoxUseTabStops"), DefaultValue(true)]
		public bool UseTabStops
		{
			get
			{
				return this.useTabStops;
			}
			set
			{
				if (this.useTabStops != value)
				{
					this.useTabStops = value;
					base.RecreateHandle();
				}
			}
		}

		#endregion

		#region Events
 
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackgroundImageChanged
		{
			add
			{
				base.BackgroundImageChanged += value;
			}
			remove
			{
				base.BackgroundImageChanged -= value;
			}
		}
 
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
		public new event EventHandler Click
		{
			add
			{
				base.Click += value;
			}
			remove
			{
				base.Click -= value;
			}
		}
 
		[Category("Behavior"), Description("drawItemEvent")]
		public event DrawItemEventHandler DrawItem
		{
			add
			{
				base.Events.AddHandler(McListItems.EVENT_DRAWITEM, value);
			}
			remove
			{
				base.Events.RemoveHandler(McListItems.EVENT_DRAWITEM, value);
			}
		}
		[Category("Behavior"), Description("measureItemEvent")]
		public event MeasureItemEventHandler MeasureItem
		{
			add
			{
				base.Events.AddHandler(McListItems.EVENT_MEASUREITEM, value);
			}
			remove
			{
				base.Events.RemoveHandler(McListItems.EVENT_MEASUREITEM, value);
			}
		}
 
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event PaintEventHandler Paint
		{
			add
			{
				base.Paint += value;
			}
			remove
			{
				base.Paint -= value;
			}
		}
		[Category("Behavior"), Description("selectedIndexChangedEvent")]
		public event EventHandler SelectedIndexChanged
		{
			add
			{
				base.Events.AddHandler(McListItems.EVENT_SELECTEDINDEXCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(McListItems.EVENT_SELECTEDINDEXCHANGED, value);
			}
		}
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
		public new event EventHandler TextChanged
		{
			add
			{
				base.TextChanged += value;
			}
			remove
			{
				base.TextChanged -= value;
			}
		}
 

		// Events
		//		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		//		public event EventHandler BackgroundImageChanged;
		//		[Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
		//		public event EventHandler Click;
		//		[Category("Behavior"), Description("drawItemEvent")]
		//		public event DrawItemEventHandler DrawItem;
		//		[Category("Behavior"), Description("measureItemEvent")]
		//		public event MeasureItemEventHandler MeasureItem;
		//		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		//		public event PaintEventHandler Paint;
		//		[Category("Behavior"), Description("selectedIndexChangedEvent")]
		//		public event EventHandler SelectedIndexChanged;
		//		[Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
		//		public event EventHandler TextChanged;

		#endregion

		#region class ItemArray

		// Nested Types
		internal class ItemArray : IComparer
		{
			static ItemArray()
			{
				McListItems.ItemArray.lastMask = 1;
			}

			public ItemArray(ListControl listControl)
			{
				this.listControl = listControl;
			}

			public object Add(object item)
			{
				this.EnsureSpace(1);
				this.version++;
				this.entries[this.count] = new McListItems.ItemArray.Entry(item);
				return this.entries[this.count++];
			}

			public void AddRange(ICollection items)
			{
				if (items == null)
				{
					throw new ArgumentNullException("items");
				}
				this.EnsureSpace(items.Count);
				foreach (object obj1 in items)
				{
					this.entries[this.count++] = new McListItems.ItemArray.Entry(obj1);
				}
				this.version++;
			}

 
			public void Clear()
			{
				this.count = 0;
				this.version++;
			}

 
			public static int CreateMask()
			{
				int num1 = McListItems.ItemArray.lastMask;
				McListItems.ItemArray.lastMask = McListItems.ItemArray.lastMask << 1;
				return num1;
			}

 
			private void EnsureSpace(int elements)
			{
				if (this.entries == null)
				{
					this.entries = new McListItems.ItemArray.Entry[Math.Max(elements, 4)];
				}
				else if ((this.count + elements) >= this.entries.Length)
				{
					int num1 = Math.Max((int) (this.entries.Length * 2), (int) (this.entries.Length + elements));
					McListItems.ItemArray.Entry[] entryArray1 = new McListItems.ItemArray.Entry[num1];
					this.entries.CopyTo(entryArray1, 0);
					this.entries = entryArray1;
				}
			}

			public int GetActualIndex(int virtualIndex, int stateMask)
			{
				if (stateMask == 0)
				{
					return virtualIndex;
				}
				int num1 = -1;
				for (int num2 = 0; num2 < this.count; num2++)
				{
					if ((this.entries[num2].state & stateMask) != 0)
					{
						num1++;
						if (num1 == virtualIndex)
						{
							return num2;
						}
					}
				}
				return -1;
			}

			public int GetCount(int stateMask)
			{
				if (stateMask == 0)
				{
					return this.count;
				}
				int num1 = 0;
				for (int num2 = 0; num2 < this.count; num2++)
				{
					if ((this.entries[num2].state & stateMask) != 0)
					{
						num1++;
					}
				}
				return num1;
			}

			internal object GetEntryObject(int virtualIndex, int stateMask)
			{
				int num1 = this.GetActualIndex(virtualIndex, stateMask);
				if (num1 == -1)
				{
					throw new IndexOutOfRangeException();
				}
				return this.entries[num1];
			}

			public IEnumerator GetEnumerator(int stateMask)
			{
				return this.GetEnumerator(stateMask, false);
			}

			public IEnumerator GetEnumerator(int stateMask, bool anyBit)
			{
				return new McListItems.ItemArray.EntryEnumerator(this, stateMask, anyBit);
			}

			public object GetItem(int virtualIndex, int stateMask)
			{
				int num1 = this.GetActualIndex(virtualIndex, stateMask);
				if (num1 == -1)
				{
					throw new IndexOutOfRangeException();
				}
				return this.entries[num1].item;
			}

			public bool GetState(int index, int stateMask)
			{
				return ((this.entries[index].state & stateMask) == stateMask);
			}

			public int IndexOf(object item, int stateMask)
			{
				int num1 = -1;
				for (int num2 = 0; num2 < this.count; num2++)
				{
					if ((stateMask == 0) || ((this.entries[num2].state & stateMask) != 0))
					{
						num1++;
						if (this.entries[num2].item.Equals(item))
						{
							return num1;
						}
					}
				}
				return -1;
			}

 
			public int IndexOfIdentifier(object identifier, int stateMask)
			{
				int num1 = -1;
				for (int num2 = 0; num2 < this.count; num2++)
				{
					if ((stateMask == 0) || ((this.entries[num2].state & stateMask) != 0))
					{
						num1++;
						if (this.entries[num2] == identifier)
						{
							return num1;
						}
					}
				}
				return -1;
			}

 
			public void Insert(int index, object item)
			{
				this.EnsureSpace(1);
				if (index < this.count)
				{
					Array.Copy(this.entries, index, this.entries, index + 1, this.count - index);
				}
				this.entries[index] = new McListItems.ItemArray.Entry(item);
				this.count++;
				this.version++;
			}

			public void Remove(object item)
			{
				int num1 = this.IndexOf(item, 0);
				if (num1 != -1)
				{
					this.RemoveAt(num1);
				}
			}

 
			public void RemoveAt(int index)
			{
				this.count--;
				for (int num1 = index; num1 < this.count; num1++)
				{
					this.entries[num1] = this.entries[num1 + 1];
				}
				this.version++;
			}

			public void SetItem(int index, object item)
			{
				this.entries[index].item = item;
			}

			public void SetState(int index, int stateMask, bool value)
			{
				if (value)
				{
					McListItems.ItemArray.Entry entry1 = this.entries[index];
					entry1.state |= stateMask;
				}
				else
				{
					McListItems.ItemArray.Entry entry2 = this.entries[index];
					entry2.state &= ~stateMask;
				}
				this.version++;
			}

			public void Sort()
			{
				Array.Sort(this.entries, 0, this.count, this);
			}

			public void Sort(Array externalArray)
			{
				Array.Sort(externalArray, this);
			}

			int IComparer.Compare(object item1, object item2)
			{
				if (item1 == null)
				{
					if (item2 == null)
					{
						return 0;
					}
					return -1;
				}
				if (item2 == null)
				{
					return 1;
				}
				if (item1 is McListItems.ItemArray.Entry)
				{
					item1 = ((McListItems.ItemArray.Entry) item1).item;
				}
				if (item2 is McListItems.ItemArray.Entry)
				{
					item2 = ((McListItems.ItemArray.Entry) item2).item;
				}
				string text1 = this.listControl.GetItemText(item1);
				string text2 = this.listControl.GetItemText(item2);
				return Application.CurrentCulture.CompareInfo.Compare(text1, text2, CompareOptions.StringSort);
			}

			public int Version
			{
				get
				{
					return this.version;
				}
			}
 

	
			// Fields
			private int count;
			private Entry[] entries;
			private static int lastMask;
			private ListControl listControl;
			private int version;
		
			#region class Entry

			// Nested Types
			private class Entry
			{
				public Entry(object item)
				{
					this.item = item;
					this.state = 0;
				}

				// Fields
				public object item;
				public int state;
			}

			#endregion

			#region class EntryEnumerator

			private class EntryEnumerator :IEnumerator  
			{
				public EntryEnumerator(McListItems.ItemArray items, int state, bool anyBit)
				{
					this.items = items;
					this.state = state;
					this.anyBit = anyBit;
					this.version = items.version;
					this.current = -1;
				}


				object GetCurrent()
				{
					if ((this.current == -1) || (this.current == this.items.count))
					{
						throw new InvalidOperationException("ListEnumCurrentOutOfRange");
					}
					return this.items.entries[this.current].item;
				}

				bool InternalMoveNext()
				{
					if (this.version != this.items.version)
					{
						throw new InvalidOperationException("ListEnumVersionMismatch");
					}
					Label_0023:
						if (this.current < (this.items.count - 1))
						{
							this.current++;
							if (this.anyBit)
							{
								if ((this.items.entries[this.current].state & this.state) != 0)
								{
									return true;
								}
								goto Label_0023;
							}
							if ((this.items.entries[this.current].state & this.state) == this.state)
							{
								return true;
							}
							goto Label_0023;
						}
					this.current = this.items.count;
					return false;
				}

 
				void InternalReset()
				{
					if (this.version != this.items.version)
					{
						throw new InvalidOperationException("ListEnumVersionMismatch");
					}
					this.current = -1;
				}


				// Fields
				private bool anyBit;
				private int current;
				private McListItems.ItemArray items;
				private int state;
				private int version;

				#region IEnumerator Members

				public object Current 
				{
					get{return GetCurrent();} 
				}

				public void Reset()
				{
					InternalReset();
				}

				public bool MoveNext()
				{
					return InternalMoveNext();
				}

				#endregion
			}
	

			#endregion

		}
		#endregion

		#region class ObjectCollection

		[ListBindable(false)]
		public class ObjectCollection : IList, ICollection, IEnumerable
		{
			public ObjectCollection(McListItems owner)
			{
				this.owner = owner;
			}

			public ObjectCollection(McListItems owner, McListItems.ObjectCollection value)
			{
				this.owner = owner;
				this.AddRange(value);
			}

			public ObjectCollection(McListItems owner, object[] value)
			{
				this.owner = owner;
				this.AddRange(value);
			}
 
			public int Add(object item)
			{
				this.owner.CheckNoDataSource();
				if (item == null)
				{
					throw new ArgumentNullException("item");
				}
				object obj1 = this.InnerArray.Add(item);
				int num1 = -1;
				bool flag1 = false;
				try
				{
					if (this.owner.sorted)
					{
						if (this.owner.updateCount <= 0)
						{
							this.InnerArray.Sort();
							num1 = this.InnerArray.IndexOfIdentifier(obj1, 0);
							if (this.owner.IsHandleCreated)
							{
								this.owner.NativeInsert(num1, item);
								this.owner.UpdateMaxItemWidth(item, false);
							}
						}
					}
					else
					{
						num1 = this.InnerArray.GetCount(0) - 1;
						if (this.owner.IsHandleCreated)
						{
							this.owner.NativeAdd(item);
							this.owner.UpdateMaxItemWidth(item, false);
						}
					}
					flag1 = true;
				}
				finally
				{
					if (!flag1)
					{
						this.InnerArray.Remove(item);
					}
				}
				this.owner.UpdateHorizontalExtent();
				return num1;
			}

			public void AddRange(object[] items)
			{
				this.owner.CheckNoDataSource();
				this.AddRangeInternal(items);
			}

			public void AddRange(McListItems.ObjectCollection value)
			{
				this.owner.CheckNoDataSource();
				this.AddRangeInternal(value);
			}

			internal void AddRangeInternal(ICollection items)
			{
				IEnumerator enumerator1;
				IDisposable disposable1;
				if (items == null)
				{
					throw new ArgumentNullException("items");
				}
				this.owner.BeginUpdate();
				if (this.owner.sorted)
				{
					enumerator1 = items.GetEnumerator();
					try
					{
						while (enumerator1.MoveNext())
						{
							if (enumerator1.Current == null)
							{
								throw new ArgumentNullException("item");
							}
						}
					}
					finally
					{
						disposable1 = enumerator1 as IDisposable;
						if (disposable1 != null)
						{
							disposable1.Dispose();
						}
					}
					this.InnerArray.AddRange(items);
					this.InnerArray.Sort();
					if (this.owner.IsHandleCreated)
					{
						Exception exception1 = null;
						object[] objArray1 = new object[items.Count];
						items.CopyTo(objArray1, 0);
						this.InnerArray.Sort(objArray1);
						object[] objArray2 = objArray1;
						for (int num2 = 0; num2 < objArray2.Length; num2++)
						{
							object obj2 = objArray2[num2];
							if (exception1 == null)
							{
								try
								{
									int num1 = this.InnerArray.IndexOf(obj2, 0);
									this.owner.NativeInsert(num1, obj2);
									this.owner.UpdateMaxItemWidth(obj2, false);
								}
								catch (Exception exception2)
								{
									exception1 = exception2;
									this.InnerArray.Remove(obj2);
								}
							}
							else
							{
								this.InnerArray.Remove(obj2);
							}
						}
						if (exception1 != null)
						{
							throw exception1;
						}
					}
				}
				else
				{
					enumerator1 = items.GetEnumerator();
					try
					{
						while (enumerator1.MoveNext())
						{
							if (enumerator1.Current == null)
							{
								throw new ArgumentNullException("item");
							}
						}
					}
					finally
					{
						disposable1 = enumerator1 as IDisposable;
						if (disposable1 != null)
						{
							disposable1.Dispose();
						}
					}
					this.InnerArray.AddRange(items);
					if (this.owner.IsHandleCreated)
					{
						Exception exception3 = null;
						foreach (object obj4 in items)
						{
							if (exception3 == null)
							{
								try
								{
									this.owner.NativeAdd(obj4);
									this.owner.UpdateMaxItemWidth(obj4, false);
									continue;
								}
								catch (Exception exception4)
								{
									exception3 = exception4;
									this.InnerArray.Remove(obj4);
									continue;
								}
							}
							this.InnerArray.Remove(obj4);
						}
						if (exception3 != null)
						{
							throw exception3;
						}
					}
				}
				this.owner.UpdateHorizontalExtent();
				this.owner.EndUpdate();
			}

 
			public virtual void Clear()
			{
				this.owner.CheckNoDataSource();
				this.ClearInternal();
			}

 
			internal void ClearInternal()
			{
				int num1 = this.owner.Items.Count;
				for (int num2 = 0; num2 < num1; num2++)
				{
					this.owner.UpdateMaxItemWidth(this.InnerArray.GetItem(num2, 0), true);
				}
				if (this.owner.IsHandleCreated)
				{
					this.owner.NativeClear();
				}
				this.InnerArray.Clear();
				this.owner.maxWidth = -1;
				this.owner.UpdateHorizontalExtent();
			}

			public bool Contains(object value)
			{
				return (this.IndexOf(value) != -1);
			}

			public void CopyTo(object[] dest, int arrayIndex)
			{
				int num1 = this.InnerArray.GetCount(0);
				for (int num2 = 0; num2 < num1; num2++)
				{
					dest[num2 + arrayIndex] = this.InnerArray.GetItem(num2, 0);
				}
			}

 
			public IEnumerator GetEnumerator()
			{
				return this.InnerArray.GetEnumerator(0);
			}

 
			public int IndexOf(object value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("ArgumentNullException");//SR.GetString("InvalidArgument", new object[] { "value", "null" }));
				}
				return this.InnerArray.IndexOf(value, 0);
			}

			internal int IndexOfIdentifier(object value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("ArgumentNullException");//SR.GetString("InvalidArgument", new object[] { "value", "null" }));
				}
				return this.InnerArray.IndexOfIdentifier(value, 0);
			}

			public void Insert(int index, object item)
			{
				this.owner.CheckNoDataSource();
				if (item == null)
				{
					throw new ArgumentNullException("item");
				}
				if ((index < 0) || (index > this.InnerArray.GetCount(0)))
				{
					throw new ArgumentOutOfRangeException("Argument Out Of RangeType Exception");//SR.GetString("InvalidArgument", new object[] { "index", index.ToString() }));
				}
				if (this.owner.sorted)
				{
					this.Add(item);
				}
				else
				{
					this.InnerArray.Insert(index, item);
					if (this.owner.IsHandleCreated)
					{
						bool flag1 = false;
						try
						{
							this.owner.NativeInsert(index, item);
							this.owner.UpdateMaxItemWidth(item, false);
							flag1 = true;
						}
						finally
						{
							if (!flag1)
							{
								this.InnerArray.RemoveAt(index);
							}
						}
					}
				}
				this.owner.UpdateHorizontalExtent();
			}

 
			public void Remove(object value)
			{
				int num1 = this.InnerArray.IndexOf(value, 0);
				if (num1 != -1)
				{
					this.RemoveAt(num1);
				}
			}

			public void RemoveAt(int index)
			{
				this.owner.CheckNoDataSource();
				if ((index < 0) || (index >= this.InnerArray.GetCount(0)))
				{
					throw new ArgumentOutOfRangeException("ArgumentOutOfRangeException");//SR.GetString("InvalidArgument", new object[] { "index", index.ToString() }));
				}
				this.owner.UpdateMaxItemWidth(this.InnerArray.GetItem(index, 0), true);
				if (this.owner.IsHandleCreated)
				{
					this.owner.NativeRemoveAt(index);
				}
				this.InnerArray.RemoveAt(index);
				this.owner.UpdateHorizontalExtent();
			}

			internal void SetItemInternal(int index, object value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if ((index < 0) || (index >= this.InnerArray.GetCount(0)))
				{
					throw new ArgumentOutOfRangeException("ArgumentOutOfRangeException");//SR.GetString("InvalidArgument", new object[] { "index", index.ToString() }));
				}
				this.owner.UpdateMaxItemWidth(this.InnerArray.GetItem(index, 0), true);
				this.InnerArray.SetItem(index, value);
				if (this.owner.IsHandleCreated)
				{
					bool flag1 = this.owner.SelectedIndex == index;
					this.owner.NativeRemoveAt(index);
					this.owner.NativeInsert(index, value);
					this.owner.UpdateMaxItemWidth(value, false);
					if (flag1)
					{
						this.owner.SelectedIndex = index;
					}
				}
				this.owner.UpdateHorizontalExtent();
			}

			void ICollection.CopyTo(Array dest, int index)
			{
				int num1 = this.InnerArray.GetCount(0);
				for (int num2 = 0; num2 < num1; num2++)
				{
					dest.SetValue(this.InnerArray.GetItem(num2, 0), num2 + index);
				}
			}

 
			public bool IsSynchronized
			{
				get{return false;}
			}

 
			public object SyncRoot
			{
				get{return this;}
			}

 
			int IList.Add(object item)
			{
				return this.Add(item);
			}

			public bool IsFixedSize
			{
				get{return false;}
			}

 
			public int Count
			{
				get
				{
					return this.InnerArray.GetCount(0);
				}
			}
			internal McListItems.ItemArray InnerArray
			{
				get
				{
					if (this.items == null)
					{
						this.items = new McListItems.ItemArray(this.owner);
					}
					return this.items;
				}
			}
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}
			[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
			public virtual object this[int index]
			{
				get
				{
					if ((index < 0) || (index >= this.InnerArray.GetCount(0)))
					{
						throw new ArgumentOutOfRangeException("ArgumentOutOfRangeException");//SR.GetString("InvalidArgument", new object[] { "index", index.ToString() }));
					}
					return this.InnerArray.GetItem(index, 0);
				}
				set
				{
					this.owner.CheckNoDataSource();
					this.SetItemInternal(index, value);
				}
			}

			// Fields
			private McListItems.ItemArray items;
			private McListItems owner;

		}

		#endregion

	}

	#region	McDrawItemEventArgs

	public class McDrawItemEventArgs : EventArgs
	{
		// Methods
		public McDrawItemEventArgs(Graphics graphics, Font font, Rectangle rect, int index, DrawItemState state)
		{
			this.graphics = graphics;
			this.font = font;
			this.rect = rect;
			this.index = index;
			this.state = state;
			this.foreColor = SystemColors.WindowText;
			this.backColor = SystemColors.Window;
		}

 
		public McDrawItemEventArgs(Graphics graphics, Font font, Rectangle rect, int index, DrawItemState state, Color foreColor, Color backColor)
		{
			this.graphics = graphics;
			this.font = font;
			this.rect = rect;
			this.index = index;
			this.state = state;
			this.foreColor = foreColor;
			this.backColor = backColor;
		}

		internal void Dispose()
		{
			this.graphics.Dispose();
		}

 
		public virtual void DrawBackground()
		{
			Brush brush1 = new SolidBrush(this.BackColor);
			this.Graphics.FillRectangle(brush1, this.rect);
			brush1.Dispose();
		}

		public virtual void DrawFocusRectangle()
		{
			if (((this.state & DrawItemState.Focus) == DrawItemState.Focus) && ((this.state & DrawItemState.NoFocusRect) != DrawItemState.NoFocusRect))
			{
				ControlPaint.DrawFocusRectangle(this.Graphics, this.rect, this.ForeColor, this.BackColor);
			}
		}

		public Color BackColor
		{
			get
			{
				if ((this.state & DrawItemState.Selected) == DrawItemState.Selected)
				{
					return SystemColors.Highlight;
				}
				return this.backColor;
			}
		}
		public Rectangle Bounds
		{
			get
			{
				return this.rect;
			}
		}
 
		public Font Font
		{
			get
			{
				return this.font;
			}
		}
		public Color ForeColor
		{
			get
			{
				if ((this.state & DrawItemState.Selected) == DrawItemState.Selected)
				{
					return SystemColors.HighlightText;
				}
				return this.foreColor;
			}
		}
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}
		public int Index
		{
			get
			{
				return this.index;
			}
		}
		public DrawItemState State
		{
			get
			{
				return this.state;
			}
		}

		// Fields
		private Color backColor;
		private Font font;
		private Color foreColor;
		private readonly Graphics graphics;
		private readonly int index;
		private readonly Rectangle rect;
		private readonly DrawItemState state;
	}
	#endregion

}
