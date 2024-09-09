using Microsoft.AspNetCore.Mvc;
using HandAnalysisAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using HandAnalysisAPI.Analyzer;
using HandAnalysisAPI.Enums;

namespace HandAnalysisAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class HandController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Analyze([FromBody] IEnumerable<Card> cards)
    {
        if (cards == null || !cards.Any())
        {
            return BadRequest("Cards collection is empty or null.");
        }

        int cardsCount = cards.Count();

        if (cardsCount < 5 || cardsCount > 7)
        {
            return BadRequest("Invalid cards amount");
        }        

        return Ok(HandAnalyzer.Analyze(cards).ToString());
    }
}