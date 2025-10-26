using CORE.APP.Models;

namespace Locations.APP.Features.Cities;

public class CityQueryResponse : Response
{
    public string CityName { get; set; }
    public int CountryId { get; set; }
    public string CountryName { get; set; }
}