using System.Text;
using EnforceParser.Core.Models.Expression.Primary.Primitives;

namespace EnforceParser.Core.Models.Expression.Operations.Contextual; 

public class EsVariableContextExpression : IEsExpression {
    public IEsExpression Context { get; set; }
    public EsVariable Variable { get; set; }

    public EsVariableContextExpression(IEsExpression context, EsVariable variable) {
        Context = context;
        Variable = variable;
    }
    
    
    public string ToEnforce() => new StringBuilder(Context.ToEnforce()).Append('.').Append(Variable.ToEnforce()).ToString();
}