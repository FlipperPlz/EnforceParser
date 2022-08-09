using System.Text;
using EnforceParser.Core.Models.Expression.Primary.Primitives;
using EnforceParser.Core.Models.Modifiers;
using EnforceParser.Core.Models.Scope;

namespace EnforceParser.Core.Models.Generics; 

public class EsGenericTypeDeclaration : IEsDeserializable<Generated.EnforceParser.GenericTypeDeclarationContext> {
    public bool IsArray { get; set; } = false;
    public List<EsVariableModifier>? TypeModifiers { get; set; }
    public EsClassReference TypeVariableType { get; set; }
    public EsVariableName TypeVariableName { get; set; }
    
    public IEsDeserializable<Generated.EnforceParser.GenericTypeDeclarationContext> FromParseRule(Generated.EnforceParser.GenericTypeDeclarationContext ctx) {
        if (ctx.variableModifier() is { } variableModifiers) {
            TypeModifiers = new List<EsVariableModifier>();
            foreach (var modifierCtx in variableModifiers) {
                var modifierText = string.Concat(modifierCtx.GetText()[0].ToString().ToUpper(), modifierCtx.GetText().AsSpan(1));
                if (Enum.TryParse(modifierText, out EsVariableModifier modifier)) TypeModifiers.Add(modifier);
                else throw new Exception($"Failed to parse type modifier from \"{modifierText}\".");
            }
        }

        if (ctx.type is not { } varType) throw new Exception();
        if (ctx.typeName is not { } varName) throw new Exception();
        IsArray = ctx.LSBracket() is not null && ctx.RSBracket() is not null;
        TypeVariableType =  (EsClassReference) new EsClassReference().FromParseRule(varType);
        TypeVariableName = (EsVariableName) new EsVariableName().FromParseRule(varName);
        return this;
    }
    public override string ToString() => ToEnforce();
    public string ToEnforce() {
        var builder = new StringBuilder();
        if (TypeModifiers is not null && TypeModifiers.Count > 0) builder.Append(string.Join(' ', TypeModifiers.Select(m => Enum.GetName(m)!.ToLower()))).Append(' ');
        return builder.Append(TypeVariableType.ToEnforce()).Append(' ').Append(TypeVariableName.ToEnforce()).ToString();
    }
}