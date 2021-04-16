namespace Merlin2.Strategies
{
    public class ModifiedSpeedStrategy : ISpeedStrategy
    {
        private double multiplier = 1;

        public ModifiedSpeedStrategy(double multiplier)
        {
            this.multiplier = multiplier;
        }

        public double GetSpeed(double speed)
        {
            return speed * multiplier;
        }
    }
}