using System;
using System.Collections.Generic;

class Parser
{
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

    public abstract class PropositionNode : Node
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
    }

    public class PremiseNode : PropositionNode
    {
        public PropositionNode node;

        public override void PrintProposition()
        {
            node.PrintProposition();
        }
    }

    public class AtomicNode : PropositionNode
    {
        public string name;

        public override void PrintProposition()
        {
            Console.Write($"{name}");
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
    }

    public enum BinaryOperator
    {
        None,
        // Todo: And
        // Todo: Or
        IfThen
        // Todo: IfOnlyIf
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
    }

    private Lexer lexer;
    private Lexer.Token currentToken;
    private Lexer.Token previousToken;
    private Stack<List<Node>> nodeStack;

    public List<PropositionNode> Propositions;
    public List<string> Atomics;

    public Parser(string src)
    {
        lexer = new Lexer(src);
        nodeStack = new Stack<List<Node>>();
        nodeStack.Push(new List<Node>());
        NextToken();
        Propositions = new List<PropositionNode>();
        Atomics = new List<string>();
    }

    private void NextToken()
    {
        previousToken = currentToken;
        currentToken = lexer.ReadToken();
        while (currentToken.type == Lexer.TokenType.Text)
        {
            nodeStack.Peek().Add(new TextNode() { text = currentToken.lexeme });
            currentToken = lexer.ReadToken();
        }
    }

    private bool Match(Lexer.TokenType type)
    {
        if (currentToken.type == type)
        {
            if (type != Lexer.TokenType.End && type != Lexer.TokenType.Stop)
            {
                nodeStack.Push(new List<Node>());
            }
            NextToken();
            return true;
        }
        return false;
    }

    private void Consume(Lexer.TokenType type, string message)
    {
        if (currentToken.type == type)
        {
            NextToken();
            return;
        }
        throw new ArgumentException(message);
    }

    private void Consume(PropositionNode node, string message)
    {
        if (currentToken.type == Lexer.TokenType.End)
        {
            List<Node> subnodes = nodeStack.Pop();
            node.subnodes = subnodes;
            nodeStack.Peek().Add(node);
            NextToken();
            return;
        }
        throw new ArgumentException(message);
    }

    private PropositionNode ParseProposition()
    {
        PropositionNode result = null;
        if (Match(Lexer.TokenType.Atomic))
        {
            string name = previousToken.lexeme.Substring(0, previousToken.lexeme.Length - 1);
            result = new AtomicNode() { name = name };
            Consume(result, "Expected '}' after atomic");
            if (!Atomics.Contains(name))
                Atomics.Add(name);
        }
        else if (Match(Lexer.TokenType.Not))
        {
            PropositionNode node = ParseProposition();
            result = new UnaryNode() { type = UnaryOperator.Not, node = node };
            Consume(result, "Expected '}' after proposition");
        }
        else if (Match(Lexer.TokenType.IfThen))
        {
            PropositionNode ifNode = ParseProposition();
            PropositionNode thenNode = ParseProposition();
            result = new BinaryNode() {  type = BinaryOperator.IfThen, leftNode = ifNode, rightNode = thenNode };
            Consume(result, "Expected '}' after left and right propositions");
        }
        else if (Match(Lexer.TokenType.ThenIf))
        {
            PropositionNode thenNode = ParseProposition();
            PropositionNode ifNode = ParseProposition();
            result = new BinaryNode() {  type = BinaryOperator.IfThen, leftNode = ifNode, rightNode = thenNode };
            Consume(result, "Expected '}' after left and right propositions");
        }
        else
            throw new ArgumentException("Expected proposition");
        return result;
    }

    public void Parse()
    {
        while (currentToken.type != Lexer.TokenType.None)
        {
            PropositionNode node = ParseProposition();
            List<Node> subnodes = nodeStack.Pop();
            nodeStack.Push(new List<Node>());
            Propositions.Add(new PremiseNode() { subnodes = subnodes, node = node });
            Match(Lexer.TokenType.Stop);
        }
    }
}
