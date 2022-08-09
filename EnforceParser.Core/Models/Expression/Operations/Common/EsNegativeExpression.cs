using System.Text;
using EnforceParser.Core.Models.Expression.Primary;

namespace EnforceParser.Core.Models.Expression.Operations.Common; 

public class EsNegativeExpression : IEsPrimaryExpression {
    public IEsExpression Expression { get; set; }
    public EsNegativeExpression(IEsExpression expression) => Expression = expression;
    public string ToEnforce() => new StringBuilder("-").Append(Expression.ToEnforce()).ToString();
    public override string ToString() => ToEnforce();

}