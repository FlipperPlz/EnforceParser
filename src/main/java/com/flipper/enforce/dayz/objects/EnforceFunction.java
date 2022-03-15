package com.flipper.enforce.dayz.objects;

import com.flipper.enforce.dayz.enums.PrimitiveTypes;
import com.flipper.enforce.dayz.objects.clazz.EnforceClassDefinition;

import java.util.Arrays;
import java.util.List;

public class EnforceFunction {
    private final EnforceClassDefinition parentClass;
    private final boolean override;
    private final PrimitiveTypes returnType;
    private final String functionName;
    private final List<FunctionParameter> parameters;

    // Variables pertaining to the code inside a function.
    private String functionBody;
    private int startBodyLn, startBodyCol, stopBodyLn, stopBodyCol;

    public EnforceFunction(EnforceClassDefinition parentClass, boolean override, PrimitiveTypes returnType, String functionName, int startBodyLn, int startBodyCol, int stopBodyLn, int stopBodyCol, FunctionParameter... parameters) {
        this.parentClass = parentClass;
        this.override = override;
        this.returnType = returnType;
        this.functionName = functionName;
        this.parameters = Arrays.asList(parameters);
        this.startBodyLn = startBodyLn;
        this.startBodyCol = startBodyCol;
        this.stopBodyLn = stopBodyLn;
        this.stopBodyCol = stopBodyCol;
    }
    public EnforceFunction(EnforceClassDefinition parentClass, boolean override, PrimitiveTypes returnType, String functionName, List<FunctionParameter> parameters, int startBodyLn, int startBodyCol, int stopBodyLn, int stopBodyCol) {
        this.parentClass = parentClass;
        this.override = override;
        this.returnType = returnType;
        this.functionName = functionName;
        this.parameters = parameters;
    }


    public boolean isOverride() {
        return override;
    }

    public PrimitiveTypes getReturnType() {
        return returnType;
    }

    public String getFunctionName() {
        return functionName;
    }

    public List<FunctionParameter> getParameterTypes() {
        return parameters;
    }

    public FunctionParameter getParameterType(int param) {
        return parameters.get(param);
    }

    public String getFunctionBody() {
        return functionBody;
    }

    public void setFunctionBody(String functionBody) {
        this.functionBody = functionBody;
    }

    public int getStartBodyLn() {
        return startBodyLn;
    }

    public int getStartBodyCol() {
        return startBodyCol;
    }

    public int getStopBodyLn() {
        return stopBodyLn;
    }

    public int getStopBodyCol() {
        return stopBodyCol;
    }

    public static class FunctionParameter {
        private final PrimitiveTypes paramType;
        private final String paramName;
        private final Object defaultParamValue;

        public FunctionParameter(PrimitiveTypes type, String paramName, Object defaultParamValue) {
            this.paramType = type;
            this.paramName = paramName;
            this.defaultParamValue = defaultParamValue;
        }

        public PrimitiveTypes getParamType() {
            return paramType;
        }

        public String getParamName() {
            return paramName;
        }

        public Object getDefaultParamValue() {
            return defaultParamValue;
        }

    }
}
