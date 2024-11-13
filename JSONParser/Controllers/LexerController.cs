using JSONParser.Models;
using static JSONParser.Models.Token;

namespace JSONParser.Controllers
{
    internal class LexerController
    {
        public static Token[] Lexer(string json)
        {
            List<Token> tokens = new List<Token>();
            int current = 0;

            while (current < json.Length)
            {
                char c = json[current];

                switch (c)
                {
                    case '{':
                        tokens.Add(new Token(TokenType.LeftBrace));
                        current++;
                        break;
                    case '}':
                        tokens.Add(new Token(TokenType.RightBrace));
                        current++;
                        break;
                    case '[':
                        tokens.Add(new Token(TokenType.LeftBracket));
                        current++;
                        break;
                    case ']':
                        tokens.Add(new Token(TokenType.RightBracket));
                        current++;
                        break;
                    case ':':
                        tokens.Add(new Token(TokenType.Colon));
                        current++;
                        break;
                    case ',':
                        tokens.Add(new Token(TokenType.Comma));
                        current++;
                        break;
                    case '"':
                        string sb = "";
                        current++;
                        while (json[current] != '"')
                        {
                            sb += json[current];
                            current++;
                        }
                        tokens.Add(new Token(TokenType.String, sb));
                        current++;
                        break;

                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    case '-':
                        string num = "";
                        while (char.IsDigit(json[current]) || json[current] == '-')
                        {
                            num += json[current];
                            current++;
                        }
                        tokens.Add(new Token(TokenType.Number, num));
                        break;
                    case 't':
                    case 'f':
                        string boolean = "";
                        while (char.IsLetter(json[current]))
                        {
                            boolean += json[current];
                            current++;
                        }
                        tokens.Add(new Token(TokenType.Boolean, boolean));
                        break;
                    case 'n':
                        string n = "";
                        while (char.IsLetter(json[current]))
                        {
                            n += json[current];
                            current++;
                        }
                        tokens.Add(new Token(TokenType.Null, n));
                        break;
                    case ' ':
                    case '\n':
                    case '\t':
                        current++;
                        break;
                    default:
                        return null;
                        throw new Exception($"Unexpected character: {c}");

                }
            }

            return tokens.ToArray();

        }
    }
}
