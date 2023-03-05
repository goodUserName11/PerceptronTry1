
namespace YetAnotherPerceptron
{
    /// <summary>
    /// Функция активации (чтобы можно было подменять)
    /// </summary>
    internal interface IActivationFunction
    {
        /// <summary>
        /// Вычислить значение функции
        /// </summary>
        /// <param name="input">Икс</param>
        /// <returns></returns>
        double CalculateOutput(double input);

        //Возможно стоит добавить производную
        /// <summary>
        /// Вычислить производную
        /// </summary>
        /// <param name="input">Икс</param>
        /// <returns></returns>
        double CalculateDerivative(double input);
    }
}
