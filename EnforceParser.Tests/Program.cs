using System.Diagnostics;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using EnforceParser.Core.Generated;
using EnforceParser.Core.Models.Standard;

namespace EnforceParser.Tests; 

public static class EnforceTesting {
    public static void Main() {
        var timer = new Stopwatch();
        var script = new EsEnforceScript();
        int fileCount = 0;
        long totalElapsed = 0;
        foreach (var enforceFile in new DirectoryInfo("P:\\scripts\\").EnumerateFiles("*.c", SearchOption.AllDirectories)) {
            timer.Start();
            Console.WriteLine($"Parsing: {enforceFile.FullName}");
            var lexer = new EnforceLexer(CharStreams.fromPath(enforceFile.FullName));
            var parser = new Core.Generated.EnforceParser(new CommonTokenStream(lexer));
            var listener = new EnforcePreParser();
            new ParseTreeWalker().Walk(listener, parser.computationalStart());
            timer.Stop();
            script.GlobalStatements.AddRange(listener.Script.GlobalStatements);
            script.DeclaredTypes.AddRange(listener.Script.DeclaredTypes);
            totalElapsed += timer.ElapsedMilliseconds;
            if (parser.NumberOfSyntaxErrors != 0) {
                Console.WriteLine($"Parsed {fileCount} file(s) in {totalElapsed}ms.\n\n");
                throw new Exception();
            }
            Console.WriteLine($"Parsed in {timer.ElapsedMilliseconds}ms.\n\n");
            fileCount++;
            timer.Reset();
        }
        Console.WriteLine($"Parsed {fileCount} file(s) in {totalElapsed}ms.\n\n");
        Console.WriteLine("Writing all to file ");
        File.WriteAllText(@"C:\Users\developer\Desktop\DayZScripts.c", script.ToEnforce());
    }
}
