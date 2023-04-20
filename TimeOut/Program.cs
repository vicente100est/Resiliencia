using Polly;
using Polly.Timeout;

try
{
    await Policy.TimeoutAsync(TimeSpan.FromSeconds(10), TimeoutStrategy.Pessimistic)
        .ExecuteAsync(async () =>
        {
            var client = new HttpClient();
            var resp = await client.GetAsync("https://jsonplaceholder.typicode.com/todos/1");

            var content = await resp.Content.ReadAsStringAsync();

            Console.WriteLine(content);
        });
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}