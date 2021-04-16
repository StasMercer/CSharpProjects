using Merlin2.Actors;

namespace Merlin2.Effects
{
    internal class DamageDealerVisitor : IVisitor
    {
        private int delta = 10;

        public DamageDealerVisitor(int delta)
        {
            this.delta = delta;
        }

        public void VisitPlayer(Player player)
        {
            player.ChangeHealth(-delta);
        }

        public void VisitSkeleton(Skeleton skelet)
        {
            skelet.ChangeHealth(-delta);
        }
    }
}