using HandAnalysisAPI.Models;
using HandAnalysisAPI.Enums;
using HandAnalysisAPI.Analyzer;

namespace HandAnalysisAPI.Handlers;
class StraightHandler : AbstractHandler
{
    public override Combinations Handle(IEnumerable<Card> cards)
    {
        if (HandAnalyzer.GetStraight(cards).Any())
        {
            return Combinations.Straight;
        }

        return base.Handle(cards);
    }
}