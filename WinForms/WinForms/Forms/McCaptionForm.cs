using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Collections;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using Nistec.Drawing;

namespace Nistec.WinForms
{

    [Serializable, ToolboxItem(false), ToolboxBitmap(typeof(McCaptionForm), "Resources.McCaptionForm.bmp"), Designer(typeof(Design.CaptionFormDesigner))]
    public class McCaptionForm : Control,ILayout// ICaptionClientStyle, IButtonControl
    {
        #region members

        const int EllipseSize = 32;
        const int TopLeftGap = 2;
        const int BottomLeftGap = 3;
        const int ControlBoxWidth = 60;

        private static Image _standardPicture;

        // Fields
        private IContainer components = null;
        //private DialogResult dialogResult;
        private int m_CaptionBorderHeight = 2;
        private int m_CaptionBarHeight = 26;
      
        private Rectangle m_CaptionBarRect = Rectangle.Empty;
        private bool m_CaptionIsDown;
        internal Rectangle m_CloseBtnRect = Rectangle.Empty;
        internal bool m_CloseButtonDown;
        internal bool m_CloseButtonHot;
        private bool m_Invalidate;
        internal bool m_MaxButtonDown;
        internal bool m_MaxButtonHot;
        internal Rectangle m_MaxButtonRect = Rectangle.Empty;
        internal bool m_MinButtonDown;
        internal bool m_MinButtonHot;
        internal Rectangle m_MinButtonRect = Rectangle.Empty;
        
        internal bool m_CaptionImageBarDown;
        internal bool m_CaptionImageBarHot;
        private Rectangle m_CaptionImageBarRect = Rectangle.Empty;
        private bool m_ShowCaptionImageBar = true;
        private Image m_CaptionImage;

        private bool m_ShowTitleBar = true;
        private char m_ShortcutKey = 'O';
        //private PopupWindow popupWindowHelper;

        Form m_form;
        private ControlLayout m_ControlLayout;
        private string _subText;

        //StyleLayout layout = base.LayoutManager.Layout;

        //Color colorBrush1;
        //Color colorBrush2;
        Color colorPush;


        // Events
        //public event CaptionButtonPopupFormRequestedHandler CaptionButtonPopupFormRequested;

        //public event CaptionPropertyChangedHandler CaptionControlPropertyChanged;

        //public event CaptionPopupCancelEventHandler CaptionPopupCancel;

        //public event CaptionPopupClosedEventHandler CaptionPopupClosed;

        #endregion

        #region ctor

        static McCaptionForm()
		{
			// Create a strip of images by loading an embedded bitmap resource
			_standardPicture = DrawUtils.LoadBitmap(Type.GetType("Nistec.WinForms.McCaptionForm"),
				"Nistec.WinForms.Images.mCtlIcon32.bmp");
		}

        // Methods
        public McCaptionForm()
        {
            //colorBrush1 = Color.AliceBlue;
            //colorBrush2 = Color.SteelBlue;
            colorPush = Color.LightGray;
            m_ControlLayout = ControlLayout.VistaLayout;
            this.m_CaptionImage = _standardPicture;
            this.InitStyle();
            this.InitControl();
            //base.ParentChanged += new EventHandler(this.xdfa3bbb2081d48cd);
        }

        protected override void Dispose(bool disposing)
        {
            //uint uidisposing = Helper.BoolToUInt(disposing);

            if (disposing && (this.components != null))
            {
                if (this.components != null)
                {
                    this.components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            m_form = base.FindForm();
        }
        #endregion


        public void NotifyDefault(bool notify)
        {

        }

        public void PerformClick()
        {
            this.OnClick(EventArgs.Empty);
        }

        #region mouse events

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            this.m_CaptionIsDown = true;

            //Rectangle tabRect = Rectangle.Empty;
            if ((m_form is McForm && m_form.ControlBox))// && ((m_form.FormBorderStyle == FormBorderStyle.SizableToolWindow) || (m_form.FormBorderStyle == FormBorderStyle.Sizable)))
            {
                if ( this.m_CloseBtnRect.Contains(e.Location))
                {
                    this.m_CloseButtonDown = true;
                    this.ShouldInvalidate = true;
                }
                else if (m_form.MinimizeBox && this.m_MinButtonRect.Contains(e.Location))
                {
                    this.m_MinButtonDown = true;
                    this.ShouldInvalidate = true;
                }
                else if (m_form.MaximizeBox && this.m_MaxButtonRect.Contains(e.Location))
                {
                    this.m_MaxButtonDown = true;
                    this.ShouldInvalidate = true;
                }
                else if (this.m_CaptionImageBarRect.Contains(e.Location))
                {
                    this.m_CaptionImageBarDown = true;
                    this.ShouldInvalidate = true;
                }

                if (m_CloseButtonHot || m_MinButtonHot || m_MaxButtonHot || m_CaptionImageBarHot)
                {
                    //this.ShouldInvalidate = true;
                    m_CloseButtonHot = false;
                    m_MinButtonHot = false;
                    m_MaxButtonHot = false;
                    m_CaptionImageBarHot = false;
                }

                //if (this.ShouldInvalidate)
                //{
                base.Invalidate();
                //}

            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (this.DesignMode)
                return;
            this.m_CaptionIsDown = false;

            //m_form.Text = "mouseLeave";

            if (this.m_CaptionImageBarHot)
            {
                this.m_CaptionImageBarHot = false;
                this.ShouldInvalidate = true;
            }
            if (this.m_CloseButtonHot)
            {
                this.m_CloseButtonHot = false;
                this.ShouldInvalidate = true;
            }
            if (this.m_CaptionImageBarDown)
            {
                this.m_CaptionImageBarDown = false;
                this.ShouldInvalidate = true;
            }
            if (this.m_CloseButtonDown)
            {
                this.m_CloseButtonDown = false;
                this.ShouldInvalidate = true;
            }
            if (this.m_MaxButtonHot)
            {
                this.m_MaxButtonHot = false;
                this.ShouldInvalidate = true;
            }
            if (this.m_MinButtonHot)
            {
                this.m_MinButtonHot = false;
                this.ShouldInvalidate = true;
            }
            if (this.m_MinButtonDown)
            {
                this.m_MinButtonDown = false;
                this.ShouldInvalidate = true;
            }
            if (this.m_MaxButtonDown)
            {
                this.m_MaxButtonDown = false;
                this.ShouldInvalidate = true;
            }
            if (this.m_CaptionImageBarDown)
            {
                this.m_CaptionImageBarDown = false;
                this.ShouldInvalidate = true;
            }


            //if (this.ShouldInvalidate)
            //{
                base.Invalidate();
            //}
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (this.DesignMode)
                return;

            if (m_CloseButtonHot || m_MinButtonHot || m_MaxButtonHot || m_CaptionImageBarHot)
            {
                this.ShouldInvalidate = true;
                m_CloseButtonHot = false;
                m_MinButtonHot = false;
                m_MaxButtonHot = false;
                m_CaptionImageBarHot = false;
            }

            if (this.m_CloseBtnRect.Contains(e.Location))
            {
                //m_form.Text = "m_CloseBtnRect";
                m_CloseButtonHot = true;
                this.ShouldInvalidate = true;
            }
            else if (this.m_MinButtonRect.Contains(e.Location))
            {
                //m_form.Text = "m_MinButtonRect";
                m_MinButtonHot = true;
                this.ShouldInvalidate = true;
            }
            else if (this.m_MaxButtonRect.Contains(e.Location))
            {
                //m_form.Text = "m_MaxButtonRect";
                m_MaxButtonHot = true;
                this.ShouldInvalidate = true;
            }
            else if (this.m_CaptionImageBarRect.Contains(e.Location))
            {
                //m_form.Text = "m_CaptionImageBarRect";
                m_CaptionImageBarHot = true;
                this.ShouldInvalidate = true;
            }
            else
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (this.m_CaptionIsDown && (!(m_CloseButtonDown || m_MinButtonDown || m_MaxButtonDown || m_CaptionImageBarDown)))
                    {
                        MoveForm();
                        return;
                    }
                }
                //this.ShouldInvalidate = false;
            }

            if (ShouldInvalidate)
            {
                base.Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (this.DesignMode)
                return;
            this.m_CaptionIsDown = false;

            if (this.m_CloseBtnRect.Contains(e.Location) && this.m_CloseButtonDown)
            {
                this.m_CloseButtonDown = false;
                m_form.Close();
                //Win32Methods.ReleaseCapture();
                //Win32Methods.SendMessage(m_form.Handle, 0x112, 0xf060, 0);
                this.ShouldInvalidate = true;
            }

            else if (this.m_MinButtonRect.Contains(e.Location) && this.m_MinButtonDown)
            {
                this.m_MinButtonDown = false;
                if (m_form.WindowState != FormWindowState.Minimized)
                {

                    //Win32Methods.SendMessage(m_form.Handle, 0x112, 0xf120, 0);
                    //m_form.WindowState = FormWindowState.Normal;
                    //}
                    //else
                    //{
                    Win32Methods.ReleaseCapture();
                    Win32Methods.SendMessage(base.FindForm().Handle, 0x112, 0xf020, 0);
                    //Win32Methods.SendMessage(m_form.Handle, 0x112, 0xf030, 0);
                    //m_form.WindowState = FormWindowState.Minimized;
                }
                this.ShouldInvalidate = true;
            }
            else if (this.m_MaxButtonRect.Contains(e.Location) && this.m_MaxButtonDown)
            {
                this.m_MaxButtonDown = false;

                Win32Methods.ReleaseCapture();
                if (m_form.WindowState == FormWindowState.Maximized)
                {
                    Win32Methods.SendMessage(m_form.Handle, 0x112, 0xf120, 0);
                    //m_form.WindowState = FormWindowState.Normal;
                }
                else
                {
                    Win32Methods.SendMessage(m_form.Handle, 0x112, 0xf030, 0);
                    //m_form.WindowState = FormWindowState.Maximized;
                }
                this.ShouldInvalidate = true;
            }
            else if (this.m_CaptionImageBarRect.Contains(e.Location) && this.m_CaptionImageBarDown)
            {
                this.m_CaptionImageBarDown = false;
                this.ShouldInvalidate = true;
            }

            if (this.ShouldInvalidate)
            {
                base.Invalidate();
            }
            m_MinButtonDown = false;
            m_MaxButtonDown = false;
            m_CaptionImageBarDown = false;
        }


        private void MoveForm()
        {
            Point mousePosition = Point.Empty;
            byte[] bytes = null;
            byte[] buffer2 = null;
            int num = 0;
            byte[] buffer4 = null;

            if (m_form is McForm)
            {
                mousePosition = Control.MousePosition;
            }
            bytes = BitConverter.GetBytes(mousePosition.X);
            buffer2 = BitConverter.GetBytes(mousePosition.Y);
            buffer4 = new byte[4];
            buffer4[0] = bytes[0];
            buffer4[1] = bytes[1];
            buffer4[2] = buffer2[0];
            buffer4[3] = buffer2[1];
            byte[] buffer3 = buffer4;
            num = BitConverter.ToInt32(buffer3, 0);
            Win32Methods.ReleaseCapture();
            Win32Methods.SendMessage(m_form.Handle, 0x112, 0xf012, num);

            this.ShouldInvalidate = true;

        }
        #endregion

        #region Draw events

        private void DrawTitleBar(Graphics g, Rectangle rect1)
        {
            StyleLayout layout = this.LayoutManager.Layout;

            Point point;
            Point point2;
            StringFormat format;
            string text;
            Font font;
            string str2;
            int gap = ShowCaptionImageBar ? 4 : 0;

            //g.FillRectangle(new LinearGradientBrush(this.ClientRectangle, layout.CaptionLightColorInternal, layout.CaptionColorInternal, 90f, true), this.ClientRectangle);

            g.DrawLine(new Pen(layout.BorderColorInternal), rect1.Location, new Point(rect1.Width, rect1.Y));
            //point = new Point(rect1.X, rect1.Y + 1);
            //point2 = new Point(rect1.Width, 4);
            //g.FillRectangle(new LinearGradientBrush(new Rectangle(point.X, point.Y, point2.X, point2.Y), colorBrush1, colorBrush2, 180f, true), new Rectangle(point.X, point.Y, point2.X, point2.Y));
            point = new Point(rect1.X + gap, rect1.Y);// + 5);
            point2 = new Point(rect1.Width, m_CaptionBarHeight);// 0x13);
            g.FillRectangle(new LinearGradientBrush(new Rectangle(point.X, point.Y, point2.X, point2.Y), layout.CaptionLightColorInternal, layout.CaptionColorInternal, 90f, true), new Rectangle(point.X, point.Y, point2.X, point2.Y));
            g.DrawLine(new Pen(layout.BorderColorInternal), new Point(rect1.Location.X + gap, rect1.Height - 1), new Point(rect1.Width, rect1.Height - 1));
            format = new StringFormat();
            format.Alignment = StringAlignment.Near;
            
            format.Trimming = StringTrimming.EllipsisCharacter;
            format.FormatFlags = StringFormatFlags.NoClip | StringFormatFlags.NoWrap;
            //format.FormatFlags |= StringFormatFlags.NoWrap;
            //format.HotkeyPrefix = HotkeyPrefix.Show;

            //Label_03E4:
            format.LineAlignment = StringAlignment.Center;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            text = "";
            //if (this.TitleTextStyle != CaptionTitleStyle.InheritFromForm)
            //{
            //    if (this.TitleTextStyle == CaptionTitleStyle.UseTextProperty)
            //    {
            //        text = this.Text;
            //        goto Label_02FB;
            //    }
            //    text = "";
            //    goto Label_02FB;
            //}
            if (m_form != null)
            {
                text = m_form.Text;
            }
            //Label_02FB:
            Rectangle textRect = new Rectangle(rect1.X + EllipseSize + 6, rect1.Y, rect1.Width - (EllipseSize + 24 + ControlBoxWidth), rect1.Height);
            font = new Font("Tohoma", 14f, FontStyle.Bold, GraphicsUnit.Pixel);
            //switch (ControlLayout)
            //{
            //    case ControlLayout.XpLayout:

            //        Rectangle r = new Rectangle(textRect.X, textRect.Y + 4, textRect.Width, textRect.Height );
            //        layout.DrawButtonRect(g, r, ButtonStates.Normal, 4);
            //        g.DrawString(text, font,new SolidBrush( layout.ColorBrush1Internal), textRect, format);
            //        break;
            //    default:
            //        g.DrawString(text, font, new SolidBrush(Color.White), textRect, format);
            //        break;
            //}

            g.DrawString(text, font, new SolidBrush(Color.White), textRect, format);
            if (!(m_form is McForm))
            {
                return;
            }
            if (m_form.ControlBox)
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                if (this.m_CloseButtonDown)
                {
                    this.DrawTitleBarButtons(g, this.m_CloseBtnRect, ButtonState.Pushed, "r");
                    goto Label_0243;
                }
                if (!this.m_CloseButtonHot)
                {
                    this.DrawTitleBarButtons(g, this.m_CloseBtnRect, ButtonState.Normal, "r");
                    goto Label_0243;
                }
                this.DrawTitleBarButtons(g, this.m_CloseBtnRect, ButtonState.Flat, "r");
            }

        Label_0243:
            str2 = "";
            if (m_form.WindowState == FormWindowState.Maximized)
            {
                str2 = "2";
                goto Label_00B7;
            }
            str2 = "1";
        Label_00B7:
            if (m_form.FormBorderStyle == FormBorderStyle.Sizable)
            {
                goto Label_01D8;
            }
            if (m_form.FormBorderStyle == FormBorderStyle.SizableToolWindow)
            {
                goto Label_01D8;
            }
            goto Label_004F;

        Label_01D8:
            if (m_form.MaximizeBox)
            {
                if (!this.m_MaxButtonDown)
                {
                    if (!this.m_MaxButtonHot)
                    {
                        this.DrawTitleBarButtons(g, this.m_MaxButtonRect, ButtonState.Normal, str2);
                        goto Label_013A;
                    }
                    this.DrawTitleBarButtons(g, this.m_MaxButtonRect, ButtonState.Flat, str2);
                    goto Label_013A;
                }
                this.DrawTitleBarButtons(g, this.m_MaxButtonRect, ButtonState.Pushed, str2);
                goto Label_013A;
            }
            this.DrawTitleBarButtons(g, this.m_MaxButtonRect, ButtonState.Inactive, str2);
        Label_013A:
            if (m_form.MinimizeBox)
            {
                if (!this.m_MinButtonDown)
                {
                    if (!this.m_MinButtonHot)
                    {
                        this.DrawTitleBarButtons(g, this.m_MinButtonRect, ButtonState.Normal, "0");
                        goto Label_004F;
                    }
                    //Label_00F2:
                    this.DrawTitleBarButtons(g, this.m_MinButtonRect, ButtonState.Flat, "0");
                    goto Label_004F;
                }
                this.DrawTitleBarButtons(g, this.m_MinButtonRect, ButtonState.Pushed, "0");
            }
            else
            {
                this.DrawTitleBarButtons(g, this.m_MinButtonRect, ButtonState.Inactive, "0");
            }
        Label_004F:
            g.SmoothingMode = SmoothingMode.Default;
            //return;

        }

        private void DrawTitleBarButtons(Graphics g, Rectangle rct, ButtonState btnState, string text1)
        {
            StyleLayout layout = this.LayoutManager.Layout;

            StringFormat format2;
            Font font2;
            ButtonState state;
            Rectangle rect = rct;
            Rectangle rectangle2 = rct;
            state = btnState;
            if (state > ButtonState.Inactive)
            {
                if (state != ButtonState.Pushed)
                {
                    if (state == ButtonState.Flat)
                    {
                        g.SmoothingMode = SmoothingMode.Default;
                        rct.Inflate(-2, -2);
                        rect = rct;
                        g.FillRectangle(new LinearGradientBrush(rect, layout.ColorBrush2Internal, layout.ColorBrush1Internal, LinearGradientMode.Vertical), rect);
                        rct.Inflate(2, 2);
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                        rct.Inflate(-1, -1);
                        this.drawRectRounded(g, rct, new Pen(layout.ColorBrush2Internal), 4);//new LinearGradientBrush(rct, colorBrush1, colorBrush2, LinearGradientMode.Vertical)), 4);
                        //Label_0353:
                        //rect.Height = rct.Height / 2;
                        //g.FillRectangle(new LinearGradientBrush(rect, colorBrush1, colorBrush2, LinearGradientMode.Vertical), rect);
                        //rectangle2 = rct;
                        //rectangle2.Height = rct.Height / 2;
                        //rectangle2.Y = rect.Bottom;
                        //g.FillRectangle(new LinearGradientBrush(rectangle2, colorBrush1, colorBrush2, LinearGradientMode.Vertical), rectangle2);
                        //rct.Inflate(2, 2);
                        //g.SmoothingMode = SmoothingMode.AntiAlias;
                        //rct.Inflate(-1, -1);
                        //this.drawRectRounded(g, rct, new Pen(new LinearGradientBrush(rct, colorBrush1, colorBrush2, LinearGradientMode.Vertical)), 4);
                        //rct.Inflate(1, 1);
                        //this.drawRectRounded(g, rct, new Pen(new LinearGradientBrush(rct, colorBrush1, colorBrush2, LinearGradientMode.Vertical)), 4);
                        goto Label_004D;
                    }
                    goto Label_0092;
                }
                g.SmoothingMode = SmoothingMode.Default;
                rct.Inflate(-2, -2);
                rect = rct;
                //rect.Height = rct.Height / 2;
                g.FillRectangle(new LinearGradientBrush(rect, colorPush, colorPush, LinearGradientMode.Vertical), rect);
                //rectangle2 = rct;
                //rectangle2.Height = rct.Height / 2;
                //rectangle2.Y = rect.Bottom;
                //g.FillRectangle(new LinearGradientBrush(rectangle2, colorBrush1, colorBrush2, LinearGradientMode.Vertical), rectangle2);
                //rct.Inflate(2, 2);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                //rct.Inflate(-1, -1);
                //this.drawRectRounded(g, rct, new Pen(new LinearGradientBrush(rct, colorBrush2, colorBrush2, LinearGradientMode.Vertical)), 4);
                rct.Inflate(1, 1);
                this.drawRectRounded(g, rct, new Pen(new LinearGradientBrush(rct, layout.ColorBrush2Internal, layout.ColorBrush1Internal, LinearGradientMode.Vertical)), 4);
                goto Label_004D;
            }
            if (state != ButtonState.Normal)
            {
                if (state == ButtonState.Inactive)
                {
                    //StringFormat format = new StringFormat();
                    //format.Alignment = StringAlignment.Center;
                    //format.LineAlignment = StringAlignment.Center;
                    //g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                    //Font font = new Font("Marlett", 16f, FontStyle.Regular, GraphicsUnit.Pixel);
                    ////ControlPaint.DrawStringDisabled(g, text1, font, layout.ColorBrush2Internal, rct, format);
                    //g.DrawString(text1, font, new SolidBrush( layout.ColorBrush2Internal), rct, format);
                    return;
                }
                goto Label_0092;
            }
            else
            {
                rct.Inflate(-1, -1);
                this.drawRectRounded(g, rct, new Pen(layout.ColorBrush2Internal), 4);
            }
            goto Label_004D;
        Label_0092:
            this.drawRectRounded(g, rct, Pens.Red, 4);
            goto Label_004D;
        Label_004D:
            format2 = new StringFormat();
            format2.Alignment = StringAlignment.Center;
            format2.LineAlignment = StringAlignment.Center;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            font2 = new Font("Marlett", 16f, FontStyle.Regular, GraphicsUnit.Pixel);
            g.DrawString(text1, font2, new SolidBrush(Color.White), rct, format2);
            //return;
        }

        private void DrawTitleBarBorderLine(Graphics g, Rectangle rect1)
        {
            //g.DrawLine(new Pen(colorBrush2), new Point(rect1.Location.X+3, rect1.Height), new Point(rect1.Width, rect1.Height));
            //g.DrawLine(new Pen(colorBrush2), new Point(rect1.Location.X+3, rect1.Height + 1), new Point(rect1.Width, rect1.Height + 1));
        }

        private void FillCaptionBackground(Graphics g, Rectangle rct)
        {
            if (rct.Height >= 1)
            {
                StyleLayout layout = this.LayoutManager.Layout;
                g.FillRectangle(new LinearGradientBrush(new Rectangle(rct.X, rct.Y - 1, rct.Width, rct.Height + 1), layout.ColorBrush2Internal, layout.ColorBrush1Internal, 90f), rct);
            }
        }

        public void paintCaption(Graphics g)//, int m_CaptionBarHeight, int m_CaptionBorderHeight, Rectangle m_CaptionImageBarRect, Rectangle mTabPaneRect, Rectangle m_CaptionBarRect)
        {
            //this.m_CaptionBarHeight, this.m_CaptionBorderHeight, this.m_CaptionImageBarRect, this.mTabPaneRect, this.m_CaptionBarRect);
            StyleLayout layout = this.LayoutManager.Layout;

            Rectangle rectangle = Rectangle.Empty;
            Image captionImage = this.CaptionImage;
            Rectangle rectangle3 = Rectangle.Empty;
            this.DrawTitleBar(g, m_CaptionBarRect);
            Rectangle buttonRect = new Rectangle(m_CaptionImageBarRect.X + TopLeftGap, m_CaptionImageBarRect.Y + TopLeftGap, m_CaptionImageBarRect.Width - (TopLeftGap * 2), m_CaptionImageBarRect.Height - (TopLeftGap * 2));
            rectangle = m_CaptionImageBarRect;
            rectangle.Offset(1, 1);

            if (((uint)m_CaptionBorderHeight) >= 0)
            {
                this.DrawTitleBarBorderLine(g, m_CaptionBarRect);
                //this.FillCaptionBackground(g, new Rectangle(0, m_CaptionBarHeight + m_CaptionBorderHeight, this.ClientRectangle.Width, this.ClientRectangle.Height - (m_CaptionBorderHeight + m_CaptionBarHeight)));
                g.SmoothingMode = SmoothingMode.HighQuality;
                
                switch (ControlLayout)
                {
                    case ControlLayout.VistaLayout:
                        if (this.ShowCaptionImageBar)
                        {
                            if (!this.m_CaptionImageBarDown)
                            {
                                if (!this.m_CaptionImageBarHot)
                                {
                                    g.FillEllipse(new LinearGradientBrush(buttonRect, layout.CaptionLightColorInternal, layout.CaptionColorInternal, LinearGradientMode.Vertical), buttonRect);
                                }
                                else
                                {
                                    g.FillEllipse(new LinearGradientBrush(buttonRect, layout.CaptionLightColorInternal, layout.CaptionColorInternal, LinearGradientMode.Vertical), buttonRect);
                                }
                            }
                            else
                            {
                                g.FillEllipse(new LinearGradientBrush(buttonRect, layout.CaptionLightColorInternal, layout.CaptionColorInternal, LinearGradientMode.Vertical), buttonRect);
                            }
                            g.DrawEllipse(new Pen(new LinearGradientBrush(buttonRect, layout.ColorBrush1Internal, layout.ColorBrush2Internal, LinearGradientMode.BackwardDiagonal), 1), buttonRect);
                            g.DrawEllipse(new Pen(new LinearGradientBrush(new Rectangle(m_CaptionImageBarRect.X, m_CaptionImageBarRect.Y, m_CaptionImageBarRect.Width, m_CaptionImageBarRect.Height), layout.ColorBrush1Internal, layout.BorderColorInternal, LinearGradientMode.ForwardDiagonal), 2), new Rectangle(m_CaptionImageBarRect.X, m_CaptionImageBarRect.Y, m_CaptionImageBarRect.Width, m_CaptionImageBarRect.Height));
                            //rectangle = m_CaptionImageBarRect;
                            //rectangle.Offset(1, 1);

                            //captionImage = this.Image;
                            //if (this.CaptionButtonImage != null)
                            //{
                            //    captionImage = this.CaptionButtonImage;
                            //}
                            //captionImage = m_form.Icon.ToBitmap();
                            //goto Label_04C0;
                            //if (rectangle.Width > captionImage.Width)
                            //{
                            //    rectangle.X += (rectangle.Width / 2) - (captionImage.Width / 2);
                            //    rectangle.Width = captionImage.Width;
                            //}
                            //if (rectangle.Height > captionImage.Height)
                            //{
                            //    rectangle.Y += (rectangle.Height / 2) - (captionImage.Height / 2);
                            //    rectangle.Height = captionImage.Height;
                            //}
                            //if (rectangle.Height < captionImage.Height)
                            //{
                            //    goto Label_03D2;
                            //}
                            //if (rectangle.Width > captionImage.Width)
                            //{
                            //    goto Label_03D2;
                            //}
                            //if ((((uint)m_CaptionBorderHeight) - ((uint)m_CaptionBarHeight)) > uint.MaxValue)
                            //{
                            //    return;
                            //}
                            if (captionImage != null)
                                g.DrawImage(captionImage, buttonRect.X + 4, buttonRect.Y + 4, buttonRect.Width - 8, buttonRect.Height - 8);//, GraphicsUnit.Pixel);//.DrawImageUnscaled(captionImage, rectangle);
                        }
                        goto Label_02;
                    case ControlLayout.Visual:
                        g.FillEllipse(new LinearGradientBrush(buttonRect, layout.CaptionLightColorInternal, layout.CaptionColorInternal, LinearGradientMode.Vertical), buttonRect);
                        g.DrawEllipse(new Pen(new LinearGradientBrush(buttonRect, layout.ColorBrush1Internal, layout.ColorBrush2Internal, LinearGradientMode.BackwardDiagonal), 1), buttonRect);
                        g.DrawEllipse(new Pen(new LinearGradientBrush(new Rectangle(m_CaptionImageBarRect.X, m_CaptionImageBarRect.Y, m_CaptionImageBarRect.Width, m_CaptionImageBarRect.Height), layout.ColorBrush1Internal, layout.BorderColorInternal, LinearGradientMode.ForwardDiagonal), 2), new Rectangle(m_CaptionImageBarRect.X, m_CaptionImageBarRect.Y, m_CaptionImageBarRect.Width, m_CaptionImageBarRect.Height));
                        if (captionImage != null)
                            g.DrawImage(captionImage, buttonRect.X + 4, buttonRect.Y + 4, buttonRect.Width - 8, buttonRect.Height - 8);//, GraphicsUnit.Pixel);//.DrawImageUnscaled(captionImage, rectangle);
                        goto Label_02;
                    //goto Label_01;
                    case ControlLayout.XpLayout:
                        if (captionImage != null)
                            g.DrawImage(captionImage, buttonRect.X + 4, buttonRect.Y + 4, buttonRect.Width - 4, buttonRect.Height - 4);//, GraphicsUnit.Pixel);//.DrawImageUnscaled(captionImage, rectangle);
                        goto Label_02;
                    //g.DrawImageUnscaledAndClipped(captionImage, rectangle);
                        //g.DrawImage(captionImage, m_CaptionBarRect.X + 4, m_CaptionBarRect.Y + 4, captionImage.Width, captionImage.Height);//, GraphicsUnit.Pixel);//g.DrawImageUnscaledAndClipped(captionImage, rectangle);
                        //break;
                    default:
                        if (captionImage != null)
                            g.DrawImage(captionImage, m_CaptionBarRect.X + 4, m_CaptionBarRect.Y + 4, 16, 16);//, GraphicsUnit.Pixel);//g.DrawImageUnscaledAndClipped(captionImage, rectangle);
                        goto Label_02;
                        //break;
                }
                //}
                //goto Label_021B;
                //goto Label_02;
            }
            //Label_01:
            //if (captionImage != null)
            //{
            //    if (rectangle.Width > captionImage.Width)
            //    {
            //        rectangle.X += (rectangle.Width / 2) - (captionImage.Width / 2);
            //        rectangle.Width = captionImage.Width;
            //    }
            //    if (rectangle.Height > captionImage.Height)
            //    {
            //        rectangle.Y += (rectangle.Height / 2) - (captionImage.Height / 2);
            //        rectangle.Height = captionImage.Height;
            //    }
            //    if (rectangle.Height < captionImage.Height)
            //    {
            //        g.DrawImage(captionImage, rectangle.X , rectangle.Y , rectangle.Width , rectangle.Height );//, GraphicsUnit.Pixel);//.DrawImageUnscaled(captionImage, rectangle);
            //    }
            //    else
            //    {
            //        g.DrawImageUnscaledAndClipped(captionImage, rectangle);
            //        //g.DrawImage(captionImage, buttonRect.X + 4, buttonRect.Y + 4, buttonRect.Width - 4, buttonRect.Height - 4);//, GraphicsUnit.Pixel);//.DrawImageUnscaled(captionImage, rectangle);
            //        //g.DrawImage(captionImage, rectangle.X, rectangle.Y, captionImage.Width, captionImage.Height);//, GraphicsUnit.Pixel);//.DrawImageUnscaled(captionImage, rectangle);
            //    }
            //}
            Label_02:
            g.SmoothingMode = SmoothingMode.Default;
            return;// goto Label_00C3;
        }


        public void drawRectRounded(Graphics g, Rectangle rect, Pen p, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            rect.Width--;
            rect.Height--;

            //top right
            path.AddArc((rect.X + rect.Width) - radius, rect.Y, radius, radius, 270f, 90f);
            //bottom right
            path.AddArc((rect.X + rect.Width) - radius, (rect.Y + rect.Height) - radius, radius, radius, 0f, 90f);
            //bottom left
            path.AddArc(rect.X, (rect.Y + rect.Height) - radius, radius, radius, 90f, 90f);
            //top left
            path.AddArc(rect.X, rect.Y, radius, radius, 180f, 90f);
            path.CloseAllFigures();
            g.DrawPath(p, path);
            path.Dispose();
        }

        public void doLayout()
        {
            Graphics graphics = null;
            int num = 0;
            Rectangle rectangle3 = Rectangle.Empty;
            int num2 = 0;
            McCaptionForm control = this;
            goto Label_06A7;
        Label_0007:
            if (((uint)num2) > uint.MaxValue)
            {
                goto Label_0077;
            }
        Label_0077:
            if ((((uint)num) + ((uint)num)) <= uint.MaxValue)
            {
                if (((uint)num) >= 0)
                {
                    goto Label_0007;
                }
                goto Label_054C;
            }
            if ((((uint)num2) + ((uint)num)) >= 0)
            {
                if ((((uint)num) + ((uint)num)) < 0)
                {
                    goto Label_0638;
                }
                if (((uint)num) <= uint.MaxValue)
                {
                    goto Label_011F;
                }
            }
            goto Label_0077;
        Label_011F:
            if ((((uint)num2) + ((uint)num)) < 0)
            {
                if (0 == 0)
                {
                    goto Label_0077;
                }
            }
            goto Label_0077;

        Label_0374:
            //this.mTabAreaHeight = control.Height - (this.mTabTopPadding + this.mTabHeight);
            //if (this.mTabAreaHeight >= 1)
            //{
            //    rectangle3 = new Rectangle(1, (this.mTabTopPadding + this.mTabHeight) - 1, control.ClientRectangle.Width - 2, this.mTabAreaHeight);
            //    this.mTabPaneRect = rectangle3;
            //    rectangle3.Inflate(-3, -3);
            //    num2 = 0;
            //    if ((((uint)num) | 3) == 0)
            //    {
            //        goto Label_0464;
            //    }
            //    goto Label_0034;// Label_0022;
            //}
            //this.mTabPaneRect = Rectangle.Empty;
            //Label_0034:
            graphics.Dispose();
            control.ShouldInvalidate = false;
            return;
        Label_03C2:
            this.m_MaxButtonRect.Y++;
            this.m_MinButtonRect.Y++;
            if ((((uint)num2) + ((uint)num)) <= uint.MaxValue)
            {
                goto Label_0374;// Label_01F9;
            }
            goto Label_054C;
        Label_03F9:
            this.m_CloseBtnRect.Y++;
            goto Label_03C2;
        Label_0464:
            if ((((uint)num) + ((uint)num)) > uint.MaxValue)
            {
                goto Label_04B1;
            }
            this.m_MinButtonRect.Y = 1;
            goto Label_03F9;
        Label_047E:
            this.m_CloseBtnRect = new Rectangle(0, 0, 0x16, 0x16);
            this.m_MaxButtonRect = new Rectangle(0, 0, 0x16, 0x16);
            this.m_MinButtonRect = new Rectangle(0, 0, 0x16, 0x16);
        Label_04B1:
            this.m_CloseBtnRect.X = this.m_CaptionBarRect.Right - 0x19;
            this.m_CloseBtnRect.Y = 1;
            if (((uint)num) > uint.MaxValue)
            {
                goto Label_06A7;
            }
            this.m_MaxButtonRect.X = this.m_CloseBtnRect.Left - 0x19;
            this.m_MaxButtonRect.Y = 1;
            this.m_MinButtonRect.X = this.m_MaxButtonRect.Left - 0x19;
            goto Label_0464;
        Label_04F4:
            //num = this.mTabLeftPadding;
            Rectangle clientRectangle = control.ClientRectangle;
            clientRectangle.Height = this.m_CaptionBarHeight;
            this.m_CaptionBarRect = clientRectangle;
            if (m_form is McForm)
            {
                goto Label_047E;
            }
            goto Label_0374;// Label_0211;
        Label_0544:
            if (!this.ShowCaptionImageBar)
            {
                goto Label_05BD;
            }
        Label_054C:
            if (this.ShowCaptionImageBar)
            {
                goto Label_04F4;
            }
            if (this.ShowTitleBar)
            {
                goto Label_04F4;
            }
            this.m_CaptionBorderHeight = 0;
            if ((((uint)num) - ((uint)num2)) >= 0)
            {
                this.m_CaptionBarHeight = 0;
                //this.mTabLeftPadding = 5;
                //this.mTabTopPadding = 0;
                goto Label_04F4;
            }
            goto Label_0374;// Label_01F9;
        Label_059A:
            if (!this.ShowTitleBar)
            {
                goto Label_05F3;
            }
            if ((((uint)num2) - ((uint)num2)) >= 0)
            {
                goto Label_0544;
            }
        Label_05BD:
            //this.mTabLeftPadding = 5;
            if ((((uint)num2) | 3) != 0)
            {
                goto Label_054C;
            }
            goto Label_0007;
        Label_05D0:
            this.m_CaptionImageBarRect = Rectangle.Empty;
            if ((((uint)num) - ((uint)num2)) <= uint.MaxValue)
            {
                goto Label_059A;
            }
        Label_05F3:
            this.m_CaptionBorderHeight = 0;
            this.m_CaptionBarHeight = 0;
            goto Label_0544;
        Label_0621:
            this.m_CaptionBorderHeight = 2;
        //this.mTabAreaHeight = 0x5c;
        //this.mTabHeight = 0x16;
        Label_0638:
            if (!this.ShowCaptionImageBar)
            {
                goto Label_05D0;
            }
            this.m_CaptionImageBarRect = new Rectangle(TopLeftGap, TopLeftGap, EllipseSize, EllipseSize);
            if ((((uint)num) | uint.MaxValue) != 0)
            {
                goto Label_068E;
            }
        Label_0674:
            this.m_CaptionBarHeight = 25;
            if (((uint)num) >= 0)
            {
                goto Label_0621;
            }
        Label_068E:
            if ((((uint)num2) & 0) == 0)
            {
                goto Label_059A;
            }
        Label_06A7:
            graphics = control.CreateGraphics();
            //this.mTabLeftPadding = 0x30;
            if (((((uint)num) | 3) == 0) || ((((uint)num2) - ((uint)num)) < 0))
            {
                goto Label_0077;
            }
            if ((((uint)num) & 0) == 0)
            {
                goto Label_0674;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //CaptionStyle style;
            //if (base.Disposing)
            //    return;

            base.OnPaint(e);

            //if (this.ShouldInvalidate)
            //{
            //if (base.Parent is ICaptionClientStyle)
            //{
            //    style = ((ICaptionClientStyle)base.Parent).Style;
            //}
            //else
            //{
            //    if (!(m_form is CaptionForm))
            //    {
            //        style = this.Style;
            //    }
            //    else
            //    {
            //        style = ((CaptionForm)m_form).Style;
            //    }
            //}
            //this.mStyle = style;

            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            this.doLayout();
            this.m_CaptionImageBarRect = new Rectangle(TopLeftGap, TopLeftGap, EllipseSize, EllipseSize);
            paintCaption(e.Graphics);//, this.m_CaptionBarHeight, this.m_CaptionBorderHeight, this.m_CaptionImageBarRect, this.mTabPaneRect, this.m_CaptionBarRect);
            //}
        }
        #endregion

        #region override

        protected override void OnResize(EventArgs e)
        {
            this.CreatePath();
            this.ShouldInvalidate = true;
            base.OnResize(e);
        }

        protected override bool ProcessMnemonic(char charCode)
        {
            //uint uflag = 0;
            //bool flag = false;

            if (Control.ModifierKeys != Keys.Alt)
            {
                return base.ProcessMnemonic(charCode);
            }
            if (Control.IsMnemonic(charCode, "&" + this.m_ShortcutKey))
            {
                //this.x8da261cb161bacaf();
                return true;
            }
            //try
            //{
            //    uflag = Helper.BoolToUInt(flag);
            //    if ((((uint)Helper.BoolToUInt(flag)) | 0x7fffffff) != 0)
            //    {
            //        this.m_Invalidate = true;
            //        base.Invalidate();
            //        return true;
            //    }
            //}
            //finally
            //{
            //}
            return base.ProcessMnemonic(charCode);
        }

        internal void SendMsg(ref Message m)
        {
            this.WndProc(ref m);
        }

        protected override void WndProc(ref Message m)
        {
            Point point = Point.Empty;
            int msg = 0;
            if (!base.DesignMode)
            {
                if ((!m.ToString().ToLower().Contains("WM_LBUTTON".ToLower()) && ((((uint)msg) & 0) == 0)) && !m.ToString().ToLower().Contains("WM_RBUTTON".ToLower()))
                {
                    goto Label_0033;
                }
                point = this.x3d0370f1e847fa3e((int)m.LParam);
                if (this.m_CaptionBarRect.Contains(point) && ((!this.m_CaptionImageBarRect.Contains(point) && !this.m_MaxButtonRect.Contains(point)) && (!this.m_CloseBtnRect.Contains(point) && !this.m_MinButtonRect.Contains(point))))
                {
                    m.Result = (IntPtr)2;
                    msg = m.Msg;
                    if (msg == 0x203)
                    {
                        Win32Methods.SendMessage(m_form.Handle, 0xa3, 2, 0);
                    }
                }
                goto Label_0033;
            }
        Label_0033:
            base.WndProc(ref m);
        }

        #endregion

        #region private methods

        private int x31b92269760e6e6d(int x130fbcecf32fe781)
        {
            return (x130fbcecf32fe781 >> 0x10);
        }

        private Point x3d0370f1e847fa3e(int x130fbcecf32fe781)
        {
            return new Point(this.xcb3a309bf0197241(x130fbcecf32fe781), this.x31b92269760e6e6d(x130fbcecf32fe781));
        }


        private void CreatePath()
        {
            Rectangle rectangle = Rectangle.Empty;
            Region region = null;
            GraphicsPath path = new GraphicsPath();
            rectangle = base.ClientRectangle;
            rectangle.Height--;
            rectangle.Width--;
            int radius = 4;
            int radiusB = 4;
            int radiusTopLeft = 43;
            switch (this.ControlLayout)
            {
                case ControlLayout.VistaLayout:
                    radius = 4;
                    radiusTopLeft = 43;
                    //top right
                    path.AddArc((rectangle.X + rectangle.Width) - radius, rectangle.Y, radius, radius, 270f, 90f);
                    //bottom right
                    path.AddArc((rectangle.X + rectangle.Width) - radius, (rectangle.Y + rectangle.Height) - radius, radius, radius, 0f, 90f);
                    //bottom left
                    path.AddArc(rectangle.X, (rectangle.Y + rectangle.Height) - radius, radius, radius, 90f, 90f);
                    //top left
                    path.AddArc(rectangle.X, rectangle.Y, radiusTopLeft, radiusTopLeft, 180f, 90f);
                    break;
                default:
                    radius = 12;
                    //top right
                    path.AddArc((rectangle.X + rectangle.Width) - radius, rectangle.Y, radius, radius, 270f, 90f);
                    //bottom right
                    path.AddArc((rectangle.X + rectangle.Width) - radiusB, (rectangle.Y + rectangle.Height) - radiusB, radiusB, radiusB, 0f, 90f);
                    //bottom left
                    path.AddArc(rectangle.X, (rectangle.Y + rectangle.Height) - radiusB, radiusB, radiusB, 90f, 90f);
                    //top left
                    path.AddArc(rectangle.X, rectangle.Y, radius, radius, 180f, 90f);
                    break;
            }
            
            path.CloseAllFigures();
            region = new Region();
            region.MakeEmpty();
            region.Union(path);
            path.Widen(SystemPens.Control);
            new Region(path);
            region.Union(path);
            base.Region = region;
            path.Dispose();
        }


        private void InitControl()
        {
            base.SuspendLayout();
            base.Name = "McCaptionForm";
            this.Size = new Size(0x102, 36);
            base.ResumeLayout(false);
        }



        private void x8a9dc6229002d4c6(bool xbcea506a33cf9111)
        {
        }

        //private void x8da261cb161bacaf()
        //{
        //    CaptionPopupForm base2;
        //    if (this.CaptionButtonPopupFormRequested == null)
        //    {
        //        return;
        //    }
        ////Label_001F:
        //    base2 = null;
        //    //this.CaptionButtonPopupFormRequested(this, ref base2);
        //    if ((0 == 0) && (base2 == null))
        //    {
        //            return;
        //    }
        //    try
        //    {
        //        this.popupWindowHelper.AssignHandle(m_form.Handle);
        //    }
        //    catch
        //    {
        //        return;
        //    }
        //    Point location = base.PointToScreen(new Point(this.m_CaptionImageBarRect.Left, this.m_CaptionImageBarRect.Bottom));
        ////Label_006F:
        //    base2.Style = this.Style;
        //    this.popupWindowHelper.ShowPopup(m_form, base2, location);
        //}

        //xc256bcbd35c60777
        private void InitStyle()
        {
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            base.SetStyle(ControlStyles.Selectable, true);
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            base.SetStyle(ControlStyles.UserPaint, true);
        }

        private int xcb3a309bf0197241(int x130fbcecf32fe781)
        {
            return (x130fbcecf32fe781 & 0xffff);
        }
        #endregion

        #region properties

        internal bool ShouldInvalidate
        {
            get
            {
                return this.m_Invalidate;
            }
            set
            {
                this.m_Invalidate = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public char ShortcutKey
        {
            get
            {
                return this.m_ShortcutKey;
            }
            set
            {
                this.m_ShortcutKey = value;
            }
        }

        public Image CaptionImage
        {
            get
            {
                Image value = this.FindForm().Icon.ToBitmap();

                if (value != null)
                {
                    return value;
                }
                return this.m_CaptionImage;
            }
            set
            {

                if (value == null)
                {
                    value = this.FindForm().Icon.ToBitmap();
                }
                this.m_CaptionImage = value;

                //this.x3bacd662c5d69be4("CaptionButtonImage");
            }
        }


        [DefaultValue(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        internal bool ShowCaptionImageBar
        {
            get
            {
                return this.m_ShowCaptionImageBar;
            }
            set
            {
                this.m_ShowCaptionImageBar = value;
                this.Invalidate();
                //this.x3bacd662c5d69be4("ShowCaptionImageBar");
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), DefaultValue(true)]
        internal bool ShowTitleBar
        {
            get
            {
                return this.m_ShowTitleBar;
            }
            set
            {
                this.m_ShowTitleBar = value;
                this.Invalidate();
                //this.x3bacd662c5d69be4("ShowTitleBar");
            }
        }

        public new Size Size
        {
            get
            {
                return base.Size;
            }
            set
            {
                if (base.Width != value.Width)
                {
                    this.m_Invalidate = true;
                }
                if (base.Height != value.Height)
                {
                    this.m_Invalidate = true;
                }
                base.Size = value;
            }
        }

        [Category("Caption"), Description("Sub title text"), Localizable(true), Browsable(true)]
        public string SubText
        {
            get { return _subText ; }

            set
            {
                _subText = value;
                this.Invalidate();
            }
        }

        //[Category("Caption"), Description("Main title text"), Localizable(true), Browsable(true)]//,EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public override string Text
        //{
        //    get { return base.Text; }

        //    set
        //    {
        //        base.Text = value;

        //        if (autoCaptionText && form != null)
        //        {
        //            form.Text = value;
        //        }
        //        //lblCaption.Text = value;
        //        //DrowTextInternal(); 
        //        this.Invalidate();
        //    }
        //}
        //DialogResult IButtonControl.DialogResult
        //{
        //    get
        //    {
        //        return this.dialogResult;
        //    }
        //    set
        //    {
        //        if (Enum.IsDefined(typeof(DialogResult), value))
        //        {
        //            this.dialogResult = value;
        //        }
        //    }
        //}

        //public CaptionTitleStyle TitleTextStyle
        //{
        //    get
        //    {
        //        return this.mTitleTextStyle;
        //    }
        //    set
        //    {
        //        this.mTitleTextStyle = value;
        //        //this.x3bacd662c5d69be4("TitleTextStyle");
        //    }
        //}

        [Category("Style"), DefaultValue(ControlLayout.VistaLayout)]
        public virtual ControlLayout ControlLayout
        {
            get { return m_ControlLayout; }
            set
            {
                if (m_ControlLayout != value)
                {
                    m_ControlLayout = value;

                    switch (this.ControlLayout)
                    {
                        case ControlLayout.VistaLayout:
                            m_ShowCaptionImageBar = true;
                            break;
                        default:
                            m_ShowCaptionImageBar = false;
                            break;
                    }

                    //if(m_ControlLayout==ControlLayout.System && IsHandleCreated)
                    //{
                    //  this.BackColor=this.Parent.BackColor;  
                    //}
                    OnStylePropertyChanged(new PropertyChangedEventArgs("ControlLayout"));
                    OnControlLayoutChanged(EventArgs.Empty);
                    this.Invalidate();
                }
            }

        }
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new int TabIndex
        {
            get { return base.TabIndex; }
            set
            {
                base.TabIndex = value;
            }
        }
        #endregion

        #region ILayout

        protected IStyle m_StylePainter;

        [Browsable(false)]
        public PainterTypes PainterType
        {
            get { return PainterTypes.Flat; }
        }

        [Category("Style"), DefaultValue(null), RefreshProperties(RefreshProperties.All)]
        public IStyle StylePainter
        {
            get { return m_StylePainter; }
            set
            {
                if (m_StylePainter != value)
                {
                    if (this.m_StylePainter != null)
                        this.m_StylePainter.PropertyChanged -= new PropertyChangedEventHandler(m_Style_PropertyChanged);
                    m_StylePainter = value;
                    if (this.m_StylePainter != null)
                        this.m_StylePainter.PropertyChanged += new PropertyChangedEventHandler(m_Style_PropertyChanged);
                    OnStylePainterChanged(EventArgs.Empty);
                    this.Invalidate(true);
                }
            }
        }

        [Browsable(false)]
        public virtual IStyleLayout LayoutManager
        {
            get
            {
                if (this.m_StylePainter != null)
                    return this.m_StylePainter.Layout as IStyleLayout;
                else
                    return StyleLayout.DefaultLayout as IStyleLayout;// this.m_Style as IStyleLayout;
            }
        }

        public virtual void SetStyleLayout(StyleLayout value)
        {
            if (this.m_StylePainter != null)
                this.m_StylePainter.Layout.SetStyleLayout(value);
        }

        public virtual void SetStyleLayout(Styles value)
        {
            if (this.m_StylePainter != null)
                m_StylePainter.Layout.SetStyleLayout(value);
        }

        protected virtual void OnStylePainterChanged(EventArgs e)
        {
            OnStylePropertyChanged(new PropertyChangedEventArgs("ControlLayout"));
            //if (autoChildrenStyle)
            //{
            //    SetChildrenStyle(false);
            //}
        }

        //protected virtual void SetChildrenStyle(bool clear)
        //{
        //    foreach (Control c in this.Controls)
        //    {
        //        if (c is ILayout)
        //        {
        //            ((ILayout)c).StylePainter = clear ? null : this.StylePainter;
        //        }
        //    }
        //    this.Invalidate(true);
        //}

        protected virtual void OnStylePropertyChanged(PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("ControlLayout") || e.PropertyName.Equals("ColorBrush1") || e.PropertyName.Equals("BackgroundColor"))
                SerializeBackColor(Color.Empty, false);
            if (e.PropertyName.Equals("ControlLayout") || e.PropertyName.Equals("ForeColor"))
                SerializeForeColor(Color.Empty, false);
            if (e.PropertyName.Equals("ControlLayout") || e.PropertyName.Equals("TextFont"))
                SerializeFont(Form.DefaultFont, false);

            if ((DesignMode || IsHandleCreated))
                this.Invalidate(true);
        }

        private void m_Style_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnStylePropertyChanged(e);
        }

        protected virtual void OnControlLayoutChanged(EventArgs e)
        {

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
                base.ForeColor = LayoutManager.Layout.ForeColorInternal;
            else if (force)
                base.ForeColor = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected void SerializeBackColor(Color value, bool force)
        {
            //switch (m_ControlLayout)
            //{
            //    case ControlLayout.Visual:
            //    case ControlLayout.XpLayout:
            //        base.BackColor = LayoutManager.Layout.ColorBrush1Internal;
            //        break;
            //    default:
            //        if (IsHandleCreated && StylePainter != null)
            //            base.BackColor = LayoutManager.Layout.BackgroundColorInternal;
            //        else if (force)
            //            base.BackColor = value;
            //        break;
            //}
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected bool ShouldSerializeBackColor()
        {
            if (!IsHandleCreated)
                return false;
            switch (m_ControlLayout)
            {
                case ControlLayout.Visual:
                case ControlLayout.XpLayout:
                    return true;
                default:
                    return StylePainter != null;

            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected bool ShouldSerializeForeColor()
        {
            return IsHandleCreated && StylePainter != null;
        }
        #endregion

    }

}
