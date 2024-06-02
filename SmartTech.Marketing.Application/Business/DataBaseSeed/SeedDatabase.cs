using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Geometries;
using SmartTech.Marketing.Application.Contract;
using SmartTech.Marketing.Domain.Entities;

namespace SmartTech.Marketing.Application.Business.DataBaseSeed
{
    public class SeedDatabaseHandler : IRequestHandler<SeedDatabaseHandlerInput, SeedDatabaseHandlerOutput>
    {
        private readonly IDataBaseService _databaseService;
        private readonly ILogger<SeedDatabaseHandler> _logger;
        public SeedDatabaseHandler(ILogger<SeedDatabaseHandler> logger, IDataBaseService databaseService)
        {
            _logger = logger;
            _databaseService = databaseService;
        }
        public async Task<SeedDatabaseHandlerOutput> Handle(SeedDatabaseHandlerInput request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling SeedDatabase business logic");
            SeedDatabaseHandlerOutput output = new SeedDatabaseHandlerOutput(request.CorrelationId());
            #region Country Seed seed
            _databaseService.Country.ExecuteDeleteAsync();

            List<Country> countries = new List<Country>
            {
                   //Main countries
                    new Country { Id = 1, CountryName = "United States", CountryPrefix = "US", CountryGeometry = null, Geom = new Point(-98.5795, 39.8283) { SRID = 3857 }, CountyGeometry = new Point(-98.5795, 39.8283) { SRID = 3857 } },
                    new Country { Id = 2, CountryName = "Canada", CountryPrefix = "CA", CountryGeometry = null, Geom = new Point(-106.3468, 56.1304) { SRID = 3857 }, CountyGeometry = new Point(-106.3468, 56.1304) { SRID = 3857 } },
                    new Country { Id = 3, CountryName = "United Kingdom", CountryPrefix = "GB", CountryGeometry = null, Geom = new Point(-3.4360, 55.3781) { SRID = 3857 }, CountyGeometry = new Point(-3.4360, 55.3781) { SRID = 3857 } },
                    new Country { Id = 4, CountryName = "Germany", CountryPrefix = "DE", CountryGeometry = null, Geom = new Point(10.4515, 51.1657) { SRID = 3857 }, CountyGeometry = new Point(10.4515, 51.1657) { SRID = 3857 } },
                    new Country { Id = 5, CountryName = "France", CountryPrefix = "FR", CountryGeometry = null, Geom = new Point(2.2137, 46.6034) { SRID = 3857 }, CountyGeometry = new Point(2.2137, 46.6034) { SRID = 3857 } },
                    new Country { Id = 6, CountryName = "Australia", CountryPrefix = "AU", CountryGeometry = null, Geom = new Point(133.7751, -25.2744) { SRID = 3857 }, CountyGeometry = new Point(133.7751, -25.2744) { SRID = 3857 } },
                    new Country { Id = 7, CountryName = "Brazil", CountryPrefix = "BR", CountryGeometry = null, Geom = new Point(-51.9253, -14.2350) { SRID = 3857 }, CountyGeometry = new Point(-51.9253, -14.2350) { SRID = 3857 } },
                    new Country { Id = 8, CountryName = "India", CountryPrefix = "IN", CountryGeometry = null, Geom = new Point(78.9629, 20.5937) { SRID = 3857 }, CountyGeometry = new Point(78.9629, 20.5937) { SRID = 3857 } },
                    new Country { Id = 9, CountryName = "China", CountryPrefix = "CN", CountryGeometry = null, Geom = new Point(104.1954, 35.8617) { SRID = 3857 }, CountyGeometry = new Point(104.1954, 35.8617) { SRID = 3857 } },
                    new Country { Id = 10, CountryName = "Japan", CountryPrefix = "JP", CountryGeometry = null, Geom = new Point(138.2529, 36.2048) { SRID = 3857 }, CountyGeometry = new Point(138.2529, 36.2048) { SRID = 3857 } },

                    // African countries
                    new Country { Id = 11, CountryName = "Nigeria", CountryPrefix = "NG", CountryGeometry = null, Geom = new Point(8.6753, 9.0820) { SRID = 3857 }, CountyGeometry = new Point(8.6753, 9.0820) { SRID = 3857 } },
                    new Country { Id = 12, CountryName = "Egypt", CountryPrefix = "EG", CountryGeometry = null, Geom = new Point(30.8025, 26.8206) { SRID = 3857 }, CountyGeometry = new Point(30.8025, 26.8206) { SRID = 3857 } },
                    new Country { Id = 13, CountryName = "South Africa", CountryPrefix = "ZA", CountryGeometry = null, Geom = new Point(22.9375, -30.5595) { SRID = 3857 }, CountyGeometry = new Point(22.9375, -30.5595) { SRID = 3857 } },
                    new Country { Id = 14, CountryName = "Kenya", CountryPrefix = "KE", CountryGeometry = null, Geom = new Point(36.8219, -1.2921) { SRID = 3857 }, CountyGeometry = new Point(36.8219, -1.2921) { SRID = 3857 } },
                    new Country { Id = 15, CountryName = "Ghana", CountryPrefix = "GH", CountryGeometry = null, Geom = new Point(-1.0232, 7.9465) { SRID = 3857 }, CountyGeometry = new Point(-1.0232, 7.9465) { SRID = 3857 } },
                    new Country { Id = 16, CountryName = "Ethiopia", CountryPrefix = "ET", CountryGeometry = null, Geom = new Point(39.9551, 9.145) { SRID = 3857 }, CountyGeometry = new Point(39.9551, 9.145) { SRID = 3857 } },
                    new Country { Id = 17, CountryName = "Tanzania", CountryPrefix = "TZ", CountryGeometry = null, Geom = new Point(34.8888, -6.3690) { SRID = 3857 }, CountyGeometry = new Point(34.8888, -6.3690) { SRID = 3857 } },
                    new Country { Id = 18, CountryName = "Uganda", CountryPrefix = "UG", CountryGeometry = null, Geom = new Point(32.2903, 1.3733) { SRID = 3857 }, CountyGeometry = new Point(32.2903, 1.3733) { SRID = 3857 } },
                    new Country { Id = 19, CountryName = "Algeria", CountryPrefix = "DZ", CountryGeometry = null, Geom = new Point(1.6596, 28.0339) { SRID = 3857 }, CountyGeometry = new Point(1.6596, 28.0339) { SRID = 3857 } },
                    new Country { Id = 20, CountryName = "Morocco", CountryPrefix = "MA", CountryGeometry = null, Geom = new Point(-7.0926, 31.7917) { SRID = 3857 }, CountyGeometry = new Point(-7.0926, 31.7917) { SRID = 3857 } },

                     //Arabian countries
                    new Country { Id = 21, CountryName = "Saudi Arabia", CountryPrefix = "SA", CountryGeometry = null, Geom = new Point(45.0792, 23.8859) { SRID = 3857 }, CountyGeometry = new Point(45.0792, 23.8859) { SRID = 3857 } },
                    new Country { Id = 22, CountryName = "United Arab Emirates", CountryPrefix = "AE", CountryGeometry = null, Geom = new Point(53.8478, 23.4241) { SRID = 3857 }, CountyGeometry = new Point(53.8478, 23.4241) { SRID = 3857 } },
                    new Country { Id = 23, CountryName = "Qatar", CountryPrefix = "QA", CountryGeometry = null, Geom = new Point(51.1839, 25.3548) { SRID = 3857 }, CountyGeometry = new Point(51.1839, 25.3548) { SRID = 3857 } },
                    new Country { Id = 24, CountryName = "Kuwait", CountryPrefix = "KW", CountryGeometry = null, Geom = new Point(47.4818, 29.3759) { SRID = 3857 }, CountyGeometry = new Point(47.4818, 29.3759) { SRID = 3857 } },
                    new Country { Id = 25, CountryName = "Oman", CountryPrefix = "OM", CountryGeometry = null, Geom = new Point(57.5836, 21.5126) { SRID = 3857 }, CountyGeometry = new Point(57.5836, 21.5126) { SRID = 3857 } },
                    new Country { Id = 26, CountryName = "Bahrain", CountryPrefix = "BH", CountryGeometry = null, Geom = new Point(50.5555, 26.0275) { SRID = 3857 }, CountyGeometry = new Point(50.5555, 26.0275) { SRID = 3857 } },
                    new Country { Id = 27, CountryName = "Yemen", CountryPrefix = "YE", CountryGeometry = null, Geom = new Point(48.5164, 15.5527) { SRID = 3857 }, CountyGeometry = new Point(48.5164, 15.5527) { SRID = 3857 } },
                    new Country { Id = 28, CountryName = "Jordan", CountryPrefix = "JO", CountryGeometry = null, Geom = new Point(36.2384, 30.5852) { SRID = 3857 }, CountyGeometry = new Point(36.2384, 30.5852) { SRID = 3857 } },
                    new Country { Id = 29, CountryName = "Lebanon", CountryPrefix = "LB", CountryGeometry = null, Geom = new Point(35.8623, 33.8547) { SRID = 3857 }, CountyGeometry = new Point(35.8623, 33.8547) { SRID = 3857 } },
                    new Country { Id = 30, CountryName = "Syria", CountryPrefix = "SY", CountryGeometry = null, Geom = new Point(38.9968, 34.8021) { SRID = 3857 }, CountyGeometry = new Point(38.9968, 34.8021) { SRID = 3857 } },
                    new Country { Id = 31, CountryName = "Iraq", CountryPrefix = "IQ", CountryGeometry = null, Geom = new Point(43.6793, 33.2232) { SRID = 3857 }, CountyGeometry = new Point(43.6793, 33.2232) { SRID = 3857 } }





            };
            await _databaseService.Country.AddRangeAsync(countries);
            await _databaseService.DBSaveChangesAsync();
            #endregion
            #region Client Type seed
            _databaseService.ClientType.ExecuteDeleteAsync();

            List<ClientType> ClientTypes = new List<ClientType>
            {

                new ClientType{Id=1,Type="Military"},
                new ClientType{Id=2,Type="Military"}


            };
            await _databaseService.ClientType.AddRangeAsync(ClientTypes);
            await _databaseService.DBSaveChangesAsync(cancellationToken);
            #endregion
            #region Payment Type seed
            _databaseService.ContractPaymentType.ExecuteDeleteAsync();

            List<ContractPaymentType> CcntractPaymentType = new List<ContractPaymentType>
            {

                new ContractPaymentType{Id=1,Type="Cash"},
                new ContractPaymentType{Id=2,Type="Credit"}


            };
            await _databaseService.ContractPaymentType.AddRangeAsync(CcntractPaymentType);
            await _databaseService.DBSaveChangesAsync(cancellationToken);
            #endregion
            #region System Parameters Seed
            _databaseService.SysParam.ExecuteDeleteAsync();
            _databaseService.SysParam.Add(new SysParam { Id=1, ParamName= "IsAuthorizationRequired",ParamValue=true });
           await _databaseService.DBSaveChangesAsync(cancellationToken);
            #endregion
            return output;
        }
    }
}
