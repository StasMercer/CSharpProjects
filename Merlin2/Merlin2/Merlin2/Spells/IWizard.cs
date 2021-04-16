using Merlin2d.Game.Actors;

namespace Merlin2.Spells
{
    public interface IWizard : IActor
    {
        void ChangeMana(int delta);

        int GetMana();

        void Cast(ISpell spell);

        bool IsCasting();

        void ChangeCasting(bool cast);
    }
}