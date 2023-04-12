using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Security.Permissions;

using MControl.Util;
using MControl.Drawing;
using System.Threading;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace MControl.WinForms
{
    //[Designer( typeof(Design.McFormDesigner))]
    public partial class McForm : FormBase
    {
        #region ctor

        static McForm()
		{
			// Create a strip of images by loading an embedded bitmap resource
			_internalImages = DrawUtils.LoadBitmapStrip(Type.GetType("MControl.WinForms.McForm"),
				"MControl.WinForms.Images.ImagesControlBox.bmp",
				new Size(16, 16),new Point(0,0));
		}

        public McForm()
        {
            //m_ControlLayout = ControlLayout.Visual;
            InitializeComponent();
            InitRibbonForm();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            //SetClientSize();
            if (this.caption.ShowFormBox != base.ControlBox)
            {
                this.ControlBox = base.ControlBox;
            }
            //if (this.caption.Text != base.Text)
            //{
            //    this.caption.Text = base.Text;
            //}
        }

        //private void SetClientSize()
        //{
        //    if (DesignMode) return;

        //    switch (ControlLayout)
        //    {
        //        case ControlLayout.System:
        //            this.caption.ControlLayout = ControlLayout.Visual;
        //            caption.ShowFormBox = false;
        //            clientSetting = false;
        //            SetClientSizeCore(this.ClientSize.Width+2, this.ClientSize.Height + 34);
        //            break;
        //        case ControlLayout.Visual:
        //            //clientSetting = false;
        //            //SetClientSizeCore(this.ClientSize.Width, this.ClientSize.Height + 34);
        //            break;
        //        case ControlLayout.XpLayout:
        //        case ControlLayout.VistaLayout:
        //            break;
        //        default:
        //            //this.caption.ControlLayout = m_ControlLayout;
        //            //caption.ShowFormBox = false;
        //            //clientSetting = false;
        //            //SetClientSizeCore(this.ClientSize.Width + 2, this.ClientSize.Height + 34);
        //            break;
        //    }
        //}

        //private void SetControlLayout()
        //{
        //    switch (ControlLayout)
        //    {
        //        case ControlLayout.System:
        //            Radius = 1;
        //            this.caption.ControlLayout = ControlLayout.Visual;
        //            //this.caption.ShowFormBox = false;
        //            break;
        //        case ControlLayout.Visual:
        //        case ControlLayout.XpLayout:
        //        case ControlLayout.VistaLayout:
        //            Radius = 8;
        //            this.caption.ControlLayout = ControlLayout;
        //            break;
        //        default:
        //            Radius = 1;
        //            this.caption.ControlLayout = ControlLayout;
        //            break;
        //    }
        //    //if (oldValue == ControlLayout.System)
        //    //{
        //    //    this.caption.SetControlBox (this);
        //    //}
        //    //if (DesignMode && value == ControlLayout.System)
        //    //{
        //    //    //Application.EnableVisualStyles();
        //    //    this.RecreateHandle();
        //    //}
        //    OnFormLayoutChanged(EventArgs.Empty);
        //    Invalidate();

        //}

        #endregion

        #region Members

        static ImageList _internalImages = null;
        bool clientSetting = false;
        //private ControlLayout m_ControlLayout;
        protected MControl.WinForms.McCaption caption;
        public event EventHandler FormLayoutChanged;

        #endregion

        #region Caption Property

        private bool captionVisible = true;

        [Browsable(false)]//,EditorBrowsable(EditorBrowsableState.Advanced)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public McCaption Caption
        {
            get { return this.caption; }
        }


        //		[Category("Caption")]
        //		public IStyle CaptionPainter
        //		{
        //			get {return Caption.StylePainter;}
        //			set 
        //			{
        //					Caption.StylePainter = value;
        //					this.Invalidate(true);
        //			}
        //		}
        //		[Category("Caption"),DefaultValue(null)]
        //		public Image CaptionImage
        //		{
        //			get { return this.Caption.Picture; }
        //            
        //			set
        //			{
        //				this.Caption.Picture = value;
        //				this.Invalidate();
        //			}
        //		}
        //
        //		[Category("Caption"),DefaultValue(null)]
        //		public ImageList CaptionImageList
        //		{
        //			get { return this.Caption.ImageList; }
        //		
        //			set 
        //			{ 
        //				this.Caption.ImageList = value; 
        //			}
        //		}
        //
        //		[Category("Caption"),Description("ButtonImageIndex"), Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design", typeof(System.Drawing.Design.UITypeEditor)), DefaultValue(-1), Localizable(true), TypeConverter(typeof(ImageIndexConverter))]
        //		public int CaptionImageIndex
        //		{
        //			get
        //			{
        //				return this.Caption.ImageIndex;
        //			}
        //			set
        //			{
        //				this.Caption.ImageIndex=value;
        //				this.Caption.Invalidate();
        //			}
        //		}
        //
        //		[Category("Caption"),DefaultValue(true)]
        //		[Description("Show Main title Picture")]
        //		public bool CaptionShowImage
        //		{
        //			get { return this.Caption.ShowPicture; }
        //            
        //			set
        //			{
        //				this.Caption.ShowPicture = value;
        //				this.Caption.Invalidate();
        //			}
        //		}

        //[Category("Caption")]
        //[Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public string CaptionText
        //{
        //    get { return this.Caption.Text; }
        //    set
        //    {
        //        this.Caption.Text = value;
        //    }
        //}

        //[Category("Caption")]
        //[Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public string CaptionSubText
        //{
        //    get { return this.Caption.SubText; }
        //    set
        //    {
        //        this.Caption.SubText = value;
        //    }
        //}

        //[Category("Caption")]
        //[Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public int CaptionHeight
        //{
        //    get { return this.Caption.Height; }
        //    // set { this.Caption.Height = value; }
        //}

        [Browsable(true), Category("Caption"), DefaultValue(true)]
        public bool CaptionVisible
        {
            get { return captionVisible; }// this.Caption.Visible; }
            set
            {
                this.captionVisible = value;
                this.Caption.Visible = value;
            }
        }
        //
        //
        //		[Category("Caption")]
        //		[Description("The LinearGradientBrush angle between 0 and 360")]
        //		public float CaptionGradiaentAngle
        //		{
        //			get { return this.Caption.gradiaentAngle;}
        //            
        //			set
        //			{
        //				this.Caption.gradiaentAngle=value;
        //				this.Invalidate();
        //			}
        //		}
        //
        //		[Category("Caption")]
        //		public virtual ControlLayout CaptionControlLayout 
        //		{
        //			get {return Caption.ControlLayout;}
        //			set
        //			{
        //				Caption.ControlLayout=value;
        //				this.Invalidate ();
        //			}
        //
        //		}
        //
        //		[Category("Caption"), Description("Caption BorderStyle")]
        //		public virtual BorderStyle CaptionBorderStyle
        //		{
        //			get
        //			{
        //				return Caption.BorderStyle;
        //			}
        //			set
        //			{
        //					Caption.BorderStyle = value;
        //					this.Invalidate();
        //			}
        //		}

        #endregion

        #region Region

        // Fields
        //private IContainer components = null;
        const int padding = 3;
        const int Radius = 8;
       
        private bool BFLAG;
        private bool formActive;

        [DllImport("user32.dll")]
        public static extern bool AdjustWindowRectEx(ref Structures.RECT lpRect, int dwStyle, bool bMenu, int dwExStyle);


        // Methods
        private void InitRibbonForm()
        {
            this.InitStyle();
            this.InitComponents();
            //this.CreateRegion();
        }

        protected virtual void AdjustBounds(ref int width, ref int height)
        {
            this.AdjustBounds(ref width, ref height, BoundsSpecified.Size);
        }

        protected virtual void AdjustBounds(ref int width, ref int height, BoundsSpecified specified)
        {
            //if (ControlLayout == ControlLayout.System)
            //{
            //    return;
            //}

            Structures.RECT lpRect = new Structures.RECT(0, 0, width, height);
            CreateParams param;
            param = this.CreateParams;
            AdjustWindowRectEx(ref lpRect, param.Style, false, param.ExStyle);
            if ((specified & BoundsSpecified.Height) == BoundsSpecified.Height)
            {
                height -= (lpRect.Rect.Height - height);// -1;
            }
            while ((specified & BoundsSpecified.Width) == BoundsSpecified.Width)
            {
                width -= (lpRect.Rect.Width - width);// -1;
                break;
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
            //if (ControlLayout == ControlLayout.System)
            //{
            //    e.Graphics.Clear(BackColor);
            //    base.OnPaint(e);
            //    return;
            //}
            GraphicsPath path;
            this.CreateRegion();
            base.OnPaint(e);
            path = this.CreateGraphicsPath();// x90190202b46d8c61();

            if (base.DesignMode)
            {
                this.paintRibbonForm(e.Graphics, path, true);
            }
            else
            {
                this.paintRibbonForm(e.Graphics, path, this.formActive);
            }
            path.Dispose();
        }


        private int BoolToInt(bool value)
        {
            //if (value) return 1;
            //return 0;
            return value ? 1 : 0;
        }
        private uint BoolToUInt(bool value)
        {
            return value ? (uint)1 : (uint)0;
        }

        public void paintRibbonForm(Graphics g, GraphicsPath regionPath, bool Active)
        {
            uint uiActive = BoolToUInt(Active);

            g.SmoothingMode = SmoothingMode.Default;

            switch (ControlLayout)
            {
                case ControlLayout.VistaLayout:
                case ControlLayout.XpLayout:
                    g.FillPath(new LinearGradientBrush(new Region(regionPath).GetBounds(g), McStyleLayout.Layout.ColorBrush2Internal, McStyleLayout.Layout.ColorBrush1Internal, LinearGradientMode.Vertical), regionPath);
                    break;
                default:
                    g.FillPath(new SolidBrush(McStyleLayout.Layout.FormColor), regionPath);
                    break;
            }

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
                McStyleLayout.Layout.DrawTextAndImage(g, miniRect, Icon.ToBitmap(), HorizontalAlignment.Left, Text, Font, Active);
                //McStyleLayout.DrawImage(g, miniRect, this.Icon.ToBitmap(), ContentAlignment.MiddleLeft, Active);
                //McStyleLayout.DrawString(g, miniRect, ContentAlignment.MiddleRight, this.Text, this.Font);
            }

            g.SmoothingMode = SmoothingMode.HighQuality;

            if (!Active)
            {
                g.DrawPath(new Pen(McStyleLayout.Layout.DisableColorInternal, 4f), regionPath);
            }
            else
            {
                g.DrawPath(new Pen(McStyleLayout.Layout.BorderColorInternal, 4f), regionPath);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            //if (!DesignMode && ControlLayout == ControlLayout.System)
            //{
            //    base.OnResize(e);
            //    return;
            //}
            if (!DesignMode && IsHandleCreated)//&& ControlLayout != ControlLayout.System)
            {
                this.CreateRegion();//x5a964354ec662d0a();
                base.Invalidate();
            }
            base.OnResize(e);
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            //if (ControlLayout == ControlLayout.System)
            //{
            //    base.SetBoundsCore(x, y, width, height, specified);
            //    return;
            //}
            if (this.BFLAG || (base.DesignMode && ((((specified & BoundsSpecified.Height) == BoundsSpecified.Height)) || ((specified & BoundsSpecified.Width) == BoundsSpecified.Width))))
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
            int yadd = 0;
            if (!base.DesignMode)
            {
                //if (ControlLayout == ControlLayout.System)
                //{
                //    base.SetClientSizeCore(x, y);
                //    return;
                //}
                if (this.MdiParent != null)
                {
                    if (ControlLayout == ControlLayout.Visual)
                    {
                        if (this.FormBorderStyle != FormBorderStyle.None)
                            this.FormBorderStyle = FormBorderStyle.None;

                    }
                    if (this.FormBorderStyle == FormBorderStyle.None)// ControlLayout == ControlLayout.Visual)
                    {
                        this.BFLAG = true;
                        this.AdjustBounds(ref x, ref y);

                        yadd = 34;
                    }
                }
                //bool adjust = false;
                else if (!clientSetting)// && ControlLayout != ControlLayout.System)
                {
                    this.BFLAG = true;
                    this.AdjustBounds(ref x, ref y);
                    //if (this.FormBorderStyle == FormBorderStyle.None)
                    //    yadd = 36;
                    clientSetting = true;
                    //adjust=true;
                    //if (ControlLayout == ControlLayout.System)
                    //    y += 32;
                }
            }
            base.SetClientSizeCore(x, y + yadd);
        }
        
 
        protected override void WndProc(ref Message m)
        {

            //if (IsHandleCreated && ControlLayout == ControlLayout.System)
            //{
            //    base.WndProc(ref m);
            //    return;
            //}
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
                                if (this.SetClientRectangle(ref m))
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
                    if ((((uint)msg) + ((uint)msg)) < 0)
                    {
                        goto Label_02D5;
                    }
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
                    if (((((uint)msg) + ((uint)msg)) <= uint.MaxValue) && (msg == 0xae))
                    {
                        return;
                    }
                    goto Label_002B;
                }
                goto Label_01AB;
            }
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
        Label_008E:
            //if (base.WindowState == FormWindowState.Minimized)
            //{
            //    //if (((uint)msg) > uint.MaxValue)
            //    //{
            //    //    goto Label_01AB;
            //    //}
            //    //if (((uint)msg) > uint.MaxValue)
            //    //{
            //    //    goto Label_01D6;
            //    //}
            //    //goto Label_0083;
            //    this.BFLAG = true;
            //    base.WndProc(ref m);
            //    this.BFLAG = true;
            //    return;
            //}
            //if (base.WindowState != FormWindowState.Maximized)
            //{

            ////    if(!BSTATE)
            ////    base.WndProc(ref m);
            ////BSTATE = false;
            ////return;
            //    //return;
            //    //if ((((uint)msg) | 0xff) != 0)
            //    //{
            //    //    return;
            //    //}
            //    //goto Label_008E;
            //}
            goto Label_0083;
        Label_015D:
            if ((((uint)msg) | 4) == 0)
            {
                goto Label_01B7;
            }
            if ((((uint)msg) & 0) == 0)
            {
                base.WndProc(ref m);
                base.Invalidate();
            }
            return;
        Label_01AB:
            if (msg == 0x117)
            {
                base.WndProc(ref m);
                goto Label_03BA;
            }
        Label_01B7:
            if (msg != 0x210)
            {
                if (msg == 0x2a2)
                {
                    base.WndProc(ref m);
                    return;
                }
                goto Label_002B;
            }
            if ((((uint)msg) + ((uint)msg)) >= 0)
            {
                goto Label_015D;
            }
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
            Structures.NCCALCSIZE_PARAMS structure = (Structures.NCCALCSIZE_PARAMS)Marshal.PtrToStructure(m.LParam, typeof(Structures.NCCALCSIZE_PARAMS));
            if (((uint)msg) >= 0)
            {
                Structures.WINDOWPOS windowpos = (Structures.WINDOWPOS)Marshal.PtrToStructure(structure.lppos, typeof(Structures.WINDOWPOS));
                structure.rc0 = new Structures.RECT(windowpos.x, windowpos.y, windowpos.x + windowpos.cx, windowpos.y + windowpos.cy);
                structure.rc1 = new Structures.RECT(windowpos.x, windowpos.y, windowpos.x + windowpos.cx, windowpos.y + windowpos.cy);
                Marshal.StructureToPtr(structure, m.LParam, false);
                m.Result = new IntPtr(400);
                return;
            }
        Label_03BA:
            base.Invalidate(true);
        }

        private int x31b92269760e6e6d(int lparam)
        {
            return (lparam >> 0x10);
        }

        private Point GetPoint(int lparam)//x3d0370f1e847fa3e
        {
            return new Point(this.xcb3a309bf0197241(lparam), this.x31b92269760e6e6d(lparam));
        }

        private void CreateRegion()// x5a964354ec662d0a()
        {
            this.CreateGraphicsPath();//x90190202b46d8c61();
            GraphicsPath path = this.CreateGraphicsPath();//x90190202b46d8c61();
            Region region = new Region();
            region.MakeEmpty();
            region.Union(path);
            path.Widen(SystemPens.Control);
            new Region(path);
            region.Union(path);
            base.Region = region;
        }


        private void InitComponents()// x85601834555fb7d5()
        {
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            //base.ClientSize = new Size(0x233, 0x1cd);
            //base.Name = "RibbonForm";
            base.Padding = new Padding(padding);
            //this.Text = "RibbonForm";
            base.TransparencyKey = Color.Empty;
            base.ResumeLayout(false);
        }

        private GraphicsPath CreateGraphicsPath()// x90190202b46d8c61()
        {
            //Radius = 12;
            Rectangle clientRectangle = base.ClientRectangle;
            clientRectangle.Width--;
            clientRectangle.Height--;
            GraphicsPath path = new GraphicsPath();
            //top right
            path.AddArc((clientRectangle.X + clientRectangle.Width) - Radius, clientRectangle.Y, Radius, Radius, 270f, 90f);
            //bottom right
            //path.AddArc((clientRectangle.X + clientRectangle.Width) - Radius, (clientRectangle.Y + clientRectangle.Height) - Radius, Radius, Radius, 0f, 90f);
            path.AddArc((clientRectangle.X + clientRectangle.Width) - 1, (clientRectangle.Y + clientRectangle.Height) - 1, 1, 1, 0f, 90f);
            //bottom left
            //path.AddArc(clientRectangle.X, (clientRectangle.Y + clientRectangle.Height) - Radius, Radius, Radius, 90f, 90f);
            path.AddArc(clientRectangle.X, (clientRectangle.Y + clientRectangle.Height) - 1, 1, 1, 90f, 90f);
            //top left
            path.AddArc(clientRectangle.X, clientRectangle.Y, Radius, Radius, 180f, 90f);

            path.CloseAllFigures();
            return path;
        }

        private void InitStyle()//xc256bcbd35c60777()
        {
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.SetStyle(ControlStyles.Selectable, true);
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            base.SetStyle(ControlStyles.UserPaint, true);
        }

        private bool SetClientRectangle(ref Message msg)
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
            if (15 != 0)
            {
                clientRectangle.Height = 6;
                if ((0 != 0) || clientRectangle.Contains(p))
                {
                    msg.Result = (IntPtr)0x10;
                    return false;
                }
                clientRectangle = base.ClientRectangle;
                clientRectangle.Width = 3;
                clientRectangle.Y += 2;
            }
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

        #region properties

        [DefaultValue(ControlLayout.System),RefreshProperties ( RefreshProperties.All)]
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
                    //    MControl.WinForms.NotifyWindow.ShowNotifyMsg("MControl", "You should rebuild application");
                    //    //Application.EnableVisualStyles();
                    //    this.RecreateHandle();
                    //}
                    OnFormLayoutChanged(EventArgs.Empty);
                    Invalidate();
                }
            //}
        }


        //[RefreshProperties(RefreshProperties.All)]
        public new FormBorderStyle FormBorderStyle
        {
            get
            {
                return base.FormBorderStyle;
            }
            set
            {
                base.FormBorderStyle = value;
                switch (value)
                {

                    case FormBorderStyle.Sizable:
                    case FormBorderStyle.FixedSingle:
                    case FormBorderStyle.SizableToolWindow:
                    case FormBorderStyle.Fixed3D:
                        break;
                    case FormBorderStyle.FixedDialog:
                    case FormBorderStyle.FixedToolWindow:
                        this.MaximizeBox = false;
                        this.MinimizeBox = false;
                        break;
                    case FormBorderStyle.None:
                        //this.CaptionVisible = false;
                        break;
                }
            }
        }

        [RefreshProperties(RefreshProperties.All)]
        public new bool ControlBox
        {
            get
            {
                return base.ControlBox;
            }
            set
            {
                base.ControlBox = value;
                if (ControlLayout == ControlLayout.System)
                {
                    this.caption.ShowFormBox = false;
                }
                else
                {
                    this.caption.ShowFormBox = value;
                }
            }
        }

        public new bool MaximizeBox
        {
            get
            {
                return base.MaximizeBox;
            }
            set
            {
                base.MaximizeBox = value;
                this.caption.ShowMaximize = value;
            }
        }
        public new bool MinimizeBox
        {
            get
            {
                return base.MinimizeBox;
            }
            set
            {
                base.MinimizeBox = value;
                this.caption.ShowMinimize = value;
            }
        }

        public new string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                this.caption.Text = value;
            }
        }
        #endregion

        #region override
        protected virtual void OnFormLayoutChanged(EventArgs e)
        {
            if (FormLayoutChanged != null)
                FormLayoutChanged(this, e);
        }

        //protected override bool ShouldShowSizingGrip()
        //{
        //    //return showResizing && this.FormBorderStyle == FormBorderStyle.None;
        //    return showResizing && this.ControlLayout != ControlLayout.System;
        //}
        #endregion

        #region old
        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);
        //    Rectangle rect=new Rectangle(0,0,this.Width,this.caption.Height);
        //    using (SolidBrush sbBack = new SolidBrush(BackColor))
        //    {
        //        e.Graphics.FillRectangle(sbBack, rect);
        //        //e.Graphics.DrawLine(McStyleLayout.GetPenBorder(),rect.X,rect.Bottom,rect.Right,rect.Bottom);
        //        //e.Graphics.DrawLine(McStyleLayout.GetPenBorder(), rect.X, rect.Bottom-2, rect.Right, rect.Bottom-2);
        //    }
        //}

        //private ControlLayout FormLayoutToControlLayout()
        //{

        //    switch (FormLayout)
        //    {
        //        case FormLayout.Flat:
        //        case FormLayout.FlatDialog:
        //        case FormLayout.FlatSizeable:
        //            Radius = 1;
        //            return ControlLayout.Flat;
        //        case FormLayout.VistaSizeable:
        //        case FormLayout.VistaDialog:
        //        case FormLayout.VistaLayout:
        //            Radius = 8;
        //            return ControlLayout.VistaLayout;
        //        case FormLayout.VisualSizeable:
        //        case FormLayout.VisualDialog:
        //        case FormLayout.Visual:
        //            Radius = 1;
        //            return ControlLayout.Visual;
        //        case FormLayout.XpSizeable:
        //        case FormLayout.XpDialog:
        //        case FormLayout.XpLayout:
        //            Radius = 8;
        //            return ControlLayout.XpLayout;
        //        case FormLayout.Sytem:
        //            Radius = 1;
        //            return ControlLayout.System;
        //        default:
        //            Radius = 1;
        //            return ControlLayout.Visual;
        //    }

        //}

        //protected override void OnFormLayoutChanged(EventArgs e)
        //{
        //    FormLayoutSettings();
        //    base.OnFormLayoutChanged(e);
        //}

        //private void FormLayoutSettings()
        //{
        //    this.caption.ControlLayout = FormLayoutToControlLayout();

        //    switch (FormLayout)
        //    {
        //        case FormLayout.FlatSizeable:
        //        case FormLayout.VistaSizeable:
        //        case FormLayout.VisualSizeable:
        //        case FormLayout.XpSizeable:
        //            this.caption.Visible = true;
        //            //this.FormBorderStyle = FormBorderStyle.None;
        //            this.caption.ShowFormBox = true;
        //            this.caption.ShowClose = true;
        //            this.caption.ShowResore = true;
        //            this.caption.ShowMinimize = true;
        //            drowBorder = true;
        //            break;
        //        case FormLayout.FlatDialog:
        //        case FormLayout.VistaDialog:
        //        case FormLayout.VisualDialog:
        //        case FormLayout.XpDialog:
        //            this.caption.Visible = true;
        //            //this.FormBorderStyle = FormBorderStyle.None;
        //            this.caption.ShowFormBox = true;
        //            this.caption.ShowClose = true;
        //            this.caption.ShowResore = false;
        //            this.caption.ShowMinimize = false;
        //            drowBorder = true;
        //            break;
        //        case FormLayout.Flat:
        //        case FormLayout.VistaLayout:
        //        case FormLayout.Visual:
        //        case FormLayout.XpLayout:
        //            this.caption.Visible = true;
        //            //this.FormBorderStyle = FormBorderStyle.None;
        //            drowBorder = true;
        //            break;
        //        case FormLayout.Sytem:
        //            //if( this.FormBorderStyle == FormBorderStyle.None)
        //            //    this.FormBorderStyle = FormBorderStyle.Sizable;
        //            this.caption.ShowFormBox = false;
        //            this.caption.Visible = false;
        //            drowBorder = false;
        //            break;
        //        default:// case  FormLayout.Default:
        //            //this.FormBorderStyle = FormBorderStyle.Sizable;
        //            this.caption.Visible = true;
        //            this.caption.ShowFormBox = false;
        //            drowBorder = false;
        //            break;
        //    }
        //    //base.FormLayoutSettings();

        //}


        //protected override void FormLayoutSettings()
        //{

        //    switch (FormLayout)
        //    {
        //        case FormLayout.VisualXpSizeable:
        //            this.caption.Visible = true;
        //            this.caption.ControlLayout = ControlLayout.XpLayout;
        //            this.FormBorderStyle = FormBorderStyle.None;
        //            this.caption.ShowFormBox = true;
        //            this.caption.ShowClose = true;
        //            this.caption.ShowResore = true;
        //            this.caption.ShowMinimize = true;
        //            DrowBorder = true;
        //            break;
        //        case FormLayout.VisualXp:
        //            this.caption.Visible = true;
        //            this.caption.ControlLayout = ControlLayout.XpLayout;
        //            this.FormBorderStyle = FormBorderStyle.None;
        //            //this.caption.ShowFormBox = true;
        //            //this.caption.ShowClose = true;
        //            //this.caption.ShowResore = true;
        //            //this.caption.ShowMinimize = true;
        //            DrowBorder = true;
        //            break;
        //        case FormLayout.VisualSizeable:
        //            this.caption.Visible = true;
        //            this.caption.ControlLayout = ControlLayout.Visual;
        //            this.FormBorderStyle = FormBorderStyle.None;
        //            this.caption.ShowFormBox = true;
        //            this.caption.ShowClose = true;
        //            this.caption.ShowResore = true;
        //            this.caption.ShowMinimize = true;
        //            DrowBorder = true;
        //            break;
        //        case FormLayout.Visual:
        //            this.caption.Visible = true;
        //            this.caption.ControlLayout = ControlLayout.Visual;
        //            this.FormBorderStyle = FormBorderStyle.None;
        //            //this.caption.ShowFormBox = true;
        //            //this.caption.ShowClose = true;
        //            //this.caption.ShowResore = true;
        //            //this.caption.ShowMinimize = true;
        //            DrowBorder = true;
        //            break;
        //        case FormLayout.DialogXpSizeable:
        //        case FormLayout.DialogXp:
        //            this.caption.Visible = true;
        //            this.caption.ControlLayout = ControlLayout.XpLayout;
        //            this.FormBorderStyle = FormBorderStyle.None;
        //            this.caption.ShowFormBox = true;
        //            this.caption.ShowClose = true;
        //            this.caption.ShowResore = false;
        //            this.caption.ShowMinimize = false;
        //            DrowBorder = true;
        //            break;
        //        case FormLayout.DialogSizeable:
        //        case FormLayout.Dialog:
        //            this.caption.Visible = true;
        //            this.caption.ControlLayout = ControlLayout.Visual;
        //            this.FormBorderStyle = FormBorderStyle.None;
        //            this.caption.ShowFormBox = true;
        //            this.caption.ShowClose = true;
        //            this.caption.ShowResore = false;
        //            this.caption.ShowMinimize = false;
        //            DrowBorder = true;
        //            break;
        //        case FormLayout.FlatSizeable:
        //        case FormLayout.Flat:
        //            this.caption.Visible = false;
        //            this.caption.ShowFormBox = false;
        //            this.FormBorderStyle = FormBorderStyle.None;
        //            DrowBorder = true;
        //            break;
        //        case FormLayout.Sytem:
        //            //if( this.FormBorderStyle == FormBorderStyle.None)
        //            //    this.FormBorderStyle = FormBorderStyle.Sizable;
        //            this.caption.ShowFormBox = false;
        //            this.caption.Visible = false;
        //            DrowBorder = false;
        //            break;
        //        default:// case  FormLayout.Default:
        //            //this.FormBorderStyle = FormBorderStyle.Sizable;
        //            this.caption.Visible = true;
        //            this.caption.ShowFormBox = false;
        //            DrowBorder = false;
        //            break;
        //    }
        //    base.FormLayoutSettings();

        //}
        #endregion

    }
}