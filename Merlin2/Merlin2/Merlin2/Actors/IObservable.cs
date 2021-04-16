namespace Merlin2.Actors
{
    public interface IObservable
    {
        public void Subcribe(IObserver obs);

        public void Unsubscribe(IObserver obs);
    }
}