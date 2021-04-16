namespace Merlin2.Strategies
{
    internal class ModifiedJumpStrategy
    {
        private double multiplier = 1;

        public ModifiedJumpStrategy(double multiplier)
        {
            this.multiplier = multiplier;
        }

        public double GetSpeed(double speed)
        {
            return speed * multiplier;
        }
    }
}