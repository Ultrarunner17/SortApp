using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SortApp
{
    public class UIEngine
    {
        private Canvas graphic;
        private int numberOfItems;
        private int[] data;
        private Line[] lines;
        private int _lineThickness;


        public UIEngine(Canvas cvsGraphic, int lineThickness)
        {
            graphic = cvsGraphic;
            _lineThickness = lineThickness;
        }

        /// <summary>
        /// Initialize the graphic
        /// </summary>
        /// <param name="array">Data used</param>
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
                lines[i].Stroke = GetBrush(Color.Orange);
                lines[i].X1 = i * _lineThickness;
                lines[i].X2 = i * _lineThickness;
                lines[i].Y1 = (int)graphic.ActualHeight;
                lines[i].Y2 = (int)graphic.ActualHeight - data[i];

                graphic.Children.Add(lines[i]);
            }
        }

        /// <summary>
        /// Update the value of one line
        /// </summary>
        /// <param name="index">Index of the line to update</param>
        /// <param name="newValue">The new value</param>
        public void ChangeLineValue(int index, int newValue)
        {
            graphic.Dispatcher.Invoke(() =>
            {
                lines[index].X1 = index * _lineThickness;
                lines[index].X2 = index * _lineThickness;
                lines[index].Y1 = (int)graphic.ActualHeight;
                lines[index].Y2 = (int)graphic.ActualHeight - newValue;
            });
        }

        /// <summary>
        /// Update the line's color
        /// </summary>
        /// <param name="index">Index of the line to update</param>
        /// <param name="color">The new color</param>
        public void ChangeLineColor(int index, Color color)
        {
            graphic.Dispatcher.Invoke(() =>
            {
                lines[index].Stroke = GetBrush(color);
            });
        }

        /// <summary>
        /// Convert the enum Color to Brush color
        /// </summary>
        /// <param name="c">Color enum</param>
        /// <returns>The Brush corresponding</returns>
        private Brush GetBrush(Color c)
        {
            Brush output;

            switch (c)
            {
                case Color.Yellow:
                    output = new SolidColorBrush((System.Windows.Media.Color)ColorConverter.ConvertFromString("#E9C46A"));
                    break;
                case Color.Red:
                    output = new SolidColorBrush((System.Windows.Media.Color)ColorConverter.ConvertFromString("#d00000"));
                    break;
                case Color.Green:
                    output = new SolidColorBrush((System.Windows.Media.Color)ColorConverter.ConvertFromString("#2d6a4f"));
                    break;
                case Color.Orange:
                    output = new SolidColorBrush((System.Windows.Media.Color)ColorConverter.ConvertFromString("#E76F51")); 
                    break;
                default:
                    output = new SolidColorBrush((System.Windows.Media.Color)ColorConverter.ConvertFromString("#E76F51"));
                    break;
            }

            return output;
        }
    }

    public enum Color
    {
        Yellow,
        Red,
        Green,
        Orange
    }
}
