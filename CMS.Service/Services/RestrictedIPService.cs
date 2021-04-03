using AutoMapper;
using CMS.Core.Interfaces;
using CMS.Core.Model;
using CMS.Service.Dtos.RestrictedIP;
using CMS.Service.Interfaces;
using System.Collections.Generic;
using System.Linq;
using WebApplication.Infrastructure;
using WebApplication.Infrastructure.Interfaces;
using WebApplication.Infrastructure.Parser;

namespace CMS.Service
{
    public class RestrictedIPService : IRestrictedIPService
    {
        private readonly IRepository<RestrictedIP> _restrictedIPsRepository;
        private readonly IExceptionLogger _logger;

        public RestrictedIPService(IRepository<RestrictedIP> restrictedIPsRepository
            , IExceptionLogger logger)
        {
            _restrictedIPsRepository = restrictedIPsRepository;

            _logger = logger;
        }

        public List<RestrictedIPDto> Get()
        {
            var restrictedIPs = _restrictedIPsRepository.Get().ToList();
            return Mapper.Map<List<RestrictedIPDto>>(restrictedIPs);
        }

        public RestrictedIPDto GetById(int id)
        {
            var restrictedIP = _restrictedIPsRepository.GetById(id);
            return Mapper.Map<RestrictedIPDto>(restrictedIP);
        }

        public void Create(CreateRestrictedIPDto dto)
        {
            var restrictedIP = new RestrictedIP
            {
                IP = dto.IP
            };
            _restrictedIPsRepository.Insert(restrictedIP);
        }

        public void Update(UpdateRestrictedDto dto)
        {
            var restrictedIP = _restrictedIPsRepository.GetById(dto.Id);
            if (restrictedIP != null)
            {
                restrictedIP.IP = dto.IP;

                _restrictedIPsRepository.Update(restrictedIP);
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
                        (ex, "RestrictedIP entity with the id: '{0}', is not available." +
                        " update operation failed.", dto.Id);
                    throw;
                }
            }
        }

        public void Delete(int id)
        {
            var restrictedIP = _restrictedIPsRepository.GetById(id);
            if (restrictedIP != null)
            {
                _restrictedIPsRepository.Delete(id);
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
                        (ex, "RestrictedIP entity with the id: '{0}', is not available." +
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
                idsToRemove.ForEach(i => _restrictedIPsRepository.Delete(i));

                return idsToRemove.Count;
            }
            catch (LogicalException ex)
            {
                _logger.LogRunTimeError(ex, ex.Message);
                throw;
            };
        }
    }
}
