using System;
using System.Collections.Generic;

namespace WNPP_API.Models;

public partial class TMonk
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

    public string? FirstName { get; set; }

    public string? SurName { get; set; }

    public string? NickName { get; set; }

    public string? FullName { get; set; }

    public string? Pid { get; set; }

    public string? PassportNo { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? BloodType { get; set; }

    public string? ContactNumber { get; set; }

    public string? EmergencyNumber { get; set; }

    public string? DrugAllergy { get; set; }

    public string? CongenitalDisease { get; set; }

    public string? AddressText { get; set; }

    public string? Country { get; set; }

    public string? Moo { get; set; }

    public string? Village { get; set; }

    public string? Road { get; set; }

    public string? SubDistrict { get; set; }

    public string? District { get; set; }

    public string? Province { get; set; }

    public string? ZipCode { get; set; }

    public string? MonkRank { get; set; }

    public string? TitleConferredByTheKing { get; set; }

    public string? Preceptor { get; set; }

    public DateTime? OrdinationDate { get; set; }

    public string? Monastery { get; set; }

    public string? BelongingTemple { get; set; }

    public string? AcademicStanding { get; set; }

    public string? AcademicStandingNo { get; set; }

    public string? GraduateOfTheology { get; set; }

    public string? GraduateOfTheologyNo { get; set; }

    public string? Theologian { get; set; }

    public string? TheologianNo { get; set; }

    public string? Email { get; set; }

    public string? LineId { get; set; }

    public string? FaceBook { get; set; }
}
