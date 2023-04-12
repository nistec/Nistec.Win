using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Nistec.Drawing;
using Nistec.WinForms.Controls;


namespace Nistec.WinForms
{
	//[Designer(typeof(NavigatoreDesigner))]
	[ToolboxItem(false)]
	public class McNavChanges :McContainer
	{

		#region Members
		private System.ComponentModel.IContainer components = null;
	
		private Nistec.WinForms.Controls.McButtonCombo btn1;
		private Nistec.WinForms.Controls.McButtonCombo btn2;

		//private bool					IsMouseDown;
		private System.Windows.Forms.ToolTip toolTip;

		[Category("Action")]
		public event ButtonClickEventHandler ClickAccept;
		public event ButtonClickEventHandler ClickReject;


		#endregion

		#region Constructors

		public McNavChanges():base()
		{
		   InitializeComponent();
		   SetStyle(ControlStyles.SupportsTransparentBackColor ,true);
		  // this.BackColor =Color.Transparent; 
			//this.BringToFront();
		}

		#endregion

		#region Dispose

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

		#region Component Designer generated code
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			//System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(McChanges));
			this.btn1 = new McButtonCombo(this,"BtnAccept.gif");
			this.btn2 = new McButtonCombo(this,"BtnReject.gif");
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// btn1
			// 
			//this.btn1.Image = ((System.Drawing.Image)(resources.GetObject("btn1.Image")));
			this.btn1.Location = new System.Drawing.Point(0, 0);
			this.btn1.Name = "btn1";
			this.btn1.Size = new System.Drawing.Size(18, 18);
			this.btn1.TabIndex = 1;
			this.toolTip.SetToolTip(this.btn1, "Accept Changes");
			this.btn1.Click +=new EventHandler(this.btn1_ButtonClick);
			// 
			// btn2
			// 
			//this.btn2.Image = ((System.Drawing.Image)(resources.GetObject("btn2.Image")));
			this.btn2.Location = new System.Drawing.Point(20, 0);
			this.btn2.Name = "btn2";
			this.btn2.Size = new System.Drawing.Size(18, 18);
			this.btn2.TabIndex = 2;
			this.toolTip.SetToolTip(this.btn2, "Reject Changes");
			this.btn2.Click +=new EventHandler(this.btn2_ButtonClick);
			// 
			// McChanges
			// 
			this.Controls.Add(this.btn1);
			this.Controls.Add(this.btn2);
			this.EnabledChanged +=new EventHandler(panel1_EnabledChanged); 
			//this.BackColor = System.Drawing.SystemColors.Control;
			this.Name = "McChanges";
			this.Size = new System.Drawing.Size(38, 20);
			this.ResumeLayout(false);

		}
		#endregion

		#region Events handlers

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated (e);
	
			base.BackColor = Color.Transparent ; 
		}

		protected override void OnSizeChanged(System.EventArgs e)
		{
			base.OnSizeChanged(e);
			SetSize();
		}

		protected void SetSize()
		{
	        this.Width=this.btn1.Width + this.btn2.Width + 6;
			//this.Height =DefHeight;
			if(this.Height < Defaults.minHeight)
                  this.Height = Defaults.minHeight;
		
			this.btn1.Height   =this.Height ;
			this.btn2.Height   =this.Height ;
			//SetLocation();
			base.RecreateHandle();
			if(this.DesignMode)
				this.Refresh();

		}

//		internal void SetLocation(RightToLeft value)
//		{
//			this.RightToLeft =value;
//			base.RecreateHandle();
//			if(this.DesignMode)
//				this.Refresh();
//		}


		#endregion

		#region Overrides

			protected override void OnEnabledChanged(System.EventArgs e)
		{
			base.OnEnabledChanged(e);
			EnableButtons=this.Enabled;
 		}

		#endregion

		#region Event Click

		private void panel1_EnabledChanged(object sender, EventArgs e)
		{
			OnEnabledChanged (e);   
		}

		private void btn1_ButtonClick(object sender, System.EventArgs e)
		{
			OnButtonAcceptClick(e);
		}

		private void btn2_ButtonClick(object sender, System.EventArgs e)
		{
			OnButtonRejectClick(e);
		}

		protected virtual void OnButtonAcceptClick(EventArgs e) 
		{
			if(this.ClickAccept != null && this.Enabled  )
                this.ClickAccept(this, new ButtonClickEventArgs("Accept"));
	
		}

		protected virtual void OnButtonRejectClick(EventArgs e) 
		{
			if(this.ClickReject != null && this.Enabled  )
                this.ClickReject(this, new ButtonClickEventArgs("Reject"));
	
		}

		#endregion

		#region Methods

		protected bool IsMouseInButtonRect()
		{
			Rectangle rectButton = GetButtonRect;
			Point mPos = Control.MousePosition;
			if(rectButton.Contains(this.PointToClient(mPos)))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		#endregion 

		#region Properties

		[Browsable(false),EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool AutoChildrenStyle
		{
			get{return base.AutoChildrenStyle;}
			set
			{
				base.AutoChildrenStyle=value;
			}
		}

	   [Browsable(false)]
		public Rectangle GetButtonRect
		{
			get 
			{
				return new Rectangle(0 ,0,Width ,Height );
			}
		}

		[Category("McButton"),DefaultValue(true)]
        [RefreshProperties(RefreshProperties.All )]  
		public bool EnableButtons
		{
			get {return btn1.Enabled ;}
			set 
			{
				btn1.Enabled = value;
				btn2.Enabled = value;
				this.Invalidate();
			}
		}

		#endregion

	}
}
