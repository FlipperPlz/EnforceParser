using System.Text;
using EnforceParser.Core.Models.Expression;
using EnforceParser.Core.Models.Expression.Primary.Primitives;

namespace EnforceParser.Core.Models.Scope; 

public class EsVariableDeclaratorName : IEsSerializable {
    public EsVariableName VariableName { get; set; }
    public bool IsArray { get; set; } = false;
    public IEsExpression? ArrayBoundaries { get; set; }

    public EsVariableDeclaratorName(EsVariableName variableName, bool array = false, IEsExpression? arrBounds = null) {
        VariableName = variableName;
        IsArray = array;
        ArrayBoundaries = arrBounds;
    }
    
    public string ToEnforce() {
        var builder = new StringBuilder(VariableName.ToEnforce());
        if (IsArray) {
            builder.Append('[');
            if (ArrayBoundaries is not null) builder.Append(ArrayBoundaries.ToEnforce());
            builder.Append(']');
        }

        return builder.ToString();
    }
}