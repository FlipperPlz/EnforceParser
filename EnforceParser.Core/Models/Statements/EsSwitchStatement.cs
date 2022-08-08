namespace EnforceParser.Core.Models.Statements; 

public class EsSwitchStatement : IEsStatement, IEsDeserializable<Generated.EnforceParser.SwitchStatementContext> {
    public IEsDeserializable<Generated.EnforceParser.SwitchStatementContext> FromParseRule(Generated.EnforceParser.SwitchStatementContext ctx) {
        throw new NotImplementedException();
    }

    public string ToEnforce() {
        throw new NotImplementedException();
    }
}