using Lingva.DAL.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Lingva.DAL.Mongo
{
    public class MongoContext
    {
        private readonly IMongoDatabase _database;
        private readonly IClientSessionHandle _session;

        public IClientSessionHandle Session { get => _session; }

        public IMongoCollection<Post> Posts { get => Set<Post>(); }
        public IMongoCollection<Language> Languages { get => Set<Language>(); }
        public IMongoCollection<User> Users { get => Set<User>(); }

        public MongoContext(IConfiguration config)
        {
            string connectionString = config.GetConnectionString("LingvaMongoConnection");
            MongoClient client = new MongoClient(connectionString);
            _session = client.StartSession();
            if (client != null)
            {
                _database = _session.Client.GetDatabase("lingva");
            }               
        }      

        public IMongoCollection<T> Set<T>() where T : class, new()
        {
            string collectionName = GetTableName<T>();
            return _database.GetCollection<T>(collectionName);
        }

        protected string GetTableName<T>()
        {
            string tableName = null;
            var property = typeof(MongoContext).GetProperties().Where(t => t.PropertyType == typeof(IMongoCollection<T>)).FirstOrDefault();

            if (property != null)
            {
                tableName = property.Name;
            }
            else
            {
                tableName = typeof(T).Name + "s";
            }

            return tableName;
        }

        public async Task InitializeAsync()
        {
            if (await Set<Language>().Find(_ => true).CountDocumentsAsync() == 0)
            {
                Language languageEn = new Language() { Id = 1, Name = "en", CreateDate = DateTime.Now, ModifyDate = DateTime.Now };
                Language languageRu = new Language() { Id = 2, Name = "ru", CreateDate = DateTime.Now, ModifyDate = DateTime.Now };
                Language[] languages = { languageEn, languageRu };

                await Set<Language>().InsertManyAsync(languages);
            }

            if(await Set<Post>().Find(_ => true).CountDocumentsAsync() == 0)
            {
                Post post1 = new Post { Id = 1, Title = "Harry Potter", CreateDate = DateTime.Now, ModifyDate = DateTime.Now, Date = DateTime.Now, Language = new Language() { Id = 1, Name = "en" }, PreviewText = "Good movie", FullText = "Good movie" };
                Post post2 = new Post { Id = 2, Title = "Librium", CreateDate = DateTime.Now, ModifyDate = DateTime.Now, Date = DateTime.Now, Language = new Language() { Id = 1, Name = "en" }, PreviewText = "Eq", FullText = "Good movie" };
                Post post3 = new Post { Id = 3, Title = "2Guns", CreateDate = DateTime.Now, ModifyDate = DateTime.Now, Date = DateTime.Now, Language = new Language() { Id = 2, Name = "ru" }, PreviewText = "stuff", FullText = "Good movie" };
                Post[] posts = { post1, post2, post3 };

                await Set<Post>().InsertManyAsync(posts);
            }
        }
    }
}
