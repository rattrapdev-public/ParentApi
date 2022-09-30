﻿using System.ComponentModel.DataAnnotations;

namespace Parent.Persistence;

public class GuardianDto
{
    [Key]
    public Guid GuardianId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }
    public string EmailAddress { get; set; }
}