using System.Text;
using EnforceParser.Core.Factories;
using EnforceParser.Core.Models.Expression.Primary;

namespace EnforceParser.Core.Models.Statements; 

public class EsWhileStatement : IEsStatement, IEsDeserializable<Generated.EnforceParser.WhileStatementContext> {
    public EsParenthesisedExpression Condition { get; set; }
    public List<IEsStatement> Statements { get; set; } = new();

    public IEsDeserializable<Generated.EnforceParser.WhileStatementContext> FromParseRule(Generated.EnforceParser.WhileStatementContext ctx) {
        if (ctx.parenthesisedExpression() is null) throw new Exception();
        if (ctx.statementSingleOrBlock() is not { } statementSingleOrBlock) throw new Exception();

        Condition = (EsParenthesisedExpression) new EsParenthesisedExpression().FromParseRule(ctx.parenthesisedExpression());
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
        var builder = new StringBuilder("while ").Append(Condition.ToEnforce()).Append(' ');
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