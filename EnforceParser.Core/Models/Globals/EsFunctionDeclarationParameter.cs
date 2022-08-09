using System.Text;
using EnforceParser.Core.Factories;
using EnforceParser.Core.Models.Expression;
using EnforceParser.Core.Models.Expression.Primary.Primitives;
using EnforceParser.Core.Models.Generics;
using EnforceParser.Core.Models.Modifiers;
using EnforceParser.Core.Models.Scope;

namespace EnforceParser.Core.Models; 

public class EsFunctionDeclarationParameter : IEsDeserializable<Generated.EnforceParser.FunctionParameterContext> {
    public List<EsVariableModifier> ParameterModifiers { get; set; } = new();
    public EsClassReference ParameterType { get; set; }
    public EsVariableName ParameterName { get; set; }
    public IEsExpression? ParameterValue { get; set; }
    
    public IEsDeserializable<Generated.EnforceParser.FunctionParameterContext> FromParseRule(Generated.EnforceParser.FunctionParameterContext ctx) {
        if (ctx.variableModifier() is { } variableModifiers) {
            foreach (var modifierCtx in variableModifiers) {
                var modifierText = string.Concat(modifierCtx.GetText()[0].ToString().ToUpper(), modifierCtx.GetText().AsSpan(1));
                if (Enum.TryParse(modifierText, out EsVariableModifier modifier)) ParameterModifiers.Add(modifier);
                else throw new Exception($"Failed to parse variable identifier from \"{modifierText}\".");
            }
        }
        if (ctx.parameterType is not { } parameterType) throw new Exception();
        if (ctx.variableDeclarator() is not { } declarator) throw new Exception();

        ParameterType = (EsClassReference) new EsClassReference().FromParseRule(ctx.parameterType);

        if (declarator.variableName is not { } variableName) throw new Exception();
        if (declarator.variableValue is { } variableValue) ParameterValue = EsExpressionFactory.Create(variableValue);
        ParameterName = (EsVariableName) new EsVariableName().FromParseRule(variableName);

        return this;
    }
    public override string ToString() => ToEnforce();
    public string ToEnforce() {
        var builder = new StringBuilder();
        if (ParameterModifiers.Count > 0) builder.Append(string.Join(' ', ParameterModifiers.Select(m => Enum.GetName(m)!.ToLower()))).Append(' ');
        builder.Append(ParameterType).Append(' ').Append(ParameterName);
        if (ParameterValue is not null) builder.Append(" = ").Append(ParameterValue.ToEnforce());
        return builder.ToString();
    }
}