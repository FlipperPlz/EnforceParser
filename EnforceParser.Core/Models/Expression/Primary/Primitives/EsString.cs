using System.Text;
using Antlr4.Runtime.Misc;

namespace EnforceParser.Core.Models.Expression.Primary.Primitives; 

public class EsString : IEsPrimitiveValue<string, Generated.EnforceParser.LiteralStringContext> {
    public string Value { get; set; } = string.Empty;

    public EsString(string value) => Value = value;
    public EsString() {}
    
    public IEsDeserializable<Generated.EnforceParser.LiteralStringContext> FromParseRule(Generated.EnforceParser.LiteralStringContext ctx) {
        Value = ctx.Start.InputStream.GetText(new Interval(ctx.Start.StartIndex, ctx.Stop.StopIndex)).TrimStart('"').TrimEnd('"');
        return this;
    }

    public string ToEnforce() => new StringBuilder("\"").Append(Value).Append('"').ToString();

    public static implicit operator EsString(string s) => new(s);
    public static implicit operator string(EsString s) => s.Value;

    public EsInteger Hash() {
        int hash = 0, c = 1, length = Value.Length;
        for(var i = 0; i < length; i++) {
            hash += Value[length - 1 - i] * c;
            c *= 37;
        }
        return new EsInteger(hash);
    }
}