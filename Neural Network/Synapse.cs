using System;

namespace Neural_Network
{
    [Serializable]
    public class Synapse
    {
        public double Weight { get; set; }

        public Synapse(Random random)
        {
            Weight = random.NextDouble() * 2 - 1;
        }

        private Synapse() { }
    }
}