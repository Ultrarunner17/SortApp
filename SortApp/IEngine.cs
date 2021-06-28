using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SortApp
{
    interface IEngine
    {
        int[] Compute(int[] array, UIEngine ui, CancellationToken token);
        bool IsRunning();
    }
}
