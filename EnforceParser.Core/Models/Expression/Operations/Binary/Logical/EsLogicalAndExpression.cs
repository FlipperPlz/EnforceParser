using System.Text;

namespace EnforceParser.Core.Models.Expression.Operations.Binary.Logical; 

public class EsLogicalAndExpression : EsBinaryExpression{
    public EsLogicalAndExpression(IEsExpression left, IEsExpression right) : base(left, right) {
    }

    public override string ToEnforce() => new StringBuilder(Left.ToEnforce()).Append(" && ").Append(Right.ToEnforce()).ToString();
}