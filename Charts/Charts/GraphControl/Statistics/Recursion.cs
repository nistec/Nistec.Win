namespace Nistec.Charts.Statistics
{
    using System;
    using System.Collections.Generic;

    internal class Recursion      {
        private List<Punct> al;
        public double[] b = new double[10];
        private int ordu;
        private double Sx;
        private double Sy;
        public string text = "";
        private RecursiveType tipu = RecursiveType.Linear;
        public bool valid = true;

        public Recursion(List<Punct> al)
        {
            this.al = al;
        }

        public double calcSlgX()
        {
            double num = 0.0;
            foreach (Punct punct in this.al)
            {
                num += Math.Log10(punct.x);
            }
            return num;
        }

        public double calcSlnX()
        {
            double num = 0.0;
            foreach (Punct punct in this.al)
            {
                num += Math.Log(punct.x, 2.7182818284590451);
            }
            return num;
        }

        public double calcSx()
        {
            this.Sx = 0.0;
            foreach (Punct punct in this.al)
            {
                this.Sx += punct.x;
            }
            return this.Sx;
        }

        public double calcSXlaj(int j)
        {
            double num = 0.0;
            foreach (Punct punct in this.al)
            {
                num += Math.Pow(punct.x, (double) j);
            }
            return num;
        }

        public double calcSXlajlgY(int j)
        {
            double num = 0.0;
            foreach (Punct punct in this.al)
            {
                num += Math.Pow(punct.x, (double) j) * Math.Log10(punct.y);
            }
            return num;
        }

        public double calcSXlajlnY(int j)
        {
            double num = 0.0;
            foreach (Punct punct in this.al)
            {
                num += Math.Pow(punct.x, (double) j) * Math.Log(punct.y, 2.7182818284590451);
            }
            return num;
        }

        public double calcSXlajY(int j)
        {
            double num = 0.0;
            foreach (Punct punct in this.al)
            {
                num += Math.Pow(punct.x, (double) j) * punct.y;
            }
            return num;
        }

        public double calcSy()
        {
            this.Sy = 0.0;
            foreach (Punct punct in this.al)
            {
                this.Sy += punct.y;
            }
            return this.Sy;
        }

        public double recalculeazaY(double x)
        {
            double num = 0.0;
            if (this.tipu == RecursiveType.Polynomic)
            {
                for (int i = 0; i <= this.ordu; i++)
                {
                    num += this.b[i] * Math.Pow(x, (double) i);
                }
            }
            if (this.tipu == RecursiveType.Exponential)
            {
                num = this.b[0] * Math.Pow(this.b[1], x);
            }
            if (this.tipu == RecursiveType.PowerCurve)
            {
                num = this.b[0] * Math.Pow(x, this.b[1]);
            }
            if (this.tipu == RecursiveType.Logarithmic)
            {
                num = this.b[0] + (this.b[1] * Math.Log10(x));
            }
            if (this.tipu == RecursiveType.Hyperbolic)
            {
                num = this.b[0] + (this.b[1] / x);
            }
            return num;
        }

        public void rezolva(RecursiveType tip, int ord)
        {
            this.tipu = tip;
            this.ordu = ord;
            if (tip == RecursiveType.Hyperbolic)
            {
                Mat sistem = new Mat(2);
                sistem.mat[0][0] = this.calcSXlaj(0);
                sistem.mat[1][0] = this.calcSXlaj(-1);
                sistem.mat[0][1] = sistem.mat[1][0];
                sistem.mat[1][1] = this.calcSXlaj(-2);
                sistem.mat[0][2] = this.calcSXlajY(0);
                sistem.mat[1][2] = this.calcSXlajY(-1);
                sistem.rezolve();
                this.b[0] = sistem.mat[0][2];
                this.b[1] = sistem.mat[1][2];
                if ((this.b[0].ToString() == "NaN") || (this.b[1].ToString() == "NaN"))
                {
                    this.valid = false;
                }
                else
                {
                    this.valid = true;
                }
                this.text = " = " + this.b[0].ToString("0.00");
                if (this.b[1] > 0.0)
                {
                    this.text = this.text + " + ";
                }
                this.text = this.text + this.b[1].ToString("0.00") + "/x";
            }
            if (tip == RecursiveType.Logarithmic)
            {
                Mat sistem2 = new Mat(2);
                sistem2.mat[0][0] = this.calcSXlaj(0);
                sistem2.mat[1][0] = this.calcSlgX();
                sistem2.mat[0][1] = sistem2.mat[1][0];
                double num = 0.0;
                foreach (Punct punct in this.al)
                {
                    num += Math.Log10(punct.x) * Math.Log10(punct.x);
                }
                sistem2.mat[1][1] = num;
                sistem2.mat[0][2] = this.calcSXlajY(0);
                num = 0.0;
                foreach (Punct punct2 in this.al)
                {
                    num += Math.Log10(punct2.x) * punct2.y;
                }
                sistem2.mat[1][2] = num;
                sistem2.rezolve();
                this.b[0] = sistem2.mat[0][2];
                this.b[1] = sistem2.mat[1][2];
                if ((this.b[0].ToString() == "NaN") || (this.b[1].ToString() == "NaN"))
                {
                    this.valid = false;
                }
                else
                {
                    this.valid = true;
                }
                this.text = " = " + this.b[0].ToString("0.00");
                if (this.b[1] > 0.0)
                {
                    this.text = this.text + " + ";
                }
                this.text = this.text + this.b[1].ToString("0.00") + "*lg(x)";
            }
            if (tip == RecursiveType.PowerCurve)
            {
                double num2 = 0.0;
                foreach (Punct punct3 in this.al)
                {
                    num2 += Math.Log(punct3.x, 2.7182818284590451) * Math.Log(punct3.y, 2.7182818284590451);
                }
                double num3 = Math.Pow(this.calcSlnX(), 2.0);
                double num4 = 0.0;
                foreach (Punct punct4 in this.al)
                {
                    num4 += Math.Log(punct4.x, 2.7182818284590451) * Math.Log(punct4.x, 2.7182818284590451);
                }
                num4 *= this.calcSXlaj(0);
                this.b[1] = ((this.calcSlnX() * this.calcSXlajlnY(0)) - (this.calcSXlaj(0) * num2)) / (num3 - num4);
                this.b[0] = Math.Exp((this.calcSXlajlnY(0) - (this.b[1] * this.calcSXlajlnY(0))) / this.calcSXlaj(0));
                if ((this.b[0].ToString() == "NaN") || (this.b[1].ToString() == "NaN"))
                {
                    this.valid = false;
                }
                else
                {
                    this.valid = true;
                }
                this.text = "y = " + this.b[0].ToString("0.00") + " * x^" + this.b[1].ToString("0.00");
            }
            if (tip == RecursiveType.Exponential)
            {
                Mat sistem3 = new Mat(2);
                sistem3.mat[0][0] = this.calcSXlaj(0);
                sistem3.mat[1][0] = this.calcSx();
                sistem3.mat[0][1] = sistem3.mat[1][0];
                sistem3.mat[1][1] = this.calcSXlaj(2);
                sistem3.mat[0][2] = this.calcSXlajlgY(0);
                sistem3.mat[1][2] = this.calcSXlajlgY(1);
                sistem3.rezolve();
                this.b[0] = Math.Pow(10.0, sistem3.mat[0][2]);
                this.b[1] = Math.Pow(10.0, sistem3.mat[1][2]);
                if ((this.b[0].ToString() == "NaN") || (this.b[1].ToString() == "NaN"))
                {
                    this.valid = false;
                }
                else
                {
                    this.valid = true;
                }
                this.text = " = " + this.b[0].ToString("0.00") + " * " + this.b[1].ToString("0.00") + "^x";
            }
            if (tip == RecursiveType.Linear)
            {
                this.rezolva(RecursiveType.Polynomic, 1);
            }
            if (tip == RecursiveType.Parabolic)
            {
                this.rezolva(RecursiveType.Polynomic, 2);
            }
            if (tip == RecursiveType.Polynomic)
            {
                Mat sistem4 = new Mat(ord + 1);
                double[] numArray = new double[(2 * ord) + 1];
                for (int i = 0; i < ((2 * ord) + 1); i++)
                {
                    numArray[i] = this.calcSXlaj(i);
                }
                for (int j = 0; j <= ord; j++)
                {
                    for (int n = 0; n <= ord; n++)
                    {
                        sistem4.mat[j][n] = numArray[j + n];
                    }
                }
                for (int k = 0; k < (ord + 1); k++)
                {
                    sistem4.mat[k][ord + 1] = this.calcSXlajY(k);
                }
                sistem4.rezolve();
                this.valid = true;
                this.text = " = ";
                for (int m = 0; m < (ord + 1); m++)
                {
                    this.b[m] = sistem4.mat[m][ord + 1];
                    if (this.b[m].ToString() == "NaN")
                    {
                        this.valid = false;
                    }
                    if ((m != 0) && (this.b[m] > 0.0))
                    {
                        this.text = this.text + " + ";
                    }
                    if (this.b[m] != 0.0)
                    {
                        this.text = this.text + this.b[m].ToString("###0.##") + "*x^" + m.ToString();
                    }
                }
            }
            foreach (Punct punct5 in this.al)
            {
                punct5.y_recalc = this.recalculeazaY(punct5.x);
                punct5.Er = punct5.y_recalc - punct5.y;
            }
        }

        public enum RecursiveType
        {
            Exponential = 1,
            Hyperbolic = 8,
            Linear = 2,
            Logarithmic = 3,
            Parabolic = 7,
            Polynomic = 5,
            PowerCurve = 4
        }
    }
}

