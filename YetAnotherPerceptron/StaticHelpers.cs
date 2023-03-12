using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace YetAnotherPerceptron
{
    public class StaticHelpers
    {
        /// <summary>
        /// Приводит строку входных данных к массиву значений
        /// </summary>
        /// <param name="inputString">Строка входных данных</param>
        /// <param name="separator">Разделитель значений</param>
        /// <param name="culture">
        /// Культура разделителя целой и десятичной части 
        /// (по умолчанию точка)
        /// </param>
        /// <returns>Массив чисел с плавающей точкой</returns>
        public static double[] StringToDoubleValues(string inputString, char separator = ',', string culture = "en-us")
        {
            List<double> values = new List<double>();

            var stringValues = inputString.Split(separator);

            foreach (var stringValue in stringValues)
            {
                values.Add(double.Parse(stringValue, new CultureInfo(separator)));
            }

            return values.ToArray();
        }
    }
}
