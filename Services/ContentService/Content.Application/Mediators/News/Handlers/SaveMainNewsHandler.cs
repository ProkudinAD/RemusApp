using Ardalis.Result;
using Content.Application.Common;
using Content.Application.Mediators.News.Commands;
using Content.Domain.Entities.News;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Content.Application.Mediators.News.Handlers
{
    internal sealed class SaveMainNewsHandler(IUnitOfWork context) : IRequestHandler<SaveMainNewsCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(SaveMainNewsCommand request, CancellationToken cancellationToken)
        {
            NewsMain? entity = null;
            var newNewsMainId = Guid.NewGuid();

            if (request.Model.Id != Guid.Empty)
            {
                entity = await context.Set<NewsMain>()
                     .AsNoTracking()
                     .FirstOrDefaultAsync(x => x.Id == request.Model.Id, cancellationToken);
            }
            else
            {
                entity = context.Set<NewsMain>().Add(new NewsMain()).Entity;
                entity.Id = newNewsMainId;
            }

            entity.NewsText = request.Model.NewsText;
            entity.NewsTitle = request.Model.NewsTitle;

            await context.SaveAsync(cancellationToken);
            return Result<Guid>.Success(entity.Id);
        }
    }

}