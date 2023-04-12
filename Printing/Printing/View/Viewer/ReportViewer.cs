using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Nistec.Printing.View.Viewer;


	/// <summary>
	/// Summary description for PViewer.
	/// </summary>
public class ReportViewer : System.Windows.Forms.Form// Nistec.WinForms.FormBase
{
	public Nistec.Printing.View.Viewer.PrintViewer printViewer;
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.Container components = null;

    public ReportViewer()
	{
		//
		// Required for Windows Form Designer support
		//
		InitializeComponent();
		this.printViewer.owner=this;
	}

	/// <summary>
	/// Clean up any resources being used.
	/// </summary>
	protected override void Dispose( bool disposing )
	{
		if( disposing )
		{
			if(components != null)
			{
				components.Dispose();
			}
		}
		base.Dispose( disposing );
	}

	#region Windows Form Designer generated code
	/// <summary>
	/// Required method for Designer support - do not modify
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{
		this.printViewer = new Nistec.Printing.View.Viewer.PrintViewer();
		this.SuspendLayout();
		// 
		// printViewer1
		// 
		this.printViewer.BackColor = System.Drawing.SystemColors.ControlDark;
		this.printViewer.Dock = System.Windows.Forms.DockStyle.Fill;
		this.printViewer.Document = null;
		this.printViewer.Location = new System.Drawing.Point(0, 0);
		this.printViewer.Name = "printViewer1";
		this.printViewer.Size = new System.Drawing.Size(624, 421);
		this.printViewer.TabIndex = 0;
		// 
		// PrintViewer
		// 
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
		this.BackColor = System.Drawing.SystemColors.Control;
		this.ClientSize = new System.Drawing.Size(624, 421);
		this.Controls.Add(this.printViewer);
		this.Name = "PrintViewer";
		this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Nistec ReportViewer";
		this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		this.ResumeLayout(false);

	}
	#endregion
	
	public static void Preview(Nistec.Printing.View.Report report)
	{

        ReportViewer _frmviewer = new ReportViewer();
		_frmviewer.printViewer.Document = report.Document; 
		_frmviewer.Show();

	}

	public static void PreviewDialog(Nistec.Printing.View.Report report)
	{

        ReportViewer _frmviewer = new ReportViewer();
		_frmviewer.printViewer.Document = report.Document; 
		_frmviewer.ShowDialog();

	}

}

