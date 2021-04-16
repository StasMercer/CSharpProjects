using Merlin2.Actors;
using Merlin2.Commands;
using System.Collections.Generic;

namespace Merlin2.Spells
{
    public interface ISpell
    {
        ISpell AddEffect(IAction<AbstractCharacter> effect);

        void AddEffects(IEnumerable<IAction<AbstractCharacter>> effects);

        int GetCost();

        void Cast();
    }
}