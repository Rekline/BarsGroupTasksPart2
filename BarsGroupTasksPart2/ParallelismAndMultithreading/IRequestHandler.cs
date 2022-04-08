using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarsGroupTasksPart2
{
    /// <summary>
    /// Предоставляет возможность обработать запрос.
    /// </summary>
    public interface IRequestHandler
    {
        /// <summary>
        /// Обработать запрос.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="arguments">Аргументы запроса.</param>
        /// <returns>Результат выполнения запроса.</returns>
        string HandleRequest(string message, string[] arguments);
    }
}
