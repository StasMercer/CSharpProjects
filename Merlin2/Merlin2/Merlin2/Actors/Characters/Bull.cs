using Merlin2.Commands;
using Merlin2.Effects;
using Merlin2d.Game;
using Merlin2d.Game.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Merlin2.Actors.Characters
{
    public class Bull : AbstractCharacter, IEnemy
    {
        private AbstractCharacter target;
        private Animation animationAtack = new Animation("resources/sprites/bullatack.png", 130, 80);
        private Animation animationWalk = new Animation("resources/sprites/bullwalk.png", 100, 78);
        private int damageTime = 0;
        private bool fliped = false;
        private Random random = new Random();
        private int randomTime = 0;
        private int randomIndex = 0;
        private List<Move> moves = new List<Move>();
        public Bull(string name, int x, int y): base(name, x, y)
        {
            SetAnimation(animationWalk);
            animationWalk.Start();
            animationAtack.Start();
            for(int i = 0; i < 10; i++)
            {
                moves.Add(new Move(this, 1, random.Next(-7, 7), 0));
            }
            ChangeHealth(300);
        }
        public void Atack()
        {
            int pos = target.GetX() - this.GetX();

           
            if (Math.Abs(pos) < this.GetAnimation().GetWidth() + 5 && Math.Abs(GetY() - target.GetY()) < 50 && damageTime == 0)
            {
                SetAnimation(animationAtack);
                target.AddEffect(new DirectDamage<AbstractCharacter>(5));

                if (pos < 0)
                {
                    new Move(target, 15, -1, -1).Execute();
                }
                else
                {
                    new Move(target, 15, 1, -1).Execute();
                }
                damageTime = 20;
            }

            if (damageTime > 0) damageTime--;
        }
        public override void Update()
        {
            base.Update();
            Walk();
            Atack();
            GetWorld().GetActors().ForEach(a =>
            {
                Box box = a as Box;
                if (box != null)
                {
                    int or = box.GetX() + box.GetWidth();
                    int ol = box.GetX();
                    int r = GetX() + GetWidth();
                    int l = GetX();

                    int yb = GetY() + GetHeight();
                    int oyt = box.GetY();
                    int yt = GetY();
                    int oyb = box.GetY() + box.GetHeight();

                    int range = 5;
                    int posY = Math.Abs(oyb - yt);
                    int posCenterX = Math.Abs((r+l)/2 - (or+ol)/2);
                    if(posY < range && oyb - yt < 0 && (posCenterX < GetWidth() + 2)) // box is above bull's head 
                    {
                        this.ChangeHealth(-100);
                        box.Die();
                    }else if((oyt > yt ) && ( posCenterX < this.GetWidth()/2 + box.GetWidth()/2 + 3)) // box is lower than bull and in his range
                    {
                        box.Die();
                    }
                }
            });

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

        public void Walk()
        {
            if(randomTime == 0)
            {
                randomIndex = random.Next(moves.Count);
                randomTime = 150;
                
            }

            int pos = target.GetX() - this.GetX();
            if(pos < 0 && !fliped)
            {
                animationWalk.FlipAnimation();
                fliped = true;
            }
            if(pos > 0 && fliped)
            {
                animationWalk.FlipAnimation();
                fliped = false;
            }
            if (damageTime == 0)
            {
                SetAnimation(animationWalk);
                
            }
            if(randomTime > 0)
            {
                randomTime--;
            }
            int curPosX = GetX();
            moves[randomIndex].Execute();
            if (curPosX == GetX())
            {
                randomTime = 0;
            }
        }
        protected override void AddedToWorld()
        {
            GetWorld().AddMessage(new Message("I DONT LIKE WOOOOD", 400, 30, 20, Color.Red, MessageDuration.Indefinite));
        }
    }
}
