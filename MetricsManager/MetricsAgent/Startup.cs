using AutoMapper;
using Dapper;
using MetricsAgent.DAL;
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
            connection.Execute("insert into cpumetrics (value, time) values (70, 12345678), (45, 154678765),(90, 523523498)");
            connection.Execute("CREATE TABLE dotnetmetrics (id INTEGER PRIMARY KEY, value INT, time INT)");
            connection.Execute("insert into dotnetmetrics (value, time) values (1, 5436546754), (2, 12453456),(3, 643265346),");
            connection.Execute("CREATE TABLE hddmetrics (id INTEGER PRIMARY KEY, value INT, time INT)");
            connection.Execute("insert into hddmetrics (value, time) values (3, 6346346546457), (2, 76573448688),(1, 75675678)");
            connection.Execute("CREATE TABLE networkmetrics (id INTEGER PRIMARY KEY, value INT, time INT)");
            connection.Execute("insert into networkmetrics (value, time) values (10, 7567438), (20, 658658437)");
            connection.Execute("CREATE TABLE rammetrics (id INTEGER PRIMARY KEY, value INT, time INT)");
            connection.Execute("insert into rammetrics (value, time) values (90, 5674388), (7, 856486458)");
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
