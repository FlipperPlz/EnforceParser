using System.Text;
using EnforceParser.Core.Models.Expression.Primary;
using EnforceParser.Core.Models.Generics;
using EnforceParser.Core.Models.Modifiers;
using EnforceParser.Core.Models.Scope;

namespace EnforceParser.Core.Models.Expression; 

public class EsObjectCreationExpression : IEsExpression, IEsDeserializable<Generated.EnforceParser.ObjectCreationContext> {
    public List<EsVariableModifier> Modifiers { get; set; } = new();
    public EsClassname ObjectType { get; set; }
    public List<EsGenericType>? Generics { get; set; } = null;
    public List<IEsFunctionCallParameter>? CallParameters = null;
    
    public IEsDeserializable<Generated.EnforceParser.ObjectCreationContext> FromParseRule(Generated.EnforceParser.ObjectCreationContext ctx) {
        if (ctx.variableModifier() is { } variableModifiers) {
            foreach (var modifierCtx in variableModifiers) {
                var modifierText = string.Concat(modifierCtx.GetText()[0].ToString().ToUpper(), modifierCtx.GetText().AsSpan(1));
                if (Enum.TryParse(modifierText, out EsVariableModifier modifier)) Modifiers.Add(modifier);
                else throw new Exception($"Failed to parse variable identifier from \"{modifierText}\".");
            }
        }

        if (ctx.objectName is { } objectName) ObjectType = (EsClassname) new EsClassname().FromParseRule(objectName);

        if (ctx.typeList() is { } typeListContext) {
            Generics = new List<EsGenericType>();
            foreach (var generic in typeListContext.genericType()) Generics.Add((EsGenericType)new EsGenericType().FromParseRule(generic));
        }

        if (ctx.functionCallParameters() is { } parametersContext) {
            CallParameters = new List<IEsFunctionCallParameter>();
            if (parametersContext.functionCallParameterList() is { } parameterListRule) {
                foreach (var parameter in parameterListRule.functionCallParameter()) {
                    if (parameter.expression() is { }) {
                        CallParameters.Add((IEsFunctionCallParameter) new EsFunctionCallParameter().FromParseRule(parameter));
                    } else if (parameter.optionalParameter() is { }) {
                        CallParameters.Add((IEsFunctionCallParameter) new EsFunctionCallOptionalParameter().FromParseRule(parameter));
                    } 
                }
            }    
        }

        return this;
    }
    public override string ToString() => ToEnforce();
    public string ToEnforce() {
        var builder = new StringBuilder("new ").Append(string.Join(" ", Modifiers.Select(m => Enum.GetName(m)!.ToLower())));
        if (Modifiers.Count != 0) builder.Append(' ');
        builder.Append(ObjectType.ToEnforce());
        if (Generics is not null) builder.Append('<').Append(string.Join(", ", Generics.Select(g => g.ToEnforce()))).Append('>');
        if (CallParameters is not null) builder.Append('(').Append(string.Join(", ", CallParameters.Select(p => p.ToEnforce()))).Append(')');
        return builder.ToString();
    }
}