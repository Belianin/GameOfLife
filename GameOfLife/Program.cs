using System;
using System.Threading;

namespace GameOfLife
{
    class Program
    {
        public static void Main()
        {
            var map = new Map();
            while (true)
            {
                Thread.Sleep(300);
                Console.Clear();
                Console.CursorVisible = false;
                for (int i = 0; i < map.Width; i++)
                {
                    for (int j = 0; j < map.Height; j++)
                    {
                        if (map.Cells[i, j])
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

    public class Map
    {
        public bool[,] Cells;

        public int Width => Cells.GetLength(0);
        public int Height => Cells.GetLength(1);

        public Map()
        {
            this.Cells = new bool[10,10];
            Cells[4, 4] = true;
            Cells[4, 5] = true;
            Cells[4, 3] = true;
            Cells[3, 3] = true;
            Cells[2, 4] = true;
        }

        public void NextTurn()
        {
            var newMap = new bool[Width, Height];
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    var count = GetCountAround(x, y);
                    if (!Cells[x, y] && count == 3)
                        newMap[x, y] = true;
                    else if (Cells[x, y] && (count == 2 || count == 3))
                        newMap[x, y] = true;
                }
            }

            Cells = newMap;
        }

        private int GetCountAround(int x, int y)
        {
            var sum = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (i == 0 && j == 0)
                        continue;
                    if (Get(x + i, y + j))
                        sum++;
                }
            }

            return sum;
        }

        private bool Get(int x, int y)
        {
            x %= Width;
            if (x < 0) x = Width + x;
            y %= Height;
            if (y < 0) y = Height + y;
            
            return Cells[x, y];
        }
    }
}