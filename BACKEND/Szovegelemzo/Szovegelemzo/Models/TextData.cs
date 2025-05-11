namespace Szovegelemzo.Models
{
    public class TextData
    {
        public string? Text { get; set; }
        public string[]? Tokens { get; set; }

        private string ProcessText(string text)
        {
            string processed = text.ToLower();
            string[] chars = new string[] {",", ":", ";", @"""", "'", "＂", "〃", "ˮ", "‶", "˶", "ʺ", 
                "“", "”", "˝", "‟", "-", "_", "%", "+", "=", "(", ")", "§", "~", "$", "/", "#", "<", ">", "@",
                "{", "}", "[", "]"};
            foreach (string item in chars)
            {
                processed = processed.Replace(item, string.Empty);
            }
            return processed;
        }

        private string[] ProcessTokens(string[] tokens)
        {
            string[] chars = new string[] { ".", "?", "!" };

            for (int i = 0; i < tokens.Length; i++)
            {
                foreach (var item in chars)
                {
                    tokens[i] = tokens[i].Replace(item, string.Empty);
                }
            }

            return tokens;
        }
        public TextData(string text)
        {
            Text = ProcessText(text);
            Tokens = ProcessTokens(Text.Split(" "));
        }
    }
}
