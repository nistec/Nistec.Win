using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Text;
using System.ComponentModel;
using System.Collections;

using Nistec.Drawing;



namespace Nistec.WinForms
{
    [ToolboxItem(true), ToolboxBitmap(typeof(McContextStrip), "Toolbox.ContextMenu.bmp"), ProvideProperty("ImageList", typeof(MenuItem)), ProvideProperty("ImageIndex", typeof(MenuItem)), ProvideProperty("Draw", typeof(MenuItem))]
	public class McContextStrip : ContextMenuStrip,ILayout//,IMenu
	{

		#region Members
		#endregion

		#region Constructor

        public McContextStrip()
		{
            base.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
     
		}

		#endregion
    
  		#region ILayout

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Category("Style"), DefaultValue(ControlLayout.Visual)]
        public virtual ControlLayout ControlLayout
        {
            get { return ControlLayout.Visual; }
            set
            {
            }
        }

		protected IStyle	m_StylePainter;
 
		[Browsable(false)]
		public PainterTypes PainterType
		{
			get{return PainterTypes.Flat;}
		}

		[Category("Style"),DefaultValue(null),RefreshProperties(RefreshProperties.All)]
		public IStyle StylePainter
		{
			get {return m_StylePainter;}
			set 
			{
				if(m_StylePainter!=value)
				{
					if (this.m_StylePainter != null)
						this.m_StylePainter.PropertyChanged -=new PropertyChangedEventHandler(m_Style_PropertyChanged); 
					m_StylePainter = value;
					if (this.m_StylePainter != null)
						this.m_StylePainter.PropertyChanged +=new PropertyChangedEventHandler(m_Style_PropertyChanged); 
					OnStylePainterChanged(EventArgs.Empty);
				}
			}
		}

		[Browsable(false)]
		public virtual IStyleLayout LayoutManager
		{
			get
			{
				if(this.m_StylePainter!=null)
					return this.m_StylePainter.Layout as IStyleLayout;
				else
					return StyleLayout.DefaultLayout as IStyleLayout;// this.m_Style as IStyleLayout;
			}
		}

		public virtual void SetStyleLayout(StyleLayout value)
		{
			LayoutManager.SetStyleLayout(value);
			//if(this.m_StylePainter!=null)
			//	this.m_StylePainter.Layout.SetStyleLayout(value); 
		}

		public virtual void SetStyleLayout(Styles value)
		{
			LayoutManager.SetStyleLayout(value);
			//if(this.m_StylePainter!=null)
			//	m_StylePainter.Layout.SetStyleLayout(value);
		}

		protected virtual void OnStylePainterChanged(EventArgs e)
		{
		}

		protected virtual void OnStylePropertyChanged(PropertyChangedEventArgs e)
		{

		}

		private void m_Style_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			OnStylePropertyChanged(e);
		}

		#endregion

        [Browsable(false)]
        public new ToolStripRenderMode RenderMode
        {
            get { return System.Windows.Forms.ToolStripRenderMode.System; }
            set { base.RenderMode = ToolStripRenderMode.System; }
        }

 	}

}
