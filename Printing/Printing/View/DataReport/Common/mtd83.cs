namespace Nistec.Printing.View
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Data.OleDb;
    using System.IO;
    using System.Text;
    using System.Xml;

    internal class StreamUtil //mtd83
    {
        internal static void mtd84(ref Stream var0, Report var1)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                XmlTextWriter writer = new XmlTextWriter(stream, Encoding.UTF8);
                try
                {
                    if (var2(ref writer, ref var1))
                    {
                        stream.Seek(0L, SeekOrigin.Begin);
                        stream.WriteTo(var0);
                    }
                }
                catch (Exception exception)
                {
                    new Exception(exception.Message);
                }
                finally
                {
                    if (writer != null)
                    {
                        writer.Close();
                    }
                }
            }
        }

        internal static void mtd84(string var3, Report var1)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                XmlTextWriter writer = new XmlTextWriter(stream, Encoding.UTF8);
                using (StreamWriter writer2 = new StreamWriter(var3, false, Encoding.UTF8))
                {
                    try
                    {
                        if (var2(ref writer, ref var1))
                        {
                            stream.Seek(0L, SeekOrigin.Begin);
                            writer2.Write(reader.ReadToEnd());
                            writer2.Flush();
                        }
                    }
                    catch (Exception exception)
                    {
                        new Exception(exception.Message);
                    }
                    finally
                    {
                        if (writer != null)
                        {
                            writer.Close();
                        }
                        if (reader != null)
                        {
                            reader.Close();
                        }
                    }
                }
            }
        }

        internal static void mtd87(Stream var0, Report var1)
        {
            mtd87(var0, var1, null);
        }

        internal static void mtd87(string var3, Report var1)
        {
            mtd87(var3, var1, null);
        }

        internal static void mtd87(Stream var0, Report var1, IDesignerHost var13)
        {
            using (StreamReader reader = new StreamReader(var0, Encoding.UTF8))
            {
                XmlDocument document = new XmlDocument();
                try
                {
                    document.Load(reader);
                    var14(document, var1, var13);
                }
                catch (Exception exception)
                {
                    new Exception(exception.Message);
                }
            }
        }

        internal static void mtd87(string var3, Report var1, IDesignerHost var13)
        {
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(var3);
                var14(document, var1, var13);
            }
            catch (Exception exception)
            {
                new Exception(exception.Message);
            }
        }

        private static void var11(ref XmlTextWriter var4, ref Margins var12)
        {
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(var12))
            {
                if (descriptor.ShouldSerializeValue(var12))
                {
                    var4.WriteAttributeString(descriptor.Name, descriptor.Converter.ConvertToString(descriptor.GetValue(var12)));
                }
            }
        }

        private static void var14(XmlDocument var15, Report var1, IDesignerHost var13)
        {
            SectionCollection sections = var1.Sections;
            var1.ReportWidth = Convert.ToInt32(var15.DocumentElement.Attributes.GetNamedItem("ReportWidth").Value);
            foreach (XmlElement element in var15.DocumentElement.ChildNodes)
            {
                if (element.Name == "Section")
                {
                    Section section = (Section) var16(element.Attributes[0].Value, element.Attributes[1].Value, var13);
                    var17(element, section, var13);
                    sections.Add(section);
                    continue;
                }
                if (element.Name == "DataSource")
                {
                    var18(var1, element);
                    continue;
                }
                if (element.Name == "PageSettings")
                {
                    var19(element, ref var1);
                    continue;
                }
                if (element.Name == "Scripting")
                {
                    var20(element, var1);
                }
            }
        }

        private static object var16(string var28, string var29, IDesignerHost var13)
        {
            if (var28 == "ReportHeader")
            {
                if (var13 != null)
                {
                    return var13.CreateComponent(typeof(ReportHeader), var29);
                }
                return new ReportHeader(var29);
            }
            if (var28 == "PageHeader")
            {
                if (var13 != null)
                {
                    return var13.CreateComponent(typeof(PageHeader), var29);
                }
                return new PageHeader(var29);
            }
            if (var28 == "GroupHeader")
            {
                if (var13 != null)
                {
                    return var13.CreateComponent(typeof(GroupHeader), var29);
                }
                return new GroupHeader(var29);
            }
            if (var28 == "ReportDetail")
            {
                if (var13 != null)
                {
                    return var13.CreateComponent(typeof(ReportDetail), var29);
                }
                return new ReportDetail(var29);
            }
            if (var28 == "GroupFooter")
            {
                if (var13 != null)
                {
                    return var13.CreateComponent(typeof(GroupFooter), var29);
                }
                return new GroupFooter(var29);
            }
            if (var28 == "PageFooter")
            {
                if (var13 != null)
                {
                    return var13.CreateComponent(typeof(PageFooter), var29);
                }
                return new PageFooter(var29);
            }
            if (!(var28 == "ReportFooter"))
            {
                return null;
            }
            if (var13 != null)
            {
                return var13.CreateComponent(typeof(ReportFooter), var29);
            }
            return new ReportFooter(var29);
        }

        private static void var17(XmlElement var21, Section var7, IDesignerHost var13)
        {
            try
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(var7);
                for (int i = 2; i < var21.Attributes.Count; i++)
                {
                    XmlAttribute attribute = var21.Attributes[i];
                    if (attribute.Value != null)
                    {
                        PropertyDescriptor descriptor = properties[attribute.Name];
                        descriptor.SetValue(var7, descriptor.Converter.ConvertFromString(attribute.Value));
                    }
                }
                foreach (XmlNode node in var21.ChildNodes)
                {
                    XmlAttribute attribute2 = node.Attributes["Type"];
                    XmlAttribute attribute3 = node.Attributes["Name"];
                    if ((attribute2 != null) && (attribute3 != null))
                    {
                        McReportControl component = (McReportControl) var22(attribute2.Value, attribute3.Value, var13);
                        properties = TypeDescriptor.GetProperties(component);
                        var23(node, properties, component, var13);
                        component.Parent = var7;
                        var7.Controls.Add(component);
                    }
                }
            }
            catch (Exception exception)
            {
                new Exception(exception.Message);
            }
        }

        private static void var18(Report var1, XmlElement var21)
        {
            try
            {
                XmlAttribute attribute = var21.Attributes["CommandText"];
                XmlAttribute attribute2 = var21.Attributes["ConnectionString"];
                if ((attribute != null) && (attribute2 != null))
                {
                    string selectCommandText = Encoding.Unicode.GetString(Convert.FromBase64String(attribute.Value));
                    string selectConnectionString = Encoding.Unicode.GetString(Convert.FromBase64String(attribute2.Value));
                    var1.DataAdapter = new OleDbDataAdapter(selectCommandText, selectConnectionString);
                }
            }
            catch (Exception exception)
            {
                new Exception(exception.Message);
            }
        }

        private static void var19(XmlElement var21, ref Report var1)
        {
            try
            {
                PageSettings pageSetting = var1.PageSetting;
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(pageSetting);
                foreach (XmlAttribute attribute in var21.Attributes)
                {
                    if (attribute.Value == null)
                    {
                        continue;
                    }
                    if (attribute.Name.StartsWith("Margin"))
                    {
                        var24(pageSetting.Margins, attribute);
                        continue;
                    }
                    PropertyDescriptor descriptor = properties[attribute.Name];
                    if (descriptor != null)
                    {
                        descriptor.SetValue(pageSetting, descriptor.Converter.ConvertFromString(attribute.Value));
                    }
                }
            }
            catch (Exception exception)
            {
                new Exception(exception.Message);
            }
        }

        private static bool var2(ref XmlTextWriter var4, ref Report var1)
        {
            SectionCollection sections = var1.Sections;
            try
            {
                var4.Formatting = Formatting.Indented;
                var4.Indentation = 2;
                var4.WriteStartDocument();
                var4.WriteComment("Nistec ReportView");
                var4.WriteStartElement("ReportView", "www.Nistec.com");
                var4.WriteAttributeString("Version", var1.Version);
                var4.WriteAttributeString("ReportWidth", var1.ReportWidth.ToString());
                for (int i = 0; i < sections.Count; i++)
                {
                    Section section = sections[i];
                    var5(ref var4, ref section);
                }
                PageSettings pageSetting = var1.PageSetting;
                var6(ref var4, ref pageSetting);
                var4.WriteStartElement("DataSource");
                if ((var1.DataAdapter != null) && (var1.DataAdapter is OleDbDataAdapter))
                {
                    OleDbDataAdapter dataAdapter = (OleDbDataAdapter) var1.DataAdapter;
                    if (dataAdapter.SelectCommand != null)
                    {
                        if ((dataAdapter.SelectCommand.CommandText != null) && (dataAdapter.SelectCommand.CommandText.Length > 0))
                        {
                            var4.WriteAttributeString("CommandText", Convert.ToBase64String(Encoding.Unicode.GetBytes(dataAdapter.SelectCommand.CommandText)));
                        }
                        if (((dataAdapter.SelectCommand.Connection != null) && (dataAdapter.SelectCommand.Connection.ConnectionString != null)) && (dataAdapter.SelectCommand.Connection.ConnectionString.Length > 0))
                        {
                            var4.WriteAttributeString("ConnectionString", Convert.ToBase64String(Encoding.Unicode.GetBytes(dataAdapter.SelectCommand.Connection.ConnectionString)));
                        }
                    }
                }
                var4.WriteEndElement();
                var4.WriteStartElement("Scripting");
                var4.WriteAttributeString("Lang", var1.ScriptLanguage.ToString());
                if ((var1.Script != null) && (var1.Script.Length > 0))
                {
                    var4.WriteAttributeString("Script", Convert.ToBase64String(Encoding.Unicode.GetBytes(var1.Script)));
                }
                var4.WriteEndElement();
                var4.WriteEndDocument();
                var4.Flush();
                return true;
            }
            catch (Exception exception)
            {
                new Exception(exception.Message);
                return false;
            }
        }

        private static void var20(XmlElement var21, Report var1)
        {
            foreach (XmlAttribute attribute in var21.Attributes)
            {
                if (attribute.Name == "Lang")
                {
                    if (attribute.Value == "VB")
                    {
                        var1.ScriptLanguage = ScriptLanguage.VB;
                    }
                    else if (attribute.Value == "CSharp")
                    {
                        var1.ScriptLanguage = ScriptLanguage.CSharp;
                    }
                    continue;
                }
                if (attribute.Name == "Script")
                {
                    var1.Script = Encoding.Unicode.GetString(Convert.FromBase64String(attribute.Value));
                }
            }
        }

        private static object var22(string var28, string var29, IDesignerHost var13)
        {
            if (var28 == "TextBox")
            {
                if (var13 != null)
                {
                    return var13.CreateComponent(typeof(McTextBox), var29);
                }
                return new McTextBox(var29);
            }
            if (var28 == "Label")
            {
                if (var13 != null)
                {
                    return var13.CreateComponent(typeof(McLabel), var29);
                }
                return new McLabel(var29);
            }
            if (var28 == "Line")
            {
                if (var13 != null)
                {
                    return var13.CreateComponent(typeof(McLine), var29);
                }
                return new McLine(var29);
            }
            if (var28 == "Picture")
            {
                if (var13 != null)
                {
                    return var13.CreateComponent(typeof(McPicture), var29);
                }
                return new McPicture(var29);
            }
            if (var28 == "PageBreak")
            {
                if (var13 != null)
                {
                    return var13.CreateComponent(typeof(McPageBreak), var29);
                }
                return new McPageBreak(var29);
            }
            if (var28 == "Shape")
            {
                if (var13 != null)
                {
                    return var13.CreateComponent(typeof(McShape), var29);
                }
                return new McShape(var29);
            }
            if (var28 == "CheckBox")
            {
                if (var13 != null)
                {
                    return var13.CreateComponent(typeof(McCheckBox), var29);
                }
                return new McCheckBox(var29);
            }
            if (var28 == "SubReport")
            {
                if (var13 != null)
                {
                    return var13.CreateComponent(typeof(McSubReport), var29);
                }
                return new McSubReport(var29);
            }
            if (!(var28 == "RichTextField"))
            {
                return null;
            }
            if (var13 != null)
            {
                return var13.CreateComponent(typeof(McRichText), var29);
            }
            return new McRichText(var29);
        }

        private static void var23(XmlNode var25, PropertyDescriptorCollection var26, McReportControl var9, IDesignerHost var13)
        {
            for (int i = 2; i < var25.Attributes.Count; i++)
            {
                XmlAttribute attribute = var25.Attributes[i];
                if (attribute.Value != null)
                {
                    PropertyDescriptor descriptor = var26[attribute.Name];
                    if (descriptor != null)
                    {
                        descriptor.SetValue(var9, descriptor.Converter.ConvertFromString(attribute.Value));
                    }
                }
            }
        }

        private static void var24(Margins var27, XmlNode var25)
        {
            PropertyDescriptor descriptor = TypeDescriptor.GetProperties(var27)[var25.Name];
            if (descriptor != null)
            {
                descriptor.SetValue(var27, descriptor.Converter.ConvertFromString(var25.Value));
            }
        }

        private static void var5(ref XmlTextWriter var4, ref Section var7)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(var7);
            var4.WriteStartElement("Section");
            var4.WriteAttributeString("Type", var7.Type.ToString());
            var4.WriteAttributeString("Name", var7.Name);
            foreach (PropertyDescriptor descriptor in properties)
            {
                if (descriptor.Attributes.Contains(mtd85.mtd86) && descriptor.ShouldSerializeValue(var7))
                {
                    var4.WriteAttributeString(descriptor.Name, descriptor.Converter.ConvertToString(descriptor.GetValue(var7)));
                }
            }
            for (int i = 0; i < var7.Controls.Count; i++)
            {
                McReportControl control = var7.Controls[i];
                var8(ref var4, ref control);
            }
            var4.WriteEndElement();
        }

        private static void var6(ref XmlTextWriter var4, ref PageSettings var10)
        {
            if (var10 != null)
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(var10);
                var4.WriteStartElement("PageSettings");
                foreach (PropertyDescriptor descriptor in properties)
                {
                    if (!descriptor.ShouldSerializeValue(var10))
                    {
                        continue;
                    }
                    if (descriptor.Name == "Margins")
                    {
                        Margins margins = (Margins) descriptor.GetValue(var10);
                        var11(ref var4, ref margins);
                        continue;
                    }
                    var4.WriteAttributeString(descriptor.Name, descriptor.Converter.ConvertToString(descriptor.GetValue(var10)));
                }
                var4.WriteEndElement();
            }
        }

        private static void var8(ref XmlTextWriter var4, ref McReportControl var9)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(var9);
            var4.WriteStartElement("Control");
            var4.WriteAttributeString("Type", var9.Type.ToString());
            var4.WriteAttributeString("Name", var9.Name);
            foreach (PropertyDescriptor descriptor in properties)
            {
                if (descriptor.Attributes.Contains(mtd85.mtd86) && descriptor.ShouldSerializeValue(var9))
                {
                    var4.WriteAttributeString(descriptor.Name, descriptor.Converter.ConvertToString(descriptor.GetValue(var9)));
                }
            }
            var4.WriteEndElement();
        }
    }
}

