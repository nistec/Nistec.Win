using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using MControl.WinForms;
using MControl.Util;
using MControl.Wizards;

namespace MControl.Wizards.Forms
{
	/// <summary>
	/// Summary description for frmListDialog.
	/// </summary>
    public class LookupForm : MControl.WinForms.McForm
	{
		#region Ctor

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
        private MControl.WinForms.McLookUpList ctlList;
		private MControl.WinForms.McButton btnCancel;
		private MControl.WinForms.McButton btnOK;
        private System.Windows.Forms.Label lblSelected;

        public LookupForm()
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

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated (e);
			//this.txtFind.Focus();
		}

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.ctlList = new MControl.WinForms.McLookUpList();
            this.btnCancel = new MControl.WinForms.McButton();
            this.btnOK = new MControl.WinForms.McButton();
            this.lblSelected = new System.Windows.Forms.Label();
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
            // ctlList
            // 
            this.ctlList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ctlList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlList.ColumnDisplay = "";
            this.ctlList.ColumnSpacing = 3;
            this.ctlList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ctlList.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlList.IntegralHeight = false;
            this.ctlList.Location = new System.Drawing.Point(8, 58);
            this.ctlList.Name = "ctlList";
            this.ctlList.ReadOnly = false;
            this.ctlList.SelectionMode = System.Windows.Forms.SelectionMode.One;
            this.ctlList.ShowLookupPanel = true;
            this.ctlList.Size = new System.Drawing.Size(267, 241);
            this.ctlList.StylePainter = this.StyleGuideBase;
            this.ctlList.TabIndex = 2;
            this.ctlList.TabStop = false;
            this.ctlList.DoubleClick += new System.EventHandler(this.ctlList_DoubleClick);
            this.ctlList.SelectedIndexChanged += new System.EventHandler(this.ctlList_SelectedIndexChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(78, 307);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(64, 24);
            this.btnCancel.StylePainter = this.StyleGuideBase;
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.ToolTipText = "ביטול";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnOK.Location = new System.Drawing.Point(8, 307);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(64, 24);
            this.btnOK.StylePainter = this.StyleGuideBase;
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "Ok";
            this.btnOK.ToolTipText = "אישור";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblSelected
            // 
            this.lblSelected.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSelected.BackColor = System.Drawing.Color.Transparent;
            this.lblSelected.Location = new System.Drawing.Point(152, 307);
            this.lblSelected.Name = "lblSelected";
            this.lblSelected.Size = new System.Drawing.Size(123, 24);
            this.lblSelected.TabIndex = 8;
            // 
            // LookupForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.CancelButton = this.btnCancel;
            this.CaptionVisible = true;
            this.ClientSize = new System.Drawing.Size(283, 336);
            this.Controls.Add(this.lblSelected);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.ctlList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LookupForm";
            this.Text = "LookupForm";
            this.Controls.SetChildIndex(this.ctlList, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.lblSelected, 0);
            this.ResumeLayout(false);

		}
		#endregion

		#region members
		protected bool initData=false;
		protected string selectedFilter="";
		protected string fieldValue;
		protected string fieldView;
		protected string selectedValue;
		protected string selectedView;
		protected object[] FindLookup;
		protected DataView FiltersView;
		protected int FilterColumnIndex;
		//protected string FilterColumnName;
		protected int FindIndex;
		protected object dlgResult=null;

		#endregion

		#region public properties

		public string SelectedValue
		{
			get{return this.selectedValue;}
		}

		public string SelectedView
		{
			get{return this.selectedView;}
		}

		public McLookUpList List
		{
			get{return this.ctlList;}
		}

        //public McComboBox Fields
        //{
        //    get{return this.ctlField;}
        //}
		
        //public McComboBox FilterType
        //{
        //    get{return this.ctlFilterType;}
        //}

        //public McTextBox FindText
        //{
        //    get{return this.txtFind;}
        //}

		#endregion

		#region methods

		protected virtual void FindNext()
		{
			if(this.FindLookup==null)return;

			if(this.FindIndex<this.FindLookup.Length)
			{
				object id=this.FindLookup[this.FindIndex];
				if(id==null)
				{
					RML.ShowNotifyBoxMsg("ValueNotFound");
					goto Label_01;
				}
				if(this.ctlList.DataSource.Sort!= fieldValue)
				{
					this.ctlList.DataSource.Sort= fieldValue;
				}
				int row=this.ctlList.DataSource.Find(id);
				this.ctlList.SelectedIndex=row;
			}
			else
			{
				FindIndex=0;
				RML.ShowNotifyBoxMsg("ValueNotFound");
				return;			
			}
			Label_01:
				FindIndex++;
		}

		protected virtual void UpdateSelected()
		{
			if(this.ctlList.SelectedIndex==-1)return;

			DataRowView drv=this.ctlList.DataSource[this.ctlList.SelectedIndex];

			this.selectedValue=(string)drv[0];//this.ctlList.DataSource[.Table.r[0];
			this.selectedView=(string)drv[1];//this.ctlList[1];

			this.lblSelected.Text=string.Format("{0}:{1}", selectedValue,selectedView);

		}

		public  bool Gof3()
		{
			//this.OnFind_Click(EventArgs.Empty);
			return true;
		}

		public  bool GoSave()
		{
			this.OnOK_Click(EventArgs.Empty);
			return true;
		}

		protected virtual void ReBuildLookupList(string field,string Where)
		{
			//
		}

		protected virtual void FilterLookupList(string filter)
		{
			//
		}
		#endregion

		#region virtual 

        //private void ctlField_SelectedIndexChanged(object sender, System.EventArgs e)
        //{
        //    OnField_SelectedIndexChanged(e);
        //}

        //private void btnFind_Click(object sender, System.EventArgs e)
        //{
        //    OnFind_Click(e);
        //}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			OnOK_Click(e);
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			OnCancel_Click(e);
		}

        //private  void txtFind_TextChanged(object sender, System.EventArgs e)
        //{
        //    OnFind_TextChanged(e);
        //}

		private void ctlList_SelectedIndexChanged(object sender, EventArgs e)
		{
			OnList_SelectedIndexChanged(e);
		}
		private void ctlList_DoubleClick(object sender, EventArgs e)
		{
			OnList_DoubleClick(e);
		}

        //private void txtFind_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        //{
        //    OnFind_KeyDown(e);
        //}

        //private void ctlFilterType_SelectedIndexChanged(object sender, System.EventArgs e)
        //{
        //    OnFilterType_SelectedIndexChanged(e);
        //}

        //protected virtual void OnField_SelectedIndexChanged(System.EventArgs e)
        //{
        //    if(this.initData)
        //    {
        //        ReBuildLookupList(this.ctlField.SelectedValue.ToString(),"");
        //        //this.FilterLookupList(this.selectedFilter);
        //    }
        //}

        //protected virtual void OnFind_Click(System.EventArgs e)
        //{
		
        //}

		protected virtual void OnOK_Click(System.EventArgs e)
		{
			this.dlgResult=(object)this.selectedValue;
			this.DialogResult=DialogResult.OK;
			this.Close();
		}

		protected virtual void OnCancel_Click(System.EventArgs e)
		{
			this.DialogResult=DialogResult.No;
			this.fieldValue="";
			this.Close();
		}

        //protected virtual void OnFind_TextChanged(System.EventArgs e)
        //{
        //    //this.ctlList.SetToIndexOf(this.txtFind.Text);
        //}
		protected virtual void OnList_SelectedIndexChanged(EventArgs e)
		{
			UpdateSelected();
		}
        //protected virtual void OnFind_KeyDown(System.Windows.Forms.KeyEventArgs e)
        //{
		
        //}
		protected virtual void OnList_DoubleClick(EventArgs e)
		{
			OnOK_Click(e);
		}

        //protected virtual void OnFilterType_SelectedIndexChanged(System.EventArgs e)
        //{
        //    if(this.FiltersView!=null)
        //    {
        //        object res= this.FiltersView[this.ctlFilterType.SelectedIndex][FilterColumnIndex];
        //        if(res!=null && (string)res!=this.selectedFilter)
        //        {
        //            this.selectedFilter=(string) res;
        //            if(initData)
        //            {
        //                this.FilterLookupList(this.selectedFilter);
        //            }

        //        }
        //        else
        //        {
        //            return;
        //        }
        //    }
        //}

        //private void txtFind_Enter(object sender, System.EventArgs e)
        //{
        // this.Caption.SubText="שדה טקסט לחיפוש";
        //}

        //private void ctlField_Enter(object sender, System.EventArgs e)
        //{
        //    this.Caption.SubText="השדה לפיו יתבצע החיפוש";
        //}

        //private void ctlFilterType_Enter(object sender, System.EventArgs e)
        //{
        //    this.Caption.SubText="בחירת אפשרוית סינון";
        //}
		#endregion

	}
}
