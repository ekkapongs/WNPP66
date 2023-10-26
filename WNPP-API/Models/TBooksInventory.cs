using System;
using System.Collections.Generic;

namespace WNPP_API.Models;

public partial class TBooksInventory
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

    public int? BookId { get; set; }

    public string? BookName { get; set; }

    public DateTime? MoveDate { get; set; }

    public bool? IsDhammaHeritage { get; set; }

    public bool? IsAdjustInventory { get; set; }

    public double? UnitsCost { get; set; }

    public int? UnitsIn { get; set; }

    public int? UnitsLoss { get; set; }

    public int? UnitsBalance { get; set; }
}
