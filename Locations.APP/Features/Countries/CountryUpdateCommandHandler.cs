using CORE.APP.Models;
using Locations.APP.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Locations.APP.Features.Countries;

public class CountryUpdateCommand : IRequest<CommandResponse>
{
    public int Id { get; set; }
    public string CountryName { get; set; }
}

public class CountryUpdateCommandHandler : IRequestHandler<CountryUpdateCommand, CommandResponse>
{
    private readonly LocationsDb _db;

    public CountryUpdateCommandHandler(LocationsDb db)
    {
        _db = db;
    }

    public async Task<CommandResponse> Handle(CountryUpdateCommand request, CancellationToken cancellationToken)
    {
        var country = await _db.Countries.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        
        if (country == null)
            return new CommandResponse(false, "Country not found.");

        country.CountryName = request.CountryName;
        
        await _db.SaveChangesAsync(cancellationToken);

        return new CommandResponse(true, "Country updated successfully.");
    }
}