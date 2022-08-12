using System.Text;
using EnforceParser.Core.Generated;
using EnforceParser.Core.Models.Standard;

namespace EnforceParser.Tests; 

public class SyntaxWarningListener : EnforceParserBaseListener
{
    public readonly List<string> SyntaxWarnings = new();
    public EsEnforceScript? Script { get; set; } = null;

    private bool _methodScope = false;

    public override void ExitComputationalStart(EnforceParser.Core.Generated.EnforceParser.ComputationalStartContext context)
    {
        Script = (EsEnforceScript?) new EsEnforceScript().FromParseRule(context);
        base.ExitComputationalStart(context);
    }

    public override void EnterFunctionDeclaration(EnforceParser.Core.Generated.EnforceParser.FunctionDeclarationContext context)
    {
        _methodScope = true;
        base.EnterFunctionDeclaration(context);
    }

    public override void ExitFunctionDeclaration(EnforceParser.Core.Generated.EnforceParser.FunctionDeclarationContext context)
    {
        _methodScope = false;
        base.ExitFunctionDeclaration(context);
    }

    public override void EnterVariableModifier(EnforceParser.Core.Generated.EnforceParser.VariableModifierContext context)
    {
        if (_methodScope && (context.GetText().ToLower().StartsWith("ref")) && context.Parent is not Core.Generated.EnforceParser.GenericTypeContext)
        {
            SyntaxWarnings.Add(new StringBuilder("[Ln").Append(context.Start.Line).Append(':').Append("Col")
                .Append(context.Start.Column).Append("] ").Append("Incorrect usage of ref/reference modifier.")
                .ToString());

        }
        base.EnterVariableModifier(context);
    }

    public override string ToString() => string.Join("\n", SyntaxWarnings);
}

