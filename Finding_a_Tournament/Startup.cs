using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Finding_a_Tournament.Infrastructure.Data;
using Finding_a_Tournament.Domain.interfaces;
using  Infrastructure.RepositoryClubs;
using Microsoft.AspNetCore.Http;
using FluentValidation;
using Finding_a_Tournament.Domain.dto.Requests;
using Finding_a_Tournament.Infrastructure.Validators;

namespace Finding_a_Tournament
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Finding_a_Tournament", Version = "v1" });
            });
              services.AddDbContext<Finding_a_TournamentContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Finding_a_Tournament"))
            );
            services.AddTransient<Iclubs, RepositoryClubs>();
             services.AddTransient<Itorneo, RepositoryClubs>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IValidator<ClubsCreateRequest>,ClubsCreateRequestValidator>();
            services.AddScoped<IValidator<TorneoCreateRequest>,TorneosCreateRequestValidator>();
            //services.AddScoped<IValidator<TorneoCreateRequest>,TorneosCreateRequestValidator>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Finding_a_Tournament v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
