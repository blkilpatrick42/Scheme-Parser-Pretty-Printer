// Scanner -- The lexical analyzer for the Scheme printer and interpreter

using System;
using System.Text;
using System.IO;
using Tokens;

namespace Parse
{
    public class Scanner
    {
        private TextReader In;

        // maximum length of strings and identifier
        private const int BUFSIZE = 1000;
        private char[] buf = new char[BUFSIZE];

        public Scanner(TextReader i)
        {
            In = i;
        }
  
        public bool validIdentifierChar(char ch)
        {
            if((ch >= 'A' && ch <= 'Z') 
                || (ch >= 'a' && ch <= 'z') 
                || ch == '!' || ch == '$' 
                || ch == '%' || ch == '&' 
                || ch == '*' || ch == '+' 
                || ch == '-' || ch == '.' 
                || ch == '/' || ch == ':' 
                || ch == '<' || ch == '='
                || ch == '>' || ch == '?' 
                || ch == '@' || ch == '^' 
                || ch == '_' || ch == '~')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Token getNextToken()
        {
            int ch;

            try
            {
                // It would be more efficient if we'd maintain our own
                // input buffer and read characters out of that
                // buffer, but reading individual characters from the
                // input stream is easier.
                ch = In.Read();
   

                if (ch == -1)
                    return null;

                //Skips comments
                else if(ch == ';')
                {
                    In.ReadLine();
                    return getNextToken();
                }
                //skips whitespaces
                else if(ch == 32 || ch == 9 || ch == 13 || ch == 10 || ch == 12)
                {
                    return getNextToken();
                }
        
                // Special characters
                else if (ch == '\'')
                    return new Token(TokenType.QUOTE);
                else if (ch == '(')
                    return new Token(TokenType.LPAREN);
                else if (ch == ')')
                    return new Token(TokenType.RPAREN);
                else if (ch == '.')
                    // We ignore the special identifier `...'.

                    return new Token(TokenType.DOT);
                
                // Boolean constants
                else if (ch == '#')
                {
                    ch = In.Read();

                    if (ch == 't')
                        return new Token(TokenType.TRUE);
                    else if (ch == 'f')
                        return new Token(TokenType.FALSE);
                    else if (ch == -1)
                    {
                        Console.Error.WriteLine("Unexpected EOF following #");
                        return null;
                    }
                    else
                    {
                        Console.Error.WriteLine("Illegal character '" +
                                                (char)ch + "' following #");
                        return getNextToken();
                    }
                }

                // String constants
                else if (ch == '"')
                {
                    ch = In.Read();
                    int i;
                    for(i = 0; ch != '"'; i++)
                    {
                        buf[i] = (char)ch;
                        ch = In.Read();
                    }
                    return new StringToken(new String(buf, 0, i));
                }

    
                // Integer constants
                else if (ch >= '0' && ch <= '9')
                {
                    StringBuilder getStr = new StringBuilder();
                    getStr.Append((int)ch-48);
                    int i = 0;
                    while ((int)In.Peek() >= (int)'0' && (int)In.Peek() <= (int)'9')
                    {
                        ch = In.Read();
                        getStr.Append((int)ch - 48);
                    }
                    String gotStr = getStr.ToString();
                    Int32.TryParse(gotStr, out i);
                    // make sure that the character following the integer
                    // is not removed from the input stream
                    return new IntToken(i);
                }
        
                // Identifiers
                else if (validIdentifierChar((char)ch)) {
                    buf[0] = (char)ch;
                    int i = 0;
                    while (validIdentifierChar((char)In.Peek()) && i < 1000)
                    {
                        i++;
                        ch = In.Read();
                        buf[i] = (char)ch;
                    }
                    // make sure that the character following the integer
                    // is not removed from the input stream
                    String retString = new String(buf,0,i+1);
                    if (retString.Equals("quote"))
                    {
                        return new Token(TokenType.QUOTE);
                    }

                    return new IdentToken(retString);
                }
    
                // Illegal character
                else
                {
                    Console.Error.WriteLine("Illegal input character '"
                                            + (char)ch + '\'');
                    return getNextToken();
                }
            }
            catch (IOException e)
            {
                Console.Error.WriteLine("IOException: " + e.Message);
                return null;
            }
        }
    }
}

