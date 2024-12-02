using System;
using System.ComponentModel.DataAnnotations;

namespace demo10.Models;

public class Student
{
    [Key]
    public int? Id { get; set; }
    public string? Name { get; set; } = string.Empty;
    public int? Age { get; set; }
}
