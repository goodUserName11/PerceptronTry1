using System;

namespace Perceptron
{
    internal class Synapse : ISynapse
    {
        internal INeuron _fromNeuron;
        internal INeuron _toNeuron;
        private static Random _random = new Random();

        public double Weight { get; set; }

        public double PreviousWeight { get; set; }

        public Synapse(INeuron fromNeuraon, INeuron toNeuron, double weight)
        {
            _fromNeuron = fromNeuraon;
            _toNeuron = toNeuron;

            Weight = weight;
            PreviousWeight = 0;
        }

        public Synapse(INeuron fromNeuraon, INeuron toNeuron)
        {
            _fromNeuron = fromNeuraon;
            _toNeuron = toNeuron;

            Weight = _random.NextDouble();
            PreviousWeight = 0;
        }

        public double GetOutput()
        {
            return _fromNeuron.CalculateOutput();
        }

        public bool IsFromNeuron(Guid fromNeuronId)
        {
            return _fromNeuron.Id.Equals(fromNeuronId);
        }

        public void UpdateWeight(double learningRate, double sigma)
        {
            PreviousWeight = Weight;
            Weight += learningRate * sigma;
        }
    }
}