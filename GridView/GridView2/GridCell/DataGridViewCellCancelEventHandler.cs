namespace MControl.GridView
{
    using System;
    using System.Runtime.CompilerServices;

    /// <summary>Represents the method that will handle the <see cref="E:MControl.GridView.Grid.CellBeginEdit"></see> and <see cref="E:MControl.GridView.Grid.RowValidating"></see> events of a <see cref="T:MControl.GridView.Grid"></see>. </summary>
    /// <filterpriority>2</filterpriority>
    public delegate void GridCellCancelEventHandler(object sender, GridCellCancelEventArgs e);
}

