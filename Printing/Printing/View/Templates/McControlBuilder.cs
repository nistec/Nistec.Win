using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace MControl.Printing.View.Templates
{
    public class McControlBuilder
    {
        public static McTextBox CreateRptText(string dataField, string caption, float x, float y, float width, float height)
        {
            McTextBox ctl = new McTextBox();

            ctl.DataField = dataField;
            ctl.Name = dataField;
            ctl.Text = caption;
            //ctl.TextFont =new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            ctl.Left = x;
            ctl.Top = y;
            ctl.Width = width;
            ctl.Height = height;
            return ctl;
        }

        public static McLabel CreateRptLable(int index, string caption, float x, float y, float width, float height)
        {
            McLabel ctl = new McLabel();

            ctl.Name = "Label" + index.ToString();
            ctl.Text = caption;
            //ctl.TextFont =new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            ctl.Left = x;
            ctl.Top = y;
            ctl.Width = width;
            ctl.Height = height;
            return ctl;
        }
        public static McLabel CreateRptLable(string name, string text, float x, float y, SizeF size, Font font, Color foreColor, ContentAlignment align, bool RightToLeft)
        {
            McLabel ctl = new McLabel();

            ctl.Name = name;// "Label" + index.ToString();
            ctl.Text = text;
            ctl.TextFont = font;// new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            ctl.TextAlign = align;
            ctl.ForeColor = foreColor;

            ctl.Left = x;
            ctl.Top = y;
            ctl.Width = size.Width;
            ctl.Height = size.Height;
            return ctl;
        }

    }
}
