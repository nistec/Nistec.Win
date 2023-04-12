namespace MControl.Printing.View.Design
{
    using MControl.Printing.View;
    using MControl.Printing.View.Design.UserDesigner;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Drawing.Drawing2D;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    //mtd423
    internal class PanelDesiger : PanelDesigerBase
    {
        private ContextMenu _var0;
        private ContextMenu _var1;
        private Font _var10;
        private Section[] _var11;
        private bool _var12;
        private bool _var13;
        private Rectangle _var14;
        private Point _var15;
        private bool _var16;
        private bool _var17;
        private bool _var18;
        private bool _var19;
        private SectionCollection _var2;
        private int _var20;
        private SectionDesigner _var21;
        private Rectangle _var22;
        private int _var23;
        private Rectangle _var24;
        private ImageList _var25;
        private Color _var26;
        private RTFEditorDlg _var27;
        private bool _var28;
        private bool _var29;
        private mtd389 _var3;
        private ReportViewDesigner _var30;
        private SectionListDesigner _var31;
        private SectionListDesigner _var32;
        private mtd386 _var33;
        private ArrayList _var34;
        private SectionDesigner _var35;
        private bool _var36;
        private int _var4;
        private bool _var5;
        private int _var6;
        private bool _var7;
        private Rectangle _var8;
        private Font _var9;
        private SectionDesigner _var93;
        internal MControl.Printing.View.Design.Report mtd268;
        internal ReportControlDesigner mtd430;
        internal mtd471 mtd483;

        internal event LayoutChangedHandler mtd485;

        internal PanelDesiger(ReportViewDesigner rd)
        {
            this._var30 = rd;
            this._var28 = true;
            this._var31 = new SectionListDesigner();
            this._var32 = new SectionListDesigner();
            this._var33 = new mtd386();
            this._var34 = new ArrayList();
            this.var37();
            this.var38();
        }

        internal void mtd417(ref ImageList var49)
        {
            this._var25 = var49;
            this.var50();
        }

        internal void mtd429(object var52, EventArgs e)
        {
            PageSetupDlg setup = new PageSetupDlg();
            setup.PageSettings = this.mtd268.PageSetting;
            setup.ShowDialog();
            if (setup.mtd4)
            {
                TypeDescriptor.GetProperties(this.mtd268)["PageSetting"].SetValue(this.mtd268, this.mtd268.PageSetting);
            }
        }

        internal void mtd459(object var52, EventArgs e)
        {
            this._var31.mtd387();
            this._var32.mtd387();
            this._var33.mtd387();
            this._var34.Clear();
            this._var35 = null;
            this._var36 = false;
        }

        internal void mtd460(object var52, DesignerTransactionCloseEventArgs e)
        {
            if (this._var28 && e.TransactionCommitted)
            {
                if (this._var31.Count > 0)
                {
                    this.var126();
                }
                else if (this._var32.Count > 0)
                {
                    this.var127();
                }
                else if (this._var33.mtd166 > 0)
                {
                    this.var128();
                }
                else if (this._var34.Count > 0)
                {
                    this.var129();
                }
                else if (this._var35 != null)
                {
                    if (this._var36)
                    {
                        this._var35.mtd393.Controls.SortByIndex();
                    }
                    this._var35.mtd496(this._var5);
                    if (this._var35 == this.mtd430.mtd474)
                    {
                        this.mtd430.mtd548();
                    }
                    base.Invalidate(this._var35.mtd57);
                    base.Update();
                }
            }
        }

        internal void mtd461(object var130, ComponentEventArgs e)
        {
            if (this._var29 && this._var28)
            {
                if (e.Component is Section)
                {
                    this._var31.mtd2((Section) e.Component);
                }
                else if (e.Component is McReportControl)
                {
                    this._var33.mtd2((McReportControl) e.Component);
                }
            }
        }

        internal void mtd462(object var130, ComponentEventArgs e)
        {
            if (this._var29 && this._var28)
            {
                if (e.Component is Section)
                {
                    this._var32.mtd2((Section) e.Component);
                }
                else if (e.Component is McReportControl)
                {
                    McReportControl component = (McReportControl) e.Component;
                    Section parent = (Section) component.Parent;
                    parent.Controls.Remove(component);
                    if (this._var35 == null)
                    {
                        this._var35 = this._var3[parent];
                    }
                }
            }
        }

        internal void mtd463(object var52, ComponentChangedEventArgs e)
        {
            if (e.Member != null)
            {
                if (e.Component is Section)
                {
                    if (e.Member.Name != "Index")
                    {
                        if (e.Member.Name == "Height")
                        {
                            this.var114((Section) e.Component);
                        }
                        else if (e.Member.Name == "Name")
                        {
                            SectionDesigner mtd = this._var3[(Section)e.Component];
                            base.Invalidate(mtd.mtd57);
                            base.Update();
                        }
                        else if (e.Member.Name == "BackColor")
                        {
                            SectionDesigner mtd2 = this._var3[(Section)e.Component];
                            if (mtd2 != null)
                            {
                                mtd2.mtd496(this._var5);
                                base.Invalidate(mtd2.mtd57);
                                base.Update();
                            }
                        }
                    }
                    else if (this._var28 && (e.Component is Section))
                    {
                        int num = (this._var2.Count - 1) / 2;
                        int num2 = num + (num - ((int) e.OldValue));
                        GroupFooter footer = (GroupFooter) this._var2[num2];
                        this._var34.Add(new GroupSectionDesigner((GroupHeader)e.Component, footer));
                    }
                }
                else if (e.Component is McReportControl)
                {
                    if (e.Member.Name == "Index")
                    {
                        this._var36 = true;
                    }
                    if (this._var35 == null)
                    {
                        this._var35 = this._var3[(Section) ((McReportControl) e.Component).Parent];
                    }
                }
                else if (e.Member.Name == "ReportWidth")
                {
                    this.var115();
                }
            }
        }

        internal void mtd468(MControl.Printing.View.Design.Report var61)
        {
            this.var59();
            this.mtd268 = var61;
            this.var60();
        }

        internal void mtd469(object var52, EventArgs e)
        {
            this._var29 = true;
        }

        internal void mtd470(object var52, EventArgs e)
        {
            this._var29 = false;
        }

        internal void mtd488(SectionType var82)
        {
            SectionType groupFooter;
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            int count = this._var2.Count;
            if (var82 == SectionType.GroupHeader)
            {
                this._var12 = true;
                num2 = (count - 1) / 2;
                num3 = num2 + 2;
                groupFooter = SectionType.GroupFooter;
            }
            else if (var82 == SectionType.ReportHeader)
            {
                groupFooter = SectionType.ReportFooter;
                if (this._var2.GetItem(SectionType.ReportHeader) != null)
                {
                    return;
                }
                num2 = 0;
                num3 = count + 1;
            }
            else if (var82 == SectionType.PageHeader)
            {
                groupFooter = SectionType.PageFooter;
                if (this._var2.GetItem(SectionType.PageHeader) != null)
                {
                    return;
                }
                if (this._var2.GetItem(SectionType.ReportHeader) != null)
                {
                    num2 = 1;
                    num3 = count;
                }
                else
                {
                    num2 = 0;
                    num3 = count + 1;
                }
            }
            else
            {
                groupFooter = SectionType.ReportDetail;
                return;
            }
            base.SuspendLayout();
            num = this._var3[num2].mtd29;
            count++;
            using (DesignerTransaction transaction = this._var30.mtd426.CreateTransaction("Add-Section"))
            {
                this._var28 = false;
                this.var83(var82, num2, 0);
                this.var83(groupFooter, num3, 0);
                transaction.Commit();
                this._var28 = true;
            }
            for (int i = num2; i <= count; i++)
            {
                SectionDesigner mtd = this._var3[i];
                mtd.mtd504 = i;
                num = mtd.mtd515(num);
            }
            if (var82 == SectionType.GroupHeader)
            {
                this.var58();
            }
            this.var84();
            base.Invalidate();
            base.ResumeLayout(true);
            this.mtd516(LayoutChangedType.SectionAdd);
        }

        internal void mtd489()
        {
            int index = 0;
            int num2 = 0;
            int count = this._var2.Count;
            if (this.mtd430.mtd517.Type == SectionType.GroupHeader)
            {
                index = this._var2.IndexOf(this.mtd430.mtd517);
                num2 = (count - index) - 2;
                this._var12 = true;
            }
            else if (this.mtd430.mtd517.Type == SectionType.GroupFooter)
            {
                num2 = this._var2.IndexOf(this.mtd430.mtd517) - 1;
                index = (count - num2) - 2;
                this._var12 = true;
            }
            else if ((this.mtd430.mtd517.Type == SectionType.ReportHeader) | (this.mtd430.mtd517.Type == SectionType.ReportFooter))
            {
                index = 0;
                num2 = count - 2;
            }
            else if ((this.mtd430.mtd517.Type == SectionType.PageHeader) | (this.mtd430.mtd517.Type == SectionType.PageFooter))
            {
                if (this._var2.GetItem(SectionType.ReportHeader) != null)
                {
                    index = 1;
                    num2 = count - 3;
                }
                else
                {
                    index = 0;
                    num2 = count - 2;
                }
            }
            base.SuspendLayout();
            int y = this._var3[index].mtd29;
            count -= 3;
            int height = this._var3[index].mtd57.Height;
            this._var3.mtd394(index);
            Section component = this._var2[index];
            this._var2.RemoveAt(index);
            height += this._var3[num2].mtd57.Height;
            this._var3.mtd394(num2);
            Section section2 = this._var2[num2];
            this._var2.RemoveAt(num2);
            using (DesignerTransaction transaction = this._var30.mtd426.CreateTransaction("Delete-Section " + component.Name + " / " + section2.Name))
            {
                this._var28 = false;
                this._var30.mtd426.DestroyComponent(component);
                foreach (McReportControl control in component.Controls)
                {
                    this._var30.mtd426.DestroyComponent(control);
                }
                this._var30.mtd426.DestroyComponent(section2);
                foreach (McReportControl control2 in section2.Controls)
                {
                    this._var30.mtd426.DestroyComponent(control2);
                }
                transaction.Commit();
                this._var28 = true;
            }
            this.var84();
            base.mtd518();
            int num6 = 0;
            y = base.mtd500.Y;
            foreach (SectionDesigner mtd in this._var3)
            {
                mtd.mtd504 = num6;
                y = mtd.mtd515(y);
                num6++;
            }
            base.Invalidate();
            base.ResumeLayout(true);
            this.mtd516(LayoutChangedType.SectionDelete);
        }

        internal void mtd492()
        {
            if (this._var12)
            {
                this.var58();
            }
            if (this._var11.Length > 1)
            {
                GroupOrderDlg order = new GroupOrderDlg();
                order.GroupList = this._var11;
                order.ShowDialog();
                if (order.OK)
                {
                    this.var117(this._var11, order.GroupList);
                }
            }
        }

        internal void mtd492(Section[] var118)
        {
            this.var117(this._var11, var118);
        }

        internal void mtd498()
        {
            this.var59();
            PageHeader header = new PageHeader("PageHeader");
            ReportDetail detail = new ReportDetail("ReportDetail");
            PageFooter footer = new PageFooter("PageFooter");
            header.Height = 36f;
            detail.Height = 192f;
            footer.Height = 36f;
            this.mtd268.ReportWidth = 576f;
            this.mtd268.Sections.Add(header);
            this.mtd268.Sections.Add(detail);
            this.mtd268.Sections.Add(footer);
            this.var60();
        }

        internal void mtd499(string var62, mtd426 var63)
        {
            if (File.Exists(var62))
            {
                this.var59();
                this.mtd268.LoadLayout(var62, var63);
                this.var60();
            }
            else
            {
                MessageBox.Show("File Not Found", "MControl-Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        internal void mtd499(ref Stream var64, mtd426 var63)
        {
            this.var59();
            this.mtd268.LoadLayout(ref var64, var63);
            this.var60();
        }

        internal void mtd501()
        {
            if (this._var7)
            {
                base.SuspendLayout();
                this._var22.Width = base.Width - 0x16;
                this._var24.Height = base.Height - 0x16;
                this._var6 = base.Width;
                this.var108();
                this.var109();
                foreach (SectionDesigner mtd in this._var3)
                {
                    mtd.mtd546 = base.Width;
                }
                base.ResumeLayout(true);
            }
        }

        internal void mtd516(LayoutChangedType e)
        {
            if (this.mtd485 != null)
            {
                this.mtd485(this.mtd268, new LayoutChangedArgs(e));
            }
        }

        internal void mtd522(Rectangle var92)
        {
            if (var92.IsEmpty)
            {
                base.Invalidate();
            }
            else
            {
                base.Invalidate(var92);
            }
            base.Update();
        }

        private void ForceReportUpdate()
        {
            TypeDescriptor.GetProperties(this.mtd268)["IsDirty"].SetValue(this.mtd268, this.mtd268.IsDirty);
        }

        private void var100(int var111, int var112)
        {
            this.Cursor = Cursors.VSplit;
            int num = 0x16;
            Rectangle a = this._var24;
            if ((this._var24.X + (var111 - this._var23)) >= num)
            {
                this._var24.X += var111 - this._var23;
                this._var23 = var111;
            }
            else
            {
                this._var24.X = num;
                this._var23 = num;
            }
            a = Rectangle.Union(a, this._var24);
            a.Inflate(0, 0x16);
            base.Invalidate(a);
        }

        private bool var102()
        {
            if (((this._var30.mtd427 != null) && (this._var30.mtd427.SelectedCategory == "ReportView")) && (this._var30.mtd427.GetSelectedToolboxItem() != null))
            {
                this._var30.mtd427.SetCursor();
                return true;
            }
            return false;
        }

        private void var103(object var52, MouseEventArgs e)
        {
            Rectangle a = this._var14;
            if (this._var15.X <= e.X)
            {
                this._var14.X = this._var15.X;
                this._var14.Width = e.X - this._var15.X;
            }
            else
            {
                this._var14.X = e.X;
                this._var14.Width = this._var15.X - e.X;
            }
            if (this._var15.Y <= e.Y)
            {
                this._var14.Y = this._var15.Y;
                this._var14.Height = e.Y - this._var15.Y;
            }
            else
            {
                this._var14.Y = e.Y;
                this._var14.Height = this._var15.Y - e.Y;
            }
            Rectangle.Union(a, this._var14).Inflate(12, 12);
            this.mtd430.mtd479();
        }

        private McReportControl var104(Point var106, McControlCollection var107)
        {
            for (int i = var107.Count - 1; i >= 0; i--)
            {
                using (GraphicsPath path = new GraphicsPath())
                {
                    McReportControl control = var107[i];
                    if (control.Type == ControlType.Line)
                    {
                        McLine line = (McLine) var107[i];
                        path.StartFigure();
                        path.AddLine((float) (line.X1 + 1f), (float) (line.Y1 - 1f), (float) (line.X2 + 1f), (float) (line.Y2 - 1f));
                        path.AddLine((float) (line.X2 + 1f), (float) (line.Y2 - 1f), (float) (line.X2 - 1f), (float) (line.Y2 + 1f));
                        path.AddLine((float) (line.X2 - 1f), (float) (line.Y2 + 1f), (float) (line.X1 - 1f), (float) (line.Y1 + 1f));
                        path.AddLine((float) (line.X1 - 1f), (float) (line.Y1 + 1f), (float) (line.X1 + 1f), (float) (line.Y1 + 1f));
                        path.CloseFigure();
                    }
                    else
                    {
                        path.AddRectangle(control.Bounds);
                    }
                    if (path.IsVisible(var106))
                    {
                        return control;
                    }
                }
            }
            return null;
        }

        private void var105()
        {
            if (this._var30.mtd427.SelectedCategory == "ReportView")
            {
                ToolboxItem selectedToolboxItem = this._var30.mtd427.GetSelectedToolboxItem();
                if (selectedToolboxItem != null)
                {
                    this.mtd483.mtd472 = true;
                    if (selectedToolboxItem.DisplayName == "Label")
                    {
                        this.mtd483.mtd138 = ControlType.Label;
                    }
                    else if (selectedToolboxItem.DisplayName == "TextBox")
                    {
                        this.mtd483.mtd138 = ControlType.TextBox;
                    }
                    else if (selectedToolboxItem.DisplayName == "CheckBox")
                    {
                        this.mtd483.mtd138 = ControlType.CheckBox;
                    }
                    else if (selectedToolboxItem.DisplayName == "Picture")
                    {
                        this.mtd483.mtd138 = ControlType.Picture;
                    }
                    else if (selectedToolboxItem.DisplayName == "Shape")
                    {
                        this.mtd483.mtd138 = ControlType.Shape;
                    }
                    else if (selectedToolboxItem.DisplayName == "Line")
                    {
                        this.mtd483.mtd138 = ControlType.Line;
                    }
                    else if (selectedToolboxItem.DisplayName == "RichText")
                    {
                        this.mtd483.mtd138 = ControlType.RichTextField;
                    }
                    else if (selectedToolboxItem.DisplayName == "SubReport")
                    {
                        this.mtd483.mtd138 = ControlType.SubReport;
                    }
                    else if (selectedToolboxItem.DisplayName == "PageBreak")
                    {
                        this.mtd483.mtd138 = ControlType.PageBreak;
                    }
                }
            }
        }

        private void var108()
        {
            int y = base.mtd500.Y;
            foreach (SectionDesigner mtd in this._var3)
            {
                y = mtd.mtd515(y);
            }
            base.Invalidate();
        }

        private void var109()
        {
            foreach (SectionDesigner mtd in this._var3)
            {
                mtd.mtd536(base.mtd500.X);
            }
            this.var68();
            base.Invalidate();
        }

        private int var110()
        {
            int num = 0;
            foreach (Section section in this._var2)
            {
                num += 20 + ((int) section.Height);
            }
            return num;
        }

        private void var113()
        {
            int num = this._var21.mtd504;
            int top = this._var22.Top;
            this._var21.mtd538(this._var22.Top);
            num++;
            for (int i = num; i < this._var3.mtd166; i++)
            {
                top = this._var3[i].mtd539(top);
            }
        }

        private void var114(Section var70)
        {
            SectionDesigner mtd = this._var3[var70];
            if (mtd != null)
            {
                this._var3[mtd.mtd504].mtd540(this._var5);
                this.var84();
                base.mtd518();
                this.var108();
            }
        }

        private void var115()
        {
            int num = Convert.ToInt32(this.mtd268.ReportWidth);
            this.var116();
            base.mtd541();
            foreach (SectionDesigner mtd in this._var3)
            {
                mtd.mtd542(num, this._var5);
                mtd.mtd536(base.mtd500.X);
            }
            this.var68();
            base.Invalidate();
            base.Update();
        }

        private void var116()
        {
            int width = ((int) this.mtd268.ReportWidth) + 70;
            base.mtd549 = new Size(width, base.mtd549.Height);
        }

        private void var117(Section[] var119, Section[] var118)
        {
            int num = (this._var2.Count - 1) / 2;
            int index = this._var2.IndexOf(var119[0]);
            int num3 = index + (2 * var119.Length);
            ArrayList list = new ArrayList();
            base.SuspendLayout();
            int num7 = this._var3[index].mtd29;
            for (int i = 0; i < var118.Length; i++)
            {
                int num4 = index + i;
                int num5 = num3 - i;
                int num6 = this._var2.IndexOf(var118[i]);
                Section s = this._var2[num6];
                list.Add(new GroupSectionDesigner(num4, (GroupHeader)s));
                this.var120(s, num6, num4);
                num6 = num + (num - num6);
                s = this._var2[num6];
                this.var120(s, num6, num5);
            }
            using (DesignerTransaction transaction = this._var30.mtd426.CreateTransaction("ReOrder-Group"))
            {
                this._var28 = false;
                foreach (object obj2 in list)
                {
                    GroupSectionDesigner mtd = (GroupSectionDesigner)obj2;
                    TypeDescriptor.GetProperties(mtd.mtd296)["Index"].SetValue(mtd.mtd296, mtd.mtd544);
                }
                this.ForceReportUpdate();
                this._var28 = true;
                transaction.Commit();
            }
            for (int j = index; j <= num3; j++)
            {
                SectionDesigner mtd2 = this._var3[j];
                mtd2.mtd504 = j;
                num7 = mtd2.mtd515(num7);
            }
            this._var12 = true;
            base.Invalidate();
            base.ResumeLayout(true);
            this.mtd516(LayoutChangedType.GroupReOrder);
        }

        private void var120(Section s, int var121, int var122)
        {
            SectionDesigner mtd = this._var3[var121];
            this._var3.mtd394(var121);
            this._var3.mtd216(var122, mtd);
            this._var2.RemoveAt(var121);
            this._var2.Insert(var122, s);
        }

        private SectionDesigner var123(Point var106)
        {
            foreach (SectionDesigner mtd in this._var3)
            {
                if (mtd.mtd545(var106))
                {
                    return mtd;
                }
            }
            return null;
        }

        private void var126()
        {
            int index = 0;
            int num2 = 0;
            int num3 = 0;
            int count = this._var2.Count;
            Section section = this._var31[0];
            Section section2 = this._var31[1];
            if ((section != null) && (section2 != null))
            {
                if (((section.Type == SectionType.ReportFooter) | (section.Type == SectionType.PageFooter)) | (section.Type == SectionType.GroupFooter))
                {
                    section = section2;
                    section2 = this._var31[0];
                }
                if (section.Type == SectionType.PageHeader)
                {
                    if (this._var2.GetItem(SectionType.PageHeader) != null)
                    {
                        return;
                    }
                    if (this._var2.GetItem(SectionType.ReportHeader) != null)
                    {
                        index = 1;
                        num2 = count;
                    }
                    else
                    {
                        index = 0;
                        num2 = count + 1;
                    }
                }
                else if (section.Type == SectionType.ReportHeader)
                {
                    if (this._var2.GetItem(SectionType.ReportHeader) != null)
                    {
                        return;
                    }
                    index = 0;
                    num2 = count + 1;
                }
                else if (section.Type == SectionType.GroupHeader)
                {
                    GroupHeader header = (GroupHeader) section;
                    if ((header.Index < this._var2.Count) && (this._var2[header.Index].Type == SectionType.GroupHeader))
                    {
                        index = header.Index;
                        num2 = (((((count - 1) / 2) - index) + 1) * 2) + index;
                    }
                    else
                    {
                        index = (count - 1) / 2;
                        header.Index = index;
                        num2 = index + 2;
                    }
                }
                base.SuspendLayout();
                this._var2.Insert(index, section);
                this._var2.Insert(num2, section2);
                num3 = this._var3[index].mtd29;
                count = this._var2.Count;
                this.var66(section, 0, base.mtd500.X, index);
                this.var66(section2, 0, base.mtd500.X, num2);
                for (int i = index; i < count; i++)
                {
                    SectionDesigner mtd = this._var3[i];
                    mtd.mtd504 = i;
                    num3 = mtd.mtd515(num3);
                }
                if (section.Type == SectionType.GroupHeader)
                {
                    this.var58();
                }
                this.var84();
                base.Invalidate();
                base.ResumeLayout(true);
            }
        }

        private void var127()
        {
            int index = 0;
            int num2 = 0;
            int count = this._var2.Count;
            Section section = this._var32[0];
            Section section2 = this._var32[1];
            if ((section != null) && (section2 != null))
            {
                if (((section.Type == SectionType.ReportFooter) | (section.Type == SectionType.PageFooter)) | (section.Type == SectionType.GroupFooter))
                {
                    section = section2;
                    section2 = this._var31[0];
                }
                if (section.Type == SectionType.GroupHeader)
                {
                    index = this._var2.IndexOf(section);
                    num2 = (count - index) - 2;
                    this._var12 = true;
                }
                else if (section.Type == SectionType.ReportHeader)
                {
                    index = 0;
                    num2 = count - 2;
                }
                else
                {
                    if (section.Type != SectionType.PageHeader)
                    {
                        return;
                    }
                    if (this._var2.GetItem(SectionType.ReportHeader) != null)
                    {
                        index = 1;
                        num2 = count - 3;
                    }
                    else
                    {
                        index = 0;
                        num2 = count - 2;
                    }
                }
                base.SuspendLayout();
                int y = this._var3[index].mtd29;
                count -= 3;
                int height = this._var3[index].mtd57.Height;
                this._var3.mtd394(index);
                this._var2.RemoveAt(index);
                height += this._var3[num2].mtd57.Height;
                this._var3.mtd394(num2);
                this._var2.RemoveAt(num2);
                this.var84();
                base.mtd518();
                int num6 = 0;
                y = base.mtd500.Y;
                foreach (SectionDesigner mtd in this._var3)
                {
                    mtd.mtd504 = num6;
                    y = mtd.mtd515(y);
                    num6++;
                }
                base.Invalidate();
                base.ResumeLayout(true);
            }
        }

        private void var128()
        {
            Section parent = null;
            foreach (McReportControl control in this._var33)
            {
                if (control == null)
                {
                    continue;
                }
                parent = (Section) control.Parent;
                if (parent.Controls.IndexOf(control) == -1)
                {
                    if (control.Index < parent.Controls.Count)
                    {
                        parent.Controls.Insert(control.Index, control);
                        continue;
                    }
                    parent.Controls.Add(control);
                }
            }
            if (parent != null)
            {
                SectionDesigner mtd = this._var3[this._var2.IndexOf(parent)];
                mtd.mtd496(this._var5);
                base.Invalidate(mtd.mtd547);
                base.Update();
            }
        }

        private void var129()
        {
            if (this._var12)
            {
                this.var58();
            }
            int num = (this._var2.Count - 1) / 2;
            int index = this._var2.IndexOf(this._var11[0]);
            int num3 = index + (2 * this._var11.Length);
            base.SuspendLayout();
            int num5 = this._var3[index].mtd29;
            for (int i = 0; i < this._var34.Count; i++)
            {
                GroupSectionDesigner mtd = (GroupSectionDesigner)this._var34[i];
                int num6 = this._var2.IndexOf(mtd.mtd296);
                this.var120(mtd.mtd296, num6, mtd.mtd296.Index);
                num6 = this._var2.IndexOf(mtd.mtd305);
                int num4 = num + (num - mtd.mtd296.Index);
                this.var120(mtd.mtd305, num6, num4);
            }
            for (int j = index; j <= num3; j++)
            {
                SectionDesigner mtd2 = this._var3[j];
                mtd2.mtd504 = j;
                num5 = mtd2.mtd515(num5);
            }
            this._var12 = true;
            base.Invalidate();
            base.ResumeLayout(true);
        }

        private void var37()
        {
            this._var0 = new ContextMenu();
            this._var1 = new ContextMenu();
            this.mtd483 = new mtd471(this, this._var30.mtd426);
            this.mtd430 = new ReportControlDesigner(this, this._var30);
            this._var3 = new mtd389();
            this._var4 = 12;
            this._var5 = true;
            this._var8 = new Rectangle(3, 3, 0x10, 0x10);
            this._var9 = new Font("Tahoma", 7f);
            this._var10 = new Font("Arial", 8f);
            this._var12 = true;
            this._var14 = new Rectangle();
            this._var15 = new Point();
            this._var22 = new Rectangle(0x16, 0, 0, 2);
            this._var24 = new Rectangle(0, 0x16, 2, 0);
            this._var26 = Color.FromArgb(0xb5, 190, 0xd6);
        }

        private void var38()
        {
            base.SuspendLayout();
            this.BackColor = SystemColors.AppWorkspace;
            this.AllowDrop = true;
            base.Name = "DesignerPanel";
            base.Size = new Size(600, 600);
            this.Dock = DockStyle.Fill;
            base.Paint += new PaintEventHandler(this.var39);
            base.DoubleClick += new EventHandler(this.var40);
            base.MouseDown += new MouseEventHandler(this.var41);
            base.MouseUp += new MouseEventHandler(this.var42);
            base.MouseMove += new MouseEventHandler(this.var43);
            base.DragOver += new DragEventHandler(this.var44);
            base.DragDrop += new DragEventHandler(this.var45);
            base.mtd486 += new ScrollEventHandler(this.var46);
            base.mtd487 += new ScrollEventHandler(this.var47);
            base.Resize += new EventHandler(this.var48);
            base.ResumeLayout(false);
        }

        private void var39(object var52, PaintEventArgs e)
        {
            this.var86(var52, e);
            this.var87(var52, e);
            this.var88(var52, e);
            if (this._var18)
            {
                this.var89(this._var22, e);
            }
            else if (this._var19)
            {
                this.var89(this._var24, e);
            }
        }

        private void var40(object var52, EventArgs e)
        {
            if ((this.mtd430.mtd490 != null) && (this.mtd430.mtd490.Type == ControlType.RichTextField))
            {
                this.var53((McRichText) this.mtd430.mtd490, true);
            }
        }

        private void var41(object var52, MouseEventArgs e)
        {
            Point pt = new Point(e.X, e.Y);
            this.ContextMenu = null;
            if (this._var8.Contains(pt))
            {
                this.Cursor = Cursors.Hand;
                this.mtd430.mtd502(new object[] { this.mtd268 }, SelectionTypes.Replace);
            }
            else if (this._var24.Contains(pt))
            {
                this._var23 = e.X;
                this._var19 = true;
                this._var21 = this._var3[0];
                this._var24.X = this._var21.mtd408;
                this._var24.Height = base.Height;
                this.Cursor = Cursors.VSplit;
                this.mtd430.mtd502(new object[] { this.mtd268 }, SelectionTypes.Replace);
            }
            else
            {
                this.var94(e, MouseState.Down);
            }
        }

        private void var42(object var52, MouseEventArgs e)
        {
            this._var93 = null;
            if (this._var13)
            {
                this.var95();
            }
            else if (this._var18)
            {
                this._var18 = false;
                this.var96();
            }
            else if (this._var19)
            {
                this._var19 = false;
                this.var97();
            }
            else if (this.mtd483.mtd472 && this.mtd483.mtd473)
            {
                this.var98();
            }
            else if (this._var17)
            {
                this._var17 = false;
                this.mtd430.mtd523();
            }
            else if (this._var16)
            {
                this._var16 = false;
                this.mtd430.mtd524();
            }
        }

        private void var43(object var52, MouseEventArgs e)
        {
            Point pt = new Point(e.X, e.Y);
            if (this._var18)
            {
                this.var99(e.X, e.Y);
            }
            else if (this._var19)
            {
                this.var100(e.X, e.Y);
            }
            else if (this._var93 != null)
            {
                this._var93.mtd525(e);
            }
            else if (this._var24.Contains(pt))
            {
                this.Cursor = Cursors.SizeWE;
            }
            else if (this._var8.Contains(pt))
            {
                this.Cursor = Cursors.Hand;
            }
            else if (!this.var94(e, MouseState.Move))
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void var44(object var52, DragEventArgs e)
        {
            string[] strArray = ((string) e.Data.GetData(DataFormats.Text)).Split(new char[] { '.' });
            Point point = ((Panel) var52).PointToClient(new Point(e.X, e.Y));
            if ((this.var123(point) != null) && strArray[0].Equals("DataField"))
            {
                e.Effect = DragDropEffects.Move | DragDropEffects.Copy | DragDropEffects.Scroll;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void var45(object var52, DragEventArgs e)
        {
            string[] strArray = ((string) e.Data.GetData(DataFormats.Text)).Split(new char[] { '.' });
            Point point = ((Panel) var52).PointToClient(new Point(e.X, e.Y));
            SectionDesigner mtd = this.var123(point);
            if ((mtd != null) && strArray[0].Equals("DataField"))
            {
                this.mtd483.mtd356 = new PointF((float) (Math.Ceiling((double) ((point.X - mtd.mtd480.X) / 6.0)) * 6.0), (float) (Math.Ceiling((double) ((point.Y - mtd.mtd480.Y) / 6.0)) * 6.0));
                this.mtd483.mtd476 = new PointF(this.mtd483.mtd356.X + 72f, this.mtd483.mtd356.Y + 24f);
                this.mtd483.mtd138 = ControlType.TextBox;
                this.mtd483.mtd474 = mtd;
                this.mtd483.mtd475 = strArray[1];
                this.var98();
            }
        }

        private void var46(object var52, ScrollEventArgs e)
        {
            this.var109();
        }

        private void var47(object var52, ScrollEventArgs e)
        {
            this.var108();
        }

        private void var48(object var52, EventArgs e)
        {
            this.mtd501();
        }

        private void var50()
        {
            this._var0.MenuItems.Add(new MenuItem("Insert", new EventHandler(this.var51)));
            this._var0.MenuItems[0].MenuItems.Add(new MenuItem("Insert Report Header-Footer", new EventHandler(this.var51)));
            this._var0.MenuItems[0].MenuItems.Add(new MenuItem("Insert Page Header-Footer", new EventHandler(this.var51)));
            this._var0.MenuItems[0].MenuItems.Add(new MenuItem("Insert Group Header-Footer", new EventHandler(this.var51)));
            this._var0.MenuItems.Add(new MenuItem("Delete Section", new EventHandler(this.var51)));
            this._var0.MenuItems.Add(new MenuItem("Reorder Group", new EventHandler(this.var51)));
            this._var0.MenuItems.Add(new MenuItem("-"));
            this._var0.MenuItems.Add(new MenuItem("Paste", new EventHandler(this.var51)));
            this._var1.MenuItems.Add(new MenuItem("Insert", new EventHandler(this.var51)));
            this._var1.MenuItems[0].MenuItems.Add(new MenuItem("Insert Report Header-Footer", new EventHandler(this.var51)));
            this._var1.MenuItems[0].MenuItems.Add(new MenuItem("Insert Page Header-Footer", new EventHandler(this.var51)));
            this._var1.MenuItems[0].MenuItems.Add(new MenuItem("Insert Group Header-Footer", new EventHandler(this.var51)));
            this._var1.MenuItems.Add(new MenuItem("-"));
            this._var1.MenuItems.Add(new MenuItem("Cut", new EventHandler(this.var51)));
            this._var1.MenuItems.Add(new MenuItem("Copy", new EventHandler(this.var51)));
            this._var1.MenuItems.Add(new MenuItem("Paste", new EventHandler(this.var51)));
            this._var1.MenuItems.Add(new MenuItem("Delete", new EventHandler(this.var51)));
            this._var1.MenuItems.Add(new MenuItem("-"));
            this._var1.MenuItems.Add(new MenuItem("Bring to Front", new EventHandler(this.var51)));
            this._var1.MenuItems.Add(new MenuItem("Send to Back", new EventHandler(this.var51)));
            this._var1.MenuItems.Add(new MenuItem("-"));
            this._var1.MenuItems.Add(new MenuItem("Format Border", new EventHandler(this.var51)));
            this._var1.MenuItems.Add(new MenuItem("-"));
            this._var1.MenuItems.Add(new MenuItem("Edit RTF", new EventHandler(this.var51)));
        }

        private void var51(object var52, EventArgs e)
        {
            MenuItem item = (MenuItem) var52;
            switch (item.Text)
            {
                case "Insert Group Header-Footer":
                    this.mtd488(SectionType.GroupHeader);
                    return;

                case "Insert Report Header-Footer":
                    this.mtd488(SectionType.ReportHeader);
                    return;

                case "Insert Page Header-Footer":
                    this.mtd488(SectionType.PageHeader);
                    return;

                case "Delete Section":
                    this.mtd489();
                    return;

                case "Format Border":
                {
                    FormatBorderDlg border = new FormatBorderDlg();
                    if (this.mtd430.mtd490.Border != null)
                    {
                        border.BorderCtl= this.mtd430.mtd490.Border;
                    }
                    border.ShowDialog();
                    if (!border.DlgResult)//.mtd4)
                    {
                        break;
                    }
                    this.mtd430.mtd491(border.BorderCtl);
                    return;
                }
                case "Reorder Group":
                    this.mtd492();
                    return;

                case "Cut":
                    this.mtd430.mtd432();
                    return;

                case "Copy":
                    this.mtd430.mtd434();
                    return;

                case "Paste":
                    this.mtd430.mtd435();
                    return;

                case "Delete":
                    this.mtd430.mtd433();
                    return;

                case "Send to Back":
                    this.mtd430.mtd458();
                    return;

                case "Bring to Front":
                    this.mtd430.mtd457();
                    return;

                case "Edit RTF":
                    this.var53((McRichText) this.mtd430.mtd490, true);
                    break;

                default:
                    return;
            }
        }

        private void var53(McRichText var124, bool var125)
        {
            if (var125)
            {
                this._var27 = new RTFEditorDlg();
                //?this._var27.mtd417(ref this._var25);
                this._var27.Rtf = var124.RTF;
                this._var27.ShowDialog();
                if (this._var27.mtd4)
                {
                    TypeDescriptor.GetProperties(var124)["RTF"].SetValue(var124, this._var27.Rtf);
                }
            }
            if (var125 && (this.mtd430.mtd474 != null))
            {
                this.mtd430.mtd474.mtd496(this._var5);
                this.mtd430.mtd479();
            }
        }

        private void var54(SectionDesigner var52, ContextMenu var55, bool var56, bool var57)
        {
            if (this._var2.GetItem(SectionType.ReportHeader) == null)
            {
                var55.MenuItems[0].MenuItems[0].Enabled = true;
            }
            else
            {
                var55.MenuItems[0].MenuItems[0].Enabled = false;
            }
            if (this._var2.GetItem(SectionType.PageHeader) == null)
            {
                var55.MenuItems[0].MenuItems[1].Enabled = true;
            }
            else
            {
                var55.MenuItems[0].MenuItems[1].Enabled = false;
            }
            if (var56)
            {
                if (var52.mtd393.Type == SectionType.ReportDetail)
                {
                    var55.MenuItems[1].Enabled = false;
                }
                else
                {
                    var55.MenuItems[1].Enabled = true;
                }
                if (this._var12)
                {
                    this.var58();
                }
                if (this._var11.Length > 1)
                {
                    var55.MenuItems[2].Enabled = true;
                }
                else
                {
                    var55.MenuItems[2].Enabled = false;
                }
            }
            else
            {
                if (this.mtd430.mtd431 > 1)
                {
                    var55.MenuItems[7].Enabled = true;
                    var55.MenuItems[8].Enabled = true;
                }
                else
                {
                    var55.MenuItems[7].Enabled = false;
                    var55.MenuItems[8].Enabled = false;
                }
                if (this.mtd430.mtd490.Type == ControlType.RichTextField)
                {
                    var55.MenuItems[11].Visible = true;
                    var55.MenuItems[12].Visible = true;
                }
                else
                {
                    var55.MenuItems[11].Visible = false;
                    var55.MenuItems[12].Visible = false;
                }
            }
            if (!var57)
            {
                IDataObject dataObject = Clipboard.GetDataObject();
                if ((dataObject != null) && dataObject.GetDataPresent("Mc_CONTROLCOMPONENTS"))
                {
                    var55.MenuItems[4].Enabled = true;
                }
                else
                {
                    var55.MenuItems[4].Enabled = false;
                }
            }
            else
            {
                var55.MenuItems[4].Enabled = false;
            }
        }

        private void var58()
        {
            int num = (this._var2.Count - 1) / 2;
            ArrayList list = new ArrayList();
            for (int i = 0; i <= num; i++)
            {
                Section section = this._var2[i];
                if (section.Type == SectionType.GroupHeader)
                {
                    list.Add(section);
                }
            }
            if (list.Count > 0)
            {
                this._var11 = new Section[list.Count];
                list.CopyTo(0, this._var11, 0, list.Count);
            }
            else
            {
                this._var11 = new Section[0];
            }
            this._var12 = false;
        }

        private void var59()
        {
            this.mtd430.mtd503(false);
            foreach (SectionDesigner mtd in this._var3)
            {
                mtd.Dispose();
            }
            this._var3.mtd387();
            if (this.mtd268 != null)
            {
                foreach (Section section in this.mtd268.Sections)
                {
                    foreach (McReportControl control in section.Controls)
                    {
                        control.Site = null;
                        control.Dispose();
                    }
                    section.Controls.Clear();
                    section.Site = null;
                    section.Dispose();
                }
                this.mtd268.Sections.Clear();
                this.mtd268.ScriptLanguage = ScriptLanguage.VB;
                this.mtd268.Script = string.Empty;
                this.mtd268.ReportWidth = 576f;
                this.mtd268.DataAdapter = null;
                this.mtd268.DataSource = null;
                this.mtd268.IsDirty = false;
                this.mtd268.MaxPages = 0L;
                this.mtd268.PageSetting = new PageSettings();
                this.mtd268.Fields.Clear();
            }
        }

        private void var60()
        {
            int num = 0x16;
            int num2 = 0;
            this._var6 = base.Width;
            this._var2 = this.mtd268.Sections;
            int count = this._var2.Count;
            Section section = null;
            for (int i = 0; i < count; i++)
            {
                section = this._var2[i];
                this.var65(section);
                num = this.var66(section, num, base.mtd500.X, num2);
                num2++;
            }
            this.var67();
        }

        private void var65(Section var70)
        {
            foreach (McReportControl control in var70.Controls)
            {
                if (control.Type == ControlType.RichTextField)
                {
                    this.var53((McRichText) control, false);
                }
            }
        }

        private int var66(Section var70, int var71, int var72, int var73)
        {
            SectionDesigner mtd = new SectionDesigner(var71, var72, this.mtd268.ReportWidth, base.Width, ref var70, ref this._var9, ref this._var10, this._var5, this._var25.Images[10]);
            mtd.mtd504 = var73;
            mtd.mtd505 += new MouseEventHandler(this.var74);
            mtd.mtd506 += new MouseEventHandler(this.var75);
            mtd.mtd507 += new MouseEventHandler(this.var76);
            mtd.mtd508 += new MouseEventHandler(this.var77);
            mtd.mtd509 += new MouseEventHandler(this.var78);
            mtd.mtd510 += new MouseEventHandler(this.var79);
            mtd.mtd511 += new MouseEventHandler(this.var80);
            mtd.mtd512 += new MouseEventHandler(this.var79);
            mtd.mtd513 += new MouseEventHandler(this.var81);
            this._var3.mtd216(var73, mtd);
            return mtd.mtd514;
        }

        private void var67()
        {
            this.var68();
            this.var58();
            this._var7 = true;
            this.mtd501();
            this.var69();
            this.mtd430.mtd502(new object[] { this.mtd268 }, SelectionTypes.Replace);
        }

        private void var68()
        {
            this._var24.X = this._var3[0].mtd408;
        }

        private void var69()
        {
            int height = this.var110() + 70;
            int width = ((int) this.mtd268.ReportWidth) + 70;
            base.mtd549 = new Size(width, height);
        }

        private void var74(object var52, MouseEventArgs e)
        {
            if (!this._var17 & !this._var16)
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void var75(object var52, MouseEventArgs e)
        {
            if (!this._var17 & !this._var16)
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void var76(object var52, MouseEventArgs e)
        {
            SectionDesigner mtd = (SectionDesigner)var52;
            this.mtd430.mtd474 = mtd;
            this.mtd430.mtd502(new object[] { mtd.mtd393 }, SelectionTypes.Replace);
            this.var54(mtd, this._var0, true, true);
            this.ContextMenu = this._var0;
        }

        private void var77(object var52, MouseEventArgs e)
        {
            SectionDesigner mtd = (SectionDesigner)var52;
            if (this.var102())
            {
                if (this.mtd483.mtd472 && this.mtd483.mtd473)
                {
                    this.mtd483.mtd479();
                    this.mtd483.mtd476 = new PointF((float) (Math.Ceiling((double) ((e.X - mtd.mtd480.X) / 6.0)) * 6.0), (float) (Math.Ceiling((double) ((e.Y - mtd.mtd480.Y) / 6.0)) * 6.0));
                    this.mtd483.mtd481();
                }
            }
            else
            {
                Point point = mtd.mtd527(e.X, e.Y);
                if (this._var13 && (e.Button == MouseButtons.Left))
                {
                    this.var103(this, e);
                }
                else if (this._var16)
                {
                    this.mtd430.mtd528(var52, e);
                }
                else if (this.mtd430.mtd528(var52, e) && !this._var17)
                {
                    this.mtd430.mtd529();
                }
                else if (this._var17)
                {
                    this.mtd430.mtd530(e);
                }
                else if (this.var104(point, mtd.mtd393.Controls) != null)
                {
                    this.Cursor = Cursors.SizeAll;
                }
                else
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void var78(object var52, MouseEventArgs e)
        {
            SectionDesigner mtd = (SectionDesigner)var52;
            SectionDesigner mtd2 = this.mtd430.mtd474;
            this._var93 = mtd;
            this.mtd430.mtd474 = mtd;
            if ((e.Button == MouseButtons.Left) && this.var102())
            {
                this.var105();
                this.mtd483.mtd473 = true;
                this.mtd483.mtd474 = mtd;
                this.mtd483.mtd356 = new PointF((float) (Math.Ceiling((double) ((e.X - mtd.mtd480.X) / 6.0)) * 6.0), (float) (Math.Ceiling((double) ((e.Y - mtd.mtd480.Y) / 6.0)) * 6.0));
                this.mtd430.mtd502(new object[] { mtd.mtd393 }, SelectionTypes.Replace);
            }
            else
            {
                Point point = mtd.mtd527(e.X, e.Y);
                this.mtd430.mtd490 = this.var104(point, mtd.mtd393.Controls);
                this.mtd430.mtd531 = new Point((int) (Math.Ceiling((double) ((e.X - mtd.mtd480.X) / 6.0)) * 6.0), (int) (Math.Ceiling((double) ((e.Y - mtd.mtd480.Y) / 6.0)) * 6.0));
                if (e.Button == MouseButtons.Left)
                {
                    if ((Control.ModifierKeys != Keys.Shift) && this.mtd430.mtd532(var52, e))
                    {
                        this._var16 = true;
                    }
                    else if (this.mtd430.mtd490 != null)
                    {
                        if (mtd2 != mtd)
                        {
                            this.mtd430.mtd465();
                        }
                        this.mtd430.mtd533(e);
                        this._var17 = true;
                    }
                    else
                    {
                        this._var13 = true;
                        this._var15.X = e.X;
                        this._var15.Y = e.Y;
                        this.mtd430.mtd502(new object[] { mtd.mtd393 }, SelectionTypes.Replace);
                    }
                }
                else if (this.mtd430.mtd490 != null)
                {
                    this.mtd430.mtd533(e);
                }
                else
                {
                    this.mtd430.mtd502(new object[] { mtd.mtd393 }, SelectionTypes.Replace);
                }
                if (this.mtd430.mtd490 != null)
                {
                    this.var54(mtd, this._var1, false, false);
                    this.ContextMenu = this._var1;
                }
                else
                {
                    this.var54(mtd, this._var0, true, false);
                    this.ContextMenu = this._var0;
                }
            }
        }

        private void var79(object var52, MouseEventArgs e)
        {
            this.Cursor = Cursors.SizeNS;
        }

        private void var80(object var52, MouseEventArgs e)
        {
            SectionDesigner mtd = (SectionDesigner)var52;
            this._var20 = e.Y;
            this._var18 = true;
            this._var21 = mtd;
            this._var22.Y = this._var21.mtd534;
            this.Cursor = Cursors.HSplit;
            if (this.mtd430.mtd474 != mtd)
            {
                this.mtd430.mtd474 = mtd;
                this.mtd430.mtd502(new object[] { mtd.mtd393 }, SelectionTypes.Replace);
            }
        }

        private void var81(object var52, MouseEventArgs e)
        {
            SectionDesigner mtd = (SectionDesigner)var52;
            this._var20 = e.Y;
            this._var18 = true;
            this._var21 = this._var3[mtd.mtd504 - 1];
            this._var22.Y = this._var21.mtd534;
            if (this.mtd430.mtd474 != mtd)
            {
                this.mtd430.mtd474 = mtd;
                this.mtd430.mtd502(new object[] { mtd.mtd393 }, SelectionTypes.Replace);
            }
        }

        private int var83(SectionType var82, int var85, int var71)
        {
            Section section;
            int num = 0x24;
            if (var82 == SectionType.ReportHeader)
            {
                section = (Section) this._var30.mtd426.CreateComponent(typeof(ReportHeader), "ReportHeader");
            }
            else if (var82 == SectionType.PageHeader)
            {
                section = (Section) this._var30.mtd426.CreateComponent(typeof(PageHeader), "PageHeader");
            }
            else if (var82 == SectionType.GroupHeader)
            {
                section = (Section) this._var30.mtd426.CreateComponent(typeof(GroupHeader));
                ((GroupHeader) section).Index = var85;
            }
            else if (var82 == SectionType.ReportDetail)
            {
                section = (Section) this._var30.mtd426.CreateComponent(typeof(ReportDetail), "ReportDetail");
                num = 0xc0;
            }
            else if (var82 == SectionType.GroupFooter)
            {
                section = (Section) this._var30.mtd426.CreateComponent(typeof(GroupFooter));
            }
            else if (var82 == SectionType.PageFooter)
            {
                section = (Section) this._var30.mtd426.CreateComponent(typeof(PageFooter), "PageFooter");
            }
            else if (var82 == SectionType.ReportFooter)
            {
                section = (Section) this._var30.mtd426.CreateComponent(typeof(ReportFooter), "ReportFooter");
            }
            else
            {
                section = null;
                return 0;
            }
            section.Height = num;
            this._var2.Insert(var85, section);
            this.ForceReportUpdate();
            return this.var66(section, var71, base.mtd500.X, var85);
        }

        private void var84()
        {
            int height = this.var110() + 70;
            base.mtd549 = new Size(base.mtd549.Width, height);
        }

        private void var86(object var52, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.FillRectangle(SystemBrushes.Control, 0, 0, base.Width, 0x16);
            g.DrawRectangle(SystemPens.GrayText, this._var8);
            Region clip = g.Clip;
            e.Graphics.SetClip(new Rectangle(0x16, 4, base.Width, 14));
            this.var90(g);
            g.Clip = clip;
        }

        private void var87(object var52, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(SystemBrushes.Control, 0, 0x16, 0x16, base.Height);
        }

        private void var88(object var52, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Region clip = g.Clip;
            Rectangle rect = new Rectangle(0, 0x16, base.Width, base.Height);
            g.SetClip(rect);
            foreach (SectionDesigner mtd in this._var3)
            {
                mtd.mtd98(g);
            }
            if (this.mtd430.mtd431 > 0)
            {
                Region region2 = g.Clip;
                Matrix transform = e.Graphics.Transform;
                g.SetClip(this.mtd430.mtd474.mtd519, CombineMode.Intersect);
                g.TranslateTransform((float) this.mtd430.mtd474.mtd480.X, (float) this.mtd430.mtd474.mtd480.Y);
                if (this._var17 | this._var16)
                {
                    this.mtd430.mtd520(e.Graphics);
                }
                this.mtd430.mtd521(null, e.Graphics);
                g.Transform = transform;
                g.Clip = region2;
            }
            else if (this.mtd483.mtd472 && this.mtd483.mtd473)
            {
                Region region3 = g.Clip;
                Matrix matrix2 = e.Graphics.Transform;
                g.SetClip(this.mtd483.mtd474.mtd519, CombineMode.Intersect);
                g.TranslateTransform((float) this.mtd483.mtd474.mtd480.X, (float) this.mtd483.mtd474.mtd480.Y);
                this.mtd483.mtd478(g);
                g.Transform = matrix2;
                g.Clip = region3;
            }
            else if (this._var13)
            {
                float[] numArray = new float[] { 3f, 2f, 3f, 2f };
                using (Pen pen = new Pen(Color.Black, 1f))
                {
                    pen.DashPattern = numArray;
                    e.Graphics.DrawRectangle(pen, this._var14.X, this._var14.Y, this._var14.Width, this._var14.Height);
                }
            }
            e.Graphics.Clip = clip;
        }

        private void var89(Rectangle var91, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Region clip = graphics.Clip;
            graphics.SetClip(new Rectangle(0x16, 0x16, base.Width, base.Height));
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(150, Color.Black)))
            {
                graphics.FillRectangle(brush, var91);
            }
            e.Graphics.Clip = clip;
        }

        private void var90(Graphics g)
        {
            int x = base.mtd500.X;
            g.FillRectangle(SystemBrushes.Window, 0x16, 4, this._var24.Left, 14);
            g.FillRectangle(SystemBrushes.AppWorkspace, this._var24.Left, 4, base.Width, 14);
            int num2 = 1;
            int num3 = 11;
            int num4 = 0;
            for (int i = this._var4 + x; i <= base.Width; i += this._var4)
            {
                num4++;
                switch (num4)
                {
                    case 4:
                        g.DrawLine(SystemPens.WindowText, i, num3 - 3, i, num3 + 3);
                        break;

                    case 8:
                        if (num2 == 20)
                        {
                            g.DrawLine(SystemPens.WindowText, i, num3 - 3, i, num3 + 3);
                            num2++;
                        }
                        else
                        {
                            float height = g.MeasureString(num2.ToString(), this._var9).Height;
                            float width = g.MeasureString(num2.ToString(), this._var9).Width;
                            g.DrawString(num2.ToString(), this._var9, SystemBrushes.ControlText, (float) (i - (width / 2.2f)), (float) (num3 - (height / 2.2f)));
                            num2++;
                            num4 = 0;
                        }
                        break;

                    default:
                        g.DrawLine(SystemPens.WindowText, i, num3 - 1, i, num3 + 1);
                        break;
                }
            }
        }

        private bool var94(MouseEventArgs e, MouseState var101)
        {
            foreach (SectionDesigner mtd in this._var3)
            {
                if (mtd.mtd526(e, var101))
                {
                    return true;
                }
            }
            return false;
        }

        private void var95()
        {
            this._var13 = false;
            this.mtd430.mtd535(this._var14);
            this._var14 = Rectangle.Empty;
            this.mtd522(Rectangle.Empty);
        }

        private void var96()
        {
            Section component = this._var21.mtd393;
            using (DesignerTransaction transaction = this._var30.mtd426.CreateTransaction("Change-SectionHeight-" + this._var21.mtd91))
            {
                TypeDescriptor.GetProperties(component)["Height"].SetValue(component, (float) (this._var22.Top - this._var21.mtd537));
                transaction.Commit();
            }
        }

        private void var97()
        {
            int num;
            int num2 = this._var24.Left - this._var21.mtd408;
            int reportWidth = (int) this.mtd268.ReportWidth;
            if ((reportWidth + num2) < 6)
            {
                num = 6;
            }
            else
            {
                num = reportWidth + num2;
            }
            using (DesignerTransaction transaction = this._var30.mtd426.CreateTransaction("Change-ReportWidth"))
            {
                TypeDescriptor.GetProperties(this.mtd268)["ReportWidth"].SetValue(this.mtd268, (float) num);
                transaction.Commit();
            }
        }

        private void var98()
        {
            McReportControl control;
            this.Cursor = Cursors.Default;
            this.mtd483.mtd472 = false;
            this._var30.mtd427.SelectedToolboxItemUsed();
            this.mtd483.mtd477((int) this.mtd268.ReportWidth, null, out control, ref this._var28);
            if (control != null)
            {
                this.mtd483.mtd474.mtd496(this._var5);
                this.mtd430.mtd490 = control;
                this.mtd430.mtd474 = this.mtd483.mtd474;
                this.mtd430.mtd502(new object[] { control }, SelectionTypes.Replace);
            }
            this.mtd483.mtd111();
            this.mtd516(LayoutChangedType.ControlAdd);
        }

        private void var99(int var111, int var112)
        {
            int num = this._var21.mtd537;
            if ((this._var22.Y + (var112 - this._var20)) >= num)
            {
                this._var22.Y += var112 - this._var20;
                this._var20 = var112;
            }
            else
            {
                this._var22.Y = num;
                this._var20 = num;
            }
            this.var113();
            base.Invalidate();
        }

        internal bool mtd493
        {
            get
            {
                return this._var7;
            }
        }

        internal int mtd494
        {
            get
            {
                return this._var4;
            }
        }

        internal bool mtd495
        {
            get
            {
                return this._var5;
            }
            set
            {
                if (this._var5 != value)
                {
                    this._var5 = value;
                    foreach (SectionDesigner mtd in this._var3)
                    {
                        mtd.mtd496(this._var5);
                    }
                    base.Invalidate();
                    base.Update();
                }
            }
        }

        internal mtd389 mtd497
        {
            get
            {
                return this._var3;
            }
        }
    }
}

