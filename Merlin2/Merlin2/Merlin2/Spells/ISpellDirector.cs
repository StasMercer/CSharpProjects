namespace Merlin2.Spells
{
    public interface ISpellDirector
    {
        ISpell Build(string spellName, IWizard wizard);
    }
}