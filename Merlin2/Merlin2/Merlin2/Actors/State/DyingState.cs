using Merlin2d.Game;

namespace Merlin2.Actors.State
{
    internal class DyingState : IPlayerState
    {
        private AbstractCharacter player;
        private Animation animation = new Animation("resources/sprites/die.png", 64, 58);

        public DyingState(AbstractCharacter player)
        {
            this.player = player;
            player.SetAnimation(animation);
        }

        public Animation GetAnimation()
        {
            return animation;
        }

        public void Update()
        {
        }
    }
}