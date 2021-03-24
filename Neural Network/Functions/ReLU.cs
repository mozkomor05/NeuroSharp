using System;

namespace Neural_Network.Functions
{
    /* Rectified linear unit
     * 
     * Vlastnosti: 
     *  Obor hodnot: polouzavřený interval [0, nekonečno)
     *  Monotónnost: ano
     *  Monotónní derivace: ano
     *  Nelinearita: ano
     *  
     * POZN: Nejlepší funkce pro backpropagation.
     */

    [Serializable]
    public class ReLU : Function
    {
        public override double Value(double x) => Math.Max(0, x);

        public override double Derivative(double x) => x > 0 ? 1 : 0;
    }
}
