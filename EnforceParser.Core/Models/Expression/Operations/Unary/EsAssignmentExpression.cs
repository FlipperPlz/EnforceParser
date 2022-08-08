using System.Text;

namespace EnforceParser.Core.Models.Expression.Operations.Unary; 

public class EsAssignmentExpression : EsUnaryExpression {
    public EsAssignmentExpression(IEsExpression @base, IEsExpression expression) : base(@base, expression) {
    }

    public override string ToEnforce() =>
        new StringBuilder(Base.ToEnforce()).Append(" = ").Append(Expression.ToEnforce()).ToString();
}