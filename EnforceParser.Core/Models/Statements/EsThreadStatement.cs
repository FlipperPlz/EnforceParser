using System.Text;
using EnforceParser.Core.Models.Expression.Primary;

namespace EnforceParser.Core.Models.Statements; 

public class EsThreadStatement : IEsDeserializable<Generated.EnforceParser.ThreadStatementContext>, IEsStatement {
    public EsFunctionCall FunctionCall { get; set; }
    
    public IEsDeserializable<Generated.EnforceParser.ThreadStatementContext> FromParseRule(Generated.EnforceParser.ThreadStatementContext ctx) {
        if (ctx.functionCall() is null) throw new Exception();
        FunctionCall = (EsFunctionCall) new EsFunctionCall().FromParseRule(ctx.functionCall());
        return this;
    }

    public string ToEnforce() => new StringBuilder("thread ").Append(FunctionCall.ToEnforce()).Append(';').ToString();

    public override string ToString() => ToEnforce();
}