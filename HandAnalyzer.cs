using HandAnalysisAPI.Models;
using HandAnalysisAPI.Enums;
using HandAnalysisAPI.Handlers;

namespace HandAnalysisAPI.Analyzer;
public static class HandAnalyzer
{   
    public static Combinations Analyze(IEnumerable<Card> cards)
    {
        if (cards is null)
        {
            throw new ArgumentNullException(nameof(cards), "Cards cant be null");
        }        

        var royalFlushHandler = new RoyalFlushHandler();
        var straightFlushHandler = new StraightFlushHandler();
        var fourOfKindHandler = new FourOfAKindHandler();
        var fullHouseHandler = new FullHouseHandler();
        var flushHandler = new FlushHandler();
        var straightHandler = new StraightHandler();
        var threeOfKindHandler = new ThreeOfAKindHandler();
        var twoPairsHandler = new TwoPairsHandler();
        var pairHandler = new PairHandler();
        var highCardHandler = new HighCardHandler();

        royalFlushHandler.SetNext(straightFlushHandler);
        straightFlushHandler.SetNext(fourOfKindHandler);
        fourOfKindHandler.SetNext(fullHouseHandler);
        fullHouseHandler.SetNext(flushHandler);
        flushHandler.SetNext(straightHandler);
        straightHandler.SetNext(threeOfKindHandler);
        threeOfKindHandler.SetNext(twoPairsHandler);
        twoPairsHandler.SetNext(pairHandler);
        pairHandler.SetNext(highCardHandler);

        return royalFlushHandler.Handle(cards);
    }
    
    public static IEnumerable<Card> GetRoyalFlush(IEnumerable<Card> cards)
    {
        var straightFlush = GetStraightFlush(cards);

        return (straightFlush.Any() && straightFlush.Last().Rank == Ranks.Ten) ? straightFlush : Enumerable.Empty<Card>();
    }
    
    public static IEnumerable<Card> GetStraightFlush(IEnumerable<Card> cards)
    {
        var groupsWithPossibleStraightFlush =
            GroupCardsBySuits(cards)
            .Where(g => g.Count() > 4);

        var straightFlushes = groupsWithPossibleStraightFlush
            .Select(GetStraight)
            .FirstOrDefault(straight => straight != Enumerable.Empty<Card>());

        return straightFlushes ?? Enumerable.Empty<Card>();
    }

    public static IEnumerable<Card> GetStraight(IEnumerable<Card> cards)
    {
        var longestSequence = GetLongestSequence(cards);

        var aces = cards.Where(card => card.Rank == Ranks.Ace);

        if (longestSequence.Count == 4
            && longestSequence[^1].Rank == Ranks.Two
            && aces.Any()
            )
        {
            longestSequence.Add(aces.First());
        }

        return longestSequence.Count > 4 ? longestSequence.Take(5) : Enumerable.Empty<Card>();
    }
    
    public static IEnumerable<Card> GetFlush(IEnumerable<Card> cards)
    {
        var cardsGroupedBySuits = GroupCardsBySuits(cards);

        var flushes = cardsGroupedBySuits
            .FirstOrDefault(cGBS => cGBS.Count() >= 5);

        return flushes ?? Enumerable.Empty<Card>();
    }

    public static IEnumerable<Card> GetFourOfKind(IEnumerable<Card> cards)
    {
        var cardsGroupedByRanks = GroupCardsByRanks(cards);

        var fourOfAKinds = cardsGroupedByRanks
            .FirstOrDefault(cGBR => cGBR.Count() >= 4);

        return fourOfAKinds ?? Enumerable.Empty<Card>();
    }
    
    public static IEnumerable<Card> GetFullHouse(IEnumerable<Card> cards)
    {
        var cardsGroupedByRank = GroupCardsByRanks(cards);

        var threeOfKind = GetHighestThreeOfKind(cards);

        if(!threeOfKind.Any())
        {
            return Enumerable.Empty<Card>();
        }

        var pairs = cardsGroupedByRank
            .FirstOrDefault(cardsGrouped => 
                cardsGrouped.Count() >= 2 
                && cardsGrouped.First().Rank != threeOfKind.First().Rank)
            ?.Take(2);

        pairs ??= Enumerable.Empty<Card>();

        return (threeOfKind.Any() && pairs.Any()) ? threeOfKind.Concat(pairs) : Enumerable.Empty<Card>();
    }
    
    public static IEnumerable<Card> GetHighestThreeOfKind(IEnumerable<Card> cards)
    {
        var cardsGroupedByRanks = GroupCardsByRanks(cards);

        var threeOfAKind = cardsGroupedByRanks
            .FirstOrDefault(cGBR => cGBR.Count() == 3);

        return threeOfAKind ?? Enumerable.Empty<Card>();
    }
    
    public static IEnumerable<Card> GetHighestTwoPairs(IEnumerable<Card> cards)
    {
        var cardsGroupedByRanks = GroupCardsByRanks(cards);

        var result = cardsGroupedByRanks
            .Where(cGBR => cGBR.Count() == 2)
            .Take(2)
            .SelectMany(c => c);

        return result.Count() > 3 ? result : Enumerable.Empty<Card>();
    }

    public static IEnumerable<Card> GetPair(IEnumerable<Card> cards)
    {
        var cardsGroupedByRanks = GroupCardsByRanks(cards);

        var pairs = cardsGroupedByRanks
            .FirstOrDefault(cGBR => cGBR.Count() == 2);

        return pairs ?? Enumerable.Empty<Card>();
    }    
    
    public static IEnumerable<Card> GetHighCard(IEnumerable<Card> cards)
    {
        return cards.OrderByDescending(c => c.Rank)
            .Take(5);
    }

    private static IEnumerable<IEnumerable<Card>> GroupCardsByRanks(IEnumerable<Card> inputCards)
    {
        if (inputCards is null || inputCards.Any(c => c is null))
        {
            throw new ArgumentNullException(nameof(inputCards), "Cards cant be null");
        }

        return inputCards
            .GroupBy(c => c.Rank)
            .Select(g => g.AsEnumerable())
            .OrderByDescending(g => g.First().Rank);
    }

    private static IEnumerable<IEnumerable<Card>> GroupCardsBySuits(IEnumerable<Card> inputCards)
    {
        if (inputCards is null || inputCards.Any(c => c is null))
        {
            throw new ArgumentNullException(nameof(inputCards),"Cards cant be null");
        }

        return inputCards
            .GroupBy(c => c.Suit)
            .Select(g => g.AsEnumerable())
            .OrderByDescending(g => g.First().Rank);
    }

    private static List<Card> GetLongestSequence(IEnumerable<Card> cards)
    {        
        var firstCardsGroupedByRanks = GroupCardsByRanks(cards)
            .Select(group => group.First());

        var resultList = new List<Card>();
        var currentList = new List<Card>();

        foreach (var card in firstCardsGroupedByRanks)
        {
            if (currentList.Count == 0 || card.Rank + 1 == currentList[^1].Rank)
                currentList.Add(card);

            else
            {
                if (currentList.Count > resultList.Count)
                    resultList = new List<Card>(currentList);

                currentList.Clear();
                currentList.Add(card);
            }
        }

        if (currentList.Count > resultList.Count)
            resultList = new List<Card>(currentList);

        return resultList;
    }
   
}
