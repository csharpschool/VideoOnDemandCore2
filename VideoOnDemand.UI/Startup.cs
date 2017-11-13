using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VideoOnDemand.UI.Models;
using VideoOnDemand.UI.Services;
using VideoOnDemand.Data.Data.Entities;
using VideoOnDemand.Data.Data;
using VideoOnDemand.UI.Repositories;
using VideoOnDemand.UI.Models.DTOModels;
using VideoOnDemand.Data.Services;

namespace VideoOnDemand.UI
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
            services.AddDbContext<VODContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<VODContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            //services.AddSingleton<IReadRepository, MockReadRepository>();
            services.AddScoped<IReadRepository, SqlReadRepository>();
            services.AddTransient<IDbReadService, DbReadService>();

            services.AddMvc();

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Video, VideoDTO>();

                cfg.CreateMap<Instructor, InstructorDTO>()
                    .ForMember(dest => dest.InstructorName,
                        src => src.MapFrom(s => s.Name))
                    .ForMember(dest => dest.InstructorDescription,
                        src => src.MapFrom(s => s.Description))
                    .ForMember(dest => dest.InstructorAvatar,
                        src => src.MapFrom(s => s.Thumbnail));

                cfg.CreateMap<Download, DownloadDTO>()
                    .ForMember(dest => dest.DownloadUrl,
                        src => src.MapFrom(s => s.Url))
                    .ForMember(dest => dest.DownloadTitle,
                        src => src.MapFrom(s => s.Title));

                cfg.CreateMap<Course, CourseDTO>()
                    .ForMember(dest => dest.CourseId, src =>
                        src.MapFrom(s => s.Id))
                    .ForMember(dest => dest.CourseTitle,
                        src => src.MapFrom(s => s.Title))
                    .ForMember(dest => dest.CourseDescription,
                        src => src.MapFrom(s => s.Description))
                    .ForMember(dest => dest.MarqueeImageUrl,
                        src => src.MapFrom(s => s.MarqueeImageUrl))
                    .ForMember(dest => dest.CourseImageUrl,
                        src => src.MapFrom(s => s.ImageUrl));

                cfg.CreateMap<Module, ModuleDTO>()
                    .ForMember(dest => dest.ModuleTitle,
                        src => src.MapFrom(s => s.Title));
            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
