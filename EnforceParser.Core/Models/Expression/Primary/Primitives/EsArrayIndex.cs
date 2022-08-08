﻿using System.Text;
using EnforceParser.Core.Factories;

namespace EnforceParser.Core.Models.Expression.Primary.Primitives; 

public class EsArrayIndex : IEsPrimaryExpression, IEsDeserializable<Generated.EnforceParser.ArrayIndexExpressionContext> {
    public EsVariable Array { get; set; }
    public IEsExpression Index { get; set; }
    
    public IEsDeserializable<Generated.EnforceParser.ArrayIndexExpressionContext> FromParseRule(Generated.EnforceParser.ArrayIndexExpressionContext ctx) {
        if (ctx.arrayIndex() is not { } indexContext || indexContext.expression() is not { } expressionContext || ctx.identifier() is not { } arr) throw new Exception();
        Array = (EsVariable) new EsVariable().FromParseRule(ctx.identifier());
        Index = EsExpressionFactory.Create(expressionContext);
        return this;
    }

    public string ToEnforce() => new StringBuilder(Array.ToEnforce()).Append('[').Append(Index.ToEnforce()).Append(']')
        .ToString();
}