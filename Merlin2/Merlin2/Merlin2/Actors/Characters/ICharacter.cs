using Merlin2.Commands;

namespace Merlin2.Actors
{
    public interface ICharacter : IMovable

    {
        void ChangeHealth(int delta);

        int GetHealth();

        void Die();

        void AddEffect(IAction<AbstractCharacter> effect);

        void RemoveEffect(IAction<AbstractCharacter> effect);
    }
}