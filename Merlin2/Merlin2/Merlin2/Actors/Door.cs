using Merlin2d.Game;
using System;

namespace Merlin2.Actors
{
    public class Door : AbstractActor, IObserver
    {
        private Animation animationOpen = new Animation("resources/sprites/door_open.png", 48, 16);
        private Animation animationClosed = new Animation("resources/sprites/door_closed.png", 48, 16);
        private bool isOpen = false;

        public Door(string name, int x, int y) : base(name)
        {
            SetAnimation(animationClosed);
            animationClosed.Start();
            SetPosition(x, y);
            SetPhysics(false);
        }

        protected override void AddedToWorld()
        {
            Console.WriteLine("doors added");
            GetWorld().SetWall(GetX() / 16, GetY() / 16, true);
            GetWorld().SetWall((GetX() + 16) / 16, GetY() / 16, true);
            GetWorld().SetWall((GetX() + 33) / 16, GetY() / 16, true);
        }

        public void Notify()
        {
            if (!isOpen)
            {
                SetAnimation(animationOpen);
                GetWorld().SetWall(GetX() / 16, GetY() / 16, false);
                GetWorld().SetWall((GetX() + 16) / 16, GetY() / 16, false);
                GetWorld().SetWall((GetX() + 33) / 16, GetY() / 16, false);
                isOpen = true;
            }
            else
            {
                SetAnimation(animationClosed);
                GetWorld().SetWall(GetX() / 16, GetY() / 16, true);
                GetWorld().SetWall((GetX() + 16) / 16, GetY() / 16, true);
                GetWorld().SetWall((GetX() + 33) / 16, GetY() / 16, true);
                isOpen = false;
            }
        }

        public override void Update()
        {
        }
    }
}