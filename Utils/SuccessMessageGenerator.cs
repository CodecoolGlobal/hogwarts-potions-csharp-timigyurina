namespace HogwartsPotions.Utils
{
    public static class SuccessMessageGenerator
    {
        private readonly static Random _random = new();
        private readonly static string[] _messages = new string[]
        {
            "Keep up the good work :)",
            "Let the owls hoot !",
            "Now you really got your cauldron dirty ;)",
            "Congrats, but now you have to clean up :D"
        };

        public static SuccessMessage GetRandomMessage()
        {
            return new SuccessMessage(_messages[_random.Next(0, _messages.Length)]);
        }
    }
}
