namespace MControl.GridView
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    /// <summary>Provides the base class for elements of a <see cref="T:MControl.GridView.Grid"></see> control.</summary>
    /// <filterpriority>2</filterpriority>
    public class GridElement
    {
        private MControl.GridView.Grid grid;
        private GridElementStates state;

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridElement"></see> class.</summary>
        public GridElement()
        {
            this.state = GridElementStates.Visible;
        }

        internal GridElement(GridElement dgveTemplate)
        {
            this.state = dgveTemplate.State & (GridElementStates.Visible | GridElementStates.ResizableSet | GridElementStates.Resizable | GridElementStates.ReadOnly | GridElementStates.Frozen);
        }

        /// <summary>Called when the element is associated with a different <see cref="T:MControl.GridView.Grid"></see>.</summary>
        protected virtual void OnGridChanged()
        {
        }

        /// <summary>Raises the <see cref="E:MControl.GridView.Grid.CellClick"></see> event. </summary>
        /// <param name="e">A <see cref="T:MControl.GridView.GridCellEventArgs"></see> that contains the event data. </param>
        protected void RaiseCellClick(GridCellEventArgs e)
        {
            if (this.grid != null)
            {
                this.grid.OnCellClickInternal(e);
            }
        }

        /// <summary>Raises the <see cref="E:MControl.GridView.Grid.CellContentClick"></see> event. </summary>
        /// <param name="e">A <see cref="T:MControl.GridView.GridCellEventArgs"></see> that contains the event data. </param>
        protected void RaiseCellContentClick(GridCellEventArgs e)
        {
            if (this.grid != null)
            {
                this.grid.OnCellContentClickInternal(e);
            }
        }

        /// <summary>Raises the <see cref="E:MControl.GridView.Grid.CellContentDoubleClick"></see> event. </summary>
        /// <param name="e">A <see cref="T:MControl.GridView.GridCellEventArgs"></see> that contains the event data. </param>
        protected void RaiseCellContentDoubleClick(GridCellEventArgs e)
        {
            if (this.grid != null)
            {
                this.grid.OnCellContentDoubleClickInternal(e);
            }
        }

        /// <summary>Raises the <see cref="E:MControl.GridView.Grid.CellValueChanged"></see> event. </summary>
        /// <param name="e">A <see cref="T:MControl.GridView.GridCellEventArgs"></see> that contains the event data. </param>
        protected void RaiseCellValueChanged(GridCellEventArgs e)
        {
            if (this.grid != null)
            {
                this.grid.OnCellValueChangedInternal(e);
            }
        }

        /// <summary>Raises the <see cref="E:MControl.GridView.Grid.DataError"></see> event. </summary>
        /// <param name="e">A <see cref="T:MControl.GridView.GridDataErrorEventArgs"></see> that contains the event data. </param>
        protected void RaiseDataError(GridDataErrorEventArgs e)
        {
            if (this.grid != null)
            {
                this.grid.OnDataErrorInternal(e);
            }
        }

        /// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseWheel"></see> event. </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"></see> that contains the event data. </param>
        protected void RaiseMouseWheel(MouseEventArgs e)
        {
            if (this.grid != null)
            {
                this.grid.OnMouseWheelInternal(e);
            }
        }

        internal bool StateExcludes(GridElementStates elementState)
        {
            return ((this.State & elementState) == GridElementStates.None);
        }

        internal bool StateIncludes(GridElementStates elementState)
        {
            return ((this.State & elementState) == elementState);
        }

        /// <summary>Gets the <see cref="T:MControl.GridView.Grid"></see> control associated with this element.</summary>
        /// <returns>A <see cref="T:MControl.GridView.Grid"></see> control that contains this element. The default is null.</returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MControl.GridView.Grid Grid
        {
            get
            {
                return this.grid;
            }
        }

        internal MControl.GridView.Grid GridInternal
        {
            set
            {
                if (this.Grid != value)
                {
                    this.grid = value;
                    this.OnGridChanged();
                }
            }
        }

        /// <summary>Gets the user interface (UI) state of the element.</summary>
        /// <returns>A bitwise combination of the <see cref="T:MControl.GridView.GridElementStates"></see> values representing the state.</returns>
        [EditorBrowsable(EditorBrowsableState.Advanced), Browsable(false)]
        public virtual GridElementStates State
        {
            get
            {
                return this.state;
            }
        }

        internal GridElementStates StateInternal
        {
            set
            {
                this.state = value;
            }
        }
    }
}

