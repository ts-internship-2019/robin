using iWasHere.Domain.DTOs;
using iWasHere.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iWasHere.Domain.Service
{
    public class DictionaryService
    {
        private readonly RobinContext _dbContext;
        public DictionaryService(RobinContext databaseContext)
        {
            _dbContext = databaseContext;
        }

        public List<DictionaryLandmarkTypeModel> GetDictionaryLandmarkTypeModels()
        {
            List<DictionaryLandmarkTypeModel> dictionaryLandmarkTypeModels = _dbContext.DictionaryLandmarkType.Select(a => new DictionaryLandmarkTypeModel()
            {
                Id = a.ItemId,
                Name = a.ItemName
            }).ToList();

            return dictionaryLandmarkTypeModels;
        }
        public List<DictionaryCountry> GetDictionaryCountry()
        {
            List<DictionaryCountry> dictionaryCountry = _dbContext.DictionaryCountry.Select(a => new DictionaryCountry()
            {
                CountryId = a.CountryId,
                CountryName = a.CountryName
            }).ToList();

            return dictionaryCountry;
        }
        public int GetDictionaryCountryCount()
        {
            return _dbContext.DictionaryCountry.Count();
        }

        public List<DictionaryCountry> GetDictionaryCountryFilterPage(int page, int pageSize, string txtboxCountryName)
        {
            IQueryable<DictionaryCountry> queryable = _dbContext.DictionaryCountry;
            if (!string.IsNullOrWhiteSpace(txtboxCountryName))
            {
                queryable = queryable.Where(a => a.CountryName.Contains(txtboxCountryName));
            }
            queryable = queryable.Select(a => new DictionaryCountry()
            {
                CountryId = a.CountryId,
                CountryName = a.CountryName,
            }).Skip((page - 1) * pageSize).Take(pageSize);

            return queryable.ToList();
        }

        public List<DictionaryCity> GetDictionaryCity()
        {
            List<DictionaryCity> dictionaryCity = _dbContext.DictionaryCity.Select(a => new DictionaryCity()
            {
                CityId = a.CityId,
                CityName = a.CityName,
                CountyId = a.CountyId,
            }).ToList();

            return dictionaryCity;
        }
        public int GetDictionaryCityCount()
        {
            return _dbContext.DictionaryCity.Count();
        }
        public List<DictionaryCity> GetDictionaryCityFilterPage(int page, int pageSize,string txtboxCityName)
        {
            IQueryable<DictionaryCity> queryable = _dbContext.DictionaryCity;
            if (!string.IsNullOrWhiteSpace(txtboxCityName))
            {
                queryable = queryable.Where(a => a.CityName.Contains(txtboxCityName));
            }
            queryable = queryable.Select(a => new DictionaryCity()
            {
                CityId = a.CityId,
                CityName = a.CityName,
                CountyId = a.CountyId
            }).Skip((page - 1) * pageSize).Take(pageSize);

            return queryable.ToList();
        }
     
        public List<DictionaryCounty> GetDictionaryCounty()
        {
            List<DictionaryCounty> dictionaryCounty = _dbContext.DictionaryCounty.Select(a => new DictionaryCounty()
            {
                CountyId = a.CountyId,
                CountyName = a.CountyName,
                CountryId = a.CountryId
            }).ToList();

            return dictionaryCounty;
        }
    
            public List<DictionaryTicketType> GetDictionaryTicketType()
            {
                List<DictionaryTicketType> dictionaryTickeTypeModel = _dbContext.DictionaryTicketType.Select(a => new DictionaryTicketType()
                {
                    TicketTypeId = a.TicketTypeId,
                    TicketCode = a.TicketCode,
                    TicketName = a.TicketName
                
                }).ToList();

                return dictionaryTickeTypeModel;
            }
        public int GetDictionaryTicketTypeCount()
        {
            return _dbContext.DictionaryTicketType.Count();
        }

        public List<DictionaryTicketType> GetDictionaryTicketTypePage(int page, int pageSize)
        {
            List<DictionaryTicketType> dictionaryTicketType = _dbContext.DictionaryTicketType.Select(a => new DictionaryTicketType()
            {
               TicketTypeId=a.TicketTypeId,
               TicketCode=a.TicketCode,
               TicketName=a.TicketName
            }).Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return dictionaryTicketType;
        }

        public List<DictionaryCurrency> GetDictionaryCurrency()
        {
            List<DictionaryCurrency> dictionaryCurrency = _dbContext.DictionaryCurrency.Select(a => new DictionaryCurrency()
            {
                CurrencyId = a.CurrencyId,
                CurrencyCode = a.CurrencyCode,
                CurrencyName = a.CurrencyName
            }).ToList();

            return dictionaryCurrency;
        }
        public int GetDictionaryCurrencyCount()
        {
            return _dbContext.DictionaryCurrency.Count();
        }
        public List<DictionaryCurrency> GetDictionaryCurrencyPage(int page, int pageSize)
        {
            List<DictionaryCurrency> dictionaryCurrency = _dbContext.DictionaryCurrency.Select(a => new DictionaryCurrency()
            {
                CurrencyId = a.CurrencyId,
                CurrencyCode = a.CurrencyCode,
                CurrencyName = a.CurrencyName
            }).Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return dictionaryCurrency;
        }

        public List<DictionaryLandmarkType> GetDictionaryLandmarkType()
        {
            List<DictionaryLandmarkType> dictionaryLandmarkType = _dbContext.DictionaryLandmarkType.Select(a => new DictionaryLandmarkType()
            {
                ItemId = a.ItemId,
                ItemCode = a.ItemCode,
                ItemName = a.ItemName,
                Description = a.Description
            }).ToList();

            return dictionaryLandmarkType;
        }

        public List<DictionaryLandmarkType> GetDictionaryLandmarkTypePage(int page, int pageSize)
        {
            List<DictionaryLandmarkType> dictionaryLandmarkType = _dbContext.DictionaryLandmarkType.Select(a => new DictionaryLandmarkType()
            {
                ItemId = a.ItemId,
                ItemCode = a.ItemCode,
                ItemName = a.ItemName,
                Description = a.Description
            }).Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return dictionaryLandmarkType;
        }

        public int GetDictionaryLandmarkTypeCount()
        {
            return _dbContext.DictionaryLandmarkType.Count();
        }

        public int GetDictionaryCountyCount()
        {
            return _dbContext.DictionaryCounty.Count();
        }

        public List<DictionaryCounty> GetDictionaryCountyPage(int page, int pageSize)
        {
            List<DictionaryCounty> dictionaryCounty = _dbContext.DictionaryCounty.Select(a => new DictionaryCounty()
            {
                CountyId = a.CountyId,
                CountyName = a.CountyName,
                CountryId = a.CountryId
            }).Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return dictionaryCounty;
        }
        public List<DictionaryAvailability> GetDictionaryAvailability()
        {
            List<DictionaryAvailability> dictionaryAvailability = _dbContext.DictionaryAvailability.Select(a => new DictionaryAvailability()
            {
                AvailabilityId = a.AvailabilityId,
                AvailabilityName = a.AvailabilityName,
                Schedule = a.Schedule,
            }).ToList();

            return dictionaryAvailability;
        }
        public int GetDictionaryAvailabilityCount()
        {
            return _dbContext.DictionaryAvailability.Count();
        }
        public List<DictionaryAvailability> GetDictionaryAvailabilityFilterPage(int page, int pageSize, string txtboxAvailabilityName)
        {
            IQueryable<DictionaryAvailability> queryable = _dbContext.DictionaryAvailability;
            if (!string.IsNullOrWhiteSpace(txtboxAvailabilityName))
            {
                queryable = queryable.Where(a => a.AvailabilityName.Contains(txtboxAvailabilityName));
            }
            queryable = queryable.Select(a => new DictionaryAvailability()
            {
                AvailabilityId = a.AvailabilityId,
                AvailabilityName = a.AvailabilityName,
                Schedule = a.Schedule
            }).Skip((page - 1) * pageSize).Take(pageSize);

            return queryable.ToList();
        }
        public List<DictionaryAttractionType> GetDictionaryAttractionType()
        {
            List<DictionaryAttractionType> dictionaryAttractionType = _dbContext.DictionaryAttractionType.Select(a => new DictionaryAttractionType()
            {
                AttractionTypeId = a.AttractionTypeId,
                AttractionCode = a.AttractionCode,
                AttractionName = a.AttractionName,
            }).ToList();

            return dictionaryAttractionType;
        }
        public int GetDictionaryAttractionTypeCount()
        {
            return _dbContext.DictionaryAttractionType.Count();
        }
        public List<DictionaryAttractionType> GetDictionaryAttractionTypeFilterPage(int page, int pageSize, string txtboxAttractionName)
        {
            IQueryable<DictionaryAttractionType> queryable = _dbContext.DictionaryAttractionType;
            if (!string.IsNullOrWhiteSpace(txtboxAttractionName))
            {
                queryable = queryable.Where(a => a.AttractionName.Contains(txtboxAttractionName));
            }
            queryable = queryable.Select(a => new DictionaryAttractionType()
            {
                AttractionTypeId = a.AttractionTypeId,
                AttractionCode = a.AttractionCode,
                AttractionName = a.AttractionName,
            }).Skip((page - 1) * pageSize).Take(pageSize);

            return queryable.ToList();
        }
    }

}
