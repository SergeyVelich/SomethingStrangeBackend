using Lingva.DAL.EF.Context;
using Lingva.DAL.Mongo;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Lingva.WebAPI
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(IConfiguration config)
        {
            DbProviders dbProvider = (DbProviders)Enum.Parse(typeof(DbProviders), config.GetSection("Selectors:DbProvider").Value, true);

            switch (dbProvider)
            {
                case DbProviders.Mongo:
                    await new MongoContext(config).InitializeAsync();
                    break;
                default:
                    DictionaryContextFactory factory = new DictionaryContextFactory();
                    DictionaryContext dbContext = factory.CreateDbContext(config);
                    await dbContext.InitializeAsync();
                    break;
            }
        }
    }
}
