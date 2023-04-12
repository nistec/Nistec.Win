using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using Nistec.Printing.Csv;

namespace Nistec.Printing.View
{
	internal class CSVprop
	{
		// Fields

        private bool _FirstRowHeader;
        private Nistec.Printing.View.ExportType _Format;
	
		internal CSVprop()
		{
			this.SetDefault();
		}

  
		internal void SetDefault()
		{
            this._Format = Nistec.Printing.View.ExportType.Csv;
            this._FirstRowHeader = true;
		}

 
		public bool ShouldSerializeExportType()
		{
            return (this._Format != Nistec.Printing.View.ExportType.Csv);
		}

  		[Description("Determines Export Type for output.")]
        public Nistec.Printing.View.ExportType Format
		{
			get
			{
				return this._Format;
			}
			set
			{
				this._Format = value;
			}
		}
        [Description("Determines if the FirstRow is Header.")]
        public bool FirstRowHeader
        {
            get
            {
                return this._FirstRowHeader;
            }
            set
            {
                this._FirstRowHeader = value;
            }
        }
	}


}
