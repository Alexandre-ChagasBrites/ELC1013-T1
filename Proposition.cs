using System.Diagnostics;
using System.Xml.Linq;

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

    public enum PropositionPrecedence
    {
        None,
        Atomic,
        Not,
        And,
        Or,
        IfThen,
        IfOnlyIf
    }

    public abstract class PropositionNode : Node, IEquatable<PropositionNode>
    {
        public List<Node> subnodes;

        public abstract PropositionPrecedence Precedence();
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

        protected IEnumerable<ReadOnlyMemory<char>> PrintWrapped(PropositionNode node)
        {
            if (Precedence() < node.Precedence())
                yield return "(".AsMemory();
            foreach (var x in node.PrintProposition())
                yield return x;
            if (Precedence() < node.Precedence())
                yield return ")".AsMemory();
        }

        public static PropositionNode operator!(PropositionNode node)
        {
            return node is NotNode notNode ? notNode.node : new NotNode(node);
        }

        public static PropositionNode operator &(PropositionNode leftNode, PropositionNode rightNode)
        {
            return new AndNode(leftNode, rightNode);
        }

        public static PropositionNode operator |(PropositionNode leftNode, PropositionNode rightNode)
        {
            return new OrNode(leftNode, rightNode);
        }
    }

    public struct Evaluator()
    {
        internal ulong truthyness;
        internal List<string> atomics;
        // TODO Deveria checar que há no máximo 63 ou 63 atômicos...
    }

    public class PremiseNode : PropositionNode
    {
        public PropositionNode node;

        public override PropositionPrecedence Precedence() => node.Precedence();
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
        /// imutabilidade: a comparação por igualdade é em tempo constante (por
        /// referência).
        /// </summary>
        public string name;

        public override PropositionPrecedence Precedence() => PropositionPrecedence.Atomic;

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
        public override PropositionPrecedence Precedence() => PropositionPrecedence.Not;

        public override sealed bool Eval(ref readonly Evaluator context) => !node.Eval(in context);

        public override IEnumerable<ReadOnlyMemory<char>> PrintProposition()
        {
            yield return "¬".AsMemory();
            foreach (var x in PrintWrapped(node))
                yield return x;
        }

        public override IEnumerable<ReadOnlyMemory<char>> PrintGuess()
        {
            yield return "não é verdade que ".AsMemory();
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
            foreach (var x in PrintWrapped(leftNode))
                yield return x;
            yield return PropositionOperator;
            foreach (var x in PrintWrapped(rightNode))
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
