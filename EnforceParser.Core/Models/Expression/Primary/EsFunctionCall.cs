﻿using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Text;
using EnforceParser.Core.Factories;
using EnforceParser.Core.Models.Expression.Primary.Primitives;
using EnforceParser.Core.Models.Scope;

namespace EnforceParser.Core.Models.Expression.Primary; 

public class EsFunctionCall : IEsPrimaryExpression, IEsDeserializable<Generated.EnforceParser.FunctionCallContext> {
    public EsFunctionName FunctionName { get; set; }
    public List<IEsFunctionCallParameter> FunctionParameters { get; set; } = new();
    public bool ReturnsArray { get; set; } = false;
    public IEsExpression? ArrayIndex { get; set; } = null;


    public IEsDeserializable<Generated.EnforceParser.FunctionCallContext> FromParseRule(Generated.EnforceParser.FunctionCallContext ctx) {
        if (ctx.identifier() is not { } identifier) throw new Exception();
        FunctionName = (EsFunctionName) new EsFunctionName().FromParseRule(identifier);
        
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

        if (ctx.RSBracket() is not null) ReturnsArray = true;
        if (ctx.expression() is not null) ArrayIndex = EsExpressionFactory.Create(ctx.expression());

        return this;
    }
    public override string ToString() => ToEnforce();

    public string ToEnforce() {
        var builder = new StringBuilder(FunctionName.ToEnforce()).Append('(')
            .Append(string.Join(", ", FunctionParameters.Select(f => f.ToEnforce()))).Append(')');
        if (ReturnsArray) {
            builder.Append('[');
            if(ArrayIndex is { } index) builder.Append(index.ToEnforce());
            builder.Append(']');
        }

        return builder.ToString();
    }
}

public interface IEsFunctionCallParameter : IEsDeserializable<Generated.EnforceParser.FunctionCallParameterContext> { }

public class EsFunctionCallParameter : IEsFunctionCallParameter{
    public IEsExpression ParameterValue { get; set; }
    
    public IEsDeserializable<Generated.EnforceParser.FunctionCallParameterContext> FromParseRule(Generated.EnforceParser.FunctionCallParameterContext ctx) {
        if (ctx.expression() is not { } expression) throw new Exception();
        ParameterValue = EsExpressionFactory.Create(expression);
        return this;
    }
    public override string ToString() => ToEnforce();
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
    public override string ToString() => ToEnforce();
    public string ToEnforce() => new StringBuilder(ParameterName).Append(':').Append(' ').Append(ParameterValue.ToEnforce()).ToString();
}
