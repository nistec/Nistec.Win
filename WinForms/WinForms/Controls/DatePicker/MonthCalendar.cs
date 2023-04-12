using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Nistec.WinForms
{

    [System.ComponentModel.ToolboxItem(false)]
    public class MonthCalendar : System.Windows.Forms.MonthCalendar
    {
        private System.ComponentModel.Container components = null;

        #region Constructors
        public MonthCalendar()
        {
            InitializeComponent();
        }

        #endregion

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        #endregion

        #region Component Designer generated code
        private void InitializeComponent()
        {
            // 
            // MonthCalendar
            // 
            this.AnnuallyBoldedDates = new System.DateTime[0];
            this.BoldedDates = new System.DateTime[0];
            this.MonthlyBoldedDates = new System.DateTime[0];
            this.SelectionRange = new System.Windows.Forms.SelectionRange(new System.DateTime(2002, 5, 7, 0, 0, 0, 0), new System.DateTime(2002, 5, 7, 0, 0, 0, 0));
            this.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar_DateChanged);

        }
        #endregion

        #region Events helpers
        private void monthCalendar_DateChanged(object sender, System.Windows.Forms.DateRangeEventArgs e)
        {
            this.SetSelectionRange(this.SelectionRange.Start, this.SelectionRange.Start);
        }
        #endregion

        #region Overrides
        protected override void WndProc(ref Message m)
        {
            try { base.WndProc(ref m); }
            catch { }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
        }

        internal void KeyDownInternal(KeyEventArgs e)
        {
            base.OnKeyDown(e);
        }
        #endregion

        #region Methods
        public void PostMessage(ref Message m)
        {
            base.WndProc(ref m);
        }
        #endregion
    }

}