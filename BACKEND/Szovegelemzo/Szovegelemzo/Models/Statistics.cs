namespace Szovegelemzo.Models
{
    public class Statistics
    {
        int charCount;
        int wordCount;
        int sentenceCount;
        string mostComWord;
        float readIndex;

        public Statistics(int charCount, int wordCount, int sentenceCount, string mostComWord, float readIndex)
        {
            this.charCount = charCount;
            this.wordCount = wordCount;
            this.sentenceCount = sentenceCount;
            this.mostComWord = mostComWord;
            this.readIndex = readIndex;
        }
    }
}
