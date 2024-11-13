

namespace JSONParser.Models
{
    internal class Token
    {
        public enum TokenType
        {
            LeftBrace,
            RightBrace,
            LeftBracket,
            RightBracket,
            Colon,
            Comma,
            String,
            Number,
            Boolean,
            Null
        }

        public TokenType Type;
        public string Value;

        public Token(TokenType type, string value = "")
        {
            this.Type = type;
            this.Value = value;
        }
    }
}
