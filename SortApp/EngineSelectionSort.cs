using System.Threading;

namespace SortApp
{
    public class EngineSelectionSort : IEngine
    {
        private int[] data;
        private UIEngine uiEngine;
        private bool running;

        /// <summary>
        /// Compute Selection sort algo
        /// </summary>
        /// <param name="array">Array of int to sort</param>
        /// <param name="ui">UI engine to use to visualize during sorting</param>
        /// <param name="token">Cancellation token used to cancel the task if needed</param>
        /// <returns>Int array sorted</returns>
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
                    uiEngine.ChangeLineColor(j, Color.Yellow);
                    if (data[j] <=    minValue)
                    {
                        uiEngine.ChangeLineColor(minIndex, Color.Orange);
                        minValue = data[j];
                        minIndex = j;
                        uiEngine.ChangeLineColor(minIndex, Color.Red);
                    }
                    Thread.Sleep(15);
                    if (minIndex != j)
                    {
                        uiEngine.ChangeLineColor(j, Color.Orange);
                    }
                    if (token.IsCancellationRequested)
                    {
                        running = false;
                        return data;
                    }
                }
                Swap(indexFirstUnsortedItem, minIndex);
                uiEngine.ChangeLineColor(minIndex, Color.Orange);
                uiEngine.ChangeLineColor(indexFirstUnsortedItem, Color.Green);
                indexFirstUnsortedItem++;
            }
            uiEngine.ChangeLineColor(indexFirstUnsortedItem, Color.Green);

            running = false;
            return data;
        }

        /// <summary>
        /// Swap values between two items
        /// </summary>
        /// <param name="indexFirstUnsortedItem">Index of first unsorted item</param>
        /// <param name="minIndex">Index of min item found</param>
        public void Swap(int indexFirstUnsortedItem, int minIndex)
        {
            int temp = data[indexFirstUnsortedItem];
            data[indexFirstUnsortedItem] = data[minIndex];
            uiEngine.ChangeLineValue(indexFirstUnsortedItem, data[minIndex]);

            data[minIndex] = temp;
            uiEngine.ChangeLineValue(minIndex, temp);
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
