namespace MControl.GridView
{
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    /// <summary>Represents the hosted combo box control in a <see cref="T:MControl.GridView.GridComboBoxCell"></see>.</summary>
    /// <filterpriority>2</filterpriority>
    [ClassInterface(ClassInterfaceType.AutoDispatch), ComVisible(true)]
    public class GridComboBoxEditingControl : ComboBox, IGridEditingControl
    {
        private Grid grid;
        private int rowIndex;
        private bool valueChanged;

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridComboBoxEditingControl"></see> class.</summary>
        public GridComboBoxEditingControl()
        {
            base.TabStop = false;
        }

        /// <summary>Changes the control's user interface (UI) to be consistent with the specified cell style.</summary>
        /// <param name="gridCellStyle">The <see cref="T:MControl.GridView.GridCellStyle"></see> to use as a pattern for the UI.</param>
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
        }

        /// <summary>Determines whether the specified key is a regular input key that the editing control should process or a special key that the <see cref="T:MControl.GridView.Grid"></see> should process.</summary>
        /// <returns>true if the specified key is a regular input key that should be handled by the editing control; otherwise, false.</returns>
        /// <param name="keyData">A bitwise combination of <see cref="T:System.Windows.Forms.Keys"></see> values that represents the key that was pressed.</param>
        /// <param name="gridWantsInputKey">true to indicate that the <see cref="T:MControl.GridView.Grid"></see> control can process the key; otherwise, false.</param>
        public virtual bool EditingControlWantsInputKey(Keys keyData, bool gridWantsInputKey)
        {
            if (((((keyData & Keys.KeyCode) != Keys.Down) && ((keyData & Keys.KeyCode) != Keys.Up)) && (!base.DroppedDown || ((keyData & Keys.KeyCode) != Keys.Escape))) && ((keyData & Keys.KeyCode) != Keys.Return))
            {
                return !gridWantsInputKey;
            }
            return true;
        }

        /// <summary>Retrieves the formatted value of the cell.</summary>
        /// <returns>An <see cref="T:System.Object"></see> that represents the formatted version of the cell contents.</returns>
        /// <param name="context">A bitwise combination of <see cref="T:MControl.GridView.GridDataErrorContexts"></see> values that specifies the data error context.</param>
        public virtual object GetEditingControlFormattedValue(GridDataErrorContexts context)
        {
            return this.Text;
        }

        private void NotifyGridOfValueChange()
        {
            this.valueChanged = true;
            this.grid.NotifyCurrentCellDirty(true);
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
            if (this.SelectedIndex != -1)
            {
                this.NotifyGridOfValueChange();
            }
        }

        /// <summary>Prepares the currently selected cell for editing.</summary>
        /// <param name="selectAll">true to select all of the cell's content; otherwise, false.</param>
        public virtual void PrepareEditingControlForEdit(bool selectAll)
        {
            if (selectAll)
            {
                base.SelectAll();
            }
        }

        /// <summary>Gets or sets the <see cref="T:MControl.GridView.Grid"></see> that contains the combo box control.</summary>
        /// <returns>The <see cref="T:MControl.GridView.Grid"></see> that contains the <see cref="T:MControl.GridView.GridComboBoxCell"></see> that contains this control; otherwise, null if there is no associated <see cref="T:MControl.GridView.Grid"></see>.</returns>
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

        /// <summary>Gets or sets the formatted representation of the current value of the control.</summary>
        /// <returns>An object representing the current value of this control.</returns>
        public virtual object EditingControlFormattedValue
        {
            get
            {
                return this.GetEditingControlFormattedValue(GridDataErrorContexts.Formatting);
            }
            set
            {
                string strA = value as string;
                if (strA != null)
                {
                    this.Text = strA;
                    if (string.Compare(strA, this.Text, true, CultureInfo.CurrentCulture) != 0)
                    {
                        this.SelectedIndex = -1;
                    }
                }
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

        /// <summary>Gets or sets a value indicating whether the current value of the control has changed.</summary>
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

        /// <summary>Gets the cursor used during editing.</summary>
        /// <returns>A <see cref="T:System.Windows.Forms.Cursor"></see> that represents the cursor image used by the mouse pointer during editing.</returns>
        public virtual Cursor EditingPanelCursor
        {
            get
            {
                return Cursors.Default;
            }
        }

        /// <summary>Gets a value indicating whether the cell contents need to be repositioned whenever the value changes.</summary>
        /// <returns>false in all cases.</returns>
        public virtual bool RepositionEditingControlOnValueChange
        {
            get
            {
                return false;
            }
        }
    }
}

