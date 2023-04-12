namespace MControl.Charts
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    [TypeConverter(typeof(SpacingsConverter))]
    public class Spacings
    {
        private float m_Bottom = 5f;
        private float m_ColumnsAndBars = 1f;
        private float m_Left = 5f;
        private float m_Right = 5f;
        private float m_Top = 5f;

        internal event EventHandler SpacingChanged;

        public Spacings(float top, float bottom, float left, float right, float columnsandbars)
        {
            this.m_Top = top;
            this.m_Bottom = bottom;
            this.m_Left = left;
            this.m_Right = right;
            this.m_ColumnsAndBars = columnsandbars;
        }

        public float Bottom
        {
            get
            {
                return this.m_Bottom;
            }
            set
            {
                if (value >= 0f)
                {
                    this.m_Bottom = value;
                }
                else
                {
                    this.m_Bottom = 0f;
                }
                if (this.SpacingChanged != null)
                {
                    this.SpacingChanged(this, new EventArgs());
                }
            }
        }

        public float ColumnsAndBars
        {
            get
            {
                return this.m_ColumnsAndBars;
            }
            set
            {
                if (value >= 0f)
                {
                    this.m_ColumnsAndBars = value;
                }
                else
                {
                    this.m_ColumnsAndBars = 0f;
                }
                if (this.SpacingChanged != null)
                {
                    this.SpacingChanged(this, new EventArgs());
                }
            }
        }

        public float Left
        {
            get
            {
                return this.m_Left;
            }
            set
            {
                if (value >= 0f)
                {
                    this.m_Left = value;
                }
                else
                {
                    this.m_Left = 0f;
                }
                if (this.SpacingChanged != null)
                {
                    this.SpacingChanged(this, new EventArgs());
                }
            }
        }

        public float Right
        {
            get
            {
                return this.m_Right;
            }
            set
            {
                if (value >= 0f)
                {
                    this.m_Right = value;
                }
                else
                {
                    this.m_Right = 0f;
                }
                if (this.SpacingChanged != null)
                {
                    this.SpacingChanged(this, new EventArgs());
                }
            }
        }

        public float Top
        {
            get
            {
                return this.m_Top;
            }
            set
            {
                if (value >= 0f)
                {
                    this.m_Top = value;
                }
                else
                {
                    this.m_Top = 0f;
                }
                if (this.SpacingChanged != null)
                {
                    this.SpacingChanged(this, new EventArgs());
                }
            }
        }
    }
}

