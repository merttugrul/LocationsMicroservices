using Locations.APP.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Locations.APP.Features.Cities;

public class CityQuery : IRequest<List<CityQueryResponse>>
{
}

public class CityQueryHandler : IRequestHandler<CityQuery, List<CityQueryResponse>>
{
    private readonly LocationsDb _db;

    public CityQueryHandler(LocationsDb db)
    {
        _db = db;
    }

    public async Task<List<CityQueryResponse>> Handle(CityQuery request, CancellationToken cancellationToken)
    {
        return await _db.Cities
            .Include(c => c.Country)
            .Select(c => new CityQueryResponse
            {
                Id = c.Id,
                Guid = c.Guid,
                CityName = c.CityName,
                CountryId = c.CountryId,
                CountryName = c.Country.CountryName
            })
            .ToListAsync(cancellationToken);
    }
}