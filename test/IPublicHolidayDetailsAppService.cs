using Abp.Application.Services;
using System.Collections.Generic;


namespace HRIS.Setup.PublicHolidayDetails
{
[RemoteService(false)]
public interface IPublicHolidayDetailsAppService : IApplicationService
{
Task<PublicHolidayDetailsDto> GetById(int id);
Task<QueryResult<PublicHolidayDetailsDto>> GetAll(IQueryObject queryObject);
Task<PublicHolidayDetailsDto> Create(PublicHolidayDetailsDto publicholidaydetails);
Task<PublicHolidayDetailsDto> Update(PublicHolidayDetailsDto publicholidaydetails);
Task DeleteById(int id);
}
}
