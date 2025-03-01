using System;
using ConsoleApp1.Utils;
using ConsoleApp1.BaiTap;
using ConsoleApp1.Constants;

namespace ConsoleApp1
{
    public class Program
    {
        static void ShowHelp()
        {
            Console.WriteLine("GraphAlgorithm @GS-755");
            Console.WriteLine($"Branch: {GitInfo.GIT_BRANCH}");
            Console.WriteLine("\nTo run project: No need to parse args!");
            Console.WriteLine("Note: To build release: use --merge-source argument"); 
        }
        public static void Main(string[] args)
        {
            if(args.Length > 0)
            {
                if(args[0] == "--merge-source")
                {
                    MergeReleaseSource.Execute();
                }
                else
                {
                    ShowHelp();
                }
                Environment.Exit(0);
            }
            Buoi2.Run();
            Buoi3.Run();
        }
    }
}
