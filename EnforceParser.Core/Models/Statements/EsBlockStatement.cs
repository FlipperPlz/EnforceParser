using System.Text;
using EnforceParser.Core.Factories;

namespace EnforceParser.Core.Models.Statements; 

public class EsBlockStatement : IEsStatement, IEsDeserializable<Generated.EnforceParser.StatementBlockContext> {
    public List<IEsStatement> Statements { get; set; } = new();
    
    public IEsDeserializable<Generated.EnforceParser.StatementBlockContext> FromParseRule(Generated.EnforceParser.StatementBlockContext ctx) {
        if (ctx.statement() is not { } statements) return this;
        foreach (var statement in statements) Statements.Add(EsStatementFactory.Create(statement));
        return this;
    }

    public string ToEnforce() {
        var builder = new StringBuilder("{\n");
        Statements.ForEach(s => builder.Append(s.ToEnforce()).Append('\n'));
        return builder.Append('}').ToString();
    }
}