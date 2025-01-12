using System;

namespace Clients.Models;

public class ClientVisit
{
    public DateTimeOffset DateTime { get; set; } = DateTimeOffset.Now;
    public string? Text { get; set; }
}
