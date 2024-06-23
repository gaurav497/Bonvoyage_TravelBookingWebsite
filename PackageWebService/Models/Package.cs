using System;
using System.Collections.Generic;

namespace PackageWebService.Models;

public partial class Package
{
    public string PackageId { get; set; } = null!;

    public string PackageCountry { get; set; } = null!;

    public string PackageCity { get; set; } = null!;

    public string PackageName { get; set; } = null!;

    public string PackageDesc { get; set; } = null!;

    public decimal PackageRating { get; set; }

    public int PackageReviews { get; set; }

    public string PackagePrice { get; set; } = null!;

    public string PackageDuration { get; set; } = null!;

    public string MinAge { get; set; } = null!;

    public int MaxPeople { get; set; }

    public string PackagePickup { get; set; } = null!;

    public DateOnly AvailableDate { get; set; }

    public string PackageLanguage { get; set; } = null!;

    public string PackageIternary { get; set; } = null!;

    public string PackageImage { get; set; } = null!;
}
