using Merlin2.Actors;
using Merlin2.Commands;
using Merlin2.Effects;
using Merlin2d.Game;
using System.Collections.Generic;
using System.Linq;

namespace Merlin2.Spells
{
    public class ProjectileSpellBuilder : ISpellBuilder
    {
        private Animation animation = null;
        private IEnumerable<IAction<AbstractCharacter>> effects = new List<IAction<AbstractCharacter>>();
        private SpellEffectFactory effectFactory = new SpellEffectFactory();
        private int spellCost = 0;

        public ISpellBuilder AddEffect(string effectName)
        {
            if (effectName == "damage")
            {
                effects.Append(new DirectDamage<AbstractCharacter>(10));
            }
            if (effectName == "dot-1")
            {
                effects.Append(new DamageOverTime<AbstractCharacter>());
            }

            return this;
        }

        public ISpell CreateSpell(IWizard caster)
        {
            return new ProjectileSpell(caster, effects, animation);
        }

        public ISpellBuilder SetAnimation(Animation animation)
        {
            this.animation = animation;
            return this;
        }

        public ISpellBuilder SetSpellCost(int cost)
        {
            this.spellCost = cost;
            return this;
        }
    }
}