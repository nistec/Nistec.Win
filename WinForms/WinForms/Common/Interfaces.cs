using System;
using System.Drawing;
using System.Windows.Forms;
using Nistec.Collections;


namespace Nistec.WinForms
{

	#region IMcBase

	//m_Style.DrawCheckBoxColor(g,rect,checkRect,m_BorderStyle,Enabled,focused,hot,Checked,CheckBoxTypes.McCheckBox);  

	public interface ICheckBox 
	{

		string  Text {get ;set ;}
		Font  Font {get ;set ;}
		System.Windows.Forms.BorderStyle BorderStyle {get ;set ;}
		bool IsMouseHover {get ;}
		bool Enabled {get ;set ;}
		bool Checked {get ;set ;}
		bool Focused {get ;}
		bool ReadOnly {get ;set ;}
		CheckBoxTypes CheckBoxType {get;}
		Rectangle CheckRect ();
		Appearance Appearance  {get ;set ;}
        ControlLayout ControlLayout { get;set;}
        Color BackColor { get;set;}
        Color ForeColor { get;set;}
	}

	public interface ILabel 
	{
		string  Text {get ;set ;}
		Font  Font {get ;set ;}
		Image  Image {get ;set ;}
		System.Windows.Forms.BorderStyle BorderStyle {get ;set ;}
		ContentAlignment TextAlign {get ;set ;}
		ContentAlignment ImageAlign {get ;set ;}
	}

	public interface IButton 
	{
		string  Text {get ;set ;}
		Font  Font {get ;set ;}
		Image  Image {get ;set ;}
        RightToLeft RightToLeft { get;set;}
		bool IsMouseHover {get ;}
		bool Enabled {get ;set ;}
		ContentAlignment TextAlign {get ;set ;}
		ContentAlignment ImageAlign {get ;set ;}
        ButtonStates ButtonState { get;}
		bool Focused {get ;}
		ImageList  ImageList {get ;set ;}
		int  ImageIndex {get ;set ;}
        Region Region { get;set;}
        int Width { get;set;}
        int Height { get;set;}
  
		//IButtonControl
		//bool IsDefault {get ;set ;}
		//System.Windows.Forms.DialogResult DialogResult {get ;set ;}
		//void NotifyDefault(bool value);
		//void PerformClick();
	}

	public interface IStatusBar 
	{
		string  Text {get ;set ;}
		Font  Font {get ;set ;}
		//System.Windows.Forms.BorderStyle BorderStyle {get ;set ;}
		bool  ShowPanels {get ;set ;}
		System.Windows.Forms.StatusBar.StatusBarPanelCollection Panels {get;}
		System.Windows.Forms.RightToLeft RightToLeft {get ;set ;}
		int Left {get;}
		int Height {get;}
	}

	public interface IWinMc 
	{

		string  Text {get ;set ;}

		System.Windows.Forms.BorderStyle BorderStyle {get ;set ;}

		Image Image {get ;set ;}
		
		bool IsMouseHover {get ;}

		bool Enabled {get ;set ;}

        //McStates McState {get ;set ;}
	}

	public interface IMcBase 
	{

		string  Text {get ;set ;}

		System.Windows.Forms.BorderStyle BorderStyle {get ;set ;}
		
		bool FixSize {get ;set ;}

		Image Image {get ;set ;}

		bool Enabled {get ;set ;}

		bool IsMouseHover {get ;}

	}

	public interface IMcList 
	{

		string  Text {get ;set ;}
		Font  Font {get ;set ;}
		//bool  Enabled {get ;set ;}
		System.Windows.Forms.BorderStyle BorderStyle {get ;set ;}
 		ImageList ImageList {get ;set ;}
		//int ImageIndex {get ;set ;}
		RightToLeft RightToLeft{get ;set ;}
		DrawItemStyle DrawItemStyle{get ;set ;}

	}

	public interface IComboList 
	{
		string  Text {get ;set ;}
		object SelectedItem {get;set;}
		object SelectedValue {get;set;}
		int SelectedIndex {get;set;}
		object DataSource {get;set;}
		string ValueMember {get;set;}
		string DisplayMember {get;set;}
	}

	#endregion

	#region IMcEdit
	public interface IMcEdit 
	{

		string  Text {get ;set ;}

		System.Windows.Forms.BorderStyle BorderStyle {get ;set ;}

	}
	#endregion

	#region IMcTextBox
	
	public interface IMcTextBox
	{
	
		void AppendFormatText(string s);
		void AppendText(string s);
		void ResetText();
	
		string Format {get ;set ;}

//		string ErrorMessage {get ;set ;}
//
//		bool Required {get ;set ;}

		string Text {get ;set ;}

		bool ReadOnly {get ;set ;}


		bool CausesValidation {get ;set ;}
		
		
	}
	#endregion

	#region IControlBase
//	public interface IControlBase 
//	{
//		
//		StyleDesigner Style
//		{
//			get; set;
//		}
//
//		System.Windows.Forms.BorderStyle BorderStyle
//		{
//			get ;set ;
//		}
//
//		Image Image
//		{
//			get; set;
//		}
//
//		bool ShowErrorProvider
//		{
//			get ;set ;
//		}
//
//		System.Windows.Forms.ErrorProvider ErrorBox
//		{
//			get; 
//		}
//		
//		bool IsMouseHover
//		{
//			get;
//		}
//
//	}
	#endregion

	#region ICombo

//	public interface ICombo
//	{
//		//System.Windows.Forms.McListBox InternalList {get;}
//
//		//bool DroppedDown {get;}
//	
//		string Text {get;set;}
//		
//		object SelectedItem {get;set;}
//
//		object SelectedValue {get;set;}
//
//		int SelectedIndex {get;set;}
//
//	
//	}
	#endregion

	#region IDatePicker
	public interface IDatePicker
	{

		string Text	{get;set;}
		string Format	{get;set;}

		//StyleDesigner StyleMc{get;set;}
		IStyleLayout LayoutManager{get;}

		CalendarPicker Calendar	{get;}
	}
	#endregion

	#region IDropDown
	public interface IDropDown
	{
		void DoDropDown();
		void CloseDropDown();
		bool DroppedDown {get;set;}
	}
	#endregion

    #region IPopUp

    public interface IMemo
    {
        string Text { get;set;}
        IStyleLayout LayoutManager { get;}
        IStyle StylePainter { get;}
        Size PopupSize { get;set; }
        int Width { get;set; }
        RightToLeft RightToLeft { get;set; }
        Font Font { get;set; }
        bool ReadOnly { get;set; }
        bool AutoSave { get;set; }
        ScrollBars ScrollBars { get;set; }
    }
    #endregion


	#region IField

	public interface IResource
	{
		string Text {get;set;}
	}
	#endregion

	#region IPanleBase

	public interface IPanel
	{
		//StyleMcDesigner Style{get ;set;}
		System.Windows.Forms.BorderStyle BorderStyle{get ;set;}
		Color BackColor{get ;set;} 
	}

//	public interface IPanel
//	{
//		StyleMcDesigner StyleMc{get ;set;}
//		System.Windows.Forms.BorderStyle BorderStyle{get ;set;}
//		Color BackColor{get ;set;} 
//
//	}
	#endregion

	#region ITextBoxBase
	
	public interface ITextBoxBase
	{
	
		void AppendFormatText(string s);
		void AppendText(string s);
		void ResetText();
	
		string Format
		{
			get ;set ;
		}

		string ErrorMessage
		{
			get ;set ;
		}

		bool Required
		{
			get ;set ;
		}

		string Text
		{
			get ;set ;
		}

		bool ReadOnly
		{
			get ;set ;
		}

		//		HorizontalAlignment TextAlign
		//		{
		//			get ;set ;
		//		}
		//
		//		bool CausesValidation
		//		{
		//			get ;set ;
		//		}
		
		
	}
	#endregion

	#region Style

//	public interface ILayout
//	{
//		void SetStyleLayout(Styles value);
//		void SetStyleLayout(StyleLayout value);
//		IStyleGuide StyleGuide{get;set;}
//		IStyleLayout LayoutManager{get;}
//		PainterTypes PainterType{get;}
//		IStyle StylePainter {get;set;}
//	}

//	public interface IStyleGuide//:IStyleLayout
//	{
//		Color BackColor{get;set;}
//		Color ForeColor{get;set;}
//		Color BorderColor{get;set;}
//		Color BorderHotColor{get;set;}
//		Color FocusedColor{get;set;}
//		Color BackgroundColor{get;set;}
//		Color ColorBrush1{get;set;}
//		Color ColorBrush2{get;set;}
//		Color DisableColor{get;set;}
//		Color AlternatingColor{get;set;}
//		Color CaptionColor{get;set;}
//		Color FormColor{get;set;}
//		Color HighlightColor{get;}
//		Color ContentColor{get;}
//		Color SelectedColor{get;}
//
//		Styles StylePlan{get;set;}
//		StyleGuide GetStyleGuide(Form form);
//		StyleGuide GetCurrentGuide();
//		//IStyleLayout StyleBase{get;}
//		StyleLayout Layout{get;}
//		void SetStyleLayout(StyleLayout value);
//		void SetStyleLayout(Styles value);
//
//		event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
//	}

	#endregion

	#region Panels

	public interface IHookedControl
	{
		object HookControl();
	}

	public interface IHookedPopupControl
	{
		object HookPopupControl();
	}
	#endregion

	#region IMenu
	public interface IMenu 
	{
		void DrawItem(object sender, DrawItemEventArgs e);
		void MeasureItem(object sender, MeasureItemEventArgs e);
		int GetImageIndex(MenuItem control);
		ImageList GetImageList();
	
		RightToLeft RightToLeft{get;set;}
		//StyleMenuDesigner StyleMc{get;set;}
		void SetStyleLayout(StyleLayout value);
		//Styles GetMcStyle();
		//IStyleGuide StyleGuide{get;set;}

	}
	#endregion

    #region    IMcToolButton
    
    public interface IMcToolButton
    {
        System.Windows.Forms.Control Parent { get;set;}
        string Text { get;set;}
        object Tag { get;set;}
        bool Checked { get;set;}
        string Name { get;}
        ToolButtonStyle ButtonStyle { get;set;}
        DrawItemStyle DrawItemStyle { get;set;}
        PopUpItemsCollection MenuItems { get;}
        PopUpItem SelectedPopUpItem { get;}

    }

    #endregion

    #region IToolBar

    public interface IToolBar
    {
        void InvokeButtonClick(McToolButton button);
        event ToolButtonClickEventHandler ButtonClick;

        System.Windows.Forms.Control.ControlCollection Controls { get; }

        Color BackColor { get; set;}
        BorderStyle BorderStyle { get; set;}
        ControlLayout ControlLayout { get; set;}
        DockStyle Dock { get; set;}
        bool FixSize { get; set;}
        System.Drawing.Font Font { get; set;}
        Color ForeColor { get; set;}
        Point Location { get; set;}
        String Name { get; set;}
        Padding Padding { get; set;}
        Size Size { get; set;}
        int TabIndex { get; set;}
        void ResumeLayout(bool performLayout);
        void SuspendLayout();
        int SelectedGroup { get; set;}
        IStyle StylePainter { get; set;}
    }
    #endregion

    #region IBind

    public interface IBind
	{
      string BindPropertyName();
	  bool ReadOnly {get;set;}
 	  BindingFormat BindFormat{get;}
      void BindDefaultValue();
      event EventHandler TextChanged;
  }

	#endregion

}

namespace Nistec.WinForms
{
	

	#region IFormBase
	
	public interface IFormBase
	{

		IStyle StylePainter{get;set;}
		//Styles Style	{get;set;}

		void SetStyleLayout(StyleLayout value);
		//void SetStyleLayout();
		//void SetStyleLayout(StyleGuide sg);
		//Styles GetMcStyle();
		//StyleGuide GetStyleGuide();


	}
	#endregion

}