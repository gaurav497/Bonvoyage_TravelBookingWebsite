using System;
using System.Collections.Generic;

namespace UserService.Models;

public partial class User
{
    public string UserId { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string UserEmail { get; set; } = null!;

    public long UserPhone { get; set; }

    public string UserPassword { get; set; } = null!;

    public string UserAddress { get; set; } = null!;

    public string UserRole { get; set; } = null!;
}
