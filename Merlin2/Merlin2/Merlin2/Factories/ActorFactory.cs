using Merlin2.Actors;
using Merlin2.Actors.Characters;
using Merlin2.Actors.Items;
using Merlin2d.Game;
using Merlin2d.Game.Actors;

namespace Merlin2.Factories
{
    public class ActorFactory : IFactory
    {
        public IActor Create(string actorType, string actorName, int x, int y)
        {
            if (actorType == "Player")
            {
                return new Player(actorName, x, y);
            }
            if (actorType == "Skeleton")
            {
                return new Skeleton(actorName, x, y);
            }
            if (actorType == "Door")
            {
                return new Door(actorName, x, y);
            }
            if (actorType == "Switch")
            {
                return new Switch(actorName, x, y);
            }
            if (actorType == "PreasurePlate")
            {
                return new PreassurePlate(actorName, x, y);
            }
            if (actorType == "Box")
            {
                return new Box(actorName, x, y);
            }
            if (actorType == "HealingPotion")
            {
                return new HealingPotion(actorName, x, y);
            }
            if (actorType == "SpeedPotion")
            {
                return new SpeedPotion(actorName, x, y);
            }
            if(actorType == "JumpPotion")
            {
                return new JumpPotion(actorName, x, y);
            }
            if (actorType == "SkullKiller")
            {
                return new SkullKiller(actorName, x, y);
            }
            if(actorType == "Villian")
            {
                return new Villian(actorName, x, y);
            }
            if(actorType == "Bat")
            {
                return new Bat(actorName, x, y);
            }
            if(actorType == "Bull")
            {
                return new Bull(actorName, x, y);
            }
            return null;
        }
    }
}