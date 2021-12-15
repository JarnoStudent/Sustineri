using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace Sustineri_Verdieping
{
    public class CreateCharts : Controls
    {
        public Chart ChartObj { get; private set; }
        public List<string> PointsX { get; private set; }
        public List<double> PointsY { get; private set; }

        public CreateCharts(Point point, Size size, Control parent, string chartTitle = "")
        {
            protpoint = point;
            protsize = size;
            ObjParent = parent;
            ObjName = chartTitle;
        }

        public Chart Design(SeriesChartType chartType, List<string> horizontal, List<double> vertical, string series, Color color = new Color(), int xMinimum = 0, bool valueAsLabel = true, bool isPercentage = false)
        {
            ChartObj = new Chart
            {
                Location = ObjPoint,
                Size = ObjSize,
                Parent = ObjParent
            };

            ChartObj.Titles.Add(ObjName);
            ChartObj.Titles[0].Alignment = ContentAlignment.TopLeft;
            ChartObj.Titles[0].Font = FontSustineri.H1;

            ChartObj.ChartAreas.Add("Area");
            ChartArea area = ChartObj.ChartAreas[0];
            area.AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            area.AxisX.Minimum = xMinimum;
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.LineColor = Color.Gray;

            ChartObj.Series.Add(series);
            Series chartSeries = ChartObj.Series[series];
            chartSeries.ChartType = chartType;
            chartSeries.IsValueShownAsLabel = valueAsLabel;
            chartSeries.Font = FontSustineri.TextFont;
            if (isPercentage) 
            {                
                chartSeries.LabelFormat = "{0.0}%";
            }

            if (chartType == SeriesChartType.Pie)
            {
                ChartObj.PaletteCustomColors = new Color[] { ColorSustineri.Blue, ColorSustineri.Green };
                area.Area3DStyle.Rotation = -90;
            }
            else chartSeries.Color = color;

            for (int i = 0; i < horizontal.Count; i++)
            {
                if (i < vertical.Count && vertical[i] != 0) chartSeries.Points.AddXY(horizontal[i], vertical[i]);
                else chartSeries.Points.AddXY(horizontal[i], double.NaN);
            }
            ChartObj.SendToBack();

            PointsX = horizontal;
            PointsY = vertical;

            return ChartObj;
        }
    }
}
