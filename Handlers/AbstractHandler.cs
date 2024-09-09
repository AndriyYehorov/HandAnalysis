using HandAnalysisAPI.Models;
using HandAnalysisAPI.Enums;
using HandAnalysisAPI.Analyzer;

namespace HandAnalysisAPI.Handlers;
abstract class AbstractHandler : IHandler
{
    private IHandler? _nextHandler;
    public IHandler SetNext(IHandler nextHandler)
    {
        _nextHandler = nextHandler;

        return nextHandler;
    }

    public virtual Combinations Handle(IEnumerable<Card> cards)
    {
        if (_nextHandler != null)
        {
            return _nextHandler.Handle(cards);
        }
        else 
        {
            return Combinations.HighCard;
        }
    }
}