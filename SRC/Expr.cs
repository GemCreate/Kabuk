using System;
using System.Collections.Generic;
using System.Text;

namespace Kabuk
{
    abstract class Expr
    {
         class Binary : Expr
        {
            public Expr left;
            public Token operatorToken;
            public Expr right;
            public Binary(Expr left, Token operatorToken, Expr right)
            {
                this.left = left;
                this.operatorToken = operatorToken;
                this.right = right;
            }
        }



    }
}
