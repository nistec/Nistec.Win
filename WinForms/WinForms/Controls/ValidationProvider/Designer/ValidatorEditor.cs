
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using Nistec.Win;




namespace Nistec.WinForms
{
	/// <summary>
	/// ValidatorEditor use to setup validation rule and assign it to a
	/// specific control.
	/// </summary>
	internal class ValidatorEditor : Nistec.WinForms.FormBase
	{
		private IComponentChangeService			_ComponentChangeService = null;
		private IDesignerHost					_DesignerHost			= null;
		private McValidator					_ValidationProvider		= null;
		private Hashtable						_ValidationComponents	= new Hashtable();
		private RegExPatternCollection			_RegExPatterns			= null;
		private McValidator					_LocalValidationProvider = new McValidator();
        private ValidatorRule _TestValueValidationRule = new ValidatorRule();
        private System.Windows.Forms.ComboBox ControlsDropDownList;
		private Nistec.WinForms.McButton CloseButton;
		private Nistec.WinForms.McButton NewButton;
		private Nistec.WinForms.McButton SaveButton;
		private Nistec.WinForms.McTabControl tabControl1;
		private Nistec.WinForms.McTabPage tabPage1;
		private Nistec.WinForms.McTabPage tabPage2;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private Nistec.WinForms.McLabel label2;
		private Nistec.WinForms.McLabel label3;
		private Nistec.WinForms.McLabel label4;
		private Nistec.WinForms.McLabel label5;
		private Nistec.WinForms.McButton LoadPatternsButton;
        private System.Windows.Forms.ComboBox RegExPatternsDropDownList;
		private Nistec.WinForms.McTextBox RegExErrorMessageTextBox;
		private Nistec.WinForms.McTextBox RegExTestValueTextBox;
		private Nistec.WinForms.McTextBox RegExPatternTextBox;
		private Nistec.WinForms.McButton UsePatternButton;
		private System.Windows.Forms.PropertyGrid propertyGrid1;
		private System.Windows.Forms.ToolTip toolTip1;
		private Nistec.WinForms.McButton btnTest;
		private Nistec.WinForms.McButton ctlRemove;
		private Nistec.WinForms.McButton ctlAddRange;
		private Nistec.WinForms.McButton ctlRemoveRange;
		private System.ComponentModel.IContainer components;

		/// <summary>
		/// Default Ctor.
		/// </summary>
		public ValidatorEditor()
		{
            //MessageBox.Show("vld1");
			InitializeComponent();
			//_ValidationProvider.RuleChanged+=new EventHandler(_ValidationProvider_RuleChanged);
			this.ctlRemoveRange.Visible=false;
			this.ctlAddRange.Visible=false;
		}


		/// <summary>
		/// Validation Designer Ctor.
		/// </summary>
		/// <param name="designerHost"></param>
		/// <param name="validationProvider"></param>
		/// <param name="editorSelectedComponents"></param>
		public ValidatorEditor(IDesignerHost designerHost, McValidator validationProvider, object[] editorSelectedComponents)
        {
            //MessageBox.Show("vld2");
			InitializeComponent();
            this.ctlRemoveRange.Visible = false;
			this.ctlAddRange.Visible=false;

			this._DesignerHost = designerHost;
			this._ComponentChangeService = this._DesignerHost.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
			this._ValidationProvider = validationProvider;
			if (editorSelectedComponents == null || editorSelectedComponents.Length <= 0)
				this.GetInputControls(this._DesignerHost.Container.Components);
			else
			{
				foreach(object inputControl in editorSelectedComponents)
					this.AddInputControl(inputControl);
			}

			if (this.ControlsDropDownList.Items.Count > 0) 
				this.ControlsDropDownList.SelectedIndex = 0;
			this.tabControl1.SelectedIndex=1;
			this.tabControl1.SelectedIndex=0;

		}

//		protected override void OnHandleCreated(EventArgs e)
//		{
//			base.OnHandleCreated (e);
//			this.tabControl1.SelectedTab=this.tabPage1;
//		}


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
            this.SaveButton = new Nistec.WinForms.McButton();
            this.ControlsDropDownList = new System.Windows.Forms.ComboBox();
            this.CloseButton = new Nistec.WinForms.McButton();
            this.NewButton = new Nistec.WinForms.McButton();
            this.tabControl1 = new Nistec.WinForms.McTabControl();
            this.tabPage1 = new Nistec.WinForms.McTabPage();
            this.ctlRemoveRange = new Nistec.WinForms.McButton();
            this.ctlAddRange = new Nistec.WinForms.McButton();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.tabPage2 = new Nistec.WinForms.McTabPage();
            this.btnTest = new Nistec.WinForms.McButton();
            this.label5 = new Nistec.WinForms.McLabel();
            this.label4 = new Nistec.WinForms.McLabel();
            this.label3 = new Nistec.WinForms.McLabel();
            this.label2 = new Nistec.WinForms.McLabel();
            this.RegExErrorMessageTextBox = new Nistec.WinForms.McTextBox();
            this.RegExTestValueTextBox = new Nistec.WinForms.McTextBox();
            this.RegExPatternTextBox = new Nistec.WinForms.McTextBox();
            this.LoadPatternsButton = new Nistec.WinForms.McButton();
            this.RegExPatternsDropDownList = new System.Windows.Forms.ComboBox();
            this.UsePatternButton = new Nistec.WinForms.McButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ctlRemove = new Nistec.WinForms.McButton();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // StyleGuideBase
            // 
            this.StyleGuideBase.AlternatingColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(230)))), ((int)(((byte)(220)))));
            this.StyleGuideBase.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(106)))), ((int)(((byte)(197)))));
            this.StyleGuideBase.BorderHotColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(80)))), ((int)(((byte)(184)))));
            this.StyleGuideBase.CaptionColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(78)))), ((int)(((byte)(152)))));
            this.StyleGuideBase.ColorBrush1 = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(203)))), ((int)(((byte)(183)))));
            this.StyleGuideBase.ColorBrush2 = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.StyleGuideBase.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.StyleGuideBase.FocusedColor = System.Drawing.Color.RoyalBlue;
            this.StyleGuideBase.FormColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.StyleGuideBase.StylePlan = Nistec.WinForms.Styles.Desktop;
            // 
            // SaveButton
            // 
            this.SaveButton.DialogResult = System.Windows.Forms.DialogResult.None;
            this.SaveButton.Enabled = false;
            this.SaveButton.Location = new System.Drawing.Point(174, 366);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(72, 24);
            this.SaveButton.TabIndex = 0;
            this.SaveButton.Text = "Save";
            this.toolTip1.SetToolTip(this.SaveButton, "Save ValidatorRule settings to the cuurent control.");
            this.SaveButton.ToolTipText = "Save";
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // ControlsDropDownList
            // 
            this.ControlsDropDownList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ControlsDropDownList.DropDownWidth = 396;
            this.ControlsDropDownList.IntegralHeight = false;
            this.ControlsDropDownList.ItemHeight = 13;
            this.ControlsDropDownList.Location = new System.Drawing.Point(8, 8);
            this.ControlsDropDownList.Name = "ControlsDropDownList";
            this.ControlsDropDownList.Size = new System.Drawing.Size(396, 21);
            this.ControlsDropDownList.TabIndex = 3;
            this.toolTip1.SetToolTip(this.ControlsDropDownList, "Controls that can be validate.");
            this.ControlsDropDownList.SelectedIndexChanged += new System.EventHandler(this.ControlsDropDownList_SelectedIndexChanged);
            // 
            // CloseButton
            // 
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.Location = new System.Drawing.Point(334, 366);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(70, 24);
            this.CloseButton.TabIndex = 0;
            this.CloseButton.Text = "Close";
            this.toolTip1.SetToolTip(this.CloseButton, "Close and  back to Form Designer.");
            this.CloseButton.ToolTipText = "Close";
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // NewButton
            // 
            this.NewButton.DialogResult = System.Windows.Forms.DialogResult.None;
            this.NewButton.Enabled = false;
            this.NewButton.Location = new System.Drawing.Point(94, 366);
            this.NewButton.Name = "NewButton";
            this.NewButton.Size = new System.Drawing.Size(70, 24);
            this.NewButton.TabIndex = 7;
            this.NewButton.Text = "New";
            this.toolTip1.SetToolTip(this.NewButton, "Create new ValidatorRule for current Control.");
            this.NewButton.ToolTipText = "New";
            this.NewButton.Click += new System.EventHandler(this.NewButton_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.ControlLayout = Nistec.WinForms.ControlLayout.System;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.tabControl1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabControl1.ItemSize = new System.Drawing.Size(0, 22);
            this.tabControl1.Location = new System.Drawing.Point(8, 40);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Size = new System.Drawing.Size(397, 320);
            this.tabControl1.StylePainter = this.StyleGuideBase;
            this.tabControl1.TabIndex = 8;
            this.tabControl1.TabPages.AddRange(new Nistec.WinForms.McTabPage[] {
            this.tabPage1,
            this.tabPage2});
            this.tabControl1.TabStop = false;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.tabPage1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tabPage1.ControlLayout = Nistec.WinForms.ControlLayout.Flat;
            this.tabPage1.Controls.Add(this.ctlRemoveRange);
            this.tabPage1.Controls.Add(this.ctlAddRange);
            this.tabPage1.Controls.Add(this.propertyGrid1);
            this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.tabPage1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPage1.Location = new System.Drawing.Point(4, 31);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.PageVisible = true;
            this.tabPage1.Size = new System.Drawing.Size(388, 284);
            this.tabPage1.StylePainter = this.StyleGuideBase;
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Validation Rule Property";
            this.tabPage1.ToolTipText = "";
            this.tabPage1.Visible = false;
            // 
            // ctlRemoveRange
            // 
            this.ctlRemoveRange.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ctlRemoveRange.Enabled = false;
            this.ctlRemoveRange.Location = new System.Drawing.Point(107, 261);
            this.ctlRemoveRange.Name = "ctlRemoveRange";
            this.ctlRemoveRange.Size = new System.Drawing.Size(96, 20);
            this.ctlRemoveRange.TabIndex = 13;
            this.ctlRemoveRange.Text = "Remove RangeType";
            this.toolTip1.SetToolTip(this.ctlRemoveRange, "Remove RangeType");
            this.ctlRemoveRange.ToolTipText = "Remove RangeType";
            // 
            // ctlAddRange
            // 
            this.ctlAddRange.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ctlAddRange.Enabled = false;
            this.ctlAddRange.Location = new System.Drawing.Point(3, 261);
            this.ctlAddRange.Name = "ctlAddRange";
            this.ctlAddRange.Size = new System.Drawing.Size(96, 20);
            this.ctlAddRange.TabIndex = 12;
            this.ctlAddRange.Text = "Add RangeType";
            this.toolTip1.SetToolTip(this.ctlAddRange, "Add RangeType ");
            this.ctlAddRange.ToolTipText = "Add RangeType";
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.CommandsBackColor = System.Drawing.Color.AliceBlue;
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Top;
            this.propertyGrid1.HelpVisible = false;
            this.propertyGrid1.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(388, 256);
            this.propertyGrid1.TabIndex = 0;
            this.propertyGrid1.ToolbarVisible = false;
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.tabPage2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tabPage2.ControlLayout = Nistec.WinForms.ControlLayout.Flat;
            this.tabPage2.Controls.Add(this.btnTest);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.RegExErrorMessageTextBox);
            this.tabPage2.Controls.Add(this.RegExTestValueTextBox);
            this.tabPage2.Controls.Add(this.RegExPatternTextBox);
            this.tabPage2.Controls.Add(this.LoadPatternsButton);
            this.tabPage2.Controls.Add(this.RegExPatternsDropDownList);
            this.tabPage2.Controls.Add(this.UsePatternButton);
            this.tabPage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.tabPage2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPage2.Location = new System.Drawing.Point(4, 31);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.PageVisible = true;
            this.tabPage2.Selected = false;
            this.tabPage2.Size = new System.Drawing.Size(388, 284);
            this.tabPage2.StylePainter = this.StyleGuideBase;
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Regular Expression Tester";
            this.tabPage2.ToolTipText = "";
            this.tabPage2.Visible = false;
            // 
            // btnTest
            // 
            this.btnTest.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnTest.Enabled = false;
            this.btnTest.Location = new System.Drawing.Point(217, 250);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(70, 20);
            this.btnTest.TabIndex = 11;
            this.btnTest.Text = "Test";
            this.toolTip1.SetToolTip(this.btnTest, "Test the Regex Pattern");
            this.btnTest.ToolTipText = "Test";
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.label5.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.ImageIndex = 0;
            this.label5.Location = new System.Drawing.Point(4, 168);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.Text = "Test Value:";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.ImageIndex = 0;
            this.label4.Location = new System.Drawing.Point(4, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.Text = "RegEx Pattern:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.ImageIndex = 0;
            this.label3.Location = new System.Drawing.Point(4, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.Text = "Error Message:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.ImageIndex = 0;
            this.label2.Location = new System.Drawing.Point(4, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.Text = "Pattern Name:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label2.Visible = false;
            // 
            // RegExErrorMessageTextBox
            // 
            this.RegExErrorMessageTextBox.Location = new System.Drawing.Point(16, 75);
            this.RegExErrorMessageTextBox.Name = "RegExErrorMessageTextBox";
            this.RegExErrorMessageTextBox.Size = new System.Drawing.Size(352, 20);
            this.RegExErrorMessageTextBox.TabIndex = 6;
            this.toolTip1.SetToolTip(this.RegExErrorMessageTextBox, "Error message when validate  failed.");
            // 
            // RegExTestValueTextBox
            // 
            this.RegExTestValueTextBox.Location = new System.Drawing.Point(16, 187);
            this.RegExTestValueTextBox.Multiline = true;
            this.RegExTestValueTextBox.Name = "RegExTestValueTextBox";
            this.RegExTestValueTextBox.Size = new System.Drawing.Size(352, 40);
            this.RegExTestValueTextBox.TabIndex = 5;
            this.toolTip1.SetToolTip(this.RegExTestValueTextBox, "Pattern test value .");
            this.RegExTestValueTextBox.TextChanged += new System.EventHandler(this.RegExTestValueTextBox_TextChanged);
            // 
            // RegExPatternTextBox
            // 
            this.RegExPatternTextBox.Location = new System.Drawing.Point(16, 120);
            this.RegExPatternTextBox.Multiline = true;
            this.RegExPatternTextBox.Name = "RegExPatternTextBox";
            this.RegExPatternTextBox.Size = new System.Drawing.Size(352, 42);
            this.RegExPatternTextBox.TabIndex = 4;
            this.toolTip1.SetToolTip(this.RegExPatternTextBox, "Regular Expression Pattern.");
            // 
            // LoadPatternsButton
            // 
            this.LoadPatternsButton.DialogResult = System.Windows.Forms.DialogResult.None;
            this.LoadPatternsButton.Enabled = false;
            this.LoadPatternsButton.Location = new System.Drawing.Point(272, 29);
            this.LoadPatternsButton.Name = "LoadPatternsButton";
            this.LoadPatternsButton.Size = new System.Drawing.Size(96, 23);
            this.LoadPatternsButton.TabIndex = 3;
            this.LoadPatternsButton.Text = "Load Patterns";
            this.toolTip1.SetToolTip(this.LoadPatternsButton, "Load  patterns xml file.");
            this.LoadPatternsButton.ToolTipText = "Load Patterns";
            this.LoadPatternsButton.Click += new System.EventHandler(this.LoadPatternsButton_Click);
            // 
            // RegExPatternsDropDownList
            // 
            this.RegExPatternsDropDownList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RegExPatternsDropDownList.DropDownWidth = 248;
            this.RegExPatternsDropDownList.Enabled = false;
            this.RegExPatternsDropDownList.IntegralHeight = false;
            this.RegExPatternsDropDownList.ItemHeight = 13;
            this.RegExPatternsDropDownList.Location = new System.Drawing.Point(16, 29);
            this.RegExPatternsDropDownList.Name = "RegExPatternsDropDownList";
            this.RegExPatternsDropDownList.Size = new System.Drawing.Size(248, 21);
            this.RegExPatternsDropDownList.TabIndex = 1;
            this.toolTip1.SetToolTip(this.RegExPatternsDropDownList, "Name of pattern.");
            // 
            // UsePatternButton
            // 
            this.UsePatternButton.DialogResult = System.Windows.Forms.DialogResult.None;
            this.UsePatternButton.Enabled = false;
            this.UsePatternButton.Location = new System.Drawing.Point(297, 250);
            this.UsePatternButton.Name = "UsePatternButton";
            this.UsePatternButton.Size = new System.Drawing.Size(70, 20);
            this.UsePatternButton.TabIndex = 5;
            this.UsePatternButton.Text = "Use Pattern";
            this.toolTip1.SetToolTip(this.UsePatternButton, "Copy pattern to current Control ValidatorRule.");
            this.UsePatternButton.ToolTipText = "Use Pattern";
            this.UsePatternButton.Click += new System.EventHandler(this.UsePatternButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "ReExPatternStore.xml";
            this.openFileDialog1.Filter = "Xml files|*.xml|All files|*.*";
            this.openFileDialog1.Title = "Select a Regular Expression pattern store xml file...";
            // 
            // ctlRemove
            // 
            this.ctlRemove.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ctlRemove.Location = new System.Drawing.Point(254, 366);
            this.ctlRemove.Name = "ctlRemove";
            this.ctlRemove.Size = new System.Drawing.Size(70, 24);
            this.ctlRemove.TabIndex = 9;
            this.ctlRemove.Text = "Remove";
            this.toolTip1.SetToolTip(this.ctlRemove, "Create new ValidatorRule for current Control.");
            this.ctlRemove.ToolTipText = "Remove";
            this.ctlRemove.Click += new System.EventHandler(this.ctlRemove_Click);
            // 
            // ValidatorEditor
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            //this.CancelButton = this.CloseButton;
            this.ClientSize = new System.Drawing.Size(412, 399);
            this.Controls.Add(this.ctlRemove);
            this.Controls.Add(this.NewButton);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.ControlsDropDownList);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ValidatorEditor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Validation Editor";
            this.TopMost = true;
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.Controls.SetChildIndex(this.SaveButton, 0);
            this.Controls.SetChildIndex(this.ControlsDropDownList, 0);
            this.Controls.SetChildIndex(this.CloseButton, 0);
            this.Controls.SetChildIndex(this.NewButton, 0);
            this.Controls.SetChildIndex(this.ctlRemove, 0);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		private void SaveButton_Click(object sender, System.EventArgs e)
		{
			try 
			{
                //MessageBox.Show("vld6" + this.ControlsDropDownList.SelectedItem.ToString());

				object selectedComponent = null;
				if (this.ControlsDropDownList.SelectedIndex >= 0)
					selectedComponent = this._ValidationComponents[this.ControlsDropDownList.SelectedItem];
			
				if (selectedComponent != null) 
				{
                    //MessageBox.Show("vld7");
                    this._ComponentChangeService.OnComponentChanging(this, null);
					this._ValidationProvider.SetValidatorRule(selectedComponent, this.propertyGrid1.SelectedObject as ValidatorRule);
					this._ComponentChangeService.OnComponentChanged(selectedComponent, null, null, null);
					this.Close();
				}
			} 
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void GetInputControls(ComponentCollection components)
		{
			try
			{
				foreach (object inputControl in components)
				{
					this.AddInputControl(inputControl);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void AddInputControl(object inputControl)
		{
			if (_ValidationProvider.CanExtend(inputControl))
			{
				this._ValidationComponents.Add(TypeDescriptor.GetComponentName(inputControl), inputControl);
				this.ControlsDropDownList.Items.Add(TypeDescriptor.GetComponentName(inputControl));
			}

		}
		private void RemoveInputControl()
		{
			try 
			{
				object selectedComponent = null;
				if (this.ControlsDropDownList.SelectedIndex >= 0)
				{
					selectedComponent = this._ValidationComponents[this.ControlsDropDownList.SelectedItem];
			
					if (selectedComponent != null) 
					{
						this._ComponentChangeService.OnComponentChanging(this, null);
						this._ValidationProvider.RemoveValidatorRule(selectedComponent, this.propertyGrid1.SelectedObject as ValidatorRule);
						this._ComponentChangeService.OnComponentChanged(selectedComponent, null, null, null);
					}
					if (this._ValidationComponents.Contains(selectedComponent))
					{
						this._ValidationComponents.Remove(TypeDescriptor.GetComponentName(selectedComponent));
						this.ControlsDropDownList.Items.Remove(TypeDescriptor.GetComponentName(selectedComponent));
					}
					this.propertyGrid1.SelectedObject = null;
				}
			} 
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}


		}

		private void CloseButton_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void ControlsDropDownList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			bool enabled=false;
			this.NewButton.Enabled = (this.ControlsDropDownList.SelectedItem != null);
			this.SaveButton.Enabled = this.NewButton.Enabled;

			if (this.NewButton.Enabled) 
			{

				ValidatorRule vr = this._ValidationProvider.GetValidatorRule(
					this._ValidationComponents[this.ControlsDropDownList.SelectedItem]);
				this.propertyGrid1.SelectedObject = vr as ValidatorRule;
				if(vr!=null)
				{
					enabled= vr.DataType!= BaseDataType.String;
				}
			}
			this.ctlAddRange.Enabled=enabled;
			this.ctlRemoveRange.Enabled=enabled;
			this.ctlRemove.Enabled = this.propertyGrid1.SelectedObject!=null;
		}

		private void NewButton_Click(object sender, System.EventArgs e)
		{
			this.propertyGrid1.SelectedObject = new ValidatorRule();
		}

		private void ctlRemove_Click(object sender, System.EventArgs e)
		{
			RemoveInputControl();
		}


		private void LoadPatternsButton_Click(object sender, System.EventArgs e)
		{
			if (DialogResult.OK == openFileDialog1.ShowDialog())
			{
				object repc = WinHelp.XmlStringToObject(
					IoHelper.FileToString(openFileDialog1.FileName), typeof(RegExPatternCollection));
				this._RegExPatterns = repc as RegExPatternCollection;
				if (repc != null)
				{
					this.RegExPatternsDropDownList.DataSource = this._RegExPatterns;
					this.RegExPatternsDropDownList.DisplayMember = "PatternName";
					//this.RegExPatternsDropDownList.ValueMember = "PatternName";
					this.RegExPatternsDropDownList.DataBindings.Add("Text", this._RegExPatterns, "PatternName");
					this.RegExErrorMessageTextBox.DataBindings.Add("Text", this._RegExPatterns, "ErrorMessage");
					this.RegExPatternTextBox.DataBindings.Add("Text", this._RegExPatterns, "Pattern");
					this.RegExTestValueTextBox.DataBindings.Add("Text", this._RegExPatterns, "TestValue");

				}
			}
		}

		private void TestPatternButton_Click(object sender, System.EventArgs e)
		{
			bool isValid = this._LocalValidationProvider.Validate();
			//this._LocalValidationProvider.ValidatorMessages(!isValid);
			if (isValid) 
                MessageBox.Show("Test Value is valid.", this.Name);
            else
                MessageBox.Show(this._LocalValidationProvider.ErrorMessageResult, this.Name);
         
		}

		private void UsePatternButton_Click(object sender, System.EventArgs e)
		{
			ValidatorRule vr = this.propertyGrid1.SelectedObject as ValidatorRule;
			if (vr != null)
			{
				if (!this.RegExErrorMessageTextBox.Text.Trim().Equals(string.Empty))
					vr.ErrorMessage = this.RegExErrorMessageTextBox.Text;

				if (!this.RegExErrorMessageTextBox.Text.Trim().Equals(string.Empty))
					vr.RegExPattern = this.RegExPatternTextBox.Text;
				tabControl1.SelectedIndex = 0;
				this.propertyGrid1.Update();
			}
		}

		private void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ValidatorRule vr = this.propertyGrid1.SelectedObject as ValidatorRule;

			if (this.tabControl1.SelectedIndex == 1 && vr != null)
			{
				this.RegExPatternsDropDownList.Text = string.Empty;
				this.RegExPatternTextBox.Text = vr.RegExPattern;
				this.RegExErrorMessageTextBox.Text = vr.ErrorMessage;
			}
		}

		private void propertyGrid1_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
		{
			ValidatorRule vr = this.propertyGrid1.SelectedObject as ValidatorRule;
			Control ctrl = this._ValidationComponents[this.ControlsDropDownList.SelectedItem] as Control;
			if (vr != null && ctrl != null & vr.Required == true && !ctrl.Text.Equals(string.Empty) && !ctrl.Text.Equals(vr.InitialValue))
				vr.InitialValue = ctrl.Text;
 
			
			if(e.ChangedItem.PropertyDescriptor.Name=="DataType")
			{
				//-vr.RangeValue=null;
				this.propertyGrid1.Refresh();   
				
				this.ctlAddRange.Enabled= vr.DataType!= BaseDataType.String;
				this.ctlRemoveRange.Enabled=this.ctlAddRange.Enabled;
			}

		}

		private void RegExTestValueTextBox_TextChanged(object sender, System.EventArgs e)
		{
		
			ValidatorRule vr = this._TestValueValidationRule;
			vr.RegExPattern = this.RegExPatternTextBox.Text;
			vr.ErrorMessage = this.RegExErrorMessageTextBox.Text;
			if (vr.RegExPattern.Length > 0) 
			{
				this._LocalValidationProvider.SetValidatorRule(this.RegExTestValueTextBox, vr);
				this._LocalValidationProvider.SetIconAlignment(this.RegExTestValueTextBox, ErrorIconAlignment.BottomLeft);
				//bool isValid = this._LocalValidationProvider.Validate();
				//this._LocalValidationProvider.ValidatorMessages(!isValid);
                bool isValid = this._LocalValidationProvider.ValidateAll();

				this.UsePatternButton.Enabled = isValid;
//				if (isValid) 
//					this.label5.BackColor = System.Drawing.Color.Green;
//				else 
//					this.label5.BackColor = System.Drawing.Color.Red;	
			} else
				this.UsePatternButton.Enabled = false;
			this.btnTest.Enabled=(this.RegExTestValueTextBox.Text.Length>0 && this.RegExPatternTextBox.Text.Length > 0);
		}

		private void btnTest_Click(object sender, System.EventArgs e)
		{
			if(this.RegExTestValueTextBox.Text.Length == 0)
				return;
			ValidatorRule vr = this._TestValueValidationRule;
			vr.RegExPattern = this.RegExPatternTextBox.Text;
			vr.ErrorMessage = this.RegExErrorMessageTextBox.Text;
			if (vr.RegExPattern.Length > 0) 
			{
				this._LocalValidationProvider.SetValidatorRule(this.RegExTestValueTextBox, vr);
				this._LocalValidationProvider.SetIconAlignment(this.RegExTestValueTextBox, ErrorIconAlignment.BottomLeft);
				//bool isValid = this._LocalValidationProvider.Validate();
				//this._LocalValidationProvider.ValidatorMessages(!isValid);
                bool isValid = this._LocalValidationProvider.ValidateAll();
                this.UsePatternButton.Enabled = isValid;
				string msg="";
				if (isValid) 
					msg="Valid "; //this.label5.BackColor = System.Drawing.Color.Green;
				else if(vr.ErrorMessage.Length>0)
					msg= vr.ErrorMessage;
                else
					msg= "Not Valid ";//this.label5.BackColor = System.Drawing.Color.Red;	
                Nistec.WinForms.MsgDlg.ShowMsg(this.btnTest,msg,"Nistec");
			} 
			else
				this.UsePatternButton.Enabled = false;

		}

//		private void ctlAddRange_Click(object sender, System.EventArgs e)
//		{
//			object selectedComponent = null;
//			if (this.ControlsDropDownList.SelectedIndex >= 0)
//			{
//				selectedComponent = this._ValidationComponents[this.ControlsDropDownList.SelectedItem];
//			
//				if (selectedComponent != null) 
//				{
//					ValidatorRule vr=this.propertyGrid1.SelectedObject as ValidatorRule;
//					if(vr==null)
//						vr=this._TestValueValidationRule;
//                   //- vr.AddRangeValue();
//				}
//				this.propertyGrid1.Refresh();
//			}
//
//		}
//
//		private void ctlRemoveRange_Click(object sender, System.EventArgs e)
//		{
//			object selectedComponent = null;
//			if (this.ControlsDropDownList.SelectedIndex >= 0)
//			{
//				selectedComponent = this._ValidationComponents[this.ControlsDropDownList.SelectedItem];
//			
//				if (selectedComponent != null) 
//				{
//					ValidatorRule vr=this.propertyGrid1.SelectedObject as ValidatorRule;
//					if(vr==null)
//						vr=this._TestValueValidationRule;
//					vr.RemoveRangeValue();
//				}
//				this.propertyGrid1.Refresh();
//			}
//
//		}
	}
}
