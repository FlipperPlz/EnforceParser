﻿namespace EnforceParser.Core.Models.Expression; 

public class EsThisExpression : IEsExpression {
    public string ToEnforce() => "this";
    public override string ToString() => ToEnforce();
}