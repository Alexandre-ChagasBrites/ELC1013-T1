using System.Xml.Linq;

namespace ELC1013_T1
{
    public abstract class Node
    {
        public abstract IEnumerable<string> Print();
    }

    public class TextNode : Node
    {
        public string text;

        public override IEnumerable<string> Print()
        {
            yield return text;
        }
    }

    public abstract class PropositionNode : Node, IEquatable<PropositionNode>
    {
        public List<Node> subnodes;

        public override IEnumerable<string> Print()
        {
            foreach (Node node in subnodes)
            {
                foreach (var x in node.Print())
                    yield return x;
            }
        }

        public abstract IEnumerable<string> PrintProposition();
        public abstract IEnumerable<string> PrintGuess();
        public abstract bool Equals(PropositionNode other);
    }

    public class PremiseNode : PropositionNode
    {
        public PropositionNode node;

        public override IEnumerable<string> PrintProposition()
        {
            foreach (var x in node.PrintProposition())
                yield return x;
        }

        public override IEnumerable<string> PrintGuess()
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
        public string name;

        public override IEnumerable<string> PrintProposition()
        {
            yield return name;
        }

        public override IEnumerable<string> PrintGuess() => Print();

        public override bool Equals(PropositionNode other)
        {
            return other is AtomicNode atomic && atomic.name == name;
        }
    }

    public enum UnaryOperator
    {
        None,
        Not
    }

    public class UnaryNode : PropositionNode
    {
        public UnaryOperator type;
        public PropositionNode node;

        public override IEnumerable<string> PrintProposition()
        {
            yield return "~";
            foreach (var x in node.PrintProposition())
                yield return x;
        }

        public override IEnumerable<string> PrintGuess()
        {
            yield return "não é verdade que ";
            foreach (var x in node.PrintGuess())
                yield return x;
        }

        public override bool Equals(PropositionNode other)
        {
            return other is UnaryNode unary && unary.type == type && unary.node.Equals(node);
        }
    }

    public enum BinaryOperator
    {
        None,
        And,
        Or,
        IfThen,
        IfOnlyIf
    }

    public class BinaryNode : PropositionNode
    {
        public BinaryOperator type;
        public PropositionNode leftNode;
        public PropositionNode rightNode;

        public override IEnumerable<string> PrintProposition()
        {
            foreach (var x in leftNode.PrintProposition())
                yield return x;
            leftNode.PrintProposition();
            yield return type switch {
                BinaryOperator.And => " ^ ",
                BinaryOperator.Or => " V ",
                BinaryOperator.IfThen => " -> ",
                BinaryOperator.IfOnlyIf => " <-> ",
                _ => throw new ArgumentOutOfRangeException("Invalid binary node")
            };
            foreach (var x in rightNode.PrintProposition())
                yield return x;
        }

        public override IEnumerable<string> PrintGuess()
        {
            yield return type switch {
                BinaryOperator.And => "",
                BinaryOperator.Or => "",
                BinaryOperator.IfThen => "se ",
                BinaryOperator.IfOnlyIf => "se ",
                _ => throw new ArgumentOutOfRangeException("Invalid binary node")
            };
            foreach (var x in leftNode.PrintGuess())
                yield return x;
            yield return type switch {
                BinaryOperator.And => " e ",
                BinaryOperator.Or => " ou ",
                BinaryOperator.IfThen => ", então ",
                BinaryOperator.IfOnlyIf => ", e somente se ",
                _ => throw new ArgumentOutOfRangeException("Invalid binary node")
            };
            foreach (var x in rightNode.PrintGuess())
                yield return x;
        }

        public override bool Equals(PropositionNode other)
        {
            return other is BinaryNode binary && binary.type == type && binary.leftNode.Equals(leftNode) && binary.rightNode.Equals(rightNode);
        }
    }
}
