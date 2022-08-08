using System.Text;
using EnforceParser.Core.Models.Expression.Primary;

namespace EnforceParser.Core.Models.Expression.Operations.Contextual; 

public class EsFunctionContextExpression : IEsExpression {
    public IEsExpression Context { get; set; }
    public EsFunctionCall FunctionCall { get; set; }

    public EsFunctionContextExpression(IEsExpression context, EsFunctionCall function) {
        Context = context;
        FunctionCall = function;
    }
    
    
    public string ToEnforce() => new StringBuilder(Context.ToEnforce()).Append('.').Append(FunctionCall.ToEnforce()).ToString();
}