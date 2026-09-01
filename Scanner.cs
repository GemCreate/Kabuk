using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Kabuk
{
    internal class Scanner
    {

        // The starting index of the current lexeme being scanned

        private int start = 0;
        private int current = 0;
        private int line = 1;


        private List<Token> tokens = new List<Token>();   // List to hold the tokens generated during scanning


        // Check if the scanner has reached the end of the source code

        bool isAtEnd()
        {
            return current >= Source.Length;
        }

        string Source; // The source code to be scanned

        private char peek()
        {
            if (isAtEnd()) return '\0';
            return Source[current];
            // Look at the current character without consuming it
        }

        public Scanner(string source) {
            Source = source;
            // Initialize the scanner with the provided source code
        }

        public List<Token> ScanTokens()
        {
            while (!isAtEnd())
            {
                // We are at the beginning of the next lexeme.
                start = current;
                scanToken();
            }

            tokens.Add(new Token(TokenType.EOF, "", null, line));
            // Add an end-of-file token to indicate the end of the source code
            return tokens;
        }

        private void scanToken()
        {
            char c = advance();
            switch (c)
            {
                // Handle single-character tokens and operators

              
                    
                case '(': addToken(TokenType.LEFT_PAREN); break;
                case ')': addToken(TokenType.RIGHT_PAREN); break;
                case '{': addToken(TokenType.LEFT_BRACE); break;
                case '}': addToken(TokenType.RIGHT_BRACE); break;
                case ',': addToken(TokenType.COMMA); break;
                case '.': addToken(TokenType.DOT); break;
                case '-': addToken(TokenType.MINUS); break;
                case '+': addToken(TokenType.PLUS); break;
                case ';': addToken(TokenType.SEMICOLON); break;
                case '*': addToken(TokenType.STAR); break;
                case '!':
                    addToken(match('=') ? TokenType.BANG_EQUAL : TokenType.BANG);
                    break;
                case '=':
                    addToken(match('=') ? TokenType.EQUAL_EQUAL : TokenType.EQUAL);
                    break;
                case '<':
                    addToken(match('=') ? TokenType.LESS_EQUAL : TokenType.LESS);
                    break;
                case '>':
                    addToken(match('=') ? TokenType.GREATER_EQUAL : TokenType.GREATER);
                    break;
                case '/':
                    if (match('/'))
                    {
                        // A comment goes until the end of the line.
                        while (peek() != '\n' && !isAtEnd()) advance();
                    }
                    else
                    {
                        addToken(TokenType.SLASH);
                    }
                    break;

                // meaningless characters like whitespace and newlines are ignored

                case ' ':
                case '\r':
                case '\t':
                    // Ignore whitespace.
                    break;

                case '\n':
                    line++;
                    break;

                case '"': Cstring(); break;

                default: // Handle unexpected characters and report errors

                    if (isDigit(c)) // Check if the character is a digit
                    {
                        number();
                    }
                    else if (isAlpha(c)) // Check if the character is an alphabetic character
                    {
                        identifier();
                    }
                    else
                    {
                        Interpreter.Error(line, "Beklenmedik karakter.");
                        // If the character doesn't match any known token, report an error
                    }
                    break;

            }
        }
        private bool isAlpha(char c)
        {
            return (c >= 'a' && c <= 'z') ||
                   (c >= 'A' && c <= 'Z') ||
                    c == '_';
        }

        private bool isAlphaNumeric(char c)
        {
            return isAlpha(c) || isDigit(c);
        }

        private void identifier()
        {
            while (isAlphaNumeric(peek())) advance();

            addToken(TokenType.IDENTIFIER);
        }
        private char peekNext()
        {
            if (current + 1 >= Source.Length) return '\0';
            return Source[current + 1];
        }

        private void number()
        {
            while (isDigit(peek())) advance();

            // Look for a fractional part.
            if (peek() == '.' && isDigit(peekNext()))
            {
                // Consume the "."
                advance();

                while (isDigit(peek())) advance();
            }

            addToken(TokenType.NUMBER,
                Double.Parse(Source[start..current]));
        }

        private bool isDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        private void Cstring () {
            while (peek() != '"' && !isAtEnd()) {
                if (peek() == '\n') line++;
                    advance();
                }

            if (isAtEnd()) {
             Interpreter.Error(line, "Sonlandırılmamış ip.");
             return;
             }

            // The closing ".
            advance();

               // Trim the surrounding quotes.
            string value = Source.Substring(start + 1, current - 1 - start);
           addToken(TokenType.STRING, value);
        }
        private bool match(char expected)
        {
            if (isAtEnd()) return false;
            if (Source[current] != expected) return false;

            current++;
            return true;
        }

        private char advance()
        {
            return Source[current++];
        }

        private void addToken(TokenType type)
        {
            addToken(type, null);
        }

        private void addToken(TokenType type, object literal)
        {
            string text = Source.Substring(start, current - start);
            tokens.Add(new Token(type, text, literal, line));
        }

        private static Dictionary<string, TokenType> keywords()
        {
            return new Dictionary<string, TokenType>
            {
                { "ve", TokenType.AND },
                { "sınıf", TokenType.CLASS },
                { "yoksa", TokenType.ELSE },
                { "yanlış", TokenType.FALSE },
                { "için", TokenType.FOR },
                { "fonksiyon", TokenType.FUN },
                { "eğer", TokenType.IF },
                { "yok", TokenType.NIL },
                { "ya da", TokenType.OR },
                { "yaz", TokenType.PRINT },
                { "ver", TokenType.RETURN },
                { "süper", TokenType.SUPER },
                { "bu", TokenType.THIS },
                { "doğru", TokenType.TRUE },
                { "değisken", TokenType.VAR },
                { "olurken", TokenType.WHILE }
            };
        }

        

    }
}
