using System.Text;
using EnforceParser.Core.Models.Generics;

namespace EnforceParser.Core.Models.Types; 

public class EsTypeDefDeclaration : IEsDeserializable<Generated.EnforceParser.TypedefDeclarationContext>, IEsType {
    public EsClassname BaseType { get; set; }
    public List<EsGenericType>? BaseTypeGenerics { get; set; }

    public EsClassname CreatedType { get; set; }
    
    public IEsDeserializable<Generated.EnforceParser.TypedefDeclarationContext> FromParseRule(Generated.EnforceParser.TypedefDeclarationContext ctx) {
        if (ctx.fromType is null) throw new Exception();
        if (ctx.toType is null) throw new Exception();

        if (ctx.fromType.keyword() is not null) {
            BaseType = new EsClassname() { Classname = ctx.fromType.keyword().GetText() };
        }else {
            BaseType = (EsClassname) new EsClassname().FromParseRule(ctx.fromType.identifier());
            if (ctx.fromType.typeList() is { } genericList) {
                BaseTypeGenerics = new();
                foreach (var genericType in genericList.genericType()) {
                    BaseTypeGenerics.Add((EsGenericType) new EsGenericType().FromParseRule(genericType));
                }
            }  
        }

        CreatedType = (EsClassname)new EsClassname().FromParseRule(ctx.toType);
        
        return this;
    }

    public string ToEnforce() {
        var builder = new StringBuilder("typedef ").Append(BaseType.ToEnforce());
        if (BaseTypeGenerics is not null) builder.Append('<').Append(string.Join(", ", BaseTypeGenerics)).Append('>');
        return builder.Append(' ').Append(CreatedType.ToEnforce()).Append(';').ToString();
    }
}