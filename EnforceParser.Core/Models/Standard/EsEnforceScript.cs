using System.Text;
using EnforceParser.Core.Models.Globals;
using EnforceParser.Core.Models.Types;

namespace EnforceParser.Core.Models.Standard; 

public class EsEnforceScript : IEsDeserializable<Generated.EnforceParser.ComputationalStartContext> {
    public List<IEsGlobalStatement> GlobalStatements;
    public List<IEsType> DeclaredTypes;
    
    public IEsDeserializable<Generated.EnforceParser.ComputationalStartContext> FromParseRule(Generated.EnforceParser.ComputationalStartContext ctx) {
        if (ctx.globalDeclaration() is { } globalDeclarations) {
            foreach (var globalDeclaration in globalDeclarations) {
                if (globalDeclaration.functionDeclaration() is not null) {
                    GlobalStatements.Add((IEsGlobalStatement) new EsFunctionDeclaration().FromParseRule(globalDeclaration.functionDeclaration()));
                } else if (globalDeclaration.variableDeclaration() is not null) {
                    GlobalStatements.Add((IEsGlobalStatement) new EsVariableDeclarationStatement().FromParseRule(globalDeclaration.variableDeclaration()));
                } 
            }
        }

        if (ctx.typeDeclaration() is { } typeDeclarations) {
            foreach (var typeDeclaration in typeDeclarations) {
                if (typeDeclaration.classDeclaration() is { } classDeclaration) {
                    DeclaredTypes.Add((IEsType) new EsClassDeclaration().FromParseRule(classDeclaration));
                } else if (typeDeclaration.enumDeclaration() is { } enumDeclaration) {
                    DeclaredTypes.Add((IEsType) new EsEnumDeclaration().FromParseRule(enumDeclaration));
                } else if (typeDeclaration.typedefDeclaration() is { } typedefDeclaration) {
                    DeclaredTypes.Add((IEsType) new EsTypeDefDeclaration().FromParseRule(typedefDeclaration));
                }
            }
        }

        return this;
    }

    public string ToEnforce() {
        var builder = new StringBuilder();
        foreach (var globalStatement in GlobalStatements) builder.Append(globalStatement.ToEnforce()).Append('\n');
        foreach (var type in DeclaredTypes) builder.Append(type.ToEnforce()).Append('\n');
        return builder.ToString();
    }
}