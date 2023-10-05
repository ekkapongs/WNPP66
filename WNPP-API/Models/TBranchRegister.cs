using System;
using System.Collections.Generic;

namespace WNPP_API.Models;

public partial class TBranchRegister
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

    public string? MonasteryName { get; set; }

    public int? MonasteryType { get; set; }

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

    public DateTime? DateOfBirth { get; set; }

    public DateTime? DateOfOrdination { get; set; }

    public DateTime? AbbotSignDate { get; set; }

    public string? CertifierTitle { get; set; }

    public string? CertifierName { get; set; }

    public string? CertifierTemple { get; set; }

    public int? CertifierTempleType { get; set; }

    public string? CertifierBranch { get; set; }

    public string? CertifierPhoneNo { get; set; }

    public string? CertifierEmail { get; set; }

    public string? CertifierLineId { get; set; }

    public DateTime? CertifierSignDate { get; set; }

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

    public string? AddressTextAbbot { get; set; }

    public string? HouseNoAbbot { get; set; }

    public string? MooAbbot { get; set; }

    public string? VillageAbbot { get; set; }

    public string? RoadAbbot { get; set; }

    public string? SubDistrictAbbot { get; set; }

    public string? DistrictAbbot { get; set; }

    public string? ProvinceAbbot { get; set; }

    public string? CountryAbbot { get; set; }

    public string? PostCodeAbbot { get; set; }

    public string? AddressTextPreceptor { get; set; }

    public string? HouseNoPreceptor { get; set; }

    public string? MooPreceptor { get; set; }

    public string? VillagePreceptor { get; set; }

    public string? RoadPreceptor { get; set; }

    public string? SubDistrictPreceptor { get; set; }

    public string? DistrictPreceptor { get; set; }

    public string? ProvincePreceptor { get; set; }

    public string? CountryPreceptor { get; set; }

    public string? PostCodePreceptor { get; set; }

    public string? AddressTextCertifier { get; set; }

    public string? HouseNoCertifier { get; set; }

    public string? MooCertifier { get; set; }

    public string? VillageCertifier { get; set; }

    public string? RoadCertifier { get; set; }

    public string? SubDistrictCertifier { get; set; }

    public string? DistrictCertifier { get; set; }

    public string? ProvinceCertifier { get; set; }

    public string? CountryCertifier { get; set; }

    public string? PostCodeCertifier { get; set; }
}
