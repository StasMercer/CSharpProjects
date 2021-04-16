using Merlin2.Commands;

namespace Merlin2.Factories
{
    public interface IEffectFactory
    {
        public ICommand Create(string effectName);
    }
}