namespace Nistec.Charts
{
    using System;
    using System.Drawing;

    [Serializable]
    public class BackgroundStripe
    {
        internal Color BrushColor;
        internal decimal MaxValue;
        internal decimal MinValue;

        public BackgroundStripe(decimal MinValue, decimal MaxValue, Color BrushColor)
        {
            if (MaxValue < MinValue)
            {
                decimal num = MaxValue;
                MaxValue = MinValue;
                MinValue = num;
            }
            this.MinValue = MinValue;
            this.MaxValue = MaxValue;
            this.BrushColor = BrushColor;
        }
    }
}

