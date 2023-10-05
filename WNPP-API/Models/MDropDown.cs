using System;
using System.Collections.Generic;

namespace WNPP_API.Models;

public partial class MDropDown
{
    public int Id { get; set; }

    public bool? ActiveStatus { get; set; }

    public int? LanguageId { get; set; }

    public string? RecordStatus { get; set; }

    public int? CreatedBy { get; set; }

    public int? ModifiedBy { get; set; }

    public string? CreatedByName { get; set; }

    public string? ModifiedByName { get; set; }

    public string? Notation { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? DropDownId { get; set; }

    public string? DropDownType { get; set; }

    public int? DropDownTypeSeq { get; set; }

    public string? DropDownName { get; set; }
}
