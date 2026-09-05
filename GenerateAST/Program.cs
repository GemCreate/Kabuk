// AST generation tool for the Kabuk programming language.
// This tool generates the abstract syntax tree (AST) classes based on the provided definitions.

namespace GenerateAST;

internal class Program
{
    static void Main(string[] args)
    {
        if (args.Length > 1)
        {
            Console.Error.WriteLine("Usage: generate_ast [output directory]");
            Environment.Exit(64); // Command line usage error
        }

        // Use the given directory, or fall back to the current directory.
        string outputDir = args.Length == 1 ? args[0] : Directory.GetCurrentDirectory();

        DefineAst(outputDir, "Expr", new List<string>
        {
            "Binary   : Expr left, Token oprtr, Expr right",
            "Grouping : Expr expression",
            "Literal  : object value",
            "Unary    : Token oprtr, Expr right"
        });
    }

    private static void DefineAst(string outputDir, string baseName, List<string> types)
    {
        string path = Path.Combine(outputDir, baseName + ".cs");
        using StreamWriter writer = new StreamWriter(path);

        writer.WriteLine("namespace Kabuk;");
        writer.WriteLine();
        writer.WriteLine("internal abstract class " + baseName);
        writer.WriteLine("{");

        DefineVisitor(writer, baseName, types);

        // The AST classes.
        foreach (string type in types)
        {
            string className = type.Split(':')[0].Trim();
            string fields = type.Split(':')[1].Trim();
            DefineType(writer, baseName, className, fields);
        }

        // The base Accept() method.
        writer.WriteLine();
        writer.WriteLine("    internal abstract R Accept<R>(IVisitor<R> visitor);");

        writer.WriteLine("}");
    }

    private static void DefineVisitor(StreamWriter writer, string baseName, List<string> types)
    {
        writer.WriteLine("    internal interface IVisitor<R>");
        writer.WriteLine("    {");

        foreach (string type in types)
        {
            string typeName = type.Split(':')[0].Trim();
            writer.WriteLine("        R Visit" + typeName + baseName + "(" + typeName + " " + baseName.ToLower() + ");");
        }

        writer.WriteLine("    }");
    }

    private static void DefineType(StreamWriter writer, string baseName, string className, string fieldList)
    {
        writer.WriteLine();
        writer.WriteLine("    internal class " + className + " : " + baseName);
        writer.WriteLine("    {");

        // Constructor.
        writer.WriteLine("        internal " + className + "(" + fieldList + ")");
        writer.WriteLine("        {");

        // Store parameters in fields.
        string[] fields = fieldList.Split(", ");
        foreach (string field in fields)
        {
            string name = field.Split(' ')[1];
            writer.WriteLine("            this." + name + " = " + name + ";");
        }

        writer.WriteLine("        }");

        // Visitor pattern.
        writer.WriteLine();
        writer.WriteLine("        internal override R Accept<R>(IVisitor<R> visitor)");
        writer.WriteLine("        {");
        writer.WriteLine("            return visitor.Visit" + className + baseName + "(this);");
        writer.WriteLine("        }");

        // Fields.
        writer.WriteLine();
        foreach (string field in fields)
        {
            writer.WriteLine("        internal readonly " + field + ";");
        }

        writer.WriteLine("    }");
    }
}
