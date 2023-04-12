namespace Nistec.Printing.ExcelXml
{
    using System;
    using System.Collections.Generic;

    public class CellCollection : List<Cell>
    {
        public void Add(Range range)
        {
            foreach (Cell cell in range)
            {
                base.Add(cell);
            }
        }

        public void Add(Row row)
        {
            foreach (Cell cell in row)
            {
                base.Add(cell);
            }
        }

        public void Add(Worksheet ws)
        {
            foreach (Cell cell in ws)
            {
                base.Add(cell);
            }
        }

        public void Add(Cell cell, Predicate<Cell> filterCondition)
        {
            if (filterCondition(cell))
            {
                base.Add(cell);
            }
        }

        public void Add(Range range, Predicate<Cell> filterCondition)
        {
            foreach (Cell cell in range)
            {
                if (filterCondition(cell))
                {
                    base.Add(cell);
                }
            }
        }

        public void Add(Row row, Predicate<Cell> filterCondition)
        {
            foreach (Cell cell in row)
            {
                if (filterCondition(cell))
                {
                    base.Add(cell);
                }
            }
        }

        public void Add(Worksheet ws, Predicate<Cell> filterCondition)
        {
            foreach (Cell cell in ws)
            {
                if (filterCondition(cell))
                {
                    base.Add(cell);
                }
            }
        }
    }
}

