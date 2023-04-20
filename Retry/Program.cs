using Polly;
using System;

public class Program
{
    public static async void Main(string[] args)
    {
        try
        {
            Policy.Handle<Exception>().Retry(3, (exception, rety) =>
            {
                Console.WriteLine($"Se jecuta {rety}");
            }).Execute(() =>
            {
                throw new Exception();
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine("Fallaron los reintentos");
        }

        await Policy.Handle<Exception>().RetryAsync(3, (exception, retry) =>
        {
            Console.WriteLine($"Se ejecuta la solicitud {retry}");
        }).ExecuteAsync(async () =>
        {
            var client = new HttpClient();
            var resp = await client.GetAsync("https://jsonplaceholder.typicode.com/todos/1");

            var content = await resp.Content.ReadAsStringAsync();

            Console.WriteLine(content);
        });
    }
}