using System.Text;
using EnforceParser.Core.Models.Generics;
using EnforceParser.Core.Models.Globals;
using EnforceParser.Core.Models.Modifiers;
using EnforceParser.Core.Models.Scope;

namespace EnforceParser.Core.Models.Types; 

public class EsClassDeclaration : IEsDeserializable<Generated.EnforceParser.ClassDeclarationContext>, IEsType {
    public EsAnnotation? ClassAnnotation { get; set; } = null;
    public List<EsTypeModifier> ClassModifiers { get; set; } = new();
    public EsClassname Classname { get; set; }
    public List<EsGenericTypeDeclaration>? ClassGenericVariables { get; set; } = null;
    public EsClassReference? SuperClass { get; set; } = null;
    public List<IEsGlobalStatement>? ClassBody { get; set; }
    
    public IEsDeserializable<Generated.EnforceParser.ClassDeclarationContext> FromParseRule(Generated.EnforceParser.ClassDeclarationContext ctx) {
        if (ctx.annotation() is { } annotation) ClassAnnotation = (EsAnnotation) new EsAnnotation().FromParseRule(annotation);
        if (ctx.typeModifer() is { } variableModifiers) {
            foreach (var modifierCtx in variableModifiers) {
                var modifierText = string.Concat(modifierCtx.GetText()[0].ToString().ToUpper(), modifierCtx.GetText().AsSpan(1));
                if (Enum.TryParse(modifierText, out EsTypeModifier modifier)) ClassModifiers.Add(modifier);
                else throw new Exception($"Failed to parse variable modifier from \"{modifierText}\".");
            }
        }

        if (ctx.classname is null) throw new Exception();
        Classname = (EsClassname)new EsClassname().FromParseRule(ctx.classname);
        if (ctx.genericTypeDeclarationList() is { } generics) {
            ClassGenericVariables = new();
            foreach (var generic in generics.genericTypeDeclaration()) ClassGenericVariables.Add((EsGenericTypeDeclaration) new EsGenericTypeDeclaration().FromParseRule(generic));
        }

        if (ctx.superclass is { } superclass) {
            EsClassname superClassName;
            List<EsGenericTypeDeclaration>? superGenerics = null;
            if (superclass.classname is not { }) throw new Exception();
            superClassName = (EsClassname) new EsClassname().FromParseRule(superclass.classname);
            if (superclass.genericTypeDeclarationList() is { } genericList) {
                superGenerics = genericList.genericTypeDeclaration().Select(generic => (EsGenericTypeDeclaration)new EsGenericTypeDeclaration().FromParseRule(generic)).ToList();
            }

            SuperClass = new EsClassReference(superClassName, superGenerics);
        }

        if (ctx.classBody is { } body) {
            ClassBody = new List<IEsGlobalStatement>();
            foreach (var global in body.globalDeclaration()) {
                if (global.functionDeclaration() is { } functionDeclaration) ClassBody.Add((IEsGlobalStatement) new EsFunctionDeclaration().FromParseRule(functionDeclaration));
                else if (global.variableDeclaration() is { } variableDeclaration) ClassBody.Add((IEsGlobalStatement) new EsVariableDeclarationStatement().FromParseRule(variableDeclaration));
            }
        }


        return this;
    }
    public override string ToString() => ToEnforce();
    public string ToEnforce() {
        var builder = new StringBuilder();
        if (ClassAnnotation is not null) builder.Append(ClassAnnotation.ToEnforce()).Append(' ');
        if (ClassModifiers.Count > 0)
            builder.Append(string.Join(' ', ClassModifiers.Select(m => Enum.GetName(m)!.ToLower()))).Append(' ');
        builder.Append("class ");
        builder.Append(Classname.ToEnforce());

        if (ClassGenericVariables is not null) 
            builder.Append('<').Append(string.Join(", ", ClassGenericVariables.Select(g => g.ToEnforce()))).Append('>');
        if (SuperClass is not null) builder.Append(" : ").Append(SuperClass.ToEnforce());
        if (ClassBody is null || ClassBody.Count == 0) return builder.Append(';').ToString();
        
        if (ClassBody.Count <= 1) {
            builder.Append(ClassBody[0].ToEnforce());
        } else {
            builder.Append('{').Append('\n');
            ClassBody.ForEach(s => builder.Append(s.ToEnforce()).Append('\n'));
            builder.Append('}');
        }

        return builder.ToString();
    }
}