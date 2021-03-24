using System;

namespace Neural_Network.Functions
{
    /* Identita (y = x)
     * 
     * Vlastnosti: 
     *  Obor hodnot: (-nekonečno, +nekonečno)
     *  Monotónnost: ano
     *  Monotónní derivace: ne
     *  Nelinearita: ne
     *  
     * POZN: Budou-li všechny vrstvy lineární, neuronová síť bude moct řešit pouze lineární problémy.
     */

    [Serializable]
    public class Identity : Function
    {
        public override double Value(double x) => x;

        public override double Derivative(double y) => 1;
    }
}
