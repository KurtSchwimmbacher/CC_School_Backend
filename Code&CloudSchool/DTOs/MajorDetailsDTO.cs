using System;

namespace Code_CloudSchool.DTOs;

public class MajorDetailsDTO
{
    public int? MajorId { get; set; }
    public string? MajorName { get; set; }
    public string? MajorCode { get; set; }
    public string? MajorDescription { get; set; }
    public int? CreditsRequired { get; set; }
}
