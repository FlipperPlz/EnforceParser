namespace EnforceParser.Core.Models.Statements; 

public class EsContinueStatement : IEsStatement, IEsDeserializable<Generated.EnforceParser.ContinueStatementContext> {
    public IEsDeserializable<Generated.EnforceParser.ContinueStatementContext> FromParseRule(Generated.EnforceParser.ContinueStatementContext ctx) => this;

    public string ToEnforce() => "continue;";
}