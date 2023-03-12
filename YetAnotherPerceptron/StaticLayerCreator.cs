using System;
using System.Collections.Generic;
using System.Text;

namespace YetAnotherPerceptron
{
    /// <summary>
    /// Создает слои
    /// </summary>
    internal static class StaticLayerCreator
    {
        /// <summary>
        /// Создать слой
        /// </summary>
        /// <param name="numberOfNeurons">Количество нейронов в слое</param>
        /// <param name="activationFunction">Функция активации нейронов в слое</param>
        /// <returns></returns>
        public static NeuralLayer CreateNeuralLayer(int numberOfNeurons, IActivationFunction activationFunction)
        {
            NeuralLayer neuralLayer = new NeuralLayer();

            for (int i = 0; i < numberOfNeurons; i++)
            {
                neuralLayer.NeuronList.Add(new Neuron(activationFunction));
            }

            return neuralLayer;
        }
    }
}
