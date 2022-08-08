using System.Text;
using EnforceParser.Core.Factories;
using EnforceParser.Core.Models.Expression;

namespace EnforceParser.Core.Models.Statements; 

public class EsReturnStatement : IEsStatement, IEsDeserializable<Generated.EnforceParser.ReturnStatementContext> {
    public IEsExpression? Expression { get; set; } = null;
    
    public IEsDeserializable<Generated.EnforceParser.ReturnStatementContext> FromParseRule(Generated.EnforceParser.ReturnStatementContext ctx) {
        if (ctx.expression() is { } expression) Expression = EsExpressionFactory.Create(expression);
        return this;
    }

    public string ToEnforce() => new StringBuilder("return")
        .Append(Expression is null ? ";" : new StringBuilder(" ").Append(Expression.ToEnforce()).Append(';').ToString())
        .ToString();
}