using System;

namespace Perceptron
{
    /// <summary>
    /// Связь
    /// </summary>
    public interface ISynapse
    {
        /// <summary>
        /// Вес связи
        /// </summary>
        double Weight { get; set; }

        /// <summary>
        /// Вес с прошлой итерации
        /// </summary>
        double PreviousWeight { get; set; }

        /// <summary>
        /// Вычислить выходное значение связи
        /// </summary>
        /// <returns>
        /// Выходное значение связи
        /// </returns>
        double GetOutput();

        /// <summary>
        /// Проверка, что нейрон является входным для этой связи
        /// </summary>
        /// <param name="fromNeuronId">ID нейрона</param>
        /// <returns>
        /// True - нейрон является входным нейроном связи 
        /// </returns>
        bool IsFromNeuron(Guid fromNeuronId);

        /// <summary>
        /// Обновить вес (обучение, "обратный прогон")
        /// </summary>
        /// <param name="learningRate">Скорость обучения</param>
        /// <param name="delta">
        ///     На сколько нужно менять
        /// </param>
        void UpdateWeight(double learningRate, double delta);
    }
}