using System.Collections.Generic;

namespace Merlin2.Spells
{
    public interface ISpellDataProvider
    {
        public Dictionary<string, SpellInfo> GetSpellInfo();

        public Dictionary<string, int> GetSpellEffects();
    }
}