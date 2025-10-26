using CORE.APP.Models;
using Locations.APP.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Locations.APP.Features.Cities;

public class CityDeleteCommand : IRequest<CommandResponse>
{
    public int Id { get; set; }
}

public class CityDeleteCommandHandler : IRequestHandler<CityDeleteCommand, CommandResponse>
{
    private readonly LocationsDb _db;

    public CityDeleteCommandHandler(LocationsDb db)
    {
        _db = db;
    }

    public async Task<CommandResponse> Handle(CityDeleteCommand request, CancellationToken cancellationToken)
    {
        var city = await _db.Cities.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        
        if (city == null)
            return new CommandResponse(false, "City not found.");

        _db.Cities.Remove(city);
        await _db.SaveChangesAsync(cancellationToken);

        return new CommandResponse(true, "City deleted successfully.");
    }
}