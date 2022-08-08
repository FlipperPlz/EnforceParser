using Antlr4.Runtime;

namespace EnforceParser.Core.Models.Expression.Primary.Primitives; 

public interface IEsPrimitiveValue<T, TRule> :IEsDeserializable<TRule>, IEsPrimaryExpression where TRule : ParserRuleContext {
    public T Value { get; set; }
}