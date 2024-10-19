using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace ELC1013_T1
{
    public class Parser
    {
        public struct Error
        {
            public string message;
            public int start;
            public int length;
        }

        private Lexer lexer;
        private Lexer.Token currentToken;
        private Lexer.Token previousToken;
        private Stack<List<Node>> nodeStack;

        public List<PremiseNode> Premises;
        public List<string> Atomics;
        public List<Error> Errors;

        public Parser(string src)
        {
            lexer = new Lexer(src);
            nodeStack = new Stack<List<Node>>();
            nodeStack.Push(new List<Node>());
            NextToken();
            Premises = new List<PremiseNode>();
            Atomics = new List<string>();
            Errors = new List<Error>();
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
                if (type != Lexer.TokenType.Close && type != Lexer.TokenType.End)
                {
                    nodeStack.Push(new List<Node>());
                }
                NextToken();
                return true;
            }
            return false;
        }

        private ArgumentException GenerateError(string message, Lexer.Token token)
        {
            System.Drawing.Point point = lexer.GetLineColumn(token.start + token.lexeme.Length);
            Error error = new Error()
            {
                message = $"Error [{point.Y},{point.X}]: {message}",
                start = token.start,
                length = token.lexeme.Length
            };
            Errors.Add(error);
            return new ArgumentException(error.message);
        }

        private void Consume(Lexer.TokenType type, string message)
        {
            if (currentToken.type == type)
            {
                NextToken();
                return;
            }
            throw GenerateError(message, currentToken);
        }

        private void Consume(PropositionNode node, string message)
        {
            if (currentToken.type == Lexer.TokenType.Close)
            {
                List<Node> subnodes = nodeStack.Pop();
                node.subnodes = subnodes;
                nodeStack.Peek().Add(node);
                NextToken();
                return;
            }
            throw GenerateError(message, currentToken);
        }

        private PropositionNode ParseProposition()
        {
            PropositionNode result;
            if (Match(Lexer.TokenType.Atomic))
            {
                string name = previousToken.lexeme.Slice(0, previousToken.lexeme.Length - 1).ToString();
                result = new AtomicNode() { name = name };
                Consume(result, "Expected '}' after atomic");
                if (!Atomics.Contains(name))
                    Atomics.Add(name);
            }
            else if (Match(Lexer.TokenType.Not))
            {
                PropositionNode node = ParseProposition();
                result = new NotNode() { node = node };
                Consume(result, "Expected '}' after proposition");
            }
            else if (Match(Lexer.TokenType.IfThen))
            {
                PropositionNode ifNode = ParseProposition();
                PropositionNode thenNode = ParseProposition();
                result = new IfThenNode() { leftNode = ifNode, rightNode = thenNode };
                Consume(result, "Expected '}' after if and then propositions");
            }
            else if (Match(Lexer.TokenType.ThenIf))
            {
                PropositionNode thenNode = ParseProposition();
                PropositionNode ifNode = ParseProposition();
                result = new IfThenNode() { leftNode = ifNode, rightNode = thenNode };
                Consume(result, "Expected '}' after then and if propositions");
            }
            else if (Match(Lexer.TokenType.IfOnlyIf))
            {
                PropositionNode lhs = ParseProposition();
                PropositionNode rhs = ParseProposition();
                result = new IfOnlyIfNode() { leftNode = lhs, rightNode = rhs };
                Consume(result, "Expected '}' after left and right propositions");
            }
            else if (Match(Lexer.TokenType.And))
            {
                PropositionNode lhs = ParseProposition();
                PropositionNode rhs = ParseProposition();
                result = new AndNode() { leftNode = lhs, rightNode = rhs };
                Consume(result, "Expected '}' after left and right propositions");
            }
            else if (Match(Lexer.TokenType.Or))
            {
                PropositionNode lhs = ParseProposition();
                PropositionNode rhs = ParseProposition();
                result = new OrNode() { leftNode = lhs, rightNode = rhs };
                Consume(result, "Expected '}' after left and right propositions");
            }
            else
                throw GenerateError("Expected proposition", currentToken);
            return result;
        }

        public void Parse()
        {
            while (currentToken.type != Lexer.TokenType.None)
            {
                try
                {
                    PropositionNode node = ParseProposition();
                    List<Node> subnodes = nodeStack.Pop();
                    nodeStack.Push(new List<Node>());
                    Premises.Add(new PremiseNode() { subnodes = subnodes, node = node });
                    if (currentToken.type != Lexer.TokenType.None)
                        Consume(Lexer.TokenType.End, "Expected newline after proposition");
                }
                catch (Exception ex)
                {
                    do
                        NextToken();
                    while (currentToken.type != Lexer.TokenType.None && previousToken.type != Lexer.TokenType.End);
                    nodeStack.Peek().Clear();
                }
            }
        }
    }
}
