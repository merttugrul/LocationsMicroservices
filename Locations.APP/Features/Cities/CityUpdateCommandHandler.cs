using CORE.APP.Models;
using Locations.APP.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Locations.APP.Features.Cities;

public class CityUpdateCommand : IRequest<CommandResponse>
{
    public int Id { get; set; }
    public string CityName { get; set; }
    public int CountryId { get; set; }
}

public class CityUpdateCommandHandler : IRequestHandler<CityUpdateCommand, CommandResponse>
{
    private readonly LocationsDb _db;

    public CityUpdateCommandHandler(LocationsDb db)
    {
        _db = db;
    }

    public async Task<CommandResponse> Handle(CityUpdateCommand request, CancellationToken cancellationToken)
    {
        var city = await _db.Cities.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        
        if (city == null)
            return new CommandResponse(false, "City not found.");

        city.CityName = request.CityName;
        city.CountryId = request.CountryId;
        
        await _db.SaveChangesAsync(cancellationToken);

        return new CommandResponse(true, "City updated successfully.");
    }
}