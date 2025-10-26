using CORE.APP.Models;
using Locations.APP.Domain;
using MediatR;

namespace Locations.APP.Features.Cities;

public class CityCreateCommand : IRequest<CommandResponse>
{
    public string CityName { get; set; }
    public int CountryId { get; set; }
}

public class CityCreateCommandHandler : IRequestHandler<CityCreateCommand, CommandResponse>
{
    private readonly LocationsDb _db;

    public CityCreateCommandHandler(LocationsDb db)
    {
        _db = db;
    }

    public async Task<CommandResponse> Handle(CityCreateCommand request, CancellationToken cancellationToken)
    {
        var city = new City
        {
            CityName = request.CityName,
            CountryId = request.CountryId
        };

        _db.Cities.Add(city);
        await _db.SaveChangesAsync(cancellationToken);

        return new CommandResponse(true, "City created successfully.");
    }
}