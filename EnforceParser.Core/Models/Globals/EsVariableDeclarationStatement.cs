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
    public EsClassname VariableType { get; set; }
    public Dictionary<EsVariableDeclaratorName, IEsExpression?> Variables;


    public IEsDeserializable<Generated.EnforceParser.VariableDeclarationContext> FromParseRule(Generated.EnforceParser.VariableDeclarationContext ctx) {
        if (ctx.annotation() is { } annotation) VariableAnnotation = (EsAnnotation) new EsAnnotation().FromParseRule(annotation);
        if (ctx.variableModifier() is { } variableModifiers) {
            foreach (var modifierCtx in variableModifiers) {
                var modifierText = string.Concat(modifierCtx.GetText()[0].ToString().ToUpper(), modifierCtx.GetText().AsSpan(1));
                if (Enum.TryParse(modifierText, out EsVariableModifier modifier)) VariableModifiers.Add(modifier);
                else throw new Exception($"Failed to parse variable modifier from \"{modifierText}\".");
            }
        }

        if (ctx.variableDeclarators() is null) throw new Exception();

        foreach (var declarator in ctx.variableDeclarators().variableDeclarator()) {
            if (declarator.variableName is not { }) throw new Exception();
            var varName = (EsVariableName) new EsVariableName().FromParseRule(declarator.variableName);
            var isArr = declarator.LSBracket() is not null;
            IEsExpression arrBounds = null;
            if(declarator.arrayLength is { } val) arrBounds = EsExpressionFactory.Create(val);
            Variables.Add(new EsVariableDeclaratorName(varName, isArr, arrBounds), EsExpressionFactory.Create(declarator.variableValue));
        }

        return this;
    }

    public string ToEnforce() {
        var builder = new StringBuilder();
        if (VariableAnnotation is not null) builder.Append(VariableAnnotation.ToEnforce()).Append(' ');
        if (VariableModifiers.Count > 0) builder.Append(string.Join(' ', VariableModifiers.Select(m => Enum.GetName(m)!.ToLower()))).Append(' ');
        builder.Append(VariableType).Append(' ');
        foreach (var (name, value) in Variables) {
            builder.Append(name.ToEnforce());
            if (value is not null) {
                builder.Append(" = ").Append(value.ToEnforce());
            }

            builder.Append(", ");
        }

        return $"{builder.ToString().TrimEnd(' ').TrimEnd(',')};";
    }
}