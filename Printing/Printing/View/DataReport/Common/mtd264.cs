namespace Nistec.Printing.View
{
    using System;

    internal class mtd264
    {
        private DataFields _var0;
        private bool _var1;
        internal CodeProvider _CodeProvider;//mtd177
        internal SectionCollection mtd265;
        internal mtd266[] mtd267 = null;
        internal Report mtd268;
        internal McSubReport mtd269;
        internal mtd270 mtd271;
        internal mtd272 mtd273;
        internal mtd272 mtd274;
        internal mtd272 mtd275;
        internal mtd272 mtd276;

        internal mtd264(ref McSubReport sr)
        {
            this.mtd269 = sr;
        }

        internal void mtd111()
        {
            this._var0.mtd111();
        }

        internal void mtd174()
        {
            this._var0.mtd174();
        }

        internal void mtd192()
        {
            this._var0.mtd192 = true;
        }

        internal void mtd280()
        {
            this._var1 = false;
            if ((this.mtd268 == null) || (this.mtd269.Report != this.mtd268))
            {
                this.mtd268 = this.mtd269.Report;
                this.mtd265 = this.mtd268.Sections;
                this.mtd271 = new mtd270(this.mtd265.Count);
                this._CodeProvider = new CodeProvider();// mtd171();
                this.var2();
                this._var1 = true;
            }
            if (!this.mtd268.mtd117(Msg.ReportStart) && this._CodeProvider.mtd178)
            {
                object[] objArray = new object[] { this.mtd268, EventArgs.Empty };
                this._CodeProvider.mtd71("Report", Methods._Start, objArray);
            }
            DataFields.mtd194(this.mtd268, this._CodeProvider);
            this._var0 = this.mtd268.DataFields;
            if (this._var0 != null)
            {
                this._var0.mtd172();
            }
        }

        private void var2()
        {
            this._CodeProvider.Text = this.mtd268.Script;
            if (this._CodeProvider.mtd282())
            {
                this._CodeProvider.ScriptLanguage = this.mtd268.ScriptLanguage;
                this._CodeProvider.mtd284(true);
            }
        }

        internal bool mtd189
        {
            get
            {
                return this._var0.mtd189;
            }
        }

        internal FieldsCollection Fields
        {
            get
            {
                return this._var0.Fields;
            }
        }

        internal bool mtd277
        {
            get
            {
                return this._var1;
            }
        }

        internal int mtd278
        {
            get
            {
                return this._var0.mtd190;
            }
        }

        internal bool mtd279
        {
            get
            {
                return (this._var0 != null);
            }
        }
    }
}

