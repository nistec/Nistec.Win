using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Text;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using Nistec.Drawing;

namespace Nistec.WinForms
{

    [Designer(typeof(Design.McButtonDesigner))]
    [DefaultEvent("Click")]
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(McButton), "Toolbox.Button.bmp")]
    public class McButton : McButtonBase
    {
        #region Pens & Brushes

        //private Rectangle[] rects0;
        //private Rectangle[] rects1;

        //private LinearGradientBrush
        //    brush00, brush01,
        //    brush02, brush03, brush05, brush07, brush09;
        //private SolidBrush
        //          brush06, brush08, _brush01, _brush02;//,brush04
        //private Pen
        //    pen01, pen02,
        //    pen03, pen04,
        //    pen05, pen06,
        //    pen07, pen08,
        //    pen09, pen10,
        //    pen11, pen12, pen13,
        //    //pen14, pen15, pen16,
        //    pen17, pen18, pen19, pen20, pen21, pen22, pen23, pen24, _pen01, _pen02;

        #endregion

        #region Members

        private Size MinSize = new Size(10, 10);
        internal ILayout owner;
        private short prefHeightCache;
        private int requestedHeight;
        private bool integralHeightAdjust;
        private bool m_FixSize;
        //private ButtonPainter xpPainter;
        [Description("OnFixedSizeChanged"), Category("PropertyChanged")]
        public event EventHandler FixedSizeChanged;


        #endregion

        #region Constructors

        public McButton()
            : base()
        {
            base.ControlLayout = ControlLayout.XpLayout;

            m_FixSize = false;
            this.prefHeightCache = -1;
            this.integralHeightAdjust = false;
            //base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.FixedHeight, m_FixSize);
            this.requestedHeight = this.DefaultSize.Height;
            this.requestedHeight = base.Height;
            //xpPainter = new ButtonPainter(this);
        }

        //internal McButton(bool net)
        //    : this()
        //{
        //    this.m_netFram = net;
        //}

        protected override void Dispose(bool disposing)
        {
            //painter.DisposePensBrushes();
            base.Dispose(disposing);
        }
        #endregion

        #region Adsust

        private void AdjustHeight()
        {
            if ((this.Anchor & (AnchorStyles.Bottom | AnchorStyles.Top)) != (AnchorStyles.Bottom | AnchorStyles.Top))
            {
                this.prefHeightCache = -1;
                base.FontHeight = -1;
                int num1 = this.requestedHeight;
                try
                {
                    if (m_FixSize)
                    {
                        base.Height = this.PreferredHeight;
                    }
                    else
                    {
                        int num2 = base.Height;
                        //if (this.ctlFlags[TextBoxBase.multiline])
                        //{
                        base.Height = Math.Max(num1, this.PreferredHeight + 2);
                        //}
                        this.integralHeightAdjust = true;
                        try
                        {
                            base.Height = num1;
                        }
                        finally
                        {
                            this.integralHeightAdjust = false;
                        }
                    }
                }
                finally
                {
                    this.requestedHeight = num1;
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("PreferredHeight"), Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), Category("Layout")]
        public virtual int PreferredHeight
        {
            get
            {
                if (this.prefHeightCache > -1)
                {
                    return this.prefHeightCache;
                }
                int num1 = base.FontHeight;
                //				if (this.BorderStyle != BorderStyle.None)
                //				{
                num1 += (SystemInformation.BorderSize.Height * 4) + 3;
                //				}
                this.prefHeightCache = (short)num1;
                return num1;
            }
        }

        protected virtual void OnFixedSizeChanged(EventArgs e)
        {
            if (FixedSizeChanged != null)
                this.FixedSizeChanged(this, e);
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if (!this.integralHeightAdjust && (height != base.Height))
            {
                this.requestedHeight = height;
            }
            if (m_FixSize)
            {
                height = this.PreferredHeight;
            }
            base.SetBoundsCore(x, y, width, height, specified);
        }

        [Localizable(true), Category("Behavior"), Description("FixSize"), DefaultValue(false), RefreshProperties(RefreshProperties.Repaint)]
        public virtual bool FixSize
        {
            get
            {
                return m_FixSize;
            }
            set
            {
                if (m_FixSize != value)
                {
                    m_FixSize = value;
                    if (this.m_FixSize)//!this.Multiline)
                    {
                        base.SetStyle(ControlStyles.FixedHeight, value);
                        this.AdjustHeight();
                    }
                    this.OnFixedSizeChanged(EventArgs.Empty);
                }
            }
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            this.AdjustHeight();//this.SetHeight();
        }

        #endregion

        #region Protected Methods

        protected override void OnSizeChanged(System.EventArgs e)
        {
            base.OnSizeChanged(e);
            if (this.Width < MinSize.Width)
                this.Width = MinSize.Width;
            if (this.Height < MinSize.Height)
                this.Height = MinSize.Height;

        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            Rectangle bounds = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            //0,60,116
            //if (this.ControlLayout == ControlLayout.XpLayout)
            //{
                //xpPainter.PaintButtonXp(e);
            //}
            if (this.owner != null)
                this.owner.LayoutManager.DrawButton(e.Graphics, bounds, this, this.ControlLayout, 3, base.isDefault);
            else
                this.LayoutManager.DrawButton(e.Graphics, bounds, this, this.ControlLayout, 3, base.isDefault);
        }

        protected override void OnParentChanged(System.EventArgs e)
        {
            if (Parent == null) return;
            this.BackColor = Color.FromArgb(0, this.Parent.BackColor);
            base.OnParentChanged(e);
        }
        #endregion

        #region PaintXP

        //private void PaintButtonXp(System.Windows.Forms.PaintEventArgs e)
        //{

        //    int X = this.Width;
        //    int Y = this.Height;
        //    Styles style = base.LayoutManager.StylePlan;

        //    CreatePensBrushes(style);

        //    e.Graphics.CompositingQuality = CompositingQuality.GammaCorrected;
        //    if (!this.Enabled)
        //    {
        //        e.Graphics.FillRectangle(_brush02, 2, 2, X - 4, Y - 4);
        //        e.Graphics.DrawLine(_pen01, 3, 1, X - 4, 1);
        //        e.Graphics.DrawLine(_pen01, 3, Y - 2, X - 4, Y - 2);
        //        e.Graphics.DrawLine(_pen01, 1, 3, 1, Y - 4);
        //        e.Graphics.DrawLine(_pen01, X - 2, 3, X - 2, Y - 4);

        //        e.Graphics.DrawLine(_pen02, 1, 2, 2, 1);
        //        e.Graphics.DrawLine(_pen02, 1, Y - 3, 2, Y - 2);
        //        e.Graphics.DrawLine(_pen02, X - 2, 2, X - 3, 1);
        //        e.Graphics.DrawLine(_pen02, X - 2, Y - 3, X - 3, Y - 2);
        //        e.Graphics.FillRectangles(_brush01, rects1);
        //    }
        //    else
        //    {

        //        e.Graphics.FillRectangle(brush00, new Rectangle(0, 0, X, Y));
        //        switch (state)
        //        {
        //            case ButtonStates.Normal:

        //                //e.Graphics.FillRectangle(silverBrush06, 2, 2, X - 4, Y - 4);
        //                //e.Graphics.FillRectangle(silverBrush07, 3, 4, X - 6, Y - 8);
        //                //e.Graphics.FillRectangle(silverBrush08, 2, Y - 4, X - 4, 2);

        //                e.Graphics.FillRectangle(brush01, 2, 2, X - 4, Y - 7);
        //                e.Graphics.DrawLine(pen01, 2, Y - 5, X - 2, Y - 5);
        //                e.Graphics.DrawLine(pen02, 2, Y - 4, X - 2, Y - 4);
        //                e.Graphics.DrawLine(pen03, 2, Y - 3, X - 2, Y - 3);
        //                e.Graphics.DrawLine(pen04, X - 4, 4, X - 4, Y - 5);
        //                e.Graphics.DrawLine(pen05, X - 3, 4, X - 3, Y - 5);

        //                if (isDefault)
        //                {
        //                    e.Graphics.FillRectangles(brush02, rects0);
        //                    e.Graphics.DrawLine(pen06, 2, 2, X - 3, 2);
        //                    e.Graphics.DrawLine(pen07, 2, 3, X - 3, 3);
        //                    e.Graphics.DrawLine(pen08, 2, Y - 4, X - 3, Y - 4);
        //                    e.Graphics.DrawLine(pen09, 2, Y - 3, X - 3, Y - 3);
        //                }

        //                break;

        //            case ButtonStates.MouseOver:

        //                //e.Graphics.FillRectangle(silverBrush06, 2, 2, X - 4, Y - 4);
        //                //e.Graphics.FillRectangle(silverBrush07, 3, 4, X - 6, Y - 8);
        //                //e.Graphics.FillRectangle(silverBrush08, 2, Y - 4, X - 4, 2);

        //                e.Graphics.FillRectangle(brush01, 2, 2, X - 4, Y - 7);
        //                e.Graphics.DrawLine(pen01, 2, Y - 5, X - 4, Y - 5);
        //                e.Graphics.DrawLine(pen02, 2, Y - 4, X - 4, Y - 4);
        //                e.Graphics.DrawLine(pen03, 2, Y - 3, X - 4, Y - 3);
        //                e.Graphics.DrawLine(pen04, X - 4, 4, X - 4, Y - 5);
        //                e.Graphics.DrawLine(pen05, X - 3, 4, X - 3, Y - 5);

        //                e.Graphics.FillRectangles(brush03, rects0);
        //                e.Graphics.DrawLine(pen10, 2, 2, X - 3, 2);
        //                e.Graphics.DrawLine(pen11, 2, 3, X - 3, 3);
        //                e.Graphics.DrawLine(pen12, 2, Y - 4, X - 3, Y - 4);
        //                e.Graphics.DrawLine(pen13, 2, Y - 3, X - 3, Y - 3);

        //                break;

        //            case ButtonStates.Pushed:
        //                //e.Graphics.FillRectangle(silverBrush06, 2, 2, X - 4, Y - 4);
        //                //e.Graphics.FillRectangle(brush09, 3, 4, X - 6, Y - 9);
        //                //e.Graphics.DrawLine(pen24, 4, 3, X - 4, 3);

        //                e.Graphics.FillRectangle(brush05, 2, 4, X - 4, Y - 8);
        //                e.Graphics.DrawLine(pen17, 2, 3, 2, Y - 4);
        //                e.Graphics.DrawLine(pen18, 3, 3, 3, Y - 4);
        //                e.Graphics.DrawLine(pen19, 2, 2, X - 3, 2);
        //                e.Graphics.DrawLine(pen20, 2, 3, X - 3, 3);
        //                e.Graphics.DrawLine(pen21, 2, Y - 4, X - 3, Y - 4);
        //                e.Graphics.DrawLine(pen22, 2, Y - 3, X - 3, Y - 3);
        //                break;
        //        }

        //        //if (this.Focused) ControlPaint.DrawFocusRectangle(e.Graphics,
        //        //    new Rectangle(3, 3, X - 6, Y - 6), Color.Black, this.BackColor);
        //    }

        //    //base.OnPaint(e);
        //    DisposePensBrushes();
        //}

        //protected override void CreatePensBrushes(Styles m_Style)
        //{
        //    DisposePensBrushes();
        //    if (Region == null) return;

        //    int X = this.Width;
        //    int Y = this.Height;

        //    brush00 = ColorManager.Brush00(new Rectangle(0, 0, X, Y));
        //    brush01 = ColorManager.Brush01(m_Style, new Rectangle(2, 2, X - 5, Y - 7));
        //    brush05 = ColorManager.Brush05(m_Style, new Rectangle(2, 2, X - 5, Y - 7));

        //    pen17 = ColorManager.Pen17(m_Style, new Rectangle(2, 3, X - 4, Y - 7));
        //    pen18 = ColorManager.Pen18(m_Style, new Rectangle(3, 3, X - 4, Y - 7));
        //    pen19 = ColorManager.Pen19(m_Style);
        //    pen20 = ColorManager.Pen20(m_Style);
        //    pen21 = ColorManager.Pen21(m_Style);
        //    pen22 = ColorManager.Pen22(m_Style);

        //    brush06 = ColorManager.Brush06();
        //    brush07 = ColorManager.Brush07(new Rectangle(3, 3, X - 6, Y - 7));
        //    brush08 = ColorManager.Brush08();

        //    brush02 = ColorManager.Brush02(m_Style, new Rectangle(2, 3, X - 4, Y - 7));
        //    brush03 = ColorManager.Brush03(m_Style, new Rectangle(2, 3, X - 4, Y - 7));
        //    //brush04 = ColorManager.Brush04(m_Style);

        //    pen06 = ColorManager.Pen06(m_Style);
        //    pen07 = ColorManager.Pen07(m_Style);
        //    pen08 = ColorManager.Pen08(m_Style);

        //    pen09 = ColorManager.Pen09(m_Style);
        //    pen10 = ColorManager.Pen10(m_Style);
        //    pen11 = ColorManager.Pen11(m_Style);
        //    pen12 = ColorManager.Pen12(m_Style);
        //    pen13 = ColorManager.Pen13(m_Style);
        //    //pen14 = ColorManager.Pen14(m_Style);
        //    //pen15 = ColorManager.Pen15(m_Style);
        //    //pen16 = ColorManager.Pen16(m_Style);

        //    brush09 = ColorManager.Brush09(new Rectangle(3, 3, X - 5, Y - 8));
        //    pen23 = ColorManager.Pen23();
        //    pen24 = ColorManager.Pen24();

        //    pen01 = ColorManager.Pen01(m_Style);
        //    pen02 = ColorManager.Pen02(m_Style);
        //    pen03 = ColorManager.Pen03(m_Style);
        //    pen04 = ColorManager.Pen04(m_Style, new Rectangle(X - 3, 4, 1, Y - 5));
        //    pen05 = ColorManager.Pen05(m_Style, new Rectangle(X - 2, 4, 1, Y - 5));

        //    _brush01 = ColorManager._Brush01(m_Style);
        //    _brush02 = ColorManager._Brush02(m_Style);
        //    _pen01 = ColorManager._Pen01(m_Style);
        //    _pen02 = ColorManager._Pen02(m_Style);


        //}

        //protected override void DisposePensBrushes()
        //{
        //    if (brush01 != null) brush01.Dispose();

        //    if (brush02 != null) brush02.Dispose();
        //    if (brush03 != null) brush03.Dispose();

        //    //if (brush04 != null) brush04.Dispose();

        //    if (brush05 != null) brush05.Dispose();
        //    if (brush06 != null) brush06.Dispose();
        //    if (brush07 != null) brush07.Dispose();
        //    if (brush08 != null) brush08.Dispose();
        //    if (brush09 != null) brush09.Dispose();

        //    if (pen01 != null) pen01.Dispose();
        //    if (pen02 != null) pen02.Dispose();
        //    if (pen03 != null) pen03.Dispose();
        //    if (pen04 != null) pen04.Dispose();
        //    if (pen05 != null) pen05.Dispose();
        //    if (pen06 != null) pen06.Dispose();
        //    if (pen07 != null) pen07.Dispose();
        //    if (pen08 != null) pen08.Dispose();
        //    if (pen09 != null) pen09.Dispose();
        //    if (pen10 != null) pen10.Dispose();
        //    if (pen11 != null) pen11.Dispose();
        //    if (pen12 != null) pen12.Dispose();
        //    if (pen13 != null) pen13.Dispose();
        //    //if (pen14 != null) pen14.Dispose();
        //    //if (pen15 != null) pen15.Dispose();
        //    //if (pen16 != null) pen16.Dispose();

        //    if (pen17 != null) pen17.Dispose();
        //    if (pen18 != null) pen18.Dispose();
        //    if (pen19 != null) pen19.Dispose();
        //    if (pen20 != null) pen20.Dispose();
        //    if (pen21 != null) pen21.Dispose();
        //    if (pen22 != null) pen22.Dispose();
        //    if (pen23 != null) pen23.Dispose();
        //    if (pen24 != null) pen24.Dispose();

        //    if (_brush01 != null) _brush01.Dispose();
        //    if (_brush02 != null) _brush02.Dispose();
        //    if (_pen01 != null) _pen01.Dispose();
        //    if (_pen02 != null) _pen02.Dispose();

        //    base.DisposePensBrushes();
        //}

        //protected override void CreateRegion()
        //{
        //    int X = this.Width;
        //    int Y = this.Height;

        //    rects0 = new Rectangle[2];
        //    rects0[0] = new Rectangle(2, 4, 2, Y - 8);
        //    rects0[1] = new Rectangle(X - 4, 4, 2, Y - 8);

        //    rects1 = new Rectangle[8];
        //    rects1[0] = new Rectangle(2, 1, 2, 2);
        //    rects1[1] = new Rectangle(1, 2, 2, 2);
        //    rects1[2] = new Rectangle(X - 4, 1, 2, 2);
        //    rects1[3] = new Rectangle(X - 3, 2, 2, 2);
        //    rects1[4] = new Rectangle(2, Y - 3, 2, 2);
        //    rects1[5] = new Rectangle(1, Y - 4, 2, 2);
        //    rects1[6] = new Rectangle(X - 4, Y - 3, 2, 2);
        //    rects1[7] = new Rectangle(X - 3, Y - 4, 2, 2);

        //    Point[] points = {
        //                         new Point(1, 0),
        //                         new Point(X-1, 0),
        //                         new Point(X-1, 1),
        //                         new Point(X, 1),
        //                         new Point(X, Y-1),
        //                         new Point(X-1, Y-1),
        //                         new Point(X-1, Y),
        //                         new Point(1, Y),
        //                         new Point(1, Y-1),
        //                         new Point(0, Y-1),
        //                         new Point(0, 1),
        //                         new Point(1, 1)};

        //    GraphicsPath path = new GraphicsPath();
        //    path.AddLines(points);

        //    this.Region = new Region(path);
        //    base.CreateRegion();
        //}

        #endregion

        #region Virtual methods

        protected override void CreateRegion()
        {
           // xpPainter.CreateRegion();
        }
        protected override void CreatePensBrushes(Styles style)
        {
            //xpPainter.CreatePensBrushes(style);
        }
        protected override void DisposePensBrushes()
        {
            //xpPainter.DisposePensBrushes();
        }

        #endregion

        public class ButtonPainter
        {

            #region Pens & Brushes

            private Rectangle[] rects0;
            private Rectangle[] rects1;

            private LinearGradientBrush
                brush00, brush01,
                brush02, brush03, brush05, brush07, brush09;
            private SolidBrush
                      brush06, brush08, _brush01, _brush02;//,brush04
            private Pen
                pen01, pen02,
                pen03, pen04,
                pen05, pen06,
                pen07, pen08,
                pen09, pen10,
                pen11, pen12, pen13,
                //pen14, pen15, pen16,
                pen17, pen18, pen19, pen20, pen21, pen22, pen23, pen24, _pen01, _pen02;

            #endregion

            #region Constructors

            private McButton Mc;

            public ButtonPainter(McButton ctl)
            {
                Mc = ctl;
            }

            #endregion

            #region Protected Methods


            internal void PaintButtonXp(System.Windows.Forms.PaintEventArgs e)
            {

                int X = Mc.Width;
                int Y = Mc.Height;
                Styles style = Mc.LayoutManager.StylePlan;

                CreatePensBrushes(style);

                e.Graphics.CompositingQuality = CompositingQuality.GammaCorrected;
                if (!Mc.Enabled)
                {
                    e.Graphics.FillRectangle(_brush02, 2, 2, X - 4, Y - 4);
                    e.Graphics.DrawLine(_pen01, 3, 1, X - 4, 1);
                    e.Graphics.DrawLine(_pen01, 3, Y - 2, X - 4, Y - 2);
                    e.Graphics.DrawLine(_pen01, 1, 3, 1, Y - 4);
                    e.Graphics.DrawLine(_pen01, X - 2, 3, X - 2, Y - 4);

                    e.Graphics.DrawLine(_pen02, 1, 2, 2, 1);
                    e.Graphics.DrawLine(_pen02, 1, Y - 3, 2, Y - 2);
                    e.Graphics.DrawLine(_pen02, X - 2, 2, X - 3, 1);
                    e.Graphics.DrawLine(_pen02, X - 2, Y - 3, X - 3, Y - 2);
                    e.Graphics.FillRectangles(_brush01, rects1);
                }
                else
                {

                    e.Graphics.FillRectangle(brush00, new Rectangle(0, 0, X, Y));
                    switch (Mc.state)
                    {
                        case ButtonStates.Normal:

                            //e.Graphics.FillRectangle(silverBrush06, 2, 2, X - 4, Y - 4);
                            //e.Graphics.FillRectangle(silverBrush07, 3, 4, X - 6, Y - 8);
                            //e.Graphics.FillRectangle(silverBrush08, 2, Y - 4, X - 4, 2);

                            e.Graphics.FillRectangle(brush01, 2, 2, X - 4, Y - 7);
                            e.Graphics.DrawLine(pen01, 2, Y - 5, X - 2, Y - 5);
                            e.Graphics.DrawLine(pen02, 2, Y - 4, X - 2, Y - 4);
                            e.Graphics.DrawLine(pen03, 2, Y - 3, X - 2, Y - 3);
                            e.Graphics.DrawLine(pen04, X - 4, 4, X - 4, Y - 5);
                            e.Graphics.DrawLine(pen05, X - 3, 4, X - 3, Y - 5);

                            if (Mc.isDefault)
                            {
                                e.Graphics.FillRectangles(brush02, rects0);
                                e.Graphics.DrawLine(pen06, 2, 2, X - 3, 2);
                                e.Graphics.DrawLine(pen07, 2, 3, X - 3, 3);
                                e.Graphics.DrawLine(pen08, 2, Y - 4, X - 3, Y - 4);
                                e.Graphics.DrawLine(pen09, 2, Y - 3, X - 3, Y - 3);
                            }

                            break;

                        case ButtonStates.MouseOver:

                            //e.Graphics.FillRectangle(silverBrush06, 2, 2, X - 4, Y - 4);
                            //e.Graphics.FillRectangle(silverBrush07, 3, 4, X - 6, Y - 8);
                            //e.Graphics.FillRectangle(silverBrush08, 2, Y - 4, X - 4, 2);

                            e.Graphics.FillRectangle(brush01, 2, 2, X - 4, Y - 7);
                            e.Graphics.DrawLine(pen01, 2, Y - 5, X - 4, Y - 5);
                            e.Graphics.DrawLine(pen02, 2, Y - 4, X - 4, Y - 4);
                            e.Graphics.DrawLine(pen03, 2, Y - 3, X - 4, Y - 3);
                            e.Graphics.DrawLine(pen04, X - 4, 4, X - 4, Y - 5);
                            e.Graphics.DrawLine(pen05, X - 3, 4, X - 3, Y - 5);

                            e.Graphics.FillRectangles(brush03, rects0);
                            e.Graphics.DrawLine(pen10, 2, 2, X - 3, 2);
                            e.Graphics.DrawLine(pen11, 2, 3, X - 3, 3);
                            e.Graphics.DrawLine(pen12, 2, Y - 4, X - 3, Y - 4);
                            e.Graphics.DrawLine(pen13, 2, Y - 3, X - 3, Y - 3);

                            break;

                        case ButtonStates.Pushed:
                            //e.Graphics.FillRectangle(silverBrush06, 2, 2, X - 4, Y - 4);
                            //e.Graphics.FillRectangle(brush09, 3, 4, X - 6, Y - 9);
                            //e.Graphics.DrawLine(pen24, 4, 3, X - 4, 3);

                            e.Graphics.FillRectangle(brush05, 2, 4, X - 4, Y - 8);
                            e.Graphics.DrawLine(pen17, 2, 3, 2, Y - 4);
                            e.Graphics.DrawLine(pen18, 3, 3, 3, Y - 4);
                            e.Graphics.DrawLine(pen19, 2, 2, X - 3, 2);
                            e.Graphics.DrawLine(pen20, 2, 3, X - 3, 3);
                            e.Graphics.DrawLine(pen21, 2, Y - 4, X - 3, Y - 4);
                            e.Graphics.DrawLine(pen22, 2, Y - 3, X - 3, Y - 3);
                            break;
                    }

                    //if (this.Focused) ControlPaint.DrawFocusRectangle(e.Graphics,
                    //    new Rectangle(3, 3, X - 6, Y - 6), Color.Black, this.BackColor);
                }

                //base.OnPaint(e);
                DisposePensBrushes();
            }


            internal void CreatePensBrushes(Styles m_Style)
            {
                DisposePensBrushes();
                if (Mc.Region == null) return;

                int X = Mc.Width;
                int Y = Mc.Height;

                brush00 = ColorManager.Brush00(new Rectangle(0, 0, X, Y));
                brush01 = ColorManager.Brush01(m_Style, new Rectangle(2, 2, X - 5, Y - 7));
                brush05 = ColorManager.Brush05(m_Style, new Rectangle(2, 2, X - 5, Y - 7));

                pen17 = ColorManager.Pen17(m_Style, new Rectangle(2, 3, X - 4, Y - 7));
                pen18 = ColorManager.Pen18(m_Style, new Rectangle(3, 3, X - 4, Y - 7));
                pen19 = ColorManager.Pen19(m_Style);
                pen20 = ColorManager.Pen20(m_Style);
                pen21 = ColorManager.Pen21(m_Style);
                pen22 = ColorManager.Pen22(m_Style);

                brush06 = ColorManager.Brush06();
                brush07 = ColorManager.Brush07(new Rectangle(3, 3, X - 6, Y - 7));
                brush08 = ColorManager.Brush08();

                brush02 = ColorManager.Brush02(m_Style, new Rectangle(2, 3, X - 4, Y - 7));
                brush03 = ColorManager.Brush03(m_Style, new Rectangle(2, 3, X - 4, Y - 7));
                //brush04 = ColorManager.Brush04(m_Style);

                pen06 = ColorManager.Pen06(m_Style);
                pen07 = ColorManager.Pen07(m_Style);
                pen08 = ColorManager.Pen08(m_Style);

                pen09 = ColorManager.Pen09(m_Style);
                pen10 = ColorManager.Pen10(m_Style);
                pen11 = ColorManager.Pen11(m_Style);
                pen12 = ColorManager.Pen12(m_Style);
                pen13 = ColorManager.Pen13(m_Style);
                //pen14 = ColorManager.Pen14(m_Style);
                //pen15 = ColorManager.Pen15(m_Style);
                //pen16 = ColorManager.Pen16(m_Style);

                brush09 = ColorManager.Brush09(new Rectangle(3, 3, X - 5, Y - 8));
                pen23 = ColorManager.Pen23();
                pen24 = ColorManager.Pen24();

                pen01 = ColorManager.Pen01(m_Style);
                pen02 = ColorManager.Pen02(m_Style);
                pen03 = ColorManager.Pen03(m_Style);
                pen04 = ColorManager.Pen04(m_Style, new Rectangle(X - 3, 4, 1, Y - 5));
                pen05 = ColorManager.Pen05(m_Style, new Rectangle(X - 2, 4, 1, Y - 5));

                _brush01 = ColorManager._Brush01(m_Style);
                _brush02 = ColorManager._Brush02(m_Style);
                _pen01 = ColorManager._Pen01(m_Style);
                _pen02 = ColorManager._Pen02(m_Style);


            }

            internal void DisposePensBrushes()
            {
                if (brush01 != null) brush01.Dispose();

                if (brush02 != null) brush02.Dispose();
                if (brush03 != null) brush03.Dispose();

                //if (brush04 != null) brush04.Dispose();

                if (brush05 != null) brush05.Dispose();
                if (brush06 != null) brush06.Dispose();
                if (brush07 != null) brush07.Dispose();
                if (brush08 != null) brush08.Dispose();
                if (brush09 != null) brush09.Dispose();

                if (pen01 != null) pen01.Dispose();
                if (pen02 != null) pen02.Dispose();
                if (pen03 != null) pen03.Dispose();
                if (pen04 != null) pen04.Dispose();
                if (pen05 != null) pen05.Dispose();
                if (pen06 != null) pen06.Dispose();
                if (pen07 != null) pen07.Dispose();
                if (pen08 != null) pen08.Dispose();
                if (pen09 != null) pen09.Dispose();
                if (pen10 != null) pen10.Dispose();
                if (pen11 != null) pen11.Dispose();
                if (pen12 != null) pen12.Dispose();
                if (pen13 != null) pen13.Dispose();
                //if (pen14 != null) pen14.Dispose();
                //if (pen15 != null) pen15.Dispose();
                //if (pen16 != null) pen16.Dispose();

                if (pen17 != null) pen17.Dispose();
                if (pen18 != null) pen18.Dispose();
                if (pen19 != null) pen19.Dispose();
                if (pen20 != null) pen20.Dispose();
                if (pen21 != null) pen21.Dispose();
                if (pen22 != null) pen22.Dispose();
                if (pen23 != null) pen23.Dispose();
                if (pen24 != null) pen24.Dispose();

                if (_brush01 != null) _brush01.Dispose();
                if (_brush02 != null) _brush02.Dispose();
                if (_pen01 != null) _pen01.Dispose();
                if (_pen02 != null) _pen02.Dispose();

                //Mc.DisposePensBrushes();
            }

            internal void CreateRegion()
            {
                int X = Mc.Width;
                int Y = Mc.Height;

                rects0 = new Rectangle[2];
                rects0[0] = new Rectangle(2, 4, 2, Y - 8);
                rects0[1] = new Rectangle(X - 4, 4, 2, Y - 8);

                rects1 = new Rectangle[8];
                rects1[0] = new Rectangle(2, 1, 2, 2);
                rects1[1] = new Rectangle(1, 2, 2, 2);
                rects1[2] = new Rectangle(X - 4, 1, 2, 2);
                rects1[3] = new Rectangle(X - 3, 2, 2, 2);
                rects1[4] = new Rectangle(2, Y - 3, 2, 2);
                rects1[5] = new Rectangle(1, Y - 4, 2, 2);
                rects1[6] = new Rectangle(X - 4, Y - 3, 2, 2);
                rects1[7] = new Rectangle(X - 3, Y - 4, 2, 2);

                Point[] points = {
								 new Point(1, 0),
								 new Point(X-1, 0),
								 new Point(X-1, 1),
								 new Point(X, 1),
								 new Point(X, Y-1),
								 new Point(X-1, Y-1),
								 new Point(X-1, Y),
								 new Point(1, Y),
								 new Point(1, Y-1),
								 new Point(0, Y-1),
								 new Point(0, 1),
								 new Point(1, 1)};

                GraphicsPath path = new GraphicsPath();
                path.AddLines(points);

                Mc.Region = new Region(path);
                //Mc.CreateRegion();
            }

            #endregion

        }
 
    }

}

