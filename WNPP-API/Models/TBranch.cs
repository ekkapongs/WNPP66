using System;
using System.Collections.Generic;

namespace WNPP_API.Models;

public partial class TBranch
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

    public string? BranchName { get; set; }

    public int? BranchType { get; set; }

    public string? BranchTypeName { get; set; }

    public string? MonasteryName { get; set; }

    public int? MonasteryType { get; set; }

    public string? MonasteryTypeName { get; set; }

    public string? MonasteryPhoneNo { get; set; }

    public string? DepositaryName { get; set; }

    public string? DepositaryPhoneNo { get; set; }

    public decimal? MonasteryAreaRai { get; set; }

    public decimal? MonasteryAreaNgan { get; set; }

    public decimal? MonasteryAreaWa { get; set; }

    public string? LandRightsDocuments { get; set; }

    public DateTime? DateOfFounding { get; set; }

    public string? EcclesiasticalTitle { get; set; }

    public int? EcclesiasticalType { get; set; }

    public string? AbbotTitle { get; set; }

    public string? AbbotName { get; set; }

    public int? AbbotType { get; set; }

    public string? AbbotTemple { get; set; }

    public string? Preceptor { get; set; }

    public string? PreceptorTemple { get; set; }

    public DateTime? DateOfAcceptPosition { get; set; }

    public string? AbbotPhoneNo { get; set; }

    public string? AbbotEmail { get; set; }

    public string? AbbotLineId { get; set; }

    public string? AbbotImageUrl { get; set; }

    public string? CertifierName { get; set; }

    public string? CertifierTemple { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public DateTime? DateOfOrdination { get; set; }

    public DateTime? AbbotSignDate { get; set; }

    public string? AddressTextMonatery { get; set; }

    public string? HouseNoMonatery { get; set; }

    public string? MooMonatery { get; set; }

    public string? VillageMonatery { get; set; }

    public string? RoadMonatery { get; set; }

    public string? SubDistrictMonatery { get; set; }

    public string? DistrictMonatery { get; set; }

    public string? ProvinceMonatery { get; set; }

    public string? CountryMonatery { get; set; }

    public string? PostCodeMonatery { get; set; }
}
