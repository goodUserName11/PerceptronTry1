using System;

namespace Perceptron
{
    /// <summary>
    /// Сигмоида
    /// </summary>
    internal class SigmoidActivationFunction : IActivationFunction
    {
        /// <summary>
        /// Множитель
        /// </summary>
        private double _coeficient;

        public SigmoidActivationFunction(double coeficient)
        {
            _coeficient = coeficient;
        }

        public double CalculateOutput(double input)
        {
            return (1 / (1 + Math.Exp(-input * _coeficient)));
        }
    }
}
