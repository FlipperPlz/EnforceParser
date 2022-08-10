using System.Text;
using EnforceParser.Core.Models.Generics;

namespace EnforceParser.Core.Models.Scope; 

public class EsSuperClass {
    public EsClassReference SuperClass { get; set; }
    public EsSuperClass(EsClassReference @ref) => SuperClass = @ref; 
    public override string ToString() => ToEnforce();
    public string ToEnforce() => SuperClass.ToEnforce();
}