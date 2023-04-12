using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Nistec.Printing;
using Nistec.Data;
using Nistec.Win;
using Nistec.Printing.Sections;

namespace Nistec.Charts
{
    public static class PieChartUtil
    {
        public static void CreateChart(McPieChart ctl, System.Data.DataTable dt, string groupByColumn, string sumColumn)
        {
            CreateChart(ctl, new Padding(60, 0, 0, 0), dt, groupByColumn, sumColumn);
        }

        public static void CreateChart(McPieChart ctl,Padding padding, System.Data.DataTable dt, string groupByColumn, string sumColumn)
        {
            if (dt == null || dt.Rows.Count == 0)
                return ;

            if (dt.Rows.Count > 500)
            {
                MsgBox.ShowWarning("Too meny items.");
                return ;
            }
            if (ctl == null)
            {
                ctl = new McPieChart();
            }
            //McPieChart ctl = new McPieChart();
            ctl.Items.Clear();
            int index = 0;
            int colorCounter = 0;
            Color[] ChartColors = new Color[] { Color.Blue, Color.Gold, Color.Green, Color.Red, Color.Olive, Color.Purple, Color.Fuchsia, Color.Silver, Color.Orange, Color.MediumSeaGreen, Color.Brown, Color.Turquoise, Color.Tomato, Color.CornflowerBlue, Color.White };
            decimal val = 0;
            string key = "";
            string[] groupFields = groupByColumn.Split(',');
            string[] sumFields = sumColumn.Split(',');

            foreach (DataRow dr in dt.Rows)
            {
                key = "";
                val = 0;
                foreach (string s in groupFields)
                {
                    if(s!="")
                    key += string.Format("{0}:{1}, ", s, dr[s]);
                }
                key.TrimEnd(',');

                foreach (string s in sumFields)
                {
                    val += (decimal)Types.ToDecimal(dr[s], 0);
                }

                if (val >= 0)
                {
                    val = Math.Round(val, 2);
                    ctl.Items.Add(new PieChartItem((double)val, ChartColors[colorCounter], key + "[" + val.ToString() + "]", key + " [" + val.ToString() + "]", 0));
                    index++;
                    colorCounter++;
                    if (colorCounter >= ChartColors.Length) colorCounter = 0;
                }
            }
            ctl.Padding = padding;// new System.Windows.Forms.Padding(60, 0, 0, 0);
            ctl.ItemStyle.SurfaceTransparency = 0.75F;
            ctl.FocusedItemStyle.SurfaceTransparency = 0.75F;
            ctl.FocusedItemStyle.SurfaceBrightness = 0.3F;
            ctl.AddChartDescription();
            //ctl.Invalidate();

            //return ctl;
        }

        public static void CreateChart(McPieChart ctl, IField[] fields)
        {
            CreateChart(ctl, new Padding(60,0,0,0), fields);
        }

        public static void CreateChart(McPieChart ctl, Padding padding, IField[] fields)
        {
            if (fields == null || fields.Length == 0)
            {
                return;
            }

            if (fields.Length > 500)
            {
                MsgBox.ShowWarning("Too meny items.");
                return ;
            }
            if (ctl == null)
            {
                ctl = new McPieChart();
            }
            //McPieChart ctl = new McPieChart();
            ctl.Items.Clear();
            int index = 0;
            int colorCounter = 0;
            Color[] ChartColors = new Color[] { Color.Blue, Color.Gold, Color.Green, Color.Red, Color.Olive, Color.Purple, Color.Fuchsia, Color.Silver, Color.Orange, Color.MediumSeaGreen, Color.Brown, Color.Turquoise, Color.Tomato, Color.CornflowerBlue, Color.White };
            foreach (IField f in fields)
            {
                decimal val = (decimal)Types.ToDecimal(f.Value, 0);
                if (val >= 0)
                {
                    val = Math.Round(val, 2);
                    ctl.Items.Add(new PieChartItem((double)val, ChartColors[colorCounter], f.Key.ToString() + " [" + f.Text + "]", f.Key.ToString() + " [" + f.Text + "]", 0));
                    index++;
                    colorCounter++;
                    if (colorCounter >= ChartColors.Length) colorCounter = 0;
                }
            }

            ctl.Padding = padding;// new System.Windows.Forms.Padding(60, 0, 0, 0);
            ctl.ItemStyle.SurfaceTransparency = 0.75F;
            ctl.FocusedItemStyle.SurfaceTransparency = 0.75F;
            ctl.FocusedItemStyle.SurfaceBrightness = 0.3F;
            ctl.AddChartDescription();

            //return ctl;

        }


        public static void SaveAs(McPieChart ctl)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "PNG Image|*.png|JPEG Image|*.jpg;*.jpeg|Bitmap Image|*.bmp|GIF Image|*.gif";

            Size imageSize;
            if (ctl.AutoSizePie)
                imageSize = new Size(ctl.Bounds.Width, ctl.Bounds.Height);
            else
                imageSize = ctl.GetChartSize(ctl.Padding);

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                switch (dlg.FilterIndex)
                {
                    case 1:
                        ctl.SaveAs(dlg.FileName, System.Drawing.Imaging.ImageFormat.Png, imageSize, ctl.Padding);
                        break;
                    case 2:
                        ctl.SaveAs(dlg.FileName, System.Drawing.Imaging.ImageFormat.Jpeg, imageSize, ctl.Padding);
                        break;
                    case 3:
                        ctl.SaveAs(dlg.FileName, System.Drawing.Imaging.ImageFormat.Bmp, imageSize, ctl.Padding);
                        break;
                    case 4:
                        ctl.SaveAs(dlg.FileName, System.Drawing.Imaging.ImageFormat.Gif, imageSize, ctl.Padding);
                        break;
                    default:
                        throw new Exception("Unknown file filter.");
                }
            }
        }
        public static void Print(McPieChart ctl)
        {
            PrintDialog dlg = new PrintDialog();
            dlg.Document = new System.Drawing.Printing.PrintDocument();
            ctl.AttachPrintDocument(dlg.Document);

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                dlg.Document.Print();
            }
        }

        public static McPrintDocument GetPrintDocument(McPieChart ctl)
        {
            Size imageSize;
            if (ctl.AutoSizePie)
                imageSize = new Size(ctl.Bounds.Width, ctl.Bounds.Height);
            else
                imageSize = ctl.GetChartSize(ctl.Padding);

            McPrintDocument printDocument = new McPrintDocument();

            ReportBuilder builder = new ReportBuilder(printDocument);
            builder.StartContainer(new LinearSections());
            SectionImage sec = new SectionImage(ctl.GetImage(System.Drawing.Imaging.ImageFormat.Gif, imageSize, ctl.Padding));
            builder.AddSection(sec);
            builder.CurrentDocument.DefaultPageSettings.Landscape = true;
            builder.CurrentSection.HorizontalAlignment = HorizontalAlignment.Center;

            return printDocument;
            //Nistec.WinCtl.Controls.McPrintPreviewDialog.Preview(printDocument);

        }

    }
}
