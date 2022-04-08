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
                    ThreadPool.QueueUserWorkItem( _ => {
                        try
                        {
                            var requestHandler = new DummyRequestHandler();
                            var requestMessage = requestHandler.HandleRequest(taskText, arguments.ToArray());
                            Console.WriteLine($"Было отправлено сообщение '{taskText}'. Присвоен идентификатор {requestMessage}");
                            Thread.Sleep(12000);
                            Console.WriteLine($"Сообщение с идентификатором {requestMessage} " +
                                $"получило ответ - {Guid.NewGuid().ToString("D")}.");
                        }
                        catch(Exception ex)
                        {
                            var requestIdWithError = Guid.NewGuid().ToString("D");
                            Console.WriteLine($"Сообщение с идентификатором {requestIdWithError} упало с ошибкой: {ex}.");
                        }
                    });
                }
                
            } while (true);
            Console.WriteLine("Приложение завершает работу");
        }
           
    }
}
