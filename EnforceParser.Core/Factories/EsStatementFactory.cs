using EnforceParser.Core.Models.Expression;
using EnforceParser.Core.Models.Statements;

namespace EnforceParser.Core.Factories; 

public static class EsStatementFactory {
    public static IEsStatement Create(Generated.EnforceParser.StatementContext ctx) {
        if (ctx.esGoto is { } @goto) return (IEsStatement) new EsGotoStatement().FromParseRule(@goto);
        if (ctx.esIf is { } @if) return (IEsStatement) new EsIfStatement().FromParseRule(@if);
        if (ctx.esWhile is { } @while) return (IEsStatement) new EsWhileStatement().FromParseRule(@while);
        if (ctx.expressionaryStatement is { } expression) return (IEsStatement) new EsExpressionStatement().FromParseRule(expression);
        if (ctx.esContinue is { } @continue) return (IEsStatement) new EsContinueStatement().FromParseRule(@continue);
        if (ctx.esBreak is { } @break) return (IEsStatement) new EsBreakStatement().FromParseRule(@break);
        if (ctx.esReturn is { } @return) return (IEsStatement) new EsReturnStatement().FromParseRule(@return);
        if (ctx.esSemicolon is { }) return new EsSemicolonStatement();
        if (ctx.esForEach is { } @foreach) return (IEsStatement) new EsForEachStatement().FromParseRule(@foreach);
        if (ctx.esFor is { } @for) return (IEsStatement) new EsForStatement().FromParseRule(@for);
        if (ctx.esSwitch is { } @switch) return (IEsStatement) new EsSwitchStatement().FromParseRule(@switch);
        if (ctx.esDelete is { } delete) return (IEsStatement) new EsDeleteStatement().FromParseRule(delete);
        if (ctx.esVariableDeclaration is { } variableDeclaration) return (IEsStatement) new EsVariableDeclarationStatement().FromParseRule(variableDeclaration);
        throw new Exception("The rule you have tried to call is not supported by the serialization base.");
    }
}