using System;

namespace MControl.Win
{

    //public enum PermsLevel
    //{
    //    DenyAll = 0,
    //    ReadOnly = 1,
    //    EditOnly = 2,
    //    FullControl = 3
    //}

    public enum UITypes
    {
        Form = 1,
        Menu = 2,
        Button = 3,
        Report = 4,
        EditBox = 5
    }

    public enum EncodingType
    {
        ASCII,
        Unicode,
        UTF7,
        UTF8
    }

    public enum CompareMethod
    {
        Binary,
        Text
    }


    #region Format Help

    /** Output.
   *
   * d :08/17/2000
   * D :Thursday, August 17, 2000
   * f :Thursday, August 17, 2000 16:32
   * F :Thursday, August 17, 2000 16:32:32
   * g :08/17/2000 16:32
   * G :08/17/2000 16:32:32
   * m :August 17
   * r :Thu, 17 Aug 2000 23:32:32 GMT
   * s :2000-08-17T16:32:32
   * t :16:32
   * T :16:32:32
   * u :2000-08-17 23:32:32Z
   * U :Thursday, August 17, 2000 23:32:32
   * y :August, 2000
   * dddd, MMMM dd yyyy :Thursday, August 17 2000
   * ddd, MMM d "'"yy :Thu, Aug 17 '00
   * dddd, MMMM dd :Thursday, August 17
   * M/yy :8/00
   * dd-MM-yy :17-08-00
   
	Format Character Description and Associated Properties 
	c, C Currency format. CurrencyNegativePattern, CurrencyPositivePattern, CurrencySymbol, CurrencyGroupSizes, CurrencyGroupSeparator, CurrencyDecimalDigits, CurrencyDecimalSeparator. 
	d, D Decimal format. 
	e, E Scientific (exponential) format. 
	f, F Fixed-point format. 
	g, G General format. 
	n, N Number format. NumberNegativePattern, NumberGroupSizes, NumberGroupSeparator, NumberDecimalDigits, NumberDecimalSeparator. 
	r, R Roundtrip format, which ensures that numbers converted to strings will have the same value when they are converted back to numbers. 
	x, X Hexadecimal format. 

	
		Console.WriteLine("The examples in en-US culture:")
		Console.WriteLine(MyDouble.ToString("C"))
		Console.WriteLine(MyDouble.ToString("E"))
		Console.WriteLine(MyDouble.ToString("P"))
		Console.WriteLine(MyDouble.ToString("N"))
		Console.WriteLine(MyDouble.ToString("F"))

   */
    #endregion

  
    /// <summary>
    /// Date Type of the component.
    /// </summary>
    public enum BaseDataType
    {
        /// <summary>
        /// Monetary data type.
        /// </summary>
        Currency,
        /// <summary>
        /// DateTime data type.
        /// </summary>
        Date,
        /// <summary>
        /// Double data type.
        /// </summary>
        Double,
        /// <summary>
        /// Integer data type.
        /// </summary>
        Integer,
        /// <summary>
        /// Default - string data type.
        /// </summary>
        String
    }


    public enum VisualStyle
    {
        IDE = 0,
        Plain = 1
    }
    
    public enum Edge
    {
        Top,
        Left,
        Bottom,
        Right,
        None
    }

   

    /// <summary>
    /// Operations that can be perform in Compare Validation.
    /// </summary>
    public enum ValidationOperator
    {
        /// <summary>
        /// Default - Check component DataType.
        /// </summary>
        DataTypeCheck,
        /// <summary>
        /// Compare is equal to ValueToCompare.
        /// </summary>
        Equal,
        /// <summary>
        /// Compare is greater than ValueToCompare.
        /// </summary>
        GreaterThan,
        /// <summary>
        /// Compare greater than or equal to ValueToCompare.
        /// </summary>
        GreaterThanEqual,
        /// <summary>
        /// Compare is less than ValueToCompare.
        /// </summary>
        LessThan,
        /// <summary>
        /// Compare is greater than ValueToCompare.
        /// </summary>
        LessThanEqual,
        /// <summary>
        /// Compare is not equal to ValueToCompare.
        /// </summary>
        NotEqual
    }
}


