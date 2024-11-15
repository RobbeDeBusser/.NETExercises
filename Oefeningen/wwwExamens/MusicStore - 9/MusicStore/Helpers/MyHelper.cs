namespace MusicStore.Helpers
{
    public class MyHelper
    {
        public static string Truncate(string text, int maxLength)
        {
            if (text.Length < maxLength) {
                return text;
            }
            else
            {
                return string.Concat(text.AsSpan(0, maxLength), "...");
            }
        }
    }
}
