namespace EnforceParser.Core.Models.Scope; 

public class EsFunctionName : IEsDeserializable<Generated.EnforceParser.IdentifierContext> {
    public string FunctionName { get; set; }
    public IEsDeserializable<Generated.EnforceParser.IdentifierContext> FromParseRule(Generated.EnforceParser.IdentifierContext ctx) {
        FunctionName = ctx.GetText();
        return this;
    }

    public string ToEnforce() => FunctionName;
}