using System;
using System.Collections.Generic;
using System.Text;

namespace Kabuk
{
    internal class Token
    {
     readonly public   TokenType type; // The type of the token
        readonly public string lexeme; // The actual string representation of the token
        readonly public object literal; // The literal value associated with the token (if any)
        readonly public int line; // The line number where the token was found

        public Token(TokenType type, string lexeme, object literal, int line)
        {
            this.type = type;
            this.lexeme = lexeme;
            this.literal = literal;
            this.line = line;

            // Initialize the token with its type, lexeme, literal value, and line number

        }

        public string toString()
        {
            return type + " " + lexeme + " " + literal;
            // Return a string representation of the token, including its type, lexeme, and literal value

        }

    }
}
