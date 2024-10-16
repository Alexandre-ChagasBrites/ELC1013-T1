namespace ELC1013_T1
{
    public abstract class Node
    {
        public abstract IEnumerable<ReadOnlyMemory<char>> Print();
    }

    public class TextNode : Node
    {
        public ReadOnlyMemory<char> text;

        public override IEnumerable<ReadOnlyMemory<char>> Print()
        {
            yield return text;
        }
    }

    public abstract class PropositionNode : Node, IEquatable<PropositionNode>
    {
        public List<Node> subnodes;

        public abstract bool Eval(ref readonly Evaluator context);

        public override IEnumerable<ReadOnlyMemory<char>> Print()
        {
            foreach (Node node in subnodes)
            {
                foreach (var x in node.Print())
                    yield return x;
            }
        }

        public abstract IEnumerable<ReadOnlyMemory<char>> PrintProposition();
        public abstract IEnumerable<ReadOnlyMemory<char>> PrintGuess();
        public abstract bool Equals(PropositionNode other);
    }

    public struct Evaluator()
    {
        internal ulong truthyness;
        internal List<string> atomics;
        // TODO Deveria checar que h� no m�ximo 63 ou 63 at�micos...
    }

    public class PremiseNode : PropositionNode
    {
        public PropositionNode node;

        public override sealed bool Eval(ref readonly Evaluator context) => node.Eval(in context);

        public override IEnumerable<ReadOnlyMemory<char>> PrintProposition()
        {
            foreach (var x in node.PrintProposition())
                yield return x;
        }

        public override IEnumerable<ReadOnlyMemory<char>> PrintGuess()
        {
            foreach (var x in node.PrintGuess())
                yield return x;
        }

        public override bool Equals(PropositionNode other)
        {
            return other is PremiseNode premise && premise.node.Equals(node);
        }
    }

    public class AtomicNode : PropositionNode
    {
        /// <summary>
        /// DEVE continuar sendo <see langword="string"/>, por causa da
        /// imutabilidade: a compara��o por igualdade � em tempo constante (por
        /// refer�ncia).
        /// </summary>
        public string name;

        public override sealed bool Eval(ref readonly Evaluator context)
        {
              return 1u == ((context.truthyness >> context.atomics.IndexOf(name)) & 1u);
        }

        public override IEnumerable<ReadOnlyMemory<char>> PrintProposition()
        {
            yield return name.AsMemory();
        }

        public override IEnumerable<ReadOnlyMemory<char>> PrintGuess() => Print();

        public override bool Equals(PropositionNode other)
        {
            return other is AtomicNode atomic && atomic.name == name;
        }
    }

    public class NotNode : PropositionNode
    {
        public PropositionNode node;

        public NotNode(PropositionNode pn)
        {
            node = pn;
        }

        public override sealed bool Eval(ref readonly Evaluator context) => !node.Eval(in context);

        public override IEnumerable<ReadOnlyMemory<char>> PrintProposition()
        {
            yield return "~".AsMemory();
            foreach (var x in node.PrintProposition())
                yield return x;
        }

        public override IEnumerable<ReadOnlyMemory<char>> PrintGuess()
        {
            yield return "n�o � verdade que ".AsMemory();
            foreach (var x in node.PrintGuess())
                yield return x;
        }

        public override bool Equals(PropositionNode other)
        {
            return other is NotNode unary && unary.node.Equals(node);
        }
    }

    public abstract class BinaryNode : PropositionNode
    {
        public PropositionNode leftNode;
        public PropositionNode rightNode;

        public BinaryNode(PropositionNode lhs, PropositionNode rhs)
        {
            leftNode = lhs;
            rightNode = rhs;
        }

        public abstract ReadOnlyMemory<char> PropositionOperator { get; }
        public abstract ReadOnlyMemory<char> GuessBefore { get; }
        public abstract ReadOnlyMemory<char> GuessMiddle { get; }

        public override sealed IEnumerable<ReadOnlyMemory<char>> PrintProposition()
        {
            foreach (var x in leftNode.PrintProposition())
                yield return x;
            leftNode.PrintProposition();
            yield return PropositionOperator;
            foreach (var x in rightNode.PrintProposition())
                yield return x;
        }

        public override sealed IEnumerable<ReadOnlyMemory<char>> PrintGuess()
        {
            yield return GuessBefore;
            foreach (var x in leftNode.PrintGuess())
                yield return x;
            yield return GuessMiddle;
            foreach (var x in rightNode.PrintGuess())
                yield return x;
        }

        public override bool Equals(PropositionNode other)
        {
            return other is BinaryNode binary && binary.leftNode.Equals(leftNode) && binary.rightNode.Equals(rightNode);
        }
    }
}
