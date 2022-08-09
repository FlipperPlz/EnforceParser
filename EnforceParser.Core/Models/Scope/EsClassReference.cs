using System.Text;
using EnforceParser.Core.Models.Generics;

namespace EnforceParser.Core.Models.Scope; 

public class EsClassReference : IEsDeserializable<Generated.EnforceParser.ClassReferenceContext> {
    public EsClassname Classname { get; set; }
    public List<EsGenericType>? Generics { get; set; }
    
    public IEsDeserializable<Generated.EnforceParser.ClassReferenceContext> FromParseRule(Generated.EnforceParser.ClassReferenceContext ctx) {
        if (ctx.classname is null) throw new Exception();
        Classname = (EsClassname) new EsClassname().FromParseRule(ctx.classname);
        if (ctx.typeList() is not null) {
            Generics = new();
            foreach (var generic in ctx.typeList().genericType()) Generics.Add((EsGenericType) new EsGenericType().FromParseRule(generic));
        }

        return this;
    }

    public override string ToString() => ToEnforce();
    public string ToEnforce() {
        var builder = new StringBuilder(Classname.ToEnforce());
        if (Generics is not null) {
            builder.Append('<').Append(string.Join(", ", Generics.Select(g => g.ToEnforce()))).Append('>');
        }

        return builder.ToString();
    }
}