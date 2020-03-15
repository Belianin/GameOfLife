using System.Threading;

namespace GameOfLife
{
    public interface ILifeView
    {
        void Run(Game game, CancellationToken token);
    }
}