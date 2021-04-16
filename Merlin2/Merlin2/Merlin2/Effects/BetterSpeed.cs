using Merlin2.Actors;
using Merlin2.Commands;
using Merlin2.Strategies;

namespace Merlin2.Effects
{
    public class BetterSpeed<T> : IAction<T> where T : AbstractCharacter
    {
        public void Execute(T reciever)
        {
            reciever.SetSpeedStrategy(new ModifiedSpeedStrategy(1.5));
        }
    }
}