namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.Drawing;
    using MControl.Printing.Pdf.Core.Element;
    using MControl.Printing.Pdf.Controls;
    using MControl.Printing.Pdf.Core.IO;
    using System;
    using System.Drawing;
    using System.IO;

    public class PdfGraphics
    {
        private GraphicsElements _b0;
        private Page _b1;
        private MControl.Printing.Pdf.ColorSpace _b2;

        internal PdfGraphics(Page b1)
        {
            this._b1 = b1;
            this._b0 = new GraphicsElements();
            this._b2 = MControl.Printing.Pdf.ColorSpace.RGB;
        }

        internal void A547(ref A120 b3, ref A112 b4)
        {
            for (int i = 0; i < this._b0.Count; i++)
            {
                this._b0[i].A119(ref b3, ref b4);
            }
            while (b3.A522.Count > 0)
            {
                b4.A176("Q ");
                b3.A438();
            }
        }

        public PdfButton AddPdfButton(string name, string caption, RectangleF bound, PdfFont font, float fontsize)
        {
            PdfButton element = new PdfButton(this.A141, name, caption, bound, font, fontsize);
            this._b0.Add(element);
            return element;
        }

        public PdfCheckBox AddPdfCheckBox(string name, RectangleF bound)
        {
            PdfCheckBox element = new PdfCheckBox(this.A141, name, bound);
            this._b0.Add(element);
            return element;
        }

        public PdfCheckBox AddPdfCheckBox(string name, float left, float top)
        {
            return this.AddPdfCheckBox(name, new RectangleF(left, top, 12f, 12f));
        }

        public PdfComboBox AddPdfComboBox(string name, string[] items, RectangleF bound, PdfFont font, float fontsize)
        {
            PdfComboBox element = new PdfComboBox(this.A141, name, items, bound, font, fontsize);
            this._b0.Add(element);
            return element;
        }

        public PdfListBox AddPdfListBox(string name, string[] items, RectangleF bound, PdfFont font, float fontsize)
        {
            PdfListBox element = new PdfListBox(this.A141, name, items, bound, font, fontsize);
            this._b0.Add(element);
            return element;
        }

        public PdfRadioGroup AddPdfRadioGroup(string name)
        {
            PdfRadioGroup element = new PdfRadioGroup(this.A141, name);
            this._b0.Add(element);
            return element;
        }

        public PdfTextField AddPdfTextBox(string name, string value, RectangleF bound, PdfFont font, float fontsize)
        {
            PdfTextField element = new PdfTextField(this.A141, name, value, bound, font, fontsize);
            this._b0.Add(element);
            return element;
        }

        public void ClipBezier(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4)
        {
            this._b0.Add(new Bezier(x1, y1, x2, y2, x3, y3, x4, y4, 1f, LineStyle.Solid, null, null, GraphicsMode.clip, null));
        }

        public void ClipBeziers(PointF[] points)
        {
            this._b0.Add(new Beziers(points, 1f, LineStyle.Solid, null, null, GraphicsMode.clip, null));
        }

        public void ClipChord(float x, float y, float width, float height, float startangle, float endangle)
        {
            this._b0.Add(new Chord(x, y, width, height, startangle, endangle, 1f, LineStyle.Solid, null, null, GraphicsMode.clip, null));
        }

        public void ClipCircle(float x, float y, float radius, float linewidth)
        {
            this._b0.Add(new Circle(x, y, radius, radius, 1f, LineStyle.Solid, null, null, GraphicsMode.stroke, null));
        }

        public void ClipEllipse(float x, float y, float width, float height)
        {
            this._b0.Add(new Ellipse(x, y, width, height, 1f, LineStyle.Solid, null, null, GraphicsMode.clip, null));
        }

        public void ClipPolygon(PointF[] points)
        {
            this._b0.Add(new Lines(points, 1f, LineStyle.Solid, null, null, GraphicsMode.clip, true, null));
        }

        public void ClipRectangle(float x, float y, float width, float height)
        {
            this._b0.Add(new MControl.Printing.Pdf.Drawing.Rectangle(x, y, width, height, 1f, LineStyle.Solid, null, null, GraphicsMode.clip, null));
        }

        public void DrawBarcode(BarcodeGraphics barcode)
        {
            this._b0.Add(barcode);
        }

        public void DrawBarcode(BarcodeGraphics barcode, float x, float y)
        {
            barcode.X = x;
            barcode.Y = y;
            this._b0.Add(barcode);
        }

        public void DrawBezier(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4, PdfColor linecolor)
        {
            this._b0.Add(new Bezier(x1, y1, x2, y2, x3, y3, x4, y4, 1f, LineStyle.Solid, linecolor, PdfColor.Transparent, GraphicsMode.stroke, null));
        }

        public void DrawBezier(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4, LineStyle linestyle, PdfColor linecolor)
        {
            this._b0.Add(new Bezier(x1, y1, x2, y2, x3, y3, x4, y4, 1f, linestyle, linecolor, PdfColor.Transparent, GraphicsMode.stroke, null));
        }

        public void DrawBezier(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4, PdfColor linecolor, PdfColor fillcolor, GraphicsMode mode)
        {
            this._b0.Add(new Bezier(x1, y1, x2, y2, x3, y3, x4, y4, 1f, LineStyle.Solid, linecolor, fillcolor, mode, null));
        }

        public void DrawBezier(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4, LineStyle linestyle, PdfColor linecolor, PdfColor fillcolor, GraphicsMode mode)
        {
            this._b0.Add(new Bezier(x1, y1, x2, y2, x3, y3, x4, y4, 1f, linestyle, linecolor, fillcolor, mode, null));
        }

        public void DrawBezier(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4, float linewidth, LineStyle linestyle, PdfColor linecolor, PdfColor fillcolor, GraphicsMode mode)
        {
            this._b0.Add(new Bezier(x1, y1, x2, y2, x3, y3, x4, y4, linewidth, linestyle, linecolor, fillcolor, mode, null));
        }

        public void DrawBezier(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4, float linewidth, LineStyle linestyle, PdfColor linecolor, PdfColor fillcolor, GraphicsMode mode, string name)
        {
            this._b0.Add(new Bezier(x1, y1, x2, y2, x3, y3, x4, y4, linewidth, linestyle, linecolor, fillcolor, mode, name));
        }

        public void DrawBeziers(PointF[] points, PdfColor linecolor)
        {
            this._b0.Add(new Beziers(points, 1f, LineStyle.Solid, linecolor, PdfColor.Transparent, GraphicsMode.stroke, null));
        }

        public void DrawBeziers(PointF[] points, LineStyle linestyle, PdfColor linecolor)
        {
            this._b0.Add(new Beziers(points, 1f, linestyle, linecolor, PdfColor.Transparent, GraphicsMode.stroke, null));
        }

        public void DrawBeziers(PointF[] points, PdfColor linecolor, PdfColor fillcolor, GraphicsMode mode)
        {
            this._b0.Add(new Beziers(points, 1f, LineStyle.Solid, linecolor, fillcolor, mode, null));
        }

        public void DrawBeziers(PointF[] points, LineStyle linestyle, PdfColor linecolor, PdfColor fillcolor, GraphicsMode mode)
        {
            this._b0.Add(new Beziers(points, 1f, linestyle, linecolor, fillcolor, mode, null));
        }

        public void DrawBeziers(PointF[] points, float linewidth, LineStyle linestyle, PdfColor linecolor, PdfColor fillcolor, GraphicsMode mode)
        {
            this._b0.Add(new Beziers(points, linewidth, linestyle, linecolor, fillcolor, mode, null));
        }

        public void DrawBeziers(PointF[] points, float linewidth, LineStyle linestyle, PdfColor linecolor, PdfColor fillcolor, GraphicsMode mode, string name)
        {
            this._b0.Add(new Beziers(points, linewidth, linestyle, linecolor, fillcolor, mode, name));
        }

        public void DrawChord(float x, float y, float width, float height, float startangle, float endangle, PdfColor linecolor)
        {
            this._b0.Add(new Chord(x, y, width, height, startangle, endangle, 1f, LineStyle.Solid, linecolor, PdfColor.Transparent, GraphicsMode.stroke, null));
        }

        public void DrawChord(float x, float y, float width, float height, float startangle, float endangle, LineStyle linestyle, PdfColor linecolor)
        {
            this._b0.Add(new Chord(x, y, width, height, startangle, endangle, 1f, linestyle, linecolor, PdfColor.Transparent, GraphicsMode.stroke, null));
        }

        public void DrawChord(float x, float y, float width, float height, float startangle, float endangle, PdfColor linecolor, PdfColor fillcolor, GraphicsMode mode)
        {
            this._b0.Add(new Chord(x, y, width, height, startangle, endangle, 1f, LineStyle.Solid, linecolor, fillcolor, mode, null));
        }

        public void DrawChord(float x, float y, float width, float height, float startangle, float endangle, LineStyle linestyle, PdfColor linecolor, PdfColor fillcolor, GraphicsMode mode)
        {
            this._b0.Add(new Chord(x, y, width, height, startangle, endangle, 1f, linestyle, linecolor, fillcolor, mode, null));
        }

        public void DrawChord(float x, float y, float width, float height, float startangle, float endangle, float linewidth, LineStyle linestyle, PdfColor linecolor, PdfColor fillcolor, GraphicsMode mode)
        {
            this._b0.Add(new Chord(x, y, width, height, startangle, endangle, linewidth, linestyle, linecolor, fillcolor, mode, null));
        }

        public void DrawChord(float x, float y, float width, float height, float startangle, float endangle, float linewidth, LineStyle linestyle, PdfColor linecolor, PdfColor fillcolor, GraphicsMode mode, string name)
        {
            this._b0.Add(new Chord(x, y, width, height, startangle, endangle, linewidth, linestyle, linecolor, fillcolor, mode, name));
        }

        public void DrawCircle(float x, float y, float radius, LineStyle linestyle, PdfColor linecolor)
        {
            this._b0.Add(new Circle(x, y, radius, radius, 1f, linestyle, linecolor, PdfColor.Transparent, GraphicsMode.stroke, null));
        }

        public void DrawCircle(float x, float y, float radius, float linewidth, PdfColor linecolor)
        {
            this._b0.Add(new Circle(x, y, radius, radius, 1f, LineStyle.Solid, linecolor, PdfColor.Transparent, GraphicsMode.stroke, null));
        }

        public void DrawCircle(float x, float y, float radius, PdfColor linecolor, PdfColor fillcolor, GraphicsMode mode)
        {
            this._b0.Add(new Circle(x, y, radius, radius, 1f, LineStyle.Solid, linecolor, fillcolor, mode, null));
        }

        public void DrawCircle(float x, float y, float radius, LineStyle linestyle, PdfColor linecolor, PdfColor fillcolor, GraphicsMode mode)
        {
            this._b0.Add(new Circle(x, y, radius, radius, 1f, linestyle, linecolor, fillcolor, mode, null));
        }

        public void DrawCircle(float x, float y, float radius, float linewidth, LineStyle linestyle, PdfColor linecolor, PdfColor fillcolor, GraphicsMode mode)
        {
            this._b0.Add(new Circle(x, y, radius, radius, linewidth, linestyle, linecolor, fillcolor, mode, null));
        }

        public void DrawCircle(float x, float y, float radius, float linewidth, LineStyle linestyle, PdfColor linecolor, PdfColor fillcolor, GraphicsMode mode, string name)
        {
            this._b0.Add(new Circle(x, y, radius, radius, linewidth, linestyle, linecolor, fillcolor, mode, name));
        }

        public void DrawCircle(float x, float y, float radiusX, float radiusY, float linewidth, LineStyle linestyle, PdfColor linecolor, PdfColor fillcolor, GraphicsMode mode, string name)
        {
            this._b0.Add(new Circle(x, y, radiusX, radiusY, linewidth, linestyle, linecolor, fillcolor, mode, name));
        }

        public void DrawEllipse(float x, float y, float width, float height, PdfColor linecolor)
        {
            this._b0.Add(new Ellipse(x, y, width, height, 1f, LineStyle.Solid, linecolor, PdfColor.Transparent, GraphicsMode.stroke, null));
        }

        public void DrawEllipse(float x, float y, float width, float height, LineStyle linestyle, PdfColor linecolor)
        {
            this._b0.Add(new Ellipse(x, y, width, height, 1f, linestyle, linecolor, PdfColor.Transparent, GraphicsMode.stroke, null));
        }

        public void DrawEllipse(float x, float y, float width, float height, PdfColor linecolor, PdfColor fillcolor, GraphicsMode mode)
        {
            this._b0.Add(new Ellipse(x, y, width, height, 1f, LineStyle.Solid, linecolor, fillcolor, mode, null));
        }

        public void DrawEllipse(float x, float y, float width, float height, LineStyle linestyle, PdfColor linecolor, PdfColor fillcolor, GraphicsMode mode)
        {
            this._b0.Add(new Ellipse(x, y, width, height, 1f, linestyle, linecolor, fillcolor, mode, null));
        }

        public void DrawEllipse(float x, float y, float width, float height, float linewidth, LineStyle linestyle, PdfColor linecolor, PdfColor fillcolor, GraphicsMode mode)
        {
            this._b0.Add(new Ellipse(x, y, width, height, linewidth, linestyle, linecolor, fillcolor, mode, null));
        }

        public void DrawEllipse(float x, float y, float width, float height, float linewidth, LineStyle linestyle, PdfColor linecolor, PdfColor fillcolor, GraphicsMode mode, string name)
        {
            this._b0.Add(new Ellipse(x, y, width, height, linewidth, linestyle, linecolor, fillcolor, mode, name));
        }

        public void DrawImage(Image image, float x, float y)
        {
            this._b0.Add(new ImageBoxEx(image, -1, this._b2, null, x, y, this.A141.Width, this.A141.Height, PictureAlignment.TopLeft, SizeMode.Clip, null, null));
        }

        public void DrawImage(Image image, float x, float y, float width, float height)
        {
            this._b0.Add(new ImageBoxEx(image, -1, this._b2, null, x, y, width, height, PictureAlignment.TopLeft, SizeMode.Clip, null, null));
        }

        public void DrawImage(Image image, float x, float y, float width, float height, PictureAlignment align, SizeMode sizemode)
        {
            this._b0.Add(new ImageBoxEx(image, -1, this._b2, null, x, y, width, height, align, sizemode, null, null));
        }

        public void DrawImage(Image image, float x, float y, float width, float height, SizeMode sizemode, Border border)
        {
            this._b0.Add(new ImageBoxEx(image, -1, this._b2, null, x, y, width, height, PictureAlignment.TopLeft, sizemode, border, null));
        }

        public void DrawImage(Image image, float x, float y, float width, float height, PictureAlignment align, SizeMode sizemode, Border border)
        {
            this._b0.Add(new ImageBoxEx(image, -1, this._b2, null, x, y, width, height, align, sizemode, border, null));
        }

        public void DrawImage(Image image, float x, float y, float width, float height, PictureAlignment align, SizeMode sizemode, Border border, string name)
        {
            this._b0.Add(new ImageBoxEx(image, -1, this._b2, null, x, y, width, height, align, sizemode, border, name));
        }

        public void DrawImageFrame(Image image, int frameindex, float x, float y)
        {
            this._b0.Add(new ImageBoxEx(image, frameindex, this._b2, null, x, y, this.A141.Width, this.A141.Height, PictureAlignment.TopLeft, SizeMode.Clip, null, null));
        }

        public void DrawImageFrame(Image image, int frameindex, float x, float y, float width, float height)
        {
            this._b0.Add(new ImageBoxEx(image, frameindex, this._b2, null, x, y, width, height, PictureAlignment.TopLeft, SizeMode.Clip, null, null));
        }

        public void DrawImageFrame(Image image, int frameindex, float x, float y, float width, float height, PictureAlignment align, SizeMode sizemode)
        {
            this._b0.Add(new ImageBoxEx(image, frameindex, this._b2, null, x, y, width, height, align, sizemode, null, null));
        }

        public void DrawImageFrame(Image image, int frameindex, float x, float y, float width, float height, SizeMode sizemode, Border border)
        {
            this._b0.Add(new ImageBoxEx(image, frameindex, this._b2, null, x, y, width, height, PictureAlignment.TopLeft, sizemode, border, null));
        }

        public void DrawImageFrame(Image image, int frameindex, float x, float y, float width, float height, PictureAlignment align, SizeMode sizemode, Border border)
        {
            this._b0.Add(new ImageBoxEx(image, frameindex, this._b2, null, x, y, width, height, align, sizemode, border, null));
        }

        public void DrawImageFrame(Image image, int frameindex, float x, float y, float width, float height, PictureAlignment align, SizeMode sizemode, Border border, string name)
        {
            this._b0.Add(new ImageBoxEx(image, frameindex, this._b2, null, x, y, width, height, align, sizemode, border, name));
        }

        public void DrawImageWithSoftMask(Image image, Image imageMask, float x, float y)
        {
            this._b0.Add(new ImageBoxEx(image, -1, this._b2, imageMask, x, y, this.A141.Width, this.A141.Height, PictureAlignment.TopLeft, SizeMode.Clip, null, null));
        }

        public void DrawImageWithSoftMask(Image image, Image imageMask, float x, float y, float width, float height)
        {
            this._b0.Add(new ImageBoxEx(image, -1, this._b2, imageMask, x, y, width, height, PictureAlignment.TopLeft, SizeMode.Clip, null, null));
        }

        public void DrawImageWithSoftMask(Image image, Image imageMask, float x, float y, float width, float height, PictureAlignment align, SizeMode sizemode)
        {
            this._b0.Add(new ImageBoxEx(image, -1, this._b2, imageMask, x, y, width, height, align, sizemode, null, null));
        }

        public void DrawImageWithSoftMask(Image image, Image imageMask, float x, float y, float width, float height, SizeMode sizemode, Border border)
        {
            this._b0.Add(new ImageBoxEx(image, -1, this._b2, imageMask, x, y, width, height, PictureAlignment.TopLeft, sizemode, border, null));
        }

        public void DrawImageWithSoftMask(Image image, Image imageMask, float x, float y, float width, float height, PictureAlignment align, SizeMode sizemode, Border border)
        {
            this._b0.Add(new ImageBoxEx(image, -1, this._b2, imageMask, x, y, width, height, align, sizemode, border, null));
        }

        public void DrawImageWithSoftMask(Image image, Image imageMask, float x, float y, float width, float height, PictureAlignment align, SizeMode sizemode, Border border, string name)
        {
            this._b0.Add(new ImageBoxEx(image, -1, this._b2, imageMask, x, y, width, height, align, sizemode, border, name));
        }

        public void DrawLine(float x1, float y1, float x2, float y2, PdfColor linecolor)
        {
            this._b0.Add(new Line(x1, y1, x2, y2, 1f, LineStyle.Solid, linecolor, null));
        }

        public void DrawLine(float x1, float y1, float x2, float y2, LineStyle linestyle, PdfColor linecolor)
        {
            this._b0.Add(new Line(x1, y1, x2, y2, 1f, linestyle, linecolor, null));
        }

        public void DrawLine(float x1, float y1, float x2, float y2, float linewidth, LineStyle linestyle, PdfColor linecolor)
        {
            this._b0.Add(new Line(x1, y1, x2, y2, linewidth, linestyle, linecolor, null));
        }

        public void DrawLine(float x1, float y1, float x2, float y2, float linewidth, LineStyle linestyle, PdfColor linecolor, string name)
        {
            this._b0.Add(new Line(x1, y1, x2, y2, linewidth, linestyle, linecolor, name));
        }

        public void DrawLines(PointF[] points, float linewidth, LineStyle linestyle, PdfColor linecolor, bool closepath)
        {
            this._b0.Add(new Lines(points, linewidth, linestyle, linecolor, PdfColor.Transparent, GraphicsMode.stroke, closepath, null));
        }

        public void DrawLines(PointF[] points, float linewidth, LineStyle linestyle, PdfColor linecolor, PdfColor fillcolor, GraphicsMode mode, bool closepath)
        {
            this._b0.Add(new Lines(points, linewidth, linestyle, linecolor, fillcolor, mode, closepath, null));
        }

        public void DrawLines(PointF[] points, float linewidth, LineStyle linestyle, PdfColor linecolor, PdfColor fillcolor, GraphicsMode mode, bool closepath, string name)
        {
            this._b0.Add(new Lines(points, linewidth, linestyle, linecolor, fillcolor, mode, closepath, name));
        }

        public void DrawLinkToFile(string filepath, float x, float y, float width, float height)
        {
            this._b0.Add(new LinkToFile(x, y, width, height, filepath, null));
        }

        public void DrawLinkToFile(string filepath, float x, float y, float width, float height, string name)
        {
            this._b0.Add(new LinkToFile(x, y, width, height, filepath, name));
        }

        public void DrawLinkToFile(string filepath, string linktext, PdfFont font, float fontsize, PdfColor textcolor, float x, float y, bool rightToleft)
        {
            float textWidth = font.GetTextWidth(linktext, fontsize);
            float height = font.Height(fontsize);
            this._b0.Add(new Text(x, y, linktext, textcolor, font, fontsize, true, rightToleft, null));
            this._b0.Add(new LinkToFile(x, y - height, textWidth, height, filepath, null));
        }

        public void DrawLinkToFileAttachment(string filepath, FileAttachmentIcon icon, float x, float y, float width, float height)
        {
            this._b0.Add(new A118(null, filepath, null, new RectangleF(x, y, width, height), PdfColor.Black, 0, icon));
        }

        public void DrawLinkToFileAttachment(string filepath, FileAttachmentIcon icon, Color color, float x, float y, float width, float height)
        {
            this._b0.Add(new A118(null, filepath, null, new RectangleF(x, y, width, height), new RGBColor(color), 0, icon));
        }

        public void DrawLinkToFileAttachment(string filepath, Stream filestream, FileAttachmentIcon icon, float x, float y, float width, float height)
        {
            this._b0.Add(new A118(null, filepath, filestream, new RectangleF(x, y, width, height), PdfColor.Black, 0, icon));
        }

        public void DrawLinkToFileAttachment(string filepath, Stream filestream, FileAttachmentIcon icon, Color color, float x, float y, float width, float height)
        {
            this._b0.Add(new A118(null, filepath, filestream, new RectangleF(x, y, width, height), new RGBColor(color), 0, icon));
        }

        public void DrawLinkToJavaScript(string script, float x, float y, float width, float height)
        {
            this._b0.Add(new LinkToJavaScript(script, x, y, width, height));
        }

        public void DrawLinkToJavaScript(string script, float x, float y, float width, float height, string name)
        {
            this._b0.Add(new LinkToJavaScript(script, x, y, width, height, name));
        }

        public void DrawLinkToJavaScript(string script, string linktext, PdfFont font, float fontsize, PdfColor textcolor, float x, float y, bool rightToleft)
        {
            float textWidth = font.GetTextWidth(linktext, fontsize);
            float height = font.Height(fontsize);
            this._b0.Add(new Text(x, y, linktext, textcolor, font, fontsize, true, rightToleft, null));
            this._b0.Add(new LinkToJavaScript(script, x, y - height, textWidth, height));
        }

        public void DrawLinkToPage(Destination destination, float x, float y, float width, float height)
        {
            this._b0.Add(new LinkToPage(destination, x, y, width, height));
        }

        public void DrawLinkToPage(Page page, float x, float y, float width, float height)
        {
            this._b0.Add(new LinkToPage(new Destination(page), x, y, width, height));
        }

        public void DrawLinkToPage(Destination destination, float x, float y, float width, float height, string name)
        {
            this._b0.Add(new LinkToPage(destination, x, y, width, height, name));
        }

        public void DrawLinkToPage(Page page, float x, float y, float width, float height, string name)
        {
            this._b0.Add(new LinkToPage(new Destination(page), x, y, width, height, name));
        }

        public void DrawLinkToPage(Page page, string linktext, PdfFont font, float fontsize, PdfColor textcolor, float x, float y, bool rightToleft)
        {
            float textWidth = font.GetTextWidth(linktext, fontsize);
            float height = font.Height(fontsize);
            this._b0.Add(new Text(x, y, linktext, textcolor, font, fontsize, true, rightToleft, null));
            this._b0.Add(new LinkToPage(new Destination(page), x, y - height, textWidth, height));
        }

        public void DrawLinkToURI(string uri, float x, float y, float width, float height)
        {
            this._b0.Add(new LinkToURI(uri, x, y, width, height));
        }

        public void DrawLinkToURI(string uri, float x, float y, float width, float height, string name)
        {
            this._b0.Add(new LinkToURI(uri, x, y, width, height, name));
        }

        public void DrawLinkToURI(string uri, string linktext, PdfFont font, float fontsize, PdfColor textcolor, float x, float y, bool rightToleft)
        {
            float textWidth = font.GetTextWidth(linktext, fontsize);
            float height = font.Height(fontsize);
            this._b0.Add(new Text(x, y, linktext, textcolor, font, fontsize, true, rightToleft, null));
            this._b0.Add(new LinkToURI(uri, x, y - height, textWidth, height));
        }

        public void DrawPolygon(PointF[] points, PdfColor linecolor, PdfColor fillcolor, GraphicsMode mode)
        {
            this._b0.Add(new Lines(points, 1f, LineStyle.Solid, linecolor, fillcolor, mode, true, null));
        }

        public void DrawPolygon(PointF[] points, float linewidth, LineStyle linestyle, PdfColor linecolor)
        {
            this._b0.Add(new Lines(points, linewidth, linestyle, linecolor, PdfColor.Transparent, GraphicsMode.stroke, true, null));
        }

        public void DrawPolygon(PointF[] points, float linewidth, LineStyle linestyle, PdfColor linecolor, PdfColor fillcolor, GraphicsMode mode)
        {
            this._b0.Add(new Lines(points, linewidth, linestyle, linecolor, fillcolor, mode, true, null));
        }

        public void DrawPolygon(PointF[] points, float linewidth, LineStyle linestyle, PdfColor linecolor, PdfColor fillcolor, GraphicsMode mode, string name)
        {
            this._b0.Add(new Lines(points, linewidth, linestyle, linecolor, fillcolor, mode, true, name));
        }

        public void DrawRectangle(float x, float y, float width, float height, PdfColor linecolor)
        {
            this._b0.Add(new MControl.Printing.Pdf.Drawing.Rectangle(x, y, width, height, 1f, LineStyle.Solid, linecolor, PdfColor.Transparent, GraphicsMode.stroke, null));
        }

        public void DrawRectangle(float x, float y, float width, float height, LineStyle linestyle, PdfColor linecolor)
        {
            this._b0.Add(new MControl.Printing.Pdf.Drawing.Rectangle(x, y, width, height, 1f, linestyle, linecolor, PdfColor.Transparent, GraphicsMode.stroke, null));
        }

        public void DrawRectangle(float x, float y, float width, float height, PdfColor linecolor, PdfColor fillcolor, GraphicsMode mode)
        {
            this._b0.Add(new MControl.Printing.Pdf.Drawing.Rectangle(x, y, width, height, 1f, LineStyle.Solid, linecolor, fillcolor, mode, null));
        }

        public void DrawRectangle(float x, float y, float width, float height, LineStyle linestyle, PdfColor linecolor, PdfColor fillcolor, GraphicsMode mode)
        {
            this._b0.Add(new MControl.Printing.Pdf.Drawing.Rectangle(x, y, width, height, 1f, linestyle, linecolor, fillcolor, mode, null));
        }

        public void DrawRectangle(float x, float y, float width, float height, float linewidth, LineStyle linestyle, PdfColor linecolor, PdfColor fillcolor, GraphicsMode mode)
        {
            this._b0.Add(new MControl.Printing.Pdf.Drawing.Rectangle(x, y, width, height, linewidth, linestyle, linecolor, fillcolor, mode, null));
        }

        public void DrawRectangle(float x, float y, float width, float height, float linewidth, LineStyle linestyle, PdfColor linecolor, PdfColor fillcolor, GraphicsMode mode, string name)
        {
            this._b0.Add(new MControl.Printing.Pdf.Drawing.Rectangle(x, y, width, height, linewidth, linestyle, linecolor, fillcolor, mode, name));
        }

        public void DrawRoundRectangle(float x, float y, float width, float height, float edgeRadius, PdfColor linecolor)
        {
            this._b0.Add(new RoundRectangle(x, y, width, height, edgeRadius, 1f, LineStyle.Solid, linecolor, PdfColor.Transparent, GraphicsMode.stroke, null));
        }

        public void DrawRoundRectangle(float x, float y, float width, float height, float edgeRadius, LineStyle linestyle, PdfColor linecolor)
        {
            this._b0.Add(new RoundRectangle(x, y, width, height, edgeRadius, 1f, linestyle, linecolor, PdfColor.Transparent, GraphicsMode.stroke, null));
        }

        public void DrawRoundRectangle(float x, float y, float width, float height, float edgeRadius, PdfColor linecolor, PdfColor fillcolor, GraphicsMode mode)
        {
            this._b0.Add(new RoundRectangle(x, y, width, height, edgeRadius, 1f, LineStyle.Solid, linecolor, fillcolor, mode, null));
        }

        public void DrawRoundRectangle(float x, float y, float width, float height, float edgeRadius, LineStyle linestyle, PdfColor linecolor, PdfColor fillcolor, GraphicsMode mode)
        {
            this._b0.Add(new RoundRectangle(x, y, width, height, edgeRadius, 1f, linestyle, linecolor, fillcolor, mode, null));
        }

        public void DrawRoundRectangle(float x, float y, float width, float height, float edgeRadius, float linewidth, LineStyle linestyle, PdfColor linecolor, PdfColor fillcolor, GraphicsMode mode)
        {
            this._b0.Add(new RoundRectangle(x, y, width, height, edgeRadius, linewidth, linestyle, linecolor, fillcolor, mode, null));
        }

        public void DrawRoundRectangle(float x, float y, float width, float height, float edgeRadius, float linewidth, LineStyle linestyle, PdfColor linecolor, PdfColor fillcolor, GraphicsMode mode, string name)
        {
            this._b0.Add(new RoundRectangle(x, y, width, height, edgeRadius, linewidth, linestyle, linecolor, fillcolor, mode, name));
        }

        public void DrawString(float x, float y, string text, PdfFont font, float fontsize)
        {
            this._b0.Add(new Text(x, y, text, PdfColor.Black, font, fontsize, false, false, null));
        }

        public void DrawString(float x, float y, string text, PdfColor textcolor, PdfFont font, float fontsize)
        {
            this._b0.Add(new Text(x, y, text, textcolor, font, fontsize, false, false, null));
        }

        public void DrawString(float x, float y, string text, PdfColor textcolor, PdfFont font, float fontsize, bool underline, bool righToleft)
        {
            this._b0.Add(new Text(x, y, text, textcolor, font, fontsize, underline, righToleft, null));
        }

        public void DrawString(float x, float y, string text, PdfColor textcolor, PdfFont font, float fontsize, bool underline, bool righToleft, string name)
        {
            this._b0.Add(new Text(x, y, text, textcolor, font, fontsize, underline, righToleft, name));
        }

        public Table DrawTable(float x, float y, Table table)
        {
            Table table2 = Table.A468(x, y, this._b1.Height - y, table);
            this._b0.Add(table);
            return table2;
        }

        public Table DrawTable(float x, float y, float height, Table table)
        {
            Table table2 = Table.A468(x, y, height, table);
            this._b0.Add(table);
            return table2;
        }

        public void DrawTextAnnot(string title, string content, float x, float y)
        {
            this._b0.Add(new Note(title, content, x, y, 0f, 0f, PdfColor.Black, false, AnnotIcon.Note));
        }

        public void DrawTextAnnot(string title, string content, float x, float y, float width, float height)
        {
            this._b0.Add(new Note(title, content, x, y, width, height, PdfColor.Black, false, AnnotIcon.Note));
        }

        public void DrawTextAnnot(string title, string content, float x, float y, float width, float height, bool isopen)
        {
            this._b0.Add(new Note(title, content, x, y, width, height, PdfColor.Black, isopen, AnnotIcon.Note));
        }

        public void DrawTextAnnot(string title, string content, float x, float y, float width, float height, RGBColor color, bool isopen)
        {
            this._b0.Add(new Note(title, content, x, y, width, height, color, isopen, AnnotIcon.Note));
        }

        public void DrawTextAnnot(string title, string content, float x, float y, float width, float height, RGBColor color, bool isopen, AnnotIcon icon)
        {
            this._b0.Add(new Note(title, content, x, y, width, height, color, isopen, icon));
        }

        public void DrawTextAnnot(string title, string content, float x, float y, float width, float height, RGBColor color, bool isopen, AnnotIcon icon, string name)
        {
            this._b0.Add(new Note(title, content, x, y, width, height, color, isopen, icon, name));
        }

        public TextArea DrawTextArea(float x, float y, TextArea textarea)
        {
            TextArea area = textarea.A468(x, y, this._b1.Height - y);
            this._b0.Add(textarea);
            return area;
        }

        public TextArea DrawTextArea(float x, float y, float height, TextArea textarea)
        {
            TextArea area = textarea.A468(x, y, height);
            this._b0.Add(textarea);
            return area;
        }

        public void DrawTextBox(float x, float y, float width, float height, string text, TextStyle textstyle)
        {
            this._b0.Add(new TextBoxEx(x, y, width, height, text, textstyle, TextAlignment.Left, false, 2f, null, null, null));
        }

        public void DrawTextBox(float x, float y, float width, float height, string text, TextStyle textstyle, TextAlignment textalign, Border border)
        {
            this._b0.Add(new TextBoxEx(x, y, width, height, text, textstyle, textalign, false, 2f, null, border, null));
        }

        public void DrawTextBox(float x, float y, float width, float height, string text, TextStyle textstyle, TextAlignment textalign, bool rightToleft, float pad, PdfColor backcolor, Border border)
        {
            this._b0.Add(new TextBoxEx(x, y, width, height, text, textstyle, textalign, rightToleft, pad, backcolor, border, null));
        }

        public void RestoreState()
        {
            this._b0.Add(new MControl.Printing.Pdf.Drawing.RestoreState());
        }

        public void SaveState()
        {
            this._b0.Add(new MControl.Printing.Pdf.Drawing.SaveState());
        }

        public void SetRotateTransform(float x, float y, float angle)
        {
            this._b0.Add(new A507(x, y, angle));
        }

        public void SetScaleTransform(float x, float y, float xScale, float yScale)
        {
            this._b0.Add(new A509(x, y, xScale, yScale));
        }

        public void SetSkewTransform(float x, float y, float xSkewAngle, float ySkewAngle)
        {
            this._b0.Add(new A513(x, y, xSkewAngle, ySkewAngle));
        }

        public void SetTransform(float x, float y, float xTranslate, float yTranslate, float rotate, float xScale, float yScale, float xSkewAngle, float ySkewAngle)
        {
            Transformation element = new Transformation();
            element.X = x;
            element.Y = y;
            element.xTranslate = xTranslate;
            element.yTranslate = yTranslate;
            element.Rotate = rotate;
            element.xScale = xScale;
            element.yScale = yScale;
            element.xSkewAngle = xSkewAngle;
            element.ySkewAngle = ySkewAngle;
            this._b0.Add(element);
        }

        public void SetTranslateTransform(float xTranslate, float yTranslate)
        {
            this._b0.Add(new A517(xTranslate, yTranslate));
        }

        public void SetTransparency(float value)
        {
            this._b0.Add(new Transparency(value));
        }

        public void SetTransparency(float stroke, float nonstroke)
        {
            this._b0.Add(new Transparency(stroke, nonstroke));
        }

        internal Page A141
        {
            get
            {
                return this._b1;
            }
        }

        internal int A293
        {
            get
            {
                return this._b0.Count;
            }
        }

        public MControl.Printing.Pdf.ColorSpace ColorSpace
        {
            get
            {
                return this._b2;
            }
            set
            {
                this._b2 = value;
            }
        }

        public GraphicsElements Elements
        {
            get
            {
                return this._b0;
            }
        }
    }
}

