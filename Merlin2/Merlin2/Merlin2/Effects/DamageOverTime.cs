using Merlin2.Actors;
using Merlin2.Commands;
using System;

namespace Merlin2.Effects
{
    public class DamageOverTime<T> : IEffect, IAction<T> where T : AbstractCharacter
    {
        private int cost = 5;

        public void Execute(T t)
        {
            throw new NotImplementedException();
        }

        public int GetCost()
        {
            return cost;
        }
    }
}