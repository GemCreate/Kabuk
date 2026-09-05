using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Kabuk
{
    internal class Interpreter : Expr.IVisitor<object>
    {


        public object VisitLiteralExpr(Expr.Literal expr)
        {
            return expr.value;
        }

        public object VisitGroupingExpr(Expr.Grouping expr)
        {
            return evaluate(expr.expression);
        }

        private object evaluate(Expr expr)
        {
            return expr.Accept(this);
        }
        public object VisitUnaryExpr(Expr.Unary expr)
        {
            object right = evaluate(expr.right);

            switch (expr.oprtr.type) {
                case TokenType.MINUS:
                    checkNumberOperand(expr.oprtr, right);
                    return -(double)right;

                case TokenType.BANG:
                    return !isTruthy(right);
            }


            return null;
        }

      private bool isTruthy(object obj)
        {
            if (obj == null) return false;
            if (obj is bool) return (bool)obj;
            return true;
        }

        public object VisitBinaryExpr(Expr.Binary expr)
        {
            object left = evaluate(expr.left);
            object right = evaluate(expr.right);

            switch (expr.oprtr.type) {
                case TokenType.GREATER:
                    checkNumberOperands(expr.oprtr, left, right);
                    return (double)left > (double)right;
                case TokenType.GREATER_EQUAL:
                    checkNumberOperands(expr.oprtr, left, right);
                    return (double)left >= (double)right;
                case TokenType.LESS:
                    checkNumberOperands(expr.oprtr, left, right);
                    return (double)left < (double)right;
                case TokenType.LESS_EQUAL:
                    checkNumberOperands(expr.oprtr, left, right);
                    return (double)left <= (double)right;

                case TokenType.MINUS:
                    checkNumberOperands(expr.oprtr, left, right);
                    return (double)left - (double)right;
                case TokenType.PLUS:
                 
                    if (left is double && right is double) {
                        return (double)left + (double)right;
                    }

                    if (left is string && right is string) {
                        return (string)left + (string)right;
                    }

                    break;

                case TokenType.SLASH:
                    checkNumberOperands(expr.oprtr, left, right);
                    return (double)left / (double)right;
                case TokenType.STAR:
                    checkNumberOperands(expr.oprtr, left, right);
                    return (double)left * (double)right;
                case TokenType.BANG_EQUAL: return !isEqual(left, right);
                case TokenType.EQUAL_EQUAL: return isEqual(left, right);
            }

                // Unreachable.
                return null;
            }
        private void checkNumberOperands(Token oprtr,
                                 object left, object right)
        {
            if (left is double && right is double) return;

            throw new RuntimeError(oprtr, "İşlenen bir sayı olmalıdır. ");
        }

        private bool isEqual(object a, object b)
        {
            if (a == null && b == null) return true;
            if (a == null) return false;

            return a.Equals(b);
        }
        private void checkNumberOperand(Token oprtr, object operand)
        {
            if (operand is double) return;
            throw new RuntimeError(oprtr, "İşlenen bir sayı olmalıdır. ");
        }
        public void Interpret(Expr expression)
        {
            try
            {
                object value = evaluate(expression);
                Console.WriteLine(stringify(value));
            }
            catch (RuntimeError error)
            {
                Program.runtimeError(error);
            }
        }

        private string stringify(object obj)
        {
            if (obj == null) return "nil";

            if (obj is double) {
                string text = obj.ToString();
                if (text.EndsWith(".0"))
                {
                    text = text.Substring(0, text.Length - 2);
                }
                return text;
            }

            return obj.ToString();
        }
    }
    }
