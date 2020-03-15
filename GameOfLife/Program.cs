namespace GameOfLife
{
    public static class Program
    {
        public static void Main()
        {
            var map = new Map(10, 20);
            map.AddFigure(5, 5, Figures.Glaider);
            
            var view = new ConsoleView();
            view.Run(map);
        }
    }
}