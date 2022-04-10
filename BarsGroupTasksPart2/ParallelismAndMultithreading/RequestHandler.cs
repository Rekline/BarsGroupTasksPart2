using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarsGroupTasksPart2
{
    public class RequestHandler
    {
        public static void Run()
        {
            var arguments = new List<string>();
            Console.WriteLine("Приложение запущено;");

            do
            {
                Console.WriteLine("Введите текст запроса для отправки. Для выхода введите /exit.");
                var taskText = Console.ReadLine();

                while (taskText == "")
                {
                    Console.WriteLine("Введён пустой текст запроса. Исправьте текст и повторите попытку.");
                    taskText = Console.ReadLine();
                }

                if (taskText == "/exit")
                    break;
                Console.WriteLine($"Будет послано сообщение '{taskText}'");

                Console.WriteLine("Введите аргументы сообщения. Для окончания добавления аргументов введите /end");
                var taskArg = Console.ReadLine();

                while (taskArg != "/end")
                {
                    while (taskArg == "")
                    {
                        Console.WriteLine("Введён пустой аргумент. Исправьте текст и повторите попытку.");
                        taskArg = Console.ReadLine();
                    }
                    arguments.Add(taskArg);
                    Console.WriteLine("Введите следующий аргумент сообщения. Для окончания добавления аргументов введите /end");
                    taskArg = Console.ReadLine();
                }

                if (taskArg == "/end")
                {
                    var requestId = Guid.NewGuid().ToString("D");
                    var argumentsArray = arguments.ToArray(); ;
                    arguments.Clear();
                    Console.WriteLine($"Было отправлено сообщение '{taskText}'. Присвоен идентификатор {requestId}");
                    ThreadPool.QueueUserWorkItem( _ => {
                        try
                        {
                            var requestHandler = new DummyRequestHandler();
                            var requestMessage = requestHandler.HandleRequest(taskText, argumentsArray);
                            Console.WriteLine($"Сообщение с идентификатором {requestId} " +
                                $"получило ответ - {requestMessage}.");
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine($"Сообщение с идентификатором {requestId} упало с ошибкой: {ex.Message}.");
                        }
                    });
                }
            } while (true);
            Console.WriteLine("Приложение завершает работу");
        }
           
    }
}
