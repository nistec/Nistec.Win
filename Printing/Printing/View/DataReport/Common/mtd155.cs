namespace Nistec.Printing.View
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    internal class mtd155 : mtd126
    {
        internal mtd247 mtd147;
        internal string[] mtd225;
        internal mtd233 mtd242;
        internal bool mtd259;
        internal static RichTextBox mtd260 = new RichTextBox();
        internal string mtd52;

        internal mtd155()
        {
        }

        internal mtd155(ref McReportControl c) : base(ref c)
        {
            McRichText text = (McRichText) c;
            this.mtd147 = new mtd247(text.ForeColor, text.BackColor);
            this.mtd242 = new mtd233(ref text);
            this.mtd225 = null;
        }

        internal mtd155(ref McReportControl c, ref FieldsCollection var0) : base(ref c)
        {
            McRichText text = (McRichText) c;
            this.mtd147 = new mtd247(text.ForeColor, text.BackColor);
            this.mtd242 = new mtd233(ref text, ref var0);
            this.mtd225 = null;
        }

        internal static mtd155 mtd105(ref mtd155 var1, mtd248 pc)
        {
            McRichText text = var1.mtd242.mtd64;
            McField[] fieldArray = var1.mtd242.Fields;
            mtd155 mtd = new mtd155();
            mtd.mtd242 = var1.mtd242;
            mtd.RptControl = var1.RptControl;
            mtd.mtd130 = text.Visible;
            mtd._Border = text.Border;
            mtd.mtd52 = text.RTF;
            mtd.mtd259 = text.RightToLeft;
            if (fieldArray != null)
            {
                mtd.mtd225 = new string[fieldArray.Length];
                int index = 0;
                foreach (McField field in fieldArray)
                {
                    object obj2 = field.Value;
                    if ((obj2 != null) && !(obj2 is DBNull))
                    {
                        mtd.mtd225[index] = field.Value.ToString();
                    }
                    else
                    {
                        mtd.mtd225[index] = string.Empty;
                    }
                    index++;
                }
            }
            if (pc.mtd57 != mtd249.mtd27)
            {
                mtd._Location = new McLocation(text.Left, text.Top, text.Width, text.Height, true);
            }
            else
            {
                mtd._Location = var1._Location;
            }
            if (pc.mtd59 != mtd250.mtd27)
            {
                mtd.mtd147 = new mtd247(text.ForeColor, text.BackColor);
                return mtd;
            }
            mtd.mtd147 = var1.mtd147;
            return mtd;
        }

        internal static void mtd22(Graphics g, mtd155 var1, RectangleF var2)
        {
            McRichText text1 = var1.mtd242.mtd64;
            McField[] fieldArray = var1.mtd242.Fields;
            mtd260.Rtf = var1.mtd52;
            mtd260.ForeColor = var1.mtd147.ForeColor;
            mtd260.BackColor = var1.mtd147.BackColor;
            if (fieldArray != null)
            {
                int index = 0;
                foreach (McField field in fieldArray)
                {
                    string str = string.Format("[!{0}]", field.Name);
                    if (mtd260.Find(str) != -1)
                    {
                        string str2 = var1.mtd225[index];
                        mtd260.SelectionLength = str.Length;
                        if (str2 != string.Empty)
                        {
                            mtd260.SelectedText = str2;
                        }
                        else
                        {
                            mtd260.SelectedText = "[Null]";
                        }
                        index++;
                    }
                }
            }
            mtd10.mtd18(g, mtd260.Handle, mtd260.BackColor, var1._Border, var2, false, var1.mtd259);
        }

        internal static mtd155 mtd253(ref mtd155 var1)
        {
            mtd155 mtd = new mtd155();
            mtd.RptControl = var1.RptControl;
            mtd._Location = var1._Location;
            mtd.mtd147 = var1.mtd147;
            mtd.mtd130 = var1.mtd130;
            mtd._Border = var1._Border;
            mtd.mtd254 = true;
            mtd.mtd242 = var1.mtd242;
            mtd.mtd225 = var1.mtd225;
            mtd.mtd259 = var1.mtd259;
            return mtd;
        }
    }
}

