using Merlin2.Actors.Characters;
using Merlin2.Commands;
using Merlin2.Effects;
using Merlin2d.Game;
using Merlin2d.Game.Enums;
using System;

namespace Merlin2.Actors
{
    public class Skeleton : AbstractCharacter, IMovable, IEnemy, IPrototype, IVisitable
    {
        private Animation animation = new Animation("resources/sprites/enemy.png", 64, 58);
        private AbstractCharacter target = null;
        private Move moveLeft;
        private Move moveRight;
        private Random random = new Random();
        private int range = 200;
        private int x = 0;
        private int stepCounter = 0;
        private int damageTime = 0;
        private Jump<AbstractCharacter> jump;

        public Skeleton(string name, int x, int y)
        {
            SetAnimation(animation);
            animation.Start();
            SetPosition(x, y);
            SetName(name);
            this.x = random.Next(-5, 5);
            jump = new Jump<AbstractCharacter>();
            moveLeft = new Move(this, 3, -1, 0);
            moveRight = new Move(this, 3, 1, 0);
        }

        public Skeleton(string name, int x, int y, AbstractCharacter target, Animation animation)
        {
            SetName(name);
            SetAnimation(animation);
            SetPosition(x, y);
            this.target = target;
            this.jump = new Jump<AbstractCharacter>();
        }

        public override void Update()
        {
            if (!RemovedFromWorld())
            {
                if (GetHealth() <= 0)
                {
                    //decorator and prototype in a row
                    GetWorld().AddActor(new SkeletonFlying(this.Clone()));
                    Die();
                }
                base.Update();
                Walk();
                Atack();
            }
        }

        public void SetTarget(AbstractCharacter actor)
        {
            this.target = actor;
        }

        public void RemoveTarget()
        {
            target = null;
        }

        //prototype pattern
        public IEnemy Clone()
        {
            return new Skeleton(GetName(), GetX(), GetY(), target, animation);
        }

        public void Atack()
        {
            int pos = target.GetX() - this.GetX();

            if (GetY() - range > target.GetY())
            {
                jump.Execute(this);
            }
            if (Math.Abs(pos) < this.GetAnimation().GetWidth() + 5 && Math.Abs(GetY() - target.GetY()) < 50 && damageTime == 0)
            {
                target.AddEffect(new DirectDamage<AbstractCharacter>(2));

                if (pos < 0)
                {
                    new Move(target, 15, -1, -1).Execute();
                }
                else
                {
                    new Move(target, 15, 1, -1).Execute();
                }
                damageTime = 30;
            }

            if (damageTime > 0) damageTime--;
        }

        public AbstractCharacter GetTarget()
        {
            return target;
        }

        public void Accept(IVisitor damageDealer)
        {
            damageDealer.VisitSkeleton(this);
        }
        protected override void AddedToWorld()
        {
            GetWorld().AddMessage(new Message("I am not what you think", 400, 30, 20, Color.Red, MessageDuration.Long));
        }
        public void Walk()
        {
            int pos = target.GetX() - this.GetX();
            int currPos = GetX();
            if (Math.Abs(pos) < range && target.GetY() + target.GetHeight() >= GetY())
            {
                if (pos < 0)
                {
                    moveLeft.Execute();
                }

                if(pos > 0)
                {
                    moveRight.Execute();
                }
            }
            else
            {
                stepCounter++;
                if (stepCounter > 20)
                {
                    x = random.Next(-5, 5);
                    stepCounter = 0;
                }
                new Move(this, 1, x, 0).Execute();
            }
            if(currPos == GetX())
            {
                jump.Execute(this);
            }
        }
    }
}