using System;

namespace Neural_Network.Functions
{
    /* Hyperbolický tangens
     * 
     * Vlastnosti: 
     *  Obor hodnot: otevřený interval (-1,1)
     *  Monotónnost: ano
     *  Monotónní derivace: ne
     *  Nelinearita: ano
     *  
     * POZN: Derivace je strmější než u sigmoidu => gradient je prudší
     */

    [Serializable]
    public class TanH : Function
    {
        public override double Value(double x) => Math.Tanh(x);

        public override double Derivative(double x) => 1 - Math.Pow(Math.Tanh(x), 2);
    }
}
