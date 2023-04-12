
using System;
using System.Windows.Forms;

namespace MControl.Collections
{
    public class ControlCollection
	{
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

		public static void ListControls(Control.ControlCollection controls)
		{
			ListControls("Control Collection", controls, false);
		}

		public static void ListControls(string title, Control.ControlCollection controls)
		{
			ListControls(title, controls, false);
		}

		public static void ListControls(string title, Control.ControlCollection controls, bool fullName)
		{
			// Output title first
			Console.WriteLine("\n{0}", title);

			// Find number of controls in the collection
			int count = controls.Count;

			// Output details for each 
			for(int index=0; index<count; index++)
			{
				Control c = controls[index];

				string typeName;
				
				if (fullName)
					typeName = c.GetType().FullName;
				else
					typeName = c.GetType().Name;

				Console.WriteLine("{0} V:{1} T:{2} N:{3}", index, c.Visible, typeName, c.Name);
			}
		}

    }
}