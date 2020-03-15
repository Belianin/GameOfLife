using System;
using System.Threading;

namespace GameOfLife
{
    class Program
    {
        public static void Main()
        {
            var map = new Map(10, 20);
            map.AddFigure(5, 5, Figures.Glaider);
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

    public static class Figures
    {
        public static bool[,] HorizontalBlinker
        {
            get
            {
                var result = new bool[3, 1];
                result[0, 0] = true;
                result[0, 1] = true;
                result[0, 2] = true;
                return result;
            }
        }

        public static bool[,] Glaider
        {
            get
            {
                var result = new bool[3, 3];
                result[2, 0] = true;
                result[2, 1] = true;
                result[2, 2] = true;
                result[1, 2] = true;
                result[0, 1] = true;
                return result;
            }
        }
    }

    public class Map
    {
        public bool[,] Cells;

        public int Width => Cells.GetLength(0);
        public int Height => Cells.GetLength(1);

        public Map(int width, int height)
        {
            this.Cells = new bool[width, height];
        }

        public void NextTurn()
        {
            var newMap = new bool[Width, Height];
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    var count = GetCountAround(x, y);
                    if (count == 3)
                        newMap[x, y] = true;
                    else if (count == 2 && Cells[x, y])
                        newMap[x, y] = true;
                }
            }

            Cells = newMap;
        }

        public void AddFigure(int x, int y, bool[,] figure)
        {
            for (int i = 0; i < figure.GetLength(0); i++)
            {
                for (int j = 0; j < figure.GetLength(1); j++)
                {
                    Set(x + i, y + j, figure[i, j]);
                }
            }
        }

        private int GetCountAround(int x, int y)
        {
            var sum = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (IsAlive(x + i, y + j))
                        sum++;
                }
            }

            if (IsAlive(x, y))
                sum--;

            return sum;
        }

        private bool IsAlive(int x, int y)
        {
            x %= Width;
            if (x < 0) x = Width + x;
            y %= Height;
            if (y < 0) y = Height + y;
            
            return Cells[x, y];
        }

        private void Set(int x, int y, bool value)
        {
            x %= Width;
            if (x < 0) x = Width + x;
            y %= Height;
            if (y < 0) y = Height + y;

            Cells[x, y] = value;
        }
    }
}