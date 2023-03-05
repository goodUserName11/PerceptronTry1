using System;
using System.Collections.Generic;

namespace YetAnotherPerceptron
{
    /// <summary>
    /// Работник нейросети
    /// (Создает ее за вас)
    /// </summary>
    internal static class NetworkStatic
    {
        /// <summary>
        /// Создать и заполнить сеть
        /// </summary>
        /// <param name="teacherInputs">Учительская выборка</param>
        /// <param name="teacherOutputs">Учительские результаты</param>
        /// <param name="learningRate">Учительские результаты</param>
        /// <param name="numberOfLayers">Количество слоев в сети</param>
        /// <param name="numberOfNeurons">Количество нейронов на каждом слое</param>
        /// <returns></returns>
        public static NeuralNetwork CreateAndFillNetwork(List<double[]> teacherInputs,
                                                         List<double[]> teacherOutputs,
                                                         double learningRate,
                                                         int numberOfLayers,
                                                         params int[] numberOfNeurons)
        {
            if (numberOfNeurons.Length < numberOfLayers)
                throw new ArgumentException("Количество параметров соответсвовать количеству слоев", "NumberOfNeurons");

            var network = new NeuralNetwork(numberOfNeurons[0], learningRate, teacherInputs, teacherOutputs);

            // Потому что первый отдали в конструкторе, поэтому с единицы
            for (int i = 1; i < numberOfNeurons.Length; i++)
            {
                network.AddLayer(LayerCreator.CreateNeuralLayer(numberOfNeurons[i], new SigmoidActivationFunction()));
            }

            //network.Train(100);

            //network.PushInputValues(new double[] { 1054, 54, 1 });
            //var outputs = network.GetOutput();

            return network;
        }
    }
}
