using AutoMapper;
using CMS.Core.Interfaces;
using CMS.Core.Model;
using CMS.Service.Dtos.UserLog;
using CMS.Service.Interfaces;
using System.Collections.Generic;
using System.Linq;
using WebApplication.Infrastructure.Interfaces;
using WebApplication.Infrastructure.Paging;

namespace CMS.Service
{
    public class UserLoggerService : IUserLoggerService
    {
        private readonly IRepository<UserLog> _userLoggerRepository;

        public UserLoggerService(IRepository<UserLog> userLoggerRepository)
        {
            _userLoggerRepository = userLoggerRepository;
        }

        public IPaging<UserLogDto> Get(string userId, string searchTerm, string sortItem
            , string sortOrder, PagingQueryString pagingQueryString)
        {
            IPaging<UserLogDto> model = new UserLogDtoPagingList();

            var query = !string.IsNullOrEmpty(searchTerm)
                ? _userLoggerRepository.Get(q => q.UserId == userId 
                    && q.User.UserName.ToLower().Contains(searchTerm.ToLower())
                    , includeProperties: "User")
                : _userLoggerRepository.Get(q => q.UserId == userId, includeProperties: "User");
            //total number of items
            int queryCount = query.Count();
            switch (sortItem)
            {
                case "username":
                    query = sortOrder == "asc" ? query.OrderBy(o => o.User.UserName)
                        : query.OrderByDescending(o => o.User.UserName);
                    break;
                case "date":
                    query = sortOrder == "asc" ? query.OrderBy(o => o.Date)
                        : query.OrderByDescending(o => o.Date);
                    break;
                default:
                    query = query.OrderByDescending(o => o.Date);
                    break;
            }

            List<UserLog> queryResult;
            if (pagingQueryString != null) //with paging
            {
                var pageSetup = new PagingSetup(pagingQueryString.Page, pagingQueryString.PageSize);
                queryResult = query.Skip(pageSetup.Offset).Take(pageSetup.Next).ToList();
                //paging controls
                var controls = pageSetup.GetPagingControls(queryCount, PagingStrategy.ReturnNull);
                if (controls != null)
                {
                    model.PagesCount = controls.PagesCount;
                    model.NextPage = controls.NextPage;
                    model.PrevPage = controls.PrevPage;
                }
            }
            else //without paging
            {
                queryResult = query.ToList();
            }
            model.PagingList = Mapper.Map<List<UserLogDto>>(queryResult);

            return model;
        }

        public UserLogDto GetById(int id)
        {
            var userLog = _userLoggerRepository.Get(q => q.Id == id, includeProperties: "User")
                .SingleOrDefault();
            if (userLog == null)
            {
                return null;
            }
            return Mapper.Map<UserLogDto>(userLog);
        }
    }
}
