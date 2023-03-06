using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization;
using System.Data;
using System.Globalization;

namespace YetAnotherPerceptron
{
    internal class Program
    {
        /// <summary>
        /// Это мейн, отсюда топаем
        /// (ничего особо важного)
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Hello NeuroWorld!");

            string inputFilePath = @"C:\Users\scorp\OneDrive\Рабочий стол\inputs.txt";
            string outputFilePath = @"C:\Users\scorp\OneDrive\Рабочий стол\outputs.txt";

            var teacherInputs = GetTeacherInputsFromTxtFile(inputFilePath);
            var teacherOutputs = GetTeacherOutputsTxtFile(outputFilePath);

            var network = NetworkStatic.CreateAndFillNetwork(teacherInputs, teacherOutputs, 0.8,
                // Слои
                3,
                //Кол-во нейронов на слоях
                teacherInputs[0].Length, 3, teacherOutputs[0].Length);

            Console.WriteLine("Настало время хака!");
            network.Train(1000);

            while (true)
            {
                Console.WriteLine("Вводи, товарищ!");
                Console.WriteLine("(Чтобы выйти бросьте пустой строкой)");

                var valuesStrings = Console.ReadLine().Split(',');
                var input = new double[teacherInputs[0].Length];

                if (valuesStrings[0] == "") break;

                for (int i = 0; i < valuesStrings.Length; i++)
                {
                    input[i] = double.Parse(valuesStrings[i], new CultureInfo("en-us"));
                }

                //var input = new double[] { 6.7, 3.1, 4.4, 1.4 };

                network.PushInputValues(input);
                var outputs = network.GetOutput();

                foreach (var output in outputs)
                {
                    Console.WriteLine(output);
                }
            }


            Console.WriteLine("OK");
            //Console.ReadLine();
        }

        static List<double[]> GetTeacherInputsFromTxtFile(string inputFilePath, char separator = ',')
        {
            var inputs = new List<double[]>();

            var inputStrings = File.ReadAllLines(inputFilePath);

            for (int i = 0; i < inputStrings.Length; i++)
            {
                var valuesStrings = inputStrings[i].Split(separator);
                var input = new double[valuesStrings.Length];

                for (int j = 0; j < valuesStrings.Length; j++)
                {
                    input[j] = double.Parse(valuesStrings[j], new CultureInfo("en-us"));
                }
                inputs.Add(input);
            }

            return inputs;
        }

        static List<double[]> GetTeacherOutputsTxtFile(string outputFilePath, char separator = ',')
        {
            var outputs = new List<double[]>();

            var outputStrings = File.ReadAllLines(outputFilePath);

            for (int i = 0; i < outputStrings.Length; i++)
            {
                var valuesStrings = outputStrings[i].Split(separator);
                var output = new double[valuesStrings.Length];

                for (int j = 0; j < valuesStrings.Length; j++)
                {
                    output[j] = double.Parse(valuesStrings[j], new CultureInfo("en-us"));
                }
                outputs.Add(output);
            }

            return outputs;
        }


        ///// <summary>
        ///// Данные для обучения (входы и выходы)
        ///// </summary>
        //struct TeacherData
        //{
        //    public double[] Inputs;
        //    public double[] Outputs;

        //    public TeacherData(double[] inputs, double[] outputs)
        //    {
        //        Inputs = inputs; 
        //        Outputs = outputs;
        //    }
        //}
    }
}
