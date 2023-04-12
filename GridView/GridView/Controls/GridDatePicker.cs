using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Runtime.InteropServices;

using Nistec.WinForms;

namespace Nistec.GridView
{
    /// <summary>
    /// Grid DatePicker control
    /// </summary>
    [DefaultProperty("GridEditName"), DesignTimeVisible(false), ToolboxItem(false)]
    public class GridDatePicker : Nistec.WinForms.McDatePicker
    {

        #region Fields

        private Grid dataGrid;
        private bool isInEditOrNavigateMode;
        internal bool editingPosition;
        //internal bool editingInternal;

        #endregion

        #region Methods
        /// <summary>
        /// Initilaized GridDatePicker
        /// </summary>
        internal GridDatePicker()
        {
            this.isInEditOrNavigateMode = true;
            base.TabStop = false;

            //base.NetReflectedFram("ba7fa38f0b671cbc");
            base.ButtonPedding = 0;
            this.editingPosition = false;
            //editingInternal = true;
            base.FixSize = true;
            base.BorderStyle = BorderStyle.None;
            base.ControlLayout = ControlLayout.Flat;
            //base.InputMask="";
        }

        internal void ClearSelected()
        {
            editingPosition = false;
            base.Text = "";
        }

        internal void SetFormatMask()
        {
            //if(format.Length==0)format="dd/MM/yyyy";
            base.SetFormatInputMask();
        }
        /// <summary>
        /// On RightToLeft Changed
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRightToLeftChanged(EventArgs e)
        {
            base.OnRightToLeftChanged(e);
            if (base.RightToLeft == RightToLeft.Yes)
                base.ButtonAlign = Nistec.WinForms.ButtonAlign.Left;
            else
                base.ButtonAlign = Nistec.WinForms.ButtonAlign.Right;

        }
        /// <summary>
        /// On Value Changed
        /// </summary>
        /// <param name="eventargs"></param>
        protected override void OnValueChanged(EventArgs eventargs)
        {
            base.OnValueChanged(eventargs);
            if (!IsHandleCreated)//&& base.IsModified)
                return;
            //if (editingInternal)
            //{
            //    editingInternal = false;
            //    return;
            //}
            if (!base.ReadOnly && editingPosition )
            {
                this.IsInEditOrNavigateMode = false;
                this.dataGrid.ColumnStartedEditing(base.Bounds);
            }
        }

        /// <summary>
        /// On Modified Internal
        /// </summary>
        /// <param name="e"></param>
        protected override void OnModifiedInternal(EventArgs e)
        {
            base.OnModifiedInternal(e);
            if (!base.ReadOnly)
            {
                this.IsInEditOrNavigateMode = false;
                this.dataGrid.ColumnStartedEditing(base.Bounds);
            }
        }
        /// <summary>
        /// OnKeyPress
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            if ((((e.KeyChar != ' ') || ((Control.ModifierKeys & Keys.Shift) != Keys.Shift)) && !base.ReadOnly) && (((Control.ModifierKeys & Keys.Control) != Keys.Control) || ((Control.ModifierKeys & Keys.Alt) != Keys.None)))
            {
                this.IsInEditOrNavigateMode = false;
                this.dataGrid.ColumnStartedEditing(base.Bounds);
            }
        }

        /// <summary>
        /// Raises the MouseWheel event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            this.dataGrid.TextBoxOnMouseWheel(e);
        }
        /// <summary>
        /// Processes a command key.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message m, Keys keyData)
        {
  
            char c = Strings.Chr((int)m.WParam);
            if (char.IsDigit(c))
            {
                this.IsInEditOrNavigateMode = false;
                this.dataGrid.ColumnStartedEditing(base.Bounds);
            }

            return ProcessKeyMessage(ref m);

        }
        /// <summary>
        /// Processes a keyboard message
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [SecurityPermission(SecurityAction.LinkDemand)]
        protected override bool ProcessKeyMessage(ref Message m)
        {
            Keys keys1 = (Keys)((int)m.WParam);

            Keys keys2 = Control.ModifierKeys;
            if (keys1 == Keys.Enter && base.DroppedDown)
            {
                base.OnKeyDown(new KeyEventArgs(Keys.Enter));
                return true;
            }
            if (((keys1 | keys2) == (Keys.Shift | Keys.Down)))
            {
                base.DoDropDown();
                return true;
            }
            if ((((keys1 | keys2) == Keys.Return) || ((keys1 | keys2) == Keys.Escape)) || ((keys1 | keys2) == (Keys.Control | Keys.Return)))
            {
                if (m.Msg == 0x102)
                {
                    return true;
                }
                return this.ProcessKeyPreview(ref m);
            }
            if (m.Msg == 0x102)
            {
                if (keys1 == Keys.LineFeed)
                {
                    return true;
                }
                return this.ProcessKeyEventArgs(ref m);
            }
            if (m.Msg == 0x101)
            {
                return true;
            }
            switch ((keys1 & Keys.KeyCode))
            {
                case Keys.Add:
                case Keys.Subtract:
                case Keys.Oemplus:
                case Keys.OemMinus:
                case Keys.Prior:
                case Keys.Next:
                    if (this.IsInEditOrNavigateMode)
                    {
                        return this.ProcessKeyPreview(ref m);
                    }
                    return this.ProcessKeyEventArgs(ref m);

                case Keys.F2:
                    this.IsInEditOrNavigateMode = false;
                    base.SelectionStart = this.Text.Length;
                    return true;

                case Keys.Space:
                    if (this.IsInEditOrNavigateMode && ((Control.ModifierKeys & Keys.Shift) == Keys.Shift))
                    {
                        if (m.Msg == 0x102)
                        {
                            return true;
                        }
                        return this.ProcessKeyPreview(ref m);
                    }
                    return this.ProcessKeyEventArgs(ref m);

                case Keys.End:
                case Keys.Home:
                    if (this.SelectionLength == this.Text.Length)
                    {
                        return this.ProcessKeyPreview(ref m);
                    }
                    return this.ProcessKeyEventArgs(ref m);

                case Keys.Left:
                    if (base.DroppedDown)
                    {
                        base.OnKeyDown(new KeyEventArgs(Keys.Left));
                        return true;
                    }
                    if (((base.SelectionStart + this.SelectionLength) != 0) && (!this.IsInEditOrNavigateMode || (this.SelectionLength != this.Text.Length)))
                    {
                        base.Select(base.SelectionStart - 1, 1);
                        return true;
                        //this.dataGrid.Focus();
                        //return this.ProcessKeyEventArgs(ref m);
                    }
                    return this.ProcessKeyPreview(ref m);

                case Keys.Up:
                    if (base.DroppedDown)
                    {
                        base.OnKeyDown(new KeyEventArgs(Keys.Up));
                        return true;
                    }
                    if ((this.Text.IndexOf("\r\n") >= 0) && ((base.SelectionStart + this.SelectionLength) >= this.Text.IndexOf("\r\n")))
                    {
                        return this.ProcessKeyEventArgs(ref m);
                    }
                    return this.ProcessKeyPreview(ref m);

                case Keys.Right:
                    if (base.DroppedDown)
                    {
                        base.OnKeyDown(new KeyEventArgs(Keys.Right));
                        return true;
                    }
                    if ((base.SelectionStart + this.SelectionLength) != this.Text.Length)
                    {
                        base.Select(base.SelectionStart + 1, 1);
                        return true;
                        //this.dataGrid.Focus();
                        //return this.ProcessKeyPreview(ref m);
                    }
                    return this.ProcessKeyEventArgs(ref m);

                case Keys.Down:
                    {
                        if (base.DroppedDown)
                        {
                            base.OnKeyDown(new KeyEventArgs(Keys.Down));
                            return true;
                        }
                        int num1 = base.SelectionStart + this.SelectionLength;
                        if (this.Text.IndexOf("\r\n", num1) == -1)
                        {
                            return this.ProcessKeyPreview(ref m);
                        }
                        return this.ProcessKeyEventArgs(ref m);
                    }
                case Keys.Delete:
                    if (this.IsInEditOrNavigateMode)
                    {
                        if (this.ProcessKeyPreview(ref m))
                        {
                            return true;
                        }
                        this.IsInEditOrNavigateMode = false;
                        this.dataGrid.ColumnStartedEditing(base.Bounds);
                        return this.ProcessKeyEventArgs(ref m);
                    }
                    return this.ProcessKeyEventArgs(ref m);

                case Keys.A:
                    if (!this.IsInEditOrNavigateMode || ((Control.ModifierKeys & Keys.Control) != Keys.Control))
                    {
                        return this.ProcessKeyEventArgs(ref m);
                    }
                    if (m.Msg == 0x102)
                    {
                        return true;
                    }
                    return this.ProcessKeyPreview(ref m);

                case Keys.Tab:
                    if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                    {
                        return this.ProcessKeyPreview(ref m);
                    }
                    return this.ProcessKeyEventArgs(ref m);
            }
            return this.ProcessKeyEventArgs(ref m);
        }
        /// <summary>
        /// Set the Grid parent of the control
        /// </summary>
        /// <param name="parentGrid"></param>
        internal void SetGrid(Grid parentGrid)
        {
            this.dataGrid = parentGrid;
        }

        /// <summary>
        /// Processes Windows messages
        /// </summary>
        [SecurityPermission(SecurityAction.LinkDemand)]
        protected override void WndProc(ref Message m)
        {
            if (((m.Msg == 770) || (m.Msg == 0x300)) || (m.Msg == 0x303))
            {
                this.IsInEditOrNavigateMode = false;
                this.dataGrid.ColumnStartedEditing(base.Bounds);
            }
            base.WndProc(ref m);
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value that indicates whether the control can be edited
        /// </summary>
        public new bool ReadOnly
        {
            get
            {
                return base.ReadOnly;
            }
            set
            {
                if (value)
                {
                    base.BackColor = dataGrid.BackColor;
                }
                base.ReadOnly = value;
            }
        }

        internal void AppendInternalText(string value)
        {
            //editingInternal = true;
            //if (value=="" || value.ToUpper() == "NOW")
            //value= DateTime.Today.ToString(this.Format);

            base.AppendInternal(value);
        }
        /// <summary>
        /// Gets or sets a value indicating whether the control is in a mode that allows either editing or navigating
        /// </summary>
        public bool IsInEditOrNavigateMode
        {
            get
            {
                return this.isInEditOrNavigateMode;
            }
            set
            {
                this.isInEditOrNavigateMode = value;
                if (value)
                {
                    base.SelectAll();
                }
            }
        }

        #endregion

    }
}
