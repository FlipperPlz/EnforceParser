package com.flipper.enforce.dayz.objects;

import com.flipper.enforce.dayz.enums.PrimitiveTypes;
import com.flipper.enforce.dayz.enums.VariableModifiers;
import com.flipper.enforce.dayz.objects.clazz.EnforceClassDefinition;

public class EnforceVariable {
    private final EnforceClassDefinition parentClass;
    private final VariableModifiers[] modifiers;
    private final PrimitiveTypes dataType;
    private final String name;
    private final int lineNumber;
    private Object value;

    public EnforceVariable(EnforceClassDefinition parentClass, VariableModifiers[] modifiers, PrimitiveTypes dataType, String varName, int lineNumber) {
        this.parentClass = parentClass;
        this.modifiers = modifiers;
        this.dataType = dataType;
        this.name = varName;
        this.lineNumber = lineNumber;
    }

    public EnforceVariable(EnforceClassDefinition parentClass, VariableModifiers[] modifiers, PrimitiveTypes dataType, String varName, Object value, int lineNumber) {
        this.parentClass = parentClass;
        this.modifiers = modifiers;
        this.dataType = dataType;
        this.name = varName;
        this.value = value;
        this.lineNumber = lineNumber;
    }

    public Object getValue() {
        return value;
    }

    public PrimitiveTypes getDataType() {
        return dataType;
    }

    public String getName() {
        return name;
    }

    public VariableModifiers[] getModifiers() {
        return modifiers;
    }

    public int getLineNumber() {
        return lineNumber;
    }
}
