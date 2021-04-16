using Merlin2.Actors;

namespace Merlin2.Commands
{
    public class Jump<T> : IAction<T> where T : AbstractCharacter
    {
        private int jumpSpeed = 12;

        public void Execute(T t)
        {
            t.SetPosition(t.GetX(), t.GetY() - (int)t.GetJumpSpeed(jumpSpeed));
            if (t.GetWorld().IntersectWithWall(t))
            {
                t.SetPosition(t.GetX(), t.GetY() + (int)t.GetJumpSpeed(jumpSpeed));
            }
            t.GetWorld().GetActors().ForEach(a =>
            {
                if (a != t &&
                t.IsAffectedByPhysics() &&
                a.IsAffectedByPhysics() &&
                t is IMovable &&
                a is IMovable &&
                t.IntersectsWithActor(a))
                {
                    t.SetPosition(t.GetX(), t.GetY() + (int)t.GetJumpSpeed(jumpSpeed));
                    a.SetPosition(a.GetX(), a.GetY() - 5);

                    if (a.GetWorld().IntersectWithWall(a))
                    {
                        a.SetPosition(a.GetX(), a.GetY() + 5);
                    }
                }
            });
        }
    }
}