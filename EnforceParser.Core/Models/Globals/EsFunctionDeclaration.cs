using System.Text;
using EnforceParser.Core.Factories;
using EnforceParser.Core.Models.Modifiers;
using EnforceParser.Core.Models.Scope;
using EnforceParser.Core.Models.Statements;

namespace EnforceParser.Core.Models; 

public class EsFunctionDeclaration : IEsGlobalStatement, IEsDeserializable<Generated.EnforceParser.FunctionDeclarationContext> {
    public EsAnnotation? FunctionAnnotation { get; set; } = null;
    public List<EsFunctionModifier> FunctionModifiers { get; set; } = new();
    public EsClassReference? ReturnType { get; set; } = null;//Null for void
    public bool Deconstructor { get; set; } = false;
    public EsFunctionName FunctionName { get; set; }
    public List<EsFunctionDeclarationParameter> FunctionParameters { get; set; } = new();
    public List<IEsStatement>? FunctionBody { get; set; } = null;


    public IEsDeserializable<Generated.EnforceParser.FunctionDeclarationContext> FromParseRule(Generated.EnforceParser.FunctionDeclarationContext ctx) {
        if (ctx.annotation() is { } annotation) FunctionAnnotation = (EsAnnotation) new EsAnnotation().FromParseRule(annotation);
        if (ctx.functionModifier() is { } functionModifiers) foreach (var modifierCtx in functionModifiers) {
            var modifierText = string.Concat(modifierCtx.GetText()[0].ToString().ToUpper(), modifierCtx.GetText().AsSpan(1));
            if (Enum.TryParse(modifierText, out EsFunctionModifier modifier)) FunctionModifiers.Add(modifier);
            else throw new Exception($"Failed to parse function modifier from \"{modifierText}\".");
        }

        if (ctx.returnType is null) throw new Exception();
        if (ctx.returnType.GetText() != "void") ReturnType = (EsClassReference?)new EsClassReference().FromParseRule(ctx.returnType);
        if (ctx.deconstructor is not null) Deconstructor = true;
        if (ctx.functionName is null) throw new Exception();
        if (ctx.statementSingleOrBlock() is not { } statementSingleOrBlock) throw new Exception();
        if (ctx.functionParameters() is null) throw new Exception();
        FunctionName = (EsFunctionName) new EsFunctionName().FromParseRule(ctx.functionName);

        foreach (var param in ctx.functionParameters().functionParameter()) FunctionParameters.Add((EsFunctionDeclarationParameter)new EsFunctionDeclarationParameter().FromParseRule(param));
        
        if (statementSingleOrBlock.statementBlock() is { } statementBlock) {
            FunctionBody = new();
            foreach (var statement in statementBlock.statement()) FunctionBody.Add(EsStatementFactory.Create(statement));
            return this;
        }
        if (statementSingleOrBlock.statement() is { }) {
            FunctionBody = new();
            FunctionBody.Add(EsStatementFactory.Create(statementSingleOrBlock.statement()));
            return this;
        }

        return this;
    }
    public override string ToString() => ToEnforce();
    public string ToEnforce() {
        var builder = new StringBuilder();
        if (FunctionAnnotation is not null) builder.Append(FunctionAnnotation.ToEnforce()).Append(' ');
        if (FunctionModifiers.Count > 0) builder.Append(string.Join(' ', FunctionModifiers.Select(m => Enum.GetName(m)!.ToLower()))).Append(' ');
        if (ReturnType is null) builder.Append(new EsClassname() { Classname = "void" }.ToEnforce()).Append(' ');
        else builder.Append(ReturnType.ToEnforce()).Append(' ');
        if (Deconstructor) builder.Append('~');
        builder.Append(FunctionName).Append('(').Append(string.Join(", ", FunctionParameters.Select(f => f.ToEnforce()))).Append(')');
        if (FunctionBody is null) return builder.Append(';').ToString();
        builder.Append(' ');
        if (FunctionBody.Count == 1) {
            builder.Append(FunctionBody[0].ToEnforce());
        } else {
            builder.Append('{').Append('\n');
            FunctionBody.ForEach(s => builder.Append(s.ToEnforce()).Append('\n'));
            builder.Append('}');
        }

        return builder.ToString();
    }
}
