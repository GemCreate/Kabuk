using System;
using System.Collections.Generic;
using System.Text;

namespace Kabuk
{
    internal class AstPrinter : Expr.IVisitor<string>
    {
        public string Print(Expr expr)
        {
            return expr.Accept(this); // Calls the accept method on the expression, passing this AstPrinter as the visitor

        }

        public string VisitBinaryExpr(Expr.Binary expr)
        {
            return Parenthesize(expr.oprtr.lexeme, expr.left, expr.right); // Calls the parenthesize method to format the binary expression
        }

        public string VisitGroupingExpr(Expr.Grouping expr)
        {
            return Parenthesize("group", expr.expression); // Calls the parenthesize method to format the grouping expression
        }

 
         public string VisitLiteralExpr(Expr.Literal expr) // Calls the parenthesize method to format the literal expression
        {
            if (expr.value == null) return "nil";
            return expr.value.ToString();
        }

     
        public string VisitUnaryExpr(Expr.Unary expr) // Calls the parenthesize method to format the unary expression
        {
            return Parenthesize(expr.oprtr.lexeme, expr.right);
        }

        string Parenthesize(string name, params Expr[] exprs) {

            StringBuilder builder = new StringBuilder();

            builder.Append("(").Append(name);
            foreach (Expr e in exprs) { 
            builder.Append(" ");
                builder.Append(e.Accept(this)); // Calls the accept method on each expression, passing this AstPrinter as the visitor
            }
            builder.Append(")");

            return builder.ToString();
        }


    }
}
