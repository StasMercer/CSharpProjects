using Merlin2d.Game;
using Merlin2d.Game.Actors;
using System;
using System.Collections.Generic;

namespace Merlin2.Actors
{
    public class Switch : AbstractActor, IObservable, IUsable
    {
        private List<IObserver> subscribers = new List<IObserver>();

        private Animation animationUp = new Animation("resources/sprites/switch_up.png", 32, 32);
        private Animation animationDown = new Animation("resources/sprites/switch_down.png", 32, 32);

        public Switch(string name, int x, int y) : base(name)
        {
            SetAnimation(animationUp);
            animationUp.Start();
            SetPosition(x, y);
            SetPhysics(false);
        }

        protected override void AddedToWorld()
        {
            Console.WriteLine("switch adedd");
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

        public void Use(IActor actor)
        {
            subscribers.ForEach(s => s.Notify());
            if (GetAnimation() == animationUp)
            {
                SetAnimation(animationDown);
            }
            else
            {
                SetAnimation(animationUp);
            }
        }

        public override void Update()
        {
        }
    }
}