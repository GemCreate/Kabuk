// AST generation tool for the Kabuk programming language.
// This tool generates the abstract syntax tree (AST) classes based on the provided definitions.


namespace GenerateAST
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 0)
            { // Check if the number of command line arguments is not equal to 0
                Console.Error.WriteLine("Usage: generate_ast <output directory>"); // Print an error message to the console if the number of arguments is not equal to 0
                Environment.Exit(64); // Exit code 64 indicates a command line usage error
                // 

            }
            string outputDir = args[0]; // Get the output directory from the command line arguments
        }

        private static void defineAst(string outputDir, string baseName, List<string> types)
        { // Define the AST classes based on the provided base name and types

            string path = Path.Combine(outputDir, baseName + ".cs");
            StreamWriter writer = new StreamWriter(path);

            // Write the necessary using directives and namespace declaration to the output file

            writer.WriteLine("namespace Kabuk {");
            writer.WriteLine();
            writer.WriteLine("internal class " + baseName + "{");

            foreach (string type in types) { 
            string className = type.Split(':')[0].Trim();
                string fields = type.Split(':')[1].Trim();
                defineType(writer, baseName, className, fields);

            }

            writer.WriteLine("}");
            writer.WriteLine("}");
            writer.Close();
        }

        private static void defineType(StreamWriter writer, string baseName, string className, string fieldList)
        {
            writer.WriteLine("internal class " + className + " : " + baseName + " {");
            // Split the field list into individual fields

            writer.WriteLine("    " + className + "(" + fieldList + ") {");
            // Split the field list into individual fields

            string[] fields = fieldList.Split(", ");
            foreach (string field in fields) { 
            string name = field.Split(" ")[1];
                writer.WriteLine("      this." + name + " = " + name + ";");

            }

            writer.WriteLine("    }");




            // fields
            writer.WriteLine();
            foreach (string field in fields) {
                writer.WriteLine("    public " + field + ";");
            
            }
            writer.WriteLine("  }");

        }

        private static void defineVisitor(StreamWriter writer, string baseName, List<string> types)
        {
            // Define the Visitor interface for the AST classes

            writer.WriteLine("  internal interface Visitor<R> {");
            foreach (string type in types)
            {
                string typeName = type.Split(':')[0].Trim();
                writer.WriteLine("    R visit" + typeName + baseName + "(" + typeName + " " + baseName.ToLower() + ");");
            }
            writer.WriteLine("  }");
        }



    }
}
