using System.Text;
using EnforceParser.Core.Factories;
using EnforceParser.Core.Models.Expression.Primary;
using EnforceParser.Core.Models.Scope;

namespace EnforceParser.Core.Models; 

public class EsEnumItem : IEsDeserializable<Generated.EnforceParser.EnumValueContext> {
    public EsEnumName ItemName { get; set; }
    public IEsPrimaryExpression? ItemValue;

    public IEsDeserializable<Generated.EnforceParser.EnumValueContext> FromParseRule(Generated.EnforceParser.EnumValueContext ctx) {
        if (ctx.itemname is not { } itemName) throw new Exception();
        ItemName = (EsEnumName) new EsEnumName().FromParseRule(itemName);
        if (ctx.itemValue is { } value) ItemValue = EsExpressionFactory.Create(value);
        return this;
    }

    public string ToEnforce() {
        var builder = new StringBuilder(ItemName.ToEnforce());
        if (ItemValue is not null) builder.Append(" = ").Append(ItemValue);
        return builder.ToString();
    }
}