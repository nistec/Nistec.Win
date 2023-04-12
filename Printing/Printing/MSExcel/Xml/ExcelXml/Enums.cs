namespace Nistec.Printing.ExcelXml
{
    using System;

    public enum CellType
    {
        None,
        Data,
        Comment
    }

    public enum ContentType
    {
        None,
        String,
        Number,
        DateTime,
        Boolean,
        //Formula,
        UnresolvedValue
    }

    public enum DisplayFormat
    {
        None,
        Text,
        Fixed,
        Standard,
        Percent,
        Scientific,
        Currency,
        GeneralDate,
        ShortDate,
        LongDate,
        Time
    }

    public enum Borderline
    {
        Continuous,
        Dash,
        DashDot,
        DashDotDot,
        Double,
        Dot,
        SlantDashDot
    }

    [Flags]
    public enum BorderSides
    {
        None = 0,
        Top = 1,
        Left = 2,
        Bottom = 4,
        Right = 8,
        All = 16

        //All = 15,
        //Bottom = 4,
        //Left = 2,
        //None = 0,
        //Right = 8,
        //Top = 1
    };

    public enum HorizontalAlignment
    {
        None,
        Left,
        Center,
        Right,
        Fill,
        Justify,
        Distributed
    }

    public enum PageLayout
    {
        None,
        CenterHorizontal,
        CenterVertical,
        CenterVerticalAndHorizontal
    }

    public enum PageOrientation
    {
        None,
        Landscape,
        Portrait
    }

    public enum ParameterType
    {
        String,
        Range,
        Formula
    }

    internal enum ParseArgumentType
    {
        None,
        Function,
        Range,
        AbsoluteRange
    }

    public enum VerticalAlignment
    {
        None,
        Top,
        Center,
        Bottom,
        Justify,
        Distributed
    }
}

