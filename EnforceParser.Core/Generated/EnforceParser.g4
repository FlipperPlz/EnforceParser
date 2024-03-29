parser grammar EnforceParser;
@header {namespace EnforceParser.Core.Generated;}
options { tokenVocab=EnforceLexer; }

computationalStart: (globalDeclaration | typeDeclaration)* EOF;
globalDeclaration:  variableDeclaration | functionDeclaration;
typeDeclaration: classDeclaration | enumDeclaration | typedefDeclaration;

//SECTION: Variables & Functions
varAndFunctionBlock: (globalDeclaration) | LCurly (globalDeclaration)* RCurly;
variableDeclaration: annotation? variableModifier* variableType=classReference variableDeclarators Semicolon;
variableDeclarators: variableDeclarator (Comma variableDeclarator)*;
variableDeclarator: variableName=identifier (LSBracket arrayLength=expression? RSBracket)? (Assign variableValue=expression)?;
functionDeclaration: annotation? functionModifier* returnType=classReference (LSBracket RSBracket)? deconstructor=BitwiseNot? functionName=identifier functionParameters statementSingleOrBlock? Semicolon?;
functionParameters: LParenthesis (functionParameter (Comma functionParameter)*)? RParenthesis;
functionParameter: variableModifier* parameterType=classReference variableDeclarator;

//SECTION: Classes & Enums
classDeclaration: annotation? typeModifer* CLASS classname=identifier genericTypeDeclarationList? superclass=typeExtension_Child? classBody=varAndFunctionBlock? Semicolon?; 
enumDeclaration: annotation? typeModifer* ENUM enumname=identifier superenum=typeExtension_Child? enumBody Semicolon?;
enumBody: LCurly (enumValue ((Comma|WHITESPACES|Semicolon) enumValue)*)? Comma? Semicolon? RCurly Semicolon?;
enumValue: itemname=identifier (Assign itemValue=expression)?;

//SECTION: Expressions & Statements
expression:  primaryExpression                                                            |
             THIS                                                                         |
             SUPER                                                                        |
             expression op=Dot
                (
                 esVariable    = identifier            |
                 esFunction    = functionCall
                )                                                                         |
             objectCreation                                                               |
             castExpression                                                               |
             expression arrayIndex                                              |
             expression suffix=(Increment | Decrement)                                    |
             prefix=(Increment | Decrement | Bang | BitwiseNot | Add | Subtract) expression            |
             expression op=(Multiply | Divide | Modulo) expression                        |
             expression op=(/*Increment | Decrement |*/ Add | Subtract) expression        |
             expression rightShift expression                                          |
             expression leftShift expression                                           |
             expression op=(LessEqual | GreaterEqual | Less | Greater) expression         |
             expression op=(Equal | Inequal) expression                                   |
             expression op=(BitwiseOr | BitwiseAnd | BitwiseNot | BitwiseXor) expression  |
             expression op=(LogicalAnd | LogicalOr) expression                            |
             <assoc=right> expression op=
                 (
                  Assign           |
                  Add_Assign       |
                  Subtract_Assign  |
                  Multiply_Assign  |
                  Divide_Assign    |
                  Or_Assign        |
                  And_Assign       |
                  LShift_Assign    |
                  RShift_Assign
                 )
                 expression                                                               ;
castExpression: LParenthesis cast=classReference RParenthesis expression;
primaryExpression:  esFunction    = functionCall              |
                    esString      = literalString             |
                    esInt         = literalInteger            |
                    esFloat       = literalFloat              |
                    esBool        = literalBoolean            |
                    parExpression = parenthesisedExpression   |
                    esArray       = literalArray              |
                    esNull        = literalNull               |
                    esVariable    = identifier                |
                    esGeneric     = classReference            ;
objectCreation: NEW variableModifier* objectName=identifier typeList? functionCallParameters?;
functionCall: identifier functionCallParameters (LSBracket expression? RSBracket)?;
parenthesisedExpression: LParenthesis expression RParenthesis;
functionCallParameters: LParenthesis functionCallParameterList? RParenthesis;
functionCallParameterList: functionCallParameter (Comma functionCallParameter)*;
functionCallParameter: expression | optionalParameter;
optionalParameter: argumentName=identifier Colon argumentValue=expression;
//Notice: Statements go under here
statementSingleOrBlock: statement;
statementBlock: emptyBlock | LCurly statement* RCurly;
statement:   expressionaryStatement = expression Semicolon          |
             esVariableDeclaration  = variableDeclaration           |
             esDelete               = deleteStatement Semicolon     |
             esIf                   = ifStatement                   |
             esFor                  = forStatement                  |
             esForEach              = foreachStatement              |
             esWhile                = whileStatement                |
             esSwitch               = switchStatement               |
             esReturn               = returnStatement               |
             esBreak                = breakStatement                |
             esContinue             = continueStatement             |
             esStatementBlock       = statementBlock                |
             esGoto                 = gotoStatement /* Unused */    |
             esLambda               = lambdaStatement               |
             esThread               = threadStatement               |
             esSemicolon            = Semicolon                     ;
threadStatement: THREAD functionCall;
gotoStatement: GOTO expression Semicolon;
ifStatement: IF condition=parenthesisedExpression ifBody=statementSingleOrBlock elseStatement?;
elseStatement: ELSE elseBody=statementSingleOrBlock;
deleteStatement: DELETE expression;
forStatement: FOR LParenthesis forControl RParenthesis loopBody=statementSingleOrBlock;
foreachStatement: FOREACH LParenthesis foreachVariable (Comma foreachVariable)* Colon enumerating=expression RParenthesis loopBody=statementSingleOrBlock;
whileStatement: WHILE condition=parenthesisedExpression loopBody=statementSingleOrBlock;
switchStatement: SWITCH parenthesisedExpression LCurly switchBlockStatementGroup* /*switchLabel**/ RCurly;
returnStatement: RETURN expression? Semicolon;
breakStatement: BREAK Semicolon;
continueStatement: CONTINUE Semicolon;
lambdaStatement: lambdaType=classReference lambdaName=identifier  lambdaArguments=functionCallParameters Semicolon;
//SECTION: Common Rules
forControl: forInit=statement forCondition=expression Semicolon (forIteration=expression)? Semicolon*;
typeExtension_Child: extends=(EXTENDS | Colon) classname=classReference;
identifier: IDENTIFIER | TYPE_INT | TYPE_BOOL | TYPE_FLOAT | TYPE_STRING | TYPE_VECTOR | VOID | AUTO | TYPENAME | FUNC;
expressionList: expression (Comma expression)* Comma*;
arrayIndex: LSBracket expression? RSBracket;
literalArray: LCurly expressionList? RCurly;
literalString: LiteralString | PREPROC_LINE | PREPROC_FILE;
literalInteger: LiteralInteger;
literalNull: NULL;
literalFloat: LiteralFloat; //TODO: Scientific Notation
literalBoolean: LiteralBoolean;
foreachVariable: variableModifier* iteratedVariableType=classReference iteratedVariableName=identifier;
switchLabel: CASE (expression) Colon (statement* | statementSingleOrBlock); 
defaultSwitchLabel: DEFAULT Colon (statement* | statementSingleOrBlock);
switchBlockStatementGroup: switchLabel | defaultSwitchLabel;
emptyBlock: LCurly RCurly;
typedefDeclaration: annotation? 'typedef' fromType=typedefType (LSBracket RSBracket)? toType=identifier Semicolon;
typedefType: keyword | classReference ;
keyword: CLASS | ENUM | SWITCH | EXTENDS | CONST | BREAK | CASE | ELSE | FOR | CONTINUE | FOREACH | IF | NEW | RETURN | THIS | THREAD | VOID | WHILE | AUTOPTR | AUTO | REF | NULL | NOTNULL | FUNC | NATIVE | VOLATILE | PROTO | STATIC | OWNED | REFERENCE | OUT | PROTECTED | EVENT | TYPEDEF | MODDED | OVERRIDE | SEALED | INOUT | SUPER | TYPENAME | POINTER | GOTO | PRIVATE | EXTERNAL | DELETE | LOCAL | TYPE_INT | TYPE_FLOAT | TYPE_BOOL | TYPE_STRING | TYPE_VECTOR | LiteralBoolean | DEFAULT;
typeList: '<' genericType  (Comma genericType)* '>';
genericType: variableModifier* type=classReference ;
genericTypeDeclarationList:  Less genericTypeDeclaration (Comma genericTypeDeclaration)* Greater;
genericTypeDeclaration: variableModifier* type=classReference typeName=identifier  (LSBracket RSBracket)?; 
annotation: LSBracket functionCall RSBracket;
classReference: classname=identifier typeList?;
leftShift: Less Less;
rightShift: Greater Greater;
//SECTION: Modifiers
typeModifer: MODDED | SEALED;
variableModifier: PRIVATE |
               PROTECTED  |
               STATIC     |
               AUTOPTR    |
               PROTO      |
               REF        |
               REFERENCE  |
               CONST      |
               OWNED      |
               OUT        |
               NOTNULL    |
               INOUT      |
               LOCAL      ;
functionModifier: PRIVATE |
               EXTERNAL   |
               PROTECTED  |
               STATIC     |
               OVERRIDE   |
               OWNED      |
               REF        | 
               REFERENCE  |
               PROTO      |
               NATIVE     |
               VOLATILE   |
               EVENT      ;