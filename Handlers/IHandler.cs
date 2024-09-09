using HandAnalysisAPI.Models;
using HandAnalysisAPI.Enums;

namespace HandAnalysisAPI.Handlers;
interface IHandler
{
    IHandler SetNext(IHandler nextHandler);
    Combinations Handle(IEnumerable<Card> cards);
}