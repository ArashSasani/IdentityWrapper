using CMS.Service.Dtos.RestrictedIP;
using System.Collections.Generic;

namespace CMS.Service.Interfaces
{
    public interface IRestrictedIPService
    {
        List<RestrictedIPDto> Get();
        RestrictedIPDto GetById(int id);
        void Create(CreateRestrictedIPDto dto);
        void Update(UpdateRestrictedDto dto);
        void Delete(int id);
        int DeleteAll(string items);
    }
}
