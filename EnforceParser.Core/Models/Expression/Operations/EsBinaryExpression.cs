namespace EnforceParser.Core.Models.Expression.Operations; 

public abstract class EsBinaryExpression : IEsExpression {
    public IEsExpression Left { get; set; }
    public IEsExpression Right { get; set; }

    public EsBinaryExpression(IEsExpression left, IEsExpression right) {
        Left = left;
        Right = right;
    }

    public abstract string ToEnforce();
}