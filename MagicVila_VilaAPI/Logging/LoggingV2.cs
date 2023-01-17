namespace MagicVila_VilaAPI.Logging
{
    public class LoggingV2 : ILogging
    {
        public void Log(string message, string type)
        {
            var now = DateTime.UtcNow;

            if (type == "error")
            {                
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine($"{now}: [ERROR] - {message}");
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.WriteLine($"{now}: [INFO] - {message}");
            }
        }
    }
}
