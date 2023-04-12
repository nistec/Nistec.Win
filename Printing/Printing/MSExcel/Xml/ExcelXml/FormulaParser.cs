namespace Nistec.Printing.ExcelXml
{
    using System;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;

    internal static class FormulaParser
    {
        private const string AbsoluteRangePattern = @"^((\[(?<File>[\w\.]+)\])?('?(?<Sheet>.+)'?!))?R(\[(?<RowStart>[\-0-9]+)\])?C(\[(?<ColStart>[\-0-9]+)\])?(:R(\[(?<RowEnd>[\-0-9]+)\])?C(\[(?<ColEnd>[\-0-9]+)\])?)?$";
        private static Regex AbsoluteRangeRegex = new Regex(@"^((\[(?<File>[\w\.]+)\])?('?(?<Sheet>.+)'?!))?R(\[(?<RowStart>[\-0-9]+)\])?C(\[(?<ColStart>[\-0-9]+)\])?(:R(\[(?<RowEnd>[\-0-9]+)\])?C(\[(?<ColEnd>[\-0-9]+)\])?)?$");
        private const string FunctionPattern = @"^(?<FunctionName>[\w\+\-]+)\((?<Parameters>.*)\)$";
        private static Regex FunctionRegex = new Regex(@"^(?<FunctionName>[\w\+\-]+)\((?<Parameters>.*)\)$");
        private const string PrintColumnsPattern = @"^('?(?<Sheet>.+)'?!)?C(?<ColStart>\d+)(:C(?<ColEnd>\d+))?$";
        private static Regex PrintColumnsRegex = new Regex(@"^('?(?<Sheet>.+)'?!)?C(?<ColStart>\d+)(:C(?<ColEnd>\d+))?$");
        private const string PrintRowPattern = @"^('?(?<Sheet>.+)'?!)?R(?<RowStart>\d+)(:R(?<RowEnd>\d+))?$";
        private static Regex PrintRowRegex = new Regex(@"^('?(?<Sheet>.+)'?!)?R(?<RowStart>\d+)(:R(?<RowEnd>\d+))?$");
        private const string RangePattern = @"^((\[(?<File>[\w\.]+)\])?('?(?<Sheet>.+)'?!))?R(?<RowStart>\d+)?C(?<ColStart>\d+)?(:R(?<RowEnd>\d+)?C(?<ColEnd>\d+)?)?$";
        private static Regex RangeRegex = new Regex(@"^((\[(?<File>[\w\.]+)\])?('?(?<Sheet>.+)'?!))?R(?<RowStart>\d+)?C(?<ColStart>\d+)?(:R(?<RowEnd>\d+)?C(?<ColEnd>\d+)?)?$");

        internal static ParseArgumentType GetArgumentType(string argument, out Match match)
        {
            Match match2 = FunctionRegex.Match(argument);
            if (match2.Success)
            {
                match = match2;
                return ParseArgumentType.Function;
            }
            Match match3 = RangeRegex.Match(argument);
            if (match3.Success)
            {
                match = match3;
                return ParseArgumentType.Range;
            }
            Match match4 = AbsoluteRangeRegex.Match(argument);
            if (match4.Success)
            {
                match = match4;
                return ParseArgumentType.AbsoluteRange;
            }
            match = null;
            return ParseArgumentType.None;
        }

        private static Cell GetCell(Worksheet ws, Cell cell, bool absolute, int row, int col)
        {
            if (absolute)
            {
                col = cell.CellIndex + col;
                row = cell.ParentRow.RowIndex + row;
            }
            return ws[col, row];
        }

        internal static void Parse(Cell cell, string formulaText,object cellValue)
        {
            if (formulaText[0] != '=')
            {
                cell.Value = formulaText;
                cell.Content = ContentType.UnresolvedValue;
            }
            formulaText = formulaText.Substring(1);
            Formula formula = new Formula();
            formula.Value = cellValue;
            ParseFormula(cell, formula, formulaText);
            if (string.IsNullOrEmpty(formula.Function) && (formula.parameters[0].ParameterType == ParameterType.String))
            {
                cell.Value = formulaText;
                cell.Content = ContentType.UnresolvedValue;
            }
            //else if (formula.parameters[0].ParameterType == ParameterType.Formula)
            //{
            //    cell.Value = formula.parameters[0].Value as Formula;
            //    cell.Content = ContentType.Formula;
            //}
            else
            {
                cell.Value = formula;
                //cell.Content = ContentType.Formula;
            }
        }

        internal static void ParseFormula(Cell cell, Formula formula, string formulaText)
        {
            Match match;
            switch (GetArgumentType(formulaText, out match))
            {
                case ParseArgumentType.None:
                    formula.Add(formulaText);
                    break;

                case ParseArgumentType.Function:
                {
                    Formula formula2 = new Formula(match.Groups["FunctionName"].Value);
                    string[] strArray = match.Groups["Parameters"].Value.Split(new char[] { ',' });
                    foreach (string str2 in strArray)
                    {
                        ParseFormula(cell, formula2, str2);
                    }
                    formula.Add(formula2);
                    break;
                }
                case ParseArgumentType.Range:
                case ParseArgumentType.AbsoluteRange:
                {
                    Range range = new Range(formulaText);
                    formula.Add(range);
                    break;
                }
            }
        }

        internal static void ParsePrintHeaders(Worksheet ws, string range)
        {
            string[] strArray = range.Split(new char[] { ',' });
            foreach (string str in strArray)
            {
                int num;
                int num2;
                Match match = PrintRowRegex.Match(str);
                if (match.Success)
                {
                    num2 = 0;
                    if (int.TryParse(match.Groups["RowStart"].Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
                    {
                        if (!(match.Groups["RowEnd"].Success && int.TryParse(match.Groups["RowEnd"].Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out num2)))
                        {
                            num2 = num;
                        }
                        ws.PrintOptions.SetTitleRows(num, num2);
                    }
                }
                else
                {
                    match = PrintColumnsRegex.Match(str);
                    if (match.Success)
                    {
                        num2 = 0;
                        if (int.TryParse(match.Groups["ColStart"].Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
                        {
                            if (!(match.Groups["ColEnd"].Success && int.TryParse(match.Groups["ColEnd"].Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out num2)))
                            {
                                num2 = num;
                            }
                            ws.PrintOptions.SetTitleColumns(num, num2);
                        }
                    }
                }
            }
        }

        internal static bool ParseRange(Cell cell, Match match, out Range range, bool absolute)
        {
            Worksheet parentSheet;
            range = null;
            if (match.Groups["File"].Success)
            {
                return false;
            }
            if (match.Groups["Sheet"].Success)
            {
                string text = match.Groups["Sheet"].Value;

                if (text.EndsWith("'"))//.Right(1) == "'")
                {
                    text = text.Substring(0,text.Length - 1);
                }
                parentSheet = cell.GetParentBook()[text];
            }
            else
            {
                parentSheet = cell.ParentRow.ParentSheet;
            }
            if (parentSheet == null)
            {
                return false;
            }
            int result = 0;
            int num2 = 0;
            Cell cellTo = null;
            if ((match.Groups["RowStart"].Success && int.TryParse(match.Groups["RowStart"].Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out result)) && !absolute)
            {
                result--;
            }
            if ((match.Groups["ColStart"].Success && int.TryParse(match.Groups["ColStart"].Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out num2)) && !absolute)
            {
                num2--;
            }
            Cell cellFrom = GetCell(parentSheet, cell, absolute, result, num2);
            if (match.Groups["RowEnd"].Success)
            {
                int num3 = 0;
                int num4 = 0;
                if ((match.Groups["RowEnd"].Success && int.TryParse(match.Groups["RowEnd"].Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out num3)) && !absolute)
                {
                    num3--;
                }
                if ((match.Groups["ColEnd"].Success && int.TryParse(match.Groups["ColEnd"].Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out num4)) && !absolute)
                {
                    num4--;
                }
                cellTo = GetCell(parentSheet, cell, absolute, num3, num4);
            }
            range = new Range(cellFrom, cellTo);
            return true;
        }
    }
}

