using iWasHere.Domain.DTOs;
using iWasHere.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;

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

        public void City_DestroyId(int id)
        {
            _dbContext.Remove(_dbContext.DictionaryCity.Single(a => a.CityId == id));
            _dbContext.SaveChanges();
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
                return ex.Message;
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

        
        #endregion

        #region ticket
        public List<Ticket> GetTicketPage(int page, int pageSize)
        {
            IQueryable<Ticket> queryable = _dbContext.Ticket.Include(a=>a.DictionaryCurrency).Include(a => a.TicketType);

            queryable = queryable.Select(a => new Ticket()
            {
                TicketId = a.TicketId,
                TicketPrice = a.TicketPrice,
                TicketTypeId = a.TicketTypeId,

                DictionaryCurrency = a.DictionaryCurrency,
                TicketType = a.TicketType
            }).Skip((page - 1) * pageSize).Take(pageSize);

            return queryable.ToList();
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
        public string TicketType_DestroyId(int id)
        {
            try
            {
                _dbContext.Remove(_dbContext.DictionaryTicketType.Single(a => a.TicketTypeId == id));
                _dbContext.SaveChanges();
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
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
                CurrencyName = a.CurrencyName,
                ConversionRate = a.ConversionRate
            }).ToList();

            return dictionaryCurrency;
        }
        public int GetDictionaryCurrencyCount()
        {
            return _dbContext.DictionaryCurrency.Count();
        }
        public List<DictionaryCurrency> GetDictionaryCurrencyPage(int page, int pageSize, string txtboxCurrencyName, string txtboxCurrencyCode)
        {
            IQueryable<DictionaryCurrency> queryable = _dbContext.DictionaryCurrency;

            if (!string.IsNullOrWhiteSpace(txtboxCurrencyName))
            {
                queryable = queryable.Where(a => a.CurrencyName.Contains(txtboxCurrencyName));
            }
            if (!string.IsNullOrWhiteSpace(txtboxCurrencyCode))
            {
                queryable = queryable.Where(a => a.CurrencyCode.Contains(txtboxCurrencyCode));
            }
            queryable = queryable.Select(a => new DictionaryCurrency()
            {
                CurrencyId = a.CurrencyId,
                CurrencyCode = a.CurrencyCode,
                CurrencyName = a.CurrencyName,
                ConversionRate = a.ConversionRate
            }).Skip((page - 1) * pageSize).Take(pageSize);

            return queryable.ToList();
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
                CurrencyName = a.CurrencyName,
                ConversionRate = a.ConversionRate
            }).Skip((page - 1) * pageSize).Take(pageSize);

            return queryable.ToList();
        }

        public int AddCurrency(DictionaryCurrency modelCurrency)
        {
            _dbContext.DictionaryCurrency.Add(modelCurrency);
            return _dbContext.SaveChanges();
        }

        public int AdaugaValuta(DictionaryCurrency ValutaAdaugata)
        {
            _dbContext.DictionaryCurrency.Add(ValutaAdaugata);
            return _dbContext.SaveChanges();
        }
        public int AddNewCurrency(DictionaryCurrency dictionaryCurrency)
        {
            _dbContext.DictionaryCurrency.Add(dictionaryCurrency);
            return _dbContext.SaveChanges();
        }

        public void Currency_DestroyId(int id)
        {
            _dbContext.Remove(_dbContext.DictionaryCurrency.Single(a => a.CurrencyId == id));
            _dbContext.SaveChanges();
        }
            public DictionaryCurrency GetDictionaryCurrencyById(int txtCurrencyId)
            {
                IQueryable<DictionaryCurrency> queryable = _dbContext.DictionaryCurrency;
                queryable = queryable.Where(a => a.CurrencyId.Equals(txtCurrencyId));
                queryable = queryable.Select(a => new DictionaryCurrency()

                {
                    CurrencyId = a.CurrencyId,
                    CurrencyCode = a.CurrencyCode,
                    CurrencyName = a.CurrencyName,
                    ConversionRate = a.ConversionRate,
                });

                return queryable.FirstOrDefault();
            }



            public void UpdateCurrency(DictionaryCurrency modelCurrency)
        {
            _dbContext.Update(modelCurrency);
            _dbContext.SaveChanges();
        }

            public void UpdateCurrencyId(DictionaryCurrency dictionaryCurrency)
            {
                _dbContext.DictionaryCurrency.Update(dictionaryCurrency);
                _dbContext.SaveChanges();
            }


            public string Currency_QuickUpdateCode(DictionaryCurrency dictionaryCurrency)
            {
            try
            {

                var target = (_dbContext.DictionaryCurrency.Single(a => a.CurrencyCode == dictionaryCurrency.CurrencyCode));
                if (target != null)
                {
                    target.CurrencyName = dictionaryCurrency.CurrencyName;
                    //      target.CurrencyCode = dictionaryCurrency.CurrencyCode;
                    target.ConversionRate = dictionaryCurrency.ConversionRate;
                }
                else _dbContext.DictionaryCurrency.Add(dictionaryCurrency);
                  //  _dbContext.Attach(target);
                   // _dbContext.Entry(target).State = EntityState.Modified;
                    _dbContext.SaveChanges();

                    return null;
                }
                catch (Exception ex)
                {
                    return "Programul nu poate fi modificat.";
                }
            }
        public string Currency_QuickUpdateId(DictionaryCurrency dictionaryCurrency)
        {
            try
            {

                var target = (_dbContext.DictionaryCurrency.Single(a => a.CurrencyId == dictionaryCurrency.CurrencyId));

                target.CurrencyName = dictionaryCurrency.CurrencyName;
                target.CurrencyCode = dictionaryCurrency.CurrencyCode;
                target.ConversionRate = dictionaryCurrency.ConversionRate;

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



        public string LandmarkType_DestroyId(int id)
        {
            try
            {
                _dbContext.Remove(_dbContext.DictionaryLandmarkType.Single(a => a.ItemId == id));
                _dbContext.SaveChanges();
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
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
        #endregion

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
        public LandmarkReadOnlyModel GetLandmarkById(int landmarkId)
        {
            IQueryable<Landmark> queryable = _dbContext.Landmark;
            IQueryable<LandmarkRating> landmarkRating = _dbContext.LandmarkRating;
            if (landmarkId>0)
            {
                queryable = queryable.Where(a => a.LandmarkId.Equals(landmarkId));
                landmarkRating = landmarkRating.Where(a => a.LandmarkId.Equals(landmarkId));
            }
            _dbContext.Landmark.Include(c => c.DictionaryCity)
                                                                        .ThenInclude(county => county.County)
                                                                            .ThenInclude(country => country.Country)
            .Include(d => d.DictionaryAttractionType)
            .Include(e => e.DictionaryAvailability)
            .Include(f => f.DictionaryItem)
            .Include(g => g.Ticket)
                .ThenInclude(currency => currency.DictionaryCurrency)
            .Include(g => g.Ticket)
                .ThenInclude(ttype => ttype.TicketType)
            .ToList();

            Landmark landmarkResult  = queryable.FirstOrDefault();
            LandmarkRating landmarkRatingResult = landmarkRating.FirstOrDefault();

            LandmarkReadOnlyModel landmarkReadOnlyModel = new LandmarkReadOnlyModel();
            landmarkReadOnlyModel = landmarkReadOnlyModel.ConvertToModel(landmarkResult, landmarkRatingResult);
            landmarkReadOnlyModel.Username = landmarkRating.Select(a => a.User.UserName).ToString();
            return landmarkReadOnlyModel;
        }
        public int GetLandmarkCount()
        {
            return _dbContext.Landmark.Count();
        }

        public List<Landmark> GetLandmarkPage(int page, int pageSize, string txtboxLendmarkName)
        {
            IQueryable<Landmark> queryable = _dbContext.Landmark.Include(c => c.DictionaryCity).Include(d => d.DictionaryAttractionType).Include(e => e.DictionaryAvailability).Include(f => f.DictionaryItem).Include(g => g.Ticket);

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
                Ticket = a.Ticket,
                DictionaryAttractionType = a.DictionaryAttractionType,
                DictionaryCity = a.DictionaryCity

            }).Skip((page - 1) * pageSize).Take(pageSize);

            return queryable.ToList();
        }

        public List<Landmark> GetLandmark()
        {
            List<Landmark> dictionaryLandmark = _dbContext.Landmark.Include(c => c.DictionaryCity)
                                                                        .ThenInclude(county => county.County)
                                                                            .ThenInclude(country => country.Country)
            .Include(d => d.DictionaryAttractionType)
            .Include(e => e.DictionaryAvailability)
            .Include(f => f.DictionaryItem)
            .Include(g => g.Ticket)
                .ThenInclude(currency => currency.DictionaryCurrency)
            .Include(g => g.Ticket)
                .ThenInclude(ttype => ttype.TicketType)
            .Select(a => new Landmark()
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
                Ticket = a.Ticket,
                DictionaryAttractionType = a.DictionaryAttractionType,
                DictionaryCity = a.DictionaryCity,


            }).ToList();

            return dictionaryLandmark;
        }
        public List<Landmark> GetLandmarkReadOnly()
        {
            IQueryable<Landmark> queryable = _dbContext.Landmark;
            queryable.Include(c => c.DictionaryCity)
                                                                        .ThenInclude(county => county.County)
                                                                            .ThenInclude(country => country.Country)
            .Include(d => d.DictionaryAttractionType)
            .Include(e => e.DictionaryAvailability)
            .Include(f => f.DictionaryItem)
            .Include(g => g.Ticket)
                .ThenInclude(currency => currency.DictionaryCurrency)
            .Include(g => g.Ticket)
                .ThenInclude(ttype => ttype.TicketType)
            .Include(h=>h.LandmarkRating)
                .ThenInclude(user=>user.User)
            .ToList();
            return queryable.ToList();
        }

        public int AddComment(LandmarkRating rating)
        {
            _dbContext.LandmarkRating.Add(rating);
            return _dbContext.SaveChanges();
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
        public Landmark GetLandmarkByIdEdit(int txtLandmarkId)
        {
            IQueryable<Landmark> queryable = _dbContext.Landmark;
            queryable = queryable.Where(a => a.LandmarkId.Equals(txtLandmarkId));
            queryable = queryable.Select(a => new Landmark()

            {
               LandmarkId=a.LandmarkId,
               LandmarkName=a.LandmarkName,
               LandmarkShortDescription=a.LandmarkShortDescription,
               TicketId=a.TicketId,
               DictionaryAvailabilityId=a.DictionaryAvailabilityId,
               DictionaryItemId=a.DictionaryItemId,
               DictionaryAttractionTypeId=a.DictionaryAttractionTypeId,
               Longitude=a.Longitude,
               Latitude=a.Latitude,
               DictionaryCityId=a.DictionaryCityId,
               DateAdded=a.DateAdded
              
            });
           
            return queryable.FirstOrDefault();
        }
        public DictionaryCity GetDictionaryCityCountyById(int txtCityId)
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
      
        public DictionaryCounty GetDictionaryCountyCountryById(int txtCountId)
        {
            IQueryable<DictionaryCounty> queryable = _dbContext.DictionaryCounty;
            queryable = queryable.Where(a => a.CountyId.Equals(txtCountId));
            queryable = queryable.Select(a => new DictionaryCounty()
            { 
                CountyId = a.CountyId,
                CountyName=a.CountyName,
                CountryId=a.CountryId
            });

            return queryable.FirstOrDefault();
        }
        public Ticket GetTicketTypeCurrency(int ticketId)
        {
            IQueryable<Ticket> queryable = _dbContext.Ticket;
            queryable = queryable.Where(a => a.TicketId.Equals(ticketId));
            queryable = queryable.Select(a => new Ticket()

            {
                TicketPrice = a.TicketPrice,
                CurrencyId = a.CurrencyId,
                TicketTypeId = a.TicketTypeId
            });

            return queryable.FirstOrDefault();
        }
        public int UpdateLandmark(Landmark landmark)
        {

            _dbContext.Landmark.Update(landmark);
            return _dbContext.SaveChanges();
        }
        public int UpdateLandmark2(Landmark landmark)
        {

            _dbContext.Landmark.Update(landmark);
            return _dbContext.SaveChanges();
        }
        public int UpdateTicket(Ticket ticket)
        {

            _dbContext.Ticket.Update(ticket);
            return _dbContext.SaveChanges();
        }
        public int AddTicketLandmark(Ticket ticket)
        {
            _dbContext.Ticket.Add(ticket);
            return _dbContext.SaveChanges();
        }



        public void SaveUploadedImagesTemporaryDB(LandmarkImage img)
        {


            int maxID1 = Convert.ToInt32(_dbContext.Landmark.Max(p => p.LandmarkId));
            int nr2 = 1;

            img.LandmarkId = Sum(maxID1, nr2);

            _dbContext.LandmarkImage.Add(img);
            _dbContext.SaveChanges();


        }

        public void SaveUploadedImagesDB(LandmarkImage img)
        {

            int maxID2 = _dbContext.Landmark.Max(p => p.LandmarkId);

            var target = (_dbContext.LandmarkImage.Single(a => a.ImageURL == img.ImageURL));

            //img.LandmarkId = maxID2;

            target.LandmarkId = maxID2;

            _dbContext.Attach(target);
            _dbContext.Entry(target).State = EntityState.Modified;
            _dbContext.SaveChanges();

        }

        public int Sum(int num1, int num2)
        {
            int total;
            total = num1 + num2;
            return total;
        }



        public Stream ExportToWord(LandmarkReadOnlyModel model)
        {
            var stream = new MemoryStream();
            using (WordprocessingDocument doc = WordprocessingDocument.Create(stream, DocumentFormat.OpenXml.WordprocessingDocumentType.Document, true))
            {
                MainDocumentPart mainPart = doc.AddMainDocumentPart();
                //model.DateAdded = _dbContext.Landmark.Where(x=>x.LandmarkId==model.LandmarkId).Select(x=>x.DateAdded).FirstOrDefault();
                new Document(new Body()).Save(mainPart);

                Body body = mainPart.Document.Body;
                body.Append(new Paragraph(
                            new Run(
                                new Text("Numele obiectivului: " + model.LandmarkName))),
                                new Paragraph(
                            new Run(
                                new Text("Descrierea obiectivului: " + model.LandmarkShortDescription))),
                                new Paragraph(
                            new Run(
                                new Text("Orasul: " + model.CityName))),
                                new Paragraph(
                            new Run(
                                new Text("Judetul: " + model.CountyName))),
                                new Paragraph(
                            new Run(
                                new Text("Tara: " + model.CountryName))),
                                new Paragraph(
                            new Run(
                                new Text("Rating: " + model.RatingValue))),
                                new Paragraph(
                            new Run(
                                new Text("Comentarii: " + model.Comment)))
                                );

                mainPart.Document.Save();

                //if you don't use the using you should close the WordprocessingDocument here
                //doc.Close();
            }
            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }



        //public int AddTicket(Ticket ticket)
        //{
        //    _dbContext.Ticket.Add(ticket);
        //    return _dbContext.SaveChanges();
        //}


        //[HttpPost]
        //public ActionResult AddNewTicketType(string code, string name)
        //{
        //    DictionaryTicketType dictionaryTicketType = new DictionaryTicketType
        //    {
        //        TicketName = name,
        //        TicketCode = code
        //    };
        //    _dictionaryService.AddTicketType(dictionaryTicketType);

        //    return Json(ModelState.ToDataSourceResult());
        //}



        #endregion

    }
}

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       