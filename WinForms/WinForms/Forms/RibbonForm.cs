using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace mControl.WinCtl.Forms
{
    public class RibbonForm : FormBase
    {
        // Fields
        private IContainer components = null;
        private bool BFLAG;
        private const int Radius=8;
        private bool formActive;

        [DllImport("user32.dll")]
        public static extern bool AdjustWindowRectEx(ref Structures.RECT lpRect, int dwStyle, bool bMenu, int dwExStyle);
 

        // Methods
        public RibbonForm()
        {
            this.InitStyle();// xc256bcbd35c60777();
            this.InitComponents();// x85601834555fb7d5();
            this.CreateRegion();// x5a964354ec662d0a();
        }

        protected virtual void AdjustBounds(ref int width, ref int height)
        {
            this.AdjustBounds(ref width, ref height, BoundsSpecified.Size);
        }

        protected virtual void AdjustBounds(ref int width, ref int height, BoundsSpecified specified)
        {
            Structures.RECT lpRect = new Structures.RECT(0, 0, width, height);
            CreateParams param;
            param = this.CreateParams;
            AdjustWindowRectEx(ref lpRect, param.Style, false, param.ExStyle);
            if ((specified & BoundsSpecified.Height) == BoundsSpecified.Height)
            {
                height -= (lpRect.Rect.Height - height) - 1;
            }
            while ((specified & BoundsSpecified.Width) == BoundsSpecified.Width)
            {
                width -= (lpRect.Rect.Width - width) - 1;
                break;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
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
            this.CreateRegion();//x5a964354ec662d0a();
            base.OnPaint(e);
            path = this.CreateGraphicsPath();// x90190202b46d8c61();

            if (base.DesignMode)
            {
                this.paintRibbonForm(e.Graphics, this, path, true);
            }
            else
            {
                this.paintRibbonForm(e.Graphics, this, path, this.formActive);
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

        public void paintRibbonForm(Graphics g, RibbonForm f, GraphicsPath regionPath, bool Active)
        {
            uint uiActive = BoolToUInt(Active);

            g.SmoothingMode = SmoothingMode.Default;
            g.FillPath(new LinearGradientBrush(new Region(regionPath).GetBounds(g), CtlStyleLayout.Layout.ColorBrush2Internal, CtlStyleLayout.Layout.ColorBrush1Internal, LinearGradientMode.Vertical), regionPath);
            do
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
            }
            while ((((uint)uiActive) - ((uint)uiActive)) < 0);
            if (!Active)
            {
                g.DrawPath(new Pen(CtlStyleLayout.Layout.DisableColorInternal, 1f), regionPath);
            }
            else 
            {
                g.DrawPath(new Pen(CtlStyleLayout.Layout.BorderColorInternal, 1f), regionPath);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            this.CreateRegion();//x5a964354ec662d0a();
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
                                if (this.xc8dd69856234b75d(ref m))
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
            if ((((uint)msg) + ((uint)msg)) <= uint.MaxValue)
            {
                return;
            }
            return;
        Label_008E:
            if ((base.WindowState == FormWindowState.Minimized) && (0 == 0))
            {
                if (((uint)msg) > uint.MaxValue)
                {
                    goto Label_01AB;
                }
                if (((uint)msg) > uint.MaxValue)
                {
                    goto Label_01D6;
                }
                goto Label_0083;
            }
            if (base.WindowState != FormWindowState.Maximized)
            {
                base.WndProc(ref m);
                if ((((uint)msg) | 0xff) != 0)
                {
                    return;
                }
                goto Label_008E;
            }
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
            base.ClientSize = new Size(0x233, 0x1cd);
            base.Name = "RibbonForm";
            base.Padding = new Padding(2);//0,0,0,2);
            this.Text = "RibbonForm";
            base.TransparencyKey = Color.Empty;
            base.ResumeLayout(false);
        }

        private GraphicsPath CreateGraphicsPath()// x90190202b46d8c61()
        {
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

        private bool xc8dd69856234b75d(ref Message msg)
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


    }


}
