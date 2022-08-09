//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.10.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:/Users/developer/Desktop/EnforceParser/EnforceParser.Core/Generated\EnforceParser.g4 by ANTLR 4.10.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace EnforceParser.Core.Generated;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="EnforceParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.10.1")]
[System.CLSCompliant(false)]
public interface IEnforceParserVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.computationalStart"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitComputationalStart([NotNull] EnforceParser.ComputationalStartContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.globalDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGlobalDeclaration([NotNull] EnforceParser.GlobalDeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.typeDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTypeDeclaration([NotNull] EnforceParser.TypeDeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.varAndFunctionBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVarAndFunctionBlock([NotNull] EnforceParser.VarAndFunctionBlockContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.variableDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVariableDeclaration([NotNull] EnforceParser.VariableDeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.variableDeclarators"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVariableDeclarators([NotNull] EnforceParser.VariableDeclaratorsContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.variableDeclarator"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVariableDeclarator([NotNull] EnforceParser.VariableDeclaratorContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.functionDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunctionDeclaration([NotNull] EnforceParser.FunctionDeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.functionParameters"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunctionParameters([NotNull] EnforceParser.FunctionParametersContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.functionParameter"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunctionParameter([NotNull] EnforceParser.FunctionParameterContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.classDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitClassDeclaration([NotNull] EnforceParser.ClassDeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.enumDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitEnumDeclaration([NotNull] EnforceParser.EnumDeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.enumBody"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitEnumBody([NotNull] EnforceParser.EnumBodyContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.enumValue"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitEnumValue([NotNull] EnforceParser.EnumValueContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpression([NotNull] EnforceParser.ExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.primaryExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPrimaryExpression([NotNull] EnforceParser.PrimaryExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.objectCreation"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitObjectCreation([NotNull] EnforceParser.ObjectCreationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.functionCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunctionCall([NotNull] EnforceParser.FunctionCallContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.parenthesisedExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitParenthesisedExpression([NotNull] EnforceParser.ParenthesisedExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.functionCallParameters"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunctionCallParameters([NotNull] EnforceParser.FunctionCallParametersContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.functionCallParameterList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunctionCallParameterList([NotNull] EnforceParser.FunctionCallParameterListContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.functionCallParameter"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunctionCallParameter([NotNull] EnforceParser.FunctionCallParameterContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.optionalParameter"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOptionalParameter([NotNull] EnforceParser.OptionalParameterContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.arrayIndexExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArrayIndexExpression([NotNull] EnforceParser.ArrayIndexExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.statementSingleOrBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStatementSingleOrBlock([NotNull] EnforceParser.StatementSingleOrBlockContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.statementBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStatementBlock([NotNull] EnforceParser.StatementBlockContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStatement([NotNull] EnforceParser.StatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.gotoStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGotoStatement([NotNull] EnforceParser.GotoStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.ifStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIfStatement([NotNull] EnforceParser.IfStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.elseStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitElseStatement([NotNull] EnforceParser.ElseStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.deleteStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDeleteStatement([NotNull] EnforceParser.DeleteStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.forStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitForStatement([NotNull] EnforceParser.ForStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.foreachStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitForeachStatement([NotNull] EnforceParser.ForeachStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.whileStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitWhileStatement([NotNull] EnforceParser.WhileStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.switchStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSwitchStatement([NotNull] EnforceParser.SwitchStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.returnStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitReturnStatement([NotNull] EnforceParser.ReturnStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.breakStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBreakStatement([NotNull] EnforceParser.BreakStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.continueStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitContinueStatement([NotNull] EnforceParser.ContinueStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.forControl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitForControl([NotNull] EnforceParser.ForControlContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.typeExtension_Child"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTypeExtension_Child([NotNull] EnforceParser.TypeExtension_ChildContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.identifier"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIdentifier([NotNull] EnforceParser.IdentifierContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.expressionList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpressionList([NotNull] EnforceParser.ExpressionListContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.arrayIndex"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArrayIndex([NotNull] EnforceParser.ArrayIndexContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.literalArray"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLiteralArray([NotNull] EnforceParser.LiteralArrayContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.literalString"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLiteralString([NotNull] EnforceParser.LiteralStringContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.literalInteger"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLiteralInteger([NotNull] EnforceParser.LiteralIntegerContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.literalNull"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLiteralNull([NotNull] EnforceParser.LiteralNullContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.literalFloat"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLiteralFloat([NotNull] EnforceParser.LiteralFloatContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.literalBoolean"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLiteralBoolean([NotNull] EnforceParser.LiteralBooleanContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.foreachVariable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitForeachVariable([NotNull] EnforceParser.ForeachVariableContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.switchLabel"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSwitchLabel([NotNull] EnforceParser.SwitchLabelContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.switchBlockStatementGroup"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSwitchBlockStatementGroup([NotNull] EnforceParser.SwitchBlockStatementGroupContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.emptyBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitEmptyBlock([NotNull] EnforceParser.EmptyBlockContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.typedefDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTypedefDeclaration([NotNull] EnforceParser.TypedefDeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.typedefType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTypedefType([NotNull] EnforceParser.TypedefTypeContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.keyword"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitKeyword([NotNull] EnforceParser.KeywordContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.typeList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTypeList([NotNull] EnforceParser.TypeListContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.genericType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGenericType([NotNull] EnforceParser.GenericTypeContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.genericTypeDeclarationList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGenericTypeDeclarationList([NotNull] EnforceParser.GenericTypeDeclarationListContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.genericTypeDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGenericTypeDeclaration([NotNull] EnforceParser.GenericTypeDeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.annotation"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAnnotation([NotNull] EnforceParser.AnnotationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.typeModifer"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTypeModifer([NotNull] EnforceParser.TypeModiferContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.variableModifier"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVariableModifier([NotNull] EnforceParser.VariableModifierContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="EnforceParser.functionModifier"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunctionModifier([NotNull] EnforceParser.FunctionModifierContext context);
}
