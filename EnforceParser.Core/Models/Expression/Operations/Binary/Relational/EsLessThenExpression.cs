using System.Text;

namespace EnforceParser.Core.Models.Expression.Operations.Binary.Relational; 

public class EsLessThenExpression : EsBinaryExpression {
    public EsLessThenExpression(IEsExpression left, IEsExpression right) : base(left, right) { }

    public override string ToEnforce() =>
        new StringBuilder(Left.ToEnforce()).Append(" < ").Append(Right.ToEnforce()).ToString();
}