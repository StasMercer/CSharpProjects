using Merlin2.Commands;
using Merlin2d.Game;

namespace Merlin2.Actors.State
{
    public class LivingState : IPlayerState
    {
        private double step = 3.9;
        private bool fliped = false;

        private Jump<AbstractCharacter> jump;

        private Move moveLeft;
        private Move moveRight;
        private Move moveUp;
        private Animation animation = new Animation("resources/sprites/player.png", 28, 47);
        private AbstractCharacter player = null;

        public LivingState(AbstractCharacter player)
        {
            this.player = player;
            player.SetAnimation(animation);
            animation.Start();

            //And here is refactor question, how can i handle the change of speedStrategy
            //for move when every move gets its speed step only once during inititalization down bellow
            //Definitly I cannot create new Move every time in update
            //So if I would change move from ICommand to IAction it can solve the problem but I break rules of
            //task though..., And maybe there is something i miss
            moveLeft = new Move(player, player.GetSpeed(step), -1, 0);
            moveRight = new Move(player, player.GetSpeed(step), 1, 0);
            moveUp = new Move(player, 7, 0, -1);
            jump = new Jump<AbstractCharacter>();
        }

        public Animation GetAnimation()
        {
            return animation;
        }

        public void Update()
        {
            animation.Stop();

            if (Input.GetInstance().IsKeyDown(Input.Key.RIGHT))
            {
                animation.Start();
                if (fliped)
                {
                    animation.FlipAnimation();
                    fliped = false;
                    player.orientation = ActorOrientation.FacingRight;
                }

                moveRight.Execute();
            }

            if (Input.GetInstance().IsKeyDown(Input.Key.LEFT))
            {
                animation.Start();
                if (!fliped)
                {
                    player.orientation = ActorOrientation.FacingLeft;
                    animation.FlipAnimation();
                    fliped = true;
                }

                moveLeft.Execute();
            }

            if (Input.GetInstance().IsKeyDown(Input.Key.SPACE))
            {
                jump.Execute(player);
            }
        }
    }
}