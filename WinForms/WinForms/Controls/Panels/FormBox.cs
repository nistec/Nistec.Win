using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Runtime.InteropServices;

namespace Nistec.WinForms
{
    public interface IFormBox
    {
        bool ShowClose{get;set;}
        bool ShowMinimize{get;set;}
        bool ShowMaximize { get;set;}
        bool Visible { get;set;}

        //Size MaximumSize { get;set;}
        //Size MinimumSize { get;set;}
        //bool AllowMaximize { get;set;}
    }


    [ToolboxItem(false), Serializable, TypeConverter(typeof(FormBoxConverter))]
    public partial class FormBox : Controls.McCustomControl, IFormBox
    {
        public FormBox()
        {
            showClose = true;
            showMaximize = true;
            showMinimize = true;
            //_AllowMaximize = true;
            //_MaximumSize = Size.Empty;
            //_MinimumSize = Size.Empty;
            //_FormState = FormWindowState.Normal;
            base.AutoChildrenStyle = true;
            InitializeComponent();
        }

        internal Form form;
        private bool showClose;
        private bool showMaximize;
        private bool showMinimize;

        //private Size _MaximumSize;
        //private Size _MinimumSize;
        //private bool _AllowMaximize;
        //private FormWindowState _FormState;
        public event EventHandler CloseClicked;
        public event EventHandler RestoeClicked;
        public event EventHandler MinimizeClicked;
        public event EventHandler FormWindowStateChanged;


        //public Size GetMdiSize()
        //{
        //    if (form == null)
        //    {
        //        return Size.Empty;
        //    }
        //    Form mdiForm = form.MdiParent;
        //    if (mdiForm == null)
        //        return Size.Empty;
        //    return mdiForm.MaximumSize;
        //}

        public Form OwnerForm
        {
            get
            {
                if (form == null)
                {
                    form = FindForm();
                }
                return form;
            }
        }

        public bool AllowMaximize
        {
            get { return showMaximize; }
            //set
            //{
            //    _AllowMaximize = value;
            //}
        }

        //public new Size MaximumSize
        //{
        //    get { return _MaximumSize; }
        //    set
        //    {
        //        _MaximumSize = value;
        //    }
        //}
        //public new Size MinimumSize
        //{
        //    get { return _MinimumSize; }
        //    set
        //    {
        //        _MinimumSize = value;
        //    }
        //}

        public bool ShowClose
        {
            get { return showClose; }
            set 
            {
                if (showClose != value)
                {
                    showClose = value;
                    this.btnColse.Visible = value;
                    ButtonSettings();
                }
            }
        }
        public bool ShowMinimize
        {
            get { return showMinimize; }
            set
            {
                if (showMinimize != value)
                {
                    showMinimize = value;
                    this.btnMinimize.Visible = value;
                    ButtonSettings();
                }
            }
        }
        public bool ShowMaximize
        {
            get { return showMaximize; }
            set
            {
                if (showMaximize != value)
                {
                    showMaximize = value;
                    this.btnResore.Visible = value;
                    ButtonSettings();
                }
            }
        }

        private void ButtonSettings()
        {
            Point[]  points = new Point[3];
            points[0] = new System.Drawing.Point(43, 3);//55
            points[1] = new System.Drawing.Point(23, 3);//29
            points[2]= new System.Drawing.Point(3, 3);//3
            int index = 0;

            if (showClose)
            {
                this.btnColse.Location = points[index];
                index++;
            }
            if (showMaximize)
            {
                this.btnResore.Location = points[index];
                index++;
            }
            if (ShowMinimize)
            {
                this.btnMinimize.Location = points[index];
            }

        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            OnMinimize_Click(e);
            if (MinimizeClicked != null)
                MinimizeClicked(this, e);
        }

        private void btnResore_Click(object sender, EventArgs e)
        {
            OnResore_Click(e);
            if (RestoeClicked != null)
                RestoeClicked(this, e);
        }

        private void btnColse_Click(object sender, EventArgs e)
        {
            OnColse_Click(e);
            if (CloseClicked != null)
                CloseClicked(this, e);
        }

        protected virtual void OnMinimize_Click(EventArgs e)
        {
            DoMinimize();
        }

        protected virtual void OnResore_Click(EventArgs e)
        {
            DoResore();
        }

        protected virtual void OnColse_Click(EventArgs e)
        {
            DoColse();
        }

        protected virtual void OnFormWindowStateChanged(EventArgs e)
        {
            if (FormWindowStateChanged != null)
                FormWindowStateChanged(this, e);
        }

        public void DoMinimize()
        {
            OwnerForm.WindowState = FormWindowState.Minimized;
            OnFormWindowStateChanged(EventArgs.Empty);
        }

        public void DoResore()
        {
            if (!AllowMaximize)
            {
                //if (_FormState == FormWindowState.Normal)
                //{
                //    if (!_MaximumSize.IsEmpty)
                //    {
                //        OwnerForm.Size = _MaximumSize;
                //        _FormState = FormWindowState.Maximized;
                //        OnFormWindowStateChanged(EventArgs.Empty);
                //    }
                //}
                //else if (_FormState == FormWindowState.Maximized)
                //{
                //    if (!_MinimumSize.IsEmpty)
                //    {
                //        OwnerForm.Size = _MinimumSize;
                //        _FormState = FormWindowState.Normal;
                //        OnFormWindowStateChanged(EventArgs.Empty);
                //    }
                //}
                //else
                //{
                //    OwnerForm.WindowState = FormWindowState.Normal;
                //    _FormState = FormWindowState.Normal;
                //    OnFormWindowStateChanged(EventArgs.Empty);
                //}
            }

            else if (OwnerForm.WindowState == FormWindowState.Normal)
            {
                if (ShowMaximize)
                {
                    OwnerForm.WindowState = FormWindowState.Maximized;
                    OnFormWindowStateChanged(EventArgs.Empty);
                }
            }
            else //if (OwnerForm.WindowState == FormWindowState.m)
            {
                OwnerForm.WindowState = FormWindowState.Normal;
                OnFormWindowStateChanged(EventArgs.Empty);
            }
        }

        public void DoColse()
        {
            OwnerForm.Close();
        }

        protected override void OnStylePainterChanged(EventArgs e)
        {
            base.OnStylePainterChanged(e);

            this.btnColse.StylePainter = this.StylePainter;
            this.btnMinimize.StylePainter = this.StylePainter;
            this.btnResore.StylePainter = this.StylePainter;
        }

    }

    #region FormBoxConverter
    /// <summary>
    /// Summary description for RangeConverter.
    /// </summary>
    public class FormBoxConverter : TypeConverter
    {
        public FormBoxConverter()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        /// <summary>
        /// allows us to display the + symbol near the property name
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="value"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(typeof(IFormBox));
        }

    }
    #endregion

}
