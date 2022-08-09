using System.Text;
using EnforceParser.Core.Factories;
using EnforceParser.Core.Models.Expression;

namespace EnforceParser.Core.Models.Statements; 

public class EsDeleteStatement : IEsStatement, IEsDeserializable<Generated.EnforceParser.DeleteStatementContext> {
    public IEsExpression Expression { get; set; }
    public IEsDeserializable<Generated.EnforceParser.DeleteStatementContext> FromParseRule(Generated.EnforceParser.DeleteStatementContext ctx) {
        if (ctx.expression() is not { } expression) throw new Exception();
        Expression = EsExpressionFactory.Create(expression);
        return this;
    }
    public override string ToString() => ToEnforce();
    public string ToEnforce() => new StringBuilder("delete ").Append(Expression.ToEnforce()).Append(';').ToString();
}