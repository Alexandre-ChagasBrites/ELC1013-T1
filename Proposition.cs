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

    public class PremiseNode : PropositionNode
    {
        public PropositionNode node;

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

        public override IEnumerable<ReadOnlyMemory<char>> PrintProposition()
        {
            yield return "~".AsMemory();
            foreach (var x in node.PrintProposition())
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
            return other is NotNode unary && /*unary.type == type &&*/ unary.node.Equals(node);
        }
    }

    public abstract class BinaryNode : PropositionNode
    {
        public PropositionNode leftNode;
        public PropositionNode rightNode;

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

    public sealed class AndNode : BinaryNode
    {
        public override ReadOnlyMemory<char> PropositionOperator => " ^ ".AsMemory();
        public override ReadOnlyMemory<char> GuessBefore => "".AsMemory();
        public override ReadOnlyMemory<char> GuessMiddle => " e ".AsMemory();

        public override bool Equals(PropositionNode other)
        {
            return other is AndNode && base.Equals(other);
        }
    }

    public sealed class OrNode : BinaryNode
    {
        public override ReadOnlyMemory<char> PropositionOperator => " V ".AsMemory();
        public override ReadOnlyMemory<char> GuessBefore => "".AsMemory();
        public override ReadOnlyMemory<char> GuessMiddle => " ou ".AsMemory();

        public override bool Equals(PropositionNode other)
        {
            return other is OrNode && base.Equals(other);
        }
    }
    public sealed class IfThenNode : BinaryNode
    {
        public override ReadOnlyMemory<char> PropositionOperator => " -> ".AsMemory();
        public override ReadOnlyMemory<char> GuessBefore => "se ".AsMemory();
        public override ReadOnlyMemory<char> GuessMiddle => ", então ".AsMemory();

        public override bool Equals(PropositionNode other)
        {
            return other is IfThenNode && base.Equals(other);
        }
    }
    public sealed class IfOnlyIfNode : BinaryNode
    {
        public override ReadOnlyMemory<char> PropositionOperator => " <-> ".AsMemory();
        public override ReadOnlyMemory<char> GuessBefore => "se ".AsMemory();
        public override ReadOnlyMemory<char> GuessMiddle => ", e somente se ".AsMemory();

        public override bool Equals(PropositionNode other)
        {
            return other is IfOnlyIfNode && base.Equals(other);
        }
    }
}
