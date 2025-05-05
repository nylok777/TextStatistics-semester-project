using Microsoft.AspNetCore.Mvc;
using Szovegelemzo.Logic;
using Szovegelemzo.Models;

namespace Szovegelemzo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TextApiController: ControllerBase
    {
        ITextAnalyzer analyzer;

        public TextApiController(ITextAnalyzer analyzer)
        {
            this.analyzer = analyzer;
        }

        [HttpPost]
        public void CreateTextData([FromBody] string text)
        {
            analyzer.CreateTextData(text);
        }

        [HttpGet]
        public Statistics GetStatistics()
        {
            return analyzer.GenerateStatistics();
        }
    }
}
