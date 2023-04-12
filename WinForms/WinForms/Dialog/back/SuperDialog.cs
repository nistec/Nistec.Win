using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using mControl.WinCtl.Controls;
using mControl.Util;
 
namespace mControl.WinCtl.Dlg
{

	/// <summary>
    /// Summary description for SuperDialog.
	/// </summary>
	public class SuperDialog : mControl.WinCtl.Forms.CtlForm
	{

	#region contructor

        private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.Button cmdExit;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Timers.Timer timer1;
        private Label lblBottom;
        private Label LblMsg;
        private CtlTextBox Input;
		private System.ComponentModel.IContainer components;

		public SuperDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();


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
		#endregion

	#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SuperDialog));
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.lblBottom = new System.Windows.Forms.Label();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdExit = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.timer1 = new System.Timers.Timer();
            this.Input = new mControl.WinCtl.Controls.CtlTextBox();
            this.LblMsg = new System.Windows.Forms.Label();
            this.pnlFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timer1)).BeginInit();
            this.SuspendLayout();
            // 
            // caption
            // 
            this.caption.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.caption.Dock = System.Windows.Forms.DockStyle.Top;
            this.caption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.caption.Image = ((System.Drawing.Image)(resources.GetObject("caption.Image")));
            this.caption.ImageSizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.caption.Location = new System.Drawing.Point(4, 4);
            this.caption.Name = "caption";
            this.caption.ShowMaximize = false;
            this.caption.ShowMinimize = false;
            this.caption.StylePainter = this.StyleGuideBase;
            this.caption.SubText = "";
            this.caption.SubTitleColor = System.Drawing.SystemColors.ControlText;
            this.caption.Text = "SuperDialog";
            // 
            // StyleGuideBase
            // 
            this.StyleGuideBase.AlternatingColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(230)))), ((int)(((byte)(220)))));
            this.StyleGuideBase.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(106)))), ((int)(((byte)(197)))));
            this.StyleGuideBase.BorderHotColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(80)))), ((int)(((byte)(184)))));
            this.StyleGuideBase.CaptionColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(78)))), ((int)(((byte)(152)))));
            this.StyleGuideBase.ColorBrush1 = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(203)))), ((int)(((byte)(183)))));
            this.StyleGuideBase.ColorBrush2 = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.FlatColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.FocusedColor = System.Drawing.Color.RoyalBlue;
            this.StyleGuideBase.FormColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.StylePlan = mControl.WinCtl.Controls.Styles.Desktop;
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.Transparent;
            this.pnlFooter.Controls.Add(this.lblBottom);
            this.pnlFooter.Controls.Add(this.cmdOK);
            this.pnlFooter.Controls.Add(this.cmdExit);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(4, 72);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Padding = new System.Windows.Forms.Padding(2);
            this.pnlFooter.Size = new System.Drawing.Size(221, 20);
            this.pnlFooter.TabIndex = 7;
            // 
            // lblBottom
            // 
            this.lblBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBottom.Location = new System.Drawing.Point(34, 2);
            this.lblBottom.Name = "lblBottom";
            this.lblBottom.Size = new System.Drawing.Size(185, 16);
            this.lblBottom.TabIndex = 9;
            this.lblBottom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmdOK
            // 
            this.cmdOK.BackColor = System.Drawing.Color.Transparent;
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOK.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdOK.ForeColor = System.Drawing.Color.Transparent;
            this.cmdOK.Image = ((System.Drawing.Image)(resources.GetObject("cmdOK.Image")));
            this.cmdOK.Location = new System.Drawing.Point(18, 2);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(16, 16);
            this.cmdOK.TabIndex = 4;
            this.toolTip1.SetToolTip(this.cmdOK, "OK");
            this.cmdOK.UseVisualStyleBackColor = false;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.BackColor = System.Drawing.Color.Transparent;
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdExit.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmdExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdExit.ForeColor = System.Drawing.Color.Transparent;
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.Location = new System.Drawing.Point(2, 2);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(16, 16);
            this.cmdExit.TabIndex = 5;
            this.toolTip1.SetToolTip(this.cmdExit, "Cancel");
            this.cmdExit.UseVisualStyleBackColor = false;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            this.timer1.SynchronizingObject = this;
            this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Elapsed);
            // 
            // Input
            // 
            this.Input.AcceptsReturn = true;
            this.Input.BackColor = System.Drawing.Color.White;
            this.Input.Location = new System.Drawing.Point(4, 33);
            this.Input.Name = "Input";
            this.Input.Size = new System.Drawing.Size(192, 20);
            this.Input.TabIndex = 10;
            // 
            // LblMsg
            // 
            this.LblMsg.Location = new System.Drawing.Point(41, 56);
            this.LblMsg.Name = "LblMsg";
            this.LblMsg.Size = new System.Drawing.Size(140, 13);
            this.LblMsg.TabIndex = 11;
            this.LblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SuperDialog
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.CancelButton = this.cmdExit;
            this.ClientSize = new System.Drawing.Size(229, 96);
            this.ControlBox = false;
            this.ControlLayout = mControl.WinCtl.Controls.ControlLayout.System;
            this.Controls.Add(this.LblMsg);
            this.Controls.Add(this.Input);
            this.Controls.Add(this.pnlFooter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SuperDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SuperDialog";
            this.toolTip1.SetToolTip(this, "To Update Press Enter To Exit press Escape");
            this.Controls.SetChildIndex(this.caption, 0);
            this.Controls.SetChildIndex(this.pnlFooter, 0);
            this.Controls.SetChildIndex(this.Input, 0);
            this.Controls.SetChildIndex(this.LblMsg, 0);
            this.pnlFooter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.timer1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

    #region InputBox

        public static string OpenInputBox(Control parent, string DisplayValue, string msg, string Caption)
        {
            return OpenInputBox(parent, DisplayValue, msg, Caption, "",true,0);
        }

        public static string OpenInputBox(Control parent, string DisplayValue, string msg, string Caption, string Label,bool captionVisible,int width)
        {
            SuperDialog f = new SuperDialog();
            f.LblMsg.Visible = false;

            if (parent != null)
            {
                f.RightToLeft = parent.RightToLeft;
                Point pt = new Point(parent.Left, parent.Top - f.Height);
                f.Location = parent.PointToScreen(pt);

                if (parent is IStyleCtl)
                {
                    f.SetStyleLayout(((IStyleCtl)parent).CtlStyleLayout.Layout);
                    f.SetChildrenStyle();
                }
                //f.Width = parent.Width;
            }
            if (width > 0)
            {
                f.Width = width;
            }
            f.lblBottom.Text = Label;
            f.Text = Caption;
            f.LblMsg.Text = msg;
            f.Input.Text = DisplayValue;
            f.CaptionVisible = captionVisible;
            //f.SetRightToLeft();

            f.ShowDialog();
            if (f.DialogResult== DialogResult.OK)
                return f.Input.Text;
            return "";
        }


   
        #endregion

	#region Notify Dialog Static Function
 	
		public static DialogResult OpenDialog(string text,string strTitle,RightToLeft rtl,Control parent)
		{
			SuperDialog f=new SuperDialog();
			f.DialogOpen (text,strTitle,rtl,parent);
			return f.DialogResult; 			  
		}

		public static DialogResult OpenDialog(string text,string strTitle,Control parent)
		{
			SuperDialog f=new SuperDialog();
			f.DialogOpen(text,strTitle,RightToLeft.No,parent);
			return f.DialogResult; 			  
		}
		
		public static DialogResult OpenDialog(string text,string strTitle,RightToLeft rtl)
		{
			SuperDialog f=new SuperDialog();
			f.DialogOpen(text,strTitle,rtl,null);
			return f.DialogResult; 			  
		}

		public static DialogResult OpenDialog(string text,string strTitle)
		{
			SuperDialog f=new SuperDialog();
			f.DialogOpen(text,strTitle,RightToLeft.No,null);
			return f.DialogResult; 			  
		}

		#endregion

	#region Notify interval Static Function

		public static void OpenMsg(string text,string strTitle,double interval,RightToLeft rtl,Control parent)
		{
			SuperDialog f=new SuperDialog();
			f.MsgOpen(text,strTitle,interval,rtl,parent,false);
		}

		public static void OpenMsg(string text,string strTitle,double interval,RightToLeft rtl)
		{
			SuperDialog f=new SuperDialog();
			f.MsgOpen(text,strTitle,interval,rtl,null,false);
		}

		public static void OpenMsg(string text,string strTitle,double interval)
		{
			SuperDialog f=new SuperDialog();
			f.MsgOpen(text,strTitle,interval,RightToLeft.No,null,false);
		}

		public static void OpenMsg(string text,string strTitle)
		{
			SuperDialog f=new SuperDialog();
			f.MsgOpen(text,strTitle,DefaultInterval,RightToLeft.No,null,false);
		}

		public static void OpenMsg(string text,string strTitle,double interval,Control parent)
		{
			SuperDialog f=new SuperDialog();
			f.MsgOpen(text,strTitle,interval,RightToLeft.No,parent,false);
		}

		public static void OpenMsg(string text,string strTitle,Control parent)
		{
			SuperDialog f=new SuperDialog();
			f.MsgOpen(text,strTitle,DefaultInterval,RightToLeft.No,parent,false);
		}

		public static void OpenMsg(string text,string strTitle,RightToLeft rtl)
		{
			SuperDialog f=new SuperDialog();
			f.MsgOpen(text,strTitle,DefaultInterval,rtl,null,false);
		}

		#endregion

	#region MsgInfo interval Static Function

		public static void OpenInfo(string text,double interval,RightToLeft rtl,Control parent)
		{
			SuperDialog f=new SuperDialog();
			f.MsgOpen(text,"",interval,rtl,parent,true);
		}

		public static void OpenInfo(string text,double interval,RightToLeft rtl)
		{
			SuperDialog f=new SuperDialog();
			f.MsgOpen(text,"",interval,rtl,null,true);
		}

		public static void OpenInfo(string text,double interval)
		{
			SuperDialog f=new SuperDialog();
			f.MsgOpen(text,"",interval,RightToLeft.No,null,true);
		}

		public static void OpenInfo(string text)
		{
			SuperDialog f=new SuperDialog();
			f.MsgOpen(text,"",DefaultInterval,RightToLeft.No,null,true);
		}

		public static void OpenInfo(string text,double interval,Control parent)
		{
			SuperDialog f=new SuperDialog();
			f.MsgOpen(text,"",interval,RightToLeft.No,parent,true);
		}

		public static void OpenInfo(string text,Control parent)
		{
			SuperDialog f=new SuperDialog();
			f.MsgOpen(text,"",DefaultInterval,RightToLeft.No,parent,true);
		}

		public static void OpenInfo(string text,RightToLeft rtl)
		{
			SuperDialog f=new SuperDialog();
			f.MsgOpen(text,"",DefaultInterval,rtl,null,true);
		}

		#endregion

	#region Property

		public const int DefaultInterval=2000;
		private double mInterval=DefaultInterval;
		private bool IsTimer=false;

		public double TimeInterval
		{
			get{return mInterval;}
			set{mInterval=value;}
		}


		private void DialogOpen(string text,string strTitle,RightToLeft rtl,Control parent)
		{
		    SetMsg(text,strTitle,rtl,parent);
			IsTimer=false;
			this.ShowDialog() ;
		}

		private void MsgOpen(string text,string strTitle,double interval,RightToLeft rtl,Control parent,bool ShowInfo)
		{
			if(ShowInfo)
			{
                //Rect1.ControlLayout =ControlLayout.Flat;
                //Rect1.BorderStyle =BorderStyle.FixedSingle;
				//Rect1.BackColor=SystemColors.Info;// StyleCtl.FlatColor =SystemColors.Info;  
				this.StyleGuideBase.FlatColor=SystemColors.Info;
			}
//			else if(parent!=null)
//			{
//				this.Parent=parent;
//				if(parent is IStyleCtl)
//				{
//					base.SetStyleLayout(((IStyleCtl)parent).CtlStyleLayout.Layout);
//				}
//			}
            this.pnlFooter.Visible = (!ShowInfo); 
			this.CaptionVisible =(!ShowInfo); 
			SetMsg(text,strTitle,rtl,parent);
			IsTimer=true;
			this.timer1.Interval = interval;
			this.timer1.Start();
			this.Show();
		}

		private void SetMsg(string text,string strTitle,RightToLeft rtl,Control parent)
		{
			const int minWidth=200;
			const int minHeight=32;
            this.Input.Visible = false;

			int rows=GetRows(text);
			System.Drawing.Graphics  gr=this.CreateGraphics();
     
			Size Ssize=mControl.Drawing.TextUtils.GetTextSize(gr,text,LblMsg.Font);
			Size Tsize=GetTextRect(text.Length,Ssize,rows,LblMsg.Font.Height ); 
			System.Drawing.Rectangle rect=new Rectangle (0,0,Tsize.Width ,Tsize.Height ); 
			Size size=mControl.Drawing.TextUtils.GetTextSize(gr,text,LblMsg.Font,ref rect,mControl.Win32.DrawTextFormatFlags.DT_WORDBREAK  | mControl.Win32.DrawTextFormatFlags.DT_INTERNAL | mControl.Win32.DrawTextFormatFlags.DT_CALCRECT | mControl.Win32.DrawTextFormatFlags.DT_VCENTER  );


			if(size.Width < minWidth)
				this.Width =minWidth;
			else
				this.Width =size.Width ;

			if(size.Height < minHeight)
				this.Height=(this.Height -LblMsg.Height) + minHeight ; 
			else 
				this.Height=(this.Height -LblMsg.Height) + size.Height ; 

			RightToLeft Rtl=rtl;

		
			if(parent !=null)
			{
				//this.Parent=parent;
				if(parent is IStyleCtl)
				{
					//base.StyleGuideBase=((IStyleCtl)parent).StylePainter as StyleGuide;//.GetCurrentGuide();
					base.SetStyleLayout(((IStyleCtl)parent).CtlStyleLayout.Layout);
					base.SetChildrenStyle();
				}

				Rtl = parent.RightToLeft; 
				Point pt=new Point(parent.Left,parent.Top-this.Height );  
				if(parent is Form )
					this.Location =GetCenterScreen();// parent.PointToScreen (pt);
				else
					this.Location = parent.Parent.PointToScreen (pt);

				if(this.Location.Y < 50)
					this.Location=new Point (this.Location.X,50);
				else if(this.Location.X < 50)
					this.Location =new  Point (50,this.Location.Y);
				else if(this.Location.X + this.Size.Width  > Screen.PrimaryScreen.WorkingArea.Width )
					this.Location=GetCenterScreen();
				else if(this.Location.Y + this.Size.Height  > Screen.PrimaryScreen.WorkingArea.Height )
					this.Location=GetCenterScreen();
	
			}
			else
			{
       			this.Location=GetCenterScreen();
				//this.StartPosition  =System.Windows.Forms.FormStartPosition.CenterScreen;
			}

			this.LblMsg.RightToLeft = Rtl;; 
			//this.LblTitle.RightToLeft = Rtl;; 
             
			this.LblMsg.Text  = text; 
			//this.LblTitle.Text  = strTitle; 
			//this.TopLevel =true;
  
		}

		private Point GetCenterScreen()
		{
			int x= (Screen.PrimaryScreen.WorkingArea.Width-this.Width)/2;
			int y= (Screen.PrimaryScreen.WorkingArea.Height-this.Height)/2;
			return new Point(x,y);
		}

		private int GetRows(string text)
		{
			int len=text.Length;
			int rows=1;
			int k=0;
			int l=-1;
			for(int i=0;i<len;i++)
			{
				l=text.IndexOf ("\r\n",k);
				if(l > 0)
				{
					rows++;
					k=l+1;
					i=l+1;                 
				}
				else
					break;
			}
			return rows;
		}

		private Size GetTextRect(int len,Size textSize,int rn,int FontHeight)
		{
            Size size=textSize;
            int rowAdd=0;
			int rows=rn;
            
			if(size.Width < 300)
				rowAdd=0;  
			else if(size.Width < 1000)
				rowAdd=2;
			else if(size.Width < 5000)
				rowAdd=5; 
			else
				rowAdd=size.Width/1500;
 
			if(rowAdd>0)
             size.Width=textSize.Width /rowAdd;
			rows += rowAdd;
			size.Height =(rows*FontHeight)+10;
            
            return size; 
		}

//		public override void SetStyleLayout(StyleGuide sg)
//		{
//			base.SetStyleLayout(sg);
//			if(sg!=null)
//			{
//			    StyleLayout sgl=sg.GetStyleLayout();		
//				BackColor=sgl.FormColor;
//				Rect1.StyleCtl.StylePlan  =sgl.StylePlan ;
//				Rect2.StyleCtl.StylePlan  =sgl.StylePlan ;
//				Invalidate();
//			}
//		}

		#endregion

	#region Methods
	

		private void cmdOK_Click(object sender, System.EventArgs e)
		{
			DialogResult=DialogResult.OK; 
			this.Close ();
		}

		private void cmdExit_Click(object sender, System.EventArgs e)
		{
			DialogResult=DialogResult.No; 
			this.Close ();
		}

        //private void NotifyBox_SizeChanged(object sender, System.EventArgs e)
        //{
        // panel2.Width =Width-pnlFooter.Width;   
        //}

		private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			this.timer1.Stop();
			this.timer1.Dispose();
            if(IsTimer)
			  this.Close();
		}

		#endregion


	}//class
}//namespace

