using System.Text;

namespace EnforceParser.Core.Models.Expression.Operations.Common; 

public class EsIncrementExpression : IEsExpression {
    public IEsExpression Expression { get; set; }
    public bool Prefix { get; set; } = false;

    public EsIncrementExpression(IEsExpression expression, bool prefix) {
        Expression = expression;
        Prefix = prefix;
    }
    public override string ToString() => ToEnforce();

    public string ToEnforce() => new StringBuilder().Append(Prefix ? "++" : Expression.ToEnforce())
        .Append(Prefix ? Expression.ToEnforce() : "++").ToString();
}