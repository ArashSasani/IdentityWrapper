using CMS.Service.Dtos.UserLog;
using WebApplication.Infrastructure.Interfaces;
using WebApplication.Infrastructure.Paging;

namespace CMS.Service.Interfaces
{
    public interface IUserLoggerService
    {
        IPaging<UserLogDto> Get(string userId, string searchTerm, string sortItem, string sortOrder
            , PagingQueryString pagingQueryString);
        UserLogDto GetById(int id);
    }
}
