using BusinessObject.Model;
using DataAccess;
using DataAccess.DAO;
using DataAccess.Repository;
using DinkToPdf.Contracts;
using DinkToPdf;
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
using Repository.IRepository;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;
using System.IO;
using System.Text;

namespace API
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
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });
            services.AddTransient<UserIRepository, UserRepository>();
            services.AddTransient<UserDAO>();
            services.AddTransient<AccountIRepository, AccountRepository>();
            services.AddTransient<AccountDAO>();
            services.AddTransient<ExamIRepository, ExamRepository>();
            services.AddTransient<ExamDAO>();
            services.AddTransient<OptionIRepository, OptionRepository>();
            services.AddTransient<OptionDAO>();
            services.AddTransient<QuestionIRepository, QuestionRepository>();
            services.AddTransient<QuestionDAO>();
            services.AddTransient<UserAnswerIRepository, UserAnswerRepository>();
            services.AddTransient<UserAnswerDAO>();
            services.AddTransient<UserExamIRepository, UserExamRepository>();
            services.AddTransient<ExamUserDAO>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
            services.AddDbContext<DBContext>(options =>
    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            //}

            var exportsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Exports");
            if (!Directory.Exists(exportsDirectory))
            {
                Directory.CreateDirectory(exportsDirectory);
            }
            var physicalProvider = new PhysicalFileProvider(exportsDirectory);
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = physicalProvider
            });

            //app.UseHttpsRedirection();
            app.UseCors("AllowAllOrigins");
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
