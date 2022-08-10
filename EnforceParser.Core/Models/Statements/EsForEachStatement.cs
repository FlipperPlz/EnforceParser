﻿using System.Text;
using EnforceParser.Core.Factories;
using EnforceParser.Core.Models.Expression;
using EnforceParser.Core.Models.Expression.Primary.Primitives;
using EnforceParser.Core.Models.Scope;

namespace EnforceParser.Core.Models.Statements; 

public class EsForEachStatement : IEsStatement , IEsDeserializable<Generated.EnforceParser.ForeachStatementContext> {
    public List<EsForEachVariable> IteratingVariables { get; set; } = new();
    public IEsExpression Enumerable { get; set; }
    public List<IEsStatement> Statements { get; set; } = new();

    public IEsDeserializable<Generated.EnforceParser.ForeachStatementContext> FromParseRule(Generated.EnforceParser.ForeachStatementContext ctx) {
        if (ctx.statementSingleOrBlock() is not { } statementSingleOrBlock) throw new Exception();
        if (ctx.foreachVariable() is not { }) throw new Exception();
        if (ctx.expression() is not { }) throw new Exception();
        foreach (var variable in ctx.foreachVariable()) IteratingVariables.Add((EsForEachVariable) new EsForEachVariable().FromParseRule(variable));
        
        Enumerable = EsExpressionFactory.Create(ctx.expression());
        if (statementSingleOrBlock.statementBlock() is { } statementBlock) {
            foreach (var statement in statementBlock.statement()) Statements.Add(EsStatementFactory.Create(statement));
            return this;
        }
        if (statementSingleOrBlock.statement() is { }) {
            Statements.Add(EsStatementFactory.Create(statementSingleOrBlock.statement()));
            return this;
        }

        throw new Exception();
    }
    public override string ToString() => ToEnforce();
    public string ToEnforce() {
        var builder = new StringBuilder("foreach (").Append(string.Join(", ", IteratingVariables.Select(v => v.ToEnforce()))).Append(" : ")
            .Append(Enumerable.ToEnforce()).Append(") ");
        if (Statements.Count == 1) {
            builder.Append(Statements[0].ToEnforce());
        } else {
            builder.Append('{').Append('\n');
            Statements.ForEach(s => builder.Append(s.ToEnforce()).Append('\n'));
            builder.Append('}');
        }

        return builder.ToString();
    }
}

public class EsForEachVariable : IEsDeserializable<Generated.EnforceParser.ForeachVariableContext> {
    public EsClassReference VariableType { get; set; }
    public EsVariableName VariableNameName { get; set; }
    
    public IEsDeserializable<Generated.EnforceParser.ForeachVariableContext> FromParseRule(Generated.EnforceParser.ForeachVariableContext ctx) {
        if (ctx.iteratedVariableType is not { } variableType) throw new Exception();
        if (ctx.iteratedVariableName is not { } variableName) throw new Exception();

        VariableType = (EsClassReference) new EsClassReference().FromParseRule(variableType);
        VariableNameName = (EsVariableName) new EsVariableName().FromParseRule(variableName);
        return this;
    }

    public string ToEnforce() => new StringBuilder(VariableType.ToEnforce()).Append(' ')
        .Append(VariableNameName.ToEnforce()).ToString();
}