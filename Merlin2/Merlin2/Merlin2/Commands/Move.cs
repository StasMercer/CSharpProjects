using Merlin2.Actors;
using Merlin2d.Game.Actors;
using System;
using System.Collections.Generic;

namespace Merlin2.Commands
{
    public class Move : ICommand
    {
        private IActor actor = null;
        private double step;
        private int dx;
        private int dy;

        private static Dictionary<IActor, double> actStep = new Dictionary<IActor, double>();

        public Move(IMovable movable, double step, int dx, int dy)
        {
            if (movable is IActor)
            {
                actor = (IActor)movable;

                this.step = step;
                this.dx = dx;
                this.dy = dy;

                if (!actStep.ContainsKey(actor))
                {
                    Console.WriteLine(actStep.Count);
                    actStep.Add(actor, 0);
                }
            }
            else
            {
                throw new ArgumentException("movable is not abstractActor");
            }
        }

        public void SetStep(double s)
        {
            step = s;
        }

        public static void RemoveFromStep(IActor actor)
        {
            if (actStep.ContainsKey(actor))
            {
                actStep.Remove(actor);
            }
        }

        public void Execute()
        {
            double step = ((IMovable)actor).GetSpeed(this.step);
            // && actor.GetWorld().GetActors().Find(a =>a!= actor && a is IMovable && actor.IntersectsWithActor(a)) == null
            if (!actor.RemovedFromWorld())
            {
                int st = 0;
                actStep[actor] += step - (int)step;
                if (actStep[actor] >= 1)
                {
                    actor.SetPosition(actor.GetX() + (((int)step + 1) * dx), actor.GetY() + (dy * ((int)step + 1)));

                    if (actor.GetWorld().IntersectWithWall(actor))
                    {
                        actor.SetPosition(actor.GetX() - (((int)step + 1) * dx), actor.GetY() - (dy * ((int)step + 1)));
                    }
                    st = 1;
                    actStep[actor] -= 1;
                }
                else
                {
                    actor.SetPosition(actor.GetX() + (dx * (int)step), actor.GetY() + (dy * (int)step));

                    if (actor.GetWorld().IntersectWithWall(actor))
                    {
                        actor.SetPosition(actor.GetX() - (dx * (int)step), actor.GetY() - (dy * (int)step));
                    }
                }

                actor.GetWorld().GetActors().ForEach(a =>
                {
                    if (a != actor &&
                    actor.IsAffectedByPhysics() &&
                    a.IsAffectedByPhysics() &&
                    actor is IMovable &&
                    a is IMovable &&
                    actor.IntersectsWithActor(a))
                    {

                        actor.SetPosition(actor.GetX() - (dx * ((int)step + st)), actor.GetY() - (dy * ((int)step + st)));
                        a.SetPosition(a.GetX() + (dx * 5), a.GetY());
                        if (a.GetWorld().IntersectWithWall(a))
                        {
                            a.SetPosition(a.GetX() - (dx * 5), a.GetY());
                        }
                    }
                });
            }
        }
    }
}