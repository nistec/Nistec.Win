using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using System.Security.Permissions;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Text;
using System.Drawing.Design;
using System.Security;

namespace Nistec.GridView
{
	internal class Triangle
	{
		// Methods
		public Triangle(){}
		private static Point[] BuildTrianglePoints(TriangleDirection dir, Rectangle bounds)
		{
			Point[] pointArray1 = new Point[3];
			int num1 = (int) (bounds.Width * 0.8);
			if ((num1 % 2) == 1)
			{
				num1++;
			}
			int num2 = (int) Math.Ceiling((num1 / 2) * 2.5);
			int num3 = (int) (bounds.Height * 0.8);
			if ((num3 % 2) == 0)
			{
				num3++;
			}
			int num4 = (int) Math.Ceiling((num3 / 2) * 2.5);
			switch (dir)
			{
				case TriangleDirection.Up:
					pointArray1[0] = new Point(0, num2);
					pointArray1[1] = new Point(num1, num2);
					pointArray1[2] = new Point(num1 / 2, 0);
					break;

				case TriangleDirection.Down:
					pointArray1[0] = new Point(0, 0);
					pointArray1[1] = new Point(num1, 0);
					pointArray1[2] = new Point(num1 / 2, num2);
					break;

				case TriangleDirection.Left:
					pointArray1[0] = new Point(num3, 0);
					pointArray1[1] = new Point(num3, num4);
					pointArray1[2] = new Point(0, num4 / 2);
					break;

				case TriangleDirection.Right:
					pointArray1[0] = new Point(0, 0);
					pointArray1[1] = new Point(0, num4);
					pointArray1[2] = new Point(num3, num4 / 2);
					break;
			}
			switch (dir)
			{
				case TriangleDirection.Up:
				case TriangleDirection.Down:
					Triangle.OffsetPoints(pointArray1, bounds.X + ((bounds.Width - num2) / 2), bounds.Y + ((bounds.Height - num1) / 2));
					return pointArray1;

				case TriangleDirection.Left:
				case TriangleDirection.Right:
					Triangle.OffsetPoints(pointArray1, bounds.X + ((bounds.Width - num3) / 2), bounds.Y + ((bounds.Height - num4) / 2));
					return pointArray1;
			}
			return pointArray1;
		}

 
		private static void OffsetPoints(Point[] points, int xOffset, int yOffset)
		{
			for (int num1 = 0; num1 < points.Length; num1++)
			{
				points[num1].X += xOffset;
				points[num1].Y += yOffset;
			}
		}

 
		public static void Paint(Graphics g, Rectangle bounds, TriangleDirection dir, Brush backBr, Pen backPen)
		{
			Triangle.Paint(g, bounds, dir, backBr, backPen, true);
		}

 
		public static void Paint(Graphics g, Rectangle bounds, TriangleDirection dir, Brush backBr, Pen backPen, bool opaque)
		{
			Point[] pointArray1 = Triangle.BuildTrianglePoints(dir, bounds);
			if (opaque)
			{
				g.FillPolygon(backBr, pointArray1);
			}
			g.DrawPolygon(backPen, pointArray1);
		}

		public static void Paint(Graphics g, Rectangle bounds, TriangleDirection dir, Brush backBr, Pen backPen1, Pen backPen2, Pen backPen3, bool opaque)
		{
			Point[] pointArray1 = Triangle.BuildTrianglePoints(dir, bounds);
			g.DrawLine(backPen1, pointArray1[0], pointArray1[1]);
			g.DrawLine(backPen2, pointArray1[1], pointArray1[2]);
			g.DrawLine(backPen3, pointArray1[2], pointArray1[0]);
		}

 

		// Fields
		private const double TRI_HEIGHT_RATIO = 2.5;
		private const double TRI_WIDTH_RATIO = 0.8;
	}
 

}
