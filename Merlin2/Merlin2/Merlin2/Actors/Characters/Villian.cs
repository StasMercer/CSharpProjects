using Merlin2d.Game;
using Merlin2d.Game.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Merlin2.Actors.Characters
{
    public class Villian : AbstractCharacter, IEnemy
    {
        private Animation animation = new Animation("resources/sprites/villian.png", 98, 214);
        private int castTime = 0;
        private Random random = new Random();
        private AbstractCharacter target;
        public Villian(string name, int x, int y) : base(name, x, y)
        {
            SetAnimation(animation);
            animation.Start();
            ChangeHealth(9900);
        }

        public void Atack()
        {
            if(castTime == 0)
            {
                Bat bat = new Bat("bat", random.Next(80, 970), random.Next(190, 600));
                bat.SetTarget(target);
                GetWorld().AddActor(bat);
                castTime = 90;
            }
            if(castTime > 0)
            {
                castTime--;
            }
        }
        public override void Update()
        {
            base.Update();
            Atack();
            if(GetHealth() <= 0)
            {
                
                Die();
            }
        }
        public AbstractCharacter GetTarget()
        {
            return target;
        }

        public void RemoveTarget()
        {
            target = null;
        }

        public void SetTarget(AbstractCharacter target)
        {
            this.target = target;
        }
        protected override void AddedToWorld()
        {
            GetWorld().AddMessage(new Message("My children can't hurt me while they are alive", 400, 30, 20, Color.Red, MessageDuration.Indefinite));
        }
        public void Walk()
        {
            throw new NotImplementedException();
        }
    }
}
