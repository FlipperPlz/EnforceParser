using System.Text;
using EnforceParser.Core.Models.Expression.Primary;

namespace EnforceParser.Core.Models.Expression.Operations.Common; 

public class EsPositiveExpression : IEsPrimaryExpression {
    public IEsExpression Expression { get; set; }
    public EsPositiveExpression(IEsExpression expression) => Expression = expression;
    public string ToEnforce() => new StringBuilder("+").Append(Expression.ToEnforce()).ToString();
    public override string ToString() => ToEnforce();
}