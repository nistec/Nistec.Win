using System;
using System.Collections.Generic;
using System.Text;

namespace MControl.Printing.View.Templates
{
    public enum InvoiceType
    {
        InvoiceEN,
        InvoiceIL
    }
    public interface IInvoice
    {
        void AddTitle(string title, string subTitle);
        void AddHeader(string customerId, string customrName, string address, string date, string details);
        void AddFooter(string footer, float vat);
        InvoiceType InvoiceType{get;}
        MControl.Printing.View.Design.ReportDesign IReport { get; }

    }
}
