using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ConsoleApp1.Utils
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
        static readonly string EXPORT_NAMESPACE = "21DH114236_TTDT";
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
            foreach(string excludedFileName in excludedCsFiles)
            {
                if(fileName.Trim() == excludedFileName)
                {
                    return true; 
                }
            }

            return false; 
        }
        /// <summary>
        /// Execute merge source procedure 
        /// </summary>
        public static void Execute()
        {
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmm");
            string outputFile = Path.Combine(PROJECT_PATH, $"Program_{timestamp}.cs");

            List<string> allClasses = new List<string>();
            Console.WriteLine($"MergeReleaseSource.Execute() Version: {VERSION}");
            Console.WriteLine("MergeReleaseSource.Execute() Start merge source");
            try
            {
                foreach (string file in Directory.GetFiles(PROJECT_PATH, "*.cs", SearchOption.AllDirectories))
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

                using (StreamWriter writer = new StreamWriter(outputFile))
                {
                    writer.WriteLine("using System;");
                    writer.WriteLine("using System.Linq;");
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

                string exportedPath = Path.GetFullPath(outputFile);
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
