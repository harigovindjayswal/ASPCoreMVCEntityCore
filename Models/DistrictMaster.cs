using System;
using System.Collections.Generic;

namespace ASPCoreMVCEntityCore.models;

public partial class DistrictMaster
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? StateId { get; set; }
}
