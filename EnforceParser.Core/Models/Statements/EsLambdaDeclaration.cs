using System.Text;
using EnforceParser.Core.Models.Expression.Primary;
using EnforceParser.Core.Models.Expression.Primary.Primitives;
using EnforceParser.Core.Models.Scope;

namespace EnforceParser.Core.Models.Statements; 

public class EsLambdaDeclaration : IEsStatement, IEsDeserializable<Generated.EnforceParser.LambdaStatementContext> {
    public EsClassReference LambdaType { get; set; }
    public EsVariableName LambdaName { get; set; }
    public List<IEsFunctionCallParameter> LambdaParameters { get; set; } = new();
    
    public IEsDeserializable<Generated.EnforceParser.LambdaStatementContext> FromParseRule(Generated.EnforceParser.LambdaStatementContext ctx) {
        if (ctx.lambdaType is null) throw new Exception();
        if (ctx.lambdaName is null) throw new Exception();
        LambdaType = (EsClassReference)new EsClassReference().FromParseRule(ctx.lambdaType);
        LambdaName = (EsVariableName)new EsVariableName().FromParseRule(ctx.lambdaName);
        if (ctx.lambdaArguments is not {} argumentCtx) throw new Exception();
        if (argumentCtx.functionCallParameterList() is not {} argumentListCtx) throw new Exception();

        foreach (var parameter in argumentListCtx.functionCallParameter()) {
            if (parameter.optionalParameter() is { }) {
                LambdaParameters.Add((IEsFunctionCallParameter) new EsFunctionCallOptionalParameter().FromParseRule(parameter));
            } else if (parameter.expression() is { }) {
                LambdaParameters.Add((IEsFunctionCallParameter) new EsFunctionCallParameter().FromParseRule(parameter));
            } 
        }

        return this;
    }

    public string ToEnforce() => new StringBuilder(LambdaType.ToEnforce()).Append(' ').Append(LambdaName.ToEnforce()).Append('(')
        .Append(string.Join(", ", LambdaParameters.Select(p => p.ToEnforce()))).Append(')').Append(';').ToString();
}