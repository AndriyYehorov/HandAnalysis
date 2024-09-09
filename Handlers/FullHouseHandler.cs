using HandAnalysisAPI.Models;
using HandAnalysisAPI.Enums;
using HandAnalysisAPI.Analyzer;

namespace HandAnalysisAPI.Handlers;
class FullHouseHandler : AbstractHandler
{
    public override Combinations Handle(IEnumerable<Card> cards)
    {
        if (HandAnalyzer.GetFullHouse(cards).Any())
        {
            return Combinations.FullHouse;
        }

        return base.Handle(cards);
    }
}