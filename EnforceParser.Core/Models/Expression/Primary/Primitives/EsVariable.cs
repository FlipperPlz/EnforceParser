namespace EnforceParser.Core.Models.Expression.Primary.Primitives; 

public class EsVariable : IEsPrimaryExpression, IEsDeserializable<Generated.EnforceParser.IdentifierContext> {
    private string VariableName { get; set; }

    public EsVariable(string variableName) => VariableName = variableName;
    public EsVariable() => VariableName = "\0Enforce Parser Err: Undefined\0";

    public IEsDeserializable<Generated.EnforceParser.IdentifierContext> FromParseRule(Generated.EnforceParser.IdentifierContext ctx) {
        VariableName = ctx.GetText();
        return this;
    }

    public string ToEnforce() => VariableName;
}