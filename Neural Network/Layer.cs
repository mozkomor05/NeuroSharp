
using Neural_Network.Functions;
using System;
using System.Collections.Generic;

namespace Neural_Network
{
    [Serializable]
    public class Layer
    {
        public List<Neuron> Neurons { get; set; }
        public Function Function { get; set; }

        public Layer(int count, Function function)
        {
            Neurons = new List<Neuron>();
            Function = function;

            for (int i = 0; i < count; i++)
                Neurons.Add(new Neuron());
        }

        public Layer(int count) : this(count, new Sigmoid()) { }

        private Layer() { }
    }
}
