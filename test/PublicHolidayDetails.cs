using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using HRIS.Entities.BaseEntity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HRIS.Entities.Setup
{

public class PublicHolidayDetails : FullAuditedEntity<int>, IMustHaveTenant, IHasIsActive, IHasIsDefault
{
public int PublicHolidayId { get; set; }
public DateTime? StartDate { get; set; }
public DateTime? EndDate { get; set; }
public bool? IsLunarDependent { get; set; }
public int? NoOfDaysForLunarAdjustment { get; set; }
public int TenantId { get; set; }
public bool? IsActive { get; set; }
public bool? IsDefault { get; set; }
public int? CompanyId {get; set; }
}
}
