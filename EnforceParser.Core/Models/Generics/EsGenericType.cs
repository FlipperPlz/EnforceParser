using System.Text;
using EnforceParser.Core.Models.Modifiers;
using EnforceParser.Core.Models.Scope;

namespace EnforceParser.Core.Models.Generics; 

public class EsGenericType : IEsSerializable, IEsDeserializable<Generated.EnforceParser.GenericTypeContext> {
    public List<EsVariableModifier> Modifiers = new();
    public EsClassReference Type;
    
    public IEsDeserializable<Generated.EnforceParser.GenericTypeContext> FromParseRule(Generated.EnforceParser.GenericTypeContext ctx) {
        if (ctx.variableModifier() is { } variableModifiers) {
            foreach (var modifierCtx in variableModifiers) {
                var modifierText = string.Concat(modifierCtx.GetText()[0].ToString().ToUpper(), modifierCtx.GetText().AsSpan(1));
                if (Enum.TryParse(modifierText, out EsVariableModifier modifier)) Modifiers.Add(modifier);
                else throw new Exception($"Failed to parse variable identifier from \"{modifierText}\".");
            }
        }

        if (ctx.classReference() is null) throw new Exception();
        Type = (EsClassReference) new EsClassReference().FromParseRule(ctx.classReference());
        return this;
    }
    public override string ToString() => ToEnforce();
    public string ToEnforce() {
        var builder = new StringBuilder();
        if (Modifiers.Count > 0) builder.Append(string.Join(' ', Modifiers.Select(m => Enum.GetName(m)!.ToLower()))).Append(' ');
        return builder.Append(Type.ToEnforce()).ToString();
    }
}