using System;

namespace YetAnotherPerceptron
{
    internal class InputSynapse : ISynapse
    {
        internal Neuron _fromNeuron;
        internal double _output;

        public double Weight { get; private set; }
        public double PreviousWeight { get; private set; }

        public InputSynapse(Neuron fromNeuron, double output)
        {
            this._fromNeuron = fromNeuron;
            this._output = output;

            Weight = 1;
            PreviousWeight = 1;
        }

        public bool IsFromNeuron(Guid fromNeuronId)
        {
            return false;
        }

        public void UpdateWeight(double learningRate, double delta)
        {
            throw new InvalidOperationException("Бесполезно");
        }

        public double GetOutput()
        {
            return _output;
        }
    }
}