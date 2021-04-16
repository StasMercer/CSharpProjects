using Merlin2.Commands;
using Merlin2.Effects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Merlin2.Actors.Characters
{
    public abstract class AbstractFlyingEnemy : AbstractCharacter, IEnemy
    {
        private AbstractCharacter target;
        private Move moveRight;
        private Move moveLeft;
        private Move moveUp;
        private Move moveDown;
        private int damageTime = 0;
        public AbstractFlyingEnemy()
        {
            SetPhysics(false);
            moveRight = new Move(this, 1, 1, 0);
            moveLeft = new Move(this, 1, -1, 0);
            moveDown = new Move(this, 1, 0, 1);
            moveUp = new Move(this, 1, 0, -1);
        }

        public AbstractFlyingEnemy(string name, int x, int y): base(name, x, y)
        {
            
            SetPhysics(false);
            moveRight = new Move(this, 1, 1, 0);
            moveLeft = new Move(this, 1, -1, 0);
            moveDown = new Move(this, 1, 0, 1);
            moveUp = new Move(this, 1, 0, -1);
        }

        public virtual void Atack()
        {
            if (this.IntersectsWithActor(target) && damageTime == 0)
            {
                target.AddEffect(new DirectDamage<AbstractCharacter>(5));
                damageTime = 30;
            }
            if (damageTime > 0) damageTime--;
        }
        public virtual void Walk()
        {
            int posY = GetY() - target.GetY();
            int posX = GetX() - target.GetX();
            if (posY < 0)
            {
                moveDown.Execute();
            }

            if (posY > 0)
            {
                moveUp.Execute();
            }

            if (posX < 0)
            {
                moveRight.Execute();
            }
            if (posX > 0)
            {
                moveLeft.Execute();
            }
        }
        public override void Update()
        {
            base.Update();
            Walk();
            Atack();
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
    }
}
