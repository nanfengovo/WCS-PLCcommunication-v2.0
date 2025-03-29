
namespace Quartz.NET
{
    public class MyJOB : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync("Hello World!");
        }
    }
}
