using System.Text;
using EnforceParser.Core.Models.Expression.Primary;

namespace EnforceParser.Core.Models.Expression.Operations.Contextual; 

public class EsArrayIndexContextExpression : IEsExpression {
    public IEsExpression Context { get; set; }
    public EsArrayIndexExpression ArrayIndexExpression { get; set; }

    public EsArrayIndexContextExpression(IEsExpression context, EsArrayIndexExpression indexExpression) {
        Context = context;
        ArrayIndexExpression = indexExpression;
    }
    public override string ToString() => ToEnforce();
    public string ToEnforce() => new StringBuilder(Context.ToEnforce()).Append('.').Append(ArrayIndexExpression.ToEnforce()).ToString();
}