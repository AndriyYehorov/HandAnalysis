using HandAnalysisAPI.Models;
using HandAnalysisAPI.Enums;
using HandAnalysisAPI.Analyzer;

namespace HandAnalysisAPI.Handlers;
class ThreeOfAKindHandler : AbstractHandler
{
    public override Combinations Handle(IEnumerable<Card> cards)
    {
        if (HandAnalyzer.GetHighestThreeOfKind(cards).Any())
        {
            return Combinations.ThreeOfAKind;
        }

        return base.Handle(cards);
    }
}