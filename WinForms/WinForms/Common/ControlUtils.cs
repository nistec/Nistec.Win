
using System;
using System.Windows.Forms;
using System.Text;

namespace Nistec.WinForms
{
    public class ControlUtils
	{
        public static string TextWithoutMnemonics(string text)
        {
            if (text == null)
            {
                return null;
            }
            int length = text.IndexOf('&');
            if (length == -1)
            {
                return text;
            }
            StringBuilder builder = new StringBuilder(text.Substring(0, length));
            while (length < text.Length)
            {
                if (text[length] == '&')
                {
                    length++;
                }
                if (length < text.Length)
                {
                    builder.Append(text[length]);
                }
                length++;
            }
            return builder.ToString();
        }

		public static void Add(Control.ControlCollection coll, Control item)
		{
			if ((coll != null) && (item != null))
			{
				Form parentForm = item.FindForm();

				if (parentForm != null)
				{
					// Add this temporary button to the parent form
					parentForm.Controls.Add(item);

					// Must ensure that temp button is the active one
					parentForm.ActiveControl = item;
				}
                
				// Add our target control
				coll.Add(item);
			}
		}

		public static void RemoveAll(Control control)
		{
			if ((control != null) && (control.Controls.Count > 0))
			{
                Button tempButton = null;
                Form parentForm = control.FindForm();
                  
				if (parentForm != null)
				{
					// Create a hidden, temporary button
					tempButton = new Button();
					tempButton.Visible = false;

					// Add this temporary button to the parent form
					parentForm.Controls.Add(tempButton);

					// Must ensure that temp button is the active one
					parentForm.ActiveControl = tempButton;
                }

  				// Remove all entries from target
				control.Controls.Clear();

                if (parentForm != null)
                {
                    // Remove the temporary button
					tempButton.Dispose();
					parentForm.Controls.Remove(tempButton);
				}
			}
		}

		public static void Remove(Control.ControlCollection coll, Control item)
		{
			if ((coll != null) && (item != null))
			{
                Button tempButton = null;
                Form parentForm = item.FindForm();

				if (parentForm != null)
				{
					// Create a hidden, temporary button
					tempButton = new Button();
					tempButton.Visible = false;

					// Add this temporary button to the parent form
					parentForm.Controls.Add(tempButton);

					// Must ensure that temp button is the active one
					parentForm.ActiveControl = tempButton;
                }
                
				// Remove our target control
				coll.Remove(item);

                if (parentForm != null)
                {
                    // Remove the temporary button
					tempButton.Dispose();
					parentForm.Controls.Remove(tempButton);
				}
			}
		}

		public static void RemoveAt(Control.ControlCollection coll, int index)
		{
			if (coll != null)
			{
				if ((index >= 0) && (index < coll.Count))
				{
					Remove(coll, coll[index]);
				}
			}
		}
    
        public static void RemoveForm(Control source, Form form)
        {
            ContainerControl container = source.FindForm() as ContainerControl;
            
            if (container == null)
                container = source as ContainerControl;

            Button tempButton = new Button();
            tempButton.Visible = false;

            // Add this temporary button to the parent form
            container.Controls.Add(tempButton);

            // Must ensure that temp button is the active one
            container.ActiveControl = tempButton;

            // Remove Form parent
            form.Parent = null;
            
            // Remove the temporary button
            tempButton.Dispose();
            container.Controls.Remove(tempButton);
        }
    }
}