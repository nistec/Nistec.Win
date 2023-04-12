using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Text;

namespace Nistec.WinForms
{
	[Designer(typeof(Design.McDockingDesigner)), ToolboxItem(true), ToolboxBitmap(typeof(McDocking), "Toolbox.DockingManager.bmp")]
	public class McDocking : Component
	{

		// Fields
		private bool autoHide;
		private ArrayList closedControls;
		private int controlIndex;
		private DockStyle dockStyle;
		private McFloatingForm floatingForm;
		private Form form;
		private int height;
		private bool isClosing;
		private bool isFloating;
		private int left;
		private bool lockDockingPanels;
		private bool lockLayout;
		private Form parentForm;
		private int selectedIndex;
		private bool showingUndocked;
		private int top;
		private ArrayList undockedPanels;
		private int width;


		public McDocking()
		{
			this.autoHide = false;
			this.dockStyle = DockStyle.Left;
			this.left = 0;
			this.top = 0;
			this.width = 200;
			this.height = 300;
			this.controlIndex = -1;
			this.selectedIndex = 0;
			this.floatingForm = null;
			this.isFloating = false;
			this.isClosing = false;
			this.lockDockingPanels = false;
			this.undockedPanels = new ArrayList();
			this.closedControls = new ArrayList();
			this.showingUndocked = true;
			this.parentForm = null;
			this.form = null;
			this.lockLayout = false;
			IContainer container1 = base.Container;
		}

 
		internal void AddUndockingPanels(McDockingPanel dockingPanel)
		{
            if (this.UndockedPanels == null)
            {
                this.UndockedPanels = new ArrayList();
            }
			this.UndockedPanels.Add(dockingPanel);
			this.Form = (dockingPanel.FloatingForm != null) ? dockingPanel.LastDockForm : (dockingPanel.Parent as Form);
		}

		internal McDockingTab GetDockingControl(string guid)
		{
			foreach (McDockingPanel panel1 in this.DockedPanels)
			{
				foreach (McDockingTab control1 in panel1.Controls)
				{
					if (control1.Guid == guid)
					{
						return control1;
					}
				}
			}
			foreach (McDockingPanel panel2 in this.UndockedPanels)
			{
				foreach (McDockingTab control2 in panel2.Controls)
				{
					if (control2.Guid == guid)
					{
						return control2;
					}
				}
			}
			foreach (McDockingTab control3 in this.ClosedControls)
			{
				McDockingPanel panel3 = new McDockingPanel(this);
				panel3.LastDockForm = this.ParentForm;
				panel3.Controls.Add(control3);
				if (control3.Guid == guid)
				{
					return control3;
				}
			}
			return null;
		}

 
		internal McDockingPanel GetDockingPanel(DockStyle dockStyle)
		{
			foreach (Control control1 in this.ParentForm.Controls)
			{
				if ((control1 is McDockingPanel) && (control1.Dock == dockStyle))
				{
					return (control1 as McDockingPanel);
				}
			}
			return null;
		}

 
		public void LoadConfig(Stream stream)
		{
			this.LockLayout = true;
			XmlTextReader reader1 = new XmlTextReader(stream);
			this.LoadConfig(reader1);
			this.LockLayout = false;
			this.SetLayoutAllPanels();
		}

		public void LoadConfig(string file)
		{
			if (File.Exists(file))
			{
				FileStream stream1 = new FileStream(file, FileMode.Open);
				this.LoadConfig(stream1);
				stream1.Flush();
				stream1.Close();
			}
		}

		public void LoadConfig(XmlTextReader tr)
		{
			this.LockLayout = true;
			if (tr.IsStartElement() && (tr.Name == "McDockingManagerConfig"))
			{
				this.autoHide = false;
				this.dockStyle = DockStyle.Left;
				this.width = 200;
				this.height = 300;
				this.LoadNodes(tr);
			}
			this.LockLayout = false;
			this.SetLayoutAllPanels();
		}

 
		private void LoadNodes(XmlTextReader tr)
		{
			while (tr.Read())
			{
				if (!tr.IsStartElement())
				{
					return;
				}
				if (tr.Name == "Panel")
				{
					this.controlIndex = -1;
					this.isFloating = tr.GetAttribute("Floating") == "True";
					this.isClosing = tr.GetAttribute("Closing") == "True";
					if (!this.isClosing)
					{
						this.width = int.Parse(tr.GetAttribute("Width"));
						this.height = int.Parse(tr.GetAttribute("Height"));
						this.selectedIndex = int.Parse(tr.GetAttribute("SelectedIndex"));
						if (!this.isFloating)
						{
							this.autoHide = tr.GetAttribute("AutoHide") == "True";
							this.dockStyle = (DockStyle) Enum.Parse(typeof(DockStyle), tr.GetAttribute("Dock"));
						}
						else
						{
							this.left = int.Parse(tr.GetAttribute("Left"));
							this.top = int.Parse(tr.GetAttribute("Top"));
						}
					}
					this.floatingForm = null;
				}
				if (tr.Name == "Guid")
				{
					this.controlIndex++;
					string text1 = XmlConvert.DecodeName(tr.ReadElementString());
					McDockingTab control1 = this.GetDockingControl(text1);
					if (control1 != null)
					{
						McDockingPanel panel1 = control1.Parent as McDockingPanel;
						if (!this.isClosing)
						{
							if (!this.isFloating)
							{
								if (control1.Parent.Dock != this.dockStyle)
								{
									McFloatingForm form1 = panel1.FloatingForm;
									panel1.DockControlToForm(control1, this.ParentForm, this.dockStyle);
									panel1 = this.GetDockingPanel(this.dockStyle);
									if (panel1.Controls.Count == 0)
									{
										this.UndockedPanels.Remove(panel1);
									}
								}
								if (control1.Parent.Controls.IndexOf(control1) != this.controlIndex)
								{
									control1.Parent.Controls.SetChildIndex(control1, this.controlIndex);
								}
								if (!this.autoHide)
								{
									panel1.Width = this.width;
									panel1.Height = this.height;
								}
								else
								{
									panel1.AutoHide = this.autoHide;
									panel1.Collapsed = true;
									panel1.CollapsedSize = new Size(this.width, this.height);
								}
							}
							else if (this.floatingForm == null)
							{
								McFloatingForm form2 = panel1.FloatingForm;
								panel1.UndockControl(control1, new Rectangle(this.left, this.top, this.width, this.height), true, panel1.Parent);
								this.floatingForm = ((McDockingPanel) control1.Parent).FloatingForm;
								this.floatingForm.Width = this.width;
								this.floatingForm.Height = this.height;
								if (panel1.Controls.Count == 0)
								{
									this.UndockedPanels.Remove(panel1);
								}
							}
							else
							{
								McFloatingForm form3 = panel1.FloatingForm;
								panel1.DockControlToPanel((McDockingPanel) this.floatingForm.Controls[0]);
								if (panel1.Controls.Count == 0)
								{
									this.UndockedPanels.Remove(panel1);
								}
							}
							if ((this.controlIndex == this.selectedIndex) && (((McDockingPanel) control1.Parent).SelectedTab != control1))
							{
								((McDockingPanel) control1.Parent).SelectedTab = control1;
							}
						}
						else
						{
							panel1.DoClose(control1);
						}
					}
				}
				this.LoadNodes(tr);
			}
		}

		private void OnClosedUndockingPanels(object sender, EventArgs e)
		{
			foreach (McDockingPanel panel1 in this.UndockedPanels)
			{
				Form form1 = panel1.Parent as Form;
				if (form1 != null)
				{
					form1.Close();
					form1.Dispose();
				}
			}
			this.UndockedPanels.Clear();
		}

		internal void RemoveUndockingPanels(McDockingPanel dockingPanel)
		{
			this.UndockedPanels.Remove(dockingPanel);
		}

 
		public void SaveConfig(Stream stream)
		{
			XmlTextWriter writer1 = new XmlTextWriter(stream, Encoding.UTF8);
			writer1.Formatting = Formatting.Indented;
			writer1.WriteStartDocument(true);
			this.SaveConfig(writer1);
			writer1.WriteEndDocument();
			writer1.Flush();
		}

 
		public void SaveConfig(string file)
		{
			FileStream stream1 = new FileStream(file, FileMode.Create);
			this.SaveConfig(stream1);
			stream1.Flush();
			stream1.Close();
		}

		public void SaveConfig(XmlTextWriter tw)
		{
			tw.WriteStartElement("McDockingManagerConfig");
			foreach (McDockingPanel panel1 in this.DockedPanels)
			{
				if (panel1.Controls.Count > 0)
				{
					tw.WriteStartElement("Panel", panel1.Text);
					tw.WriteAttributeString("Floating", "False");
					tw.WriteAttributeString("Closing", "False");
					tw.WriteAttributeString("AutoHide", panel1.AutoHide.ToString());
					tw.WriteAttributeString("Dock", panel1.Dock.ToString());
					if (panel1.Collapsed)
					{
						tw.WriteAttributeString("Width", panel1.CollapsedSize.Width.ToString());
						tw.WriteAttributeString("Height", panel1.CollapsedSize.Height.ToString());
					}
					else
					{
						tw.WriteAttributeString("Width", panel1.Width.ToString());
						tw.WriteAttributeString("Height", panel1.Height.ToString());
					}
					tw.WriteAttributeString("SelectedIndex", panel1.Controls.IndexOf(panel1.SelectedTab).ToString());
					tw.WriteStartElement("Controls", panel1.Text);
					foreach (McDockingTab control1 in panel1.Controls)
					{
						tw.WriteElementString("Guid", XmlConvert.EncodeName(control1.Guid));
					}
					tw.WriteEndElement();
					tw.WriteEndElement();
				}
			}
			foreach (McDockingPanel panel2 in this.UndockedPanels)
			{
				if ((panel2.FloatingForm != null) && (panel2.Controls.Count > 0))
				{
					tw.WriteStartElement("Panel", panel2.Text);
					tw.WriteAttributeString("Floating", "True");
					tw.WriteAttributeString("Closing", "False");
					tw.WriteAttributeString("Left", panel2.FloatingForm.Left.ToString());
					tw.WriteAttributeString("Top", panel2.FloatingForm.Top.ToString());
					tw.WriteAttributeString("Width", panel2.FloatingForm.Width.ToString());
					tw.WriteAttributeString("Height", panel2.FloatingForm.Height.ToString());
					tw.WriteAttributeString("SelectedIndex", panel2.Controls.IndexOf(panel2.SelectedTab).ToString());
					tw.WriteStartElement("Controls", panel2.Text);
					foreach (McDockingTab control2 in panel2.Controls)
					{
						tw.WriteElementString("Guid", XmlConvert.EncodeName(control2.Guid));
					}
					tw.WriteEndElement();
					tw.WriteEndElement();
				}
			}
			if (this.ClosedControls.Count > 0)
			{
				tw.WriteStartElement("Panel");
				tw.WriteAttributeString("Floating", "False");
				tw.WriteAttributeString("Closing", "True");
				foreach (McDockingTab control3 in this.ClosedControls)
				{
					tw.WriteElementString("Guid", XmlConvert.EncodeName(control3.Guid));
				}
				tw.WriteEndElement();
			}
			tw.WriteEndElement();
		}

		internal void SetLayoutAllPanels()
		{
			if (this.ParentForm != null)
			{
				foreach (Control control1 in this.ParentForm.Controls)
				{
					if (control1 is McDockingPanel)
					{
						((McDockingPanel) control1).SetLayoutAllPanels();
						return;
					}
				}
			}
			foreach (McDockingPanel panel1 in this.UndockedPanels)
			{
				panel1.SetLayoutAllPanels();
				return;
			}
		}

 
		public void ShowUndockingPanels()
		{
			if (!this.ShowingUndocked)
			{
				foreach (McDockingPanel panel1 in this.UndockedPanels)
				{
					if (!panel1.FloatingForm.Visible)
					{
						this.ParentForm.AddOwnedForm(panel1.FloatingForm);
						panel1.FloatingForm.Show();
					}
				}
				this.ShowingUndocked = true;
			}
		}

		[Browsable(false)]
		public ArrayList ClosedControls
		{
			get
			{
                if (this.closedControls == null)
                {
                    this.closedControls = new ArrayList();
                }
                return this.closedControls;
			}
			set
			{
				this.closedControls = value;
			}
		}
		[Browsable(false)]
		public ArrayList DockedPanels
		{
			get
			{
				ArrayList list1 = new ArrayList();
				foreach (Control control1 in this.ParentForm.Controls)
				{
					if (control1 is McDockingPanel)
					{
						list1.Add(control1);
					}
				}
				return list1;
			}
		}
 
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		internal Form Form
		{
			get
			{
				return this.form;
			}
			set
			{
				if (((this.form == null) && (this.form != value)) && (value != null))
				{
					this.form = value;
					this.form.Closed += new EventHandler(this.OnClosedUndockingPanels);
				}
			}
		}
		[DefaultValue(false), Browsable(false)]
		public bool LockDockingPanels
		{
			get
			{
				return this.lockDockingPanels;
			}
			set
			{
				this.lockDockingPanels = value;
			}
		}
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		internal bool LockLayout
		{
			get
			{
				return this.lockLayout;
			}
			set
			{
				this.lockLayout = value;
			}
		}
		[Browsable(false)]
		public Form ParentForm
		{
			get
			{
				return this.parentForm;
			}
			set
			{
				this.parentForm = value;
			}
		}
 
		[DefaultValue(true), Browsable(false)]
		public bool ShowingUndocked
		{
			get
			{
				return this.showingUndocked;
			}
			set
			{
				this.showingUndocked = value;
			}
		}
 
		[Browsable(false)]
		public ArrayList UndockedPanels
		{
			get
			{
				return this.undockedPanels;
			}
			set
			{
				this.undockedPanels = value;
			}
		}
 




	}

}