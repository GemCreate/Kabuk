namespace Kabuk;

internal abstract class Stmt
{
    internal interface IVisitor<R>
    {
        R VisitExpressionStmt(Expression stmt);
        R VisitPrintStmt(Print stmt);
    }

    internal class Expression : Stmt
    {
        internal Expression(Expr expression)
        {
            this.expression = expression;
        }

        internal override R Accept<R>(IVisitor<R> visitor)
        {
            return visitor.VisitExpressionStmt(this);
        }

        internal readonly Expr expression;
    }

    internal class Print : Stmt
    {
        internal Print(Expr expression)
        {
            this.expression = expression;
        }

        internal override R Accept<R>(IVisitor<R> visitor)
        {
            return visitor.VisitPrintStmt(this);
        }

        internal readonly Expr expression;
    }

    internal abstract R Accept<R>(IVisitor<R> visitor);
}
