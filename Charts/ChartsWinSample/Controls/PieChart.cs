using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MControl.Charts;

namespace ChartsSample.Controls
{
    public partial class PieChart : UserControl
    {
        public PieChart()
        {
            InitializeComponent();

            ctlPieChart1.Items.Add(new PieChartItem(10, Color.Blue, "Blue", "Blue tool tip", 0));
            ctlPieChart1.Items.Add(new PieChartItem(10, Color.Gold, "Gold", "Gold tool tip", 0));
            ctlPieChart1.Items.Add(new PieChartItem(10, Color.Green, "Green", "Green tool tip", 0));
            ctlPieChart1.Items.Add(new PieChartItem(20, Color.Red, "Red", "Red tool tip", 0));


            ctlPieChart1.ItemStyle.SurfaceTransparency = 0.75F;
            ctlPieChart1.FocusedItemStyle.SurfaceTransparency = 0.75F;
            ctlPieChart1.FocusedItemStyle.SurfaceBrightness = 0.3F;

            ctlPieChart1.Margin = new System.Windows.Forms.Padding(60, 0, 0, 0);
     
            //cmbTextMode.Items.Add(ctlPieChart1.TextDisplayTypes.Always);
            //cmbTextMode.Items.Add(ctlPieChart1.TextDisplayTypes.FitOnly);
            //cmbTextMode.Items.Add(ctlPieChart1.TextDisplayTypes.Never);
            //cmbTextMode.SelectedIndex = 0;

            trkRotation.Value = (int)(ctlPieChart1.Rotation * 180 / Math.PI);
            trkIncline.Value = (int)(ctlPieChart1.Leaning * 180 / Math.PI);
            trkThickness.Value = (int)(ctlPieChart1.Depth);
            trkRadius.Value = (int)(ctlPieChart1.Radius);
            //ctlPieChart1.ChartMargin = new Padding(20,0,0,0);
            //ctlPieChart1.AddPanels(new string[] { "Blue", "Gold", "Green", "Red" });
            //trkEdgeBrightness.Value = (int)(ctlPieChart1.ItemStyle.EdgeBrightnessFactor * 100);
            //trkFocusedEdgeBrightness.Value = (int)(ctlPieChart1.FocusedItemStyle.EdgeBrightnessFactor * 100);
            //trkSurfaceBrightness.Value = (int)(ctlPieChart1.ItemStyle.SurfaceBrightnessFactor * 100);
            //trkFocusedSurfaceBrightness.Value = (int)(ctlPieChart1.FocusedItemStyle.SurfaceBrightnessFactor * 100);
            trkSurfaceTransparency.Value = (int)(ctlPieChart1.ItemStyle.SurfaceTransparency * 100);
            //trkFocusedSurfaceTransparency.Value = (int)(ctlPieChart1.FocusedItemStyle.SurfaceTransparency * 100);

            //chkAutoSizeRadius.Checked = PieChart1.AutoSizePie;
            //chkShowEdges.Checked = PieChart1.ShowEdges;
            //chkShowToolTips.Checked = PieChart1.ShowToolTips;

            //cmbTextMode.SelectedItem = PieChart1.TextDisplayMode;

            //propertyGrid1.SelectedObject = new ItemCollectionProxy(PieChart1);
        }

        private void trkRotation_Scroll(object sender, EventArgs e)
        {
            ctlPieChart1.Rotation = (float)(trkRotation.Value * Math.PI / 180);
        }

        private void trkIncline_Scroll(object sender, EventArgs e)
        {
            ctlPieChart1.Leaning = (float)(trkIncline.Value * Math.PI / 180);
            this.Text = trkIncline.Value.ToString();
        }

        private void trkThickness_Scroll(object sender, EventArgs e)
        {
            ctlPieChart1.Depth = trkThickness.Value;
            this.Text = trkThickness.Value.ToString();
        }

        private void trkRadius_Scroll(object sender, EventArgs e)
        {
            ctlPieChart1.Radius = trkRadius.Value;
        }

        private void trkSurfaceTransparency_Scroll(object sender, EventArgs e)
        {
            ctlPieChart1.ItemStyle.SurfaceTransparency = (float)trkSurfaceTransparency.Value / 100;
        }

        private void trkFocusedSurfaceTransparency_Scroll(object sender, EventArgs e)
        {
            //ctlPieChart1.FocusedItemStyle.SurfaceTransparency = (float)trkFocusedSurfaceTransparency.Value / 100;
        }

        private void trkEdgeBrightness_Scroll(object sender, EventArgs e)
        {
            //ctlPieChart1.ItemStyle.EdgeBrightnessFactor = (float)trkEdgeBrightness.Value / 100;
        }

        private void ctlButton1_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "PNG Image|*.png|JPEG Image|*.jpg;*.jpeg|Bitmap Image|*.bmp|GIF Image|*.gif";

            Size imageSize;
            if (ctlPieChart1.AutoSizePie)
                imageSize = new Size(ctlPieChart1.Bounds.Width, ctlPieChart1.Bounds.Height);
            else
                imageSize = ctlPieChart1.GetChartSize(ctlPieChart1.Padding);

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                switch (dlg.FilterIndex)
                {
                    case 1:
                        ctlPieChart1.SaveAs(dlg.FileName, System.Drawing.Imaging.ImageFormat.Png, imageSize, ctlPieChart1.Padding);
                        break;
                    case 2:
                        ctlPieChart1.SaveAs(dlg.FileName, System.Drawing.Imaging.ImageFormat.Jpeg, imageSize, ctlPieChart1.Padding);
                        break;
                    case 3:
                        ctlPieChart1.SaveAs(dlg.FileName, System.Drawing.Imaging.ImageFormat.Bmp, imageSize, ctlPieChart1.Padding);
                        break;
                    case 4:
                        ctlPieChart1.SaveAs(dlg.FileName, System.Drawing.Imaging.ImageFormat.Gif, imageSize, ctlPieChart1.Padding);
                        break;
                    default:
                        throw new Exception("Unknown file filter.");
                }
            }
        }
        private void ctlButton2_Click(object sender, EventArgs e)
        {
            PrintDialog dlg = new PrintDialog();
            dlg.Document = new System.Drawing.Printing.PrintDocument();
            ctlPieChart1.AttachPrintDocument(dlg.Document);

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                dlg.Document.Print();
            }
        }

        private void ctlButton3_Click(object sender, EventArgs e)
        {
            Size imageSize;
            if (ctlPieChart1.AutoSizePie)
                imageSize = new Size(ctlPieChart1.Bounds.Width, ctlPieChart1.Bounds.Height);
            else
                imageSize = ctlPieChart1.GetChartSize(ctlPieChart1.Padding);

            MControl.Printing.McPrintDocument printDocument = new MControl.Printing.McPrintDocument();

            MControl.Printing.ReportBuilder builder = new MControl.Printing.ReportBuilder(printDocument);
            builder.StartContainer(new MControl.Printing.Sections.LinearSections());
            MControl.Printing.Sections.SectionImage sec = new MControl.Printing.Sections.SectionImage(ctlPieChart1.GetImage(System.Drawing.Imaging.ImageFormat.Gif, imageSize, ctlPieChart1.Padding));
            builder.AddSection(sec);
            builder.CurrentDocument.DefaultPageSettings.Landscape = true;
            builder.CurrentSection.HorizontalAlignment = HorizontalAlignment.Center;

            MControl.Printing.McPrintPreviewDialog.Preview(printDocument);

        }
    }
}
