using Merlin2.Actors.Characters;
using Merlin2.Commands;
using Merlin2.Strategies;
using Merlin2d.Game;
using Merlin2d.Game.Actors;

namespace Merlin2.Actors
{
    internal class Box : AbstractCharacter, IMovable
    {
        private Animation boxAnimation = new Animation("resources/sprites/box.png", 48, 39);
        private double speed = 7;
        private ISpeedStrategy speedStrategy = new NormalSpeedStrategy();
        private Move moveRight;
        private Move moveLeft;

        public Box(string name, int x, int y) : base(name, x, y)
        {
            SetAnimation(boxAnimation);
            
            moveRight = new Move(this, speed, 1, 0);
            moveLeft = new Move(this, speed, -1, 0);
            ChangeHealth(-90);
        }



        public override void Update()
        {
            IActor intersected = GetWorld().GetActors().Find(a => a is IMovable &&
            a != this && IntersectsWithActor(a) &&
            a.IsAffectedByPhysics());
            if (intersected != null)
            {
                
                int or = intersected.GetX() + intersected.GetWidth();
                int ol = intersected.GetX();
                int r = GetX() + GetWidth();
                int l = GetX();

                int yb = GetY() + GetHeight();
                int oyt = intersected.GetY();
                int yt = GetY();
                int oyb = intersected.GetY() + intersected.GetHeight();
                
                /*Console.WriteLine($"{yb} < {oyt} {yt}>{oyb} {r}<{ol} {l}>{or}");
                Console.WriteLine($"{this.GetName()}  with {intersected.GetName()}");*/
                if (intersected.GetX() > this.GetX() && oyb > yt)
                {
                    moveRight.Execute();
                }
                if (intersected.GetX() < this.GetX() && oyb > yt)
                {
                    moveLeft.Execute();
                }
            }
            if(GetHealth() <= 0)
            {
                Die();
            }
        }
    }
}