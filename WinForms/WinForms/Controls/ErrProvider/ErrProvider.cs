using System;
using System.Windows.Forms;
using System.ComponentModel;
using mControl.Util;

namespace mControl.WinCtl.Controls
{

	[TypeConverter(typeof(ErrProviderConverter))]
	public class ErrProvider
	{
		#region Members
		private mControl.WinCtl.Controls.CtlErrorProvider m_ErrorProvider;
		
		private ErrProviders m_provider;
		private bool m_IsErrorOn;
		private bool m_ShowErrorProvider;
		private System.Windows.Forms.Timer errTimer;
		private int m_TimerInterval;
		private Control Ctl;
		string m_ErrorMessage;
        string m_Caption = "Error Provider";

		public event ErrorOcurredEventHandler ErrorOcurred;
		#endregion

		#region Constructor
		public ErrProvider(Control ctl)
		{
			//m_ErrorProvider=new CtlErrorProvider(ctl as ContainerControl);
			Ctl=ctl;
			m_provider=ErrProviders.ErrIcon;
			m_ShowErrorProvider=false;
		}

		public ErrProvider(Control ctl,ErrProviders provider)
		{
			//m_ErrorProvider=new CtlErrorProvider(ctl as ContainerControl);
			Ctl=ctl;
			m_provider=provider;
			m_ShowErrorProvider=false;
		}

		private void initErrTimer()
		{
			m_TimerInterval=5000;
			errTimer = new System.Windows.Forms.Timer();
			errTimer.Enabled = false;
			errTimer.Tick   += new System.EventHandler(OnTimerTick);

		}
		
		#endregion

		#region Timer

		private void OnTimerTick(object sender, EventArgs e)
		{
      		ReleaseTimer();
		}

		private void StartTimer()
		{
			errTimer.Start();
		}

		private void SetUpTimer()
		{
			//m_ptr=IntPtr.Zero; 
			//m_ptr= mControl.Win32.WinAPI.GetActiveWindow();
			errTimer.Interval =this.m_TimerInterval;
			errTimer.Enabled = true;
		}

		private void ReleaseTimer()
		{
			//m_ptr=IntPtr.Zero;
			errTimer.Stop (); 
			errTimer.Enabled = false;
			ResetError(Ctl);
		}

		#endregion

		#region Static Methods
	
		public static void  ShowError(Control ctl,ErrProviders provider,string msg)
		{
			ErrProvider err=new ErrProvider( ctl, provider);
			err.m_ShowErrorProvider=true;
			if(provider==ErrProviders.ErrIcon)
			{
				err.initErrTimer();
				err.SetUpTimer();
				err.SetError(ctl,msg);
				err.StartTimer();
			}
			else
			{
				err.SetError(ctl,msg);
			}
		}

		public static void  ShowError(Control ctl,ErrProviders provider,string msg,int timeInterval)
		{
			ErrProvider err=new ErrProvider( ctl, provider);
			err.m_ShowErrorProvider=true;
			err.m_TimerInterval=timeInterval;
			err.initErrTimer();
			err.SetUpTimer();
			err.SetError(ctl,msg);
			err.StartTimer();
		}

		#endregion

		#region Properties

		[Browsable(false)]
		public System.Windows.Forms.IContainerControl ContainerControl
		{
			get{return (IContainerControl) Ctl;}//m_ErrorProvider.ContainerControl;}
		}

		[DefaultValue(ErrProviders.ErrIcon)]
		public ErrProviders Provider
		{
			get{return this.m_provider;}
			set{this.m_provider=value;}
		}

		public bool IsErrorOn
		{
			get{return this.m_IsErrorOn;}
		}
		
		[DefaultValue(false)]
		public bool ShowErrorProvider
		{
			get{return this.m_ShowErrorProvider;}
			set{this.m_ShowErrorProvider=value;}
		}

		[DefaultValue("")]
		public string ErrorMessage
		{
			get{return m_ErrorMessage;}
			set{m_ErrorMessage=value;}
		}

        [DefaultValue("Error Provider")]
        public string Caption
        {
            get { return m_Caption; }
            set { m_Caption = value; }
        }

		#endregion

		#region Methods

		public void SetError()
		{
          SetError(Ctl,m_ErrorMessage); 
		}

		public void SetError(string value)
		{
			SetError(Ctl,value); 
		}

		public void SetError(Control ctl,string value)
		{
			//if(m_ShowErrorProvider)
			//{
                
			   //m_ErrorProvider=new mControl.WinCtl.Controls.CtlErrorProvider(ctl as ContainerControl,m_provider);
			   //m_ErrorProvider.SetError(ctl,value);

				switch(m_provider)
				{
					case ErrProviders.ErrIcon:
						m_ErrorProvider=new mControl.WinCtl.Controls.CtlErrorProvider();//ctl as ContainerControl);
						m_ErrorProvider.SetError(ctl,value);
						m_IsErrorOn=true;
						break;
					case ErrProviders.Info:
                        mControl.WinCtl.Dlg.MsgDlg.OpenMsg(value, Caption);//ctl);
						break;
					case ErrProviders.MsgBox:
                        mControl.WinCtl.Dlg.MsgDlg.OpenDialog(value, Caption);//ctl);
                        //MsgBox.ShowError(value);
						break;
					case ErrProviders.NotifyBar:
						mControl.WinCtl.Dlg.NotifyWindow.ShowNotifyMsg(Caption,value);
						break;

				}
			//}
			OnErrorOcurred(ctl,new ErrorOcurredEventArgs(value));
		}

		public void ResetError()
		{
          ResetError(Ctl); 
		}

		public void ResetError(Control ctl)
		{
			if (m_IsErrorOn && m_ShowErrorProvider) 
			{
				m_ErrorProvider.SetError(ctl,"");

				//SetError(ctl, "");
			}
			m_IsErrorOn=false;
		}

		protected void OnErrorOcurred(Control ctl, ErrorOcurredEventArgs e)
		{
			if (e.Message.Length > 0)
			{
				if(ErrorOcurred!=null)
				{
					ErrorOcurredEventArgs oArg = new ErrorOcurredEventArgs(e.Message);
					ErrorOcurred(ctl, oArg); 
				}
			}
			//SetError( e.Message);
		}
		#endregion
	}


	#region ErrProviderConverter
	/// <summary>
	/// Summary description for StyleBaseConverter.
	/// </summary>
	public class ErrProviderConverter : TypeConverter
	{
		public ErrProviderConverter()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		/// <summary>
		/// allows us to display the + symbol near the property name
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <param name="value"></param>
		/// <param name="attributes"></param>
		/// <returns></returns>
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			return TypeDescriptor.GetProperties(typeof(ErrProvider));
		}

	}
	#endregion

}
