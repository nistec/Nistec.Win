namespace Nistec.Printing.View
{
    using System;

    internal class mtd288
    {
        internal mtd288()
        {
        }

        internal void mtd22(ref mtd161 var0)
        {
            mtd163 mtd3;
            mtd289 mtd4;
            mtd245 mtd = new mtd245();
            int endIndex = 0;
            bool flag = false;
            float num2 = 0f;
            int num3 = 0;
            int num4 = 0;
            float num5 = 0f;
            mtd264 mtd5 = var0.mtd264;
            mtd5.mtd280();
            if (!mtd5.mtd279)
            {
                goto Label_0766;
            }
            if (mtd5.mtd277)
            {
                mtd.mtd277(null, var0, true);
            }
            mtd266[] mtdArray = mtd5.mtd267;
            mtd272 mtd2 = mtd5.mtd275;
            if (mtd2 != null)
            {
                mtd4 = mtd2.mtd290.mtd242;
                if (!mtd4.pageHeader.mtd117(Msg.Initialize) && mtd5._CodeProvider.mtd178)
                {
                    object[] objArray = new object[] { mtd4.pageHeader, EventArgs.Empty };
                    mtd5._CodeProvider.mtd71(mtd4.pageHeader.Name, Methods._Initialize, objArray);
                }
                mtd209 mtd6 = null;
                mtd3 = mtd.mtd292(0, ref mtd2, ref mtd6, num5, false);
                var0.mtd162.mtd2(mtd3);
                num2 += mtd3.Height;
                num5 += mtd3.Height;
                num4++;
            }
            mtd2 = mtd5.mtd273;
            if (mtd2 != null)
            {
                mtd4 = mtd2.mtd290.mtd242;
                if (!mtd4.reportHeader.mtd117(Msg.Initialize) && mtd5._CodeProvider.mtd178)
                {
                    object[] objArray2 = new object[] { mtd4.reportHeader, EventArgs.Empty };
                    mtd5._CodeProvider.mtd71(mtd4.reportHeader.Name, Methods._Initialize, objArray2);
                }
                if (mtd4.reportHeader.Visible)
                {
                    mtd209 mtd7 = null;
                    mtd3 = mtd.mtd292(num4, ref mtd2, ref mtd7, num5, false);
                    var0.mtd162.mtd2(mtd3);
                    num2 += mtd3.Height;
                    num5 += mtd3.Height;
                    num4++;
                }
            }
            num3 = mtd5.mtd271.mtd166;
        Label_052E:
            while (!mtd5.mtd189)
            {
                for (int i = 0; i < num3; i++)
                {
                    mtd2 = mtd5.mtd271[i];
                    mtd3 = mtd2.mtd290;
                    if (mtd3._SectionType == SectionType.GroupHeader)
                    {
                        if (!flag)
                        {
                            mtd5.mtd174();
                            if (mtd5.mtd189)
                            {
                                goto Label_052E;
                            }
                            flag = true;
                        }
                        endIndex = mtd5.mtd278;
                        mtd4 = mtd3.mtd242;
                        if (!mtd245.mtd294(ref mtd4))
                        {
                            mtd4.mtd295();
                            if (!mtd4.groupHeader.mtd117(Msg.Initialize) && mtd5._CodeProvider.mtd178)
                            {
                                object[] objArray3 = new object[] { mtd4.groupHeader, EventArgs.Empty };
                                mtd5._CodeProvider.mtd71(mtd4.groupHeader.Name, Methods._Initialize, objArray3);
                            }
                            if (mtd4.groupHeader.Visible)
                            {
                                mtd209 mtd8 = null;
                                mtd3 = mtd.mtd292(num4, ref mtd2, ref mtd8, num5, false);
                                num4++;
                                var0.mtd162.mtd2(mtd3);
                                num2 += mtd3.Height;
                                num5 += mtd3.Height;
                            }
                            mtd4.mtd297 = mtd5.mtd278;
                        }
                    }
                    else if (mtd3._SectionType == SectionType.ReportDetail)
                    {
                        bool flag2 = false;
                        mtd2.mtd298 = true;
                        if ((mtdArray != null) && (mtdArray.Length > 0))
                        {
                            flag2 = true;
                        }
                        while (!mtd5.mtd189)
                        {
                            if (!flag)
                            {
                                mtd5.mtd174();
                                if (!mtd5.mtd189)
                                {
                                    goto Label_033D;
                                }
                                flag = true;
                                endIndex = mtd5.mtd278;
                                break;
                            }
                            flag = false;
                        Label_033D:
                            endIndex = mtd5.mtd278;
                            if (flag2 && !mtd245.mtd299(ref mtdArray))
                            {
                                flag = false;
                                endIndex = mtd5.mtd278 - 1;
                                mtd5.mtd192();
                                break;
                            }
                            mtd245.mtd300(ref mtd2);
                            mtd4 = mtd3.mtd242;
                            if (!mtd4.reportDetail.mtd117(Msg.Initialize) && mtd5._CodeProvider.mtd178)
                            {
                                object[] objArray4 = new object[] { mtd4.reportDetail, EventArgs.Empty };
                                mtd5._CodeProvider.mtd71(mtd4.reportDetail.Name, Methods._Initialize, objArray4);
                            }
                            if (mtd4.reportDetail.Visible)
                            {
                                mtd209 mtd9 = null;
                                mtd3 = mtd.mtd292(num4, ref mtd2, ref mtd9, num5, true);
                                num4++;
                                var0.mtd162.mtd2(mtd3);
                                num2 += mtd3.Height;
                                num5 += mtd3.Height;
                            }
                            mtd2.mtd298 = false;
                        }
                    }
                    else if (mtd3._SectionType == SectionType.GroupFooter)
                    {
                        mtd4 = mtd3.mtd242;
                        mtd289 mtd10 = mtd4.mtd302;
                        if ((mtd10 != null) && (mtd5.mtd189 | !mtd245.mtd294(ref mtd10)))
                        {
                            mtd.mtd303(ref mtd2.mtd304, mtd10.mtd297, endIndex);
                            if (!mtd4.groupFooter.mtd117(Msg.Initialize) && mtd5._CodeProvider.mtd178)
                            {
                                object[] objArray5 = new object[] { mtd4.groupFooter, EventArgs.Empty };
                                mtd5._CodeProvider.mtd71(mtd4.groupFooter.Name, Methods._Initialize, objArray5);
                            }
                            if (mtd4.groupFooter.Visible)
                            {
                                mtd209 mtd11 = null;
                                mtd3 = mtd.mtd292(num4, ref mtd2, ref mtd11, num5, false);
                                num4++;
                                var0.mtd162.mtd2(mtd3);
                                num2 += mtd3.Height;
                                num5 += mtd3.Height;
                            }
                        }
                    }
                }
            }
            mtd2 = mtd5.mtd274;
            if (mtd2 != null)
            {
                mtd4 = mtd2.mtd290.mtd242;
                mtd.mtd303(ref mtd2.mtd304, 0, mtd5.mtd278);
                if (!mtd4.reportFooter.mtd117(Msg.Initialize) && mtd5._CodeProvider.mtd178)
                {
                    object[] objArray6 = new object[] { mtd4.reportFooter, EventArgs.Empty };
                    mtd5._CodeProvider.mtd71(mtd4.reportFooter.Name, Methods._Initialize, objArray6);
                }
                if (mtd4.reportFooter.Visible)
                {
                    mtd209 mtd12 = null;
                    mtd3 = mtd.mtd292(num4, ref mtd2, ref mtd12, num5, false);
                    num4++;
                    var0.mtd162.mtd2(mtd3);
                    num2 += mtd3.Height;
                    num5 += mtd3.Height;
                }
            }
            mtd2 = mtd5.mtd276;
            if (mtd2 != null)
            {
                mtd307[] mtdArray2 = mtd2.mtd308;
                if (mtdArray2 != null)
                {
                    for (int j = 0; j < mtdArray2.Length; j++)
                    {
                        mtd307 mtd13 = mtdArray2[j];
                        mtd159 mtd14 = mtd13.mtd309.mtd242;
                        McTextBox box = mtd14._McTextBox;
                        box.Value = mtd14._McField.mtd110(box.SummaryFunc);
                    }
                }
                mtd4 = mtd2.mtd290.mtd242;
                if (!mtd4.pageFooter.mtd117(Msg.Initialize) && mtd5._CodeProvider.mtd178)
                {
                    object[] objArray7 = new object[] { mtd4.pageFooter, EventArgs.Empty };
                    mtd5._CodeProvider.mtd71(mtd4.reportFooter.Name, Methods._Initialize, objArray7);
                }
                mtd209 mtd15 = null;
                mtd3 = mtd.mtd292(num4, ref mtd2, ref mtd15, num5, false);
                num4++;
                var0.mtd162.mtd2(mtd3);
                num2 += mtd3.Height;
                num5 += mtd3.Height;
            }
            if ((num2 > var0._Location.Height) && mtd5.mtd269.CanGrow)
            {
                McLocation.mtd243(ref var0._Location, num2);
            }
            else if ((num2 < var0._Location.Height) && mtd5.mtd269.CanShrink)
            {
                McLocation.mtd243(ref var0._Location, num2);
            }
        Label_0766:
            if (!mtd5.mtd268.mtd117(Msg.ReportEnd) && mtd5._CodeProvider.mtd178)
            {
                object[] objArray8 = new object[] { mtd5.mtd268, EventArgs.Empty };
                mtd5._CodeProvider.mtd71("Report", Methods._End, objArray8);
            }
        }

        internal void mtd310(ref mtd164 var1, ref mtd161 var2, float var3, mtd245 var4)
        {
            float num2;
            mtd163 mtd3;
            int num4;
            int num = -1;
            mtd163 mtd = null;
            mtd161 mtd2 = mtd161.mtd105(ref var2);
            for (int i = 0; i < var2.mtd162.Count; i++)
            {
                mtd = var2.mtd162.mtd143(i);
                if ((var3 > mtd.mtd29) && (var3 < (mtd.mtd29 + mtd.Height)))
                {
                    num = i + 1;
                    goto Label_006E;
                }
                if (var3 <= mtd.mtd29)
                {
                    num = i;
                    num2 = 0f;
                    goto Label_009C;
                }
            }
            if (num == -1)
            {
                return;
            }
        Label_006E:
            var4.mtd311(ref mtd, out mtd3, 0f, var3);
            num2 = mtd3.mtd29 + mtd3.Height;
            mtd2.mtd162.mtd2(mtd3);
        Label_009C:
            num4 = var2.mtd162.Count;
            for (int j = num; j < num4; j++)
            {
                mtd = var2.mtd162.mtd143(num);
                mtd.mtd29 = num2;
                num2 += mtd.Height;
                mtd2.mtd162.mtd2(mtd);
                var2.mtd162.mtd217(mtd);
            }
            mtd2.mtd286 = true;
            mtd2.mtd208 = var2;
            var2.Height = var3;
            mtd2.Height -= var2.Height;
            mtd2.mtd129 = 0f;
            var1.mtd2(mtd2);
        }

        internal void mtd312(ref mtd161 var5, mtd245 var4)
        {
            mtd163 mtd2;
            mtd161 mtd = var5.mtd208;
            float num = mtd.mtd129 + mtd.Height;
            int num2 = 0;
            if (mtd.mtd162.Count > 0)
            {
                mtd2 = mtd.mtd162.mtd143(mtd.mtd162.Count - 1);
                if (mtd2.mtd286)
                {
                    mtd163 nPRow = var5.mtd162.mtd143(0);
                    var4.mtd313(ref mtd2, ref nPRow);
                    num2 = 1;
                }
            }
            for (int i = num2; i < var5.mtd162.Count; i++)
            {
                mtd2 = var5.mtd162.mtd143(i);
                mtd2.mtd29 = num;
                num += mtd2.Height;
                mtd.mtd162.mtd2(mtd2);
                var5.mtd162.mtd217(mtd2);
            }
            mtd.Height += var5.Height;
        }
    }
}

