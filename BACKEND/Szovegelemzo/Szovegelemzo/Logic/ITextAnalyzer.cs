using Szovegelemzo.Models;

namespace Szovegelemzo.Logic
{
    public interface ITextAnalyzer
    {
        void CreateTextData(string text);
        int GetCharCount();
        int GetSentenceCount();
        int GetWordCount();
        Dictionary<string, int> MostCommonWords();
        float ReadabilityIndex();
        Statistics GenerateStatistics();
    }
}