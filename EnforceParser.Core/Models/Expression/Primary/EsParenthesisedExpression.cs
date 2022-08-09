using System.Text;
using EnforceParser.Core.Factories;

namespace EnforceParser.Core.Models.Expression.Primary; 

public class EsParenthesisedExpression : IEsDeserializable<Generated.EnforceParser.ParenthesisedExpressionContext> {
    public IEsExpression Expression { get; set; }

    public IEsDeserializable<Generated.EnforceParser.ParenthesisedExpressionContext> FromParseRule(
        Generated.EnforceParser.ParenthesisedExpressionContext ctx) {
        if (ctx.expression() is not { } expression) throw new Exception();
        Expression = EsExpressionFactory.Create(expression);
        return this;
    }
    public override string ToString() => ToEnforce();
    public string ToEnforce() => new StringBuilder("(").Append(Expression.ToEnforce()).Append(')').ToString();
}