using System.Text;
using EnforceParser.Core.Factories;
using EnforceParser.Core.Models.Expression;
using EnforceParser.Core.Models.Expression.Primary.Primitives;
using EnforceParser.Core.Models.Modifiers;
using EnforceParser.Core.Models.Scope;
using EnforceParser.Core.Models.Statements;

namespace EnforceParser.Core.Models.Globals; 

public class EsVariableDeclarationStatement : IEsStatement, IEsGlobalStatement, IEsDeserializable<Generated.EnforceParser.VariableDeclarationContext> {
    public EsAnnotation? VariableAnnotation { get; set; } = null;
    public List<EsVariableModifier> VariableModifiers { get; set; } = new();
    public EsClassReference VariableType { get; set; }
    public List<EsVariableDeclarator> Variables { get; set; } = new();


    public IEsDeserializable<Generated.EnforceParser.VariableDeclarationContext> FromParseRule(Generated.EnforceParser.VariableDeclarationContext ctx) {
        if (ctx.annotation() is { } annotation) VariableAnnotation = (EsAnnotation) new EsAnnotation().FromParseRule(annotation);
        if (ctx.variableModifier() is { } variableModifiers) {
            foreach (var modifierCtx in variableModifiers) {
                var modifierText = string.Concat(modifierCtx.GetText()[0].ToString().ToUpper(), modifierCtx.GetText().AsSpan(1));
                if (Enum.TryParse(modifierText, out EsVariableModifier modifier)) VariableModifiers.Add(modifier);
                else throw new Exception($"Failed to parse variable modifier from \"{modifierText}\".");
            }
        }

        if (ctx.variableType is not { } variableType) throw new Exception();
        VariableType = (EsClassReference) new EsClassReference().FromParseRule(variableType);
        
        if (ctx.variableDeclarators() is null) throw new Exception();

        foreach (var declarator in ctx.variableDeclarators().variableDeclarator()) 
            Variables.Add((EsVariableDeclarator) new EsVariableDeclarator().FromParseRule(declarator));

        return this;
    }
    public override string ToString() => ToEnforce();
    public string ToEnforce() {
        var builder = new StringBuilder();
        if (VariableAnnotation is not null) builder.Append(VariableAnnotation.ToEnforce()).Append(' ');
        if (VariableModifiers.Count > 0) builder.Append(string.Join(' ', VariableModifiers.Select(m => Enum.GetName(m)!.ToLower()))).Append(' ');
        builder.Append(VariableType).Append(' ').Append(string.Join(", ", Variables.Select(v => v.ToEnforce())));

        return $"{builder.ToString().TrimEnd(' ').TrimEnd(',')};";
    }
}