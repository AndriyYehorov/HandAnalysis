using HandAnalysisAPI.Models;
using HandAnalysisAPI.Enums;
using HandAnalysisAPI.Analyzer;

namespace HandAnalysisAPI.Handlers;
class StraightFlushHandler : AbstractHandler
{
    public override Combinations Handle(IEnumerable<Card> cards)
    {
        if (HandAnalyzer.GetStraightFlush(cards).Any())
        {
            return Combinations.StraightFlush;
        }

        return base.Handle(cards);
    }
}