using Merlin2.Actors;

namespace Merlin2.Effects
{
    public interface IVisitor
    {
        public void VisitSkeleton(Skeleton skelet);

        public void VisitPlayer(Player player);
    }
}