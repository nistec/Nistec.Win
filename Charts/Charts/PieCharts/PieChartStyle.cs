using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Nistec.Charts
{
    public partial class McPieChart : Control
  {
    /// <summary>
    /// Represents the possible text display modes for the PieChart.
    /// </summary>
    public enum TextDisplayTypes
    {
      Always,
      FitOnly,
      Never,
    }

    /// <summary>
    /// Represents the possible styles corresponding to a PieChart.
    /// </summary>
    public class PieChartStyle
    {
      #region Constructor
      /// <summary>
      /// Constructs a new instance of PieChartItemStyle.
      /// </summary>
      /// <param name="container">The control that contains the style.</param>
        internal PieChartStyle(McPieChart container)
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
      /// Whether or not the pie should be sized to automatically fit the control.
      /// </summary>
      private bool autoSize = false;

      /// <summary>
      /// The radius of the pie.
      /// </summary>
      private float radius = 200F;

      /// <summary>
      /// The depth of the pie.
      /// </summary>
      private float depth = 10F;

      /// <summary>
      /// The rotation of the pie.
      /// </summary>
      private float rotation = 0F;

      /// <summary>
      /// The leaning at which the pie is viewed.
      /// </summary>
      private float leaning = (float)(Math.PI / 6);

      /// <summary>
      /// Whether or not edges should be drawn.
      /// </summary>
      private bool showEdges = true;

      /// <summary>
      /// The text display mode of the control.
      /// </summary>
      private TextDisplayTypes textDisplayMode = TextDisplayTypes.FitOnly;

      /// <summary>
      /// Whether or not tool tips should be shown on the control.
      /// </summary>
      private bool showToolTips = true;

      /// <summary>
      /// The minimum radius when the control is auto-sized.
      /// </summary>
      internal const float AutoSizeMinimumRadius = 10F;

      ///// <summary>
      ///// internal margin
      ///// </summary>
      //  private Padding margin = new Padding(0);
 
      #endregion

      #region Properties
      /// <summary>
      /// Gets or sets if the pie should be sized to fit the control.  If this property is true,
      /// the Radius property is ignored.
      /// </summary>
      public bool AutoSizePie
      {
        get
        {
          return autoSize;
        }
        set
        {
          if (autoSize != value)
          {
            autoSize = value;
            container.MarkStructuralChange();
            container.FireAutoSizePieChanged();
          }
        }
      }

      /// <summary>
      /// Gets or sets radius of the control, in pixels.  If AutoSizePie is set to true, this value will be ignored.
      /// </summary>
      public float Radius
      {
        get
        {
          return radius;
        }
        set
        {
          if (!AutoSizePie)
          {
            RadiusInternal = value;
          }
        }
      }

      /// <summary>
      /// Gets or sets the radius of the control, ignoring the AutoSizePie property.
      /// </summary>
      internal float RadiusInternal
      {
        get
        {
          return radius;
        }
        set
        {
          if (radius != value)
          {
            if (value <= 0)
              throw new ArgumentOutOfRangeException("Radius", value, "Radius must be a positive value.");

            radius = value;
            container.MarkStructuralChange();
            container.FireRadiusChanged();
          }
        }
      }

      /// <summary>
      /// Gets or sets depth of the pie, in pixels.
      /// </summary>
      /// <remarks>This represents the three-dimensional depth of the control.
      /// The actual visual depth of the control depends on the leaning.  To determine what the apparent
      /// depth of the control is, use the Style.VisualHeight property.  The depth must be greater than or equal to 0.</remarks>
      public float Depth
      {
        get
        {
          return depth;
        }
        set
        {
          if (depth != value)
          {
            if (value < 0)
              throw new ArgumentOutOfRangeException("Depth", value, "Depth must be greater than or equal to 0.");

            depth = value;
            container.MarkStructuralChange();
            container.FireDepthChanged();
          }
        }
      }

      /// <summary>
      /// Gets the visual depth of the pie, after the leaning has been taken into account.
      /// </summary>
      public float VisualDepth
      {
        get
        {
          return (float)(Depth * Math.Cos(Leaning));
        }
      }

      /// <summary>
      /// Gets or sets the rotation of the pie chart.  This is represented in radians, with positive values indicating
      /// a rotation in the clockwise direction.
      /// </summary>
      public float Rotation
      {
        get
        {
          return rotation;
        }
        set
        {
          if (rotation != value)
          {
            rotation = (float)(value % (Math.PI * 2));
            if (rotation < 0)
            {
              rotation += (float)(Math.PI * 2);
            }

            container.MarkStructuralChange();
            container.FireRotationChanged();
          }
        }
      }

      /// <summary>
      /// Gets or sets the leaning of the control.  This is represented in radians, where an angle of 0
      /// represents looking at the edge of the control and an angle of pi represents looking
      /// straight down at the top of the pie.
      /// </summary>
      /// <remarks>
      /// The angle must be greater than 0 and less than or equal to pi radians.
      /// </remarks>
      public float Leaning
      {
        get
        {
          return leaning;
        }
        set
        {
          if (leaning != value)
          {
            if (value <= 0 || value > Math.PI / 2)
              throw new ArgumentOutOfRangeException("Leaning", value, "The leaning must be a radian angle greater than 0 and less than or equal to PI / 2.");

            leaning = value;
            container.MarkStructuralChange();
            container.FireLeaningChanged();
          }
        }
      }

      /// <summary>
      /// Gets or sets if edges should be drawn on pie slices.  If false, edges are not drawn.
      /// </summary>
      public bool ShowEdges
      {
        get
        {
          return showEdges;
        }
        set
        {
          if (showEdges != value)
          {
            showEdges = value;
            container.MarkVisualChange();
            container.FireShowEdgesChanged();
          }
        }
      }

      /// <summary>
      /// Gets or sets if text should be drawn on pie slices.
      /// </summary>
      /// <remarks>
      /// This can have one of three values.  If TextDisplayTypes.Always, the text is always drawn.
      /// If TextDisplayTypes.FitOnly, the text is drawn only if it fits in the wedge.  If TextDisplayTypes.Never,
      /// the text is never drawn.
      /// </remarks>
      public TextDisplayTypes TextDisplayMode
      {
        get
        {
          return textDisplayMode;
        }
        set
        {
          if (textDisplayMode != value)
          {
            textDisplayMode = value;
            container.MarkVisualChange();
            container.FireTextDisplayModeChanged();
          }
        }
      }

      /// <summary>
      /// Gets or sets if tool tips should be shown when the mouse hovers over pie slices.  If false, tool tips are not shown.
      /// </summary>
      public bool ShowToolTips
      {
        get
        {
          return showToolTips;
        }
        set
        {
          if (showToolTips != value)
          {
            showToolTips = value;
            container.FireShowToolTipsChanged();
          }
        }
      }

      /// <summary>
      /// Represents the brightness factor used in determining shadow colors.
      /// </summary>
      internal float ShadowBrightnessFactor
      {
        get
        {
          return -0.3F;
        }
      }

      /// <summary>
      /// Gets the width of the major axis of the ellipse.  This is half the total
      /// width of the ellipse.
      /// </summary>
      internal float EllipseWidth
      {
        get
        {
          return Radius;
        }
      }

      /// <summary>
      /// Gets the height of the minor axis of the ellipse.  This is half the total
      /// height of the ellipse.
      /// </summary>
      internal float EllipseHeight
      {
        get
        {
          return Radius * HeightWidthRatio;
        }
      }

      /// <summary>
      /// Gets the height-width ratio for the ellipse.
      /// </summary>
      internal float HeightWidthRatio
      {
        get
        {
          return (float)Math.Sin(Leaning);
        }
      }

      // /// <summary>
      ///// Gets or sets the internal Margin of Pie chart.
      ///// </summary>
      // public Padding  Margin
      //  {
      //      get
      //      {
      //          return margin;
      //      }
      //      set
      //      {
      //          margin = value;
      //      }
      //  }
      #endregion
    }
  }
}