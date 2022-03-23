using AutoMapper;
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

            ConfigureSqlLiteConnection();
            
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
            connection.Execute("DROP TABLE IF EXISTS cpumetrics");
            connection.Execute("DROP TABLE IF EXISTS dotnetmetrics");
            connection.Execute("DROP TABLE IF EXISTS hddmetrics");
            connection.Execute("DROP TABLE IF EXISTS networkmetrics");
            connection.Execute("DROP TABLE IF EXISTS rammetrics");

            connection.Execute("CREATE TABLE cpumetrics (id INTEGER PRIMARY KEY, value INT, time INT)");
            connection.Execute("insert into cpumetrics (value, time) values (10, 1234567891), (11, 5432134567)");
            connection.Execute("CREATE TABLE dotnetmetrics (id INTEGER PRIMARY KEY, value INT, time INT)");
            connection.Execute("insert into dotnetmetrics (value, time) values (11, 1468189122), (12, 1564554789)");
            connection.Execute("CREATE TABLE hddmetrics (id INTEGER PRIMARY KEY, value INT, time INT)");
            connection.Execute("insert into hddmetrics (value, time) values (21, 1356795467), (31, 1895689509)");
            connection.Execute("CREATE TABLE networkmetrics (id INTEGER PRIMARY KEY, value INT, time INT)");
            connection.Execute("insert into networkmetrics (value, time) values (41, 6745695678), (51, 7654797880)");
            connection.Execute("CREATE TABLE rammetrics (id INTEGER PRIMARY KEY, value INT, time INT)");
            connection.Execute("insert into rammetrics (value, time) values (61, 15436789123), (71,1235643567 )");
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

