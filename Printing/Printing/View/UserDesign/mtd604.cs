namespace MControl.Printing.View.Design.UserDesigner
{
    using System;
    using System.ComponentModel.Design;

    internal class mtd604 : DesignerTransaction
    {
        private mtd426 _var0;

        internal mtd604(mtd426 var0)
        {
            this._var0 = var0;
        }

        internal mtd604(mtd426 var0, string var1) : base(var1)
        {
            this._var0 = var0;
        }

        protected override void OnCancel()
        {
            this._var0.mtd601(false);
            this._var0.mtd602(false);
        }

        protected override void OnCommit()
        {
            this._var0.mtd601(true);
            this._var0.mtd602(true);
        }
    }
}

