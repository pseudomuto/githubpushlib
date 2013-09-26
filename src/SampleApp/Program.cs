using GitHubPushLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApp
{
    class Program
    {
        private static readonly string TOKEN_KEY = "GHPL_TOKEN";
        private static readonly string OWNER_KEY = "GHPL_OWNER";
        private static readonly string REPO_KEY = "GHPL_REPO";

        private static readonly string SEPARATOR = new string('*', 80);

        static void Main(string[] args)
        {
            // See README.md for setting up GHPL_TOKEN envirnoment variable before running...

            var token = Environment.GetEnvironmentVariable(
                    TOKEN_KEY, 
                    EnvironmentVariableTarget.User
                );

            var owner = ConfigurationManager.AppSettings[OWNER_KEY];
            var repo = ConfigurationManager.AppSettings[REPO_KEY];

            PrintHeader(token, owner, repo);

            Console.WriteLine();
            Console.WriteLine("Ensuring current version of file is in the repo...");

            var service = new ContentService(token);

            var file = new DiskFile("Files/content_file.gif");
            var target = new FileTarget(owner, repo, file.Name);

            service.PushFile(file, target, "pushing file via GitHubPushLib");
            
            Console.WriteLine("Finished!");
            Console.Read();
        }

        private static void PrintHeader(string token, string owner, string repo)
        {
            Console.WriteLine(SEPARATOR);
            Console.WriteLine("Using token: {0}", token);
            Console.WriteLine("For Repo: {0}/{1}", owner, repo);
            Console.WriteLine(SEPARATOR);
        }
    }
}
