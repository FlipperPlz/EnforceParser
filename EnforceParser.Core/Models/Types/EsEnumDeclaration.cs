using System.Text;
using EnforceParser.Core.Models.Generics;
using EnforceParser.Core.Models.Modifiers;
using EnforceParser.Core.Models.Scope;

namespace EnforceParser.Core.Models.Types; 

public class EsEnumDeclaration : IEsDeserializable<Generated.EnforceParser.EnumDeclarationContext>, IEsType {
    public EsAnnotation? EnumAnnotation { get; set; } = null;
    public List<EsTypeModifier> EnumModifiers { get; set; } = new();
    public EsClassname EnumName { get; set; }
    public EsClassReference? SuperEnum { get; set; } = null;
    public List<EsEnumItem>? EnumBody { get; set; }
    
    public IEsDeserializable<Generated.EnforceParser.EnumDeclarationContext> FromParseRule(Generated.EnforceParser.EnumDeclarationContext ctx) {
        if (ctx.annotation() is { } annotation) EnumAnnotation = (EsAnnotation) new EsAnnotation().FromParseRule(annotation);
        if (ctx.enumname is null) throw new Exception();
        EnumName = (EsClassname) new EsClassname().FromParseRule(ctx.enumname);
        if (ctx.typeModifer() is { } variableModifiers) {
            foreach (var modifierCtx in variableModifiers) {
                var modifierText = string.Concat(modifierCtx.GetText()[0].ToString().ToUpper(), modifierCtx.GetText().AsSpan(1));
                if (Enum.TryParse(modifierText, out EsTypeModifier modifier)) EnumModifiers.Add(modifier);
                else throw new Exception($"Failed to parse enum modifier from \"{modifierText}\".");
            }
        }
        if (ctx.superenum is { } superenum) {
            EsClassname superEnumName;
            List<EsGenericTypeDeclaration>? superGenerics = null;
            if (superenum.classname is not { }) throw new Exception();
            superEnumName = (EsClassname) new EsClassname().FromParseRule(superenum.classname);
            if (superenum.genericTypeDeclarationList() is { } genericList) superGenerics = genericList.genericTypeDeclaration().Select(generic => (EsGenericTypeDeclaration) new EsGenericTypeDeclaration().FromParseRule(generic)).ToList();
            

            SuperEnum = new EsClassReference(superEnumName, superGenerics);
        }

        if (ctx.enumBody() is { } enumBody) {
            if (ctx.enumBody().enumValue() is { } enumValues) {
                EnumBody = new List<EsEnumItem>();
                foreach (var enumValue in enumValues) {
                    EnumBody.Add((EsEnumItem)new EsEnumItem().FromParseRule(enumValue));
                }
            }
        }

        return this;
    }
    public override string ToString() => ToEnforce();
    public string ToEnforce() {
        var builder = new StringBuilder();
        if (EnumAnnotation is not null) builder.Append(EnumAnnotation.ToEnforce()).Append(' ');
        if (EnumModifiers.Count > 0) builder.Append(string.Join(' ', EnumModifiers.Select(m => Enum.GetName(m)!.ToLower()))).Append(' ');
        builder.Append(EnumName.ToEnforce());

        if (SuperEnum is not null) builder.Append(" : ").Append(SuperEnum.ToEnforce());
        
        if (EnumBody is null || EnumBody.Count == 0) return builder.Append(';').ToString();

        if (EnumBody.Count <= 1) {
            return builder.Append(EnumBody[0].ToEnforce()).Append(';').ToString();
        } else {
            builder.Append('{').Append('\n');
            EnumBody.ForEach(s => builder.Append(s.ToEnforce()).Append('\n'));
            return builder.Append('}').ToString();
        }
    }
}