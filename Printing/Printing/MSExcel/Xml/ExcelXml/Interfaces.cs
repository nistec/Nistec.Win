namespace Nistec.Printing.ExcelXml
{
    using System;
    using System.Drawing;

    public interface IStyle
    {
        IAlignmentOptions Alignment { get; set; }

        IBorderOptions Border { get; set; }

        DisplayFormat DisplayFormat { get; set; }

        IFontOptions Font { get; set; }

        IInteriorOptions Interior { get; set; }
    }

    public interface IInteriorOptions
    {
        System.Drawing.Color Color { get; set; }
    }

    public interface IAlignmentOptions
    {
        HorizontalAlignment Horizontal { get; set; }

        int Indent { get; set; }

        int Rotate { get; set; }

        bool ShrinkToFit { get; set; }

        VerticalAlignment Vertical { get; set; }

        bool WrapText { get; set; }
    }

    public interface IBorderOptions
    {
        System.Drawing.Color Color { get; set; }

        Borderline LineStyle { get; set; }

        BorderSides Sides { get; set; }

        int Weight { get; set; }
    }

    public interface IFontOptions
    {
        bool Bold { get; set; }

        System.Drawing.Color Color { get; set; }

        bool Italic { get; set; }

        string Name { get; set; }

        int Size { get; set; }

        bool Strikeout { get; set; }

        bool Underline { get; set; }
    }
}

