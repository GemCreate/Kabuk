using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Kabuk
{
    internal class Parser
    {
	private List<Token> tokens;
	private int current = 0;

		//private Expression expression()
		//{

		//	return equality();

		//}

  //      private Expr Equality()
  //      {
  //          Expr expr = Comparison();

  //          while (Match(TokenType.BangEqual, TokenType.EqualEqual))
  //          {
  //              Token operatorToken = Previous();
  //              Expr right = Comparison();
  //              expr = new Expr.Binary(expr, operatorToken, right);
  //          }

  //          return expr;
  //      }


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



}
