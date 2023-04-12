using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Runtime.InteropServices;

using Nistec.Drawing;
using Nistec.Win32;

namespace Nistec.WinForms
{

	[ToolboxItem(true), ToolboxBitmap(typeof(McTreeCombo), "Toolbox.TreeCombo.bmp"),Designer(typeof(Design.McDesigner))]
	public class McTreeCombo : Nistec.WinForms.Controls.McButtonEdit
	{

		#region Members
		// Events
		[Category("Behavior")]
		public event EventHandler DropDown;
		[Category("Behavior")]
		public event EventHandler DropUp;
		[Category("Behavior")]
		public event TreeNodesFillEventHandler TreeNodesFill;
		[Category("Behavior")]
		public event TreeValueChangedEventHandler ValueChanged;


		// Fields
		//private EventHandler DropDown;
		private int dropDownWidth;
		//private EventHandler DropUp;
		private int maxDropDownHeight;
		private object selectedItem;
		private bool selectOnlyTagNotNull;
		//private TreeNodesFillEventHandler TreeNodesFill;
		internal TreeViewPopupForm TreeViewPopupForm;
		//private ValueChangedEventHandler ValueChanged;
		McTreeView treeView;

		#endregion

		#region Constructor

        public McTreeCombo()
		{

			this.treeView=new McTreeView();
			this.TreeViewPopupForm = null;
			this.selectedItem = null;
			this.dropDownWidth = 0;
			this.selectOnlyTagNotNull = false;
			this.maxDropDownHeight = 200;
			//base.McTextBox.Visible = false;
			//base.ButtonBitmap = McPaint.GetImage("Mcmulsoft.Controls", "Mcmulsoft.Controls.Bmp.ComboDown.bmp");
		}

 
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				//base.ButtonBitmap.Dispose();
			}
			base.Dispose(disposing);
		}

		#endregion

		#region PopUp

		protected  void ShowPopUp()
		{
			if (this.TreeViewPopupForm == null)
			{
				//TreeNode[] treeNodes=new TreeNode[this.Nodes.Count];
				//this.Nodes.CopyTo(treeNodes,0);
				this.InvokeDropDown(EventArgs.Empty);
				this.TreeViewPopupForm = new TreeViewPopupForm(this);
				Form form1 = base.FindForm();
				if (form1 != null)
				{
					form1.AddOwnedForm(this.TreeViewPopupForm);
				}
				Rectangle rectangle1 = base.RectangleToScreen(base.ClientRectangle);
				this.TreeViewPopupForm.Handle.ToString();
				this.TreeViewPopupForm.Location = new Point(rectangle1.Left, (rectangle1.Top + base.Height) + 1);
				int num1 = this.DropDownWidth;
				if (num1 <= 0)
				{
					num1 = base.Width;
				}
				this.TreeViewPopupForm.Size = new Size(num1, this.MaxDropDownHeight);
				if (this.TreeViewPopupForm.Bottom > Screen.PrimaryScreen.WorkingArea.Bottom)
				{
					this.TreeViewPopupForm.Top = (rectangle1.Top - 1) - this.MaxDropDownHeight;
				}
				this.TreeViewPopupForm.Closed += new EventHandler(this.OnClosePopup);
				TreeNodesFillEventArgs args1 = new TreeNodesFillEventArgs(this.TreeViewPopupForm.tvNodes.Nodes, this.TreeViewPopupForm.tvNodes);
				this.InvokeTreeNodesFill(args1);
				this.TreeViewPopupForm.ShowPopupForm();
				//this.TreeViewPopupForm.AddTreeNodes(this.Nodes);// treeNodes);

				this.TreeViewPopupForm.Focus();
				DroppedDown = true;
				this.OnDropDown(new System.EventArgs());

			}
			else
			{
				this.TreeViewPopupForm.ClosePopupForm();
				this.TreeViewPopupForm = null;
				DroppedDown=false;
			}

		}
	
		public override void DoDropDown()
		{
			if(DroppedDown)
			{
				CloseDropDown();
				return;
			}	
		
			ShowPopUp();	
			//base.DoDropDown ();
		}

		public override void CloseDropDown()
		{
			if(DroppedDown && this.TreeViewPopupForm != null)
			{
				this.TreeViewPopupForm.ClosePopupForm();
				this.TreeViewPopupForm = null;
			}
			DroppedDown=false;
		}

		#endregion

        #region StyleProperty

        [Category("Style"), DefaultValue(typeof(Color), "WindowText")]
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set
            {
                SerializeForeColor(value, true);
            }
        }

        [Category("Style"), DefaultValue(typeof(Color), "Window")]
        public override Color BackColor
        {
            get { return base.BackColor; }
            set
            {
                SerializeBackColor(value, true);
            }
        }

        protected override void OnStylePropertyChanged(PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("ControlLayout") || e.PropertyName.Equals("BackColor"))
                SerializeBackColor(Color.Empty, false);
            if (e.PropertyName.Equals("ControlLayout") || e.PropertyName.Equals("ForeColor"))
                SerializeForeColor(Color.Empty, false);
            if (e.PropertyName.Equals("ControlLayout") || e.PropertyName.Equals("TextFont"))
                SerializeFont(Form.DefaultFont, false);
            if ((DesignMode || IsHandleCreated))
                this.Invalidate(true);
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected void SerializeFont(Font value, bool force)
        {
            if (ShouldSerializeForeColor())
                this.Font = LayoutManager.Layout.TextFontInternal;
            else if (force)
                this.Font = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected void SerializeForeColor(Color value, bool force)
        {
            if (ShouldSerializeForeColor())
            {
                base.ForeColor = LayoutManager.Layout.ForeColorInternal;
            }
            else if (force)
            {
                base.ForeColor = value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected void SerializeBackColor(Color value, bool force)
        {
            if (ShouldSerializeBackColor())
            {
                base.BackColor = LayoutManager.Layout.BackColorInternal;
            }
            else if (force)
            {
                base.BackColor = value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected bool ShouldSerializeBackColor()
        {
            return IsHandleCreated && StylePainter != null;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected bool ShouldSerializeForeColor()
        {
            return IsHandleCreated && StylePainter != null;
        }

        #endregion

 		public void InvokeDropDown(EventArgs e)
		{
			this.OnDropDown(e);
		}

		public void InvokeDropUp(EventArgs e)
		{
			this.OnDropUp(e);
		}

 		public void InvokeTreeNodesFill(TreeNodesFillEventArgs e)
		{
			this.OnTreeNodesFill(e);
		}

		public void InvokeValueChanged(TreeValueChangedEventArgs e)
		{
			this.OnValueChanged(e);
		}

		private void OnClosePopup(object sender, EventArgs e)
		{
			if (this.TreeViewPopupForm != null)
			{
				this.InvokeDropUp(e);
				this.TreeViewPopupForm = null;
			}
			DroppedDown=false;
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

		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged(e);
			//base.McTextBox.Visible = false;
			base.Invalidate();
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if ((e.Button & MouseButtons.Left) > MouseButtons.None)
			{
				DoDropDown();
			}
		}
 
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if (this.TreeViewPopupForm != null)
			{
				this.TreeViewPopupForm.LockClose = false;
			}
		}

 
		protected override void OnPaint(PaintEventArgs p)
		{
			base.OnPaint(p);
			if (!base.Enabled)
			{
				return;
			}
			Graphics graphics1 = p.Graphics;
			new Rectangle(0, 0, base.Width - 1, base.Height - 1);
			Rectangle rectangle1 = this.GetContentRect();
			rectangle1.X--;
			using (StringFormat format1 = new StringFormat())
			{
				format1.FormatFlags |= StringFormatFlags.NoWrap;
				format1.Trimming = StringTrimming.EllipsisCharacter;
				format1.LineAlignment = StringAlignment.Center;
				if (base.Enabled)
				{
					using (SolidBrush brush1 = new SolidBrush(this.ForeColor))
					{
						graphics1.DrawString(this.Text, this.Font, brush1, (RectangleF) rectangle1, format1);
						goto Label_00D1;
					}
				}
				graphics1.DrawString(this.Text, this.Font, SystemBrushes.ControlDark, (RectangleF) rectangle1, format1);
			}
			Label_00D1:
				if (this.Focused)
				{
					Rectangle rectangle2 = this.GetContentRect();
					rectangle2.X--;
					rectangle2.Y++;
					rectangle2.Width--;
					rectangle2.Height -= 2;
					ControlPaint.DrawFocusRectangle(graphics1, rectangle2);
				}
		}

		protected override void OnSystemColorsChanged(EventArgs e)
		{
			base.OnSystemColorsChanged(e);
			McColors.InitColors();
		}

		protected virtual void OnTreeNodesFill(TreeNodesFillEventArgs e)
		{
			if (this.TreeNodesFill != null)
			{
				this.TreeNodesFill(this, e);
			}
		}

		protected virtual void OnValueChanged(TreeValueChangedEventArgs e)
		{
			if (this.ValueChanged != null)
			{
				this.ValueChanged(this, e);
			}
		}

 
		[Category("Behavior"), DefaultValue(0)]
		public virtual int DropDownWidth
		{
			get
			{
				return this.dropDownWidth;
			}
			set
			{
				this.dropDownWidth = value;
			}
		}
		[Category("Behavior"), DefaultValue(200)]
		public virtual int MaxDropDownHeight
		{
			get
			{
				return this.maxDropDownHeight;
			}
			set
			{
				this.maxDropDownHeight = value;
			}
		}
 
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public virtual object SelectedItem
		{
			get
			{
				return this.selectedItem;
			}
			set
			{
				this.selectedItem = value;
			}
		}
		[DefaultValue(false), Category("Behavior")]
		public virtual bool SelectOnlyTagNotNull
		{
			get
			{
				return this.selectOnlyTagNotNull;
			}
			set
			{
				this.selectOnlyTagNotNull = value;
			}
		}

		[DefaultValue(null), Category("Behavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public System.Windows.Forms.TreeNodeCollection Nodes
		{
			get
			{
				return this.treeView.Nodes;
			}
//			set
//			{
//				this.selectOnlyTagNotNull = value;
//			}
		}

	}
	

	#region McTreeView
//	[ToolboxItem(false)]//, ToolboxBitmap(typeof(McTreeView), "Toolbox.McTreeView.bmp")]
//	public class McTreeView : TreeView
//	{
//		public McTreeView()
//		{
//		}
//
//		public void Lock()
//		{
//			Win32.WinAPI.LockWindowUpdate(base.Handle);
//		}
//
//		protected override void OnClick(EventArgs e)
//		{
//			base.OnClick(e);
//			Point point1 = base.PointToClient(Cursor.Position);
//			TreeNode node1 = base.GetNodeAt(point1);
//			base.SelectedNode = node1;
//		}
//
// 
//		public void SetBold(TreeNode node, bool isBold)
//		{
//			McTreeView.SetBold(this, node, isBold);
//		}
//
//		public static void SetBold(McTreeView treeView, TreeNode node, bool isBold)
//		{
//			try
//			{
//				if (node.Handle != IntPtr.Zero)
//				{
//					TVITEMEX tvitemex1 = new TVITEMEX();
//					tvitemex1.mask = 0x18;
//					tvitemex1.hItem = node.Handle;
//					tvitemex1.state = isBold ? 0x10 : 0;
//					tvitemex1.stateMask = 0x10;
//					IntPtr ptr1 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(TVITEMEX)));
//					Marshal.StructureToPtr(tvitemex1, ptr1, true);
//					Win32.WinAPI.SendMessage(treeView.Handle, 0x113f, 0, ptr1);
//					Marshal.FreeHGlobal(ptr1);
//				}
//			}
//			catch
//			{
//			}
//		}
//
//		public void Unlock()
//		{
//			Win32.WinAPI.LockWindowUpdate(IntPtr.Zero);
//		}
//
//
//		// Fields
//		private const int TV_FIRST = 0x1100;
//		private const int TVIF_HANDLE = 0x10;
//		private const int TVIF_STATE = 8;
//		private const int TVIS_BOLD = 0x10;
//		private const int TVM_SETITEMW = 0x113f;
//	}
	#endregion


}
