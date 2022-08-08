using EnforceParser.Core.Models.Expression;
using EnforceParser.Core.Models.Expression.Primary;
using EnforceParser.Core.Models.Expression.Primary.Primitives;

namespace EnforceParser.Core.Factories; 

public static class EsExpressionFactory {
    public static IEsExpression Create(Generated.EnforceParser.ExpressionContext ctx) {
        if (ctx.primaryExpression() is { } primaryExpression) return Create(primaryExpression);
        throw new NotImplementedException();
    }

    private static IEsPrimaryExpression Create(Generated.EnforceParser.PrimaryExpressionContext ctx) {
        if (ctx.esString is { } esString) return (IEsPrimaryExpression) new EsString().FromParseRule(esString);
        if (ctx.esInt is { } esInt) return (IEsPrimaryExpression) new EsInteger().FromParseRule(esInt);
        if (ctx.esFloat is { } esFloat) return (IEsPrimaryExpression) new EsFloat().FromParseRule(esFloat);
        if (ctx.esBool is { } esBool) return (IEsPrimaryExpression) new EsBoolean().FromParseRule(esBool);
        if (ctx.esArray is { } esArray) return (IEsPrimaryExpression) new EsArray().FromParseRule(esArray);
        if (ctx.esVariable is { } esVariable) new EsVariable().FromParseRule(esVariable);
        if (ctx.esNull is { } esNull) new EsNullType().FromParseRule(esNull);
        if (ctx.esFunction is { } esFunction) new EsFunctionCall().FromParseRule(esFunction);
        if (ctx.esArrayIndex is { } esArrayIndex) new EsArrayIndex().FromParseRule(esArrayIndex);
        if (ctx.parExpression is { } esParExpression) new EsParenthesisedExpression().FromParseRule(esParExpression);
        throw new NotImplementedException();
    }
}