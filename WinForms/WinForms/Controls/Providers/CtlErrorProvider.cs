using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

using Nistec.Win;

namespace Nistec.WinForms
{
	public enum ErrProviders
	{
		None,
		ErrIcon,
		MsgBox,
		Info,
		NotifyBar
	}

	[DesignTimeVisible(true),ToolboxItem(true),ToolboxBitmap (typeof(McErrorProvider),"Toolbox.ErrorProvider.bmp")]
	public class McErrorProvider:System.Windows.Forms.ErrorProvider
	{
		#region Members
		private ErrProviders m_provider;
        private string m_Caption = "Error Provider";

		//private bool m_IsErrorOn;

		public event ErrorOcurredEventHandler ErrorOcurred;
		#endregion

		#region Constructor
		public McErrorProvider():base()
		{
			m_provider=ErrProviders.ErrIcon;
		}
		public McErrorProvider(ContainerControl ctl):base(ctl)
		{
			m_provider=ErrProviders.ErrIcon;
		}

		public McErrorProvider(ContainerControl ctl,ErrProviders provider):base(ctl)
		{
			m_provider=provider;
		}

		
		#endregion

		#region Properties

		[DefaultValue(ErrProviders.ErrIcon)]
		public ErrProviders Provider
		{
			get{return this.m_provider;}
			set{this.m_provider=value;}
		}

        [DefaultValue("Error Provider")]
        public string Caption
        {
            get { return this.m_Caption; }
            set { this.m_Caption = value; }
        }

//		[Browsable(false)]
//		public bool IsErrorOn
//		{
//			get{return this.m_IsErrorOn;}
//		}
		
		#endregion

		#region Methods

        public void SetError(Control ctl, string value,ErrProviders provider)
        {
            switch (provider)
            {
                case ErrProviders.None:
                    //Do nothing
                    break;
                case ErrProviders.ErrIcon:
                    base.SetError(ctl, value);
                    //m_IsErrorOn=true;
                    break;
                case ErrProviders.Info:
                    Nistec.WinForms.MsgDlg.ShowMsg(value, Caption);//ctl);
                    break;
                case ErrProviders.MsgBox:
                    Nistec.WinForms.MsgDlg.ShowDialog(value, Caption);//ctl);
                    //MsgBox.ShowError(value);
                    break;
                case ErrProviders.NotifyBar:
                    Nistec.WinForms.NotifyWindow.ShowNotifyMsg(null, NotifyStyle.Msg, Caption, value);
                    break;

            }
            OnErrorOcurred(ctl, new ErrorOcurredEventArgs(value));
        }

		public new void SetError(Control ctl,string value)
		{
            SetError(ctl, value, m_provider);
		}
	   
		public void ResetError(Control ctl)
		{
			//if (m_IsErrorOn && m_provider==ErrProviders.ErrIcon) 
			//{
				base.SetError(ctl,"");
			//}
			//m_IsErrorOn=false;
		}

		protected void OnErrorOcurred(Control ctl, ErrorOcurredEventArgs e)
		{
			if(e.Message==null)
				return;
			if (e.Message.Length > 0)
			{
				if(ErrorOcurred!=null)
				{
					ErrorOcurredEventArgs oArg = new ErrorOcurredEventArgs(e.Message);
					ErrorOcurred(ctl, oArg); 
				}
			}
		}
		#endregion
	}


}
