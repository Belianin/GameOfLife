using System;
using System.Threading;

namespace GameOfLife
{
    public class ConsoleView : ILifeView
    {
        public void Run(Game game, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                Thread.Sleep(300);
                Console.Clear();
                Console.CursorVisible = false;
                for (int i = 0; i < game.Width; i++)
                {
                    for (int j = 0; j < game.Height; j++)
                    {
                        if (game[i, j])
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
                
                game.NextTurn();
            }
        }
    }
}