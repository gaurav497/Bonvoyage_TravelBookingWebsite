using System;
using System.Collections.Generic;

namespace BookingWebService.Models;

public partial class Booking
{
    public string BookingId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string PackageId { get; set; } = null!;

    public string PackageName { get; set; } = null!;

    public string PackageImage { get; set; } = null!;

    public int BookingPerson { get; set; }

    public int BookingRooms { get; set; }

    public DateOnly? Bookingdate { get; set; }

    public int? TotalCost { get; set; }
}
