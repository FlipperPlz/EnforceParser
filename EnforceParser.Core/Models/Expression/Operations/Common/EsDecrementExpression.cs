using System.Text;

namespace EnforceParser.Core.Models.Expression.Operations.Common; 

public class EsDecrementExpression : IEsExpression {
    public IEsExpression Expression { get; set; }
    public bool Prefix { get; set; } = false;

    public EsDecrementExpression(IEsExpression expression, bool prefix) {
        Expression = expression;
        Prefix = prefix;
    }

    public string ToEnforce() => new StringBuilder().Append(Prefix ? "--" : Expression.ToEnforce())
        .Append(Prefix ? Expression.ToEnforce() : "--").ToString();
}