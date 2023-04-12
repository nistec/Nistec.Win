using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
 
namespace Nistec.WinForms
{
    [Designer(typeof(Design.ToolBarContainerDesigner)), ToolboxItem(true), ToolboxBitmap(typeof(McToolBarContainer), "Toolbox.ToolBarContainer.bmp")]
    public class McToolBarContainer : Nistec.WinForms.Controls.McContainer
    {
        public const int DefaultMinSize = 24;
        //private ToolBarCollection toolBarList;
        private McToolBar selectedToolBar;

        public McToolBar SelectedToolBar
        {
            get { return selectedToolBar; }
            set { selectedToolBar = value; }
        }

        public McToolBarContainer()
            //: base(ControlLayout.Visual)
        {
            //toolBarList = new ToolBarCollection();
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            this.Padding = new Padding(2);

            this.BringToFront();
        }


        //public ToolBarCollection ToolBarList
        //{
        //    get
        //    {
        //        return toolBarList;
        //    }
        //}

        public void AddToolBar(McToolBar toolBar)
        {
            this.Controls.Add(toolBar);
        }
        public void RemoveToolBar(McToolBar toolBar)
        {
            this.Controls.Remove(toolBar);
        }

        public void Clear()
        {
            this.Controls.Clear();
            //toolBarList.Clear();
        }

        public void SelectToolBar(Control toolBar)
        {
            foreach(Control tb in this.Controls)
            {
                if(tb==toolBar)
                {
                    tb.Visible=true;
                    return;
                }
            }
        }

        [Editor("Nistec.WinForms.ToolBarCollectionEditor", typeof(System.Drawing.Design.UITypeEditor))]
        public ControlCollection Items
        {
            get { return this.Controls; }
        }

        //protected override void OnControlAdded(ControlEventArgs e)
        //{
        //    base.OnControlAdded(e);

        //    McToolBar toolBar = (McToolBar)e.Control;
        //    //toolBar.Dock = DockStyle.Left;
        //    //c.ParentBar = this;
        //    //c.Text = "";
        //    this.Controls.SetChildIndex(toolBar, 0);
        //    //toolBarList.Add(toolBar);

        //}

        

        //protected override void OnControlRemoved(ControlEventArgs e)
        //{
        //    McToolBar toolBar = (McToolBar)e.Control;
        //    //toolBarList.Remove(toolBar);
            
        //    toolBar.Controls.Clear();

        //    //int count = toolBar.Controls.Count;
        //    //for (int index = 0; index < count; index++)
        //    //{
        //    //    toolBar.Controls.RemoveAt(0);
        //    //}
         

        //    base.OnControlRemoved(e);

        //}
        
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (this.Height < DefaultMinSize)
            {
                this.Height = DefaultMinSize;
                this.Invalidate(true);
            }
        }

        internal McToolBar GetBarAtPoint(Point p)
        {
            using (Graphics graphics1 = Graphics.FromHwnd(base.Handle))
            {
                foreach (Control tb in this.Controls)
                {
                    if (!tb.Visible && !base.DesignMode)
                    {
                        continue;
                    }
                    //Rectangle rectangle1 = this.GetPageRectangle(graphics1, page1);
                    Rectangle rectangle1 = (Rectangle)tb.ClientRectangle;// .GetPageRectangle(graphics1, page1);

                    if (rectangle1.Contains(p))
                    {
                        return (McToolBar)tb;
                    }
                }
            }
            return null;
        }

        }

    public class ToolBarCollection : List<McToolBar>
    {


    }
}
