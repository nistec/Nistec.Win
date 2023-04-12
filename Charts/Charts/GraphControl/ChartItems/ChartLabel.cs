namespace Nistec.Charts
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    internal class ChartLabel
    {
        private bool Draw = true;
        private bool Drawn;
        private SizeF size;
        private string text;
        private int x;
        private int y;

        public ChartLabel(string text, float x, float y, SizeF size)
        {
            this.x = Convert.ToInt32(x);
            this.y = Convert.ToInt32(y);
            this.size = size;
            this.text = text;
        }

        public static void DrawList(List<ChartLabel> list, Graphics g, Font f, Brush br, int width, int height, int offsetX, int offsetY)
        {
            foreach (ChartLabel label in list)
            {
                if (label.Draw && !label.Drawn)
                {
                    g.DrawString(label.text, f, br, (float) label.x, (float) label.y);
                    label.Drawn = true;
                }
            }
        }

        private static void HideUnwantedLabels(List<ChartLabel> list, int width, int height, int offsetX, int offsetY)
        {
            foreach (ChartLabel label in list)
            {
                if (!label.IsInBoundries(width, height, offsetX, offsetY))
                {
                    label.Draw = false;
                }
            }
            int count = list.Count;
            for (int i = count - 1; i >= 0; i--)
            {
                if (list[i].Draw && !list[i].Drawn)
                {
                    for (int k = i + 1; k < count; k++)
                    {
                        if ((list[k].Draw && list[i].Draw) && list[i].Ovelaps(list[k]))
                        {
                            list[i].Draw = false;
                        }
                    }
                }
            }
            for (int j = 0; j < count; j++)
            {
                if (list[j].Draw)
                {
                    for (int m = 0; m < j; m++)
                    {
                        if (list[m].Drawn && list[j].Ovelaps(list[m]))
                        {
                            list[j].Draw = false;
                        }
                    }
                }
            }
        }

        private bool IsInBoundries(int width, int height, int offsetX, int offsetY)
        {
            if (this.x < offsetX)
            {
                return false;
            }
            if (this.y < offsetY)
            {
                return false;
            }
            if ((this.x + this.size.Width) > (width + offsetX))
            {
                return false;
            }
            if ((this.y + this.size.Height) > (height + offsetY))
            {
                return false;
            }
            return true;
        }

        private bool Ovelaps(ChartLabel l)
        {
            ChartLabel label = this;
            ChartLabel label2 = l;
            return (Overlaps(label.x, label2.x, label.size.Width, label2.size.Width) && Overlaps(label.y, label2.y, label.size.Height, label2.size.Height));
        }

        private static bool Overlaps(int start1, int start2, float dim1, float dim2)
        {
            return (((start1 <= start2) && ((start1 + dim1) >= start2)) || ((start2 <= start1) && ((start2 + dim2) >= start1)));
        }

        public SizeF Size
        {
            get
            {
                return this.size;
            }
            set
            {
                this.size = value;
            }
        }

        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
            }
        }

        public int X
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
            }
        }

        public int Y
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;
            }
        }
    }
}

