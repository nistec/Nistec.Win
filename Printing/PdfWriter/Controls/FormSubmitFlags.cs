namespace MControl.Printing.Pdf.Controls
{
    using System;

    [Flags]
    public enum FormSubmitFlags
    {
        CanonicalFormat = 0x100,
        EmbedForm = 0x800,
        ExclFKey = 0x400,
        ExclNonUserAnnots = 0x200,
        ExportFormat = 2,
        GetMethod = 4,
        IncludeAnnotations = 0x40,
        IncludeAppendSaves = 0x20,
        IncludeExclude = 0,
        IncludeNoValueFields = 1,
        SubmitCoordinates = 8,
        SubmitPdf = 0x80,
        XFDF = 0x10
    }
}

