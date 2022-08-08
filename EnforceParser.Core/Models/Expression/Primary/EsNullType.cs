namespace EnforceParser.Core.Models.Expression.Primary; 

public class EsNullType : IEsPrimaryExpression, IEsDeserializable<Generated.EnforceParser.LiteralNullContext> {
    public EsNullType() { }
    public IEsDeserializable<Generated.EnforceParser.LiteralNullContext> FromParseRule(Generated.EnforceParser.LiteralNullContext ctx) => this;
    public string ToEnforce() => "null";
}