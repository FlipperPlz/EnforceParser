using EnforceParser.Core.Models.Types;

namespace EnforceParser.Core.Models.Standard; 

public class EsEnforceScript : IEsDeserializable<Generated.EnforceParser.ComputationalStartContext> {
    public List<IEsGlobalStatement> GlobalStatements = new List<IEsGlobalStatement>();
    public List<IEsType> DeclaredTypes = new();
    
    public IEsDeserializable<Generated.EnforceParser.ComputationalStartContext> FromParseRule(Generated.EnforceParser.ComputationalStartContext ctx) {
        throw new NotImplementedException();
    }

    public string ToEnforce() {
        throw new NotImplementedException();
    }
}