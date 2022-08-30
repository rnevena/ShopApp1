using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ShopApp1.Api.Core;
using ShopApp1.Application;
using ShopApp1.Application.Commands.Categories;
using ShopApp1.Application.Commands.Materials;
using ShopApp1.Application.Commands.Orders;
using ShopApp1.Application.Commands.Products;
using ShopApp1.Application.Commands.Users;
using ShopApp1.Application.Email;
using ShopApp1.Application.Queries.Categories;
using ShopApp1.Application.Queries.Materials;
using ShopApp1.Application.Queries.Orders;
using ShopApp1.Application.Queries.Products;
using ShopApp1.DataAccess;
using ShopApp1.Implementation.Commands.Categories;
using ShopApp1.Implementation.Commands.Materials;
using ShopApp1.Implementation.Commands.Orders;
using ShopApp1.Implementation.Commands.Products;
using ShopApp1.Implementation.Commands.Users;
using ShopApp1.Implementation.Email;
using ShopApp1.Implementation.Logging;
using ShopApp1.Implementation.Queries.Categories;
using ShopApp1.Implementation.Queries.Materials;
using ShopApp1.Implementation.Queries.Orders;
using ShopApp1.Implementation.Queries.Products;
using ShopApp1.Implementation.Validators.Categories;
using ShopApp1.Implementation.Validators.Materials;
using ShopApp1.Implementation.Validators.Orders;
using ShopApp1.Implementation.Validators.Products;
using ShopApp1.Implementation.Validators.Users;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp1.Api
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
            services.AddTransient<ShopApp1Context>();

            // group - test klasa
            //services.AddTransient<ICreateGroupCommand, CreateGroupCommand>();
            //services.AddTransient<IDeleteGroupCommand, DeleteGroupCommand>();
            //services.AddTransient<CreateGroupValidator>();


            // categories
            services.AddTransient<ICreateCategoryCommand, CreateCategoryCommand>();
            services.AddTransient<IDeleteCategoryCommand, DeleteCategoryCommand>();
            services.AddTransient<IUpdateCategoryCommand, UpdateCategoryCommand>();
            services.AddTransient<IGetCategoriesQuery, GetCategoriesQuery>();
            services.AddTransient<IGetOneCategoryQuery, GetOneCategoryQuery>();
            services.AddTransient<CreateCategoryValidator>();
            services.AddTransient<UpdateCategoryValidator>();
            services.AddTransient<DeleteCategoryValidator>();

            //materials
            services.AddTransient<ICreateMaterialCommand, CreateMaterialCommand>();
            services.AddTransient<IDeleteMaterialCommand, DeleteMaterialCommand>();
            services.AddTransient<IUpdateMaterialCommand, UpdateMaterialCommand>();
            services.AddTransient<IGetMaterialsQuery, GetMaterialsQuery>();
            services.AddTransient<IGetOneMaterialQuery, GetOneMaterialQuery>();
            services.AddTransient<CreateMaterialValidator>();
            services.AddTransient<DeleteMaterialValidator>();
            services.AddTransient<UpdateMaterialValidator>();

            //products
            services.AddTransient<ICreateProductCommand, CreateProductCommand>();
            services.AddTransient<IDeleteProductCommand, DeleteProductCommand>();
            services.AddTransient<IUpdateProductCommand, UpdateProductCommand>();
            services.AddTransient<IGetProductsQuery, GetProductsQuery>();
            services.AddTransient<IGetOneProductQuery, GetOneProductQuery>();
            services.AddTransient<CreateProductValidator>();

            //services.AddTransient<IDbConnection>(db => new SqlConnection(
            //        Configuration.GetConnectionString("Data Source=localhost;Initial Catalog=ShopApp4;Integrated Security=True")));

            //orders
            services.AddTransient<ICreateOrderCommand, CreateOrderCommand>();
            services.AddTransient<IGetOrdersQuery, GetOrdersQuery>();
            services.AddTransient<CreateOrderValidator>();

            //user
            services.AddTransient<IRegisterUserCommand, RegisterUserCommand>();
            services.AddTransient<ICreateUserCommand, CreateUserCommand>();
            services.AddTransient<IDeleteUserCommand, DeleteUserCommand>();
            services.AddTransient<IUpdateUserCommand, UpdateUserCommand>();
            services.AddTransient<RegisterValidator>();

            // autorizacija
            services.AddHttpContextAccessor();
            services.AddTransient<IApplicationActor>(x=>
            {
                var accessor = x.GetService<IHttpContextAccessor>();
                var user = accessor.HttpContext.User;

                if (user.FindFirst("ActorData") == null)
                {
                    return new AnonymousActor();
                }

                var actorString = user.FindFirst("ActorData").Value;

                var actor = JsonConvert.DeserializeObject<JwtActor>(actorString);

                return actor;
            });
            services.AddTransient<IUseCaseLogger, ConsoleUseCaseLogger>();
            services.AddTransient<IUseCaseLogger, DatabaseUseCaseLogger>();
            services.AddTransient<UseCaseExecutor>();

            // jwt
            services.AddTransient<JwtManager>();

            //smtp
            services.AddTransient<IEmailSender, SmtpEmailSender>();

            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = "asp_api",
                    ValidateIssuer = true,
                    ValidAudience = "Any",
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsMyVerySecretKey")),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddControllers();

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseMiddleware<GlobalExceptionHandler>();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
