namespace MControl.Printing.View.Design
{
    using MControl.Printing.View;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Drawing;
    using System.Runtime.InteropServices;

    internal class mtd471
    {
        private string _var0;
        private float _var1;
        private SectionDesigner _var10;
        private ControlType _var11;
        private Rectangle _var12;
        private PanelDesiger _var13;
        private IDesignerHost _var14;
        private float _var2;
        private float _var3;
        private float _var4;
        private bool _var5;
        private bool _var6;
        private bool _var7;
        private PointF _var8;
        private PointF _var9;

        internal mtd471(PanelDesiger var13, IDesignerHost var14)
        {
            this._var13 = var13;
            this._var14 = var14;
        }

        internal void mtd111()
        {
            this._var3 = 0f;
            this._var4 = 0f;
            this._var5 = false;
            this._var6 = false;
            this._var7 = false;
            this._var8 = new PointF(0f, 0f);
            this._var9 = new PointF(0f, 0f);
            this._var0 = string.Empty;
        }

        internal void mtd477(int var16, string var17, out McReportControl c, ref bool var18)
        {
            c = null;
            if (this._var7)
            {
                if (this._var11 == ControlType.Line)
                {
                    if (this._var8.Equals(this._var9))
                    {
                        return;
                    }
                }
                else if ((this._var11 != ControlType.PageBreak) && ((this._var3 == 0f) | (this._var4 == 0f)))
                {
                    return;
                }
                using (DesignerTransaction transaction = this._var14.CreateTransaction("Add-" + this._var11.ToString()))
                {
                    var18 = false;
                    c = (McReportControl) this.var19();
                    if (c != null)
                    {
                        if (this._var11 == ControlType.Line)
                        {
                            McLine line = (McLine) c;
                            line.X1 = this._var8.X;
                            line.Y1 = this._var8.Y;
                            line.X2 = this._var9.X;
                            line.Y2 = this._var9.Y;
                        }
                        else if (this._var11 == ControlType.PageBreak)
                        {
                            c.Top = this._var2;
                        }
                        else
                        {
                            c.DataField = this._var0;
                            c.Location = new PointF(this._var1, this._var2);
                            c.Size = new SizeF(this._var3, this._var4);
                        }
                        Section s = this._var10.mtd393;
                        c.Parent = s;
                        s.Controls.Add(c);
                        this.ForceSectionUpdate(s);
                        transaction.Commit();
                        var18 = true;
                    }
                }
            }
        }

        internal void mtd478(Graphics g)
        {
            if (this._var7)
            {
                if (this._var11 == ControlType.Line)
                {
                    g.DrawLine(SystemPens.WindowFrame, this._var8, this._var9);
                }
                else
                {
                    using (Pen pen = new Pen(Color.FromArgb(200, Color.Black), 2f))
                    {
                        Rectangle rect = new Rectangle((int) this._var1, (int) this._var2, (int) this._var3, (int) this._var4);
                        g.DrawRectangle(pen, rect);
                    }
                }
            }
        }

        internal void mtd479()
        {
            this._var12 = new Rectangle(((int) this._var1) + this._var10.mtd480.X, ((int) this._var2) + this._var10.mtd480.Y, (int) this._var3, (int) this._var4);
        }

        internal void mtd481()
        {
            Rectangle a = new Rectangle(((int) this._var1) + this._var10.mtd480.X, ((int) this._var2) + this._var10.mtd480.Y, (int) this._var3, (int) this._var4);
            a = Rectangle.Union(a, this._var12);
            this._var12 = a;
            a.Inflate(12, 12);
            this._var13.Invalidate(a);
            this._var13.Update();
        }

        private void ForceSectionUpdate(Section s)
        {
            TypeDescriptor.GetProperties(s)["IsDirty"].SetValue(s, s.IsDirty);
        }

        private void var15()
        {
            if (this._var8.X > this._var9.X)
            {
                this._var1 = this._var9.X;
                this._var3 = this._var8.X - this._var9.X;
            }
            else
            {
                this._var1 = this._var8.X;
                this._var3 = this._var9.X - this._var8.X;
            }
            if (this._var8.Y > this._var9.Y)
            {
                this._var2 = this._var9.Y;
                this._var4 = this.mtd356.Y - this.mtd476.Y;
            }
            else
            {
                this._var2 = this._var8.Y;
                this._var4 = this._var9.Y - this._var8.Y;
            }
        }

        private IComponent var19()
        {
            if (this._var11 == ControlType.Label)
            {
                return this._var14.CreateComponent(typeof(McLabel));
            }
            if (this._var11 == ControlType.TextBox)
            {
                return this._var14.CreateComponent(typeof(McTextBox));
            }
            if (this._var11 == ControlType.Line)
            {
                return this._var14.CreateComponent(typeof(McLine));
            }
            if (this._var11 == ControlType.Shape)
            {
                return this._var14.CreateComponent(typeof(McShape));
            }
            if (this._var11 == ControlType.Picture)
            {
                return this._var14.CreateComponent(typeof(McPicture));
            }
            if (this._var11 == ControlType.PageBreak)
            {
                return this._var14.CreateComponent(typeof(McPageBreak));
            }
            if (this._var11 == ControlType.CheckBox)
            {
                return this._var14.CreateComponent(typeof(McCheckBox));
            }
            if (this._var11 == ControlType.RichTextField)
            {
                return this._var14.CreateComponent(typeof(McRichText));
            }
            if (this._var11 == ControlType.SubReport)
            {
                return this._var14.CreateComponent(typeof(McSubReport));
            }
            return null;
        }

        internal ControlType mtd138
        {
            get
            {
                return this._var11;
            }
            set
            {
                this._var11 = value;
            }
        }

        internal PointF mtd356
        {
            get
            {
                return this._var8;
            }
            set
            {
                this._var8 = value;
            }
        }

        internal bool mtd472
        {
            get
            {
                return this._var5;
            }
            set
            {
                this._var5 = value;
            }
        }

        internal bool mtd473
        {
            get
            {
                return this._var6;
            }
            set
            {
                this._var6 = value;
            }
        }

        internal SectionDesigner mtd474
        {
            get
            {
                return this._var10;
            }
            set
            {
                this._var10 = value;
            }
        }

        internal string mtd475
        {
            get
            {
                return this._var0;
            }
            set
            {
                this._var0 = value;
            }
        }

        internal PointF mtd476
        {
            get
            {
                return this._var9;
            }
            set
            {
                this._var9 = value;
                this._var7 = true;
                this.var15();
            }
        }
    }
}

