namespace MControl.GridView
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    internal class GridUtilities
    {
        private const byte GRIDROWHEADERCELL_contentMarginHeight = 3;
        private const byte GRIDROWHEADERCELL_contentMarginWidth = 3;
        private const byte GRIDROWHEADERCELL_horizontalTextMarginLeft = 1;
        private const byte GRIDROWHEADERCELL_horizontalTextMarginRight = 2;
        private const byte GRIDROWHEADERCELL_iconMarginHeight = 2;
        private const byte GRIDROWHEADERCELL_iconMarginWidth = 3;
        private const byte GRIDROWHEADERCELL_iconsHeight = 11;
        private const byte GRIDROWHEADERCELL_iconsWidth = 12;
        private const byte GRIDROWHEADERCELL_verticalTextMargin = 1;

        internal static ContentAlignment ComputeDrawingContentAlignmentForCellStyleAlignment(GridContentAlignment alignment)
        {
            switch (alignment)
            {
                case GridContentAlignment.TopLeft:
                    return ContentAlignment.TopLeft;

                case GridContentAlignment.TopCenter:
                    return ContentAlignment.TopCenter;

                case GridContentAlignment.TopRight:
                    return ContentAlignment.TopRight;

                case GridContentAlignment.MiddleLeft:
                    return ContentAlignment.MiddleLeft;

                case GridContentAlignment.MiddleCenter:
                    return ContentAlignment.MiddleCenter;

                case GridContentAlignment.MiddleRight:
                    return ContentAlignment.MiddleRight;

                case GridContentAlignment.BottomLeft:
                    return ContentAlignment.BottomLeft;

                case GridContentAlignment.BottomCenter:
                    return ContentAlignment.BottomCenter;

                case GridContentAlignment.BottomRight:
                    return ContentAlignment.BottomRight;
            }
            return ContentAlignment.MiddleCenter;
        }

        internal static TextFormatFlags ComputeTextFormatFlagsForCellStyleAlignment(bool rightToLeft, GridContentAlignment alignment, GridTriState wrapMode)
        {
            TextFormatFlags glyphOverhangPadding;
            switch (alignment)
            {
                case GridContentAlignment.TopLeft:
                    glyphOverhangPadding = TextFormatFlags.GlyphOverhangPadding;
                    if (!rightToLeft)
                    {
                        glyphOverhangPadding = glyphOverhangPadding;
                    }
                    else
                    {
                        glyphOverhangPadding |= TextFormatFlags.Right;
                    }
                    break;

                case GridContentAlignment.TopCenter:
                    glyphOverhangPadding = TextFormatFlags.HorizontalCenter;
                    break;

                case GridContentAlignment.TopRight:
                    glyphOverhangPadding = TextFormatFlags.GlyphOverhangPadding;
                    if (!rightToLeft)
                    {
                        glyphOverhangPadding |= TextFormatFlags.Right;
                    }
                    else
                    {
                        glyphOverhangPadding = glyphOverhangPadding;
                    }
                    break;

                case GridContentAlignment.MiddleLeft:
                    glyphOverhangPadding = TextFormatFlags.VerticalCenter;
                    if (rightToLeft)
                    {
                        glyphOverhangPadding |= TextFormatFlags.Right;
                    }
                    else
                    {
                        glyphOverhangPadding = glyphOverhangPadding;
                    }
                    break;

                case GridContentAlignment.MiddleCenter:
                    glyphOverhangPadding = TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter;
                    break;

                case GridContentAlignment.BottomCenter:
                    glyphOverhangPadding = TextFormatFlags.Bottom | TextFormatFlags.HorizontalCenter;
                    break;

                case GridContentAlignment.BottomRight:
                    glyphOverhangPadding = TextFormatFlags.Bottom;
                    if (rightToLeft)
                    {
                        glyphOverhangPadding = glyphOverhangPadding;
                    }
                    else
                    {
                        glyphOverhangPadding |= TextFormatFlags.Right;
                    }
                    break;

                case GridContentAlignment.MiddleRight:
                    glyphOverhangPadding = TextFormatFlags.VerticalCenter;
                    if (rightToLeft)
                    {
                        glyphOverhangPadding = glyphOverhangPadding;
                    }
                    else
                    {
                        glyphOverhangPadding |= TextFormatFlags.Right;
                    }
                    break;

                case GridContentAlignment.BottomLeft:
                    glyphOverhangPadding = TextFormatFlags.Bottom;
                    if (rightToLeft)
                    {
                        glyphOverhangPadding |= TextFormatFlags.Right;
                    }
                    else
                    {
                        glyphOverhangPadding = glyphOverhangPadding;
                    }
                    break;

                default:
                    glyphOverhangPadding = TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter;
                    break;
            }
            if (wrapMode == GridTriState.False)
            {
                glyphOverhangPadding |= TextFormatFlags.SingleLine;
            }
            else
            {
                glyphOverhangPadding |= TextFormatFlags.WordBreak;
            }
            glyphOverhangPadding |= TextFormatFlags.NoPrefix;
            glyphOverhangPadding |= TextFormatFlags.PreserveGraphicsClipping;
            if (rightToLeft)
            {
                glyphOverhangPadding |= TextFormatFlags.RightToLeft;
            }
            return glyphOverhangPadding;
        }

        internal static Size GetPreferredRowHeaderSize(Graphics graphics, string val, GridCellStyle cellStyle, int borderAndPaddingWidths, int borderAndPaddingHeights, bool showRowErrors, bool showGlyph, Size constraintSize, TextFormatFlags flags)
        {
            int width;
            int num2;
            switch (GridCell.GetFreeDimensionFromConstraint(constraintSize))
            {
                case GridFreeDimension.Height:
                {
                    int num4 = 1;
                    int height = 1;
                    int maxWidth = constraintSize.Width - borderAndPaddingWidths;
                    if (string.IsNullOrEmpty(val))
                    {
                        if ((showGlyph || showRowErrors) && (maxWidth >= 0x12))
                        {
                            num4 = 15;
                        }
                    }
                    else
                    {
                        if (showGlyph && (maxWidth >= 0x12))
                        {
                            num4 = 15;
                            maxWidth -= 0x12;
                        }
                        if (showRowErrors && (maxWidth >= 0x12))
                        {
                            num4 = 15;
                            maxWidth -= 0x12;
                        }
                        if (maxWidth > 9)
                        {
                            maxWidth -= 9;
                            if (cellStyle.WrapMode == GridTriState.True)
                            {
                                height = GridCell.MeasureTextHeight(graphics, val, cellStyle.Font, maxWidth, flags);
                            }
                            else
                            {
                                height = GridCell.MeasureTextSize(graphics, val, cellStyle.Font, flags).Height;
                            }
                            height += 2;
                        }
                    }
                    return new Size(0, Math.Max(num4, height) + borderAndPaddingHeights);
                }
                case GridFreeDimension.Width:
                {
                    width = 0;
                    num2 = constraintSize.Height - borderAndPaddingHeights;
                    if (string.IsNullOrEmpty(val))
                    {
                        goto Label_007B;
                    }
                    int maxHeight = num2 - 2;
                    if (maxHeight <= 0)
                    {
                        goto Label_007B;
                    }
                    if (cellStyle.WrapMode != GridTriState.True)
                    {
                        width = GridCell.MeasureTextSize(graphics, val, cellStyle.Font, flags).Width;
                        break;
                    }
                    width = GridCell.MeasureTextWidth(graphics, val, cellStyle.Font, maxHeight, flags);
                    break;
                }
                default:
                    Size size;
                    if (!string.IsNullOrEmpty(val))
                    {
                        if (cellStyle.WrapMode == GridTriState.True)
                        {
                            size = GridCell.MeasureTextPreferredSize(graphics, val, cellStyle.Font, 5f, flags);
                        }
                        else
                        {
                            size = GridCell.MeasureTextSize(graphics, val, cellStyle.Font, flags);
                        }
                        size.Width += 9;
                        size.Height += 2;
                    }
                    else
                    {
                        size = new Size(0, 1);
                    }
                    if (showGlyph)
                    {
                        size.Width += 0x12;
                    }
                    if (showRowErrors)
                    {
                        size.Width += 0x12;
                    }
                    if (showGlyph || showRowErrors)
                    {
                        size.Height = Math.Max(size.Height, 15);
                    }
                    size.Width += borderAndPaddingWidths;
                    size.Height += borderAndPaddingHeights;
                    return size;
            }
            width += 9;
        Label_007B:
            if (num2 >= 15)
            {
                if (showGlyph)
                {
                    width += 0x12;
                }
                if (showRowErrors)
                {
                    width += 0x12;
                }
            }
            return new Size(Math.Max(width, 1) + borderAndPaddingWidths, 0);
        }

        internal static Rectangle GetTextBounds(Rectangle cellBounds, string text, TextFormatFlags flags, GridCellStyle cellStyle)
        {
            return GetTextBounds(cellBounds, text, flags, cellStyle, cellStyle.Font);
        }

        internal static Rectangle GetTextBounds(Rectangle cellBounds, string text, TextFormatFlags flags, GridCellStyle cellStyle, Font font)
        {
            if (((flags & TextFormatFlags.SingleLine) != TextFormatFlags.GlyphOverhangPadding) && (TextRenderer.MeasureText(text, font, new Size(0x7fffffff, 0x7fffffff), flags).Width > cellBounds.Width))
            {
                flags |= TextFormatFlags.EndEllipsis;
            }
            Size proposedSize = new Size(cellBounds.Width, cellBounds.Height);
            Size size = TextRenderer.MeasureText(text, font, proposedSize, flags);
            if (size.Width > proposedSize.Width)
            {
                size.Width = proposedSize.Width;
            }
            if (size.Height > proposedSize.Height)
            {
                size.Height = proposedSize.Height;
            }
            if (size == proposedSize)
            {
                return cellBounds;
            }
            return new Rectangle(GetTextLocation(cellBounds, size, flags, cellStyle), size);
        }

        internal static Point GetTextLocation(Rectangle cellBounds, Size sizeText, TextFormatFlags flags, GridCellStyle cellStyle)
        {
            Point point = new Point(0, 0);
            GridContentAlignment middleLeft = cellStyle.Alignment;
            if ((flags & TextFormatFlags.RightToLeft) != TextFormatFlags.GlyphOverhangPadding)
            {
                switch (middleLeft)
                {
                    case GridContentAlignment.MiddleRight:
                        middleLeft = GridContentAlignment.MiddleLeft;
                        break;

                    case GridContentAlignment.BottomLeft:
                        middleLeft = GridContentAlignment.BottomRight;
                        break;

                    case GridContentAlignment.BottomRight:
                        middleLeft = GridContentAlignment.BottomLeft;
                        break;

                    case GridContentAlignment.TopLeft:
                        middleLeft = GridContentAlignment.TopRight;
                        break;

                    case GridContentAlignment.TopRight:
                        middleLeft = GridContentAlignment.TopLeft;
                        break;

                    case GridContentAlignment.MiddleLeft:
                        middleLeft = GridContentAlignment.MiddleRight;
                        break;
                }
            }
            GridContentAlignment alignment3 = middleLeft;
            if (alignment3 <= GridContentAlignment.MiddleCenter)
            {
                switch (alignment3)
                {
                    case GridContentAlignment.TopLeft:
                        point.X = cellBounds.X;
                        point.Y = cellBounds.Y;
                        return point;

                    case GridContentAlignment.TopCenter:
                        point.X = cellBounds.X + ((cellBounds.Width - sizeText.Width) / 2);
                        point.Y = cellBounds.Y;
                        return point;

                    case (GridContentAlignment.TopCenter | GridContentAlignment.TopLeft):
                        return point;

                    case GridContentAlignment.TopRight:
                        point.X = cellBounds.Right - sizeText.Width;
                        point.Y = cellBounds.Y;
                        return point;

                    case GridContentAlignment.MiddleLeft:
                        point.X = cellBounds.X;
                        point.Y = cellBounds.Y + ((cellBounds.Height - sizeText.Height) / 2);
                        return point;

                    case GridContentAlignment.MiddleCenter:
                        point.X = cellBounds.X + ((cellBounds.Width - sizeText.Width) / 2);
                        point.Y = cellBounds.Y + ((cellBounds.Height - sizeText.Height) / 2);
                        return point;
                }
                return point;
            }
            if (alignment3 <= GridContentAlignment.BottomLeft)
            {
                switch (alignment3)
                {
                    case GridContentAlignment.MiddleRight:
                        point.X = cellBounds.Right - sizeText.Width;
                        point.Y = cellBounds.Y + ((cellBounds.Height - sizeText.Height) / 2);
                        return point;

                    case GridContentAlignment.BottomLeft:
                        point.X = cellBounds.X;
                        point.Y = cellBounds.Bottom - sizeText.Height;
                        return point;
                }
                return point;
            }
            switch (alignment3)
            {
                case GridContentAlignment.BottomCenter:
                    point.X = cellBounds.X + ((cellBounds.Width - sizeText.Width) / 2);
                    point.Y = cellBounds.Bottom - sizeText.Height;
                    return point;

                case GridContentAlignment.BottomRight:
                    point.X = cellBounds.Right - sizeText.Width;
                    point.Y = cellBounds.Bottom - sizeText.Height;
                    return point;
            }
            return point;
        }

        internal static bool ValidTextFormatFlags(TextFormatFlags flags)
        {
            return ((flags & ~(TextFormatFlags.PreserveGraphicsTranslateTransform | TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.PrefixOnly | TextFormatFlags.HidePrefix | TextFormatFlags.NoFullWidthCharacterBreak | TextFormatFlags.WordEllipsis | TextFormatFlags.RightToLeft | TextFormatFlags.ModifyString | TextFormatFlags.EndEllipsis | TextFormatFlags.PathEllipsis | TextFormatFlags.TextBoxControl | TextFormatFlags.Internal | TextFormatFlags.NoPrefix | TextFormatFlags.ExternalLeading | TextFormatFlags.NoClipping | TextFormatFlags.ExpandTabs | TextFormatFlags.SingleLine | TextFormatFlags.WordBreak | TextFormatFlags.Bottom | TextFormatFlags.VerticalCenter | TextFormatFlags.Right | TextFormatFlags.HorizontalCenter)) == TextFormatFlags.GlyphOverhangPadding);
        }
    }
}

