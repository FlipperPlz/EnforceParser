using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using EnforceParser.Core.Generated;

namespace EnforceParser.Tests; 

public static class EnforceTesting {
    public static void Main() {
        var lexer = new EnforceLexer(CharStreams.fromPath(@"C:\Users\developer\Desktop\DayZ Modding\test.c.txt"));
        var parser = new Core.Generated.EnforceParser(new CommonTokenStream(lexer));
        var listener = new EnforcePreParser();
        new ParseTreeWalker().Walk(listener, parser.computationalStart());
        var script = listener.Script;
    }
}