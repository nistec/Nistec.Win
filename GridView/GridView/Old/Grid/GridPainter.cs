using System;
using System.ComponentModel;
using mControl.WinCtl.Controls;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;   
using System.Text;


namespace mControl.GridStyle
{

	public class GridStyleLayout:StyleLayout,IStyleGrid//IGridLayout
	{

		public GridStyleLayout()
		{
			//FlatLayout=FlatLayout.Dark;
		}

		public PainterTypes PainterType
		{
			get{return PainterTypes.Grid;}
		}

		#region Properties

//		public FlatLayout FlatLayout
//		{
//			get {return base.FlatLayoutInternal;}
//			set
//			{
//				base.FlatLayoutInternal=value;
//				//ctl.BorderColor=value; 
//				OnPropertyChanged("FlatLayout");
//			}
//		}

		/// <summary>
		/// BorderColor
		/// </summary>
		public Color BorderColor
		{
			get {return base.BorderColorInternal;}
			set
			{
				base.BorderColorInternal=value;
				//ctl.BorderColor=value; 
				OnPropertyChanged("BorderColor");
			}
		}

		/// <summary>
		/// BorderHotColor
		/// </summary>
		public Color BorderHotColor
		{
			get {return base.BorderHotColorInternal;}
			set
			{
				base.BorderHotColorInternal=value;
				//ctl.BorderHotColor=value;
				OnPropertyChanged("BorderHotColor");
			}
		}

		/// <summary>
		/// CaptionColor
		/// </summary>
		public Color CaptionBackColor
		{
			get {return base.CaptionColorInternal;}
			set
			{
				base.CaptionColorInternal=value;
				//ctl.CaptionBackColor=value;
				OnPropertyChanged("CaptionBackColor");
			}
		}

		/// <summary>
		/// ColorBrush2
		/// </summary>
		public Color CaptionForeColor
		{
			get {return base.ColorBrush2Internal;}
			set
			{
				base.ColorBrush2Internal=value;
				//ctl.CaptionForeColor=value;
				OnPropertyChanged("CaptionForeColor");
			}
		}

		/// <summary>
		/// AlternatingColor
		/// </summary>
		public Color AlternatingColor
		{
			get {return base.AlternatingColorInternal;}
			set
			{
				base.AlternatingColorInternal=value;
				//ctl.CaptionForeColor=value;
				OnPropertyChanged("AlternatingColor");
			}
		}

		/// <summary>
		/// BorderColor
		/// </summary>
		public Color HeaderBackColor
		{
			get 
			{
				//if(FlatLayoutInternal==FlatLayout.Dark)
				//	return base.CaptionColorInternal;
				//else
				//	return base.ColorBrush1Internal;
				return base.CaptionColorInternal;//.BorderColorInternal;}
			}
		}

		/// <summary>
		/// LightLightColor
		/// </summary>
		public Color HeaderForeColor
		{
			get 
			{
				//if(FlatLayoutInternal==FlatLayout.Dark)
				//	return base.LightLightColor;
				//else
				//	return base.FocusedColorInternal;
				return base.LightLightColor;
			}
		}

		/// <summary>
		/// DarkColor
		/// </summary>
		public Color SelectionBackColor
		{
			get{return base.BorderColorInternal;}//DarkColor;}
		    
		}

		/// <summary>
		/// BackColor
		/// </summary>
		public Color SelectionForeColor
		{
			get {return base.BackColorInternal;}
		}

		/// <summary>
		/// BackColor
		/// </summary>
		public Color BackColor
		{
			get {return base.BackColorInternal;}
			set
			{
				base.BackColorInternal=value;
				//ctl.GridBackColor=value;
				OnPropertyChanged("BackColor");
			}
		}

		/// <summary>
		/// ForeColor
		/// </summary>
		public Color ForeColor
		{
			get {return base.ForeColorInternal;}
			set
			{
				base.ForeColorInternal=value;
				//ctl.GridForeColor=value;
				OnPropertyChanged("ForeColor");
			}
		}

//		public Color ColorBrush1
//		{
//			get {return base.ColorBrush1Internal;}
//		}
//		public Color ColorBrush2
//		{
//			get {return base.ColorBrush2Internal;}
//		}
//		public Color FlatColor
//		{
//			get {return base.FlatColorInternal;}
//		}
//		public Color FocusedColor
//		{
//			get {return base.FocusedColorInternal;}
//		}

		#endregion
        
	}

		#region Interfaces

//		public interface IGridLayout
//		{
//			//Styles StylePlan {get;set;}
//			StyleLayout Layout {get;}
//
//			Color BorderColor {get;set;}
//			Color BorderHotColor {get;set;}
//			//Color CaptionBackColor {get;set;}
//			//Color CaptionForeColor {get;set;}
//			Color AlternatingColor {get;set;}
//			Color GridBackColor {get;set;}
//			Color GridForeColor {get;set;}
//			Color HeaderBackColor {get;}
//			Color HeaderForeColor {get;}
//			Color SelectionBackColor {get;}
//			Color SelectionForeColor {get;}
//
////			Color ColorBrush1{get;}
////			Color ColorBrush2{get;}
////			Color FlatColor{get;}
////			Color FocusedColor{get;}
//
//		}

		#endregion

}
