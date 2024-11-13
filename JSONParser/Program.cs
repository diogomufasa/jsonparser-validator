using JSONParser.Models;
using JSONParser.Controllers;

namespace JSONParser
{
    public class Program
    {
        public static void Main()
        {
            try
            {
                string path = "C:\\Users\\User\\Desktop\\C#\\Tests\\jsons\\step4\\valid.json"; 
                string json = File.ReadAllText(path);
                
                if (string.IsNullOrEmpty(json))
                {
                    throw new Exception("No input");
                }
                
                Token[] tokens = LexerController.Lexer(json);
               
                if (tokens == null)
                {
                    throw new Exception("Invalid json");
                }

                byte code = SyntacticalController.Parser(tokens);

                if (code == 1)
                { 
                    throw new Exception("Invalid json ");
                }

                Console.WriteLine("Valid json ");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }



}