using System.Text;
using EnforceParser.Core.Models.Expression.Primary;

namespace EnforceParser.Core.Models.Expression.Operations.Contextual; 

public class EsArrayIndexContextExpression : IEsExpression {
    public IEsExpression Context { get; set; }
    public EsArrayIndex ArrayIndex { get; set; }

    public EsArrayIndexContextExpression(IEsExpression context, EsArrayIndex index) {
        Context = context;
        ArrayIndex = index;
    }
    public override string ToString() => ToEnforce();
    public string ToEnforce() => new StringBuilder(Context.ToEnforce()).Append('.').Append(ArrayIndex.ToEnforce()).ToString();
}