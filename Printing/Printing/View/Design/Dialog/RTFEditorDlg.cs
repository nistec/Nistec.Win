using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;


namespace MControl.Printing.View.Design
{
	public class RTFEditorDlg : System.Windows.Forms.Form// MControl.WinForms.FormBase
	{
		#region Members

		internal bool mtd4;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ToolBar toolBar1;
		private System.Windows.Forms.ToolBarButton tbOpen;
		private System.Windows.Forms.ToolBarButton tbField;
		private System.Windows.Forms.ToolBarButton tbCut;
		private System.Windows.Forms.ToolBarButton tbCopy;
		private System.Windows.Forms.ToolBarButton tbPaste;
		private System.Windows.Forms.ToolBarButton tbDelete;
		private System.Windows.Forms.ToolBarButton tbUndo;
		private System.Windows.Forms.ToolBarButton tbRedo;
		private System.Windows.Forms.ToolBarButton tbFont;
		private System.Windows.Forms.ToolBarButton tbForeColor;
		private System.Windows.Forms.ToolBarButton tbLeft;
		private System.Windows.Forms.ToolBarButton tbCenter;
		private System.Windows.Forms.ToolBarButton tbRight;
		private System.Windows.Forms.ToolBarButton tbBullet;
		private System.Windows.Forms.ToolBarButton tbLessLI;
		private System.Windows.Forms.ToolBarButton tbMoreLI;
		private System.Windows.Forms.ToolBarButton tbLessRI;
		private System.Windows.Forms.ToolBarButton tbMoreRI;
		private System.Windows.Forms.ToolBarButton tbLessFLI;
		private System.Windows.Forms.ToolBarButton tbMoreFLI;
		internal System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.RichTextBox RTB;
		private System.Windows.Forms.ToolBarButton tbLine1;
		private System.Windows.Forms.ToolBarButton tbLine2;
		private System.Windows.Forms.ToolBarButton tbSave;
		private System.Windows.Forms.ToolBarButton tbClose;
		private System.Windows.Forms.ToolBarButton tbLine3;
		private System.Windows.Forms.ToolBarButton tbLine4;

		internal ContextMenu _ContextMenu;//mtd395;
		#endregion

		#region Constructor

		public RTFEditorDlg()
		{
			this.components = null;
			this._ContextMenu = new ContextMenu();
			this.InitializeComponent();
			//this.var2=this.imageList1;

		}

		#endregion

		#region Init

//		internal void InitImages(ref ImageList ImgList)//mtd413
//		{
//			//this.var2 = ImgList;
//			this.var3();
//			this.var7();
//		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && (this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

 
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RTFEditorDlg));
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.tbOpen = new System.Windows.Forms.ToolBarButton();
            this.tbField = new System.Windows.Forms.ToolBarButton();
            this.tbLine1 = new System.Windows.Forms.ToolBarButton();
            this.tbCut = new System.Windows.Forms.ToolBarButton();
            this.tbCopy = new System.Windows.Forms.ToolBarButton();
            this.tbPaste = new System.Windows.Forms.ToolBarButton();
            this.tbDelete = new System.Windows.Forms.ToolBarButton();
            this.tbUndo = new System.Windows.Forms.ToolBarButton();
            this.tbRedo = new System.Windows.Forms.ToolBarButton();
            this.tbFont = new System.Windows.Forms.ToolBarButton();
            this.tbForeColor = new System.Windows.Forms.ToolBarButton();
            this.tbLine2 = new System.Windows.Forms.ToolBarButton();
            this.tbLeft = new System.Windows.Forms.ToolBarButton();
            this.tbCenter = new System.Windows.Forms.ToolBarButton();
            this.tbRight = new System.Windows.Forms.ToolBarButton();
            this.tbLine3 = new System.Windows.Forms.ToolBarButton();
            this.tbBullet = new System.Windows.Forms.ToolBarButton();
            this.tbLessLI = new System.Windows.Forms.ToolBarButton();
            this.tbMoreLI = new System.Windows.Forms.ToolBarButton();
            this.tbLessRI = new System.Windows.Forms.ToolBarButton();
            this.tbMoreRI = new System.Windows.Forms.ToolBarButton();
            this.tbLessFLI = new System.Windows.Forms.ToolBarButton();
            this.tbMoreFLI = new System.Windows.Forms.ToolBarButton();
            this.tbLine4 = new System.Windows.Forms.ToolBarButton();
            this.tbClose = new System.Windows.Forms.ToolBarButton();
            this.tbSave = new System.Windows.Forms.ToolBarButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.RTB = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // StyleGuideBase
            // 
            this.StyleGuideBase.AlternatingColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(230)))), ((int)(((byte)(220)))));
            this.StyleGuideBase.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(106)))), ((int)(((byte)(197)))));
            this.StyleGuideBase.BorderHotColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(80)))), ((int)(((byte)(184)))));
            this.StyleGuideBase.CaptionColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(78)))), ((int)(((byte)(152)))));
            this.StyleGuideBase.ColorBrush1 = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(203)))), ((int)(((byte)(183)))));
            this.StyleGuideBase.ColorBrush2 = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.FocusedColor = System.Drawing.Color.RoyalBlue;
            this.StyleGuideBase.FormColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.StylePlan = MControl.WinForms.Styles.Desktop;
            // 
            // toolBar1
            // 
            this.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.tbOpen,
            this.tbField,
            this.tbLine1,
            this.tbCut,
            this.tbCopy,
            this.tbPaste,
            this.tbDelete,
            this.tbUndo,
            this.tbRedo,
            this.tbFont,
            this.tbForeColor,
            this.tbLine2,
            this.tbLeft,
            this.tbCenter,
            this.tbRight,
            this.tbLine3,
            this.tbBullet,
            this.tbLessLI,
            this.tbMoreLI,
            this.tbLessRI,
            this.tbMoreRI,
            this.tbLessFLI,
            this.tbMoreFLI,
            this.tbLine4,
            this.tbClose,
            this.tbSave});
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.imageList1;
            this.toolBar1.Location = new System.Drawing.Point(0, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(552, 28);
            this.toolBar1.TabIndex = 4;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // tbOpen
            // 
            this.tbOpen.ImageIndex = 0;
            this.tbOpen.Name = "tbOpen";
            this.tbOpen.Tag = "Open file";
            this.tbOpen.ToolTipText = "Open file";
            // 
            // tbField
            // 
            this.tbField.ImageIndex = 1;
            this.tbField.Name = "tbField";
            this.tbField.Tag = "Field";
            this.tbField.ToolTipText = "Insert Field";
            // 
            // tbLine1
            // 
            this.tbLine1.Name = "tbLine1";
            this.tbLine1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbCut
            // 
            this.tbCut.ImageIndex = 2;
            this.tbCut.Name = "tbCut";
            this.tbCut.Tag = "Cut";
            this.tbCut.ToolTipText = "Cut";
            // 
            // tbCopy
            // 
            this.tbCopy.ImageIndex = 3;
            this.tbCopy.Name = "tbCopy";
            this.tbCopy.Tag = "Copy";
            this.tbCopy.ToolTipText = "Copy";
            // 
            // tbPaste
            // 
            this.tbPaste.ImageIndex = 4;
            this.tbPaste.Name = "tbPaste";
            this.tbPaste.Tag = "Paste";
            this.tbPaste.ToolTipText = "Paste";
            // 
            // tbDelete
            // 
            this.tbDelete.ImageIndex = 5;
            this.tbDelete.Name = "tbDelete";
            this.tbDelete.Tag = "Delete";
            this.tbDelete.ToolTipText = "Delete";
            // 
            // tbUndo
            // 
            this.tbUndo.ImageIndex = 6;
            this.tbUndo.Name = "tbUndo";
            this.tbUndo.Tag = "Undo";
            this.tbUndo.ToolTipText = "Undo";
            // 
            // tbRedo
            // 
            this.tbRedo.ImageIndex = 7;
            this.tbRedo.Name = "tbRedo";
            this.tbRedo.Tag = "Redo";
            this.tbRedo.ToolTipText = "Redo";
            // 
            // tbFont
            // 
            this.tbFont.ImageIndex = 8;
            this.tbFont.Name = "tbFont";
            this.tbFont.Tag = "Font";
            this.tbFont.ToolTipText = "Font";
            // 
            // tbForeColor
            // 
            this.tbForeColor.ImageIndex = 9;
            this.tbForeColor.Name = "tbForeColor";
            this.tbForeColor.Tag = "ForeColor";
            this.tbForeColor.ToolTipText = "ForeColor";
            // 
            // tbLine2
            // 
            this.tbLine2.Name = "tbLine2";
            this.tbLine2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbLeft
            // 
            this.tbLeft.ImageIndex = 10;
            this.tbLeft.Name = "tbLeft";
            this.tbLeft.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbLeft.Tag = "Left";
            this.tbLeft.ToolTipText = "Left Alignment";
            // 
            // tbCenter
            // 
            this.tbCenter.ImageIndex = 11;
            this.tbCenter.Name = "tbCenter";
            this.tbCenter.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbCenter.Tag = "Center";
            this.tbCenter.ToolTipText = "Center Alignment";
            // 
            // tbRight
            // 
            this.tbRight.ImageIndex = 12;
            this.tbRight.Name = "tbRight";
            this.tbRight.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbRight.Tag = "Right";
            this.tbRight.ToolTipText = "Right Alignment";
            // 
            // tbLine3
            // 
            this.tbLine3.Name = "tbLine3";
            this.tbLine3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbBullet
            // 
            this.tbBullet.ImageIndex = 13;
            this.tbBullet.Name = "tbBullet";
            this.tbBullet.Tag = "Bullet";
            this.tbBullet.ToolTipText = "Bullet";
            // 
            // tbLessLI
            // 
            this.tbLessLI.ImageIndex = 14;
            this.tbLessLI.Name = "tbLessLI";
            this.tbLessLI.Tag = "LessLI";
            this.tbLessLI.ToolTipText = "Decrease Left ident";
            // 
            // tbMoreLI
            // 
            this.tbMoreLI.ImageIndex = 15;
            this.tbMoreLI.Name = "tbMoreLI";
            this.tbMoreLI.Tag = "MoreLI";
            this.tbMoreLI.ToolTipText = "Increase Left ident";
            // 
            // tbLessRI
            // 
            this.tbLessRI.ImageIndex = 16;
            this.tbLessRI.Name = "tbLessRI";
            this.tbLessRI.Tag = "LessRI";
            this.tbLessRI.ToolTipText = "Decrease Right ident";
            // 
            // tbMoreRI
            // 
            this.tbMoreRI.ImageIndex = 17;
            this.tbMoreRI.Name = "tbMoreRI";
            this.tbMoreRI.Tag = "MoreRI";
            this.tbMoreRI.ToolTipText = "Increase Right ident";
            // 
            // tbLessFLI
            // 
            this.tbLessFLI.ImageIndex = 18;
            this.tbLessFLI.Name = "tbLessFLI";
            this.tbLessFLI.Tag = "LessFLI";
            this.tbLessFLI.ToolTipText = "Decrease First Line ident";
            // 
            // tbMoreFLI
            // 
            this.tbMoreFLI.ImageIndex = 19;
            this.tbMoreFLI.Name = "tbMoreFLI";
            this.tbMoreFLI.Tag = "MoreFLI";
            this.tbMoreFLI.ToolTipText = "Increase First Line ident";
            // 
            // tbLine4
            // 
            this.tbLine4.Name = "tbLine4";
            this.tbLine4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbClose
            // 
            this.tbClose.ImageIndex = 21;
            this.tbClose.Name = "tbClose";
            this.tbClose.Tag = "Close";
            this.tbClose.ToolTipText = "Close";
            // 
            // tbSave
            // 
            this.tbSave.ImageIndex = 20;
            this.tbSave.Name = "tbSave";
            this.tbSave.Tag = "Save";
            this.tbSave.ToolTipText = "Save";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            this.imageList1.Images.SetKeyName(4, "");
            this.imageList1.Images.SetKeyName(5, "");
            this.imageList1.Images.SetKeyName(6, "");
            this.imageList1.Images.SetKeyName(7, "");
            this.imageList1.Images.SetKeyName(8, "");
            this.imageList1.Images.SetKeyName(9, "");
            this.imageList1.Images.SetKeyName(10, "");
            this.imageList1.Images.SetKeyName(11, "");
            this.imageList1.Images.SetKeyName(12, "");
            this.imageList1.Images.SetKeyName(13, "");
            this.imageList1.Images.SetKeyName(14, "");
            this.imageList1.Images.SetKeyName(15, "");
            this.imageList1.Images.SetKeyName(16, "");
            this.imageList1.Images.SetKeyName(17, "");
            this.imageList1.Images.SetKeyName(18, "");
            this.imageList1.Images.SetKeyName(19, "");
            this.imageList1.Images.SetKeyName(20, "");
            this.imageList1.Images.SetKeyName(21, "");
            this.imageList1.Images.SetKeyName(22, "");
            this.imageList1.Images.SetKeyName(23, "");
            this.imageList1.Images.SetKeyName(24, "");
            this.imageList1.Images.SetKeyName(25, "");
            this.imageList1.Images.SetKeyName(26, "");
            this.imageList1.Images.SetKeyName(27, "");
            this.imageList1.Images.SetKeyName(28, "");
            // 
            // RTB
            // 
            this.RTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RTB.Location = new System.Drawing.Point(0, 28);
            this.RTB.Name = "RTB";
            this.RTB.Size = new System.Drawing.Size(552, 225);
            this.RTB.TabIndex = 5;
            this.RTB.Text = "";
            // 
            // RTFEditorDlg
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(552, 253);
            this.Controls.Add(this.RTB);
            this.Controls.Add(this.toolBar1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RTFEditorDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rich Text Editor";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.RTB_Dispose);
            this.Controls.SetChildIndex(this.toolBar1, 0);
            this.Controls.SetChildIndex(this.RTB, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

 
		private void var0(object sender, EventArgs e)
		{
			this.SettingContextMenu(this.RTB.SelectedText.Length);
		}

		private void RTB_Dispose(object sender, CancelEventArgs e)//var1
		{
			this.RTB.Dispose();
		}

		private void SetInsertField()//var11
		{
			InsertFieldDlg field1 = new InsertFieldDlg();
			field1.ShowDialog();
			if (field1.mtd4 && (field1.txtField.Text.Length > 0))
			{
				this.RTB.SelectedText = "[!" + field1.txtField.Text + "]";
			}
		}

		#region Menu bar

		private void toolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
		{
          MenuCmd(e.Button.Tag.ToString());
		}

		private void MenuCmd(string cmd)
		{
			switch (cmd)
			{
				case "OpenFile":
				{
					OpenFileDialog dialog1 = new OpenFileDialog();
					dialog1.Filter = "Report files (*.rtf)|";
					dialog1.FilterIndex = 0;
					dialog1.RestoreDirectory = true;
					if (((dialog1.ShowDialog(this) == DialogResult.OK) && dialog1.CheckPathExists) && (dialog1.FileName.Length > 0))
					{
						this.RTB.LoadFile(dialog1.FileName);
					}
					return;
				}
				case "Field":
				{
					this.SetInsertField();
					return;
				}
				case "Cut":
				{
					this.RTB.Cut();
					return;
				}
				case "Copy":
				{
					this.RTB.Copy();
					return;
				}
				case "Paste":
				{
					this.RTB.Paste();
					return;
				}
				case "Delete":
				{
					this.RTB.SelectedText = "";
					return;
				}
				case "Undo":
				{
					this.RTB.Undo();
					return;
				}
				case "Redo":
				{
					this.RTB.Redo();
					return;
				}
				case "Font":
				{
					FontDialog dialog2 = new FontDialog();
					if (dialog2.ShowDialog(this) == DialogResult.OK)
					{
						this.RTB.SelectionFont = dialog2.Font;
					}
					return;
				}
				case "ForeColor":
				{
					ColorDialog dialog3 = new ColorDialog();
					if (dialog3.ShowDialog(this) == DialogResult.OK)
					{
						this.RTB.SelectionColor = dialog3.Color;
					}
					return;
				}
				case "Bullet":
				{
					if (!this.RTB.SelectionBullet)
					{
						this.RTB.BulletIndent = 0x12;
						this.RTB.SelectionBullet = true;
						return;
					}
					this.RTB.SelectionBullet = false;
					return;
				}
				case "Left":
				{
					this.RTB.SelectionAlignment = HorizontalAlignment.Left;
					return;
				}
				case "Center":
				{
					this.RTB.SelectionAlignment = HorizontalAlignment.Center;
					return;
				}
				case "Right":
				{
					this.RTB.SelectionAlignment = HorizontalAlignment.Right;
					return;
				}
				case "MoreLI":
				{
					this.RTB.SelectionIndent += 0x12;
					return;
				}
				case "LessLI":
				{
					if ((this.RTB.SelectionIndent - 0x12) >= 0)
					{
						this.RTB.SelectionIndent -= 0x12;
						return;
					}
					this.RTB.SelectionIndent = 0;
					return;
				}
				case "MoreRI":
				{
					this.RTB.SelectionRightIndent += 0x12;
					return;
				}
				case "LessRI":
				{
					if ((this.RTB.SelectionRightIndent - 0x12) >= 0)
					{
						this.RTB.SelectionRightIndent -= 0x12;
						return;
					}
					this.RTB.SelectionRightIndent = 0;
					return;
				}
				case "MoreFLI":
				{
					this.RTB.SelectionHangingIndent += 0x12;
					return;
				}
				case "LessFLI":
				{
					if ((this.RTB.SelectionHangingIndent - 0x12) >= 0)
					{
						this.RTB.SelectionHangingIndent -= 0x12;
						return;
					}
					this.RTB.SelectionHangingIndent = 0;
					return;
				}
				case "OK":
					this.mtd4 = true;
					base.Close();
					break;
			}
		}

		#endregion

//		private void var3()
//		{
//			int num1 = 4;
//			this.mtd396 = new CtlInternal("OpenFile", "Open RTF File", Color.Gainsboro);
//			this.mtd396.mtd8 = FlatStyle.Flat;
//			this.mtd396.Location = new Point(num1, 2);
//			this.mtd396.mtd9 = this.var2.Images[0];//0x2a];
//			this.mtd396.MouseDown += new MouseEventHandler(this.var4);
//			num1 += 0x1a;
//			this.mtd204 = new CtlInternal("Field", "Insert Data Field", Color.Gainsboro);
//			this.mtd204.mtd8 = FlatStyle.Flat;
//			this.mtd204.Location = new Point(num1, 2);
//			this.mtd204.mtd9 = this.var2.Images[1];//0x29];
//			this.mtd204.MouseDown += new MouseEventHandler(this.var4);
//			num1 += 0x1a;
//			this.mtd397 = new CtlInternal("Cut", "Cut", Color.Gainsboro);
//			this.mtd397.mtd8 = FlatStyle.Flat;
//			this.mtd397.Location = new Point(num1, 2);
//			this.mtd397.mtd9 = this.var2.Images[2];//0x11];
//			this.mtd397.MouseDown += new MouseEventHandler(this.var4);
//			num1 += 0x18;
//			this.mtd398 = new CtlInternal("Copy", "Copy", Color.Gainsboro);
//			this.mtd398.mtd8 = FlatStyle.Flat;
//			this.mtd398.Location = new Point(num1, 2);
//			this.mtd398.mtd9 = this.var2.Images[3];//0x12];
//			this.mtd398.MouseDown += new MouseEventHandler(this.var4);
//			num1 += 0x18;
//			this.mtd399 = new CtlInternal("Paste", "Paste", Color.Gainsboro);
//			this.mtd399.mtd8 = FlatStyle.Flat;
//			this.mtd399.Location = new Point(num1, 2);
//			this.mtd399.mtd9 = this.var2.Images[4];//0x13];
//			this.mtd399.MouseDown += new MouseEventHandler(this.var4);
//			num1 += 0x18;
//			this.mtd400 = new CtlInternal("Delete", "Delete", Color.Gainsboro);
//			this.mtd400.mtd8 = FlatStyle.Flat;
//			this.mtd400.Location = new Point(num1, 2);
//			this.mtd400.mtd9 = this.var2.Images[5];//20];
//			this.mtd400.MouseDown += new MouseEventHandler(this.var4);
//			num1 += 0x1a;
//			this.mtd401 = new CtlInternal("Undo", "Undo", Color.Gainsboro);
//			this.mtd401.mtd8 = FlatStyle.Flat;
//			this.mtd401.Location = new Point(num1, 2);
//			this.mtd401.mtd9 = this.var2.Images[6];//0x2c];
//			this.mtd401.MouseDown += new MouseEventHandler(this.var4);
//			num1 += 0x18;
//			this.mtd402 = new CtlInternal("Redo", "Redo", Color.Gainsboro);
//			this.mtd402.mtd8 = FlatStyle.Flat;
//			this.mtd402.Location = new Point(num1, 2);
//			this.mtd402.mtd9 = this.var2.Images[7];//0x2b];
//			this.mtd402.MouseDown += new MouseEventHandler(this.var4);
//			num1 += 0x1a;
//			this.mtd132 = new CtlInternal("Font", "Font", Color.Gainsboro);
//			this.mtd132.mtd8 = FlatStyle.Flat;
//			this.mtd132.Location = new Point(num1, 2);
//			this.mtd132.mtd9 = this.var2.Images[8];//0x18];
//			this.mtd132.MouseDown += new MouseEventHandler(this.var4);
//			num1 += 0x18;
//			this.mtd40 = new CtlInternal("ForeColor", "ForeColor", Color.Gainsboro);
//			this.mtd40.mtd8 = FlatStyle.Flat;
//			this.mtd40.Location = new Point(num1, 2);
//			this.mtd40.mtd9 = this.var2.Images[9];//0x21];
//			this.mtd40.MouseDown += new MouseEventHandler(this.var4);
//			num1 += 0x1a;
//			this.mtd28 = new CtlInternal("Left", "Left", Color.Gainsboro);
//			this.mtd28.mtd8 = FlatStyle.Flat;
//			this.mtd28.Location = new Point(num1, 2);
//			this.mtd28.mtd9 = this.var2.Images[10];//0x19];
//			this.mtd28.MouseDown += new MouseEventHandler(this.var4);
//			num1 += 0x18;
//			this.mtd403 = new CtlInternal("Center", "Center", Color.Gainsboro);
//			this.mtd403.mtd8 = FlatStyle.Flat;
//			this.mtd403.Location = new Point(num1, 2);
//			this.mtd403.mtd9 = this.var2.Images[11];//0x1a];
//			this.mtd403.MouseDown += new MouseEventHandler(this.var4);
//			num1 += 0x18;
//			this.mtd404 = new CtlInternal("Right", "Right", Color.Gainsboro);
//			this.mtd404.mtd8 = FlatStyle.Flat;
//			this.mtd404.Location = new Point(num1, 2);
//			this.mtd404.mtd9 = this.var2.Images[12];//0x1b];
//			this.mtd404.MouseDown += new MouseEventHandler(this.var4);
//			num1 += 0x1a;
//			this.mtd405 = new CtlInternal("Bullet", "Bullet", Color.Gainsboro);
//			this.mtd405.mtd8 = FlatStyle.Flat;
//			this.mtd405.Location = new Point(num1, 2);
//			this.mtd405.mtd9 = this.var2.Images[13];//0x1d];
//			this.mtd405.MouseDown += new MouseEventHandler(this.var4);
//			num1 += 0x18;
//			this.mtd407 = new CtlInternal("LessLI", "Decrease Left Indent", Color.Gainsboro);
//			this.mtd407.mtd8 = FlatStyle.Flat;
//			this.mtd407.Location = new Point(num1, 2);
//			this.mtd407.mtd9 = this.var2.Images[14];//0x23];
//			this.mtd407.MouseDown += new MouseEventHandler(this.var4);
//			num1 += 0x18;
//			this.mtd406 = new CtlInternal("MoreLI", "Increase Left Indent", Color.Gainsboro);
//			this.mtd406.mtd8 = FlatStyle.Flat;
//			this.mtd406.Location = new Point(num1, 2);
//			this.mtd406.mtd9 = this.var2.Images[15];//0x24];
//			this.mtd406.MouseDown += new MouseEventHandler(this.var4);
//			num1 += 0x18;
//			this.mtd408 = new CtlInternal("MoreRI", "Increase Right Indent", Color.Gainsboro);
//			this.mtd408.mtd8 = FlatStyle.Flat;
//			this.mtd408.Location = new Point(num1, 2);
//			this.mtd408.mtd9 = this.var2.Images[16];//0x26];
//			this.mtd408.MouseDown += new MouseEventHandler(this.var4);
//			num1 += 0x18;
//			this.mtd409 = new CtlInternal("LessRI", "Decrease Right Indent", Color.Gainsboro);
//			this.mtd409.mtd8 = FlatStyle.Flat;
//			this.mtd409.Location = new Point(num1, 2);
//			this.mtd409.mtd9 = this.var2.Images[17];//0x25];
//			this.mtd409.MouseDown += new MouseEventHandler(this.var4);
//			num1 += 0x18;
//			this.mtd410 = new CtlInternal("MoreFLI", "Increase First Line Indent", Color.Gainsboro);
//			this.mtd410.mtd8 = FlatStyle.Flat;
//			this.mtd410.Location = new Point(num1, 2);
//			this.mtd410.mtd9 = this.var2.Images[18];//40];
//			this.mtd410.MouseDown += new MouseEventHandler(this.var4);
//			num1 += 0x18;
//			this.mtd411 = new CtlInternal("LessFLI", "Decrease First Line Indent", Color.Gainsboro);
//			this.mtd411.mtd8 = FlatStyle.Flat;
//			this.mtd411.Location = new Point(num1, 2);
//			this.mtd411.mtd9 = this.var2.Images[19];//0x27];
//			this.mtd411.MouseDown += new MouseEventHandler(this.var4);
//			this.mtd412 = new CtlInternal("Cancel", "Cancel", new Size(0x48, 0x18));
//			this.mtd412.Text = "Cancel";
//			this.mtd412.mtd8 = FlatStyle.Standard;
//			this.mtd412.BackColor = Color.Gainsboro;
//			this.mtd412.Location = new Point(0x1a8, 0x110);
//			this.mtd412.MouseDown += new MouseEventHandler(this.var5);
//			this.mtd4 = new CtlInternal("OK", "OK", new Size(0x48, 0x18));
//			this.mtd4.Text = "OK";
//			this.mtd4.mtd8 = FlatStyle.Standard;
//			this.mtd4.BackColor = Color.Gainsboro;
//			this.mtd4.Location = new Point(0x15a, 0x110);
//			this.mtd4.MouseDown += new MouseEventHandler(this.var6);
//			this.pnltoolbar.Controls.AddRange(new Control[] { 
//																this.mtd396, this.mtd204, this.mtd397, this.mtd398, this.mtd399, this.mtd400, this.mtd401, this.mtd402, this.mtd132, this.mtd40, this.mtd28, this.mtd403, this.mtd404, this.mtd405, this.mtd406, this.mtd407, 
//																this.mtd408, this.mtd409, this.mtd410, this.mtd411
//															});
//			base.Controls.AddRange(new Control[] { this.mtd412, this.mtd4 });
//		}
//
//		private void var4(object sender, MouseEventArgs e)
//		{
//			this.var12(((CtlInternal) sender).Name);
//		}
//
//		private void var5(object sender, MouseEventArgs e)
//		{
//			this.mtd4 = false;
//			base.Close();
//		}
//
//		private void var6(object sender, MouseEventArgs e)
//		{
//			this.mtd4 = true;
//			base.Close();
//		}

 
		private void InitContextMenu()//var7
		{
			this._ContextMenu.MenuItems.Add(new MenuItem("Insert Field", new EventHandler(this.var8)));
			this._ContextMenu.MenuItems.Add(new MenuItem("-"));
			this._ContextMenu.MenuItems.Add(new MenuItem("Cut", new EventHandler(this.var8)));
			this._ContextMenu.MenuItems.Add(new MenuItem("Copy", new EventHandler(this.var8)));
			this._ContextMenu.MenuItems.Add(new MenuItem("Paste", new EventHandler(this.var8)));
			this._ContextMenu.MenuItems.Add(new MenuItem("Delete", new EventHandler(this.var8)));
			this.SettingContextMenu(0);
			this.RTB.ContextMenu = this._ContextMenu;
		}

 
		private void var8(object sender, EventArgs e)
		{
			string text1 = ((MenuItem) sender).Text;
			if (text1 == "Insert Field")
			{
				this.SetInsertField();
			}
			else
			{
				this.MenuCmd(text1);
			}
		}

		private void SettingContextMenu(int i)//var9
		{
			if (i > 0)
			{
				this._ContextMenu.MenuItems[2].Enabled = true;
				this._ContextMenu.MenuItems[3].Enabled = true;
				this._ContextMenu.MenuItems[5].Enabled = true;
			}
			else
			{
				this._ContextMenu.MenuItems[2].Enabled = false;
				this._ContextMenu.MenuItems[3].Enabled = false;
				this._ContextMenu.MenuItems[5].Enabled = false;
			}
		}

 
		public string Rtf//mtd52
		{
			get
			{
				return this.RTB.Rtf;
			}
			set
			{
				this.RTB.Rtf = value;
			}
		}


		// Fields
//		internal CtlInternal mtd132;
//		internal CtlInternal mtd204;
//		internal CtlInternal mtd28;
//		internal ContextMenu mtd395;
//		internal CtlInternal mtd396;
//		internal CtlInternal mtd397;
//		internal CtlInternal mtd398;
//		internal CtlInternal mtd399;
//		internal CtlInternal mtd4;
//		internal CtlInternal mtd40;
//		internal CtlInternal mtd400;
//		internal CtlInternal mtd401;
//		internal CtlInternal mtd402;
//		internal CtlInternal mtd403;
//		internal CtlInternal mtd404;
//		internal CtlInternal mtd405;
//		internal CtlInternal mtd406;
//		internal CtlInternal mtd407;
//		internal CtlInternal mtd408;
//		internal CtlInternal mtd409;
//		internal CtlInternal mtd410;
//		internal CtlInternal mtd411;
//		internal CtlInternal mtd412;
//		private ImageList var2;

	}

}
