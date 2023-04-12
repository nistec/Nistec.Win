using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Collections;
using System.IO;
using System.Xml;
using MControl.SyntaxEditor;
using MControl.SyntaxEditor.Document;

namespace MControl.SyntaxEditor
{

    internal class IntelliForm : MControl.WinForms.Controls.McPopUpBase
    {
        #region members

        internal System.Windows.Forms.ListView lstv;
        protected TextEditor mparent = null;
        private bool dispose = false;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private ImageList imageList1;
        private IContainer components;
        private object selectedItem = null;

        #endregion

        #region Constructors

        public IntelliForm()
        {

        }

        public IntelliForm(TextEditor parent)
            : base(parent)
        {
            mparent = parent;
            this.KeyPreview = false;
            InitializeComponent();
            this.TopLevel = false;
        }
        
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IntelliForm));
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.lstv = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // columnHeader1
            // 
            this.columnHeader1.Name = "columnHeader1";
            // 
            // lstv
            // 
            this.lstv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lstv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstv.FullRowSelect = true;
            this.lstv.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.lstv.HideSelection = false;
            this.lstv.LabelWrap = false;
            this.lstv.Location = new System.Drawing.Point(0, 0);
            this.lstv.MultiSelect = false;
            this.lstv.Name = "lstv";
            this.lstv.Size = new System.Drawing.Size(129, 97);
            this.lstv.SmallImageList = this.imageList1;
            this.lstv.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lstv.TabIndex = 1;
            this.lstv.TabStop = false;
            this.lstv.UseCompatibleStateImageBehavior = false;
            this.lstv.View = System.Windows.Forms.View.SmallIcon;
            this.lstv.DoubleClick += new System.EventHandler(this.lstv_DoubleClick);
            this.lstv.LostFocus += new System.EventHandler(this.lstv_LostFocus);
            this.lstv.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstv_KeyUp);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "method.gif");
            this.imageList1.Images.SetKeyName(1, "properties.gif");
            this.imageList1.Images.SetKeyName(2, "field.gif");
            // 
            // IntelliForm
            // 
            this.ClientSize = new System.Drawing.Size(129, 97);
            this.Controls.Add(this.lstv);
            this.Name = "IntelliForm";
            this.Text = "IntelliForm";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        public void ShowPopUp(Words list, IntPtr hwnd, Size size, Point pt)
        {
            lstv.Items.Clear();

            foreach (WORD s in list)
            {
                lstv.Items.Add(s.Text,(int)s.MemberType);
            }
            this.selectedItem = null;

            this.Location = pt;
            this.Size = size;
            this.Height = this.Height;
            this.LockClose = true;
            this.lstv.Columns[0].Width = this.lstv.Width;
            base.ShowPopUp(hwnd, 4);
            base.Start = true;
            
            //this.ClientSize = size;
            //this.Height = this.Height;
            //this.lstv.Columns[0].Width = this.lstv.Width;

            this.BringToFront();
            lstv.Visible = true;
            lstv.Focus();
            this.lstv.Items[0].Selected = true;
        }

        #endregion

        #region Dispose

        public void DisposePopUp(bool disposing)
        {
            dispose = disposing;
            this.Dispose(disposing);
        }

        protected override void Dispose(bool disposing)
        {
            //this.panel1.Controls.Clear ();

            //if (disposing)
            //{
            //    //mparent.Controls[0].LostFocus -= new System.EventHandler(this.ParentControlLostFocus);
            //    if (components != null)
            //    {
            //        components.Dispose();
            //    }
            //}
            base.Dispose(dispose);// disposing );
        }
        #endregion

        #region Overrides

        void lstv_LostFocus(object sender, EventArgs e)
        {
            this.Close();
        }

        void lstv_DoubleClick(object sender, EventArgs e)
        {
            OnSelectionChanged();
        }

        void lstv_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape || e.KeyData == Keys.Back)
            {
                this.Close();
            }
            else if (e.KeyData == Keys.Enter || e.KeyData == Keys.Tab)
            {
                OnSelectionChanged();
            }
        }

        internal void OnSelectionChanged()
        {
            if (lstv.FocusedItem != null)
            {
                this.selectedItem = lstv.FocusedItem.Text;
                mparent.CompleteIntelliWord(this.selectedItem);
            }
            this.Close();
        }

        protected override void OnClosed(System.EventArgs e)
        {
            this.Hide();
            base.OnClosed(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            base.LockClose = true;
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {

            switch ((keyData & Keys.KeyCode))
            {
                case Keys.W:
                    if ((keyData & Keys.Control) != Keys.None)
                        this.Close();
                    break;
            }
            return base.ProcessDialogKey(keyData);
        }

        #endregion

        #region Properties

        public string CurrentItemText
        {
            get { return this.lstv.FocusedItem.Text; }
        }

        public override object SelectedItem
        {
            get
            {
                return this.selectedItem;//.lstv.FocusedItem as object;
            }
        }

        internal new bool LockClose
        {
            get { return base.LockClose; }
            set { base.LockClose = value; }
        }

        #endregion
 
    }

}