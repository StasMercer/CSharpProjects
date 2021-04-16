using Merlin2d.Game;

namespace Merlin2.Actors.State
{
    public interface IPlayerState
    {
        void Update();

        Animation GetAnimation();
    }
}