using Lingva.Additional.Mapping.DataAdapter;
using Lingva.BC;
using Lingva.BC.Contracts;
using Lingva.BC.Services;
using Lingva.DAL.CosmosSqlApi;
using Lingva.DAL.Dapper;
using Lingva.DAL.EF.Context;
using Lingva.DAL.EF.Repositories;
using Lingva.DAL.Mongo;
using Lingva.DAL.Repositories;
using Lingva.WebAPI.Infrastructure.Adapters;
using Lingva.WebAPI.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Security.Claims;

namespace Lingva.WebAPI.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServiceExtensions
    {
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration config)
        {
            //bool useIs = bool.Parse(config.GetSection("UseIS").Value);

            //if (useIs)
            //{
            //    return;
            //}

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.Authority = "http://localhost:6050"; // Auth Server
                o.Audience = "resourceapi"; // API Resource Id
                o.RequireHttpsMetadata = false;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiReader", policy => policy.RequireClaim("scope", "resourceapi"));
                options.AddPolicy("Admin", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
            });
        }

        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddScoped<IDataAdapter, DataAdapter>();
            services.AddSingleton(AppMapperConfig.GetMapper());
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Lingva API",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "Serhii Behma",
                        Email = string.Empty,
                        //Url = "https://twitter.com/spboyer"
                    },
                    License = new License
                    {
                        Name = "Use under LICX",
                        //Url = "https://example.com/license"
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        public static void ConfigureOptions(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<StorageOptions>(config.GetSection("StorageConfig"));
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }

        public static void ConfigureDbProvider(this IServiceCollection services, IConfiguration config)
        {
            DbProviders dbProvider = (DbProviders)Enum.Parse(typeof(DbProviders), config.GetSection("Selectors:DbProvider").Value, true);

            switch (dbProvider)
            {
                case DbProviders.Dapper:
                    services.ConfigureDapper();
                    break;
                case DbProviders.Mongo:
                    services.ConfigureMongo();
                    break;
                case DbProviders.CosmosSqlApi:
                    services.ConfigureAzureCosmosDB();
                    break;
                default:
                    services.ConfigureEF(config);
                    break;
            }
        }

        public static void ConfigureEF(this IServiceCollection services, IConfiguration config)
        {
            services.ConfigureEFContext(config);
            services.ConfigureEFRepositories();
        }

        public static void ConfigureDapper(this IServiceCollection services)
        {
            services.AddScoped<DapperContext>();
            services.ConfigureDapperRepositories();
        }

        public static void ConfigureMongo(this IServiceCollection services)
        {
            services.AddTransient<MongoContext>();
            services.ConfigureMongoRepositories();
        }

        public static void ConfigureAzureCosmosDB(this IServiceCollection services)
        {
            services.AddTransient<CosmosSqlApiContext>();
            services.ConfigureAzureCosmosDBRepositories();
        }

        public static void ConfigureEFContext(this IServiceCollection services, IConfiguration config)
        {
            string connectionStringValue = config.GetConnectionString("LingvaEFConnection");

            services.AddDbContext<DictionaryContext>(options =>
            {
                options.UseSqlServer(connectionStringValue);
                options.UseLazyLoadingProxies();
            });
        }

        public static void ConfigureEFRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
        }

        public static void ConfigureDapperRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository, DAL.Dapper.Repositories.Repository>();
            services.AddScoped<IGroupRepository, DAL.Dapper.Repositories.GroupRepository>();
        }

        public static void ConfigureMongoRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository, DAL.Mongo.Repositories.Repository>();
            services.AddScoped<IGroupRepository, DAL.Mongo.Repositories.GroupRepository>();
        }

        public static void ConfigureAzureCosmosDBRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository, DAL.CosmosSqlApi.Repositories.Repository>();
            services.AddScoped<IGroupRepository, DAL.CosmosSqlApi.Repositories.GroupRepository>();
        }

        public static void ConfigureManagers(this IServiceCollection services)
        {
            services.AddScoped<IGroupManager, GroupManager>();
            services.AddScoped<IInfoManager, InfoManager>();
            services.AddScoped<IFileStorageManager, FileStorageManager>();
        }

        public static void ConfigureDataAdapters(this IServiceCollection services)
        {
            services.AddScoped<QueryOptionsAdapter>();
        }
    }
}