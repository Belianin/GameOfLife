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
                Thread.Sleep(1000);
                Console.Clear();
                for (int i = 0; i < map.Cells.GetLength(0); i++)
                {
                    for (int j = 0; j < map.Cells.GetLength(1); j++)
                    {
                        if (map.Cells[i, j])
                            Console.Write("#");
                        else
                        {
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

        public Map()
        {
            this.Cells = new bool[10,10];
            Cells[4, 4] = true;
            Cells[4, 5] = true;
            Cells[4, 3] = true;
        }

        public void NextTurn()
        {
            var newMap = new bool[Cells.GetLength(0), Cells.GetLength(1)];
            for (int x = 0; x < Cells.GetLength(0); x++)
            {
                for (int y = 0; y < Cells.GetLength(1); y++)
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
            while (x < 0)
            {
                x = Cells.GetLength(0) - x;
            }

            while (y < 0)
            {
                y = Cells.GetLength(1) - y;
            }
            return Cells[x % Cells.GetLength(0), y % Cells.GetLength(1)];
        }
    }
}