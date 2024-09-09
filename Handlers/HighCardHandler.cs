using HandAnalysisAPI.Models;
using HandAnalysisAPI.Enums;
using HandAnalysisAPI.Analyzer;

namespace HandAnalysisAPI.Handlers;
class HighCardHandler : AbstractHandler
{
    public override Combinations Handle(IEnumerable<Card> cards)
    {
        if (HandAnalyzer.GetHighCard(cards).Any())
        {
            return Combinations.HighCard;
        }

        return base.Handle(cards);
    }
}