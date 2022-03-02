﻿using AutoMapper;
using Dapper;
using MetricsAgent.DAL;
using MetricsAgent.DAL.Interface;
using MetricsAgent.Interface;
using MetricsAgent.Repositories;
using Microsoft.OpenApi.Models;
using System.Data.SQLite;

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
            var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();

            services.AddControllers();
            services.AddControllersWithViews();
            services.AddSingleton<ICpuMetricsRepository, CpuMetricsRepository>();

            services.AddSingleton<IDotNetMetricsRepository, DotNetMetricsRepository>();

            services.AddSingleton<IHddMetricsRepository, HddMetricsRepository>();

            services.AddSingleton<INetworkMetricsRepository, NetworkMetricsRepository>();

            services.AddSingleton<IRamMetricsRepository, RamMetricsRepository>();

            services.AddSingleton<IConnectionManager, ConnectionManager>();

            services.AddSingleton(mapper);

            
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "MetricsManager",
                    Description = "Отслеживает состояние параметров системы",
                    Contact = new OpenApiContact
                    {
                        Name = "Денисенко Марк",
                        Email = "Morearti1726263@yandex.ru"
                    },
                    Version = "v1"
                });
            });
        }

        private void ConfigureSqlLiteConnection()
        {
            using (var connection = new SQLiteConnection(ConnectionManager.ConnectionString))
            {
                PrepareSchema(connection);
            }
        }

        private void PrepareSchema(SQLiteConnection connection)
        {
            using var command = new SQLiteCommand(connection);

            command.CommandText = "DROP TABLE IF EXISTS cpumetrics";
            command.ExecuteNonQuery();
            command.CommandText = @"CREATE TABLE cpumetrics(id INTEGER PRIMARY KEY, value INT, time INT64)";
            command.ExecuteNonQuery();
         
            command.CommandText = "DROP TABLE IF EXISTS dotnetmetrics";
            command.ExecuteNonQuery();
            command.CommandText = @"CREATE TABLE dotnetmetrics(id INTEGER PRIMARY KEY, value INT, time INT64)";
            command.ExecuteNonQuery();
            
            command.CommandText = "DROP TABLE IF EXISTS hddmetrics";
            command.ExecuteNonQuery();
            command.CommandText = @"CREATE TABLE hddmetrics(id INTEGER PRIMARY KEY, value INT, time INT64)";
            command.ExecuteNonQuery();
            
            command.CommandText = "DROP TABLE IF EXISTS networkmetrics";
            command.ExecuteNonQuery();
            command.CommandText = @"CREATE TABLE networkmetrics(id INTEGER PRIMARY KEY, value INT, time INT64)";
            command.ExecuteNonQuery();
            
            command.CommandText = "DROP TABLE IF EXISTS rammetrics";
            command.ExecuteNonQuery();
            command.CommandText = @"CREATE TABLE rammetrics(id INTEGER PRIMARY KEY, value INT, time INT64)";
            command.ExecuteNonQuery();
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MetricsAgent v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

        }
    }
}

