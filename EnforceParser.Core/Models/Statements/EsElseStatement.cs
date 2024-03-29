﻿using System.Text;
using EnforceParser.Core.Factories;

namespace EnforceParser.Core.Models.Statements; 

public class EsElseStatement : IEsStatement, IEsDeserializable<Generated.EnforceParser.ElseStatementContext> {
    public List<IEsStatement> Statements { get; set; } = new();
    
    public IEsDeserializable<Generated.EnforceParser.ElseStatementContext> FromParseRule(Generated.EnforceParser.ElseStatementContext ctx) {
        if (ctx.statementSingleOrBlock() is not { } statementSingleOrBlock) throw new Exception();
        if (statementSingleOrBlock.statement() is { }) {
            Statements.Add(EsStatementFactory.Create(statementSingleOrBlock.statement()));
            return this;
        }

        throw new Exception();
    }
    public override string ToString() => ToEnforce();
    public string ToEnforce() {
        var builder = new StringBuilder("else ");
        if (Statements.Count == 1) return builder.Append(Statements[0].ToEnforce()).ToString();
        builder.Append('{').Append('\n');
        Statements.ForEach(s => builder.Append(s.ToEnforce()).Append('\n'));
        builder.Append('}');
        return builder.ToString();
    }
}