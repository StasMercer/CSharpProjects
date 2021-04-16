using Merlin2.Commands;
using Merlin2.Effects;
using Merlin2d.Game;
using System;
using System.Collections.Generic;
using System.Text;

namespace Merlin2.Actors.Characters
{
    public class Bat : AbstractFlyingEnemy
    {
        private Animation animation = new Animation("resources/sprites/bat.png", 16, 11);


        public Bat(string name, int x, int y): base(name, x, y)
        {
            SetAnimation(animation);
            animation.Start();
            
            ChangeHealth(-90);
        }

        public override void Update()
        {
            base.Update();
            if (this.IntersectsWithActor(GetTarget()))
            {
                Die();
            }
            if(GetHealth() <= 0)
            {
                GetWorld().AddActor(new DeadBat(GetX(), GetY()));
                Die();
            }
        }
    }
}
