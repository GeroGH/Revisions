using System.Windows.Forms;

using Tekla.Structures.Drawing;
using Point = Tekla.Structures.Geometry3d.Point;
using PointList = Tekla.Structures.Drawing.PointList;

namespace Revisions
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            this.InitializeComponent();

            var MyDrawingHandler = new DrawingHandler();
            if (MyDrawingHandler.GetConnectionStatus())
            {
                var views = MyDrawingHandler.GetActiveDrawing().GetSheet().GetAllViews();
                while (views.MoveNext())
                {
                    Cloud cloud;
                    var pointList = new PointList();
                    pointList.Add(new Point(10, 10));
                    pointList.Add(new Point(250, 10));
                    pointList.Add(new Point(350, 450));
                    pointList.Add(new Point(375, 675));
                    cloud = new Cloud(views.Current as ViewBase, pointList);
                    cloud.Attributes.Line.Color = DrawingColors.Cyan;
                    cloud.Insert();
                }
            }

            MyDrawingHandler.GetActiveDrawing().CommitChanges();
        }
    }
}
