using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using System.Collections.Generic;
using System.Linq;
using HRIS.Extensions;
using HRIS.WebQueryModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HRIS.Setup.PublicHolidayDetails
{
[RemoteService(false)]
public class PublicHolidayDetailsAppService : ApplicationService, IPublicHolidayDetailsAppService
{
private readonly IObjectMapper _objectMapper;
private  readonly IRepository<Entities.Setup.PublicHolidayDetails> _publicholidaydetailsRepository;

public PublicHolidayDetailsAppService(IObjectMapper objectMapper,IRepository<Entities.Setup.PublicHolidayDetails> publicholidaydetailsRepository)
{
_objectMapper = objectMapper;
_publicholidaydetailsRepository = publicholidaydetailsRepository;
}

public async Task<PublicHolidayDetailsDto> Create(PublicHolidayDetailsDto publicholidaydetails)
{
var publicholidaydetailsObj = _objectMapper.Map<Entities.Setup.PublicHolidayDetails>(publicholidaydetails);
publicholidaydetailsObj.TenantId = 2;
return _objectMapper.Map<PublicHolidayDetailsDto>(await _publicholidaydetailsRepository.InsertAsync(publicholidaydetailsObj));
}

public async Task<PublicHolidayDetailsDto> GetById(int id)
{
var result = await _publicholidaydetailsRepository
.FirstOrDefaultAsync(w => w.Id == id && w.IsDeleted == false);

return _objectMapper.Map<PublicHolidayDetailsDto>(result);
}

public async Task<QueryResult<PublicHolidayDetailsDto>> GetAll(IQueryObject queryObject)
{
var result = new QueryResult<PublicHolidayDetailsDto>();
var query = _publicholidaydetailsRepository
.GetAll()
.Where(w => w.IsDeleted == false).AsQueryable();

var columnsMap = new Dictionary<string, Expression<Func<Entities.Setup.PublicHolidayDetails, object>>>
{
["PublicHolidayId"] = v => v.PublicHolidayId,
["StartDate"] = v => v.StartDate,
["EndDate"] = v => v.EndDate,
["IsLunarDependent"] = v => v.IsLunarDependent,
["NoOfDaysForLunarAdjustment"] = v => v.NoOfDaysForLunarAdjustment,
};
query = query.ApplyOrdering(queryObject, columnsMap);

result.TotalItems = await query.CountAsync();

query = query.ApplyPaging(queryObject);

result.Items = _objectMapper.Map<IEnumerable<PublicHolidayDetailsDto>>(await query.ToListAsync());

return result;
}

public async Task<PublicHolidayDetailsDto> Update(PublicHolidayDetailsDto publicholidaydetails)
{
var result = _objectMapper.Map<Entities.Setup.PublicHolidayDetails>(publicholidaydetails);
return _objectMapper.Map<PublicHolidayDetailsDto>(await _publicholidaydetailsRepository.UpdateAsync(result));
}

public async Task DeleteById(int id)
{
await _publicholidaydetailsRepository.DeleteAsync(id);
}

}
}
