using Merlin2.Actors;
using Merlin2.Commands;

namespace Merlin2.Effects
{
    public class Heal<T> : IAction<T> where T : AbstractCharacter
    {
        private int healPoints = 10;

        public void Execute(T reciever)
        {
            reciever.ChangeHealth(healPoints);
        }
    }
}