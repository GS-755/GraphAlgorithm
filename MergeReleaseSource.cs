using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using ConsoleApp1.Constants;

namespace ConsoleApp1
{
    public static class MergeReleaseSource
    {
        /// <summary>
        /// Batch version
        /// </summary>
        static readonly string VERSION = "1.0";
        /// <summary>
        /// Root Project PATH 
        /// </summary>
        static readonly string PROJECT_PATH = @"..\\.."; 
        /// <summary>
        /// Declare namespace here for export!
        /// </summary>
        static readonly string EXPORT_NAMESPACE = "TTDT_21DH114236";
        /// <summary>
        /// Declare list .cs file(s) to exclude for merge source 
        /// </summary>
        static List<string> excludedCsFiles = new List<string>
        {
            ".NETFramework,Version=v4.7.2.AssemblyAttributes.cs",
            "AssemblyInfo.cs"
        };
        /// <summary>
        /// Function to check if current file is in excluded list or not 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>Excluded OR NOT :)</returns>
        static bool IsFileExcluded(string fileName)
        {
            if(fileName == null)
            {
                return false; 
            }
            string fileNameWithoutPath = Path.GetFileName(fileName);
            string matchCsFileName = excludedCsFiles.FirstOrDefault(x => (x != null && x == fileNameWithoutPath));

            return (matchCsFileName != null);
        }
        /// <summary>
        /// Execute merge source procedure 
        /// </summary>
        public static void Execute()
        {
            string outputFile = Path.Combine(PROJECT_PATH, $"Program_{GitInfo.GIT_BRANCH}.cs");
            string exportedPath = Path.GetFullPath(outputFile);
            List<string> allClasses = new List<string>();
            Console.WriteLine($"MergeReleaseSource.Execute() Version: {VERSION}");
            Console.WriteLine("MergeReleaseSource.Execute() Start merge source");
            if (File.Exists(exportedPath))
            {
                File.Delete(exportedPath);
                Console.WriteLine("Deleted old merged source file!");
            }
            string[] arrCsFiles = Directory.GetFiles(PROJECT_PATH, "*.cs", SearchOption.AllDirectories);
            if(arrCsFiles == null || arrCsFiles.Length <= 0)
            {
                Console.WriteLine("Invalid *.cs file list!");
                return; 
            }
            try
            {
                foreach (string file in arrCsFiles)
                {
                    if (IsFileExcluded(file))
                    {
                        continue;
                    }
                    string classContent = File.ReadAllText(file);
                    var syntaxTree = CSharpSyntaxTree.ParseText(classContent);
                    var root = syntaxTree.GetRoot();
                    var classes = root.DescendantNodes().OfType<ClassDeclarationSyntax>();
                    foreach (var classNode in classes)
                    {
                        allClasses.Add(classNode.ToFullString());
                    }
                }

                using (StreamWriter writer = new StreamWriter(outputFile, false))
                {
                    writer.WriteLine("using System;");
                    writer.WriteLine("using System.IO;");
                    writer.WriteLine("using System.Linq;");
                    writer.WriteLine("using System.Configuration;");
                    writer.WriteLine("using System.Collections.Generic;");
                    writer.WriteLine();
                    writer.WriteLine("using Microsoft.CodeAnalysis;");
                    writer.WriteLine("using Microsoft.CodeAnalysis.CSharp;");
                    writer.WriteLine("using Microsoft.CodeAnalysis.CSharp.Syntax;");
                    writer.WriteLine();
                    writer.WriteLine($"namespace {EXPORT_NAMESPACE}");
                    writer.WriteLine("{");
                    foreach (string classContent in allClasses)
                    {
                        writer.WriteLine(classContent);
                        writer.WriteLine();
                    }
                    writer.WriteLine("}");
                }
                Console.WriteLine($"Merge all source complete! All classes are now in {exportedPath}");
            }
            catch(Exception ex)
            {
                Console.WriteLine("MergeReleaseSource.Execute() unhandled exception: ");
                Console.WriteLine(ex); 
            }
            Console.WriteLine("MergeReleaseSource.Execute() End merge source\n");
        }
    }
}
