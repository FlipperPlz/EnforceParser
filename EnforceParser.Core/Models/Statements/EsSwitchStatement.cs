using System.Text;
using EnforceParser.Core.Factories;
using EnforceParser.Core.Models.Expression;
using EnforceParser.Core.Models.Expression.Primary;

namespace EnforceParser.Core.Models.Statements; 

public class EsSwitchStatement : IEsStatement, IEsDeserializable<Generated.EnforceParser.SwitchStatementContext> {
    public EsParenthesisedExpression Switching { get; set; }
    public List<IEsSwitchCase> Cases { get; set; } = new();
    
    public IEsDeserializable<Generated.EnforceParser.SwitchStatementContext> FromParseRule(Generated.EnforceParser.SwitchStatementContext ctx) {
        if (ctx.parenthesisedExpression() is not { } expression) throw new Exception();
        Switching = (EsParenthesisedExpression)new EsParenthesisedExpression().FromParseRule(expression);
        if (ctx.switchBlockStatementGroup() is not { } switchBlock) throw new Exception();
        foreach (var @case in switchBlock) {
            if (@case.defaultSwitchLabel() is { } defaultSwitchLabel) Cases.Add((IEsSwitchCase) new EsDefaultSwitchCase().FromParseRule(defaultSwitchLabel));
            else if (@case.switchLabel() is { } switchLabel) Cases.Add((IEsSwitchCase) new EsSwitchCase().FromParseRule(switchLabel));
        }

        return this;
    }
    public override string ToString() => ToEnforce();
    public string ToEnforce() {
        var builder = new StringBuilder("switch").Append(Switching.ToEnforce()).Append(" {\n");
        Cases.ForEach(c => builder.Append(c.ToEnforce()).Append('\n'));
        return builder.Append('}').ToString();
    }
    
}

public interface IEsSwitchCase : IEsSerializable { public List<IEsStatement> CaseBody { get; set; } }

public class EsDefaultSwitchCase : IEsSwitchCase, IEsDeserializable<Generated.EnforceParser.DefaultSwitchLabelContext> {
    public List<IEsStatement> CaseBody { get; set; } = new();

    public IEsDeserializable<Generated.EnforceParser.DefaultSwitchLabelContext> FromParseRule(Generated.EnforceParser.DefaultSwitchLabelContext ctx) {
        if (ctx.statement() is { }) {
            foreach (var statement in ctx.statement()) CaseBody.Add(EsStatementFactory.Create(statement));
            return this;
        }
        if (ctx.statementSingleOrBlock() is not { } statementSingleOrBlock) throw new Exception();

        if (statementSingleOrBlock.statement() is { }) {
            CaseBody.Add(EsStatementFactory.Create(statementSingleOrBlock.statement()));
            return this;
        }

        throw new Exception();
    }
    public override string ToString() => ToEnforce();

    public string ToEnforce() {
        var builder = new StringBuilder("default: ");
        if (CaseBody.Count == 1) {
            builder.Append(CaseBody[0].ToEnforce());
        } else {
            builder.Append('{').Append('\n');
            foreach (var statement in CaseBody) builder.Append(statement.ToEnforce()).Append('\n');
            builder.Append('}');
        }

        return builder.ToString();
    }
}

public class EsSwitchCase : IEsSwitchCase, IEsDeserializable<Generated.EnforceParser.SwitchLabelContext> {
    public IEsExpression CaseExpression { get; set; }
    public List<IEsStatement> CaseBody { get; set; } = new();
    public override string ToString() => ToEnforce();

    public IEsDeserializable<Generated.EnforceParser.SwitchLabelContext> FromParseRule(Generated.EnforceParser.SwitchLabelContext ctx) {
        if (ctx.expression() is null) throw new Exception();
        CaseExpression = EsExpressionFactory.Create(ctx.expression());
        if (ctx.statement() is { }) {
            foreach (var statement in ctx.statement()) CaseBody.Add(EsStatementFactory.Create(statement));
            return this;
        }
        if (ctx.statementSingleOrBlock() is not { } statementSingleOrBlock) throw new Exception();
        if (statementSingleOrBlock.statement() is { }) {
            CaseBody.Add(EsStatementFactory.Create(statementSingleOrBlock.statement()));
            return this;
        }

        throw new Exception();
    }

    public string ToEnforce() {
        var builder = new StringBuilder("case ");
        builder.Append(CaseExpression.ToEnforce()).Append(':').Append(' ');
        if (CaseBody.Count == 1) {
            builder.Append(CaseBody[0].ToEnforce());
        } else {
            builder.Append('{').Append('\n');
            foreach (var statement in CaseBody) builder.Append(statement.ToEnforce()).Append('\n');
            builder.Append('}');
        }

        return builder.ToString();
    }

}