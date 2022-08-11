using System.Text;

namespace EnforceParser.Core.Models.Expression.Operations.Common; 

public class EsNegationalBitwiseExpression : IEsExpression {
    public IEsExpression Expression { get; set; }
    public EsNegationalBitwiseExpression(IEsExpression expression) => Expression = expression;
    public string ToEnforce() => new StringBuilder("~").Append(Expression).ToString();
    public override string ToString() => ToEnforce();

}