using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;

namespace ELC1013_T1
{
    public partial class Lexer
    {
        public struct Token
        {
            public TokenType type;
            public int start;
            public ReadOnlyMemory<char> lexeme;
        }

        private readonly string src;
        private int index = 0;

        public Lexer(string src)
        {
            this.src = src.Replace("\r\n", "\n");
        }

        public Token ReadToken()
        {
            if (index >= src.Length)
                return new Token() { type = TokenType.None, start = index, lexeme = src.AsMemory(index, 0) };

            TokenType type = TokenType.Text;
            int start = index;

            if (src[index] == '\n')
            {
                type = TokenType.End;
                index++;
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

            return new Token() { type = type, start = start, lexeme = src.AsMemory(start, index - start) };
        }

        public System.Drawing.Point GetLineColumn(int index)
        {
            int start = 0;
            int line = 1;
            for (int i = 0; i < index; i++)
            {
                if (src[i] == '\n')
                {
                    start = i + 1;
                    line++;
                }
            }
            return new System.Drawing.Point(index - start + 1, line);
        }
    }
}
