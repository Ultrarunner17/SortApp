using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SortApp
{
    public class UIEngine
    {
        public Canvas graphic;
        private int numberOfItems;
        private int[] data;
        private Line[] lines;
        private int _lineThickness;
        public UIEngine(Canvas cvsGraphic, int lineThickness)
        {
            graphic = cvsGraphic;
            _lineThickness = lineThickness;
        }

        public void DrawGraphic(int[] array)
        {
            data = array;
            numberOfItems = (int)graphic.ActualWidth/_lineThickness;
            lines = new Line[numberOfItems];
            graphic.Children.Clear();

            for (int i = 0; i < numberOfItems; i++)
            {
                lines[i] = new Line();
                lines[i].Visibility = Visibility.Visible;
                lines[i].StrokeThickness = _lineThickness;
                lines[i].Stroke = System.Windows.Media.Brushes.Black;
                lines[i].X1 = i * _lineThickness;
                lines[i].X2 = i * _lineThickness;
                lines[i].Y1 = 0;
                lines[i].Y2 = data[i];

                graphic.Children.Add(lines[i]);
            }
        }

        public void ChangeLineValue(int index, int newValue, Canvas graphic)
        {
            graphic.Dispatcher.Invoke(() =>
            {
                lines[index].X1 = index * _lineThickness;
                lines[index].X2 = index * _lineThickness;
                lines[index].Y1 = 0;
                lines[index].Y2 = newValue;
            });
        }

        public void ChangeLineColor(int index, Color color)
        {
            graphic.Dispatcher.Invoke(() =>
            {
                lines[index].Stroke = GetBrush(color);
            });
        }

        private System.Windows.Media.Brush GetBrush(Color c)
        {
            System.Windows.Media.Brush output;

            switch (c)
            {
                case Color.Blue:
                    output = System.Windows.Media.Brushes.Blue;
                    break;
                case Color.Red:
                    output = System.Windows.Media.Brushes.Red;
                    break;
                case Color.Green:
                    output = System.Windows.Media.Brushes.Green;
                    break;
                case Color.Black:
                    output = System.Windows.Media.Brushes.Black;
                    break;
                default:
                    output = System.Windows.Media.Brushes.Black;
                    break;
            }

            return output;
        }
    }

    public enum Color
    {
        Blue,
        Red,
        Green,
        Black
    }
}
