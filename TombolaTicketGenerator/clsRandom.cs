using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TombolaTicketGenerator
{
    internal static class clsRandom
    {
        private static int seed;

        private static ThreadLocal<Random> threadLocal = new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref seed)));

        static clsRandom()
        {
            seed = Environment.TickCount;
        }

        internal static Random Instance { get { return threadLocal.Value; } }
    }
}
