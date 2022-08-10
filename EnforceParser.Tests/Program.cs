using System.Diagnostics;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using EnforceParser.Core.Generated;

namespace EnforceParser.Tests; 

public static class EnforceTesting {
    public static void Main() {
        var timer = new Stopwatch();
        foreach (var enforceFile in new DirectoryInfo("P:\\scripts\\").EnumerateFiles("*.c", SearchOption.AllDirectories)) {
            timer.Start();
            Console.WriteLine($"Parsing: {enforceFile.FullName}");
            var lexer = new EnforceLexer(CharStreams.fromPath(enforceFile.FullName));
            var parser = new Core.Generated.EnforceParser(new CommonTokenStream(lexer));
            var listener = new EnforcePreParser();
            new ParseTreeWalker().Walk(listener, parser.computationalStart());
            timer.Stop();
            if (parser.NumberOfSyntaxErrors != 0) throw new Exception();
            Console.WriteLine($"Parsed in {timer.ElapsedMilliseconds}ms.\n\n");
            
            timer.Reset();
        }
    }
}