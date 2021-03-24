using System;
using System.Xml.Serialization;

namespace Neural_Network.Functions
{
    [XmlInclude(typeof(Identity))]
    [XmlInclude(typeof(ReLU))]
    [XmlInclude(typeof(Sigmoid))]
    [XmlInclude(typeof(TanH))]
    [Serializable]
    public abstract class Function
    {
        public abstract double Value(double y); // funkční hodnota
        public abstract double Derivative(double y); // první derivace
    }
}
