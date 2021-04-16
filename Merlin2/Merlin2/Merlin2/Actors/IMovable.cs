using Merlin2.Strategies;

namespace Merlin2.Actors
{
    public interface IMovable
    {
        void SetSpeedStrategy(ISpeedStrategy speedStrategy);

        double GetSpeed(double speed);
    }
}