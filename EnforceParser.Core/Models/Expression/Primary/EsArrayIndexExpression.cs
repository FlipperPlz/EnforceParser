using System.Text;
using EnforceParser.Core.Factories;
using EnforceParser.Core.Models.Expression.Primary.Primitives;

namespace EnforceParser.Core.Models.Expression.Primary; 

public class EsArrayIndexExpression : IEsPrimaryExpression, IEsDeserializable<Generated.EnforceParser.ArrayIndexExpressionContext> {
    public EsVariableName Array { get; set; }
    public List<EsArrayIndex> Indexes { get; set; } = new List<EsArrayIndex>();
    
    public IEsDeserializable<Generated.EnforceParser.ArrayIndexExpressionContext> FromParseRule(Generated.EnforceParser.ArrayIndexExpressionContext ctx) {
        if (ctx.arrayIndex() is not { } indexContexts || ctx.identifier() is not { } arr) throw new Exception();
        Array = (EsVariableName) new EsVariableName().FromParseRule(ctx.identifier());
        foreach (var arrayIndexContext in indexContexts) Indexes.Add((EsArrayIndex) new EsArrayIndex().FromParseRule(arrayIndexContext));
        return this;
    }
    public override string ToString() => ToEnforce();

    public string ToEnforce() {
        var builder = new StringBuilder(Array.ToEnforce());
        Indexes.ForEach(i => builder.Append(i.ToEnforce()));
        return builder.ToString();
    }
}