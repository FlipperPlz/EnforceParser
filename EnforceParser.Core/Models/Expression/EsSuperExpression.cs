namespace EnforceParser.Core.Models.Expression; 

public class EsSuperExpression : IEsExpression {
    public string ToEnforce() => "super";
}