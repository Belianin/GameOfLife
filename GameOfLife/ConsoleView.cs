using System;
using System.Threading;

namespace GameOfLife
{
    public class ConsoleView : ILifeView
    {
        public void Run(Map map, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                Thread.Sleep(300);
                Console.Clear();
                Console.CursorVisible = false;
                for (int i = 0; i < map.Width; i++)
                {
                    for (int j = 0; j < map.Height; j++)
                    {
                        if (map[i, j])
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("#");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write(".");
                        }
                    }

                    Console.WriteLine();
                }
                
                map.NextTurn();
            }
        }
    }
}