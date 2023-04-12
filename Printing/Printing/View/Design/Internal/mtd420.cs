namespace MControl.Printing.View.Design
{
    using System;
    using System.ComponentModel.Design;
    using System.Globalization;
    using System.Resources;

    internal class mtd420 : IResourceService, IDisposable
    {
        private string _var0;
        private ResXResourceReader _var1;
        private ResXResourceWriter _var2;

        public mtd420(string var0)
        {
            this._var0 = var0;
        }

        public void Dispose()
        {
            if (this._var2 != null)
            {
                this._var2.Dispose();
                this._var2 = null;
            }
        }

        public IResourceReader GetResourceReader(CultureInfo info)
        {
            try
            {
                if (this._var1 == null)
                {
                    this._var1 = new ResXResourceReader(this._var0);
                }
                return this._var1;
            }
            catch
            {
                return null;
            }
        }

        public IResourceWriter GetResourceWriter(CultureInfo info)
        {
            try
            {
                if (this._var2 == null)
                {
                    this._var2 = new ResXResourceWriter(this._var0);
                }
                return this._var2;
            }
            catch
            {
                return null;
            }
        }
    }
}

