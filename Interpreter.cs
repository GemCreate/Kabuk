using System;
using System.Collections.Generic;
using System.Text;

namespace Kabuk
{
    internal class Interpreter
    {
        static bool hadError = false; // Flag to track if an error has occurred

        public void Interpret(string code)
        {
            Scanner scanner = new Scanner(code); // Create a new Scanner instance with the provided code
            List<Token> tokens = scanner.ScanTokens(); // Scan the code and get the list of tokens

            foreach (Token token in tokens)
            {
                Console.WriteLine(token.toString());

            }
            if (hadError) Environment.Exit(65);
            hadError = false;

            //Program.ThrowError("NOT IMPLEMENTED."); 
        }

        public static void Error(int line, string message)
        {
            // Report an error with the specified line number and message
            report(line, "", message);
        }

        private static void report(int line, string where, string message)
        {
            // Print the error message to the console with the line number and additional information
            Console.WriteLine( "[line " + line + "] Error" + where + ": " + message);
            hadError = true;
        }

    }
}
