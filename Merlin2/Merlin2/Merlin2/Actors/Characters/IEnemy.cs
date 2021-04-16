namespace Merlin2.Actors.Characters
{
    //decorator pattern
    public interface IEnemy
    {
        public void Atack();

        public void SetTarget(AbstractCharacter target);

        public void RemoveTarget();

        public AbstractCharacter GetTarget();

        public void Walk();
    }
}