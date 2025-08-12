using PolicyProcessor.Infrastructure.Abstractions;

namespace PolicyProcessor.Repository.Repositories;

public class PolicyQueryRepository(PolicyDbContext dbContext)
    : IPolicyQueryRepository
{
    
}