using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Cards.Application.Models;
using Cards.Application.Queries.Actions;
using Cards.API.Extensions;

namespace Cards.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class CardController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<GetCardActions> _cardValidator;
    public CardController(IMediator mediator, IValidator<GetCardActions> cardValidator)
    {
        _mediator = mediator;
        _cardValidator = cardValidator;
    }

    [HttpPost("available-actions")]
    public async Task<ActionResult<CardActions>> GetCardActions(GetCardActions cardActions, CancellationToken cancellationToken)
    {
        var cardValidation = await _cardValidator.ValidateAsync(cardActions, cancellationToken);
        if (!cardValidation.IsValid)
        {
            cardValidation.AddToModelState(ModelState);
            return BadRequest(ModelState);
        }

        return Ok(await _mediator.Send(cardActions, cancellationToken));
    }
}