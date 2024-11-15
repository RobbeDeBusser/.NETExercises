namespace MusicStore.Helpers
{
    public class MyHelper
    {
        public static string Truncate(string input, int lenght)
        {
            if (input.Length <= lenght)
            {
                return input;
            }
            else
            {
                return string.Concat(input.AsSpan(0, lenght), "...");
            }
        }
    }
}
