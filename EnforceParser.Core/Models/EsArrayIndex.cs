using System.Text;
using EnforceParser.Core.Factories;
using EnforceParser.Core.Models.Expression;

namespace EnforceParser.Core.Models; 

public class EsArrayIndex : IEsDeserializable<Generated.EnforceParser.ArrayIndexContext> {
    public IEsExpression? Index { get; set; } = null;
    
    public IEsDeserializable<Generated.EnforceParser.ArrayIndexContext> FromParseRule(Generated.EnforceParser.ArrayIndexContext ctx) {
        if(ctx.expression() is { } expression) Index = EsExpressionFactory.Create(expression);
        return this;
    }

    public string ToEnforce() {
        var builder = new StringBuilder("[");
        if (Index is not null) builder.Append(Index.ToEnforce());
        return builder.Append(']').ToString();
    }
}