using Merlin2.Actors;
using Merlin2d.Game.Actors;

namespace Merlin2.Commands
{
    public class Fall<T> : IAction<T> where T : IActor
    {
        private int time = 0;
        private Move moveDown;
        private Move moveUp;

        public Fall(T t)
        {
            moveDown = new Move((IMovable)t, 1, 0, 1);
            moveUp = new Move((IMovable)t, 1, 0, -1);
        }

        public void Execute(T t)
        {
            int yPos = t.GetY();
            time++;
            moveDown.SetStep(0.5 * time);
            moveDown.Execute();
            /*IMovable touched = t.GetWorld().GetActors().Find(a => t.IntersectsWithActor(a)) as IMovable;
            if(touched != null)
            {
                moveDown.SetStep(-0.5 * time);
                moveDown.Execute();
            }*/
            if (yPos == t.GetY())
            {
                time = 0;
                moveDown.SetStep(0);
            }
        }
    }
}