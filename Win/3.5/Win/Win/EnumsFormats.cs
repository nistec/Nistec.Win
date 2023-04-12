using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MControl.Win
{

    #region Enums
    public enum DateInterval
    {
        Year,
        Quarter,
        Month,
        DayOfYear,
        Day,
        WeekOfYear,
        Weekday,
        Hour,
        Minute,
        Second
    }

    public enum FirstDayOfWeek
    {
        System,
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday
    }


    #endregion

    public enum Direction
    {
        Vertical = 0,
        Horizontal = 1
    }

    public enum Alignment
    {
        Center,
        Left,
        Right
    }

    #region Format

    public enum Formats
    {
        None,
        GeneralNumber,
        FixNumber,
        StandadNumber,
        Money,
        GeneralDate,
        LongDate,
        ShortDate,
        LongTime,
        ShortTime,
        Percent
        //		TrueFalse,
        //		YesNo,
        //		OnOff,
        //		CustomDate,
        //		CustomNumber
    }

    public enum DateFormats
    {
        GeneralDate,
        LongDate,
        ShortDate,
        LongTime,
        ShortTime,
        CustomDate
    }

    public enum NumberFormats
    {
        GeneralNumber,
        FixNumber,
        StandadNumber,
        Money,
        Percent,
        CustomNumber
    }

    public enum BoolFormats
    {
        TrueFalse,
        YesNo,
        OnOff
    }

    public enum FieldType//DataTypes//BaseFormats
    {
        Text = 0,
        Number = 1,
        Date = 2,
        Bool = 3
    }

    public enum DateTimeFormats
    {
        ShortDatePattern,
        LongDatePattern,
        FullDateAndShortTimePattern,
        FullDateAndLongTimePattern,
        GeneralShortTime,
        GeneralLongTime,
        MonthDayPattern,
        RFC1123Pattern,
        SortableDateTimePattern,
        ShortTimePattern,
        LongTimePattern,
        UniversalSortableDateTimePattern,
        FullLongDateAndLongTime,
        YearMonthPattern
    }
    #endregion

}
