using Abp.Application.Services.Dto;
using Abp.AutoMapper;


namespace HRIS.Setup.PublicHolidayDetails.Dto
{
[AutoMap(typeof(Entities.Setup.PublicHolidayDetails))]
public class PublicHolidayDetailsDto : EntityDto
{
public int PublicHolidayId{ get; set; }
public DateTime? StartDate{ get; set; }
public DateTime? EndDate{ get; set; }
public bool? IsLunarDependent{ get; set; }
public int? NoOfDaysForLunarAdjustment{ get; set; }
public bool? IsActive{ get; set; }
public bool? IsDefault{ get; set; }

public PublicHolidayDetailsDto ()
{
IsActive = false;
IsDefault = false;
}
}
}
