using System;
using System.Collections.Generic;
using System.Linq;

namespace Perceptron
{
    /// <summary>
    /// Взвешенная сумма
    /// </summary>
    internal class WeightedSumFunction : IInputFunction
    {
        public double CalculateInput(List<ISynapse> inputs)
        {
            return inputs.Select(x => x.Weight * x.GetOutput()).Sum();
        }
    }
}
