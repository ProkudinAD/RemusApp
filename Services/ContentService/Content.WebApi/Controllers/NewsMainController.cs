using Content.Application.Mediators.News.Commands;
using Content.Application.Mediators.News.Queries;
using Content.Contracts.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace TwoTowers.WebApi.Controllers;

[ApiController]
[Route("api/news")]
public class NewsMainController(IMediator mediator) : ControllerBase 
{
    [HttpGet("list")]
    [ProducesResponseType(typeof(IEnumerable<NewsMainDto>), 200)]
    public async Task<IActionResult> GetList()
    {   
        var result = await mediator.Send(new GetMainNewsListQuery());
        return Ok(result);
    }

    [HttpPost("save")]
    public async Task<IActionResult> Save([FromBody] NewsMainDto dto)
    {
        await mediator.Send(new SaveMainNewsCommand(dto));
        return Ok();
    }
}


