using CORE.APP.Models;
using Locations.APP.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Locations.APP.Features.Countries;

public class CountryDeleteCommand : IRequest<CommandResponse>
{
    public int Id { get; set; }
}

public class CountryDeleteCommandHandler : IRequestHandler<CountryDeleteCommand, CommandResponse>
{
    private readonly LocationsDb _db;

    public CountryDeleteCommandHandler(LocationsDb db)
    {
        _db = db;
    }

    public async Task<CommandResponse> Handle(CountryDeleteCommand request, CancellationToken cancellationToken)
    {
        var country = await _db.Countries.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        
        if (country == null)
            return new CommandResponse(false, "Country not found.");

        _db.Countries.Remove(country);
        await _db.SaveChangesAsync(cancellationToken);

        return new CommandResponse(true, "Country deleted successfully.");
    }
}