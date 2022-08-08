using System.Collections.ObjectModel;
using System.Text;
using EnforceParser.Core.Factories;

namespace EnforceParser.Core.Models.Expression.Primary.Primitives; 

public class EsArray : IEsPrimitiveValue<List<IEsExpression>, Generated.EnforceParser.LiteralArrayContext> {
    public List<IEsExpression> Value { get; set; } = new();

    public EsArray(IEnumerable<IEsExpression> arrValues) => Value = arrValues.ToList();

    public EsArray() { }

    public IEsDeserializable<Generated.EnforceParser.LiteralArrayContext> FromParseRule(Generated.EnforceParser.LiteralArrayContext ctx) {
        if (ctx.expressionList() is null) return this;
        foreach (var expression in ctx.expressionList().expression()) Value.Add(EsExpressionFactory.Create(expression));
        return this;
    }

    public string ToEnforce() => new StringBuilder("{ ").Append(string.Join(", ", Value.ToList())).Append(" }").ToString();
    
    public IEsExpression this[int i] {
        get => (Value[i] ?? throw new Exception("Failed to find value in EsArray at index {i}"));
        set => Value[i] = value;
    }
    
    public static implicit operator EsArray (IEsExpression[] b) => new(b);
    public static implicit operator IEsExpression[](EsArray b) => b.Value.ToArray();

}