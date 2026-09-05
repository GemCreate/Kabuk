using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq.Expressions;
using System.Text;


namespace Kabuk
{
    internal class Parser
    {
	private List<Token> tokens;
	private int current = 0;

        private Expr expression()
        {

            return equality();

        }

        private Expr equality()
        {
            Expr expr = comparison();

            while (match(TokenType.BANG_EQUAL, TokenType.EQUAL_EQUAL))
            {
                Token operatorToken = previous();
                Expr right = comparison();
                expr = new Expr.Binary(expr, operatorToken, right);
            }

            return expr;
        }

        private bool match(params TokenType[] types)
        {
            foreach (TokenType type in types)
            {
                if (check(type))
                {
                    advance();
                    return true;
                }
            }
            return false;
        }

        private bool check(TokenType type)
        {
            if (isAtEnd()) return false;
            return peek().type == type;
        }

        private Token advance()
        {
            if (!isAtEnd()) current++;
            return previous();
        }

        private bool isAtEnd()
        {
            return peek().type == TokenType.EOF;
        }

        private Token peek()
        {
            return tokens[current];
        }

        private Token previous()
        {
            return tokens[current - 1];
        }

        private Expr comparison()
        {
            Expr expr = term();

            while(match(TokenType.GREATER, TokenType.GREATER_EQUAL, TokenType.LESS, TokenType.LESS_EQUAL))
            {
                Token operatorToken = previous();
                Expr right = term();
                expr = new Expr.Binary(expr, operatorToken, right);
            }

            return expr;
        }
        private Expr term()
        {
            Expr expr = factor();

            while (match(TokenType.MINUS, TokenType.PLUS))
            {
                Token oprtr = previous();
                Expr right = factor();
                expr = new Expr.Binary(expr, oprtr, right);
            }

            return expr;
        }

        private Expr factor()
        {
            Expr expr = unary();

            while (match(TokenType.SLASH, TokenType.STAR))
            {
                Token oprtr = previous();
                Expr right = unary();
                expr = new Expr.Binary(expr, oprtr, right);
            }

            return expr;
        }

        private Expr unary()
        {
            if (match(TokenType.BANG, TokenType.MINUS))
            {
                Token oprtr = previous();
                Expr right = unary();
                return new Expr.Unary(oprtr, right);
            }

            return primary();
        }

        private Expr primary()
        {
         
            if (match(TokenType.FALSE)) return new Expr.Literal(false);
           else  if (match(TokenType.TRUE)) return new Expr.Literal(true);
            else if (match(TokenType.NIL)) return new Expr.Literal(null);

            else if (match(TokenType.NUMBER, TokenType.STRING))
            {
                return new Expr.Literal(previous().literal);
            }

            else if (match(TokenType.LEFT_PAREN))
            {
                Expr expr = expression();
                consume(TokenType.RIGHT_PAREN, "İfadeden sonra ')' bekleniyor.");
                return new Expr.Grouping(expr);
            }
            else
            {
                throw error(peek(), "İfade bekleniyor.");
            }
      

          
        }

        private Token consume(TokenType type, string message)
        {
            if (check(type)) return advance();
            else
            {
               throw error(peek(), message);
              
            }
        }
        private ParseError error(Token token, string message)
        {
            Error(token, message);
            return new ParseError();
        }
        static void Error(Token token, string message)
        {
            if (token.type == TokenType.EOF)
            {
                Program.Error(token.line, " sonunda" + message);
            }
            else
            {
                Program.Error(token.line, " burada -> '" + token.lexeme + "'" + message);
            }
        }

        private void synchronize()
        {
            advance();

            while (!isAtEnd())
            {
                if (previous().type == TokenType.SEMICOLON) return;

                switch (peek().type)
                {
                    case TokenType.CLASS:
                    case TokenType.FUN:
                    case TokenType.VAR:
                    case TokenType.FOR:
                    case TokenType.IF:
                    case TokenType.WHILE:
                    case TokenType.PRINT:
                    case TokenType.RETURN:
                        return;
                }

                advance();
            }
        }

        public Expr parse()
        {
            try
            {
                return expression();
            }
            catch (ParseError error)
            {
                return null;
            }
        }


        //private Expression Equality()
        //{
        //	Expression expr = comparison();

        //	while (match(TokenType.BANG_EQUAL, EQUAL_EQUAL))
        //	{

        //		Token operator = previous();
        //		Expression right = comparison();
        //		// expr = new 

        //		// Will continue later 	
        //		// 04/09/2026

        // I'll just rewrite this part

        //	}

        //}

        public Parser(List<Token> tokens) {

	this.tokens = tokens;
	}

    }

    internal class ParseError : Exception {
    
    }

}
