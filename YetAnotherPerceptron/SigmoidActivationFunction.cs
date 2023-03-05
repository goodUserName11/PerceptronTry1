using System;

namespace YetAnotherPerceptron
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

        /// <summary>
        /// сдвиг по оси Y
        /// </summary>
        private double _yshift;

        /// <summary>
        /// сдвиг по оси X
        /// </summary>
        private double _xshift;

        public SigmoidActivationFunction()
        {
            _coeficient = 1;
            _yshift = 0;
            _xshift = 0;
        }

        public SigmoidActivationFunction(double coeficient, double shift, double xshift)
        {
            _coeficient = coeficient;
            _yshift = shift;
            _xshift = xshift;
        }

        public double CalculateOutput(double input)
        {
            return (1 / (1 + Math.Exp(-(input + _xshift) * _coeficient)) + _yshift);
        }

        //Возможен косяк из-за всяких сдвигов (но вроде не должно)
        public double CalculateDerivative(double input)
        {
            return CalculateOutput(input) * (1 - CalculateOutput(input));
        }
    }
}
