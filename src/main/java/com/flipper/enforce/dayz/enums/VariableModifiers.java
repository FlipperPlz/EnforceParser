package com.flipper.enforce.dayz.enums;

import java.util.Locale;

public enum VariableModifiers {
    PRIVATE, PROTECTED, STATIC, AUTOPTR, PROTO, REF, CONST, OUT, INOUT;

    public String syntaxCase() {
        return this.name().toLowerCase(Locale.ROOT);
    }

    public static VariableModifiers getVariableModifier(String modifierName) {
        for(VariableModifiers modifier : values()) if(modifier.name().equalsIgnoreCase(modifierName)) return modifier;
        return null;
    }
}
