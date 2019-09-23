using iWasHere.Domain.DTOs;
using iWasHere.Domain.Models;
using Microsoft.AspNetCore.Mvc;
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
                CountryName = a.CountryName,
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

        public void Country_DestroyId(int id)
        {
            _dbContext.Remove(_dbContext.DictionaryCountry.Single(a => a.CountryId == id));
            _dbContext.SaveChanges();
        }

        public int AddNewCountry(DictionaryCountry dictionaryCountry)
        {
            _dbContext.DictionaryCountry.Add(dictionaryCountry);
            return _dbContext.SaveChanges();
        }

        public DictionaryCountry GetDictionaryCountryById(int txtCountryId)
        {
            IQueryable<DictionaryCountry> queryable = _dbContext.DictionaryCountry;
            queryable = queryable.Where(a => a.CountryId.Equals(txtCountryId));
            queryable = queryable.Select(a => new DictionaryCountry()

            {
                CountryId = a.CountryId,
                CountryName = a.CountryName,
            });

            return queryable.FirstOrDefault();
        }



        public int UpdateCountry(DictionaryCountry dictionaryCountry)
        {

            _dbContext.DictionaryCountry.Update(dictionaryCountry);
            return _dbContext.SaveChanges();
        }

        public void UpdateCountryId(DictionaryCountry dictionaryCountry)
        {
            _dbContext.DictionaryCountry.Update(dictionaryCountry);
            _dbContext.SaveChanges();
        }


        public string Country_QuickUpdateId(DictionaryCountry dictionaryCountry)
        {
            try
            {

                var target = (_dbContext.DictionaryCountry.Single(a => a.CountryId == dictionaryCountry.CountryId));


                target.CountryName = dictionaryCountry.CountryName;

                _dbContext.Attach(target);
                _dbContext.Entry(target).State = EntityState.Modified;
                _dbContext.SaveChanges();

                return null;
            }
            catch (Exception ex)
            {
                return "Tara nu poate fi modificata.";
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

        public DictionaryCity GetDictionaryCityById(int txtCityId)
        {
            IQueryable<DictionaryCity> queryable = _dbContext.DictionaryCity;
            queryable = queryable.Where(a => a.CityId.Equals(txtCityId));
            queryable = queryable.Select(a => new DictionaryCity()

            {
                CityId = a.CityId,
                CityName = a.CityName,
                CountyId = a.CountyId
            });

            return queryable.FirstOrDefault();
        }
        public void UpdateCity(DictionaryCity modelCity)
        {
            _dbContext.Update(modelCity);
            _dbContext.SaveChanges();
        }

        public int AddCity(DictionaryCity modelCity)
        {
            _dbContext.DictionaryCity.Add(modelCity);
            return _dbContext.SaveChanges();
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

        public int AddCounty(DictionaryCounty modelCounty)
        {
            _dbContext.DictionaryCounty.Add(modelCounty);
            return _dbContext.SaveChanges();
        }

        public DictionaryCounty GetDictionaryCountyById(int txtCountyId)
        {
            IQueryable<DictionaryCounty> queryable = _dbContext.DictionaryCounty;
            queryable = queryable.Where(a => a.CountyId.Equals(txtCountyId));
            queryable = queryable.Select(a => new DictionaryCounty()

            {
                CountyId = a.CountyId,
                CountyName = a.CountyName,
                CountryId = a.CountryId
            });

            return queryable.FirstOrDefault();
        }

        public void UpdateCounty(DictionaryCounty modelCounty)
        {
            _dbContext.Update(modelCounty);
            _dbContext.SaveChanges();
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

                //FK-urile catre tabele
                DictionaryAvailability = a.DictionaryAvailability,
                DictionaryItem = a.DictionaryItem,
                DictionaryAttractionType = a.DictionaryAttractionType,
                DictionaryCity = a.DictionaryCity

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
        public List<DictionaryTicketType> GetCmbTicketType()
        {
            IQueryable<DictionaryTicketType> queryable = _dbContext.DictionaryTicketType;
            queryable = queryable.Select(a => new DictionaryTicketType()
            {
                TicketTypeId = a.TicketTypeId,
                TicketName = a.TicketName
            });

            return queryable.ToList();
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

               public DictionaryLandmarkType GetDictionaryLandmarkTypeById(int txtLandmarkTypeId)
        {
            IQueryable<DictionaryLandmarkType> queryable = _dbContext.DictionaryLandmarkType;
            queryable = queryable.Where(a => a.ItemId.Equals(txtLandmarkTypeId));
            queryable = queryable.Select(a => new DictionaryLandmarkType()
           
            {
                ItemId = a.ItemId,
                ItemCode = a.ItemCode,
                ItemName = a.ItemName,
                Description = a.Description
            });
          
            return queryable.FirstOrDefault();
        }



        public int UpdateLandmarkType(DictionaryLandmarkType lmkType)
        {

            _dbContext.DictionaryLandmarkType.Update(lmkType);
            return _dbContext.SaveChanges();
        }

        public void UpdateLandmarkTypeId(DictionaryLandmarkType lmkType)
        {
            _dbContext.DictionaryLandmarkType.Update(lmkType);
            _dbContext.SaveChanges();
        }


        public string LandmarkType_QuickUpdateId(DictionaryLandmarkType dictionaryLandmarkType)
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

        #region dictionaryavailability

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
                Schedule = a.Schedule,
            }).Skip((page - 1) * pageSize).Take(pageSize);

            return queryable.ToList();
        }



        public void Availability_DestroyId(int id)
        {
            _dbContext.Remove(_dbContext.DictionaryAvailability.Single(a => a.AvailabilityId == id));
            _dbContext.SaveChanges();
        }

        public int GetDictionaryAvailabilityCount()
        {
            return _dbContext.DictionaryAvailability.Count();
        }

        public int AddNewAvailability(DictionaryAvailability dictionaryAvailability)
        {
            _dbContext.DictionaryAvailability.Add(dictionaryAvailability);
            return _dbContext.SaveChanges();
        }

        public DictionaryAvailability GetDictionaryAvailabilityById(int txtAvailabilityId)
        {
            IQueryable<DictionaryAvailability> queryable = _dbContext.DictionaryAvailability;
            queryable = queryable.Where(a => a.AvailabilityId.Equals(txtAvailabilityId));
            queryable = queryable.Select(a => new DictionaryAvailability()

            {
                AvailabilityId = a.AvailabilityId,
                AvailabilityName = a.AvailabilityName,
                Schedule = a.Schedule,
            });

            return queryable.FirstOrDefault();
        }



        public int UpdateAvailability(DictionaryAvailability dictionaryAvailability)
        {

            _dbContext.DictionaryAvailability.Update(dictionaryAvailability);
            return _dbContext.SaveChanges();
        }

        public void UpdateAvailabilityId(DictionaryAvailability dictionaryAvailability)
        {
            _dbContext.DictionaryAvailability.Update(dictionaryAvailability);
            _dbContext.SaveChanges();
        }


        public string Availability_QuickUpdateId(DictionaryAvailability dictionaryAvailability)
        {
            try
            {

                var target = (_dbContext.DictionaryAvailability.Single(a => a.AvailabilityId == dictionaryAvailability.AvailabilityId));

                target.AvailabilityName = dictionaryAvailability.AvailabilityName;
                target.Schedule = dictionaryAvailability.Schedule;

                _dbContext.Attach(target);
                _dbContext.Entry(target).State = EntityState.Modified;
                _dbContext.SaveChanges();

                return null;
            }
            catch (Exception ex)
            {
                return "Programul nu poate fi modificat.";
            }
        }

        #endregion

        #region dictionaryattractiontype
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



        public void AttractionType_DestroyId(int id)
        {
            _dbContext.Remove(_dbContext.DictionaryAttractionType.Single(a => a.AttractionTypeId == id));
            _dbContext.SaveChanges();
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

        public DictionaryAttractionType GetDictionaryAttractionTypeById(int txtAttractionTypeId)
        {
            IQueryable<DictionaryAttractionType> queryable = _dbContext.DictionaryAttractionType;
            queryable = queryable.Where(a => a.AttractionTypeId.Equals(txtAttractionTypeId));
            queryable = queryable.Select(a => new DictionaryAttractionType()

            {
                AttractionTypeId = a.AttractionTypeId,
                AttractionCode = a.AttractionCode,
                AttractionName = a.AttractionName,
            });

            return queryable.FirstOrDefault();
        }



        public int UpdateAttractionType(DictionaryAttractionType dictionaryAttractionType)
        {

            _dbContext.DictionaryAttractionType.Update(dictionaryAttractionType);
            return _dbContext.SaveChanges();
        }

        public void UpdateAttractionTypeId(DictionaryAttractionType dictionaryAttractionType)
        {
            _dbContext.DictionaryAttractionType.Update(dictionaryAttractionType);
            _dbContext.SaveChanges();
        }


        public string AttractionType_QuickUpdateId(DictionaryAttractionType dictionaryAttractionType)
        {
            try
            {

                var target = (_dbContext.DictionaryAttractionType.Single(a => a.AttractionTypeId == dictionaryAttractionType.AttractionTypeId));

                target.AttractionCode = dictionaryAttractionType.AttractionCode;
                target.AttractionName = dictionaryAttractionType.AttractionName;

                _dbContext.Attach(target);
                _dbContext.Entry(target).State = EntityState.Modified;
                _dbContext.SaveChanges();

                return null;
            }
            catch (Exception ex)
            {
                return "Tipul de atractie nu poate fi modificat.";
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

        #region landmark
        public List<DictionaryCity> GetGmbCity()
        {
            IQueryable<DictionaryCity> queryable = _dbContext.DictionaryCity;
            queryable = queryable.Select(a => new DictionaryCity()
            {
                CityId = a.CityId,
                CityName = a.CityName
            });
            return queryable.ToList();
        }

        public List<DictionaryCurrency> GetCmbCurrency()
        {
            IQueryable<DictionaryCurrency> queryable = _dbContext.DictionaryCurrency;
            queryable = queryable.Select(a => new DictionaryCurrency()
            {
               CurrencyId=a.CurrencyId,
               CurrencyName=a.CurrencyName
            });
            return queryable.ToList();
        }
        public List<DictionaryLandmarkType> GetCmbLandmarkDetail()
        {
            IQueryable<DictionaryLandmarkType> queryable = _dbContext.DictionaryLandmarkType;
            queryable = queryable.Select(a => new DictionaryLandmarkType()
            {
              ItemId=a.ItemId,
              ItemName=a.ItemName
            });
            return queryable.ToList();
        }
        public List<DictionaryAttractionType> GetCmbAttraction()
        {
            IQueryable<DictionaryAttractionType> queryable = _dbContext.DictionaryAttractionType;
            queryable = queryable.Select(a => new DictionaryAttractionType()
            {
                AttractionTypeId=a.AttractionTypeId,
                AttractionName=a.AttractionName
            });
            return queryable.ToList();
        }
        public List<DictionaryAvailability> GeCmbAvailability()
        {
            IQueryable<DictionaryAvailability> queryable = _dbContext.DictionaryAvailability;
            queryable = queryable.Select(a => new DictionaryAvailability()
            {
               AvailabilityId=a.AvailabilityId,
               AvailabilityName=a.AvailabilityName
            });
            return queryable.ToList();
        }
        public int AddTicket(Ticket ticket)
        {
            _dbContext.Ticket.Add(ticket);
            return _dbContext.SaveChanges();
        }
        public int AddLandmark(Landmark landmark)
        {
            _dbContext.Landmark.Add(landmark);
            return _dbContext.SaveChanges();
        }
        public List<DictionaryCity> Cascading_Get_City(int? county)
        {

            IQueryable<DictionaryCity> queryable = _dbContext.DictionaryCity;
            if (county != null)
            {
                queryable = queryable.Where(a => a.CountyId == county);
            }
            queryable = queryable.Select(a => new DictionaryCity()
            {
                CityId = a.CityId,
                CityName = a.CityName
            });
          
            return queryable.ToList();
        }
        public List<DictionaryCounty> Cascading_Get_County(int? country)
        {

            IQueryable<DictionaryCounty> queryable = _dbContext.DictionaryCounty;
            if (country != null)
            {
                queryable = queryable.Where(a => a.CountryId == country);
            }
            queryable = queryable.Select(a => new DictionaryCounty()
            {
                CountyId = a.CountyId,
                CountyName = a.CountyName
            });

            return queryable.ToList();
        }
        #endregion

    }
}

