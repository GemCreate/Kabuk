// Kabuk a Turkish programming language
// Ver. 1.0.0
// (Unfinished demo / CAS project)

// Follows the Crafting Interpreters book for the implementation of the interpreter
// (The original book is written in Java, but this implementation is in C# { It was easy to translate Java to C#, because they share syntax and are really similiar})
// The original book can be found at https://craftinginterpreters.com/

// Since this project is based on the Crafting Interpreters book, it is also based on the Lox language, though this project is not just Lox. 

// The project is open source and can be found on GitHub:

// No AI was used in the making of this project (except for the comments)

// The project is licensed under the MIT license, which means you can use it for free, modify it, and distribute it as you wish. However, you must include the original copyright notice and license in any copies or substantial portions of the software.

// - EXTRA NOTES: -

// I know the capitalization is not consistent, but I will fix it in the future. For now, I will keep it as it is, because it is not a big deal.



namespace Kabuk
{
    internal class Program
    {
        static bool hadError = false;  // Flag to indicate if there was an error during execution
        static bool hadRuntimeError = false;  // Flag to indicate if there was a runtime error during execution

        private static readonly Interpreter interpreter = new Interpreter();


        // Entry point of the application
        static void Main(string[] args)
        {

            Console.WriteLine("Kabuk s1.0.0");

            // Check if the user has provided a file path as an argument
            if (args.Length == 0)
            {

                Console.WriteLine("Lütfen kod dosyası giriniz");
                Console.WriteLine("Kullanım: kabuk <dosya yolu>");
            }
            else
            {
                // if the user has provided a file path, read the file and process it

                string filePath = string.Join(",", args); // Join the arguments to form the file path

                if (File.Exists(filePath))
                { // Check if the file exists

                    string fileName = Path.GetFileName(filePath);  // Get the file name from the file path
                    long fileSize = new FileInfo(filePath).Length / 1024 / 1024;  // Get the file size in MB

                    Console.WriteLine("{0} - {1} MB", fileName, fileSize); // Print the file name and size to the console

                    

                    string text = File.ReadAllText(filePath); // Read the content of the file into a string variable
                  Run(text); // Call the Interpret method of the Interpreter class with the file content


                }

            }
        }
       public static void runtimeError(RuntimeError error)
        {
            Console.Error.WriteLine(error.Message +
                "\n[satır " + error.token.line + "]");
            hadRuntimeError = true;
        }

        public static void Run(string code)
        {
            Scanner scanner = new Scanner(code); // Create a new Scanner instance with the provided code
            List<Token> tokens = scanner.ScanTokens(); // Scan the code and get the list of tokens

            Parser parser = new Parser(tokens);
            Expr expression = parser.parse();

            // Stop if there was a syntax error.
            if (hadError) return;

            interpreter.Interpret(expression);

            Console.WriteLine(new AstPrinter().Print(expression));

            if (hadError) Environment.Exit(65);
            hadError = false;
            if (hadRuntimeError) Environment.Exit(70);
            hadRuntimeError = false;
            //Program.ThrowError("NOT IMPLEMENTED."); 
        }

        public static void Error(int line, String message)
        {
            report(line, "", message);
        }

        private static void report(int line, string where,
                                   string message)
        {
            Console.Error.WriteLine(
                "[Satır " + line + "] Hata" + where + ": " + message);
            hadError = true;
        }



    }
    }


