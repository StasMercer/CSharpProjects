using Merlin2.Commands;
using Merlin2.Strategies;
using Merlin2d.Game;
using System.Collections.Generic;

namespace Merlin2.Actors
{
    public abstract class AbstractCharacter : AbstractActor, ICharacter
    {
        private List<IAction<AbstractCharacter>> effects = new List<IAction<AbstractCharacter>>();
        private ISpeedStrategy speedStrategy = new NormalSpeedStrategy();
        private ISpeedStrategy jumpSpeedStrategy = new NormalSpeedStrategy();
        private int health = 100;
        private int maxHealth = 100;

        public ActorOrientation orientation = ActorOrientation.FacingRight;
        private Message msg;

        public AbstractCharacter() : base()
        {
        }

        public AbstractCharacter(string name, int x, int y) : base(name)
        {
            SetName(name);
            SetPosition(x, y);
        }

        public void AddEffect(IAction<AbstractCharacter> effect)
        {
            if (effects.Contains(effect))
            {
                return;
            }
            effects.Add(effect);
        }

        public void ChangeHealth(int delta)
        {
            health += delta;
        }

        public void Die()
        {
            GetWorld().RemoveMessage(msg);
            RemoveFromWorld();
        }

        public double GetJumpSpeed(double speed)
        {
            return jumpSpeedStrategy.GetSpeed(speed);
        }

        public int GetHealth()
        {
            return health;
        }

        public double GetSpeed(double speed)
        {
            return speedStrategy.GetSpeed(speed);
        }

        public void RemoveEffect(IAction<AbstractCharacter> effect)
        {
            effects.Remove(effect);
        }

        public override void Update()
        {
            if (!RemovedFromWorld())
            {
                GetWorld().RemoveMessage(msg);
                msg = new Message(GetHealth().ToString(), GetX(), GetY() - 20);
                GetWorld().AddMessage(msg);

                if (effects.Count > 0)
                {
                    effects.ForEach(e => e.Execute(this));
                    effects.Clear();
                }
            }
        }

        public void SetJumpSpeedStrategy(ISpeedStrategy speedStrategy)
        {
            this.jumpSpeedStrategy = speedStrategy;
        }

        public void SetSpeedStrategy(ISpeedStrategy speedStrategy)
        {
            this.speedStrategy = speedStrategy;
        }
    }
}