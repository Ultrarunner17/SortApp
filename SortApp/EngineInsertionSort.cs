using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SortApp
{
    public class EngineInsertionSort : IEngine
    {
        private int[] data;
        private UIEngine uiEngine;
        private bool running;

        public int[] Compute(int[] array, UIEngine ui, CancellationToken token)
        {
            running = true;
            data = array;
            uiEngine = ui;

            for (int i = 0; i < data.Length - 1; i++)
            {
                for (int j = i + 1; j > 0; j--)
                {
                    uiEngine.ChangeLineColor(j - 1, Color.Blue);
                    if (data[j - 1] > data[j])
                    {
                        int temp = data[j - 1];
                        data[j - 1] = data[j];
                        uiEngine.ChangeLineValue(j - 1, data[j], uiEngine.graphic);
                        data[j] = temp;
                        uiEngine.ChangeLineValue(j, temp, uiEngine.graphic);
                    }
                    Thread.Sleep(15);
                    uiEngine.ChangeLineColor(j - 1, Color.Black);
                    if (token.IsCancellationRequested)
                    {
                        running = false;
                        return data;
                    }
                }
            }

            running = false;
            return data;
        }

        public bool IsRunning()
        {
            return running;
        }
    }
}
