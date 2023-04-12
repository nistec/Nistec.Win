using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Nistec.Printing.View.Viewer
{
	/// <summary>
	/// Summary description for ViewerBarDesigner.
	/// </summary>
	public class ViewerBarDesigner : System.Windows.Forms.Form
	{
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ToolBar ViewerBar;

		#region ViewerBarMembers

		private System.Windows.Forms.ToolBarButton tbPrint;
		private System.Windows.Forms.ToolBarButton tbExport;
		private System.Windows.Forms.ToolBarButton tbFind;
		private System.Windows.Forms.ToolBarButton tbPageOne;
		private System.Windows.Forms.ToolBarButton tbPageTwo;
		private System.Windows.Forms.ToolBarButton tbPageThree;
		private System.Windows.Forms.ToolBarButton tbPageFour;
		private System.Windows.Forms.ToolBarButton tbPageSix;
		private System.Windows.Forms.ToolBarButton tbZoomIn;
		private System.Windows.Forms.ToolBarButton tbZoomOut;
		private System.Windows.Forms.ToolBarButton tbZoom;
		private System.Windows.Forms.ToolBarButton tbNext;
		private System.Windows.Forms.ToolBarButton tbPrev;
		private System.Windows.Forms.ToolBarButton tbFirst;
		private System.Windows.Forms.ToolBarButton tbLast;
		private System.Windows.Forms.ToolBarButton tbClose;
		private System.Windows.Forms.ToolBarButton tbGroup1;
		private System.Windows.Forms.ToolBarButton tbGroup2;
		private System.Windows.Forms.ToolBarButton tbGroup3;
		private System.Windows.Forms.ToolBarButton tbGroup4;
		
		private System.Windows.Forms.ContextMenu ZoomDropDown;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem menuItem10;
		//private System.Windows.Forms.TextBox txtCurrentPage;

		public event EventHandler DropDownChange;
		private int DropDownIndex;
		private System.Windows.Forms.ImageList ImagesDesigner;
		private string DropDownText;

		#endregion

		public ViewerBarDesigner()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ViewerBarDesigner));
			this.ViewerBar = new System.Windows.Forms.ToolBar();
			this.tbPrint = new System.Windows.Forms.ToolBarButton();
			this.tbExport = new System.Windows.Forms.ToolBarButton();
			this.tbFind = new System.Windows.Forms.ToolBarButton();
			this.tbGroup1 = new System.Windows.Forms.ToolBarButton();
			this.tbPageOne = new System.Windows.Forms.ToolBarButton();
			this.tbPageTwo = new System.Windows.Forms.ToolBarButton();
			this.tbPageThree = new System.Windows.Forms.ToolBarButton();
			this.tbPageFour = new System.Windows.Forms.ToolBarButton();
			this.tbPageSix = new System.Windows.Forms.ToolBarButton();
			this.tbGroup2 = new System.Windows.Forms.ToolBarButton();
			this.tbZoomIn = new System.Windows.Forms.ToolBarButton();
			this.tbZoomOut = new System.Windows.Forms.ToolBarButton();
			this.tbZoom = new System.Windows.Forms.ToolBarButton();
			this.ZoomDropDown = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.menuItem10 = new System.Windows.Forms.MenuItem();
			this.tbGroup3 = new System.Windows.Forms.ToolBarButton();
			this.tbClose = new System.Windows.Forms.ToolBarButton();
			this.tbGroup4 = new System.Windows.Forms.ToolBarButton();
			this.tbPrev = new System.Windows.Forms.ToolBarButton();
			this.tbNext = new System.Windows.Forms.ToolBarButton();
			this.tbFirst = new System.Windows.Forms.ToolBarButton();
			this.tbLast = new System.Windows.Forms.ToolBarButton();
			this.ImagesDesigner = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// ViewerBar
			// 
			this.ViewerBar.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
			this.ViewerBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						 this.tbPrint,
																						 this.tbExport,
																						 this.tbFind,
																						 this.tbGroup1,
																						 this.tbPageOne,
																						 this.tbPageTwo,
																						 this.tbPageThree,
																						 this.tbPageFour,
																						 this.tbPageSix,
																						 this.tbGroup2,
																						 this.tbZoomIn,
																						 this.tbZoomOut,
																						 this.tbZoom,
																						 this.tbGroup3,
																						 this.tbClose,
																						 this.tbGroup4,
																						 this.tbFirst,
																						 this.tbPrev,
																						 this.tbNext,
																						 this.tbLast});
			this.ViewerBar.DropDownArrows = true;
			this.ViewerBar.ImageList = this.ImagesDesigner;
			this.ViewerBar.Location = new System.Drawing.Point(0, 0);
			this.ViewerBar.Name = "ViewerBar";
			this.ViewerBar.ShowToolTips = true;
			this.ViewerBar.Size = new System.Drawing.Size(624, 28);
			this.ViewerBar.TabIndex = 0;
			// 
			// tbPrint
			// 
			this.tbPrint.ImageIndex = 0;
			this.tbPrint.Tag = "Print";
			this.tbPrint.ToolTipText = "Print";
			// 
			// tbExport
			// 
			this.tbExport.ImageIndex = 1;
			this.tbExport.Tag = "Export";
			this.tbExport.ToolTipText = "Export";
			// 
			// tbFind
			// 
			this.tbFind.ImageIndex = 2;
			this.tbFind.Tag = "Find";
			this.tbFind.ToolTipText = "Find";
			// 
			// tbGroup1
			// 
			this.tbGroup1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// tbPageOne
			// 
			this.tbPageOne.ImageIndex = 3;
			this.tbPageOne.Tag = "OnePage";
			this.tbPageOne.ToolTipText = "One Page";
			// 
			// tbPageTwo
			// 
			this.tbPageTwo.ImageIndex = 4;
			this.tbPageTwo.Tag = "TwoPages";
			this.tbPageTwo.ToolTipText = "Two Pages";
			// 
			// tbPageThree
			// 
			this.tbPageThree.ImageIndex = 5;
			this.tbPageThree.Tag = "ThreePages";
			this.tbPageThree.ToolTipText = "Three Pages";
			// 
			// tbPageFour
			// 
			this.tbPageFour.ImageIndex = 6;
			this.tbPageFour.Tag = "FourPages";
			this.tbPageFour.ToolTipText = "Four Pages";
			// 
			// tbPageSix
			// 
			this.tbPageSix.ImageIndex = 7;
			this.tbPageSix.Tag = "SixPages";
			this.tbPageSix.ToolTipText = "Six pages";
			// 
			// tbGroup2
			// 
			this.tbGroup2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// tbZoomIn
			// 
			this.tbZoomIn.ImageIndex = 8;
			this.tbZoomIn.Tag = "ZoomIn";
			this.tbZoomIn.ToolTipText = "Zoom In";
			// 
			// tbZoomOut
			// 
			this.tbZoomOut.ImageIndex = 9;
			this.tbZoomOut.Tag = "ZoomOut";
			this.tbZoomOut.ToolTipText = "Zoom Out";
			// 
			// tbZoom
			// 
			this.tbZoom.DropDownMenu = this.ZoomDropDown;
			this.tbZoom.ImageIndex = 10;
			this.tbZoom.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton;
			this.tbZoom.Tag = "Zoom";
			// 
			// ZoomDropDown
			// 
			this.ZoomDropDown.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem1,
																						 this.menuItem2,
																						 this.menuItem3,
																						 this.menuItem4,
																						 this.menuItem5,
																						 this.menuItem6,
																						 this.menuItem7,
																						 this.menuItem8,
																						 this.menuItem9,
																						 this.menuItem10});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "10%";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.Text = "25%";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 2;
			this.menuItem3.Text = "50%";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 3;
			this.menuItem4.Text = "75%";
			this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 4;
			this.menuItem5.Text = "100%";
			this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 5;
			this.menuItem6.Text = "200%";
			this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 6;
			this.menuItem7.Text = "400%";
			this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 7;
			this.menuItem8.Text = "800%";
			this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
			// 
			// menuItem9
			// 
			this.menuItem9.Index = 8;
			this.menuItem9.Text = "Page Width";
			this.menuItem9.Click += new System.EventHandler(this.menuItem9_Click);
			// 
			// menuItem10
			// 
			this.menuItem10.Index = 9;
			this.menuItem10.Text = "Full Page";
			this.menuItem10.Click += new System.EventHandler(this.menuItem10_Click);
			// 
			// tbGroup3
			// 
			this.tbGroup3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// tbClose
			// 
			this.tbClose.ImageIndex = 11;
			this.tbClose.Tag = "Close";
			this.tbClose.ToolTipText = "Close";
			// 
			// tbGroup4
			// 
			this.tbGroup4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// tbPrev
			// 
			this.tbPrev.ImageIndex = 13;
			this.tbPrev.Tag = "PrevPage";
			this.tbPrev.ToolTipText = "Previous Page";
			// 
			// tbNext
			// 
			this.tbNext.ImageIndex = 14;
			this.tbNext.Tag = "NextPage";
			this.tbNext.ToolTipText = "Next Page";
			// 
			// tbFirst
			// 
			this.tbFirst.ImageIndex = 12;
			this.tbFirst.Tag = "FirstPage";
			this.tbFirst.ToolTipText = "First page";
			// 
			// tbLast
			// 
			this.tbLast.ImageIndex = 15;
			this.tbLast.Tag = "LastPage";
			this.tbLast.ToolTipText = "Last page";
			// 
			// ImagesDesigner
			// 
			this.ImagesDesigner.ImageSize = new System.Drawing.Size(16, 16);
			this.ImagesDesigner.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImagesDesigner.ImageStream")));
			this.ImagesDesigner.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// ViewerBarDesigner
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(624, 273);
			this.Controls.Add(this.ViewerBar);
			this.Name = "ViewerBarDesigner";
			this.Text = "ViewerBar";
			this.ResumeLayout(false);

		}
		#endregion

		#region Events

		//		private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		//		{
		//			OnButtonClick(e);
		//		}
		//
		//		private void toolBar1_ButtonDropDown(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		//		{
		//			OnButtonDropDown(e);
		//		}

		//		protected override void OnButtonClick(System.Windows.Forms.ToolBarButtonClickEventArgs e)
		//		{
		//		 //
		//		}
		//
		//		protected override void OnButtonDropDown( System.Windows.Forms.ToolBarButtonClickEventArgs e)
		//		{
		//		 base.OnButtonDropDown(e);
		//		}

		private void menuItem1_Click(object sender, EventArgs e)
		{
			OnDropDownChange(e,0,"10%"); 
			this.menuItem1.Checked = true;

		}
		private void menuItem2_Click(object sender, EventArgs e)
		{
			OnDropDownChange(e,1,"25%"); 
			this.menuItem2.Checked = true;
		}
		private void menuItem3_Click(object sender, EventArgs e)
		{
			OnDropDownChange(e,2,"50%"); 
			this.menuItem3.Checked = true;
		}
		private void menuItem4_Click(object sender, EventArgs e)
		{
			OnDropDownChange(e,3,"75%"); 
			this.menuItem4.Checked = true;
		}
		private void menuItem5_Click(object sender, EventArgs e)
		{
			OnDropDownChange(e,4,"100%"); 
			this.menuItem5.Checked = true;
		}
		private void menuItem6_Click(object sender, EventArgs e)
		{
			OnDropDownChange(e,5,"200%"); 
			this.menuItem6.Checked = true;
		}
		private void menuItem7_Click(object sender, EventArgs e)
		{
			OnDropDownChange(e,6,"400%"); 
			this.menuItem7.Checked = true;
		}
		private void menuItem8_Click(object sender, EventArgs e)
		{
			OnDropDownChange(e,7,"800%"); 
			this.menuItem8.Checked = true;
		}
		private void menuItem9_Click(object sender, EventArgs e)
		{
			OnDropDownChange(e,8,this.menuItem9.Text); 
			this.menuItem9.Checked = true;
		}
		private void menuItem10_Click(object sender, EventArgs e)
		{
			OnDropDownChange(e,9,this.menuItem10.Text); 
			this.menuItem10.Checked = true;
		}

		protected virtual void OnDropDownChange(EventArgs e,int Index,string txt)
		{

			if(DropDownButton!=Index)
			{
				switch(DropDownIndex)
				{
					case 0:
						this.menuItem1.Checked = false;
						break;
					case 1:
						this.menuItem2.Checked = false;
						break;
					case 2:
						this.menuItem3.Checked = false;
						break;
					case 3:
						this.menuItem4.Checked = false;
						break;
					case 4:
						this.menuItem5.Checked = false;
						break;
					case 5:
						this.menuItem6.Checked = false;
						break;
					case 6:
						this.menuItem7.Checked = false;
						break;
					case 7:
						this.menuItem8.Checked = false;
						break;
					case 8:
						this.menuItem9.Checked = false;
						break;
					case 9:
						this.menuItem10.Checked = false;
						break;
				}

				DropDownIndex=Index;
				DropDownText=txt;
				if(DropDownChange!=null)
					DropDownChange(this,e);
			}
		}

		#endregion

		#region Properties

		public int DropDownButton
		{
			get{return this.DropDownIndex;}
		}

		public string DropDownSelected
		{
			get{return this.DropDownText;}
		}

		public ToolBar ViewerMenu
		{
			get{return this.ViewerBar;}
		}

		//		public Rectangle LastButton
		//		{
		//			get{return   this.tbNext.Rectangle;}
		//		}
		//		public TextBox CurrentPager
		//		{
		//			get{return   this.txtCurrentPage;}
		//		}

		#endregion
	}
}
