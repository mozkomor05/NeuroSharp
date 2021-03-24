using System;
using System.Collections.Generic;

namespace Neural_Network
{
    [Serializable]
    public class Neuron
    {
        public List<Synapse> Synapses { get; set; }
        public double Value { get; set; }
        public double Bias { get; set; } = 0; //Práh
        public double Delta { get; set; } //Chybový faktor (cost)

        public Neuron()
        {
            Synapses = new List<Synapse>();
        }
    }
}