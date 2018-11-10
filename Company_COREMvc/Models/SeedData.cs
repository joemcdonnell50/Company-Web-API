using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_COREMvc.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new Company_COREMvcContext(
                serviceProvider.GetRequiredService<DbContextOptions<Company_COREMvcContext>>()))
            {
                // Look for any companies. 
                if (context.Company.Any())
                {
                    return;   // DB has been seeded
                }

                //If db has no entries seed db with foloowing data. 
                context.Company.AddRange(
                     new Company
                     {
                         Name = "Apple Inc.",
                         Exchange = "NASDAQ",
                         Ticker = "AAPL",
                         ISIN = "US0378331005",
                         Website = "http://www.apple.com"
                     },

                     new Company
                     {
                         Name = "British Airways Plc",
                         Exchange = "Pink Sheets",
                         Ticker = "BAIRY",
                         ISIN = "US1104193065",
                         Website = ""
                     },

                     new Company
                     {
                         Name = "Heineken NV",
                         Exchange = "Euronext Amsterdam",
                         Ticker = "HEIA",
                         ISIN = "NL0000009165",
                         Website = ""
                     },

                     new Company
                     {
                         Name = "Panasonic Corp",
                         Exchange = "Tokyo Stock Exchange",
                         Ticker = "6752",
                         ISIN = "JP3866800000",
                         Website = "http://www.panasonic.co.jp"
                     },

                     new Company
                     {
                         Name = "Porsche Automobil",
                         Exchange = "Deutsche Börse",
                         Ticker = "PAH3",
                         ISIN = "DE000PAH0038",
                         Website = "https://www.porsche.com/"
                     }
                );
                context.SaveChanges();
            }
        }
    }
}
