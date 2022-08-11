using System.Text;
using EnforceParser.Core.Factories;
using EnforceParser.Core.Models.Expression;

namespace EnforceParser.Core.Models.Statements; 

public class EsForStatement : IEsStatement , IEsDeserializable<Generated.EnforceParser.ForStatementContext> {
    public IEsStatement ForInit { get; set; }
    public IEsExpression ForCondition { get; set; }
    public IEsExpression? ForIteration { get; set; }
    
    public List<IEsStatement> Statements { get; set; } = new();

    
    public IEsDeserializable<Generated.EnforceParser.ForStatementContext> FromParseRule(Generated.EnforceParser.ForStatementContext ctx) {
        if (ctx.forControl() is not { } forControl) throw new Exception();
        if (ctx.statementSingleOrBlock() is not { } statementSingleOrBlock) throw new Exception();
        if (forControl.forInit is not { } forInit) throw new Exception();
        if (forControl.forCondition is not { } forCondition) throw new Exception();
        if (forControl.forIteration is { } forIteration) ForIteration = EsExpressionFactory.Create(forIteration);
        
        ForInit = EsStatementFactory.Create(forInit);
        ForCondition = EsExpressionFactory.Create(forCondition);
        
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
        var builder = new StringBuilder("for (").Append(ForInit.ToEnforce()).Append(ForCondition.ToEnforce()).Append("; ");
        if (ForIteration is not null) builder.Append(ForIteration.ToEnforce());
        builder.Append(") ");
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