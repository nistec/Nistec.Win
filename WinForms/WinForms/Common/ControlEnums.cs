
using System;

namespace Nistec.WinForms
{

	[System.ComponentModel.DefaultValue(GradientStyle.TopToBottom)]
	public enum GradientStyle
	{
		/// <summary>
		/// Gradient Angle 0
		/// </summary>
		RightToLeft=0,
		/// <summary>
		/// Gradient Angle 90
		/// </summary>
		BottomToTop=90,
		/// <summary>
		/// Gradient Angle 180
		/// </summary>
		LeftToRight=180,
		/// <summary>
		/// Gradient Angle 270 : Default
		/// </summary>
		TopToBottom=270
	}

	public enum DrawItemStyle
	{
		Default=0,
		LinkLabel=1,
		CheckBox=2
	}

    //public enum ComboStyles
    //{
    //    Button,
    //    Label,
    //    Simple,
    //    ButtonXp,
    //}

	public enum ButtonAlign
	{
		Right=0,
		Left=1
	}

	public enum GuideOptions
	{
		Default,
		Forces,
		Release,
	}
	public enum ImageIDEStrip
	{
		Close = 0,
		Maximize = 1,
		Restore = 2,
		AutoHideOff = 3,
		HutoHideOn = 4
	}


	public enum PropogateName
	{
		BackColor,
		ActiveColor,
		ActiveTextColor,
		InactiveTextColor,
		ResizeBarColor,
		ResizeBarVector,
		CaptionFont,
		TabControlFont,
		ZoneMinMax,
		PlainTabBorder
	}
	public enum McMousePosition
	{
		// Fields
		HTBORDER = 0x12,
		HTBOTTOM = 15,
		HTBOTTOMLEFT = 0x10,
		HTBOTTOMRIGHT = 0x11,
		HTCAPTION = 2,
		HTCLIENT = 1,
		HTCLOSE = 20,
		HTERROR = -2,
		HTGROWBOX = 4,
		HTHELP = 0x15,
		HTHSCROLL = 6,
		HTLEFT = 10,
		HTMAXBUTTON = 9,
		HTMENU = 5,
		HTMINBUTTON = 8,
		HTNOWHERE = 0,
		HTOBJECT = 0x13,
		HTRIGHT = 11,
		HTSIZE = 4,
		HTSYSMENU = 3,
		HTTOP = 12,
		HTTOPLEFT = 13,
		HTTOPRIGHT = 14,
		HTTRANSPARENT = -1,
		HTVSCROLL = 7
	}


	#region Structs

    //public struct ListItem
    //{
    //    public object ValueMember;
    //    public string DisplyMember;
    //    public ListItem(object valueMember,string displyMember)
    //    {
    //        ValueMember = valueMember;
    //        DisplyMember = displyMember;
    //    }
    //    public bool IsEmpty
    //    {
    //        get { return ValueMember == null; }
    //    }
    //    public static readonly ListItem Empty = new ListItem();
    //}
	#endregion

	#region Data

	/// <summary>
	/// Enum Accept Changes position
	/// </summary>
	public enum AcceptChanges
	{   
		/// <summary>
		/// Accept Changes
		/// </summary>
		yes,
		/// <summary>
		/// Cancel Changes
		/// </summary>
		No,
		/// <summary>
		/// Stay current
		/// </summary>
		Cancel
	}

	/// <summary>
	/// Enum Changes Status position
	/// </summary>
	public enum ChangesStatus
	{
		/// <summary>
		/// No Changes
		/// </summary>
		None,
		/// <summary>
		/// Continue with process
		/// </summary>
		Continue,
		/// <summary>
		/// Stop process
		/// </summary>
		Stop
	}


	#endregion

	public enum ButtonStates
	{
		Normal,
		MouseOver,
		Pushed,
        Focused
	}

	public enum MaskTypes
	{
		None=0,
		Numeric=1,
		Date=2,
		Time=3,
		Custom=4
	}

	public enum ListItemMember
	{
		ValueMember,
		DisplyMember
	}

	public enum MultiType
	{
		Text,
		Combo,
		Date,
		Number,
		Boolean,
		Color,
		Font,
		Brows,
		BrowsFolder,
		Explorer,
        Memo,
		Custom
	}
}



