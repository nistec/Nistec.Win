namespace MControl.GridView
{
    using System;
    using System.Runtime.CompilerServices;

    /// <summary>Represents the method that will handle the <see cref="E:MControl.GridView.Grid.CellValueNeeded"></see> event or <see cref="E:MControl.GridView.Grid.CellValuePushed"></see> event of a <see cref="T:MControl.GridView.Grid"></see>. </summary>
    /// <filterpriority>2</filterpriority>
    public delegate void GridCellValueEventHandler(object sender, GridCellValueEventArgs e);
}

