namespace Szovegelemzo.Models
{
    public class Statistics
    {
        public int CharCount { get; set; }
        public int WordCount { get; set; }
        public int SentenceCount { get; set; }
        public string[] MostComWords { get; set; }
        public int[] MostComWordCounts { get; set; }
        public float ReadIndex { get; set; }
        
    }
}
