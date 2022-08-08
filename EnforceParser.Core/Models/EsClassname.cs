namespace EnforceParser.Core.Models;

public class EsClassname : IEsSerializable, IEsDeserializable<Generated.EnforceParser.IdentifierContext> {
    public string Classname { get; set; }
    
    public IEsDeserializable<Generated.EnforceParser.IdentifierContext> FromParseRule(Generated.EnforceParser.IdentifierContext ctx) {
        Classname = ctx.GetText();
        return this;
    }

    public string ToEnforce() => Classname;
}