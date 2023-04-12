using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using mControl.Util;
using System.Threading;
using mControl.Threading;
using mControl.Data;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Windows.Forms.Design;
using System.Globalization;

namespace mControl.GridView.Develop
{
    partial class GridColumnsAddDlg
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
            ComponentResourceManager manager = new ComponentResourceManager(typeof(GridColumnsAddDlg));
            this.dataBoundColumnRadioButton = new RadioButton();
            this.overarchingTableLayoutPanel = new TableLayoutPanel();
            this.checkBoxesTableLayoutPanel = new TableLayoutPanel();
            this.frozenCheckBox = new CheckBox();
            this.visibleCheckBox = new CheckBox();
            this.readOnlyCheckBox = new CheckBox();
            this.okCancelTableLayoutPanel = new TableLayoutPanel();
            this.addButton = new Button();
            this.cancelButton = new Button();
            this.columnInDataSourceLabel = new Label();
            this.dataColumns = new ListBox();
            this.unboundColumnRadioButton = new RadioButton();
            this.nameLabel = new Label();
            this.nameTextBox = new TextBox();
            this.typeLabel = new Label();
            this.columnTypesCombo = new ComboBox();
            this.headerTextLabel = new Label();
            this.headerTextBox = new TextBox();
            this.overarchingTableLayoutPanel.SuspendLayout();
            this.checkBoxesTableLayoutPanel.SuspendLayout();
            this.okCancelTableLayoutPanel.SuspendLayout();
            base.SuspendLayout();
            //manager.ApplyResources(this.dataBoundColumnRadioButton, "dataBoundColumnRadioButton");
            this.dataBoundColumnRadioButton.Checked = true;
            this.overarchingTableLayoutPanel.SetColumnSpan(this.dataBoundColumnRadioButton, 2);
            this.dataBoundColumnRadioButton.Margin = new Padding(0, 0, 0, 3);
            this.dataBoundColumnRadioButton.Name = "dataBoundColumnRadioButton";
            this.dataBoundColumnRadioButton.CheckedChanged += new EventHandler(this.dataBoundColumnRadioButton_CheckedChanged);
            //manager.ApplyResources(this.overarchingTableLayoutPanel, "overarchingTableLayoutPanel");
            this.overarchingTableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.overarchingTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            this.overarchingTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250f));
            this.overarchingTableLayoutPanel.Controls.Add(this.checkBoxesTableLayoutPanel, 0, 10);
            this.overarchingTableLayoutPanel.Controls.Add(this.okCancelTableLayoutPanel, 1, 11);
            this.overarchingTableLayoutPanel.Controls.Add(this.dataBoundColumnRadioButton, 0, 0);
            this.overarchingTableLayoutPanel.Controls.Add(this.columnInDataSourceLabel, 0, 1);
            this.overarchingTableLayoutPanel.Controls.Add(this.dataColumns, 0, 2);
            this.overarchingTableLayoutPanel.Controls.Add(this.unboundColumnRadioButton, 0, 3);
            this.overarchingTableLayoutPanel.Controls.Add(this.nameLabel, 0, 4);
            this.overarchingTableLayoutPanel.Controls.Add(this.nameTextBox, 1, 4);
            this.overarchingTableLayoutPanel.Controls.Add(this.typeLabel, 0, 6);
            this.overarchingTableLayoutPanel.Controls.Add(this.columnTypesCombo, 1, 6);
            this.overarchingTableLayoutPanel.Controls.Add(this.headerTextLabel, 0, 8);
            this.overarchingTableLayoutPanel.Controls.Add(this.headerTextBox, 1, 8);
            this.overarchingTableLayoutPanel.Margin = new Padding(12);
            this.overarchingTableLayoutPanel.Name = "overarchingTableLayoutPanel";
            this.overarchingTableLayoutPanel.RowStyles.Add(new RowStyle());
            this.overarchingTableLayoutPanel.RowStyles.Add(new RowStyle());
            this.overarchingTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            this.overarchingTableLayoutPanel.RowStyles.Add(new RowStyle());
            this.overarchingTableLayoutPanel.RowStyles.Add(new RowStyle());
            this.overarchingTableLayoutPanel.RowStyles.Add(new RowStyle());
            this.overarchingTableLayoutPanel.RowStyles.Add(new RowStyle());
            this.overarchingTableLayoutPanel.RowStyles.Add(new RowStyle());
            this.overarchingTableLayoutPanel.RowStyles.Add(new RowStyle());
            this.overarchingTableLayoutPanel.RowStyles.Add(new RowStyle());
            this.overarchingTableLayoutPanel.RowStyles.Add(new RowStyle());
            this.overarchingTableLayoutPanel.RowStyles.Add(new RowStyle());
            //manager.ApplyResources(this.checkBoxesTableLayoutPanel, "checkBoxesTableLayoutPanel");
            this.checkBoxesTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            this.checkBoxesTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            this.checkBoxesTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            this.checkBoxesTableLayoutPanel.Controls.Add(this.frozenCheckBox, 2, 0);
            this.checkBoxesTableLayoutPanel.Controls.Add(this.visibleCheckBox, 0, 0);
            this.checkBoxesTableLayoutPanel.Controls.Add(this.readOnlyCheckBox, 1, 0);
            this.checkBoxesTableLayoutPanel.Margin = new Padding(0, 3, 0, 6);
            this.overarchingTableLayoutPanel.SetColumnSpan(this.checkBoxesTableLayoutPanel, 2);
            this.checkBoxesTableLayoutPanel.Name = "checkBoxesTableLayoutPanel";
            this.checkBoxesTableLayoutPanel.RowStyles.Add(new RowStyle());
            //manager.ApplyResources(this.frozenCheckBox, "frozenCheckBox");
            this.frozenCheckBox.Margin = new Padding(3, 0, 0, 0);
            this.frozenCheckBox.Name = "frozenCheckBox";
            //manager.ApplyResources(this.visibleCheckBox, "visibleCheckBox");
            this.visibleCheckBox.Checked = true;
            this.visibleCheckBox.CheckState = CheckState.Checked;
            this.visibleCheckBox.Margin = new Padding(0, 0, 3, 0);
            this.visibleCheckBox.Name = "visibleCheckBox";
            //manager.ApplyResources(this.readOnlyCheckBox, "readOnlyCheckBox");
            this.readOnlyCheckBox.Margin = new Padding(3, 0, 3, 0);
            this.readOnlyCheckBox.Name = "readOnlyCheckBox";
            //manager.ApplyResources(this.okCancelTableLayoutPanel, "okCancelTableLayoutPanel");
            this.okCancelTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            this.okCancelTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            this.okCancelTableLayoutPanel.Controls.Add(this.addButton, 0, 0);
            this.okCancelTableLayoutPanel.Controls.Add(this.cancelButton, 1, 0);
            this.okCancelTableLayoutPanel.Margin = new Padding(0, 6, 0, 0);
            this.okCancelTableLayoutPanel.Name = "okCancelTableLayoutPanel";
            this.okCancelTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
            //manager.ApplyResources(this.addButton, "addButton");
            this.addButton.Margin = new Padding(0, 0, 3, 0);
            this.addButton.Name = "addButton";
            this.addButton.Click += new EventHandler(this.addButton_Click);
            //manager.ApplyResources(this.cancelButton, "cancelButton");
            this.cancelButton.Margin = new Padding(3, 0, 0, 0);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Click += new EventHandler(this.cancelButton_Click);
            //manager.ApplyResources(this.columnInDataSourceLabel, "columnInDataSourceLabel");
            this.overarchingTableLayoutPanel.SetColumnSpan(this.columnInDataSourceLabel, 2);
            this.columnInDataSourceLabel.Margin = new Padding(14, 3, 0, 0);
            this.columnInDataSourceLabel.Name = "columnInDataSourceLabel";
            //manager.ApplyResources(this.dataColumns, "dataColumns");
            this.overarchingTableLayoutPanel.SetColumnSpan(this.dataColumns, 2);
            this.dataColumns.FormattingEnabled = true;
            this.dataColumns.Margin = new Padding(14, 2, 0, 3);
            this.dataColumns.Name = "dataColumns";
            this.dataColumns.SelectedIndexChanged += new EventHandler(this.dataColumns_SelectedIndexChanged);
            //manager.ApplyResources(this.unboundColumnRadioButton, "unboundColumnRadioButton");
            this.overarchingTableLayoutPanel.SetColumnSpan(this.unboundColumnRadioButton, 2);
            this.unboundColumnRadioButton.Margin = new Padding(0, 6, 0, 3);
            this.unboundColumnRadioButton.Name = "unboundColumnRadioButton";
            this.unboundColumnRadioButton.CheckedChanged += new EventHandler(this.unboundColumnRadioButton_CheckedChanged);
            //manager.ApplyResources(this.nameLabel, "nameLabel");
            this.nameLabel.Margin = new Padding(14, 3, 2, 3);
            this.nameLabel.Name = "nameLabel";
            //manager.ApplyResources(this.nameTextBox, "nameTextBox");
            this.nameTextBox.Margin = new Padding(3, 3, 0, 3);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Validating += new CancelEventHandler(this.nameTextBox_Validating);
            //manager.ApplyResources(this.typeLabel, "typeLabel");
            this.typeLabel.Margin = new Padding(14, 3, 2, 3);
            this.typeLabel.Name = "typeLabel";
            //manager.ApplyResources(this.columnTypesCombo, "columnTypesCombo");
            this.columnTypesCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            this.columnTypesCombo.FormattingEnabled = true;
            this.columnTypesCombo.Margin = new Padding(3, 3, 0, 3);
            this.columnTypesCombo.Name = "columnTypesCombo";
            this.columnTypesCombo.Sorted = true;
            //manager.ApplyResources(this.headerTextLabel, "headerTextLabel");
            this.headerTextLabel.Margin = new Padding(14, 3, 2, 3);
            this.headerTextLabel.Name = "headerTextLabel";
            //manager.ApplyResources(this.headerTextBox, "headerTextBox");
            this.headerTextBox.Margin = new Padding(3, 3, 0, 3);
            this.headerTextBox.Name = "headerTextBox";
            //manager.ApplyResources(this, "$this");
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.cancelButton;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.Controls.Add(this.overarchingTableLayoutPanel);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.HelpButton = true;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "DataGridViewAddColumnDialog";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.HelpButtonClicked += new CancelEventHandler(this.DataGridViewAddColumnDialog_HelpButtonClicked);
            base.Closed += new EventHandler(this.DataGridViewAddColumnDialog_Closed);
            base.VisibleChanged += new EventHandler(this.DataGridViewAddColumnDialog_VisibleChanged);
            base.Load += new EventHandler(this.DataGridViewAddColumnDialog_Load);
            base.HelpRequested += new HelpEventHandler(this.DataGridViewAddColumnDialog_HelpRequested);
            this.overarchingTableLayoutPanel.ResumeLayout(false);
            this.overarchingTableLayoutPanel.PerformLayout();
            this.checkBoxesTableLayoutPanel.ResumeLayout(false);
            this.checkBoxesTableLayoutPanel.PerformLayout();
            this.okCancelTableLayoutPanel.ResumeLayout(false);
            this.okCancelTableLayoutPanel.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        #endregion

        // Fields
        private Button addButton;
        private Button cancelButton;
        private TableLayoutPanel checkBoxesTableLayoutPanel;
        private Label columnInDataSourceLabel;
        private static Type[] columnTypes = new Type[] { typeof(DataGridViewButtonColumn), typeof(DataGridViewCheckBoxColumn), typeof(DataGridViewComboBoxColumn), typeof(DataGridViewImageColumn), typeof(DataGridViewLinkColumn), typeof(DataGridViewTextBoxColumn) };
        private ComboBox columnTypesCombo;
        private RadioButton dataBoundColumnRadioButton;
        private ListBox dataColumns;
        private static Type dataGridViewColumnDesignTimeVisibleAttributeType = typeof(DataGridViewColumnDesignTimeVisibleAttribute);
        private DataGridViewColumnCollection dataGridViewColumns;
        private static Type dataGridViewColumnType = typeof(DataGridViewColumn);
        private CheckBox frozenCheckBox;
        private TextBox headerTextBox;
        private Label headerTextLabel;
        private static Type iComponentChangeServiceType = typeof(IComponentChangeService);
        private static Type iDesignerHostType = typeof(IDesignerHost);
        private static Type iDesignerType = typeof(IDesigner);
        private static Type iHelpServiceType = typeof(IHelpService);
        private static Type iNameCreationServiceType = typeof(INameCreationService);
        private int initialDataGridViewColumnsCount = -1;
        private int insertAtPosition = -1;
        private static Type iTypeDiscoveryServiceType = typeof(ITypeDiscoveryService);
        private static Type iTypeResolutionServiceType = typeof(ITypeResolutionService);
        private static Type iUIServiceType = typeof(IUIService);
        private DataGridView liveDataGridView;
        private Label nameLabel;
        private TextBox nameTextBox;
        private TableLayoutPanel okCancelTableLayoutPanel;
        private TableLayoutPanel overarchingTableLayoutPanel;
        private bool persistChangesToDesigner;
        private CheckBox readOnlyCheckBox;
        private Label typeLabel;
        private RadioButton unboundColumnRadioButton;
        private CheckBox visibleCheckBox;

    }
}