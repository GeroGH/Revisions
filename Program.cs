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
                    var drawing = dh.GetActiveDrawing();
                    var drawingType = drawing.GetType().ToString();
                    var plotName = drawing.GetPlotFileName(true);
                    var revIndex = plotName.LastIndexOf("_");
                    var rev = plotName.Substring(revIndex + 1);

                    var picker = dh.GetPicker();
                    picker.PickTwoPoints("Pick insertion point ...", "Pick diagonal point ...", out var startPoint, out var endPoint, out var view);

                    var pointList = new PointList();
                    pointList.Add(new Point(startPoint.X, startPoint.Y));
                    pointList.Add(new Point(endPoint.X, startPoint.Y));
                    pointList.Add(new Point(endPoint.X, endPoint.Y));
                    pointList.Add(new Point(startPoint.X, endPoint.Y));

                    var cloud = new Cloud(view, pointList);
                    cloud.Attributes.Line.Color = DrawingColors.Cyan;
                    cloud.Bulge = 0.30;
                    cloud.Insert();
                    dh.GetActiveDrawing().CommitChanges();

                    picker.PickPoint("Pick revision insertion point ...", out var endPointRev, out var viewRev);

                    var revisionPoint = new Point(endPointRev.X, endPointRev.Y);
                    var text = new Text(view, revisionPoint, rev);
                    text.Attributes.Font.Height = 2.5;
                    text.Attributes.Frame.Color = DrawingColors.Black;
                    text.Attributes.Frame.Type = FrameTypes.Triangle;
                    text.Attributes.Font.Color = DrawingColors.Cyan;
                    text.Insert();

                    drawing.CommitChanges();
                }
                catch (Exception)
                {
                    return;
                }
            }
        }
    }
}
