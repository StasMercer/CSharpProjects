namespace Merlin2.Commands
{
    public interface IAction<T>
    {
        void Execute(T t);
    }
}