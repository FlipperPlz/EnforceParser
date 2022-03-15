package com.flipper.enforce.dayz.enums;

public enum PrimitiveTypes {
    INT("int", 0),
    FLOAT("float", 0.0),
    BOOLEAN("bool", false),
    STRING("string", ""),
    VECTOR("vector", "(0.0,0.0,0.0)"),
    VOID("void"),
    CLASS("Class", null),
    TYPENAME("typename", null);

    private final String type;
    private final Object defaultValue;
    PrimitiveTypes(String type, Object defaultValue) {
        this.type = type;
        this.defaultValue = defaultValue;
    }

    PrimitiveTypes(String type) {
        this.type = type;
        this.defaultValue = 0xFF; //Datatype void has no value
    }

    public String syntax() {
        return this.getType();
    }

    public static PrimitiveTypes getPrimitiveType(String name) {
        for(PrimitiveTypes type : values()) if(type.syntax().equalsIgnoreCase(name)) return type;
        return null;
    }

    public String getType() {
        return type;
    }

    public Object getDefaultValue() {
        return defaultValue;
    }
}
