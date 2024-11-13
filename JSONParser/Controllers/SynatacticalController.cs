using JSONParser.Models;
using static JSONParser.Models.Token;

namespace JSONParser.Controllers
{
    internal class SyntacticalController
    {
        public static byte Parser(Token[] tokens)
        {
            int current = 0;

            void ParseObject()
            {
                if (tokens[current].Type != TokenType.LeftBrace)
                    throw new Exception("Expected {");

                current++;

                while (current < tokens.Length && tokens[current].Type != TokenType.RightBrace)
                {
                    if (tokens[current].Type != TokenType.String)
                        throw new Exception("Expected string key in object");

                    current++;

                    if (current >= tokens.Length || tokens[current].Type != TokenType.Colon)
                        throw new Exception("Expected : after object key");

                    current++;
                    ParseValue();

                    if (current < tokens.Length && tokens[current].Type == TokenType.Comma)
                    {
                        current++;
                        if (tokens[current].Type == TokenType.RightBrace)
                            throw new Exception("Trailing comma in object");
                    }
                }

                if (current >= tokens.Length || tokens[current].Type != TokenType.RightBrace)
                    throw new Exception("Expected }");

                current++;
            }

            void ParseArray()
            {
                if (tokens[current].Type != TokenType.LeftBracket)
                    throw new Exception("Expected [");

                current++;

                while (current < tokens.Length && tokens[current].Type != TokenType.RightBracket)
                {
                    ParseValue();

                    if (current < tokens.Length && tokens[current].Type == TokenType.Comma)
                    {
                        current++;
                        if (tokens[current].Type == TokenType.RightBracket)
                            throw new Exception("Trailing comma in array");
                    }
                }

                if (current >= tokens.Length || tokens[current].Type != TokenType.RightBracket)
                    throw new Exception("Expected ]");

                current++;
            }

            void ParseValue()
            {
                if (current >= tokens.Length)
                    throw new Exception("Unexpected end of input");

                switch (tokens[current].Type)
                {
                    case TokenType.LeftBrace:
                        ParseObject();
                        break;

                    case TokenType.LeftBracket:
                        ParseArray();
                        break;

                    case TokenType.String:
                    case TokenType.Number:
                    case TokenType.Boolean:
                    case TokenType.Null:
                        current++;
                        break;

                    default:
                        throw new Exception($"Unexpected token: {tokens[current].Type}");
                }
            }

            // Start parsing
            try
            {
                ParseValue();

                if (current < tokens.Length)
                    throw new Exception("Unexpected tokens after parsing");

                return 0; // Success
            }
            catch (Exception)
            {
                return 1; // Error
            }



        }
    }
} 

