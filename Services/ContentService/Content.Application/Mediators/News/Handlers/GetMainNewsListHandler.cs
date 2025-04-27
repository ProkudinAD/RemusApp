using AutoMapper;
using Content.Application.Common;
using Content.Application.Mediators.News.Queries;
using Content.Contracts.Dto;
using Content.Domain.Entities.News;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Redis.Cache.Cache.Interface;

namespace Content.Application.Mediators.News.Handlers
{
    public class GetMainNewsListHandler : IRequestHandler<GetMainNewsListQuery, List<NewsMainDto>>
    {
        private readonly IUnitOfWork _context;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public GetMainNewsListHandler(IUnitOfWork context, IMapper mapper, ICacheService cacheService)
        {
            _context = context;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<List<NewsMainDto>> Handle(GetMainNewsListQuery request, CancellationToken cancellationToken)
        {
            const string cacheKey = "MainNewsList";
            var cachedData = await _cacheService.GetAsync<List<NewsMainDto>>(cacheKey);

            if (cachedData != null)
            {
                return cachedData;
            }

            var newsList = await _context.Set<NewsMain>()
                .AsNoTracking()
                .ToListAsync();

            var mappedNewsList = _mapper.Map<List<NewsMainDto>>(newsList);

            await _cacheService.SetAsync(cacheKey, mappedNewsList, TimeSpan.FromMinutes(1));

            return mappedNewsList;
        }
    }
}