using Merlin2.Actors;
using Merlin2.Commands;
using Merlin2.Strategies;

namespace Merlin2.Effects
{
    public class BetterJump<T> : IAction<T> where T : AbstractCharacter
    {
        private int healPoints = 10;

        public void Execute(T reciever)
        {
            reciever.SetJumpSpeedStrategy(new ModifiedSpeedStrategy(1.3));
        }
    }
}