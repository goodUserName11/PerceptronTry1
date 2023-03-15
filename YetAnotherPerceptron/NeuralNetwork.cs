using System;
using System.Collections.Generic;
using System.Linq;

namespace YetAnotherPerceptron
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    ///     <listheader>TODO:</listheader>
    ///     <list type="number">
    ///         <item>Добавить список и обработку тестового множества</item>
    ///         <item>Передавать функцию активации из вне</item>
    ///     </list>
    /// </remarks>
    public class NeuralNetwork
    {
        /// <summary>
        /// Альфа
        /// </summary>
        private double _learningRate;
        private Random _random;

        /// <summary>
        /// Данные для обучения (обучающая выборка)
        /// </summary>
        private List<double[]> _teacherInputs;

        /// <summary>
        /// Ожидаемые результаты
        /// </summary>
        private List<double[]> _teacherOutputs;

        internal List<NeuralLayer> Layers { get; private set; }

        public NeuralNetwork(int numberOfInputNeurons,
                             double learningRate,
                             List<double[]> teacherInputs,
                             List<double[]> teacherOutputs)
        {
            Layers = new List<NeuralLayer>();

            CreateInputLayer(numberOfInputNeurons);

            _learningRate = learningRate;
            _teacherInputs = teacherInputs;
            _teacherOutputs = teacherOutputs;

            _random = new Random();
        }

        /// <summary>
        /// Добавляет новый слой
        /// </summary>
        /// <param name="newLayer"></param>
        internal void AddLayer(NeuralLayer newLayer)
        {
            if (Layers.Count > 0)
            {
                var lastLayer = Layers.Last();
                newLayer.ConnectLayers(lastLayer);
            }

            Layers.Add(newLayer);
        }

        /// <summary>
        /// Подать (мне Таноса!) значения на входной слой
        /// </summary>
        public void PushInputValues(double[] inputs)
        {
            Layers.First().NeuronList.ForEach(x => x.PushValueOnInput(inputs[Layers.First().NeuronList.IndexOf(x)]));
        }

        /// <summary>
        /// Вычисляет выходные значения крайних нейронов
        /// </summary>
        /// <returns></returns>
        public List<double> GetOutput()
        {
            var returnValue = new List<double>();

            Layers.Last().NeuronList.ForEach(neuron =>
            {
                returnValue.Add(neuron.CalculateOutput());
            });

            return returnValue;
        }

        public void Train(int numberOfEpochs)
        {
            double totalError = 0;
            List<double[]> teacherInputs;
            List<double[]> teacherOutputs;

            for (int i = 0; i < numberOfEpochs; i++)
            {
                teacherInputs = _teacherInputs.ToList();
                teacherOutputs = _teacherOutputs.ToList();
                totalError = 0;

                while (teacherInputs.Count > 0) 
                {
                    var randomInput = _random.Next(teacherInputs.Count);
                    PushInputValues(teacherInputs[randomInput]);

                    var outputs = new List<double>();

                    // получаем выходы
                    Layers.Last().NeuronList.ForEach(x =>
                    {
                        outputs.Add(x.CalculateOutput());
                    });

                    totalError += CalculateCurrentError(outputs, teacherOutputs, randomInput);
                    //Console.WriteLine($"error: {totalError}");


                    HandleOutputLayer(randomInput, teacherOutputs);
                    HandleHiddenLayers(randomInput, teacherOutputs);

                    teacherInputs.RemoveAt(randomInput);
                    teacherOutputs.RemoveAt(randomInput);
                }

                Console.WriteLine($"{i + 1}. error: {totalError / _teacherInputs.Count}");
            }
        }

        /// <summary>
        /// Вспомогательная, создает входной слой
        /// </summary>
        private void CreateInputLayer(int numberOfInputNeurons)
        {
            var inputLayer = StaticLayerCreator.CreateNeuralLayer(numberOfInputNeurons, new SigmoidActivationFunction());
            inputLayer.NeuronList.ForEach(x => x.AddInputSynapse(0));
            AddLayer(inputLayer);
        }

        /// <summary>
        /// Вспомогательная, считает ошибку сети (для текущей пары входных/выходных данных)
        /// (Считаем ошибку, складывая ошибки со всех выходных нейронов)
        /// </summary>
        private double CalculateCurrentError(List<double> outputs, List<double[]> teacherOutputs, int row)
        {
            double currError = 0;

            outputs.ForEach(output =>
            {
                var error = Math.Pow(teacherOutputs[row][outputs.IndexOf(output)] - output, 2);
                currError += error;
            });

            return currError;
        }

        /// <summary>
        /// Вспомогательная, запускает алгоритм обратного обучения для выходного слоя сети
        /// (обратного распространения ошибки)
        /// ???
        /// </summary>
        /// <param name="row">
        /// Номер значения из массива ожидаемых сначений
        /// </param>
        private void HandleOutputLayer(int row, List<double[]> teacherOutpust)
        {
            Layers.Last().NeuronList.ForEach(neuron =>
            {
                neuron.Inputs.ForEach(connection =>
                {
                    // y
                    var output = neuron.CalculateOutput();
                    // x
                    var netInput = connection.GetOutput();

                    // t
                    var expectedOutput = teacherOutpust[row][Layers.Last().NeuronList.IndexOf(neuron)];

                    //var derivate = neuron.CalculateDerivate(netInput) * (expectedOutput - output);
                    var derivate = (expectedOutput - output) * output * (1 - output);

                    //                          все-таки наоборот
                    // дельта вес = x * производная * (y-t) (альфа спряталась в другом замке)
                    //                                (t-y) - правильно
                    //var delta = netInput * derivate * (expectedOutput - output);
                    var delta = /*-1 **/ netInput * derivate /** (expectedOutput - output)*/;

                    //                            а вот и альфа
                    connection.UpdateWeight(delta, _learningRate);

                    neuron.PreviousDerivate = derivate;
                    neuron.PreviousExpectedOutput = expectedOutput;
                });
            });
        }

        /// <summary>
        /// Вспомогательная, запускает алгоритм обратного обучения для скрытого слоя сети
        /// (обратного распространения ошибки)
        /// ???
        /// </summary>
        private void HandleHiddenLayers(int row, List<double[]> teacherOutpust)
        {
            for (int k = Layers.Count - 2; k > 0; k--)
            {
                Layers[k].NeuronList.ForEach(neuron =>
                {
                    neuron.Inputs.ForEach(connection =>
                    {
                        // y
                        var output = neuron.CalculateOutput();
                        // x
                        var netInput = connection.GetOutput();

                        // t
                        var expectedOutput = neuron.PreviousExpectedOutput;

                        var derivate = neuron.CalculateDerivate(netInput);

                        // сумма (сигм * веса)
                        // Сигма = производная * (t-y)
                        double sumPartial = 0;

                        Layers[k + 1].NeuronList
                        .ForEach(outputNeuron =>
                        {
                            outputNeuron.Inputs.Where(i => i.IsFromNeuron(neuron.Id))
                            .ToList()
                            .ForEach(outConnection =>
                            {
                                // Считаем сумму (сигм * веса)
                                // Сигма = производная * (t-y)
                                //                       (t-y) - правильно

                                sumPartial += outConnection.PreviousWeight * outputNeuron.PreviousDerivate/* * (expectedOutput - output)*/;
                            });
                        });

                        // дельта вес = x * производная * Сумма (сигм * веса) (альфа спряталась в другом замке)
                        //var delta = netInput * derivate * sumPartial;
                        var delta = /*-1 **/ netInput * sumPartial * output * (1 - output);
                        //                             а вот же она
                        connection.UpdateWeight(delta, _learningRate);

                        neuron.PreviousDerivate = derivate;
                        neuron.PreviousExpectedOutput = expectedOutput;
                    });
                });
            }
        }
    }
}
