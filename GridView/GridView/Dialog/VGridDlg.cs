using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Nistec.WinForms;
using Nistec.Win;

namespace Nistec.GridView
{
    /// <summary>
    /// VGridDlg
    /// </summary>
    public partial class VGridDlg : Nistec.WinForms.McForm
    {
        internal static bool IsOpen = false;
        /// <summary>
        /// When Property Item Changed
        /// </summary>
        public event PropertyItemChangedEventHandler PropertyItemChanged;
        /// <summary>
        /// When Botton Save Clicked
        /// </summary>
        public event EventHandler SaveChanged;

        /// <summary>
        /// Open VGridDlg
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="name"></param>
        public static void Open(DataRow dr, string name)
        {
            VGridDlg pd = new VGridDlg(dr, name);
            pd.Show();
        }
        /// <summary>
        /// Open VGridDlg
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="name"></param>
        public static void Open(DataRowView dr, string name)
        {
            VGridDlg pd = new VGridDlg(dr, name);
            pd.Show();
        }
        /// <summary>
        /// Open VGridDlg
        /// </summary>
        /// <param name="style"></param>
        /// <param name="dr"></param>
        /// <param name="name"></param>
        public static void Open(IStyle style, DataRow dr, string name)
        {
            VGridDlg pd = new VGridDlg(style, dr, name);
            pd.Show();
        }

        /// <summary>
        /// Initilaized VGridDlg
        /// </summary>
        public VGridDlg()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Initilaized VGridDlg
        /// </summary>
        /// <param name="style"></param>
        public VGridDlg(IStyle style):this()
        {
            base.SetStyleLayout(style);
        }

        /// <summary>
        /// Initilaized VGridDlg
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="name"></param>
        public VGridDlg(DataRow dr, string name)
            : this()
        {
            SelectObject(dr, name);
         }
        /// <summary>
         /// Initilaized VGridDlg
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="name"></param>
        public VGridDlg(DataRowView dr, string name)
            : this()
        {
            SelectObject(dr, name);
        }
        /// <summary>
        /// Initilaized VGridDlg
        /// </summary>
        /// <param name="style"></param>
        /// <param name="dr"></param>
        /// <param name="name"></param>
        public VGridDlg(IStyle style, DataRow dr, string name)
            : this(style)
        {
            SelectObject(dr, name);
        }


        /// <summary>
        /// Select Object
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="name"></param>
        public void SelectObject(DataRow dr, string name)
        {
            propertyGrid1.SetDataBinding(dr, name);
            this.Caption.SubText = name;
        }
        /// <summary>
        /// Select Object
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="name"></param>
        public void SelectObject(DataRowView dr, string name)
        {
            propertyGrid1.SetDataBinding(dr, name);
            this.Caption.SubText = name;
        }
        /// <summary>
        /// Select Object
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="name"></param>
        public void SelectObject(GridField[] fields, string name)
        {
            propertyGrid1.SetDataBinding(fields, name);
            this.Caption.SubText = name;
        }
        /// <summary>
        /// Select Object
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="name"></param>
        public void SelectObject(GridFieldCollection fields, string name)
        {
            propertyGrid1.SetDataBinding(fields, name);
            this.Caption.SubText = name;
        }
        /// <summary>
        /// Occurs on Load
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            VGridDlg.IsOpen = true;
        }
        /// <summary>
        /// Occurs on closed
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            VGridDlg.IsOpen = false;
        }
        /// <summary>
        /// Get VGrid
        /// </summary>
        [Browsable(false),EditorBrowsable(EditorBrowsableState.Advanced )]
        public VGrid VGrid
        {
            get { return this.propertyGrid1; }
        }

        /// <summary>
        /// ShowToolBar
        /// </summary>
        public bool ShowToolBar
        {
            get { return this.mcToolBar.Visible; }
            set { this.mcToolBar.Visible = value; }
        }

       

        void mcToolBar_ButtonClick(object sender, Nistec.WinForms.ToolButtonClickEventArgs e)
        {
            switch (e.Button.Name)
            {
                case "tbSave":
                    OnSaveChanged(EventArgs.Empty);
                    break;
                case "tbPrint":
                    GridPerform.Print(this.propertyGrid1);
                    break;
                case "tbExport":
                    GridPerform.Export(this.propertyGrid1);
                    break;
                case "tbFit":
                    break;
            }
            OnToolButtonClicked(e);
        }

        /// <summary>
        /// OnToolButtonClick
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnToolButtonClicked(Nistec.WinForms.ToolButtonClickEventArgs e)
        {

        }


        private void mnExport_Click(object sender, EventArgs e)
        {
            GridPerform.Export(this.propertyGrid1);
            //this.propertyGrid1.PerformExport();
        }

        private void mnPrint_Click(object sender, EventArgs e)
        {
            GridPerform.Print(this.propertyGrid1);
            //this.propertyGrid1.PerformPrint();
        }

        private void propertyGrid1_CurrentCellChanged(object sender, EventArgs e)
        {
            GridField field = propertyGrid1.GetCurrentField();
            if (field != null)
            {
                lblDescription.Text = field.Description.Length > 0 ? field.Description : field.Key;
            }
            else
            {
                lblDescription.Text = "";
            }
        }

        void propertyGrid1_ReadOnlyChanged(object sender, System.EventArgs e)
        {
            this.tbSave.Enabled = !this.propertyGrid1.ReadOnly;
        }
        void propertyGrid1_DirtyChanged(object sender, System.EventArgs e)
        {
            this.tbSave.Enabled = !this.propertyGrid1.ReadOnly && this.propertyGrid1.Dirty;
        }

        void propertyGrid1_PropertyItemChanged(object sender, PropertyItemChangedEventArgs e)
        {
            OnPropertyItemChanged(e);
        }
        /// <summary>
        /// OnSaveChanged
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnSaveChanged(EventArgs e)
        {
            this.tbSave.Enabled = false;
            if (SaveChanged != null)
                SaveChanged(this, e);
        }
        /// <summary>
        /// OnPropertyItemChanged
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnPropertyItemChanged(PropertyItemChangedEventArgs e)
        {
            if (PropertyItemChanged != null)
                PropertyItemChanged(this, e);
        }
    }
}