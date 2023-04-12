using System;

namespace MControl.Win
{


    //public enum CompareMethod
    //{
    //    Binary,
    //    Text
    //}





	public enum AllowClose
	{
		Ask,
		AskIfNotFinish,
		AlwaysAllow
	}

//	public enum PanelItemStyle
//	{
//		Item = 1,
//		Separator = 2,
//		Option
//	}
	
	public enum LineDirection
	{
		Horizontal  = 1,
		Vertical = 2,
	}
	
	public enum LeftRight
	{
		Left  = 1,
		Right = 2,
	}

	public enum OptionHighLight
	{
		Rectangle = 1,
		Font = 2
	}

	public enum ThumbDraggedFireFrequency
	{
		MouseMove,
		MouseUp
	}

	public enum DrawState
	{
		Normal,
		Hot,
		Pressed,
		Disable
	}

	public enum ScrollBarEvent
	{
		LineUp,
		LineDown,
		PageUp,
		PageDown,
		ThumbUp,
		ThumbDown
	}

	

	public enum ScrollBarHit
	{
		UpArrow,
		DownArrow,
		PageUp,
		PageDown,
		Thumb,
		LeftArrow,
		RightArrow,
		PageLeft,
		PageRight,
		None
	}



	public enum GridSelectionMode
	{
		Cell = 1,
		Row = 2,
		Col = 3
	}

	[Flags]
	public enum EditableModes
	{
		None = 0,
		F2Key = 1,
		DoubleClick = 2,
		SingleClick = 4,
		AnyKey = 9
	}

	public enum CommonBorderStyle
	{
		Normal = 1,
		Raised = 2,
		Inset = 3
	}
}


