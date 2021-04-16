using Merlin2.Actors;
using Merlin2.Commands;
using System;

namespace Merlin2.Effects
{
    public class DirectDamage<T> : IEffect, IAction<T> where T : AbstractCharacter
    {
        private int damage = 10;

        public DirectDamage(int dmg)
        {
            this.damage = dmg;
        }

        public void Execute(T t)
        {
            t.ChangeHealth(-damage);
        }

        public int GetCost()
        {
            throw new NotImplementedException();
        }
    }
}