using Merlin2.Actors;
using Merlin2.Commands;
using Merlin2.Effects;
using Merlin2.Strategies;
using Merlin2d.Game;
using System.Collections.Generic;
using System.Linq;

namespace Merlin2.Spells
{
    public class ProjectileSpell : AbstractActor, IMovable, ISpell
    {
        private IEnumerable<IAction<AbstractCharacter>> effects = null;
        private ISpeedStrategy speedStrategy = new NormalSpeedStrategy();
        private IWizard caster = null;
        private int cost = 20;
        private double speed = 4;
        private Animation animation = null;
        private Move moveRight;
        private Move moveLeft;
        private Move moveOnCast;
        private DamageDealerVisitor damageDealer = new DamageDealerVisitor(10);
        private DirectDamage<AbstractCharacter> damage = new DirectDamage<AbstractCharacter>(10);

        public ProjectileSpell(IWizard caster, IEnumerable<IAction<AbstractCharacter>> effects, Animation animation)
        {
            this.effects = effects;
            this.caster = caster;
            this.animation = animation;
            SetAnimation(animation);
            animation.Start();
            moveRight = new Move(this, GetSpeed(speed), 1, 0);
            moveLeft = new Move(this, GetSpeed(speed), -1, 0);
            SetPhysics(false);
        }

        public ISpell AddEffect(IAction<AbstractCharacter> effect)
        {
            this.effects.Append(effect);
            return (this);
        }

        public void AddEffects(IEnumerable<IAction<AbstractCharacter>> effects)
        {
            this.effects.Concat(effects);
        }

        public void Cast()
        {
            int xDelta = 0;
            if (((Player)caster).orientation == ActorOrientation.FacingRight)
            {
                moveOnCast = moveRight;
                xDelta = 64;
            }
            if (((Player)caster).orientation == ActorOrientation.FacingLeft)
            {
                moveOnCast = moveLeft;
                xDelta = -64;
                animation.FlipAnimation();
            }
            caster.GetWorld().AddActor(this);
            SetPosition(caster.GetX() + xDelta, caster.GetY());
        }

        public int GetCost()
        {
            return this.cost;
        }

        public double GetSpeed(double speed)
        {
            return speedStrategy.GetSpeed(speed);
        }

        public void SetSpeedStrategy(ISpeedStrategy speedStrategy)
        {
            this.speedStrategy = speedStrategy;
        }

        public override void Update()
        {
            int prevPosition = GetX();
            if (!this.RemovedFromWorld())
            {
                moveOnCast.Execute();
            }

            if (prevPosition == GetX() || GetWorld().IntersectWithWall(this))
            {
                //this.RemoveFromWorld();
                Move.RemoveFromStep(this);
                this.RemoveFromWorld();

                //Player p = (Player)caster;
                //p.Die();
                //caster.GetWorld().GetActors().Remove(this);
                //speedStrategy = null;
                //effects = null;
                //SetAnimation(null);
            }

            GetWorld().GetActors().ForEach(a =>
            {
                if (a != this && this.IntersectsWithActor(a))
                {
                    //Visitor pattern
                    if (a is IVisitable)
                    {
                        ((IVisitable)a).Accept(damageDealer);
                    }
                    else
                    {
                        if (a is AbstractCharacter)
                        {
                            ((AbstractCharacter)a).AddEffect(new DirectDamage<AbstractCharacter>(10));
                        }
                    }

                    Move.RemoveFromStep(this);
                    this.RemoveFromWorld();
                }
            });
        }
    }
}