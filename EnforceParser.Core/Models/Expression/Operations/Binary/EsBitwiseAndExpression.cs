using System.Text;

namespace EnforceParser.Core.Models.Expression.Operations.Binary; 

public class EsBitwiseAndExpression : EsBinaryExpression {
    public EsBitwiseAndExpression(IEsExpression left, IEsExpression right) : base(left, right) { }

    public override string ToEnforce() => new StringBuilder(Left.ToEnforce()).Append(" & ").Append(Right.ToEnforce()).ToString();
}