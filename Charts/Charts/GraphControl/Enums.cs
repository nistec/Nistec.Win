using System;
using System.Collections.Generic;
using System.Text;

namespace Nistec.Charts
{

    internal static class ChartMethods
    {
        public static bool IsTypeInternal(ChartType type)
        {
            if(!(type == ChartType.BarsWide3D || type==ChartType.PipesStack
                || type == ChartType.Surface3D || type== ChartType.SurfaceMulti3D
                || type== ChartType.SurfaceStack3D))
                return true ;

            return false;

        }

        public static string[] ChartTypeList
        {
            get { return Enum.GetNames(typeof(ChartType)); }
        }
    }

    public enum ChartType
    {
        Bars,
        Bars3D,
        BarsMulti,
        BarsMulti3D,
        BarsStack,
        BarsStack3D,
        BarsWide3D,
        Pie,
        Pie3D,
        PieExpanded,
        PieExpanded3D,
        Line,
        LineMulti,
        LineMulti3D,
        LineCurveMulti,
        Pipes,
        PipesMulti,
        PipesStack,
        Surface,
        Surface3D,
        SurfaceMulti,
        SurfaceMulti3D,
        SurfaceStack,
        SurfaceStack3D
        //CustomMap,
        //CustomMapPoints,
        //CustomBars
    }

    //public class en
    //{
        //ChartType type;
        //private void test()
        //{
        //    if (((((this.type ==ChartType.BarsMulti) || (this.type ==ChartType.LineMulti)) || ((this.type ==ChartType.BarsMulti) )) || (((this.type ==ChartType.BarsStack3D)) || ((this.type ==ChartType.BarsMulti3D) || (this.type ==ChartType.SurfaceMulti)))) 
        //        || ((   (((this.type ==ChartType.SurfaceStack)) || ((this.type ==ChartType.LineMulti3D) || (this.type ==ChartType.LineCurveMulti)))) || (((this.type ==ChartType.PipesStack)) || (((this.type ==ChartType.Surface3D) || (this.type ==ChartType.SurfaceStack3D)) || (this.type ==ChartType.SurfaceMulti3D)))))
        //    {
        //        //bitmap2 = this.DrawMultiKey();
        //    }
        //    if (((((this.type ==ChartType.Pie)) || ((this.type ==ChartType.Pie3D) || (this.type ==ChartType.PieExpanded))) || ((this.type ==ChartType.PieExpanded3D))) && (this.XAxisLabels != string.Empty))
        //    {
        //        //bitmap2 = this.DrawPieKey();
        //    }
        //    if (((this.type ==ChartType.CustomMap) || (this.type ==ChartType.CustomMapPoints)) && (this.FieldCategory != string.Empty))
        //    {
        //        //bitmap2 = this.DrawCategoryKey();
        //    }

        //}
    //}
    public enum ChartType1
    {
        Bars=0,
        Pie=1,
        Bars3D=2,
        //Donut=3,
        Multibar=4,
        Line=5,
        MultiLine=6,
        StackBars=7,
        //StackBarsFull=8,
        StackBars3D=9,
        //StackBars3DFull=10,
        Multibars3D=11,
        Pipe ,//Cylinders=12,
        Pie3D=13,
        Surface=14,
        MultiSurface=15,
        CustomMap,//Map=16,
        ExpandedPie,//ExplodedPie=17,
        //RadarLine=18,
        //RadarSurface=19,
        CustomDrawnBars,//UserDrawnBars=20
        //MultiRadarLine=21,
        //MultiRadarSurface=22,
        //StackRadarLine=23,
        //StackRadarSurface=24,
        StackSurface=25,
        //FullStackSurface=26,
        Multi3DLine=27,
        MultiCurve=28,
        Expanded3DPie,//Exploded3DPie=29,
        //Exploded3DDonut=30,
        MapPoints=31,
        Bars3DWide=32,
        StackPipes,//StackCylinders=33,
        //FullStackCylinders=34,
        Surface3D=35,
        MultiSurface3D=36,
        StackSurface3D=37
    }
}
