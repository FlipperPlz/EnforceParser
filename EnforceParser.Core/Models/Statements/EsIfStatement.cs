using System.Text;
using EnforceParser.Core.Factories;
using EnforceParser.Core.Models.Expression;
using EnforceParser.Core.Models.Expression.Primary;

namespace EnforceParser.Core.Models.Statements; 

public class EsIfStatement : IEsStatement, IEsDeserializable<Generated.EnforceParser.IfStatementContext> {
    public EsParenthesisedExpression Condition { get; set; }
    public List<IEsStatement> Statements { get; set; } = new();
    public EsElseStatement? ElseStatement { get; set; } = null;

    public IEsDeserializable<Generated.EnforceParser.IfStatementContext> FromParseRule(Generated.EnforceParser.IfStatementContext ctx) {
        if (ctx.statementSingleOrBlock() is not { } statementSingleOrBlock) throw new Exception();
        if (ctx.parenthesisedExpression() is not { } condition) throw new Exception();
        Condition = (EsParenthesisedExpression) new EsParenthesisedExpression().FromParseRule(condition);
        if (ctx.elseStatement() is { } @else) ElseStatement = (EsElseStatement) new EsElseStatement().FromParseRule(@else);
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
        var builder = new StringBuilder("if ").Append(Condition.ToEnforce()).Append(' ');
        if (Statements.Count <= 0) {
            builder.Append(Statements[0].ToEnforce());
        } else {
            builder.Append('{').Append('\n');
            Statements.ForEach(s => builder.Append(s.ToEnforce()).Append('\n'));
            builder.Append('}');
        }

        if (ElseStatement is not null) builder.Append(ElseStatement.ToEnforce());
        return builder.ToString();
    }
}