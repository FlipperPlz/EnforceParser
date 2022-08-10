using System.Text;
using EnforceParser.Core.Factories;
using EnforceParser.Core.Models.Scope;

namespace EnforceParser.Core.Models.Expression; 

public class EsCastedExpression : IEsExpression, IEsDeserializable<Generated.EnforceParser.CastExpressionContext> {
    public EsClassReference CastTo;
    public IEsExpression ToCast;
    public IEsDeserializable<Generated.EnforceParser.CastExpressionContext> FromParseRule(Generated.EnforceParser.CastExpressionContext ctx) {
        if (ctx.cast is null) throw new Exception();
        if(ctx.expression() is null ) throw new Exception();

        CastTo = (EsClassReference)new EsClassReference().FromParseRule(ctx.cast);
        ToCast = EsExpressionFactory.Create(ctx.expression());
        return this;
    }

    public string ToEnforce() => new StringBuilder("(").Append(CastTo.ToEnforce()).Append(") ")
        .Append(ToCast.ToEnforce()).ToString();
}