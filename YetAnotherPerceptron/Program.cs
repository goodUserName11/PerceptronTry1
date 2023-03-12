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

            //string inputFilePath = @"C:\Users\scorp\OneDrive\Рабочий стол\inputs.txt";
            //string outputFilePath = @"C:\Users\scorp\OneDrive\Рабочий стол\outputs.txt";            
            string inputFilePath;
            string outputFilePath;
            int numberOfEpoches;

            if (args.Length == 2) 
            { 
                inputFilePath = args[0];
                outputFilePath = args[1];
            }
            else
            {
                Console.WriteLine("Введите путь для файла с входными данными обучающей выборки");
                inputFilePath = Console.ReadLine();
                
                Console.WriteLine("Введите путь для файла с результатами обучающей выборки");
                outputFilePath = Console.ReadLine();
            }

            Console.WriteLine("Введите кол-во эпох");
            int.TryParse(Console.ReadLine(), out numberOfEpoches);

            var teacherInputs = NetworkStatics.GetTeacherDataFromTxtFile(inputFilePath);
            var teacherOutputs = NetworkStatics.GetTeacherDataFromTxtFile(outputFilePath);

            var network = NetworkStatics.CreateAndFillNetwork(teacherInputs, teacherOutputs, 0.8,
                // Слои
                3,
                //Кол-во нейронов на слоях
                teacherInputs[0].Length, 3, teacherOutputs[0].Length);

            Console.WriteLine("Обучение...");
            network.Train(1000);

            while (true)
            {
                Console.WriteLine("Вводите входные данные");
                Console.WriteLine("(Чтобы выйти бросьте пустой строкой)");
                
                var inputString = Console.ReadLine();

                if (inputString == "") break;

                var input = StaticHelpers.StringToDoubleValues(inputString);

                network.PushInputValues(input);
                var outputs = network.GetOutput();

                Print(outputs);
            }

            Console.WriteLine("OK");
        }

        /// <summary>
        /// Печать листа в консоли
        /// </summary>
        /// <param name="dataList">Лист, который нужно напечатать</param>
        static void Print(List<double> dataList)
        {
            foreach (var value in dataList)
            {
                Console.WriteLine(value);
            }
        }
    }
}
