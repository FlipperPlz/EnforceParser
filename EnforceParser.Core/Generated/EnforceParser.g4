parser grammar EnforceParser;
@header {namespace EnforceParser.Core.Generated;}
options { tokenVocab=EnforceLexer; }
computationalStart: (globalDeclaration | typeDeclaration | typedefDeclaration)* EOF;
globalDeclaration:  variableDeclaration | functionDeclaration;
typeDeclaration: classDeclaration | enumDeclaration | typedefDeclaration;
//SECTION: Variables & Functions
varAndFunctionBlock: LCurly (functionDeclaration | variableDeclaration)* RCurly;
variableDeclaration: annotation? variableModifier* variableType=identifier typeList? variableDeclarators Semicolon;
variableDeclarators: variableDeclarator (Comma variableDeclarator)*;
variableDeclarator: variableName=identifier (LSBracket arrayLength=expression? RSBracket)? (Assign variableValue=expression)?;
functionDeclaration: annotation? functionModifier* returnType=identifier typeList? deconstructor=BitwiseNot? functionName=identifier functionParameters statementSingleOrBlock? Semicolon?;
functionParameters: LParenthesis (functionParameter (Comma functionParameter)*)? RParenthesis;
functionParameter: variableModifier* parameterType=identifier variableDeclarator;
//SECTION: Classes & Enums
classDeclaration: annotation? typeModifer* CLASS classname=identifier superclass=typeExtension_Child? classBody=varAndFunctionBlock Semicolon?; //TODO: Generic Types
enumDeclaration: annotation? typeModifer* ENUM enumname=identifier superenum=typeExtension_Child? enumBody Semicolon?;
enumBody: LCurly (enumValue ((Comma|WHITESPACES) enumValue)*)? RCurly;
enumValue: itemname=identifier (Assign itemValue=primaryExpression)?;
//SECTION: Expressions & Statements
expression:  primaryExpression                                                            |
             THIS                                                                         |
             SUPER                                                                        |
             expression op=Dot
                (
                 esVariable    = identifier            |
                 esArrayIndex  = identifier arrayIndex |
                 esFunction    = functionCall
                )                                                                         |
             objectCreation                                                               |
             expression suffix=(Increment | Decrement)                                    |
             prefix=(Increment | Decrement | Bang /*| Add | Subtract*/) expression        |
             expression (parenthesisedExpression) expression                              |
             expression op=(Multiply | Divide | Modulo) expression                        |
             expression op=(/*Increment | Decrement |*/ Add | Subtract) expression        |
             expression op=(LShift | RShift) expression                                   |
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
primaryExpression:  esFunction    = functionCall              |
                    esString      = literalString             |
                    esInt         = LiteralInteger            |
                    esFloat       = LiteralFloat              |
                    esBool        = LiteralBoolean            |
                    parExpression = parenthesisedExpression   |
                    esArray       = literalArray              |
                    esNull        = NULL                      |
                    esVariable    = identifier                |
//                  esGeneric     = identifier typeList       |
                    esArrayIndex  = identifier arrayIndex     ;
objectCreation: NEW variableModifier* objectName=identifier typeList? functionCallParameters?;
functionCall: identifier functionCallParameters;
parenthesisedExpression: LParenthesis expression RParenthesis;
functionCallParameters: LParenthesis functionCallParameterList? RParenthesis;
functionCallParameterList: functionCallParameter (Comma functionCallParameter)*;
functionCallParameter: expression | optionalParameter;
optionalParameter: argumentName=identifier Colon argumentValue=expression;
//Notice: Statements go under here
statementSingleOrBlock: statement | statementBlock;
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
             esGoto                 = gotoStatement /* Unused */    |
             esSemicolon            = Semicolon                     ;
gotoStatement: GOTO expression Semicolon;
ifStatement: IF condition=parenthesisedExpression ifBody=statementSingleOrBlock elseStatement?;
elseStatement: ELSE elseBody=statementSingleOrBlock;
deleteStatement: DELETE expression;
forStatement: FOR LParenthesis forControl RParenthesis loopBody=statementSingleOrBlock;
foreachStatement: FOREACH LParenthesis foreachVariable Colon enumerating=expression RParenthesis loopBody=statementSingleOrBlock;
whileStatement: WHILE condition=parenthesisedExpression loopBody=statementSingleOrBlock;
switchStatement: SWITCH parenthesisedExpression LCurly switchBlockStatementGroup* /*switchLabel**/ RCurly;
returnStatement: RETURN expression? Semicolon;
breakStatement: BREAK Semicolon;
continueStatement: CONTINUE Semicolon;
//SECTION: Common Rules
forControl: forInit=statement forCondition=expression Semicolon forIteration=expression Semicolon*;
typeExtension_Child: extends=(EXTENDS | Colon) classname=identifier;
identifier: IDENTIFIER | TYPE_INT | TYPE_BOOL | TYPE_FLOAT | TYPE_STRING | TYPE_VECTOR | VOID | AUTO | TYPENAME | FUNC;
expressionList: expression (Comma expression)*;
arrayIndex: LSBracket expression? RSBracket;
literalArray: LCurly expressionList? RCurly;
literalString: LiteralString | PREPROC_LINE | PREPROC_FILE;
foreachVariable: (identifier | AUTO) iteratedVariableName=identifier;
switchLabel: CASE (expression) Colon | DEFAULT Colon;
switchBlockStatementGroup: switchLabel  (statementBlock | statement*);
emptyBlock: LCurly RCurly;
typedefDeclaration: annotation? 'typedef' fromType=typedefType toType=typedefType Semicolon;
typedefType: keyword | identifier typeList?;
keyword: CLASS | ENUM | SWITCH | EXTENDS | CONST | BREAK | CASE | ELSE | FOR | CONTINUE | FOREACH | IF | NEW | RETURN | THIS | THREAD | VOID | WHILE | AUTOPTR | AUTO | REF | NULL | NOTNULL | FUNC | NATIVE | VOLATILE | PROTO | STATIC | OWNED | REFERENCE | OUT | PROTECTED | EVENT | TYPEDEF | MODDED | OVERRIDE | SEALED | INOUT | SUPER | TYPENAME | POINTER | GOTO | PRIVATE | EXTERNAL | DELETE | LOCAL | TYPE_INT | TYPE_FLOAT | TYPE_BOOL | TYPE_STRING | TYPE_VECTOR | LiteralBoolean | DEFAULT;
typeList: Less genericType (Comma genericType)* Greater;
genericType: variableModifier* type=identifier;
annotation: LSBracket functionCall RSBracket;
//SECTION: Modifiers
typeModifer: MODDED | SEALED;
variableModifier: PRIVATE |
               PROTECTED |
               STATIC |
               AUTOPTR |
               PROTO |
               REF | REFERENCE |
               CONST |
               OUT |
               INOUT;
functionModifier: PRIVATE |
               PROTECTED |
               STATIC |
               OVERRIDE |
               PROTO |
               NATIVE;