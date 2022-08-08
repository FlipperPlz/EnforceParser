namespace EnforceParser.Core.Models.Expression.Operations; 

public abstract class EsUnaryExpression : IEsExpression {
    public IEsExpression Base { get; set; }
    public IEsExpression Expression { get; set; }

    public EsUnaryExpression(IEsExpression @base, IEsExpression expression) {
        Base = @base;
        Expression = expression;
    }

    public abstract string ToEnforce();
}