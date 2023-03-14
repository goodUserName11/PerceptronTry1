using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace YetAnotherPerceptron
{
    /// <summary>
    /// Статические методы для работы с нейросетью
    /// (В том числе создает ее за вас)
    /// </summary>
    public static class NetworkStatics
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
                network.AddLayer(StaticLayerCreator.CreateNeuralLayer(numberOfNeurons[i], new SigmoidActivationFunction()));
            }

            //network.Train(100);

            //network.PushInputValues(new double[] { 1054, 54, 1 });
            //var outputs = network.GetOutput();

            return network;
        }

        /// <summary>
        /// Получает данные учительской выборки (входы или выходы) из текстового файла
        /// </summary>
        /// <param name="dataFilePath">Путь к файлу с данными</param>
        /// <param name="separator">Разделитель значений</param>
        /// <param name="culture">
        /// Культура разделителя целой и десятичной части 
        /// (по умолчанию точка)
        /// </param>
        /// <returns>Список массивов (входный или выходных) значений</returns>
        public static List<double[]> GetTeacherDataFromTxtFile(string dataFilePath, char separator = ',', string culture = "en-us")
        {
            var valuesArrayList = new List<double[]>();

            var valuesStrings = File.ReadAllLines(dataFilePath);

            for (int i = 0; i < valuesStrings.Length; i++)
            {
                valuesArrayList.Add(StaticHelpers.StringToDoubleValues(valuesStrings[i], separator, culture));
            }

            return valuesArrayList;
        }
    }
}
