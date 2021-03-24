using System;

namespace Neural_Network.Functions
{
    /* Sigmoidální přenosová funkce
     * 
     * Vlastnosti: 
     *  Obor hodnot: otevřený interval (0,1)
     *  Monotónnost: ano
     *  Monotónní derivace: ano
     *  Nelinearita: ano
     *  
     * POZN: U backpropagation učícího algoritmu může nastat "vanishing gradient problem"
     */

    [Serializable]
    public class Sigmoid : Function
    {
        public override double Value(double x) => 1 / (1 + Math.Exp(-x));

        public override double Derivative(double y) => Value(y) * (1 - Value(y));
    }
}
