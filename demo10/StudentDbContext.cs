using System;
using demo10.Models;
using Microsoft.EntityFrameworkCore;

namespace demo10;

public class StudentDbContext : DbContext
{
    public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options) { }

    public DbSet<Student> Student { get; set; }
}
