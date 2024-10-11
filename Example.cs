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
            ifthen{Se p{o time joga bem}, q{ganha o campeonato}}.
            ifthen{Se o time not{não p{joga bem}}, r{o técnico é culpado}}.
            ifthen{Se q{o time ganha o campeonato}, s{os torcedores ficam contentes}}.
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
        Console.WriteLine();

        foreach (PremiseNode node in parser.Premises)
        {
            node.Print();
            Console.Write(" ");
            node.PrintProposition();
            Console.WriteLine();
        }
        Console.WriteLine();

        Inferencer inferencer = new Inferencer();
        foreach (PremiseNode node in parser.Premises.Take(parser.Premises.Count - 1))
        {
            inferencer.Infer(node.node);
        }

        foreach (PropositionNode node in inferencer.Propositions)
        {
            node.PrintProposition();
            Console.Write(" ");
            node.PrintGuess();
            Console.WriteLine();
        }

        // Todo: Resultado em HTML
    }
}
