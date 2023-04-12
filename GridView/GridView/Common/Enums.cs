using System;

namespace Nistec.GridView
{
    //public enum TextAlignment
    //{
    //    // Summary:
    //    //     The object or text is aligned on the left of the control element.
    //    Left = 0,
    //    //
    //    // Summary:
    //    //     The object or text is aligned on the right of the control element.
    //    Right = 1,
    //    //
    //    // Summary:
    //    //     The object or text is aligned in the center of the control element.
    //    Center = 2,
    //    //
    //    // Summary:
    //    //     The object or text is aligned By RightToLeft Property of the control element.
    //    Auto = 3
    //}

    //public enum GridRowChangeState
    //{
    //    None,
    //    Editing,
    //    Cancel,
    //    AddNew,
    //    Complited,
    //    Deleting,
    //    Deleted,
    //}

    /// <summary>
    /// GridRowState
    /// </summary>
    public enum GridRowState
    {
        /// <summary>
        /// Default
        /// </summary>
        Default = 0,
        /// <summary>
        /// Edit
        /// </summary>
        Edit = 1,
        /// <summary>
        /// New
        /// </summary>
        New = 2
    }
    /// <summary>
    /// GridDirtyState
    /// </summary>
    public enum GridDirtyState
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// Cell
        /// </summary>
        Cell = 1,
        /// <summary>
        /// Row
        /// </summary>
        Row = 2,
        /// <summary>
        /// Grid
        /// </summary>
        Grid=3
    }
    /// <summary>
    /// AggregateMode
    /// </summary>
	public enum AggregateMode
	{
        /// <summary>
        /// None
        /// </summary>
		None=0,
        /// <summary>
        /// Sum
        /// </summary>
		Sum=1,
        /// <summary>
        /// Avg
        /// </summary>
		Avg=2,
        /// <summary>
        /// Max
        /// </summary>
		Max=3,
        /// <summary>
        /// Min
        /// </summary>
		Min=4
	}
    /// <summary>
    /// GridFilterType
    /// </summary>
	public enum GridFilterType
	{
        /// <summary>
        /// None
        /// </summary>
		None=0,
        /// <summary>
        /// Equal
        /// </summary>
		Equal=1,
        /// <summary>
        /// Like
        /// </summary>
		Like=2
	}
    /// <summary>
    /// GridButtonStyle
    /// </summary>
	public enum GridButtonStyle
	{
        /// <summary>
        /// Button
        /// </summary>
		Button,
        /// <summary>
        /// Link
        /// </summary>
		Link,
        /// <summary>
        /// ButtonMenu
        /// </summary>
		ButtonMenu
	}
    /// <summary>
    /// GridButtonState
    /// </summary>
	public enum GridButtonState
	{
        /// <summary>
        /// Normal
        /// </summary>
		Normal,
        /// <summary>
        /// Pushed
        /// </summary>
		Pushed,
        /// <summary>
        /// Active
        /// </summary>
		Active
		//Inactive
	}
    /// <summary>
    /// SelectionType
    /// </summary>
	public enum SelectionType
	{
        /// <summary>
        /// Cell
        /// </summary>
		Cell=0,
        /// <summary>
        /// Tab
        /// </summary>
        Tab = 1,
        /// <summary>
        /// FullRow
        /// </summary>
        FullRow = 2,
        /// <summary>
        /// LabelRow
        /// </summary>
        LabelRow = 3
	}
    /// <summary>
    /// GridLineStyle
    /// </summary>
	public enum GridLineStyle
	{
        /// <summary>
        /// None
        /// </summary>
		None,
        /// <summary>
        /// Solid
        /// </summary>
		Solid
	}

    /// <summary>
    /// IGridEditingService
    /// </summary>
	internal interface IGridEditingService
	{
		// Methods
		bool BeginEdit(GridColumnStyle gridColumn, int rowNumber);
		bool EndEdit(GridColumnStyle gridColumn, int rowNumber, bool shouldAbort);
	}
    /// <summary>
    /// TriangleDirection
    /// </summary>
	internal enum TriangleDirection
	{
        /// <summary>
        /// Up
        /// </summary>
		Up,
        /// <summary>
        /// Down
        /// </summary>
		Down,
        /// <summary>
        /// Left
        /// </summary>
		Left,
        /// <summary>
        /// Right
        /// </summary>
		Right
	}
    /// <summary>
    /// GridParentRowsLabelStyle
    /// </summary>
	public enum GridParentRowsLabelStyle
	{
        /// <summary>
        /// None
        /// </summary>
		None,
        /// <summary>
        /// TableName
        /// </summary>
		TableName,
        /// <summary>
        /// ColumnName
        /// </summary>
		ColumnName,
        /// <summary>
        /// Both
        /// </summary>
		Both
	}
 
    /// <summary>
    /// GridColumnType
    /// </summary>
	public enum GridColumnType
	{
        /// <summary>
        /// None
        /// </summary>
		None,
        /// <summary>
        /// TextColumn
        /// </summary>
		TextColumn ,
        /// <summary>
        /// ComboColumn
        /// </summary>
		ComboColumn ,
        /// <summary>
        /// DateTimeColumn
        /// </summary>
		DateTimeColumn ,
        /// <summary>
        /// LabelColumn
        /// </summary>
		LabelColumn ,
       /// <summary>
        /// ButtonColumn
       /// </summary>
		ButtonColumn,
        /// <summary>
        /// ProgressColumn
        /// </summary>
		ProgressColumn ,
        /// <summary>
        /// BoolColumn
        /// </summary>
		BoolColumn ,
        /// <summary>
        /// IconColumn
        /// </summary>
		IconColumn ,
        /// <summary>
        /// MultiColumn
        /// </summary>
		MultiColumn ,
        /// <summary>
        /// NumericColumn
        /// </summary>
		NumericColumn ,
        /// <summary>
        /// VGridColumn
        /// </summary>
        VGridColumn ,
        /// <summary>
        /// GridColumn
        /// </summary>
		GridColumn ,
        /// <summary>
        /// MemoColumn
        /// </summary>
        MemoColumn
	}
    /// <summary>
    /// GridItemType
    /// </summary>
    public enum GridItemType
    {
        /// <summary>
        /// Property
        /// </summary>
        Property,
        /// <summary>
        /// Category
        /// </summary>
        Category,
        /// <summary>
        /// ArrayValue
        /// </summary>
        ArrayValue,
        /// <summary>
        /// Root
        /// </summary>
        Root
    }
 
 }
