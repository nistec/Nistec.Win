
using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;

using Nistec.Collections;
using Nistec.Drawing;
using Nistec.WinForms;
using Nistec.WinForms.Design;

namespace Nistec.Wizards
{


    [ToolboxItem(false), Designer(typeof(Design.McPageDesigner))]
	public class WizardPage : Nistec.WinForms.McTabPage
	{
		// Instance fields
		protected bool _fullPage;
        //protected string _subTitle;
        //protected string _captionTitle;
        internal McWizard wizParent;
        
		// Instance events
		public event EventHandler FullPageChanged;
		public event EventHandler PageTitleChanged;
		//public event EventHandler CaptionTitleChanged;
    
		public WizardPage()
		{
			_fullPage = false;
			//_subTitle = "(Page Description not defined)";
			//_captionTitle = "(Page Title)";
		}

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new BorderStyle BorderStyle
        {
            get { return base.BorderStyle; }
            set { base.BorderStyle = value; }
        }
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override  Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }

		public bool FullPage
		{
			get { return _fullPage; }
		    
			set 
			{
				if (_fullPage != value)
				{
					_fullPage = value;
					OnFullPageChanged(EventArgs.Empty);
				}
			}
		}

        public override string Text
        {
            get { return base.Text; }

            set
            {
                if (base.Text != value)
                {
                    base.Text = value;
                    OnPageTitleChanged(EventArgs.Empty);
                }
            }
        }

        //public string SubTitle
        //{
        //    get { return this.Text; }

        //    set 
        //    {
        //        if (this.Text != value)
        //        {
        //            this.Text = value;
        //            OnSubTitleChanged(EventArgs.Empty);
        //        }
        //    }
        //}
		
        //public string CaptionTitle
        //{
        //    get { return _captionTitle; }
		    
        //    set
        //    {
        //        if (_captionTitle != value)
        //        {
        //            _captionTitle = value;
        //            OnCaptionTitleChanged(EventArgs.Empty);
        //        }
        //    }
        //}
		
		public virtual void OnFullPageChanged(EventArgs e)
		{
			if (FullPageChanged != null)
				FullPageChanged(this, e);
		}
    
		public virtual void OnPageTitleChanged(EventArgs e)
		{
           
			if (PageTitleChanged != null)
				PageTitleChanged(this, e);
		}

        //public virtual void OnCaptionTitleChanged(EventArgs e)
        //{
        //    if (CaptionTitleChanged != null)
        //        CaptionTitleChanged(this, e);
        //}
	}

	

 
}
