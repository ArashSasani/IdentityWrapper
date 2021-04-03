using AutoMapper;
using CMS.Core.Interfaces;
using CMS.Core.Model;
using CMS.Service.Dtos.RestrictedAccessTime;
using CMS.Service.Interfaces;
using DateConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication.Infrastructure;
using WebApplication.Infrastructure.Interfaces;
using WebApplication.Infrastructure.Paging;
using WebApplication.Infrastructure.Parser;

namespace CMS.Service
{
    public class RestrictedAccessTimeService : IRestrictedAccessTimeService
    {
        private readonly IRepository<RestrictedAccessTime> _restrictedAccessTimeRepository;

        private readonly IExceptionLogger _logger;

        public RestrictedAccessTimeService(IRepository<RestrictedAccessTime> restrictedAccessTimeRepository
            ,IExceptionLogger logger)
        {
            _restrictedAccessTimeRepository = restrictedAccessTimeRepository;

            _logger = logger;
        }

        public IPaging<RestrictedAccessTimeDto> Get(string searchTerm, string sortItem, string sortOrder
            , PagingQueryString pagingQueryString)
        {
            IPaging<RestrictedAccessTimeDto> model = new RestrictedAccessTimeDtoPagingList();

            var query = _restrictedAccessTimeRepository.Get();
            //total number of items
            int queryCount = query.Count();
            switch (sortItem)
            {
                case "date":
                    query = sortOrder == "asc" ? query.OrderBy(o => o.Date)
                        : query.OrderByDescending(o => o.Date);
                    break;
                case "from_time":
                    query = sortOrder == "asc" ? query.OrderBy(o => o.FromTime)
                        : query.OrderByDescending(o => o.FromTime);
                    break;
                case "to_time":
                    query = sortOrder == "asc" ? query.OrderBy(o => o.ToTime)
                        : query.OrderByDescending(o => o.ToTime);
                    break;
                default:
                    query = query.OrderByDescending(o => o.Id);
                    break;
            }

            List<RestrictedAccessTime> queryResult;
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
            model.PagingList = Mapper.Map<List<RestrictedAccessTimeDto>>(queryResult);

            return model;
        }

        public RestrictedAccessTimeDto GetById(int id)
        {
            var restrictedAccessTime = _restrictedAccessTimeRepository.GetById(id);
            if (restrictedAccessTime == null)
            {
                return null;
            }
            return Mapper.Map<RestrictedAccessTimeDto>(restrictedAccessTime);
        }

        public void Create(CreateRestrictedAccessTimeDto dto)
        {
            var restrictedAccessTime = new RestrictedAccessTime
            {
                Date = !string.IsNullOrEmpty(dto.Date) 
                    ? (DateTime?)dto.Date.PersianToGregorian(Converter.IncludeTime.No) : null,
                FromTime = dto.FromTime,
                ToTime = dto.ToTime
            };

            _restrictedAccessTimeRepository.Insert(restrictedAccessTime);
        }

        public void Update(UpdateRestrictedAccessTimeDto dto)
        {
            var restrictedAccessTime = _restrictedAccessTimeRepository.GetById(dto.Id);
            if (restrictedAccessTime != null)
            {
                restrictedAccessTime.Date = !string.IsNullOrEmpty(dto.Date)
                    ? (DateTime?)dto.Date.PersianToGregorian(Converter.IncludeTime.No) : null;
                restrictedAccessTime.FromTime = dto.FromTime;
                restrictedAccessTime.ToTime = dto.ToTime;

                _restrictedAccessTimeRepository.Update(restrictedAccessTime);
            }
            else
            {
                try
                {
                    throw new LogicalException();
                }
                catch (LogicalException ex)
                {
                    _logger.LogLogicalError
                        (ex, "RestrictedAccessTime entity with the id: '{0}', is not available." +
                        " update operation failed.", dto.Id);
                    throw;
                }
            }
        }

        public void Delete(int id)
        {
            var restrictedAccessTime = _restrictedAccessTimeRepository.GetById(id);
            if (restrictedAccessTime != null)
            {
                _restrictedAccessTimeRepository.Delete(restrictedAccessTime);
            }
            else
            {
                try
                {
                    throw new LogicalException();
                }
                catch (LogicalException ex)
                {
                    _logger.LogLogicalError
                        (ex, "RestrictedAccessTime entity with the id: '{0}', is not available." +
                        " delete operation failed.", id);
                    throw;
                }
            }
        }

        public int DeleteAll(string items)
        {
            try
            {
                var idsToRemove = items.ParseToIntArray().ToList();
                idsToRemove.ForEach(i => _restrictedAccessTimeRepository.Delete(i));

                return idsToRemove.Count;
            }
            catch (LogicalException ex)
            {
                _logger.LogRunTimeError(ex, ex.Message);
                throw;
            }
        }
    }
}
