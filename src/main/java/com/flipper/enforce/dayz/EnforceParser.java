package com.flipper.enforce.dayz;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.flipper.enforce.dayz.enums.PrimitiveTypes;
import com.flipper.enforce.dayz.enums.VariableModifiers;
import com.flipper.enforce.dayz.objects.EnforceFunction;
import com.flipper.enforce.dayz.objects.EnforceVariable;
import com.flipper.enforce.dayz.objects.clazz.EnforceClass;
import com.flipper.enforce.dayz.objects.clazz.EnforceClassDefinition;

import java.io.*;
import java.util.*;

public class EnforceParser {
    private File inputFile;
    private BufferedReader inputFileReader;

    private List<EnforceClassDefinition> classDefinitions = new ArrayList<>();

    private String currentLine;
    private int currentLineNumber;


    public EnforceParser(File file) throws Exception {
        this.inputFile = file;
        this.inputFileReader = new BufferedReader(new FileReader(inputFile));
        this.currentLineNumber = 0;

        scanForClassDefinitions();

    }

    public static Object parseEnforceFile(File file) {
        Set<String> linesInFile = new HashSet<>();

        try {
            FileReader fileReader = new FileReader(file);
            BufferedReader br = new BufferedReader(fileReader);
            String line;
            while ((line = br.readLine()) != null) linesInFile.add(line);
        } catch (Exception e) {
            e.printStackTrace();
        }

        //TODO: Parse global functions and variables

        //Set<EnforceClassDefinition> classDefinitions = scanForClassDefinitions1(linesInFile);
        //List<EnforceFunction> allFunctionDefinitions = scanForFunctionDefinitions(linesInFile, classDefinitions);

        //List<Integer> linesContainingFunctions = new ArrayList<>();

        //for(EnforceFunction function : allFunctionDefinitions) for (int i = function.getStartBodyLn(); i <= function.getStopBodyLn(); i++) linesContainingFunctions.add(i);

        //List<EnforceVariable> allVariableDeclarations = scanForAllVariableDeclarations(linesInFile, classDefinitions, linesContainingFunctions);

        //EnforceClass clazz = new EnforceClass(classDefinitions.stream().toList(), allVariableDeclarations, allFunctionDefinitions);
        try {
            System.out.println("Parsed: \n");
            //System.out.println(new ObjectMapper().writerWithDefaultPrettyPrinter().writeValueAsString(clazz));
        } catch (Exception e) {
            e.printStackTrace();
        }

        return new Object();
    }

    public List<EnforceClassDefinition> scanForClassDefinitions() throws Exception {
        List<EnforceClassDefinition> returnList = new ArrayList<>();
        inputFileReader.reset();

        this.currentLine = inputFileReader.readLine();
        this.currentLineNumber = 0;
        while (currentLine != null) {

            if(this.currentLine.contains("class")) {
                StringTokenizer lineTokenizer = new StringTokenizer(currentLine);
                String currentWord = lineTokenizer.nextToken();
                if (currentWord.equals("modded") && lineTokenizer.nextToken().equalsIgnoreCase("class")) {
                    String className = (lineTokenizer.nextToken());
                    boolean moddedClass = true;

                    while (!currentLine.contains("{")) nextLine();

                    final int startLine = currentLineNumber;
                    final int startCol = currentLine.indexOf('{');

                    int[] endLocation = findEndBraceLocation(startLine, startCol);

                    final int stopLine = endLocation[0];
                    final int stopCol = endLocation[1];

                    returnList.add(new EnforceClassDefinition(className, moddedClass, startLine, startCol, stopLine, stopCol));

                } else if (currentWord.equals("class")) {
                    String className = (lineTokenizer.nextToken());
                    boolean moddedClass = false;

                    while (!currentLine.contains("{")) nextLine();

                    final int startLine = currentLineNumber;
                    final int startCol = currentLine.indexOf('{');

                    int[] endLocation = findEndBraceLocation(startLine, startCol);

                    final int stopLine = endLocation[0];
                    final int stopCol = endLocation[1];
                    returnList.add(new EnforceClassDefinition(className, moddedClass, startLine, startCol, stopLine, stopCol));
                }
            }

            this.currentLine = inputFileReader.readLine();
            this.currentLineNumber++;
        }
        return returnList;
    }

    private static List<EnforceFunction> scanForFunctionDefinitions(Set<String> linesInFile, Set<EnforceClassDefinition> classDefinitions) {
        List<EnforceFunction> returnList = new ArrayList<>();
        List<String> lines = linesInFile.stream().toList();

        for(EnforceClassDefinition classDefinition : classDefinitions) {
            for(int j = (classDefinition.getDefinitionStartLn() - 1); j <= (classDefinition.getDefinitionStopLn() - 1); j++) {
                int iterator = j - 1;

                String currentLine = lines.get(iterator);
                String[] words = getWordsInLine(currentLine);

                if(words.length <= 1 || Arrays.asList(words).contains("class ") || Arrays.asList(words).contains("modded ")) continue;

                boolean override = false;
                boolean foundReturnType = false;
                PrimitiveTypes returnType = null;
                String functionName = null;
                List<EnforceFunction.FunctionParameter> parameters = new ArrayList<>();
                VariableModifiers mod = VariableModifiers.getVariableModifier(words[1]);  //NULL IF NONE ARE FOUND

                for(int i = 0; i < words.length; i++) {
                    if(words[i] == "override" && !foundReturnType) override = true;

                    PrimitiveTypes _primitiveType = PrimitiveTypes.getPrimitiveType(words[i]);
                    if(_primitiveType != null) {
                        returnType = _primitiveType;
                        foundReturnType = true;

                        String _functionDef = words[i + 1];
                        String[] _functionDefSplit = _functionDef.split("\\(");

                        functionName = _functionDefSplit[0];
                        String _parameters;
                        if(_functionDefSplit.length >= 2) _parameters = _functionDefSplit[1];

                        String[] _parameters_split = (currentLine.split("//")[0]).trim().replaceAll("="," =").split(",");

                        int bodyStart = 0, bodyStartCol = 0, bodyEnd = 0, bodyEndCol = 0;
                        boolean foundStart = false, foundEnd = false;

                        while (!foundStart) {
                            var _currentLine = lines.get(iterator);
                            if(_currentLine.contains("{")) {
                                bodyStart = iterator;
                                bodyStartCol = _currentLine.indexOf("{");
                                foundStart = true;
                            } else {
                                iterator++;
                            } //TODO: Error message for missing brackets
                        }

                        getEndingBraceLocation(new HashSet<>(lines), bodyStart, bodyStartCol);


                        returnList.add(new EnforceFunction(classDefinition, override, returnType, functionName, parameters, bodyStart, bodyStartCol, bodyEnd, bodyEndCol));
                    } else {
                        System.out.println(String.format("[debug] %s is not a primitive type.", words[i]));
                        continue;
                    }
                }

            }
        }

        //TODO: Functions With No Parent Class
        return returnList.isEmpty() ? null : returnList;
    }

    private static List<EnforceVariable> scanForAllVariableDeclarations(Set<String> linesInFile, Set<EnforceClassDefinition> classDefinitions, List<Integer> linesContainingFunctions) {
        List<EnforceVariable> returnList = new ArrayList<>();
        List<String> lines = linesInFile.stream().toList();

        for (EnforceClassDefinition classDefinition : classDefinitions) {
            for (int j = (classDefinition.getDefinitionStartLn() - 1); j <= (classDefinition.getDefinitionStopLn() - 1); j++) {
                int iterator = j - 1;
                if(linesContainingFunctions.contains(iterator)) continue;

                String currentLine = lines.get(iterator);
                String[] splitLine = getWordsInLine(currentLine);

                List<VariableModifiers> modifiersUsed = new ArrayList<>();
                PrimitiveTypes variableType = null;
                String variableName = null, variableValue = null;

                boolean doneFindingModifiers = false, variableTypeFound = false, noModifiers = false;

                for(int i = 0; i < splitLine.length; i++) {
                    boolean skip = false;
                    String word = splitLine[i];
                    if (word == "") break;
                    System.out.println("onword: " + word);

                    //Find variable modifiers of declaration.
                    if (!doneFindingModifiers) {
                        VariableModifiers mod = VariableModifiers.getVariableModifier(word);
                        if (mod != null) {
                            switch (mod) {
                                case PRIVATE:
                                    modifiersUsed.add(VariableModifiers.PRIVATE);
                                    continue;
                                case PROTECTED:
                                    modifiersUsed.add(VariableModifiers.PROTECTED);
                                    continue;
                                case STATIC:
                                    modifiersUsed.add(VariableModifiers.STATIC);
                                    continue;
                                case AUTOPTR:
                                    modifiersUsed.add(VariableModifiers.AUTOPTR);
                                    continue;
                                case PROTO:
                                    modifiersUsed.add(VariableModifiers.PROTO);
                                    continue;
                                case REF:
                                    modifiersUsed.add(VariableModifiers.REF);
                                    continue;
                                case CONST:
                                    modifiersUsed.add(VariableModifiers.CONST);
                                    continue;
                                case OUT:
                                    modifiersUsed.add(VariableModifiers.OUT);
                                    continue;
                                case INOUT:
                                    modifiersUsed.add(VariableModifiers.INOUT);
                                    continue;
                                default:
                                    doneFindingModifiers = true;
                                    //DEBUG-LOG
                                    System.out.println(String.format("[debug] %s is NOT a Variable Modifier. Assuming all modifiers have been found.", word));
                                    continue;
                            }
                        } else {
                            if (currentLine.contains("class ") || currentLine.contains("{") || currentLine == "{") {
                                System.out.println("[error] Some shit broke :)");
                                skip = true;
                                break;
                            }
                            doneFindingModifiers = true;

                            //DEBUG-LOG
                            System.out.println(String.format("[debug] %s is NOT a Variable Modifier. Assuming all modifiers have been found.", word));
                        }
                    }

                    //Find primitive type of variable declaration.
                    if(doneFindingModifiers && !variableTypeFound && !skip) {
                        variableType = PrimitiveTypes.getPrimitiveType(word);
                        variableTypeFound = true;
                        //DEBUG-LOG
                        if(variableType != null) System.out.println(String.format("[debug] Primitive type was found: %s", variableType.syntax()));
                        continue;
                    }

                    //Register variables that lack a definition.
                    if(word.endsWith(";") && !skip) {
                        returnList.add(new EnforceVariable(classDefinition, modifiersUsed.stream().toArray(VariableModifiers[]::new), variableType, word.substring(0, word.length() - 1), (iterator + 1)));
                        continue;
                    }

                    if(variableName == null && variableType != null && !skip) {
                        variableName = word;
                        continue;
                    }else if(variableName != null && word.equals("=") && variableType != null && !skip) {
                        String arg = splitLine[i+1];
                        if(arg.endsWith(";")) {
                            variableValue = arg.substring(0, arg.length() - 1);
                            returnList.add(new EnforceVariable(classDefinition, modifiersUsed.stream().toArray(VariableModifiers[]::new), variableType, variableName, variableValue, (iterator + 1)));
                            continue;
                        }
                        continue;
                    }
                }
            }
        }

        return returnList;
    }


    private static String[] getWordsInLine(String line) {
        String[] splitLine = (line.split("//")[0]).trim().replaceAll("="," =").split(" ");
        return Arrays.stream(splitLine).filter(value -> value != null && value.length() > 0).toArray(size -> new String[size]);
    }

    private void nextLine() throws Exception {
        this.currentLine = inputFileReader.readLine();
        this.currentLineNumber++;
    }

    private int[] findEndBraceLocation(int startBraceLine, int startBraceCol) throws Exception {
        BufferedReader tempReader = this.inputFileReader;
        tempReader.reset();
        tempReader.skip(startBraceLine + 1);

        String tempLine = tempReader.readLine().substring(startBraceCol);//Maybe substring(startBraceCol - 1)
                                                                         //Or maybe even substring(startBraceCol + 1)
                                                                         //All I know is I'm too high to care or test it

        int nestedBraces = 0, tempLineNumber = 0;

        while (tempLine != null) {
            while(!currentLine.contains("{") && !currentLine.contains("{")) {
                tempLineNumber++;
                tempLine = tempReader.readLine();
            }

            int tempCharNumber = 0;
            for(char c : tempLine.toCharArray()) {
                switch (c) {
                    case '{':
                        if(nestedBraces == 0) return new int[] {tempLineNumber, tempCharNumber};
                        nestedBraces++;
                        continue;
                    case '}':
                        nestedBraces--;
                        continue;
                    default:
                        continue;
                }
            }

            tempLineNumber++;
            tempLine = tempReader.readLine();
        }
        return null;
    }

    private static int[] getEndingBraceLocation(Set<String> lines, int startingBraceLine, int startingBraceCol) {
        int iLine = 0;
        int nestedBraces = 0;
        for (String line : lines) {
            iLine++;
            if (iLine < startingBraceLine) continue;
            int iChar = -1;
            for (char c : line.toCharArray()) {
                iChar++;
                if (iChar <= startingBraceCol && iLine == startingBraceLine) continue;
                switch (c) {
                    case '{':
                        nestedBraces++;
                        continue;
                    case '}':
                        if (nestedBraces != 0) {
                            nestedBraces--;
                            continue;
                        } else {
                            return new int[]{iLine, iChar};
                        }
                    default:
                        continue;
                }
            }
        }
        return null;
    }
}
