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

            var teacherInputs = NetworkStatic.GetTeacherDataFromTxtFile(inputFilePath);
            var teacherOutputs = NetworkStatic.GetTeacherDataFromTxtFile(outputFilePath);

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
                
                var inputString = Console.ReadLine();

                if (inputString == "") break;

                var input = StaticHelpers.StringToDoubleValues(inputString);

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
    }
}
