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

namespace Kabuk
{
    internal class Program
    {

        

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

                    Interpreter interpreter = new Interpreter(); // Create an instance of the Interpreter class

                    string text = File.ReadAllText(filePath); // Read the content of the file into a string variable
                    interpreter.Interpret(text); // Call the Interpret method of the Interpreter class with the file content


                }

            }
        }

            public static void ThrowError(string message)
            {
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Hata: " + message);
                Console.ResetColor();
                Environment.Exit(14);

            }



        }
    }


