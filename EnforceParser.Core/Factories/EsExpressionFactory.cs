using EnforceParser.Core.Models.Expression;
using EnforceParser.Core.Models.Expression.Operations.Binary;
using EnforceParser.Core.Models.Expression.Operations.Binary.Logical;
using EnforceParser.Core.Models.Expression.Operations.Binary.Relational;
using EnforceParser.Core.Models.Expression.Operations.Common;
using EnforceParser.Core.Models.Expression.Operations.Contextual;
using EnforceParser.Core.Models.Expression.Operations.Unary;
using EnforceParser.Core.Models.Expression.Primary;
using EnforceParser.Core.Models.Expression.Primary.Primitives;

namespace EnforceParser.Core.Factories;

public static class EsExpressionFactory {
    public static IEsExpression Create(Generated.EnforceParser.ExpressionContext ctx) {
        if (ctx.primaryExpression() is { } primary) return Create(primary);
        if (ctx.THIS() is { } @this) return new EsThisExpression();
        if (ctx.SUPER() is { } @super) return new EsSuperExpression();
        if (ctx.expression() is not { } expressions) throw new Exception();
        
        switch (expressions.Length) {
            case 1:
                //Single expression with operator (only one in this case)
                if (ctx.op is { } op) {
                    return op.Text switch {
                        "." => CreateContextChange(ctx),
                        _ => throw new Exception($"Unknown operator '{op.Text}' in expression context.")
                    };
                }

                //Single expression with suffix
                if (ctx.suffix is { } suffix) {
                    return suffix.Text switch {
                        "++" => new EsIncrementExpression(Create(expressions[0]), false),
                        "--" => new EsDecrementExpression(Create(expressions[0]), false),
                        _ => throw new Exception($"Unknown suffix '{suffix.Text}' in expression context.")
                    };
                }

                //Single expression with prefix
                if (ctx.prefix is { } prefix) {
                    return prefix.Text switch {
                        "++" => new EsIncrementExpression(Create(expressions[0]), true),
                        "--" => new EsDecrementExpression(Create(expressions[0]), true),
                        "~" => throw new Exception("probably not working/used in enforce."),
                        "!" => new EsNegationalExpression(Create(expressions[0])),
                        _ => throw new Exception($"Unknown prefix '{prefix.Text}' in expression context.")
                    };
                }
                throw new Exception();
            case 2:
                if (ctx.op is { } @operator) {
                    return @operator.Text switch {
                        "*" => new EsMultiplicationExpression(Create(expressions[0]), Create(expressions[1])),
                        "/" => new EsDivisionExpression(Create(expressions[0]), Create(expressions[1])),
                        "%" => new EsDivisionExpression(Create(expressions[0]), Create(expressions[1])),
                        "++" => throw new Exception("probably not working/used in enforce."),
                        "+" => new EsAdditionExpression(Create(expressions[0]), Create(expressions[1])),
                        "--" => throw new Exception("probably not working/used in enforce."),
                        "-" => new EsSubtractionExpression(Create(expressions[0]), Create(expressions[1])),
                        "<<" => new EsLeftShiftExpression(Create(expressions[0]), Create(expressions[1])),
                        ">>" => new EsRightShiftExpression(Create(expressions[0]), Create(expressions[1])),
                        "<=" => new EsLessThenOrEqualExpression(Create(expressions[0]), Create(expressions[1])),
                        "<" => new EsLessThenExpression(Create(expressions[0]), Create(expressions[1])),
                        ">=" => new EsMoreThenOrEqualExpression(Create(expressions[0]), Create(expressions[1])),
                        ">" => new EsMoreThenExpression(Create(expressions[0]), Create(expressions[1])),
                        "==" => new EsEqualityExpression(Create(expressions[0]), Create(expressions[1])),
                        "!=" => new EsInequalityExpression(Create(expressions[0]), Create(expressions[1])),
                        "~" => new EsBitwiseNotExpression(Create(expressions[0]), Create(expressions[1])),
                        "^" => new EsBitwiseXorExpression(Create(expressions[0]), Create(expressions[1])),
                        "&" => new EsBitwiseAndExpression(Create(expressions[0]), Create(expressions[1])),
                        "&&" => new EsLogicalAndExpression(Create(expressions[0]), Create(expressions[1])),
                        "|" => new EsBitwiseOrExpression(Create(expressions[0]), Create(expressions[1])),
                        "||" => new EsLogicalOrExpression(Create(expressions[0]), Create(expressions[1])),
                        "=" => new EsAssignmentExpression(Create(expressions[0]), Create(expressions[1])),
                        "+=" => new EsAddAssignExpression(Create(expressions[0]), Create(expressions[1])),
                        "-=" => new EsSubtractAssignExpression(Create(expressions[0]), Create(expressions[1])),
                        "*=" => new EsMultiplyAssignExpression(Create(expressions[0]), Create(expressions[1])),
                        "/=" => new EsDivideAssignExpression(Create(expressions[0]), Create(expressions[1])),
                        "|=" => new EsBitwiseOrAssignExpression(Create(expressions[0]), Create(expressions[1])),
                        "&=" => new EsBitwiseAndAssignExpression(Create(expressions[0]), Create(expressions[1])),
                        "<<=" => new EsLeftShiftAssignExpression(Create(expressions[0]), Create(expressions[1])),
                        ">>=" => new EsRightShiftAssignExpression(Create(expressions[0]), Create(expressions[1])),
                        _ => throw new Exception($"Unknown operator '{@operator.Text}' in expression context.")

                    };
                }
                throw new Exception();
        }
        throw new Exception();
    }

    private static IEsPrimaryExpression Create(Generated.EnforceParser.PrimaryExpressionContext ctx) {
        if (ctx.esString is { } esString) return (IEsPrimaryExpression)new EsString().FromParseRule(esString);
        if (ctx.esInt is { } esInt) return (IEsPrimaryExpression)new EsInteger().FromParseRule(esInt);
        if (ctx.esFloat is { } esFloat) return (IEsPrimaryExpression)new EsFloat().FromParseRule(esFloat);
        if (ctx.esBool is { } esBool) return (IEsPrimaryExpression)new EsBoolean().FromParseRule(esBool);
        if (ctx.esArray is { } esArray) return (IEsPrimaryExpression)new EsArray().FromParseRule(esArray);
        if (ctx.esVariable is { } esVariable) new EsVariable().FromParseRule(esVariable);
        if (ctx.esNull is { } esNull) new EsNullType().FromParseRule(esNull);
        if (ctx.esFunction is { } esFunction) new EsFunctionCall().FromParseRule(esFunction);
        if (ctx.esArrayIndex is { } esArrayIndex) new EsArrayIndex().FromParseRule(esArrayIndex);
        if (ctx.parExpression is { } esParExpression) new EsParenthesisedExpression().FromParseRule(esParExpression);
        throw new Exception("The rule you have tried to call is not supported by the serialization base.");
    }

    private static IEsExpression CreateContextChange(Generated.EnforceParser.ExpressionContext ctx) {
        if (ctx.esFunction is { } functionCall) return new EsFunctionContextExpression(Create(ctx.expression()[0]), (EsFunctionCall)new EsFunctionCall().FromParseRule(functionCall));
        if (ctx.esArrayIndex is { } arrayIndex) return new EsArrayIndexContextExpression(Create(ctx.expression()[0]), (EsArrayIndex)new EsArrayIndex().FromParseRule(arrayIndex));
        if (ctx.esVariable is { } variable) return new EsVariableContextExpression(Create(ctx.expression()[0]), (EsVariable)new EsVariable().FromParseRule(variable));
        throw new Exception("The rule you have tried to call is not supported by the serialization base.");
    }
}