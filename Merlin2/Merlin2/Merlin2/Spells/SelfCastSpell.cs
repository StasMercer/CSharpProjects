using Merlin2.Actors;
using Merlin2.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Merlin2.Spells
{
    public class SelfCastSpell : ISpell
    {
        private IEnumerable<IAction<AbstractCharacter>> effects = null;

        public SelfCastSpell(IWizard caster, IEnumerable<IAction<AbstractCharacter>> effects)
        {
            this.effects = effects;
        }

        public ISpell AddEffect(IAction<AbstractCharacter> effect)
        {
            this.effects.Append(effect);
            return this;
        }

        public void AddEffects(IEnumerable<IAction<AbstractCharacter>> effects)
        {
            this.effects.Concat(effects);
        }

        public void Cast()
        {
            throw new NotImplementedException();
        }

        public int GetCost()
        {
            throw new NotImplementedException();
        }
    }
}