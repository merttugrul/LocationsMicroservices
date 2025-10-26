using CORE.APP.Models;
using Locations.APP.Domain;
using MediatR;

namespace Locations.APP.Features.Countries;

public class CountryCreateCommand : IRequest<CommandResponse>
{
    public string CountryName { get; set; }
}

public class CountryCreateCommandHandler : IRequestHandler<CountryCreateCommand, CommandResponse>
{
    private readonly LocationsDb _db;

    public CountryCreateCommandHandler(LocationsDb db)
    {
        _db = db;
    }

    public async Task<CommandResponse> Handle(CountryCreateCommand request, CancellationToken cancellationToken)
    {
        var country = new Country
        {
            CountryName = request.CountryName
        };

        _db.Countries.Add(country);
        await _db.SaveChangesAsync(cancellationToken);

        return new CommandResponse(true, "Country created successfully.");
    }
}