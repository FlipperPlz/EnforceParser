using Antlr4.Runtime;

namespace EnforceParser.Core; 

public interface IEsDeserializable<in T> : IEsSerializable where T : ParserRuleContext {
    public IEsDeserializable<T> FromParseRule(T ctx);
}