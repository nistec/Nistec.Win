namespace Nistec.WinForms
{
    partial class FormBase
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
            //if (acmd != null)
            //{
            //    UnWireAsyncCmd();
            //}
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBase));
            //this.ctlResize = new Nistec.WinForms.McResize(this);
            this.StyleGuideBase = new Nistec.WinForms.StyleGuide(this.components);
            //((System.ComponentModel.ISupportInitialize)(this.ctlResize)).BeginInit();
            this.SuspendLayout();
            //// 
            //// ctlResize
            //// 
            //this.ctlResize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            //this.ctlResize.BackColor = System.Drawing.Color.Transparent;
            //this.ctlResize.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            //this.ctlResize.ForeColor = System.Drawing.SystemColors.ControlText;
            //this.ctlResize.Location = new System.Drawing.Point(276, 256);
            //this.ctlResize.Name = "ctlResize";
            //this.ctlResize.ReadOnly = false;
            //this.ctlResize.Size = new System.Drawing.Size(20, 20);
            //this.ctlResize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            //this.ctlResize.TabIndex = 7;
            //this.ctlResize.TabStop = false;
            //this.ctlResize.Visible = false;
            // 
            // StyleGuideBase
            // 
            this.StyleGuideBase.AlternatingColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.StyleGuideBase.BorderColor = System.Drawing.SystemColors.Desktop;
            this.StyleGuideBase.BorderHotColor = System.Drawing.SystemColors.HotTrack;
            this.StyleGuideBase.CaptionColor = System.Drawing.SystemColors.ActiveCaption;
            this.StyleGuideBase.CaptionFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StyleGuideBase.ColorBrush1 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.StyleGuideBase.ColorBrush2 = System.Drawing.Color.White;
            this.StyleGuideBase.DisableColor = System.Drawing.Color.DarkGray;
            this.StyleGuideBase.BackgroundColor = System.Drawing.Color.White;
            this.StyleGuideBase.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(74)))), ((int)(((byte)(74)))));
            this.StyleGuideBase.FormColor = System.Drawing.Color.White;
            this.StyleGuideBase.StylePlan = Nistec.WinForms.Styles.Custom;
            this.StyleGuideBase.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.StyleGuideBase.ColorStyleChanged += new Nistec.WinForms.ColorStyleChangedEventHandler(this.StyleGuideBase_ColorStyleChanged);
            this.StyleGuideBase.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(this.StyleGuideBase_PropertyChanged);
            // 
            // FormBase
            // 
            //this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            //this.ClientSize = new System.Drawing.Size(292, 273);
            //this.Controls.Add(this.ctlResize);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormBase";
            this.Text = "McForm";
            //((System.ComponentModel.ISupportInitialize)(this.ctlResize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion


    }
}