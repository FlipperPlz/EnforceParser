namespace EnforceParser.Core.Models.Statements; 

public class EsSemicolonStatement : IEsStatement {
    public string ToEnforce() => ";";
}