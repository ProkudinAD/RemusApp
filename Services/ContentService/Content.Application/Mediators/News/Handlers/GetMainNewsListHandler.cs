using AutoMapper;
using Content.Application.Common;
using Content.Application.Mediators.News.Queries;
using Content.Contracts.Dto;
using Content.Domain.Entities.News;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Content.Application.Mediators.News.Handlers
{
    public class GetMainNewsListHandler : IRequestHandler<GetMainNewsListQuery, List<NewsMainDto>>
    {
        private readonly IUnitOfWork _context;
        private readonly IMapper _mapper;

        public GetMainNewsListHandler(IUnitOfWork context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<NewsMainDto>> Handle(GetMainNewsListQuery request, CancellationToken cancellationToken)
        {
            var newsList = await _context.Set<NewsMain>()
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<NewsMainDto>>(newsList);
        }
    }
}