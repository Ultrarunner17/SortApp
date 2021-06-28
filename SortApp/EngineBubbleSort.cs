using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace SortApp
{
    public class EngineBubbleSort : IEngine
    {
        private int[] data;
        private UIEngine uiEngine;
        private bool running = false;

        public int[] Compute(int[] array, UIEngine ui, CancellationToken token)
        {
            running = true;
            data = array;
            uiEngine = ui;
            int indexOfLastUnsortedItem = data.Length - 1;

            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < indexOfLastUnsortedItem; j++)
                {
                    uiEngine.ChangeLineColor(j + 1, Color.Blue);
                    if (data[j] > data[j + 1])
                    {
                        Swap(j, j + 1);
                    }
                    Thread.Sleep(15);
                    uiEngine.ChangeLineColor(j + 1, Color.Black);

                    if (token.IsCancellationRequested)
                    {
                        running = false;
                        return data;
                    }
                }
                uiEngine.ChangeLineColor(indexOfLastUnsortedItem, Color.Green);
                indexOfLastUnsortedItem--;
            }

            running = false;
            return data;
        } 

        private void Swap(int first, int second)
        {
            int temp = data[first];
            data[first] = data[second];
            uiEngine.ChangeLineValue(first, data[second], uiEngine.graphic);

            data[second] = temp;
            uiEngine.ChangeLineValue(second, temp, uiEngine.graphic);
        }

        public bool IsRunning()
        {
            return running;
        }
    }
}
