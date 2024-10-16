namespace ELC1013_T1
{
    public partial class Lexer
    {
        public enum TokenType
        {
            None,
            Text,
            Close,
            End,
            Atomic,
            Not,
            IfThen,
            ThenIf,
            And,
            Or,
            IfOnlyIf,
        }

        static private TokenType GetTokenType(string lexeme)
        {
            return lexeme switch
            {
                "not" => TokenType.Not,
                "ifthen" => TokenType.IfThen,
                "thenif" => TokenType.ThenIf,
                "and" => TokenType.And,
                "or" => TokenType.Or,
                "ifonlyif" => TokenType.IfOnlyIf,
                _ => TokenType.Atomic,
            };
        }
    }

    public partial class Parser
    {
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
                    PropositionNode node0 = ParseProposition();
                    result = new NotNode(node0);
                Consume(result, "Expected '}' after not proposition(s)");
            }
            else if (Match(Lexer.TokenType.IfThen))
            {
                    PropositionNode node0 = ParseProposition();
                    PropositionNode node1 = ParseProposition();
                    result = new IfThenNode(node0, node1);
                Consume(result, "Expected '}' after if and then proposition(s)");
            }
            else if (Match(Lexer.TokenType.ThenIf))
            {
                    PropositionNode node0 = ParseProposition();
                    PropositionNode node1 = ParseProposition();
                    result = new IfThenNode(node1, node0);
                Consume(result, "Expected '}' after then and if proposition(s)");
            }
            else if (Match(Lexer.TokenType.And))
            {
                    PropositionNode node0 = ParseProposition();
                    PropositionNode node1 = ParseProposition();
                    result = new AndNode(node0, node1);
                Consume(result, "Expected '}' after and proposition(s)");
            }
            else if (Match(Lexer.TokenType.Or))
            {
                    PropositionNode node0 = ParseProposition();
                    PropositionNode node1 = ParseProposition();
                    result = new OrNode(node0, node1);
                Consume(result, "Expected '}' after or proposition(s)");
            }
            else if (Match(Lexer.TokenType.IfOnlyIf))
            {
                    PropositionNode node0 = ParseProposition();
                    PropositionNode node1 = ParseProposition();
                    result = new IfOnlyIfNode(node0, node1);
                Consume(result, "Expected '}' after left and right proposition(s)");
            }
            else
                throw GenerateError("Expected proposition");
            return result;
        }
    }
}
