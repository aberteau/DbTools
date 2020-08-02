using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using CommandLine;
using DbTools.Core;
using DbTools.DbStructure.Data;

namespace DbTools.DbStructure.ExportConsoleApp
{
    class Program
    {
        public class Options
        {
            [Option("connectionString", HelpText = "DB connection string", Required = true)]
            public String ConnectionString { get; set; }

            [Option("tableCatalog", HelpText = "Table Catalog")]
            public String TableCatalog { get; set; }

            [Option("tableSchema", HelpText = "Table Schema")]
            public String TableSchema { get; set; }

            [Option("outputPath", HelpText = "Output Path", Required = true)]
            public String OutputPath { get; set; }
        }

        static async Task Main(string[] args)
        {
            Task<int> result = Parser.Default.ParseArguments<Options>(args)
                .MapResult(RunAndReturnExitCodeAsync,
                    _ => Task.FromResult(1));
        }

        static async Task<int> RunAndReturnExitCodeAsync(Options options)
        {
            await WriteJsonAsync(
                options.ConnectionString,
                options.TableCatalog,
                options.TableSchema,
                options.OutputPath);

            Console.WriteLine("Press Enter key to quit...");
            Console.ReadLine();

            return 0;
        }

        private static async Task WriteJsonAsync(string connectionString, string tableCatalog, string tableSchema, string filepath)
        {
            if (String.IsNullOrWhiteSpace(connectionString))
                throw new Exception("connectionString must be provided");

            if (String.IsNullOrWhiteSpace(tableCatalog) && String.IsNullOrWhiteSpace(tableSchema))
                throw new Exception("tableCatalog or tableSchema must be provided");

            String outputPath = filepath;
            if (String.IsNullOrWhiteSpace(outputPath))
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                string applicationFolder = Path.GetDirectoryName(codeBase);
                string chrono = GetChrono();
                string jsonFilename = $"Database-{chrono}.json";
                outputPath = Path.Combine(applicationFolder, jsonFilename);
            }

            Console.WriteLine($"Connecting on '{connectionString}'");

            Database database = Services.MySql.DatabaseHelper.GetDatabase(connectionString, tableCatalog, tableSchema);
            await JsonHelper.WriteJsonAsync(database, outputPath);

            Console.WriteLine("Generation completed");
            Console.WriteLine($"Output file generated in '{outputPath}'");
        }

        public static string GetChrono()
        {
            string chrono = DateTime.Now.ToString("yyMMddHHmmss");
            return chrono;
        }
    }
}
