using System;

namespace YetAnotherPerceptron
{
    internal class Synapse : ISynapse
    {
        private static Random _random = new Random();

        internal Neuron _fromNeuron;
        internal Neuron _toNeuron;

        public double Weight { get; private set; }
        public double PreviousWeight { get; private set; }

        public Synapse(Neuron fromNeuraon, Neuron toNeuron, double weight)
        {
            _fromNeuron = fromNeuraon;
            _toNeuron = toNeuron;

            Weight = weight;
        }

        public Synapse(Neuron _fromNeuron, Neuron _toNeuron)
        {
            this._fromNeuron = _fromNeuron;
            this._toNeuron = _toNeuron;

            Weight = _random.NextDouble();
            PreviousWeight = 0;
        }

        public bool IsFromNeuron(Guid fromNeuronId)
        {
            return _fromNeuron.Id.Equals(fromNeuronId);
        }

        public void UpdateWeight(double delta, double learningRate)
        {
            PreviousWeight = Weight;
            Weight += delta * learningRate;
        }

        public double GetOutput()
        {
            return _fromNeuron.CalculateOutput();
        }
    }
}