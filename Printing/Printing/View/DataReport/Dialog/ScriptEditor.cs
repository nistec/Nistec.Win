using System;
using System.Windows.Forms;
using System.Drawing;
using System.Text.RegularExpressions;
using System.CodeDom.Compiler;

namespace Nistec.Printing.View
{
	public class ScriptEditor : System.Windows.Forms.Form// Nistec.WinForms.FormBase
	{
		// Fields
		private System.ComponentModel.Container components = null;
		private CodeProvider var6;
		private bool var7;
        internal ScriptLanguage scriptLanguage;//mtd359;
		internal bool mtd4;
		internal Button cbCancel;
		internal Button cbCompile;
		internal Button cbOk;
		internal ComboBox cbxEvent;
		internal ComboBox cbxLang;
		internal ComboBox cbxObject;
		internal ColumnHeader ColumnHeader3;
		internal ColumnHeader ColumnHeader4;
		//private Container components;
		internal Label lblEvent;
		internal Label lblLang;
		internal Label lblObject;
		internal ListView lvErrors;
		internal RichTextBox rtfScript;

		public ScriptEditor()
		{
			this.components = null;
			this.var6 = new CodeProvider();
			this.var7 = true;
			this.InitializeComponent();
		}

		internal void mtd172(string[] var8, ref ScriptLanguage var9, ref string var10)
		{
			if (var9 == ScriptLanguage.VB)
			{
                this.scriptLanguage = ScriptLanguage.VB;
			}
			else
			{
                this.scriptLanguage = ScriptLanguage.CSharp;
			}
			this.rtfScript.Text = var10;
			//this.cbxLang.BeginUpdate();
			this.cbxLang.Items.Add("VB");
			this.cbxLang.Items.Add("C#");
            this.cbxLang.SelectedIndex = (int)this.scriptLanguage;
			//this.cbxLang.EndUpdate();
			//this.cbxObject.BeginUpdate();
			this.cbxObject.Items.Add("Report");
			for (int num1 = 0; num1 <= (var8.Length - 1); num1++)
			{
				this.cbxObject.Items.Add(var8[num1]);
			}
			this.cbxObject.SelectedIndex = 0;
			//this.cbxObject.EndUpdate();
			this.var11();
		}

 
		protected override void Dispose(bool disposing)
		{
			if (disposing && (this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

 
		private void InitializeComponent()
		{
			this.rtfScript = new System.Windows.Forms.RichTextBox();
			this.lvErrors = new ListView();
			this.ColumnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.ColumnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.cbCompile = new Button();
			this.cbCancel = new Button();
			this.cbOk = new Button();
			this.lblLang = new System.Windows.Forms.Label();
			this.lblEvent = new System.Windows.Forms.Label();
			this.lblObject = new System.Windows.Forms.Label();
			this.cbxLang = new ComboBox();
            this.cbxEvent = new ComboBox();
            this.cbxObject = new ComboBox();
			this.SuspendLayout();
			// 
			// rtfScript
			// 
			this.rtfScript.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.rtfScript.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.rtfScript.Location = new System.Drawing.Point(9, 39);
			this.rtfScript.Name = "rtfScript";
			this.rtfScript.Size = new System.Drawing.Size(558, 264);
			this.rtfScript.TabIndex = 23;
			this.rtfScript.Text = "";
			this.rtfScript.WordWrap = false;
			// 
			// lvErrors
			// 
			this.lvErrors.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lvErrors.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																					   this.ColumnHeader3,
																					   this.ColumnHeader4});
			this.lvErrors.FullRowSelect = true;
			this.lvErrors.GridLines = true;
			this.lvErrors.Location = new System.Drawing.Point(9, 311);
			this.lvErrors.MultiSelect = false;
			this.lvErrors.Name = "lvErrors";
			this.lvErrors.Size = new System.Drawing.Size(558, 96);
			this.lvErrors.TabIndex = 22;
			this.lvErrors.View = System.Windows.Forms.View.Details;
			// 
			// ColumnHeader3
			// 
			this.ColumnHeader3.Text = "Error";
			this.ColumnHeader3.Width = 500;
			// 
			// ColumnHeader4
			// 
			this.ColumnHeader4.Text = "Line";
			this.ColumnHeader4.Width = 50;
			// 
			// cbCompile
			// 
			this.cbCompile.Location = new System.Drawing.Point(496, 416);
			this.cbCompile.Name = "cbCompile";
			this.cbCompile.Size = new System.Drawing.Size(72, 24);
			this.cbCompile.TabIndex = 21;
			this.cbCompile.Text = "Compile";
			this.cbCompile.Click += new System.EventHandler(this.var0);
			// 
			// cbCancel
			// 
			this.cbCancel.Location = new System.Drawing.Point(416, 416);
			this.cbCancel.Name = "cbCancel";
			this.cbCancel.Size = new System.Drawing.Size(72, 24);
			this.cbCancel.TabIndex = 20;
			this.cbCancel.Text = "Cancel";
			this.cbCancel.Click += new System.EventHandler(this.var1);
			// 
			// cbOk
			// 
			this.cbOk.Location = new System.Drawing.Point(336, 416);
			this.cbOk.Name = "cbOk";
			this.cbOk.Size = new System.Drawing.Size(72, 24);
			this.cbOk.TabIndex = 19;
			this.cbOk.Text = "OK";
			this.cbOk.Click += new System.EventHandler(this.var2);
			// 
			// lblLang
			// 
			this.lblLang.Location = new System.Drawing.Point(448, 11);
			this.lblLang.Name = "lblLang";
			this.lblLang.Size = new System.Drawing.Size(64, 16);
			this.lblLang.TabIndex = 18;
			this.lblLang.Text = "Language :";
			// 
			// lblEvent
			// 
			this.lblEvent.Location = new System.Drawing.Point(193, 11);
			this.lblEvent.Name = "lblEvent";
			this.lblEvent.Size = new System.Drawing.Size(40, 16);
			this.lblEvent.TabIndex = 17;
			this.lblEvent.Text = "Event :";
			// 
			// lblObject
			// 
			this.lblObject.Location = new System.Drawing.Point(9, 11);
			this.lblObject.Name = "lblObject";
			this.lblObject.Size = new System.Drawing.Size(48, 16);
			this.lblObject.TabIndex = 16;
			this.lblObject.Text = "Object :";
			// 
			// cbxLang
			// 
			this.cbxLang.Location = new System.Drawing.Point(512, 7);
			this.cbxLang.Name = "cbxLang";
			this.cbxLang.Size = new System.Drawing.Size(56, 21);
			this.cbxLang.TabIndex = 15;
			this.cbxLang.SelectedIndexChanged += new System.EventHandler(this.var3);
			// 
			// cbxEvent
			// 
			this.cbxEvent.Location = new System.Drawing.Point(241, 7);
			this.cbxEvent.Name = "cbxEvent";
			this.cbxEvent.Size = new System.Drawing.Size(128, 21);
			this.cbxEvent.TabIndex = 14;
			this.cbxEvent.SelectedValueChanged += new System.EventHandler(this.var4);
			// 
			// cbxObject
			// 
			this.cbxObject.Location = new System.Drawing.Point(57, 7);
			this.cbxObject.Name = "cbxObject";
			this.cbxObject.Size = new System.Drawing.Size(128, 21);
			this.cbxObject.TabIndex = 13;
			this.cbxObject.SelectedIndexChanged += new System.EventHandler(this.var5);
			// 
			// ScriptEditor
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(576, 447);
			this.Controls.Add(this.rtfScript);
			this.Controls.Add(this.lvErrors);
			this.Controls.Add(this.cbCompile);
			this.Controls.Add(this.cbCancel);
			this.Controls.Add(this.cbOk);
			this.Controls.Add(this.lblLang);
			this.Controls.Add(this.lblEvent);
			this.Controls.Add(this.lblObject);
			this.Controls.Add(this.cbxLang);
			this.Controls.Add(this.cbxEvent);
			this.Controls.Add(this.cbxObject);
			this.Name = "ScriptEditor";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ScriptEditor";
			this.ResumeLayout(false);

		}

 
		private void var0(object var12, EventArgs e)
		{
            this.var6.ScriptLanguage = this.scriptLanguage;
            this.var6.Text = this.rtfScript.Text;
            if (!this.var6.mtd284(false))
			{
                foreach (CompilerError error1 in this.var6.ErrorCollection)
				{
					ListViewItem item1 = new ListViewItem(error1.ErrorText);
					int num1 = error1.Line - 2;
					item1.SubItems.Add(num1.ToString());
					this.lvErrors.Items.Add(item1);
				}
				MessageBox.Show("Compile Failed", "ScriptEngine", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			else
			{
				this.lvErrors.Items.Clear();
				MessageBox.Show("Compile Succeded", "ScriptEngine", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}

		private void var1(object var12, EventArgs e)
		{
			base.Close();
		}

		private void var11()
		{
			//this.cbxEvent.BeginUpdate();
			this.cbxEvent.Items.Clear();
			if (this.var7)
			{
				this.cbxEvent.Items.Add("Start");
				this.cbxEvent.Items.Add("End");
				this.cbxEvent.Items.Add("DataInitialize");
				this.cbxEvent.Items.Add("FetchData");
				this.cbxEvent.Items.Add("NoData");
			}
			else
			{
				this.cbxEvent.Items.Add("Initialize");
				this.cbxEvent.Items.Add("OnPaint");
			}
			this.cbxEvent.Text = "";
			this.cbxEvent.SelectedItem = null;
			//this.cbxEvent.EndUpdate();
		}

		private void var2(object var12, EventArgs e)
		{
			this.mtd4 = true;
			base.Close();
		}

 
		private void var3(object sender, EventArgs e)
		{
			if (this.cbxLang.SelectedIndex == 0)
			{
                this.scriptLanguage = ScriptLanguage.VB;
			}
			else
			{
                this.scriptLanguage = ScriptLanguage.CSharp;
			}
		}

		private void var4(object var12, EventArgs e)
		{
			string text2;
			if (!this.var7)
			{
				if ((text2 = this.cbxEvent.Text) != null)
				{
					string text1;
					text2 = string.IsInterned(text2);
					if (text2 != "Initialize")
					{
						if (text2 == "OnPaint")
						{
							text1 = this.cbxObject.Text + "_OnPaint";
							if (!Regex.IsMatch(this.rtfScript.Text, "Report_End"))
							{
                                if (this.scriptLanguage == ScriptLanguage.VB)
								{
									this.rtfScript.AppendText(string.Concat(new object[] { '\r', "Public Sub ", text1, "(ByVal Sender as Object, ByVal e as System.EventArgs)", '\r', '\r', "End Sub", '\r' }));
								}
								else
								{
									this.rtfScript.AppendText(string.Concat(new object[] { '\r', "public void ", text1, "(object Sender, System.EventArgs e)", '\r', "{", '\r', '\r', "}", '\r' }));
								}
							}
						}
					}
					else
					{
						text1 = this.cbxObject.Text + "_Initialize";
						if (Regex.IsMatch(this.rtfScript.Text, text1))
						{
							return;
						}
                        if (this.scriptLanguage == ScriptLanguage.VB)
						{
							this.rtfScript.AppendText(string.Concat(new object[] { '\r', "Public Sub ", text1, "(ByVal Sender as Object, ByVal e as System.EventArgs)", '\r', '\r', "End Sub", '\r' }));
						}
						else
						{
							this.rtfScript.AppendText(string.Concat(new object[] { '\r', "public void ", text1, "(object Sender, System.EventArgs e)", '\r', "{", '\r', '\r', "}", '\r' }));
						}
					}
				}
			}
			else if ((text2 = this.cbxEvent.Text) != null)
			{
				text2 = string.IsInterned(text2);
				if (text2 != "Start")
				{
					if (text2 == "End")
					{
						if (!Regex.IsMatch(this.rtfScript.Text, "Report_End"))
						{
                            if (this.scriptLanguage == ScriptLanguage.VB)
							{
								this.rtfScript.AppendText(string.Concat(new object[] { '\r', "Public Sub Report_End(ByVal Sender as Object, ByVal e as System.EventArgs)", '\r', '\r', "End Sub", '\r' }));
							}
							else
							{
								this.rtfScript.AppendText(string.Concat(new object[] { '\r', "public void Report_End(object Sender, System.EventArgs e)", '\r', "{", '\r', '\r', "}", '\r' }));
							}
						}
					}
					else if (text2 == "DataInitialize")
					{
						if (!Regex.IsMatch(this.rtfScript.Text, "Report_DataInitialize"))
						{
                            if (this.scriptLanguage == ScriptLanguage.VB)
							{
								this.rtfScript.AppendText(string.Concat(new object[] { '\r', "Public Sub Report_DataInitialize(ByVal Sender as Object, ByVal e as System.EventArgs)", '\r', '\r', "End Sub", '\r' }));
							}
							else
							{
								this.rtfScript.AppendText(string.Concat(new object[] { '\r', "public void Report_DataInitialize(object Sender, System.EventArgs e)", '\r', "{", '\r', '\r', "}", '\r' }));
							}
						}
					}
					else if (text2 == "FetchData")
					{
						if (!Regex.IsMatch(this.rtfScript.Text, "Report_FetchData"))
						{
                            if (this.scriptLanguage == ScriptLanguage.VB)
							{
								this.rtfScript.AppendText(string.Concat(new object[] { '\r', "Public Sub Report_FetchData(ByVal Sender as Object, ByVal e as System.EventArgs)", '\r', '\r', "End Sub", '\r' }));
							}
							else
							{
								this.rtfScript.AppendText(string.Concat(new object[] { '\r', "public void Report_FetchData(object Sender, System.EventArgs e)", '\r', "{", '\r', '\r', "}", '\r' }));
							}
						}
					}
					else if ((text2 == "NoData") && !Regex.IsMatch(this.rtfScript.Text, "Report_NoData"))
					{
                        if (this.scriptLanguage == ScriptLanguage.VB)
						{
							this.rtfScript.AppendText(string.Concat(new object[] { '\r', "Public Sub Report_NoData(ByVal Sender as Object, ByVal e as System.EventArgs)", '\r', '\r', "End Sub", '\r' }));
						}
						else
						{
							this.rtfScript.AppendText(string.Concat(new object[] { '\r', "public void Report_NoData(object Sender, System.EventArgs e)", '\r', "{", '\r', '\r', "}", '\r' }));
						}
					}
				}
				else
				{
					if (Regex.IsMatch(this.rtfScript.Text, "Report_Start"))
					{
						return;
					}
                    if (this.scriptLanguage == ScriptLanguage.VB)
					{
						this.rtfScript.AppendText(string.Concat(new object[] { '\r', "Public Sub Report_Start(ByVal Sender as Object, ByVal e as System.EventArgs)", '\r', '\r', "End Sub", '\r' }));
					}
					else
					{
						this.rtfScript.AppendText(string.Concat(new object[] { '\r', "public void Report_Start(object Sender, System.EventArgs e)", '\r', "{", '\r', '\r', "}", '\r' }));
					}
				}
			}
		}

		private void var5(object sender, EventArgs e)
		{
			if ((this.cbxObject.SelectedIndex > 0) & this.var7)
			{
				this.var7 = false;
				this.var11();
			}
			else if ((this.cbxObject.SelectedIndex == 0) & !this.var7)
			{
				this.var7 = true;
				this.var11();
			}
		}


	}

}
