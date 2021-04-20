using CarDealership.Helpers;
using CarDealership.Models;
using CarDealership.Repositories;
using CarDealership.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace CarDealership
{
    public class Startup
    {
        public IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers().AddNewtonsoftJson();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddDbContext<DealershipContext>(opt =>opt.UseInMemoryDatabase("CarDealership"));

            // configure strongly typed settings objects
            var appSettingsSection = _configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure DI for application services
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<ILookupService, LookupService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CarDealership", Version = "v1" });
            });

            services.AddCors(o => o.AddPolicy("AllowAllPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CarDealership"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllowAllPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            seedDatabase(app);
        }


        // Seed database with data
        private void seedDatabase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DealershipContext>();

                seedLookupTable<BodyStyle, BodyStyles>(context);
                seedLookupTable<TransmissionType, TransmissionTypes>(context);

                // Changes made to include MPG in the entries

                context.Cars.Add(new Car(new Guid("3f7c5757-23f6-4160-8ccb-fc60a741af81")) { Make = "Hyundai", Model = "Genesis", Year = 2014, Price = 35000, Horsepower = 333, BodyStyle = BodyStyles.Coupe, TransmissionType = TransmissionTypes.Manual, MilesPerGallon = 30});
                context.Cars.Add(new Car() { Make = "Nissan", Model = "Quest", Year = 2008, Price = 6000, Horsepower = 235, BodyStyle = BodyStyles.Minivan, TransmissionType = TransmissionTypes.Automatic, MilesPerGallon = 28 });
                context.Cars.Add(new Car() { Make = "INFINITI", Model = "QX60", Year = 2016, Price = 26500, Horsepower = 295, BodyStyle = BodyStyles.SUV, TransmissionType = TransmissionTypes.CVT, MilesPerGallon = 34 });
                context.Cars.Add(new Car() { Make = "Ford", Model = "Mustang", Year = 2015, Price = 18500, Horsepower = 300, BodyStyle = BodyStyles.Coupe, TransmissionType = TransmissionTypes.Automatic, MilesPerGallon = 26 });
                context.Cars.Add(new Car() { Make = "Toyota", Model = "Corolla", Year = 2017, Price = 13500, Horsepower = 132, BodyStyle = BodyStyles.Sedan, TransmissionType = TransmissionTypes.Automatic, MilesPerGallon = 38 });
                context.Cars.Add(new Car() { Make = "Ford", Model = "F-150", Year = 2021, Price = 75945, Horsepower = 430, BodyStyle = BodyStyles.PickupTruck, TransmissionType = TransmissionTypes.Automatic, MilesPerGallon = 18 });
                
                context.SaveChanges();
            }
        }

        // This method inspects the values in an enum and seeds the corresponding lookup table in the database with values to match
        private void seedLookupTable<TLookupEntity, TEnum>(DealershipContext context)
            where TLookupEntity : LookupBase, new()
            where TEnum : IConvertible
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException("TEnum must be an enumerated type.");
            }

            foreach (var enumValue in Enum.GetValues(typeof(TEnum)))
            {
                if (typeof(TLookupEntity) == typeof(BodyStyle))
                {
                    context.BodyStyles.Add(new BodyStyle { Id = (int)enumValue, Name = enumValue.ToString() });
                }

                if (typeof(TLookupEntity) == typeof(TransmissionType))
                {
                    context.TransmissionTypes.Add(new TransmissionType { Id = (int)enumValue, Name = enumValue.ToString() });
                }
            }
        }
    }
}
