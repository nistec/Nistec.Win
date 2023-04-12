using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Threading;
using System.Text;
using Nistec.WinForms;

namespace Nistec.Wizards
{
   	#region StampActions
    //public enum StampActions
    //{
    //    EditedBy = 1,
    //    DateTime = 2,
    //    Custom = 4
    //}
	#endregion

	/// <summary>
	/// An extended RichTextBox contains toolbar.
	/// </summary>
    [ToolboxItem(true), ToolboxBitmap(typeof(McRichText), "Toolbox.McRichText.bmp")]
    public class McRichText : Nistec.WinForms.Controls.McCustomControl// McUserControl
	{
		//Used for looping
		private RichTextBox rtbTemp = new RichTextBox();
 
		#region Windows Generated
     	private System.Windows.Forms.RichTextBox rtb1;
		private System.Windows.Forms.ImageList imgList1;

        private Nistec.WinForms.McToolBar tb1;
        private Nistec.WinForms.McToolButton tbbSeparator1;
        private Nistec.WinForms.McToolButton tbbOpen;
        private Nistec.WinForms.McToolButton tbbSave;
        private Nistec.WinForms.McToolButton tbbStamp;
        private Nistec.WinForms.McToolButton tbbPaste;
        private Nistec.WinForms.McToolButton tbbCopy;
        private Nistec.WinForms.McToolButton tbbCut;
        private Nistec.WinForms.McToolButton tbbSeparator5;
        private Nistec.WinForms.McToolButton tbbRedo;
        private Nistec.WinForms.McToolButton tbbUndo;
        private Nistec.WinForms.McToolButton tbbSeparator4;
        private Nistec.WinForms.McToolButton tbbRight;
        private Nistec.WinForms.McToolButton tbbCenter;
        private Nistec.WinForms.McToolButton tbbLeft;
        private Nistec.WinForms.McToolButton tbbSeparator3;
        private Nistec.WinForms.McToolButton tbbStrikeout;
        private Nistec.WinForms.McToolButton tbbUnderline;
        private Nistec.WinForms.McToolButton tbbItalic;
        private Nistec.WinForms.McToolButton tbbBold;
        private Nistec.WinForms.McToolButton tbbSeparator2;
        private Nistec.WinForms.McToolButton tbbColor;
        private Nistec.WinForms.McToolButton tbbFontSize;
        private Nistec.WinForms.McToolButton tbbFont;

		private System.Windows.Forms.ContextMenu cmColors;
		private System.Windows.Forms.MenuItem miBlack;
		private System.Windows.Forms.MenuItem miBlue;
		private System.Windows.Forms.MenuItem miRed;
		private System.Windows.Forms.MenuItem miGreen;
		private System.Windows.Forms.OpenFileDialog ofd1;
		private System.Windows.Forms.SaveFileDialog sfd1;
		private System.Windows.Forms.ContextMenu cmFonts;
		private System.Windows.Forms.MenuItem miArial;
		private System.Windows.Forms.MenuItem miGaramond;
		private System.Windows.Forms.MenuItem miTahoma;
		private System.Windows.Forms.MenuItem miTimesNewRoman;
		private System.Windows.Forms.MenuItem miVerdana;
		private System.Windows.Forms.MenuItem miCourierNew;
		private System.Windows.Forms.MenuItem miMicrosoftSansSerif;
		private System.Windows.Forms.ContextMenu cmFontSizes;
		private System.Windows.Forms.MenuItem mi8;
		private System.Windows.Forms.MenuItem mi9;
		private System.Windows.Forms.MenuItem mi10;
		private System.Windows.Forms.MenuItem mi11;
		private System.Windows.Forms.MenuItem mi12;
		private System.Windows.Forms.MenuItem mi14;
		private System.Windows.Forms.MenuItem mi16;
		private System.Windows.Forms.MenuItem mi18;
		private System.Windows.Forms.MenuItem mi20;
		private System.Windows.Forms.MenuItem mi22;
		private System.Windows.Forms.MenuItem mi24;
		private System.Windows.Forms.MenuItem mi26;
		private System.Windows.Forms.MenuItem mi28;
		private System.Windows.Forms.MenuItem mi36;
		private System.Windows.Forms.MenuItem mi48;
		private System.Windows.Forms.MenuItem mi72;
		private System.ComponentModel.IContainer components;

        public McRichText()//:base(true)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			//Update the graphics on the toolbar
			UpdateToolbar();
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
		#endregion

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(McRichText));
            this.tb1 = new Nistec.WinForms.McToolBar();
            this.tbbStamp = new Nistec.WinForms.McToolButton();
            this.imgList1 = new System.Windows.Forms.ImageList(this.components);
            this.tbbPaste = new Nistec.WinForms.McToolButton();
            this.tbbCopy = new Nistec.WinForms.McToolButton();
            this.tbbCut = new Nistec.WinForms.McToolButton();
            this.tbbSeparator5 = new Nistec.WinForms.McToolButton();
            this.tbbRedo = new Nistec.WinForms.McToolButton();
            this.tbbUndo = new Nistec.WinForms.McToolButton();
            this.tbbSeparator4 = new Nistec.WinForms.McToolButton();
            this.tbbRight = new Nistec.WinForms.McToolButton();
            this.tbbCenter = new Nistec.WinForms.McToolButton();
            this.tbbLeft = new Nistec.WinForms.McToolButton();
            this.tbbSeparator3 = new Nistec.WinForms.McToolButton();
            this.tbbStrikeout = new Nistec.WinForms.McToolButton();
            this.tbbUnderline = new Nistec.WinForms.McToolButton();
            this.tbbItalic = new Nistec.WinForms.McToolButton();
            this.tbbBold = new Nistec.WinForms.McToolButton();
            this.tbbSeparator2 = new Nistec.WinForms.McToolButton();
            this.tbbColor = new Nistec.WinForms.McToolButton();
            this.cmColors = new System.Windows.Forms.ContextMenu();
            this.miBlack = new System.Windows.Forms.MenuItem();
            this.miBlue = new System.Windows.Forms.MenuItem();
            this.miRed = new System.Windows.Forms.MenuItem();
            this.miGreen = new System.Windows.Forms.MenuItem();
            this.tbbFontSize = new Nistec.WinForms.McToolButton();
            this.cmFontSizes = new System.Windows.Forms.ContextMenu();
            this.mi8 = new System.Windows.Forms.MenuItem();
            this.mi9 = new System.Windows.Forms.MenuItem();
            this.mi10 = new System.Windows.Forms.MenuItem();
            this.mi11 = new System.Windows.Forms.MenuItem();
            this.mi12 = new System.Windows.Forms.MenuItem();
            this.mi14 = new System.Windows.Forms.MenuItem();
            this.mi16 = new System.Windows.Forms.MenuItem();
            this.mi18 = new System.Windows.Forms.MenuItem();
            this.mi20 = new System.Windows.Forms.MenuItem();
            this.mi22 = new System.Windows.Forms.MenuItem();
            this.mi24 = new System.Windows.Forms.MenuItem();
            this.mi26 = new System.Windows.Forms.MenuItem();
            this.mi28 = new System.Windows.Forms.MenuItem();
            this.mi36 = new System.Windows.Forms.MenuItem();
            this.mi48 = new System.Windows.Forms.MenuItem();
            this.mi72 = new System.Windows.Forms.MenuItem();
            this.tbbFont = new Nistec.WinForms.McToolButton();
            this.cmFonts = new System.Windows.Forms.ContextMenu();
            this.miArial = new System.Windows.Forms.MenuItem();
            this.miCourierNew = new System.Windows.Forms.MenuItem();
            this.miGaramond = new System.Windows.Forms.MenuItem();
            this.miMicrosoftSansSerif = new System.Windows.Forms.MenuItem();
            this.miTahoma = new System.Windows.Forms.MenuItem();
            this.miTimesNewRoman = new System.Windows.Forms.MenuItem();
            this.miVerdana = new System.Windows.Forms.MenuItem();
            this.tbbSeparator1 = new Nistec.WinForms.McToolButton();
            this.tbbOpen = new Nistec.WinForms.McToolButton();
            this.tbbSave = new Nistec.WinForms.McToolButton();
            this.rtb1 = new System.Windows.Forms.RichTextBox();
            this.ofd1 = new System.Windows.Forms.OpenFileDialog();
            this.sfd1 = new System.Windows.Forms.SaveFileDialog();
            this.tb1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tb1
            // 
            this.tb1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb1.ControlLayout = Nistec.WinForms.ControlLayout.Visual;
            this.tb1.Controls.Add(this.tbbStamp);
            this.tb1.Controls.Add(this.tbbPaste);
            this.tb1.Controls.Add(this.tbbCopy);
            this.tb1.Controls.Add(this.tbbCut);
            this.tb1.Controls.Add(this.tbbSeparator5);
            this.tb1.Controls.Add(this.tbbRedo);
            this.tb1.Controls.Add(this.tbbUndo);
            this.tb1.Controls.Add(this.tbbSeparator4);
            this.tb1.Controls.Add(this.tbbRight);
            this.tb1.Controls.Add(this.tbbCenter);
            this.tb1.Controls.Add(this.tbbLeft);
            this.tb1.Controls.Add(this.tbbSeparator3);
            this.tb1.Controls.Add(this.tbbStrikeout);
            this.tb1.Controls.Add(this.tbbUnderline);
            this.tb1.Controls.Add(this.tbbItalic);
            this.tb1.Controls.Add(this.tbbBold);
            this.tb1.Controls.Add(this.tbbSeparator2);
            this.tb1.Controls.Add(this.tbbColor);
            this.tb1.Controls.Add(this.tbbFontSize);
            this.tb1.Controls.Add(this.tbbFont);
            this.tb1.Controls.Add(this.tbbSeparator1);
            this.tb1.Controls.Add(this.tbbOpen);
            this.tb1.Controls.Add(this.tbbSave);
            this.tb1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tb1.FixSize = false;
            this.tb1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tb1.ImageList = this.imgList1;
            this.tb1.Location = new System.Drawing.Point(2, 2);
            this.tb1.Name = "tb1";
            this.tb1.Padding = new System.Windows.Forms.Padding(12, 3, 3, 3);
            this.tb1.Size = new System.Drawing.Size(500, 28);
            this.tb1.TabIndex = 1;
            this.tb1.ButtonClick += new Nistec.WinForms.ToolButtonClickEventHandler(this.tb1_ButtonClick);
            // 
            // tbbStamp
            // 
            this.tbbStamp.ButtonStyle = Nistec.WinForms.ToolButtonStyle.Button;
            this.tbbStamp.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbbStamp.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbbStamp.ImageIndex = 8;
            this.tbbStamp.ImageList = this.imgList1;
            this.tbbStamp.Location = new System.Drawing.Point(459, 3);
            this.tbbStamp.Name = "tbbStamp";
            this.tbbStamp.Size = new System.Drawing.Size(22, 22);
            this.tbbStamp.TabIndex = 22;
            this.tbbStamp.Tag = "edit stamp";
            this.tbbStamp.ToolTipText = "Add Stamp text";
            // 
            // imgList1
            // 
            this.imgList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList1.ImageStream")));
            this.imgList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList1.Images.SetKeyName(0, "");
            this.imgList1.Images.SetKeyName(1, "");
            this.imgList1.Images.SetKeyName(2, "");
            this.imgList1.Images.SetKeyName(3, "");
            this.imgList1.Images.SetKeyName(4, "");
            this.imgList1.Images.SetKeyName(5, "");
            this.imgList1.Images.SetKeyName(6, "");
            this.imgList1.Images.SetKeyName(7, "");
            this.imgList1.Images.SetKeyName(8, "");
            this.imgList1.Images.SetKeyName(9, "");
            this.imgList1.Images.SetKeyName(10, "");
            this.imgList1.Images.SetKeyName(11, "");
            this.imgList1.Images.SetKeyName(12, "");
            this.imgList1.Images.SetKeyName(13, "");
            this.imgList1.Images.SetKeyName(14, "");
            this.imgList1.Images.SetKeyName(15, "");
            this.imgList1.Images.SetKeyName(16, "");
            this.imgList1.Images.SetKeyName(17, "");
            this.imgList1.Images.SetKeyName(18, "");
            this.imgList1.Images.SetKeyName(19, "");
            this.imgList1.Images.SetKeyName(20, "");
            // 
            // tbbPaste
            // 
            this.tbbPaste.ButtonStyle = Nistec.WinForms.ToolButtonStyle.Button;
            this.tbbPaste.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbbPaste.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbbPaste.ImageIndex = 19;
            this.tbbPaste.ImageList = this.imgList1;
            this.tbbPaste.Location = new System.Drawing.Point(437, 3);
            this.tbbPaste.Name = "tbbPaste";
            this.tbbPaste.Size = new System.Drawing.Size(22, 22);
            this.tbbPaste.TabIndex = 21;
            this.tbbPaste.Tag = "paste";
            this.tbbPaste.ToolTipText = "Paste";
            // 
            // tbbCopy
            // 
            this.tbbCopy.ButtonStyle = Nistec.WinForms.ToolButtonStyle.Button;
            this.tbbCopy.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbbCopy.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbbCopy.ImageIndex = 18;
            this.tbbCopy.ImageList = this.imgList1;
            this.tbbCopy.Location = new System.Drawing.Point(415, 3);
            this.tbbCopy.Name = "tbbCopy";
            this.tbbCopy.Size = new System.Drawing.Size(22, 22);
            this.tbbCopy.TabIndex = 20;
            this.tbbCopy.Tag = "copy";
            this.tbbCopy.ToolTipText = "Copy";
            // 
            // tbbCut
            // 
            this.tbbCut.ButtonStyle = Nistec.WinForms.ToolButtonStyle.Button;
            this.tbbCut.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbbCut.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbbCut.ImageIndex = 17;
            this.tbbCut.ImageList = this.imgList1;
            this.tbbCut.Location = new System.Drawing.Point(393, 3);
            this.tbbCut.Name = "tbbCut";
            this.tbbCut.Size = new System.Drawing.Size(22, 22);
            this.tbbCut.TabIndex = 19;
            this.tbbCut.Tag = "cut";
            this.tbbCut.ToolTipText = "Cut";
            // 
            // tbbSeparator5
            // 
            this.tbbSeparator5.ButtonStyle = Nistec.WinForms.ToolButtonStyle.Separator;
            this.tbbSeparator5.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbbSeparator5.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbbSeparator5.Location = new System.Drawing.Point(385, 3);
            this.tbbSeparator5.Name = "tbbSeparator5";
            this.tbbSeparator5.Size = new System.Drawing.Size(8, 22);
            this.tbbSeparator5.TabIndex = 18;
            this.tbbSeparator5.ToolTipText = "";
            // 
            // tbbRedo
            // 
            this.tbbRedo.ButtonStyle = Nistec.WinForms.ToolButtonStyle.Button;
            this.tbbRedo.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbbRedo.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbbRedo.ImageIndex = 13;
            this.tbbRedo.ImageList = this.imgList1;
            this.tbbRedo.Location = new System.Drawing.Point(363, 3);
            this.tbbRedo.Name = "tbbRedo";
            this.tbbRedo.Size = new System.Drawing.Size(22, 22);
            this.tbbRedo.TabIndex = 17;
            this.tbbRedo.Tag = "redo";
            this.tbbRedo.ToolTipText = "Redo";
            // 
            // tbbUndo
            // 
            this.tbbUndo.ButtonStyle = Nistec.WinForms.ToolButtonStyle.Button;
            this.tbbUndo.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbbUndo.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbbUndo.ImageIndex = 12;
            this.tbbUndo.ImageList = this.imgList1;
            this.tbbUndo.Location = new System.Drawing.Point(341, 3);
            this.tbbUndo.Name = "tbbUndo";
            this.tbbUndo.Size = new System.Drawing.Size(22, 22);
            this.tbbUndo.TabIndex = 16;
            this.tbbUndo.Tag = "undo";
            this.tbbUndo.ToolTipText = "Undo";
            // 
            // tbbSeparator4
            // 
            this.tbbSeparator4.ButtonStyle = Nistec.WinForms.ToolButtonStyle.Separator;
            this.tbbSeparator4.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbbSeparator4.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbbSeparator4.Location = new System.Drawing.Point(333, 3);
            this.tbbSeparator4.Name = "tbbSeparator4";
            this.tbbSeparator4.Size = new System.Drawing.Size(8, 22);
            this.tbbSeparator4.TabIndex = 15;
            this.tbbSeparator4.ToolTipText = "";
            // 
            // tbbRight
            // 
            this.tbbRight.ButtonStyle = Nistec.WinForms.ToolButtonStyle.CheckButton;
            this.tbbRight.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbbRight.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbbRight.ImageIndex = 6;
            this.tbbRight.ImageList = this.imgList1;
            this.tbbRight.Location = new System.Drawing.Point(311, 3);
            this.tbbRight.Name = "tbbRight";
            this.tbbRight.Size = new System.Drawing.Size(22, 22);
            this.tbbRight.TabIndex = 14;
            this.tbbRight.Tag = "right";
            this.tbbRight.ToolTipText = "Right";
            // 
            // tbbCenter
            // 
            this.tbbCenter.ButtonStyle = Nistec.WinForms.ToolButtonStyle.CheckButton;
            this.tbbCenter.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbbCenter.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbbCenter.ImageIndex = 5;
            this.tbbCenter.ImageList = this.imgList1;
            this.tbbCenter.Location = new System.Drawing.Point(289, 3);
            this.tbbCenter.Name = "tbbCenter";
            this.tbbCenter.Size = new System.Drawing.Size(22, 22);
            this.tbbCenter.TabIndex = 13;
            this.tbbCenter.Tag = "center";
            this.tbbCenter.ToolTipText = "Center";
            // 
            // tbbLeft
            // 
            this.tbbLeft.ButtonStyle = Nistec.WinForms.ToolButtonStyle.CheckButton;
            this.tbbLeft.Checked = true;
            this.tbbLeft.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbbLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbbLeft.ImageIndex = 4;
            this.tbbLeft.ImageList = this.imgList1;
            this.tbbLeft.Location = new System.Drawing.Point(267, 3);
            this.tbbLeft.Name = "tbbLeft";
            this.tbbLeft.Size = new System.Drawing.Size(22, 22);
            this.tbbLeft.TabIndex = 12;
            this.tbbLeft.Tag = "left";
            this.tbbLeft.ToolTipText = "Left";
            // 
            // tbbSeparator3
            // 
            this.tbbSeparator3.ButtonStyle = Nistec.WinForms.ToolButtonStyle.Separator;
            this.tbbSeparator3.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbbSeparator3.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbbSeparator3.Location = new System.Drawing.Point(259, 3);
            this.tbbSeparator3.Name = "tbbSeparator3";
            this.tbbSeparator3.Size = new System.Drawing.Size(8, 22);
            this.tbbSeparator3.TabIndex = 11;
            this.tbbSeparator3.ToolTipText = "";
            // 
            // tbbStrikeout
            // 
            this.tbbStrikeout.ButtonStyle = Nistec.WinForms.ToolButtonStyle.CheckButton;
            this.tbbStrikeout.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbbStrikeout.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbbStrikeout.ImageIndex = 3;
            this.tbbStrikeout.ImageList = this.imgList1;
            this.tbbStrikeout.Location = new System.Drawing.Point(237, 3);
            this.tbbStrikeout.Name = "tbbStrikeout";
            this.tbbStrikeout.Size = new System.Drawing.Size(22, 22);
            this.tbbStrikeout.TabIndex = 10;
            this.tbbStrikeout.Tag = "strikeout";
            this.tbbStrikeout.ToolTipText = "Strikeout";
            // 
            // tbbUnderline
            // 
            this.tbbUnderline.ButtonStyle = Nistec.WinForms.ToolButtonStyle.CheckButton;
            this.tbbUnderline.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbbUnderline.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbbUnderline.ImageIndex = 2;
            this.tbbUnderline.ImageList = this.imgList1;
            this.tbbUnderline.Location = new System.Drawing.Point(215, 3);
            this.tbbUnderline.Name = "tbbUnderline";
            this.tbbUnderline.Size = new System.Drawing.Size(22, 22);
            this.tbbUnderline.TabIndex = 9;
            this.tbbUnderline.Tag = "underline";
            this.tbbUnderline.ToolTipText = "Underline";
            // 
            // tbbItalic
            // 
            this.tbbItalic.ButtonStyle = Nistec.WinForms.ToolButtonStyle.CheckButton;
            this.tbbItalic.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbbItalic.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbbItalic.ImageIndex = 1;
            this.tbbItalic.ImageList = this.imgList1;
            this.tbbItalic.Location = new System.Drawing.Point(193, 3);
            this.tbbItalic.Name = "tbbItalic";
            this.tbbItalic.Size = new System.Drawing.Size(22, 22);
            this.tbbItalic.TabIndex = 8;
            this.tbbItalic.Tag = "italic";
            this.tbbItalic.ToolTipText = "Italic";
            // 
            // tbbBold
            // 
            this.tbbBold.ButtonStyle = Nistec.WinForms.ToolButtonStyle.CheckButton;
            this.tbbBold.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbbBold.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbbBold.ImageIndex = 0;
            this.tbbBold.ImageList = this.imgList1;
            this.tbbBold.Location = new System.Drawing.Point(171, 3);
            this.tbbBold.Name = "tbbBold";
            this.tbbBold.Size = new System.Drawing.Size(22, 22);
            this.tbbBold.TabIndex = 7;
            this.tbbBold.Tag = "bold";
            this.tbbBold.ToolTipText = "Bold";
            // 
            // tbbSeparator2
            // 
            this.tbbSeparator2.ButtonStyle = Nistec.WinForms.ToolButtonStyle.Separator;
            this.tbbSeparator2.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbbSeparator2.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbbSeparator2.Location = new System.Drawing.Point(163, 3);
            this.tbbSeparator2.Name = "tbbSeparator2";
            this.tbbSeparator2.Size = new System.Drawing.Size(8, 22);
            this.tbbSeparator2.TabIndex = 6;
            this.tbbSeparator2.ToolTipText = "";
            // 
            // tbbColor
            // 
            this.tbbColor.ButtonStyle = Nistec.WinForms.ToolButtonStyle.DropDownButton;
            this.tbbColor.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbbColor.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbbColor.DropDownMenu = this.cmColors;
            this.tbbColor.ImageIndex = 7;
            this.tbbColor.ImageList = this.imgList1;
            this.tbbColor.Location = new System.Drawing.Point(130, 3);
            this.tbbColor.Name = "tbbColor";
            this.tbbColor.Size = new System.Drawing.Size(33, 22);
            this.tbbColor.TabIndex = 5;
            this.tbbColor.Tag = "color";
            this.tbbColor.ToolTipText = "Font Color";
            // 
            // cmColors
            // 
            this.cmColors.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miBlack,
            this.miBlue,
            this.miRed,
            this.miGreen});
            // 
            // miBlack
            // 
            this.miBlack.Index = 0;
            this.miBlack.Text = "Black";
            this.miBlack.Click += new System.EventHandler(this.Color_Click);
            // 
            // miBlue
            // 
            this.miBlue.Index = 1;
            this.miBlue.Text = "Blue";
            this.miBlue.Click += new System.EventHandler(this.Color_Click);
            // 
            // miRed
            // 
            this.miRed.Index = 2;
            this.miRed.Text = "Red";
            this.miRed.Click += new System.EventHandler(this.Color_Click);
            // 
            // miGreen
            // 
            this.miGreen.Index = 3;
            this.miGreen.Text = "Green";
            this.miGreen.Click += new System.EventHandler(this.Color_Click);
            // 
            // tbbFontSize
            // 
            this.tbbFontSize.ButtonStyle = Nistec.WinForms.ToolButtonStyle.DropDownButton;
            this.tbbFontSize.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbbFontSize.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbbFontSize.DropDownMenu = this.cmFontSizes;
            this.tbbFontSize.ImageIndex = 15;
            this.tbbFontSize.ImageList = this.imgList1;
            this.tbbFontSize.Location = new System.Drawing.Point(97, 3);
            this.tbbFontSize.Name = "tbbFontSize";
            this.tbbFontSize.Size = new System.Drawing.Size(33, 22);
            this.tbbFontSize.TabIndex = 4;
            this.tbbFontSize.Tag = "font size";
            this.tbbFontSize.ToolTipText = "Font Size";
            // 
            // cmFontSizes
            // 
            this.cmFontSizes.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mi8,
            this.mi9,
            this.mi10,
            this.mi11,
            this.mi12,
            this.mi14,
            this.mi16,
            this.mi18,
            this.mi20,
            this.mi22,
            this.mi24,
            this.mi26,
            this.mi28,
            this.mi36,
            this.mi48,
            this.mi72});
            // 
            // mi8
            // 
            this.mi8.Index = 0;
            this.mi8.Text = "8";
            this.mi8.Click += new System.EventHandler(this.FontSize_Click);
            // 
            // mi9
            // 
            this.mi9.Index = 1;
            this.mi9.Text = "9";
            this.mi9.Click += new System.EventHandler(this.FontSize_Click);
            // 
            // mi10
            // 
            this.mi10.Index = 2;
            this.mi10.Text = "10";
            this.mi10.Click += new System.EventHandler(this.FontSize_Click);
            // 
            // mi11
            // 
            this.mi11.Index = 3;
            this.mi11.Text = "11";
            this.mi11.Click += new System.EventHandler(this.FontSize_Click);
            // 
            // mi12
            // 
            this.mi12.Index = 4;
            this.mi12.Text = "12";
            this.mi12.Click += new System.EventHandler(this.FontSize_Click);
            // 
            // mi14
            // 
            this.mi14.Index = 5;
            this.mi14.Text = "14";
            this.mi14.Click += new System.EventHandler(this.FontSize_Click);
            // 
            // mi16
            // 
            this.mi16.Index = 6;
            this.mi16.Text = "16";
            this.mi16.Click += new System.EventHandler(this.FontSize_Click);
            // 
            // mi18
            // 
            this.mi18.Index = 7;
            this.mi18.Text = "18";
            this.mi18.Click += new System.EventHandler(this.FontSize_Click);
            // 
            // mi20
            // 
            this.mi20.Index = 8;
            this.mi20.Text = "20";
            this.mi20.Click += new System.EventHandler(this.FontSize_Click);
            // 
            // mi22
            // 
            this.mi22.Index = 9;
            this.mi22.Text = "22";
            this.mi22.Click += new System.EventHandler(this.FontSize_Click);
            // 
            // mi24
            // 
            this.mi24.Index = 10;
            this.mi24.Text = "24";
            this.mi24.Click += new System.EventHandler(this.FontSize_Click);
            // 
            // mi26
            // 
            this.mi26.Index = 11;
            this.mi26.Text = "26";
            this.mi26.Click += new System.EventHandler(this.FontSize_Click);
            // 
            // mi28
            // 
            this.mi28.Index = 12;
            this.mi28.Text = "28";
            this.mi28.Click += new System.EventHandler(this.FontSize_Click);
            // 
            // mi36
            // 
            this.mi36.Index = 13;
            this.mi36.Text = "36";
            this.mi36.Click += new System.EventHandler(this.FontSize_Click);
            // 
            // mi48
            // 
            this.mi48.Index = 14;
            this.mi48.Text = "48";
            this.mi48.Click += new System.EventHandler(this.FontSize_Click);
            // 
            // mi72
            // 
            this.mi72.Index = 15;
            this.mi72.Text = "72";
            this.mi72.Click += new System.EventHandler(this.FontSize_Click);
            // 
            // tbbFont
            // 
            this.tbbFont.ButtonStyle = Nistec.WinForms.ToolButtonStyle.DropDownButton;
            this.tbbFont.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbbFont.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbbFont.DropDownMenu = this.cmFonts;
            this.tbbFont.ImageIndex = 14;
            this.tbbFont.ImageList = this.imgList1;
            this.tbbFont.Location = new System.Drawing.Point(64, 3);
            this.tbbFont.Name = "tbbFont";
            this.tbbFont.Size = new System.Drawing.Size(33, 22);
            this.tbbFont.TabIndex = 3;
            this.tbbFont.Tag = "font";
            this.tbbFont.ToolTipText = "Font";
            // 
            // cmFonts
            // 
            this.cmFonts.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miArial,
            this.miCourierNew,
            this.miGaramond,
            this.miMicrosoftSansSerif,
            this.miTahoma,
            this.miTimesNewRoman,
            this.miVerdana});
            // 
            // miArial
            // 
            this.miArial.Index = 0;
            this.miArial.Text = "Arial";
            this.miArial.Click += new System.EventHandler(this.Font_Click);
            // 
            // miCourierNew
            // 
            this.miCourierNew.Index = 1;
            this.miCourierNew.Text = "Courier New";
            this.miCourierNew.Click += new System.EventHandler(this.Font_Click);
            // 
            // miGaramond
            // 
            this.miGaramond.Index = 2;
            this.miGaramond.Text = "Garamond";
            this.miGaramond.Click += new System.EventHandler(this.Font_Click);
            // 
            // miMicrosoftSansSerif
            // 
            this.miMicrosoftSansSerif.Index = 3;
            this.miMicrosoftSansSerif.Text = "Microsoft Sans Serif";
            this.miMicrosoftSansSerif.Click += new System.EventHandler(this.Font_Click);
            // 
            // miTahoma
            // 
            this.miTahoma.Index = 4;
            this.miTahoma.Text = "Tahoma";
            this.miTahoma.Click += new System.EventHandler(this.Font_Click);
            // 
            // miTimesNewRoman
            // 
            this.miTimesNewRoman.Index = 5;
            this.miTimesNewRoman.Text = "Times New Roman";
            this.miTimesNewRoman.Click += new System.EventHandler(this.Font_Click);
            // 
            // miVerdana
            // 
            this.miVerdana.Index = 6;
            this.miVerdana.Text = "Verdana";
            this.miVerdana.Click += new System.EventHandler(this.Font_Click);
            // 
            // tbbSeparator1
            // 
            this.tbbSeparator1.ButtonStyle = Nistec.WinForms.ToolButtonStyle.Separator;
            this.tbbSeparator1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbbSeparator1.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbbSeparator1.Location = new System.Drawing.Point(56, 3);
            this.tbbSeparator1.Name = "tbbSeparator1";
            this.tbbSeparator1.Size = new System.Drawing.Size(8, 22);
            this.tbbSeparator1.TabIndex = 2;
            this.tbbSeparator1.ToolTipText = "";
            // 
            // tbbOpen
            // 
            this.tbbOpen.ButtonStyle = Nistec.WinForms.ToolButtonStyle.Button;
            this.tbbOpen.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbbOpen.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbbOpen.ImageIndex = 10;
            this.tbbOpen.ImageList = this.imgList1;
            this.tbbOpen.Location = new System.Drawing.Point(34, 3);
            this.tbbOpen.Name = "tbbOpen";
            this.tbbOpen.Size = new System.Drawing.Size(22, 22);
            this.tbbOpen.TabIndex = 1;
            this.tbbOpen.Tag = "open";
            this.tbbOpen.ToolTipText = "Open";
            // 
            // tbbSave
            // 
            this.tbbSave.ButtonStyle = Nistec.WinForms.ToolButtonStyle.Button;
            this.tbbSave.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbbSave.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbbSave.ImageIndex = 11;
            this.tbbSave.ImageList = this.imgList1;
            this.tbbSave.Location = new System.Drawing.Point(12, 3);
            this.tbbSave.Name = "tbbSave";
            this.tbbSave.Size = new System.Drawing.Size(22, 22);
            this.tbbSave.TabIndex = 0;
            this.tbbSave.Tag = "save";
            this.tbbSave.ToolTipText = "Save";
            // 
            // rtb1
            // 
            this.rtb1.AcceptsTab = true;
            this.rtb1.AutoWordSelection = true;
            this.rtb1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb1.Location = new System.Drawing.Point(2, 30);
            this.rtb1.Name = "rtb1";
            this.rtb1.Size = new System.Drawing.Size(500, 192);
            this.rtb1.TabIndex = 1;
            this.rtb1.Text = "";
            this.rtb1.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtb1_LinkClicked);
            this.rtb1.SelectionChanged += new System.EventHandler(this.rtb1_SelectionChanged);
            // 
            // ofd1
            // 
            this.ofd1.DefaultExt = "rtf";
            this.ofd1.Filter = "Rich Text Files|*.rtf|Plain Text File|*.txt";
            this.ofd1.Title = "Open File";
            // 
            // sfd1
            // 
            this.sfd1.DefaultExt = "rtf";
            this.sfd1.Filter = "Rich Text File|*.rtf|Plain Text File|*.txt";
            this.sfd1.Title = "Save As";
            // 
            // RichTextBoxExtended
            // 
            this.Controls.Add(this.rtb1);
            this.Controls.Add(this.tb1);
            this.Name = "RichTextBoxExtended";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.Size = new System.Drawing.Size(504, 224);
            this.tb1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

 		#endregion

        #region Istyle

        protected override void OnStylePainterChanged(EventArgs e)
         {
             base.OnStylePainterChanged(e);
             this.tb1.StylePainter = this.StylePainter;
         }

        #endregion

        #region Selection Change event
         [Description("Occurs when the selection is changed"),
		Category("Behavior")]
		// Raised in tb1 SelectionChanged event so that user can do useful things
		public event System.EventHandler SelChanged;
		#endregion

		#region Stamp Event Stuff
		[Description("Occurs when the stamp button is clicked"), 
		 Category("Behavior")]
		public event System.EventHandler StampClicked;
        
		/// <summary>
		/// OnStamp event
		/// </summary>
		protected virtual void OnStamp(EventArgs e)
		{
            if (StampClicked != null)
                StampClicked(this, e);
            if (this.m_StampText.Length > 0)
            {
                string textStmp = this.m_StampText;
                textStmp = textStmp.Replace("[Now]", DateTime.Now.ToLongDateString());

                if (Thread.CurrentPrincipal == null || Thread.CurrentPrincipal.Identity == null || Thread.CurrentPrincipal.Identity.Name.Length <= 0)
                    textStmp = textStmp.Replace("[Name]", Environment.UserName);
                else
                    textStmp = textStmp.Replace("[Name]", Thread.CurrentPrincipal.Identity.Name); 
 

                StringBuilder stamp = new StringBuilder(""); //holds our stamp text
                if (rtb1.Text.Length > 0) stamp.Append("\r\n\r\n"); //add two lines for space
                stamp.Append(textStmp + "\r\n");
                rtb1.SelectionLength = 0; //unselect everything basicly
                rtb1.SelectionStart = rtb1.Text.Length; //start new selection at the end of the text
                rtb1.SelectionColor = this.StampColor; //make the selection blue
                rtb1.SelectionFont = new Font(rtb1.SelectionFont, FontStyle.Bold); //set the selection font and style
                rtb1.AppendText(stamp.ToString()); //add the stamp to the richtextbox
                rtb1.Focus(); //set focus back on the richtextbox

            }

            //switch(StampAction)
            //{
            //    case StampActions.EditedBy:
            //    {
            //        StringBuilder stamp = new StringBuilder(""); //holds our stamp text
            //        if(rtb1.Text.Length > 0) stamp.Append("\r\n\r\n"); //add two lines for space
            //        stamp.Append("Edited by "); 
            //        //use the CurrentPrincipal name if one exsist else use windows logon username
            //        if(Thread.CurrentPrincipal == null || Thread.CurrentPrincipal.Identity == null || Thread.CurrentPrincipal.Identity.Name.Length <= 0)
            //            stamp.Append(Environment.UserName);
            //        else
            //            stamp.Append(Thread.CurrentPrincipal.Identity.Name);
            //        stamp.Append(" on " + DateTime.Now.ToLongDateString() + "\r\n");
			
            //        rtb1.SelectionLength = 0; //unselect everything basicly
            //        rtb1.SelectionStart = rtb1.Text.Length; //start new selection at the end of the text
            //        rtb1.SelectionColor = this.StampColor; //make the selection blue
            //        rtb1.SelectionFont = new Font(rtb1.SelectionFont, FontStyle.Bold); //set the selection font and style
            //        rtb1.AppendText(stamp.ToString()); //add the stamp to the richtextbox
            //        rtb1.Focus(); //set focus back on the richtextbox
            //    } break; //end edited by stamp
            //    case StampActions.DateTime:
            //    {
            //        StringBuilder stamp = new StringBuilder(""); //holds our stamp text
            //        if(rtb1.Text.Length > 0) stamp.Append("\r\n\r\n"); //add two lines for space
            //        stamp.Append(DateTime.Now.ToLongDateString() + "\r\n");
            //        rtb1.SelectionLength = 0; //unselect everything basicly
            //        rtb1.SelectionStart = rtb1.Text.Length; //start new selection at the end of the text
            //        rtb1.SelectionColor = this.StampColor; //make the selection blue
            //        rtb1.SelectionFont = new Font(rtb1.SelectionFont, FontStyle.Bold); //set the selection font and style
            //        rtb1.AppendText(stamp.ToString()); //add the stamp to the richtextbox
            //        rtb1.Focus(); //set focus back on the richtextbox
            //    } break;
            //} //end select
		}
		#endregion

		#region Toolbar button click
		/// <summary>
		///     Handler for the toolbar button click event
		/// </summary>
        /// 

         private void tb1_ButtonClick(object sender, ToolButtonClickEventArgs e)
         {
             //Switch based on the tooltip of the button pressed
             // true if style to be added
             // false to remove style
             bool add = e.Button.Checked;

             switch (e.Button.Tag.ToString().ToLower())
             {
                 case "bold":
                     ChangeFontStyle(FontStyle.Bold, add);
                     break;
                 case "italic":
                     ChangeFontStyle(FontStyle.Italic, add);
                     break;
                 case "underline":
                     ChangeFontStyle(FontStyle.Underline, add);
                     break;
                 case "strikeout":
                     ChangeFontStyle(FontStyle.Strikeout, add);
                     break;
                 case "left":
                     //change horizontal alignment to left
                     rtb1.SelectionAlignment = HorizontalAlignment.Left;
                     tbbCenter.Checked = false;
                     tbbRight.Checked = false;
                     break;
                 case "center":
                     //change horizontal alignment to center
                     tbbLeft.Checked = false;
                     rtb1.SelectionAlignment = HorizontalAlignment.Center;
                     tbbRight.Checked = false;
                     break;
                 case "right":
                     //change horizontal alignment to right
                     tbbLeft.Checked = false;
                     tbbCenter.Checked = false;
                     rtb1.SelectionAlignment = HorizontalAlignment.Right;
                     break;
                 case "edit stamp":
                     OnStamp(new EventArgs()); //send stamp event
                     break;
                 case "color":
                     rtb1.SelectionColor = Color.Black;
                     break;
                 case "undo":
                     rtb1.Undo();
                     break;
                 case "redo":
                     rtb1.Redo();
                     break;
                 case "open":
                     try
                     {
                         if (ofd1.ShowDialog() == DialogResult.OK && ofd1.FileName.Length > 0)
                             if (System.IO.Path.GetExtension(ofd1.FileName).ToLower().Equals(".rtf"))
                                 rtb1.LoadFile(ofd1.FileName, RichTextBoxStreamType.RichText);
                             else
                                 rtb1.LoadFile(ofd1.FileName, RichTextBoxStreamType.PlainText);
                     }
                     catch (ArgumentException ae)
                     {
                         if (ae.Message == "Invalid file format.")
                             MessageBox.Show("There was an error loading the file: " + ofd1.FileName);
                     }
                     break;
                 case "save":
                     if (sfd1.ShowDialog() == DialogResult.OK && sfd1.FileName.Length > 0)
                         if (System.IO.Path.GetExtension(sfd1.FileName).ToLower().Equals(".rtf"))
                             rtb1.SaveFile(sfd1.FileName);
                         else
                             rtb1.SaveFile(sfd1.FileName, RichTextBoxStreamType.PlainText);
                     break;
                 case "cut":
                     {
                         if (rtb1.SelectedText.Length <= 0) break;
                         rtb1.Cut();
                         break;
                     }
                 case "copy":
                     {
                         if (rtb1.SelectedText.Length <= 0) break;
                         rtb1.Copy();
                         break;
                     }
                 case "paste":
                     {
                         try
                         {
                             rtb1.Paste();
                         }
                         catch
                         {
                             MessageBox.Show("Paste Failed");
                         }
                         break;
                     }
                 case "print":
                     {
                         if (rtb1.SelectedText.Length <= 0) return;
                         Nistec.Printing.ReportBuilder.PrintTextDocument(rtb1.Rtf,"","");
                         break;
                     }
                 case "email":
                     {
                         if (rtb1.SelectedText.Length <= 0) break;
                         EmailDlg dlg = new EmailDlg();
                         dlg.Body = rtb1.Text;
                         break;
                     }
             } //end select
         }
		#endregion

		#region Update Toolbar
		/// <summary>
		///     Update the toolbar button statuses
		/// </summary>
		public void UpdateToolbar()
		{
			// Get the font, fontsize and style to apply to the toolbar buttons
			Font fnt = GetFontDetails();
			// Set font style buttons to the styles applying to the entire selection
			FontStyle style = fnt.Style;
			
			//Set all the style buttons using the gathered style
			tbbBold.Checked		= fnt.Bold; //bold button
			tbbItalic.Checked	= fnt.Italic; //italic button
			tbbUnderline.Checked	= fnt.Underline; //underline button
			tbbStrikeout.Checked	= fnt.Strikeout; //strikeout button
			tbbLeft.Checked		= (rtb1.SelectionAlignment == HorizontalAlignment.Left); //justify left
			tbbCenter.Checked	= (rtb1.SelectionAlignment == HorizontalAlignment.Center); //justify center
			tbbRight.Checked		= (rtb1.SelectionAlignment == HorizontalAlignment.Right); //justify right
	
			//Check the correct color
			foreach(MenuItem mi in cmColors.MenuItems)
				mi.Checked = (rtb1.SelectionColor == Color.FromName(mi.Text));
			
			//Check the correct font
			foreach(MenuItem mi in cmFonts.MenuItems)
				mi.Checked = (fnt.FontFamily.Name == mi.Text);

			//Check the correct font size
			foreach(MenuItem mi in cmFontSizes.MenuItems)
				mi.Checked = ((int)fnt.SizeInPoints == float.Parse(mi.Text));
		}
		#endregion

		#region Update Toolbar Seperators
		private void UpdateToolbarSeperators()
		{
			//Save & Open
			if(!tbbSave.Visible && !tbbOpen.Visible) 
				tbbSeparator3.Visible = false;
			else
				tbbSeparator3.Visible = true;

			//Font & Font Size
			if(!tbbFont.Visible && !tbbFontSize.Visible && !tbbColor.Visible) 
				tbbSeparator5.Visible = false;
			else
				tbbSeparator5.Visible = true;

			//Bold, Italic, Underline, & Strikeout
			if(!tbbBold.Visible && !tbbItalic.Visible && !tbbUnderline.Visible && !tbbStrikeout.Visible)
				tbbSeparator1.Visible = false;
			else
				tbbSeparator1.Visible = true;

			//Left, Center, & Right
			if(!tbbLeft.Visible && !tbbCenter.Visible && !tbbRight.Visible)
				tbbSeparator2.Visible = false;
			else
				tbbSeparator2.Visible = true;

			//Undo & Redo
			if(!tbbUndo.Visible && !tbbRedo.Visible) 
				tbbSeparator4.Visible = false;
			else
				tbbSeparator4.Visible = true;
		}
#endregion

		#region RichTextBox Selection Change
		/// <summary>
		///		Change the toolbar buttons when new text is selected
		///		and raise event SelChanged
		/// </summary>
		private void rtb1_SelectionChanged(object sender, System.EventArgs e)
		{
			//Update the toolbar buttons
			UpdateToolbar();
			
			//Send the SelChangedEvent
			if (SelChanged != null)
				SelChanged(this, e);
		}
#endregion

		#region Color Click
		/// <summary>
		///     Change the richtextbox color
		/// </summary>
		private void Color_Click(object sender, System.EventArgs e)
		{
			//set the richtextbox color based on the name of the menu item
			ChangeFontColor(Color.FromName(((MenuItem)sender).Text));
		}
		#endregion

		#region Font Click
		/// <summary>
		///     Change the richtextbox font
		/// </summary>
		private void Font_Click(object sender, System.EventArgs e)
		{
			// Set the font for the entire selection
			ChangeFont(((MenuItem)sender).Text);
		}
		#endregion

		#region Font Size Click
		/// <summary>
		///     Change the richtextbox font size
		/// </summary>
		private void FontSize_Click(object sender, System.EventArgs e)
		{
			//set the richtextbox font size based on the name of the menu item
			ChangeFontSize(float.Parse(((MenuItem)sender).Text));
		}
		#endregion

		#region Link Clicked
		/// <summary>
		/// Starts the default browser if a link is clicked
		/// </summary>
		private void rtb1_LinkClicked(object sender, System.Windows.Forms.LinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(e.LinkText);
		}
		#endregion

		#region Public Properties
		/// <summary>
		///     The toolbar that is contained with-in the RichTextBoxExtened control
		/// </summary>
		[Description("The internal toolbar control"),
		Category("Internal Controls")]
		public McToolBar Toolbar
		{
			get { return tb1; }
		}

		/// <summary>
		///     The RichTextBox that is contained with-in the RichTextBoxExtened control
		/// </summary>
		[Description("The internal richtextbox control"),
		Category("Internal Controls")]
		public RichTextBox RichTextBox
		{
			get	{ return rtb1; }
		}

		/// <summary>
		///     Show the save button or not
		/// </summary>
		[Description("Show the save button or not"),
		Category("Appearance")]
		public bool ShowSave
		{
			get { return tbbSave.Visible; }
			set { tbbSave.Visible = value; UpdateToolbarSeperators(); }
		}

		/// <summary>
		///    Show the open button or not 
		/// </summary>
		[Description("Show the open button or not"),
		Category("Appearance")]
		public bool ShowOpen
		{
			get { return tbbOpen.Visible; }
			set	{ tbbOpen.Visible = value; UpdateToolbarSeperators(); }
		}

		/// <summary>
		///     Show the stamp button or not
		/// </summary>
		[Description("Show the stamp button or not"),
		 Category("Appearance")]
		public bool ShowStamp
		{
			get { return tbbStamp.Visible; }
			set { tbbStamp.Visible = value; }
		}
		
		/// <summary>
		///     Show the color button or not
		/// </summary>
		[Description("Show the color button or not"),
		Category("Appearance")]
		public bool ShowColors
		{
			get { return tbbColor.Visible; }
			set { tbbColor.Visible = value; }
		}

		/// <summary>
		///     Show the undo button or not
		/// </summary>
		[Description("Show the undo button or not"),
		Category("Appearance")]
		public bool ShowUndo
		{
			get { return tbbUndo.Visible; }
			set { tbbUndo.Visible = value; UpdateToolbarSeperators(); }
		}

		/// <summary>
		///     Show the redo button or not
		/// </summary>
		[Description("Show the redo button or not"),
		Category("Appearance")]
		public bool ShowRedo
		{
			get { return tbbRedo.Visible; }
			set { tbbRedo.Visible = value; UpdateToolbarSeperators(); }
		}

		/// <summary>
		///     Show the bold button or not
		/// </summary>
		[Description("Show the bold button or not"),
		Category("Appearance"),DefaultValue(true)]
		public bool ShowBold
		{
			get { return tbbBold.Visible; }
			set { tbbBold.Visible = value; UpdateToolbarSeperators(); }
		}

		/// <summary>
		///     Show the italic button or not
		/// </summary>
		[Description("Show the italic button or not"),
       Category("Appearance"), DefaultValue(true)]
		public bool ShowItalic
		{
			get { return tbbItalic.Visible; }
			set { tbbItalic.Visible = value; UpdateToolbarSeperators(); }
		}

		/// <summary>
		///     Show the underline button or not
		/// </summary>
		[Description("Show the underline button or not"),
       Category("Appearance"), DefaultValue(true)]
		public bool ShowUnderline
		{
			get { return tbbUnderline.Visible; }
			set { tbbUnderline.Visible = value; UpdateToolbarSeperators(); }
		}

		/// <summary>
		///     Show the strikeout button or not
		/// </summary>
		[Description("Show the strikeout button or not"),
       Category("Appearance"), DefaultValue(true)]
		public bool ShowStrikeout
		{
			get { return tbbStrikeout.Visible; }
			set { tbbStrikeout.Visible = value; UpdateToolbarSeperators(); }
		}

		/// <summary>
		///     Show the left justify button or not
		/// </summary>
		[Description("Show the left justify button or not"),
       Category("Appearance"), DefaultValue(true)]
		public bool ShowLeftJustify
		{
			get { return tbbLeft.Visible; }
			set { tbbLeft.Visible = value; UpdateToolbarSeperators(); }
		}

		/// <summary>
		///     Show the right justify button or not
		/// </summary>
		[Description("Show the right justify button or not"),
       Category("Appearance"), DefaultValue(true)]
		public bool ShowRightJustify
		{
			get { return tbbRight.Visible; }
			set { tbbRight.Visible = value; UpdateToolbarSeperators(); }
		}

		/// <summary>
		///     Show the center justify button or not
		/// </summary>
		[Description("Show the center justify button or not"),
       Category("Appearance"), DefaultValue(true)]
		public bool ShowCenterJustify
		{
			get { return tbbCenter.Visible; }
			set { tbbCenter.Visible = value; UpdateToolbarSeperators(); }
		}

		/// <summary>
		///     Determines how the stamp button will respond
		/// </summary>
       // StampActions m_StampAction = StampActions.EditedBy;
       // [Description("Determines how the stamp button will respond"),
       //Category("Behavior")]
       // public StampActions StampAction
       // {
       //     get { return m_StampAction; }
       //     set { m_StampAction = value; }
       // }

         private string m_StampText = "";
         /// <summary>
         /// Determines the stamp text will add on StampClick action
         /// You can Write free text
         /// [Now] will replace with DateTime Now
         /// [Name] will replace with the current thread owner
         /// </summary>
         [Description("Stamp text to add on stamp action"),
        Category("Behavior"), DefaultValue("")]
         public string StampText
         {
             get { return m_StampText; }
             set { m_StampText = value; }
         }

		/// <summary>
		///     Color of the stamp text
		/// </summary>
		Color m_StampColor = Color.Blue;

		[Description("Color of the stamp text"),
       Category("Appearance"), DefaultValue(typeof(Color),"Blue")]
		public Color StampColor
		{
			get { return m_StampColor; }
			set { m_StampColor = value; }
		}
			
		/// <summary>
		///     Show the font button or not
		/// </summary>
		[Description("Show the font button or not"),
       Category("Appearance"), DefaultValue(true)]
		public bool ShowFont
		{
			get { return tbbFont.Visible; }
			set { tbbFont.Visible = value; }
		}

		/// <summary>
		///     Show the font size button or not
		/// </summary>
		[Description("Show the font size button or not"),
       Category("Appearance"), DefaultValue(true)]
		public bool ShowFontSize
		{
			get { return tbbFontSize.Visible; }
			set { tbbFontSize.Visible = value; }
		}

		/// <summary>
		///     Show the cut button or not
		/// </summary>
		[Description("Show the cut button or not"),
       Category("Appearance"), DefaultValue(true)]
		public bool ShowCut
		{
			get { return tbbCut.Visible; }
			set { tbbCut.Visible = value; }
		}

		/// <summary>
		///     Show the copy button or not
		/// </summary>
		[Description("Show the copy button or not"),
       Category("Appearance"), DefaultValue(true)]
		public bool ShowCopy
		{
			get { return tbbCopy.Visible; }
			set { tbbCopy.Visible = value; }
		}

		/// <summary>
		///     Show the paste button or not
		/// </summary>
		[Description("Show the paste button or not"),
       Category("Appearance"), DefaultValue(true)]
		public bool ShowPaste
		{
			get { return tbbPaste.Visible; }
			set { tbbPaste.Visible = value; }
		}

		/// <summary>
		///     Detect URLs with-in the richtextbox
		/// </summary>
		[Description("Detect URLs with-in the richtextbox"),
       Category("Behavior"), DefaultValue(true)]
		public bool DetectURLs
		{
			get { return rtb1.DetectUrls; }
			set { rtb1.DetectUrls = value; }
		}

		/// <summary>
		/// Determines if the TAB key moves to the next control or enters a TAB character in the richtextbox
		/// </summary>
		[Description("Determines if the TAB key moves to the next control or enters a TAB character in the richtextbox"),
       Category("Behavior"), DefaultValue(true)]
		public bool AcceptsTab
		{
			get { return rtb1.AcceptsTab; }
			set { rtb1.AcceptsTab = value; }
		}

		/// <summary>
		/// Determines if auto word selection is enabled
		/// </summary>
		[Description("Determines if auto word selection is enabled"),
       Category("Behavior"), DefaultValue(true)]
		public bool AutoWordSelection
		{
			get { return rtb1.AutoWordSelection; }
			set { rtb1.AutoWordSelection = value; }
		}
		#endregion

		#region Change font
		/// <summary>
		///     Change the richtextbox font for the current selection
		/// </summary>
		public void ChangeFont(string fontFamily)
		{
			//This method should handle cases that occur when multiple fonts/styles are selected
			// Parameters:-
			// fontFamily - the font to be applied, eg "Courier New"

			// Reason: The reason this method and the others exist is because
			// setting these items via the selection font doen't work because
			// a null selection font is returned for a selection with more 
			// than one font!
			
			int rtb1start = rtb1.SelectionStart;				
			int len = rtb1.SelectionLength; 
			int rtbTempStart = 0;						

			// If len <= 1 and there is a selection font, amend and return
			if (len <= 1 && rtb1.SelectionFont != null)
			{
				rtb1.SelectionFont =
					new Font(fontFamily, rtb1.SelectionFont.Size, rtb1.SelectionFont.Style);
				return;
			}

			// Step through the selected text one char at a time
			rtbTemp.Rtf = rtb1.SelectedRtf;
			for(int i = 0; i < len; ++i) 
			{ 
				rtbTemp.Select(rtbTempStart + i, 1); 
				rtbTemp.SelectionFont = new Font(fontFamily, rtbTemp.SelectionFont.Size, rtbTemp.SelectionFont.Style);
			}

			// Replace & reselect
			rtbTemp.Select(rtbTempStart,len);
			rtb1.SelectedRtf = rtbTemp.SelectedRtf;
			rtb1.Select(rtb1start,len);
			return;
		}
		#endregion

		#region Change font style
		/// <summary>
		///     Change the richtextbox style for the current selection
		/// </summary>
		public void ChangeFontStyle(FontStyle style, bool add)
		{
			//This method should handle cases that occur when multiple fonts/styles are selected
			// Parameters:-
			//	style - eg FontStyle.Bold
			//	add - IF true then add else remove
			
			// throw error if style isn't: bold, italic, strikeout or underline
			if (   style != FontStyle.Bold
				&& style != FontStyle.Italic
				&& style != FontStyle.Strikeout
				&& style != FontStyle.Underline)
					throw new  System.InvalidProgramException("Invalid style parameter to ChangeFontStyle");
			
			int rtb1start = rtb1.SelectionStart;				
			int len = rtb1.SelectionLength; 
			int rtbTempStart = 0;			
			
			//if len <= 1 and there is a selection font then just handle and return
			if(len <= 1 && rtb1.SelectionFont != null)
			{
				//add or remove style 
				if (add)
					rtb1.SelectionFont = new Font(rtb1.SelectionFont, rtb1.SelectionFont.Style | style);
				else
					rtb1.SelectionFont = new Font(rtb1.SelectionFont, rtb1.SelectionFont.Style & ~style);
				
				return;
			}
			
			// Step through the selected text one char at a time	
			rtbTemp.Rtf = rtb1.SelectedRtf;
			for(int i = 0; i < len; ++i) 
			{ 
				rtbTemp.Select(rtbTempStart + i, 1); 

				//add or remove style 
				if (add)
					rtbTemp.SelectionFont = new Font(rtbTemp.SelectionFont, rtbTemp.SelectionFont.Style | style);
				else
					rtbTemp.SelectionFont = new Font(rtbTemp.SelectionFont, rtbTemp.SelectionFont.Style & ~style);
			}

			// Replace & reselect
			rtbTemp.Select(rtbTempStart,len);
			rtb1.SelectedRtf = rtbTemp.SelectedRtf;
			rtb1.Select(rtb1start,len);
			return;
		}
		#endregion

		#region Change font size
		/// <summary>
		///     Change the richtextbox font size for the current selection
		/// </summary>
		public void ChangeFontSize(float fontSize)
		{
			//This method should handle cases that occur when multiple fonts/styles are selected
			// Parameters:-
			// fontSize - the fontsize to be applied, eg 33.5
			
			if (fontSize <= 0.0)
				throw new System.InvalidProgramException("Invalid font size parameter to ChangeFontSize");
			
			int rtb1start = rtb1.SelectionStart;				
			int len = rtb1.SelectionLength; 
			int rtbTempStart = 0;

			// If len <= 1 and there is a selection font, amend and return
			if (len <= 1 && rtb1.SelectionFont != null)
			{
				rtb1.SelectionFont =
					new Font(rtb1.SelectionFont.FontFamily, fontSize, rtb1.SelectionFont.Style);
				return;
			}
			
			// Step through the selected text one char at a time
			rtbTemp.Rtf = rtb1.SelectedRtf;
			for(int i = 0; i < len; ++i) 
			{ 
				rtbTemp.Select(rtbTempStart + i, 1); 
				rtbTemp.SelectionFont = new Font(rtbTemp.SelectionFont.FontFamily, fontSize, rtbTemp.SelectionFont.Style);
			}

			// Replace & reselect
			rtbTemp.Select(rtbTempStart,len);
			rtb1.SelectedRtf = rtbTemp.SelectedRtf;
			rtb1.Select(rtb1start,len);
			return;
		}
		#endregion

		#region Change font color
		/// <summary>
		///     Change the richtextbox font color for the current selection
		/// </summary>
		public void ChangeFontColor(Color newColor)
		{
			//This method should handle cases that occur when multiple fonts/styles are selected
			// Parameters:-
			//	newColor - eg Color.Red
			
			int rtb1start = rtb1.SelectionStart;				
			int len = rtb1.SelectionLength; 
			int rtbTempStart = 0;			
			
			//if len <= 1 and there is a selection font then just handle and return
			if(len <= 1 && rtb1.SelectionFont != null)
			{
				rtb1.SelectionColor = newColor;
				return;
			}
			
			// Step through the selected text one char at a time	
			rtbTemp.Rtf = rtb1.SelectedRtf;
			for(int i = 0; i < len; ++i) 
			{ 
				rtbTemp.Select(rtbTempStart + i, 1); 

				//change color
				rtbTemp.SelectionColor = newColor;
			}

			// Replace & reselect
			rtbTemp.Select(rtbTempStart,len);
			rtb1.SelectedRtf = rtbTemp.SelectedRtf;
			rtb1.Select(rtb1start,len);
			return;
		}
		#endregion

		#region Get Font Details
		/// <summary>
		///     Returns a Font with:
		///     1) The font applying to the entire selection, if none is the default font. 
		///     2) The font size applying to the entire selection, if none is the size of the default font.
		///     3) A style containing the attributes that are common to the entire selection, default regular.
		/// </summary>		
		/// 
		public Font GetFontDetails()
		{
			//This method should handle cases that occur when multiple fonts/styles are selected
			
			int rtb1start = rtb1.SelectionStart;				
			int len = rtb1.SelectionLength; 
			int rtbTempStart = 0;

			if (len <= 1)						
			{
				// Return the selection or default font
				if (rtb1.SelectionFont != null)
					return rtb1.SelectionFont;
				else
					return rtb1.Font;
			}

			// Step through the selected text one char at a time	
			// after setting defaults from first char
			rtbTemp.Rtf = rtb1.SelectedRtf;
		
			//Turn everything on so we can turn it off one by one
			FontStyle replystyle =			
				FontStyle.Bold | FontStyle.Italic | FontStyle.Strikeout | FontStyle.Underline;
			
			// Set reply font, size and style to that of first char in selection.
			rtbTemp.Select(rtbTempStart, 1);
			string replyfont = rtbTemp.SelectionFont.Name;
			float replyfontsize = rtbTemp.SelectionFont.Size;
			replystyle = replystyle & rtbTemp.SelectionFont.Style;
			
			// Search the rest of the selection
			for(int i = 1; i < len; ++i)				
			{ 
				rtbTemp.Select(rtbTempStart + i, 1); 
				
				// Check reply for different style
				replystyle = replystyle & rtbTemp.SelectionFont.Style;
				
				// Check font
				if (replyfont != rtbTemp.SelectionFont.FontFamily.Name)
					replyfont = "";

				// Check font size
				if (replyfontsize != rtbTemp.SelectionFont.Size)
					replyfontsize = (float)0.0;
			}

			// Now set font and size if more than one font or font size was selected
			if (replyfont == "")
				replyfont = rtbTemp.Font.FontFamily.Name;

			if (replyfontsize == 0.0)
				replyfontsize = rtbTemp.Font.Size;

			// generate reply font
			Font reply 
				= new Font(replyfont, replyfontsize, replystyle);
			
			return reply;
		}
		#endregion

	} //end class
} //end namespace

