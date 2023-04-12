namespace MControl.GridView
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Globalization;
    using System.Security.Permissions;
    using System.Windows.Forms;

    /// <summary>Displays a graphic in a <see cref="T:MControl.GridView.Grid"></see> control. </summary>
    /// <filterpriority>2</filterpriority>
    public class GridImageCell : GridCell
    {
        private static System.Type cellType = typeof(GridImageCell);
        private static ColorMap[] colorMap = new ColorMap[] { new ColorMap() };
        private const byte GRIDIMAGECELL_valueIsIcon = 1;
        private static System.Type defaultTypeIcon = typeof(Icon);
        private static System.Type defaultTypeImage = typeof(Image);
        private static Bitmap errorBmp = null;
        private static Icon errorIco = null;
        private byte flags;
        private static readonly int PropImageCellDescription = PropertyStore.CreateKey();
        private static readonly int PropImageCellLayout = PropertyStore.CreateKey();

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridImageCell"></see> class, configuring it for use with cell values other than <see cref="T:System.Drawing.Icon"></see> objects.</summary>
        public GridImageCell() : this(false)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridImageCell"></see> class, optionally configuring it for use with <see cref="T:System.Drawing.Icon"></see> cell values.</summary>
        /// <param name="valueIsIcon">The cell will display an <see cref="T:System.Drawing.Icon"></see> value.</param>
        public GridImageCell(bool valueIsIcon)
        {
            if (valueIsIcon)
            {
                this.flags = 1;
            }
        }

        /// <summary>Creates an exact copy of this cell.</summary>
        /// <returns>An <see cref="T:System.Object"></see> that represents the cloned <see cref="T:MControl.GridView.GridImageCell"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public override object Clone()
        {
            GridImageCell cell;
            System.Type type = base.GetType();
            if (type == cellType)
            {
                cell = new GridImageCell();
            }
            else
            {
                cell = (GridImageCell) Activator.CreateInstance(type);
            }
            base.CloneInternal(cell);
            cell.ValueIsIconInternal = this.ValueIsIcon;
            cell.Description = this.Description;
            cell.ImageLayoutInternal = this.ImageLayout;
            return cell;
        }

        /// <summary>Creates a new accessible object for the <see cref="T:MControl.GridView.GridImageCell"></see>. </summary>
        /// <returns>A new <see cref="T:MControl.GridView.GridImageCell.GridImageCellAccessibleObject"></see> for the <see cref="T:MControl.GridView.GridImageCell"></see>. </returns>
        protected override AccessibleObject CreateAccessibilityInstance()
        {
            return new GridImageCellAccessibleObject(this);
        }

        protected override Rectangle GetContentBounds(Graphics graphics, GridCellStyle cellStyle, int rowIndex)
        {
            GridAdvancedBorderStyle style;
            GridElementStates states;
            Rectangle rectangle;
            if (cellStyle == null)
            {
                throw new ArgumentNullException("cellStyle");
            }
            if (((base.Grid == null) || (rowIndex < 0)) || (base.OwningColumn == null))
            {
                return Rectangle.Empty;
            }
            object obj2 = this.GetValue(rowIndex);
            object formattedValue = this.GetFormattedValue(obj2, rowIndex, ref cellStyle, null, null, GridDataErrorContexts.Formatting);
            base.ComputeBorderStyleCellStateAndCellBounds(rowIndex, out style, out states, out rectangle);
            return this.PaintPrivate(graphics, rectangle, rectangle, rowIndex, states, formattedValue, null, cellStyle, style, GridPaintParts.ContentForeground, true, false, false);
        }

        protected override Rectangle GetErrorIconBounds(Graphics graphics, GridCellStyle cellStyle, int rowIndex)
        {
            GridAdvancedBorderStyle style;
            GridElementStates states;
            Rectangle rectangle;
            if (cellStyle == null)
            {
                throw new ArgumentNullException("cellStyle");
            }
            if (((base.Grid == null) || (rowIndex < 0)) || (((base.OwningColumn == null) || !base.Grid.ShowCellErrors) || string.IsNullOrEmpty(this.GetErrorText(rowIndex))))
            {
                return Rectangle.Empty;
            }
            object obj2 = this.GetValue(rowIndex);
            object formattedValue = this.GetFormattedValue(obj2, rowIndex, ref cellStyle, null, null, GridDataErrorContexts.Formatting);
            base.ComputeBorderStyleCellStateAndCellBounds(rowIndex, out style, out states, out rectangle);
            return this.PaintPrivate(graphics, rectangle, rectangle, rowIndex, states, formattedValue, this.GetErrorText(rowIndex), cellStyle, style, GridPaintParts.ContentForeground, false, true, false);
        }

        /// <summary>Returns a graphic as it would be displayed in the cell.</summary>
        /// <returns>An object that represents the formatted image.</returns>
        /// <param name="cellStyle">The <see cref="T:MControl.GridView.GridCellStyle"></see> in effect for the cell. </param>
        /// <param name="context">A bitwise combination of <see cref="T:MControl.GridView.GridDataErrorContexts"></see> values describing the context in which the formatted value is needed. </param>
        /// <param name="valueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter"></see> associated with the value type that provides custom conversion to the formatted value type, or null if no such custom conversion is needed.</param>
        /// <param name="rowIndex">The index of the cell's parent row. </param>
        /// <param name="value">The value to be formatted. </param>
        /// <param name="formattedValueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter"></see> associated with the formatted value type that provides custom conversion from the value type, or null if no such custom conversion is needed.</param>
        protected override object GetFormattedValue(object value, int rowIndex, ref GridCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, GridDataErrorContexts context)
        {
            if ((context & GridDataErrorContexts.ClipboardContent) != 0)
            {
                return this.Description;
            }
            object obj2 = base.GetFormattedValue(value, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
            if ((obj2 == null) && (cellStyle.NullValue == null))
            {
                return null;
            }
            if (this.ValueIsIcon)
            {
                Icon errorIcon = obj2 as Icon;
                if (errorIcon == null)
                {
                    errorIcon = ErrorIcon;
                }
                return errorIcon;
            }
            Image errorBitmap = obj2 as Image;
            if (errorBitmap == null)
            {
                errorBitmap = ErrorBitmap;
            }
            return errorBitmap;
        }

        protected override Size GetPreferredSize(Graphics graphics, GridCellStyle cellStyle, int rowIndex, Size constraintSize)
        {
            Size empty;
            if (base.Grid == null)
            {
                return new Size(-1, -1);
            }
            if (cellStyle == null)
            {
                throw new ArgumentNullException("cellStyle");
            }
            Rectangle stdBorderWidths = base.StdBorderWidths;
            int num = (stdBorderWidths.Left + stdBorderWidths.Width) + cellStyle.Padding.Horizontal;
            int num2 = (stdBorderWidths.Top + stdBorderWidths.Height) + cellStyle.Padding.Vertical;
            GridFreeDimension freeDimensionFromConstraint = GridCell.GetFreeDimensionFromConstraint(constraintSize);
            object obj2 = base.GetFormattedValue(rowIndex, ref cellStyle, GridDataErrorContexts.PreferredSize | GridDataErrorContexts.Formatting);
            Image image = obj2 as Image;
            Icon icon = null;
            if (image == null)
            {
                icon = obj2 as Icon;
            }
            if ((freeDimensionFromConstraint == GridFreeDimension.Height) && (this.ImageLayout == GridImageCellLayout.Zoom))
            {
                if ((image != null) || (icon != null))
                {
                    if (image != null)
                    {
                        int num3 = constraintSize.Width - num;
                        if ((num3 <= 0) || (image.Width == 0))
                        {
                            empty = Size.Empty;
                        }
                        else
                        {
                            empty = new Size(0, Math.Min(image.Height, decimal.ToInt32((image.Height * num3) / image.Width)));
                        }
                    }
                    else
                    {
                        int num4 = constraintSize.Width - num;
                        if ((num4 <= 0) || (icon.Width == 0))
                        {
                            empty = Size.Empty;
                        }
                        else
                        {
                            empty = new Size(0, Math.Min(icon.Height, decimal.ToInt32((icon.Height * num4) / icon.Width)));
                        }
                    }
                }
                else
                {
                    empty = new Size(0, 1);
                }
            }
            else if ((freeDimensionFromConstraint == GridFreeDimension.Width) && (this.ImageLayout == GridImageCellLayout.Zoom))
            {
                if ((image != null) || (icon != null))
                {
                    if (image != null)
                    {
                        int num5 = constraintSize.Height - num2;
                        if ((num5 <= 0) || (image.Height == 0))
                        {
                            empty = Size.Empty;
                        }
                        else
                        {
                            empty = new Size(Math.Min(image.Width, decimal.ToInt32((image.Width * num5) / image.Height)), 0);
                        }
                    }
                    else
                    {
                        int num6 = constraintSize.Height - num2;
                        if ((num6 <= 0) || (icon.Height == 0))
                        {
                            empty = Size.Empty;
                        }
                        else
                        {
                            empty = new Size(Math.Min(icon.Width, decimal.ToInt32((icon.Width * num6) / icon.Height)), 0);
                        }
                    }
                }
                else
                {
                    empty = new Size(1, 0);
                }
            }
            else
            {
                if (image != null)
                {
                    empty = new Size(image.Width, image.Height);
                }
                else if (icon != null)
                {
                    empty = new Size(icon.Width, icon.Height);
                }
                else
                {
                    empty = new Size(1, 1);
                }
                switch (freeDimensionFromConstraint)
                {
                    case GridFreeDimension.Height:
                        empty.Width = 0;
                        goto Label_02F6;

                    case GridFreeDimension.Width:
                        empty.Height = 0;
                        goto Label_02F6;
                }
            }
        Label_02F6:
            if (freeDimensionFromConstraint != GridFreeDimension.Height)
            {
                empty.Width += num;
                if (base.Grid.ShowCellErrors)
                {
                    empty.Width = Math.Max(empty.Width, (num + 8) + 12);
                }
            }
            if (freeDimensionFromConstraint != GridFreeDimension.Width)
            {
                empty.Height += num2;
                if (base.Grid.ShowCellErrors)
                {
                    empty.Height = Math.Max(empty.Height, (num2 + 8) + 11);
                }
            }
            return empty;
        }

        protected override object GetValue(int rowIndex)
        {
            object obj2 = base.GetValue(rowIndex);
            if (obj2 == null)
            {
                GridImageColumn owningColumn = base.OwningColumn as GridImageColumn;
                if (owningColumn == null)
                {
                    return obj2;
                }
                if (defaultTypeImage.IsAssignableFrom(this.ValueType))
                {
                    Image image = owningColumn.Image;
                    if (image != null)
                    {
                        return image;
                    }
                    return obj2;
                }
                if (defaultTypeIcon.IsAssignableFrom(this.ValueType))
                {
                    Icon icon = owningColumn.Icon;
                    if (icon != null)
                    {
                        return icon;
                    }
                }
            }
            return obj2;
        }

        private Rectangle ImgBounds(Rectangle bounds, int imgWidth, int imgHeight, GridImageCellLayout imageLayout, GridCellStyle cellStyle)
        {
            Rectangle empty = Rectangle.Empty;
            switch (imageLayout)
            {
                case GridImageCellLayout.NotSet:
                case GridImageCellLayout.Normal:
                    empty = new Rectangle(bounds.X, bounds.Y, imgWidth, imgHeight);
                    break;

                case GridImageCellLayout.Zoom:
                    if ((imgWidth * bounds.Height) >= (imgHeight * bounds.Width))
                    {
                        empty = new Rectangle(bounds.X, bounds.Y, bounds.Width, decimal.ToInt32((imgHeight * bounds.Width) / imgWidth));
                        break;
                    }
                    empty = new Rectangle(bounds.X, bounds.Y, decimal.ToInt32((imgWidth * bounds.Height) / imgHeight), bounds.Height);
                    break;
            }
            if (base.Grid.RightToLeftInternal)
            {
                switch (cellStyle.Alignment)
                {
                    case GridContentAlignment.MiddleRight:
                        empty.X = bounds.X;
                        goto Label_025B;

                    case GridContentAlignment.BottomLeft:
                        empty.X = bounds.Right - empty.Width;
                        goto Label_025B;

                    case GridContentAlignment.BottomRight:
                        empty.X = bounds.X;
                        goto Label_025B;

                    case GridContentAlignment.TopLeft:
                        empty.X = bounds.Right - empty.Width;
                        goto Label_025B;

                    case GridContentAlignment.TopRight:
                        empty.X = bounds.X;
                        goto Label_025B;

                    case GridContentAlignment.MiddleLeft:
                        empty.X = bounds.Right - empty.Width;
                        goto Label_025B;
                }
            }
            else
            {
                switch (cellStyle.Alignment)
                {
                    case GridContentAlignment.MiddleRight:
                        empty.X = bounds.Right - empty.Width;
                        goto Label_025B;

                    case GridContentAlignment.BottomLeft:
                        empty.X = bounds.X;
                        goto Label_025B;

                    case GridContentAlignment.BottomRight:
                        empty.X = bounds.Right - empty.Width;
                        goto Label_025B;

                    case GridContentAlignment.TopLeft:
                        empty.X = bounds.X;
                        goto Label_025B;

                    case GridContentAlignment.TopRight:
                        empty.X = bounds.Right - empty.Width;
                        goto Label_025B;

                    case GridContentAlignment.MiddleLeft:
                        empty.X = bounds.X;
                        goto Label_025B;
                }
            }
        Label_025B:
            switch (cellStyle.Alignment)
            {
                case GridContentAlignment.TopCenter:
                case GridContentAlignment.MiddleCenter:
                case GridContentAlignment.BottomCenter:
                    empty.X = bounds.X + ((bounds.Width - empty.Width) / 2);
                    break;
            }
            GridContentAlignment alignment = cellStyle.Alignment;
            if (alignment <= GridContentAlignment.MiddleCenter)
            {
                switch (alignment)
                {
                    case GridContentAlignment.TopLeft:
                    case GridContentAlignment.TopCenter:
                    case GridContentAlignment.TopRight:
                        empty.Y = bounds.Y;
                        return empty;

                    case (GridContentAlignment.TopCenter | GridContentAlignment.TopLeft):
                        return empty;

                    case GridContentAlignment.MiddleLeft:
                    case GridContentAlignment.MiddleCenter:
                        goto Label_030C;
                }
                return empty;
            }
            if (alignment <= GridContentAlignment.BottomLeft)
            {
                switch (alignment)
                {
                    case GridContentAlignment.MiddleRight:
                        goto Label_030C;

                    case GridContentAlignment.BottomLeft:
                        goto Label_032E;
                }
                return empty;
            }
            switch (alignment)
            {
                case GridContentAlignment.BottomCenter:
                case GridContentAlignment.BottomRight:
                    goto Label_032E;

                default:
                    return empty;
            }
        Label_030C:
            empty.Y = bounds.Y + ((bounds.Height - empty.Height) / 2);
            return empty;
        Label_032E:
            empty.Y = bounds.Bottom - empty.Height;
            return empty;
        }

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, GridElementStates elementState, object value, object formattedValue, string errorText, GridCellStyle cellStyle, GridAdvancedBorderStyle advancedBorderStyle, GridPaintParts paintParts)
        {
            if (cellStyle == null)
            {
                throw new ArgumentNullException("cellStyle");
            }
            this.PaintPrivate(graphics, clipBounds, cellBounds, rowIndex, elementState, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts, false, false, true);
        }

        private Rectangle PaintPrivate(Graphics g, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, GridElementStates elementState, object formattedValue, string errorText, GridCellStyle cellStyle, GridAdvancedBorderStyle advancedBorderStyle, GridPaintParts paintParts, bool computeContentBounds, bool computeErrorIconBounds, bool paint)
        {
            Rectangle empty;
            Point point;
            if (paint && GridCell.PaintBorder(paintParts))
            {
                this.PaintBorder(g, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
            }
            Rectangle cellValueBounds = cellBounds;
            Rectangle rectangle3 = this.BorderWidths(advancedBorderStyle);
            cellValueBounds.Offset(rectangle3.X, rectangle3.Y);
            cellValueBounds.Width -= rectangle3.Right;
            cellValueBounds.Height -= rectangle3.Bottom;
            if (((cellValueBounds.Width <= 0) || (cellValueBounds.Height <= 0)) || (!paint && !computeContentBounds))
            {
                if (computeErrorIconBounds)
                {
                    if (!string.IsNullOrEmpty(errorText))
                    {
                        return base.ComputeErrorIconBounds(cellValueBounds);
                    }
                    return Rectangle.Empty;
                }
                return Rectangle.Empty;
            }
            Rectangle destRect = cellValueBounds;
            if (cellStyle.Padding != Padding.Empty)
            {
                if (base.Grid.RightToLeftInternal)
                {
                    destRect.Offset(cellStyle.Padding.Right, cellStyle.Padding.Top);
                }
                else
                {
                    destRect.Offset(cellStyle.Padding.Left, cellStyle.Padding.Top);
                }
                destRect.Width -= cellStyle.Padding.Horizontal;
                destRect.Height -= cellStyle.Padding.Vertical;
            }
            bool flag = (elementState & GridElementStates.Selected) != GridElementStates.None;
            SolidBrush cachedBrush = base.Grid.GetCachedBrush((GridCell.PaintSelectionBackground(paintParts) && flag) ? cellStyle.SelectionBackColor : cellStyle.BackColor);
            if ((destRect.Width > 0) && (destRect.Height > 0))
            {
                Image image = formattedValue as Image;
                Icon icon = null;
                if (image == null)
                {
                    icon = formattedValue as Icon;
                }
                if ((icon != null) || (image != null))
                {
                    GridImageCellLayout imageLayout = this.ImageLayout;
                    switch (imageLayout)
                    {
                        case GridImageCellLayout.NotSet:
                            if (base.OwningColumn is GridImageColumn)
                            {
                                imageLayout = ((GridImageColumn) base.OwningColumn).ImageLayout;
                            }
                            else
                            {
                                imageLayout = GridImageCellLayout.Normal;
                            }
                            break;

                        case GridImageCellLayout.Stretch:
                            if (paint)
                            {
                                if (GridCell.PaintBackground(paintParts))
                                {
                                    GridCell.PaintPadding(g, cellValueBounds, cellStyle, cachedBrush, base.Grid.RightToLeftInternal);
                                }
                                if (GridCell.PaintContentForeground(paintParts))
                                {
                                    if (image != null)
                                    {
                                        ImageAttributes imageAttr = new ImageAttributes();
                                        imageAttr.SetWrapMode(WrapMode.TileFlipXY);
                                        g.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttr);
                                        imageAttr.Dispose();
                                    }
                                    else
                                    {
                                        g.DrawIcon(icon, destRect);
                                    }
                                }
                            }
                            empty = destRect;
                            goto Label_037E;
                    }
                    Rectangle a = this.ImgBounds(destRect, (image == null) ? icon.Width : image.Width, (image == null) ? icon.Height : image.Height, imageLayout, cellStyle);
                    empty = a;
                    if (paint)
                    {
                        if (GridCell.PaintBackground(paintParts) && (cachedBrush.Color.A == 0xff))
                        {
                            g.FillRectangle(cachedBrush, cellValueBounds);
                        }
                        if (GridCell.PaintContentForeground(paintParts))
                        {
                            Region clip = g.Clip;
                            g.SetClip(Rectangle.Intersect(Rectangle.Intersect(a, destRect), Rectangle.Truncate(g.VisibleClipBounds)));
                            if (image != null)
                            {
                                g.DrawImage(image, a);
                            }
                            else
                            {
                                g.DrawIconUnstretched(icon, a);
                            }
                            g.Clip = clip;
                        }
                    }
                }
                else
                {
                    if ((paint && GridCell.PaintBackground(paintParts)) && (cachedBrush.Color.A == 0xff))
                    {
                        g.FillRectangle(cachedBrush, cellValueBounds);
                    }
                    empty = Rectangle.Empty;
                }
            }
            else
            {
                if ((paint && GridCell.PaintBackground(paintParts)) && (cachedBrush.Color.A == 0xff))
                {
                    g.FillRectangle(cachedBrush, cellValueBounds);
                }
                empty = Rectangle.Empty;
            }
        Label_037E:
            point = base.Grid.CurrentCellAddress;
            if (((paint && GridCell.PaintFocus(paintParts)) && ((point.X == base.ColumnIndex) && (point.Y == rowIndex))) && (base.Grid.ShowFocusCues && base.Grid.Focused))
            {
                ControlPaint.DrawFocusRectangle(g, cellValueBounds, Color.Empty, cachedBrush.Color);
            }
            if ((base.Grid.ShowCellErrors && paint) && GridCell.PaintErrorIcon(paintParts))
            {
                base.PaintErrorIcon(g, cellStyle, rowIndex, cellBounds, cellValueBounds, errorText);
            }
            return empty;
        }

        /// <filterpriority>1</filterpriority>
        public override string ToString()
        {
            return ("GridImageCell { ColumnIndex=" + base.ColumnIndex.ToString(CultureInfo.CurrentCulture) + ", RowIndex=" + base.RowIndex.ToString(CultureInfo.CurrentCulture) + " }");
        }

        /// <summary>Gets the default value that is used when creating a new row.</summary>
        /// <returns>An object containing a default image placeholder, or null to display an empty cell.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public override object DefaultNewRowValue
        {
            get
            {
                if (defaultTypeImage.IsAssignableFrom(this.ValueType))
                {
                    return ErrorBitmap;
                }
                if (defaultTypeIcon.IsAssignableFrom(this.ValueType))
                {
                    return ErrorIcon;
                }
                return null;
            }
        }

        /// <summary>Gets or sets the text associated with the image.</summary>
        /// <returns>The text associated with the image displayed in the cell.</returns>
        /// <filterpriority>1</filterpriority>
        [DefaultValue("")]
        public string Description
        {
            get
            {
                object obj2 = base.Properties.GetObject(PropImageCellDescription);
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return string.Empty;
            }
            set
            {
                if (!string.IsNullOrEmpty(value) || base.Properties.ContainsObject(PropImageCellDescription))
                {
                    base.Properties.SetObject(PropImageCellDescription, value);
                }
            }
        }

        /// <summary>Gets the type of the cell's hosted editing control. </summary>
        /// <returns>The <see cref="T:System.Type"></see> of the underlying editing control. As implemented in this class, this property is always null.</returns>
        /// <filterpriority>1</filterpriority>
        public override System.Type EditType
        {
            get
            {
                return null;
            }
        }

        internal static Bitmap ErrorBitmap
        {
            get
            {
                if (errorBmp == null)
                {
                    errorBmp = new Bitmap(typeof(Grid), "ImageInError.bmp");
                }
                return errorBmp;
            }
        }

        internal static Icon ErrorIcon
        {
            get
            {
                if (errorIco == null)
                {
                    errorIco = new Icon(typeof(Grid), "IconInError.ico");
                }
                return errorIco;
            }
        }

        /// <summary>Gets the type of the formatted value associated with the cell.</summary>
        /// <returns>A <see cref="T:System.Type"></see> object representing display value type of the cell, which is the <see cref="T:System.Drawing.Image"></see> type if the <see cref="P:MControl.GridView.GridImageCell.ValueIsIcon"></see> property is set to false or the <see cref="T:System.Drawing.Icon"></see> type otherwise.</returns>
        /// <filterpriority>1</filterpriority>
        public override System.Type FormattedValueType
        {
            get
            {
                if (this.ValueIsIcon)
                {
                    return defaultTypeIcon;
                }
                return defaultTypeImage;
            }
        }

        /// <summary>Gets or sets the graphics layout for the cell. </summary>
        /// <returns>A <see cref="T:MControl.GridView.GridImageCellLayout"></see> for this cell. The default is <see cref="F:MControl.GridView.GridImageCellLayout.NotSet"></see>.</returns>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The supplied <see cref="T:MControl.GridView.GridImageCellLayout"></see> value is invalid. </exception>
        /// <filterpriority>1</filterpriority>
        [DefaultValue(0)]
        public GridImageCellLayout ImageLayout
        {
            get
            {
                bool flag;
                int integer = base.Properties.GetInteger(PropImageCellLayout, out flag);
                if (flag)
                {
                    return (GridImageCellLayout) integer;
                }
                return GridImageCellLayout.Normal;
            }
            set
            {
                if (!System.Windows.Forms.ClientUtils.IsEnumValid(value, (int) value, 0, 3))
                {
                    throw new InvalidEnumArgumentException("value", (int) value, typeof(GridImageCellLayout));
                }
                if (this.ImageLayout != value)
                {
                    base.Properties.SetInteger(PropImageCellLayout, (int) value);
                    base.OnCommonChange();
                }
            }
        }

        internal GridImageCellLayout ImageLayoutInternal
        {
            set
            {
                if (this.ImageLayout != value)
                {
                    base.Properties.SetInteger(PropImageCellLayout, (int) value);
                }
            }
        }

        /// <summary>Gets or sets a value indicating whether this cell displays an <see cref="T:System.Drawing.Icon"></see> value.</summary>
        /// <returns>true if this cell displays an <see cref="T:System.Drawing.Icon"></see> value; otherwise, false.</returns>
        [DefaultValue(false)]
        public bool ValueIsIcon
        {
            get
            {
                return ((this.flags & 1) != 0);
            }
            set
            {
                if (this.ValueIsIcon != value)
                {
                    this.ValueIsIconInternal = value;
                    if (base.Grid != null)
                    {
                        if (base.RowIndex != -1)
                        {
                            base.Grid.InvalidateCell(this);
                        }
                        else
                        {
                            base.Grid.InvalidateColumnInternal(base.ColumnIndex);
                        }
                    }
                }
            }
        }

        internal bool ValueIsIconInternal
        {
            set
            {
                if (this.ValueIsIcon != value)
                {
                    if (value)
                    {
                        this.flags = (byte) (this.flags | 1);
                    }
                    else
                    {
                        this.flags = (byte) (this.flags & -2);
                    }
                    if ((((base.Grid != null) && (base.RowIndex != -1)) && ((base.Grid.NewRowIndex == base.RowIndex) && !base.Grid.VirtualMode)) && ((value && (base.Value == ErrorBitmap)) || (!value && (base.Value == ErrorIcon))))
                    {
                        base.Value = this.DefaultNewRowValue;
                    }
                }
            }
        }

        /// <summary>Gets or sets the data type of the values in the cell. </summary>
        /// <returns>The <see cref="T:System.Type"></see> of the cell's value.</returns>
        /// <filterpriority>1</filterpriority>
        public override System.Type ValueType
        {
            get
            {
                System.Type valueType = base.ValueType;
                if (valueType != null)
                {
                    return valueType;
                }
                if (this.ValueIsIcon)
                {
                    return defaultTypeIcon;
                }
                return defaultTypeImage;
            }
            set
            {
                base.ValueType = value;
                this.ValueIsIcon = (value != null) && defaultTypeIcon.IsAssignableFrom(value);
            }
        }

        /// <summary>Provides information about a <see cref="T:MControl.GridView.GridImageCell"></see> to accessibility client applications.</summary>
        protected class GridImageCellAccessibleObject : GridCell.GridCellAccessibleObject
        {
            /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridImageCell.GridImageCellAccessibleObject"></see> class. </summary>
            /// <param name="owner">The <see cref="T:MControl.GridView.GridCell"></see> that owns the <see cref="T:MControl.GridView.GridImageCell.GridImageCellAccessibleObject"></see>.</param>
            public GridImageCellAccessibleObject(GridCell owner) : base(owner)
            {
            }

            /// <summary>Performs the default action of the <see cref="T:MControl.GridView.GridImageCell.GridImageCellAccessibleObject"></see>.</summary>
            [SecurityPermission(SecurityAction.Demand, Flags=SecurityPermissionFlag.UnmanagedCode)]
            public override void DoDefaultAction()
            {
            }

            /// <summary>Gets the number of child accessible objects that belong to the <see cref="T:MControl.GridView.GridImageCell.GridImageCellAccessibleObject"></see>.</summary>
            /// <returns>The value –1.</returns>
            public override int GetChildCount()
            {
                return 0;
            }

            /// <summary>Gets a string that represents the default action of the <see cref="T:MControl.GridView.GridImageCell"></see>.</summary>
            /// <returns>An empty string ("").</returns>
            public override string DefaultAction
            {
                get
                {
                    return string.Empty;
                }
            }

            /// <summary>Gets the text associated with the image in the image cell.</summary>
            /// <returns>The text associated with the image in the image cell.</returns>
            public override string Description
            {
                get
                {
                    GridImageCell owner = base.Owner as GridImageCell;
                    if (owner != null)
                    {
                        return owner.Description;
                    }
                    return null;
                }
            }

            /// <summary>Gets a string representing the formatted value of the owning cell. </summary>
            /// <returns>A <see cref="T:System.String"></see> representation of the cell value.</returns>
            /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridCell.GridCellAccessibleObject.Owner"></see> property is null.</exception>
            public override string Value
            {
                [SecurityPermission(SecurityAction.Demand, Flags=SecurityPermissionFlag.UnmanagedCode)]
                get
                {
                    return base.Value;
                }
                [SecurityPermission(SecurityAction.Demand, Flags=SecurityPermissionFlag.UnmanagedCode)]
                set
                {
                }
            }
        }
    }
}

