using Merlin2d.Game;

namespace Merlin2.Actors.Items
{
    public class SkullKiller : AbstractActor
    {
        private Animation animation = new Animation("resources/sprites/skullkiller.png", 4, 4);

        public SkullKiller(string name, int x, int y) : base(name)
        {
            SetAnimation(animation);
            SetPosition(x, y);
            SetPhysics(false);
        }
    }
}