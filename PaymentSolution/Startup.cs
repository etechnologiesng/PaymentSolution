﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PaymentSolution.Common;
using PaymentSolution.Core.Application.Interface;
using PaymentSolution.Core.Entity;
using PaymentSolution.Core.Service;
using PaymentSolution.Data;

namespace PaymentSolution
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            var appSettings = Configuration.GetSection("AppSettings").Get<AppSettings>();
            services.AddTransient<IPayment, PaymentService>();

            services.AddDbContext<PaymentDbContext>(op => op.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddIdentity<AppUser, AppRole>(op =>
            {
                op.Password.RequiredLength = 6;


            }).AddEntityFrameworkStores<PaymentDbContext>();
            services.AddAuthorization(op => op.AddPolicy("First", po =>
             {
                 po.RequireRole("Admin", "Customer");
             }));
            var key = Encoding.UTF8.GetBytes(Configuration["AppSettings:JWT_Secret"].ToString());
            services.AddAuthentication(x =>
            {

                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }

                ).AddJwtBearer(x =>
                {
                    //This should be set to true in production environment
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = false;

                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                        
                    };
                }
                );
         
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new Info { Title = "Naija Event API using Screaming Architecture", Version = "v1" });


            //});
        }
                    
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
