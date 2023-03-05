using System;
using System.Collections.Generic;
using System.Text;

namespace Perceptron
{
    /// <summary>
    /// Функция передачи значения на нейрон (на всякий случай)
    /// </summary>
    internal interface IInputFunction
    {
        double CalculateInput(List<ISynapse> inputs);
    }
}
