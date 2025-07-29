using System;
using System.Collections.Generic;

namespace ASPCoreMVCEntityCore.models;

public partial class Student
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public DateOnly? Dob { get; set; }

    public string? Gender { get; set; }

    public string? State { get; set; }

    public string? District { get; set; }

    public string? Hoby { get; set; }

    public string? PhotoPath { get; set; }

    public string? FilePath { get; set; }

    public bool? IsActive { get; set; }
}

public partial class StudentDTO
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public DateOnly? Dob { get; set; }

    public string? Gender { get; set; }

    public string? State { get; set; }

    public string? District { get; set; }

    public List<string>? Hoby { get; set; }

    public IFormFile? PhotoPath { get; set; }

    public IFormFile? FilePath { get; set; }

    public bool IsActive { get; set; }
}

public partial class BindDtlsDTO
{
    public int Id { get; set; }

    public string? Name { get; set; }
}
