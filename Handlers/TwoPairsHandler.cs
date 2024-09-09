using HandAnalysisAPI.Models;
using HandAnalysisAPI.Enums;
using HandAnalysisAPI.Analyzer;

namespace HandAnalysisAPI.Handlers;
class TwoPairsHandler : AbstractHandler
{
    public override Combinations Handle(IEnumerable<Card> cards)
    {
        if (HandAnalyzer.GetHighestTwoPairs(cards).Any())
        {
            return Combinations.TwoPairs;
        }

        return base.Handle(cards);
    }
}