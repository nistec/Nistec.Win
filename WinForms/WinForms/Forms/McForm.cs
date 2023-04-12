using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using Nistec.Drawing;

namespace Nistec.WinForms
{
    public class McForm : FormBase//, IRibbonClientStyle
    {

        #region members
        static ImageList _internalImages = null;

        // Fields
        private IContainer components = null;
        private bool formActive;
        //internal BasePainter mCurrentRenderer;
        private bool mInheritStyleFromParent = true;
        //private RibbonStyle mStyle;
        private bool BFLAG;
        private bool MAXFLAG;
        private McCaptionForm caption;

        //Color colorActiveBorder;
        //Color colorInActiveBorder;
        //Color colorBrush1;
        //Color colorBrush2;
        #endregion

        #region ctor

        static McForm()
		{
			// Create a strip of images by loading an embedded bitmap resource
			_internalImages = DrawUtils.LoadBitmapStrip(Type.GetType("Nistec.WinForms.McForm"),
				"Nistec.WinForms.Images.ImagesControlBox.bmp",
				new Size(16, 16),new Point(0,0));
		}

        public McForm()
        {
            //colorActiveBorder = Color.SteelBlue;
            //colorInActiveBorder = Color.Gray;
            //colorBrush1 = Color.AliceBlue;
            //colorBrush2 = Color.SteelBlue;

            this.InitStyle();// xc256bcbd35c60777();
            this.InitComponents();// x85601834555fb7d5();
            this.CreateRegion();// x5a964354ec662d0a();
        }
        public McForm(IStyle style):base(style)
        {
            this.InitStyle();// xc256bcbd35c60777();
            this.InitComponents();// x85601834555fb7d5();
            this.CreateRegion();// x5a964354ec662d0a();
        }

        private void InitStyle()//xc256bcbd35c60777()
        {
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.SetStyle(ControlStyles.Selectable, true);
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            base.SetStyle(ControlStyles.UserPaint, true);
        }
        private void InitComponents()
        {
            caption = new McCaptionForm();
            base.SuspendLayout();
            // 
            // caption
            // 
            this.caption.BackColor = System.Drawing.Color.Transparent;
            //this.caption.Image = null;
            this.caption.Dock = System.Windows.Forms.DockStyle.Top;
            this.caption.StylePainter = this.StyleGuideBase;
            this.caption.Location = new System.Drawing.Point(4, 4);
            this.caption.Name = "caption";
            this.caption.ShortcutKey = 'O';
            //this.caption.Size = new System.Drawing.Size(584, 37);
            this.caption.TabIndex = 9999;
            ///
            ///McForm
            ///
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x233, 0x1cd);
            this.Controls.Add(this.caption);
            this.Controls.SetChildIndex(this.caption, 0);
            base.Name = "McForm";
            base.Padding = new Padding(2);
            //this.Text = "McForm";
            base.TransparencyKey = Color.Empty;
            base.ResumeLayout(false);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion

        #region override

        protected virtual void AdjustBounds(ref int width, ref int height)
        {
            this.AdjustBounds(ref width, ref height, BoundsSpecified.Size);
        }

        protected virtual void AdjustBounds(ref int width, ref int height, BoundsSpecified specified)
        {
            Structures.RECT lpRect = new Structures.RECT(0, 0, width, height);
            CreateParams param;
            param = this.CreateParams;
            Win32Methods.AdjustWindowRectEx(ref lpRect, param.Style, false, param.ExStyle);
            if (!DesignMode)
            {
                switch (FormBorderStyle)
                {
                    case FormBorderStyle.Fixed3D:
                    case FormBorderStyle.FixedDialog:
                    case FormBorderStyle.FixedSingle:
                        return;
                    case FormBorderStyle.FixedToolWindow:
                        height += 10;
                        width += 4;
                        return;
                }
            }
            if ((specified & BoundsSpecified.Height) == BoundsSpecified.Height)
            {
                height -= (lpRect.Rect.Height - height) - 1;
            }
            if ((specified & BoundsSpecified.Width) == BoundsSpecified.Width)
            {
                width -= (lpRect.Rect.Width - width) - 1;
            }
        }
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            this.formActive = true;
            base.Invalidate();
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            this.formActive = false;
            base.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            GraphicsPath path;
            this.CreateRegion();
            base.OnPaint(e);
            path = this.CreateGraphicsPath();

            if (base.DesignMode)
            {
                paintForm(e.Graphics, path, true);
            }
            else
            {
                paintForm(e.Graphics, path, this.formActive);
            }
            path.Dispose();
        }


        protected override void OnResize(EventArgs e)
        {
            this.CreateRegion();
            base.Invalidate();
            base.OnResize(e);
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if (this.BFLAG || (base.DesignMode && (((0 == 0) && ((specified & BoundsSpecified.Height) == BoundsSpecified.Height)) || ((specified & BoundsSpecified.Width) == BoundsSpecified.Width))))
            {
                if ((width != base.Width) || (height != base.Height))
                {
                    this.AdjustBounds(ref width, ref height, specified);
                }
                this.BFLAG = false;
            }
            base.SetBoundsCore(x, y, width, height, specified);
        }

        protected override void SetClientSizeCore(int x, int y)
        {
            if (!base.DesignMode)
            {
                this.BFLAG = true;
                this.AdjustBounds(ref x, ref y);
            }
            base.SetClientSizeCore(x, y);
        }


        protected override void WndProc(ref Message m)
        {
            int msg = m.Msg;
            if ((((uint)msg) & 0) == 0)
            {
                if (msg <= 0x86)
                {
                    if (msg <= 20)
                    {
                        goto Label_Break;//   break;
                    }
                    if (msg == 0x20)
                    {
                        base.WndProc(ref m);
                        return;
                    }
                    if (msg != 0x47)
                    {
                        switch (msg)
                        {
                            case 0x83:
                                base.WndProc(ref m);
                                goto Label_02D5;

                            case 0x84:
                                if (this.SetMsg(ref m))
                                {
                                    base.WndProc(ref m);
                                    return;
                                }
                                return;

                            case 0x85:
                                m.Result = IntPtr.Zero;
                                return;
                            case 0x86:
                                m.Result = new IntPtr(1);
                                return;
                        }
                        goto Label_002B;
                    }
                    //if ((((uint)msg) + ((uint)msg)) < 0)
                    //{
                    //    goto Label_02D5;
                    //}
                    goto Label_008E;
                Label_Break:
                    if (msg == 12)
                    {
                        base.DefWndProc(ref m);
                        this.Refresh();
                        return;
                    }
                    if (msg != 20)
                    {
                        goto Label_002B;
                    }
                    goto Label_01D6;
                }
                if (msg <= 0xae)
                {
                    switch (msg)
                    {
                        case 160:
                            base.WndProc(ref m);
                            return;

                        case 0xa1:
                            base.WndProc(ref m);
                            return;

                        case 0xa2:
                            base.WndProc(ref m);
                            return;
                    }
                    if (msg == 0xae)
                    {
                        return;
                    }
                    goto Label_002B;
                }
                goto Label_01AB;
            }
        //if (0 == 0)
        //{
        //? goto Label_01AB;
        //}
        //goto Label_015D;
        Label_002B:
            base.WndProc(ref m);
            return;
        Label_0083:
            this.BFLAG = true;
            base.WndProc(ref m);
            this.BFLAG = true;
            //if ((((uint)msg) + ((uint)msg)) <= uint.MaxValue)
            //{
            //    return;
            //}
            return;
        //return;
        Label_008E:
            if ((base.WindowState == FormWindowState.Minimized))
            {
                //if (((uint)msg) > uint.MaxValue)
                //{
                //    goto Label_01AB;
                //}
                //if (((uint)msg) > uint.MaxValue)
                //{
                //    goto Label_01D6;
                //}
                goto Label_0083;
            }
            //Label_00CF:
            if (base.WindowState != FormWindowState.Maximized)
            {
                base.WndProc(ref m);
                //if ((((uint)msg) | 0xff) != 0)
                //{
                    return;
                //}
                //goto Label_008E;
            }
            goto Label_0083;
        Label_01AB:
            if (msg == 0x117)
            {
                base.WndProc(ref m);
                goto Label_03BA;
            }
        //Label_01B7:
            if (msg != 0x210)
            {
                if (msg == 0x2a2)
                {
                    base.WndProc(ref m);
                    return;
                }
                goto Label_002B;
            }
            //if ((((uint)msg) + ((uint)msg)) >= 0)
            //{
            //    goto Label_015D;
            //}
            //return;

        //Label_015D:
            //if ((((uint)msg) | 4) == 0)
            //{
            //    goto Label_01B7;
            //}
            //if ((((uint)msg) & 0) == 0)
            //{
                base.WndProc(ref m);
                base.Invalidate();
            //}
            return;

        Label_01D6:
            m.Result = new IntPtr(1);
            return;
        Label_02D5:
            if (m.WParam == IntPtr.Zero)
            {
                Structures.RECT rect = new Structures.RECT(base.ClientRectangle);
                Structures.RECT rect2 = new Structures.RECT(rect.left, rect.top, rect.right, rect.bottom);
                Marshal.StructureToPtr(rect2, m.LParam, false);
                m.Result = IntPtr.Zero;
                return;
            }
            int gap = 0;
            if (!DesignMode)
            {
                FormWindowState state = this.WindowState;
                gap = state == FormWindowState.Normal && MAXFLAG ? -34 : 0;
                if (state == FormWindowState.Maximized)
                    MAXFLAG = true;
                //if (FormBorderStyle != FormBorderStyle.Sizable && FormBorderStyle != FormBorderStyle.SizableToolWindow)
                //{
                //    gap = 34;
                //}
            }
            Structures.NCCALCSIZE_PARAMS structure = (Structures.NCCALCSIZE_PARAMS)Marshal.PtrToStructure(m.LParam, typeof(Structures.NCCALCSIZE_PARAMS));
            if (((uint)msg) >= 0)
            {
                Structures.WINDOWPOS windowpos = (Structures.WINDOWPOS)Marshal.PtrToStructure(structure.lppos, typeof(Structures.WINDOWPOS));
                structure.rc0 = new Structures.RECT(windowpos.x, windowpos.y, windowpos.x + windowpos.cx, windowpos.y + windowpos.cy + gap);
                structure.rc1 = new Structures.RECT(windowpos.x, windowpos.y, windowpos.x + windowpos.cx, windowpos.y + windowpos.cy + gap);
                Marshal.StructureToPtr(structure, m.LParam, false);
                m.Result = new IntPtr(400);
                return;
            }
        Label_03BA:
            base.Invalidate(true);
        }
        #endregion

        #region private methods

        private void paintForm(Graphics g, GraphicsPath regionPath, bool Active)
        {
            //uint uiActive = Helper.BoolToUInt(Active);
            StyleLayout layout= base.LayoutManager.Layout;
            g.SmoothingMode = SmoothingMode.Default;
            switch (this.ControlLayout)
            {
                case ControlLayout.VistaLayout:
                case ControlLayout.XpLayout:
                case ControlLayout.Visual:
                    g.FillPath(new LinearGradientBrush(new Region(regionPath).GetBounds(g), layout.ColorBrush2Internal, layout.ColorBrush1Internal, LinearGradientMode.Vertical), regionPath);
                    break;
                default:
                    g.FillPath(new LinearGradientBrush(new Region(regionPath).GetBounds(g), layout.FormColor, layout.FormColor, LinearGradientMode.Vertical), regionPath);
                    break;
            }

            g.SmoothingMode = SmoothingMode.HighQuality;

            if (base.WindowState == FormWindowState.Minimized)
            {
                Rectangle miniRect = ClientRectangle;
                miniRect.X += 4;
                miniRect.Width -= 4;
                int top = (miniRect.Height - 16) / 2;
                _internalImages.Draw(g, miniRect.Right - 20, top, 3);
                _internalImages.Draw(g, miniRect.Right - 38, top, 2);
                _internalImages.Draw(g, miniRect.Right - 56, top, 1);
                miniRect.Width -= 60;
                LayoutManager.Layout.DrawTextAndImage(g, miniRect, Icon.ToBitmap(), HorizontalAlignment.Left, Text, Font, Active);
                //LayoutManager.DrawImage(g, miniRect, this.Icon.ToBitmap(), ContentAlignment.MiddleLeft, Active);
                //LayoutManager.DrawString(g, miniRect, ContentAlignment.MiddleRight, this.Text, this.Font);
            }

            if (this.FormBorderStyle == FormBorderStyle.None)
            {
                return;
            }
            if (!Active)
            {
                g.DrawPath(new Pen(layout.DisableColorInternal, 4f), regionPath);
            }
            else 
            {
                g.DrawPath(new Pen(layout.BorderColorInternal, 4f), regionPath);
            }
        }
        private int x31b92269760e6e6d(int lparam)
        {
            return (lparam >> 0x10);
        }

        private Point GetPoint(int lparam)//x3d0370f1e847fa3e
        {
            return new Point(this.xcb3a309bf0197241(lparam), this.x31b92269760e6e6d(lparam));
        }

        private void CreateRegion()
        {
            this.CreateGraphicsPath();
            GraphicsPath path = this.CreateGraphicsPath();
            Region region = new Region();
            region.MakeEmpty();
            region.Union(path);
            path.Widen(SystemPens.Control);
            new Region(path);
            region.Union(path);
            base.Region = region;
        }


        private GraphicsPath CreateGraphicsPath()
        {
            int radius=12;
            int radiusB=2;

            Rectangle clientRectangle = base.ClientRectangle;
            clientRectangle.Width--;
            clientRectangle.Height--;
            GraphicsPath path = new GraphicsPath();

            switch (this.ControlLayout)
            {
                case ControlLayout.VistaLayout:
                    radius = 12;
                    //top right
                    path.AddArc((clientRectangle.X + clientRectangle.Width) - radius, clientRectangle.Y, radius, radius, 270f, 90f);
                    //bottom right
                    path.AddArc((clientRectangle.X + clientRectangle.Width) - radius, (clientRectangle.Y + clientRectangle.Height) - radius, radius, radius, 0f, 90f);
                    //bottom left
                    path.AddArc(clientRectangle.X, (clientRectangle.Y + clientRectangle.Height) - radius, radius, radius, 90f, 90f);
                    //top left
                    path.AddArc(clientRectangle.X, clientRectangle.Y, 43, 43, 180f, 90f);
                    break;
                default:
                    radius = 12;// 8;
                    //top right
                    path.AddArc((clientRectangle.X + clientRectangle.Width) - radius, clientRectangle.Y, radius, radius, 270f, 90f);
                    //bottom right
                    path.AddArc((clientRectangle.X + clientRectangle.Width) - radiusB, (clientRectangle.Y + clientRectangle.Height) - radiusB, radiusB, radiusB, 0f, 90f);
                    //bottom left
                    path.AddArc(clientRectangle.X, (clientRectangle.Y + clientRectangle.Height) - radiusB, radiusB, radiusB, 90f, 90f);
                    //top left
                    path.AddArc(clientRectangle.X, clientRectangle.Y, radius, radius, 180f, 90f);
                    break;
            }
            path.CloseAllFigures();
            return path;
        }


        private bool SetMsg(ref Message msg)
        {
            Rectangle clientRectangle;
            Point p = this.GetPoint((int)msg.LParam);
            p = base.PointToClient(p);
            clientRectangle = base.ClientRectangle;
            goto Label_035A;
        Label_00B0:
            clientRectangle.Y = base.ClientRectangle.Bottom - 3;
            clientRectangle.Width -= 4;
            if (!clientRectangle.Contains(p))
            {
                clientRectangle = base.ClientRectangle;
                clientRectangle.Width = 3;
                clientRectangle.X = base.ClientRectangle.Right - 3;
                clientRectangle.Y += 2;
                clientRectangle.Height -= 4;
                if (!clientRectangle.Contains(p))
                {
                    return true;
                }
                msg.Result = (IntPtr)11;
                return false;
            }
            msg.Result = (IntPtr)15;
            return false;
        Label_0105:
            clientRectangle = base.ClientRectangle;
            clientRectangle.Height = 3;
            clientRectangle.X += 2;
            goto Label_00B0;
        Label_0116:
            msg.Result = (IntPtr)12;
            return false;
        Label_0155:
            if (clientRectangle.Contains(p))
            {
                msg.Result = (IntPtr)10;
                return false;
            }
            clientRectangle = base.ClientRectangle;
            clientRectangle.Height = 3;
            clientRectangle.X += 2;
            clientRectangle.Width -= 4;
            if (clientRectangle.Contains(p))
            {
                goto Label_0116;
            }
            goto Label_0105;
        Label_0170:
            clientRectangle.Height -= 4;
            goto Label_0155;
        Label_0220:
            clientRectangle.Width = 6;
            clientRectangle.Y = base.ClientRectangle.Bottom - 6;
            clientRectangle.Height = 6;
            if (clientRectangle.Contains(p))
            {
                msg.Result = (IntPtr)0x10;
                return false;
            }
            clientRectangle = base.ClientRectangle;
            clientRectangle.Width = 3;
            clientRectangle.Y += 2;
            goto Label_0170;
        Label_029D:
            clientRectangle = base.ClientRectangle;
            clientRectangle.Width = 6;
            clientRectangle.Height = 6;
            clientRectangle.X = base.ClientRectangle.Right - 6;
            clientRectangle.Y = base.ClientRectangle.Bottom - 6;
            if (clientRectangle.Contains(p))
            {
                msg.Result = (IntPtr)0x11;
                return false;
            }
            clientRectangle = base.ClientRectangle;
            goto Label_0220;
        Label_02B7:
            msg.Result = (IntPtr)14;
            return false;
        Label_02D9:
            clientRectangle = base.ClientRectangle;
            clientRectangle.Width = 6;
            //Label_02E8:
            clientRectangle.Height = 6;
            clientRectangle.X = base.ClientRectangle.Right - 6;
            if (clientRectangle.Contains(p))
            {
                goto Label_02B7;
            }
            goto Label_029D;
        Label_0313:
            if (clientRectangle.Contains(p))
            {
                msg.Result = (IntPtr)13;
                return false;
            }
            goto Label_02D9;
        Label_035A:
            clientRectangle = base.ClientRectangle;
            clientRectangle.Width = 0x16;
            clientRectangle.Height = 0x16;
            goto Label_0313;
        }

        private int xcb3a309bf0197241(int lparam)
        {
            return (lparam & 0xffff);
        }
        #endregion

        #region  Properties

        [Browsable(false)]//,EditorBrowsable(EditorBrowsableState.Advanced)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public McCaptionForm Caption
        {
            get { return this.caption; }
        }

        [DefaultValue(ControlLayout.VistaLayout), RefreshProperties(RefreshProperties.All)]
        public override ControlLayout ControlLayout
        {
            get { return base.ControlLayout; }
            set
            {
                //if (m_ControlLayout != value)
                //{
                //ControlLayout oldValue = base.ControlLayout;

                base.ControlLayout = value;
                //switch (value)
                //{
                //    case ControlLayout.System:
                //        Radius = 12;
                //        Padding = new Padding(4);
                //        //this.caption.ShowFormBox = false;
                //        break;
                //    case ControlLayout.Visual:
                //        Radius = 12;
                //        Padding = new Padding(4);
                //        //this.FormBorderStyle = FormBorderStyle.None;
                //        break;
                //    case ControlLayout.XpLayout:
                //        Radius = 12;
                //        Padding = new Padding(4);
                //        break;
                //    case ControlLayout.VistaLayout:
                //        Radius = 12;
                //        Padding = new Padding(4);
                //        break;
                //    default:
                //        Radius = 12;
                //        Padding = new Padding(4);
                //        break;
                //}
                this.caption.ControlLayout = value;


                //if (oldValue == ControlLayout.System)
                //{
                //    this.caption.SetControlBox (this);
                //}
                //if (DesignMode && oldValue!=value && value == ControlLayout.System)
                //{
                //    Nistec.WinForms.NotifyWindow.ShowNotifyMsg("Nistec", "You should rebuild application");
                //    //Application.EnableVisualStyles();
                //    this.RecreateHandle();
                //}
                //OnFormLayoutChanged(EventArgs.Empty);
                Invalidate();
            }
            //}
        }

        [DefaultValue(true), Browsable(false)]
        internal bool InheritStyle
        {
            get
            {
                return this.mInheritStyleFromParent;
            }
            set
            {
                this.mInheritStyleFromParent = value;
            }
        }

        //[DefaultValue(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        //public bool ShowCaptionButton
        //{
        //    get
        //    {
        //        return this.caption.ShowCaptionButton;
        //    }
        //    set
        //    {
        //        this.caption.ShowCaptionButton = value;
        //        //this.x3bacd662c5d69be4("ShowCaptionButton");
        //    }
        //}

        [Browsable(true), Category("Caption"), DefaultValue(true)]
        public bool CaptionVisible
        {
            get { return this.caption.Visible; }
            set
            {
                //this.captionVisible = value;
                this.caption.Visible = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), DefaultValue(true)]
        public bool ShowTitleBar
        {
            get
            {
                return this.caption.ShowTitleBar;
            }
            set
            {
                this.caption.ShowTitleBar = value;
                //this.x3bacd662c5d69be4("ShowTitleBar");
            }
        }
        //public RibbonStyle Style
        //{
        //    get
        //    {
        //        return this.mStyle;
        //    }
        //    set
        //    {
        //        this.mStyle = value;
        //        this.mCurrentRenderer = BasePainter.GetPainter(this, value);
        //        base.Invalidate(true);
        //    }
        //}

        #endregion
    }
}
