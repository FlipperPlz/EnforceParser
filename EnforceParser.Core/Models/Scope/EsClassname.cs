namespace EnforceParser.Core.Models.Scope;

public class EsClassname : IEsSerializable, IEsDeserializable<Generated.EnforceParser.IdentifierContext> {
    public string Classname { get; set; }
    
    public IEsDeserializable<Generated.EnforceParser.IdentifierContext> FromParseRule(Generated.EnforceParser.IdentifierContext ctx) {
        Classname = ctx.GetText();
        return this;
    }
    public override string ToString() => ToEnforce();
    public string ToEnforce() => Classname;
}