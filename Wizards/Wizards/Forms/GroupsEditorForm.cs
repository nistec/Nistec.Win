using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using MControl.WinForms;
using MControl.Wizards.Controls;
using MControl.Util;

namespace MControl.Wizards.Forms
{
	/// <summary>
    /// Summary description for GroupsEditorForm.
	/// </summary>
	public class GroupsEditorForm : McForm
	{
        private MControl.Wizards.McTabPanels wizTabPanels;
        private McTabPage ctlPageConfig;
        private McTabPage ctlPageGroups;
        private MControl.GridView.Grid gridConfig;
        private McGroupsEditor ctlListsEditor;
        private MControl.GridView.GridTextColumn CodeId;
        private MControl.GridView.GridTextColumn CodeName;
        private MControl.GridView.GridTextColumn GroupID;
        private MControl.GridView.GridBoolColumn State;
   		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        public GroupsEditorForm()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GroupsEditorForm));
            this.wizTabPanels = new MControl.Wizards.McTabPanels();
            this.ctlPageConfig = new MControl.WinForms.McTabPage();
            this.gridConfig = new MControl.GridView.Grid();
            this.CodeId = new MControl.GridView.GridTextColumn();
            this.CodeName = new MControl.GridView.GridTextColumn();
            this.GroupID = new MControl.GridView.GridTextColumn();
            this.State = new MControl.GridView.GridBoolColumn();
            this.ctlPageGroups = new MControl.WinForms.McTabPage();
            this.ctlListsEditor = new MControl.Wizards.Controls.McGroupsEditor();
            this.ctlPageConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridConfig)).BeginInit();
            this.ctlPageGroups.SuspendLayout();
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
            this.StyleGuideBase.FlatColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.FocusedColor = System.Drawing.Color.RoyalBlue;
            this.StyleGuideBase.FormColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.StylePlan = MControl.WinForms.Styles.Desktop;
            // 
            // wizTabPanels
            // 
            this.wizTabPanels.ButtonImageList = null;
            this.wizTabPanels.ButtonMenuStyle = MControl.WinForms.ButtonMenuStyles.Button;
            this.wizTabPanels.ButttonCancelText = "Cancel";
            this.wizTabPanels.ButttonHelpText = "Help";
            this.wizTabPanels.ButttonUpdateText = "OK";
            this.wizTabPanels.CaptionFont = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.wizTabPanels.CaptionImage = ((System.Drawing.Image)(resources.GetObject("wizTabPanels.CaptionImage")));
            this.wizTabPanels.CaptionSubText = null;
            this.wizTabPanels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizTabPanels.ListCaption = "";
            this.wizTabPanels.ListWidth = 150;
            this.wizTabPanels.Location = new System.Drawing.Point(2, 38);
            this.wizTabPanels.Name = "wizTabPanels";
            this.wizTabPanels.Padding = new System.Windows.Forms.Padding(2);
            this.wizTabPanels.SelectedIndex = 0;
            this.wizTabPanels.Size = new System.Drawing.Size(576, 318);
            this.wizTabPanels.TabIndex = 1;
            this.wizTabPanels.WizardPages.AddRange(new MControl.WinForms.McTabPage[] {
            this.ctlPageConfig,
            this.ctlPageGroups});
            // 
            // ctlPageConfig
            // 
            this.ctlPageConfig.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlPageConfig.Controls.Add(this.gridConfig);
            this.ctlPageConfig.Location = new System.Drawing.Point(4, 4);
            this.ctlPageConfig.Name = "ctlPageConfig";
            this.ctlPageConfig.Padding = new System.Windows.Forms.Padding(4);
            this.ctlPageConfig.Size = new System.Drawing.Size(411, 211);
            this.ctlPageConfig.Text = "Group Config";
            // 
            // gridConfig
            // 
            this.gridConfig.BackColor = System.Drawing.Color.White;
            this.gridConfig.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gridConfig.Columns.AddRange(new MControl.GridView.GridColumnStyle[] {
            this.CodeId,
            this.CodeName,
            this.GroupID,
            this.State});
            this.gridConfig.ControlLayout = MControl.WinForms.ControlLayout.System;
            this.gridConfig.DataMember = "";
            this.gridConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.gridConfig.ForeColor = System.Drawing.Color.Black;
            this.gridConfig.Location = new System.Drawing.Point(4, 4);
            this.gridConfig.MappingName = "AccountSort";
            this.gridConfig.Name = "gridConfig";
            this.gridConfig.Size = new System.Drawing.Size(403, 203);
            this.gridConfig.TabIndex = 0;
            // 
            // CodeId
            // 
            this.CodeId.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.CodeId.Format = "G";
            this.CodeId.FormatType = MControl.Formats.GeneralNumber;
            this.CodeId.HeaderText = "CodeId";
            this.CodeId.MappingName = "CodeID";
            this.CodeId.Width = 68;
            // 
            // CodeName
            // 
            this.CodeName.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.CodeName.Format = "";
            this.CodeName.HeaderText = "CodeName";
            this.CodeName.MappingName = "CodeName";
            this.CodeName.Width = 139;
            // 
            // GroupID
            // 
            this.GroupID.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.GroupID.Format = "G";
            this.GroupID.FormatType = MControl.Formats.GeneralNumber;
            this.GroupID.HeaderText = "GroupID";
            this.GroupID.MappingName = "GroupID";
            this.GroupID.Width = 70;
            // 
            // State
            // 
            this.State.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.State.HeaderText = "State";
            this.State.MappingName = "IsGroup";
            this.State.NullValue = null;
            this.State.Text = "";
            this.State.Width = 73;
            // 
            // ctlPageGroups
            // 
            this.ctlPageGroups.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlPageGroups.Controls.Add(this.ctlListsEditor);
            this.ctlPageGroups.Location = new System.Drawing.Point(4, 4);
            this.ctlPageGroups.Name = "ctlPageGroups";
            this.ctlPageGroups.Padding = new System.Windows.Forms.Padding(4);
            this.ctlPageGroups.Size = new System.Drawing.Size(411, 211);
            this.ctlPageGroups.Text = "Groups Editor";
            // 
            // ctlListsEditor
            // 
            this.ctlListsEditor.AccessibleDescription = "";
            this.ctlListsEditor.BindFormat = MControl.WinForms.BindingFormat.String;
            this.ctlListsEditor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctlListsEditor.DefaultValue = null;
            this.ctlListsEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlListsEditor.FixSize = false;
            this.ctlListsEditor.Location = new System.Drawing.Point(4, 4);
            this.ctlListsEditor.Name = "ctlListsEditor";
            this.ctlListsEditor.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ctlListsEditor.Size = new System.Drawing.Size(403, 203);
            this.ctlListsEditor.TabIndex = 0;
            this.ctlListsEditor.ButtonAllRightToLeft_Click += new System.EventHandler(this.ctlListsEditor_ButtonAllRightToLeft_Click);
            this.ctlListsEditor.ButtonAllLeftToRight_Click += new System.EventHandler(this.ctlListsEditor_ButtonAllLeftToRight_Click);
            this.ctlListsEditor.ButtonLeft_Click += new System.EventHandler(this.ctlListsEditor_ButtonLeft_Click);
            this.ctlListsEditor.ButtonLeftToRight_Click += new System.EventHandler(this.ctlListsEditor_ButtonLeftToRight_Click);
            this.ctlListsEditor.ButtonRightToLeft_Click += new System.EventHandler(this.ctlListsEditor_ButtonRightToLeft_Click);
            this.ctlListsEditor.ButtonRightClick += new System.EventHandler(this.ctlListsEditor_ButtonRightClick);
            // 
            // GroupsEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.CaptionVisible = true;
            this.ClientSize = new System.Drawing.Size(580, 358);
            this.ControlLayout = MControl.WinForms.ControlLayout.System;
            this.Controls.Add(this.wizTabPanels);
            this.Name = "GroupsEditorForm";
            this.Text = "Config Editor";
            this.Controls.SetChildIndex(this.wizTabPanels, 0);
            this.ctlPageConfig.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridConfig)).EndInit();
            this.ctlPageGroups.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

        //private DataTable dt1;
        //private DataTable dt2;
        //private DataView dv1;
        //private DataView dv2;
        //private bool page2Loaded = false;
        string[] selectedKeys;
        object selectedValue;

        protected override bool Initialize(object[] args)
        {
            return true;
        }

 
        #region properties

        public string LabelLeft
        {
            get{return this.ctlListsEditor.ctlLabelLeft.Text;}
            set{this.ctlListsEditor.ctlLabelLeft.Text=value;}
        }
        
        public string LabelRight
        {
            get{return this.ctlListsEditor.ctlLabelRight.Text;}
            set{this.ctlListsEditor.ctlLabelRight.Text=value;}
        } 
        
        public string ListValueMemberLeft
        {
            get{return ctlListsEditor.ctlListBoxLeft.ValueMember;}
            set{ctlListsEditor.ctlListBoxLeft.ValueMember=value;}
        }

        public string ListDisplayMemberLeft
        {
            get{return this.ctlListsEditor.ctlListBoxLeft.DisplayMember;}
            set{this.ctlListsEditor.ctlListBoxLeft.DisplayMember=value;}
        }

        public object ListDataSourceLeft
        {
            get { return this.ctlListsEditor.ctlListBoxLeft.DataSource; }
            set { this.ctlListsEditor.ctlListBoxLeft.DataSource = value; }
        }

        public string ListValueMemberRight
        {
            get{return this.ctlListsEditor.ctlListBoxRight.ValueMember;}
            set{this.ctlListsEditor.ctlListBoxRight.ValueMember=value;}
        }


        public string ListDisplayMemberRight
        {
            get{return this.ctlListsEditor.ctlListBoxRight.DisplayMember;}
            set{this.ctlListsEditor.ctlListBoxRight.DisplayMember=value;}
        }

        public object ListDataSourceRight
        {
            get { return this.ctlListsEditor.ctlListBoxRight.DataSource; }
            set { this.ctlListsEditor.ctlListBoxRight.DataSource = value; }
        }

        public string ComboValueMemberLeft
        {
            get { return ctlListsEditor.ctlComboLeft.ValueMember; }
            set { ctlListsEditor.ctlComboLeft.ValueMember = value; }
        }

        public string ComboDisplayMemberLeft
        {
            get { return this.ctlListsEditor.ctlComboLeft.DisplayMember; }
            set { this.ctlListsEditor.ctlComboLeft.DisplayMember = value; }
        }

        public object ComboDataSourceLeft
        {
            get { return this.ctlListsEditor.ctlComboLeft.DataSource; }
            set { this.ctlListsEditor.ctlComboLeft.DataSource = value; }
        }

        public string ComboValueMemberRight
        {
            get { return this.ctlListsEditor.ctlComboRight.ValueMember; }
            set { this.ctlListsEditor.ctlComboRight.ValueMember = value; }
        }


        public string ComboDisplayMemberRight
        {
            get { return this.ctlListsEditor.ctlComboRight.DisplayMember; }
            set { this.ctlListsEditor.ctlComboRight.DisplayMember = value; }
        }

        public object ComboDataSourceRight
        {
            get { return this.ctlListsEditor.ctlComboRight.DataSource; }
            set { this.ctlListsEditor.ctlComboRight.DataSource = value; }
        }



        public GridView.Grid GridConfig
        {
            get { return this.gridConfig; }
        }

        public Wizards.McTabPanels TabPanels
        {
            get { return this.wizTabPanels; }
        }


        public string[] SelectedKeys
        {
            get { return this.selectedKeys; }
        }

        public object SelectedValue
        {
            get { return this.selectedValue; }
        }
            

        #endregion

        public void SetDataSource(DataTable source,string mappingName)
        {
            gridConfig.MappingName = mappingName;
            gridConfig.DataSource = source;
        }


         private void ctlListsEditor_ButtonAllLeftToRight_Click(object sender, EventArgs e)
        {
            selectedKeys=null;
            selectedValue = null;
            object code = ctlListsEditor.ctlComboRight.SelectedValue;
            if (code == null)
                return;
            string accId = "";
            int i = 0;

            string[] keys = new string[this.ctlListsEditor.ctlListBoxLeft.Items.Count];
            foreach (object o in this.ctlListsEditor.ctlListBoxLeft.Items)
            {
                accId = (string)((DataRowView)o)[this.ctlListsEditor.ctlListBoxLeft.ValueMember];
                keys[i] = accId;
                i++;
            }

            selectedKeys = keys;
            selectedValue = code;
            //MoveRecords(code, keys);
        }

        private void ctlListsEditor_ButtonAllRightToLeft_Click(object sender, EventArgs e)
        {
            selectedKeys = null;
            selectedValue = null;
            object code = ctlListsEditor.ctlComboLeft.SelectedValue;
            if (code == null)
                return;
            string accId = "";
            int i = 0;
 
            string[] keys = new string[this.ctlListsEditor.ctlListBoxRight.Items.Count];
            foreach (object o in this.ctlListsEditor.ctlListBoxRight.Items)
            {
                accId = (string)((DataRowView)o)[this.ctlListsEditor.ctlListBoxRight.ValueMember];
                 keys[i] = accId;
                i++;
            }

            selectedKeys = keys;
            selectedValue = code;
            //MoveRecords(code, keys);
        }

        private void ctlListsEditor_ButtonLeft_Click(object sender, EventArgs e)
        {
        }

        private void ctlListsEditor_ButtonLeftToRight_Click(object sender, EventArgs e)
        {
            selectedKeys = null;
            selectedValue = null;
            object code = ctlListsEditor.ctlComboRight.SelectedValue;
            if (code == null)
                return;
            string accId = "";
            int i = 0;

            string[] keys = new string[this.ctlListsEditor.ctlListBoxLeft.SelectedItems.Count];
            foreach (object o in this.ctlListsEditor.ctlListBoxLeft.SelectedItems)
            {
                accId = (string)((DataRowView)o)[this.ctlListsEditor.ctlListBoxLeft.ValueMember];
                keys[i] = accId;
                i++;
            }

            selectedKeys = keys;
            selectedValue = code;
            //MoveRecords(code, keys);
        }

        private void ctlListsEditor_ButtonRightClick(object sender, EventArgs e)
        {
        }

        private void ctlListsEditor_ButtonRightToLeft_Click(object sender, EventArgs e)
        {
            selectedKeys = null;
            selectedValue = null;
            object code = ctlListsEditor.ctlComboLeft.SelectedValue;
            if (code == null)
                return;
            string accId = "";
            int i = 0;

            string[] keys = new string[this.ctlListsEditor.ctlListBoxRight.SelectedItems.Count];
            foreach (object o in this.ctlListsEditor.ctlListBoxRight.SelectedItems)
            {
                accId = (string)((DataRowView)o)[this.ctlListsEditor.ctlListBoxRight.ValueMember];
                keys[i] = accId;
                i++;
            }

            selectedKeys = keys;
            selectedValue = code;
            //MoveRecords(code, keys);
        }

        //private void ctlListsEditor_ComboLeft_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    object code = this.ctlListsEditor.ctlComboLeft.SelectedValue;
        //    if (code != null)
        //    {
        //        DataView dv = new DataView(dt1,"Code=" + code.ToString(),"Code",DataViewRowState.CurrentRows);
        //        this.ctlListsEditor.ctlListBoxLeft.DataSource = dv;// dtLeft;
        //    }
        //}

        //private void ctlListsEditor_ComboRight_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    object code = this.ctlListsEditor.ctlComboRight.SelectedValue;
        //    if (code != null)
        //    {
        //        DataView dv = new DataView(dt1, "Code=" + code.ToString(), "Code", DataViewRowState.CurrentRows);
        //        this.ctlListsEditor.ctlListBoxRight.DataSource = dv;// dtRight;
        //    }
        //}

        //private void MoveRecords(object code, string[] keys)
        //{
        //    int i = 0;

        //    foreach (string s in keys)
        //    {
        //        i = dv2.Find(s);
        //        if (i > -1)
        //            dv2[i]["Code"] = code;
        //    }

        //    MControl.Data.IDBCmd cmd = null;// MControl.Data.DBUtil.Create(Dal.DB.Connection);
        //    cmd.UpdateChanges(dv2.Table, "Accounts");
        //    dt1.AcceptChanges();
 
        //}

        //private void wizTabPanels_SelectionItemChanged(object sender, EventArgs e)
        //{
        //    this.SuspendLayout();
        //    if (this.wizTabPanels.SelectedIndex == 1)
        //    {
        //        if (!page2Loaded)
        //            this.InitPage2();
        //    }
        //    this.wizTabPanels.ShowBottom = this.wizTabPanels.SelectedIndex == 0;
        //    this.ResumeLayout(false);
        //}

        //private void gridConfig_CurrentRowChanging(object sender, CancelEventArgs e)
        //{
        //    if (!this.gridConfig.Dirty)
        //        return;

        //    int code = 0;
        //    string codename = "";
        //    int group = 0;
        //    bool isgroup = false;
        //    try
        //    {
        //        code = int.Parse(this.gridConfig[0].ToString());
        //        codename = this.gridConfig[1].ToString();
        //        group = int.Parse(this.gridConfig[2].ToString());
        //        isgroup = bool.Parse(this.gridConfig[3].ToString());
        //    }
        //    catch
        //    {
        //        RM.ShowError(RM.ErrorDataType);
        //        e.Cancel = true;
        //    }
        //    bool ok = false;// RuleUtil.CheckEmpty("CodeName", codename);
        //    //if (ok)
        //    //{
        //    //    ok = RuleUtil.CheckDuplicate(this.gridConfig.DataList, new string[] { "CodeID", "CodeName" }, new object[] { code, codename });
        //    //}
        //    //if (ok && isgroup)
        //    //{
        //    //    ok = RuleUtil.CheckDuplicate(this.gridConfig.DataList.Table, "IsGroup=1", "GroupID", group);
        //    //}
        //    //if (ok)
        //    //{
        //    //    MControl.Data.IDBCmd cmd= MControl.Data.DBUtil.Create(Dal.DB.Connection);
        //    //    cmd.UpdateChanges(this.gridConfig.DataList.Table);
        //    //    this.gridConfig.AcceptChanges();
        //    //}
        //    e.Cancel = !ok;
        //}
        //private DataView GetAccountList(object codeID)
        //{
        //    int code = (int)MControl.Util.Types.StringToInt(codeID.ToString(), 0);
        //    DataTable dt = null;// Dal.DB.Accoounts.Accounts(code);
        //    dt.TableName = "Accounts";

        //    return dt.DefaultView;
        //}

        //private void InitPage2()
        //{
        //    this.ctlListsEditor.ctlLabelLeft.Text = "קוד מיון";
        //    this.ctlListsEditor.ctlLabelRight.Text = "קוד מיון";
        //    this.ctlListsEditor.ctlListBoxRight.ValueMember = "AccountID";
        //    this.ctlListsEditor.ctlListBoxRight.DisplayMember = "AccountName";
        //    this.ctlListsEditor.ctlListBoxLeft.ValueMember = "AccountID";
        //    this.ctlListsEditor.ctlListBoxLeft.DisplayMember = "AccountName";

        //    //ListConfig.AccountSort(this.ctlListsEditor.ctlComboLeft);
        //    //ListConfig.AccountSort(this.ctlListsEditor.ctlComboRight);

        //    dt1 = null;// Dal.DB.Accoounts.Accounts();
        //    dt1.TableName = "Accounts";

        //    dv1 = dt1.DefaultView;
        //    dv1.Sort = "Code";

        //    dv2 = dt1.DefaultView;
        //    dv2.Sort = "AccountID";
        //    page2Loaded = true;

        //}
 
    }
}

