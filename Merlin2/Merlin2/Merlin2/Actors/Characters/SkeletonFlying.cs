using Merlin2.Actors.Items;
using Merlin2.Commands;
using Merlin2.Effects;
using Merlin2d.Game;
using Merlin2d.Game.Actors;
using Merlin2d.Game.Enums;

namespace Merlin2.Actors.Characters
{
    internal class SkeletonFlying : AbstractFlyingEnemy
    {
        //decorator pattern
        public IEnemy baseEnemy;

        public Animation animation = new Animation("resources/sprites/flyingskull.png", 34, 29);

        public SkeletonFlying(IEnemy enemy): base()
        {

            baseEnemy = enemy;
            SetAnimation(animation);
            animation.Start();
            SetPosition(((IActor)enemy).GetX(), ((IActor)enemy).GetY());
            SetTarget(enemy.GetTarget());
            ChangeHealth(900);
        }



        public override void Update()
        {
            base.Update();
            IActor killer = GetWorld().GetActors().Find(a => a is SkullKiller);
            if (this.IntersectsWithActor(killer))
            {
                Die();
                killer.RemoveFromWorld();
            }
        }

        protected override void AddedToWorld()
        {
            GetWorld().AddMessage(new Message("Didn't wait? Lead it to the cage", 450, 30, 20, Color.Red, MessageDuration.Indefinite));
        }

       
    }
}