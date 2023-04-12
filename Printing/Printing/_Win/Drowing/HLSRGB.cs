namespace MControl.Drawing
{
    using System;
    using System.Drawing;

    public class HLSRGB
    {
        private byte blue;
        private byte green;
        private float hue;
        private float luminance;
        private byte red;
        private float saturation;

        public HLSRGB()
        {
        }

        public HLSRGB(HLSRGB hlsrgb)
        {
            this.red = hlsrgb.Red;
            this.blue = hlsrgb.Blue;
            this.green = hlsrgb.Green;
            this.luminance = hlsrgb.Luminance;
            this.hue = hlsrgb.Hue;
            this.saturation = hlsrgb.Saturation;
        }

        public HLSRGB(System.Drawing.Color c)
        {
            this.red = c.R;
            this.green = c.G;
            this.blue = c.B;
            this.ToHLS();
        }

        public HLSRGB(byte red, byte green, byte blue)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
            this.ToHLS();
        }

        public HLSRGB(float hue, float luminance, float saturation)
        {
            if ((saturation < 0f) || (saturation > 1f))
            {
                throw new ArgumentOutOfRangeException("Saturation", "Saturation must be between 0.0 and 1.0");
            }
            if ((hue < 0f) || (hue > 360f))
            {
                throw new ArgumentOutOfRangeException("Hue", "Hue must be between 0.0 and 360.0");
            }
            if ((luminance < 0f) || (luminance > 1f))
            {
                throw new ArgumentOutOfRangeException("Luminance", "Luminance must be between 0.0 and 1.0");
            }
            this.hue = hue;
            this.luminance = luminance;
            this.saturation = saturation;
            this.ToRGB();
        }

        public void DarkenColor(float darkenBy)
        {
            this.luminance *= darkenBy;
            this.ToRGB();
        }

        public void LightenColor(float lightenBy)
        {
            this.luminance *= 1f + lightenBy;
            if (this.luminance > 1f)
            {
                this.luminance = 1f;
            }
            this.ToRGB();
        }

        private void ToHLS()
        {
            byte num = Math.Min(this.red, Math.Min(this.green, this.blue));
            byte num2 = Math.Max(this.red, Math.Max(this.green, this.blue));
            float num3 = num2 - num;
            float num4 = num2 + num;
            this.luminance = num4 / 510f;
            if (num2 == num)
            {
                this.saturation = 0f;
                this.hue = 0f;
            }
            else
            {
                float num5 = ((float) (num2 - this.red)) / num3;
                float num6 = ((float) (num2 - this.green)) / num3;
                float num7 = ((float) (num2 - this.blue)) / num3;
                this.saturation = (this.luminance <= 0.5f) ? (num3 / num4) : (num3 / (510f - num4));
                if (this.red == num2)
                {
                    this.hue = 60f * ((6f + num7) - num6);
                }
                if (this.green == num2)
                {
                    this.hue = 60f * ((2f + num5) - num7);
                }
                if (this.blue == num2)
                {
                    this.hue = 60f * ((4f + num6) - num5);
                }
                if (this.hue > 360f)
                {
                    this.hue -= 360f;
                }
            }
        }

        private void ToRGB()
        {
            if (this.saturation == 0.0)
            {
                this.red = (byte) (this.luminance * 255f);
                this.green = this.red;
                this.blue = this.red;
            }
            else
            {
                float num2;
                if (this.luminance <= 0.5f)
                {
                    num2 = this.luminance + (this.luminance * this.saturation);
                }
                else
                {
                    num2 = (this.luminance + this.saturation) - (this.luminance * this.saturation);
                }
                float num = (2f * this.luminance) - num2;
                this.red = this.ToRGB1(num, num2, this.hue + 120f);
                this.green = this.ToRGB1(num, num2, this.hue);
                this.blue = this.ToRGB1(num, num2, this.hue - 120f);
            }
        }

        private byte ToRGB1(float rm1, float rm2, float rh)
        {
            if (rh > 360f)
            {
                rh -= 360f;
            }
            else if (rh < 0f)
            {
                rh += 360f;
            }
            if (rh < 60f)
            {
                rm1 += ((rm2 - rm1) * rh) / 60f;
            }
            else if (rh < 180f)
            {
                rm1 = rm2;
            }
            else if (rh < 240f)
            {
                rm1 += ((rm2 - rm1) * (240f - rh)) / 60f;
            }
            return (byte) (rm1 * 255f);
        }

        public byte Blue
        {
            get
            {
                return this.blue;
            }
            set
            {
                this.blue = value;
                this.ToHLS();
            }
        }

        public System.Drawing.Color Color
        {
            get
            {
                return System.Drawing.Color.FromArgb(this.red, this.green, this.blue);
            }
            set
            {
                this.red = value.R;
                this.green = value.G;
                this.blue = value.B;
                this.ToHLS();
            }
        }

        public byte Green
        {
            get
            {
                return this.green;
            }
            set
            {
                this.green = value;
                this.ToHLS();
            }
        }

        public float Hue
        {
            get
            {
                return this.hue;
            }
            set
            {
                if ((value < 0f) || (value > 360f))
                {
                    throw new ArgumentOutOfRangeException("Hue", "Hue must be between 0.0 and 360.0");
                }
                this.hue = value;
                this.ToRGB();
            }
        }

        public float Luminance
        {
            get
            {
                return this.luminance;
            }
            set
            {
                if ((value < 0f) || (value > 1f))
                {
                    throw new ArgumentOutOfRangeException("Luminance", "Luminance must be between 0.0 and 1.0");
                }
                this.luminance = value;
                this.ToRGB();
            }
        }

        public byte Red
        {
            get
            {
                return this.red;
            }
            set
            {
                this.red = value;
                this.ToHLS();
            }
        }

        public float Saturation
        {
            get
            {
                return this.saturation;
            }
            set
            {
                if ((value < 0f) || (value > 1f))
                {
                    throw new ArgumentOutOfRangeException("Saturation", "Saturation must be between 0.0 and 1.0");
                }
                this.saturation = value;
                this.ToRGB();
            }
        }
    }
}

