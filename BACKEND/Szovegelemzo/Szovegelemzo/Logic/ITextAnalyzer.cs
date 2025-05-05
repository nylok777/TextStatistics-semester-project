using Szovegelemzo.Models;

namespace Szovegelemzo.Logic
{
    public interface ITextAnalyzer
    {
        void CreateTextData(string text);
        int GetCharCount();
        int GetSentenceCount();
        int GetWordCount();
        string MostCommonWord();
        float ReadabilityIndex();
        Statistics GenerateStatistics();
    }
}