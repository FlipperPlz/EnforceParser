namespace EnforceParser.Core.Models.Expression.Primary.Primitives; 

public class EsBoolean : IEsPrimitiveValue<bool, Generated.EnforceParser.LiteralBooleanContext> {
    public bool Value { get; set; } = false;
    public EsBoolean() {}
    public EsBoolean(bool b) => Value = b;
    public IEsDeserializable<Generated.EnforceParser.LiteralBooleanContext> FromParseRule(Generated.EnforceParser.LiteralBooleanContext ctx) {
        Value = ctx.LiteralBoolean().GetText() == "true";
        return this;
    }
    public string ToEnforce() => Value ? "true" : "false";
    public static EsBoolean operator !(EsBoolean boolean) => new(!boolean.Value);
    public static implicit operator EsBoolean (bool b) => new(b);
    public static implicit operator bool (EsBoolean b) => b.Value;
}