using System.Text;
using EnforceParser.Core.Models.Expression;
using EnforceParser.Core.Models.Modifiers;

namespace EnforceParser.Core.Models; 

public class EsGenericType : IEsSerializable, IEsDeserializable<Generated.EnforceParser.GenericTypeContext> {
    public List<EsVariableModifier> Modifiers;
    public EsClassname Type;
    
    public IEsDeserializable<Generated.EnforceParser.GenericTypeContext> FromParseRule(Generated.EnforceParser.GenericTypeContext ctx) {
        if (ctx.variableModifier() is { } variableModifiers) {
            foreach (var modifierCtx in variableModifiers) {
                var modifierText = string.Concat(modifierCtx.GetText()[0].ToString().ToUpper(), modifierCtx.GetText().AsSpan(1));
                if (Enum.TryParse(modifierText, out EsVariableModifier modifier)) Modifiers.Add(modifier);
                else throw new Exception($"Failed to parse variable identifier from \"{modifierText}\".");
            }
        }

        if (ctx.identifier() is null) throw new Exception();
        Type = (EsClassname) new EsClassname().FromParseRule(ctx.identifier());
        return this;
    }

    public string ToEnforce() => new StringBuilder(string.Join(' ', Modifiers.Select(m => Enum.GetName(m)!.ToLower()))).Append(Modifiers.Count == 0 ? Type.ToEnforce() : $" {Type.ToEnforce()}").ToString();
}