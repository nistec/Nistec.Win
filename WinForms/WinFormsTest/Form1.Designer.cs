using Nistec.Win;
namespace WinCtlTest
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            Nistec.WinForms.ValidatorRule validatorRule1 = new Nistec.WinForms.ValidatorRule();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Node1");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Node2");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Node3");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Node0", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Node5");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Node6");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Node7");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Node4", new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6,
            treeNode7});
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Node1");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Node0");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Node1");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Root", new System.Windows.Forms.TreeNode[] {
            treeNode9,
            treeNode10,
            treeNode11});
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Node5");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Node6");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Node2");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Node1", new System.Windows.Forms.TreeNode[] {
            treeNode13,
            treeNode14,
            treeNode15});
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Node4");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Node5");
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("Node3", new System.Windows.Forms.TreeNode[] {
            treeNode17,
            treeNode18});
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("Node7");
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("Node6", new System.Windows.Forms.TreeNode[] {
            treeNode20});
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("Node9");
            System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("Node10");
            System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("Node11");
            System.Windows.Forms.TreeNode treeNode25 = new System.Windows.Forms.TreeNode("Node8", new System.Windows.Forms.TreeNode[] {
            treeNode22,
            treeNode23,
            treeNode24});
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolBar = new Nistec.WinForms.McToolBar();
            this.tbStyles = new Nistec.WinForms.McToolButton();
            this.Desktop = new Nistec.WinForms.PopUpItem();
            this.SteelBlue = new Nistec.WinForms.PopUpItem();
            this.Goldenrod = new Nistec.WinForms.PopUpItem();
            this.SeaGreen = new Nistec.WinForms.PopUpItem();
            this.Carmel = new Nistec.WinForms.PopUpItem();
            this.Silver = new Nistec.WinForms.PopUpItem();
            this.Media = new Nistec.WinForms.PopUpItem();
            this.SystemStyle = new Nistec.WinForms.PopUpItem();
            this.tbControlLayout = new Nistec.WinForms.McToolButton();
            this.Visual = new Nistec.WinForms.PopUpItem();
            this.Flat = new Nistec.WinForms.PopUpItem();
            this.XpLayout = new Nistec.WinForms.PopUpItem();
            this.VistaLayout = new Nistec.WinForms.PopUpItem();
            this.SystemLayout = new Nistec.WinForms.PopUpItem();
            this.tbValidation = new Nistec.WinForms.McToolButton();
            this.tbPermission = new Nistec.WinForms.McToolButton();
            this.tbResources = new Nistec.WinForms.McToolButton();
            this.tbProperty = new Nistec.WinForms.McToolButton();
            this.ctlDockingManager = new Nistec.WinForms.McDocking();
            this.dockPanelBottom = new Nistec.WinForms.McDockingPanel();
            this.dockTabTaskBar = new Nistec.WinForms.McDockingTab();
            this.ctlHelpLabel1 = new Nistec.WinForms.McHelpLabel();
            this.mskIP = new Nistec.WinForms.McMaskedBox();
            this.txtDate = new Nistec.WinForms.McTextBox();
            this.mskDate = new Nistec.WinForms.McMaskedBox();
            this.ctlSpinEdit1 = new Nistec.WinForms.McSpinEdit();
            this.mskPhone = new Nistec.WinForms.McMaskedBox();
            this.txtNumber = new Nistec.WinForms.McTextBox();
            this.txtSimple = new Nistec.WinForms.McTextBox();
            this.dockTabMessage = new Nistec.WinForms.McDockingTab();
            this.txtMessages = new Nistec.WinForms.McTextBox();
            this.dockTabTrace = new Nistec.WinForms.McDockingTab();
            this.ctlListBox1 = new Nistec.WinForms.McListBox();
            this.dockPanelLeft = new Nistec.WinForms.McDockingPanel();
            this.dockTabTools = new Nistec.WinForms.McDockingTab();
            this.ctlTaskPanelBar1 = new Nistec.WinForms.McTaskBar();
            this.tpHook = new Nistec.WinForms.McTaskPanel();
            this.linkShowNotify = new Nistec.WinForms.LinkLabelItem();
            this.linkShowMsg = new Nistec.WinForms.LinkLabelItem();
            this.linkInputBox = new Nistec.WinForms.LinkLabelItem();
            this.tpResources = new Nistec.WinForms.McTaskPanel();
            this.linkLabelItem4 = new Nistec.WinForms.LinkLabelItem();
            this.linkLabelItem5 = new Nistec.WinForms.LinkLabelItem();
            this.tpPermission = new Nistec.WinForms.McTaskPanel();
            this.linkLabelItem6 = new Nistec.WinForms.LinkLabelItem();
            this.linkLabelItem7 = new Nistec.WinForms.LinkLabelItem();
            this.tpValidation = new Nistec.WinForms.McTaskPanel();
            this.ctlTreeView1 = new Nistec.WinForms.McTreeView();
            this.dockTabTree = new Nistec.WinForms.McDockingTab();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.dockPanelRight = new Nistec.WinForms.McDockingPanel();
            this.dockTabProperty = new Nistec.WinForms.McDockingTab();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.ctlTabControl1 = new Nistec.WinForms.McTabControl();
            this.pageEditControls = new Nistec.WinForms.McTabPage();
            this.ctlLabel7 = new Nistec.WinForms.McLabel();
            this.ctlLabel6 = new Nistec.WinForms.McLabel();
            this.ctlLabel5 = new Nistec.WinForms.McLabel();
            this.ctlLabel4 = new Nistec.WinForms.McLabel();
            this.ctlLabel3 = new Nistec.WinForms.McLabel();
            this.ctlLabel2 = new Nistec.WinForms.McLabel();
            this.ctlLabel1 = new Nistec.WinForms.McLabel();
            this.pageCommand = new Nistec.WinForms.McTabPage();
            this.btnMenu2 = new Nistec.WinForms.McButtonMenu();
            this.popUpItem4 = new Nistec.WinForms.PopUpItem();
            this.popUpItem5 = new Nistec.WinForms.PopUpItem();
            this.popUpItem6 = new Nistec.WinForms.PopUpItem();
            this.btnProgress2 = new Nistec.WinForms.McButton();
            this.ctlProgressBar2 = new Nistec.WinForms.McProgressBar();
            this.btnProgress1 = new Nistec.WinForms.McButton();
            this.ctlXpLine1 = new Nistec.WinForms.McShapes();
            this.ctlProgressBar1 = new Nistec.WinForms.McProgressBar();
            this.ctlLinkLabel1 = new Nistec.WinForms.McLinkLabel();
            this.btnMenu1 = new Nistec.WinForms.McButtonMenu();
            this.popUpItem1 = new Nistec.WinForms.PopUpItem();
            this.popUpItem2 = new Nistec.WinForms.PopUpItem();
            this.popUpItem3 = new Nistec.WinForms.PopUpItem();
            this.btnError = new Nistec.WinForms.McButton();
            this.btnInfo = new Nistec.WinForms.McButton();
            this.pageCombos = new Nistec.WinForms.McTabPage();
            this.ctlImageList = new Nistec.WinForms.McMultiPicker();
            this.ctlLabel8 = new Nistec.WinForms.McLabel();
            this.ctlLabel9 = new Nistec.WinForms.McLabel();
            this.ctlLabel10 = new Nistec.WinForms.McLabel();
            this.ctlLabel11 = new Nistec.WinForms.McLabel();
            this.ctlLabel12 = new Nistec.WinForms.McLabel();
            this.ctlLabel13 = new Nistec.WinForms.McLabel();
            this.ctlLabel14 = new Nistec.WinForms.McLabel();
            this.ctlDatePicker1 = new Nistec.WinForms.McDatePicker();
            this.ctlBoolean = new Nistec.WinForms.McMultiPicker();
            this.ctlFonts = new Nistec.WinForms.McMultiPicker();
            this.ctlColors = new Nistec.WinForms.McMultiPicker();
            this.ctlDropDown1 = new Nistec.WinForms.McMultiPicker();
            this.ctlComboBox1 = new Nistec.WinForms.McComboBox();
            this.pageMulti = new Nistec.WinForms.McTabPage();
            this.ctlMultiBox7 = new Nistec.WinForms.McMultiBox();
            this.ctlMultiBox6 = new Nistec.WinForms.McMultiBox();
            this.ctlMultiBox5 = new Nistec.WinForms.McMultiBox();
            this.ctlMultiBox4 = new Nistec.WinForms.McMultiBox();
            this.ctlMultiBox3 = new Nistec.WinForms.McMultiBox();
            this.ctlMultiBox2 = new Nistec.WinForms.McMultiBox();
            this.ctlLabel15 = new Nistec.WinForms.McLabel();
            this.ctlLabel16 = new Nistec.WinForms.McLabel();
            this.ctlLabel17 = new Nistec.WinForms.McLabel();
            this.ctlLabel18 = new Nistec.WinForms.McLabel();
            this.ctlLabel19 = new Nistec.WinForms.McLabel();
            this.ctlLabel20 = new Nistec.WinForms.McLabel();
            this.ctlLabel21 = new Nistec.WinForms.McLabel();
            this.ctlMultiBox1 = new Nistec.WinForms.McMultiBox();
            this.ctlValidator1 = new Nistec.WinForms.McValidator(this.components);
            this.ctlResource1 = new Nistec.WinForms.McResource(this.components);
            //this.ctlPermission1 = new Nistec.WinForms.McPermission(this.components);
            this.ctlPanel1 = new Nistec.WinForms.McPanel();
            this.ctlContextStrip1 = new Nistec.WinForms.McContextStrip();
            this.context1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.context2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBar.SuspendLayout();
            this.dockPanelBottom.SuspendLayout();
            this.dockTabTaskBar.SuspendLayout();
            this.dockTabMessage.SuspendLayout();
            this.dockTabTrace.SuspendLayout();
            this.dockPanelLeft.SuspendLayout();
            this.dockTabTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctlTaskPanelBar1)).BeginInit();
            this.ctlTaskPanelBar1.SuspendLayout();
            this.tpHook.SuspendLayout();
            this.tpResources.SuspendLayout();
            this.tpPermission.SuspendLayout();
            this.tpValidation.SuspendLayout();
            this.dockTabTree.SuspendLayout();
            this.dockPanelRight.SuspendLayout();
            this.dockTabProperty.SuspendLayout();
            this.ctlTabControl1.SuspendLayout();
            this.pageEditControls.SuspendLayout();
            this.pageCommand.SuspendLayout();
            this.pageCombos.SuspendLayout();
            this.pageMulti.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctlResource1)).BeginInit();
            this.ctlPanel1.SuspendLayout();
            this.ctlContextStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // StyleGuideBase
            // 
            this.StyleGuideBase.AlternatingColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(230)))), ((int)(((byte)(220)))));
            this.StyleGuideBase.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(106)))), ((int)(((byte)(197)))));
            this.StyleGuideBase.BorderHotColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(80)))), ((int)(((byte)(184)))));
            this.StyleGuideBase.CaptionColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(126)))), ((int)(((byte)(177)))));
            this.StyleGuideBase.ColorBrush1 = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(203)))), ((int)(((byte)(183)))));
            this.StyleGuideBase.ColorBrush2 = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.FocusedColor = System.Drawing.Color.RoyalBlue;
            this.StyleGuideBase.FormColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.StylePlan = Nistec.WinForms.Styles.Desktop;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
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
            // 
            // toolBar
            // 
            this.toolBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(203)))), ((int)(((byte)(183)))));
            this.toolBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolBar.ControlLayout = Nistec.WinForms.ControlLayout.Visual;
            this.toolBar.Controls.Add(this.tbStyles);
            this.toolBar.Controls.Add(this.tbControlLayout);
            this.toolBar.Controls.Add(this.tbValidation);
            this.toolBar.Controls.Add(this.tbPermission);
            this.toolBar.Controls.Add(this.tbResources);
            this.toolBar.Controls.Add(this.tbProperty);
            this.toolBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.toolBar.FixSize = false;
            this.toolBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.toolBar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.toolBar.ImageList = this.imageList1;
            this.toolBar.Location = new System.Drawing.Point(2, 38);
            this.toolBar.Name = "toolBar";
            this.toolBar.Padding = new System.Windows.Forms.Padding(12, 3, 3, 3);
            this.toolBar.SelectedGroup = -1;
            this.toolBar.Size = new System.Drawing.Size(784, 28);
            this.toolBar.StylePainter = this.StyleGuideBase;
            this.toolBar.TabIndex = 10;
            this.toolBar.ButtonClick += new Nistec.WinForms.ToolButtonClickEventHandler(this.toolBar_ButtonClick);
            // 
            // tbStyles
            // 
            this.tbStyles.ButtonStyle = Nistec.WinForms.ToolButtonStyle.DropDownButton;
            this.tbStyles.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbStyles.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbStyles.DrawItemStyle = Nistec.WinForms.DrawItemStyle.CheckBox;
            this.tbStyles.ImageList = this.imageList1;
            this.tbStyles.Location = new System.Drawing.Point(213, 3);
            this.tbStyles.MenuItems.AddRange(new Nistec.WinForms.PopUpItem[] {
            this.Desktop,
            this.SteelBlue,
            this.Goldenrod,
            this.SeaGreen,
            this.Carmel,
            this.Silver,
            this.Media,
            this.SystemStyle});
            this.tbStyles.Name = "tbStyles";
            this.tbStyles.Size = new System.Drawing.Size(113, 22);
            this.tbStyles.StylePainter = this.StyleGuideBase;
            this.tbStyles.TabIndex = 6;
            this.tbStyles.Text = "Color Style";
            this.tbStyles.ToolTipText = "Color Style";
            // 
            // Desktop
            // 
            this.Desktop.Text = "Desktop";
            // 
            // SteelBlue
            // 
            this.SteelBlue.Text = "SteelBlue";
            // 
            // Goldenrod
            // 
            this.Goldenrod.Text = "Goldenrod";
            // 
            // SeaGreen
            // 
            this.SeaGreen.Text = "SeaGreen";
            // 
            // Carmel
            // 
            this.Carmel.Text = "Carmel";
            // 
            // Silver
            // 
            this.Silver.Text = "Silver";
            // 
            // Media
            // 
            this.Media.Text = "Media";
            // 
            // SystemStyle
            // 
            this.SystemStyle.Text = "System";
            // 
            // tbControlLayout
            // 
            this.tbControlLayout.ButtonStyle = Nistec.WinForms.ToolButtonStyle.DropDownButton;
            this.tbControlLayout.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbControlLayout.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbControlLayout.DrawItemStyle = Nistec.WinForms.DrawItemStyle.CheckBox;
            this.tbControlLayout.ImageList = this.imageList1;
            this.tbControlLayout.Location = new System.Drawing.Point(100, 3);
            this.tbControlLayout.MenuItems.AddRange(new Nistec.WinForms.PopUpItem[] {
            this.Visual,
            this.Flat,
            this.XpLayout,
            this.VistaLayout,
            this.SystemLayout});
            this.tbControlLayout.Name = "tbControlLayout";
            this.tbControlLayout.Size = new System.Drawing.Size(113, 22);
            this.tbControlLayout.StylePainter = this.StyleGuideBase;
            this.tbControlLayout.TabIndex = 5;
            this.tbControlLayout.Text = "Control Layout";
            this.tbControlLayout.ToolTipText = "Control Layout";
            // 
            // Visual
            // 
            this.Visual.Text = "Visual";
            // 
            // Flat
            // 
            this.Flat.Text = "Flat";
            // 
            // XpLayout
            // 
            this.XpLayout.Text = "XpLayout";
            // 
            // VistaLayout
            // 
            this.VistaLayout.Text = "VistaLayout";
            // 
            // SystemLayout
            // 
            this.SystemLayout.Text = "System";
            // 
            // tbValidation
            // 
            this.tbValidation.ButtonStyle = Nistec.WinForms.ToolButtonStyle.Button;
            this.tbValidation.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbValidation.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbValidation.ImageList = this.imageList1;
            this.tbValidation.Location = new System.Drawing.Point(78, 3);
            this.tbValidation.Name = "tbValidation";
            this.tbValidation.Size = new System.Drawing.Size(22, 22);
            this.tbValidation.StylePainter = this.StyleGuideBase;
            this.tbValidation.TabIndex = 3;
            this.tbValidation.ToolTipText = "Do Validation Action";
            // 
            // tbPermission
            // 
            this.tbPermission.ButtonStyle = Nistec.WinForms.ToolButtonStyle.Button;
            this.tbPermission.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbPermission.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbPermission.ImageList = this.imageList1;
            this.tbPermission.Location = new System.Drawing.Point(56, 3);
            this.tbPermission.Name = "tbPermission";
            this.tbPermission.Size = new System.Drawing.Size(22, 22);
            this.tbPermission.StylePainter = this.StyleGuideBase;
            this.tbPermission.TabIndex = 2;
            this.tbPermission.ToolTipText = "Do Permission Action";
            // 
            // tbResources
            // 
            this.tbResources.ButtonStyle = Nistec.WinForms.ToolButtonStyle.Button;
            this.tbResources.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbResources.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbResources.ImageList = this.imageList1;
            this.tbResources.Location = new System.Drawing.Point(34, 3);
            this.tbResources.Name = "tbResources";
            this.tbResources.Size = new System.Drawing.Size(22, 22);
            this.tbResources.StylePainter = this.StyleGuideBase;
            this.tbResources.TabIndex = 1;
            this.tbResources.ToolTipText = "Do resources action";
            // 
            // tbProperty
            // 
            this.tbProperty.ButtonStyle = Nistec.WinForms.ToolButtonStyle.Button;
            this.tbProperty.DialogResult = System.Windows.Forms.DialogResult.None;
            this.tbProperty.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbProperty.ImageList = this.imageList1;
            this.tbProperty.Location = new System.Drawing.Point(12, 3);
            this.tbProperty.Name = "tbProperty";
            this.tbProperty.Size = new System.Drawing.Size(22, 22);
            this.tbProperty.StylePainter = this.StyleGuideBase;
            this.tbProperty.TabIndex = 0;
            this.tbProperty.ToolTipText = "Set Property for active control";
            // 
            // ctlDockingManager
            // 
            this.ctlDockingManager.ClosedControls = ((System.Collections.ArrayList)(resources.GetObject("ctlDockingManager.ClosedControls")));
            this.ctlDockingManager.ParentForm = this;
            this.ctlDockingManager.UndockedPanels = ((System.Collections.ArrayList)(resources.GetObject("ctlDockingManager.UndockedPanels")));
            // 
            // dockPanelBottom
            // 
            this.dockPanelBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dockPanelBottom.Controls.Add(this.dockTabTaskBar);
            this.dockPanelBottom.Controls.Add(this.dockTabMessage);
            this.dockPanelBottom.Controls.Add(this.dockTabTrace);
            this.dockPanelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dockPanelBottom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.dockPanelBottom.Location = new System.Drawing.Point(2, 390);
            this.dockPanelBottom.Manager = this.ctlDockingManager;
            this.dockPanelBottom.Name = "dockPanelBottom";
            this.dockPanelBottom.Padding = new System.Windows.Forms.Padding(1, 24, 1, 24);
            this.dockPanelBottom.Size = new System.Drawing.Size(784, 87);
            this.dockPanelBottom.StylePainter = this.StyleGuideBase;
            this.dockPanelBottom.TabIndex = 11;
            // 
            // dockTabTaskBar
            // 
            this.dockTabTaskBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.dockTabTaskBar.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dockTabTaskBar.Controls.Add(this.ctlHelpLabel1);
            this.dockTabTaskBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockTabTaskBar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dockTabTaskBar.Guid = "f8837132-cee7-4a22-990d-2a7230127c1d";
            this.dockTabTaskBar.Image = null;
            this.dockTabTaskBar.Location = new System.Drawing.Point(1, 24);
            this.dockTabTaskBar.Name = "dockTabTaskBar";
            this.dockTabTaskBar.Padding = new System.Windows.Forms.Padding(2);
            this.dockTabTaskBar.Size = new System.Drawing.Size(782, 39);
            this.dockTabTaskBar.StylePainter = this.StyleGuideBase;
            this.dockTabTaskBar.TabIndex = 0;
            this.dockTabTaskBar.Text = "TaskBar";
            // 
            // ctlHelpLabel1
            // 
            this.ctlHelpLabel1.BackColor = System.Drawing.SystemColors.Info;
            this.ctlHelpLabel1.BorderColor = System.Drawing.Color.Black;
            this.ctlHelpLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlHelpLabel1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.ctlHelpLabel1.Location = new System.Drawing.Point(2, 2);
            this.ctlHelpLabel1.Name = "ctlHelpLabel1";
            this.ctlHelpLabel1.Size = new System.Drawing.Size(778, 35);
            this.ctlHelpLabel1.TabIndex = 0;
            this.ctlHelpLabel1.TabStop = false;
            this.ctlHelpLabel1.TrackSelection = true;
            // 
            // mskIP
            // 
            this.mskIP.BackColor = System.Drawing.Color.White;
            this.mskIP.ForeColor = System.Drawing.Color.Black;
            this.mskIP.InputMask = "990-990-000-000";
            this.mskIP.Location = new System.Drawing.Point(176, 144);
            this.mskIP.MaxLength = 15;
            this.mskIP.Name = "mskIP";
            this.mskIP.Size = new System.Drawing.Size(168, 20);
            this.ctlHelpLabel1.SetStatusText(this.mskIP, "Mask IP");
            this.mskIP.StylePainter = this.StyleGuideBase;
            this.mskIP.TabIndex = 9;
            this.mskIP.Text = "___-___-___-___";
            // 
            // txtDate
            // 
            this.txtDate.BackColor = System.Drawing.Color.White;
            this.txtDate.ForeColor = System.Drawing.Color.Black;
            this.txtDate.Format = "d";
            this.txtDate.Location = new System.Drawing.Point(176, 72);
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(168, 20);
            this.ctlHelpLabel1.SetStatusText(this.txtDate, "Format Date");
            this.txtDate.StylePainter = this.StyleGuideBase;
            this.txtDate.TabIndex = 8;
            // 
            // mskDate
            // 
            this.mskDate.BackColor = System.Drawing.Color.White;
            this.mskDate.ForeColor = System.Drawing.Color.Black;
            this.mskDate.InputMask = "00/00/0000";
            this.mskDate.Location = new System.Drawing.Point(176, 120);
            this.mskDate.MaxLength = 10;
            this.mskDate.Name = "mskDate";
            this.mskDate.Size = new System.Drawing.Size(168, 20);
            this.ctlHelpLabel1.SetStatusText(this.mskDate, "Mask date");
            this.mskDate.StylePainter = this.StyleGuideBase;
            this.mskDate.TabIndex = 7;
            this.mskDate.Text = "__/__/____";
            // 
            // ctlSpinEdit1
            // 
            this.ctlSpinEdit1.BackColor = System.Drawing.Color.White;
            this.ctlSpinEdit1.ButtonAlign = Nistec.WinForms.ButtonAlign.Right;
            this.ctlSpinEdit1.DecimalPlaces = 0;
            this.ctlSpinEdit1.DefaultValue = "";
            this.ctlSpinEdit1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ctlSpinEdit1.Format = "N";
            this.ctlSpinEdit1.FormatType = NumberFormats.StandadNumber;
            this.ctlSpinEdit1.Location = new System.Drawing.Point(176, 168);
            this.ctlSpinEdit1.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.ctlSpinEdit1.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ctlSpinEdit1.Name = "ctlSpinEdit1";
            this.ctlSpinEdit1.Size = new System.Drawing.Size(168, 20);
            this.ctlHelpLabel1.SetStatusText(this.ctlSpinEdit1, "Spin Edit");
            this.ctlSpinEdit1.StylePainter = this.StyleGuideBase;
            this.ctlSpinEdit1.TabIndex = 6;
            this.ctlSpinEdit1.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // mskPhone
            // 
            this.mskPhone.BackColor = System.Drawing.Color.White;
            this.mskPhone.ForeColor = System.Drawing.Color.Black;
            this.mskPhone.InputMask = "999-0000000";
            this.mskPhone.Location = new System.Drawing.Point(176, 96);
            this.mskPhone.MaxLength = 11;
            this.mskPhone.Name = "mskPhone";
            this.mskPhone.Size = new System.Drawing.Size(168, 20);
            this.ctlHelpLabel1.SetStatusText(this.mskPhone, "Mask Phone Number");
            this.mskPhone.StylePainter = this.StyleGuideBase;
            this.mskPhone.TabIndex = 5;
            this.mskPhone.Text = "___-_______";
            // 
            // txtNumber
            // 
            this.txtNumber.BackColor = System.Drawing.Color.White;
            this.txtNumber.ForeColor = System.Drawing.Color.Black;
            this.txtNumber.Format = "N";
            this.txtNumber.Location = new System.Drawing.Point(176, 48);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(168, 20);
            this.ctlHelpLabel1.SetStatusText(this.txtNumber, "Format Number");
            this.txtNumber.StylePainter = this.StyleGuideBase;
            this.txtNumber.TabIndex = 4;
            // 
            // txtSimple
            // 
            this.txtSimple.BackColor = System.Drawing.Color.White;
            this.txtSimple.ForeColor = System.Drawing.Color.Black;
            this.txtSimple.Location = new System.Drawing.Point(176, 24);
            this.txtSimple.Name = "txtSimple";
            this.txtSimple.Size = new System.Drawing.Size(168, 20);
            this.ctlHelpLabel1.SetStatusText(this.txtSimple, "Text Box");
            this.txtSimple.StylePainter = this.StyleGuideBase;
            this.txtSimple.TabIndex = 2;
            validatorRule1.FieldName = "Text Box";
            validatorRule1.Required = true;
            this.ctlValidator1.SetValidatorRule(this.txtSimple, validatorRule1);
            // 
            // dockTabMessage
            // 
            this.dockTabMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.dockTabMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dockTabMessage.Controls.Add(this.txtMessages);
            this.dockTabMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockTabMessage.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dockTabMessage.Guid = "162330b9-da16-4910-a0ca-044231b63247";
            this.dockTabMessage.Image = null;
            this.dockTabMessage.Location = new System.Drawing.Point(1, 24);
            this.dockTabMessage.Name = "dockTabMessage";
            this.dockTabMessage.Padding = new System.Windows.Forms.Padding(2);
            this.dockTabMessage.Size = new System.Drawing.Size(763, 39);
            this.dockTabMessage.StylePainter = this.StyleGuideBase;
            this.dockTabMessage.TabIndex = 1;
            this.dockTabMessage.Text = "Messages";
            // 
            // txtMessages
            // 
            this.txtMessages.BackColor = System.Drawing.Color.White;
            this.txtMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMessages.ForeColor = System.Drawing.Color.Black;
            this.txtMessages.Location = new System.Drawing.Point(2, 2);
            this.txtMessages.Multiline = true;
            this.txtMessages.Name = "txtMessages";
            this.txtMessages.Size = new System.Drawing.Size(759, 35);
            this.txtMessages.StylePainter = this.StyleGuideBase;
            this.txtMessages.TabIndex = 3;
            // 
            // dockTabTrace
            // 
            this.dockTabTrace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.dockTabTrace.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dockTabTrace.Controls.Add(this.ctlListBox1);
            this.dockTabTrace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockTabTrace.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dockTabTrace.Guid = "fc292394-cb8d-463d-9b8d-c813d9ff80e7";
            this.dockTabTrace.Image = null;
            this.dockTabTrace.Location = new System.Drawing.Point(1, 24);
            this.dockTabTrace.Name = "dockTabTrace";
            this.dockTabTrace.Padding = new System.Windows.Forms.Padding(2);
            this.dockTabTrace.Size = new System.Drawing.Size(782, 39);
            this.dockTabTrace.StylePainter = this.StyleGuideBase;
            this.dockTabTrace.TabIndex = 2;
            this.dockTabTrace.Text = "Trace";
            // 
            // ctlListBox1
            // 
            this.ctlListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlListBox1.Items.AddRange(new object[] {
            "Task item 1",
            "Task item 2",
            "Task item 3",
            ""});
            this.ctlListBox1.Location = new System.Drawing.Point(2, 2);
            this.ctlListBox1.Name = "ctlListBox1";
            this.ctlListBox1.ReadOnly = false;
            this.ctlListBox1.Size = new System.Drawing.Size(778, 28);
            this.ctlListBox1.TabIndex = 1;
            // 
            // dockPanelLeft
            // 
            this.dockPanelLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dockPanelLeft.Controls.Add(this.dockTabTools);
            this.dockPanelLeft.Controls.Add(this.dockTabTree);
            this.dockPanelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.dockPanelLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.dockPanelLeft.Location = new System.Drawing.Point(2, 66);
            this.dockPanelLeft.Manager = this.ctlDockingManager;
            this.dockPanelLeft.Name = "dockPanelLeft";
            this.dockPanelLeft.Padding = new System.Windows.Forms.Padding(1, 20, 5, 24);
            this.dockPanelLeft.Size = new System.Drawing.Size(177, 324);
            this.dockPanelLeft.StylePainter = this.StyleGuideBase;
            this.dockPanelLeft.TabIndex = 12;
            this.dockPanelLeft.Paint += new System.Windows.Forms.PaintEventHandler(this.dockPanelLeft_Paint);
            // 
            // dockTabTools
            // 
            this.dockTabTools.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.dockTabTools.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dockTabTools.Controls.Add(this.ctlTaskPanelBar1);
            this.dockTabTools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockTabTools.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dockTabTools.Guid = "c6d2d2ad-e210-4414-bc06-88708b4c6eea";
            this.dockTabTools.Image = null;
            this.dockTabTools.Location = new System.Drawing.Point(1, 20);
            this.dockTabTools.Name = "dockTabTools";
            this.dockTabTools.Padding = new System.Windows.Forms.Padding(2);
            this.dockTabTools.Size = new System.Drawing.Size(171, 280);
            this.dockTabTools.StylePainter = this.StyleGuideBase;
            this.dockTabTools.TabIndex = 1;
            this.dockTabTools.Text = "Tools";
            // 
            // ctlTaskPanelBar1
            // 
            this.ctlTaskPanelBar1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctlTaskPanelBar1.ControlLayout = Nistec.WinForms.ControlLayout.XpLayout;
            this.ctlTaskPanelBar1.Controls.Add(this.tpHook);
            this.ctlTaskPanelBar1.Controls.Add(this.tpResources);
            this.ctlTaskPanelBar1.Controls.Add(this.tpPermission);
            this.ctlTaskPanelBar1.Controls.Add(this.tpValidation);
            this.ctlTaskPanelBar1.Dock = System.Windows.Forms.DockStyle.Left;
            this.ctlTaskPanelBar1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ctlTaskPanelBar1.ImageList = this.imageList1;
            this.ctlTaskPanelBar1.Location = new System.Drawing.Point(2, 2);
            this.ctlTaskPanelBar1.Name = "ctlTaskPanelBar1";
            this.ctlTaskPanelBar1.Panels.AddRange(new Nistec.WinForms.McTaskPanel[] {
            this.tpHook,
            this.tpResources,
            this.tpPermission,
            this.tpValidation});
            this.ctlTaskPanelBar1.SingleActive = true;
            this.ctlTaskPanelBar1.Size = new System.Drawing.Size(162, 276);
            this.ctlTaskPanelBar1.StylePainter = this.StyleGuideBase;
            this.ctlTaskPanelBar1.TabIndex = 11;
            // 
            // tpHook
            // 
            this.tpHook.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tpHook.Controls.Add(this.linkShowNotify);
            this.tpHook.Controls.Add(this.linkShowMsg);
            this.tpHook.Controls.Add(this.linkInputBox);
            this.tpHook.ControlSpace = 0;
            this.ctlResource1.SetFieldName(this.tpHook, "tp_Messages");
            this.tpHook.Image = ((System.Drawing.Image)(resources.GetObject("tpHook.Image")));
            this.tpHook.Items.AddRange(new Nistec.WinForms.LinkLabelItem[] {
            this.linkShowNotify,
            this.linkShowMsg,
            this.linkInputBox});
            this.tpHook.Location = new System.Drawing.Point(8, 8);
            this.tpHook.Name = "tpHook";
            this.tpHook.PanelHook = true;
            this.tpHook.Size = new System.Drawing.Size(146, 94);
            this.tpHook.StylePainter = this.StyleGuideBase;
            this.tpHook.TabIndex = 0;
            this.tpHook.Text = "Title";
            this.tpHook.TitleFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // linkShowNotify
            // 
            this.linkShowNotify.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.linkShowNotify.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkShowNotify.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkShowNotify.ImageIndex = 0;
            this.linkShowNotify.ImageList = this.imageList1;
            this.linkShowNotify.Location = new System.Drawing.Point(0, 28);
            this.linkShowNotify.Name = "linkShowNotify";
            this.linkShowNotify.ShowToolTip = true;
            this.linkShowNotify.Size = new System.Drawing.Size(146, 20);
            this.linkShowNotify.TabIndex = 0;
            this.linkShowNotify.Text = "Show Notify Wondow";
            this.linkShowNotify.ToolTipText = "Show Notify Wondow";
            this.linkShowNotify.Click += new System.EventHandler(this.linkShowNotify_Click);
            // 
            // linkShowMsg
            // 
            this.linkShowMsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.linkShowMsg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkShowMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkShowMsg.ImageIndex = 7;
            this.linkShowMsg.ImageList = this.imageList1;
            this.linkShowMsg.Location = new System.Drawing.Point(0, 48);
            this.linkShowMsg.Name = "linkShowMsg";
            this.linkShowMsg.ShowToolTip = true;
            this.linkShowMsg.Size = new System.Drawing.Size(146, 20);
            this.linkShowMsg.TabIndex = 1;
            this.linkShowMsg.Text = "Show Message";
            this.linkShowMsg.ToolTipText = "Show Message";
            this.linkShowMsg.Click += new System.EventHandler(this.linkShowMsg_Click);
            // 
            // linkInputBox
            // 
            this.linkInputBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.linkInputBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkInputBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkInputBox.ImageIndex = 3;
            this.linkInputBox.ImageList = this.imageList1;
            this.linkInputBox.Location = new System.Drawing.Point(0, 68);
            this.linkInputBox.Name = "linkInputBox";
            this.linkInputBox.ShowToolTip = true;
            this.linkInputBox.Size = new System.Drawing.Size(146, 20);
            this.linkInputBox.TabIndex = 2;
            this.linkInputBox.Text = "Show Input Box";
            this.linkInputBox.ToolTipText = "Show Input Box";
            this.linkInputBox.Click += new System.EventHandler(this.linkInputBox_Click);
            // 
            // tpResources
            // 
            this.tpResources.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tpResources.Controls.Add(this.linkLabelItem4);
            this.tpResources.Controls.Add(this.linkLabelItem5);
            this.tpResources.ControlSpace = 0;
            this.ctlResource1.SetFieldName(this.tpResources, "tp_Resources");
            this.tpResources.Items.AddRange(new Nistec.WinForms.LinkLabelItem[] {
            this.linkLabelItem4,
            this.linkLabelItem5});
            this.tpResources.Location = new System.Drawing.Point(8, 110);
            this.tpResources.Name = "tpResources";
            this.tpResources.Size = new System.Drawing.Size(146, 25);
            this.tpResources.StylePainter = this.StyleGuideBase;
            this.tpResources.TabIndex = 1;
            this.tpResources.Text = "Title";
            this.tpResources.TitleFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // linkLabelItem4
            // 
            this.linkLabelItem4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelItem4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLabelItem4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelItem4.ImageIndex = 5;
            this.linkLabelItem4.ImageList = this.imageList1;
            this.linkLabelItem4.Location = new System.Drawing.Point(0, 28);
            this.linkLabelItem4.Name = "linkLabelItem4";
            this.linkLabelItem4.Size = new System.Drawing.Size(146, 20);
            this.linkLabelItem4.TabIndex = 0;
            this.linkLabelItem4.Text = "linkLabelItem4";
            // 
            // linkLabelItem5
            // 
            this.linkLabelItem5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelItem5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLabelItem5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelItem5.ImageIndex = 8;
            this.linkLabelItem5.ImageList = this.imageList1;
            this.linkLabelItem5.Location = new System.Drawing.Point(0, 48);
            this.linkLabelItem5.Name = "linkLabelItem5";
            this.linkLabelItem5.Size = new System.Drawing.Size(146, 20);
            this.linkLabelItem5.TabIndex = 1;
            this.linkLabelItem5.Text = "linkLabelItem5";
            // 
            // tpPermission
            // 
            this.tpPermission.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tpPermission.Controls.Add(this.linkLabelItem6);
            this.tpPermission.Controls.Add(this.linkLabelItem7);
            this.tpPermission.ControlSpace = 0;
            this.ctlResource1.SetFieldName(this.tpPermission, "tp_Permission");
            this.tpPermission.Items.AddRange(new Nistec.WinForms.LinkLabelItem[] {
            this.linkLabelItem6,
            this.linkLabelItem7});
            this.tpPermission.Location = new System.Drawing.Point(8, 142);
            this.tpPermission.Name = "tpPermission";
            this.tpPermission.Size = new System.Drawing.Size(146, 25);
            this.tpPermission.StylePainter = this.StyleGuideBase;
            this.tpPermission.TabIndex = 3;
            this.tpPermission.Text = "Title";
            this.tpPermission.TitleFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // linkLabelItem6
            // 
            this.linkLabelItem6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelItem6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLabelItem6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelItem6.ImageIndex = 0;
            this.linkLabelItem6.ImageList = this.imageList1;
            this.linkLabelItem6.Location = new System.Drawing.Point(0, 28);
            this.linkLabelItem6.Name = "linkLabelItem6";
            this.linkLabelItem6.Size = new System.Drawing.Size(146, 20);
            this.linkLabelItem6.TabIndex = 0;
            this.linkLabelItem6.Text = "linkLabelItem6";
            // 
            // linkLabelItem7
            // 
            this.linkLabelItem7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelItem7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLabelItem7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelItem7.ImageIndex = 2;
            this.linkLabelItem7.ImageList = this.imageList1;
            this.linkLabelItem7.Location = new System.Drawing.Point(0, 48);
            this.linkLabelItem7.Name = "linkLabelItem7";
            this.linkLabelItem7.Size = new System.Drawing.Size(146, 20);
            this.linkLabelItem7.TabIndex = 1;
            this.linkLabelItem7.Text = "linkLabelItem7";
            // 
            // tpValidation
            // 
            this.tpValidation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tpValidation.Controls.Add(this.ctlTreeView1);
            this.tpValidation.ControlSpace = 2;
            this.ctlResource1.SetFieldName(this.tpValidation, "tp_Validation");
            this.tpValidation.Location = new System.Drawing.Point(8, 174);
            this.tpValidation.Name = "tpValidation";
            this.tpValidation.Size = new System.Drawing.Size(146, 25);
            this.tpValidation.StylePainter = this.StyleGuideBase;
            this.tpValidation.TabIndex = 4;
            this.tpValidation.Text = "Title";
            this.tpValidation.TitleFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // ctlTreeView1
            // 
            this.ctlTreeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ctlTreeView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ctlTreeView1.Location = new System.Drawing.Point(2, 27);
            this.ctlTreeView1.Name = "ctlTreeView1";
            treeNode1.Name = "";
            treeNode1.Text = "Node1";
            treeNode2.Name = "";
            treeNode2.Text = "Node2";
            treeNode3.Name = "";
            treeNode3.Text = "Node3";
            treeNode4.Name = "";
            treeNode4.Text = "Node0";
            treeNode5.Name = "";
            treeNode5.Text = "Node5";
            treeNode6.Name = "";
            treeNode6.Text = "Node6";
            treeNode7.Name = "";
            treeNode7.Text = "Node7";
            treeNode8.Name = "";
            treeNode8.Text = "Node4";
            this.ctlTreeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode8});
            this.ctlTreeView1.Size = new System.Drawing.Size(142, 102);
            this.ctlTreeView1.StylePainter = this.StyleGuideBase;
            this.ctlTreeView1.TabIndex = 1;
            // 
            // dockTabTree
            // 
            this.dockTabTree.BackColor = System.Drawing.Color.AliceBlue;
            this.dockTabTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dockTabTree.Controls.Add(this.treeView1);
            this.dockTabTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockTabTree.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.dockTabTree.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dockTabTree.Guid = "b9536af0-bd9d-427b-a424-1922a61a1fcf";
            this.dockTabTree.Image = null;
            this.dockTabTree.Location = new System.Drawing.Point(1, 20);
            this.dockTabTree.Name = "dockTabTree";
            this.dockTabTree.Padding = new System.Windows.Forms.Padding(2);
            this.dockTabTree.Size = new System.Drawing.Size(171, 261);
            this.dockTabTree.StylePainter = this.StyleGuideBase;
            this.dockTabTree.TabIndex = 0;
            this.dockTabTree.Text = "TreeList";
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(2, 2);
            this.treeView1.Name = "treeView1";
            treeNode9.Name = "";
            treeNode9.Text = "Node1";
            treeNode10.Name = "";
            treeNode10.Text = "Node0";
            treeNode11.Name = "";
            treeNode11.Text = "Node1";
            treeNode12.Name = "";
            treeNode12.Text = "Root";
            treeNode13.Name = "";
            treeNode13.Text = "Node5";
            treeNode14.Name = "";
            treeNode14.Text = "Node6";
            treeNode15.Name = "";
            treeNode15.Text = "Node2";
            treeNode16.Name = "";
            treeNode16.Text = "Node1";
            treeNode17.Name = "";
            treeNode17.Text = "Node4";
            treeNode18.Name = "";
            treeNode18.Text = "Node5";
            treeNode19.Name = "";
            treeNode19.Text = "Node3";
            treeNode20.Name = "";
            treeNode20.Text = "Node7";
            treeNode21.Name = "";
            treeNode21.Text = "Node6";
            treeNode22.Name = "";
            treeNode22.Text = "Node9";
            treeNode23.Name = "";
            treeNode23.Text = "Node10";
            treeNode24.Name = "";
            treeNode24.Text = "Node11";
            treeNode25.Name = "";
            treeNode25.Text = "Node8";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode12,
            treeNode16,
            treeNode19,
            treeNode21,
            treeNode25});
            this.treeView1.Size = new System.Drawing.Size(167, 257);
            this.treeView1.TabIndex = 0;
            // 
            // dockPanelRight
            // 
            this.dockPanelRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dockPanelRight.Controls.Add(this.dockTabProperty);
            this.dockPanelRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.dockPanelRight.Location = new System.Drawing.Point(596, 66);
            this.dockPanelRight.Manager = this.ctlDockingManager;
            this.dockPanelRight.Name = "dockPanelRight";
            this.dockPanelRight.Padding = new System.Windows.Forms.Padding(5, 20, 1, 1);
            this.dockPanelRight.Size = new System.Drawing.Size(190, 324);
            this.dockPanelRight.StylePainter = this.StyleGuideBase;
            this.dockPanelRight.TabIndex = 13;
            // 
            // dockTabProperty
            // 
            this.dockTabProperty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.dockTabProperty.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dockTabProperty.Controls.Add(this.propertyGrid1);
            this.dockTabProperty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockTabProperty.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dockTabProperty.Guid = "0c9153dc-9001-4886-a437-2c2a0865b3aa";
            this.dockTabProperty.Image = null;
            this.dockTabProperty.Location = new System.Drawing.Point(5, 20);
            this.dockTabProperty.Name = "dockTabProperty";
            this.dockTabProperty.Padding = new System.Windows.Forms.Padding(2);
            this.dockTabProperty.Size = new System.Drawing.Size(184, 303);
            this.dockTabProperty.StylePainter = this.StyleGuideBase;
            this.dockTabProperty.TabIndex = 0;
            this.dockTabProperty.Text = "Properties";
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.CommandsBackColor = System.Drawing.Color.AliceBlue;
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.HelpVisible = false;
            this.propertyGrid1.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.propertyGrid1.Location = new System.Drawing.Point(2, 2);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.SelectedObject = this.dockTabProperty;
            this.propertyGrid1.Size = new System.Drawing.Size(180, 299);
            this.propertyGrid1.TabIndex = 0;
            this.propertyGrid1.ToolbarVisible = false;
            // 
            // ctlTabControl1
            // 
            this.ctlTabControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlTabControl1.ControlLayout = Nistec.WinForms.ControlLayout.XpLayout;
            this.ctlTabControl1.Controls.Add(this.pageEditControls);
            this.ctlTabControl1.Controls.Add(this.pageCommand);
            this.ctlTabControl1.Controls.Add(this.pageCombos);
            this.ctlTabControl1.Controls.Add(this.pageMulti);
            this.ctlTabControl1.ItemSize = new System.Drawing.Size(0, 22);
            this.ctlTabControl1.Location = new System.Drawing.Point(4, 18);
            this.ctlTabControl1.Name = "ctlTabControl1";
            this.ctlTabControl1.Size = new System.Drawing.Size(382, 235);
            this.ctlTabControl1.StylePainter = this.StyleGuideBase;
            this.ctlTabControl1.TabIndex = 14;
            this.ctlTabControl1.TabPages.AddRange(new Nistec.WinForms.McTabPage[] {
            this.pageEditControls,
            this.pageCommand,
            this.pageCombos,
            this.pageMulti});
            this.ctlTabControl1.TabStop = false;
            this.ctlTabControl1.Text = "ctlTabControl1";
            // 
            // pageEditControls
            // 
            this.pageEditControls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.pageEditControls.Controls.Add(this.ctlLabel7);
            this.pageEditControls.Controls.Add(this.ctlLabel6);
            this.pageEditControls.Controls.Add(this.ctlLabel5);
            this.pageEditControls.Controls.Add(this.ctlLabel4);
            this.pageEditControls.Controls.Add(this.ctlLabel3);
            this.pageEditControls.Controls.Add(this.mskIP);
            this.pageEditControls.Controls.Add(this.txtDate);
            this.pageEditControls.Controls.Add(this.mskDate);
            this.pageEditControls.Controls.Add(this.ctlSpinEdit1);
            this.pageEditControls.Controls.Add(this.mskPhone);
            this.pageEditControls.Controls.Add(this.txtNumber);
            this.pageEditControls.Controls.Add(this.ctlLabel2);
            this.pageEditControls.Controls.Add(this.txtSimple);
            this.pageEditControls.Controls.Add(this.ctlLabel1);
            //this.ctlPermission1.SetFieldName(this.pageEditControls, "EditControls");
            this.pageEditControls.Location = new System.Drawing.Point(4, 29);
            this.pageEditControls.Name = "pageEditControls";
            this.pageEditControls.Size = new System.Drawing.Size(373, 201);
            this.pageEditControls.StylePainter = this.StyleGuideBase;
            this.pageEditControls.Text = "Edit Controls";
            // 
            // ctlLabel7
            // 
            this.ctlLabel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel7.Location = new System.Drawing.Point(16, 168);
            this.ctlLabel7.Name = "ctlLabel7";
            this.ctlLabel7.Size = new System.Drawing.Size(152, 20);
            this.ctlLabel7.StylePainter = this.StyleGuideBase;
            this.ctlLabel7.Text = "Spin Edit";
            this.ctlLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctlLabel6
            // 
            this.ctlLabel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel6.Location = new System.Drawing.Point(16, 144);
            this.ctlLabel6.Name = "ctlLabel6";
            this.ctlLabel6.Size = new System.Drawing.Size(152, 20);
            this.ctlLabel6.StylePainter = this.StyleGuideBase;
            this.ctlLabel6.Text = "Mask IP Address";
            this.ctlLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctlLabel5
            // 
            this.ctlLabel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel5.Location = new System.Drawing.Point(16, 120);
            this.ctlLabel5.Name = "ctlLabel5";
            this.ctlLabel5.Size = new System.Drawing.Size(152, 20);
            this.ctlLabel5.StylePainter = this.StyleGuideBase;
            this.ctlLabel5.Text = "Mask Date";
            this.ctlLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctlLabel4
            // 
            this.ctlLabel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel4.Location = new System.Drawing.Point(16, 96);
            this.ctlLabel4.Name = "ctlLabel4";
            this.ctlLabel4.Size = new System.Drawing.Size(152, 20);
            this.ctlLabel4.StylePainter = this.StyleGuideBase;
            this.ctlLabel4.Text = "Mask Phone Number";
            this.ctlLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctlLabel3
            // 
            this.ctlLabel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel3.Location = new System.Drawing.Point(16, 72);
            this.ctlLabel3.Name = "ctlLabel3";
            this.ctlLabel3.Size = new System.Drawing.Size(152, 20);
            this.ctlLabel3.StylePainter = this.StyleGuideBase;
            this.ctlLabel3.Text = "Format Date";
            this.ctlLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctlLabel2
            // 
            this.ctlLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel2.Location = new System.Drawing.Point(16, 48);
            this.ctlLabel2.Name = "ctlLabel2";
            this.ctlLabel2.Size = new System.Drawing.Size(152, 20);
            this.ctlLabel2.StylePainter = this.StyleGuideBase;
            this.ctlLabel2.Text = "Format Number";
            this.ctlLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctlLabel1
            // 
            this.ctlLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel1.Location = new System.Drawing.Point(16, 24);
            this.ctlLabel1.Name = "ctlLabel1";
            this.ctlLabel1.Size = new System.Drawing.Size(152, 20);
            this.ctlLabel1.StylePainter = this.StyleGuideBase;
            this.ctlLabel1.Text = "Text Box";
            this.ctlLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pageCommand
            // 
            this.pageCommand.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.pageCommand.Controls.Add(this.btnMenu2);
            this.pageCommand.Controls.Add(this.btnProgress2);
            this.pageCommand.Controls.Add(this.ctlProgressBar2);
            this.pageCommand.Controls.Add(this.btnProgress1);
            this.pageCommand.Controls.Add(this.ctlXpLine1);
            this.pageCommand.Controls.Add(this.ctlProgressBar1);
            this.pageCommand.Controls.Add(this.ctlLinkLabel1);
            this.pageCommand.Controls.Add(this.btnMenu1);
            this.pageCommand.Controls.Add(this.btnError);
            this.pageCommand.Controls.Add(this.btnInfo);
            //this.ctlPermission1.SetFieldName(this.pageCommand, "CommandControls");
            this.pageCommand.Location = new System.Drawing.Point(4, 29);
            this.pageCommand.Name = "pageCommand";
            this.pageCommand.Size = new System.Drawing.Size(373, 201);
            this.pageCommand.StylePainter = this.StyleGuideBase;
            this.pageCommand.Text = "Command Controls";
            // 
            // btnMenu2
            // 
            this.btnMenu2.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnMenu2.DrawItemStyle = Nistec.WinForms.DrawItemStyle.CheckBox;
            this.btnMenu2.Location = new System.Drawing.Point(208, 69);
            this.btnMenu2.MenuItems.AddRange(new Nistec.WinForms.PopUpItem[] {
            this.popUpItem4,
            this.popUpItem5,
            this.popUpItem6});
            this.btnMenu2.Name = "btnMenu2";
            this.btnMenu2.Size = new System.Drawing.Size(136, 20);
            this.btnMenu2.StylePainter = this.StyleGuideBase;
            this.btnMenu2.TabIndex = 13;
            this.btnMenu2.Text = "Checked  Menu";
            this.btnMenu2.ToolTipItems = null;
            this.btnMenu2.ToolTipText = "Checked  Menu";
            // 
            // popUpItem4
            // 
            this.popUpItem4.Checked = true;
            this.popUpItem4.Text = "Check 1";
            // 
            // popUpItem5
            // 
            this.popUpItem5.Text = "Check 2";
            // 
            // popUpItem6
            // 
            this.popUpItem6.Text = "Check 3";
            // 
            // btnProgress2
            // 
            this.btnProgress2.ControlLayout = Nistec.WinForms.ControlLayout.Visual;
            this.btnProgress2.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnProgress2.Location = new System.Drawing.Point(16, 168);
            this.btnProgress2.Name = "btnProgress2";
            this.btnProgress2.Size = new System.Drawing.Size(104, 24);
            this.btnProgress2.StylePainter = this.StyleGuideBase;
            this.btnProgress2.TabIndex = 12;
            this.btnProgress2.Text = "Progress";
            this.btnProgress2.ToolTipText = "Progress";
            this.btnProgress2.Click += new System.EventHandler(this.btnProgress2_Click);
            // 
            // ctlProgressBar2
            // 
            this.ctlProgressBar2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.ctlProgressBar2.BarColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(126)))), ((int)(((byte)(177)))));
            this.ctlProgressBar2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlProgressBar2.Location = new System.Drawing.Point(184, 168);
            this.ctlProgressBar2.Maximum = 1000;
            this.ctlProgressBar2.Name = "ctlProgressBar2";
            this.ctlProgressBar2.Size = new System.Drawing.Size(160, 24);
            this.ctlProgressBar2.StylePainter = this.StyleGuideBase;
            this.ctlProgressBar2.TabIndex = 11;
            this.ctlProgressBar2.TabStop = false;
            this.ctlProgressBar2.Value = 0;
            // 
            // btnProgress1
            // 
            this.btnProgress1.ControlLayout = Nistec.WinForms.ControlLayout.Visual;
            this.btnProgress1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnProgress1.Location = new System.Drawing.Point(16, 136);
            this.btnProgress1.Name = "btnProgress1";
            this.btnProgress1.Size = new System.Drawing.Size(104, 24);
            this.btnProgress1.StylePainter = this.StyleGuideBase;
            this.btnProgress1.TabIndex = 10;
            this.btnProgress1.Text = "Progress";
            this.btnProgress1.ToolTipText = "Progress";
            this.btnProgress1.Click += new System.EventHandler(this.btnProgress1_Click);
            // 
            // ctlXpLine1
            // 
            this.ctlXpLine1.Location = new System.Drawing.Point(16, 120);
            this.ctlXpLine1.Name = "ctlXpLine1";
            this.ctlXpLine1.ShapeColor = System.Drawing.Color.Transparent;
            this.ctlXpLine1.ShapeType = Nistec.WinForms.ShapeTypes.XpLine;
            this.ctlXpLine1.Size = new System.Drawing.Size(328, 1);
            this.ctlXpLine1.TabIndex = 9;
            // 
            // ctlProgressBar1
            // 
            this.ctlProgressBar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.ctlProgressBar1.BarColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(126)))), ((int)(((byte)(177)))));
            this.ctlProgressBar1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlProgressBar1.Location = new System.Drawing.Point(184, 136);
            this.ctlProgressBar1.Maximum = 1000;
            this.ctlProgressBar1.Name = "ctlProgressBar1";
            this.ctlProgressBar1.Size = new System.Drawing.Size(160, 24);
            this.ctlProgressBar1.StylePainter = this.StyleGuideBase;
            this.ctlProgressBar1.TabIndex = 8;
            this.ctlProgressBar1.TabStop = false;
            this.ctlProgressBar1.Value = 0;
            // 
            // ctlLinkLabel1
            // 
            this.ctlLinkLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlLinkLabel1.ControlLayout = Nistec.WinForms.ControlLayout.Visual;
            this.ctlLinkLabel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLinkLabel1.Location = new System.Drawing.Point(16, 96);
            this.ctlLinkLabel1.Name = "ctlLinkLabel1";
            this.ctlLinkLabel1.Size = new System.Drawing.Size(136, 16);
            this.ctlLinkLabel1.StylePainter = this.StyleGuideBase;
            this.ctlLinkLabel1.TabIndex = 7;
            this.ctlLinkLabel1.TabStop = true;
            this.ctlLinkLabel1.Text = "www.mControlnet.com";
            this.ctlLinkLabel1.ToolTip = "";
            // 
            // btnMenu1
            // 
            this.btnMenu1.ButtonMenuStyle = Nistec.WinForms.ButtonMenuStyles.Media;
            this.btnMenu1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnMenu1.FixSize = false;
            this.btnMenu1.ImageList = this.imageList1;
            this.btnMenu1.Location = new System.Drawing.Point(208, 24);
            this.btnMenu1.MenuItems.AddRange(new Nistec.WinForms.PopUpItem[] {
            this.popUpItem1,
            this.popUpItem2,
            this.popUpItem3});
            this.btnMenu1.Name = "btnMenu1";
            this.btnMenu1.Size = new System.Drawing.Size(136, 37);
            this.btnMenu1.StylePainter = this.StyleGuideBase;
            this.btnMenu1.TabIndex = 6;
            this.btnMenu1.Text = "Button Menu";
            this.btnMenu1.ToolTipItems = null;
            this.btnMenu1.ToolTipText = "Button Menu";
            // 
            // popUpItem1
            // 
            this.popUpItem1.ImageList = this.imageList1;
            this.popUpItem1.Text = "popUpItem1";
            // 
            // popUpItem2
            // 
            this.popUpItem2.ImageList = this.imageList1;
            this.popUpItem2.Text = "popUpItem2";
            // 
            // popUpItem3
            // 
            this.popUpItem3.ImageList = this.imageList1;
            this.popUpItem3.Text = "popUpItem3";
            // 
            // btnError
            // 
            this.btnError.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnError.Location = new System.Drawing.Point(16, 56);
            this.btnError.Name = "btnError";
            this.btnError.Size = new System.Drawing.Size(104, 24);
            this.btnError.StylePainter = this.StyleGuideBase;
            this.btnError.TabIndex = 5;
            this.btnError.Text = "show Error";
            this.btnError.ToolTipText = "show Error";
            this.btnError.Click += new System.EventHandler(this.btnError_Click);
            // 
            // btnInfo
            // 
            this.btnInfo.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnInfo.Location = new System.Drawing.Point(16, 24);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(104, 24);
            this.btnInfo.StylePainter = this.StyleGuideBase;
            this.btnInfo.TabIndex = 4;
            this.btnInfo.Text = "show Info";
            this.btnInfo.ToolTipText = "show Info";
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // pageCombos
            // 
            this.pageCombos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.pageCombos.Controls.Add(this.ctlImageList);
            this.pageCombos.Controls.Add(this.ctlLabel8);
            this.pageCombos.Controls.Add(this.ctlLabel9);
            this.pageCombos.Controls.Add(this.ctlLabel10);
            this.pageCombos.Controls.Add(this.ctlLabel11);
            this.pageCombos.Controls.Add(this.ctlLabel12);
            this.pageCombos.Controls.Add(this.ctlLabel13);
            this.pageCombos.Controls.Add(this.ctlLabel14);
            this.pageCombos.Controls.Add(this.ctlDatePicker1);
            this.pageCombos.Controls.Add(this.ctlBoolean);
            this.pageCombos.Controls.Add(this.ctlFonts);
            this.pageCombos.Controls.Add(this.ctlColors);
            this.pageCombos.Controls.Add(this.ctlDropDown1);
            this.pageCombos.Controls.Add(this.ctlComboBox1);
            this.pageCombos.Location = new System.Drawing.Point(4, 29);
            this.pageCombos.Name = "pageCombos";
            this.pageCombos.Size = new System.Drawing.Size(373, 201);
            this.pageCombos.StylePainter = this.StyleGuideBase;
            this.pageCombos.Text = "Combo Controls";
            // 
            // ctlImageList
            // 
            this.ctlImageList.ButtonToolTip = "Image list box";
            this.ctlImageList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ctlImageList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ctlImageList.DropDownWidth = 168;
            this.ctlImageList.FontSize = 8.25F;
            this.ctlImageList.ImageList = this.imageList1;
            this.ctlImageList.IntegralHeight = false;
            this.ctlImageList.ItemHeight = 17;
            this.ctlImageList.Items.AddRange(new object[] {
            "Item",
            "Print",
            "Child",
            "Build",
            "Read"});
            this.ctlImageList.Location = new System.Drawing.Point(176, 144);
            this.ctlImageList.Name = "ctlImageList";
            this.ctlImageList.PickerType = Nistec.WinForms.PickerType.Images;
            this.ctlImageList.Size = new System.Drawing.Size(168, 20);
            this.ctlImageList.StylePainter = this.StyleGuideBase;
            this.ctlImageList.TabIndex = 22;
            // 
            // ctlLabel8
            // 
            this.ctlLabel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel8.Location = new System.Drawing.Point(16, 168);
            this.ctlLabel8.Name = "ctlLabel8";
            this.ctlLabel8.Size = new System.Drawing.Size(152, 20);
            this.ctlLabel8.StylePainter = this.StyleGuideBase;
            this.ctlLabel8.Text = "Date Picker";
            this.ctlLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctlLabel9
            // 
            this.ctlLabel9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlLabel9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel9.Location = new System.Drawing.Point(16, 144);
            this.ctlLabel9.Name = "ctlLabel9";
            this.ctlLabel9.Size = new System.Drawing.Size(152, 20);
            this.ctlLabel9.StylePainter = this.StyleGuideBase;
            this.ctlLabel9.Text = "Image List";
            this.ctlLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctlLabel10
            // 
            this.ctlLabel10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlLabel10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel10.Location = new System.Drawing.Point(16, 120);
            this.ctlLabel10.Name = "ctlLabel10";
            this.ctlLabel10.Size = new System.Drawing.Size(152, 20);
            this.ctlLabel10.StylePainter = this.StyleGuideBase;
            this.ctlLabel10.Text = "Boolean";
            this.ctlLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctlLabel11
            // 
            this.ctlLabel11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlLabel11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel11.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel11.Location = new System.Drawing.Point(16, 96);
            this.ctlLabel11.Name = "ctlLabel11";
            this.ctlLabel11.Size = new System.Drawing.Size(152, 20);
            this.ctlLabel11.StylePainter = this.StyleGuideBase;
            this.ctlLabel11.Text = "Font List";
            this.ctlLabel11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctlLabel12
            // 
            this.ctlLabel12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlLabel12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel12.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel12.Location = new System.Drawing.Point(16, 72);
            this.ctlLabel12.Name = "ctlLabel12";
            this.ctlLabel12.Size = new System.Drawing.Size(152, 20);
            this.ctlLabel12.StylePainter = this.StyleGuideBase;
            this.ctlLabel12.Text = "Color List";
            this.ctlLabel12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctlLabel13
            // 
            this.ctlLabel13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlLabel13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel13.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel13.Location = new System.Drawing.Point(16, 48);
            this.ctlLabel13.Name = "ctlLabel13";
            this.ctlLabel13.Size = new System.Drawing.Size(152, 20);
            this.ctlLabel13.StylePainter = this.StyleGuideBase;
            this.ctlLabel13.Text = "DropDownList";
            this.ctlLabel13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctlLabel14
            // 
            this.ctlLabel14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlLabel14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel14.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel14.Location = new System.Drawing.Point(16, 24);
            this.ctlLabel14.Name = "ctlLabel14";
            this.ctlLabel14.Size = new System.Drawing.Size(152, 20);
            this.ctlLabel14.StylePainter = this.StyleGuideBase;
            this.ctlLabel14.Text = "ComboBox";
            this.ctlLabel14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctlDatePicker1
            // 
            this.ctlDatePicker1.ButtonToolTip = "";
            this.ctlDatePicker1.Format = "G";
            this.ctlDatePicker1.InputMask = "00/00/0000 00:00:00";
            this.ctlDatePicker1.Location = new System.Drawing.Point(176, 168);
            this.ctlDatePicker1.MaxValue = new System.DateTime(2999, 12, 31, 0, 0, 0, 0);
            this.ctlDatePicker1.MinValue = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.ctlDatePicker1.Name = "ctlDatePicker1";
            this.ctlDatePicker1.Size = new System.Drawing.Size(168, 20);
            this.ctlDatePicker1.StylePainter = this.StyleGuideBase;
            this.ctlDatePicker1.TabIndex = 5;
            this.ctlDatePicker1.Value = new System.DateTime(2008, 10, 4, 19, 37, 45, 500);
            // 
            // ctlBoolean
            // 
            this.ctlBoolean.ButtonToolTip = "Boolean list box";
            this.ctlBoolean.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ctlBoolean.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ctlBoolean.DropDownWidth = 168;
            this.ctlBoolean.FontSize = 8.25F;
            this.ctlBoolean.IntegralHeight = false;
            this.ctlBoolean.Location = new System.Drawing.Point(176, 120);
            this.ctlBoolean.Name = "ctlBoolean";
            this.ctlBoolean.PickerType = Nistec.WinForms.PickerType.Bool;
            this.ctlBoolean.Size = new System.Drawing.Size(168, 20);
            this.ctlBoolean.StylePainter = this.StyleGuideBase;
            this.ctlBoolean.TabIndex = 4;
            // 
            // ctlFonts
            // 
            this.ctlFonts.ButtonToolTip = "Font list box";
            this.ctlFonts.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ctlFonts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ctlFonts.DropDownWidth = 168;
            this.ctlFonts.FontSize = 8.25F;
            this.ctlFonts.IntegralHeight = false;
            this.ctlFonts.Location = new System.Drawing.Point(176, 96);
            this.ctlFonts.Name = "ctlFonts";
            this.ctlFonts.PickerType = Nistec.WinForms.PickerType.Fonts;
            this.ctlFonts.Size = new System.Drawing.Size(168, 20);
            this.ctlFonts.StylePainter = this.StyleGuideBase;
            this.ctlFonts.TabIndex = 3;
            // 
            // ctlColors
            // 
            this.ctlColors.ButtonToolTip = "Color list box";
            this.ctlColors.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ctlColors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ctlColors.DropDownWidth = 168;
            this.ctlColors.FontSize = 8.25F;
            this.ctlColors.IntegralHeight = false;
            this.ctlColors.Location = new System.Drawing.Point(176, 72);
            this.ctlColors.Name = "ctlColors";
            this.ctlColors.PickerType = Nistec.WinForms.PickerType.Colors;
            this.ctlColors.Size = new System.Drawing.Size(168, 20);
            this.ctlColors.StylePainter = this.StyleGuideBase;
            this.ctlColors.TabIndex = 2;
            // 
            // ctlDropDown1
            // 
            this.ctlDropDown1.ButtonToolTip = "DropDown list box";
            this.ctlDropDown1.DefaultValue = "Adi";
            this.ctlDropDown1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ctlDropDown1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ctlDropDown1.DropDownWidth = 168;
            this.ctlDropDown1.FontSize = 8.25F;
            this.ctlDropDown1.IntegralHeight = false;
            this.ctlDropDown1.Items.AddRange(new object[] {
            "Adi",
            "Beny",
            "Sylvia",
            "Roni",
            "David",
            ""});
            this.ctlDropDown1.Location = new System.Drawing.Point(176, 48);
            this.ctlDropDown1.Name = "ctlDropDown1";
            this.ctlDropDown1.Size = new System.Drawing.Size(168, 20);
            this.ctlDropDown1.StylePainter = this.StyleGuideBase;
            this.ctlDropDown1.TabIndex = 1;
            this.ctlDropDown1.Text = "Adi";
            // 
            // ctlComboBox1
            // 
            this.ctlComboBox1.ButtonToolTip = "";
            this.ctlComboBox1.DefaultValue = "Item1";
            this.ctlComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ctlComboBox1.DropDownWidth = 60;
            this.ctlComboBox1.IntegralHeight = false;
            this.ctlComboBox1.Items.AddRange(new object[] {
            "Item1",
            "Item2",
            "Item3",
            "Item4",
            "Item5"});
            this.ctlComboBox1.Location = new System.Drawing.Point(176, 24);
            this.ctlComboBox1.Name = "ctlComboBox1";
            this.ctlComboBox1.Size = new System.Drawing.Size(168, 20);
            this.ctlComboBox1.StylePainter = this.StyleGuideBase;
            this.ctlComboBox1.TabIndex = 0;
            this.ctlComboBox1.Text = "Item1";
            // 
            // pageMulti
            // 
            this.pageMulti.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.pageMulti.Controls.Add(this.ctlMultiBox7);
            this.pageMulti.Controls.Add(this.ctlMultiBox6);
            this.pageMulti.Controls.Add(this.ctlMultiBox5);
            this.pageMulti.Controls.Add(this.ctlMultiBox4);
            this.pageMulti.Controls.Add(this.ctlMultiBox3);
            this.pageMulti.Controls.Add(this.ctlMultiBox2);
            this.pageMulti.Controls.Add(this.ctlLabel15);
            this.pageMulti.Controls.Add(this.ctlLabel16);
            this.pageMulti.Controls.Add(this.ctlLabel17);
            this.pageMulti.Controls.Add(this.ctlLabel18);
            this.pageMulti.Controls.Add(this.ctlLabel19);
            this.pageMulti.Controls.Add(this.ctlLabel20);
            this.pageMulti.Controls.Add(this.ctlLabel21);
            this.pageMulti.Controls.Add(this.ctlMultiBox1);
            this.pageMulti.Location = new System.Drawing.Point(4, 29);
            this.pageMulti.Name = "pageMulti";
            this.pageMulti.Size = new System.Drawing.Size(373, 201);
            this.pageMulti.StylePainter = this.StyleGuideBase;
            this.pageMulti.Text = "Multi Controls";
            // 
            // ctlMultiBox7
            // 
            this.ctlMultiBox7.ButtonToolTip = "Custom";
            this.ctlMultiBox7.Location = new System.Drawing.Point(176, 168);
            this.ctlMultiBox7.MultiType = Nistec.WinForms.MultiType.Custom;
            this.ctlMultiBox7.Name = "ctlMultiBox7";
            this.ctlMultiBox7.Size = new System.Drawing.Size(168, 20);
            this.ctlMultiBox7.StylePainter = this.StyleGuideBase;
            this.ctlMultiBox7.TabIndex = 34;
            // 
            // ctlMultiBox6
            // 
            this.ctlMultiBox6.ButtonToolTip = "Combo list box";
            this.ctlMultiBox6.Location = new System.Drawing.Point(176, 144);
            this.ctlMultiBox6.MultiType = Nistec.WinForms.MultiType.Combo;
            this.ctlMultiBox6.Name = "ctlMultiBox6";
            this.ctlMultiBox6.Size = new System.Drawing.Size(168, 20);
            this.ctlMultiBox6.StylePainter = this.StyleGuideBase;
            this.ctlMultiBox6.TabIndex = 33;
            // 
            // ctlMultiBox5
            // 
            this.ctlMultiBox5.ButtonToolTip = "Explorer";
            this.ctlMultiBox5.Location = new System.Drawing.Point(176, 120);
            this.ctlMultiBox5.MultiType = Nistec.WinForms.MultiType.Explorer;
            this.ctlMultiBox5.Name = "ctlMultiBox5";
            this.ctlMultiBox5.Size = new System.Drawing.Size(168, 20);
            this.ctlMultiBox5.StylePainter = this.StyleGuideBase;
            this.ctlMultiBox5.TabIndex = 32;
            // 
            // ctlMultiBox4
            // 
            this.ctlMultiBox4.ButtonToolTip = "Brows";
            this.ctlMultiBox4.Location = new System.Drawing.Point(176, 96);
            this.ctlMultiBox4.MultiType = Nistec.WinForms.MultiType.Brows;
            this.ctlMultiBox4.Name = "ctlMultiBox4";
            this.ctlMultiBox4.Size = new System.Drawing.Size(168, 20);
            this.ctlMultiBox4.StylePainter = this.StyleGuideBase;
            this.ctlMultiBox4.TabIndex = 31;
            // 
            // ctlMultiBox3
            // 
            this.ctlMultiBox3.ButtonToolTip = "BrowsFolder";
            this.ctlMultiBox3.Location = new System.Drawing.Point(176, 72);
            this.ctlMultiBox3.MultiType = Nistec.WinForms.MultiType.BrowsFolder;
            this.ctlMultiBox3.Name = "ctlMultiBox3";
            this.ctlMultiBox3.Size = new System.Drawing.Size(168, 20);
            this.ctlMultiBox3.StylePainter = this.StyleGuideBase;
            this.ctlMultiBox3.TabIndex = 30;
            // 
            // ctlMultiBox2
            // 
            this.ctlMultiBox2.ButtonToolTip = "Font";
            this.ctlMultiBox2.Location = new System.Drawing.Point(176, 48);
            this.ctlMultiBox2.MultiType = Nistec.WinForms.MultiType.Font;
            this.ctlMultiBox2.Name = "ctlMultiBox2";
            this.ctlMultiBox2.Size = new System.Drawing.Size(168, 20);
            this.ctlMultiBox2.StylePainter = this.StyleGuideBase;
            this.ctlMultiBox2.TabIndex = 29;
            // 
            // ctlLabel15
            // 
            this.ctlLabel15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlLabel15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel15.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel15.Location = new System.Drawing.Point(16, 168);
            this.ctlLabel15.Name = "ctlLabel15";
            this.ctlLabel15.Size = new System.Drawing.Size(152, 20);
            this.ctlLabel15.StylePainter = this.StyleGuideBase;
            this.ctlLabel15.Text = "Multi Custom and InputBox";
            this.ctlLabel15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctlLabel16
            // 
            this.ctlLabel16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlLabel16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel16.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel16.Location = new System.Drawing.Point(16, 144);
            this.ctlLabel16.Name = "ctlLabel16";
            this.ctlLabel16.Size = new System.Drawing.Size(152, 20);
            this.ctlLabel16.StylePainter = this.StyleGuideBase;
            this.ctlLabel16.Text = "Multi List";
            this.ctlLabel16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctlLabel17
            // 
            this.ctlLabel17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlLabel17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel17.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel17.Location = new System.Drawing.Point(16, 120);
            this.ctlLabel17.Name = "ctlLabel17";
            this.ctlLabel17.Size = new System.Drawing.Size(152, 20);
            this.ctlLabel17.StylePainter = this.StyleGuideBase;
            this.ctlLabel17.Text = "Multi Explorer";
            this.ctlLabel17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctlLabel18
            // 
            this.ctlLabel18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlLabel18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel18.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel18.Location = new System.Drawing.Point(16, 96);
            this.ctlLabel18.Name = "ctlLabel18";
            this.ctlLabel18.Size = new System.Drawing.Size(152, 20);
            this.ctlLabel18.StylePainter = this.StyleGuideBase;
            this.ctlLabel18.Text = "Multi Brows File";
            this.ctlLabel18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctlLabel19
            // 
            this.ctlLabel19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlLabel19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel19.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel19.Location = new System.Drawing.Point(16, 72);
            this.ctlLabel19.Name = "ctlLabel19";
            this.ctlLabel19.Size = new System.Drawing.Size(152, 20);
            this.ctlLabel19.StylePainter = this.StyleGuideBase;
            this.ctlLabel19.Text = "Multi Brows Folder";
            this.ctlLabel19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctlLabel20
            // 
            this.ctlLabel20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlLabel20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel20.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel20.Location = new System.Drawing.Point(16, 48);
            this.ctlLabel20.Name = "ctlLabel20";
            this.ctlLabel20.Size = new System.Drawing.Size(152, 20);
            this.ctlLabel20.StylePainter = this.StyleGuideBase;
            this.ctlLabel20.Text = "Multi Font Dialog";
            this.ctlLabel20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctlLabel21
            // 
            this.ctlLabel21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlLabel21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel21.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel21.Location = new System.Drawing.Point(16, 24);
            this.ctlLabel21.Name = "ctlLabel21";
            this.ctlLabel21.Size = new System.Drawing.Size(152, 20);
            this.ctlLabel21.StylePainter = this.StyleGuideBase;
            this.ctlLabel21.Text = "Multi Color Dialog";
            this.ctlLabel21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctlMultiBox1
            // 
            this.ctlMultiBox1.ButtonToolTip = "Color";
            this.ctlMultiBox1.Location = new System.Drawing.Point(176, 24);
            this.ctlMultiBox1.MultiType = Nistec.WinForms.MultiType.Color;
            this.ctlMultiBox1.Name = "ctlMultiBox1";
            this.ctlMultiBox1.Size = new System.Drawing.Size(168, 20);
            this.ctlMultiBox1.StylePainter = this.StyleGuideBase;
            this.ctlMultiBox1.TabIndex = 0;
            // 
            // ctlValidator1
            // 
            this.ctlValidator1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.BlinkIfDifferentError;
            this.ctlValidator1.Icon = ((System.Drawing.Icon)(resources.GetObject("ctlValidator1.Icon")));
            this.ctlValidator1.ValidatorDisplay = Nistec.WinForms.ValidatorDisplay.Dynamic;
            // 
            // ctlResource1
            // 
            this.ctlResource1.ActionMode = Nistec.WinForms.ActionMode.Manual;
            this.ctlResource1.CultureName = "en";
            this.ctlResource1.CultureSupport = new string[] {
        "en",
        "he"};
            this.ctlResource1.CurrentForm = this;
            this.ctlResource1.ResourceBaseName = "Nistec.WinControls.Resources.Resource1";
            this.ctlResource1.ResourceManager = null;
            //// 
            //// ctlPermission1
            //// 
            //this.ctlPermission1.ActionMode = Nistec.WinForms.ActionMode.Auto;
            //this.ctlPermission1.CurrentForm = this;
            //this.ctlPermission1.DefaultLevel = Nistec.Data.PermsLevel.FullControl;
            //this.ctlPermission1.Perms = null;
            // 
            // ctlPanel1
            // 
            this.ctlPanel1.BackColor = System.Drawing.Color.White;
            this.ctlPanel1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ctlPanel1.ContextMenuStrip = this.ctlContextStrip1;
            this.ctlPanel1.Controls.Add(this.ctlTabControl1);
            this.ctlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ctlPanel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlPanel1.Location = new System.Drawing.Point(179, 66);
            this.ctlPanel1.Name = "ctlPanel1";
            this.ctlPanel1.Size = new System.Drawing.Size(417, 324);
            this.ctlPanel1.StylePainter = this.StyleGuideBase;
            this.ctlPanel1.TabIndex = 15;
            // 
            // ctlContextStrip1
            // 
            this.ctlContextStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.context1ToolStripMenuItem,
            this.context2ToolStripMenuItem});
            this.ctlContextStrip1.Name = "ctlContextStrip1";
            this.ctlContextStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ctlContextStrip1.Size = new System.Drawing.Size(123, 48);
            // 
            // context1ToolStripMenuItem
            // 
            this.context1ToolStripMenuItem.Name = "context1ToolStripMenuItem";
            this.context1ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.context1ToolStripMenuItem.Text = "Context 1";
            // 
            // context2ToolStripMenuItem
            // 
            this.context2ToolStripMenuItem.Name = "context2ToolStripMenuItem";
            this.context2ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.context2ToolStripMenuItem.Text = "Context 2";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionVisible = true;
            this.ClientSize = new System.Drawing.Size(788, 479);
            this.Controls.Add(this.ctlPanel1);
            this.Controls.Add(this.dockPanelRight);
            this.Controls.Add(this.dockPanelLeft);
            this.Controls.Add(this.dockPanelBottom);
            this.Controls.Add(this.toolBar);
            this.Name = "Form1";
            this.Text = "Nistec Win Controls";
            this.Controls.SetChildIndex(this.toolBar, 0);
            this.Controls.SetChildIndex(this.dockPanelBottom, 0);
            this.Controls.SetChildIndex(this.dockPanelLeft, 0);
            this.Controls.SetChildIndex(this.dockPanelRight, 0);
            this.Controls.SetChildIndex(this.ctlPanel1, 0);
            this.toolBar.ResumeLayout(false);
            this.dockPanelBottom.ResumeLayout(false);
            this.dockTabTaskBar.ResumeLayout(false);
            this.dockTabMessage.ResumeLayout(false);
            this.dockTabMessage.PerformLayout();
            this.dockTabTrace.ResumeLayout(false);
            this.dockPanelLeft.ResumeLayout(false);
            this.dockTabTools.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ctlTaskPanelBar1)).EndInit();
            this.ctlTaskPanelBar1.ResumeLayout(false);
            this.tpHook.ResumeLayout(false);
            this.tpResources.ResumeLayout(false);
            this.tpPermission.ResumeLayout(false);
            this.tpValidation.ResumeLayout(false);
            this.dockTabTree.ResumeLayout(false);
            this.dockPanelRight.ResumeLayout(false);
            this.dockTabProperty.ResumeLayout(false);
            this.ctlTabControl1.ResumeLayout(false);
            this.pageEditControls.ResumeLayout(false);
            this.pageEditControls.PerformLayout();
            this.pageCommand.ResumeLayout(false);
            this.pageCombos.ResumeLayout(false);
            this.pageMulti.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ctlResource1)).EndInit();
            this.ctlPanel1.ResumeLayout(false);
            this.ctlContextStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private Nistec.WinForms.McToolBar toolBar;
        private Nistec.WinForms.McToolButton tbControlLayout;
        private Nistec.WinForms.McToolButton tbValidation;
        private Nistec.WinForms.McToolButton tbPermission;
        private Nistec.WinForms.McToolButton tbResources;
        private Nistec.WinForms.McToolButton tbProperty;
        private Nistec.WinForms.McDocking ctlDockingManager;
        private Nistec.WinForms.McDockingPanel dockPanelBottom;
        private Nistec.WinForms.McDockingTab dockTabTaskBar;
        private Nistec.WinForms.McDockingTab dockTabMessage;
        private Nistec.WinForms.McDockingTab dockTabTrace;
        private Nistec.WinForms.McDockingPanel dockPanelLeft;
        private Nistec.WinForms.McDockingTab dockTabTree;
        private System.Windows.Forms.TreeView treeView1;
        private Nistec.WinForms.McDockingTab dockTabTools;
        private Nistec.WinForms.McDockingPanel dockPanelRight;
        private Nistec.WinForms.McDockingTab dockTabProperty;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private Nistec.WinForms.McTabControl ctlTabControl1;
        private Nistec.WinForms.McTabPage pageEditControls;
        private Nistec.WinForms.McLabel ctlLabel7;
        private Nistec.WinForms.McLabel ctlLabel6;
        private Nistec.WinForms.McLabel ctlLabel5;
        private Nistec.WinForms.McLabel ctlLabel4;
        private Nistec.WinForms.McLabel ctlLabel3;
        private Nistec.WinForms.McMaskedBox mskIP;
        private Nistec.WinForms.McTextBox txtDate;
        private Nistec.WinForms.McMaskedBox mskDate;
        private Nistec.WinForms.McSpinEdit ctlSpinEdit1;
        private Nistec.WinForms.McMaskedBox mskPhone;
        private Nistec.WinForms.McTextBox txtNumber;
        private Nistec.WinForms.McLabel ctlLabel2;
        private Nistec.WinForms.McTextBox txtSimple;
        private Nistec.WinForms.McLabel ctlLabel1;
        private Nistec.WinForms.McTabPage pageCommand;
        private Nistec.WinForms.McButtonMenu btnMenu2;
        private Nistec.WinForms.McButton btnProgress2;
        private Nistec.WinForms.McProgressBar ctlProgressBar2;
        private Nistec.WinForms.McButton btnProgress1;
        private Nistec.WinForms.McShapes ctlXpLine1;
        private Nistec.WinForms.McProgressBar ctlProgressBar1;
        private Nistec.WinForms.McLinkLabel ctlLinkLabel1;
        private Nistec.WinForms.McButtonMenu btnMenu1;
        private Nistec.WinForms.McButton btnError;
        private Nistec.WinForms.McButton btnInfo;
        private Nistec.WinForms.McTabPage pageCombos;
        private Nistec.WinForms.McMultiPicker ctlImageList;
        private Nistec.WinForms.McLabel ctlLabel8;
        private Nistec.WinForms.McLabel ctlLabel9;
        private Nistec.WinForms.McLabel ctlLabel10;
        private Nistec.WinForms.McLabel ctlLabel11;
        private Nistec.WinForms.McLabel ctlLabel12;
        private Nistec.WinForms.McLabel ctlLabel13;
        private Nistec.WinForms.McLabel ctlLabel14;
        private Nistec.WinForms.McDatePicker ctlDatePicker1;
        private Nistec.WinForms.McMultiPicker ctlBoolean;
        private Nistec.WinForms.McMultiPicker ctlFonts;
        private Nistec.WinForms.McMultiPicker ctlColors;
        private Nistec.WinForms.McMultiPicker ctlDropDown1;
        private Nistec.WinForms.McComboBox ctlComboBox1;
        private Nistec.WinForms.McTabPage pageMulti;
        private Nistec.WinForms.McMultiBox ctlMultiBox7;
        private Nistec.WinForms.McMultiBox ctlMultiBox6;
        private Nistec.WinForms.McMultiBox ctlMultiBox5;
        private Nistec.WinForms.McMultiBox ctlMultiBox4;
        private Nistec.WinForms.McMultiBox ctlMultiBox3;
        private Nistec.WinForms.McMultiBox ctlMultiBox2;
        private Nistec.WinForms.McLabel ctlLabel15;
        private Nistec.WinForms.McLabel ctlLabel16;
        private Nistec.WinForms.McLabel ctlLabel17;
        private Nistec.WinForms.McLabel ctlLabel18;
        private Nistec.WinForms.McLabel ctlLabel19;
        private Nistec.WinForms.McLabel ctlLabel20;
        private Nistec.WinForms.McLabel ctlLabel21;
        private Nistec.WinForms.McMultiBox ctlMultiBox1;
        private Nistec.WinForms.McTaskBar ctlTaskPanelBar1;
        private Nistec.WinForms.McTaskPanel tpHook;
        private Nistec.WinForms.LinkLabelItem linkShowNotify;
        private Nistec.WinForms.LinkLabelItem linkShowMsg;
        private Nistec.WinForms.LinkLabelItem linkInputBox;
        private Nistec.WinForms.McTaskPanel tpResources;
        private Nistec.WinForms.LinkLabelItem linkLabelItem4;
        private Nistec.WinForms.LinkLabelItem linkLabelItem5;
        private Nistec.WinForms.McTaskPanel tpPermission;
        private Nistec.WinForms.LinkLabelItem linkLabelItem6;
        private Nistec.WinForms.LinkLabelItem linkLabelItem7;
        private Nistec.WinForms.McTaskPanel tpValidation;
        private Nistec.WinForms.McTreeView ctlTreeView1;
        private Nistec.WinForms.McValidator ctlValidator1;
        private Nistec.WinForms.McResource ctlResource1;
        //private Nistec.WinForms.McPermission ctlPermission1;
        private Nistec.WinForms.McPanel ctlPanel1;
        private Nistec.WinForms.McToolButton tbStyles;
        private Nistec.WinForms.PopUpItem Desktop;
        private Nistec.WinForms.PopUpItem SteelBlue;
        private Nistec.WinForms.PopUpItem Goldenrod;
        private Nistec.WinForms.PopUpItem SeaGreen;
        private Nistec.WinForms.PopUpItem Carmel;
        private Nistec.WinForms.PopUpItem Silver;
        private Nistec.WinForms.PopUpItem Media;
        private Nistec.WinForms.PopUpItem SystemStyle;
        private Nistec.WinForms.PopUpItem Visual;
        private Nistec.WinForms.PopUpItem Flat;
        private Nistec.WinForms.PopUpItem XpLayout;
        private Nistec.WinForms.PopUpItem VistaLayout;
        private Nistec.WinForms.PopUpItem SystemLayout;
        private Nistec.WinForms.McTextBox txtMessages;
        private Nistec.WinForms.McContextStrip ctlContextStrip1;
        private System.Windows.Forms.ToolStripMenuItem context1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem context2ToolStripMenuItem;
        private Nistec.WinForms.PopUpItem popUpItem4;
        private Nistec.WinForms.PopUpItem popUpItem5;
        private Nistec.WinForms.PopUpItem popUpItem6;
        private Nistec.WinForms.PopUpItem popUpItem1;
        private Nistec.WinForms.PopUpItem popUpItem2;
        private Nistec.WinForms.PopUpItem popUpItem3;
        private Nistec.WinForms.McHelpLabel ctlHelpLabel1;
        private Nistec.WinForms.McListBox ctlListBox1;
    }
}