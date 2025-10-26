using Locations.APP.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Locations.APP.Features.Cities;

public class CityByCountryIdQuery : IRequest<List<CityQueryResponse>>
{
    public int CountryId { get; set; }
}

public class CityByCountryIdQueryHandler : IRequestHandler<CityByCountryIdQuery, List<CityQueryResponse>>
{
    private readonly LocationsDb _db;

    public CityByCountryIdQueryHandler(LocationsDb db)
    {
        _db = db;
    }

    public async Task<List<CityQueryResponse>> Handle(CityByCountryIdQuery request, CancellationToken cancellationToken)
    {
        return await _db.Cities
            .Include(c => c.Country)
            .Where(c => c.CountryId == request.CountryId)
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