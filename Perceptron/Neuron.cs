using System;
using System.Collections.Generic;
using System.Linq;

namespace Perceptron
{
    internal class Neuron : INeuron
    {
        private IActivationFunction _activationFunction;
        private IInputFunction _inputFunction;

        /// <summary>
        /// Входные связи нейрона
        /// </summary>
        public List<ISynapse> Inputs { get; set; }

        /// <summary>
        /// Выходные связи нейрона
        /// </summary>
        public List<ISynapse> Outputs { get; set; }

        public Guid Id { get; private set; }

        /// <summary>
        /// Производная с прошлой итерации
        /// </summary>
        public double PreviousPartialDerivate { get; set; }

        public Neuron(IActivationFunction activationFunction, IInputFunction inputFunction)
        {
            Id = Guid.NewGuid();
            Inputs = new List<ISynapse>();
            Outputs = new List<ISynapse>();

            _activationFunction = activationFunction;
            _inputFunction = inputFunction;
        }

        /// <summary>
        /// Связать два нейрона
        /// (Это выходной нейрон для нейрона из параметров)
        /// </summary>
        /// <param name="inputNeuron">Входной нейрон связи</param>
        public void AddInputNeuron(INeuron inputNeuron)
        {
            var synapse = new Synapse(inputNeuron, this);
            Inputs.Add(synapse);
            inputNeuron.Outputs.Add(synapse);
        }

        /// <summary>
        /// Связать два нейрона
        /// (Это входной нейрон для нейрона из параметров)
        /// </summary>
        /// <param name="outputNeuron">Выходной нейрон связи</param>
        public void AddOutputNeuron(INeuron outputNeuron)
        {
            var synapse = new Synapse(this, outputNeuron);
            Outputs.Add(synapse);
            outputNeuron.Inputs.Add(synapse);
        }

        /// <summary>
        /// Вычислить выходное значение нейрона
        /// (Сбор выходных значений с предыдущих нейронов.
        /// Страшная цепная реакция)
        /// </summary>
        /// <returns>
        /// Выходное значение нейрона
        /// </returns>
        public double CalculateOutput()
        {
            return _activationFunction.CalculateOutput(_inputFunction.CalculateInput(this.Inputs));
        }

        /// <summary>
        /// Связь для входного слоя (входы)
        /// Input Layer neurons just receive input values.
        /// For this they need to have connections.
        /// This function adds this kind of connection to the neuron.
        /// </summary>
        /// <param name="inputValue">
        /// Входное значение для связи
        /// </param>
        public void AddInputSynapse(double inputValue)
        {
            var inputSynapse = new InputSynapse(this, inputValue);
            Inputs.Add(inputSynapse);
        }

        /// <summary>
        /// Передает значение на входные связи (на следующий слой)
        /// </summary>
        /// <param name="inputValue">
        /// Новое значение, которое передается на вход связи
        /// </param>
        public void PushValueOnInput(double inputValue)
        {
            ((InputSynapse)Inputs.First()).Output = inputValue;
        }
    }
}
