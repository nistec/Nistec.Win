namespace MControl.Charts
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    [TypeConverter(typeof(TextFormatConverter))]
    public class TextFormat
    {
        private Alignments m_Alignment = Alignments.Center;
        private System.Drawing.Color m_Color = System.Drawing.Color.Black;
        private System.Drawing.Font m_Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
        private bool m_Visible = true;

        internal event EventHandler TextFormatChanged;

        public TextFormat(Alignments Alignment)
        {
            this.m_Alignment = Alignment;
        }

        [DefaultValue(Alignments.Center)]
        public Alignments Alignment
        {
            get
            {
                return this.m_Alignment;
            }
            set
            {
                this.m_Alignment = value;
                if (this.TextFormatChanged != null)
                {
                    this.TextFormatChanged(this, new EventArgs());
                }
            }
        }
        [DefaultValue(typeof(System.Drawing.Color),"Black")]
        public System.Drawing.Color Color
        {
            get
            {
                return this.m_Color;
            }
            set
            {
                this.m_Color = value;
                if (this.TextFormatChanged != null)
                {
                    this.TextFormatChanged(this, new EventArgs());
                }
            }
        }

        [DefaultValue(typeof(System.Drawing.Font), "Microsoft Sans Serif")]
        public System.Drawing.Font Font
        {
            get
            {
                return this.m_Font;
            }
            set
            {
                this.m_Font = value;
                if (this.TextFormatChanged != null)
                {
                    this.TextFormatChanged(this, new EventArgs());
                }
            }
        }
        [DefaultValue(true)]
        public bool Visible
        {
            get
            {
                return this.m_Visible;
            }
            set
            {
                this.m_Visible = value;
                if (this.TextFormatChanged != null)
                {
                    this.TextFormatChanged(this, new EventArgs());
                }
            }
        }
    }
}

