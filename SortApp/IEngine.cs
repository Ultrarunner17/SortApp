using System.Threading;

namespace SortApp
{
    interface IEngine
    {
        int[] Compute(int[] array, UIEngine ui, CancellationToken token);
        bool IsRunning();
    }
}
