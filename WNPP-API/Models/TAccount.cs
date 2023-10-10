using System;
using System.Collections.Generic;

namespace WNPP_API.Models;

public partial class TAccount
{
    public int Id { get; set; }

    public bool? ActiveStatus { get; set; }

    public int? Language { get; set; }

    public string? RecordStatus { get; set; }

    public int? CreatedBy { get; set; }

    public int? ModifiedBy { get; set; }

    public string? CreatedByName { get; set; }

    public string? ModifiedByName { get; set; }

    public string? Notation { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public DateTime? ExpireDate { get; set; }

    public string? DisplayName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Ref1 { get; set; }

    public string? Ref2 { get; set; }
}
