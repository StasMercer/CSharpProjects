using Merlin2d.Game;
using Merlin2d.Game.Actors;

namespace Merlin2.Actors
{
    public abstract class AbstractActor : IActor
    {
        private string name;
        private int x;
        private int y;
        private Animation animation;
        private IWorld world;
        private bool physics = true;
        private bool shouldRemove = false;

        public AbstractActor()
        {
            name = "";
        }

        public AbstractActor(string name)
        {
            this.name = name;
        }

        public virtual Animation GetAnimation()
        {
            return animation;
        }

        public int GetHeight()
        {
            return animation.GetHeight();
        }

        public string GetName()
        {
            return name;
        }

        public int GetWidth()
        {
            return animation.GetWidth();
        }

        public IWorld GetWorld()
        {
            return world;
        }

        public int GetX()
        {
            return x;
        }

        public int GetY()
        {
            return y;
        }

        public bool IntersectsWithActor(IActor other)
        {
            int or = other.GetX() + other.GetWidth();
            int ol = other.GetX();
            int r = GetX() + GetWidth();
            int l = GetX();

            int yb = GetY() + GetHeight();
            int oyt = other.GetY();
            int yt = GetY();
            int oyb = other.GetY() + other.GetHeight();
            if (yb < oyt || yt > oyb || r < ol || l > or)
            {
                return false;
            }
            return true;
        }

        public bool IsAffectedByPhysics()
        {
            return physics;
        }

        public void OnAddedToWorld(IWorld world)
        {
            this.world = world;
            AddedToWorld();
        }

        protected virtual void AddedToWorld()
        {
        }

        public bool RemovedFromWorld()
        {
            return shouldRemove;
        }

        public void RemoveFromWorld()
        {
            shouldRemove = true;
        }

        public void SetAnimation(Animation animation)
        {
            this.animation = animation;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public void SetPhysics(bool isPhysicsEnabled)
        {
            this.physics = isPhysicsEnabled;
        }

        public void SetPosition(int posX, int posY)
        {
            x = posX;
            y = posY;
        }

        public virtual void Update()
        {
        }
    }
}