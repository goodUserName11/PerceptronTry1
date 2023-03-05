using System;

namespace YetAnotherPerceptron
{
    internal interface ISynapse
    {
        double Weight { get; }
        double PreviousWeight { get; }

        /// <summary>
        /// Проверка, что нейрон является входным для этой связи
        /// </summary>
        /// <param name="fromNeuronId">ID нейрона</param>
        /// <returns>
        /// True - нейрон является входным нейроном связи 
        /// </returns>
        bool IsFromNeuron(Guid fromNeuronId);

        /// <summary>
        /// Применить изменение веса
        /// </summary>
        /// <param name="delta">Разница</param>
        /// <param name="learningRate">Скорость обучения (альфа)</param>
        void UpdateWeight(double delta, double learningRate);

        /// <summary>
        /// Вычислить выходное значение связи
        /// </summary>
        /// <returns></returns>
        double GetOutput();
    }
}