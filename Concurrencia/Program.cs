using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Concurrencia
{
    class Program
    {
        static void Main(string[] args)
        {
            var tiempo = System.Diagnostics.Stopwatch.StartNew();
            var tareas = new List<Task>();
            for (int i = 1; i < 100; i++)
            {
                tareas.Add(Task.Factory.StartNew(
                    async () =>
                    {
                        var client = new HttpClient();
                        var response = 
                            await client.GetAsync("https://jsonplaceholder.typicode.com/todos/1");
                        var result = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(result);
                    }
                    ));
            }
            Task.WaitAll(tareas.ToArray());
            tiempo.Stop();
            Console.WriteLine(tiempo.ElapsedMilliseconds);
            Console.ReadKey();
        }
    }
}
