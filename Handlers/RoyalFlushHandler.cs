using HandAnalysisAPI.Models;
using HandAnalysisAPI.Enums;
using HandAnalysisAPI.Analyzer;

namespace HandAnalysisAPI.Handlers;
class RoyalFlushHandler : AbstractHandler
{
    public override Combinations Handle(IEnumerable<Card> cards)
    {
        if (HandAnalyzer.GetRoyalFlush(cards).Any())
        {
            return Combinations.RoyalFlush;
        }

        return base.Handle(cards);
    }
}