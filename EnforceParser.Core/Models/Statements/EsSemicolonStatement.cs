namespace EnforceParser.Core.Models.Statements; 

public class EsSemicolonStatement : IEsStatement {
    public string ToEnforce() => ";";
    public override string ToString() => ToEnforce();
}