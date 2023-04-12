using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Nistec.Charts
{
  public partial class McPieChart
  {
    /// <summary>
    /// Represents the possible styles corresponding to a PieChartItem.
    /// </summary>
    public class PieChartItemStyle
    {
      #region Constructor
      /// <summary>
      /// Constructs a new instance of PieChartItemStyle.
      /// </summary>
      /// <param name="container">The control that contains the style.</param>
        internal PieChartItemStyle(McPieChart container)
      {
        this.container = container;
      }
      #endregion

      #region Fields
      /// <summary>
      /// The control that contains the style.
      /// </summary>
        private McPieChart container;

      /// <summary>
      /// The factor by which edge brightness will be affected.
      /// </summary>
      private float edgeBrightness = -0.3F;

      /// <summary>
      /// The surface alpha transparency factor.
      /// </summary>
      private float surfaceTransparency = 1F;

      /// <summary>
      /// The factor by which surface brightness will be affected.
      /// </summary>
      private float surfaceBrightness = 0F;
      #endregion

      #region Properties
      /// <summary>
      /// Gets or sets the surface alpha transparency factor.
      /// </summary>
      /// <remarks>
      /// This value must be between 0 and 1, and represents the multiplier that is applied to the 
      /// alpha value of the color for pie slices that use this style.
      /// </remarks>
      public float SurfaceTransparency
      {
        get
        {
          return surfaceTransparency;
        }
        set
        {
          if (surfaceTransparency != value)
          {
            if (value < 0 || value > 1)
              throw new ArgumentOutOfRangeException("SurfaceAlphaTransparenty", value, "The SurfaceTransparency must be between 0 and 1 inclusive.");

            surfaceTransparency = value;
            container.MarkVisualChange(true);
          }
        }
      }

      /// <summary>
      /// Gets or sets the factor by which edge brightness will be affected.
      /// </summary>
      /// <remarks>See <see cref="PieChart.DrawingMetrics.ChangeColorBrightness"/> for more information about brighness modification.</remarks>
      public float EdgeBrightness
      {
        get
        {
          return edgeBrightness;
        }
        set
        {
          if (edgeBrightness != value)
          {
            if (value < -1 || value > 1)
              throw new ArgumentOutOfRangeException("EdgeBrightness", value, "The EdgeBrightness must be between -1 and 1 inclusive.");

            edgeBrightness = value;
            container.MarkVisualChange(true);
          }
        }
      }

      /// <summary>
      /// Gets or sets the factor by which surface brightness will be affected.
      /// </summary>
      /// <remarks>See <see cref="PieChart.DrawingMetrics.ChangeColorBrightness"/> for more information about brighness modification.</remarks>
      public float SurfaceBrightness
      {
        get
        {
          return surfaceBrightness;
        }
        set
        {
          if (surfaceBrightness != value)
          {
            if (value < -1 || value > 1)
              throw new ArgumentOutOfRangeException("SurfaceBrightness", value, "The SurfaceBrightness must be between -1 and 1 inclusive.");

            surfaceBrightness = value;
            container.MarkVisualChange(true);
          }
        }
      }
      #endregion
    }
  }
}
