using CORE.APP.Domain;
using System.ComponentModel.DataAnnotations;

namespace Locations.APP.Domain;

public class City : Entity
{
    [Required]
    [StringLength(100)]
    public string CityName { get; set; }

    [Required]
    public int CountryId { get; set; }
    
    public Country Country { get; set; }
}