using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

using Nistec.Printing.View.Html;

namespace Nistec.Printing.View
{
	public class Htmlprop//cls1085
	{

		// Fields
		private string _Title;
		private bool _IsMultiPage;
		private HtmlCharacterSet _HtmlCharacterSet;//var2;

        public static Htmlprop Default()
        {
            Htmlprop p = new Htmlprop();
            return p;
        }

		internal Htmlprop()
		{
			this.cls111();
		}

 
		internal void cls111()
		{
			this._Title = string.Empty;
			this._IsMultiPage = false;
			this._HtmlCharacterSet = HtmlCharacterSet.utf_8;
		}

 
		public bool ShouldSerializeCharSet()
		{
			return (this._HtmlCharacterSet != HtmlCharacterSet.utf_8);
		}

		public bool ShouldSerializeTitle()
		{
			return (this._Title != null);
		}

 
		[Description("CharSet used for HTML pages.")]
		public HtmlCharacterSet CharSet
		{
			get
			{
				return this._HtmlCharacterSet;
			}
			set
			{
				this._HtmlCharacterSet = value;
			}
		}
 
		[DefaultValue(false), Description("Determines whether multiple HTML pages are generated for the document. If true, one HTML page will be generated for each page in the reports document. If false, (the default value) one HTML page is generated for the all pages in the Reports document.")]
		public bool IsMultiPage
		{
			get
			{
				return this._IsMultiPage;
			}
			set
			{
				this._IsMultiPage = value;
			}
		}
 
		[Description("Title used in head of HTML pages.")]
		public string Title
		{
			get
			{
				return this._Title;
			}
			set
			{
				this._Title = value;
			}
		}
 
	}

}
