using System.Xml.Linq;

public abstract class Node
{
	public abstract void Print();
}

public class TextNode : Node
{
	public string text;

	public override void Print()
	{
		Console.Write($"{text}");
	}
}

public abstract class PropositionNode : Node, IEquatable<PropositionNode>
{
	public List<Node> subnodes;

	public override void Print()
	{
		foreach (Node node in subnodes)
		{
			node.Print();
		}
	}

	public abstract void PrintProposition();
	public abstract void PrintGuess();
	public abstract bool Equals(PropositionNode other);
}

public class PremiseNode : PropositionNode
{
	public PropositionNode node;

	public override void PrintProposition()
	{
		node.PrintProposition();
	}

	public override void PrintGuess()
	{
		node.PrintGuess();
	}

	public override bool Equals(PropositionNode other)
	{
		return other is PremiseNode premise && premise.node.Equals(node);
	}
}

public class AtomicNode : PropositionNode
{
	public string name;

	public override void PrintProposition()
	{
		Console.Write($"{name}");
	}

	public override void PrintGuess()
	{
		Print();
	}

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

	public override void PrintProposition()
	{
		Console.Write("~");
		node.PrintProposition();
	}

	public override void PrintGuess()
	{
		Console.Write("não é verdade que ");
		node.PrintGuess();
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

	public override void PrintProposition()
	{
		leftNode.PrintProposition();
		Console.Write(" -> ");
		rightNode.PrintProposition();
	}

	public override void PrintGuess()
	{
		Console.Write("se ");
		leftNode.PrintGuess();
		Console.Write(", então ");
		rightNode.PrintGuess();
	}

	public override bool Equals(PropositionNode other)
	{
		return other is BinaryNode binary && binary.type == type && binary.leftNode.Equals(leftNode) && binary.rightNode.Equals(rightNode);
	}
}
