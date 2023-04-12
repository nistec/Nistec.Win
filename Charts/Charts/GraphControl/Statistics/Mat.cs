namespace Nistec.Charts.Statistics
{
    using System;

    //Sistem
    internal class Mat
    {
        private int m;
        internal double[][] mat;

        internal Mat(int m)
        {
            this.mat = new double[m][];
            for (int i = 0; i < m; i++)
            {
                this.mat[i] = new double[m + 1];
            }
            this.m = m;
        }

        internal int GetLiniarMaxCol(int col, int Cu)//GetLiniaCuElMaxPeColJ(int colJ, int incepandCu)
        {
            double num = this.mat[Cu][col];
            int num2 = Cu;
            for (int i = Cu; i < this.m; i++)
            {
                if (num < this.mat[i][col])
                {
                    num = this.mat[i][col];
                    num2 = i;
                }
            }
            return num2;
        }

        internal void rezolve()
        {
            for (int i = 0; i < this.m; i++)
            {
                int index = this.GetLiniarMaxCol(i, i);
                double[] numArray = this.mat[index];
                this.mat[index] = this.mat[i];
                this.mat[i] = numArray;
                double num3 = this.mat[i][i];
                for (int j = i; j < (this.m + 1); j++)
                {
                    this.mat[i][j] /= num3;
                }
                for (int k = 0; k < this.m; k++)
                {
                    if (k != i)
                    {
                        num3 = this.mat[k][i];
                        for (int m = 0; m < (this.m + 1); m++)
                        {
                            this.mat[k][m] -= num3 * this.mat[i][m];
                        }
                    }
                }
            }
        }
    }
}

