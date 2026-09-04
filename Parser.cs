using System;
using System.Collections.Generic;
using System.Text;

namespace Kabuk
{
    internal class Parser
    {
	private List<Token> tokens;
	private int current = 0;

	/* private Expression expression() {
	
	return equality();
	
	}	

	private Expression equality() {
	Expression expr = comparison();

	while (match(TokenType.BANG_EQUAL, EQUAL_EQUAL)) {
	
	Token operator = previous();
	Expression right = comparison();
	// expr = new 

	// Will continue later 	
	// 04/09/2026
	}
	
	} */

	public Parser(List<Token> tokens) {

	this.tokens = tokens;
	}

    }
}
