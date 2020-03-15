namespace GameOfLife
{
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
}