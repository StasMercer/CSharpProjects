using Merlin2d.Game;
using Merlin2d.Game.Actors;
using System.Collections.Generic;
using System.Linq;

namespace Merlin2.Commands
{
    public class Gravity : IPhysics
    {
        private IWorld world;

        private Dictionary<IActor, Fall<IActor>> actDict = new Dictionary<IActor, Fall<IActor>>();

        public void Execute()
        {
            this.world.GetActors().Where(a => a.IsAffectedByPhysics())
                .ToList()
                .ForEach(a =>
                {
                    if (!actDict.ContainsKey(a))
                    {
                        actDict.Add(a, new Fall<IActor>(a));
                    }
                });
            foreach (KeyValuePair<IActor, Fall<IActor>> entry in actDict)
            {
                entry.Value.Execute(entry.Key);
            }
        }

        public void SetWorld(IWorld world)
        {
            this.world = world;
        }
    }
}