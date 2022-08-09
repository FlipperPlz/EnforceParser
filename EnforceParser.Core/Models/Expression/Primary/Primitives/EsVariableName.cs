namespace EnforceParser.Core.Models.Expression.Primary.Primitives; 

public class EsVariableName : IEsPrimaryExpression, IEsDeserializable<Generated.EnforceParser.IdentifierContext> {
    private string VariableName { get; set; }

    public EsVariableName(string variableName) => VariableName = variableName;
    public EsVariableName() => VariableName = "\0Enforce Parser Err: Undefined\0";

    public IEsDeserializable<Generated.EnforceParser.IdentifierContext> FromParseRule(Generated.EnforceParser.IdentifierContext ctx) {
        VariableName = ctx.GetText();
        return this;
    }
    public override string ToString() => ToEnforce();
    public string ToEnforce() => VariableName;
}