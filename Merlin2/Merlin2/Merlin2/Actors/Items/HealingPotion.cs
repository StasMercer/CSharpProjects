using Merlin2.Commands;
using Merlin2.Effects;
using Merlin2d.Game;
using Merlin2d.Game.Actors;
using Merlin2d.Game.Items;

namespace Merlin2.Actors.Items
{
    public class HealingPotion : AbstractActor, IItem, IUsable
    {
        private Animation animation = new Animation("resources/sprites/healingpotion.png", 16, 16);
        private Animation animationEmpty = new Animation("resources/sprites/healingpotion_empty.png", 16, 16);
        private IAction<AbstractCharacter> healEffect = new Heal<AbstractCharacter>();

        public HealingPotion()
        {
            SetAnimation(animation);
            animation.Start();
            SetPhysics(false);
        }

        public HealingPotion(string name, int x, int y) : base(name)
        {
            SetPosition(x, y);
            SetAnimation(animation);
            animation.Start();
            SetPhysics(false);
        }

        public void Use(IActor actor)
        {
            if (GetAnimation() != animationEmpty)
            {
                ((Player)actor).AddEffect(healEffect);
                SetAnimation(animationEmpty);
            }
        }
    }
}