using System;
using System.Collections.Generic;

namespace VideoCall.DataAccess.Entities;

public partial class User
{
    public Guid Id { get; set; }

    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Fullname { get; set; } = null!;

    public bool Status { get; set; }
    public string? CallId { get; set; }
}
public class UserCall : User
{
    public string SesstionID { get; set; }
    public bool isLogined { get; set; }
}
