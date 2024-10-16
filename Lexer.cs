using System;
using System.Collections.Generic;

namespace ELC1013_T1
{
    public partial class Lexer
    {
        public struct Token
        {
            public TokenType type;
            public ReadOnlyMemory<char> lexeme;
#if false
            public string lexeme;
#endif
            public int line;
        }

        private string src;
        private int index = 0;
        private int line = 1;

        public Lexer(string src)
        {
            this.src = src.Replace("\r\n", "\n");
        }

        public Token ReadToken()
        {
            if (index >= src.Length)
                return new Token() { type = TokenType.None, line = line };

            TokenType type = TokenType.Text;
            int start = index;

            if (src[index] == '\n')
            {
                type = TokenType.End;
                index++;
                line++;
            }
            else if (src[index] == '}')
            {
                type = TokenType.Close;
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

            return new Token() { type = type, lexeme = src.AsMemory(start, index - start), line = line };
        }
    }
}
