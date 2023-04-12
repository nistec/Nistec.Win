using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using MControl.WinForms;

namespace MControl.Wizards.Forms
{
 

	/// <summary>
	/// Summary description for frmNavList.
	/// </summary>
    public class NavBarListForm : NavBarForm
	{
		#region Ctor

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public NavBarListForm()
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

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.ctlList = new MControl.WinForms.McListBox();
            this.ctlSplitter1 = new MControl.WinForms.McSplitter();
            this.ctlPanel1 = new MControl.WinForms.McPanel();
            this.ctlToolBar = new MControl.WinForms.McToolBar();
            ((System.ComponentModel.ISupportInitialize)(this.ctlNavBar)).BeginInit();
            this.SuspendLayout();
            // 
            // ctlNavBar
            // 
            this.ctlNavBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctlNavBar.Connection = null;
            this.ctlNavBar.ControlLayout = MControl.WinForms.ControlLayout.Visual;
            this.ctlNavBar.DBProvider = MControl.Data.DBProvider.SqlServer;
            this.ctlNavBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ctlNavBar.EnableButtons = false;
            this.ctlNavBar.Location = new System.Drawing.Point(2, 309);
            this.ctlNavBar.ManagerDataAdapter = null;
            this.ctlNavBar.MessageDelete = "Delete";
            this.ctlNavBar.MessageSaveChanges = "SaveChanges";
            this.ctlNavBar.Name = "ctlNavBar";
            this.ctlNavBar.Padding = new System.Windows.Forms.Padding(2);
            this.ctlNavBar.ShowChangesButtons = true;
            this.ctlNavBar.Size = new System.Drawing.Size(461, 24);
            this.ctlNavBar.SizingGrip = false;
            this.ctlNavBar.StylePainter = this.StyleGuideBase;
            this.ctlNavBar.TabIndex = 0;
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
            this.ctlList.BackColor = System.Drawing.Color.White;
            this.ctlNavBar.SetDataField(this.ctlList, "");
            this.ctlList.Dock = System.Windows.Forms.DockStyle.Left;
            this.ctlList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.ctlList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ctlList.ForeColor = System.Drawing.Color.Black;
            this.ctlList.ItemHeight = 18;
            this.ctlList.Location = new System.Drawing.Point(2, 66);
            this.ctlList.Name = "ctlList";
            this.ctlList.ReadOnly = false;
            this.ctlList.Size = new System.Drawing.Size(136, 243);
            this.ctlList.StylePainter = this.StyleGuideBase;
            this.ctlList.TabIndex = 7;
            this.ctlList.SelectedIndexChanged += new System.EventHandler(this.ctlList_SelectedIndexChanged);
            // 
            // ctlSplitter1
            // 
            this.ctlSplitter1.BackColor = System.Drawing.Color.White;
            this.ctlSplitter1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ctlSplitter1.ForeColor = System.Drawing.Color.Black;
            this.ctlSplitter1.Location = new System.Drawing.Point(138, 66);
            this.ctlSplitter1.Name = "ctlSplitter1";
            this.ctlSplitter1.Size = new System.Drawing.Size(4, 243);
            this.ctlSplitter1.StylePainter = this.StyleGuideBase;
            this.ctlSplitter1.TabIndex = 8;
            this.ctlSplitter1.TabStop = false;
            // 
            // ctlPanel1
            // 
            this.ctlPanel1.BackColor = System.Drawing.Color.White;
            this.ctlPanel1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ctlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ctlPanel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlPanel1.Location = new System.Drawing.Point(142, 66);
            this.ctlPanel1.Name = "ctlPanel1";
            this.ctlPanel1.Size = new System.Drawing.Size(321, 243);
            this.ctlPanel1.StylePainter = this.StyleGuideBase;
            this.ctlPanel1.TabIndex = 9;
            // 
            // ctlToolBar
            // 
            this.ctlToolBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(203)))), ((int)(((byte)(183)))));
            this.ctlToolBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctlToolBar.ControlLayout = MControl.WinForms.ControlLayout.Visual;
            this.ctlToolBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlToolBar.FixSize = false;
            this.ctlToolBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ctlToolBar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlToolBar.Location = new System.Drawing.Point(2, 38);
            this.ctlToolBar.Name = "ctlToolBar";
            this.ctlToolBar.Padding = new System.Windows.Forms.Padding(12, 3, 3, 3);
            this.ctlToolBar.Size = new System.Drawing.Size(461, 28);
            this.ctlToolBar.StylePainter = this.StyleGuideBase;
            this.ctlToolBar.TabIndex = 10;
            this.ctlToolBar.ButtonClick += new MControl.WinForms.ToolButtonClickEventHandler(this.ctlToolBar_ButtonClick);
            // 
            // NavBarListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.CaptionVisible = true;
            this.ClientSize = new System.Drawing.Size(465, 335);
            this.Controls.Add(this.ctlPanel1);
            this.Controls.Add(this.ctlSplitter1);
            this.Controls.Add(this.ctlList);
            this.Controls.Add(this.ctlToolBar);
            this.Name = "NavBarListForm";
            this.Text = "NavBar List Form";
            this.Controls.SetChildIndex(this.ctlToolBar, 0);
            this.Controls.SetChildIndex(this.ctlNavBar, 0);
            this.Controls.SetChildIndex(this.ctlList, 0);
            this.Controls.SetChildIndex(this.ctlSplitter1, 0);
            this.Controls.SetChildIndex(this.ctlPanel1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ctlNavBar)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		protected MControl.WinForms.McListBox ctlList;
		protected MControl.WinForms.McSplitter ctlSplitter1;
		protected MControl.WinForms.McPanel ctlPanel1;
        protected MControl.WinForms.McToolBar ctlToolBar;

		#region NavBar_List synchronize

		// navBar=1 ctlList=2
		private int ModeStatus;
        private bool synchronize=true;
        private int listEnabledMode = 0;
        public bool UseSynchronize
        {
            get { return synchronize; }
            set { synchronize = value; }
        }

		private void ctlList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
            if (!synchronize)
            {
                if (this.ctlList.Focused && ModeStatus != 1)
                    OnRowSelectedChanged(false);
            }
		}

		protected override void OnPositionChanged(EventArgs e)
		{
			base.OnPositionChanged (e);
			if(ModeStatus !=2)
				OnRowSelectedChanged(true);

		}

		private void OnRowSelectedChanged(bool isNavBar)
		{
            try
            {
                if (!synchronize)
                {
                    OnRowSelectedChanged(EventArgs.Empty);
                    return;
                }
                if (isNavBar)
                {
                    ModeStatus = 1;
                    this.ctlList.SelectedIndex = this.ctlNavBar.SelectedIndex;
                }
                else
                {
                    ModeStatus = 2;
                    if (this.ctlNavBar.DataList.Sort.Length == 0)
                        this.ctlNavBar.DataList.Sort = this.ctlList.ValueMember;
                    int i = this.ctlNavBar.DataList.Find(this.ctlList.SelectedValue);
                    if (i > -1)
                    {
                        this.ctlNavBar.SelectedIndex = i;
                    }
                }
                ModeStatus = 0;
                OnRowSelectedChanged(EventArgs.Empty);
            }
            catch { }
		}

        protected virtual void OnRowSelectedChanged(EventArgs e)
        {
            if (listEnabledMode > 0)
                listEnabledMode++;
            if (listEnabledMode > 2)
            {
                this.ctlList.Enabled = true;
                listEnabledMode = 0;
            }
        }

		protected override void OnNavBarUpdated(NavBarUpdatedEventArgs e)
		{
			base.OnNavBarUpdated (e);
			if(e.NavBarStatus==NavStatus.New || e.NavBarStatus==NavStatus.Delete)
			{
				Initialize(null);// ReBind();
			}

		}
       
        protected override void OnNavBarRowNew(System.Data.DataTableNewRowEventArgs e)
        {
            base.OnNavBarRowNew(e);
            if (synchronize)
            {
                this.ctlList.Enabled = false;
                listEnabledMode = 1;
            }
        }
		#endregion

        private void ctlToolBar_ButtonClick(object sender, ToolButtonClickEventArgs e)
        {
            OnToolBarClick(e);
        }

        protected virtual void OnToolBarClick(ToolButtonClickEventArgs e)
        {

        }

 	}
}

