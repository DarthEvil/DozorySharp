using System;

namespace DozorySharp
{
    /// <summary>
    /// Некорректное задание параметра
    /// </summary>
    internal class SetParameterError : ApplicationException
    {
        public SetParameterError(string message) : base(message)
        {
        }
    }
}