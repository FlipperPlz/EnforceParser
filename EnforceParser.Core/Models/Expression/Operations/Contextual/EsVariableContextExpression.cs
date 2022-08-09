using System.Text;
using EnforceParser.Core.Models.Expression.Primary.Primitives;

namespace EnforceParser.Core.Models.Expression.Operations.Contextual; 

public class EsVariableContextExpression : IEsExpression {
    public IEsExpression Context { get; set; }
    public EsVariableName VariableName { get; set; }

    public EsVariableContextExpression(IEsExpression context, EsVariableName variableName) {
        Context = context;
        VariableName = variableName;
    }
    
    public override string ToString() => ToEnforce();
    public string ToEnforce() => new StringBuilder(Context.ToEnforce()).Append('.').Append(VariableName.ToEnforce()).ToString();
}