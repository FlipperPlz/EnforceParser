using System.Text;
using EnforceParser.Core.Models.Expression.Primary;

namespace EnforceParser.Core.Models; 

public class EsAnnotation : IEsDeserializable<Generated.EnforceParser.AnnotationContext> {
    public EsFunctionCall FunctionCall { get; set; }
    
    public IEsDeserializable<Generated.EnforceParser.AnnotationContext> FromParseRule(Generated.EnforceParser.AnnotationContext ctx) {
        if (ctx.functionCall() is null) throw new Exception();
        FunctionCall = (EsFunctionCall) new EsFunctionCall().FromParseRule(ctx.functionCall());
        return this;
    }
    public override string ToString() => ToEnforce();
    public string ToEnforce() => new StringBuilder("[").Append(FunctionCall.ToEnforce()).Append(']').ToString();
}