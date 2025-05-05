namespace Szovegelemzo.Models
{
    public class TextData
    {
        public string Text { get; set; }
        public string[]? Tokens { get; set; }

        public TextData()
        {
            Text = "";
        }
        public TextData(string text)
        {
            Text = text.ToLower();
            Tokens = text.Split(" ");
        }
    }
}
