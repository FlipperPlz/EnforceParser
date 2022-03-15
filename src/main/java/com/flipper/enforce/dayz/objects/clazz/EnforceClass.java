package com.flipper.enforce.dayz.objects.clazz;

import com.flipper.enforce.dayz.objects.EnforceFunction;
import com.flipper.enforce.dayz.objects.EnforceVariable;

import java.util.*;

public class EnforceClass {

    private final List<EnforceClassDefinition> classDefinitions;
    private final List<EnforceVariable> variables;
    private final List<EnforceFunction> functions;

    public EnforceClass(List<EnforceClassDefinition> classDefinitions, List<EnforceVariable> variables, List<EnforceFunction> functions) {
        this.classDefinitions = classDefinitions;
        this.variables = variables;
        this.functions = functions;
    }

    public List<EnforceClassDefinition> getClassDefinitions() {
        return classDefinitions;
    }

    public List<EnforceVariable> getVariables() {
        return variables;
    }

    public List<EnforceFunction> getFunctions() {
        return functions;
    }
}