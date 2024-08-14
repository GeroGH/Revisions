using System;
using Tekla.Structures.Drawing;

using Point = Tekla.Structures.Geometry3d.Point;
using PointList = Tekla.Structures.Drawing.PointList;

namespace Revisions
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            while (true)
            {
                try
                {
                    var dh = new DrawingHandler();

                    var picker = dh.GetPicker();
                    picker.PickTwoPoints("Pick insertion point", "Pick diagonal point", out var startPoint, out var endPoint, out var view);

                    var pointList = new PointList();
                    pointList.Add(new Point(startPoint.X, startPoint.Y));
                    pointList.Add(new Point(endPoint.X, startPoint.Y));
                    pointList.Add(new Point(endPoint.X, endPoint.Y));
                    pointList.Add(new Point(startPoint.X, endPoint.Y));

                    var cloud = new Cloud(view as ViewBase, pointList);
                    cloud.Attributes.Line.Color = DrawingColors.Cyan;
                    cloud.Bulge = 0.30;
                    cloud.Insert();

                    //var offset = 50;
                    //if (view.IsSheet) { offset = 5; }

                    //var revisionPoint = new Point(endPoint.X + offset, endPoint.Y + offset);
                    //var text = new Text(view as ViewBase, revisionPoint, "P01");
                    //text.Attributes.Frame.Color = DrawingColors.Black;
                    //text.Attributes.Frame.Type = FrameTypes.Triangle;
                    //text.Insert();

                    dh.GetActiveDrawing().CommitChanges();
                }
                catch (Exception)
                {
                    return;
                }
            }
        }
    }
}
