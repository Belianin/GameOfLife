using System.Threading;

namespace GameOfLife
{
    public interface ILifeView
    {
        void Run(Map map, CancellationToken token);
    }
}