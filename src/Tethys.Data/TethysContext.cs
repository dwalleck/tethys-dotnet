
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Npgsql;

using Tethys.Models;

namespace Tethys.Data;

public class TethysContext : DbContext
    {
        public DbSet<TestResult> TestResults => Set<TestResult>();

        public TethysContext()
        {
            NpgsqlConnection.GlobalTypeMapper.MapEnum<ResultFormat>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<TestStatus>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<TestTrigger>();
            //NpgsqlConnection.GlobalTypeMapper.MapEnum<Project>();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasPostgresEnum<ResultFormat>();
            builder.HasPostgresEnum<ApplicationLanguage>();
            builder.HasPostgresEnum<TestStatus>();
            builder.HasPostgresEnum<TestTrigger>();
            builder.HasPostgresEnum<Project>();
            builder.Entity<TestResult>()
                .HasIndex(tr => tr.Name);
            builder.Entity<TestResult>()
                .HasIndex(tr => tr.RunAt);
            builder.Entity<TestResult>()
                .HasIndex(tr => tr.Status);
            builder.Entity<TestResult>()
                .HasIndex(tr => tr.Trigger);
            // builder.Entity<TestResult>()
            //     .HasIndex(tr => tr.Project);
        }

        private static ILoggerFactory ContextLoggerFactory
        {
            get
            {
                return LoggerFactory.Create(b => b.AddConsole().AddFilter("", LogLevel.Information));
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configBuilder = new ConfigurationBuilder()
                .AddEnvironmentVariables();
            var config = configBuilder.Build();
            optionsBuilder
                .UseNpgsql($"Host={config["HOST"]};Database={config["DATABASE"]};Username={config["DBUSER"]};Password={config["PASSWORD"]}", o => o.UseNodaTime())
                .UseSnakeCaseNamingConvention();
                //.UseLoggerFactory(ContextLoggerFactory);
        }
    }
