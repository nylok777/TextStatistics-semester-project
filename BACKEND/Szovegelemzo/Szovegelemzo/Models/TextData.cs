namespace Szovegelemzo.Models
{
    public class TextData
    {
        public string? Text { get; set; }
        public string[]? Tokens { get; set; }

        private string ProcessText(string text)
        {
            string processed = text.ToLower();
            string[] chars = new string[] {",", ":", ";", @"""", "'", "＂", "〃", "ˮ", "‶", "˶", "ʺ", "“", "”", "˝", "‟"};
            foreach (string item in chars)
            {
                processed = processed.Replace(item, string.Empty);
            }
            return processed;
        }
        public TextData(string text)
        {
            Text = ProcessText(text);
            Tokens = Text.Split(" ");
        }
    }
}
