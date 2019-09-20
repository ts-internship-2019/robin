using iWasHere.Domain.DTOs;
using iWasHere.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iWasHere.Domain.Service
{
    public class DictionaryService
    {
        private readonly RobinContext _dbContext;
        private static bool UpdateDatabase = false;

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
        #region country
        public List<DictionaryCountry> GetDictionaryCountry()
        {
            List<DictionaryCountry> dictionaryCountry = _dbContext.DictionaryCountry.Select(a => new DictionaryCountry()
            {
                CountryId = a.CountryId,
                CountryName = a.CountryName
            }).ToList();

            return dictionaryCountry;
        }

        public List<DictionaryCountry> GetCmbCountry()
        {           
            IQueryable<DictionaryCountry> queryable = _dbContext.DictionaryCountry;
            queryable = queryable.Select(a => new DictionaryCountry()       
            {
                CountryId = a.CountryId,
                CountryName = a.CountryName
            });
            
                return queryable.ToList();                      
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

        public string Country_DestroyId(int id)
        {
            try
            {
                _dbContext.Remove(_dbContext.DictionaryCountry.Single(a => a.CountryId == id));
                _dbContext.SaveChanges();
                return null;
            }
            catch (Exception ex)
            {
                return "Aceasta tara nu poate fi stearsa.";
            }
        }
        #endregion

        #region dictionarycity
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
        public List<DictionaryCity> GetDictionaryCityFilterPage(int page, int pageSize, string txtboxCityName, int cmbboxCountyId)
        {
            IQueryable<DictionaryCity> queryable = _dbContext.DictionaryCity.Include(c=>c.County);
            if (!string.IsNullOrWhiteSpace(txtboxCityName))
            {
                queryable = queryable.Where(a => a.CityName.Contains(txtboxCityName));
            }
            if (cmbboxCountyId>0)
            {
                queryable = queryable.Where(a => a.County.CountyId==(cmbboxCountyId));
            }
            queryable = queryable.Select(a => new DictionaryCity()
            {
                CityId = a.CityId,
                CityName = a.CityName,
                CountyId = a.CountyId,
                County = a.County
            }).Skip((page - 1) * pageSize).Take(pageSize);

            return queryable.ToList();
        }

        public string City_DestroyId(int id)
        {
            try
            {
                _dbContext.Remove(_dbContext.DictionaryCity.Single(a => a.CityId == id));
                _dbContext.SaveChanges();
                return null;
            }
            catch (Exception ex)
            {
                return "Acest oras nu poate fi sters.";
            }
        }
        public List<DictionaryCounty> GetCmbCounty()
        {
            IQueryable<DictionaryCounty> queryable = _dbContext.DictionaryCounty;
            queryable = queryable.Select(a => new DictionaryCounty()
            {
                CountyId = a.CountyId,
                CountyName = a.CountyName,
                CountryId = a.CountryId
            });

            return queryable.ToList();
        }
        #endregion

        #region dictionaryCounty
        public int GetDictionaryCountyCount()
        {
            return _dbContext.DictionaryCounty.Count();
        }

        public List<DictionaryCounty> GetDictionaryCountyPage(int page, int pageSize, string txtboxCountyName, int cmbboxCountryId)
        {
            IQueryable<DictionaryCounty> queryableCounty = _dbContext.DictionaryCounty.Include(c=>c.Country);

            if (!string.IsNullOrWhiteSpace(txtboxCountyName))
            {
                queryableCounty = queryableCounty.Where(a => a.CountyName.Contains(txtboxCountyName));
            }
            if (cmbboxCountryId > 0)
            {
                queryableCounty = queryableCounty.Where(a => a.Country.CountryId == (cmbboxCountryId));
            }
            queryableCounty = queryableCounty.Select(a => new DictionaryCounty()
            {
                CountyId = a.CountyId,
                CountyName = a.CountyName,
                CountryId = a.CountryId,
                Country = a.Country
            }).Skip((page - 1) * pageSize).Take(pageSize);

            return queryableCounty.ToList();
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

        public int AddNewCounty(DictionaryCounty dictionaryCounty)
        {
            _dbContext.DictionaryCounty.Add(dictionaryCounty);
            return _dbContext.SaveChanges();
        }

        public string County_DestroyId(int id)
        {
            try
            {
                _dbContext.Remove(_dbContext.DictionaryCounty.Single(a => a.CountyId == id));
                _dbContext.SaveChanges();
                return null;
            }
            catch (Exception ex)
            {
                return "Acest judet nu poate fi stearsa.";
            }
        }

        public string County_UpdateId(DictionaryCounty dictionaryCounty)
        {
            try
            {
                var target = (_dbContext.DictionaryCounty.Single(a => a.CountyId == dictionaryCounty.CountyId));

                target.CountyName = dictionaryCounty.CountyName;
                target.CountryId = dictionaryCounty.CountryId;

                _dbContext.Attach(target);
                _dbContext.Entry(target).State = EntityState.Modified;
                _dbContext.SaveChanges();

                return null;
            }
            catch (Exception ex)
            {
                return "Acest judet nu poate fi modificat.";
            }
        }

        public int GetLandmarkCount()
        {
            return _dbContext.Landmark.Count();
        }

        public List<Landmark> GetLandmarkPage(int page, int pageSize, string txtboxLendmarkName)
        {
            IQueryable<Landmark> queryable = _dbContext.Landmark.Include(c => c.DictionaryCity).Include(d=>d.DictionaryAttractionType).Include(e => e.DictionaryAvailability).Include(f => f.DictionaryItem).Include(g => g.Ticket);
            
            if (!string.IsNullOrWhiteSpace(txtboxLendmarkName))
            {
                queryable = queryable.Where(a => a.LandmarkName.Contains(txtboxLendmarkName));
            }
            queryable = queryable.Select(a => new Landmark()            
            {
                LandmarkId = a.LandmarkId,
                LandmarkName = a.LandmarkName,
                LandmarkShortDescription = a.LandmarkShortDescription,
                TicketId = a.TicketId,
                DictionaryAvailabilityId = a.DictionaryAvailabilityId,
                DictionaryItemId = a.DictionaryItemId,
                DateAdded = a.DateAdded,
                DictionaryAttractionTypeId = a.DictionaryAttractionTypeId,
                Longitude = a.Longitude,
                Latitude = a.Latitude,
                DictionaryCityId = a.DictionaryCityId,
                DictionaryCity = a.DictionaryCity,
                DictionaryAttractionType = a.DictionaryAttractionType,
                DictionaryAvailability = a.DictionaryAvailability,
                DictionaryItem= a.DictionaryItem,
                Ticket=a.Ticket

            }).Skip((page-1)*pageSize).Take(pageSize);

            return queryable.ToList();
        }

        public List<Landmark> GetLandmark()
        {
            List<Landmark> dictionaryLandmark = _dbContext.Landmark.Select(a => new Landmark()
            {
                LandmarkId = a.LandmarkId,
                LandmarkName = a.LandmarkName,
                LandmarkShortDescription = a.LandmarkShortDescription,
                TicketId = a.TicketId,
                DictionaryAvailabilityId = a.DictionaryAvailabilityId,
                DictionaryItemId = a.DictionaryItemId,
                DateAdded = a.DateAdded,
                DictionaryAttractionTypeId = a.DictionaryAttractionTypeId,
                Longitude = a.Longitude,
                Latitude = a.Latitude,
                DictionaryCityId = a.DictionaryCityId

            }).ToList();

            return dictionaryLandmark;
        }
        #endregion

        #region tickettype
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
                TicketTypeId = a.TicketTypeId,
                TicketCode = a.TicketCode,
                TicketName = a.TicketName
            }).Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return dictionaryTicketType;
        }

          public List<DictionaryTicketType> GetDictionaryTicketTypeFilterPage(int page, int pageSize,string txtTicketTypeCode)
        {
            IQueryable<DictionaryTicketType> queryable = _dbContext.DictionaryTicketType;
            if (!string.IsNullOrWhiteSpace(txtTicketTypeCode))
            {
                queryable = queryable.Where(a => a.TicketCode.Contains(txtTicketTypeCode));
            }
           
            queryable = queryable.Select(a => new DictionaryTicketType()
            {
               TicketTypeId=a.TicketTypeId,
               TicketCode=a.TicketCode,
               TicketName=a.TicketName
            }).Skip((page - 1) * pageSize).Take(pageSize);

            return queryable.ToList();
        }

        public DictionaryTicketType GetDictionaryTicketTypeById(int txtTicketTypeId)
        {
            IQueryable<DictionaryTicketType> queryable = _dbContext.DictionaryTicketType;
            queryable = queryable.Where(a => a.TicketTypeId.Equals(txtTicketTypeId));
            queryable = queryable.Select(a => new DictionaryTicketType()
           
            {
                TicketTypeId = a.TicketTypeId,
                TicketCode = a.TicketCode,
                TicketName = a.TicketName
            });
          
            return queryable.FirstOrDefault();
        }
        public int UpdateTicketType(DictionaryTicketType dictType)
        {
        
             _dbContext.DictionaryTicketType.Update(dictType);
            return _dbContext.SaveChanges();
        }

        public void UpdateTicketTypeId(DictionaryTicketType dictType)
        {
            _dbContext.Update(dictType);
            _dbContext.SaveChanges();
        }
        public int AddTicketType(DictionaryTicketType dictType)
        {
            _dbContext.DictionaryTicketType.Add(dictType);
            return _dbContext.SaveChanges();
        }
        public void TicketType_DestroyId(int id)
        {
            _dbContext.Remove(_dbContext.DictionaryTicketType.Single(a => a.TicketTypeId == id));
            _dbContext.SaveChanges();
        }
        #endregion

        #region dictionarycurrency
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

        public List<DictionaryCurrency> GetDictionaryCurrencyFilterPage(int page, int pageSize, string txtboxCurrencyName)
        {
            IQueryable<DictionaryCurrency> queryable = _dbContext.DictionaryCurrency;
            if (!string.IsNullOrWhiteSpace(txtboxCurrencyName))
            {
                queryable = queryable.Where(a => a.CurrencyName.Contains(txtboxCurrencyName));
            }
            queryable = queryable.Select(a => new DictionaryCurrency()
            {
                CurrencyId = a.CurrencyId,
                CurrencyCode = a.CurrencyCode,
                CurrencyName = a.CurrencyName
            }).Skip((page - 1) * pageSize).Take(pageSize);

            return queryable.ToList();
        }

        public int AdaugaValuta(DictionaryCurrency ValutaAdaugata)
        {
            _dbContext.DictionaryCurrency.Add(ValutaAdaugata);
            return _dbContext.SaveChanges();
        }

        public string Currency_DestroyId(int id)
        {
            try
            {
                _dbContext.Remove(_dbContext.DictionaryCurrency.Single(a => a.CurrencyId == id));
                _dbContext.SaveChanges();
                return null;
            }
            catch (Exception ex)
            {
                return "Aceasta atractie nu poate fi stearsa.";
            }
        }

        #endregion

        #region LandmarkType

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

        public List<DictionaryLandmarkType> GetDictionaryLandmarkTypeFilterPage(int page, int pageSize, string txtboxItemName)
        {
            IQueryable<DictionaryLandmarkType> queryable = _dbContext.DictionaryLandmarkType;
            if (!string.IsNullOrWhiteSpace(txtboxItemName))
            {
                queryable = queryable.Where(a => a.ItemName.Contains(txtboxItemName));
            }
            queryable = queryable.Select(a => new DictionaryLandmarkType()
            {
                ItemId = a.ItemId,
                ItemCode = a.ItemCode,
                ItemName = a.ItemName,
                Description = a.Description
            }).Skip((page - 1) * pageSize).Take(pageSize);

            return queryable.ToList();
        }



        public void LandmarkType_DestroyId(int id)
        {
            _dbContext.Remove(_dbContext.DictionaryLandmarkType.Single(a => a.ItemId == id));
            _dbContext.SaveChanges();
        }

    public int GetDictionaryLandmarkTypeCount()
        {
            return _dbContext.DictionaryLandmarkType.Count();
        }

         public int AddNewLandmarkDetails(DictionaryLandmarkType dictionaryLandmarkType)
        {
            _dbContext.DictionaryLandmarkType.Add(dictionaryLandmarkType);
            return _dbContext.SaveChanges();
        }
        #endregion

        #region dictionaryavailability

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
        public List<DictionaryAvailability> GetDictionaryAvailability()
        {
            List<DictionaryAvailability> dictionaryAvailability = _dbContext.DictionaryAvailability.Select(a => new DictionaryAvailability()
            {
                AvailabilityId = a.AvailabilityId,
                AvailabilityName = a.AvailabilityName,
                Schedule = a.Schedule
            }).ToList();

            return dictionaryAvailability;

        }
        public int GetDictionaryAvailabilityCount()
        {
            return _dbContext.DictionaryAvailability.Count();
        }

        public string Availability_DestroyId(int id)
        {
            try
            {
                _dbContext.Remove(_dbContext.DictionaryAvailability.Single(a => a.AvailabilityId == id));
                _dbContext.SaveChanges();
                return null;
            }
            catch (Exception ex)
            {
                return "Aceast program nu poate fi sters.";
            }
        }

        #endregion

        #region dictionaryattractiontype
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
                AttractionName = a.AttractionName
            }).Skip((page - 1) * pageSize).Take(pageSize);

            return queryable.ToList();
        }
        public List<DictionaryAttractionType> GetDictionaryAttractionType()
        {
            List<DictionaryAttractionType> dictionaryAttractionType = _dbContext.DictionaryAttractionType.Select(a => new DictionaryAttractionType()
            {
                AttractionTypeId = a.AttractionTypeId,
                AttractionCode = a.AttractionCode,
                AttractionName = a.AttractionName
            }).ToList();

            return dictionaryAttractionType;
        }
        public int GetDictionaryAttractionTypeCount()
        {
            return _dbContext.DictionaryAttractionType.Count();
        }

        public int AddNewAttractionType(DictionaryAttractionType dictionaryAttractionType)
        {
            _dbContext.DictionaryAttractionType.Add(dictionaryAttractionType);
            return _dbContext.SaveChanges();
        }

        public string AttractionType_DestroyId(int id)
        {
            try
            {
                _dbContext.Remove(_dbContext.DictionaryAttractionType.Single(a => a.AttractionTypeId == id));
                _dbContext.SaveChanges();
                return null;
            }
            catch (Exception ex)
            {
                return "Aceasta atractie nu poate fi stearsa.";
            }
        }

        public string LandmarkType_UpdateId(DictionaryLandmarkType dictionaryLandmarkType)
        {
            try
            {
                var target = (_dbContext.DictionaryLandmarkType.Single(a => a.ItemId == dictionaryLandmarkType.ItemId));

                target.ItemCode = dictionaryLandmarkType.ItemCode;
                target.ItemName = dictionaryLandmarkType.ItemName;
                target.Description = dictionaryLandmarkType.Description;

                _dbContext.Attach(target);
                _dbContext.Entry(target).State = EntityState.Modified;
                _dbContext.SaveChanges();

                return null;
            }
            catch (Exception ex)
            {
                return "Acest Landmark nu poate fi modificat.";
            }
        }
        #endregion
    }
}

