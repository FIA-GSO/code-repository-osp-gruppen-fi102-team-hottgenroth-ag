using Logisitcs.BLL;
using Logisitcs.BLL.Factories;
using Logisitcs.BLL.Helper;
using Logisitcs.BLL.Interfaces;
using Logisitcs.BLL.Interfaces.Factories;
using Logisitcs.BLL.Interfaces.ModelInterfaces;
using Logisitcs.BLL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Logistics.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private IWebHostEnvironment CurrentEnvironment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            CurrentEnvironment = env;

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);//required for the font loading in itext7 (used for pdf generation)
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Register Mimetypes
            services.AddResponseCompression(options =>
            {
                options.MimeTypes = new[] {
               "application/json"
            };
                options.EnableForHttps = true;
            });

            #region BLL_DAL_DI

            //DAL
            services.AddTransient<IProjectDataFactory>(provider => new ProjectDataFactory());
            services.AddTransient<IProjectFactory>(provider => new ProjectFactory());
            services.AddTransient<ITransportBoxDataFactory>(provider => new TransportBoxDataFactory());
            services.AddTransient<ITransportboxFactory>(provider => new TransportboxFactory());

            //Helper
            services.AddTransient<PdfHelper>(provider => new PdfHelper());

            //BLL
            services.AddTransient<IProjectBll>(provider => new ProjectBll(provider.GetService<IProjectDataFactory>(), provider.GetService<IProjectFactory>()));
            services.AddTransient<ITransportboxBll>(provider => new TransportboxBLL(provider.GetService<ITransportBoxDataFactory>(), provider.GetService<ITransportboxFactory>()));
            services.AddTransient<IArticleBll>(provider => new ArticleBll());
            services.AddTransient<IPDFBLL>(provider => new PDFBLL(provider.GetService<PdfHelper>()));
            services.AddTransient<ILoginBll>(provider => new LoginBll());

            #endregion BLL_DAL_DI

            #region Json

            // Add Newtonsoft JSON
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                // Configure a custom converter
                /*options.SerializerSettings.Converters.Add(new JsonConverter<INTERFACE, OBJECT>());*/
                options.SerializerSettings.Converters.Add(new JsonConverter<IUserData, UserData>());
                options.SerializerSettings.Converters.Add(new JsonConverter<ILoginData, LoginData>());
                options.SerializerSettings.Converters.Add(new JsonConverter<IProjectData, ProjectData>());
                options.SerializerSettings.Converters.Add(new JsonConverter<ITransportBoxData, TransportBoxData>());
                options.SerializerSettings.Converters.Add(new JsonConverter<IPdfData, PdfData>());
                options.SerializerSettings.Converters.Add(new JsonConverter<IArticleData, ArticleData>());

                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            });

            #endregion Json

                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,
                      ValidIssuer = Configuration["Jwt:Issuer"],
                      ValidAudience = Configuration["Jwt:Issuer"],
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                  };
               });

                services.AddMvc();

          

                services.AddSwaggerGen(opt =>
                  {
                 opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Logistics.API", Version = "v1" });
                 opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                 {
                     In = ParameterLocation.Header,
                     Description = "Please enter token",
                     Name = "Authorization",
                     Type = SecuritySchemeType.Http,
                     BearerFormat = "JWT",
                     Scheme = "bearer"
                 });
                 opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                  {
                     new OpenApiSecurityScheme
                     {
                        Reference = new OpenApiReference
                        {
                           Type = ReferenceType.SecurityScheme,
                           Id = "Bearer"
                        }
                     },
                     new string[] { }
                     }
                  });
                  opt.CustomSchemaIds(type => type.ToString());
                });

                services.AddHttpClient();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();
            app.UseCors(options =>
               options.WithOrigins("http://localhost:4200", "https://localhost:44349")
               .AllowAnyMethod()
               .AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();

                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.)
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WApp.Server V1");
                });
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}