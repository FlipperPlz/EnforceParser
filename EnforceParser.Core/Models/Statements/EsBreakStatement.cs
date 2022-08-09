namespace EnforceParser.Core.Models.Statements; 

public class EsBreakStatement : IEsStatement, IEsDeserializable<Generated.EnforceParser.BreakStatementContext> {
    public IEsDeserializable<Generated.EnforceParser.BreakStatementContext> FromParseRule(Generated.EnforceParser.BreakStatementContext ctx) => this;
    public override string ToString() => ToEnforce();
    public string ToEnforce() => "break;";
}