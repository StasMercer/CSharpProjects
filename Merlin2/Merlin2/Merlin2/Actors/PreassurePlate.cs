using Merlin2d.Game;
using System.Collections.Generic;

namespace Merlin2.Actors
{
    internal class PreassurePlate : AbstractActor, IObservable
    {
        private List<IObserver> subscribers = new List<IObserver>();

        private bool pressed = false;
        private Animation animationNormal = new Animation("resources/sprites/plate_normal.png", 32, 16);
        private Animation animationPressed = new Animation("resources/sprites/plate_pressed.png", 32, 16);

        public PreassurePlate(string actorName, int x, int y) : base(actorName)
        {
            SetPosition(x, y);
            SetPhysics(false);
            SetAnimation(animationNormal);
        }

        public void Subcribe(IObserver obs)
        {
            if (!subscribers.Contains(obs))
            {
                subscribers.Add(obs);
            }
        }

        public void Unsubscribe(IObserver obs)
        {
            if (subscribers.Contains(obs))
            {
                subscribers.Remove(obs);
            }
        }

        protected override void AddedToWorld()
        {
            //GetWorld().SetWall(GetX() / 16, GetY() / 16, true);
        }

        public override void Update()
        {
            IMovable preser = GetWorld().GetActors().Find(a => a != this && a is IMovable && a.IntersectsWithActor(this)) as IMovable;
            if (preser != null)
            {
                if (!pressed)
                {
                    SetAnimation(animationPressed);
                    subscribers.ForEach(x => x.Notify());
                }
                pressed = true;
            }
            else
            {
                if (pressed)
                {
                    SetAnimation(animationNormal);
                    subscribers.ForEach(x => x.Notify());
                }
                pressed = false;
            }
        }
    }
}