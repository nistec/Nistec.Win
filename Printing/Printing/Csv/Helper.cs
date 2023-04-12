namespace Nistec.Printing.Csv
{
    using Nistec.Printing;
    using System;
    using System.Collections;
    using System.Data;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using Nistec.Printing.Data;

    public class CsvHelper
    {
        private static string _lastLoadFolder = string.Empty;
        private static string _lastSaveFolder = string.Empty;
        private static int[] _maxLengths = new int[0];
        private static int _recordsRead = 0;
        private static int _totalRecordLength = 0;
        private const int DETERMINE_TYPE_AND_LENGTH_SCANROWS = 0x3e8;
        private const int FILE_FORMAT_SCANROWS = 100;
        private const int FIXED_COLUMN_SCANROWS = 0x3e8;
        private const int STRING_TYPE_ROUNDING = 50;

        public static CsvReadProperties AutoDetect(CsvReadProperties currentProperties)
        {
            string str;
            CsvReadProperties properties = (CsvReadProperties) currentProperties.Clone();
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            using (StreamReader reader = new StreamReader(currentProperties.Filename))
            {
                while (((str = ReadLine(reader, currentProperties)) != null) && (num3 < 100))
                {
                    if (str.IndexOf(',') > -1)
                    {
                        num++;
                    }
                    if (str.IndexOf('\t') > -1)
                    {
                        num2++;
                    }
                    num3++;
                }
            }
            if (num >= (num3 - 1))
            {
                properties.FileType = FileTypes.Delimited;
                properties.FieldDelimiter = ",";
                properties.TextQualifier = "\"";
                properties.DataSource = GetSchemaDelimited(properties);
                return properties;
            }
            if (num2 >= (num3 - 1))
            {
                properties.FileType = FileTypes.Delimited;
                properties.FieldDelimiter = "\t";
                properties.TextQualifier = "\"";
                properties.DataSource = GetSchemaDelimited(properties);
                return properties;
            }
            properties.FileType = FileTypes.Fixed;
            AdoTable schemaFixed = GetSchemaFixed(properties);
            if (schemaFixed.Columns.Count > 1)
            {
                properties.DataSource = schemaFixed;
                return properties;
            }
            int length = 0;
            using (StreamReader reader2 = new StreamReader(currentProperties.Filename))
            {
                while ((str = ReadLine(reader2, currentProperties)) != null)
                {
                    if (str.Length > length)
                    {
                        length = str.Length;
                    }
                }
            }
            schemaFixed.Columns.Clear();
            schemaFixed.Columns.Add(new AdoColumn("Column1", typeof(string), length));
            properties.DataSource = schemaFixed;
            return properties;
        }

        private static int CSVColumnCount(string line, string fieldDelimiter, string textQualifier)
        {
            if (line == null)
            {
                return 0;
            }
            bool flag = false;
            int num = 1;
            for (int i = 0; i < line.Length; i++)
            {
                string str = line.Substring(i, 1);
                if (str == fieldDelimiter)
                {
                    if (!flag)
                    {
                        num++;
                    }
                }
                else if (str == textQualifier)
                {
                    flag = !flag;
                }
            }
            return num;
        }

        public static string CSVCreateHeader(DataTable table, CsvWriteProperties properties)
        {
            if ((table == null) || (table.Columns.Count == 0))
            {
                return "";
            }
            StringBuilder builder = new StringBuilder();
            foreach (DataColumn column in table.Columns)
            {
                if (builder.Length > 0)
                {
                    builder.Append(properties.FieldDelimiter);
                }
                builder.Append(properties.TextQualifier);
                builder.Append(column.ColumnName);
                builder.Append(properties.TextQualifier);
            }
            builder.Append(GetRecordSeparator(properties.RecordSeparator));
            return builder.ToString();
        }

        private static string CSVReadLine(StreamReader reader, CsvReadProperties properties)
        {
            string line = ReadLine(reader, properties);
            if (line == null)
            {
                return null;
            }
            if ((CSVTextQualifierCount(line, properties.FieldDelimiter, properties.TextQualifier) % 2) != 0)
            {
                string str2;
                do
                {
                    str2 = ReadLine(reader, properties);
                    if (str2 == null)
                    {
                        return line;
                    }
                    line = line + "\r\n" + str2;
                }
                while ((CSVTextQualifierCount(str2, properties.FieldDelimiter, properties.TextQualifier) % 2) == 0);
            }
            return line;
        }

        private static int CSVTextQualifierCount(string line, string fieldDelimiter, string textQualifier)
        {
            int num = 0;
            if (line != null)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    if (line.Substring(i, 1) == textQualifier)
                    {
                        num++;
                    }
                }
            }
            return num;
        }

        private static Type DetermineType(string[] values)
        {
            if (values.Length != 0)
            {
                if (values[0] == null)
                {
                    return typeof(string);
                }
                bool flag = false;
                for (int i = 0; i < values.Length; i++)
                {
                    if (values[i].IndexOf(".") > -1)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    try
                    {
                        for (int m = 0; m < values.Length; m++)
                        {
                            decimal.Parse(values[m]);
                        }
                        return typeof(decimal);
                    }
                    catch
                    {
                        try
                        {
                            for (int n = 0; n < values.Length; n++)
                            {
                                double.Parse(values[n]);
                            }
                            return typeof(double);
                        }
                        catch
                        {
                        }
                    }
                }
                bool flag2 = true;
                for (int j = 0; j < values.Length; j++)
                {
                    if ((((values[j] != "1") && (values[j] != "0")) && ((values[j] != "-1") && (values[j].ToLower() != "true"))) && (((values[j].ToLower() != "false") && (values[j].ToLower() != "yes")) && (values[j].ToLower() != "no")))
                    {
                        flag2 = false;
                        break;
                    }
                }
                if (flag2)
                {
                    return typeof(bool);
                }
                try
                {
                    for (int num5 = 0; num5 < values.Length; num5++)
                    {
                        DateTime.Parse(values[num5]);
                    }
                    return typeof(DateTime);
                }
                catch
                {
                }
                bool flag3 = true;
                Regex regex = new Regex("^[-+]?[0-9]+$");
                for (int k = 0; k < values.Length; k++)
                {
                    if (!regex.IsMatch(values[k]))
                    {
                        flag3 = false;
                        break;
                    }
                }
                if (flag3)
                {
                    try
                    {
                        int num7 = 1;
                        for (int num8 = 0; num8 < values.Length; num8++)
                        {
                            double num9 = double.Parse(values[num8]);
                            if ((num7 == 1) && ((num9 < -32768.0) || (num9 > 32767.0)))
                            {
                                num7++;
                            }
                            if ((num7 == 2) && ((num9 < -2147483648.0) || (num9 > 2147483647.0)))
                            {
                                num7++;
                            }
                            if ((num7 == 3) && ((num9 < -9.2233720368547758E+18) || (num9 > 9.2233720368547758E+18)))
                            {
                                num7++;
                            }
                            if ((num7 == 4) && ((num9 < -3.4028234663852886E+38) || (num9 > 3.4028234663852886E+38)))
                            {
                                num7++;
                            }
                            if (num7 > 4)
                            {
                                break;
                            }
                        }
                        switch (num7)
                        {
                            case 1:
                                return typeof(short);

                            case 2:
                                return typeof(int);

                            case 3:
                                return typeof(long);

                            case 4:
                                return typeof(float);
                        }
                        return typeof(double);
                    }
                    catch
                    {
                        return typeof(string);
                    }
                }
            }
            return typeof(string);
        }

        public static string FormatRow(DataRow row, CsvWriteProperties properties)
        {
            StringBuilder builder = new StringBuilder();

            if (properties.FileType != FileTypes.Delimited)
            {
                if (properties.DataSource.Columns.Count == 0)
                {
                    return "";
                }
                foreach (AdoColumn field in properties.DataSource.Columns) //(DataColumn column2 in row.Table.Columns)
                {
                    DataColumn column2 = row.Table.Columns[field.ColumnName];

                    int length;
                    string str = "";
                    if (row[column2] != DBNull.Value)
                    {
                        string name = column2.DataType.Name;
                        if (name == null)
                        {
                            goto Label_02B7;
                        }
                        if (!(name == "DateTime"))
                        {
                            if (name == "Boolean")
                            {
                                goto Label_0280;
                            }
                            if (name == "Byte[]")
                            {
                                goto Label_029E;
                            }
                            goto Label_02B7;
                        }
                        str = ((DateTime) row[column2]).ToString(properties.DateFormat);
                    }
                    goto Label_02F1;
                Label_0280:
                    if ((bool) row[column2])
                    {
                        str = "1";
                    }
                    else
                    {
                        str = "0";
                    }
                    goto Label_02F1;
                Label_029E:
                    str = Encoding.ASCII.GetString((byte[]) row[column2]);
                    goto Label_02F1;
                Label_02B7:
                    str = row[column2].ToString().Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ");
                Label_02F1:
                    length = 0;
                    if (properties.DataSource.Columns.Contains(column2.ColumnName))
                    {
                        length = properties.DataSource.Columns[column2.ColumnName].Length;
                    }
                    if (str.Length < length)
                    {
                        str = str.PadRight(length, ' ');
                    }
                    if (str.Length > length)
                    {
                        builder.Append(str.Substring(0, length));
                        continue;
                    }
                    builder.Append(str);
                }
            }
            else
            {

                if (properties.DataSource.Columns.Count == 0)
                {
                    properties.DataSource.CreateFields(row.Table);
                }

                foreach (AdoColumn field in properties.DataSource.Columns) //(DataColumn column in row.Table.Columns)
                {
                    DataColumn column = row.Table.Columns[field.ColumnName];

                    DateTime time;
                    if (column != row.Table.Columns[0])
                    {
                        builder.Append(properties.FieldDelimiter);
                    }
                    if (row[column] != DBNull.Value)
                    {
                        string str2 = column.DataType.Name;
                        if (str2 == null)
                        {
                            goto Label_0197;
                        }
                        if (!(str2 == "String"))
                        {
                            if (str2 == "DateTime")
                            {
                                goto Label_0132;
                            }
                            if (str2 == "Boolean")
                            {
                                goto Label_0156;
                            }
                            if (str2 == "Byte[]")
                            {
                                goto Label_0178;
                            }
                            goto Label_0197;
                        }
                        builder.Append(properties.TextQualifier);
                        if (properties.TextQualifier == "")
                        {
                            builder.Append(row[column].ToString());
                        }
                        else
                        {
                            builder.Append(row[column].ToString().Replace(properties.TextQualifier, properties.TextQualifier + properties.TextQualifier));
                        }
                        builder.Append(properties.TextQualifier);
                    }
                    continue;
                Label_0132:
                    time = (DateTime) row[column];
                    builder.Append(time.ToString(properties.DateFormat));
                    continue;
                Label_0156:
                    if ((bool) row[column])
                    {
                        builder.Append(1);
                    }
                    else
                    {
                        builder.Append(0);
                    }
                    continue;
                Label_0178:
                    builder.Append(Encoding.ASCII.GetString((byte[]) row[column]));
                    continue;
                Label_0197:
                    builder.Append(row[column].ToString());
                }
            }
            return builder.ToString();
        }

 
        public static string GetRecordSeparator(RecordSeparators separator)
        {
            switch (separator)
            {
                case RecordSeparators.Unix:
                    return "\n";

                case RecordSeparators.Mac:
                    return "\r";
            }
            return "\r\n";
        }

        public static AdoTable GetSchemaDelimited(CsvReadProperties properties)
        {
            string str;
            CsvReadProperties properties2 = (CsvReadProperties) properties.Clone();
            AdoTable table = new AdoTable();
            int num = 0;
            string[] strArray = null;
            using (StreamReader reader = new StreamReader(properties2.Filename))
            {
                if (properties2.FirstRowHeaders)
                {
                    str = CSVReadLine(reader, properties2);
                    if (str != null)
                    {
                        if (properties2.TextQualifier != "")
                        {
                            str = str.Replace(properties2.TextQualifier, "");
                        }
                        strArray = str.Split(properties2.FieldDelimiter.ToCharArray());
                    }
                }
                else
                {
                    str = CSVReadLine(reader, properties2);
                }
                num = CSVColumnCount(str, properties2.FieldDelimiter, properties2.TextQualifier);
            }
            for (int i = 0; i < num; i++)
            {
                if (strArray == null)
                {
                    table.Columns.Add(new AdoColumn("Column" + (i + 1), typeof(string), 0));
                }
                else if (i < strArray.Length)
                {
                    table.Columns.Add(new AdoColumn(strArray[i], typeof(string), 0));
                }
                else
                {
                    table.Columns.Add(new AdoColumn("Column" + (i + 1), typeof(string), 0));
                }
            }
            properties2.DataSource = table;
            using (StreamReader reader2 = new StreamReader(properties2.Filename))
            {
                if (properties2.FirstRowHeaders)
                {
                    str = CSVReadLine(reader2, properties2);
                }
                int index = 0;
                object[] objArray = new object[0x3e8];
                while (((str = CSVReadLine(reader2, properties2)) != null) && (index < 0x3e8))
                {
                    objArray[index++] = ParseLine(str, properties2);
                }
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    int length = 0;
                    ArrayList list = new ArrayList();
                    for (index = 0; index < 0x3e8; index++)
                    {
                        object[] objArray2 = (object[]) objArray[index];
                        if (objArray2 != null)
                        {
                            object obj2 = objArray2[j].ToString();
                            if (((obj2 != null) && (obj2 != DBNull.Value)) && (obj2.ToString() != ""))
                            {
                                string str2 = obj2.ToString();
                                list.Add(str2);
                                if (str2.Length > length)
                                {
                                    length = str2.Length;
                                }
                            }
                        }
                    }
                    AdoColumn column = table.Columns[j];
                    column.DataType = DetermineType((string[]) list.ToArray(typeof(string)));
                    if (column.DataType == typeof(string))
                    {
                        if ((length > 0) && ((length % 50) == 0))
                        {
                            column.Length = length;
                        }
                        else
                        {
                            column.Length = ((length + 50) / 50) * 50;
                        }
                    }
                }
            }
            if ((properties2.DataSource.Columns.Count > 0) && (properties2.DataSource.Columns[0].ColumnName != "Column1"))
            {
                for (int k = 0; k < properties2.DataSource.Columns.Count; k++)
                {
                    if (k < table.Columns.Count)
                    {
                        table.Columns[k].ColumnName = properties2.DataSource.Columns[k].ColumnName;
                    }
                }
            }
            return table;
        }

        public static AdoTable GetSchemaFixed(CsvReadProperties properties)
        {
            string str;
            AdoTable table = new AdoTable();
            int length = 0;
            using (StreamReader reader = new StreamReader(properties.Filename))
            {
                while ((str = ReadLine(reader, properties)) != null)
                {
                    if (str.Length > length)
                    {
                        length = str.Length;
                    }
                }
            }
            int num2 = 0;
            int[] numArray = new int[length];
            using (StreamReader reader2 = new StreamReader(properties.Filename))
            {
                while (((str = ReadLine(reader2, properties)) != null) && (num2++ < 0x3e8))
                {
                    for (int j = 0; (j < length) && (j < str.Length); j++)
                    {
                        if (str.Substring(j, 1) == " ")
                        {
                            numArray[j]++;
                        }
                    }
                }
            }
            int num4 = 1;
            bool flag = true;
            AdoColumn column = new AdoColumn("Column" + num4++, typeof(string), 0);
            for (int i = 0; i < length; i++)
            {
                if (numArray[i] >= ((((double) num2) / 100.0) * 98.0))
                {
                    flag = false;
                }
                else if (!flag)
                {
                    column.Length = i - column.Length;
                    table.Columns.Add(column);
                    column = new AdoColumn("Column" + num4++, typeof(string), i);
                    flag = true;
                }
            }
            column.Length = length - column.Length;
            table.Columns.Add(column);
            if ((properties.DataSource.Columns.Count > 0) && (properties.DataSource.Columns[0].ColumnName != "Column1"))
            {
                for (int k = 0; k < properties.DataSource.Columns.Count; k++)
                {
                    if (k < table.Columns.Count)
                    {
                        table.Columns[k].ColumnName = properties.DataSource.Columns[k].ColumnName;
                    }
                }
            }
            return table;
        }

        public static AdoTable GetVerticalRecordSchema(CsvReadProperties properties)
        {
            AdoTable table = new AdoTable();
            try
            {
                using (StreamReader reader = new StreamReader(properties.Filename, Encoding.GetEncoding(properties.Encoding)))
                {
                    string str;
                    int num = 0;
                    while ((str = ReadNextRecord(reader, properties)) != null)
                    {
                        string name = "Column" + ++num;
                        object[] objArray = ParseLine(str, properties);
                        if (properties.FirstRowHeaders && (objArray.Length > 0))
                        {
                            name = objArray[0].ToString();
                        }
                        table.Columns.Add(new AdoColumn(name));
                    }
                    return table;
                }
            }
            catch (Exception)
            {
            }
            return table;
        }

        public static object[] ParseLine(string line, CsvReadProperties properties)
        {
            int count = properties.DataSource.Columns.Count;
            if (count == 0)
            {
                return new object[0];
            }
            string[] strArray = new string[count];
            if (properties.FileType == FileTypes.Fixed)
            {
                int startIndex = 0;
                int length = 0;
                for (int j = 0; (j < count) && (startIndex < line.Length); j++)
                {
                    length = properties.DataSource.Columns[j].Length;
                    if ((startIndex + length) <= line.Length)
                    {
                        strArray[j] = line.Substring(startIndex, length).Trim();
                    }
                    else if ((line.Length - startIndex) >= 0)
                    {
                        strArray[j] = line.Substring(startIndex, line.Length - startIndex).Trim();
                    }
                    startIndex += length;
                }
            }
            else
            {
                StringBuilder builder = new StringBuilder();
                int index = 0;
                char ch = '\0';
                if (properties.FieldDelimiter != "")
                {
                    ch = properties.FieldDelimiter[0];
                }
                char ch2 = '\0';
                if (properties.TextQualifier != "")
                {
                    ch2 = properties.TextQualifier[0];
                }
                char ch3 = ch;
                bool flag = false;
                foreach (char ch4 in line)
                {
                    if (ch4 == ch)
                    {
                        if (!flag)
                        {
                            if (ch3 != ch)
                            {
                                strArray[index] = builder.ToString();
                            }
                            if (++index >= count)
                            {
                                break;
                            }
                            builder = new StringBuilder();
                        }
                        else
                        {
                            builder.Append(ch4);
                        }
                    }
                    else if (ch4 == ch2)
                    {
                        flag = !flag;
                        if (flag && (ch3 == ch2))
                        {
                            builder.Append(ch4);
                        }
                    }
                    else
                    {
                        builder.Append(ch4);
                    }
                    ch3 = ch4;
                }
                if (((index < count) && !flag) && (ch3 != ch))
                {
                    strArray[index] = builder.ToString();
                }
            }
            object[] objArray = new object[count];
            for (int i = 0; i < count; i++)
            {
                string name = properties.DataSource.Columns[i].DataType.Name;
                if (strArray[i] == null)
                {
                    objArray[i] = DBNull.Value;
                }
                else if (name == "String")
                {
                    objArray[i] = strArray[i];
                }
                else if (strArray[i] == "")
                {
                    objArray[i] = DBNull.Value;
                }
                else
                {
                    try
                    {
                        switch (properties.DataSource.Columns[i].DataType.Name)
                        {
                            case "DateTime":
                                objArray[i] = DateTime.Parse(strArray[i]);
                                goto Label_0510;

                            case "Int64":
                                objArray[i] = long.Parse(strArray[i]);
                                goto Label_0510;

                            case "Int32":
                                objArray[i] = int.Parse(strArray[i]);
                                goto Label_0510;

                            case "Int16":
                                objArray[i] = short.Parse(strArray[i]);
                                goto Label_0510;

                            case "Decimal":
                                objArray[i] = decimal.Parse(strArray[i]);
                                goto Label_0510;

                            case "Double":
                                objArray[i] = double.Parse(strArray[i]);
                                goto Label_0510;

                            case "Single":
                                objArray[i] = float.Parse(strArray[i]);
                                goto Label_0510;

                            case "Boolean":
                                switch (strArray[i].ToLower())
                                {
                                    case "false":
                                        goto Label_0487;

                                    case "yes":
                                        goto Label_0494;

                                    case "no":
                                        goto Label_04A1;

                                    case "-1":
                                        goto Label_04AE;

                                    case "0":
                                        goto Label_04BB;

                                    case "1":
                                        goto Label_04C8;
                                }
                                goto Label_04D5;

                            case "Byte[]":
                                objArray[i] = new ASCIIEncoding().GetBytes(strArray[i]);
                                goto Label_0510;

                            default:
                                goto Label_0510;
                        }
                        //objArray[i] = true;
                        //goto Label_0510;
                    Label_0487:
                        objArray[i] = false;
                        goto Label_0510;
                    Label_0494:
                        objArray[i] = true;
                        goto Label_0510;
                    Label_04A1:
                        objArray[i] = false;
                        goto Label_0510;
                    Label_04AE:
                        objArray[i] = true;
                        goto Label_0510;
                    Label_04BB:
                        objArray[i] = false;
                        goto Label_0510;
                    Label_04C8:
                        objArray[i] = true;
                        goto Label_0510;
                    Label_04D5:
                        objArray[i] = bool.Parse(strArray[i]);
                    }
                    catch
                    {
                        objArray[i] = DBNull.Value;
                    }
                Label_0510:;
                }
            }
            return objArray;
        }

        private static string ReadLine(StreamReader reader, CsvReadProperties properties)
        {
            StringBuilder builder = new StringBuilder();
            bool flag = false;
            while (!reader.EndOfStream && !flag)
            {
                char ch = Convert.ToChar(reader.Read());
                switch (properties.RecordSeparator)
                {
                    case RecordSeparators.Unix:
                    {
                        if (ch != '\n')
                        {
                            break;
                        }
                        flag = true;
                        continue;
                    }
                    case RecordSeparators.Mac:
                    {
                        if (ch != '\r')
                        {
                            goto Label_004E;
                        }
                        flag = true;
                        continue;
                    }
                    default:
                        goto Label_0058;
                }
                builder.Append(ch);
                continue;
            Label_004E:
                builder.Append(ch);
                continue;
            Label_0058:
                if (ch == '\r')
                {
                    if (!reader.EndOfStream)
                    {
                        ch = Convert.ToChar(reader.Read());
                        if (ch == '\n')
                        {
                            flag = true;
                        }
                        else
                        {
                            builder.Append('\r');
                            builder.Append(ch);
                        }
                    }
                }
                else
                {
                    builder.Append(ch);
                }
            }
            if (!flag && (builder.Length == 0))
            {
                return null;
            }
            return builder.ToString();
        }

        public static string ReadNextRecord(StreamReader reader, CsvReadProperties properties)
        {
            string str = null;
            if (properties.FileType == FileTypes.Fixed)
            {
                str = ReadLine(reader, properties);
            }
            else
            {
                str = CSVReadLine(reader, properties);
            }
            if (str != null)
            {
                _recordsRead++;
                _totalRecordLength += str.Length;
            }
            return str;
        }

        public static void ResetAverageRecord()
        {
            _recordsRead = 0;
            _totalRecordLength = 0;
        }

        public static int AverageRecordLength
        {
            get
            {
                try
                {
                    if (_recordsRead == 0)
                    {
                        return 0;
                    }
                    return (_totalRecordLength / _recordsRead);
                }
                catch
                {
                    return 0;
                }
            }
        }

        public static string LastLoadFolder
        {
            get
            {
                return _lastLoadFolder;
            }
            set
            {
                _lastLoadFolder = value;
            }
        }

        public static string LastSaveFolder
        {
            get
            {
                return _lastSaveFolder;
            }
            set
            {
                _lastSaveFolder = value;
            }
        }
    }
}

