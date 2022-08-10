using System.Text;
using EnforceParser.Core.Models.Generics;

namespace EnforceParser.Core.Models.Scope; 

public class EsSuperClass {
    public EsClassname SuperClassname { get; set; }
    public List<EsGenericTypeDeclaration>? SuperClassGenerics { get; set; }
    
    public EsSuperClass(EsClassname classname, List<EsGenericTypeDeclaration>? generics = null) {
        SuperClassname = classname;
        SuperClassGenerics = generics;
    }
    public override string ToString() => ToEnforce();
    public string ToEnforce() {
        var builder = new StringBuilder(SuperClassname.ToEnforce());
        if (SuperClassGenerics is not null) {
            builder.Append('<').Append(string.Join(", ", SuperClassGenerics.Select(g => g.ToEnforce()))).Append('>');
        }

        return builder.Append(' ').ToString();
    }
}