namespace Nistec.Printing.View
{
    using System;
    using System.Collections;
    using System.Runtime.InteropServices;

    internal class mtd245
    {
        private mtd288 _var0 = new mtd288();

        internal mtd245()
        {
        }

        internal static string mtd246(ref object var15, ref string var16)
        {
            try
            {
                if ((var16 != null) && (var16.Length > 0))
                {
                    if (var15 is DateTime)
                    {
                        return Convert.ToDateTime(var15).ToString(var16);
                    }
                    if (!(var15 is string) && ((((var15 is int) || (var15 is float)) || ((var15 is double) || (var15 is decimal))) || (((var15 is long) || (var15 is short)) || (var15 is short))))
                    {
                        return Convert.ToDouble(var15).ToString(var16);
                    }
                    return var15.ToString();
                }
                return var15.ToString();
            }
            catch
            {
                return "";
            }
        }

        internal void mtd277(Document doc, mtd161 PSrField, bool IsSubReport)
        {
            SectionCollection sections;
            FieldsCollection fieldss;
            ArrayList list = new ArrayList(5);
            int num = -1;
            mtd264 mtd = null;
            mtd272 mtd2 = null;
            mtd272 mPFRow = null;
            mtd272 mtd4 = null;
            mtd272 CtlInternal = null;
            mtd314[] mDetailFields = null;
            int num2 = 0;
            mtd272 mtd7 = null;
            mtd314 mtd8 = null;
            mtd314 mtd9 = null;
            mtd163 mtd10 = null;
            if (IsSubReport)
            {
                mtd = PSrField.mtd264;
                sections = mtd.mtd265;
                fieldss = mtd.Fields;
            }
            else
            {
                sections = doc._Sections;
                fieldss = doc.Fields;
            }
            mtd270 mtd6 = new mtd270(sections.Count);
            for (int i = 0; i < sections.Count; i++)
            {
                Section s = sections[i];
                s.mtd97();
                int count = s.Controls.Count;
                SectionType type = s.Type;
                if ((type == SectionType.PageHeader) | (type == SectionType.PageFooter))
                {
                    McControlCollection controls = s.Controls;
                    controls.mtd0();
                    mtd7 = new mtd272(ref s, count);
                    if (type == SectionType.PageHeader)
                    {
                        mtd2 = mtd7;
                    }
                    else
                    {
                        mPFRow = mtd7;
                    }
                    int index = 0;
                    for (int j = 0; j < count; j++)
                    {
                        McReportControl c = controls[j];
                        c.SetSize();
                        if (c.Type == ControlType.TextBox)
                        {
                            mtd314.mtd326(ref mtd8, ref c);
                            mtd7.mtd343.SetValue(mtd8, index);
                            index++;
                        }
                        else if (c.Type == ControlType.Label)
                        {
                            mtd314.mtd328(ref mtd8, ref c);
                            mtd7.mtd343.SetValue(mtd8, index);
                            index++;
                        }
                        else if (c.Type == ControlType.Line)
                        {
                            mtd314.mtd330(ref mtd8, ref c);
                            mtd7.mtd343.SetValue(mtd8, index);
                            index++;
                        }
                        else if (c.Type == ControlType.Picture)
                        {
                            mtd314.mtd332(ref mtd8, ref c);
                            mtd7.mtd343.SetValue(mtd8, index);
                            index++;
                        }
                        else if (c.Type == ControlType.CheckBox)
                        {
                            mtd314.mtd334(ref mtd8, ref c);
                            mtd7.mtd343.SetValue(mtd8, index);
                            index++;
                        }
                        else if (c.Type == ControlType.Shape)
                        {
                            mtd314.mtd336(ref mtd8, ref c);
                            mtd7.mtd343.SetValue(mtd8, index);
                            index++;
                        }
                        else if (c.Type == ControlType.RichTextField)
                        {
                            mtd155 mtd11 = new mtd155(ref c);
                            mtd314.mtd339(ref mtd8, ref mtd11);
                            mtd7.mtd343.SetValue(mtd8, index);
                            index++;
                        }
                    }
                    mtd7.mtd214(index);
                }
                else
                {
                    McReportControl[] controlList = null;
                    s.Controls.mtd1(out controlList);
                    mtd7 = new mtd272(ref s, controlList.Length);
                    mtd10 = mtd7.mtd290;
                    int num7 = 0;
                    int num8 = 0;
                    for (int k = 0; k < count; k++)
                    {
                        McField field;
                        McReportControl control2 = controlList[k];
                        control2.SetSize();
                        if (control2.Type == ControlType.TextBox)
                        {
                            mtd314.mtd326(ref mtd8, ref control2);
                            mtd158 mtd12 = mtd8.mtd309;
                            McTextBox box = mtd12.mtd242._McTextBox;
                            if ((box.DataField != null) && (box.DataField.Length > 0))
                            {
                                field = fieldss[box.DataField];
                                if (field != null)
                                {
                                    mtd159 mtd13 = mtd12.mtd242;
                                    mtd13._McField = field;
                                    if (type == SectionType.GroupHeader)
                                    {
                                        list.Add(new mtd266(ref field));
                                        box.Field = field;
                                    }
                                    else if ((type == SectionType.GroupFooter) | (type == SectionType.ReportFooter))
                                    {
                                        if (box.SummaryFunc != AggregateType.None)
                                        {
                                            mtd13._McField.mtd104 = true;
                                            mtd7.mtd304.SetValue(mtd12, num7);
                                            num7++;
                                        }
                                    }
                                    else
                                    {
                                        if (box.SummaryRunning != SummaryRunning.None)
                                        {
                                            mtd7.mtd304.SetValue(mtd12, num7);
                                            num7++;
                                        }
                                        box.Field = field;
                                    }
                                }
                            }
                            mtd7.mtd343.SetValue(mtd8, num8);
                            num8++;
                        }
                        else if (control2.Type == ControlType.Label)
                        {
                            mtd314.mtd328(ref mtd8, ref control2);
                            mtd7.mtd343.SetValue(mtd8, num8);
                            num8++;
                        }
                        else if (control2.Type == ControlType.Line)
                        {
                            mtd314.mtd330(ref mtd8, ref control2);
                            mtd7.mtd343.SetValue(mtd8, num8);
                            num8++;
                        }
                        else if (control2.Type == ControlType.Picture)
                        {
                            mtd314.mtd332(ref mtd8, ref control2);
                            mtd231 mtd14 = mtd8.mtd320.mtd242;
                            McPicture picture = mtd14.mtd232;
                            if (picture.DataField != null)
                            {
                                field = fieldss[picture.DataField];
                                mtd14.mtd204 = field;
                                picture.Field = field;
                            }
                            mtd7.mtd343.SetValue(mtd8, num8);
                            num8++;
                        }
                        else if (control2.Type == ControlType.CheckBox)
                        {
                            mtd314.mtd334(ref mtd8, ref control2);
                            mtd226 mtd15 = mtd8.mtd319.mtd242;
                            McCheckBox box2 = mtd15.mtd227;
                            if (box2.DataField != null)
                            {
                                field = fieldss[box2.DataField];
                                mtd15.mtd204 = field;
                                box2.Field = field;
                            }
                            mtd7.mtd343.SetValue(mtd8, num8);
                            num8++;
                        }
                        else if (control2.Type == ControlType.Shape)
                        {
                            mtd314.mtd336(ref mtd8, ref control2);
                            mtd7.mtd343.SetValue(mtd8, num8);
                            num8++;
                        }
                        else if (control2.Type == ControlType.PageBreak)
                        {
                            if (IsSubReport)
                            {
                                continue;
                            }
                            mtd314.mtd338(ref mtd8, ref control2);
                            mtd7.mtd343.SetValue(mtd8, num8);
                            if (mtd10._SectionType == SectionType.ReportDetail)
                            {
                                doc.mtd370 = true;
                            }
                            else if ((mtd10._SectionType == SectionType.GroupHeader) || (mtd10._SectionType == SectionType.PageFooter))
                            {
                                doc.mtd371 = true;
                            }
                            num8++;
                        }
                        else if (control2.Type == ControlType.SubReport)
                        {
                            mtd161 mtd16 = new mtd161(ref control2);
                            mtd314.mtd341(ref mtd8, ref mtd16);
                            mtd7.mtd343.SetValue(mtd8, num8);
                            num8++;
                        }
                        else if (control2.Type == ControlType.RichTextField)
                        {
                            mtd155 mtd17 = new mtd155(ref control2, ref fieldss);
                            mtd314.mtd339(ref mtd8, ref mtd17);
                            mtd7.mtd343.SetValue(mtd8, num8);
                            num8++;
                        }
                        if (k == 0)
                        {
                            mtd10.mtd242.mtd359.Add(mtd8.mtd199._Location.Top + mtd8.mtd199._Location.Height);
                            mtd9 = mtd8;
                        }
                        else
                        {
                            mtd368(ref mtd9, ref mtd8, ref mtd10.mtd242.mtd359);
                            mtd9 = mtd8;
                        }
                    }
                    mtd7.mtd344(num7);
                    if (num8 != controlList.Length)
                    {
                        mtd7.mtd214(num8);
                    }
                    switch (type)
                    {
                        case SectionType.GroupHeader:
                            mtd6.mtd2(mtd7);
                            mtd10.mtd242.mtd360(list, num);
                            num = list.Count - 1;
                            break;

                        case SectionType.GroupFooter:
                        {
                            mtd6.mtd2(mtd7);
                            mtd163 mtd18 = mtd6[((num2 - mtd6.mtd166) + 1) + num2].mtd290;
                            mtd10.mtd242.mtd302 = mtd18.mtd242;
                            break;
                        }
                        case SectionType.ReportHeader:
                            mtd4 = mtd7;
                            break;

                        case SectionType.ReportFooter:
                            CtlInternal = mtd7;
                            break;

                        case SectionType.ReportDetail:
                            mDetailFields = mtd7.mtd343;
                            mtd6.mtd2(mtd7);
                            num2 = mtd6.mtd166 - 1;
                            break;

                        default:
                            mtd6.mtd2(mtd7);
                            break;
                    }
                }
            }
            if (((mDetailFields != null) && (mPFRow != null)) && (mPFRow.mtd343.Length > 0))
            {
                this.mtd372(ref mPFRow, ref mDetailFields, fieldss);
            }
            if (!IsSubReport)
            {
                doc.mtd275 = mtd2;
                doc.mtd276 = mPFRow;
                doc.mtd273 = mtd4;
                doc.mtd274 = CtlInternal;
                doc.mtd271 = mtd6;
                doc.mtd267 = this.var22(list);
            }
            else
            {
                mtd.mtd275 = mtd2;
                mtd.mtd276 = mPFRow;
                mtd.mtd273 = mtd4;
                mtd.mtd274 = CtlInternal;
                mtd.mtd271 = mtd6;
                mtd.mtd267 = this.var22(list);
            }
        }

        internal mtd163 mtd292(int var1, ref mtd272 var2, ref mtd209 var3, float var4, bool var5)
        {
            float num = 0f;
            float num2 = 0f;
            float num3 = 0f;
            int num4 = 0;
            int num5 = 0;
            mtd163 mtd = var2.mtd290;
            mtd314[] mtdArray = var2.mtd343;
            mtd163 mtd2 = mtd.mtd105(var1, var4);
            mtd164 mtd3 = mtd2.mtd167;
            if (mtdArray.Length > 0)
            {
                num4 = mtdArray[0].mtd316;
            }
            foreach (mtd314 mtd4 in mtdArray)
            {
                switch (mtd4._ControlType)
                {
                    case ControlType.Label:
                    {
                        mtd148 mtd6 = mtd148.mtd105(ref mtd4.mtd317, mtd4.mtd325);
                        num5 = mtd4.mtd316;
                        num2 = mtd6.mtd129 + mtd6.Height;
                        num3 = mtd367(num4, num5, num, ref mtd.mtd242.mtd359, num3);
                        if (num3 != 0f)
                        {
                            McLocation.mtd257(ref mtd6._Location, mtd6._Location.Top + num3);
                            num2 += num3;
                        }
                        mtd3.mtd211(mtd6);
                        break;
                    }
                    case ControlType.Line:
                    {
                        mtd149 mtd7 = mtd149.mtd105(ref mtd4.mtd318, mtd4.mtd325);
                        num5 = mtd4.mtd316;
                        num2 = mtd7.mtd129 + mtd7.Height;
                        num3 = mtd367(num4, num5, num, ref mtd.mtd242.mtd359, num3);
                        if (num3 != 0f)
                        {
                            LinePosition.mtd257(ref mtd7.mtd151, num3);
                            num2 += num3;
                        }
                        mtd3.mtd211(mtd7);
                        break;
                    }
                    case ControlType.Picture:
                    {
                        mtd152 mtd8 = mtd152.mtd105(ref mtd4.mtd320, mtd4.mtd325);
                        num5 = mtd4.mtd316;
                        num2 = mtd8.mtd129 + mtd8.Height;
                        num3 = mtd367(num4, num5, num, ref mtd.mtd242.mtd359, num3);
                        if (num3 != 0f)
                        {
                            McLocation.mtd257(ref mtd8._Location, mtd8._Location.Top + num3);
                            num2 += num3;
                        }
                        mtd3.mtd211(mtd8);
                        break;
                    }
                    case ControlType.CheckBox:
                    {
                        mtd136 mtd9 = mtd136.mtd105(ref mtd4.mtd319, mtd4.mtd325);
                        num5 = mtd4.mtd316;
                        num2 = mtd9.mtd129 + mtd9.Height;
                        num3 = mtd367(num4, num5, num, ref mtd.mtd242.mtd359, num3);
                        if (num3 != 0f)
                        {
                            McLocation.mtd257(ref mtd9._Location, mtd9._Location.Top + num3);
                            num2 += num3;
                        }
                        mtd3.mtd211(mtd9);
                        break;
                    }
                    case ControlType.Shape:
                    {
                        mtd156 mtd10 = mtd156.mtd105(ref mtd4.mtd321, mtd4.mtd325);
                        num5 = mtd4.mtd316;
                        num2 = mtd10.mtd129 + mtd10.Height;
                        num3 = mtd367(num4, num5, num, ref mtd.mtd242.mtd359, num3);
                        if (num3 != 0f)
                        {
                            McLocation.mtd257(ref mtd10._Location, mtd10._Location.Top + num3);
                            num2 += num3;
                        }
                        mtd3.mtd211(mtd10);
                        break;
                    }
                    case ControlType.RichTextField:
                    {
                        mtd155 mtd11 = mtd155.mtd105(ref mtd4.mtd323, mtd4.mtd325);
                        num5 = mtd4.mtd316;
                        num2 = mtd11.mtd129 + mtd11.Height;
                        num3 = mtd367(num4, num5, num, ref mtd.mtd242.mtd359, num3);
                        if (num3 != 0f)
                        {
                            McLocation.mtd257(ref mtd11._Location, mtd11._Location.Top + num3);
                            num2 += num3;
                        }
                        mtd3.mtd211(mtd11);
                        break;
                    }
                    case ControlType.SubReport:
                    {
                        mtd161 mtd12 = mtd161.mtd105(ref mtd4.mtd324, mtd4.mtd325);
                        num5 = mtd4.mtd316;
                        this._var0.mtd22(ref mtd12);
                        num2 = mtd12.mtd129 + mtd12.Height;
                        num3 = mtd367(num4, num5, num, ref mtd.mtd242.mtd359, num3);
                        if (num3 != 0f)
                        {
                            McLocation.mtd257(ref mtd12._Location, mtd12._Location.Top + num3);
                            num2 += num3;
                        }
                        mtd3.mtd211(mtd12);
                        break;
                    }
                    case ControlType.PageBreak:
                    {
                        mtd126 mtd13 = mtd4.mtd322;
                        if (((McPageBreak)mtd13.RptControl).Enabled)
                        {
                            num5 = mtd4.mtd316;
                            num3 = mtd367(num4, num5, num, ref mtd.mtd242.mtd359, num3);
                            var3.mtd2(mtd13._Location.Top + num3);
                        }
                        num2 = (mtd13.mtd129 + mtd13.Height) + num3;
                        break;
                    }
                    case ControlType.TextBox:
                    {
                        mtd158 mtd5 = mtd158.mtd105(ref mtd4.mtd309, mtd4.mtd325);
                        num5 = mtd4.mtd316;
                        if (mtd5.mtd137 != null)
                        {
                            num2 = mtd159.mtd241(ref mtd5, ref mtd4.mtd309._Location);
                        }
                        else
                        {
                            num2 = mtd5.mtd129 + mtd5.Height;
                        }
                        num3 = mtd367(num4, num5, num, ref mtd.mtd242.mtd359, num3);
                        if (num3 != 0f)
                        {
                            McLocation.mtd257(ref mtd5._Location, mtd5._Location.Top + num3);
                            num2 += num3;
                        }
                        mtd3.mtd211(mtd5);
                        break;
                    }
                }
                num4 = num5;
                if (num < num2)
                {
                    num = num2;
                }
            }
            if (mtd.mtd242.mtd359.Count > 0)
            {
                num3 = mtd.Height - ((float)mtd.mtd242.mtd359[mtd.mtd242.mtd359.Count - 1]);
            }
            else
            {
                num3 = 0f;
            }
            if (mtd.mtd242._Section.CanGrow && ((num + num3) > mtd.Height))
            {
                mtd2.Height = num + num3;
                return mtd2;
            }
            if (mtd.mtd242._Section.CanShrink && ((num + num3) < mtd.Height))
            {
                mtd2.Height = num + num3;
            }
            return mtd2;
        }

        internal static bool mtd294(ref mtd289 var14)
        {
            bool flag = false;
            mtd266[] mtdArray = var14.mtd358;
            if (mtdArray != null)
            {
                for (int i = 0; i < mtdArray.Length; i++)
                {
                    if (mtdArray[i].mtd285)
                    {
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return flag;
        }

        internal static bool mtd299(ref mtd266[] var13)
        {
            bool flag = false;
            if (var13 != null)
            {
                for (int i = 0; i < var13.Length; i++)
                {
                    if (var13[i].mtd285)
                    {
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return flag;
        }

        internal static void mtd300(ref mtd272 mPageRow)
        {
            foreach (mtd158 mtd in mPageRow.mtd304)
            {
                SummaryRunning summaryRunning = mtd.mtd242._McTextBox.SummaryRunning;
                McField field = mtd.mtd242._McField;
                if (summaryRunning != SummaryRunning.None)
                {
                    object obj2;
                    if ((summaryRunning == SummaryRunning.Group) && mPageRow.mtd298)
                    {
                        obj2 = field.Value;
                    }
                    else
                    {
                        object obj3 = mtd.mtd242.mtd137;
                        object obj4 = field.Value;
                        if (obj3 == null)
                        {
                            obj2 = obj4;
                        }
                        else if ((field.Typecode == TypeCode.String) || (field.Typecode == TypeCode.DateTime))
                        {
                            obj2 = obj4;
                        }
                        else
                        {
                            try
                            {
                                if (field.Typecode == TypeCode.Int32)
                                {
                                    obj2 = ((int) obj3) + ((int) obj4);
                                }
                                else if (field.Typecode == TypeCode.Single)
                                {
                                    obj2 = ((float) obj3) + ((float) obj4);
                                }
                                else if (field.Typecode == TypeCode.Decimal)
                                {
                                    obj2 = ((decimal) obj3) + ((decimal) obj4);
                                }
                                else if (field.Typecode == TypeCode.Double)
                                {
                                    obj2 = ((double) obj3) + ((double) obj4);
                                }
                                else
                                {
                                    obj2 = obj4;
                                }
                            }
                            catch
                            {
                                obj2 = obj4;
                            }
                        }
                    }
                    field.Value = obj2;
                }
            }
        }

        internal void mtd303(ref mtd158[] FxTextFields, int index, int endIndex)
        {
            foreach (mtd158 mtd2 in FxTextFields)
            {
                if (mtd2.ControlType == ControlType.TextBox)
                {
                    mtd159 mtd = mtd2.mtd242;
                    McTextBox box = mtd._McTextBox;
                    box.Value = mtd._McField.mtd110(index, endIndex, box.SummaryFunc);
                }
            }
        }

        internal void mtd311(ref mtd163 var18, out mtd163 var19, float var20, float var21)
        {
            float num = 0f;
            mtd164 mtd = var18.mtd167;
            var19 = var18.mtd105(var20, true);
            mtd164 mtd2 = new mtd164(mtd.mtd210);
            mtd164 mtd3 = var19.mtd167;
            foreach (mtd126 mtd4 in mtd)
            {
                num = var18.mtd29 + mtd4.mtd129;
                if ((num + mtd4.Height) > var21)
                {
                    if (num >= var21)
                    {
                        mtd4.mtd129 = num - var21;
                        mtd3.mtd2(mtd4);
                    }
                    else if (num < var21)
                    {
                        if (mtd4.ControlType == ControlType.TextBox)
                        {
                            mtd158 mtd5 = (mtd158) mtd4;
                            mtd5 = mtd158.mtd253(ref mtd5);
                            mtd5.mtd129 = -(var21 - num);
                            mtd2.mtd2(mtd4);
                            mtd3.mtd2(mtd5);
                        }
                        else if (mtd4.ControlType == ControlType.Label)
                        {
                            mtd148 mtd6 = (mtd148) mtd4;
                            mtd6 = mtd148.mtd253(ref mtd6);
                            mtd6.mtd129 = -(var21 - num);
                            mtd2.mtd2(mtd4);
                            mtd3.mtd2(mtd6);
                        }
                        else if (mtd4.ControlType == ControlType.Line)
                        {
                            mtd149 mtd7 = (mtd149) mtd4;
                            mtd7 = mtd149.mtd253(ref mtd7);
                            mtd7.mtd129 = -(var21 - num);
                            mtd2.mtd2(mtd4);
                            mtd3.mtd2(mtd7);
                        }
                        else if (mtd4.ControlType == ControlType.Picture)
                        {
                            mtd152 mtd8 = (mtd152) mtd4;
                            mtd8 = mtd152.mtd253(ref mtd8);
                            mtd8.mtd129 = -(var21 - num);
                            mtd2.mtd2(mtd4);
                            mtd3.mtd2(mtd8);
                        }
                        else if (mtd4.ControlType == ControlType.CheckBox)
                        {
                            mtd136 mtd9 = (mtd136) mtd4;
                            mtd9 = mtd136.mtd253(ref mtd9);
                            mtd9.mtd129 = -(var21 - num);
                            mtd2.mtd2(mtd4);
                            mtd3.mtd2(mtd9);
                        }
                        else if (mtd4.ControlType == ControlType.Shape)
                        {
                            mtd156 mtd10 = (mtd156) mtd4;
                            mtd10 = mtd156.mtd253(ref mtd10);
                            mtd10.mtd129 = -(var21 - num);
                            mtd2.mtd2(mtd4);
                            mtd3.mtd2(mtd10);
                        }
                        else if (mtd4.ControlType == ControlType.SubReport)
                        {
                            mtd161 mtd11 = (mtd161) mtd4;
                            this._var0.mtd310(ref mtd3, ref mtd11, var21 - num, this);
                            mtd2.mtd2(mtd4);
                        }
                        else if (mtd4.ControlType == ControlType.RichTextField)
                        {
                            mtd155 mtd12 = (mtd155) mtd4;
                            mtd12 = mtd155.mtd253(ref mtd12);
                            mtd12.mtd129 = -(var21 - num);
                            mtd2.mtd2(mtd4);
                            mtd3.mtd2(mtd12);
                        }
                    }
                    continue;
                }
                mtd2.mtd2(mtd4);
            }
            var18.mtd167 = mtd2;
            var18.mtd286 = true;
            var18.Height = var21 - var18.mtd29;
            var19.Height -= var18.Height;
            var19.mtd29 = var20;
        }

        internal void mtd313(ref mtd163 oPRow, ref mtd163 nPRow)
        {
            mtd164 mtd = oPRow.mtd167;
            foreach (mtd126 mtd3 in nPRow.mtd167)
            {
                if (mtd3.ControlType == ControlType.SubReport)
                {
                    mtd161 mtd4 = (mtd161) mtd3;
                    if (mtd4.mtd286)
                    {
                        this._var0.mtd312(ref mtd4, this);
                    }
                    else
                    {
                        mtd3.mtd129 += oPRow.Height;
                        mtd.mtd2(mtd3);
                    }
                    continue;
                }
                if (!mtd3.mtd254)
                {
                    mtd3.mtd129 += oPRow.Height;
                    mtd.mtd2(mtd3);
                }
            }
            oPRow.Height += nPRow.Height;
            oPRow.mtd286 = false;
            mtd.mtd213();
        }

        internal static float mtd367(int var9, int var10, float var11, ref ArrayList var8, float var12)
        {
            if (var9 != var10)
            {
                float num = (float) var8[var9];
                if (num != var11)
                {
                    var12 = var11 - num;
                }
            }
            return var12;
        }

        internal static void mtd368(ref mtd314 var6, ref mtd314 var7, ref ArrayList var8)
        {
            int num = var8.Count - 1;
            float num2 = (float) var8[num];
            McLocation mtd = var6.mtd199._Location;
            McLocation mtd2 = var7.mtd199._Location;
            if ((mtd2.Top == mtd.Top) | (num2 >= mtd2.Top))
            {
                var7.mtd316 = var6.mtd316;
                if (num2 < (mtd2.Top + mtd2.Height))
                {
                    var8[num] = mtd2.Top + mtd2.Height;
                }
            }
            else
            {
                var7.mtd316 = var6.mtd316 + 1;
                var8.Add(mtd2.Top + mtd2.Height);
            }
        }

        internal static bool mtd369(string var17)
        {
            foreach (char ch in var17)
            {
                if (!char.IsNumber(ch))
                {
                    return false;
                }
            }
            return true;
        }

        internal void mtd372(ref mtd272 mPFRow, ref mtd314[] mDetailFields, FieldsCollection fields)
        {
            int index = 0;
            mtd314[] mtdArray = mPFRow.mtd343;
            mtd307[] destinationArray = new mtd307[mtdArray.Length];
            foreach (mtd314 mtd2 in mtdArray)
            {
                if (mtd2._ControlType == ControlType.TextBox)
                {
                    mtd159 mtd = mtd2.mtd309.mtd242;
                    McTextBox box = mtd._McTextBox;
                    string dataField = box.DataField;
                    if (((box.SummaryFunc != AggregateType.None) && (dataField != null)) && (dataField.Length > 0))
                    {
                        McField field = fields[dataField];
                        if (field != null)
                        {
                            foreach (mtd314 mtd3 in mDetailFields)
                            {
                                if ((mtd3._ControlType == ControlType.TextBox) && (field == mtd3.mtd309.mtd242._McField))
                                {
                                    field.mtd104 = true;
                                    mtd._McField = field;
                                    mtd307 mtd4 = new mtd307();
                                    mtd4.mtd309 = mtd2.mtd309;
                                    mtd4.mtd345 = mtd3.mtd309.RptControl;
                                    destinationArray.SetValue(mtd4, index);
                                    index++;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            if ((index > 0) && (index < mtdArray.Length))
            {
                mtd307[] sourceArray = destinationArray;
                destinationArray = new mtd307[index];
                Array.Copy(sourceArray, destinationArray, index);
                mPFRow.mtd308 = destinationArray;
            }
        }

        private mtd266[] var22(ArrayList var13)
        {
            if ((var13 == null) || (var13.Count <= 0))
            {
                return null;
            }
            mtd266[] mtdArray = new mtd266[var13.Count];
            for (int i = 0; i < var13.Count; i++)
            {
                mtdArray[i] = (mtd266) var13[i];
            }
            return mtdArray;
        }
    }
}

