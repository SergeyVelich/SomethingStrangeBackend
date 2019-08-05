using Lingva.DAL.EF.EntityConfigurations;
using Lingva.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Lingva.DAL.EF.Context
{
    [ExcludeFromCodeCoverage]
    public class DictionaryContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<User> Users { get; set; }

        public DictionaryContext(DbContextOptions<DictionaryContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new LanguageConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.ApplyConfiguration(new PostTagConfiguration());

            Seed(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        public virtual void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Language>().HasData(
                new { Id = 1, Name = "en", CreateDate = DateTime.Now, ModifyDate = DateTime.Now },
                new { Id = 2, Name = "ru", CreateDate = DateTime.Now, ModifyDate = DateTime.Now });

            modelBuilder.Entity<Tag>().HasData(
                new { Id = 1, Name = "coding", CreateDate = DateTime.Now, ModifyDate = DateTime.Now },
                new { Id = 2, Name = "history", CreateDate = DateTime.Now, ModifyDate = DateTime.Now });

            modelBuilder.Entity<User>().HasData(
                new { Id = 1, Name = "Serhii", CreateDate = DateTime.Now, ModifyDate = DateTime.Now, Email = "veloceraptor89@gmail.com" },
                new { Id = 2, Name = "Eugeniya", CreateDate = DateTime.Now, ModifyDate = DateTime.Now, Email = "tucker_serega@mail.ru" });

            modelBuilder.Entity<Post>().HasData(
                new { Id = 1, Title = "Harry Potter", CreateDate = DateTime.Now, ModifyDate = DateTime.Now, Date = DateTime.Now, LanguageId = 1, AuthorId = 1, PreviewText = "Good movie", FullText = "Good movie" },
                new { Id = 2, Title = "Librium", CreateDate = DateTime.Now, ModifyDate = DateTime.Now, Date = DateTime.Now, LanguageId = 1, AuthorId = 2, PreviewText = "Eq", FullText = "Good movie" },
                new { Id = 3, Title = "2Guns", CreateDate = DateTime.Now, ModifyDate = DateTime.Now, Date = DateTime.Now, LanguageId = 2, AuthorId = 1, PreviewText = "stuff", FullText = "Good movie" });

            modelBuilder.Entity<PostTag>().HasData(
                new { Id = 1, CreateDate = DateTime.Now, ModifyDate = DateTime.Now, PostId = 1, TagId = 1 },
                new { Id = 2, CreateDate = DateTime.Now, ModifyDate = DateTime.Now, PostId = 1, TagId = 2 });
        }

        public async Task InitializeAsync()
        {
            await Database.MigrateAsync();
        }
    }
}
