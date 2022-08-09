using System.Text;
using EnforceParser.Core.Factories;
using EnforceParser.Core.Models.Expression;
using EnforceParser.Core.Models.Expression.Primary.Primitives;
using EnforceParser.Core.Models.Scope;

namespace EnforceParser.Core.Models; 

public class EsVariableDeclarator : IEsDeserializable<Generated.EnforceParser.VariableDeclaratorContext> {
    public EsVariableDeclaratorName VariableName { get; set; }
    public IEsExpression? VariableValue { get; set; } = null;
    
    public IEsDeserializable<Generated.EnforceParser.VariableDeclaratorContext> FromParseRule(Generated.EnforceParser.VariableDeclaratorContext ctx) {
        if (ctx.variableName is not { }) throw new Exception();
        IEsExpression? arrBounds = null;
        if(ctx.arrayLength is { } val) arrBounds = EsExpressionFactory.Create(val);
        if (ctx.variableValue is not null) VariableValue = EsExpressionFactory.Create(ctx.variableValue);
        VariableName = new EsVariableDeclaratorName((EsVariableName) new EsVariableName().FromParseRule(ctx.variableName), ctx.LSBracket() is not null, arrBounds);
        return this;
    }
    public override string ToString() => ToEnforce();
    public string ToEnforce() {
        var builder = new StringBuilder(VariableName.ToEnforce());
        if (VariableValue is not null) builder.Append(" = ").Append(VariableValue.ToEnforce());
        return builder.ToString();
    }
}