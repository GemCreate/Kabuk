namespace Kabuk;

// NOT GONNA USE THE AST GENERATOR FOR THIS CUZ C# != JAVA


internal abstract class Stmt
{
    public interface IVisitor
    {
        void VisitExpressionStmt(Expression stmt);
        void VisitPrintStmt(Print stmt);
        void VisitVarStmt(Var stmt);
    }

    public abstract void Accept(IVisitor visitor);

    public class Expression : Stmt
    {
        public readonly Expr Expr;          
        public Expression(Expr expr) => Expr = expr;
        public override void Accept(IVisitor visitor) => visitor.VisitExpressionStmt(this);
    }

    public class Print : Stmt
    {
        public readonly Expr Expression;
        public Print(Expr expression) => Expression = expression;
        public override void Accept(IVisitor visitor) => visitor.VisitPrintStmt(this);
    }
    public class Var : Stmt
    {
        public readonly Token Name;
        public readonly Expr Initializer;
        public Var(Token name, Expr initializer) {
         Name = name; 
            Initializer = initializer;
         }
        public override void Accept(IVisitor visitor)
        {
            visitor.VisitVarStmt(this);
        }
    }
}
