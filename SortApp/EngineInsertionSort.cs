using System.Threading;

namespace SortApp
{
    public class EngineInsertionSort : IEngine
    {
        private int[] data;
        private UIEngine uiEngine;
        private bool running;

        /// <summary>
        /// Compute Insertion sort algo
        /// </summary>
        /// <param name="array">Array of int to sort</param>
        /// <param name="ui">UI engine to use to visualize during sorting</param>
        /// <param name="token">Cancellation token used to cancel the task if needed</param>
        /// <returns>Int array sorted</returns>
        public int[] Compute(int[] array, UIEngine ui, CancellationToken token)
        {
            running = true;
            data = array;
            uiEngine = ui;

            for (int i = 0; i < data.Length - 1; i++)
            {
                for (int j = i + 1; j > 0; j--)
                {
                    uiEngine.ChangeLineColor(j - 1, Color.Yellow);
                    if (data[j - 1] > data[j])
                    {
                        int temp = data[j - 1];
                        data[j - 1] = data[j];
                        uiEngine.ChangeLineValue(j - 1, data[j]);
                        data[j] = temp;
                        uiEngine.ChangeLineValue(j, temp);
                    }
                    Thread.Sleep(15);
                    uiEngine.ChangeLineColor(j - 1, Color.Orange);
                    if (token.IsCancellationRequested)
                    {
                        running = false;
                        return data;
                    }
                }
            }

            for (int i = 0; i < data.Length; i++)
            {
                uiEngine.ChangeLineColor(i, Color.Green);
            }

            running = false;
            return data;
        }

        /// <summary>
        /// Tells if the sorting is running
        /// </summary>
        /// <returns>True if running, False if not </returns>
        public bool IsRunning()
        {
            return running;
        }
    }
}
