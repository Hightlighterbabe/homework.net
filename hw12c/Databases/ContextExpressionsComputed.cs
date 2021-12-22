using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace hw12с.DataBases
{
    public class ContextExpressionsComputed: DbContext
    {
        private const string ConnectionString = "Data Source=localhost; Initial Catalog=CacheOfExps; Integrated Security=True";
        private readonly ILoggerFactory _loggerFactory = LoggerFactory.Create(config => config.AddConsole());

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory);
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        public DbSet<ExpressionsComputed> Items { get; set; }
        public new void SaveChanges() => base.SaveChanges();
    }
}