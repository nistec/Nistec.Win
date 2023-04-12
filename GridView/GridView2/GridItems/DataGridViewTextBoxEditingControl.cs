namespace MControl.GridView
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Security.Permissions;
    using System.Windows.Forms;

    /// <summary>Represents a text box control that can be hosted in a <see cref="T:MControl.GridView.GridTextBoxCell"></see>. </summary>
    /// <filterpriority>2</filterpriority>
    [ClassInterface(ClassInterfaceType.AutoDispatch), ComVisible(true)]
    public class GridTextBoxEditingControl : TextBox, IGridEditingControl
    {
        private static readonly GridContentAlignment anyCenter = (GridContentAlignment.BottomCenter | GridContentAlignment.MiddleCenter | GridContentAlignment.TopCenter);
        private static readonly GridContentAlignment anyRight = (GridContentAlignment.BottomRight | GridContentAlignment.MiddleRight | GridContentAlignment.TopRight);
        private static readonly GridContentAlignment anyTop = (GridContentAlignment.TopRight | GridContentAlignment.TopCenter | GridContentAlignment.TopLeft);
        private Grid grid;
        private bool repositionOnValueChange;
        private int rowIndex;
        private bool valueChanged;

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridTextBoxEditingControl"></see> class.</summary>
        public GridTextBoxEditingControl()
        {
            base.TabStop = false;
        }

        /// <summary>Changes the control's user interface (UI) to be consistent with the specified cell style.</summary>
        /// <param name="gridCellStyle">The <see cref="T:MControl.GridView.GridCellStyle"></see> to use as the model for the UI.</param>
        public virtual void ApplyCellStyleToEditingControl(GridCellStyle gridCellStyle)
        {
            this.Font = gridCellStyle.Font;
            if (gridCellStyle.BackColor.A < 0xff)
            {
                Color color = Color.FromArgb(0xff, gridCellStyle.BackColor);
                this.BackColor = color;
                this.grid.EditingPanel.BackColor = color;
            }
            else
            {
                this.BackColor = gridCellStyle.BackColor;
            }
            this.ForeColor = gridCellStyle.ForeColor;
            if (gridCellStyle.WrapMode == GridTriState.True)
            {
                base.WordWrap = true;
            }
            base.TextAlign = TranslateAlignment(gridCellStyle.Alignment);
            this.repositionOnValueChange = (gridCellStyle.WrapMode == GridTriState.True) && ((gridCellStyle.Alignment & anyTop) == GridContentAlignment.NotSet);
        }

        /// <summary>Determines whether the specified key is a regular input key that the editing control should process or a special key that the <see cref="T:MControl.GridView.Grid"></see> should process.</summary>
        /// <returns>true if the specified key is a regular input key that should be handled by the editing control; otherwise, false.</returns>
        /// <param name="keyData">A <see cref="T:System.Windows.Forms.Keys"></see> that represents the key that was pressed.</param>
        /// <param name="gridWantsInputKey">true when the <see cref="T:MControl.GridView.Grid"></see> wants to process the keyData; otherwise, false.</param>
        public virtual bool EditingControlWantsInputKey(Keys keyData, bool gridWantsInputKey)
        {
            switch ((keyData & Keys.KeyCode))
            {
                case Keys.Prior:
                case Keys.Next:
                    if (!this.valueChanged)
                    {
                        break;
                    }
                    return true;

                case Keys.End:
                case Keys.Home:
                    if (this.SelectionLength == this.Text.Length)
                    {
                        break;
                    }
                    return true;

                case Keys.Left:
                    if (((this.RightToLeft != RightToLeft.No) || ((this.SelectionLength == 0) && (base.SelectionStart == 0))) && ((this.RightToLeft != RightToLeft.Yes) || ((this.SelectionLength == 0) && (base.SelectionStart == this.Text.Length))))
                    {
                        break;
                    }
                    return true;

                case Keys.Up:
                    if ((this.Text.IndexOf("\r\n") < 0) || ((base.SelectionStart + this.SelectionLength) < this.Text.IndexOf("\r\n")))
                    {
                        break;
                    }
                    return true;

                case Keys.Right:
                    if (((this.RightToLeft != RightToLeft.No) || ((this.SelectionLength == 0) && (base.SelectionStart == this.Text.Length))) && ((this.RightToLeft != RightToLeft.Yes) || ((this.SelectionLength == 0) && (base.SelectionStart == 0))))
                    {
                        break;
                    }
                    return true;

                case Keys.Down:
                {
                    int startIndex = base.SelectionStart + this.SelectionLength;
                    if (this.Text.IndexOf("\r\n", startIndex) == -1)
                    {
                        break;
                    }
                    return true;
                }
                case Keys.Delete:
                    if ((this.SelectionLength <= 0) && (base.SelectionStart >= this.Text.Length))
                    {
                        break;
                    }
                    return true;

                case Keys.Return:
                    if ((((keyData & (Keys.Alt | Keys.Control | Keys.Shift)) == Keys.Shift) && this.Multiline) && base.AcceptsReturn)
                    {
                        return true;
                    }
                    break;
            }
            return !gridWantsInputKey;
        }

        /// <summary>Retrieves the formatted value of the cell.</summary>
        /// <returns>An <see cref="T:System.Object"></see> that represents the formatted version of the cell contents.</returns>
        /// <param name="context">One of the <see cref="T:MControl.GridView.GridDataErrorContexts"></see> values that specifies the data error context.</param>
        public virtual object GetEditingControlFormattedValue(GridDataErrorContexts context)
        {
            return this.Text;
        }

        private void NotifyGridOfValueChange()
        {
            this.valueChanged = true;
            this.grid.NotifyCurrentCellDirty(true);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            this.grid.OnMouseWheelInternal(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            this.NotifyGridOfValueChange();
        }

        /// <summary>Prepares the currently selected cell for editing.</summary>
        /// <param name="selectAll">true to select the cell contents; otherwise, false.</param>
        public virtual void PrepareEditingControlForEdit(bool selectAll)
        {
            if (selectAll)
            {
                base.SelectAll();
            }
            else
            {
                base.SelectionStart = this.Text.Length;
            }
        }

        /// <summary>Processes key events.</summary>
        /// <returns>true if the key event was handled by the editing control; otherwise, false.</returns>
        /// <param name="m">A <see cref="T:System.Windows.Forms.Message"></see> indicating the key that was pressed.</param>
        [SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.UnmanagedCode)]
        protected override bool ProcessKeyEventArgs(ref Message m)
        {
            Keys wParam = (Keys) ((int) m.WParam);
            if (wParam != Keys.LineFeed)
            {
                if (wParam == Keys.Return)
                {
                    if ((m.Msg == 0x102) && (((Control.ModifierKeys != Keys.Shift) || !this.Multiline) || !base.AcceptsReturn))
                    {
                        return true;
                    }
                    goto Label_0094;
                }
                if (wParam != Keys.A)
                {
                    goto Label_0094;
                }
            }
            else
            {
                if (((m.Msg != 0x102) || (Control.ModifierKeys != Keys.Control)) || (!this.Multiline || !base.AcceptsReturn))
                {
                    goto Label_0094;
                }
                return true;
            }
            if ((m.Msg == 0x100) && (Control.ModifierKeys == Keys.Control))
            {
                base.SelectAll();
                return true;
            }
        Label_0094:
            return base.ProcessKeyEventArgs(ref m);
        }

        private static HorizontalAlignment TranslateAlignment(GridContentAlignment align)
        {
            if ((align & anyRight) != GridContentAlignment.NotSet)
            {
                return HorizontalAlignment.Right;
            }
            if ((align & anyCenter) != GridContentAlignment.NotSet)
            {
                return HorizontalAlignment.Center;
            }
            return HorizontalAlignment.Left;
        }

        /// <summary>Gets or sets the <see cref="T:MControl.GridView.Grid"></see> that contains the text box control.</summary>
        /// <returns>A <see cref="T:MControl.GridView.Grid"></see> that contains the <see cref="T:MControl.GridView.GridTextBoxCell"></see> that contains this control; otherwise, null if there is no associated <see cref="T:MControl.GridView.Grid"></see>.</returns>
        public virtual Grid EditingControlGrid
        {
            get
            {
                return this.grid;
            }
            set
            {
                this.grid = value;
            }
        }

        /// <summary>Gets or sets the formatted representation of the current value of the text box control.</summary>
        /// <returns>An object representing the current value of this control.</returns>
        public virtual object EditingControlFormattedValue
        {
            get
            {
                return this.GetEditingControlFormattedValue(GridDataErrorContexts.Formatting);
            }
            set
            {
                this.Text = (string) value;
            }
        }

        /// <summary>Gets or sets the index of the owning cell's parent row.</summary>
        /// <returns>The index of the row that contains the owning cell; -1 if there is no owning row.</returns>
        public virtual int EditingControlRowIndex
        {
            get
            {
                return this.rowIndex;
            }
            set
            {
                this.rowIndex = value;
            }
        }

        /// <summary>Gets or sets a value indicating whether the current value of the text box control has changed.</summary>
        /// <returns>true if the value of the control has changed; otherwise, false.</returns>
        public virtual bool EditingControlValueChanged
        {
            get
            {
                return this.valueChanged;
            }
            set
            {
                this.valueChanged = value;
            }
        }

        /// <summary>Gets the cursor used when the mouse pointer is over the <see cref="P:MControl.GridView.Grid.EditingPanel"></see> but not over the editing control.</summary>
        /// <returns>A <see cref="T:System.Windows.Forms.Cursor"></see> that represents the mouse pointer used for the editing panel. </returns>
        public virtual Cursor EditingPanelCursor
        {
            get
            {
                return Cursors.Default;
            }
        }

        /// <summary>Gets a value indicating whether the cell contents need to be repositioned whenever the value changes.</summary>
        /// <returns>true if the cell's <see cref="P:MControl.GridView.GridCellStyle.WrapMode"></see> is set to true and the alignment property is not set to one of the <see cref="T:MControl.GridView.GridContentAlignment"></see> values that aligns the content to the top; otherwise, false.</returns>
        public virtual bool RepositionEditingControlOnValueChange
        {
            get
            {
                return this.repositionOnValueChange;
            }
        }
    }
}

