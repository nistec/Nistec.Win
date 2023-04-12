using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Nistec.Drawing;
using System.Security.Permissions;
using Nistec.Win32;

namespace Nistec.WinForms.Controls
{

    public enum ButtonComboType
    {
        Combo = 0,
        Brows = 1,
        Up = 2,
        Down = 3,
        Custom = 4
    }


    [DefaultEvent("Click")]
    [ToolboxItem(false)]
    internal class McButtonCombo : McButtonBase
    {

        #region Members


        private ButtonComboType m_ImageType;
        protected ILayout owner;
        internal bool buttonMedia;
        #endregion

        #region Constructors

        internal McButtonCombo(ILayout ctl)
            : base()
        {
            owner = ctl;
            base.ControlLayout = ControlLayout.Visual;
            InitButtonCombo(ButtonComboType.Combo);
        }

        internal McButtonCombo(ILayout ctl, string image)
            : base()
        {
            owner = ctl;
            base.ControlLayout = ControlLayout.Visual;
            InitButtonCombo(ButtonComboType.Custom);
            base.m_Image = ResourceUtil.LoadImage(Global.ImagesPath + image);
        }

        internal McButtonCombo(ILayout ctl, ButtonComboType imageType)
            : base()
        {
            owner = ctl;
            InitButtonCombo(imageType);
        }

        private void InitButtonCombo(ButtonComboType imageType)
        {

            // 
            // McButton
            // 
            this.Name = "McButtonCombo";
            this.Size = new System.Drawing.Size(20, 20);
            this.ResizeRedraw = true;
            this.AutoToolTip = false;

            m_ImageType = imageType;

            if (imageType == ButtonComboType.Brows)
                base.m_Image = ResourceUtil.LoadImage(Global.ImagesPath + "btnBrows.gif");
            else if (imageType != ButtonComboType.Custom)
                base.m_Image = ResourceUtil.LoadImage(Global.ImagesPath + "btnCombo.gif");

        }
        #endregion

        #region Dispose

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        #endregion

        #region Properties

        public ButtonComboType ButtonComboType
        {
            get { return m_ImageType; }
            set
            {
                m_ImageType = value;
                if (value == ButtonComboType.Combo)
                    this.m_Image = ResourceUtil.LoadImage(Global.ImagesPath + "btnCombo.gif");
                else if (value == ButtonComboType.Brows)
                    this.m_Image = ResourceUtil.LoadImage(Global.ImagesPath + "btnBrows.gif");
                else if (value == ButtonComboType.Down)
                    this.m_Image = ResourceUtil.LoadImage(Global.ImagesPath + "downSmallArrow.gif");
                else if (value == ButtonComboType.Up)
                    this.m_Image = ResourceUtil.LoadImage(Global.ImagesPath + "upSmallArrow.gif");
                this.Invalidate();
            }
        }

        public bool IsMouseDown
        {
            get { return m_MouseDown; }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint (e);
           
            Rectangle bounds = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            if (owner != null)
                owner.LayoutManager.DrawButtonRect(e.Graphics, bounds, this, ControlLayout);
            else
                base.LayoutManager.DrawButtonRect(e.Graphics, bounds, this, ControlLayout);

            if (m_Image != null)
            {
                PointF iPoint = new Point(0, 0);
                if (buttonMedia)
                    iPoint.Y = 1 + (bounds.Height - m_Image.Height);
                else
                    iPoint.Y = ((bounds.Height - m_Image.Height) / 2);

                iPoint.X = ((bounds.Width - m_Image.Width) / 2);
                if (this.Enabled)
                {
                    e.Graphics.DrawImage(m_Image, (int)iPoint.X + 1, (int)iPoint.Y);
                }
                else
                {
                    e.Graphics.DrawImage(m_Image, (int)iPoint.X + 1, (int)iPoint.Y);
                    //ControlPaint.DrawImageDisabled(g, m_Image, (int)iPoint.X+1, (int)iPoint.Y,this.BackColor);//Parent.BackColor);
                }
            }
        }

        #endregion

    }
}
