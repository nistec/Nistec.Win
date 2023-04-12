namespace Nistec.Charts.Statistics
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    public static class Statistic
    {
        internal static string IsStatisticsField(string ColumnName)
        {
            int num;
            string str = ArithmeticAverage.GetArithmeticAverageBaseColumn(ColumnName);
            if (str != null)
            {
                return str;
            }
            str = GeometricAverage.GetGeometricAverageBaseColumn(ColumnName);
            if (str != null)
            {
                return str;
            }
            str = HarmonicAverage.GetHarmonicAverageBaseColumn(ColumnName);
            if (str != null)
            {
                return str;
            }
            str = Median.GetMedianAverageBaseColumn(ColumnName);
            if (str != null)
            {
                return str;
            }
            str = Mode.GetModeAverageBaseColumn(ColumnName);
            if (str != null)
            {
                return str;
            }
            str = RunningAverage.GetRunningAverageBaseColumn(ColumnName);
            if (str != null)
            {
                return str;
            }
            str = RecursiveExponential.GetRecursiveExponentialBaseColumn(ColumnName);
            if ((str != null) && (str != string.Empty))
            {
                return str;
            }
            str = RecursiveLinear.GetRecursiveLinearBaseColumn(ColumnName);
            if ((str != null) && (str != string.Empty))
            {
                return str;
            }
            str = RecursiveLogarithmic.GetRecursiveLogarithmicBaseColumn(ColumnName, out num);
            if ((str != null) && (str != string.Empty))
            {
                return str;
            }
            str = RecursivePowerCurve.GetRecursivePowerCurveBaseColumn(ColumnName, out num);
            if ((str != null) && (str != string.Empty))
            {
                return str;
            }
            str = RecursivePolynomic.GetRecursivePolynomicBaseColumn(ColumnName, out num);
            if ((str != null) && (str != string.Empty))
            {
                return str;
            }
            str = RecursiveParabolic.GetRecursiveParabolicBaseColumn(ColumnName);
            if ((str != null) && (str != string.Empty))
            {
                return str;
            }
            str = RecursiveHyperbolic.GetRecursiveHyperbolicBaseColumn(ColumnName);
            if ((str != null) && (str != string.Empty))
            {
                return str;
            }
            str = RandomData.GetRandomDataBaseColumn(ColumnName, out num);
            if ((str != null) && (str != string.Empty))
            {
                return str;
            }
            return null;
        }

        public static void Rezolve(DataTable dt, DataItemCollection dataItems, KeyItemCollection key)
        {
            int num = 0;
            foreach (DataItem item in dataItems)
            {
                KeyItem li = new KeyItem();
                if (key.Count > num)
                {
                    li = key[num];
                }
                ArithmeticAverage.AddArithmeticAverage(dt, item.Name);
                GeometricAverage.AddGeometricAverage(dt, item.Name);
                HarmonicAverage.AddHarmonicAverage(dt, item.Name);
                Median.AddMedianAverage(dt, item.Name);
                Mode.AddModeAverage(dt, item.Name);
                RunningAverage.AddRunningAverage(dt, item.Name);
                RecursiveExponential.AddExponentialRecursion(dt, item.Name, li);
                RecursiveLinear.AddLinearRecursion(dt, item.Name, li);
                RecursiveLogarithmic.AddLogarithmicRecursion(dt, item.Name, li);
                RecursivePowerCurve.AddPowerCurveRecursion(dt, item.Name, li);
                RecursivePolynomic.AddPolynomicRecursion(dt, item.Name, li);
                RecursiveParabolic.AddParabolicRecursion(dt, item.Name, li);
                RecursiveHyperbolic.AddHyperbolicRecursion(dt, item.Name, li);
                RandomData.AddRandomData(dt, item.Name);
                num++;
            }
        }
    }
}

