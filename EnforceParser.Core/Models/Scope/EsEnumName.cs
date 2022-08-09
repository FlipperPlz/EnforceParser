namespace EnforceParser.Core.Models.Scope; 

public class EsEnumName : IEsDeserializable<Generated.EnforceParser.IdentifierContext> {
    public string EnumName { get; set; }
    
    public IEsDeserializable<Generated.EnforceParser.IdentifierContext> FromParseRule(Generated.EnforceParser.IdentifierContext ctx) {
        EnumName = ctx.GetText();
        return this;
    }

    public string ToEnforce() => EnumName;
}