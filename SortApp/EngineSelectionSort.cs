using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SortApp
{
    public class EngineSelectionSort : IEngine
    {
        private int[] data;
        private UIEngine uiEngine;
        private bool running;

        public int[] Compute(int[] array, UIEngine ui, CancellationToken token)
        {
            running  = true;
            data = array;
            uiEngine = ui;
            int indexFirstUnsortedItem = 0;
            int minValue;
            int minIndex;

            for (int i = 0; i < data.Length - 1; i++)
            {
                minValue = data[indexFirstUnsortedItem];
                minIndex = indexFirstUnsortedItem;
                uiEngine.ChangeLineColor(minIndex, Color.Red);
                for (int j = indexFirstUnsortedItem; j < data.Length; j++)
                {
                    uiEngine.ChangeLineColor(j, Color.Blue);
                    if (data[j] <=    minValue)
                    {
                        uiEngine.ChangeLineColor(minIndex, Color.Black);
                        minValue = data[j];
                        minIndex = j;
                        uiEngine.ChangeLineColor(minIndex, Color.Red);
                    }
                    Thread.Sleep(15);
                    if (minIndex != j)
                    {
                        uiEngine.ChangeLineColor(j, Color.Black);
                    }
                    if (token.IsCancellationRequested)
                    {
                        running = false;
                        return data;
                    }
                }
                Swap(indexFirstUnsortedItem, minIndex);
                uiEngine.ChangeLineColor(minIndex, Color.Black);
                uiEngine.ChangeLineColor(indexFirstUnsortedItem, Color.Green);
                indexFirstUnsortedItem++;
            }
            uiEngine.ChangeLineColor(indexFirstUnsortedItem, Color.Green);

            running = false;
            return data;
        }

        public void Swap(int indexFirstUnsortedItem, int minIndex)
        {
            int temp = data[indexFirstUnsortedItem];
            data[indexFirstUnsortedItem] = data[minIndex];
            uiEngine.ChangeLineValue(indexFirstUnsortedItem, data[minIndex], uiEngine.graphic);

            data[minIndex] = temp;
            uiEngine.ChangeLineValue(minIndex, temp, uiEngine.graphic);
        }

        public bool IsRunning()
        {
            return running;
        }
    }
}
