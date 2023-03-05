using System;
using System.Collections.Generic;
using System.Linq;

namespace YetAnotherPerceptron
{
    internal class Neuron
    {
        private IActivationFunction _activationFunction;

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
        public double PreviousDerivate { get; set; }

        //!!!!!!!!!!!!!!
        public double PreviousExpectedOutput { get; set; }

        /// <summary>
        /// Вход нейрона (для обучения)
        /// </summary>
        public double Input { get; private set; }

        public Neuron(IActivationFunction activationFunction)
        {
            Id = Guid.NewGuid();
            Inputs = new List<ISynapse>();
            Outputs = new List<ISynapse>();

            _activationFunction = activationFunction;
        }

        /// <summary>
        /// Связать два нейрона
        /// (Это выходной нейрон для нейрона из параметров)
        /// </summary>
        /// <param name="inputNeuron">Входной нейрон связи</param>
        public void AddInputNeuron(Neuron inputNeuron)
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
        public void AddOutputNeuron(Neuron outputNeuron)
        {
            var synapse = new Synapse(this, outputNeuron);
            Outputs.Add(synapse);
            outputNeuron.Inputs.Add(synapse);
        }

        private double WeightedSum(List<ISynapse> inputs)
        {
            return inputs.Select(x => x.Weight * x.GetOutput()).Sum();
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
            Input = WeightedSum(this.Inputs);
            return _activationFunction.CalculateOutput(Input);
        }

        /// <summary>
        /// Связь для нейронов входного слоя (входы)
        /// </summary>
        /// <param name="inputValue">
        /// Входное значение (вход) связи
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
            ((InputSynapse)Inputs.First())._output = inputValue;
        }
        
        /// <summary>
        /// Вычислить производную
        /// </summary>
        /// <param name="input">икс</param>
        /// <returns></returns>
        public double CalculateDerivate(double input)
        {
            return _activationFunction.CalculateDerivative(input);
        }
    }
}
