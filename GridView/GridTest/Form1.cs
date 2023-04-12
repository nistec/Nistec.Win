using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using mControl.GridView;
using System.Data;

namespace GridTest
{
	/// <summary>
	/// Summary description for Form3.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private mControl.GridView.Grid grid1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button button1;
		private mControl.WinCtl.Controls.StyleGrid styleGrid1;
		private mControl.GridView.GridMemoColumn gridTextColumn1;
		private mControl.GridView.GridBoolColumn gridBoolColumn1;
		private mControl.GridView.GridComboColumn gridComboColumn1;
		private mControl.GridView.GridDateColumn gridDateColumn1;
		private mControl.GridView.GridProgressColumn gridProgressColumn1;
        private mControl.GridView.VGridColumn gridControlColumn1;
		private mControl.GridView.GridMultiColumn gridMultiColumn1;
		private mControl.GridView.GridButtonColumn gridButtonColumn1;
        private Button button2;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private mControl.WinCtl.Controls.CtlSpinEdit ctlSpinEdit1;
        private mControl.WinCtl.Controls.CtlSpinEdit ctlSpinEdit2;
        private mControl.WinCtl.Controls.CtlComboBox ctlComboBox1;
        private GridNumericColumn gridNumericColumn1;
        private Button button3;
        private Button button4;
        private CheckBox checkIsNew;
        private TextBox textRowState;
        private Button btnRowState;
        private Panel panel1;
        private DataSet dataSet1;
        private DataTable dataTable1;
        private DataTable dataTable2;
        private DataTable dataTable3;
        private Button button5;
        private mControl.WinCtl.Controls.StyleEdit styleEdit1;
        private Button button6;
        private Button button7;
        private Timer timer1;
        private CheckBox checkBox3;
        private ProgressBar progressBar1;
        private Button button8;
        private Button button9;
        private TextBox textFilter;
        private GridStatusBar gridStatusBar1;
        private GridStatusPanel gridStatusPanel1;
        private GridStatusPanel gridStatusPanel2;
        private StatusBarPanel statusBarPanel1;
        private StatusBarPanel statusBarPanel2;
        private Button button10;
        private Button button11;
		private System.ComponentModel.IContainer components;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.ctlComboBox1 = new mControl.WinCtl.Controls.CtlComboBox();
            this.ctlSpinEdit2 = new mControl.WinCtl.Controls.CtlSpinEdit();
            this.ctlSpinEdit1 = new mControl.WinCtl.Controls.CtlSpinEdit();
            this.styleGrid1 = new mControl.WinCtl.Controls.StyleGrid(this.components);
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.checkIsNew = new System.Windows.Forms.CheckBox();
            this.textRowState = new System.Windows.Forms.TextBox();
            this.btnRowState = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button11 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.dataSet1 = new System.Data.DataSet();
            this.dataTable1 = new System.Data.DataTable();
            this.dataTable2 = new System.Data.DataTable();
            this.dataTable3 = new System.Data.DataTable();
            this.styleEdit1 = new mControl.WinCtl.Controls.StyleEdit(this.components);
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.textFilter = new System.Windows.Forms.TextBox();
            this.gridStatusBar1 = new mControl.GridView.GridStatusBar();
            this.grid1 = new mControl.GridView.Grid();
            this.gridControlColumn1 = new mControl.GridView.VGridColumn();
            this.gridTextColumn1 = new mControl.GridView.GridMemoColumn();
            this.gridBoolColumn1 = new mControl.GridView.GridBoolColumn();
            this.gridComboColumn1 = new mControl.GridView.GridComboColumn();
            this.gridDateColumn1 = new mControl.GridView.GridDateColumn();
            this.gridProgressColumn1 = new mControl.GridView.GridProgressColumn();
            this.gridMultiColumn1 = new mControl.GridView.GridMultiColumn();
            this.gridButtonColumn1 = new mControl.GridView.GridButtonColumn();
            this.gridNumericColumn1 = new mControl.GridView.GridNumericColumn();
            this.gridStatusPanel1 = new mControl.GridView.GridStatusPanel();
            this.gridStatusPanel2 = new mControl.GridView.GridStatusPanel();
            this.statusBarPanel1 = new System.Windows.Forms.StatusBarPanel();
            this.statusBarPanel2 = new System.Windows.Forms.StatusBarPanel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel2)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(95, 7);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(278, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "textBox1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(64, 24);
            this.button1.TabIndex = 3;
            this.button1.Text = "accept";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(15, 48);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(64, 24);
            this.button2.TabIndex = 4;
            this.button2.Text = "cancel";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(109, 55);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(45, 17);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "dirty";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(195, 55);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(67, 17);
            this.checkBox2.TabIndex = 6;
            this.checkBox2.Text = "dirtyRow";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // ctlComboBox1
            // 
            this.ctlComboBox1.ButtonToolTip = "";
            this.ctlComboBox1.IntegralHeight = false;
            this.ctlComboBox1.Location = new System.Drawing.Point(513, 70);
            this.ctlComboBox1.Name = "ctlComboBox1";
            this.ctlComboBox1.Size = new System.Drawing.Size(112, 20);
            this.ctlComboBox1.TabIndex = 9;
            this.ctlComboBox1.Text = "ctlComboBox1";
            // 
            // ctlSpinEdit2
            // 
            this.ctlSpinEdit2.ButtonAlign = mControl.WinCtl.Controls.ButtonAlign.Right;
            this.ctlSpinEdit2.ControlLayout = mControl.WinCtl.Controls.ControlLayout.Flat;
            this.ctlSpinEdit2.DecimalPlaces = 0;
            this.ctlSpinEdit2.DefaultValue = "";
            this.ctlSpinEdit2.Format = "N";
            this.ctlSpinEdit2.FormatType = mControl.Util.NumberFormats.StandadNumber;
            this.ctlSpinEdit2.Location = new System.Drawing.Point(513, 40);
            this.ctlSpinEdit2.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.ctlSpinEdit2.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ctlSpinEdit2.Name = "ctlSpinEdit2";
            this.ctlSpinEdit2.Size = new System.Drawing.Size(112, 20);
            this.ctlSpinEdit2.TabIndex = 8;
            this.ctlSpinEdit2.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // ctlSpinEdit1
            // 
            this.ctlSpinEdit1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ctlSpinEdit1.ButtonAlign = mControl.WinCtl.Controls.ButtonAlign.Right;
            this.ctlSpinEdit1.ControlLayout = mControl.WinCtl.Controls.ControlLayout.System;
            this.ctlSpinEdit1.DecimalPlaces = 0;
            this.ctlSpinEdit1.DefaultValue = "";
            this.ctlSpinEdit1.FixSize = false;
            this.ctlSpinEdit1.Format = "N";
            this.ctlSpinEdit1.FormatType = mControl.Util.NumberFormats.StandadNumber;
            this.ctlSpinEdit1.Location = new System.Drawing.Point(513, 7);
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
            this.ctlSpinEdit1.Size = new System.Drawing.Size(112, 20);
            this.ctlSpinEdit1.TabIndex = 7;
            this.ctlSpinEdit1.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // styleGrid1
            // 
            this.styleGrid1.AlternatingColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(243)))), ((int)(((byte)(213)))));
            this.styleGrid1.BorderColor = System.Drawing.Color.Goldenrod;
            this.styleGrid1.BorderHotColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.styleGrid1.CaptionBackColor = System.Drawing.Color.DarkGoldenrod;
            this.styleGrid1.CaptionFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleGrid1.CaptionForeColor = System.Drawing.Color.Ivory;
            this.styleGrid1.CaptionLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(237)))), ((int)(((byte)(176)))));
            this.styleGrid1.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(252)))), ((int)(((byte)(231)))));
            this.styleGrid1.HeaderForeColor = System.Drawing.Color.Black;
            this.styleGrid1.StylePlan = mControl.WinCtl.Controls.Styles.Goldenrod;
            this.styleGrid1.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(309, 55);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(64, 24);
            this.button3.TabIndex = 10;
            this.button3.Text = "accept";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(15, 79);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(64, 23);
            this.button4.TabIndex = 11;
            this.button4.Text = "is new";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // checkIsNew
            // 
            this.checkIsNew.AutoSize = true;
            this.checkIsNew.Location = new System.Drawing.Point(109, 84);
            this.checkIsNew.Name = "checkIsNew";
            this.checkIsNew.Size = new System.Drawing.Size(56, 17);
            this.checkIsNew.TabIndex = 12;
            this.checkIsNew.Text = "is new";
            this.checkIsNew.UseVisualStyleBackColor = true;
            // 
            // textRowState
            // 
            this.textRowState.Location = new System.Drawing.Point(513, 110);
            this.textRowState.Name = "textRowState";
            this.textRowState.Size = new System.Drawing.Size(112, 20);
            this.textRowState.TabIndex = 13;
            // 
            // btnRowState
            // 
            this.btnRowState.Location = new System.Drawing.Point(309, 84);
            this.btnRowState.Name = "btnRowState";
            this.btnRowState.Size = new System.Drawing.Size(64, 24);
            this.btnRowState.TabIndex = 14;
            this.btnRowState.Text = "Row state";
            this.btnRowState.Click += new System.EventHandler(this.btnRowState_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.button11);
            this.panel1.Controls.Add(this.button10);
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.ctlComboBox1);
            this.panel1.Controls.Add(this.btnRowState);
            this.panel1.Controls.Add(this.ctlSpinEdit1);
            this.panel1.Controls.Add(this.checkIsNew);
            this.panel1.Controls.Add(this.textRowState);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.ctlSpinEdit2);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.checkBox2);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Location = new System.Drawing.Point(32, 297);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(628, 137);
            this.panel1.TabIndex = 15;
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(394, 85);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(75, 23);
            this.button11.TabIndex = 17;
            this.button11.Text = "ReBind";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(394, 55);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(75, 23);
            this.button10.TabIndex = 16;
            this.button10.Text = "DataGrid";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(394, 8);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 15;
            this.button5.Text = "Find";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            this.dataSet1.Tables.AddRange(new System.Data.DataTable[] {
            this.dataTable1,
            this.dataTable2,
            this.dataTable3});
            // 
            // dataTable1
            // 
            this.dataTable1.TableName = "Table1";
            // 
            // dataTable2
            // 
            this.dataTable2.TableName = "Table2";
            // 
            // dataTable3
            // 
            this.dataTable3.TableName = "Table3";
            // 
            // styleEdit1
            // 
            this.styleEdit1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(106)))), ((int)(((byte)(197)))));
            this.styleEdit1.BorderHotColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(80)))), ((int)(((byte)(184)))));
            this.styleEdit1.CaptionFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleEdit1.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(78)))), ((int)(((byte)(152)))));
            this.styleEdit1.StylePlan = mControl.WinCtl.Controls.Styles.Desktop;
            this.styleEdit1.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(31, 261);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(64, 24);
            this.button6.TabIndex = 16;
            this.button6.Text = "Current";
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(101, 261);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(64, 24);
            this.button7.TabIndex = 17;
            this.button7.Text = "Form2";
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(410, 266);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(47, 17);
            this.checkBox3.TabIndex = 18;
            this.checkBox3.Text = "Spin";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(475, 267);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 17);
            this.progressBar1.TabIndex = 19;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(581, 262);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 20;
            this.button8.Text = "button8";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(193, 262);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(64, 24);
            this.button9.TabIndex = 21;
            this.button9.Text = "Filter";
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // textFilter
            // 
            this.textFilter.Location = new System.Drawing.Point(263, 262);
            this.textFilter.Name = "textFilter";
            this.textFilter.Size = new System.Drawing.Size(112, 20);
            this.textFilter.TabIndex = 22;
            this.textFilter.Text = "textBox2";
            // 
            // gridStatusBar1
            // 
            this.gridStatusBar1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gridStatusBar1.ControlLayout = mControl.WinCtl.Controls.ControlLayout.Visual;
            this.gridStatusBar1.Dock = System.Windows.Forms.DockStyle.None;
            this.gridStatusBar1.Grid = this.grid1;
            this.gridStatusBar1.GridColumns.AddRange(new mControl.GridView.GridStatusPanel[] {
            this.gridStatusPanel1,
            this.gridStatusPanel2});
            //this.gridStatusBar1.HScrollPosition = 0;
            this.gridStatusBar1.Location = new System.Drawing.Point(32, 222);
            this.gridStatusBar1.Name = "gridStatusBar1";
            this.gridStatusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.statusBarPanel1,
            this.statusBarPanel2});
            this.gridStatusBar1.ProgressValue = 0;
            this.gridStatusBar1.ShowPanels = true;
            this.gridStatusBar1.Size = new System.Drawing.Size(628, 24);
            this.gridStatusBar1.StartPanelPosition = 35;
            this.gridStatusBar1.TabIndex = 23;
            // 
            // grid1
            // 
            this.grid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grid1.BackColor = System.Drawing.Color.White;
            this.grid1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.grid1.CaptionFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grid1.CaptionText = "test";
            this.grid1.CaptionVisible = false;
            this.grid1.Columns.AddRange(new mControl.GridView.GridColumnStyle[] {
            this.gridControlColumn1,
            this.gridTextColumn1,
            this.gridBoolColumn1,
            this.gridComboColumn1,
            this.gridDateColumn1,
            this.gridProgressColumn1,
            this.gridMultiColumn1,
            this.gridButtonColumn1,
            this.gridNumericColumn1});
            this.grid1.DataMember = "";
            this.grid1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.grid1.ForeColor = System.Drawing.Color.Black;
            this.grid1.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.grid1.Location = new System.Drawing.Point(32, 13);
            this.grid1.Name = "grid1";
            this.grid1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grid1.SelectionType = mControl.GridView.SelectionType.Tab;
            this.grid1.Size = new System.Drawing.Size(628, 203);
            this.grid1.TabIndex = 1;
            this.grid1.DirtyRowChanged += new System.EventHandler(this.grid1_DirtyRowChanged);
            this.grid1.RowDeleted += new System.EventHandler(this.grid1_RowDeleted);
            this.grid1.NewRowCompleted += new System.EventHandler(this.grid1_NewRowComplited);
            this.grid1.NewRowAdded += new System.EventHandler(this.grid1_NewRowAdded);
            this.grid1.DirtyChanged += new System.EventHandler(this.grid1_DirtyChanged);
            this.grid1.CurrentRowChanged += new System.EventHandler(this.grid1_CurrentRowChanged);
            this.grid1.CurrentRowChanging += new System.ComponentModel.CancelEventHandler(this.grid1_CurrentRowChanging);
            this.grid1.RowDeleting += new System.ComponentModel.CancelEventHandler(this.grid1_RowDeleting);
            // 
            // gridControlColumn1
            // 
            this.gridControlColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.gridControlColumn1.AllowNull = false;
            this.gridControlColumn1.Format = "";
            this.gridControlColumn1.HeaderText = "Grid";
            this.gridControlColumn1.MappingName = "0";
            this.gridControlColumn1.Text = "";
            this.gridControlColumn1.VisibleRows = 20;
            this.gridControlColumn1.Width = 50;
            // 
            // gridTextColumn1
            // 
            this.gridTextColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.gridTextColumn1.AllowNull = false;
            this.gridTextColumn1.AutoAdjust = true;
            this.gridTextColumn1.Format = "";
            this.gridTextColumn1.HeaderText = "Text";
            this.gridTextColumn1.MappingName = "Txt";
            this.gridTextColumn1.Width = 60;
            // 
            // gridBoolColumn1
            // 
            this.gridBoolColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.gridBoolColumn1.AllowNull = false;
            this.gridBoolColumn1.HeaderText = "Bool";
            //this.gridBoolColumn1.IsBound = false;
            this.gridBoolColumn1.MappingName = "Bool";
            this.gridBoolColumn1.NullValue = ((object)(resources.GetObject("gridBoolColumn1.NullValue")));
            this.gridBoolColumn1.Text = "";
            this.gridBoolColumn1.Width = 50;
            // 
            // gridComboColumn1
            // 
            this.gridComboColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.gridComboColumn1.AllowNull = false;
            this.gridComboColumn1.Format = "";
            this.gridComboColumn1.HeaderText = "Combo";
            this.gridComboColumn1.Items.AddRange(new object[] {
            "Item1",
            "Item2",
            "Item3",
            "Item4",
            "Item5"});
            this.gridComboColumn1.MappingName = "Combo";
            this.gridComboColumn1.Width = 81;
            // 
            // gridDateColumn1
            // 
            this.gridDateColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.gridDateColumn1.AllowNull = false;
            this.gridDateColumn1.Format = "";
            this.gridDateColumn1.HeaderText = "date";
            this.gridDateColumn1.MappingName = "Date";
            this.gridDateColumn1.MaxValue = new System.DateTime(2999, 12, 31, 0, 0, 0, 0);
            this.gridDateColumn1.MinValue = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.gridDateColumn1.UseMask = false;
            this.gridDateColumn1.Width = 81;
            // 
            // gridProgressColumn1
            // 
            this.gridProgressColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.gridProgressColumn1.AllowNull = false;
            this.gridProgressColumn1.HeaderText = "Progress";
            this.gridProgressColumn1.MappingName = "Progress";
            this.gridProgressColumn1.NullText = "0";
            this.gridProgressColumn1.Text = "";
            this.gridProgressColumn1.Width = 81;
            // 
            // gridMultiColumn1
            // 
            this.gridMultiColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.gridMultiColumn1.AllowNull = false;
            this.gridMultiColumn1.AutoAdjust = true;
            this.gridMultiColumn1.Format = "";
            this.gridMultiColumn1.HeaderText = "Multi";
            this.gridMultiColumn1.MappingName = "Multi";
            this.gridMultiColumn1.MultiType = mControl.WinCtl.Controls.MultiComboTypes.Boolean;
            this.gridMultiColumn1.Width = 0;
            // 
            // gridButtonColumn1
            // 
            this.gridButtonColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.gridButtonColumn1.AllowNull = false;
            this.gridButtonColumn1.HeaderText = "Button";
            this.gridButtonColumn1.ImageList = null;
            this.gridButtonColumn1.IsBound = false;
            this.gridButtonColumn1.MappingName = "Button";
            this.gridButtonColumn1.Text = "";
            this.gridButtonColumn1.Width = 40;
            // 
            // gridNumericColumn1
            // 
            this.gridNumericColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.gridNumericColumn1.DecimalPlaces = 0;
            this.gridNumericColumn1.Format = "";
            this.gridNumericColumn1.HeaderText = "Numeric";
            this.gridNumericColumn1.MappingName = "Icon";
            this.gridNumericColumn1.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.gridNumericColumn1.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.gridNumericColumn1.NullText = "0";
            this.gridNumericColumn1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.gridNumericColumn1.Width = 81;
            // 
            // gridStatusPanel1
            // 
            this.gridStatusPanel1.AggregateMode = mControl.GridView.AggregateMode.Sum;
            this.gridStatusPanel1.Column = this.gridNumericColumn1;
            this.gridStatusPanel1.DecimalPlaces = 0;
            this.gridStatusPanel1.Format = "N";
            this.gridStatusPanel1.HeaderText = "Numeric";
            this.gridStatusPanel1.PanelName = "";
            this.gridStatusPanel1.Width = 75;
            // 
            // gridStatusPanel2
            // 
            this.gridStatusPanel2.AggregateMode = mControl.GridView.AggregateMode.Sum;
            this.gridStatusPanel2.Column = this.gridProgressColumn1;
            this.gridStatusPanel2.DecimalPlaces = 0;
            this.gridStatusPanel2.Format = "N";
            this.gridStatusPanel2.HeaderText = "Progress";
            this.gridStatusPanel2.PanelName = "";
            this.gridStatusPanel2.Width = 75;
            // 
            // statusBarPanel1
            // 
            this.statusBarPanel1.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.statusBarPanel1.Name = "statusBarPanel1";
            this.statusBarPanel1.Tag = "Icon";
            this.statusBarPanel1.Width = 81;
            // 
            // statusBarPanel2
            // 
            this.statusBarPanel2.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.statusBarPanel2.Name = "statusBarPanel2";
            this.statusBarPanel2.Tag = "Progress";
            this.statusBarPanel2.Width = 81;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(684, 446);
            this.Controls.Add(this.gridStatusBar1);
            this.Controls.Add(this.textFilter);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.grid1);
            this.Name = "Form1";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Text = "Form3";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}


		public System.Data.DataSet DS=new System.Data.DataSet();

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad (e);
            BindData();
            SetStatusBar();
		}

        private void BindData()
        {
            //this.gridControlColumn1.MappingName="tbl2";
            //this.gridControlColumn1.DataSource=DataSource.CreateDataTable("tbl2",10);
            //this.label1.MappingName="tbl";
            DS.Relations.Clear();
            DS.Tables.Clear();

            System.Data.DataTable dt1 = DataSource.CreateDataTable("Tbl1", 10, 1);
            System.Data.DataTable dt2 = DataSource.CreateDataRelation("Tbl2", 10, 6);
            System.Data.DataTable dt3 = DataSource.CreateDataRelation("Tbl3", 10, 5);
            DS.Tables.AddRange(new System.Data.DataTable[] { dt1, dt2, dt3 });

            //DataRelation rel1 =
            //DS.Relations.Add("rel1",
            //DS.Tables["Tbl1"].Columns["Icon"],
            //DS.Tables["Tbl2"].Columns["Id"], false);

            //DataRelation rel2 =
            //DS.Relations.Add("rel2",
            //DS.Tables["Tbl1"].Columns["Icon"],
            //DS.Tables["Tbl3"].Columns["Id"], false);


            //DS.Tables.Add(dt1);

            //gridControlColumn1.DataMember = "Tbl2";
            //gridControlColumn1.DataSource = DS;
            //gridControlColumn1.ForeignKey = "Icon";
            //gridControlColumn1.CaptionVisible = true;


            //this.grid1.BeginInit();
            //this.grid1.DataMember = "";// "Tbl1";
            this.grid1.DataSource = DS.Tables["Tbl1"];// DS;
            //this.grid1.EndInit();

            //this.grid1.MappingName = "Tbl1";
            //this.grid1.DataSource = dt1;

        }

		private void button1_Click(object sender, System.EventArgs e)
		{
			//this.grid1.DataMember=this.textBox1.Text;
            this.grid1.AcceptChanges();
       
		}

        private void grid1_CurrentRowChanging(object sender, CancelEventArgs e)
        {
            if (this.grid1.DirtyRow)
            {
                DialogResult dr = MessageBox.Show("Cancel ?", "", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                    e.Cancel = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.grid1.RejectChanges();

  
        }

        private void grid1_DirtyChanged(object sender, EventArgs e)
        {
            this.checkBox1.Checked = this.grid1.Dirty;

        }

        private void grid1_DirtyRowChanged(object sender, EventArgs e)
        {
            this.checkBox2.Checked = this.grid1.DirtyRow;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.Show();
        }

        private void grid1_NewRowAdded(object sender, EventArgs e)
        {
            this.checkIsNew.Checked = this.grid1.IsNewRow;
        }

        private void grid1_RowDeleted(object sender, EventArgs e)
        {
            //string s = "new row";
 
        }

        private void grid1_RowDeleting(object sender, CancelEventArgs e)
        {
            //string s = "new row adding";
            //e.Cancel = true;

        }

        private void grid1_NewRowComplited(object sender, EventArgs e)
        {
            this.checkIsNew.Checked = this.grid1.IsNewRow;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this.grid1.IsNewRow.ToString());
        }

        private void grid1_CurrentRowChanged(object sender, EventArgs e)
        {
            this.textRowState.Text = grid1.GridRowState.ToString();
        }

        private void btnRowState_Click(object sender, EventArgs e)
        {
            this.textRowState.Text = grid1.GridRowState.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
           GridPerform.Find(grid1);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            GridPerform.Summarize(grid1);
            //this.grid1.PerformCurrentRow();
            //this.grid1[1, 1] = "0";
            //this.grid1.RefreshCurrent();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.Show();
        }

        private int count=0;

        private void button8_Click(object sender, EventArgs e)
        {
            if (this.checkBox3.Checked)
            {
                this.timer1.Interval = 100;
                this.timer1.Enabled = true;
                this.timer1.Start();
            }
            else
            {
                this.timer1.Stop();
                this.timer1.Enabled=false;
            }
       }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.checkBox3.Checked)
            {
                this.progressBar1.Value = count;
                count++;
                if (count >= 100)
                {
                    count = 0;
                    BindData();
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //this.grid1.RowFilter = this.textFilter.Text;
            //this.gridStatusBar1.SummarizeColumns();
            //this.grid1.DeleteRow(3);

            GridPerform.Filter(this.grid1);
           
            //GridChartDlg.Open(grid1, "chart");

            //grid1.PerformGridContextMenu();
            //grid1.PerformColumnContextMenu();
           
        }

        private void SetStatusBar()
        {
            this.gridStatusBar1.InitilaizeColumns = true;// SummarizeAllColumnsSetting();
            //this.gridStatusBar1.AddPanel(this.gridNumericColumn1, mControl.GridView.AggregateMode.Sum);

        }

        private void button10_Click(object sender, EventArgs e)
        {
            Form5 f = new Form5();
            f.Show();
        }

        private void ReBindData()
        {
            //this.gridControlColumn1.MappingName="tbl2";
            //this.gridControlColumn1.DataSource=DataSource.CreateDataTable("tbl2",10);
            //this.label1.MappingName="tbl";
            DS.Relations.Clear();
            DS.Tables.Clear();

            System.Data.DataTable dt1 = DataSource.CreateDataTable("Tbl1", 20, 1);
            System.Data.DataTable dt2 = DataSource.CreateDataTable("Tbl2", 20, 12);
            System.Data.DataTable dt3 = DataSource.CreateDataTable("Tbl3", 20, 1);
            DS.Tables.AddRange(new System.Data.DataTable[] { dt1, dt2, dt3 });

            DataRelation rel1 =
            DS.Relations.Add("rel1",
            DS.Tables["Tbl1"].Columns["Icon"],
            DS.Tables["Tbl2"].Columns["Icon"], false);

            DataRelation rel2 =
            DS.Relations.Add("rel2",
            DS.Tables["Tbl1"].Columns["Icon"],
            DS.Tables["Tbl3"].Columns["Icon"], false);


            //DS.Tables.Add(dt1);

            //gridControlColumn1.DataMember = "Tbl2";
            //gridControlColumn1.DataSource = DS;
            //gridControlColumn1.ForeignKey = "Icon";
            //gridControlColumn1.CaptionVisible = true;


            //this.grid1.BeginInit();
            //this.grid1.DataMember = "Tbl1";
            this.grid1.ReBinding(DS.Tables["Tbl1"]);// DS;
            //this.grid1.EndInit();

            //this.grid1.MappingName = "Tbl1";
            //this.grid1.DataSource = dt1;

        }

        private void button11_Click(object sender, EventArgs e)
        {
            ReBindData();
        }

	}
}
