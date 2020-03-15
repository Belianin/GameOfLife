using System;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfLife
{
    public static class Program
    {
        public static void Main()
        {
            var map = new Map(10, 20);
            map.AddFigure(5, 5, Figures.Glaider);

            var cts = new CancellationTokenSource();
            var view = new ConsoleView();
            Task.Run(() => view.Run(map, cts.Token), cts.Token);

            Console.ReadKey();
            cts.Cancel();
        }
    }
}