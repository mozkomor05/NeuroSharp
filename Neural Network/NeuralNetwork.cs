using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Neural_Network
{
    [Serializable]
    public class NeuralNetwork
    {
        public List<Layer> Layers { get; set; }
        public double LearningRate { get; set; } //koeficient učení, vysoký = rychlost, nízký = kvalita

        /// <summary>
        /// Creates covolutional neural network with fully connected layers.
        /// </summary>
        /// <param name="learningRate">Learning rate. It is important to find optimal value (almost everytime below one).</param>
        /// <param name="inputNeurons">Number od neurons in input layer.</param>
        /// <param name="layers">Add any number of layers. The last one is always output layer and the number of neurons in the output layer indicates number of outputs.</param>
        public NeuralNetwork(double learningRate, int inputNeurons, params Layer[] layers)
        {
            layers = layers.Where(c => c != null).ToArray();

            if (layers.Length < 1) return; //nemožné mít míň než dvě vrsty (vstupní + výstupní)

            LearningRate = learningRate;
            Layers = new List<Layer>(layers);
            Layers.Insert(0, new Layer(inputNeurons));

            Random random = new Random();

            for (int i = 1; i < Layers.Count; i++)
                Layers[i].Neurons.ForEach(neuron => Layers[i - 1].Neurons.ForEach(x => neuron.Synapses.Add(new Synapse(random))));
        }

        private NeuralNetwork() { }

        public static NeuralNetwork ImportData(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(NeuralNetwork));

            using (StreamReader reader = new StreamReader(path))
                return (NeuralNetwork)serializer.Deserialize(reader);
        }

        public void ExportData(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(NeuralNetwork));

            using (StringWriter sw = new StringWriter())
            {
                serializer.Serialize(sw, this);
                using (StreamWriter stream = new StreamWriter(path))
                    stream.Write(sw.ToString());
            }
        }

        public double[] FeedForward(params double[] input)
        {
            if (input.Length != Layers[0].Neurons.Count) return null; //Opět nemožné, nastaveny musí být všechny neurony

            for (int i = 0; i < Layers[0].Neurons.Count; i++)
                Layers[0].Neurons[i].Value = input[i];

            for (int i = 1; i < Layers.Count; i++)
            {
                Layers[i].Neurons.ForEach(neuron =>
                {
                    double sum = neuron.Bias;

                    for (int j = 0; j < Layers[i - 1].Neurons.Count; j++)
                        sum += Layers[i - 1].Neurons[j].Value * neuron.Synapses[j].Weight;

                    neuron.Value = Layers[i].Function.Value(sum);
                });
            }

            return Layers.Last().Neurons.Select(neuron => neuron.Value).ToArray();
        }

        /* BackPropagation algoritmus
         * Algoritmus je flexibilní pro různé aktivační funkce. Jediné co se předpokládá je, že jako ztrátová funkce
         * se používá střední kvadratická chyba (MSE). E = 1/2 suma (hodnota - očekávaná hodnota)^2
         */
        public void BackPropagation(double[] input, double[] expOutput)
        {
            if ((input.Length != Layers[0].Neurons.Count) || (expOutput.Length != Layers.Last().Neurons.Count)) return; //opět, vstupní hodnoty a výstpní musí sedět s neurony

            double[] actualOutput = FeedForward(input);

            for (int i = Layers.Count - 1; i >= 1; i--)
                for (int j = 0; j < Layers[i].Neurons.Count; j++)
                {
                    Neuron neuron = Layers[i].Neurons[j];
                    double sum = neuron.Bias;

                    for (int k = 0; k < Layers[i - 1].Neurons.Count; k++)
                        sum += Layers[i - 1].Neurons[k].Value * neuron.Synapses[k].Weight;

                    neuron.Delta = Layers.Last().Function.Derivative(sum);

                    if (i != Layers.Count - 1)
                    {
                        double costSum = 0;
                        Layers[i + 1].Neurons.ForEach(n => costSum += n.Delta * n.Synapses[j].Weight);

                        neuron.Delta *= costSum;
                    }
                    else
                        neuron.Delta *= expOutput[j] - actualOutput[j];
                }

            for (int i = Layers.Count - 1; i >= 1; i--)
                Layers[i].Neurons.ForEach(neuron =>
                {
                    neuron.Bias += LearningRate * neuron.Delta;

                    for (int j = 0; j < neuron.Synapses.Count; j++)
                        neuron.Synapses[j].Weight += LearningRate * neuron.Delta * Layers[i - 1].Neurons[j].Value;
                });
        }

        /* Celková chyba chyba neuronové sítě se počítá stejně, jako je předpokládáno v backpropagation algoritmu.
         */
        public void CalculateError(double[] input, double[] expOutput)
        {
            if ((input.Length != Layers[0].Neurons.Count) || (expOutput.Length != Layers.Last().Neurons.Count)) return;

            double[] actualOutput = FeedForward(input.ToArray());
        }
    }
}
