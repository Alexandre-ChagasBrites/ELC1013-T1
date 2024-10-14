using System;
using System.Collections.Generic;

public class Lexer
{
    public enum TokenType
    {
        None,
        Text,
        Atomic,
        Not,
        IfThen,
        ThenIf,
        End,
        Stop,
        And,
        Or,
        IfOnlyIf,
        // TODO Adicionar o NotAtomic útil
    }

    public struct Token
    {
        public TokenType type;
        // Todo: Substituir por Memory<char>
        public string lexeme;
    }

    private string src;
    private int index = 0;

    public Lexer(string src)
    {
        this.src = src;
    }

    public Token ReadToken()
    {
        if (index >= src.Length)
            return new Token() { type = TokenType.None };

        TokenType type = TokenType.Text;
        int start = index;

        if (src[index] == '\n')
        {
            type = TokenType.Stop;
            index++;
        }
        else if (src[index] == '}')
        {
            type = TokenType.End;
            index++;
        }
        else
        {
            // Avança o index até encontrar um caractere especial
            while (index < src.Length && src[index] != '{' && src[index] != '}' && src[index] != '\n')
                index++;
            // Depois que é lido até o '{' é preciso identificar
            // se o token é do tipo Text recuando o index
            if (index < src.Length && src[index] == '{')
            {
                int tmp = index;
                while (tmp > start && !Char.IsWhiteSpace(src[tmp]))
                    tmp--;
                // Se for encontrado um WhiteSpace após o
                // recuo então o token é do tipo Text 
                if (Char.IsWhiteSpace(src[tmp]))
                    index = tmp + 1;
                else
                {
                    type = GetTokenType(src.Substring(start, index - start));
                    index++;
                }
            }
        }

        return new Token() { type = type, lexeme = src.Substring(start, index - start) };
    }

    private TokenType GetTokenType(string lexeme)
    {
        return lexeme switch
        {
            "not" => TokenType.Not,
            "ifthen" => TokenType.IfThen,
            "thenif" => TokenType.ThenIf,
            "and" => TokenType.And,
            "or" => TokenType.Or,
            "ifonlyif" => TokenType.IfOnlyIf,
            // TODO Adicionar o tal de NotAtomic
            _ => TokenType.Atomic,
        };
    }
}
