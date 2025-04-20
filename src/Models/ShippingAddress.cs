using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TallerWebM.src.Models
{
    public class ShippingAddress
    {
    public required int Id { get; set; } = 0;

    public required string Street { get; set; } = string.Empty;

    public required int NumberStreet { get; set; } = 0;

    public required string Commune { get; set; } = string.Empty;

    public required string Region { get; set; } = string.Empty;

    public required string ZipCode { get; set; } = string.Empty;

    public int UserId { get; set; } = 0;

    public User User { get; set; } 
    }
}