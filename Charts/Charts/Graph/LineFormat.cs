namespace MControl.Charts
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    [TypeConverter(typeof(LineFormatConverter))]
    public class LineFormat
    {
        private System.Drawing.Color m_Color = System.Drawing.Color.Black;
        private int m_Thickness = 1;
        private bool m_Visible = true;

        internal event EventHandler LineChanged;

        public LineFormat(bool Visible, int Thickness, System.Drawing.Color Color)
        {
            this.m_Visible = Visible;
            this.m_Thickness = Thickness;
            this.m_Color = Color;
        }

        public System.Drawing.Color Color
        {
            get
            {
                return this.m_Color;
            }
            set
            {
                this.m_Color = value;
                if (this.LineChanged != null)
                {
                    this.LineChanged(this, new EventArgs());
                }
            }
        }

        public int Thickness
        {
            get
            {
                return this.m_Thickness;
            }
            set
            {
                if (value >= 1)
                {
                    this.m_Thickness = value;
                }
                else
                {
                    this.m_Thickness = 1;
                }
                if (this.LineChanged != null)
                {
                    this.LineChanged(this, new EventArgs());
                }
            }
        }

        public bool Visible
        {
            get
            {
                return this.m_Visible;
            }
            set
            {
                this.m_Visible = value;
                if (this.LineChanged != null)
                {
                    this.LineChanged(this, new EventArgs());
                }
            }
        }
    }
}

