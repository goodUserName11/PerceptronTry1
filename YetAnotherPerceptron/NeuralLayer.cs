using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YetAnotherPerceptron
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    ///     <listheader>TODO:</listheader>
    ///     <list type="number">
    ///         <item>Додумать слои (возмодно все)</item>
    ///     </list>
    /// </remarks>
    internal class NeuralLayer
    {
        public List<Neuron> NeuronList {  get; private set; }

        public NeuralLayer()
        {
            NeuronList = new List<Neuron>();
        }

        /// <summary>
        /// Соединение двух слоев
        /// (Это выходной слой (НЕ последний))
        /// </summary>
        /// <param name="inputLayer">Слой, который ялвяется входным для этого слоя</param>
        public void ConnectLayers(NeuralLayer inputLayer)
        {
            var combos = NeuronList.SelectMany(neuron => inputLayer.NeuronList, (neuron, input) => new { neuron, input });
            combos.ToList().ForEach(x => x.neuron.AddInputNeuron(x.input));
        } 
    }
}
