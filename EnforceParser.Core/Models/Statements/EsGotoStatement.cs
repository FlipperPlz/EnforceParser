using System.Text;
using EnforceParser.Core.Factories;
using EnforceParser.Core.Models.Expression;

namespace EnforceParser.Core.Models.Statements; 

public class EsGotoStatement : IEsStatement, IEsDeserializable<Generated.EnforceParser.GotoStatementContext> {
    public IEsExpression Expression { get; set; }
    
    public IEsDeserializable<Generated.EnforceParser.GotoStatementContext> FromParseRule(Generated.EnforceParser.GotoStatementContext ctx) {
        if (ctx.expression() is not { } expression) throw new Exception();
        Expression = EsExpressionFactory.Create(expression);
        return this;
    }

    public string ToEnforce() => new StringBuilder("goto ").Append(Expression.ToEnforce()).Append(';').ToString();
}