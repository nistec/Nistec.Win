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
using Nistec.WinForms.Design;

namespace Nistec.WinForms.Controls
{

	//[ToolboxItem(false),DefaultProperty("Items"), DefaultEvent("SelectedIndexChanged"), Designer("System.Windows.Forms.Design.ListBoxDesigner, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ToolboxItem(false),DefaultProperty("Items"), DefaultEvent("SelectedIndexChanged"), Designer(typeof(McListControlDesigner))]
	public class McListControl : ListControl
	{

		#region Members
		// Fields
		private BorderStyle borderStyle;
		private static bool checkedOS;
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
		private static bool runningOnWin2K;
		private bool scrollAlwaysVisible;
		private SelectedIndexCollection selectedIndices;
		private SelectedObjectCollection selectedItems;
		private bool selectedValueChangedFired;
		private SelectionMode selectionMode;
		private bool sorted;
		private int topIndex;
		private int updateCount;
		private bool useTabStops;

		//		protected Color m_SelectionColor;
		//		protected Color m_SelectionBorderColor;
		//		protected Color m_SelectionForeColor;

		#endregion

		#region Constructor
		static McListControl()
		{
			McListControl.EVENT_SELECTEDINDEXCHANGED = new object();
			McListControl.EVENT_DRAWITEM = new object();
			McListControl.EVENT_MEASUREITEM = new object();
			McListControl.checkedOS = false;
			McListControl.runningOnWin2K = true;
		}

 
		public McListControl()
		{
			this.itemHeight = 13;
			this.horizontalExtent = 0;
			this.maxWidth = -1;
			this.updateCount = 0;
			this.sorted = false;
			this.scrollAlwaysVisible = false;
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
			this.selectionMode = SelectionMode.One;
			base.SetStyle(ControlStyles.UserPaint, false);
			base.SetStyle(ControlStyles.StandardClick, false);
			base.SetBounds(0, 0, 120, 0x60);
			this.requestedHeight = base.Height;
			
			//			this.m_SelectionColor=Color.LightSteelBlue;
			//			this.m_SelectionBorderColor=Color.Blue;
			//			this.m_SelectionForeColor=Color.Navy;

		}

		#endregion

		#region InternalMethods

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
			if (base.DataSource != null)
			{
				throw new ArgumentException("DataSourceLocksItems");
			}
		}

		public void ClearSelected()
		{
			bool flag1 = false;
			int num1 = (this.itemsCollection == null) ? 0 : this.itemsCollection.Count;
			for (int num2 = 0; num2 < num1; num2++)
			{
				if (this.SelectedItems.GetSelected(num2))
				{
					flag1 = true;
					this.SelectedItems.SetSelected(num2, false);
					if (base.IsHandleCreated)
					{
						this.NativeSetSelected(num2, false);
					}
				}
			}
			if (flag1)
			{
				this.OnSelectedIndexChanged(EventArgs.Empty);
			}
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
				//return num1;
			}
			finally
			{
				Win32.WinAPI.SelectObject(new HandleRef(this, ptr1), new HandleRef(this, ptr2));
				Win32.WinAPI.ReleaseDC(new HandleRef(this, base.Handle), new HandleRef(this, ptr1));
			}
			return num1;
		}

		protected virtual McListControl.ObjectCollection CreateItemCollection()
		{
			return new McListControl.ObjectCollection(this);
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
			if ((this.itemsCollection != null) && this.SelectedItems.GetSelected(index))
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
			if (this.selectionMode == SelectionMode.One)
			{
				SendMessage(390, value ? index : -1, 0);
			}
			else
			{
				SendMessage(0x185, value ? -1 : 0, index);
			}
		}

		private void NativeUpdateSelection()
		{
			int num1 = this.Items.Count;
			for (int num2 = 0; num2 < num1; num2++)
			{
				this.SelectedItems.SetSelected(num2, false);
			}
			int[] numArray1 = null;
			switch (this.selectionMode)
			{
				case SelectionMode.One:
				{
					int num3 = (int) SendMessage(0x188, 0, 0);
					if (num3 >= 0)
					{
						numArray1 = new int[] { num3 };
					}
					break;
				}
				case SelectionMode.MultiSimple:
				case SelectionMode.MultiExtended:
				{
					int num4 = (int) SendMessage(400, 0, 0);
					if (num4 > 0)
					{
						numArray1 = new int[num4];
						Win32.WinAPI.SendMessage(new HandleRef(this, base.Handle), 0x191, num4, numArray1);
					}
					break;
				}
			}
			if (numArray1 != null)
			{
				int[] numArray2 = numArray1;
				for (int num6 = 0; num6 < numArray2.Length; num6++)
				{
					int num5 = numArray2[num6];
					this.SelectedItems.SetSelected(num5, true);
				}
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
				if (this.SelectionMode != SelectionMode.None)
				{
					this.SelectedIndex = base.DataManager.Position;
				}
			}
		}

 
		protected virtual void OnDrawItem(DrawItemEventArgs e)
		{
			DrawItemEventHandler handler1 = (DrawItemEventHandler) base.Events[McListControl.EVENT_DRAWITEM];
			if (handler1 != null)
			{
				handler1(this, e);
			}
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
					if (((this.selectionMode != SelectionMode.None) && (this.selectedItems != null)) && this.selectedItems.GetSelected(num2))
					{
						this.NativeSetSelected(num2, true);
					}
				}
			}
			if (((this.selectedItems != null) && (this.selectedItems.Count > 0)) && (this.selectionMode == SelectionMode.One))
			{
				this.SelectedItems.Dirty();
				this.SelectedItems.EnsureUpToDate();
			}
			this.UpdateHorizontalExtent();
		}

 
		protected override void OnHandleDestroyed(EventArgs e)
		{
			this.SelectedItems.EnsureUpToDate();
			if (base.Disposing)
			{
				this.itemsCollection = null;
			}
			base.OnHandleDestroyed(e);
		}

		protected virtual void OnMeasureItem(MeasureItemEventArgs e)
		{
			MeasureItemEventHandler handler1 = (MeasureItemEventHandler) base.Events[McListControl.EVENT_MEASUREITEM];
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
			EventHandler handler1 = (EventHandler) base.Events[McListControl.EVENT_SELECTEDINDEXCHANGED];
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
			McListControl.ObjectCollection collection1 = this.itemsCollection;
			this.itemsCollection = null;
			this.selectedIndices = null;
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
				//this.itemsCollection = null;
				this.Items.AddRangeInternal(objArray1);
			}
			if ((base.DataManager != null) && (this.SelectionMode != SelectionMode.None))
			{
				this.SelectedIndex = base.DataManager.Position;
			}
			else if (collection1 != null)
			{
				int num2 = collection1.Count;
				for (int num3 = 0; num3 < num2; num3++)
				{
					if (collection1.InnerArray.GetState(num3, McListControl.SelectedObjectCollection.SelectedObjectMask))
					{
						this.SelectedItem = collection1[num3];
					}
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
			this.SelectedItems.Dirty();
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
			if (this.selectionMode == SelectionMode.None)
			{
				throw new InvalidOperationException("ListBoxInvalidSelectionMode");
			}
			this.SelectedItems.SetSelected(index, value);
			if (base.IsHandleCreated)
			{
				this.NativeSetSelected(index, value);
			}
			this.SelectedItems.Dirty();
			this.OnSelectedIndexChanged(EventArgs.Empty);
		}

		protected virtual void Sort()
		{
			this.CheckNoDataSource();
			McListControl.SelectedObjectCollection collection1 = this.SelectedItems;
			collection1.EnsureUpToDate();
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
						if (collection1.GetSelected(num2))
						{
							this.NativeSetSelected(num2, true);
						}
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
					if (this.selectedItems != null)
					{
						this.selectedItems.Dirty();
					}
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

 
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode=true)]
		protected override void WndProc(ref Message m)
		{
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
							if (this.selectedItems != null)
							{
								this.selectedItems.Dirty();
							}
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
				params1.ClassName = "LISTBOX";
				params1.Style |= 0x200041;
				if (this.scrollAlwaysVisible)
				{
					params1.Style |= 0x1000;
				}
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
				switch (this.selectionMode)
				{
					case SelectionMode.None:
					{
						params1.Style |= 0x4000;
						break;
					}
					case SelectionMode.MultiSimple:
					{
						params1.Style |= 8;
						break;
					}
					case SelectionMode.MultiExtended:
					{
						params1.Style |= 0x800;
						break;
					}
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
				if ((this.drawMode != DrawMode.OwnerDrawFixed) && (this.drawMode != DrawMode.OwnerDrawVariable))
				{
					return this.GetItemHeight(0);
				}
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
		public McListControl.ObjectCollection Items
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
				if (!McListControl.RunningOnWin2K)
				{
					return RightToLeft.No;
				}
				return base.RightToLeft;
			}
			set
			{
				base.RightToLeft = value;
			}
		}
		private static bool RunningOnWin2K
		{
			get
			{
				if (!McListControl.checkedOS)
				{
					new EnvironmentPermission(PermissionState.Unrestricted).Assert();
					try
					{
						if ((Environment.OSVersion.Platform != PlatformID.Win32NT) || (Environment.OSVersion.Version.Major < 5))
						{
							McListControl.runningOnWin2K = false;
						}
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
				}
				return McListControl.runningOnWin2K;
			}
		}
 
		[Localizable(true), DefaultValue(false), Category("Behavior"), Description("ListBoxScrollIsVisible")]
		public bool ScrollAlwaysVisible
		{
			get
			{
				return this.scrollAlwaysVisible;
			}
			set
			{
				if (this.scrollAlwaysVisible != value)
				{
					this.scrollAlwaysVisible = value;
					base.RecreateHandle();
				}
			}
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("ListBoxSelectedIndex"), Bindable(true), Browsable(false)]
		public override int SelectedIndex
		{
			get
			{
				if (this.selectionMode != SelectionMode.None)
				{
					if ((this.selectionMode == SelectionMode.One) && base.IsHandleCreated)
					{
						return (int) this.SendMessage(0x188, 0, 0);
					}
					if ((this.itemsCollection != null) && (this.SelectedItems.Count > 0))
					{
						return this.Items.IndexOfIdentifier(this.SelectedItems.GetObjectAt(0));
					}
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
				if (this.selectionMode == SelectionMode.None)
				{
					throw new ArgumentException("ListBoxInvalidSelectionMode", "value");
				}
				if ((this.selectionMode == SelectionMode.One) && (value != -1))
				{
					int num2 = this.SelectedIndex;
					if (num2 != value)
					{
						if (num2 != -1)
						{
							this.SelectedItems.SetSelected(num2, false);
						}
						this.SelectedItems.SetSelected(value, true);
						if (base.IsHandleCreated)
						{
							this.NativeSetSelected(value, true);
						}
						this.OnSelectedIndexChanged(EventArgs.Empty);
					}
				}
				else if (value == -1)
				{
					if (this.SelectedIndex != -1)
					{
						this.ClearSelected();
					}
				}
				else if (!this.SelectedItems.GetSelected(value))
				{
					this.SelectedItems.SetSelected(value, true);
					if (base.IsHandleCreated)
					{
						this.NativeSetSelected(value, true);
					}
					this.OnSelectedIndexChanged(EventArgs.Empty);
				}
			}
		}
 
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), Description("ListBoxSelectedIndices")]
		public McListControl.SelectedIndexCollection SelectedIndices
		{
			get
			{
				if (this.selectedIndices == null)
				{
					this.selectedIndices = new McListControl.SelectedIndexCollection(this);
				}
				return this.selectedIndices;
			}
		}
		[Bindable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("ListBoxSelectedItem"), Browsable(false)]
		public object SelectedItem
		{
			get
			{
				if (this.SelectedItems.Count > 0)
				{
					return this.SelectedItems[0];
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
						if (num1 == -1)
						{
							return;
						}
						this.SelectedIndex = num1;
					}
					else
					{
						this.SelectedIndex = -1;
					}
				}
			}
		}
 
		[Description("ListBoxSelectedItems"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public McListControl.SelectedObjectCollection SelectedItems
		{
			get
			{
				if (this.selectedItems == null)
				{
					this.selectedItems = new McListControl.SelectedObjectCollection(this);
				}
				return this.selectedItems;
			}
		}
		[Description("ListBoxSelectionMode"), DefaultValue(1), Category("Behavior")]
		public virtual SelectionMode SelectionMode
		{
			get
			{
				return this.selectionMode;
			}
			set
			{
				if (!Enum.IsDefined(typeof(SelectionMode), value))
				{
					throw new InvalidEnumArgumentException("value", (int) value, typeof(SelectionMode));
				}
				if (this.selectionMode != value)
				{
					this.SelectedItems.EnsureUpToDate();
					this.selectionMode = value;
					base.RecreateHandle();
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
 
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Bindable(false)]
		public override string Text
		{
			get
			{
				if ((this.SelectionMode != SelectionMode.None) && (this.SelectedItem != null))
				{
					return base.FilterItemOnProperty(this.SelectedItem).ToString();
				}
				return base.Text;
			}
			set
			{
				base.Text = value;
				if (((this.SelectionMode != SelectionMode.None) && (value != null)) && ((this.SelectedItem == null) || !value.Equals(base.GetItemText(this.SelectedItem))))
				{
					int num1 = this.Items.Count;
					for (int num2 = 0; num2 < num1; num2++)
					{
						if (string.Compare(value, base.GetItemText(this.Items[num2]), true, CultureInfo.CurrentCulture) == 0)
						{
							this.SelectedIndex = num2;
							return;
						}
					}
				}
			}
		}
 
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
				base.Events.AddHandler(McListControl.EVENT_DRAWITEM, value);
			}
			remove
			{
				base.Events.RemoveHandler(McListControl.EVENT_DRAWITEM, value);
			}
		}
		[Category("Behavior"), Description("measureItemEvent")]
		public event MeasureItemEventHandler MeasureItem
		{
			add
			{
				base.Events.AddHandler(McListControl.EVENT_MEASUREITEM, value);
			}
			remove
			{
				base.Events.RemoveHandler(McListControl.EVENT_MEASUREITEM, value);
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
				base.Events.AddHandler(McListControl.EVENT_SELECTEDINDEXCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(McListControl.EVENT_SELECTEDINDEXCHANGED, value);
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
				McListControl.ItemArray.lastMask = 1;
			}

			public ItemArray(ListControl listControl)
			{
				this.listControl = listControl;
			}

			public object Add(object item)
			{
				this.EnsureSpace(1);
				this.version++;
				this.entries[this.count] = new McListControl.ItemArray.Entry(item);
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
					this.entries[this.count++] = new McListControl.ItemArray.Entry(obj1);
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
				int num1 = McListControl.ItemArray.lastMask;
				McListControl.ItemArray.lastMask = McListControl.ItemArray.lastMask << 1;
				return num1;
			}

 
			private void EnsureSpace(int elements)
			{
				if (this.entries == null)
				{
					this.entries = new McListControl.ItemArray.Entry[Math.Max(elements, 4)];
				}
				else if ((this.count + elements) >= this.entries.Length)
				{
					int num1 = Math.Max((int) (this.entries.Length * 2), (int) (this.entries.Length + elements));
					McListControl.ItemArray.Entry[] entryArray1 = new McListControl.ItemArray.Entry[num1];
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
				return new McListControl.ItemArray.EntryEnumerator(this, stateMask, anyBit);
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
				this.entries[index] = new McListControl.ItemArray.Entry(item);
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
					McListControl.ItemArray.Entry entry1 = this.entries[index];
					entry1.state |= stateMask;
				}
				else
				{
					McListControl.ItemArray.Entry entry2 = this.entries[index];
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
				if (item1 is McListControl.ItemArray.Entry)
				{
					item1 = ((McListControl.ItemArray.Entry) item1).item;
				}
				if (item2 is McListControl.ItemArray.Entry)
				{
					item2 = ((McListControl.ItemArray.Entry) item2).item;
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
				public EntryEnumerator(McListControl.ItemArray items, int state, bool anyBit)
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
				private McListControl.ItemArray items;
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
			public ObjectCollection(McListControl owner)
			{
				this.owner = owner;
			}

			public ObjectCollection(McListControl owner, McListControl.ObjectCollection value)
			{
				this.owner = owner;
				this.AddRange(value);
			}

			public ObjectCollection(McListControl owner, object[] value)
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

			public void AddRange(McListControl.ObjectCollection value)
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
					this.owner.SelectedItems.SetSelected(index, false);
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
			internal McListControl.ItemArray InnerArray
			{
				get
				{
					if (this.items == null)
					{
						this.items = new McListControl.ItemArray(this.owner);
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
			private McListControl.ItemArray items;
			private McListControl owner;

		}

		#endregion

		#region class SelectedIndexCollection

		public class SelectedIndexCollection : IList, ICollection, IEnumerable
		{
			public SelectedIndexCollection(McListControl owner)
			{
				this.owner = owner;
			}

 
			public bool Contains(int selectedIndex)
			{
				return (this.IndexOf(selectedIndex) != -1);
			}

			public void CopyTo(Array dest, int index)
			{
				int num1 = this.Count;
				for (int num2 = 0; num2 < num1; num2++)
				{
					dest.SetValue((object) this[num2], num2 + index);
				}
			}

			public IEnumerator GetEnumerator()
			{
				return new McListControl.SelectedIndexCollection.SelectedIndexEnumerator(this);
			}

 
			public int IndexOf(int selectedIndex)
			{
				if (((selectedIndex >= 0) && (selectedIndex < this.InnerArray.GetCount(0))) && this.InnerArray.GetState(selectedIndex, McListControl.SelectedObjectCollection.SelectedObjectMask))
				{
					return this.InnerArray.IndexOf(this.InnerArray.GetItem(selectedIndex, 0), McListControl.SelectedObjectCollection.SelectedObjectMask);
				}
				return -1;
			}

 
			public bool IsSynchronized
			{
				get{return true;}
			}

 
			public object SyncRoot
			{
				get{return this;}
			}

 
			int IList.Add(object value)
			{
				throw new NotSupportedException();
			}

			void IList.Clear()
			{
				throw new NotSupportedException();
			}

			bool IList.Contains(object selectedIndex)
			{
				if (selectedIndex is int)
				{
					return this.Contains((int) selectedIndex);
				}
				return false;
			}

 
			public bool IsFixedSize
			{
				get{return true;}
			}

			public object GetItem(int index)
			{
				return this[index];
			}

			public object GetItemObject(int index)
			{
				return this.InnerArray.GetEntryObject(index, McListControl.SelectedObjectCollection.SelectedObjectMask);
				//return this.InnerArray.IndexOfIdentifier(obj1, 0);
			}
			public void SetItemObject(int index,object value)
			{
				this.InnerArray.SetItem(index, value);
			}
 
			public object this[int index]
			{
				get{return GetItemObject(index);}
				set{SetItemObject(index,value);}
			}

			int IList.IndexOf(object selectedIndex)
			{
				if (selectedIndex is int)
				{
					return this.IndexOf((int) selectedIndex);
				}
				return -1;
			}

			void IList.Insert(int index, object value)
			{
				throw new NotSupportedException();
			}

 
			void IList.Remove(object value)
			{
				throw new NotSupportedException();
			}

			void IList.RemoveAt(int index)
			{
				throw new NotSupportedException();
			}

			void SetItem(int index, object value)
			{
				throw new NotSupportedException();
			}

//			public object this[int index, object value]
//			{
//				set{throw new NotSupportedException();}
//			}
 
			[Browsable(false)]
			public int Count
			{
				get
				{
					return this.owner.SelectedItems.Count;
				}
			}
 
			private McListControl.ItemArray InnerArray
			{
				get
				{
					this.owner.SelectedItems.EnsureUpToDate();
					return this.owner.Items.InnerArray;
				}
			}
 
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}
			//			public int this[int index]
			//			{
			//				get
			//				{
			//					object obj1 = this.InnerArray.GetEntryObject(index, McListControl.SelectedObjectCollection.SelectedObjectMask);
			//					return this.InnerArray.IndexOfIdentifier(obj1, 0);
			//				}
			//			}
 

			// Fields
			private McListControl owner;

			// Nested Types
			private class SelectedIndexEnumerator : IEnumerator
			{
				public SelectedIndexEnumerator(McListControl.SelectedIndexCollection items)
				{
					this.items = items;
					this.current = -1;
				}

				object GetCurrent()
				{
					if ((this.current == -1) || (this.current == this.items.Count))
					{
						throw new InvalidOperationException("ListEnumCurrentOutOfRange");
					}
					return this.items[this.current];
				}

				bool InternalMoveNext()
				{
					if (this.current < (this.items.Count - 1))
					{
						this.current++;
						return true;
					}
					this.current = this.items.Count;
					return false;
				}

				void InternalReset()
				{
					this.current = -1;
				}


				// Fields
				private int current;
				private McListControl.SelectedIndexCollection items;

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
		}

		#endregion

		#region SelectedObjectCollection

		public class SelectedObjectCollection : IList, ICollection, IEnumerable
		{
			static SelectedObjectCollection()
			{
				McListControl.SelectedObjectCollection.SelectedObjectMask = McListControl.ItemArray.CreateMask();
			}

			public SelectedObjectCollection(McListControl owner)
			{
				this.owner = owner;
				this.stateDirty = true;
				this.lastVersion = -1;
			}

			public bool Contains(object selectedObject)
			{
				return (this.IndexOf(selectedObject) != -1);
			}

			public void CopyTo(Array dest, int index)
			{
				int num1 = this.InnerArray.GetCount(McListControl.SelectedObjectCollection.SelectedObjectMask);
				for (int num2 = 0; num2 < num1; num2++)
				{
					dest.SetValue(this.InnerArray.GetItem(num2, McListControl.SelectedObjectCollection.SelectedObjectMask), num2 + index);
				}
			}

			internal void Dirty()
			{
				this.stateDirty = true;
			}

			internal void EnsureUpToDate()
			{
				if (this.stateDirty)
				{
					this.stateDirty = false;
					if (this.owner.IsHandleCreated)
					{
						this.owner.NativeUpdateSelection();
					}
				}
			}

 
			public IEnumerator GetEnumerator()
			{
				return this.InnerArray.GetEnumerator(McListControl.SelectedObjectCollection.SelectedObjectMask);
			}

			internal object GetObjectAt(int index)
			{
				return this.InnerArray.GetEntryObject(index, McListControl.SelectedObjectCollection.SelectedObjectMask);
			}

			internal bool GetSelected(int index)
			{
				return this.InnerArray.GetState(index, McListControl.SelectedObjectCollection.SelectedObjectMask);
			}

			public int IndexOf(object selectedObject)
			{
				return this.InnerArray.IndexOf(selectedObject, McListControl.SelectedObjectCollection.SelectedObjectMask);
			}

 
			internal void SetSelected(int index, bool value)
			{
				this.InnerArray.SetState(index, McListControl.SelectedObjectCollection.SelectedObjectMask, value);
			}

 
			public bool IsSynchronized
			{
				get{return false;}
			}

			public object SyncRoot
			{
				get{return this;}
			}

			int IList.Add(object value)
			{
				throw new NotSupportedException();
			}

 
			void IList.Clear()
			{
				throw new NotSupportedException();
			}

			public bool IsFixedSize
			{
				get{return true;}
			}

 
			void IList.Insert(int index, object value)
			{
				throw new NotSupportedException();
			}

			void IList.Remove(object value)
			{
				throw new NotSupportedException();
			}

			void IList.RemoveAt(int index)
			{
				throw new NotSupportedException();
			}

 
			public int Count
			{
				get
				{
					if (this.owner.IsHandleCreated)
					{
						switch (this.owner.selectionMode)
						{
							case SelectionMode.None:
							{
								return 0;
							}
							case SelectionMode.One:
							{
								if (this.owner.SelectedIndex >= 0)
								{
									return 1;
								}
								return 0;
							}
							case SelectionMode.MultiSimple:
							case SelectionMode.MultiExtended:
							{
								return (int) this.owner.SendMessage(400, 0, 0);
							}
						}
						return 0;
					}
					if (this.lastVersion != this.InnerArray.Version)
					{
						this.lastVersion = this.InnerArray.Version;
						this.count = this.InnerArray.GetCount(McListControl.SelectedObjectCollection.SelectedObjectMask);
					}
					return this.count;
				}
			}
 
			private McListControl.ItemArray InnerArray
			{
				get
				{
					this.EnsureUpToDate();
					return this.owner.Items.InnerArray;
				}
			}
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}
 
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
			public object this[int index]
			{
				get
				{
					return this.InnerArray.GetItem(index, McListControl.SelectedObjectCollection.SelectedObjectMask);
				}
				set
				{
					throw new NotSupportedException();
				}
			}


			// Fields
			private int count;
			private int lastVersion;
			private McListControl owner;
			internal static int SelectedObjectMask;
			private bool stateDirty;
		}
		#endregion

	}

}
