using System;
using System.Collections.Generic;

namespace PackageWebService.Models;

public partial class Wishlist
{
    public int Id { get; set; }

    public string? UserId { get; set; }

    public string? PackageId { get; set; }
}
