namespace Nistec.Charts
{
    using System;

    public class ChartException : Exception
    {
        public ChartException(Exception ex)
            : base("An exception occured in Nistec.Charts: " + ex.Message, ex)
        {
        }
    }

    public static class ChartValidation 
    {

        public static bool ValidateData(object data, int dataItemsCount, bool throwOnError)
        {
            if (data == null)
            {
                if (throwOnError)
                    ThrowNoData();
                return false;
            }
            if (dataItemsCount == 0)
            {
                if (throwOnError)
                    ThrowNoDataItems();
                return false;
            }
            return true;
        }

        public static bool ValidateDataSource(object data,bool throwOnError)
        {
            if (data == null)
            {
                if (throwOnError)
                    ThrowNoData();
                return false;
            }
            return true;
        }

        public static bool ValidateDataItems(int dataItemsCount, bool throwOnError)
        {
            if (dataItemsCount == 0)
            {
                if (throwOnError)
                    ThrowNoDataItems();
                return false;
            }
            return true;
        }

        public static bool ValidateRowCount(int rowCount, bool throwOnError)
        {
            if (rowCount == 0)
            {
                if (throwOnError)
                    ThrowNoDataItems();
                return false;
            }
            return true;
        }

        public static void ThrowNoData()
        {
            throw new Exception("No Data Found!");
            //throw new Exception("ERROR: No Data Source Found! Use either DataSource or DataSourceID properties to set the datasource.");
        }
        public static void ThrowNoDataItems()
        {
            throw new Exception("No Data Item Found!");
            //throw new Exception("ERROR: No Data Fileds set! Use either DataItems property or SetDataFields method to set the fileds that will appear on the Y axis.");
        }
        public static void ThrowNoRowCount()
        {
                throw new Exception("No Data Row Found!");
        }



    }
}

