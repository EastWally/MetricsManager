using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsAgent.DAL;
using System.Data.SQLite;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Repositories;

namespace MetricsAgent
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            ConfigureSqlLiteConnection(services, new DBConfig());
            services.AddSingleton<IDBConfig, DBConfig>();
            services.AddSingleton<ICpuMetricsRepository, CpuMetricsRepository>();
            services.AddSingleton<IDotNetMetricsRepository, DotNetMetricsRepository>();
            services.AddSingleton<IHddMetricsRepository, HddMetricsRepository>();
            services.AddSingleton<INetworkMetricsRepository, NetworkMetricsRepository>();
            services.AddSingleton<IRamMetricsRepository, RamMetricsRepository>();
        }

        private void ConfigureSqlLiteConnection(IServiceCollection services, IDBConfig dBConfig)
        {
            string connectionString = dBConfig.ConnectionString;
            var connection = new SQLiteConnection(connectionString);
            connection.Open();
            PrepareSchema(connection);
        }

        private void PrepareSchema(SQLiteConnection connection)
        {
            using (var command = new SQLiteCommand(connection))
            {
                CreateTable(command, "cpumetrics", 10);
                CreateTable(command, "dotnetmetrics", 20);
                CreateTable(command, "hddmetrics", 30);
                CreateTable(command, "networkmetrics", 40);
                CreateTable(command, "rammetrics", 50);
            }
        }

        private void CreateTable(SQLiteCommand command, string tableName, int value)
        {
            command.CommandText = $"DROP TABLE IF EXISTS {tableName}";
            command.ExecuteNonQuery();

            command.CommandText = $"CREATE TABLE {tableName}(id INTEGER PRIMARY KEY, value INT, time INT)";
            command.ExecuteNonQuery();

            command.CommandText = $"INSERT INTO {tableName}(value, time) VALUES({value++},{DateTimeOffset.Now.AddDays(-3).ToUnixTimeSeconds()})";
            command.ExecuteNonQuery();
            command.CommandText = $"INSERT INTO {tableName}(value, time) VALUES({value++},{DateTimeOffset.Now.AddDays(-2).ToUnixTimeSeconds()})";
            command.ExecuteNonQuery();
            command.CommandText = $"INSERT INTO {tableName}(value, time) VALUES({value},{DateTimeOffset.Now.AddDays(-1).ToUnixTimeSeconds()})";
            command.ExecuteNonQuery();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
