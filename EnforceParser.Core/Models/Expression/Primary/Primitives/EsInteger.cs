using System.Text;
using Antlr4.Runtime.Misc;

namespace EnforceParser.Core.Models.Expression.Primary.Primitives; 

public class EsInteger : IEsPrimitiveValue<int, Generated.EnforceParser.LiteralIntegerContext> {
    public int Value { get; set; }

    public EsInteger(int i) => Value = i;
    public EsInteger() {}
    
    public static implicit operator EsInteger (int i) => new(i);
    public static implicit operator int (EsInteger i) => i.Value;
    public override string ToString() => ToEnforce();
    public IEsDeserializable<Generated.EnforceParser.LiteralIntegerContext> FromParseRule(Generated.EnforceParser.LiteralIntegerContext ctx) {
        var found = ctx.Start.InputStream.GetText(new Interval(ctx.Start.StartIndex, ctx.Stop.StopIndex));

        try {
            Value = int.Parse(found);
        } catch (Exception e) {
            Console.WriteLine($"Error while parsing {{{found}}} as {GetType().Name}.");
            Console.WriteLine(e);
            throw;
        }

        return this;
    }

    public string ToEnforce() => Value.ToString();

}