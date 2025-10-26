using Locations.APP.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Locations.APP.Features.Countries;

public class CountryQuery : IRequest<List<CountryQueryResponse>>
{
}

public class CountryQueryHandler : IRequestHandler<CountryQuery, List<CountryQueryResponse>>
{
    private readonly LocationsDb _db;

    public CountryQueryHandler(LocationsDb db)
    {
        _db = db;
    }

    public async Task<List<CountryQueryResponse>> Handle(CountryQuery request, CancellationToken cancellationToken)
    {
        return await _db.Countries
            .Select(c => new CountryQueryResponse
            {
                Id = c.Id,
                Guid = c.Guid,
                CountryName = c.CountryName
            })
            .ToListAsync(cancellationToken);
    }
}