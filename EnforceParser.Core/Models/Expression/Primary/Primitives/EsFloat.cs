using System.Globalization;
using Antlr4.Runtime.Misc;

namespace EnforceParser.Core.Models.Expression.Primary.Primitives; 

public class EsFloat : IEsPrimitiveValue<float, Generated.EnforceParser.LiteralFloatContext> {
    public float Value { get; set; } = 0;
    
    public EsFloat(float i) => Value = i;
    public EsFloat() {}
    
    public static implicit operator EsFloat (float i) => new(i);
    public static implicit operator float (EsFloat i) => i.Value;
    
    public IEsDeserializable<Generated.EnforceParser.LiteralFloatContext> FromParseRule(Generated.EnforceParser.LiteralFloatContext ctx) {
        var found = ctx.Start.InputStream.GetText(new Interval(ctx.Start.StartIndex, ctx.Stop.StopIndex));
        try {
            Value = (float) double.Parse(found, NumberStyles.Any);
        } catch (Exception e) {
            Console.WriteLine($"Error while parsing {{{found}}} as {GetType().Name}.");
            throw;
        }

        return this;
    }

    public string ToEnforce() => Value.ToString("R");
}