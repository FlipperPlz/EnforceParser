namespace EnforceParser.Core.Models.Expression; 

public class EsSuperExpression : IEsExpression {
    public string ToEnforce() => "super";
    public override string ToString() => ToEnforce();
}