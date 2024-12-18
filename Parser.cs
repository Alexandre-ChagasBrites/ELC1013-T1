using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace ELC1013_T1
{
    public partial class Parser
    {
        public struct Error
        {
            public string message;
            public int start;
            public int length;
        }

        private readonly Lexer lexer;
        private Lexer.Token currentToken;
        private Lexer.Token previousToken;
        private readonly Stack<List<Node>> nodeStack;

        public List<PremiseNode> Premises {  get; private set; }
        public List<string> Atomics { get; private set; }
        public List<Error> Errors { get; private set; }

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
