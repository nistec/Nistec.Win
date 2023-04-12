using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Nistec.WinForms.Controls
{
	[System.ComponentModel.ToolboxItem(false)]
	public  class McControl : System.Windows.Forms.Control
	{

		#region NetReflectedFram
        //internal bool m_netFram=false;

        //public void NetReflectedFram(string pk)
        //{
        //    try
        //    {
        //        // this is done because this method can be called explicitly from code.
        //        System.Reflection.MethodBase method = (System.Reflection.MethodBase) (new System.Diagnostics.StackTrace().GetFrame(1).GetMethod());
        //        m_netFram = Nistec.Util.Net.nf_1.nf_2(method, pk);
        //    }
        //    catch{}
        //}

        //protected override void OnHandleCreated(EventArgs e)
        //{
        //    base.OnHandleCreated (e);
        //    //if(!DesignMode && !m_netFram)
        //    //{
        //    //    Nistec.Util.Net.netWinMc.NetFram(this.Name, "Mc"); 
        //    //}
        //}

		#endregion

		#region Members
		private System.ComponentModel.Container components = null;


		#endregion

		#region Constructors

		public McControl()
		{
			this.Name = "McControl";
		}

        //internal McControl(bool net):this()
        //{	
        //    m_netFram=net;
        //}

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

        [Browsable(true)]
        public override ContextMenu ContextMenu
        {
            get
            {
                return base.ContextMenu;
            }
            set
            {
                base.ContextMenu = value;
            }
        }
        public Message LastMsg;
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            LastMsg = m;
        }
	}

}
