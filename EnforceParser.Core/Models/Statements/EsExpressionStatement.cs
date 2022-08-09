using System.Text;
using EnforceParser.Core.Factories;
using EnforceParser.Core.Models.Expression;

namespace EnforceParser.Core.Models.Statements; 

public class EsExpressionStatement : IEsStatement, IEsDeserializable<Generated.EnforceParser.ExpressionContext> {
    public IEsExpression Expression { get; set; }
    
    public IEsDeserializable<Generated.EnforceParser.ExpressionContext> FromParseRule(Generated.EnforceParser.ExpressionContext ctx) {
        Expression = EsExpressionFactory.Create(ctx);
        return this;
    }
    public override string ToString() => ToEnforce();
    public string ToEnforce() => new StringBuilder(Expression.ToString()).Append(';').ToString();
}