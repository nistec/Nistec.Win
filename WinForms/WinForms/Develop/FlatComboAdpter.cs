using System;
using System.Collections.Generic;
using System.Text;

namespace mControl.WinCtl.Develop
{
    internal class FlatComboAdapter
    {
        // Fields
        private Rectangle clientRect;
        internal Rectangle dropDownRect;
        private Rectangle innerBorder;
        private Rectangle innerInnerBorder;
        private RightToLeft origRightToLeft;
        private Rectangle outerBorder;
        private Rectangle whiteFillRect;
        private const int WhiteFillRectWidth = 5;

        // Methods
        public FlatComboAdapter(ComboBox comboBox, bool smallButton)
        {
            this.clientRect = comboBox.ClientRectangle;
            int width = SystemInformation.HorizontalScrollBarArrowWidth;
            this.outerBorder = new Rectangle(this.clientRect.Location, new Size(this.clientRect.Width - 1, this.clientRect.Height - 1));
            this.innerBorder = new Rectangle(this.outerBorder.X + 1, this.outerBorder.Y + 1, (this.outerBorder.Width - width) - 2, this.outerBorder.Height - 2);
            this.innerInnerBorder = new Rectangle(this.innerBorder.X + 1, this.innerBorder.Y + 1, this.innerBorder.Width - 2, this.innerBorder.Height - 2);
            this.dropDownRect = new Rectangle(this.innerBorder.Right + 1, this.innerBorder.Y, width, this.innerBorder.Height + 1);
            if (smallButton)
            {
                this.whiteFillRect = this.dropDownRect;
                this.whiteFillRect.Width = 5;
                this.dropDownRect.X += 5;
                this.dropDownRect.Width -= 5;
            }
            this.origRightToLeft = comboBox.RightToLeft;
            if (this.origRightToLeft == RightToLeft.Yes)
            {
                this.innerBorder.X = this.clientRect.Width - this.innerBorder.Right;
                this.innerInnerBorder.X = this.clientRect.Width - this.innerInnerBorder.Right;
                this.dropDownRect.X = this.clientRect.Width - this.dropDownRect.Right;
                this.whiteFillRect.X = (this.clientRect.Width - this.whiteFillRect.Right) + 1;
            }
        }

        public virtual void DrawFlatCombo(ComboBox comboBox, Graphics g)
        {
            if (comboBox.DropDownStyle == ComboBoxStyle.Simple)
            {
            }
            Color c = this.GetOuterBorderColor(comboBox);
            Color innerBorderColor = this.GetInnerBorderColor(comboBox);
            bool flag = comboBox.RightToLeft == RightToLeft.Yes;
            this.DrawFlatComboDropDown(comboBox, g, this.dropDownRect);
            if (!LayoutUtils.IsZeroWidthOrHeight(this.whiteFillRect))
            {
                using (Brush brush = new SolidBrush(innerBorderColor))
                {
                    g.FillRectangle(brush, this.whiteFillRect);
                }
            }
            if (c.IsSystemColor)
            {
                Pen pen = SystemPens.FromSystemColor(c);
                g.DrawRectangle(pen, this.outerBorder);
                if (flag)
                {
                    g.DrawRectangle(pen, new Rectangle(this.outerBorder.X, this.outerBorder.Y, this.dropDownRect.Width + 1, this.outerBorder.Height));
                }
                else
                {
                    g.DrawRectangle(pen, new Rectangle(this.dropDownRect.X, this.outerBorder.Y, this.outerBorder.Right - this.dropDownRect.X, this.outerBorder.Height));
                }
            }
            else
            {
                using (Pen pen = new Pen(c))
                {
                    g.DrawRectangle(pen, this.outerBorder);
                    if (flag)
                    {
                        g.DrawRectangle(pen, new Rectangle(this.outerBorder.X, this.outerBorder.Y, this.dropDownRect.Width + 1, this.outerBorder.Height));
                    }
                    else
                    {
                        g.DrawRectangle(pen, new Rectangle(this.dropDownRect.X, this.outerBorder.Y, this.outerBorder.Right - this.dropDownRect.X, this.outerBorder.Height));
                    }
                }
            }
            if (innerBorderColor.IsSystemColor)
            {
                Pen pen = SystemPens.FromSystemColor(innerBorderColor);
                g.DrawRectangle(pen, this.innerBorder);
                g.DrawRectangle(pen, this.innerInnerBorder);
            }
            else
            {
                using (Pen pen = new Pen(innerBorderColor))
                {
                    g.DrawRectangle(pen, this.innerBorder);
                    g.DrawRectangle(pen, this.innerInnerBorder);
                }
            }
            if (!comboBox.Enabled || (comboBox.FlatStyle == FlatStyle.Popup))
            {
                bool focused = comboBox.ContainsFocus || comboBox.MouseIsOver;
                using (Pen pen = new Pen(this.GetPopupOuterBorderColor(comboBox, focused)))
                {
                    Pen pen6 = comboBox.Enabled ? pen : SystemPens.Control;
                    if (flag)
                    {
                        g.DrawRectangle(pen6, new Rectangle(this.outerBorder.X, this.outerBorder.Y, this.dropDownRect.Width + 1, this.outerBorder.Height));
                    }
                    else
                    {
                        g.DrawRectangle(pen6, new Rectangle(this.dropDownRect.X, this.outerBorder.Y, this.outerBorder.Right - this.dropDownRect.X, this.outerBorder.Height));
                    }
                    g.DrawRectangle(pen, this.outerBorder);
                }
            }
        }

        protected virtual void DrawFlatComboDropDown(ComboBox comboBox, Graphics g, Rectangle dropDownRect)
        {
            g.FillRectangle(SystemBrushes.Control, dropDownRect);
            Brush brush = comboBox.Enabled ? SystemBrushes.ControlText : SystemBrushes.ControlDark;
            Point point = new Point(dropDownRect.Left + (dropDownRect.Width / 2), dropDownRect.Top + (dropDownRect.Height / 2));
            if (this.origRightToLeft == RightToLeft.Yes)
            {
                point.X -= dropDownRect.Width % 2;
            }
            else
            {
                point.X += dropDownRect.Width % 2;
            }
            Point[] points = new Point[] { new Point(point.X - 2, point.Y - 1), new Point(point.X + 3, point.Y - 1), new Point(point.X, point.Y + 2) };
            g.FillPolygon(brush, points);
        }

        protected virtual Color GetInnerBorderColor(ComboBox comboBox)
        {
            if (!comboBox.Enabled)
            {
                return SystemColors.Control;
            }
            return comboBox.BackColor;
        }

        protected virtual Color GetOuterBorderColor(ComboBox comboBox)
        {
            if (!comboBox.Enabled)
            {
                return SystemColors.ControlDark;
            }
            return SystemColors.Window;
        }

        protected virtual Color GetPopupOuterBorderColor(ComboBox comboBox, bool focused)
        {
            if (comboBox.Enabled && !focused)
            {
                return SystemColors.Window;
            }
            return SystemColors.ControlDark;
        }

        public bool IsValid(ComboBox combo)
        {
            if (combo.ClientRectangle == this.clientRect)
            {
                return (combo.RightToLeft == this.origRightToLeft);
            }
            return false;
        }

        public void ValidateOwnerDrawRegions(ComboBox comboBox, Rectangle updateRegionBox)
        {
            NativeMethods.RECT rect;
            if (comboBox != null)
            {
            }
            Rectangle r = new Rectangle(0, 0, comboBox.Width, this.innerBorder.Top);
            Rectangle rectangle2 = new Rectangle(0, this.innerBorder.Bottom, comboBox.Width, comboBox.Height - this.innerBorder.Bottom);
            Rectangle rectangle3 = new Rectangle(0, 0, this.innerBorder.Left, comboBox.Height);
            Rectangle rectangle4 = new Rectangle(this.innerBorder.Right, 0, comboBox.Width - this.innerBorder.Right, comboBox.Height);
            if (r.IntersectsWith(updateRegionBox))
            {
                rect = new NativeMethods.RECT(r);
                SafeNativeMethods.ValidateRect(new HandleRef(comboBox, comboBox.Handle), ref rect);
            }
            if (rectangle2.IntersectsWith(updateRegionBox))
            {
                rect = new NativeMethods.RECT(rectangle2);
                SafeNativeMethods.ValidateRect(new HandleRef(comboBox, comboBox.Handle), ref rect);
            }
            if (rectangle3.IntersectsWith(updateRegionBox))
            {
                rect = new NativeMethods.RECT(rectangle3);
                SafeNativeMethods.ValidateRect(new HandleRef(comboBox, comboBox.Handle), ref rect);
            }
            if (rectangle4.IntersectsWith(updateRegionBox))
            {
                rect = new NativeMethods.RECT(rectangle4);
                SafeNativeMethods.ValidateRect(new HandleRef(comboBox, comboBox.Handle), ref rect);
            }
        }
    }


}
