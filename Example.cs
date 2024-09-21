using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Example
{
    public static void Main()
    {
        string src = """
            if{Se p{o time joga bem}}, then{q{ganha o campeonato}}.
            if{Se o time not{não p{joga bem}}}, then{r{o técnico é culpado}}.
            if{Se q{o time ganha o campeonato}}, then{s{os torcedores ficam contentes}}.
            Os torcedores not{não s{estão contentes}}.
            Logo, r{o técnico é culpado}.
            """;
        src = src.Replace("\r\n", "\n");

        Parser parser = new Parser(src);
        parser.Parse();

        foreach (string atomic in parser.Atomics)
        {
            Console.WriteLine($"Atomic: {atomic}");
        }

        foreach (Parser.PremiseNode node in parser.Nodes)
        {
            node.Print();
            Console.Write(" ");
            node.PrintProposition();
            Console.WriteLine();
        }
    }
}
