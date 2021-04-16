using Merlin2d.Game;
using System.Collections.Generic;
using System.Linq;

namespace Merlin2.Spells
{
    public class SpellDirector : ISpellDirector
    {
        private Dictionary<string, int> effects = new Dictionary<string, int>();
        private Dictionary<string, SpellInfo> spells = new Dictionary<string, SpellInfo>();

        public SpellDirector(ISpellDataProvider provider)
        {
            spells = provider.GetSpellInfo();
            effects = provider.GetSpellEffects();
        }

        public ISpell Build(string spellName, IWizard caster)
        {
            ISpellBuilder builder;
            if (spells[spellName].SpellType == SpellType.Projectile)
            {
                builder = new ProjectileSpellBuilder();
                builder.SetAnimation(new Animation(spells[spellName].AnimationPath, spells[spellName].AnimationWidth, spells[spellName].AnimationHeight));
            }
            else
            {
                builder = new SelfCastSpellBuilder();
            }

            spells[spellName].EffectNames.ToList().ForEach(e => builder.AddEffect(e));

            return builder.CreateSpell(caster);
        }
    }
}