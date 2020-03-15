namespace GameOfLife
{
    public class Map
    {
        private bool[,] cells;

        public int Width => cells.GetLength(0);
        public int Height => cells.GetLength(1);

        public Map(int width, int height)
        {
            this.cells = new bool[width, height];
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
                    else if (count == 2 && cells[x, y])
                        newMap[x, y] = true;
                }
            }

            cells = newMap;
        }

        public void AddFigure(int x, int y, bool[,] figure)
        {
            for (int i = 0; i < figure.GetLength(0); i++)
            {
                for (int j = 0; j < figure.GetLength(1); j++)
                {
                    this[x + i, y + j] = figure[i, j];
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
                    if (this[x + i, y + j])
                        sum++;
                }
            }

            if (this[x, y])
                sum--;

            return sum;
        }

        public bool this[int x, int y]
        {
            get
            {
                x %= Width;
                if (x < 0) x = Width + x;
                y %= Height;
                if (y < 0) y = Height + y;
            
                return cells[x, y];
            }
            set
            {
                x %= Width;
                if (x < 0) x = Width + x;
                y %= Height;
                if (y < 0) y = Height + y;

                cells[x, y] = value;
            }
        }
    }
}