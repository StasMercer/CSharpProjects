namespace Merlin2.Effects
{
    public interface IVisitable
    {
        public void Accept(IVisitor visitor);
    }
}