﻿using System.Text;
using EnforceParser.Core.Models.Generics;
using EnforceParser.Core.Models.Scope;

namespace EnforceParser.Core.Models.Types; 

public class EsTypeDefDeclaration : IEsDeserializable<Generated.EnforceParser.TypedefDeclarationContext>, IEsType {
    public EsClassReference BaseType { get; set; }
    public bool IsBaseArray { get; set; } = false;

    public EsClassname CreatedType { get; set; }
    
    public IEsDeserializable<Generated.EnforceParser.TypedefDeclarationContext> FromParseRule(Generated.EnforceParser.TypedefDeclarationContext ctx) {
        if (ctx.fromType is null) throw new Exception();
        if (ctx.toType is null) throw new Exception();

        if (ctx.fromType.keyword() is not null) 
            BaseType = new EsClassReference() { Classname = new EsClassname() {Classname = ctx.fromType.keyword().GetText() } };
        else 
            BaseType = (EsClassReference) new EsClassReference().FromParseRule(ctx.fromType.classReference());
        if (ctx.LSBracket() is { }) IsBaseArray = true;

        CreatedType = (EsClassname)new EsClassname().FromParseRule(ctx.toType);
        
        return this;
    }
    public override string ToString() => ToEnforce();

    public string ToEnforce() {
        var builder = new StringBuilder("typedef ");
        builder.Append(BaseType.ToEnforce());
        if (IsBaseArray) builder.Append("[]");
        return builder.Append(' ').Append(CreatedType.ToEnforce()).Append(';').ToString();
    } 
}