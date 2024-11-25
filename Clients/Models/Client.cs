using System;
using System.Collections.Generic;

namespace ClientsMVVM.Models;

public class Client
{
    public required Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string Surname { get; set; }
    public string Description { get; set; } = null!;

    public List<ClientVisit> Visits { get; set; } = [];
}
