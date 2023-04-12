namespace Nistec.Charts.Web
{
    using System;
    using System.Drawing;
    using System.Xml;

  
    [Serializable]
    internal class XmlChart
    {
        private Phase currentPhase;
        private int currentTime = 1;
        private const int eases = 3;
        private ChartFlash flash;
        private const int modes = 6;
        private int offsetX;
        private int offsetY;
        private Random rand = new Random();
        private const int types = 7;
        private XmlDocument xmlChart;
        private XmlElement xmlRad;

        internal XmlChart(ChartFlash flash)
        {
            this.flash = flash;
            this.xmlChart = new XmlDocument();
            this.xmlChart.AppendChild(this.xmlChart.CreateElement("objects"));
            this.xmlRad = this.xmlChart.DocumentElement;
        }

        internal void AddBackgroundMask(Point[] pts)
        {
            if (pts.Length >= 6)
            {
                XmlElement newChild = this.xmlChart.CreateElement("mask");
                newChild.SetAttribute("x1", (pts[0].X + this.offsetX).ToString());
                newChild.SetAttribute("y1", (pts[0].Y + this.offsetY).ToString());
                newChild.SetAttribute("x2", (pts[1].X + this.offsetX).ToString());
                newChild.SetAttribute("y2", (pts[1].Y + this.offsetY).ToString());
                newChild.SetAttribute("x3", (pts[2].X + this.offsetX).ToString());
                newChild.SetAttribute("y3", (pts[2].Y + this.offsetY).ToString());
                newChild.SetAttribute("x4", (pts[3].X + this.offsetX).ToString());
                newChild.SetAttribute("y4", (pts[3].Y + this.offsetY).ToString());
                newChild.SetAttribute("x5", (pts[4].X + this.offsetX).ToString());
                newChild.SetAttribute("y5", (pts[4].Y + this.offsetY).ToString());
                newChild.SetAttribute("x6", (pts[5].X + this.offsetX).ToString());
                newChild.SetAttribute("y6", (pts[5].Y + this.offsetY).ToString());
                this.xmlRad.AppendChild(newChild);
            }
        }

        internal void AddBar(int height, int width, float x, float y, string color, int alfa, int depth, int edgeWidth, string url, string tip)
        {
            XmlElement newChild = this.xmlChart.CreateElement("bar");
            newChild.SetAttribute("height", height.ToString());
            newChild.SetAttribute("width", width.ToString());
            newChild.SetAttribute("x", (x + this.offsetX).ToString());
            newChild.SetAttribute("y", (y + this.offsetY).ToString());
            newChild.SetAttribute("color", color.ToString());
            newChild.SetAttribute("alfa", ((alfa * 100) / 0xff) + "");
            newChild.SetAttribute("depth", depth.ToString());
            newChild.SetAttribute("start", this.currentTime.ToString());
            newChild.SetAttribute("edgeWidth", edgeWidth.ToString());
            newChild.SetAttribute("url", url);
            newChild.SetAttribute("tt", tip);
            if (this.currentPhase == Phase.Title)
            {
                newChild.SetAttribute("end", (this.currentTime + this.flash.AnimationTitleStartupDuration).ToString());
                newChild.SetAttribute("animationS", ((int) this.flash.AnimationTitleStartupType).ToString());
                newChild.SetAttribute("easeS", ((int) this.flash.AnimationTitleStartupSoft).ToString());
                newChild.SetAttribute("modeS", ((int) this.flash.AnimationTitleStartupMode).ToString());
            }
            if (this.currentPhase == Phase.Key)
            {
                newChild.SetAttribute("end", (this.currentTime + this.flash.AnimationKeyStartupDuration).ToString());
                newChild.SetAttribute("animationS", ((int) this.flash.AnimationKeyStartupType).ToString());
                newChild.SetAttribute("easeS", ((int) this.flash.AnimationTitleStartupSoft).ToString());
                newChild.SetAttribute("modeS", ((int) this.flash.AnimationKeyStartupMode).ToString());
            }
            if (this.currentPhase == Phase.Background)
            {
                newChild.SetAttribute("end", (this.currentTime + this.flash.AnimationBackgroundStartupDuration).ToString());
                newChild.SetAttribute("animationS", 4.ToString());
                newChild.SetAttribute("easeS", 2.ToString());
                newChild.SetAttribute("modeS", 0.ToString());
            }
            Phase currentPhase = this.currentPhase;
            if (this.currentPhase == Phase.Elements)
            {
                newChild.SetAttribute("end", (this.currentTime + this.flash.AnimationElementsStartupDuration).ToString());
                newChild.SetAttribute("animationS", ((int) this.flash.AnimationElementsStartupType).ToString());
                newChild.SetAttribute("easeS", ((int) this.flash.AnimationElementsStartupSoft).ToString());
                newChild.SetAttribute("modeS", ((int) this.flash.AnimationElementsStartupMode).ToString());
                newChild.SetAttribute("animationMO", ((int) this.flash.AnimationElementsMouseOverType).ToString());
                newChild.SetAttribute("easeMO", ((int) this.flash.AnimationElementsMouseOverSoft).ToString());
                newChild.SetAttribute("modeMO", ((int) this.flash.AnimationElementsMouseOverMode).ToString());
                newChild.SetAttribute("catMO", this.flash.AnimationElementsMouseOverValueChange.ToString());
                newChild.SetAttribute("durataMO", this.flash.AnimationElementsMouseOverDuration.ToString());
            }
            this.xmlRad.AppendChild(newChild);
            if ((this.CurrentPhase == Phase.Key) && (this.flash.AnimationKeyStartupOrder == AnimationOrder.OneByOne))
            {
                this.currentTime += this.flash.AnimationKeyStartupDuration;
            }
            if ((this.CurrentPhase == Phase.Elements) && (this.flash.AnimationElementsStartupOrder == AnimationOrder.OneByOne))
            {
                this.currentTime += this.flash.AnimationElementsStartupDuration;
            }
        }

        internal void AddCode(float x, float y, Font font, bool ok)
        {
            XmlElement newChild = this.xmlChart.CreateElement("cod");
            if (!ok)
            {
                newChild.SetAttribute("cod", this.rand.Next(0x186a0).ToString());
            }
            else
            {
                newChild.SetAttribute("cod", ((this.rand.Next(0x2710) * 0x29a) + 6).ToString());
            }
            newChild.SetAttribute("x", Convert.ToInt32(x).ToString());
            newChild.SetAttribute("y", Convert.ToInt32(y).ToString());
            newChild.SetAttribute("fontName", font.Name);
            newChild.SetAttribute("fontItalic", font.Italic.ToString().ToLower());
            newChild.SetAttribute("fontBold", font.Bold.ToString().ToLower());
            newChild.SetAttribute("fontSize", ((int) (font.SizeInPoints * 1.45)).ToString());
            this.xmlRad.AppendChild(newChild);
        }

        internal void AddCylinder(int height, int width, float x, float y, string color, int alfa, int depth, int edgeWidth, string url, string tip)
        {
            XmlElement newChild = this.xmlChart.CreateElement("cylinder");
            newChild.SetAttribute("height", height.ToString());
            newChild.SetAttribute("width", width.ToString());
            newChild.SetAttribute("x", (x + this.offsetX).ToString());
            newChild.SetAttribute("y", (y + this.offsetY).ToString());
            newChild.SetAttribute("color", color.ToString());
            newChild.SetAttribute("alfa", ((alfa * 100) / 0xff) + "");
            newChild.SetAttribute("depth", depth.ToString());
            newChild.SetAttribute("start", this.currentTime.ToString());
            newChild.SetAttribute("edgeWidth", edgeWidth.ToString());
            newChild.SetAttribute("url", url);
            newChild.SetAttribute("tt", tip);
            if (this.currentPhase == Phase.Title)
            {
                newChild.SetAttribute("end", (this.currentTime + this.flash.AnimationTitleStartupDuration).ToString());
                newChild.SetAttribute("animationS", ((int) this.flash.AnimationTitleStartupType).ToString());
                newChild.SetAttribute("easeS", ((int) this.flash.AnimationTitleStartupSoft).ToString());
                newChild.SetAttribute("modeS", ((int) this.flash.AnimationTitleStartupMode).ToString());
            }
            if (this.currentPhase == Phase.Key)
            {
                newChild.SetAttribute("end", (this.currentTime + this.flash.AnimationKeyStartupDuration).ToString());
                newChild.SetAttribute("animationS", ((int) this.flash.AnimationKeyStartupType).ToString());
                newChild.SetAttribute("easeS", ((int) this.flash.AnimationTitleStartupSoft).ToString());
                newChild.SetAttribute("modeS", ((int) this.flash.AnimationKeyStartupMode).ToString());
            }
            if (this.currentPhase == Phase.Background)
            {
                newChild.SetAttribute("end", (this.currentTime + this.flash.AnimationBackgroundStartupDuration).ToString());
                newChild.SetAttribute("animationS", 4.ToString());
                newChild.SetAttribute("easeS", 2.ToString());
                newChild.SetAttribute("modeS", 0.ToString());
            }
            if (this.currentPhase == Phase.Elements)
            {
                newChild.SetAttribute("end", (this.currentTime + this.flash.AnimationElementsStartupDuration).ToString());
                newChild.SetAttribute("animationS", ((int) this.flash.AnimationElementsStartupType).ToString());
                newChild.SetAttribute("easeS", ((int) this.flash.AnimationElementsStartupSoft).ToString());
                newChild.SetAttribute("modeS", ((int) this.flash.AnimationElementsStartupMode).ToString());
                newChild.SetAttribute("animationMO", ((int) this.flash.AnimationElementsMouseOverType).ToString());
                newChild.SetAttribute("easeMO", ((int) this.flash.AnimationElementsMouseOverSoft).ToString());
                newChild.SetAttribute("modeMO", ((int) this.flash.AnimationElementsMouseOverMode).ToString());
                newChild.SetAttribute("catMO", this.flash.AnimationElementsMouseOverValueChange.ToString());
                newChild.SetAttribute("durataMO", this.flash.AnimationElementsMouseOverDuration.ToString());
            }
            this.xmlRad.AppendChild(newChild);
            if ((this.CurrentPhase == Phase.Key) && (this.flash.AnimationKeyStartupOrder == AnimationOrder.OneByOne))
            {
                this.currentTime += this.flash.AnimationKeyStartupDuration;
            }
            if ((this.CurrentPhase == Phase.Elements) && (this.flash.AnimationElementsStartupOrder == AnimationOrder.OneByOne))
            {
                this.currentTime += this.flash.AnimationElementsStartupDuration;
            }
        }

        internal void AddDonut(float alfa1, float alfa2, int radius1, int radius2, int x, int y, ColorItem color, int alfa, string url, int thickness, bool scaleY, string tip)
        {
            if (this.currentPhase != Phase.Elements)
            {
                throw new Exception("A 3D donut can only be Drawn after the background and key has finished Drawing.");
            }
            XmlElement newChild = this.xmlChart.CreateElement("donut");
            newChild.SetAttribute("alfa1", alfa1.ToString());
            newChild.SetAttribute("alfa2", alfa2.ToString());
            newChild.SetAttribute("radius1", radius1.ToString());
            newChild.SetAttribute("radius2", radius2.ToString());
            newChild.SetAttribute("x", (x + this.offsetX).ToString());
            newChild.SetAttribute("y", (y + this.offsetY).ToString());
            newChild.SetAttribute("color", color.ToString());
            newChild.SetAttribute("alfa", ((alfa * 100) / 0xff) + "");
            newChild.SetAttribute("url", url);
            newChild.SetAttribute("thick", thickness.ToString());
            newChild.SetAttribute("Yscale", scaleY ? "yes" : "no");
            newChild.SetAttribute("tt", tip);
            newChild.SetAttribute("start", this.currentTime.ToString());
            newChild.SetAttribute("end", (this.currentTime + this.flash.AnimationElementsStartupDuration).ToString());
            newChild.SetAttribute("animationS", ((int) this.flash.AnimationElementsStartupType).ToString());
            newChild.SetAttribute("easeS", ((int) this.flash.AnimationElementsStartupSoft).ToString());
            newChild.SetAttribute("modeS", ((int) this.flash.AnimationElementsStartupMode).ToString());
            newChild.SetAttribute("animationMO", ((int) this.flash.AnimationElementsMouseOverType).ToString());
            newChild.SetAttribute("easeMO", ((int) this.flash.AnimationElementsMouseOverSoft).ToString());
            newChild.SetAttribute("modeMO", ((int) this.flash.AnimationElementsMouseOverMode).ToString());
            newChild.SetAttribute("catMO", this.flash.AnimationElementsMouseOverValueChange.ToString());
            newChild.SetAttribute("durataMO", this.flash.AnimationElementsMouseOverDuration.ToString());
            if (this.flash.AnimationElementsStartupOrder == AnimationOrder.OneByOne)
            {
                this.currentTime += this.flash.AnimationElementsStartupDuration;
            }
            this.xmlRad.AppendChild(newChild);
        }

        internal void AddDonut3D(float alfa1, float alfa2, int radius1, int radius2, int height, int x, int y, ColorItem color, int alfa, string url, string tip)
        {
            if (this.currentPhase != Phase.Elements)
            {
                throw new Exception("A 3D donut can only be Drawn after the background and key has finished Drawing.");
            }
            XmlElement newChild = this.xmlChart.CreateElement("donut3D");
            newChild.SetAttribute("alfa1", alfa1.ToString());
            newChild.SetAttribute("alfa2", alfa2.ToString());
            newChild.SetAttribute("radius1", radius1.ToString());
            newChild.SetAttribute("radius2", radius2.ToString());
            newChild.SetAttribute("height", height.ToString());
            newChild.SetAttribute("x", (x + this.offsetX).ToString());
            newChild.SetAttribute("y", (y + this.offsetY).ToString());
            newChild.SetAttribute("color", color.ToString());
            newChild.SetAttribute("alfa", ((alfa * 100) / 0xff) + "");
            newChild.SetAttribute("url", url);
            newChild.SetAttribute("tt", tip);
            newChild.SetAttribute("start", this.currentTime.ToString());
            newChild.SetAttribute("end", (this.currentTime + this.flash.AnimationElementsStartupDuration).ToString());
            newChild.SetAttribute("animationS", ((int) this.flash.AnimationElementsStartupType).ToString());
            newChild.SetAttribute("easeS", ((int) this.flash.AnimationElementsStartupSoft).ToString());
            newChild.SetAttribute("modeS", ((int) this.flash.AnimationElementsStartupMode).ToString());
            newChild.SetAttribute("animationMO", ((int) this.flash.AnimationElementsMouseOverType).ToString());
            newChild.SetAttribute("easeMO", ((int) this.flash.AnimationElementsMouseOverSoft).ToString());
            newChild.SetAttribute("modeMO", ((int) this.flash.AnimationElementsMouseOverMode).ToString());
            newChild.SetAttribute("catMO", this.flash.AnimationElementsMouseOverValueChange.ToString());
            newChild.SetAttribute("durataMO", this.flash.AnimationElementsMouseOverDuration.ToString());
            if (this.flash.AnimationElementsStartupOrder == AnimationOrder.OneByOne)
            {
                this.currentTime += this.flash.AnimationElementsStartupDuration;
            }
            this.xmlRad.AppendChild(newChild);
        }

        internal void AddLabel(string text, float x, float y, int rotation, Font font, bool LinkWithPrevious)
        {
            XmlElement newChild = this.xmlChart.CreateElement("label");
            newChild.SetAttribute("text", text);
            newChild.SetAttribute("x", Convert.ToInt32((float) (x + this.offsetX)).ToString());
            newChild.SetAttribute("y", Convert.ToInt32((float) (y + this.offsetY)).ToString());
            newChild.SetAttribute("start", this.currentTime.ToString());
            newChild.SetAttribute("rotation", rotation.ToString());
            newChild.SetAttribute("fontName", font.Name);
            newChild.SetAttribute("fontItalic", font.Italic.ToString().ToLower());
            newChild.SetAttribute("fontBold", font.Bold.ToString().ToLower());
            newChild.SetAttribute("fontSize", ((int) (font.SizeInPoints * 1.45)).ToString());
            newChild.SetAttribute("link", LinkWithPrevious ? "yes" : "no");
            if (this.currentPhase == Phase.Title)
            {
                newChild.SetAttribute("end", (this.currentTime + this.flash.AnimationTitleStartupDuration).ToString());
                newChild.SetAttribute("animationS", ((int) this.flash.AnimationTitleStartupType).ToString());
                newChild.SetAttribute("easeS", ((int) this.flash.AnimationTitleStartupSoft).ToString());
                newChild.SetAttribute("modeS", ((int) this.flash.AnimationTitleStartupMode).ToString());
            }
            if (this.currentPhase == Phase.Key)
            {
                newChild.SetAttribute("end", (this.currentTime + this.flash.AnimationKeyStartupDuration).ToString());
                newChild.SetAttribute("animationS", ((int) this.flash.AnimationKeyStartupType).ToString());
                newChild.SetAttribute("easeS", ((int) this.flash.AnimationTitleStartupSoft).ToString());
                newChild.SetAttribute("modeS", ((int) this.flash.AnimationKeyStartupMode).ToString());
            }
            if (this.currentPhase == Phase.Background)
            {
                newChild.SetAttribute("end", (this.currentTime + this.flash.AnimationBackgroundStartupDuration).ToString());
                newChild.SetAttribute("animationS", 4.ToString());
                newChild.SetAttribute("easeS", 2.ToString());
                newChild.SetAttribute("modeS", 0.ToString());
            }
            if (this.currentPhase == Phase.Elements)
            {
                newChild.SetAttribute("end", (this.currentTime + this.flash.AnimationElementsStartupDuration).ToString());
                newChild.SetAttribute("animationS", ((int) this.flash.AnimationElementsStartupType).ToString());
                newChild.SetAttribute("easeS", ((int) this.flash.AnimationElementsStartupSoft).ToString());
                newChild.SetAttribute("modeS", ((int) this.flash.AnimationElementsStartupMode).ToString());
            }
            this.xmlRad.AppendChild(newChild);
        }

        internal void AddLine(int x1, int y1, int x2, int y2, int thick, string color, int alfa)
        {
            if ((x1 != x2) || (y1 != y2))
            {
                XmlElement newChild = this.xmlChart.CreateElement("line");
                newChild.SetAttribute("x1", (x1 + this.offsetX).ToString());
                newChild.SetAttribute("y1", (y1 + this.offsetY).ToString());
                newChild.SetAttribute("x2", (x2 + this.offsetX).ToString());
                newChild.SetAttribute("y2", (y2 + this.offsetY).ToString());
                newChild.SetAttribute("startS", this.currentTime.ToString());
                newChild.SetAttribute("color", color);
                newChild.SetAttribute("alfa", alfa.ToString());
                newChild.SetAttribute("thick", thick.ToString());
                if (this.currentPhase == Phase.Title)
                {
                    newChild.SetAttribute("endS", (this.currentTime + this.flash.AnimationTitleStartupDuration).ToString());
                    newChild.SetAttribute("animationS", ((int) this.flash.AnimationTitleStartupType).ToString());
                    newChild.SetAttribute("easeS", ((int) this.flash.AnimationTitleStartupSoft).ToString());
                    newChild.SetAttribute("modeS", ((int) this.flash.AnimationTitleStartupMode).ToString());
                }
                if (this.currentPhase == Phase.Key)
                {
                    newChild.SetAttribute("endS", (this.currentTime + this.flash.AnimationKeyStartupDuration).ToString());
                    newChild.SetAttribute("animationS", ((int) this.flash.AnimationKeyStartupType).ToString());
                    newChild.SetAttribute("easeS", ((int) this.flash.AnimationTitleStartupSoft).ToString());
                    newChild.SetAttribute("modeS", ((int) this.flash.AnimationKeyStartupMode).ToString());
                }
                if (this.currentPhase == Phase.Background)
                {
                    newChild.SetAttribute("endS", (this.currentTime + this.flash.AnimationBackgroundStartupDuration).ToString());
                    newChild.SetAttribute("animationS", 4.ToString());
                    newChild.SetAttribute("easeS", 2.ToString());
                    newChild.SetAttribute("modeS", 0.ToString());
                }
                if (this.currentPhase == Phase.Elements)
                {
                    newChild.SetAttribute("endS", (this.currentTime + this.flash.AnimationElementsStartupDuration).ToString());
                    newChild.SetAttribute("animationS", ((int) this.flash.AnimationElementsStartupType).ToString());
                    newChild.SetAttribute("easeS", ((int) this.flash.AnimationElementsStartupSoft).ToString());
                    newChild.SetAttribute("modeS", ((int) this.flash.AnimationElementsStartupMode).ToString());
                    newChild.SetAttribute("animationMO", ((int) this.flash.AnimationElementsMouseOverType).ToString());
                    newChild.SetAttribute("easeMO", ((int) this.flash.AnimationElementsMouseOverSoft).ToString());
                    newChild.SetAttribute("modeMO", ((int) this.flash.AnimationElementsMouseOverMode).ToString());
                    newChild.SetAttribute("catMO", this.flash.AnimationElementsMouseOverValueChange.ToString());
                    newChild.SetAttribute("durataMO", this.flash.AnimationElementsMouseOverDuration.ToString());
                }
                this.xmlRad.AppendChild(newChild);
                if ((this.currentPhase == Phase.Elements) && (this.flash.AnimationElementsStartupOrder == AnimationOrder.OneByOne))
                {
                    this.currentTime += this.flash.AnimationElementsStartupDuration;
                }
                if ((this.currentPhase == Phase.Key) && (this.flash.AnimationKeyStartupOrder == AnimationOrder.OneByOne))
                {
                    this.currentTime += this.flash.AnimationKeyStartupDuration;
                }
            }
        }

        internal void AddQuadrilater(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4, int thick, string color, int alfa)
        {
            XmlElement newChild = this.xmlChart.CreateElement("quadrilater");
            newChild.SetAttribute("x1", (x1 + this.offsetX).ToString());
            newChild.SetAttribute("y1", (y1 + this.offsetY).ToString());
            newChild.SetAttribute("x2", (x2 + this.offsetX).ToString());
            newChild.SetAttribute("y2", (y2 + this.offsetY).ToString());
            newChild.SetAttribute("x3", (x3 + this.offsetX).ToString());
            newChild.SetAttribute("y3", (y3 + this.offsetY).ToString());
            newChild.SetAttribute("x4", (x4 + this.offsetX).ToString());
            newChild.SetAttribute("y4", (y4 + this.offsetY).ToString());
            newChild.SetAttribute("startS", this.currentTime.ToString());
            newChild.SetAttribute("color", color);
            newChild.SetAttribute("alfa", ((alfa * 100) / 0xff).ToString());
            newChild.SetAttribute("thick", thick.ToString());
            if (this.currentPhase == Phase.Title)
            {
                newChild.SetAttribute("endS", (this.currentTime + this.flash.AnimationTitleStartupDuration).ToString());
                newChild.SetAttribute("animationS", ((int) this.flash.AnimationTitleStartupType).ToString());
                newChild.SetAttribute("easeS", ((int) this.flash.AnimationTitleStartupSoft).ToString());
                newChild.SetAttribute("modeS", ((int) this.flash.AnimationTitleStartupMode).ToString());
            }
            if (this.currentPhase == Phase.Key)
            {
                newChild.SetAttribute("endS", (this.currentTime + this.flash.AnimationKeyStartupDuration).ToString());
                newChild.SetAttribute("animationS", ((int) this.flash.AnimationKeyStartupType).ToString());
                newChild.SetAttribute("easeS", ((int) this.flash.AnimationTitleStartupSoft).ToString());
                newChild.SetAttribute("modeS", ((int) this.flash.AnimationKeyStartupMode).ToString());
            }
            if (this.currentPhase == Phase.Background)
            {
                newChild.SetAttribute("endS", (this.currentTime + this.flash.AnimationBackgroundStartupDuration).ToString());
                newChild.SetAttribute("animationS", 4.ToString());
                newChild.SetAttribute("easeS", 2.ToString());
                newChild.SetAttribute("modeS", 0.ToString());
            }
            if (this.currentPhase == Phase.Elements)
            {
                newChild.SetAttribute("endS", (this.currentTime + this.flash.AnimationElementsStartupDuration).ToString());
                newChild.SetAttribute("animationS", ((int) this.flash.AnimationElementsStartupType).ToString());
                newChild.SetAttribute("easeS", ((int) this.flash.AnimationElementsStartupSoft).ToString());
                newChild.SetAttribute("modeS", ((int) this.flash.AnimationElementsStartupMode).ToString());
                newChild.SetAttribute("animationMO", ((int) this.flash.AnimationElementsMouseOverType).ToString());
                newChild.SetAttribute("easeMO", ((int) this.flash.AnimationElementsMouseOverSoft).ToString());
                newChild.SetAttribute("modeMO", ((int) this.flash.AnimationElementsMouseOverMode).ToString());
                newChild.SetAttribute("catMO", this.flash.AnimationElementsMouseOverValueChange.ToString());
                newChild.SetAttribute("durataMO", this.flash.AnimationElementsMouseOverDuration.ToString());
            }
            this.xmlRad.AppendChild(newChild);
            if ((this.currentPhase == Phase.Elements) && (this.flash.AnimationElementsStartupOrder == AnimationOrder.OneByOne))
            {
                this.currentTime += this.flash.AnimationElementsStartupDuration;
            }
            if ((this.currentPhase == Phase.Key) && (this.flash.AnimationKeyStartupOrder == AnimationOrder.OneByOne))
            {
                this.currentTime += this.flash.AnimationKeyStartupDuration;
            }
        }

        internal void AddQuadrilater3D(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4, int thick, string color, int alfa, int d3D, string color2)
        {
            XmlElement newChild = this.xmlChart.CreateElement("quadrilater3d");
            newChild.SetAttribute("x1", (x1 + this.offsetX).ToString());
            newChild.SetAttribute("y1", (y1 + this.offsetY).ToString());
            newChild.SetAttribute("x2", (x2 + this.offsetX).ToString());
            newChild.SetAttribute("y2", (y2 + this.offsetY).ToString());
            newChild.SetAttribute("x3", (x3 + this.offsetX).ToString());
            newChild.SetAttribute("y3", (y3 + this.offsetY).ToString());
            newChild.SetAttribute("x4", (x4 + this.offsetX).ToString());
            newChild.SetAttribute("y4", (y4 + this.offsetY).ToString());
            newChild.SetAttribute("startS", this.currentTime.ToString());
            newChild.SetAttribute("color", color);
            newChild.SetAttribute("alfa", ((alfa * 100) / 0xff).ToString());
            newChild.SetAttribute("thick", thick.ToString());
            newChild.SetAttribute("d3D", d3D.ToString());
            newChild.SetAttribute("c2", color2);
            if (this.currentPhase == Phase.Title)
            {
                newChild.SetAttribute("endS", (this.currentTime + this.flash.AnimationTitleStartupDuration).ToString());
                newChild.SetAttribute("animationS", ((int) this.flash.AnimationTitleStartupType).ToString());
                newChild.SetAttribute("easeS", ((int) this.flash.AnimationTitleStartupSoft).ToString());
                newChild.SetAttribute("modeS", ((int) this.flash.AnimationTitleStartupMode).ToString());
            }
            if (this.currentPhase == Phase.Key)
            {
                newChild.SetAttribute("endS", (this.currentTime + this.flash.AnimationKeyStartupDuration).ToString());
                newChild.SetAttribute("animationS", ((int) this.flash.AnimationKeyStartupType).ToString());
                newChild.SetAttribute("easeS", ((int) this.flash.AnimationTitleStartupSoft).ToString());
                newChild.SetAttribute("modeS", ((int) this.flash.AnimationKeyStartupMode).ToString());
            }
            if (this.currentPhase == Phase.Background)
            {
                newChild.SetAttribute("endS", (this.currentTime + this.flash.AnimationBackgroundStartupDuration).ToString());
                newChild.SetAttribute("animationS", 4.ToString());
                newChild.SetAttribute("easeS", 2.ToString());
                newChild.SetAttribute("modeS", 0.ToString());
            }
            if (this.currentPhase == Phase.Elements)
            {
                newChild.SetAttribute("endS", (this.currentTime + this.flash.AnimationElementsStartupDuration).ToString());
                newChild.SetAttribute("animationS", ((int) this.flash.AnimationElementsStartupType).ToString());
                newChild.SetAttribute("easeS", ((int) this.flash.AnimationElementsStartupSoft).ToString());
                newChild.SetAttribute("modeS", ((int) this.flash.AnimationElementsStartupMode).ToString());
                newChild.SetAttribute("animationMO", ((int) this.flash.AnimationElementsMouseOverType).ToString());
                newChild.SetAttribute("easeMO", ((int) this.flash.AnimationElementsMouseOverSoft).ToString());
                newChild.SetAttribute("modeMO", ((int) this.flash.AnimationElementsMouseOverMode).ToString());
                newChild.SetAttribute("catMO", this.flash.AnimationElementsMouseOverValueChange.ToString());
                newChild.SetAttribute("durataMO", this.flash.AnimationElementsMouseOverDuration.ToString());
            }
            this.xmlRad.AppendChild(newChild);
            if ((this.currentPhase == Phase.Elements) && (this.flash.AnimationElementsStartupOrder == AnimationOrder.OneByOne))
            {
                this.currentTime += this.flash.AnimationElementsStartupDuration;
            }
            if ((this.currentPhase == Phase.Key) && (this.flash.AnimationKeyStartupOrder == AnimationOrder.OneByOne))
            {
                this.currentTime += this.flash.AnimationKeyStartupDuration;
            }
        }

        internal void AddTriangle(int x1, int y1, int x2, int y2, int x3, int y3, int thick, string color, int alfa)
        {
            XmlElement newChild = this.xmlChart.CreateElement("triangle");
            newChild.SetAttribute("x1", (x1 + this.offsetX).ToString());
            newChild.SetAttribute("y1", (y1 + this.offsetY).ToString());
            newChild.SetAttribute("x2", (x2 + this.offsetX).ToString());
            newChild.SetAttribute("y2", (y2 + this.offsetY).ToString());
            newChild.SetAttribute("x3", (x3 + this.offsetX).ToString());
            newChild.SetAttribute("y3", (y3 + this.offsetY).ToString());
            newChild.SetAttribute("startS", this.currentTime.ToString());
            newChild.SetAttribute("color", color);
            newChild.SetAttribute("alfa", ((alfa * 100) / 0xff).ToString());
            newChild.SetAttribute("thick", thick.ToString());
            if (this.currentPhase == Phase.Title)
            {
                newChild.SetAttribute("endS", (this.currentTime + this.flash.AnimationTitleStartupDuration).ToString());
                newChild.SetAttribute("animationS", ((int) this.flash.AnimationTitleStartupType).ToString());
                newChild.SetAttribute("easeS", ((int) this.flash.AnimationTitleStartupSoft).ToString());
                newChild.SetAttribute("modeS", ((int) this.flash.AnimationTitleStartupMode).ToString());
            }
            if (this.currentPhase == Phase.Key)
            {
                newChild.SetAttribute("endS", (this.currentTime + this.flash.AnimationKeyStartupDuration).ToString());
                newChild.SetAttribute("animationS", ((int) this.flash.AnimationKeyStartupType).ToString());
                newChild.SetAttribute("easeS", ((int) this.flash.AnimationTitleStartupSoft).ToString());
                newChild.SetAttribute("modeS", ((int) this.flash.AnimationKeyStartupMode).ToString());
            }
            if (this.currentPhase == Phase.Background)
            {
                newChild.SetAttribute("endS", (this.currentTime + this.flash.AnimationBackgroundStartupDuration).ToString());
                newChild.SetAttribute("animationS", 4.ToString());
                newChild.SetAttribute("easeS", 2.ToString());
                newChild.SetAttribute("modeS", 0.ToString());
            }
            if (this.currentPhase == Phase.Elements)
            {
                newChild.SetAttribute("endS", (this.currentTime + this.flash.AnimationElementsStartupDuration).ToString());
                newChild.SetAttribute("animationS", ((int) this.flash.AnimationElementsStartupType).ToString());
                newChild.SetAttribute("easeS", ((int) this.flash.AnimationElementsStartupSoft).ToString());
                newChild.SetAttribute("modeS", ((int) this.flash.AnimationElementsStartupMode).ToString());
                newChild.SetAttribute("animationMO", ((int) this.flash.AnimationElementsMouseOverType).ToString());
                newChild.SetAttribute("easeMO", ((int) this.flash.AnimationElementsMouseOverSoft).ToString());
                newChild.SetAttribute("modeMO", ((int) this.flash.AnimationElementsMouseOverMode).ToString());
                newChild.SetAttribute("catMO", this.flash.AnimationElementsMouseOverValueChange.ToString());
                newChild.SetAttribute("durataMO", this.flash.AnimationElementsMouseOverDuration.ToString());
            }
            this.xmlRad.AppendChild(newChild);
            if ((this.currentPhase == Phase.Elements) && (this.flash.AnimationElementsStartupOrder == AnimationOrder.OneByOne))
            {
                this.currentTime += this.flash.AnimationElementsStartupDuration;
            }
            if ((this.currentPhase == Phase.Key) && (this.flash.AnimationKeyStartupOrder == AnimationOrder.OneByOne))
            {
                this.currentTime += this.flash.AnimationKeyStartupDuration;
            }
        }

        internal void AddUserBar(int height, int width, float x, float y, string color, int alfa, string src, int edgeWidth, string url)
        {
        }

        internal void EndMask()
        {
            XmlElement newChild = this.xmlChart.CreateElement("endmask");
            this.xmlRad.AppendChild(newChild);
        }

        internal void SaveChart(string s)
        {
            this.xmlChart.Save(s);
        }

        public Phase CurrentPhase
        {
            get
            {
                return this.currentPhase;
            }
            set
            {
                if (this.currentPhase >= value)
                {
                    throw new Exception("Invalid Phase order in xmlChart");
                }
                if (this.currentPhase == Phase.Title)
                {
                    this.CurrentTime += this.flash.AnimationTitleStartupDuration;
                }
                if ((this.currentPhase == Phase.Key) && (this.flash.AnimationKeyStartupOrder == AnimationOrder.All))
                {
                    this.CurrentTime += this.flash.AnimationKeyStartupDuration;
                }
                if (this.currentPhase == Phase.Background)
                {
                    this.CurrentTime += this.flash.AnimationBackgroundStartupDuration;
                }
                this.currentPhase = value;
            }
        }

        private int CurrentTime
        {
            get
            {
                return this.currentTime;
            }
            set
            {
                this.currentTime = value;
            }
        }

        public int OffsetX
        {
            get
            {
                return this.offsetX;
            }
            set
            {
                this.offsetX = value;
            }
        }

        public int OffsetY
        {
            get
            {
                return this.offsetY;
            }
            set
            {
                this.offsetY = value;
            }
        }
    }
}

