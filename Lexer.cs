using System;

public class Lexer
{
    public enum TokenType
    {
        None,
        Text,
        BegNode,
        EndNode,
        Stop
    }
    
    public struct Token
    {
        public TokenType type;
        // Todo: Substituir por ReadOnlySpan<char>
        public string lexeme;
    }
    
    public static Token ReadToken(string src, ref int index)
    {
        if (index >= src.Length)
        return new Token(){ type=TokenType.None };
        
        TokenType type = TokenType.Text;
        int start = index;
        
        if (src[index] == '\n')
        {
            type = TokenType.Stop;
            index++;
        }
        else if (src[index] == '}')
        {
            type = TokenType.EndNode;
            index++;
        }
        else
        {
            // Avança o index até encontrar um caractere especial
            while (index < src.Length && src[index] != '{' && src[index] != '}' && src[index] != '\n')
                index++;
            // Depois que é lido até o '{' é preciso identificar
            // se o token é um BegNode recuando o index
            if (index < src.Length && src[index] == '{')
            {
                int tmp = index;
                while (tmp > start && !Char.IsWhiteSpace(src[tmp]))
                    tmp--;
                // Se for encontrado um WhiteSpace após o
                // recuo então o token não é do tipo BegNode 
                if (Char.IsWhiteSpace(src[tmp]))
                    index = tmp + 1;
                else
                {
                    type = TokenType.BegNode;
                    index++;
                }
            }
        }
        
        return new Token(){ type=type, lexeme=src.Substring(start, index - start) };
    }
    
    public static void Main(string[] args)
    {
        string src = @"if{Se p{o time joga bem}}, then{q{ganha o campeonato}}.
            if{Se o time not{não p{joga bem}}}, then{r{o técnico é culpado}}.
            if{Se q{o time ganha o campeonato}}, then{s{os torcedores ficam contentes}}.
            Os torcedores not{não s{estão contentes}}.
            Logo, r{o técnico é culpado}.";
        
        int index = 0;
        while (index < src.Length)
        {
            Token token = ReadToken(src, ref index);
            Console.WriteLine($"{token.type} \"{token.lexeme}\"");
        }
    }
}
