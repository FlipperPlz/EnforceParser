using System.Text;

namespace EnforceParser.Core.Models.Expression.Primary; 

public class EsArrayIndexExpression : IEsExpression {
    public IEsExpression Expression { get; set; }
    public EsArrayIndex Index { get; set; }
    
    public EsArrayIndexExpression(IEsExpression expression, EsArrayIndex index) {
        Expression = expression;
        Index = index;
    }
    
    public override string ToString() => ToEnforce();

    public string ToEnforce() => new StringBuilder(Expression.ToEnforce()).Append(Index.ToEnforce()).ToString();
}