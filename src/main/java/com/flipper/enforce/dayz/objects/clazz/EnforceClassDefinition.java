package com.flipper.enforce.dayz.objects.clazz;

public class EnforceClassDefinition {

    private final String className;
    private final boolean moddedClass;

    private final int definitionStartLn, definitionStartCol, definitionStopLn, definitionStopCol;

    public EnforceClassDefinition(String className, boolean moddedClass, int definitionStartLn, int definitionStartCol, int definitionStopLn, int definitionStopCol) {
        this.className = className;
        this.moddedClass = moddedClass;
        this.definitionStartLn = definitionStartLn;
        this.definitionStartCol = definitionStartCol;
        this.definitionStopLn = definitionStopLn;
        this.definitionStopCol = definitionStopCol;
    }

    public EnforceClassDefinition(String className, boolean moddedClass, int[] startLocation, int[] stopLocation) {
        this.className = className;
        this.moddedClass = moddedClass;
        this.definitionStartLn = startLocation[0];
        this.definitionStartCol = startLocation[1];
        this.definitionStopLn = stopLocation[0];
        this.definitionStopCol = stopLocation[1];
    }

    public String getClassName() {
        return className;
    }

    public boolean isModded() {
        return moddedClass;
    }

    public int getDefinitionStartLn() {
        return definitionStartLn;
    }

    public int getDefinitionStartCol() {
        return definitionStartCol;
    }

    public int getDefinitionStopLn() {
        return definitionStopLn;
    }

    public int getDefinitionStopCol() {
        return definitionStopCol;
    }
}
