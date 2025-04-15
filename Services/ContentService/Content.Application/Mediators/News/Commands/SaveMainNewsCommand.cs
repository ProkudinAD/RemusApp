using Ardalis.Result;
using MediatR;
using Content.Contracts.Dto;

namespace Content.Application.Mediators.News.Commands

{
    public sealed record SaveMainNewsCommand(NewsMainDto Model) : IRequest<Result<Guid>>;
}