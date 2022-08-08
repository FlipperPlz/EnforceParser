using System.Text;

namespace EnforceParser.Core.Models.Expression.Operations.Binary.Relational; 

public class EsLessThenOrEqualExpression : EsBinaryExpression {
    public EsLessThenOrEqualExpression(IEsExpression left, IEsExpression right) : base(left, right) { }

    public override string ToEnforce() =>
        new StringBuilder(Left.ToEnforce()).Append(" <= ").Append(Right.ToEnforce()).ToString();
}