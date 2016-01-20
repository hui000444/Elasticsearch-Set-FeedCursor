using System;
using CommandLine;

namespace Elasticsearch.Set.FeedCursor
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = Parser.Default.ParseArguments<CLIOptions>(args);
            result
                .Return(
                    options =>
                    {
                        try
                        {
                            new CommandRunner(options).Run();
                            return 0;
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine("Message: {0}", exception.Message);
                            Console.WriteLine("CallStack: {0}", exception.StackTrace);
                            Console.WriteLine("InnerException: {0}", exception.InnerException);
                            return -1;
                        }
                    },
                    errors => -1);

        }

    }
}
