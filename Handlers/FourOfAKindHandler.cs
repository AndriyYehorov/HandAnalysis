using HandAnalysisAPI.Models;
using HandAnalysisAPI.Enums;
using HandAnalysisAPI.Analyzer;

namespace HandAnalysisAPI.Handlers;
class FourOfAKindHandler : AbstractHandler
{
    public override Combinations Handle(IEnumerable<Card> cards)
    {
        if (HandAnalyzer.GetFourOfKind(cards).Any())
        {
            return Combinations.FourOfAKind;
        }

        return base.Handle(cards);
    }
}