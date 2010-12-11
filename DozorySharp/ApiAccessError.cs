using System;

namespace DozorySharp
{
    /// <summary>
    /// Ошибка доступа к API
    /// </summary>
    internal class ApiAccessError : ApplicationException
    {
        public ApiAccessError(string message) : base(message)
        {
        }
    }
}