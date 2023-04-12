namespace SyntaxEditorTest
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            //MControl.SyntaxEditor.Document.DefaultSyntaxProperties defaultSyntaxProperties1 = new MControl.SyntaxEditor.Document.DefaultSyntaxProperties();
            this.ctlSyntaxEditor1 = new MControl.SyntaxEditor.TextEditor();
            this.SuspendLayout();
            // 
            // ctlSyntaxEditor1
            // 
            this.ctlSyntaxEditor1.Encoding = null;
            this.ctlSyntaxEditor1.Location = new System.Drawing.Point(23, 23);
            this.ctlSyntaxEditor1.Name = "ctlSyntaxEditor1";
            //this.ctlSyntaxEditor1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ctlSyntaxEditor1.Size = new System.Drawing.Size(489, 604);
            this.ctlSyntaxEditor1.PropertyPressed += new MControl.SyntaxEditor.IntelliListEventHandler(ctlSyntaxEditor1_PropertyPressed);
            //this.ctlSyntaxEditor1.IntelliPressed += new MControl.SyntaxEditor.IntelliListEventHandler(ctlSyntaxEditor1_IntelliPressed);

            //defaultSyntaxProperties1.AllowCaretBeyondEOL = false;
            //defaultSyntaxProperties1.AutoInsertCurlyBracket = true;
            //defaultSyntaxProperties1.BracketMatchingStyle = MControl.SyntaxEditor.Document.BracketMatchingStyle.After;
            //defaultSyntaxProperties1.ConvertTabsToSpaces = false;
            //defaultSyntaxProperties1.CreateBackupCopy = false;
            //defaultSyntaxProperties1.DocumentSelectionMode = MControl.SyntaxEditor.Document.DocumentSelectionMode.Normal;
            //defaultSyntaxProperties1.EnableFolding = true;
            //defaultSyntaxProperties1.Encoding = null;
            //defaultSyntaxProperties1.Font = new System.Drawing.Font("Courier New", 10F);
            //defaultSyntaxProperties1.HideMouseCursor = false;
            //defaultSyntaxProperties1.IndentStyle = MControl.SyntaxEditor.Document.IndentStyle.Smart;
            //defaultSyntaxProperties1.IsIconBarVisible = true;
            //defaultSyntaxProperties1.LineTerminator = "\r\n";
            //defaultSyntaxProperties1.LineViewerStyle = MControl.SyntaxEditor.Document.LineViewerStyle.None;
            //defaultSyntaxProperties1.MouseWheelScrollDown = true;
            //defaultSyntaxProperties1.MouseWheelTextZoom = true;
            //defaultSyntaxProperties1.ShowEOLMarker = false;
            //defaultSyntaxProperties1.ShowHorizontalRuler = false;
            //defaultSyntaxProperties1.ShowInvalidLines = false;
            //defaultSyntaxProperties1.ShowLineNumbers = true;
            //defaultSyntaxProperties1.ShowMatchingBracket = true;
            //defaultSyntaxProperties1.ShowSpaces = false;
            //defaultSyntaxProperties1.ShowTabs = false;
            //defaultSyntaxProperties1.ShowVerticalRuler = false;
            //defaultSyntaxProperties1.TabIndent = 4;
            //defaultSyntaxProperties1.UseAntiAliasedFont = false;
            //defaultSyntaxProperties1.VerticalRulerRow = 80;
            //this.ctlSyntaxEditor1.SyntaxProperties = defaultSyntaxProperties1;
            //this.ctlSyntaxEditor1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 351);
            this.Controls.Add(this.ctlSyntaxEditor1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        void ctlSyntaxEditor1_IntelliPressed(object sender, MControl.SyntaxEditor.IntelliListEventArgs e)
        {
        }

        void ctlSyntaxEditor1_PropertyPressed(object sender, MControl.SyntaxEditor.IntelliListEventArgs e)
        {
            string s = e.Word;
            e.Items.AddRange(new string[] {"abc","dfg" },1);
        }

        #endregion

        private MControl.SyntaxEditor.TextEditor ctlSyntaxEditor1;
    }
}

