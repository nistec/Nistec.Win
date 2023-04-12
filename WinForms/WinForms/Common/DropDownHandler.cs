using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Nistec.WinForms
{
    internal class DropDownHandler
    {
        Form form;
        Form mdiForm;
        Control Owner;
        Form ComboPopUp;

        internal DropDownHandler(Control owner, Form comboPopUp)
        {
            if (!(owner is IDropDown))
            {
                throw new ArgumentException("This type not supported");
            }
            Owner = owner;
            ComboPopUp = comboPopUp;
            SetForm();
        }

        private void SetForm()
        {

            if (form != null)
            {
                form.Deactivate -= new EventHandler(form_Deactivate);
                form.Move -= new EventHandler(form_Move);

                if (mdiForm != null)
                {
                    mdiForm.Deactivate -= new EventHandler(form_Deactivate);
                    mdiForm.Move -= new EventHandler(form_Move);
                }

            }
            else //if (form == null)
            {
                if (this.Owner != null)
                    form = this.Owner.FindForm();
                else
                    form = Form.ActiveForm;

                if (form != null)
                {
                    mdiForm = form.MdiParent;
                }

            }
            if (form != null)
            {
                form.Deactivate += new EventHandler(form_Deactivate);
                form.Move += new EventHandler(form_Move);
                if (mdiForm != null)
                {
                    mdiForm.Deactivate += new EventHandler(form_Deactivate);
                    mdiForm.Move += new EventHandler(form_Move);
                }
            }

        }

        //private int deactiveCount;
        private void form_Deactivate(object sender, EventArgs e)
        {
            //form.Deactivate -= new EventHandler(form_Deactivate);

            if (((IDropDown)Owner).DroppedDown)
            {
                Point p = this.ComboPopUp.PointToClient(Cursor.Position);

                if (this.ComboPopUp.ClientRectangle.Contains(p))
                {
                    return;
                }
                //EndHook();
                ((IDropDown)Owner).CloseDropDown();

            }
        }

        private void form_Move(object sender, EventArgs e)
        {
            ((IDropDown)Owner).CloseDropDown();
        }

    }
}
