using System;
using System.Collections.Generic;
using System.Linq;

namespace Perceptron
{
    internal class NeuralLayer
    {
        public List<INeuron> Neurons;

        public NeuralLayer()
        {
            Neurons = new List<INeuron>();
        }

        /// <summary>
        /// Соединение двух слоев
        /// (Это выходной слой (НЕ последний))
        /// </summary>
        /// <param name="inputLayer">Слой, который ялвяется входным для этого слоя</param>
        public void ConnectLayers(NeuralLayer inputLayer)
        {
            var combos = Neurons.SelectMany(neuron => inputLayer.Neurons, (neuron, input) => new { neuron, input });
            combos.ToList().ForEach(x => x.neuron.AddInputNeuron(x.input));
        }
    }
}
