using Merlin2d.Game;

namespace Merlin2.Actors.State
{
    public class WizardState : IPlayerState
    {
        private Animation animation = new Animation("resources/sprites/player.png", 64, 58);
        private Player player = null;
        private IPlayerState defaultState;

        public WizardState(Player player, IPlayerState defaultState)
        {
            this.defaultState = defaultState;
            this.player = player;
            player.SetAnimation(animation);
            animation.Start();
        }

        public Animation GetAnimation()
        {
            return animation;
        }

        public void Update()
        {
            if (defaultState is DyingState)
            {
                return;
            }
            defaultState.Update();
        }
    }
}