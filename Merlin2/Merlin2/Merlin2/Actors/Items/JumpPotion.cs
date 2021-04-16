﻿using Merlin2.Commands;
using Merlin2.Effects;
using Merlin2d.Game;
using Merlin2d.Game.Actors;
using Merlin2d.Game.Items;

namespace Merlin2.Actors.Items
{
    public class JumpPotion : AbstractActor, IItem, IUsable
    {
        private Animation animation = new Animation("resources/sprites/jumppotion.png", 16, 16);
        private Animation animationEmpty = new Animation("resources/sprites/healingpotion_empty.png", 16, 16);
        private IAction<AbstractCharacter> jumpEffect = new BetterJump<AbstractCharacter>();

        public JumpPotion()
        {
            SetAnimation(animation);
            animation.Start();
            SetPhysics(false);
        }

        public JumpPotion(string name, int x, int y) : base(name)
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
                ((Player)actor).AddEffect(jumpEffect);
                SetAnimation(animationEmpty);
            }
        }
    }
}