using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Text;
using EnforceParser.Core.Factories;
using EnforceParser.Core.Models.Expression.Primary.Primitives;

namespace EnforceParser.Core.Models.Expression.Primary; 

public class EsFunctionCall : IEsPrimaryExpression, IEsDeserializable<Generated.EnforceParser.FunctionCallContext> {
    public EsVariableName FunctionName { get; set; }
    public List<IEsFunctionCallParameter> FunctionParameters { get; set; } = new();


    public IEsDeserializable<Generated.EnforceParser.FunctionCallContext> FromParseRule(Generated.EnforceParser.FunctionCallContext ctx) {
        if (ctx.identifier() is not { } identifier) throw new Exception();
        FunctionName = (EsVariableName) new EsVariableName().FromParseRule(identifier);
        
        if(ctx.functionCallParameters() is { } parametersCtx) {
            if (parametersCtx.functionCallParameterList() is { } parameterListRule) {
                foreach (var parameter in parameterListRule.functionCallParameter()) {
                    if (parameter.expression() is { }) {
                        FunctionParameters.Add((IEsFunctionCallParameter) new EsFunctionCallParameter().FromParseRule(parameter));
                    } else if (parameter.optionalParameter() is { }) {
                        FunctionParameters.Add((IEsFunctionCallParameter) new EsFunctionCallOptionalParameter().FromParseRule(parameter));
                    } 
                }
            }            
        }

        return this;
    }

    public string ToEnforce() => new StringBuilder(FunctionName.ToEnforce()).Append('(')
        .Append(string.Join(", ", FunctionParameters.Select(f => f.ToEnforce()))).Append(')').ToString();
}

public interface IEsFunctionCallParameter : IEsDeserializable<Generated.EnforceParser.FunctionCallParameterContext> { }

public class EsFunctionCallParameter : IEsFunctionCallParameter{
    public IEsExpression ParameterValue { get; set; }
    
    public IEsDeserializable<Generated.EnforceParser.FunctionCallParameterContext> FromParseRule(Generated.EnforceParser.FunctionCallParameterContext ctx) {
        if (ctx.expression() is not { } expression) throw new Exception();
        ParameterValue = EsExpressionFactory.Create(expression);
        return this;
    }

    public string ToEnforce() => ParameterValue.ToEnforce();
}

public class EsFunctionCallOptionalParameter : IEsFunctionCallParameter {
    public string ParameterName { get; set; }
    public IEsExpression ParameterValue { get; set; }
    public IEsDeserializable<Generated.EnforceParser.FunctionCallParameterContext> FromParseRule(Generated.EnforceParser.FunctionCallParameterContext ctx) {
        if (ctx.expression() is not { } expression) throw new Exception();
        if (ctx.optionalParameter().identifier() is not { } identifier) throw new Exception();
        ParameterName = identifier.GetText();    
        ParameterValue = EsExpressionFactory.Create(expression);
        return this;
    }

    public string ToEnforce() => new StringBuilder(ParameterName).Append(':').Append(' ').Append(ParameterValue.ToEnforce()).ToString();
}
