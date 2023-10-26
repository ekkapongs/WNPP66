using System;
using System.Collections.Generic;

namespace WNPP_API.Models;

public partial class TBook
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

    public string? Isbn { get; set; }

    public string? BookName { get; set; }

    public string? Edition { get; set; }

    public string? Author { get; set; }

    public string? Publisher { get; set; }

    public double? Price { get; set; }

    public double? Cost { get; set; }

    public DateTime? PublicationDate { get; set; }

    public bool? IsDhammaHeritage { get; set; }

    public int? InStock { get; set; }

    public int? CountDamaged { get; set; }
}
