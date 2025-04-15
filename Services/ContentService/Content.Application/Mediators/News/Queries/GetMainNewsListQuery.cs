using Content.Contracts.Dto;
using MediatR;

namespace Content.Application.Mediators.News.Queries

{
    public record GetMainNewsListQuery() : IRequest<List<NewsMainDto>>;
}