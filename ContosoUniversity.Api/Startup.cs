using ContosoUniversity.Data;
using ContosoUniversity.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using AutoMapper;
using ContosoUniversity.Api.Services;

namespace ContosoUniversity.Api
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
            services.AddAutoMapper(typeof(Startup));

            services.AddCors();

            services.AddMvc()
                .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            var schoolDbConnectionString = Configuration.GetConnectionString("schoolDbConnectionString");

            services.AddDbContext<SchoolContext>(options => options
                .UseLazyLoadingProxies()
                .UseSqlServer(schoolDbConnectionString));

            services.AddScoped<IStudentsRepository, StudentsRepository>();

            services.AddScoped<ICoursesRepository, CoursesRepository>();

            services.AddScoped<IEnrollmentsRepository, EnrollmentsRepository>();

            services.AddScoped<IStudentsService, StudentsesService>();

            services.AddScoped<IEnrollmentsService, EnrollmentsService>();

            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("v1", new Info
                {
                    Title = "Contoso University",
                    Version = "v1",
                    Description = "Services for Contoso University application"
                });

                o.EnableAnnotations();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<SchoolContext>();

                context.Database.Migrate();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
                builder.WithOrigins("http://localhost:4200"));

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Students");
                c.DocumentTitle = "Contoso University API Docs";
            });

            app.UseMvc();
        }
    }
}
