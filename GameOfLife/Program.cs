using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfLife
{
    public static class Program
    {
        public static void Main()
        {
            PlayFunctional();   
        }

        public static void Play()
        {
            var game = new Game(10, 20);
            game.AddFigure(5, 5, Figures.Glaider);

            var cts = new CancellationTokenSource();
            var view = new ConsoleView();
            Task.Run(() => view.Run(game, cts.Token), cts.Token);

            Console.ReadKey();
            cts.Cancel();
        }

        public static void PlayFunctional()
        {
            Console.CursorVisible = false;
            var game = new FunctionalGame();
            game.AddFigure(new [] {(3, 3), (3, 4), (3, 5)});

            new EndlessEnumerator<bool>(true)
                .ForEach(_ =>
                {
                    Console.Clear();
                    game.Live.ForEach(point =>
                    {
                        Console.SetCursorPosition(point.x, point.y);
                        Console.Write("#");
                    });
                    Thread.Sleep(500);
                    game.NextTurn();
                });
        }
    }

    public class FunctionalGame
    {
        public HashSet<(int x, int y)> Live;

        public FunctionalGame()
        {
            Live = new HashSet<(int x, int y)>();
        }

        public void NextTurn()
        {
            Live = Live
                .SelectMany(GetNeighbours)
                .Concat(Live)
                .Distinct() // а нужно ли, если там в конце ToHashSet
                .Where(IsAlive)
                .ToHashSet();
        }

        public void AddFigure(IEnumerable<(int x, int y)> points)
        {
            points.ForEach(p => Live.Add(p));
        }

        private IEnumerable<(int x, int y)> GetNeighbours((int x, int y) point)
        {
            return Enumerable
                .Range(point.x - 1, 3)
                .SelectMany(x => Enumerable.Range(point.y - 1, 3).Select(y => (x, y)))
                .Where(p => p.x != point.x || p.y != point.y);
        }

        private bool IsAlive((int x, int y) point)
        {
            var neighbours = GetNeighbours(point).Count(Live.Contains);
            
            return neighbours == 3 || (Live.Contains(point) && neighbours == 2);
        }
    }

    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var e in source)
            {
                action(e);
            }
        }
    }
    
    public class EndlessEnumerator<T> : IEnumerable<T>, IEnumerator<T>
    {
        public EndlessEnumerator(T current)
        {
            Current = current;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool MoveNext() => true;

        public void Reset() {}

        public T Current { get; }

        object IEnumerator.Current => Current;

        public void Dispose() {}
    }
}