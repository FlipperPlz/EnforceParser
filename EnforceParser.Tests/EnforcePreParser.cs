using EnforceParser.Core.Generated;
using EnforceParser.Core.Models.Standard;

namespace EnforceParser.Tests; 

public class EnforcePreParser : EnforceParserBaseListener {
    public EsEnforceScript Script;

    public override void ExitComputationalStart(Core.Generated.EnforceParser.ComputationalStartContext context) {
        Script = (EsEnforceScript) new EsEnforceScript().FromParseRule(context);
    }
}