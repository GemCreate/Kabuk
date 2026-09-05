namespace Kabuk;

internal abstract class Expr
{
    internal interface IVisitor<R>
    {
        R VisitBinaryExpr(Binary expr);
        R VisitGroupingExpr(Grouping expr);
        R VisitLiteralExpr(Literal expr);
        R VisitUnaryExpr(Unary expr);
    }

    internal class Binary : Expr
    {
        internal Binary(Expr left, Token oprtr, Expr right)
        {
            this.left = left;
            this.oprtr = oprtr;
            this.right = right;
        }

        internal override R Accept<R>(IVisitor<R> visitor)
        {
            return visitor.VisitBinaryExpr(this);
        }

        internal readonly Expr left;
        internal readonly Token oprtr;
        internal readonly Expr right;
    }

    internal class Grouping : Expr
    {
        internal Grouping(Expr expression)
        {
            this.expression = expression;
        }

        internal override R Accept<R>(IVisitor<R> visitor)
        {
            return visitor.VisitGroupingExpr(this);
        }

        internal readonly Expr expression;
    }

    internal class Literal : Expr
    {
        internal Literal(object value)
        {
            this.value = value;
        }

        internal override R Accept<R>(IVisitor<R> visitor)
        {
            return visitor.VisitLiteralExpr(this);
        }

        internal readonly object value;
    }

    internal class Unary : Expr
    {
        internal Unary(Token oprtr, Expr right)
        {
            this.oprtr = oprtr;
            this.right = right;
        }

        internal override R Accept<R>(IVisitor<R> visitor)
        {
            return visitor.VisitUnaryExpr(this);
        }

        internal readonly Token oprtr;
        internal readonly Expr right;
    }

    internal abstract R Accept<R>(IVisitor<R> visitor);
}
