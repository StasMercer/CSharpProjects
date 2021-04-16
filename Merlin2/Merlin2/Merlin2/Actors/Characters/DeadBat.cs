using Merlin2.Effects;
using Merlin2.Strategies;
using Merlin2d.Game;
using Merlin2d.Game.Actors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Merlin2.Actors.Characters
{
    public class DeadBat : AbstractActor, IMovable
    {
        private ISpeedStrategy speedStrategy = new NormalSpeedStrategy();

        private Animation animation = new Animation("resources/sprites/deadbat.png", 16, 12);
        private int lifeTime = 490; //7 seconds
        public DeadBat(int x, int y)
        {
            SetPosition(x, y);
            SetAnimation(animation);
            animation.Start();
        }

        public double GetSpeed(double speed)
        {
            return speedStrategy.GetSpeed(speed);
        }
        public override void Update()
        {
            base.Update();
            IActor villian = GetWorld().GetActors().Find(a => a is Villian);
            if(lifeTime > 0)
            {
                lifeTime--;
            }
            else
            {
                RemoveFromWorld();
            }
            
            if(villian != null && this.IntersectsWithActor(villian))
            {
                ((AbstractCharacter)villian).AddEffect(new DirectDamage<AbstractCharacter>(1000));
                RemoveFromWorld();
            }
        }
        public void SetSpeedStrategy(ISpeedStrategy speedStrategy)
        {
            this.speedStrategy = speedStrategy;
        }
    }
}
