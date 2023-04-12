using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using MControl.WinForms;

namespace MControl.Wizards.Forms
{


	/// <summary>
	/// Summary description for frmListDlg.
	/// </summary>
	public class ListDlgForm : MControl.WinForms.McForm
	{

		public enum ListDialogMode
		{
			Dialog,
			DropDown
		}

		#region Ctor
		protected MControl.Wizards.McListFinder wizList;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        public ListDlgForm()
		{
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

		protected override void OnDeactivate(EventArgs e)
		{
			base.OnDeactivate (e);
			this.Close();
		}

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.wizList = new MControl.Wizards.McListFinder();
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
            // wizList
            // 
            this.wizList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizList.FinderText = "";
            this.wizList.Location = new System.Drawing.Point(2, 38);
            this.wizList.Name = "wizList";
            this.wizList.OpenAsListOnly = false;
            this.wizList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.wizList.SelectionMode = System.Windows.Forms.SelectionMode.One;
            this.wizList.ShowButtons = true;
            this.wizList.ShowTitle = true;
            this.wizList.Size = new System.Drawing.Size(260, 277);
            this.wizList.StylePainter = this.StyleGuideBase;
            this.wizList.TabIndex = 3;
            this.wizList.TabStop = false;
            this.wizList.Text = "wizListFinder1";
            this.wizList.Title = "";
            this.wizList.TitleHeight = 0;
            this.wizList.TitleImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.wizList.TitleTextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.wizList.CancelClick += new System.EventHandler(this.wizList_CancelClick);
            this.wizList.OkClick += new System.EventHandler(this.wizList_OkClick);
            // 
            // ListDlgForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.CaptionVisible = true;
            this.ClientSize = new System.Drawing.Size(264, 317);
            this.ControlLayout = MControl.WinForms.ControlLayout.System;
            this.Controls.Add(this.wizList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ListDlgForm";
            this.Text = "MControl List Dialog";
            this.Controls.SetChildIndex(this.wizList, 0);
            this.ResumeLayout(false);

		}
		#endregion

		#region ListFinder methods

		private object dlgResult=null;
		private ListDialogMode Mode=ListDialogMode.Dialog;
		private Control Mc=null;

        //public static void OpenDropDown(Control ctl,int maxRowVisible ,ListDialogType listType,string value)
        //{
        //    OpenDialog(ListDialogMode.DropDown,ctl,maxRowVisible,listType,value);
        //}

        //public static object OpenDialog(ListDialogType listType,string value)
        //{
        //    return OpenDialog(ListDialogMode.Dialog,null,20,listType,value);
        //}

        public static object OpenDialog(object dataSourec, string valueMember, string displyMember, string selectedValue)
        {
            return OpenDialog(ListDialogMode.Dialog, null, SelectionMode.One, RightToLeft.Inherit, 20,0, dataSourec, valueMember, displyMember, "MControl", selectedValue);
        }

        public static object OpenDialog(object dataSourec, string valueMember, string displyMember, string Title, string selectedValue)
		{
            return OpenDialog(ListDialogMode.Dialog, null, SelectionMode.One, RightToLeft.Inherit, 20,0, dataSourec, valueMember, displyMember, Title, selectedValue);
		}

        public static object OpenDialog(ListDialogMode mode, Control ctl, object dataSourec, string valueMember, string displyMember, string Title, string selectedValue)
        {
            return OpenDialog(mode, ctl, SelectionMode.One, RightToLeft.Inherit, 20,0, dataSourec, valueMember, displyMember, Title, selectedValue);
        }

        public static object OpenDialog(ListDialogMode mode, Control ctl,  int width,object dataSourec, string valueMember, string displyMember, string Title, string selectedValue)
        {
            return OpenDialog(mode, ctl, SelectionMode.One, RightToLeft.Inherit, 20, width, dataSourec, valueMember, displyMember, Title, selectedValue);
        }

        public static object OpenDialog(ListDialogMode mode, Control ctl, SelectionMode selectionMode, RightToLeft rtl, int maxRowVisible, int width, object dataSourec, string valueMember, string displyMember, string Title, string selectedValue)
        {
            ListDlgForm f = new ListDlgForm();
            f.RightToLeft=rtl;

            f.Text = Title;
            f.wizList.RightToLeft = RightToLeft.Inherit;
            f.wizList.Title = Title;
            f.wizList.ValueMember = valueMember;
            f.wizList.DisplayMember = displyMember;
            f.wizList.DataSource = dataSourec;
            f.wizList.SelectionMode = selectionMode;
            if (width > 0)
                f.wizList.Width = width;
            f.wizList.List.Visible = true;

            if (selectedValue != null)
            {
                //f.wizList.McTextBox.Text=value;
                f.wizList.List.FindString(selectedValue);
            }
            if (mode == ListDialogMode.DropDown)
            {
                f.Mc = ctl;
                f.Mode = ListDialogMode.DropDown;
                f.Owner = ctl.FindForm();

                f.StartPosition = FormStartPosition.Manual;
                f.Width = ctl.Width + 8;
                f.Height = 94 + (f.wizList.List.ItemHeight * maxRowVisible);
                f.Location = ctl.Parent.PointToScreen(new Point(ctl.Left, ctl.Bottom + 1));
                f.CaptionVisible = false;
                f.FormBorderStyle = FormBorderStyle.None;
                f.ShowInTaskbar = false;
                f.Show();
                f.wizList.List.Focus();
                //f.Activate();
                //MControl.Win32.WinAPI.ShowWindow(f.Handle,1);
                return null;
            }
            else
            {
                if (ctl != null)
                {
                    f.StartPosition = FormStartPosition.CenterParent;
                }
                else
                    f.StartPosition = FormStartPosition.CenterScreen;
                f.ShowDialog();
                return f.dlgResult;
            }
        }

		public object DlgResult
		{
			get{return  dlgResult;}
		}

        public MControl.Data.Record[] SelectedListItems
        {
            get { return wizList.SelectedListItems; }
        }

//		private void wizList_DoubleClick(object sender, System.EventArgs e)
//		{
//			cmdOK();
//		}

		private void wizList_OkClick(object sender, System.EventArgs e)
		{
			cmdOK();
		}

		private void wizList_CancelClick(object sender, System.EventArgs e)
		{
			this.DialogResult=DialogResult.No;
			this.dlgResult=null;
			this.Close();
		}


		private void cmdOK()
		{
			this.dlgResult=this.wizList.List.SelectedValue;

			if(Mode==ListDialogMode.DropDown)
			{
				if(this.dlgResult!=null)
				{
					Mc.Text=this.dlgResult.ToString();
				}
			}
			else
			{
				this.DialogResult=DialogResult.OK;
				this.dlgResult=this.wizList.List.SelectedValue;
				if(this.dlgResult==null)
				{
					this.DialogResult=DialogResult.No;
				}
				this.Close();
			}
		}

		public  bool GoIns()
		{
			this.Close();
			return true;
		}

		#endregion

	}
}
