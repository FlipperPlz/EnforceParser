using System.Text;
using EnforceParser.Core.Models.Generics;

namespace EnforceParser.Core.Models.Scope; 

public class EsClassReference : IEsSerializable {
    public EsClassname Classname { get; set; }
    public List<EsGenericTypeDeclaration>? Generics { get; set; }
    
    public EsClassReference(EsClassname classname, List<EsGenericTypeDeclaration>? generics = null) {
        Classname = classname;
        Generics = generics;
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